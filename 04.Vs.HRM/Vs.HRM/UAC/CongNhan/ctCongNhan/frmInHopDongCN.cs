﻿using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class frmInHopDongCN : DevExpress.XtraEditors.XtraForm
    {
        private long idCN;
        private long idHD;

        public frmInHopDongCN(Int64 idCongNhan, Int64 idHopDong, string tencn)
        {
            InitializeComponent();
            NONN_HoTenCN.Text = tencn.ToUpper();
            idCN = idCongNhan;
            idHD = idHopDong;
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        //sự kiên load form
        private void formInHopDongCN_Load(object sender, EventArgs e)
        {
            //chkChuaThamGia.Visible = false;
            //chkDaThamGia.Visible = false;
            chkChuaThamGia.Checked = true;
            rdo_ChonBaoCao.SelectedIndex = 0;
            if (Commons.Modules.ObjSystems.KyHieuDV_CN(Convert.ToInt64(Commons.Modules.iCongNhan)) == "SB")
            {
                rdo_ChonBaoCao.Properties.Items.RemoveAt(3);
            }
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

                        int n = rdo_ChonBaoCao.SelectedIndex;
                        if (rdo_ChonBaoCao.Properties.Items.Count < 5)
                            n = (n >= 2 ? n + 1 : n);
                        switch (n)
                        {
                            case 0:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV_CN(Convert.ToInt64(Commons.Modules.iCongNhan)))
                                    {
                                        case "MT":
                                            {
                                                HopDongLaoDong();
                                                break;
                                            }
                                        case "SB":
                                            {
                                                HopDongLaoDong_SB();
                                                break;
                                            }
                                        default:
                                            HopDongLaoDong();
                                            break;
                                    }

                                }
                                break;
                            case 1:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV_CN(Convert.ToInt64(Commons.Modules.iCongNhan)))
                                    {
                                        case "MT":
                                            {
                                                HopDongThuViecCDDH();
                                                break;
                                            }
                                        case "SB":
                                            {
                                                HopDongThuViec_SB();
                                                break;
                                            }
                                        default:
                                            HopDongThuViecCDDH();
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
                                                HopDongThuViecCNQC();
                                                break;
                                            }
                                        case "SB":
                                            {
                                                HopDongThoiVu_SB();
                                                break;
                                            }
                                        default:
                                            HopDongThuViecCNQC();
                                            break;
                                    }

                                }
                                break;
                            case 3:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV_CN(Convert.ToInt64(Commons.Modules.iCongNhan)))
                                    {
                                        case "MT":
                                            {
                                                HopDongDaoTao();
                                                break;
                                            }

                                        case "SB":
                                            {
                                                HopDongThoiVu_SB();
                                                break;
                                            }
                                        default:
                                            HopDongDaoTao();
                                            break;
                                    }

                                }
                                break;

                            case 4:
                                {
                                    if (chkChuaThamGia.Checked == false && chkDaThamGia.Checked == false)
                                    {
                                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChuaChonLoaiThamGia"));
                                        return;
                                    }
                                    ToKhaiCapSoBHXH();
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

        private void HopDongLaoDong()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            frmViewReport frm = new frmViewReport();
            frm.rpt = new rptHopDongLaoDong_MT(dNgayIn.DateTime);

            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong", conn);
            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
            cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idHD;
            cmd.CommandType = CommandType.StoredProcedure;

            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            dt = new DataTable();
            dt = ds.Tables[0].Copy();
            dt.TableName = "DATA";
            frm.AddDataSource(dt);

            dtbc = new DataTable();
            dtbc = ds.Tables[1].Copy();
            dtbc.TableName = "NOI_DUNG";
            frm.AddDataSource(dtbc);

            frm.ShowDialog();
        }
        private void HopDongThuViecCDDH()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongThuViec_CDDH(dNgayIn.DateTime);

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongThuViec_MT", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idHD;
                cmd1.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);

                dtbc = new DataTable();
                dtbc = ds.Tables[1].Copy();
                dtbc.TableName = "NOI_DUNG";
                frm.AddDataSource(dtbc);

                frm.ShowDialog();
            }
            catch { }
        }
        private void HopDongThuViecCNQC()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn2;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongThuViec_CNQC(dNgayIn.DateTime);

                conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn2.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongThuViec_MT", conn2);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idHD;
                cmd1.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);

                dtbc = new DataTable();
                dtbc = ds.Tables[1].Copy();
                dtbc.TableName = "NOI_DUNG";
                frm.AddDataSource(dtbc);

                frm.ShowDialog();
            }
            catch { }
        }
        private void HopDongDaoTao()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn3;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongDaoTao(dNgayIn.DateTime);

                conn3 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn3.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongDaoTao", conn3);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idHD;
                cmd1.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);

                dtbc = new DataTable();
                dtbc = ds.Tables[1].Copy();
                dtbc.TableName = "NOI_DUNG";
                frm.AddDataSource(dtbc);

                frm.ShowDialog();
            }
            catch { }
        }
        private void ToKhaiCapSoBHXH()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            int type = 1;
            if (chkDaThamGia.Checked == true)
            {
                type = 2;
            }
            else
            {
                type = 1;
            }
            try
            {
                System.Data.SqlClient.SqlConnection conn4;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptToKhaiBaoHiemXaHoi(type, idCN,dNgayIn.DateTime);

                conn4 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn4.Open();

                System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand("rptToKhaiCapSoBHXH", conn4);
                cmd2.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd2.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd2.Parameters.Add("@Type", SqlDbType.Int).Value = type;
                cmd2.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd2.Parameters.Add("@NgayIn", SqlDbType.Date).Value = dNgayIn.EditValue;
                cmd2.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd2);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (type == 2)
                {
                    dt.TableName = "TPYE2";
                }
                else
                {
                    dt.TableName = "DATA";
                }
                frm.AddDataSource(dt);

                try
                {
                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables[1].Copy();
                    dt1.TableName = "DATA2";
                    frm.AddDataSource(dt1);
                }
                catch { }
                frm.ShowDialog();
            }
            catch (Exception ex) { }
        }
        private void HopDongLaoDong_SB()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            frmViewReport frm = new frmViewReport();
            frm.rpt = new rptHopDongLaoDong_SB(dNgayIn.DateTime);

            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong_SB", conn);
            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
            cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idHD;
            cmd.CommandType = CommandType.StoredProcedure;

            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            dt = new DataTable();
            dt = ds.Tables[0].Copy();
            dt.TableName = "DATA";
            frm.AddDataSource(dt);

            dtbc = new DataTable();
            dtbc = ds.Tables[1].Copy();
            dtbc.TableName = "NOI_DUNG";
            frm.AddDataSource(dtbc);

            frm.ShowDialog();
        }
        private void HopDongThuViec_SB()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDongThuViec_SB(dNgayIn.DateTime);

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongThuViec_SB", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idHD;
                cmd1.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);

                dtbc = new DataTable();
                dtbc = ds.Tables[1].Copy();
                dtbc.TableName = "NOI_DUNG";
                frm.AddDataSource(dtbc);

                frm.ShowDialog();
            }
            catch { }
        }

        private void HopDongThoiVu_SB()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongThoiVu_SB(dNgayIn.DateTime);

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongThoiVu_SB", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idHD;
                cmd1.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);

                dtbc = new DataTable();
                dtbc = ds.Tables[1].Copy();
                dtbc.TableName = "NOI_DUNG";
                frm.AddDataSource(dtbc);

                frm.ShowDialog();
            }
            catch { }
        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = rdo_ChonBaoCao.SelectedIndex;
            if (rdo_ChonBaoCao.Properties.Items.Count < 5)
                n = (n >= 2 ? n + 1 : n);
            switch (n)
            {
                case 4:
                    chkDaThamGia.Enabled = true;
                    chkChuaThamGia.Enabled = true;
                    break;
                default:
                    chkDaThamGia.Enabled = false;
                    chkChuaThamGia.Enabled = false;
                    break;
            }
        }

        private void chkDaThamGia_CheckedChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            chkChuaThamGia.Checked = false;
            Commons.Modules.sLoad = "";
            //if(chkDaThamGia.Checked == true)
            //{
            //    chkChuaThamGia.Checked = false;
            //    return;
            //}
            //else
            //{
            //    chkChuaThamGia.Checked = true;
            //    return;
            //}
        }

        private void chkChuaThamGia_CheckedChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            chkDaThamGia.Checked = false;
            Commons.Modules.sLoad = "";
        }
    }
}