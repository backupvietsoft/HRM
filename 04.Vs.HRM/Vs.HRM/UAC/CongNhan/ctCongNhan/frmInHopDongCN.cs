using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using Vs.Report;
using Aspose.Words;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using DevExpress.Map.Native;
using System.IO;
using DevExpress.XtraPrinting.Preview;
using System.Threading;
using DevExpress.XtraGauges.Core.Base;
using Microsoft.Win32;

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
            rdo_ChonBaoCao.SelectedIndex = 0;

            if (Commons.Modules.KyHieuDV == "SB")
            {
                rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_HopDongDaoTao").FirstOrDefault());
            }
            else if (Commons.Modules.KyHieuDV == "DM")
            {
                rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_HopDongLaoDongKhoang").FirstOrDefault());
                rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_HopDongDaoTao").FirstOrDefault());
                rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_HopDongThucViecCN_QC").FirstOrDefault());
            }
            else if (Commons.Modules.KyHieuDV == "BT")
            {
                rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_HopDongLaoDongKhoang").FirstOrDefault());
                rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_HopDongDaoTao").FirstOrDefault());
                rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_HopDongThucViecCN_QC").FirstOrDefault());
            }
            else if(Commons.Modules.KyHieuDV == "TG")
            {
                rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_HopDongLaoDongKhoang").FirstOrDefault());
            }
            else
            {
                rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_HopDongLaoDongKhoang").FirstOrDefault());
                rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_HopDongDaoTao").FirstOrDefault());
            }
            dNgayIn.EditValue = DateTime.Today;
            Commons.OSystems.SetDateEditFormat(dNgayIn);
            Commons.Modules.sLoad = "";
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            if (btn == null || btn.Tag == null) return;
            switch (btn.Tag.ToString())
            {
                case "In":
                    {
                        switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
                        {
                            case "rdo_HopDongLaoDong":
                                {
                                    bool kiemHD = Convert.ToBoolean(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(HD_GIA_HAN,0) FROM dbo.HOP_DONG_LAO_DONG WHERE ID_HDLD = " + idHD + ""));
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                if (kiemHD)
                                                {
                                                    HopDongLaoDong();
                                                }
                                                else
                                                {
                                                    HopDongThuViecCDDH();
                                                }
                                                break;
                                            }
                                        case "SB":
                                            {
                                                if (kiemHD)
                                                {
                                                    HopDongLaoDong_SB_Report();
                                                    //HopDongLaoDong_SB();
                                                }
                                                else
                                                {
                                                    HopDongThuViec_SB();
                                                }

                                                break;
                                            }
                                        case "AP":
                                            {
                                                if (kiemHD)
                                                {
                                                    InHopDongLaoDongAP();
                                                }
                                                else
                                                {

                                                }
                                                break;
                                            }
                                        case "DM":
                                            {
                                                if (kiemHD)
                                                {
                                                    HopDongLaoDong_DM();
                                                }
                                                else
                                                {
                                                    HopDongThuViec_DM();
                                                }
                                                break;
                                            }
                                        case "HN":
                                            {
                                                if (kiemHD)
                                                {
                                                    HopDongLaoDong_HN();
                                                }
                                                else
                                                {
                                                    HopDongThuViec_HN();
                                                }
                                                break;
                                            }
                                        case "NB":
                                            {
                                                if (kiemHD)
                                                {
                                                    HopDongLaoDong_NB();
                                                }
                                                else
                                                {
                                                    HopDongThuViec_NB();
                                                }
                                                break;
                                            }
                                        case "NC":
                                            {
                                                if (kiemHD)
                                                {
                                                    HopDongLaoDong_NC();
                                                }
                                                else
                                                {
                                                    HopDongThuViec_NC();
                                                }
                                                break;
                                            }
                                        case "VV":
                                            {
                                                if (kiemHD)
                                                {
                                                    InHopDongLaoDong_VV();
                                                }
                                                else
                                                {
                                                    InHopDongThuViec_VV();
                                                }
                                                break;
                                            }
                                        case "BT":
                                            {
                                                if (kiemHD)
                                                {
                                                    HopDongLaoDong_BT();
                                                }
                                                else
                                                {
                                                    HopDongThuViec_BT();
                                                }
                                                break;
                                            }
                                        case "TG":
                                            {
                                                if (kiemHD)
                                                {
                                                    InHopDongLaoDong_TG();
                                                }
                                                else
                                                {
                                                    InHopDongThuViec_TG();
                                                }
                                                break;
                                            }
                                        default:
                                            HopDongLaoDong();
                                            break;
                                    }

                                }
                                break;
                            case "rdo_QuaTrinhThamGiaBHXH":
                                {
                                    InQuaTrinhTGBHXH();
                                }
                                break;
                            case "rdo_HopDongThucViecCN_QC":
                                {
                                    switch (Commons.Modules.KyHieuDV)
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
                                        case "HN":
                                            {
                                                HopDongThoiVu_HN();
                                                break;
                                            }
                                        case "NB":
                                            {
                                                ThongBaoKetThucHDLD();
                                                break;
                                            }
                                        case "NC":
                                            {
                                                ThongBaoKetThucHDLD();
                                                break;
                                            }
                                        case "TG":
                                            {
                                                HopDongThoiVu_TG();
                                                break;
                                            }
                                        default:
                                            HopDongThuViecCNQC();
                                            break;
                                    }

                                }
                                break;
                            case "rdo_HopDongDaoTao":
                                {
                                    switch (Commons.Modules.KyHieuDV)
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
                                        case "TG":
                                            {
                                                InToKhaiDangKyThue(idCN);
                                                break;
                                            }
                                        default:
                                            HopDongDaoTao();
                                            break;
                                    }

                                }
                                break;

                            case "rdo_HopDongLaoDongKhoang":
                                {
                                    HopDongLaoDongKhoang_SB();
                                    break;
                                }
                            case "rdo_ToKhaiBHXH":
                                {
                                    ToKhaiCapSoBHXH();
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
            cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = "";
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
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = "";
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
                frm.rpt = new rptToKhaiBaoHiemXaHoi(type, idCN, dNgayIn.DateTime);

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
        private void HopDongLaoDong_SB_Report()
        {
            try
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
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = "";
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
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
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
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = "";
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
        private void HopDongLaoDong_SB()
        {
            try
            {
                //lấy data dữ liệu
                System.Data.SqlClient.SqlConnection conn1;
                DataTable dt = new DataTable();

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong_SB", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idHD;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = "";
                cmd1.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                DataRow row = dt.Rows[0];

                string sPath = "";

                if (!System.IO.Directory.Exists("Report")) // kiểm tra xem forder đã có chưa , nếu chưa có thì tạo 
                {
                    System.IO.Directory.CreateDirectory("Report");
                }
                sPath = "Report\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".docx";

                //fill vào báo cáo
                var date = dNgayIn.DateTime;
                Document baoCao = new Document("Template\\TemplateSB\\HopDongLaoDong.doc");
                baoCao.MailMerge.Execute(new[] { "NGAY_IN" }, new[] { string.Format("Ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year) });
                foreach (DataColumn item in dt.Columns)
                {
                    if (Commons.Modules.ObjSystems.IsnullorEmpty(row[item]))
                    {
                        baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { "" });

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
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
        }
        private void HopDongLaoDong_DM()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDong_DM(dNgayIn.DateTime);

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong_DM", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idHD;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = "";
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
            catch (Exception ex) { }
        }
        private void HopDongThuViec_DM()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDongThuViec_DM(dNgayIn.DateTime);

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongThuViec_DM", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idHD;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = "";
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
            catch (Exception ex) { }
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
        private void HopDongThoiVu_HN()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongThoiVu_HN(dNgayIn.DateTime);

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongThoiVu_HN", conn1);
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
        private void HopDongLaoDongKhoang_SB()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            frmViewReport frm = new frmViewReport();
            frm.rpt = new rptHopDongLaoDongKhoang(dNgayIn.DateTime);

            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDongKhoang", conn);
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
        private void HopDongLaoDong_HN()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDong_HN(dNgayIn.DateTime);

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong_HN", conn1);
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
        private void HopDongLaoDong_NB()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDong_NB(dNgayIn.DateTime);

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong_NB", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idHD;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = "";
                cmd1.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                DataRow row = dt.Rows[0];

                string sPath = "";

                if (!System.IO.Directory.Exists("Report")) // kiểm tra xem forder đã có chưa , nếu chưa có thì tạo 
                {
                    System.IO.Directory.CreateDirectory("Report");
                }
                sPath = "Report\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".docx";

                //fill vào báo cáo
                //var date = Convert.ToDateTime(row["NGAY_BAT_DAU_HD"]);
                var date = dNgayIn.DateTime;
                Document baoCao = new Document("Template\\TemplateNB\\HopDongLaoDong.doc");
                baoCao.MailMerge.Execute(new[] { "Ngay_Thang_Nam_BC" }, new[] { string.Format("Hôm nay, ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year) });
                foreach (DataColumn item in dt.Columns)
                {
                    if (Commons.Modules.ObjSystems.IsnullorEmpty(row[item]))
                    {
                        baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { "" });
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
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
        }
        private void HopDongLaoDong_NC()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDong_NB(dNgayIn.DateTime);

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong_NB", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idHD;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = "";
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
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
        }
        private void HopDongThuViec_NC()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDongThuViec_NB(DateTime.Now);

                dt = new DataTable();
                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongThuViec_NB", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idHD;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = "";
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
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
        }
        private void HopDongThuViec_NB()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                dt = new DataTable();
                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongThuViec_NB", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idHD;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = "";
                cmd1.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();

                DataRow row = dt.Rows[0];

                string sPath = "";

                if (!System.IO.Directory.Exists("Report")) // kiểm tra xem forder đã có chưa , nếu chưa có thì tạo 
                {
                    System.IO.Directory.CreateDirectory("Report");
                }
                sPath = "Report\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".docx";

                //fill vào báo cáo
                //var date = Convert.ToDateTime(row["NGAY_BAT_DAU_HD"]);
                var date = dNgayIn.DateTime;

                string sTenFile = "Template\\TemplateNB\\HopDongThuViec.doc";
                if (Convert.ToDouble(dt.Rows[0]["MUC_LUONG_CHINH"]) == 0) // nếu lương bằng 0 thì in hợp đồng không lương 
                {
                    sTenFile = "Template\\TemplateNB\\HopDongThuViecTT.doc";
                }

                Document baoCao = new Document(sTenFile);
                baoCao.MailMerge.Execute(new[] { "Ngay_Thang_Nam_BC" }, new[] { string.Format("Hôm nay, ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year) });
                foreach (DataColumn item in dt.Columns)
                {
                    if (Commons.Modules.ObjSystems.IsnullorEmpty(row[item]))
                    {
                        baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { "" });
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
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
        }
        private void HopDongThuViec_HN()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongThuViec_HN(dNgayIn.DateTime);

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong_HN", conn1);
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
        //sự kiện các nút xử lí
        private void InHopDongLaoDongAP()
        {
            try
            {
                //lấy data dữ liệu
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "rptHopDongLaoDong_AP", Commons.Modules.UserName, Commons.Modules.TypeLanguage, idCN, idHD, ""));
                DataRow row = dt.Rows[0];

                string sPath = "";

                if (!System.IO.Directory.Exists("Report")) // kiểm tra xem forder đã có chưa , nếu chưa có thì tạo 
                {
                    System.IO.Directory.CreateDirectory("Report");
                }
                sPath = "Report\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".docx";

                //fill vào báo cáo
                //var date = Convert.ToDateTime(row["NGAY_BAT_DAU_HD"]);
                var date = dNgayIn.DateTime;
                Document baoCao = new Document("Template\\TemplateAP\\HopDongLaoDong.doc");
                baoCao.MailMerge.Execute(new[] { "Ngay_Thang_Nam_BC" }, new[] { string.Format("Hôm nay, ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year) });
                foreach (DataColumn item in dt.Columns)
                {
                    if (Commons.Modules.ObjSystems.IsnullorEmpty(row[item]))
                    {
                        baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { "................." });

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
        }
        private void InHopDongLaoDong_VV()
        {
            try
            {
                //lấy data dữ liệu

                System.Data.SqlClient.SqlConnection conn1;
                DataTable dt = new DataTable();

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong_VV", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idHD;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = "";
                cmd1.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                DataRow row = dt.Rows[0];

                string sPath = "";

                if (!System.IO.Directory.Exists("Report")) // kiểm tra xem forder đã có chưa , nếu chưa có thì tạo 
                {
                    System.IO.Directory.CreateDirectory("Report");
                }
                sPath = "Report\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".docx";

                //fill vào báo cáo
                //var date = Convert.ToDateTime(row["NGAY_BAT_DAU_HD"]);
                var date = dNgayIn.DateTime;
                Document baoCao = new Document("Template\\TemplateVV\\HopDongLaoDong.doc");
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
                baoCao.Save(sPath);
                Process.Start(sPath);
            }
            catch (Exception ex)
            {
            }
        }
        private void InHopDongThuViec_VV()
        {
            try
            {
                //lấy data dữ liệu

                System.Data.SqlClient.SqlConnection conn1;
                DataTable dt = new DataTable();

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongThuViec_VV", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idHD;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = "";
                cmd1.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                DataRow row = dt.Rows[0];


                string sPath = "";

                if (!System.IO.Directory.Exists("Report")) // kiểm tra xem forder đã có chưa , nếu chưa có thì tạo 
                {
                    System.IO.Directory.CreateDirectory("Report");
                }
                sPath = "Report\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".docx";

                //fill vào báo cáo
                //var date = Convert.ToDateTime(row["NGAY_BAT_DAU_HD"]);
                var date = dNgayIn.DateTime;
                Document baoCao = new Document("Template\\TemplateVV\\HopDongThuViec.doc");
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
                baoCao.Save(sPath);
                Process.Start(sPath);
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
        }
        // Bình thuận
        private void HopDongLaoDong_BT()
        {
            DataTable dt = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDong_DM(dNgayIn.DateTime);

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong_BT", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idHD;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = "";
                cmd1.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                ds.Tables[0].TableName = "HDLD";

                string sPath = "";
                if (!System.IO.Directory.Exists("Report")) // kiểm tra xem forder đã có chưa , nếu chưa có thì tạo 
                {
                    System.IO.Directory.CreateDirectory("Report");
                }
                sPath = "Report\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

                // If the file name is not an empty string open it for saving.
                Commons.TemplateExcel.FillReport(sPath, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateBT\\HopDongLaoDong.xlsx", ds, new string[] { "{", "}" });
                Process.Start(sPath);
            }
            catch { }
        }
        private void HopDongThuViec_BT()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                dt = new DataTable();
                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongThuViec_BT", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idHD;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = "";
                cmd1.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                ds.Tables[0].TableName = "HDTV";

                string sPath = "";
                if (!System.IO.Directory.Exists("Report")) // kiểm tra xem forder đã có chưa , nếu chưa có thì tạo 
                {
                    System.IO.Directory.CreateDirectory("Report");
                }
                sPath = "Report\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                Commons.TemplateExcel.FillReport(sPath, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateBT\\HopDongThuViec.xlsx", ds, new string[] { "{", "}" });
                Process.Start(sPath);
            }
            catch { }
        }
        // TG
        private void InHopDongThuViec_TG()
        {
            try
            {
                //lấy data dữ liệu

                System.Data.SqlClient.SqlConnection conn1;
                DataTable dt = new DataTable();

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongThuViec_TG", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idHD;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = "";
                cmd1.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                DataRow row = dt.Rows[0];

                string sPath = "";

                if (!System.IO.Directory.Exists("Report")) // kiểm tra xem forder đã có chưa , nếu chưa có thì tạo 
                {
                    System.IO.Directory.CreateDirectory("Report");
                }
                sPath = "Report\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".docx";

                string sTenHopDong = "HopDongThuViec.doc";
                if (dt.Rows[0]["KY_HIEU_HD"].ToString() == "DT")
                {
                    sTenHopDong = "HopDongDaoTao.doc";
                }
                //fill vào báo cáo
                var date = dNgayIn.DateTime;
                Document baoCao = new Document("Template\\TemplateTG\\" + sTenHopDong + "");
                foreach (DataColumn item in dt.Columns)
                {
                    if (Commons.Modules.ObjSystems.IsnullorEmpty(row[item]))
                    {
                        baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { "" });

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
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
        }
        private void InHopDongLaoDong_TG()
        {
            try
            {
                //lấy data dữ liệu

                System.Data.SqlClient.SqlConnection conn1;
                DataTable dt = new DataTable();

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong_TG", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = idHD;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = "";
                cmd1.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                DataRow row = dt.Rows[0];

                string sPath = "";

                if (!System.IO.Directory.Exists("Report")) // kiểm tra xem forder đã có chưa , nếu chưa có thì tạo 
                {
                    System.IO.Directory.CreateDirectory("Report");
                }
                sPath = "Report\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".docx";

                //fill vào báo cáo
                //var date = Convert.ToDateTime(row["NGAY_BAT_DAU_HD"]);
                var date = dNgayIn.DateTime;
                Document baoCao = new Document("Template\\TemplateTG\\HopDongLaoDong.doc");
                //baoCao.MailMerge.Execute(new[] { "NGAY_BD_HD" }, new[] { string.Format("ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year) });
                //baoCao.MailMerge.Execute(new[] { "NGAY_KY" }, new[] { string.Format("ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year) });
                //baoCao.MailMerge.Execute(new[] { "NGAY_KT_HD" }, new[] { string.Format("ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year) });
                foreach (DataColumn item in dt.Columns)
                {
                    if (Commons.Modules.ObjSystems.IsnullorEmpty(row[item]))
                    {
                        baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { "" });

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
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
        }
        private void HopDongThoiVu_TG()
        {
            DataTable dt = new DataTable();

            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                dt = new DataTable();

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongThoiVu_TG", conn1);
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
                DataTable dt2 = new DataTable();
                dt2 = ds.Tables[1].Copy();

                DataRow row = dt.Rows[0];

                string sPath = "";

                if (!System.IO.Directory.Exists("Report")) // kiểm tra xem forder đã có chưa , nếu chưa có thì tạo 
                {
                    System.IO.Directory.CreateDirectory("Report");
                }
                sPath = "Report\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".docx";

                //fill vào báo cáo
                //var date = Convert.ToDateTime(row["NGAY_BAT_DAU_HD"]);
                var date = dNgayIn.DateTime;
                Document baoCao = new Document("Template\\TemplateTG\\HDLDThoiVu.doc");
                //baoCao.MailMerge.Execute(new[] { "NGAY_BD_HD" }, new[] { string.Format("ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year) });
                //baoCao.MailMerge.Execute(new[] { "NGAY_KY" }, new[] { string.Format("ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year) });
                //baoCao.MailMerge.Execute(new[] { "NGAY_KT_HD" }, new[] { string.Format("ngày {0} tháng {1} năm {2}", date.Day, date.Month, date.Year) });
                foreach (DataColumn item in dt.Columns)
                {
                    if (Commons.Modules.ObjSystems.IsnullorEmpty(row[item]))
                    {
                        baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { "" });

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
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
        }
        private void InToKhaiDangKyThue(Int64 ID_CN)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                System.Data.DataTable dt = new System.Data.DataTable();

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptToKhaiDangKyThue", conn);
                cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = ID_CN;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new System.Data.DataTable();
                dt = ds.Tables[0].Copy();
                DataRow row = dt.Rows[0];

                string sPath = "";

                if (!System.IO.Directory.Exists("Report")) // kiểm tra xem forder đã có chưa , nếu chưa có thì tạo 
                {
                    System.IO.Directory.CreateDirectory("Report");
                }
                sPath = "Report\\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".docx";

                //fill vào báo cáo
                Document baoCao = new Document("Template\\TemplateTG\\ToKhaiThueTNCN.doc");
                foreach (DataColumn item in dt.Columns)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(row[item])))
                    {
                        baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { "" });
                        continue;
                    }
                    switch (item.DataType.Name)
                    {
                        case "DateTime":
                            {
                                baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { Convert.ToDateTime(row[item]).ToString("dd/MM/yyyy") });
                                break;
                            }

                        default:
                            {
                                baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { Convert.ToString(row[item]) });
                                break;

                            }
                    }
                }
                baoCao.Save(sPath);
                Process.Start(sPath);
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }

            this.Cursor = Cursors.Default;
        }
        private void ThongBaoKetThucHDLD()
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            frmViewReport frm = new frmViewReport();
            frm.rpt = new rptThongBaoKetThucHDLD_NB(dNgayIn.DateTime);

            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptThongBaoKetThucHDLD_NB", conn);
            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idCN;
            cmd.Parameters.Add("@ID_HD", SqlDbType.Int).Value = idHD;
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
        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
            {
                case "rdo_ToKhaiBHXH":
                    chkDaThamGia.Enabled = true;
                    break;
                default:
                    chkDaThamGia.Enabled = false;
                    break;
            }
        }
        private void InQuaTrinhTGBHXH()
        {
            frmViewReport frm = new frmViewReport();
            frm.rpt = new rptThamGiaBHXH(Commons.Modules.iCongNhan);
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetThamGiaBHXH", Commons.Modules.iCongNhan));
            if (dt == null || dt.Rows.Count == 0)
            {
                Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"));
                return;
            }
            dt.TableName = "DATA";
            frm.AddDataSource(dt);

            frm.ShowDialog();
        }
        private void frmInHopDongCN_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo("Report"); //Assuming Test is your Folder
                FileInfo[] Files = d.GetFiles(); //Getting Text files
                foreach (FileInfo file in Files)
                {
                    try
                    {
                        file.Delete();
                    }
                    catch { }
                }
            }
            catch { }
        }
    }
}