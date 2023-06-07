﻿using Aspose.Words;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Commands.Internal;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Diagnostics;
using Vs.Report;

namespace Vs.HRM
{
    public partial class frmInLuongCN : DevExpress.XtraEditors.XtraForm
    {
        private long idCN;
        private long idL;
        private DateTime dNgayHL;
        public frmInLuongCN(Int64 idCongNhan, Int64 idLuong, DateTime ngayhl, string tencn)
        {
            InitializeComponent();
            NONN_HoTenCN.Text = tencn.ToUpper();
            idCN = idCongNhan;
            idL = idLuong;
            dNgayHL = ngayhl;
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }
        //sự kiên load form
        private void formInLuongCN_Load(object sender, EventArgs e)
        {
            rdo_ChonBaoCao.SelectedIndex = 0;
            dNgayIn.EditValue = DateTime.Today;
            Commons.OSystems.SetDateEditFormat(dNgayIn);
            if (Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString() == "DM")
            {
                rdo_ChonBaoCao.Properties.Items.RemoveAt(1);
            }

            if (Commons.Modules.KyHieuDV == "AP")
            {
                rdo_ChonBaoCao.Properties.Items.RemoveAt(0);
            }

        }
        //sự kiện các nút xử lí
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
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
                                case "rdo_QuyetDinhLuong":
                                    {
                                        #region in cu
                                        System.Data.SqlClient.SqlConnection conn;
                                        DataTable dt = new DataTable();
                                        try
                                        {
                                            frmViewReport frm = new frmViewReport();
                                            frm.rpt = new rptQuyetDinhLuongCN(dNgayIn.DateTime);
                                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn.Open();
                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhLuongCN", conn);
                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                                            cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idL;
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
                                        catch
                                        {

                                        }
                                        #endregion
                                        #region in mới

                                        #endregion
                                        break;
                                    }
                                case "rdo_QuaTrinhLuongCN":
                                    {

                                        System.Data.SqlClient.SqlConnection conn;
                                        DataTable dt = new DataTable();
                                        frmViewReport frm = new frmViewReport();
                                        try
                                        {
                                            frm.rpt = new rptQuaTrinhLuongCN(dNgayIn.DateTime);

                                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn.Open();

                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuaTrinhLuongCN", conn);
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
                                        }
                                        catch
                                        {

                                        }

                                        break;
                                    }
                                case "rdo_QuyetDinhNangLuong":
                                    {
                                        switch (Commons.Modules.KyHieuDV)
                                        {
                                            case "DM":
                                                {
                                                    System.Data.SqlClient.SqlConnection conn;
                                                    DataTable dt = new DataTable();
                                                    frmViewReport frm = new frmViewReport();
                                                    try
                                                    {
                                                        frm.rpt = new rptQuyetDinhNangLuong_DM(dNgayIn.DateTime);

                                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                        conn.Open();

                                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhNangLuong_DM", conn);
                                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                        cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = idCN;
                                                        cmd.Parameters.Add("@ID_SQD", SqlDbType.BigInt).Value = idL;
                                                        cmd.Parameters.Add("@NgayQD", SqlDbType.Date).Value = dNgayHL;
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
                                                    catch
                                                    {

                                                    }
                                                    break;
                                                }
                                            case "AP":
                                                {

                                                    try
                                                    {
                                                        //lấy data dữ liệu
                                                        DataTable dt = new DataTable();
                                                        dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "rptQuyetDinhNangLuong_AP", Commons.Modules.UserName, Commons.Modules.TypeLanguage,idCN,idL));
                                                        DataRow row = dt.Rows[0];
                                                        string sPath = "";
                                                        sPath = Commons.Modules.MExcel.SaveFiles("Work file (*.doc)|*.docx");
                                                        if (sPath == "") return;
                                                        //fill vào báo cáo
                                                        var dateHL = Convert.ToDateTime(row["NGAY_HIEU_LUC"]);
                                                        var date = DateTime.Now;
                                                        Document baoCao = new Document("Template\\TemplateAP\\QuyetDinhNangLuong.doc");
                                                        baoCao.MailMerge.Execute(new[] { "Ngay_Thang_Nam_BC" }, new[] { string.Format("ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year) });
                                                        baoCao.MailMerge.Execute(new[] { "Ngay_Thang_Nam_HL" }, new[] { string.Format("ngày {0} tháng {1} năm {2}", dateHL.Day, dateHL.Month, dateHL.Year) });
                                                        foreach (DataColumn item in dt.Columns)
                                                        {
                                                            if (Commons.Modules.ObjSystems.IsnullorEmpty(row[item]))
                                                            {
                                                                baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { "............." });
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
                                                    catch
                                                    {
                                                    }
                                                    break;
                                                }
                                            case "HN":
                                                {
                                                    System.Data.SqlClient.SqlConnection conn;
                                                    DataTable dt = new DataTable();
                                                    frmViewReport frm = new frmViewReport();
                                                    try
                                                    {
                                                        frm.rpt = new rptQuyetDinhLuongCN_HN(dNgayIn.DateTime);

                                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                        conn.Open();

                                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhNangLuong", conn);
                                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                        cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = idCN;
                                                        cmd.Parameters.Add("@ID_SQD", SqlDbType.BigInt).Value = idL;
                                                        cmd.Parameters.Add("@NgayQD", SqlDbType.Date).Value = dNgayHL;
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
                                                    catch
                                                    {

                                                    }
                                                    break;
                                                }

                                            default:
                                                {
                                                    System.Data.SqlClient.SqlConnection conn;
                                                    DataTable dt = new DataTable();
                                                    frmViewReport frm = new frmViewReport();
                                                    try
                                                    {
                                                        frm.rpt = new rptQuyetDinhNangLuongCN(dNgayIn.DateTime);

                                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                        conn.Open();

                                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhNangLuong", conn);
                                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                        cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = idCN;
                                                        cmd.Parameters.Add("@ID_SQD", SqlDbType.BigInt).Value = idL;
                                                        cmd.Parameters.Add("@NgayQD", SqlDbType.Date).Value = dNgayHL;
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
                                                    catch
                                                    {

                                                    }
                                                    break;
                                                }
                                        }

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
            catch { }
        }

    }
}