using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Vs.Report;
using System.Windows.Forms;

namespace Vs.HRM
{
    public partial class ucBaoCaoCongTac : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBaoCaoCongTac()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this,windowsUIButton);
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "Print":
                    {
                        frmViewReport frm = new frmViewReport();

                        switch (rdo_ChonBaoCao.SelectedIndex)
                        {
                            case 0:
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "NB":
                                            {
                                                System.Data.SqlClient.SqlConnection conn;
                                                DataTable dt = new DataTable();
                                                frm = new frmViewReport();
                                                frm.rpt = new rptBCQuaTrinhCongTacTH_NB(lk_NgayIn.DateTime, dTuNgay.DateTime, dDenNgay.DateTime);
                                                try
                                                {
                                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                    conn.Open();

                                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCQuaTrinhCongTacTH_NB", conn);

                                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                                    cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = lk_NgayIn.EditValue;
                                                    cmd.CommandType = CommandType.StoredProcedure;
                                                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                                    DataSet ds = new DataSet();
                                                    adp.Fill(ds);
                                                    dt = new DataTable();
                                                    dt = ds.Tables[0].Copy();
                                                    dt.TableName = "DA_TA";
                                                    frm.AddDataSource(dt);
                                                }
                                                catch (Exception ex)
                                                {
                                                }
                                                frm.ShowDialog();
                                                break;
                                            }
                                        default:
                                            {
                                                System.Data.SqlClient.SqlConnection conn;
                                                DataTable dt = new DataTable();
                                                frm = new frmViewReport();
                                                frm.rpt = new rptBCQuaTrinhCongTacTH(lk_NgayIn.DateTime, dTuNgay.DateTime, dDenNgay.DateTime);
                                                try
                                                {
                                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                                    conn.Open();

                                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCQuaTrinhCongTacTH", conn);

                                                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                                    cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = lk_NgayIn.EditValue;
                                                    cmd.CommandType = CommandType.StoredProcedure;
                                                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                                    DataSet ds = new DataSet();
                                                    adp.Fill(ds);
                                                    dt = new DataTable();
                                                    dt = ds.Tables[0].Copy();
                                                    dt.TableName = "DA_TA";
                                                    frm.AddDataSource(dt);
                                                }
                                                catch (Exception ex)
                                                {
                                                }
                                                frm.ShowDialog();
                                                break;
                                            }
                                    }
                                }
                                break;
                            case 1:
                                {
                                    if(Commons.Modules.KyHieuDV == "NB")
                                    {
                                        InExcelDSCBCNVDieuChuyen_NB();

                                    }

                                    else
                                    {
                                        ////DateTime firstDateTime = new DateTime(DateTime.Now.Year, Convert.ToInt32(txtThang.EditValue), 1);
                                        ////DateTime secondDateTime = new DateTime(DateTime.Now.Year, Convert.ToInt32(txtThang.EditValue), DateTime.DaysInMonth(DateTime.Now.Year, Convert.ToInt32(txtThang.EditValue)));

                                       ////// string sTieuDe = "DANH SÁCH THAY ĐỔI LƯƠNG " + Convert.ToString(txtThang.EditValue);

                                        System.Data.SqlClient.SqlConnection conn1;
                                        DataTable dt = new DataTable();
                                        string sPS = "rptDSCBCNVDieuChuyen";
                                        if (Commons.Modules.KyHieuDV == "NB")
                                        {
                                            sPS = "rptDSCBCNVDieuChuyen_NB";
                                            frm.rpt = new rptDSCBNVDieuChuyen_NB(lk_NgayIn.DateTime, dTuNgay.DateTime, dDenNgay.DateTime);
                                        }
                                        else
                                        {
                                            frm.rpt = new rptDSCBNVDieuChuyen(lk_NgayIn.DateTime, dTuNgay.DateTime, dDenNgay.DateTime);
                                        }

                                        try
                                        {
                                            conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn1.Open();

                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sPS, conn1);

                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                            cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.EditValue;
                                            cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.EditValue;
                                            //    cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = 1;
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
                                        frm.ShowDialog();
                                    }

                                    break;
                                }

                            default:
                                break;
                        }
                        break;

                    }
                default:
                    break;
            }
        }

        private void InExcelDSCBCNVDieuChuyen_NB()
        {
            try
            {
                string sPS = "rptDSCBCNVDieuChuyen_NB";
                System.Data.SqlClient.SqlConnection connect = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                connect.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sPS, connect);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = dTuNgay.EditValue;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = dDenNgay.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
              
                string sFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                try
                {
                    string sDDFile = Commons.Modules.ObjSystems.CapnhatTL("");
                    if (sDDFile != "\\")
                        sFileName = sDDFile + "\\" + sFileName;
                }
                catch { }

                System.IO.FileInfo file = new System.IO.FileInfo(sFileName);
                if (file.Exists)
                {
                    file.Delete();
                }

                //rptDSCBCNVDieuChuyen_NB
                ExcelPackage pck = new ExcelPackage(file);
                var ws1 = pck.Workbook.Worksheets.Add(Commons.Modules.ObjLanguages.GetLanguage(sPS, "lblTIEU_DE"));

                Commons.Modules.MExcel.MTTChung(ws1, 1, 1, 0, 0);
                int iDong = 4;
                Commons.Modules.MExcel.MText(ws1, sPS, "lblTIEU_DE", iDong, 1, iDong, dt.Columns.Count, true, true, 13, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                iDong++;
                Commons.Modules.MExcel.MText(ws1, "", Commons.Modules.ObjLanguages.GetLanguage(sPS, "sTNgay") + " " + dTuNgay.DateTime.ToString("dd/MM/yyyy") + " " + Commons.Modules.ObjLanguages.GetLanguage(sPS, "sDNgay") + " " + dDenNgay.DateTime.ToString("dd/MM/yyyy"), iDong, 1, iDong, dt.Columns.Count, true, true, 13, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                iDong = iDong + 3;

                List<List<Object>> WidthColumns = new List<List<Object>>();
                List<Object> WidthColumnsName = new List<Object>();

                WidthColumnsName = new List<Object>() { "STT", 5 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "SO_QD", 10 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "HO_TEN", 20 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "MS_CN", 10 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "DK", 5 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "CK", 5 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "TEN_TO_CU", 13 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "TEN_TO_MOI", 13 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "NGAY_HIEU_LUC", 15, "dd/MM/yyyy" };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "CONG_VIEC_CU", 25 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "CONG_VIEC_MOI", 25 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "MUC_LUONG_MOI", 15 };
                WidthColumns.Add(WidthColumnsName);
                WidthColumnsName = new List<Object>() { "GHI_CHU", 15 };
                WidthColumns.Add(WidthColumnsName);

                ws1.Cells[iDong, 1].LoadFromDataTable(dt, true);
                ws1.Row(iDong - 1).Height = 30;
                Commons.Modules.MExcel.MFormatExcel(ws1, dt, iDong - 1, sPS, WidthColumns, true, true, true);
                Commons.Modules.MExcel.MFormatExcel(ws1, dt, iDong, sPS, WidthColumns, true, true, true);

                //Format
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    switch (dt.Columns[i].ColumnName)
                    {
                        case "STT":
                            ws1.Cells[iDong - 1, i + 1, iDong, i + 1].Merge = true;
                            ws1.Cells[iDong, i + 1, iDong + dt.Rows.Count, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            break;
                        case "MS_CN":
                            ws1.Cells[iDong - 1, i + 1, iDong, i + 1].Merge = true;
                            ws1.Cells[iDong, i + 1, iDong + dt.Rows.Count, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            try { ws1.Cells[iDong, i + 1, iDong + dt.Rows.Count, i + 1].Style.Numberformat.Format = "0"; } catch { }
                            break;
                        case "DK":
                            ws1.Cells[iDong - 1, i + 1, iDong - 1, i + 2].Merge = true;
                            ws1.Cells[iDong - 1, i + 1, iDong - 1, i + 2].Value = Commons.Modules.ObjLanguages.GetLanguage(sPS, "sTinhTrangHopDong");
                            ws1.Cells[iDong, i + 1, iDong + dt.Rows.Count, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            break;
                        case "CK":
                            ws1.Cells[iDong, i + 1, iDong + dt.Rows.Count, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            break;
                        case "NGAY_HIEU_LUC":
                            ws1.Cells[iDong - 1, i + 1, iDong, i + 1].Merge = true;
                            ws1.Cells[iDong, i + 1, iDong + dt.Rows.Count, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            break;
                        case "CONG_VIEC_CU":
                            ws1.Cells[iDong - 1, i + 1, iDong, i + 1].Merge = true;
                            ws1.Cells[iDong, i + 1, iDong + dt.Rows.Count, i + 1].Style.WrapText = true;
                            break;
                        case "MUC_LUONG_MOI":
                            ws1.Cells[iDong - 1, i + 1, iDong, i + 1].Merge = true;
                            ws1.Cells[iDong, i + 1, iDong + dt.Rows.Count, i + 1].Style.WrapText = true;
                            break;
                        default:
                            ws1.Cells[iDong - 1, i + 1, iDong, i + 1].Merge = true;
                            break;
                    }
                }

                ws1.Cells[1, 1, 11 + dt.Rows.Count, dt.Columns.Count + 1].Style.Font.Name = "Times New Roman";
                ws1.Cells[6, 1, 11 + dt.Rows.Count, dt.Columns.Count + 1].Style.Font.Size = 10;

                iDong = iDong + dt.Rows.Count + 1;

                Commons.Modules.MExcel.MText(ws1, "", Commons.Modules.ObjLanguages.GetLanguage("NgayThangNam", "Ngay") + " " + lk_NgayIn.DateTime.Day + " " + Commons.Modules.ObjLanguages.GetLanguage("NgayThangNam", "Thang") + " " + lk_NgayIn.DateTime.Month + " " + Commons.Modules.ObjLanguages.GetLanguage("NgayThangNam", "Nam") + " " + lk_NgayIn.DateTime.Year, iDong + 1, dt.Columns.Count - 2, iDong + 1, dt.Columns.Count, true, true, 10, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                Commons.Modules.MExcel.MText(ws1, sPS, "sNguoiLapBieu", iDong + 2, 1, iDong + 2, 3, true, true, 10, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                Commons.Modules.MExcel.MText(ws1, sPS, "sPhongHCHS", iDong + 2, 8, iDong + 2, 9, true, true, 10, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                Commons.Modules.MExcel.MText(ws1, sPS, "sBanGiamDoc", iDong + 2, dt.Columns.Count - 2, iDong + 2, dt.Columns.Count, true, true, 10, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

                if (file.Exists)
                    file.Delete();
                pck.SaveAs(file);
                System.Diagnostics.Process.Start(file.FullName);
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }


        private void ucBaoCaoCongTac_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_DON_VI, Commons.Modules.ObjSystems.DataDonVi(false), "ID_DV", "TEN_DV", "TEN_DV");
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);

            Commons.OSystems.SetDateEditFormat(dTuNgay);
            Commons.OSystems.SetDateEditFormat(dDenNgay);
            Commons.OSystems.SetDateEditFormat(lk_NgayIn);

            dTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year));
            dDenNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year)).AddMonths(1).AddDays(-1);
            lk_NgayIn.EditValue = DateTime.Today;
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

        private void tablePanel1_Validated(object sender, EventArgs e)
        {

        }

        private void dtThang_Validated(object sender, EventArgs e)
        {
            //try
            //{
            //    DateTime firstDateTime = new DateTime(dtThang.DateTime.Year, Convert.ToInt32(dtThang.DateTime.Month), 1);
            //    dTuNgay.EditValue = firstDateTime;
            //    int t = DateTime.DaysInMonth(firstDateTime.Year, firstDateTime.Month);
            //    DateTime secondDateTime = new DateTime(dtThang.DateTime.Year, Convert.ToInt32(dtThang.DateTime.Month), t);
            //    dDenNgay.EditValue = secondDateTime;
            //}
            //catch
            //{

            //}
        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch
            { }
        }
       
    }
}
