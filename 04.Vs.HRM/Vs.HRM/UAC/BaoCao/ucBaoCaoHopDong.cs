using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Windows.Forms;
using Vs.Report;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Reflection;
using System.Drawing;
using System.Linq;
using DevExpress.Map.Native;
using DevExpress.CodeParser;
using Aspose.Words;
using System.IO;
using OfficeOpenXml;
using System.Collections.Generic;
using OfficeOpenXml.Style;

namespace Vs.HRM
{
    public partial class ucBaoCaoHopDong : DevExpress.XtraEditors.XtraUserControl
    {
        private string SaveExcelFile;
        public ucBaoCaoHopDong()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, windowsUIButton);
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            try
            {

                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "Print":
                        {
                            frmViewReport frm = new frmViewReport();
                            DataTable dt;
                            switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
                            {
                                case "rdo_HDHienTai":
                                    {
                                        System.Data.SqlClient.SqlConnection conn;
                                        dt = new DataTable();
                                        string sTieuDe = "DANH SÁCH CÔNG NHÂN HỢP ĐỒNG";
                                        frm.rpt = new rptBCHopDongHetHan(lk_NgayTinh.DateTime, sTieuDe, lk_NgayIn.DateTime, lk_NgayIn.DateTime, Convert.ToInt32(LK_DON_VI.EditValue));

                                        try
                                        {
                                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn.Open();

                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCHopDongCongNhan", conn);

                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                            cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_NgayIn.DateTime;
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                            DataSet ds = new DataSet();
                                            adp.Fill(ds);
                                            dt = new DataTable();
                                            dt = ds.Tables[0].Copy();
                                            dt.TableName = "DA_TA";
                                            frm.AddDataSource(dt);
                                            frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
                                        }
                                        catch
                                        { }
                                        frm.ShowDialog();
                                    }
                                    break;
                                case "rdo_HDHetHan":
                                    {
                                        System.Data.SqlClient.SqlConnection conn1;
                                        dt = new DataTable();
                                        string sTieuDe1 = "DANH SÁCH CÔNG NHÂN HẾT HẠN HỢP ĐỒNG";
                                        switch (Commons.Modules.KyHieuDV)
                                        {
                                            case "SB":
                                                {
                                                    frm.rpt = new rptBCHopDongHetHan_SB(lk_NgayTinh.DateTime, sTieuDe1, lk_NgayIn.DateTime, lk_NgayIn.DateTime);
                                                    break;
                                                }
                                            case "NB":
                                                {
                                                    inHetHanHDNB(dTuNgay.DateTime, dDenNgay.DateTime);
                                                    break;
                                                }
                                            default:
                                                {
                                                    frm.rpt = new rptBCHopDongHetHan(lk_NgayTinh.DateTime, sTieuDe1, lk_NgayIn.DateTime, lk_NgayIn.DateTime, Convert.ToInt32(LK_DON_VI.EditValue));
                                                    break;
                                                }
                                        }

                                        try
                                        {
                                            conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn1.Open();

                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCHopDongHetHan", conn1);

                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                            cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_NgayTinh.DateTime;
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);


                                            DataSet ds = new DataSet();
                                            adp.Fill(ds);
                                            dt = new DataTable();
                                            dt = ds.Tables[0].Copy();
                                            dt.TableName = "DA_TA";
                                            frm.AddDataSource(dt);
                                            frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
                                        }
                                        catch
                                        { }


                                        frm.ShowDialog();
                                    }
                                    break;
                                case "rdo_HDKyGiaiDoan":
                                    {
                                        if (chkInHopDong.Checked)
                                        {
                                            grvCongNhan.CloseEditor();
                                            grvCongNhan.UpdateCurrentRow();
                                            string sBT = "sBTTaoHDLD" + Commons.Modules.iIDUser;
                                            dt = new DataTable();
                                            try
                                            {
                                                dt = ((DataTable)grdCongNhan.DataSource).AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).CopyToDataTable();
                                            }
                                            catch
                                            {
                                                Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"));
                                                return;
                                            }
                                            if (dt.Rows.Count == 0)
                                            {
                                                return;
                                            }
                                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, dt, "");

                                            bool kiemHD = Convert.ToBoolean(((DataTable)grdCongNhan.DataSource).AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).CopyToDataTable().Rows[0]["HD_GIA_HAN"]);
                                            switch (Commons.Modules.KyHieuDV)
                                            {
                                                case "DM":
                                                    {
                                                        if (kiemHD)
                                                        {
                                                            InHDLD_DM(sBT);
                                                        }
                                                        else
                                                        {
                                                            InHDTV_DM(sBT);
                                                        }
                                                        break;
                                                    }
                                                case "BT":
                                                    {
                                                        if (kiemHD)
                                                        {
                                                            InHopDongLaoDong_BT(sBT);
                                                        }
                                                        else
                                                        {
                                                            InHopDongThuViec_BT(sBT);
                                                        }
                                                        break;
                                                    }
                                                case "NC":
                                                    {
                                                        if (kiemHD)
                                                        {
                                                            HopDongLaoDong_NC(sBT);
                                                        }
                                                        else
                                                        {
                                                            HopDongThuViec_NC(sBT);
                                                        }
                                                        break;
                                                    }
                                                case "NB":
                                                    {
                                                        if (kiemHD)
                                                        {
                                                            HopDongLaoDong_NB(sBT);
                                                        }
                                                        else
                                                        {
                                                            HopDongThuViec_NB(sBT);
                                                        }
                                                        break;
                                                    }
                                                case "HN":
                                                    {
                                                        HopDongLaoDong_HN(sBT);
                                                        break;
                                                    }
                                                case "SB":
                                                    {
                                                        if (kiemHD)
                                                        {
                                                            HopDongLaoDong_SB(sBT);
                                                        }
                                                        else
                                                        {
                                                            InHopDongThuViec_SB(sBT);
                                                        }
                                                        break;
                                                    }
                                                case "VV":
                                                    {
                                                        if (kiemHD)
                                                        {
                                                            InHopDongLaoDong_VV(sBT);
                                                        }
                                                        else
                                                        {
                                                            InHopDongThuViec_VV(sBT);
                                                        }
                                                        break;
                                                    }
                                                case "AP":
                                                    {
                                                        InHopDongLaoDong_AP(sBT);
                                                        break;
                                                    }
                                                case "TG":
                                                    {
                                                        if (kiemHD)
                                                        {
                                                            InHopDongLaoDong_TG(sBT);
                                                        }
                                                        else
                                                        {
                                                            InHopDongThuViec_TG(sBT);
                                                        }
                                                        break;
                                                    }

                                                case "MT":
                                                    {
                                                        if (kiemHD)
                                                        {
                                                            InHopDongLaoDong_MT(sBT);
                                                        }
                                                        else
                                                        {
                                                            InHopDongThuViec_MT(sBT);
                                                        }
                                                        break;
                                                    }
                                            }
                                        }
                                        else
                                        {
                                            if (Commons.Modules.KyHieuDV == "SB")
                                            {
                                                BaoCaoHopDongGiaiDoan_SB(1);
                                                return;
                                            }
                                            if (Commons.Modules.KyHieuDV == "NB")
                                            {
                                                inKyHD_NB(dTuNgay.DateTime, dDenNgay.DateTime);
                                                return;
                                            }
                                            System.Data.SqlClient.SqlConnection conn2;
                                            dt = new DataTable();
                                            string sTieuDe2 = "DANH SÁCH CÔNG NHÂN KÝ HỢP ĐỒNG";
                                            frm.rpt = new rptBCHopDongHetHan(lk_NgayIn.DateTime, sTieuDe2, dTuNgay.DateTime, dDenNgay.DateTime, Convert.ToInt32(LK_DON_VI.EditValue));

                                            try
                                            {
                                                conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                conn2.Open();

                                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCHopDongGiaiDoan", conn2);

                                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.DateTime;
                                                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.DateTime;
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);


                                                DataSet ds = new DataSet();
                                                adp.Fill(ds);
                                                dt = new DataTable();
                                                dt = ds.Tables[0].Copy();
                                                dt.TableName = "DA_TA";
                                                frm.AddDataSource(dt);
                                                frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
                                            }
                                            catch
                                            { }
                                            frm.ShowDialog();
                                        }
                                    }
                                    break;

                                //Tai ký hợp đồng
                                case "rdo_TaiKyHDLD":
                                    {
                                        if (Commons.Modules.KyHieuDV == "SB")
                                        {
                                            BaoCaoHopDongGiaiDoan_SB(2);
                                        }
                                        else
                                        {

                                            #region Tái ký 
                                            System.Data.SqlClient.SqlConnection conn2;
                                            dt = new DataTable();
                                            string sTieuDe2 = Commons.Modules.TypeLanguage == 1 ? "LIST OF EMPLOYEES WHO ARE DUE TO RENEW THEIR LABOR CONTRACTS IN " : "DANH SÁCH CB-CNV TỚI HẠN TÁI KÝ HĐLĐ THÁNG ";
                                            frm.rpt = new rptBCTaiKyHopDongLaoDong(lk_NgayIn.DateTime, sTieuDe2, dTuNgay.DateTime, dDenNgay.DateTime);

                                            try
                                            {
                                                conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                conn2.Open();

                                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSNVToiHanTaiKyHopDong", conn2);

                                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dTuNgay.EditValue);
                                                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dDenNgay.EditValue);
                                                cmd.CommandType = CommandType.StoredProcedure;
                                                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);


                                                DataSet ds = new DataSet();
                                                adp.Fill(ds);
                                                dt = new DataTable();
                                                dt = ds.Tables[0].Copy();
                                                dt.TableName = "DATA";
                                                frm.AddDataSource(dt);
                                                frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
                                            }
                                            catch
                                            { }
                                            frm.ShowDialog();
                                            #endregion
                                        }
                                    }
                                    break;

                                // Tới hạn ký hợp đồng
                                case "rdo_ToiHanKyHDLD":
                                    {
                                        #region Tới hạn ký hợp đồng
                                        System.Data.SqlClient.SqlConnection conn2;
                                        dt = new DataTable();
                                        string sTieuDe2 = Commons.Modules.TypeLanguage == 1 ? "LIST OF EMPLOYEES DUE TO SIGN LABOR CONTRACTS IN " : "DANH SÁCH CB-CNV TỚI HẠN KÝ HĐLĐ THÁNG ";
                                        frm.rpt = new rptBCToiHanKyHopDongLaoDong(lk_NgayIn.DateTime, sTieuDe2, dTuNgay.DateTime, dDenNgay.DateTime);

                                        try
                                        {
                                            conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn2.Open();

                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSNVToiHanKyHopDong", conn2);

                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                            cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dTuNgay.EditValue);
                                            cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dDenNgay.EditValue);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);


                                            DataSet ds = new DataSet();
                                            adp.Fill(ds);
                                            dt = new DataTable();
                                            dt = ds.Tables[0].Copy();
                                            dt.TableName = "DATA";
                                            frm.AddDataSource(dt);
                                            frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
                                        }
                                        catch
                                        { }
                                        frm.ShowDialog();
                                        #endregion
                                        break;
                                    }
                                    //case "rdoThamNienCuaCNV":
                                    //    {
                                    //        System.Data.SqlClient.SqlConnection conn;
                                    //        dt = new DataTable();
                                    //        frm = new frmViewReport();
                                    //        frm.rpt = new rptDSThamNien(lk_NgayIn.DateTime);

                                    //        try
                                    //        {
                                    //            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                    //            conn.Open();

                                    //            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSThamNien", conn);

                                    //            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                    //            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                    //            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                    //            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                    //            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                    //            cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = lk_NgayTinh.EditValue;
                                    //            cmd.CommandType = CommandType.StoredProcedure;
                                    //            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                    //            DataSet ds = new DataSet();
                                    //            adp.Fill(ds);
                                    //            dt = new DataTable();
                                    //            dt = ds.Tables[0].Copy();
                                    //            dt.TableName = "DA_TA";
                                    //            frm.AddDataSource(dt);
                                    //        }
                                    //        catch
                                    //        { }
                                    //        frm.ShowDialog();
                                    //        break;
                                    //    }
                                    //case "rdo_BaoCaoThamNien":
                                    //    {
                                    //        switch (Commons.Modules.KyHieuDV)
                                    //        {
                                    //            case "DM":
                                    //                {
                                    //                    BaoCaoTongHopThamNien_DM();
                                    //                    break;
                                    //                }
                                    //            default:
                                    //                {
                                    //                    BaoCaoTongHopThamNien_DM();
                                    //                    break;
                                    //                }
                                    //        }
                                    //        break;
                                    //    }
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
            catch { }
        }

        private void ucBaoCaoHopDong_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadCboDonVi();
            LoadCboXiNghiep();
            LoadCboTo();

            Commons.OSystems.SetDateEditFormat(dTuNgay);
            Commons.OSystems.SetDateEditFormat(dDenNgay);
            Commons.OSystems.SetDateEditFormat(lk_NgayIn);
            Commons.OSystems.SetDateEditFormat(lk_NgayTinh);

            lk_NgayIn.EditValue = DateTime.Today;
            lk_NgayTinh.EditValue = DateTime.Today;
            DateTime dtTN = DateTime.Today;
            DateTime dtDN = DateTime.Today;
            dtDN = dtDN.AddMonths(1);
            dtDN = dtDN.AddDays(-(dtDN.Day));
            dTuNgay.EditValue = dtTN;
            dDenNgay.EditValue = dtDN;
            rdo_ChonBaoCao_SelectedIndexChanged(null, null);
            rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_ToiHanKyHDLD").FirstOrDefault());
            Commons.Modules.sLoad = "";
        }

        private void inKyHD_NB(DateTime TNgay, DateTime DNgay)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCKyHopDong_NB", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dTuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dDenNgay.EditValue);

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = ds.Tables[0].Copy();

                string sFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                try
                {
                    string sDDFile = Commons.Modules.ObjSystems.CapnhatTL("");
                    if (sDDFile != "\\")
                        sFileName = sDDFile + "\\" + sFileName;
                }
                catch { }

                FileInfo file = new FileInfo(sFileName);
                if (file.Exists)
                {
                    file.Delete();
                }
                ExcelPackage pck = new ExcelPackage(file);
                var ws1 = pck.Workbook.Worksheets.Add(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "sBCDanhSachKyHopDongLaoDong"));

                Commons.Modules.MExcel.MTTChung(ws1, 1, 1, 0, 0);

                int iDong = 4;

                Commons.Modules.MExcel.MText(ws1, this.Name, "sBCDanhSachCongNhanHetHanHD", iDong, 1, iDong, dt.Columns.Count, true, true, 13, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                iDong++;

                Commons.Modules.MExcel.MText(ws1, "", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "sTNgay") + " " + TNgay.ToString("dd/MM/yyyy") + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "sDNgay") + " " + DNgay.ToString("dd/MM/yyyy"), iDong, 1, iDong, dt.Columns.Count, true, true, 13, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);


                iDong = iDong + 2;

                System.Data.DataTable dtTMP = new System.Data.DataTable();
                List<List<Object>> WidthColumns = new List<List<Object>>();
                List<Object> WidthColumnsName = new List<Object>();
                int iRowBorder = 0;
                WidthColumnsName = new List<Object>() { "STT", 5 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "SO_HDLD", 9 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "MS_CN", 5, "0" };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "TEN_TO", 10 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "HO_TEN", 18 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "NGAY_SINH", 10 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "DIA_CHI_THUONG_TRU", 27 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "SO_CMND", 12 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "NOI_CAP", 16 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "NGAY_CAP", 10, "dd/MM/yyyy" };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "NGAY_VAO_LAM", 10, "dd/MM/yyyy" };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "NGAY_BAT_DAU_HD", 10, "dd/MM/yyyy" };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "TEN_LCV", 32 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "MUC_LUONG_CHINH", 11, Commons.Modules.sSoLeDG };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "TEN_LHDLD", 6.5 };
                WidthColumns.Add(WidthColumnsName);

                ws1.Cells[iDong, 1].LoadFromDataTable(dt, true);
                Commons.Modules.MExcel.MFormatExcel(ws1, dt, iDong, "sBCDanhSachKyHopDongLaoDong", WidthColumns, true, true, true);

                iDong = iDong + dtTMP.Rows.Count + 1;
                iRowBorder = iRowBorder + dtTMP.Rows.Count + 7;
                var border = ws1.Cells[9, 1, iRowBorder + 3, dt.Columns.Count - 1].Style.Border;
                border.Top.Style = ExcelBorderStyle.Thin;
                border.Left.Style = ExcelBorderStyle.Thin;
                border.Bottom.Style = ExcelBorderStyle.Thin;
                border.Right.Style = ExcelBorderStyle.Thin;

                ws1.Cells[1, 1, iRowBorder + dt.Rows.Count + 3, dt.Columns.Count].Style.Font.Name = "Times New Roman";
                ws1.Cells[7, 1, iRowBorder + dt.Rows.Count + 3, dt.Columns.Count].Style.Font.Size = 10;

                Commons.Modules.MExcel.MText(ws1, "", "Namco , ngày " + lk_NgayIn.DateTime.Day.ToString() + " tháng " + lk_NgayIn.DateTime.Month.ToString() + " năm " + lk_NgayIn.DateTime.Year.ToString() + "", iDong + dt.Rows.Count + 1, 11, iDong + dt.Rows.Count + 1, 14, true, true, 10,
                    OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

                Commons.Modules.MExcel.MText(ws1, this.Name, "sNguoiLapBieu", iDong + dt.Rows.Count + 2, 2, iDong + dt.Rows.Count + 2, 3, true, true, 10, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

                Commons.Modules.MExcel.MText(ws1, this.Name, "sPhongHCNS", iDong + dt.Rows.Count + 2, 6, iDong + dt.Rows.Count + 2, 7, true, true, 10, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

                Commons.Modules.MExcel.MText(ws1, this.Name, "sBanGiamDoc", iDong + dt.Rows.Count + 2, 11, iDong + dt.Rows.Count + 2, 14, true, true, 10, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

                if (file.Exists)
                    file.Delete();
                pck.SaveAs(file);
                System.Diagnostics.Process.Start(file.FullName);

            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void inHetHanHDNB(DateTime TNgay, DateTime DNgay)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCHopDongHetHan_NB", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dTuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(dDenNgay.EditValue);

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = ds.Tables[0].Copy();

                string sFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                try
                {
                    string sDDFile = Commons.Modules.ObjSystems.CapnhatTL("");
                    if (sDDFile != "\\")
                        sFileName = sDDFile + "\\" + sFileName;
                }
                catch { }

                FileInfo file = new FileInfo(sFileName);
                if (file.Exists)
                {
                    file.Delete();
                }
                ExcelPackage pck = new ExcelPackage(file);
                var ws1 = pck.Workbook.Worksheets.Add(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "sBCDanhSachCongNhanHetHanHD"));

                Commons.Modules.MExcel.MTTChung(ws1, 1, 1, 0, 0);

                int iDong = 4;

                Commons.Modules.MExcel.MText(ws1, this.Name, "sBCDanhSachCongNhanHetHanHD", iDong, 1, iDong, dt.Columns.Count, true, true, 13, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                iDong++;

                Commons.Modules.MExcel.MText(ws1, "", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "sTNgay") + " " + TNgay.ToString("dd/MM/yyyy") + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "sDNgay") + " " + DNgay.ToString("dd/MM/yyyy"), iDong, 1, iDong, dt.Columns.Count, true, true, 13, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

                iDong = iDong + 2;

                System.Data.DataTable dtTMP = new System.Data.DataTable();
                List<List<Object>> WidthColumns = new List<List<Object>>();
                List<Object> WidthColumnsName = new List<Object>();
                int iRowBorder = 0;
                
                WidthColumnsName = new List<Object>() { "STT", 4.5 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "SO_HDLD", 10 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "MS_CN", 6.5,"0" };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "TEN_TO", 10 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "HO_TEN", 20 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "NGAY_SINH", 10 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "DIA_CHI_THUONG_TRU", 29 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "SO_CMND", 12 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "NOI_CAP", 10};
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "NGAY_CAP", 10 , "dd/MM/yyyy" };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "NGAY_VAO_LAM", 10,"dd/MM/yyyy" };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "NGAY_KY", 10, "dd/MM/yyyy" };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "NGAY_HET_HD", 10, "dd/MM/yyyy" };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "TEN_LCV", 35, "dd/MM/yyyy" };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "TEN_LHDLD", 10 };
                WidthColumns.Add(WidthColumnsName);

                ws1.Cells[iDong, 1].LoadFromDataTable(dt, true);
                Commons.Modules.MExcel.MFormatExcel(ws1, dt, iDong, "sBCDanhSachCongNhanHetHanHD", WidthColumns, true, true, true);

                iDong = iDong + dtTMP.Rows.Count + 1;
                iRowBorder = iRowBorder + dtTMP.Rows.Count + 7;
                var border = ws1.Cells[iRowBorder, 1, iRowBorder + dt.Rows.Count, dt.Columns.Count - 1].Style.Border;
                border.Top.Style = ExcelBorderStyle.Thin;
                border.Left.Style = ExcelBorderStyle.Thin;
                border.Bottom.Style = ExcelBorderStyle.Thin;
                border.Right.Style = ExcelBorderStyle.Thin;

                ws1.Cells[1, 1, iRowBorder + dt.Rows.Count + 3 ,  dt.Columns.Count].Style.Font.Name = "Times New Roman";
                ws1.Cells[7, 1, iRowBorder + dt.Rows.Count + 3, dt.Columns.Count].Style.Font.Size = 10;

                Commons.Modules.MExcel.MText(ws1, "", "Namco , ngày " + lk_NgayIn.DateTime.Day.ToString() + " tháng " + lk_NgayIn.DateTime.Month.ToString() + " năm " + lk_NgayIn.DateTime.Year.ToString() + "", iDong + dt.Rows.Count + 1, 11, iDong + dt.Rows.Count + 1, 14, true, true, 10,
                    OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

                Commons.Modules.MExcel.MText(ws1, this.Name, "sNguoiLapBieu", iDong + dt.Rows.Count + 2, 2, iDong + dt.Rows.Count + 2, 3, true, true, 10, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

                Commons.Modules.MExcel.MText(ws1, this.Name, "sPhongHCNS", iDong + dt.Rows.Count + 2, 6, iDong + dt.Rows.Count + 2, 7, true, true, 10, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

                Commons.Modules.MExcel.MText(ws1, this.Name, "sBanGiamDoc", iDong + dt.Rows.Count + 2, 11, iDong + dt.Rows.Count + 2, 14, true, true, 10, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

                if (file.Exists)
                    file.Delete();
                pck.SaveAs(file);
                System.Diagnostics.Process.Start(file.FullName);

            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }

        }


        private void LoadCboDonVi()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                if (LK_DON_VI.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_DON_VI, dt, "ID_DV", "TEN_DV", "TEN_DV");
                }
                else
                {
                    LK_DON_VI.Properties.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void LoadCboXiNghiep()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboXI_NGHIEP", LK_DON_VI.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                if (LK_XI_NGHIEP.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_XI_NGHIEP, dt, "ID_XN", "TEN_XN", "TEN_XN");
                }
                else
                {
                    LK_XI_NGHIEP.Properties.DataSource = dt;
                }
                LK_XI_NGHIEP.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void LoadCboTo()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTO", LK_DON_VI.EditValue, LK_XI_NGHIEP.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                if (LK_TO.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_TO, dt, "ID_TO", "TEN_TO", "TEN_TO");
                }
                else
                {
                    LK_TO.Properties.DataSource = dt;
                }
                LK_TO.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }

        }
        private void LoadGrdCongNhan()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn2.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCHopDongGiaiDoan", conn2);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.DateTime;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);


                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[1].Copy();
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdCongNhan, grvCongNhan, dt, true, false, false, true, true, this.Name);
                grvCongNhan.Columns["ID_CN"].Visible = false;
                grvCongNhan.Columns["ID_HDLD"].Visible = false;
                grvCongNhan.Columns["CHON"].Visible = false;
                try
                {
                    grvCongNhan.OptionsSelection.CheckBoxSelectorField = "CHON";
                    grvCongNhan.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
                catch { }
            }
            catch { }
        }
        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboXiNghiep();
            LoadCboTo();
            LoadGrdCongNhan();
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboTo();
            LoadGrdCongNhan();
        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
                {
                    case "rdo_HDKyGiaiDoan":
                        {
                            lblTNgay.Visible = true;
                            lblDNgay.Visible = true;
                            dTuNgay.Visible = true;
                            dDenNgay.Visible = true;
                            grdCongNhan.Visible = true;
                            lblInHopDong.Visible = true;
                            chkInHopDong.Visible = true;
                            LoadGrdCongNhan();
                        }
                        break;
                    default:
                        lblTNgay.Visible = true;
                        lblDNgay.Visible = true;
                        dTuNgay.Visible = true;
                        dDenNgay.Visible = true;
                        grdCongNhan.Visible = false;
                        lblInHopDong.Visible = false;
                        chkInHopDong.Visible = false;
                        break;
                }
            }
            catch
            { }
        }

        private void BaoCaoHopDongGiaiDoan_SB(int iLoai) // -- 1 hợp đồng  giai đoạn, 2 công nhân tái ký
        {
            //string datetime = "01/01/" + Convert.ToString(lk_Nam.EditValue);
            //DateTime tungay = Convert.ToDateTime(datetime);
            //try { datetime = "31/12/" + Convert.ToString(lk_Nam.EditValue); } catch { }
            //DateTime denngay = Convert.ToDateTime(datetime);
            //int iType = rdo_DiTreVeSom.SelectedIndex;

            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            DataTable dtBCPhep;
            try
            {

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCHopDongGiaiDoan_SB", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.DateTime;
                cmd.Parameters.Add("@LoaiHD", SqlDbType.Int).Value = iLoai;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);


                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCPhep = new DataTable();
                dtBCPhep = ds.Tables[0].Copy();

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                this.Cursor = Cursors.WaitCursor;
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 11;
                int iTNgay = 1;
                int iDNgay = 20;
                int iSoNgay = (iDNgay - iTNgay);

                Microsoft.Office.Interop.Excel.Range row1_tendv = oSheet.get_Range("A1");
                row1_tendv.Value = "SB SAIGON FASHION LTD.,CO";
                row1_tendv.Font.Bold = true;
                row1_tendv.Font.Name = fontName;
                row1_tendv.Font.Size = 9;
                row1_tendv.Font.Color = Color.FromArgb(0, 0, 255);

                Microsoft.Office.Interop.Excel.Range row2_tendc = oSheet.get_Range("A2");
                row2_tendc.Value = "Tan Thuan EPZ, Dist 7, HCMC";
                row2_tendc.Font.Bold = true;
                row2_tendc.Font.Name = fontName;
                row2_tendc.Font.Size = 9;
                row2_tendc.Font.Color = Color.FromArgb(0, 0, 255);

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCPhep.Columns.Count - 2);
                Microsoft.Office.Interop.Excel.Range row2_TieuDe_BaoCao0 = oSheet.get_Range("A4", lastColumn + "4");
                row2_TieuDe_BaoCao0.Merge();
                row2_TieuDe_BaoCao0.Font.Size = 14;
                row2_TieuDe_BaoCao0.Font.Name = fontName;
                row2_TieuDe_BaoCao0.RowHeight = 33;
                row2_TieuDe_BaoCao0.Font.Bold = true;
                row2_TieuDe_BaoCao0.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao0.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao0.Value2 = "DANH SÁCH LAO ĐỘNG VIỆT NAM "+ (iLoai == 1 ? "" : "TÁI") +" KÝ HỢP ĐỒNG LAO ĐỘNG THÁNG " + Convert.ToDateTime(dTuNgay.EditValue).ToString("MM/yyyy");
                row2_TieuDe_BaoCao0.Font.Color = Color.FromArgb(0, 0, 255);

                Microsoft.Office.Interop.Excel.Range row4_TieuDe_Format = oSheet.get_Range("A5", lastColumn + "5");
                row4_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Font.Color = Color.Red;
                row4_TieuDe_Format.WrapText = true;

                Microsoft.Office.Interop.Excel.Range row4_A = oSheet.get_Range("A5");
                row4_A.ColumnWidth = 5;
                row4_A.RowHeight = 45;
                row4_A.Value2 = "STT";


                Microsoft.Office.Interop.Excel.Range row4_C = oSheet.get_Range("B5");
                row4_C.ColumnWidth = 25;
                row4_C.Value2 = "Họ và tên";

                Microsoft.Office.Interop.Excel.Range row4_B = oSheet.get_Range("C5");
                row4_B.ColumnWidth = 10;
                row4_B.Value2 = "Mã số";

                Microsoft.Office.Interop.Excel.Range row4_D = oSheet.get_Range("D5");
                row4_D.ColumnWidth = 15;
                row4_D.Value2 = "Chuyền Tổ";

                Microsoft.Office.Interop.Excel.Range row4_E = oSheet.get_Range("E5");
                row4_E.ColumnWidth = 15;
                row4_E.Value2 = "Số hợp đồng";

                Microsoft.Office.Interop.Excel.Range row4_H4 = oSheet.get_Range("F5");
                row4_H4.ColumnWidth = 15;
                row4_H4.Value2 = "Bậc lương";

                Microsoft.Office.Interop.Excel.Range row4_I4 = oSheet.get_Range("G5");
                row4_I4.ColumnWidth = 15;
                row4_I4.Value2 = "Tiền lương";

                Microsoft.Office.Interop.Excel.Range row4_J4 = oSheet.get_Range("H5");
                row4_J4.ColumnWidth = 25;
                row4_J4.Value2 = "Ngày hợp đồng";

                Microsoft.Office.Interop.Excel.Range row4_K4 = oSheet.get_Range("I5");
                row4_K4.Value2 = "Lần Ký HĐ";
                row4_K4.ColumnWidth = 10;

                Microsoft.Office.Interop.Excel.Range row4_NS = oSheet.get_Range("J5");
                row4_NS.Value2 = "Ngày sinh";
                row4_NS.ColumnWidth = 15;

                Microsoft.Office.Interop.Excel.Range row4_NgayTV = oSheet.get_Range("K5");
                row4_NgayTV.Value2 = "Ngày thử việc";
                row4_NgayTV.ColumnWidth = 10;

                Microsoft.Office.Interop.Excel.Range row4_GC = oSheet.get_Range("L5");
                row4_GC.Value2 = "Ghi chú";
                row4_GC.ColumnWidth = 15;


                DataRow[] dr = dtBCPhep.Select();
                string[,] rowData = new string[dr.Length, dtBCPhep.Columns.Count];

                int col = 0;
                int rowCnt = 0;
                int rowCntY = 6; //Dùng để tính tổng cột Y
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCPhep.Columns.Count - 2; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }
                    //formatRange1 = oSheet.get_Range("Y" + rowCntY.ToString());
                    //formatRange1.Value2 = "X"+ rowCntY + "-W"+ rowCntY + "";
                    //oSheet.get_Range("Y"+ rowCntY + "").Value2 = "=X"+ rowCntY + " - W"+ rowCntY + "";
                    //rowCntY++;
                    rowCnt++;
                }
                rowCnt = rowCnt + 5;
                oSheet.get_Range("A6", lastColumn + rowCnt.ToString()).Value2 = rowData;

                Microsoft.Office.Interop.Excel.Range formatRange;

                formatRange = oSheet.get_Range("A6", "A" + (rowCnt).ToString());
                formatRange.NumberFormat = "#,##0";
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, XlTextParsingType.xlDelimited, XlTextQualifier.xlTextQualifierDoubleQuote);

                formatRange = oSheet.get_Range("G6", "G" + (rowCnt).ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, XlTextParsingType.xlDelimited, XlTextQualifier.xlTextQualifierDoubleQuote);

                string currentColumn = "";
                for (col = 2; col < dtBCPhep.Columns.Count - 2; col++)
                {
                    currentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(currentColumn + "6", currentColumn + (rowCnt).ToString());
                    formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                }

                oSheet.get_Range("A6", lastColumn + rowCnt.ToString()).Font.Name = fontName;
                oSheet.get_Range("A6", lastColumn + rowCnt.ToString()).Font.Size = fontSizeNoiDung;
                oSheet.get_Range("A6", lastColumn + rowCnt.ToString()).RowHeight = 25;
                ////Kẻ khung toàn bộ

                BorderAround(oSheet.get_Range("A5", lastColumn + rowCnt.ToString()));

                rowCnt = rowCnt + 2;
                formatRange = oSheet.get_Range("H" + rowCnt + "", "I" + rowCnt);
                formatRange.Merge();
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Name = fontName;
                formatRange.Value2 = dtBCPhep.Rows[0]["CHUC_VU"].ToString();
                formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                formatRange.WrapText = false;


                rowCnt = rowCnt + 5;
                formatRange = oSheet.get_Range("H" + rowCnt + "", "I" + rowCnt);
                formatRange.Merge();
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Name = fontName;
                formatRange.Font.Bold = true;
                formatRange.Value2 = dtBCPhep.Rows[0]["NK"].ToString();
                formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                formatRange.WrapText = false;

                this.Cursor = Cursors.Default;
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
        }
        public string SaveFiles(string MFilter)
        {
            try
            {
                SaveFileDialog f = new SaveFileDialog();
                f.Filter = MFilter;
                f.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                try
                {
                    DialogResult res = f.ShowDialog();
                    if (res == DialogResult.OK)
                        return f.FileName;
                    return "";
                }
                catch
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        private void BorderAround(Microsoft.Office.Interop.Excel.Range range)
        {
            Borders borders = range.Borders;
            borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
            borders.Color = Color.Black;
            borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
            borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;
        }
        static string CharacterIncrement(int colCount)
        {
            int TempCount = 0;
            string returnCharCount = string.Empty;

            if (colCount <= 25)
            {
                TempCount = colCount;
                char CharCount = Convert.ToChar((Convert.ToInt32('A') + TempCount));
                returnCharCount += CharCount;
                return returnCharCount;
            }
            else
            {
                var rev = 0;

                while (colCount >= 26)
                {
                    colCount = colCount - 26;
                    rev++;
                }

                returnCharCount += CharacterIncrement(rev - 1);
                returnCharCount += CharacterIncrement(colCount);
                return returnCharCount;
            }
        }
        private void BaoCaoTongHopThamNien_HN()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCTongHopThamNien_HN", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = lk_NgayIn.EditValue;
                //cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = secondTime;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();

                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 12;


                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);
                string nameColumn = CharacterIncrement(dtBCThang.Columns.Count - 3);


                Microsoft.Office.Interop.Excel.Range row2_TieuDe_BaoCao = oSheet.get_Range("A1", lastColumn + "1");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 18;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 30;
                row2_TieuDe_BaoCao.Value2 = "BÁO CÁO THÂM NIÊN";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Format = oSheet.get_Range("A3", lastColumn + "4"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                Microsoft.Office.Interop.Excel.Range row5_TieuDe_DV = oSheet.get_Range("A3", "A4");
                row5_TieuDe_DV.Merge();
                row5_TieuDe_DV.Value2 = "STT";
                row5_TieuDe_DV.ColumnWidth = 6;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_LDBQ = oSheet.get_Range("B3", "B4");
                row5_TieuDe_LDBQ.Merge();
                row5_TieuDe_LDBQ.Value2 = "Chuyền/Phòng";
                row5_TieuDe_LDBQ.ColumnWidth = 30;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_LDT = oSheet.get_Range("C3", "C4");
                row5_TieuDe_LDT.Merge();
                row5_TieuDe_LDT.Value2 = "Số lao động";
                row5_TieuDe_LDT.ColumnWidth = 11;


                Microsoft.Office.Interop.Excel.Range row6_TieuDe_TT = oSheet.get_Range("D3", "E3");
                row6_TieuDe_TT.Merge();
                row6_TieuDe_TT.Value2 = "Giới tính";
                row6_TieuDe_TT.RowHeight = 14;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_DT = oSheet.get_Range("D4");
                row5_TieuDe_DT.Value2 = "Nam";
                row5_TieuDe_DT.ColumnWidth = 7;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_TN = oSheet.get_Range("E4");
                row5_TieuDe_TN.Value2 = "Nữ";
                row5_TieuDe_TN.ColumnWidth = 7;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_LDG = oSheet.get_Range("F3", "J3");
                row5_TieuDe_LDG.Merge();
                row5_TieuDe_LDG.Value2 = "Thâm niên";
                row5_TieuDe_LDG.ColumnWidth = 44;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_TG = oSheet.get_Range("F4");
                row6_TieuDe_TG.Value2 = "0-3 months";
                row6_TieuDe_TG.ColumnWidth = 11;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_D1T = oSheet.get_Range("G4");
                row6_TieuDe_D1T.Value2 = "3-6 months";
                row6_TieuDe_D1T.ColumnWidth = 11;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_1_3_T = oSheet.get_Range("H4");
                row6_TieuDe_1_3_T.Merge();
                row6_TieuDe_1_3_T.Value2 = "6-9 months";
                row6_TieuDe_1_3_T.ColumnWidth = 11;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_3_6_T = oSheet.get_Range("I4");
                row6_TieuDe_3_6_T.Value2 = "9-12 months";
                row6_TieuDe_3_6_T.ColumnWidth = 11;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_6_9_T = oSheet.get_Range("J4");
                row6_TieuDe_6_9_T.Value2 = "Trên 1 năm";
                row6_TieuDe_6_9_T.ColumnWidth = 11;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_9_12_T = oSheet.get_Range("K4");
                row6_TieuDe_9_12_T.Value2 = "Ghi chú";
                row6_TieuDe_9_12_T.ColumnWidth = 20;

                Microsoft.Office.Interop.Excel.Range fortmatTitleTable = oSheet.get_Range("A3", "K4");
                fortmatTitleTable.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                fortmatTitleTable.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                Microsoft.Office.Interop.Excel.Range formatRange;

                int col = 0;
                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                int rowCONG = 0; // Row để insert dòng tổng
                //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                string sRowBD_DV = ";"; // Lưu lại các dòng của row đơn vị
                string sRowBD_XN = ";"; // Lưu lại các dòng của row xí nghiệp
                int rowBD = 5;
                string[] TEN_DV = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_DV")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[1].Copy(); // Dữ row count data
                string sRowBD_XN_Temp = "";
                for (int i = 0; i < TEN_DV.Count(); i++)
                {
                    if (chanVongDau == "")
                    {
                        rowBD = (keepRowCnt + 3);
                        chanVongDau = "Chan";
                    }
                    DataTable dt = new DataTable();
                    dtBCThang = ds.Tables[0].Copy();
                    dt = dtBCThang.AsEnumerable().Where(r => r["TEN_DV"].ToString().Equals(TEN_DV[i])).CopyToDataTable().Copy();
                    string[] TEN_XN = dt.AsEnumerable().Select(r => r.Field<string>("TEN_XN")).Distinct().ToArray();
                    // Tạo group đơn vị
                    Microsoft.Office.Interop.Excel.Range row_groupDON_VI_Format = oSheet.get_Range("A" + rowBD + "".ToString(), nameColumn + "" + rowBD + "".ToString()); //27 + 31
                    row_groupDON_VI_Format.Interior.Color = Color.FromArgb(255, 255, 0);
                    oSheet.Cells[rowBD, 1] = TEN_DV[i].ToString();
                    oSheet.Cells[rowBD, 1].Merge();
                    oSheet.Cells[rowBD, 1].Font.Bold = true;
                    oSheet.Cells[rowBD, 1].Font.Size = 14;
                    rowBD++;
                    for (int j = 0; j < TEN_XN.Count(); j++)
                    {
                        dtBCThang = ds.Tables[0].Copy();
                        dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_XN") == TEN_XN[j]).CopyToDataTable().Copy();
                        DataRow[] dr = dtBCThang.Select();
                        current_dr = dr.Count();
                        string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                        foreach (DataRow row in dr)
                        {
                            for (col = 0; col < dtBCThang.Columns.Count - 2; col++)
                            {
                                rowData[rowCnt, col] = row[col].ToString();
                            }
                            rowCnt++;
                        }
                        if (chanVongDau == "Chan") // Chạy vòng đầu tiên, rowBD_XN = 0, vì nó nằm dòng đầu tiên thì rowBD lúc này sẽ  = 7, các vòng tiếp theo sẽ lấy cái dòng BĐ của + thêm rowBD_XN = 1 vào để không bị nằm đè lên dòng thứ 9
                        {
                            dr_Cu = 0;
                            rowBD_XN = 0;
                            chanVongDau = "";
                        }
                        else
                        {
                            rowBD_XN = 1;
                        }
                        rowBD = rowBD + dr_Cu + rowBD_XN;
                        //rowCnt = rowCnt + 6 + dr_Cu;
                        rowCnt = rowBD + current_dr - 1;


                        // Tạo group xí nghiệp
                        Microsoft.Office.Interop.Excel.Range row_groupXI_NGHIEP_Format = oSheet.get_Range("A" + rowBD + "".ToString(), nameColumn + "" + rowBD + "".ToString()); //27 + 31
                        row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(146, 208, 80);
                        oSheet.Cells[rowBD, 1] = int_to_Roman(j + 1);
                        oSheet.Cells[rowBD, 2] = TEN_XN[j].ToString();
                        oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Bold = true;
                        oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Italic = true;

                        for (col = 3; col < dtBCThang.Columns.Count - 2; col++)
                        {
                            oSheet.Cells[rowBD, col] = "=+SUM(" + CharacterIncrement(col - 1) + "" + (rowBD + 1).ToString() + ":" + CharacterIncrement(col - 1) + "" + (rowCnt + 1).ToString() + ")";
                            oSheet.Cells[rowBD, col].Font.Bold = true;
                            oSheet.Cells[rowBD, col].Font.Size = 12;
                        }

                        sRowBD_XN = sRowBD_XN + rowBD.ToString() + "+;";
                        sRowBD_XN_Temp = sRowBD_XN;
                        //Đổ dữ liệu của xí nghiệp
                        oSheet.get_Range("A" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;

                        //// Dữ liệu cột tổng tăng
                        //for (int k = rowBD + 1; k <= rowCnt + 1; k++)
                        //{
                        //    oSheet.Cells[k, 3] = "=D" + k + "+E" + k + "";
                        //    oSheet.Cells[k, 6] = "=M" + k + "+N" + k + "";
                        //    oSheet.Cells[k, 15] = "=C" + k + "-F" + k + "";
                        //}
                        dr_Cu = current_dr;
                        keepRowCnt = rowCnt;
                        rowCnt = 0;
                    }

                    // Tính tổng từng đơn vị
                    Microsoft.Office.Interop.Excel.Range rowTOTAL_DON_VI = oSheet.get_Range("A" + (keepRowCnt + 2).ToString() + "".ToString(), nameColumn + "" + (keepRowCnt + 2).ToString() + "".ToString());
                    rowTOTAL_DON_VI.Interior.Color = Color.FromArgb(255, 255, 0);
                    rowTOTAL_DON_VI.Font.Bold = true;
                    rowTOTAL_DON_VI.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    rowTOTAL_DON_VI.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    rowTOTAL_DON_VI = oSheet.get_Range("A" + (keepRowCnt + 2).ToString() + "".ToString(), "B" + (keepRowCnt + 2).ToString() + "".ToString());
                    rowTOTAL_DON_VI.Merge();
                    rowTOTAL_DON_VI.Value = "TỔNG";
                    for (col = 3; col < dtBCThang.Columns.Count - 2; col++)
                    {
                        formatRange = oSheet.get_Range("" + CharacterIncrement(col - 1) + "" + (keepRowCnt + 2).ToString() + "");
                        sRowBD_XN = sRowBD_XN.Substring(0, sRowBD_XN.Length - 2);
                        sRowBD_XN = sRowBD_XN.Replace(';', Convert.ToChar(CharacterIncrement(col - 1)));
                        oSheet.Cells[keepRowCnt + 2, col] = "=" + sRowBD_XN;
                        sRowBD_XN = sRowBD_XN_Temp;
                    }
                    sRowBD_XN = ";";
                    sRowBD_XN_Temp = "";
                }

                rowCnt = keepRowCnt + 1; // Cộng 2 vì ở trên thêm 2 dòng xí nghiệp và cộng

                rowCnt++;

                for (col = 2; col < dtBCThang.Columns.Count - 2; col++)
                {

                    formatRange = oSheet.get_Range(CharacterIncrement(col - 1) + "7", CharacterIncrement(col - 1) + rowCnt.ToString());
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    formatRange.NumberFormat = "0";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                formatRange = oSheet.get_Range("A5", "" + lastColumn + "" + rowCnt + "");
                formatRange.Font.Name = fontName;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.get_Range("B5", "" + "B" + rowCnt + "");
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                BorderAround(oSheet.get_Range("A3", nameColumn + rowCnt.ToString()));

                rowCnt++;
                rowCnt++;
                formatRange = oSheet.get_Range("K" + rowCnt + "", "" + lastColumn + "" + rowCnt + "");
                formatRange.Merge();
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 12;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange.Value = "Tp.HCM , ngày " + lk_NgayIn.DateTime.Day.ToString() + " tháng " + lk_NgayIn.DateTime.Month.ToString() + " năm " + lk_NgayIn.DateTime.Year.ToString() + "";
                rowCnt++;
                formatRange = oSheet.get_Range("E" + rowCnt + "");
                formatRange.Value = "P.TCLĐ";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 12;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange = oSheet.get_Range("K" + rowCnt + "", "" + lastColumn + "" + rowCnt + "");
                formatRange.Merge();
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 12;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange.Value = "Tổng giám đốc";


                ////dịnh dạng
                ////Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 7);

                //string CurentColumn = string.Empty;
                //int colBD = 4;
                //int colKT = dtBCThang.Columns.Count;
                ////format

                //for (col = colBD; col < dtBCThang.Columns.Count - 3; col++)
                //{
                //    CurentColumn = CharacterIncrement(col);
                //    formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                //    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                //    formatRange.NumberFormat = "0.00;-0;;@";
                //    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                //}

                ////colKT++;
                ////CurentColumn = CharacterIncrement(colKT);
                ////formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                ////formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                //////formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                //////Kẻ khung toàn bộ
                //formatRange = oSheet.get_Range("A7", lastColumn + rowCnt.ToString());
                //formatRange.Font.Name = fontName;
                //formatRange.Font.Size = fontSizeNoiDung;
                //BorderAround(oSheet.get_Range("A5", lastColumn + rowCnt.ToString()));
                //// filter
                //oSheet.Application.ActiveWindow.SplitColumn = 4;
                //oSheet.Application.ActiveWindow.FreezePanes = true;
                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        private void BaoCaoTongHopThamNien_DM()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCTongHopThamNien_DM", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = lk_NgayIn.EditValue;
                //cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = secondTime;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();

                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;
                this.Cursor = Cursors.WaitCursor;
                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 12;


                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);
                string nameColumn = CharacterIncrement(dtBCThang.Columns.Count - 3);


                Microsoft.Office.Interop.Excel.Range row2_TieuDe_BaoCao = oSheet.get_Range("A1", lastColumn + "1");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 18;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 30;
                row2_TieuDe_BaoCao.Value2 = "BÁO CÁO THÂM NIÊN";

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_Format = oSheet.get_Range("A3", lastColumn + "4"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                Microsoft.Office.Interop.Excel.Range row5_TieuDe_DV = oSheet.get_Range("A3", "A4");
                row5_TieuDe_DV.Merge();
                row5_TieuDe_DV.Value2 = "STT";
                row5_TieuDe_DV.ColumnWidth = 15;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_LDBQ = oSheet.get_Range("B3", "B4");
                row5_TieuDe_LDBQ.Merge();
                row5_TieuDe_LDBQ.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTo");
                row5_TieuDe_LDBQ.ColumnWidth = 30;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_LDT = oSheet.get_Range("C3", "C4");
                row5_TieuDe_LDT.Merge();
                row5_TieuDe_LDT.Value2 = "Số lao động";
                row5_TieuDe_LDT.ColumnWidth = 11;


                Microsoft.Office.Interop.Excel.Range row6_TieuDe_TT = oSheet.get_Range("D3", "E3");
                row6_TieuDe_TT.Merge();
                row6_TieuDe_TT.Value2 = "Giới tính";
                row6_TieuDe_TT.RowHeight = 14;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_DT = oSheet.get_Range("D4");
                row5_TieuDe_DT.Value2 = "Nam";
                row5_TieuDe_DT.ColumnWidth = 7;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_TN = oSheet.get_Range("E4");
                row5_TieuDe_TN.Value2 = "Nữ";
                row5_TieuDe_TN.ColumnWidth = 7;

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_LDG = oSheet.get_Range("F3", "J3");
                row5_TieuDe_LDG.Merge();
                row5_TieuDe_LDG.Value2 = "Thâm niên";
                row5_TieuDe_LDG.ColumnWidth = 44;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_D1T = oSheet.get_Range("F4");
                row6_TieuDe_D1T.Value2 = "Dưới 1 tháng";
                row6_TieuDe_D1T.ColumnWidth = 11;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_TG = oSheet.get_Range("G4");
                row6_TieuDe_TG.Value2 = "1-3 Tháng";
                row6_TieuDe_TG.ColumnWidth = 11;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_36 = oSheet.get_Range("H4");
                row6_TieuDe_36.Value2 = "3-6 Tháng";
                row6_TieuDe_36.ColumnWidth = 11;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_1_3_T = oSheet.get_Range("I4");
                row6_TieuDe_1_3_T.Merge();
                row6_TieuDe_1_3_T.Value2 = "6-9 Tháng";
                row6_TieuDe_1_3_T.ColumnWidth = 11;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_3_6_T = oSheet.get_Range("J4");
                row6_TieuDe_3_6_T.Value2 = "9-12 Tháng";
                row6_TieuDe_3_6_T.ColumnWidth = 11;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_6_9_T = oSheet.get_Range("K4");
                row6_TieuDe_6_9_T.Value2 = "Trên 1 năm";
                row6_TieuDe_6_9_T.ColumnWidth = 11;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_9_12_T = oSheet.get_Range("L4");
                row6_TieuDe_9_12_T.Value2 = "Trên 1.5 năm";
                row6_TieuDe_9_12_T.ColumnWidth = 11;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_T2N = oSheet.get_Range("M4");
                row6_TieuDe_T2N.Value2 = "Trên 2 năm";
                row6_TieuDe_T2N.ColumnWidth = 11;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_T2_5N = oSheet.get_Range("N4");
                row6_TieuDe_T2_5N.Value2 = "Trên 2.5 năm";
                row6_TieuDe_T2_5N.ColumnWidth = 11;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_T3N = oSheet.get_Range("O4");
                row6_TieuDe_T3N.Value2 = "Trên 3 năm";
                row6_TieuDe_T3N.ColumnWidth = 11;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_T3_5N = oSheet.get_Range("P4");
                row6_TieuDe_T3_5N.Value2 = "Trên 3.5 năm";
                row6_TieuDe_T3_5N.ColumnWidth = 11;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_T4N = oSheet.get_Range("Q4");
                row6_TieuDe_T4N.Value2 = "Trên 4 năm";
                row6_TieuDe_T4N.ColumnWidth = 11;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_T4_5N = oSheet.get_Range("R4");
                row6_TieuDe_T4_5N.Value2 = "Trên 4.5 năm";
                row6_TieuDe_T4_5N.ColumnWidth = 11;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_T5N = oSheet.get_Range("S4");
                row6_TieuDe_T5N.Value2 = "Trên 5 năm";
                row6_TieuDe_T5N.ColumnWidth = 11;

                Microsoft.Office.Interop.Excel.Range row6_TieuDe_GC = oSheet.get_Range("T4");
                row6_TieuDe_GC.Value2 = "Ghi chú";
                row6_TieuDe_GC.ColumnWidth = 20;

                Microsoft.Office.Interop.Excel.Range fortmatTitleTable = oSheet.get_Range("A3", "K4");
                fortmatTitleTable.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                fortmatTitleTable.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                Microsoft.Office.Interop.Excel.Range formatRange;

                int col = 0;
                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                int rowCONG = 0; // Row để insert dòng tổng
                //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                string sRowBD_DV = ";"; // Lưu lại các dòng của row đơn vị
                string sRowBD_XN = ";"; // Lưu lại các dòng của row xí nghiệp
                int rowBD = 5;
                string[] TEN_DV = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_DV")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[1].Copy(); // Dữ row count data
                string sRowBD_XN_Temp = "";
                for (int i = 0; i < TEN_DV.Count(); i++)
                {
                    if (chanVongDau == "")
                    {
                        rowBD = (keepRowCnt + 3);
                        chanVongDau = "Chan";
                    }
                    DataTable dt = new DataTable();
                    dtBCThang = ds.Tables[0].Copy();
                    dt = dtBCThang.AsEnumerable().Where(r => r["TEN_DV"].ToString().Equals(TEN_DV[i])).CopyToDataTable().Copy();
                    string[] TEN_XN = dt.AsEnumerable().Select(r => r.Field<string>("TEN_XN")).Distinct().ToArray();
                    // Tạo group đơn vị
                    Microsoft.Office.Interop.Excel.Range row_groupDON_VI_Format = oSheet.get_Range("A" + rowBD + "".ToString(), nameColumn + "" + rowBD + "".ToString()); //27 + 31
                    row_groupDON_VI_Format.Interior.Color = Color.FromArgb(255, 255, 0);
                    oSheet.Cells[rowBD, 1] = TEN_DV[i].ToString();
                    oSheet.Cells[rowBD, 1].Merge();
                    oSheet.Cells[rowBD, 1].Font.Bold = true;
                    oSheet.Cells[rowBD, 1].Font.Size = 14;
                    rowBD++;
                    for (int j = 0; j < TEN_XN.Count(); j++)
                    {
                        dtBCThang = ds.Tables[0].Copy();
                        dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_XN") == TEN_XN[j]).CopyToDataTable().Copy();
                        DataRow[] dr = dtBCThang.Select();
                        current_dr = dr.Count();
                        string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                        foreach (DataRow row in dr)
                        {
                            for (col = 0; col < dtBCThang.Columns.Count - 2; col++)
                            {
                                rowData[rowCnt, col] = row[col].ToString();
                            }
                            rowCnt++;
                        }
                        if (chanVongDau == "Chan") // Chạy vòng đầu tiên, rowBD_XN = 0, vì nó nằm dòng đầu tiên thì rowBD lúc này sẽ  = 7, các vòng tiếp theo sẽ lấy cái dòng BĐ của + thêm rowBD_XN = 1 vào để không bị nằm đè lên dòng thứ 9
                        {
                            dr_Cu = 0;
                            rowBD_XN = 0;
                            chanVongDau = "";
                        }
                        else
                        {
                            rowBD_XN = 1;
                        }
                        rowBD = rowBD + dr_Cu + rowBD_XN;
                        //rowCnt = rowCnt + 6 + dr_Cu;
                        rowCnt = rowBD + current_dr - 1;


                        // Tạo group xí nghiệp
                        Microsoft.Office.Interop.Excel.Range row_groupXI_NGHIEP_Format = oSheet.get_Range("A" + rowBD + "".ToString(), nameColumn + "" + rowBD + "".ToString()); //27 + 31
                        row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(146, 208, 80);
                        oSheet.Cells[rowBD, 1] = int_to_Roman(j + 1);
                        oSheet.Cells[rowBD, 2] = TEN_XN[j].ToString();
                        oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Bold = true;
                        oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Italic = true;

                        for (col = 3; col < dtBCThang.Columns.Count - 2; col++)
                        {
                            oSheet.Cells[rowBD, col] = "=+SUM(" + CharacterIncrement(col - 1) + "" + (rowBD + 1).ToString() + ":" + CharacterIncrement(col - 1) + "" + (rowCnt + 1).ToString() + ")";
                            oSheet.Cells[rowBD, col].Font.Bold = true;
                            oSheet.Cells[rowBD, col].Font.Size = 12;
                        }

                        sRowBD_XN = sRowBD_XN + rowBD.ToString() + "+;";
                        sRowBD_XN_Temp = sRowBD_XN;
                        //Đổ dữ liệu của xí nghiệp
                        oSheet.get_Range("A" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;

                        //// Dữ liệu cột tổng tăng
                        //for (int k = rowBD + 1; k <= rowCnt + 1; k++)
                        //{
                        //    oSheet.Cells[k, 3] = "=D" + k + "+E" + k + "";
                        //    oSheet.Cells[k, 6] = "=M" + k + "+N" + k + "";
                        //    oSheet.Cells[k, 15] = "=C" + k + "-F" + k + "";
                        //}
                        dr_Cu = current_dr;
                        keepRowCnt = rowCnt;
                        rowCnt = 0;
                    }

                    // Tính tổng từng đơn vị
                    Microsoft.Office.Interop.Excel.Range rowTOTAL_DON_VI = oSheet.get_Range("A" + (keepRowCnt + 2).ToString() + "".ToString(), nameColumn + "" + (keepRowCnt + 2).ToString() + "".ToString());
                    rowTOTAL_DON_VI.Interior.Color = Color.FromArgb(255, 255, 0);
                    rowTOTAL_DON_VI.Font.Bold = true;
                    rowTOTAL_DON_VI.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    rowTOTAL_DON_VI.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    rowTOTAL_DON_VI = oSheet.get_Range("A" + (keepRowCnt + 2).ToString() + "".ToString(), "B" + (keepRowCnt + 2).ToString() + "".ToString());
                    rowTOTAL_DON_VI.Merge();
                    rowTOTAL_DON_VI.Value = "TỔNG";
                    for (col = 3; col < dtBCThang.Columns.Count - 2; col++)
                    {
                        formatRange = oSheet.get_Range("" + CharacterIncrement(col - 1) + "" + (keepRowCnt + 2).ToString() + "");
                        sRowBD_XN = sRowBD_XN.Substring(0, sRowBD_XN.Length - 2);
                        sRowBD_XN = sRowBD_XN.Replace(';', Convert.ToChar(CharacterIncrement(col - 1)));
                        oSheet.Cells[keepRowCnt + 2, col] = "=" + sRowBD_XN;
                        sRowBD_XN = sRowBD_XN_Temp;
                    }
                    sRowBD_XN = ";";
                    sRowBD_XN_Temp = "";
                }

                rowCnt = keepRowCnt + 1; // Cộng 2 vì ở trên thêm 2 dòng xí nghiệp và cộng

                rowCnt++;

                for (col = 2; col < dtBCThang.Columns.Count - 2; col++)
                {

                    formatRange = oSheet.get_Range(CharacterIncrement(col - 1) + "7", CharacterIncrement(col - 1) + rowCnt.ToString());
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    formatRange.NumberFormat = "0";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                formatRange = oSheet.get_Range("A5", "" + lastColumn + "" + rowCnt + "");
                formatRange.Font.Name = fontName;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.get_Range("B5", "" + "B" + rowCnt + "");
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                BorderAround(oSheet.get_Range("A3", nameColumn + rowCnt.ToString()));


                ////dịnh dạng
                ////Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 7);

                //string CurentColumn = string.Empty;
                //int colBD = 4;
                //int colKT = dtBCThang.Columns.Count;
                ////format

                //for (col = colBD; col < dtBCThang.Columns.Count - 3; col++)
                //{
                //    CurentColumn = CharacterIncrement(col);
                //    formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                //    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                //    formatRange.NumberFormat = "0.00;-0;;@";
                //    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                //}

                ////colKT++;
                ////CurentColumn = CharacterIncrement(colKT);
                ////formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                ////formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                //////formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                //////Kẻ khung toàn bộ
                //formatRange = oSheet.get_Range("A7", lastColumn + rowCnt.ToString());
                //formatRange.Font.Name = fontName;
                //formatRange.Font.Size = fontSizeNoiDung;
                //BorderAround(oSheet.get_Range("A5", lastColumn + rowCnt.ToString()));
                //// filter
                //oSheet.Application.ActiveWindow.SplitColumn = 4;
                //oSheet.Application.ActiveWindow.FreezePanes = true;
                this.Cursor = Cursors.Default;
                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        // IN HỢP ĐỒNG
        #region In hợp đồng lao động
        private void InHDLD_DM(string sBT)
        {

            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDong_DM(DateTime.Now);

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong_DM", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = -1;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.BigInt).Value = -1;
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
            catch
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCanTaoHopDongTruocKhiIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        private void HopDongLaoDong_NC(string sBT)
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDong_NB(DateTime.Now);

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong_NB", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = -1;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = -1;
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
        private void HopDongLaoDong_NB(string sBT)
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDong_NB(DateTime.Now);

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong_NB", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = -1;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = -1;
                cmd1.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();


                string sPath = "";
                sPath = SaveFiles("Work file (*.doc)|*.docx");
                if (sPath == "") return;

                sPath = sPath.Substring(0, sPath.IndexOf(DateTime.Now.ToString("yyyyMMdd")));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    string sPathTemp = "";
                    sPathTemp = sPath + Convert.ToString(dt.Rows[i]["MS_CN"]) + "_HĐLĐ.docx";
                    if (System.IO.File.Exists(sPathTemp))
                    {
                        try
                        {
                            FileInfo file = new FileInfo(sPathTemp);
                            file.Delete();
                        }
                        catch { }
                    }
                    //fill vào báo cáo
                    Document baoCao = new Document("Template\\TemplateNB\\HopDongLaoDong.doc");
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
                    baoCao.Save(sPathTemp);
                    //Process.Start(sPath);
                }
                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgInThanhCong"), Commons.Form_Alert.enmType.Success);
            }
            catch { }
        }
        private void HopDongLaoDong_HN(string sBT)
        {
            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDong_HN(DateTime.Now);

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong_HN", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = -1;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = -1;
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
        private void HopDongLaoDong_SB(string sBT)
        {
            #region in cách mới
            //DataTable dt = new DataTable();
            //DataTable dtbc = new DataTable();
            //try
            //{
            //    System.Data.SqlClient.SqlConnection conn1;
            //    dt = new DataTable();
            //    frmViewReport frm = new frmViewReport();
            //    frm.rpt = new rptHopDongLaoDong_SB(DateTime.Now);

            //    conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            //    conn1.Open();

            //    System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong_SB", conn1);
            //    cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
            //    cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            //    cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
            //    cmd1.Parameters.Add("@ID_CN", SqlDbType.Int).Value = -1;
            //    cmd1.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = -1;
            //    cmd1.CommandType = CommandType.StoredProcedure;

            //    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
            //    DataSet ds = new DataSet();
            //    adp.Fill(ds);
            //    dt = new DataTable();
            //    dt = ds.Tables[0].Copy();
            //    string sPath = "";
            //    sPath = SaveFiles("Work file (*.doc)|*.docx");
            //    if (sPath == "") return;

            //    sPath = sPath.Substring(0, sPath.IndexOf(DateTime.Now.ToString("yyyyMMdd")));
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        DataRow row = dt.Rows[i];
            //        string sPathTemp = "";
            //        sPathTemp = sPath + Convert.ToString(dt.Rows[i]["MS_CN"]) + "_HĐLĐ.docx";
            //        if (System.IO.File.Exists(sPathTemp))
            //        {
            //            try
            //            {
            //                FileInfo file = new FileInfo(sPathTemp);
            //                file.Delete();
            //            }
            //            catch { }
            //        }
            //        //fill vào báo cáo
            //        Document baoCao = new Document("Template\\TemplateSB\\HopDongLaoDong.doc");
            //        foreach (DataColumn item in dt.Columns)
            //        {
            //            if (Commons.Modules.ObjSystems.IsnullorEmpty(row[item]))
            //            {
            //                baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { "" });

            //                continue;
            //            }
            //            switch (item.DataType.Name)
            //            {
            //                case "DateTime":
            //                    {
            //                        baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { Convert.ToDateTime(row[item]).ToString("dd/MM/yyyy") });
            //                        break;
            //                    }
            //                case "Double":
            //                    {
            //                        baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { string.Format("{0:#,##0}", row[item]) });
            //                        break;
            //                    }
            //                default:
            //                    {
            //                        baoCao.MailMerge.Execute(new[] { item.ColumnName }, new[] { row[item] });
            //                        break;

            //                    }
            //            }
            //        }
            //        baoCao.Save(sPathTemp);
            //        //Process.Start(sPath);
            //    }
            //    Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgInThanhCong"), Commons.Form_Alert.enmType.Success);
            //}
            //catch { }
            #endregion

            #region in cách cũ
            try
            {
                DataTable dt = new DataTable();
                DataTable dtbc = new DataTable();
                System.Data.SqlClient.SqlConnection conn;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDong_SB(lk_NgayIn.DateTime);

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong_SB", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = -1;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
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
            #endregion
        }
        private void InHopDongLaoDong_VV(string sBT)
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
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd1.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                string sPath = "";
                sPath = SaveFiles("Work file (*.doc)|*.docx");
                if (sPath == "") return;

                sPath = sPath.Substring(0, sPath.IndexOf(DateTime.Now.ToString("yyyyMMdd")));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    string sPathTemp = "";
                    sPathTemp = sPath + Convert.ToString(dt.Rows[i]["MS_CN"]) + "_HĐLĐ.docx";
                    if (System.IO.File.Exists(sPathTemp))
                    {
                        try
                        {
                            FileInfo file = new FileInfo(sPathTemp);
                            file.Delete();
                        }
                        catch { }
                    }
                    //fill vào báo cáo
                    Document baoCao = new Document("Template\\TemplateVV\\HopDongLaoDong.doc");
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
                    baoCao.Save(sPathTemp);
                    //Process.Start(sPath);
                }
                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgInThanhCong"), Commons.Form_Alert.enmType.Success);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InHopDongLaoDong_TG(string sBT)
        {
            #region In cách mới
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
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd1.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                string sPath = "";
                sPath = SaveFiles("Work file (*.doc)|*.docx");
                if (sPath == "") return;

                sPath = sPath.Substring(0, sPath.IndexOf(DateTime.Now.ToString("yyyyMMdd")));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    string sPathTemp = "";
                    sPathTemp = sPath + Convert.ToString(dt.Rows[i]["MS_CN"]) + "_HĐLĐ.docx";
                    if (System.IO.File.Exists(sPathTemp))
                    {
                        try
                        {
                            FileInfo file = new FileInfo(sPathTemp);
                            file.Delete();
                        }
                        catch { }
                    }
                    //fill vào báo cáo
                    Document baoCao = new Document("Template\\TemplateTG\\HopDongLaoDong.doc");
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
                    baoCao.Save(sPathTemp);
                    //Process.Start(sPath);
                }
                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgInThanhCong"), Commons.Form_Alert.enmType.Success);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion

        }
        private void InHopDongLaoDong_BT(string sBT)
        {
            try
            {
                //lấy data dữ liệu

                System.Data.SqlClient.SqlConnection conn1;
                DataTable dt = new DataTable();

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong_BT", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd1.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                ds.Tables[0].TableName = "HDLD";
                string sPath = "";
                sPath = SaveFiles("Excel file (*.xlsx)|*.xlsx");
                if (sPath == "") return;

                sPath = sPath.Substring(0, sPath.IndexOf(DateTime.Now.ToString("yyyyMMdd")));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataSet dsTemp = ds.Clone();
                    dsTemp.Tables[0].TableName = "Talbe1";

                    dt = new DataTable();
                    dt = ds.Tables[0];
                    DataRow dr = dt.Rows[i];


                    // thêm DataTable mới vào DataSet mới
                    DataTable dt1 = new DataTable();
                    dt1 = dt.Clone().Copy();

                    DataRow dr1 = dt1.NewRow();
                    dr1.ItemArray = dr.ItemArray;
                    // thêm dòng đầu tiên của datatalbe ban đầu vào Datatable mới trong DataSet mới
                    dt1.Rows.Add(dr1);
                    dsTemp.Tables.Add(dt1);

                    string sPathTemp = "";
                    sPathTemp = sPath + Convert.ToString(dt1.Rows[0]["MS_CN"]) + "_HĐLD..xlsx";

                    if (System.IO.File.Exists(sPathTemp))
                    {
                        try
                        {
                            FileInfo file = new FileInfo(sPathTemp);
                            file.Delete();
                        }
                        catch { }
                    }

                    Commons.TemplateExcel.FillReport(sPathTemp, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateBT\\HopDongLaoDong.xlsx", dsTemp, new string[] { "{", "}" });
                }
                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgInThanhCong"), Commons.Form_Alert.enmType.Success);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InHopDongLaoDong_AP(string sBT)
        {
            try
            {
                //lấy data dữ liệu

                System.Data.SqlClient.SqlConnection conn1;
                DataTable dt = new DataTable();

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong_AP", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd1.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                string sPath = "";
                sPath = SaveFiles("Work file (*.doc)|*.docx");
                if (sPath == "") return;

                sPath = sPath.Substring(0, sPath.IndexOf(DateTime.Now.ToString("yyyyMMdd")));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    string sPathTemp = "";
                    sPathTemp = sPath + Convert.ToString(dt.Rows[i]["MS_CN"]) + "_HĐLĐ.docx";
                    if (System.IO.File.Exists(sPathTemp))
                    {
                        try
                        {
                            FileInfo file = new FileInfo(sPathTemp);
                            file.Delete();
                        }
                        catch { }
                    }
                    //fill vào báo cáo
                    Document baoCao = new Document("Template\\TemplateAP\\HopDongLaoDong.doc");
                    baoCao.MailMerge.Execute(new[] { "Ngay_Thang_Nam_BC" }, new[] { string.Format("Hôm nay, ngày {0} tháng {1} năm {2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year) });
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
                    baoCao.Save(sPathTemp);
                    //Process.Start(sPath);
                }
                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgInThanhCong"), Commons.Form_Alert.enmType.Success);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InHopDongLaoDong_MT(string sBT)
        {

            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDong_MT(lk_NgayIn.DateTime);

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = -1;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.BigInt).Value = -1;
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
            catch
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCanTaoHopDongTruocKhiIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        #endregion

        #region in hợp đồng thử việc
        private void InHopDongThuViec_VV(string sBT)
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
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd1.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                string sPath = "";
                sPath = SaveFiles("Work file (*.doc)|*.docx");
                if (sPath == "") return;

                sPath = sPath.Substring(0, sPath.IndexOf(DateTime.Now.ToString("yyyyMMdd")));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    string sPathTemp = "";
                    sPathTemp = sPath + Convert.ToString(dt.Rows[i]["MS_CN"]) + "_HĐTV.docx";
                    if (System.IO.File.Exists(sPathTemp))
                    {
                        try
                        {
                            FileInfo file = new FileInfo(sPathTemp);
                            file.Delete();
                        }
                        catch { }
                    }
                    //fill vào báo cáo
                    Document baoCao = new Document("Template\\TemplateVV\\HopDongThuViec.doc");
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
                    baoCao.Save(sPathTemp);
                    //Process.Start(sPath);
                }
                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgInThanhCong"), Commons.Form_Alert.enmType.Success);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InHopDongThuViec_TG(string sBT)
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
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd1.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                string sPath = "";
                sPath = SaveFiles("Work file (*.doc)|*.docx");
                if (sPath == "") return;

                sPath = sPath.Substring(0, sPath.IndexOf(DateTime.Now.ToString("yyyyMMdd")));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    string sPathTemp = "";
                    sPathTemp = sPath + Convert.ToString(dt.Rows[i]["MS_CN"]) + "_HĐTV.docx";
                    if (System.IO.File.Exists(sPathTemp))
                    {
                        try
                        {
                            FileInfo file = new FileInfo(sPathTemp);
                            file.Delete();
                        }
                        catch { }
                    }
                    //fill vào báo cáo
                    Document baoCao = new Document("Template\\TemplateTG\\HopDongThuViec.doc");
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
                    baoCao.Save(sPathTemp);
                    //Process.Start(sPath);
                }
                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgInThanhCong"), Commons.Form_Alert.enmType.Success);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InHopDongThuViec_BT(string sBT)
        {
            try
            {
                //lấy data dữ liệu

                System.Data.SqlClient.SqlConnection conn1;

                DataTable dt = new DataTable();
                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongThuViec_BT", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd1.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                ds.Tables[0].TableName = "HDTV";
                string sPath = "";
                sPath = SaveFiles("Excel file (*.xlsx)|*.xlsx");
                if (sPath == "") return;

                sPath = sPath.Substring(0, sPath.IndexOf(DateTime.Now.ToString("yyyyMMdd")));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataSet dsTemp = ds.Clone();
                    dsTemp.Tables[0].TableName = "Talbe1";

                    dt = new DataTable();
                    dt = ds.Tables[0];
                    DataRow dr = dt.Rows[i];


                    // thêm DataTable mới vào DataSet mới
                    DataTable dt1 = new DataTable();
                    dt1 = dt.Clone().Copy();

                    DataRow dr1 = dt1.NewRow();
                    dr1.ItemArray = dr.ItemArray;
                    // thêm dòng đầu tiên của datatalbe ban đầu vào Datatable mới trong DataSet mới
                    dt1.Rows.Add(dr1);
                    dsTemp.Tables.Add(dt1);

                    string sPathTemp = "";
                    sPathTemp = sPath + Convert.ToString(dt1.Rows[0]["MS_CN"]) + "_HĐTV..xlsx";

                    if (System.IO.File.Exists(sPathTemp))
                    {
                        try
                        {
                            FileInfo file = new FileInfo(sPathTemp);
                            file.Delete();
                        }
                        catch { }
                    }

                    Commons.TemplateExcel.FillReport(sPathTemp, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateBT\\HopDongThuViec.xlsx", dsTemp, new string[] { "{", "}" });
                }
                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgInThanhCong"), Commons.Form_Alert.enmType.Success);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void HopDongThuViec_NB(string sBT)
        {
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDongThuViec_NB(DateTime.Now);
                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongThuViec_NB", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = -1;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.BigInt).Value = -1;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd1.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dtTemp = new DataTable();
                dtTemp = ds.Tables[0].Copy();

                string sPath = "";
                sPath = SaveFiles("Work file (*.doc)|*.docx");
                if (sPath == "") return;

                sPath = sPath.Substring(0, sPath.IndexOf(DateTime.Now.ToString("yyyyMMdd")));
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    DataRow row = dtTemp.Rows[i];
                    string sPathTemp = "";
                    sPathTemp = sPath + Convert.ToString(dtTemp.Rows[i]["MS_CN"]) + "_HĐTV.docx";
                    if (System.IO.File.Exists(sPathTemp)) // kiểm tra xem hợp đồng thử việc của công nhân đó đã có trong forder này chưa, nếu có rồi thì xóa 
                    {
                        try
                        {
                            FileInfo file = new FileInfo(sPathTemp);
                            file.Delete();
                        }
                        catch { }
                    }

                    // kiểm tra mức lương để biết in ra báo cáo nào
                    string sTenFile = "Template\\TemplateNB\\HopDongThuViec.doc";
                    if (Convert.ToDouble(dtTemp.Rows[i]["MUC_LUONG_CHINH"]) == 0) // nếu lương bằng 0 thì in hợp đồng không lương 
                    {
                        sTenFile = "Template\\TemplateNB\\HopDongThuViecTT.doc";
                    }

                    //fill vào báo cáo
                    Document baoCao = new Document(sTenFile);
                    foreach (DataColumn item in dtTemp.Columns)
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
                    baoCao.Save(sPathTemp);
                    //Process.Start(sPath);
                }
                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgInThanhCong"), Commons.Form_Alert.enmType.Success);
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBT);
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
        }
        private void HopDongThuViec_NC(string sBT)
        {
            try
            {

                System.Data.SqlClient.SqlConnection conn1;
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDongThuViec_NB(DateTime.Now);
                DataTable dt = new DataTable();
                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongThuViec_NB", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = -1;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.BigInt).Value = -1;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd1.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);

                dt = new DataTable();
                dt = ds.Tables[1].Copy();
                dt.TableName = "NOI_DUNG";
                frm.AddDataSource(dt);

                Commons.Modules.ObjSystems.XoaTable(sBT);

                frm.ShowDialog();
            }
            catch
            {
                Commons.Modules.ObjSystems.XoaTable(sBT);
            }
        }
        private void InHopDongThuViec_SB(string sBT)
        {
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDongThuViec_SB(DateTime.Now);
                DataTable dt = new DataTable();
                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongThuViec_SB", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = -1;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.BigInt).Value = -1;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
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

                Commons.Modules.ObjSystems.XoaTable(sBT);

                frm.ShowDialog();
            }
            catch
            {
                Commons.Modules.ObjSystems.XoaTable(sBT);
            }
        }
        private void InHDTV_DM(string sBT)
        {
            DataTable dt = new DataTable();
            try
            {

                System.Data.SqlClient.SqlConnection conn1;
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDongThuViec_DM(lk_NgayIn.DateTime);
                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongThuViec_DM", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = -1;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.BigInt).Value = -1;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd1.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);

                dt = new DataTable();
                dt = ds.Tables[1].Copy();
                dt.TableName = "NOI_DUNG";
                frm.AddDataSource(dt);

                frm.ShowDialog();
            }
            catch
            {
            }
        }
        private void InHopDongThuViec_MT(string sBT)
        {
            DataTable dt = new DataTable();
            try
            {

                System.Data.SqlClient.SqlConnection conn1;
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongThuViec_CDDH(lk_NgayIn.DateTime);
                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongThuViec_MT", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = -1;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.BigInt).Value = -1;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd1.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);

                dt = new DataTable();
                dt = ds.Tables[1].Copy();
                dt.TableName = "NOI_DUNG";
                frm.AddDataSource(dt);

                frm.ShowDialog();
            }
            catch
            {
            }
        }
        #endregion
        public static string int_to_Roman(int n)
        {
            string[] roman_symbol = { "MMM", "MM", "M", "CM", "DCCC", "DCC", "DC", "D", "CD", "CCC", "CC", "C", "XC", "LXXX", "LXX", "LX", "L", "XL", "XXX", "XX", "X", "IX", "VIII", "VII", "VI", "V", "IV", "III", "II", "I" };
            int[] int_value = { 3000, 2000, 1000, 900, 800, 700, 600, 500, 400, 300, 200, 100, 90, 80, 70, 60, 50, 40, 30, 20, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

            var roman_numerals = new System.Text.StringBuilder();
            var index_num = 0;
            while (n != 0)
            {
                if (n >= int_value[index_num])
                {
                    n -= int_value[index_num];
                    roman_numerals.Append(roman_symbol[index_num]);
                }
                else
                {
                    index_num++;
                }
            }

            return roman_numerals.ToString();
        }

        private void dTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag.ToString() != "rdo_HDKyGiaiDoan") return;
            LoadGrdCongNhan();
        }

        private void dDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag.ToString() != "rdo_HDKyGiaiDoan") return;
            LoadGrdCongNhan();
        }

        private void LK_TO_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadGrdCongNhan();
        }
    }
}
