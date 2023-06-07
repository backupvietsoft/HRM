﻿using Aspose.Words;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.Templates;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using Vs.Report;

namespace Vs.HRM
{
    public partial class frmInQuyetDinhThoiViec : DevExpress.XtraEditors.XtraForm
    {
        private int iID_CN = 494;
        private int iID_QDTV = 13;
        DateTime dtNgayThoiViec;
        public frmInQuyetDinhThoiViec(int ID_QDTV, int ID_CN, DateTime ngaythoiviec)
        {
            InitializeComponent();
            iID_CN = ID_CN;
            iID_QDTV = ID_QDTV;
            dtNgayThoiViec = ngaythoiviec;
        }
        //sự kiên load form
        private void frmInQuyetDinhThoiViec_Load(object sender, EventArgs e)
        {
            if (Commons.Modules.KyHieuDV == "SB")
            {
                rdo_ChonBaoCao.Properties.Items.RemoveAt(5);
                rdo_ChonBaoCao.Properties.Items.RemoveAt(4);
                rdo_ChonBaoCao.Properties.Items.RemoveAt(3);
                this.Size = new Size(450, 200);
                tablePanel1.Columns[2].Visible = true;
            }
            else
            {
                tablePanel1.Columns[2].Visible = false;
                this.Size = new Size(500, 250);
            }
            rdo_ChonBaoCao.SelectedIndex = 0;
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }
        //sự kiện các nút xử lí
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "In":
                    {
                        switch (rdo_ChonBaoCao.SelectedIndex)
                        {
                            // Quyết định thôi việc
                            case 0:
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                QuyetDinhThoiViec();
                                                break;
                                            }
                                        case "SB":
                                            {
                                                QuyetDinhThoiViec_SB();
                                                break;
                                            }
                                        default:
                                            QuyetDinhThoiViec();
                                            break;
                                    }

                                }
                                break;
                            //Quyết định sa thải
                            case 1:
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                QuyetDinhSaThai();
                                                break;
                                            }
                                        case "SB":
                                            {
                                                QuyetDinhThoiViecTroCap_SB();
                                                break;
                                            }
                                        default:
                                            QuyetDinhSaThai();
                                            break;
                                    }

                                }
                                break;
                            //Quyết định thanh lý hợp đồng trước năm 2008
                            case 2:
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                ThanhLyHDTruoc2008();
                                                break;
                                            }
                                        case "SB":
                                            {
                                                QuyetDinhThoiViecBoViec_SB();
                                                break;
                                            }
                                        default:
                                            ThanhLyHDTruoc2008();
                                            break;
                                    }

                                }
                                break;
                            //Quyết định thanh lý hợp đồng sau năm 2008 có trợ cấp
                            case 3:
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                ThanhLyHDSau2008();
                                                break;
                                            }
                                        default:
                                            ThanhLyHDSau2008();
                                            break;
                                    }

                                    break;
                                }
                            //Quyết định sa thải có trợ cấp
                            case 4:
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                QuyetDinhSaThaiTroCap();
                                                break;
                                            }
                                        default:
                                            QuyetDinhSaThaiTroCap();
                                            break;
                                    }

                                }
                                break;
                            //Quyết định thôi việc vi phạm thời gian báo trước
                            case 5:
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                QuyetDinhThoiViecVPThoiGian();
                                                break;
                                            }
                                        default:
                                            QuyetDinhThoiViecVPThoiGian();
                                            break;
                                    }

                                }
                                break;

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
        private void QuyetDinhThoiViec()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptQuyetDinhThoiViec(DateTime.Now, 1);

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhThoiViec", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@KHDV", SqlDbType.NVarChar).Value = Commons.Modules.ObjSystems.KyHieuDV_CN(Convert.ToInt64(iID_CN));
                cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = iID_QDTV;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = iID_CN;
                cmd.Parameters.Add("@NgayThoiViec", SqlDbType.DateTime).Value = dtNgayThoiViec;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
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
        private void QuyetDinhSaThai()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptQuyetDinhSaThai(DateTime.Now, 1);

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhThoiViec", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@KHDV", SqlDbType.NVarChar).Value = "MT";
                cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = iID_QDTV;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = iID_CN;
                cmd.Parameters.Add("@NgayThoiViec", SqlDbType.DateTime).Value = dtNgayThoiViec;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
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
        private void ThanhLyHDTruoc2008()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptQuyetDinhThanhLyHDTruoc2008(DateTime.Now, 1);

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhThoiViec", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@KHDV", SqlDbType.NVarChar).Value = "MT";
                cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = iID_QDTV;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = iID_CN;
                cmd.Parameters.Add("@NgayThoiViec", SqlDbType.DateTime).Value = dtNgayThoiViec;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
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
        private void ThanhLyHDSau2008()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptQuyetDinhThoiViecTroCap(DateTime.Now, 1);

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhThoiViec", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@KHDV", SqlDbType.NVarChar).Value = "MT";
                cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = iID_QDTV;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = iID_CN;
                cmd.Parameters.Add("@NgayThoiViec", SqlDbType.DateTime).Value = dtNgayThoiViec;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
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
        private void QuyetDinhSaThaiTroCap()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptQuyetDinhSaThaiTroCap(DateTime.Now, 1);

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhThoiViec", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@KHDV", SqlDbType.NVarChar).Value = "MT";
                cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = iID_QDTV;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = iID_CN;
                cmd.Parameters.Add("@NgayThoiViec", SqlDbType.DateTime).Value = dtNgayThoiViec;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
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
        private void QuyetDinhThoiViecVPThoiGian()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptQuyetDinhViPhamTGHopDong(DateTime.Now, 1);

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhThoiViec", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@KHDV", SqlDbType.NVarChar).Value = "MT";
                cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = iID_QDTV;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = iID_CN;
                cmd.Parameters.Add("@NgayThoiViec", SqlDbType.DateTime).Value = dtNgayThoiViec;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
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
        private void QuyetDinhThoiViec_SB()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                string sPathFile = "Template\\TemplateSB\\QuyetDinhThoiViec.docx";
                int NNgu = 0;
                if (chkTiengAnh.Checked == true)
                {
                    NNgu = 1;
                    sPathFile = "Template\\TemplateSB\\QuyetDinhThoiViecTA.docx";
                }

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhThoiViec_SB", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = NNgu;
                cmd.Parameters.Add("@KHDV", SqlDbType.NVarChar).Value = "SB";
                cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = iID_QDTV;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = iID_CN;
                cmd.Parameters.Add("@NgayThoiViec", SqlDbType.DateTime).Value = dtNgayThoiViec;
                cmd.Parameters.Add("@Lydo", SqlDbType.Int).Value = 1;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                DataRow row = dt.Rows[0];

                string sPath = "";

                if (!System.IO.Directory.Exists("Report")) // kiểm tra xem forder đã có chưa , nếu chưa có thì tạo 
                {
                    System.IO.Directory.CreateDirectory("Report");
                }
                sPath = "Report\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".docx";

                Document baoCao = new Document(sPathFile);

                //fill vào báo cáo
                //var date = Convert.ToDateTime(row["NGAY_BAT_DAU_HD"]);
                //baoCao.MailMerge.Execute(new[] { "NGAY_BD_HD" }, new[] { string.Format("ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year) });
                //baoCao.MailMerge.Execute(new[] { "NGAY_KY" }, new[] { string.Format("ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year) });
                //baoCao.MailMerge.Execute(new[] { "NGAY_KT_HD" }, new[] { string.Format("ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year) });
                foreach (DataColumn item in dt.Columns)
                {
                    if (Commons.Modules.ObjSystems.IsnullorEmpty(row[item]))
                    {
                        baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { "..." });

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
                baoCao.SaveAndOpenFile(sPath);
            }
            catch (Exception ex) { Commons.Modules.ObjSystems.MsgError(ex.Message); }
        }
        private void QuyetDinhThoiViec_NB()
        {
            System.Data.SqlClient.SqlConnection conn;
            frmViewReport frm = new frmViewReport();
            int NNgu = 0;
            if (chkTiengAnh.Checked == false)
            {
                frm.rpt = new rptQuyetDinhThoiViec_SB(DateTime.Now, 1);
                NNgu = 0;
            }
            else
            {
                frm.rpt = new rptQuyetDinhThoiViecTiengAnh_SB(DateTime.Now, 1);
                NNgu = 1;
            }

            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhThoiViec_SB", conn);
            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = NNgu;
            cmd.Parameters.Add("@KHDV", SqlDbType.NVarChar).Value = "SB";
            cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = iID_QDTV;
            cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = iID_CN;
            cmd.Parameters.Add("@NgayThoiViec", SqlDbType.DateTime).Value = dtNgayThoiViec;
            cmd.CommandType = CommandType.StoredProcedure;

            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0].Copy();
            dt.TableName = "DATA";
            frm.AddDataSource(dt);

        }
        private void QuyetDinhThoiViecTroCap_SB()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                frmViewReport frm = new frmViewReport();
                int NNgu = 0;
                if (chkTiengAnh.Checked == false)
                {
                    frm.rpt = new rptQuyetDinhThoiViecTroCap_SB(DateTime.Now, 1);
                    NNgu = 0;
                }
                else
                {
                    frm.rpt = new rptQuyetDinhThoiViecTroCapTiengAnh_SB(DateTime.Now, 1);
                    NNgu = 1;
                }

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhThoiViec_SB", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = NNgu;
                cmd.Parameters.Add("@KHDV", SqlDbType.NVarChar).Value = "SB";
                cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = iID_QDTV;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = iID_CN;
                cmd.Parameters.Add("@NgayThoiViec", SqlDbType.DateTime).Value = dtNgayThoiViec;
                cmd.Parameters.Add("@Lydo", SqlDbType.Int).Value = 1;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
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
        private void QuyetDinhThoiViecBoViec_SB()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                frmViewReport frm = new frmViewReport();
                int NNgu = 0;
                string sPathFile = "Template\\TemplateSB\\QuyetDinhBoViec.docx";
                if (chkTiengAnh.Checked == true)
                {
                    NNgu = 1;
                    sPathFile = "Template\\TemplateSB\\QuyetDinhBoViecTA.docx";
                }

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhThoiViec_SB", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = NNgu;
                cmd.Parameters.Add("@KHDV", SqlDbType.NVarChar).Value = "SB";
                cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = iID_QDTV;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = iID_CN;
                cmd.Parameters.Add("@NgayThoiViec", SqlDbType.DateTime).Value = dtNgayThoiViec;
                cmd.Parameters.Add("@Lydo", SqlDbType.Int).Value = 2;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                DataRow row = dt.Rows[0];

                string sPath = "";

                if (!System.IO.Directory.Exists("Report")) // kiểm tra xem forder đã có chưa , nếu chưa có thì tạo 
                {
                    System.IO.Directory.CreateDirectory("Report");
                }
                sPath = "Report\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".docx";

                Document baoCao = new Document(sPathFile);

                //fill vào báo cáo
                //var date = Convert.ToDateTime(row["NGAY_BAT_DAU_HD"]);
                //baoCao.MailMerge.Execute(new[] { "NGAY_BD_HD" }, new[] { string.Format("ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year) });
                //baoCao.MailMerge.Execute(new[] { "NGAY_KY" }, new[] { string.Format("ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year) });
                //baoCao.MailMerge.Execute(new[] { "NGAY_KT_HD" }, new[] { string.Format("ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year) });
                foreach (DataColumn item in dt.Columns)
                {
                    if (Commons.Modules.ObjSystems.IsnullorEmpty(row[item]))
                    {
                        baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { "..." });

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
                baoCao.SaveAndOpenFile(sPath);
            }
            catch (Exception ex) { Commons.Modules.ObjSystems.MsgError(ex.Message); }
        }
    }
}