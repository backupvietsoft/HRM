﻿using DevExpress.CodeParser;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Linq;
using Vs.Payroll;
using Vs.Report;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Collections.Generic;
using System.Reflection;
using System.Globalization;
using System.Threading.Tasks;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace Vs.TimeAttendance
{
    public partial class ucBaoCaoTongHopNam : DevExpress.XtraEditors.XtraUserControl
    {
        private string SaveExcelFile;
        private string formatSL;
        public ucBaoCaoTongHopNam()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, windowsUIButton);
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
        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "Print":
                    {
                        frmViewReport frm = new frmViewReport();
                        DataTable dt;
                        switch (rdo_ChonBaoCao.SelectedIndex)
                        {
                            case 0:
                                {
                                    switch (Commons.Modules.KyHieuDV)
                                    {
                                        case "AP":
                                            {
                                                BangCongNam_AP();
                                                break;
                                            }

                                        default:
                                            {
                                                BangCongNam();
                                            }
                                            break;
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    InBaoCaoTongHop_DM();
                                    break;
                                }
                            case 2:
                                {
                                    if (Commons.Modules.KyHieuDV == "AP")
                                    {
                                        BangCongVangNam_AP();
                                    }
                                    else if (Commons.Modules.KyHieuDV == "TG")
                                    {
                                        BangCongVangNam_TG();
                                    }
                                    else
                                    {
                                        BangCongVangNam();
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    DanhSachXL_TG();
                                    break;
                                }
                            case 4:
                                {
                                    BangTheoDoiPhepNam_TG();
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
        private void BorderAround(Range range)
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
        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                obj = null;
            }
            finally
            { GC.Collect(); }
        }

        private void ucBaoCaoTongHopThang_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            rdo_ChonNam.Visible = false;
            LoadCboDonVi();
            LoadCboXiNghiep();
            LoadCboTo();
            datNam.EditValue = DateTime.Now;
            //lk_DenNgay.EditValue = DateTime.Today;
            int xThang = Convert.ToDateTime(datNam.EditValue).Month;
            datTThang.EditValue = Convert.ToDateTime(datNam.EditValue).AddMonths(-xThang + 1);
            datDThang.EditValue = Convert.ToDateTime(datNam.EditValue).AddMonths(-xThang + 12);
            rdo_LocLaoDong.SelectedIndex = 0;
            rdo_LocLaoDong.Visible = false;
            Commons.Modules.sLoad = "";
            switch (Commons.Modules.KyHieuDV)
            {
                case "SB":
                    {
                        formatSL = "#,##0.00;(#,##0.00); ; ";
                        break;
                    }
                default:
                    formatSL = "#,##0.0;(#,##0.0); ; ";
                    break;
            }
            if (Commons.Modules.KyHieuDV != "TG")
            {
                rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdoDSXepLoaiABC").FirstOrDefault());
                rdo_ChonBaoCao.Properties.Items.Remove(rdo_ChonBaoCao.Properties.Items.Where(x => x.Tag.ToString() == "rdo_TheoDoiPhepNamTucTe").FirstOrDefault());
            }
            //LoadTinhTrangHopDong();
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

        private void LoadTinhTrangHopDong()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoTinhTrangHopDong", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
            //Commons.Modules.ObjSystems.MLoadLookUpEdit(LK_LOAI_HD, dt, "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
        }

        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboXiNghiep();
            LoadCboTo();
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboTo();
        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Commons.Modules.ObjSystems.DataThongTinChung(-1).Rows[0]["KY_HIEU_DV"].ToString() != "DM")
            //{
            //    return;
            //}
            switch (rdo_ChonBaoCao.SelectedIndex)
            {
                case 1:
                    {
                        rdo_ChonNam.Visible = true;
                        break;
                    }
                case 3:
                    {
                        rdo_LocLaoDong.Visible = true;
                        break;
                    }
                default:
                    rdo_LocLaoDong.Visible = false;
                    rdo_ChonNam.Visible = false;
                    break;
            }
        }
        private void grvThang_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {


        }

        private void windowsUIButton_Click(object sender, EventArgs e)
        {

        }

        private void rdo_DiTreVeSom_SelectedIndexChanged(object sender, EventArgs e)
        {
            //switch (rdo_ChonBaoCao.SelectedIndex)
            //{
            //    case 0:
            //        {
            //            rdo_DiTreVeSom.Visible = true;

            //        }
            //        break;
            //    case 1:
            //        {
            //            rdo_DiTreVeSom.Visible = true;
            //        }
            //        break;
            //    case 2:
            //        {
            //            rdo_DiTreVeSom.Visible = true;
            //        }
            //        break;
            //    default:
            //        break;
            //}
        }

        private void calThang_DateTimeCommit_1(object sender, EventArgs e)
        {
            //try
            //{
            //    LK_Thang.Text = calThang.DateTime.ToString("MM/yyyy");
            //    DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdThang);
            //    DataRow[] dr;
            //    dr = dtTmp.Select("NGAY_TTXL" + "='" + LK_Thang.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
            //    if (dr.Count() == 1)
            //    {
            //    }
            //    else { }
            //}
            //catch (Exception ex)
            //{
            //    LK_Thang.Text = calThang.DateTime.ToString("MM/yyyy");
            //}
            //LK_Thang.ClosePopup();
        }

        private void LK_Thang_EditValueChanged(object sender, EventArgs e)
        {
            //DateTime tungay = Convert.ToDateTime(LK_Thang.EditValue);
            //DateTime denngay = Convert.ToDateTime(LK_Thang.EditValue).AddMonths(+1);
            //lk_TuNgay.EditValue = Convert.ToDateTime("01/" + tungay.Month + "/" + tungay.Year);
            //lk_DenNgay.EditValue = Convert.ToDateTime("01/" + denngay.Month + "/" + tungay.Year).AddDays(-1);
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

        private void datNam_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            int xThang = Convert.ToDateTime(datNam.EditValue).Month;
            datTThang.EditValue = Convert.ToDateTime(datNam.EditValue).AddMonths(-xThang + 1);
            datDThang.EditValue = Convert.ToDateTime(datNam.EditValue).AddMonths(-xThang + 12);
        }

        private void BangTHChamCongNam_MT()
        {
            try
            {
                frmViewReport frm = new frmViewReport();
                DataTable dt = new DataTable();
                System.Data.SqlClient.SqlConnection conn;
                dt = new DataTable();
                frm.rpt = new rptBangTongHopCongNam(Convert.ToDateTime(datTThang.EditValue), Convert.ToDateTime(datDThang.EditValue));
                try
                {
                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangCongNam_MT"), conn);

                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                    cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToDateTime(datNam.EditValue).Year;
                    cmd.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    dt = new DataTable();
                    dt = ds.Tables[0].Copy();
                    if (dt.Rows.Count == 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    dt.TableName = "DATA";
                    frm.AddDataSource(dt);
                    frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
                }
                catch (Exception ex)
                { }
                frm.ShowDialog();
            }
            catch { }
        }
        private void BangTHChamCongNam_SB()
        {
            try
            {
                frmViewReport frm = new frmViewReport();
                DataTable dt = new DataTable();
                System.Data.SqlClient.SqlConnection conn;
                dt = new DataTable();
                frm.rpt = new rptBangTongHopCongNam(Convert.ToDateTime(datTThang.EditValue), Convert.ToDateTime(datDThang.EditValue));
                try
                {
                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangCongNam_SB"), conn);

                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                    cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToDateTime(datNam.EditValue).Year;
                    cmd.Parameters.Add("@LamTron", SqlDbType.Int).Value = Commons.Modules.iLamTronGio;
                    cmd.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    dt = new DataTable();
                    dt = ds.Tables[0].Copy();
                    if (dt.Rows.Count == 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    dt.TableName = "DATA";
                    frm.AddDataSource(dt);
                    frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
                }
                catch
                { }
                frm.ShowDialog();
            }
            catch { }
        }
        private void BangTHCongTangCaNam_MT()
        {
            try
            {
                frmViewReport frm = new frmViewReport();
                DataTable dt = new DataTable();
                System.Data.SqlClient.SqlConnection conn;
                dt = new DataTable();
                frm.rpt = new rptBangTongHopTangCaNam(Convert.ToDateTime(datTThang.EditValue), Convert.ToDateTime(datDThang.EditValue));
                try
                {
                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangCongTangCaNam_MT"), conn);

                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                    cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToDateTime(datNam.EditValue).Year;
                    cmd.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    dt = new DataTable();
                    dt = ds.Tables[0].Copy();
                    if (dt.Rows.Count == 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    dt.TableName = "DATA";
                    frm.AddDataSource(dt);
                    frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
                }
                catch
                { }
                frm.ShowDialog();
            }
            catch { }
        }
        private void BangTHCongTangCaNam_SB()
        {
            try
            {
                frmViewReport frm = new frmViewReport();
                DataTable dt = new DataTable();
                System.Data.SqlClient.SqlConnection conn;
                dt = new DataTable();
                frm.rpt = new rptBangTongHopTangCaNam(Convert.ToDateTime(datTThang.EditValue), Convert.ToDateTime(datDThang.EditValue));
                try
                {
                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangCongTangCaNam_SB"), conn);

                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                    cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToDateTime(datNam.EditValue).Year;
                    cmd.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    dt = new DataTable();
                    dt = ds.Tables[0].Copy();
                    if (dt.Rows.Count == 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    dt.TableName = "DATA";
                    frm.AddDataSource(dt);
                    frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
                }
                catch
                { }
                frm.ShowDialog();
            }
            catch { }
        }
        private void BangTHCongVangNam_MT()
        {
            try
            {
                frmViewReport frm = new frmViewReport();
                DataTable dt = new DataTable();
                System.Data.SqlClient.SqlConnection conn;
                dt = new DataTable();
                string sTieuDe = Commons.Modules.ObjLanguages.GetLanguage("rptBangTongHopCongVangNam", "lblTIEU_DE") + " " + Convert.ToDateTime(datTThang.EditValue).ToString("MM/yyyy") + " " + Commons.Modules.ObjLanguages.GetLanguage("rptBangTongHopCongVangNam", "lblDenNGay") + " " + Convert.ToDateTime(datDThang.EditValue).ToString("MM/yyyy");
                //"TỔNG NGÀY NGHỈ TRONG NĂM TỪ " : "TOTAL NUMBER OF HOLIDAYS IN THE YEAR ") +Convert.ToDateTime(datTThang.EditValue).ToString("MM/yyyy") + (Commons.Modules.TypeLanguage == 0 ? " ĐẾN " : " TO ") + Convert.ToDateTime(datDThang.EditValue).ToString("MM/yyyy") + "";
                frm.rpt = new rptBangTongHopCongVangNam(sTieuDe);
                try
                {
                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangCongVangNam_MT"), conn);

                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                    cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToDateTime(datNam.EditValue).Year;
                    cmd.Parameters.Add("@LamTronCong", SqlDbType.Bit).Value = Commons.Modules.iLamTronGio;
                    cmd.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    dt = new DataTable();
                    dt = ds.Tables[0].Copy();
                    if (dt.Rows.Count == 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    dt.TableName = "DATA";
                    frm.AddDataSource(dt);
                    frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
                }
                catch
                { }
                frm.ShowDialog();
            }
            catch { }
        }
        private void BangTHCongVangNam_SB()
        {
            try
            {
                frmViewReport frm = new frmViewReport();
                DataTable dt = new DataTable();
                System.Data.SqlClient.SqlConnection conn;
                dt = new DataTable();
                string sTieuDe = Commons.Modules.ObjLanguages.GetLanguage("rptBangTongHopCongVangNam", "lblTIEU_DE") + " " + Convert.ToDateTime(datTThang.EditValue).ToString("MM/yyyy") + " " + Commons.Modules.ObjLanguages.GetLanguage("rptBangTongHopCongVangNam", "lblDenNGay") + " " + Convert.ToDateTime(datDThang.EditValue).ToString("MM/yyyy");
                //"TỔNG NGÀY NGHỈ TRONG NĂM TỪ " : "TOTAL NUMBER OF HOLIDAYS IN THE YEAR ") +Convert.ToDateTime(datTThang.EditValue).ToString("MM/yyyy") + (Commons.Modules.TypeLanguage == 0 ? " ĐẾN " : " TO ") + Convert.ToDateTime(datDThang.EditValue).ToString("MM/yyyy") + "";
                frm.rpt = new rptBangTongHopCongVangNam(sTieuDe);
                try
                {
                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangCongVangNam_SB"), conn);

                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                    cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToDateTime(datNam.EditValue).Year;
                    cmd.Parameters.Add("@LamTronCong", SqlDbType.Bit).Value = Commons.Modules.iLamTronGio;
                    cmd.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    dt = new DataTable();
                    dt = ds.Tables[0].Copy();
                    if (dt.Rows.Count == 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    dt.TableName = "DATA";
                    frm.AddDataSource(dt);
                    frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung(-1));
                }
                catch
                { }
                frm.ShowDialog();
            }
            catch { }
        }
        private void BangCongNam()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCCongNam;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongNam", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToDateTime(datNam.EditValue).Year;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCCongNam = new DataTable();
                dtBCCongNam = ds.Tables[0].Copy();

                if (dtBCCongNam.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                int lastColumn = 0;
                lastColumn = dtBCCongNam.Columns.Count;

                int col = 1;

                TaoTTChung_TheoDV(oSheet, 1, 2, 1, 7, 0, 0);

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, lastColumn]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG NĂM " + Convert.ToDateTime(datNam.EditValue).Year + "";


                Range row4_TieuDe_Format = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, lastColumn]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);

                Range row5_TieuDe_STT = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_STT.Value2 = "STT";
                row5_TieuDe_STT.ColumnWidth = 10;
                col++;

                Range row5_TieuDe_MSCN = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_MSCN.Value2 = "MSCN";
                row5_TieuDe_MSCN.ColumnWidth = 12;
                col++;

                Range row5_TieuDe_HOTEN = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_HOTEN.Value2 = "Họ và tên";
                row5_TieuDe_HOTEN.ColumnWidth = 25;
                col++;

                Range row5_TieuDe_XN = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_XN.Value2 = "Xưởng/Phòng ban";
                row5_TieuDe_XN.ColumnWidth = 25;
                col++;

                Range row5_TieuDe_TO = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_TO.Value2 = "Chuyền/Phòng";
                row5_TieuDe_TO.ColumnWidth = 25;
                col++;

                Range row5_TieuDe_NVL = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_NVL.Value2 = "Ngày vào làm";
                row5_TieuDe_NVL.ColumnWidth = 10;
                col++;

                //lay tieu de cot thang 
                for (int iThang = 1; iThang <= 12; iThang++)
                {
                    oSheet.Cells[6, col] = "TH " + iThang.ToString();
                    col++;
                }

                Range row5_TieuDe_TongCong = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_TongCong.Value2 = "Tổng cộng";
                row5_TieuDe_TongCong.ColumnWidth = 10;

                DataRow[] dr = dtBCCongNam.Select();
                string[,] rowData = new string[dr.Count(), dtBCCongNam.Columns.Count];

                int rowCnt = 0;
                int i = 0;
                foreach (DataRow row in dr)
                {
                    for (i = 0; i < dtBCCongNam.Columns.Count; i++)
                    {
                        rowData[rowCnt, i] = row[i].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 6;
                oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, lastColumn]].Value2 = rowData;
                Microsoft.Office.Interop.Excel.Range formatRange;

                formatRange = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, 1]];
                formatRange.NumberFormat = "#,##0";
                formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[7, 6], oSheet.Cells[rowCnt, 6]];
                formatRange.NumberFormat = "dd/MM/yyyy";
                formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                for (i = 7; i <= lastColumn; i++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[7, i], oSheet.Cells[rowCnt, i]];
                    formatRange.NumberFormat = formatSL;
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                formatRange = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                //formatRange = oSheet.Range[oSheet.Cells[7, 6], oSheet.Cells[rowCnt, lastColumn]];
                //formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                //formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                BorderAround(oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]]);
                // filter
                this.Cursor = Cursors.Default;
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }
        private void BangCongNam_AP()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCCongNam;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongNam_AP", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToDateTime(datNam.EditValue).Year;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCCongNam = new DataTable();
                dtBCCongNam = ds.Tables[0].Copy();

                if (dtBCCongNam.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                int lastColumn = 0;
                lastColumn = dtBCCongNam.Columns.Count;

                int col = 1;

                TaoTTChung_TheoDV(oSheet, 1, 2, 1, 7, 0, 0);

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, lastColumn]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG NĂM " + Convert.ToDateTime(datNam.EditValue).Year + "";


                Range row4_TieuDe_Format = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, lastColumn]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);

                Range row5_TieuDe_STT = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_STT.Value2 = "STT";
                row5_TieuDe_STT.ColumnWidth = 10;
                col++;

                Range row5_TieuDe_MSCN = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_MSCN.Value2 = "MSCN";
                row5_TieuDe_MSCN.ColumnWidth = 12;
                col++;

                Range row5_TieuDe_HOTEN = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_HOTEN.Value2 = "Họ và tên";
                row5_TieuDe_HOTEN.ColumnWidth = 25;
                col++;

                Range row5_TieuDe_NVL = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_NVL.Value2 = "Ngày vào làm";
                row5_TieuDe_NVL.ColumnWidth = 10;
                col++;

                //lay tieu de cot thang 
                for (int iThang = 1; iThang <= 12; iThang++)
                {
                    oSheet.Cells[6, col] = "TH " + iThang.ToString();
                    col++;
                }

                Range row5_TieuDe_TongCong = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_TongCong.Value2 = "Tổng cộng";
                row5_TieuDe_TongCong.ColumnWidth = 10;

                col++;
                Range row5_TieuDe_BoPhan = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_BoPhan.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTo");
                row5_TieuDe_BoPhan.ColumnWidth = 25;


                DataRow[] dr = dtBCCongNam.Select();
                string[,] rowData = new string[dr.Count(), dtBCCongNam.Columns.Count];

                int rowCnt = 0;
                int i = 0;
                foreach (DataRow row in dr)
                {
                    for (i = 0; i < dtBCCongNam.Columns.Count; i++)
                    {
                        rowData[rowCnt, i] = row[i].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 6;
                oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, lastColumn]].Value2 = rowData;
                Microsoft.Office.Interop.Excel.Range formatRange;

                formatRange = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, 1]];
                formatRange.NumberFormat = "#,##0";
                formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[7, 4], oSheet.Cells[rowCnt, 4]];
                formatRange.NumberFormat = "dd/MM/yyyy";
                formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                for (i = 5; i <= lastColumn - 1; i++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[7, i], oSheet.Cells[rowCnt, i]];
                    formatRange.NumberFormat = formatSL;
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                formatRange = oSheet.Range[oSheet.Cells[1, 1], oSheet.Cells[3, lastColumn]];
                formatRange.Font.Name = fontName;

                formatRange = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                //formatRange = oSheet.Range[oSheet.Cells[7, 6], oSheet.Cells[rowCnt, lastColumn]];
                //formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                //formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                BorderAround(oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]]);
                // filter
                this.Cursor = Cursors.Default;
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }
        private void BangCongVangNam()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCVNam;
                DataTable dtLDV;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongVangNam", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToDateTime(datNam.EditValue).Year;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCVNam = new DataTable();
                dtBCVNam = ds.Tables[0].Copy();
                dtLDV = new DataTable();
                dtLDV = ds.Tables[1].Copy();


                if (dtBCVNam.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                int lastColumn = 0;
                lastColumn = dtBCVNam.Columns.Count;

                int col = 1;

                TaoTTChung_TheoDV(oSheet, 1, 2, 1, 7, 0, 0);

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, lastColumn]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG VẮNG NĂM " + Convert.ToDateTime(datNam.EditValue).Year + "";


                Range row4_TieuDe_Format = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, lastColumn]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);

                Range row5_TieuDe_STT = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_STT.Value2 = "STT";
                row5_TieuDe_STT.ColumnWidth = 10;
                col++;

                Range row5_TieuDe_MSCN = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_MSCN.Value2 = "MSCN";
                row5_TieuDe_MSCN.ColumnWidth = 12;
                col++;

                Range row5_TieuDe_HOTEN = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_HOTEN.Value2 = "Họ và tên";
                row5_TieuDe_HOTEN.ColumnWidth = 25;
                col++;

                Range row5_TieuDe_XN = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_XN.Value2 = "Xưởng/Phòng ban";
                row5_TieuDe_XN.ColumnWidth = 25;
                col++;

                Range row5_TieuDe_TO = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_TO.Value2 = "Chuyền/Phòng";
                row5_TieuDe_TO.ColumnWidth = 25;
                col++;

                Range row5_TieuDe_NVL = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_NVL.Value2 = "Ngày vào làm";
                row5_TieuDe_NVL.ColumnWidth = 10;
                col++;

                //lay tieu de cot nghi 
                DataRow[] drN = dtLDV.Select();
                //string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                foreach (DataRow rowN in drN)
                {
                    oSheet.Cells[6, col] = rowN[1].ToString();
                    col++;
                }

                Range row5_TieuDe_TongCong = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_TongCong.Value2 = "Tổng cộng";
                row5_TieuDe_TongCong.ColumnWidth = 10;

                DataRow[] dr = dtBCVNam.Select();
                string[,] rowData = new string[dr.Count(), dtBCVNam.Columns.Count];

                int rowCnt = 0;
                int i = 0;
                foreach (DataRow row in dr)
                {
                    for (i = 0; i < dtBCVNam.Columns.Count; i++)
                    {
                        rowData[rowCnt, i] = row[i].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 6;
                oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, lastColumn]].Value2 = rowData;
                Microsoft.Office.Interop.Excel.Range formatRange;

                formatRange = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, 1]];
                formatRange.NumberFormat = "#,##0";
                formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[7, 6], oSheet.Cells[rowCnt, 6]];
                formatRange.NumberFormat = "dd/MM/yyyy";
                formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                for (i = 7; i <= lastColumn; i++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[7, i], oSheet.Cells[rowCnt, i]];
                    formatRange.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                formatRange = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                //formatRange = oSheet.Range[oSheet.Cells[7, 6], oSheet.Cells[rowCnt, lastColumn]];
                //formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                //formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                BorderAround(oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]]);
                // filter
                this.Cursor = Cursors.Default;
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }
        private void BangCongVangNam_AP()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCVNam;
                DataTable dtLDV;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongVangNam_AP", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToDateTime(datNam.EditValue).Year;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCVNam = new DataTable();
                dtBCVNam = ds.Tables[0].Copy();
                dtLDV = new DataTable();
                dtLDV = ds.Tables[1].Copy();


                if (dtBCVNam.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                int lastColumn = 0;
                lastColumn = dtBCVNam.Columns.Count;

                int col = 1;

                TaoTTChung_TheoDV(oSheet, 1, 2, 1, 7, 0, 0);

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, lastColumn]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG TỔNG HỢP CÔNG VẮNG NĂM " + Convert.ToDateTime(datNam.EditValue).Year + "";


                Range row4_TieuDe_Format = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, lastColumn]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);

                Range row5_TieuDe_STT = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_STT.Value2 = "STT";
                row5_TieuDe_STT.ColumnWidth = 10;
                col++;

                Range row5_TieuDe_MSCN = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_MSCN.Value2 = "MSCN";
                row5_TieuDe_MSCN.ColumnWidth = 12;
                col++;

                Range row5_TieuDe_HOTEN = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_HOTEN.Value2 = "Họ và tên";
                row5_TieuDe_HOTEN.ColumnWidth = 25;
                col++;

                Range row5_TieuDe_NVL = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_NVL.Value2 = "Ngày vào làm";
                row5_TieuDe_NVL.ColumnWidth = 10;
                col++;

                //lay tieu de cot nghi 
                DataRow[] drN = dtLDV.Select();
                //string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                foreach (DataRow rowN in drN)
                {
                    oSheet.Cells[6, col] = rowN[1].ToString();
                    col++;
                }

                Range row5_TieuDe_TongCong = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_TongCong.Value2 = "Tổng cộng";
                row5_TieuDe_TongCong.ColumnWidth = 10;

                col++;

                Range row5_TieuDe_To = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[6, col]];
                row5_TieuDe_To.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTo");
                row5_TieuDe_To.ColumnWidth = 25;

                DataRow[] dr = dtBCVNam.Select();
                string[,] rowData = new string[dr.Count(), dtBCVNam.Columns.Count];

                int rowCnt = 0;
                int i = 0;
                foreach (DataRow row in dr)
                {
                    for (i = 0; i < dtBCVNam.Columns.Count; i++)
                    {
                        rowData[rowCnt, i] = row[i].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 6;
                oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, lastColumn]].Value2 = rowData;
                Microsoft.Office.Interop.Excel.Range formatRange;

                formatRange = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, 1]];
                formatRange.NumberFormat = "#,##0";
                formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[7, 4], oSheet.Cells[rowCnt, 4]];
                formatRange.NumberFormat = "dd/MM/yyyy";
                formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                for (i = 5; i <= lastColumn - 1; i++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[7, i], oSheet.Cells[rowCnt, i]];
                    formatRange.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }


                formatRange = oSheet.Range[oSheet.Cells[1, 1], oSheet.Cells[3, lastColumn]];
                formatRange.Font.Name = fontName;

                formatRange = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                //formatRange = oSheet.Range[oSheet.Cells[7, 6], oSheet.Cells[rowCnt, lastColumn]];
                //formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                //formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                BorderAround(oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]]);
                // filter
                this.Cursor = Cursors.Default;
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }
        private void InBaoCaoTongHop_DM()
        {
            switch (rdo_ChonNam.SelectedIndex)
            {
                case 0:
                    {
                        if (Commons.Modules.KyHieuDV == "AP")
                        {
                            BangCongTangCaTuan_AP();
                        }
                        else
                        {
                            BangCongTangCaTuan_DM();
                        }
                        break;
                    }
                case 1:
                    {
                        if (Commons.Modules.KyHieuDV == "AP")
                        {
                            BangCongTangCaQuy_AP();
                        }
                        else
                        {
                            BangCongTangCaQuy_DM();
                        }
                        break;
                    }
                case 2:
                    {
                        if (Commons.Modules.KyHieuDV == "AP")
                        {
                            BangCongTangCaNam_AP();
                        }
                        else
                        {
                            BangCongTangCaNam_DM();
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        private void BangCongTangCaNam_DM()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongTangCaNam_DM", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToDateTime(datNam.EditValue).Year;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                if (dtBCThang.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                int lastColumn = 0;
                lastColumn = dtBCThang.Columns.Count;

                TaoTTChung_TheoDV(oSheet, 1, 2, 1, 7, 0, 0);

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, 23]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THÁNG NGOÀI GIỜ NĂM " + Convert.ToDateTime(datNam.EditValue).Year + "";


                Range row4_TieuDe_Format = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[8, 23]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);

                Range row4_TieuDe_TTNV = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, 7]];
                row4_TieuDe_TTNV.Merge();
                row4_TieuDe_TTNV.Value2 = "Thông tin nhân viên (Staff information)";

                Range row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[6, 8], oSheet.Cells[6, 19]];
                row4_TieuDe_TTC.Merge();
                row4_TieuDe_TTC.Value2 = "Tháng tăng ca (Overtime day)";

                Range row4_TieuDe_TTTCT = oSheet.Range[oSheet.Cells[6, 20], oSheet.Cells[6, 23]];
                row4_TieuDe_TTTCT.Merge();
                row4_TieuDe_TTTCT.RowHeight = 30;
                row4_TieuDe_TTTCT.Value2 = "Thông tin tăng ca tháng (Information about monthly overtime)";

                Range row5_TieuDe_STT = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[8, 1]];
                row5_TieuDe_STT.Merge();
                row5_TieuDe_STT.Value2 = "STT";
                row5_TieuDe_STT.ColumnWidth = 10;

                Range row6_TieuDe_STT = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[8, 1]];
                row6_TieuDe_STT.RowHeight = 54;

                Range row5_TieuDe_MSCN = oSheet.Range[oSheet.Cells[7, 2], oSheet.Cells[8, 2]];
                row5_TieuDe_MSCN.Merge();
                row5_TieuDe_MSCN.Value2 = "MSCN";
                row5_TieuDe_MSCN.ColumnWidth = 10;

                Range row5_TieuDe_HOTEN = oSheet.Range[oSheet.Cells[7, 3], oSheet.Cells[8, 3]];
                row5_TieuDe_HOTEN.Merge();
                row5_TieuDe_HOTEN.Value2 = "Họ và tên";
                row5_TieuDe_HOTEN.ColumnWidth = 25;

                Range row5_TieuDe_XN = oSheet.Range[oSheet.Cells[7, 4], oSheet.Cells[8, 4]];
                row5_TieuDe_XN.Merge();
                row5_TieuDe_XN.Value2 = "Xưởng/Phòng ban";
                row5_TieuDe_XN.ColumnWidth = Commons.Modules.KyHieuDV == "SB" ? 20 : 35;

                Range row5_TieuDe_TO = oSheet.Range[oSheet.Cells[7, 5], oSheet.Cells[8, 5]];
                row5_TieuDe_TO.Merge();
                row5_TieuDe_TO.Value2 = "Chuyền/Phòng";
                row5_TieuDe_TO.ColumnWidth = Commons.Modules.KyHieuDV == "SB" ? 20 : 35;

                Range row5_TieuDe_NTV = oSheet.Range[oSheet.Cells[7, 6], oSheet.Cells[8, 6]];
                row5_TieuDe_NTV.Merge();
                row5_TieuDe_NTV.Value2 = "Ngày thử việc";
                row5_TieuDe_NTV.ColumnWidth = 10;

                Range row5_TieuDe_NVL = oSheet.Range[oSheet.Cells[7, 7], oSheet.Cells[8, 7]];
                row5_TieuDe_NVL.Merge();
                row5_TieuDe_NVL.Value2 = "Ngày vào làm";
                row5_TieuDe_NVL.ColumnWidth = 10;

                int thang_bd = 1;
                int thang_kt = 12;
                int col_bd = 8;
                while (thang_bd <= thang_kt)
                {
                    Range rowtemp = oSheet.Range[oSheet.Cells[8, col_bd], oSheet.Cells[8, col_bd]];
                    rowtemp.Value2 = null;
                    Range row6_b = oSheet.Range[oSheet.Cells[7, col_bd], oSheet.Cells[7, col_bd]];
                    row6_b.Value2 = "TH" + thang_bd;
                    row6_b.ColumnWidth = 6;
                    row6_b = oSheet.Range[oSheet.Cells[7, col_bd], oSheet.Cells[8, col_bd]];
                    row6_b.Merge();
                    col_bd += 1;
                    thang_bd++;
                }

                Range row5_TieuDe_TSGTC = oSheet.Range[oSheet.Cells[7, 20], oSheet.Cells[8, 20]];
                row5_TieuDe_TSGTC.Merge();
                row5_TieuDe_TSGTC.Value2 = "Tăng ca 150%";

                Range row5_TieuDe_TSGTCCN = oSheet.Range[oSheet.Cells[7, 21], oSheet.Cells[8, 21]];
                row5_TieuDe_TSGTCCN.Merge();
                row5_TieuDe_TSGTCCN.Value2 = "Tăng ca 200%";

                Range row5_TieuDe_TSGTCCD = oSheet.Range[oSheet.Cells[7, 22], oSheet.Cells[8, 22]];
                row5_TieuDe_TSGTCCD.Merge();
                row5_TieuDe_TSGTCCD.Value2 = "Tăng ca 300%";

                Range row5_TieuDe_TSGTCNT = oSheet.Range[oSheet.Cells[7, 23], oSheet.Cells[8, 23]];
                row5_TieuDe_TSGTCNT.Merge();
                row5_TieuDe_TSGTCNT.Value2 = "Tổng số giờ tăng ca";


                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                int rowBD = 9;
                string[] TEN_TO = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_TO")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[0].Copy(); // Dữ row count data
                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt


                for (int j = 0; j < TEN_TO.Count(); j++)
                {
                    dtBCThang = ds.Tables[0].Copy();
                    dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_TO") == TEN_TO[j]).CopyToDataTable().Copy();
                    DataRow[] dr = dtBCThang.Select();
                    current_dr = dr.Count();
                    string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                    foreach (DataRow row in dr)
                    {
                        for (col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                        {
                            rowData[rowCnt, col_bd] = row[col_bd].ToString();
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
                    rowCnt = rowBD + current_dr - 1;

                    // Tạo group tổ
                    Range row_groupXI_NGHIEP_Format = oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[rowBD, lastColumn]];
                    row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(146, 208, 80);
                    oSheet.Cells[rowBD, 1] = TEN_TO[j].ToString();
                    oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Bold = true;

                    //Đổ dữ liệu của xí nghiệp
                    oSheet.Range[oSheet.Cells[(rowBD + 1), 1], oSheet.Cells[(rowCnt + 1).ToString(), lastColumn]].Value2 = rowData;

                    dr_Cu = current_dr;
                    keepRowCnt = rowCnt;
                    rowCnt = 0;
                }

                rowCnt = keepRowCnt;
                rowCnt++;

                Microsoft.Office.Interop.Excel.Range formatRange;

                formatRange = oSheet.Range[oSheet.Cells[9, 6], oSheet.Cells[rowCnt, 6]];
                formatRange.NumberFormat = "dd/MM/yyyy";
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                formatRange = oSheet.Range[oSheet.Cells[9, 7], oSheet.Cells[rowCnt, 7]];
                formatRange.NumberFormat = "dd/MM/yyyy";
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                for (int i = 8; i <= 23; i++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[9, i], oSheet.Cells[rowCnt, i]];
                    formatRange.NumberFormat = "#,##0.00;(#,##0.0); ; ";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                formatRange = oSheet.Range[oSheet.Cells[9, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                formatRange = oSheet.Range[oSheet.Cells[9, 6], oSheet.Cells[rowCnt, 27]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                BorderAround(oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]]);
                // filter
                this.Cursor = Cursors.Default;
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }
        private void BangCongTangCaQuy_DM()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongTangCaQuy_DM", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToDateTime(datNam.EditValue).Year;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                if (dtBCThang.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;


                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                int lastColumn = 0;
                lastColumn = dtBCThang.Columns.Count;

                TaoTTChung_TheoDV(oSheet, 1, 2, 1, 7, 0, 0);

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, 19]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THÁNG NGOÀI GIỜ NĂM " + Convert.ToDateTime(datNam.EditValue).Year + " THEO QUÝ";


                Range row4_TieuDe_Format = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[8, 15]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);

                Range row4_TieuDe_TTNV = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, 7]];
                row4_TieuDe_TTNV.Merge();
                row4_TieuDe_TTNV.RowHeight = 30;
                row4_TieuDe_TTNV.Value2 = "Thông tin nhân viên (Staff information)";

                Range row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[6, 8], oSheet.Cells[6, 11]];
                row4_TieuDe_TTC.Merge();
                row4_TieuDe_TTC.Value2 = "Qúy tăng ca (Overtime day)";

                Range row4_TieuDe_TTTCT = oSheet.Range[oSheet.Cells[6, 12], oSheet.Cells[6, 15]];
                row4_TieuDe_TTTCT.Merge();
                row4_TieuDe_TTTCT.Value2 = "Thông tin tăng ca tháng (Information about monthly overtime)";

                Range row5_TieuDe_STT = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[8, 1]];
                row5_TieuDe_STT.Merge();
                row5_TieuDe_STT.Value2 = "STT";
                row5_TieuDe_STT.ColumnWidth = 10;

                Range row6_TieuDe_STT = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[8, 1]];
                row6_TieuDe_STT.RowHeight = 54;

                Range row5_TieuDe_MSCN = oSheet.Range[oSheet.Cells[7, 2], oSheet.Cells[8, 2]];
                row5_TieuDe_MSCN.Merge();
                row5_TieuDe_MSCN.Value2 = "MSCN";
                row5_TieuDe_MSCN.ColumnWidth = 10;

                Range row5_TieuDe_HOTEN = oSheet.Range[oSheet.Cells[7, 3], oSheet.Cells[8, 3]];
                row5_TieuDe_HOTEN.Merge();
                row5_TieuDe_HOTEN.Value2 = "Họ và tên";
                row5_TieuDe_HOTEN.ColumnWidth = 25;

                Range row5_TieuDe_XN = oSheet.Range[oSheet.Cells[7, 4], oSheet.Cells[8, 4]];
                row5_TieuDe_XN.Merge();
                row5_TieuDe_XN.Value2 = "Xưởng/Phòng ban";
                row5_TieuDe_XN.ColumnWidth = Commons.Modules.KyHieuDV == "SB" ? 20 : 35;


                Range row5_TieuDe_TO = oSheet.Range[oSheet.Cells[7, 5], oSheet.Cells[8, 5]];
                row5_TieuDe_TO.Merge();
                row5_TieuDe_TO.Value2 = "Chuyền/Phòng";
                row5_TieuDe_TO.ColumnWidth = Commons.Modules.KyHieuDV == "SB" ? 20 : 35;

                Range row5_TieuDe_NTV = oSheet.Range[oSheet.Cells[7, 6], oSheet.Cells[8, 6]];
                row5_TieuDe_NTV.Merge();
                row5_TieuDe_NTV.Value2 = "Ngày thử việc";
                row5_TieuDe_NTV.ColumnWidth = 10;

                Range row5_TieuDe_NVL = oSheet.Range[oSheet.Cells[7, 7], oSheet.Cells[8, 7]];
                row5_TieuDe_NVL.Merge();
                row5_TieuDe_NVL.Value2 = "Ngày vào làm";
                row5_TieuDe_NVL.ColumnWidth = 10;

                int thang_bd = 1;
                int thang_kt = 4;
                int col_bd = 8;
                while (thang_bd <= thang_kt)
                {
                    Range rowtemp = oSheet.Range[oSheet.Cells[8, col_bd], oSheet.Cells[8, col_bd]];
                    rowtemp.Value2 = null;
                    Range row6_b = oSheet.Range[oSheet.Cells[7, col_bd], oSheet.Cells[7, col_bd]];
                    //Range row6_b = oSheet.get_Range(CharacterIncrement(col_bd - 1) + "5", "" + CharacterIncrement(col_bd - 1) + "6");
                    row6_b.Value2 = "Q" + thang_bd;
                    row6_b.ColumnWidth = 6;
                    row6_b = oSheet.Range[oSheet.Cells[7, col_bd], oSheet.Cells[8, col_bd]];
                    row6_b.Merge();
                    col_bd += 1;
                    thang_bd++;
                }

                Range row5_TieuDe_TSGTC = oSheet.Range[oSheet.Cells[7, 12], oSheet.Cells[8, 12]];
                row5_TieuDe_TSGTC.Merge();
                row5_TieuDe_TSGTC.Value2 = "Tăng ca 150%";

                Range row5_TieuDe_TSGTCCN = oSheet.Range[oSheet.Cells[7, 13], oSheet.Cells[8, 13]];
                row5_TieuDe_TSGTCCN.Merge();
                row5_TieuDe_TSGTCCN.Value2 = "Tăng ca 200%";



                Range row5_TieuDe_TSGTCCD = oSheet.Range[oSheet.Cells[7, 14], oSheet.Cells[8, 14]];
                row5_TieuDe_TSGTCCD.Merge();
                row5_TieuDe_TSGTCCD.Value2 = "Tăng ca 300%";

                Range row5_TieuDe_TSGTCNT = oSheet.Range[oSheet.Cells[7, 15], oSheet.Cells[8, 15]];
                row5_TieuDe_TSGTCNT.Merge();
                row5_TieuDe_TSGTCNT.Value2 = "Tổng số giờ tăng";

                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                    {
                        rowData[rowCnt, col_bd] = row[col_bd].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 8;
                oSheet.Range[oSheet.Cells[9, 1], oSheet.Cells[rowCnt, lastColumn]].Value2 = rowData;
                Microsoft.Office.Interop.Excel.Range formatRange;

                formatRange = oSheet.Range[oSheet.Cells[9, 6], oSheet.Cells[rowCnt, 6]];
                formatRange.NumberFormat = "dd/MM/yyyy";
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                formatRange = oSheet.Range[oSheet.Cells[9, 7], oSheet.Cells[rowCnt, 7]];
                formatRange.NumberFormat = "dd/MM/yyyy";
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                for (int i = 8; i <= 19; i++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[9, i], oSheet.Cells[rowCnt, i]];
                    formatRange.NumberFormat = "#,##0.00;(#,##0.0); ; ";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                formatRange = oSheet.Range[oSheet.Cells[9, 6], oSheet.Cells[rowCnt, 19]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                formatRange = oSheet.Range[oSheet.Cells[9, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                BorderAround(oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]]);
                // filter
                this.Cursor = Cursors.Default;

                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }
        private void BangCongTangCaTuan_DM()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongTangCaTuan_DM", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToDateTime(datNam.EditValue).Year;
                cmd.CommandTimeout = 50;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                if (dtBCThang.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;


                //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                int lastColumn = 0;
                lastColumn = dtBCThang.Columns.Count;


                TaoTTChung_TheoDV(oSheet, 1, 2, 1, 7, 0, 0);

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, 64]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THÁNG NGOÀI GIỜ NĂM " + Convert.ToDateTime(datNam.EditValue).Year + " THEO TUẦN";


                Range row4_TieuDe_Format = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[8, 64]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);

                Range row4_TieuDe_TTNV = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, 7]];
                row4_TieuDe_TTNV.Merge();
                row4_TieuDe_TTNV.Value2 = "Thông tin nhân viên (Staff information)";

                Range row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[6, 8], oSheet.Cells[6, 60]];
                row4_TieuDe_TTC.Merge();
                row4_TieuDe_TTC.Value2 = "Tuần tăng ca (Overtime day)";

                Range row4_TieuDe_TTTCT = oSheet.Range[oSheet.Cells[6, 61], oSheet.Cells[6, 64]];
                row4_TieuDe_TTTCT.Merge();
                row4_TieuDe_TTTCT.RowHeight = 32;
                row4_TieuDe_TTTCT.Value2 = "Thông tin tăng ca tháng (Information about monthly overtime)";

                Range row5_TieuDe_STT = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[8, 1]];
                row5_TieuDe_STT.Merge();
                row5_TieuDe_STT.Value2 = "STT";
                row5_TieuDe_STT.ColumnWidth = 10;

                Range row6_TieuDe_STT = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[8, 1]];
                row6_TieuDe_STT.RowHeight = 54;

                Range row5_TieuDe_MSCN = oSheet.Range[oSheet.Cells[7, 2], oSheet.Cells[8, 2]];
                row5_TieuDe_MSCN.Merge();
                row5_TieuDe_MSCN.Value2 = "MSCN";
                row5_TieuDe_MSCN.ColumnWidth = 10;

                Range row5_TieuDe_HOTEN = oSheet.Range[oSheet.Cells[7, 3], oSheet.Cells[8, 3]];
                row5_TieuDe_HOTEN.Merge();
                row5_TieuDe_HOTEN.Value2 = "Họ và tên";
                row5_TieuDe_HOTEN.ColumnWidth = 25;

                Range row5_TieuDe_XN = oSheet.Range[oSheet.Cells[7, 4], oSheet.Cells[8, 4]];
                row5_TieuDe_XN.Merge();
                row5_TieuDe_XN.Value2 = "Xưởng/Phòng ban";
                row5_TieuDe_XN.ColumnWidth = Commons.Modules.KyHieuDV == "SB" ? 20 : 35;


                Range row5_TieuDe_TO = oSheet.Range[oSheet.Cells[7, 5], oSheet.Cells[8, 5]];
                row5_TieuDe_TO.Merge();
                row5_TieuDe_TO.Value2 = "Chuyền/Phòng";
                row5_TieuDe_TO.ColumnWidth = Commons.Modules.KyHieuDV == "SB" ? 20 : 35;

                Range row5_TieuDe_NTV = oSheet.Range[oSheet.Cells[7, 6], oSheet.Cells[8, 6]];
                row5_TieuDe_NTV.Merge();
                row5_TieuDe_NTV.Value2 = "Ngày thử việc";
                row5_TieuDe_NTV.ColumnWidth = 10;

                Range row5_TieuDe_NVL = oSheet.Range[oSheet.Cells[7, 7], oSheet.Cells[8, 7]];
                row5_TieuDe_NVL.Merge();
                row5_TieuDe_NVL.Value2 = "Ngày vào làm";
                row5_TieuDe_NVL.ColumnWidth = 10;

                int thang_bd = 1;
                int thang_kt = 53;
                int col_bd = 8;
                while (thang_bd <= thang_kt)
                {
                    Range rowtemp = oSheet.Range[oSheet.Cells[8, col_bd], oSheet.Cells[8, col_bd]];
                    rowtemp.Value2 = null;
                    Range row6_b = oSheet.Range[oSheet.Cells[7, col_bd], oSheet.Cells[7, col_bd]];
                    //Range row6_b = oSheet.get_Range(CharacterIncrement(col_bd - 1) + "5", "" + CharacterIncrement(col_bd - 1) + "6");
                    row6_b.Value2 = thang_bd;
                    row6_b.ColumnWidth = 4;
                    row6_b = oSheet.Range[oSheet.Cells[7, col_bd], oSheet.Cells[8, col_bd]];
                    row6_b.Merge();
                    col_bd += 1;
                    thang_bd++;
                }

                Range row5_TieuDe_TSGTC = oSheet.Range[oSheet.Cells[7, 61], oSheet.Cells[8, 61]];
                row5_TieuDe_TSGTC.Merge();
                row5_TieuDe_TSGTC.Value2 = "Tăng ca 150%";

                Range row5_TieuDe_TSGTCCN = oSheet.Range[oSheet.Cells[7, 62], oSheet.Cells[8, 62]];
                row5_TieuDe_TSGTCCN.Merge();
                row5_TieuDe_TSGTCCN.Value2 = "Tăng ca 200%";

                Range row5_TieuDe_TSGTCCD = oSheet.Range[oSheet.Cells[7, 63], oSheet.Cells[8, 63]];
                row5_TieuDe_TSGTCCD.Merge();
                row5_TieuDe_TSGTCCD.Value2 = "Tăng ca 300%";

                Range row5_TieuDe_TSGTCNT = oSheet.Range[oSheet.Cells[7, 64], oSheet.Cells[8, 64]];
                row5_TieuDe_TSGTCNT.Merge();
                row5_TieuDe_TSGTCNT.Value2 = "Tổng số giờ tăng ca";


                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                    {
                        rowData[rowCnt, col_bd] = row[col_bd].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 8;
                oSheet.Range[oSheet.Cells[9, 1], oSheet.Cells[rowCnt, lastColumn]].Value2 = rowData; ;
                Microsoft.Office.Interop.Excel.Range formatRange;

                formatRange = oSheet.Range[oSheet.Cells[9, 6], oSheet.Cells[rowCnt, 6]];
                formatRange.NumberFormat = "dd/MM/yyyy";
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                formatRange = oSheet.Range[oSheet.Cells[9, 7], oSheet.Cells[rowCnt, 7]];
                formatRange.NumberFormat = "dd/MM/yyyy";
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                for (int i = 8; i <= 64; i++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[9, i], oSheet.Cells[rowCnt, i]];
                    formatRange.NumberFormat = "#,##0.00;(#,##0.0); ; ";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                formatRange = oSheet.Range[oSheet.Cells[9, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                formatRange = oSheet.Range[oSheet.Cells[9, 6], oSheet.Cells[rowCnt, 68]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                BorderAround(oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]]);
                // filter
                this.Cursor = Cursors.Default;

                oXL.Visible = true;
                oXL.UserControl = true;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }
        private void BangCongTangCaTuan_AP()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongTangCaTuan_AP", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToDateTime(datNam.EditValue).Year;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                if (dtBCThang.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;


                //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                int lastColumn = 0;
                lastColumn = dtBCThang.Columns.Count;


                TaoTTChung_TheoDV(oSheet, 1, 2, 1, 7, 0, 0);

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, 63]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THÁNG NGOÀI GIỜ NĂM " + Convert.ToDateTime(datNam.EditValue).Year + " THEO TUẦN";


                Range row4_TieuDe_Format = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[8, 63]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);

                Range row4_TieuDe_TTNV = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, 5]];
                row4_TieuDe_TTNV.Merge();
                row4_TieuDe_TTNV.Value2 = "Thông tin nhân viên (Staff information)";

                Range row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[6, 6], oSheet.Cells[6, 58]];
                row4_TieuDe_TTC.Merge();
                row4_TieuDe_TTC.Value2 = "Tuần tăng ca (Overtime day)";

                Range row4_TieuDe_TTTCT = oSheet.Range[oSheet.Cells[6, 59], oSheet.Cells[6, 63]];
                row4_TieuDe_TTTCT.Merge();
                row4_TieuDe_TTTCT.RowHeight = 32;
                row4_TieuDe_TTTCT.Value2 = "Thông tin tăng ca tháng (Information about monthly overtime)";

                Range row5_TieuDe_STT = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[8, 1]];
                row5_TieuDe_STT.Merge();
                row5_TieuDe_STT.Value2 = "STT";
                row5_TieuDe_STT.ColumnWidth = 10;

                Range row6_TieuDe_STT = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[8, 1]];
                row6_TieuDe_STT.RowHeight = 54;

                Range row5_TieuDe_MSCN = oSheet.Range[oSheet.Cells[7, 2], oSheet.Cells[8, 2]];
                row5_TieuDe_MSCN.Merge();
                row5_TieuDe_MSCN.Value2 = "MSCN";
                row5_TieuDe_MSCN.ColumnWidth = 10;

                Range row5_TieuDe_HOTEN = oSheet.Range[oSheet.Cells[7, 3], oSheet.Cells[8, 3]];
                row5_TieuDe_HOTEN.Merge();
                row5_TieuDe_HOTEN.Value2 = "Họ và tên";
                row5_TieuDe_HOTEN.ColumnWidth = 25;

                Range row5_TieuDe_NTV = oSheet.Range[oSheet.Cells[7, 4], oSheet.Cells[8, 4]];
                row5_TieuDe_NTV.Merge();
                row5_TieuDe_NTV.Value2 = "Ngày thử việc";
                row5_TieuDe_NTV.ColumnWidth = 10;

                Range row5_TieuDe_NVL = oSheet.Range[oSheet.Cells[7, 5], oSheet.Cells[8, 5]];
                row5_TieuDe_NVL.Merge();
                row5_TieuDe_NVL.Value2 = "Ngày vào làm";
                row5_TieuDe_NVL.ColumnWidth = 10;

                int thang_bd = 1;
                int thang_kt = 53;
                int col_bd = 6;
                while (thang_bd <= thang_kt)
                {
                    Range rowtemp = oSheet.Range[oSheet.Cells[8, col_bd], oSheet.Cells[8, col_bd]];
                    rowtemp.Value2 = null;
                    Range row6_b = oSheet.Range[oSheet.Cells[7, col_bd], oSheet.Cells[7, col_bd]];
                    //Range row6_b = oSheet.get_Range(CharacterIncrement(col_bd - 1) + "5", "" + CharacterIncrement(col_bd - 1) + "6");
                    row6_b.Value2 = thang_bd;
                    row6_b.ColumnWidth = 4;
                    row6_b = oSheet.Range[oSheet.Cells[7, col_bd], oSheet.Cells[8, col_bd]];
                    row6_b.Merge();
                    col_bd += 1;
                    thang_bd++;
                }

                Range row5_TieuDe_TSGTC = oSheet.Range[oSheet.Cells[7, 59], oSheet.Cells[8, 59]];
                row5_TieuDe_TSGTC.Merge();
                row5_TieuDe_TSGTC.Value2 = "Tăng ca 150%";

                Range row5_TieuDe_TSGTCCN = oSheet.Range[oSheet.Cells[7, 60], oSheet.Cells[8, 60]];
                row5_TieuDe_TSGTCCN.Merge();
                row5_TieuDe_TSGTCCN.Value2 = "Tăng ca 200%";

                Range row5_TieuDe_TSGTCCD = oSheet.Range[oSheet.Cells[7, 61], oSheet.Cells[8, 61]];
                row5_TieuDe_TSGTCCD.Merge();
                row5_TieuDe_TSGTCCD.Value2 = "Tăng ca 300%";

                Range row5_TieuDe_TSGTCNT = oSheet.Range[oSheet.Cells[7, 62], oSheet.Cells[8, 62]];
                row5_TieuDe_TSGTCNT.Merge();
                row5_TieuDe_TSGTCNT.Value2 = "Tổng số giờ tăng ca";

                Range row5_TieuDe_To = oSheet.Range[oSheet.Cells[7, 63], oSheet.Cells[8, 63]];
                row5_TieuDe_To.ColumnWidth = 25;
                row5_TieuDe_To.Merge();
                row5_TieuDe_To.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTo");


                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                    {
                        rowData[rowCnt, col_bd] = row[col_bd].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 8;
                oSheet.Range[oSheet.Cells[9, 1], oSheet.Cells[rowCnt, lastColumn]].Value2 = rowData; ;
                Microsoft.Office.Interop.Excel.Range formatRange;

                formatRange = oSheet.Range[oSheet.Cells[9, 4], oSheet.Cells[rowCnt, 4]];
                formatRange.NumberFormat = "dd/MM/yyyy";
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                formatRange = oSheet.Range[oSheet.Cells[9, 5], oSheet.Cells[rowCnt, 5]];
                formatRange.NumberFormat = "dd/MM/yyyy";
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                for (int i = 6; i <= 62; i++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[9, i], oSheet.Cells[rowCnt, i]];
                    formatRange.NumberFormat = "#,##0.00;(#,##0.0); ; ";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                formatRange = oSheet.Range[oSheet.Cells[1, 1], oSheet.Cells[3, lastColumn]];
                formatRange.Font.Name = fontName;

                formatRange = oSheet.Range[oSheet.Cells[9, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                formatRange = oSheet.Range[oSheet.Cells[9, 6], oSheet.Cells[rowCnt, 62]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                BorderAround(oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]]);
                // filter
                this.Cursor = Cursors.Default;

                oXL.Visible = true;
                oXL.UserControl = true;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }
        private void BangCongTangCaQuy_AP()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongTangCaQuy_AP", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToDateTime(datNam.EditValue).Year;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                if (dtBCThang.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;


                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                int lastColumn = 0;
                lastColumn = dtBCThang.Columns.Count;

                TaoTTChung_TheoDV(oSheet, 1, 2, 1, 7, 0, 0);

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, 19]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THÁNG NGOÀI GIỜ NĂM " + Convert.ToDateTime(datNam.EditValue).Year + " THEO QUÝ";


                Range row4_TieuDe_Format = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[8, 14]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);

                Range row4_TieuDe_TTNV = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, 5]];
                row4_TieuDe_TTNV.Merge();
                row4_TieuDe_TTNV.RowHeight = 30;
                row4_TieuDe_TTNV.Value2 = "Thông tin nhân viên (Staff information)";

                Range row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[6, 6], oSheet.Cells[6, 9]];
                row4_TieuDe_TTC.Merge();
                row4_TieuDe_TTC.Value2 = "Qúy tăng ca (Overtime day)";

                Range row4_TieuDe_TTTCT = oSheet.Range[oSheet.Cells[6, 10], oSheet.Cells[6, 13]];
                row4_TieuDe_TTTCT.Merge();
                row4_TieuDe_TTTCT.Value2 = "Thông tin tăng ca tháng (Information about monthly overtime)";

                Range row5_TieuDe_STT = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[8, 1]];
                row5_TieuDe_STT.Merge();
                row5_TieuDe_STT.Value2 = "STT";
                row5_TieuDe_STT.ColumnWidth = 10;

                Range row6_TieuDe_STT = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[8, 1]];
                row6_TieuDe_STT.RowHeight = 54;

                Range row5_TieuDe_MSCN = oSheet.Range[oSheet.Cells[7, 2], oSheet.Cells[8, 2]];
                row5_TieuDe_MSCN.Merge();
                row5_TieuDe_MSCN.Value2 = "MSCN";
                row5_TieuDe_MSCN.ColumnWidth = 10;

                Range row5_TieuDe_HOTEN = oSheet.Range[oSheet.Cells[7, 3], oSheet.Cells[8, 3]];
                row5_TieuDe_HOTEN.Merge();
                row5_TieuDe_HOTEN.Value2 = "Họ và tên";
                row5_TieuDe_HOTEN.ColumnWidth = 25;

                Range row5_TieuDe_NTV = oSheet.Range[oSheet.Cells[7, 4], oSheet.Cells[8, 4]];
                row5_TieuDe_NTV.Merge();
                row5_TieuDe_NTV.Value2 = "Ngày thử việc";
                row5_TieuDe_NTV.ColumnWidth = 10;

                Range row5_TieuDe_NVL = oSheet.Range[oSheet.Cells[7, 5], oSheet.Cells[8, 5]];
                row5_TieuDe_NVL.Merge();
                row5_TieuDe_NVL.Value2 = "Ngày vào làm";
                row5_TieuDe_NVL.ColumnWidth = 10;

                int thang_bd = 1;
                int thang_kt = 4;
                int col_bd = 6;
                while (thang_bd <= thang_kt)
                {
                    Range rowtemp = oSheet.Range[oSheet.Cells[8, col_bd], oSheet.Cells[8, col_bd]];
                    rowtemp.Value2 = null;
                    Range row6_b = oSheet.Range[oSheet.Cells[7, col_bd], oSheet.Cells[7, col_bd]];
                    //Range row6_b = oSheet.get_Range(CharacterIncrement(col_bd - 1) + "5", "" + CharacterIncrement(col_bd - 1) + "6");
                    row6_b.Value2 = "Q" + thang_bd;
                    row6_b.ColumnWidth = 6;
                    row6_b = oSheet.Range[oSheet.Cells[7, col_bd], oSheet.Cells[8, col_bd]];
                    row6_b.Merge();
                    col_bd += 1;
                    thang_bd++;
                }

                Range row5_TieuDe_TSGTC = oSheet.Range[oSheet.Cells[7, 10], oSheet.Cells[8, 10]];
                row5_TieuDe_TSGTC.Merge();
                row5_TieuDe_TSGTC.Value2 = "Tăng ca 150%";

                Range row5_TieuDe_TSGTCCN = oSheet.Range[oSheet.Cells[7, 11], oSheet.Cells[8, 11]];
                row5_TieuDe_TSGTCCN.Merge();
                row5_TieuDe_TSGTCCN.Value2 = "Tăng ca 200%";



                Range row5_TieuDe_TSGTCCD = oSheet.Range[oSheet.Cells[7, 12], oSheet.Cells[8, 12]];
                row5_TieuDe_TSGTCCD.Merge();
                row5_TieuDe_TSGTCCD.Value2 = "Tăng ca 300%";

                Range row5_TieuDe_TSGTCNT = oSheet.Range[oSheet.Cells[7, 13], oSheet.Cells[8, 13]];
                row5_TieuDe_TSGTCNT.Merge();
                row5_TieuDe_TSGTCNT.Value2 = "Tổng số giờ tăng";

                Range row5_TieuDe_To = oSheet.Range[oSheet.Cells[7, 14], oSheet.Cells[8, 14]];
                row5_TieuDe_To.ColumnWidth = 25;
                row5_TieuDe_To.Merge();
                row5_TieuDe_To.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTo");

                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                    {
                        rowData[rowCnt, col_bd] = row[col_bd].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 8;
                oSheet.Range[oSheet.Cells[9, 1], oSheet.Cells[rowCnt, lastColumn]].Value2 = rowData;
                Microsoft.Office.Interop.Excel.Range formatRange;

                formatRange = oSheet.Range[oSheet.Cells[9, 4], oSheet.Cells[rowCnt, 4]];
                formatRange.NumberFormat = "dd/MM/yyyy";
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                formatRange = oSheet.Range[oSheet.Cells[9, 5], oSheet.Cells[rowCnt, 5]];
                formatRange.NumberFormat = "dd/MM/yyyy";
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                for (int i = 6; i <= lastColumn - 1; i++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[9, i], oSheet.Cells[rowCnt, i]];
                    formatRange.NumberFormat = "#,##0.00;(#,##0.0); ; ";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                formatRange = oSheet.Range[oSheet.Cells[9, 6], oSheet.Cells[rowCnt, lastColumn - 1]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[1, 1], oSheet.Cells[3, lastColumn]];
                formatRange.Font.Name = fontName;

                formatRange = oSheet.Range[oSheet.Cells[9, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                BorderAround(oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]]);
                // filter
                this.Cursor = Cursors.Default;

                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }
        private void BangCongTangCaNam_AP()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongTangCaNam_AP", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToDateTime(datNam.EditValue).Year;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();
                if (dtBCThang.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                int lastColumn = 0;
                lastColumn = dtBCThang.Columns.Count;

                TaoTTChung_TheoDV(oSheet, 1, 2, 1, 7, 0, 0);

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, 22]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THÁNG NGOÀI GIỜ NĂM " + Convert.ToDateTime(datNam.EditValue).Year + "";


                Range row4_TieuDe_Format = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[8, 22]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);

                Range row4_TieuDe_TTNV = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, 5]];
                row4_TieuDe_TTNV.Merge();
                row4_TieuDe_TTNV.Value2 = "Thông tin nhân viên (Staff information)";

                Range row4_TieuDe_TTC = oSheet.Range[oSheet.Cells[6, 6], oSheet.Cells[6, 17]];
                row4_TieuDe_TTC.Merge();
                row4_TieuDe_TTC.Value2 = "Tháng tăng ca (Overtime day)";

                Range row4_TieuDe_TTTCT = oSheet.Range[oSheet.Cells[6, 18], oSheet.Cells[6, 21]];
                row4_TieuDe_TTTCT.Merge();
                row4_TieuDe_TTTCT.RowHeight = 30;
                row4_TieuDe_TTTCT.Value2 = "Thông tin tăng ca tháng (Information about monthly overtime)";

                Range row5_TieuDe_STT = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[8, 1]];
                row5_TieuDe_STT.Merge();
                row5_TieuDe_STT.Value2 = "STT";
                row5_TieuDe_STT.ColumnWidth = 10;

                Range row6_TieuDe_STT = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[8, 1]];
                row6_TieuDe_STT.RowHeight = 54;

                Range row5_TieuDe_MSCN = oSheet.Range[oSheet.Cells[7, 2], oSheet.Cells[8, 2]];
                row5_TieuDe_MSCN.Merge();
                row5_TieuDe_MSCN.Value2 = "MSCN";
                row5_TieuDe_MSCN.ColumnWidth = 10;

                Range row5_TieuDe_HOTEN = oSheet.Range[oSheet.Cells[7, 3], oSheet.Cells[8, 3]];
                row5_TieuDe_HOTEN.Merge();
                row5_TieuDe_HOTEN.Value2 = "Họ và tên";
                row5_TieuDe_HOTEN.ColumnWidth = 25;

                Range row5_TieuDe_NTV = oSheet.Range[oSheet.Cells[7, 4], oSheet.Cells[8, 4]];
                row5_TieuDe_NTV.Merge();
                row5_TieuDe_NTV.Value2 = "Ngày thử việc";
                row5_TieuDe_NTV.ColumnWidth = 10;

                Range row5_TieuDe_NVL = oSheet.Range[oSheet.Cells[7, 5], oSheet.Cells[8, 5]];
                row5_TieuDe_NVL.Merge();
                row5_TieuDe_NVL.Value2 = "Ngày vào làm";
                row5_TieuDe_NVL.ColumnWidth = 10;



                int thang_bd = 1;
                int thang_kt = 12;
                int col_bd = 6;
                while (thang_bd <= thang_kt)
                {
                    Range rowtemp = oSheet.Range[oSheet.Cells[8, col_bd], oSheet.Cells[8, col_bd]];
                    rowtemp.Value2 = null;
                    Range row6_b = oSheet.Range[oSheet.Cells[7, col_bd], oSheet.Cells[7, col_bd]];
                    row6_b.Value2 = "TH" + thang_bd;
                    row6_b.ColumnWidth = 6;
                    row6_b = oSheet.Range[oSheet.Cells[7, col_bd], oSheet.Cells[8, col_bd]];
                    row6_b.Merge();
                    col_bd += 1;
                    thang_bd++;
                }

                Range row5_TieuDe_TSGTC = oSheet.Range[oSheet.Cells[7, 18], oSheet.Cells[8, 18]];
                row5_TieuDe_TSGTC.Merge();
                row5_TieuDe_TSGTC.Value2 = "Tăng ca 150%";

                Range row5_TieuDe_TSGTCCN = oSheet.Range[oSheet.Cells[7, 19], oSheet.Cells[8, 19]];
                row5_TieuDe_TSGTCCN.Merge();
                row5_TieuDe_TSGTCCN.Value2 = "Tăng ca 200%";

                Range row5_TieuDe_TSGTCCD = oSheet.Range[oSheet.Cells[7, 20], oSheet.Cells[8, 20]];
                row5_TieuDe_TSGTCCD.Merge();
                row5_TieuDe_TSGTCCD.Value2 = "Tăng ca 300%";

                Range row5_TieuDe_TSGTCNT = oSheet.Range[oSheet.Cells[7, 21], oSheet.Cells[8, 21]];
                row5_TieuDe_TSGTCNT.Merge();
                row5_TieuDe_TSGTCNT.Value2 = "Tổng số giờ tăng ca";

                Range row5_TieuDe_To = oSheet.Range[oSheet.Cells[7, 22], oSheet.Cells[8, 22]];
                row5_TieuDe_To.ColumnWidth = 25;
                row5_TieuDe_To.Merge();
                row5_TieuDe_To.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTo");

                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                int rowBD = 9;
                string[] TEN_TO = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_TO")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[0].Copy(); // Dữ row count data
                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt


                for (int j = 0; j < TEN_TO.Count(); j++)
                {
                    dtBCThang = ds.Tables[0].Copy();
                    dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_TO") == TEN_TO[j]).CopyToDataTable().Copy();
                    DataRow[] dr = dtBCThang.Select();
                    current_dr = dr.Count();
                    string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                    foreach (DataRow row in dr)
                    {
                        for (col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                        {
                            rowData[rowCnt, col_bd] = row[col_bd].ToString();
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
                    rowCnt = rowBD + current_dr - 1;

                    // Tạo group tổ
                    Range row_groupXI_NGHIEP_Format = oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[rowBD, lastColumn]];
                    row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(146, 208, 80);
                    oSheet.Cells[rowBD, 1] = TEN_TO[j].ToString();
                    oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Bold = true;

                    //Đổ dữ liệu của xí nghiệp
                    oSheet.Range[oSheet.Cells[(rowBD + 1), 1], oSheet.Cells[(rowCnt + 1).ToString(), lastColumn]].Value2 = rowData;

                    dr_Cu = current_dr;
                    keepRowCnt = rowCnt;
                    rowCnt = 0;
                }

                rowCnt = keepRowCnt;
                rowCnt++;

                Microsoft.Office.Interop.Excel.Range formatRange;

                formatRange = oSheet.Range[oSheet.Cells[9, 4], oSheet.Cells[rowCnt, 4]];
                formatRange.NumberFormat = "dd/MM/yyyy";
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                formatRange = oSheet.Range[oSheet.Cells[9, 5], oSheet.Cells[rowCnt, 5]];
                formatRange.NumberFormat = "dd/MM/yyyy";
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                for (int i = 6; i <= lastColumn - 1; i++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[9, i], oSheet.Cells[rowCnt, i]];
                    formatRange.NumberFormat = "#,##0.00;(#,##0.0); ; ";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                formatRange = oSheet.Range[oSheet.Cells[1, 1], oSheet.Cells[3, lastColumn]];
                formatRange.Font.Name = fontName;

                formatRange = oSheet.Range[oSheet.Cells[9, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                formatRange = oSheet.Range[oSheet.Cells[9, 6], oSheet.Cells[rowCnt, lastColumn - 1]];
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                BorderAround(oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]]);
                // filter
                this.Cursor = Cursors.Default;
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }
        private void DanhSachXL_TG()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptDSXepLoaiNam_TG"), conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Commons.Modules.ObjSystems.setDate1Month(datTThang.DateTime, 0);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Commons.Modules.ObjSystems.setDate1Month(datDThang.DateTime, 1);
                cmd.Parameters.Add("@LOC_NHAN_SU", SqlDbType.Int).Value = rdo_LocLaoDong.SelectedIndex;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dtTo = new DataTable();
                dtTo = ds.Tables[0].Copy();

                DataTable dtData = new DataTable();
                dtData = ds.Tables[1].Copy();

                DataTable dtLoaiNghi = new DataTable();
                dtLoaiNghi = ds.Tables[2].Copy();

                if (dtTo.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oApp;
                Microsoft.Office.Interop.Excel.Workbook oBook;
                Excel.Worksheet oSheet;
                this.Cursor = Cursors.WaitCursor;
                oApp = new Microsoft.Office.Interop.Excel.Application();
                oApp.Visible = true;
                Commons.Modules.ObjSystems.ShowWaitForm(this);
                oBook = oApp.Workbooks.Add();
                oSheet = (Excel.Worksheet)oBook.ActiveSheet;

                int lastColumn = dtData.Columns.Count - 1;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                int oRow = 5;
                TaoTTChung_TheoDV(oSheet, 1, 3, 1, lastColumn, 0, 0);
                foreach (DataRow rowTo in dtTo.Rows)
                {
                    //oSheet.Name = rowTo[1].ToString();
                    DataTable dataTemp = dtData.AsEnumerable().Where(r => r.Field<Int64>("ID_TO") == Convert.ToInt64(rowTo[0])).CopyToDataTable().Copy();

                    Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow, lastColumn]];
                    row2_TieuDe_BaoCao.Merge();
                    row2_TieuDe_BaoCao.Font.Size = 20;
                    row2_TieuDe_BaoCao.Font.Name = fontName;
                    row2_TieuDe_BaoCao.Font.Bold = true;
                    row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row2_TieuDe_BaoCao.RowHeight = 25;
                    row2_TieuDe_BaoCao.Value2 = "DANH SÁCH XẾP LOẠI A,B,C -" + rowTo[1].ToString();

                    oRow++;

                    row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow, lastColumn]];
                    row2_TieuDe_BaoCao.Merge();
                    row2_TieuDe_BaoCao.Font.Size = 20;
                    row2_TieuDe_BaoCao.Font.Name = fontName;
                    row2_TieuDe_BaoCao.Font.Bold = true;
                    row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row2_TieuDe_BaoCao.RowHeight = 25;
                    row2_TieuDe_BaoCao.Value2 = "TỪ THÁNG " + datTThang.DateTime.ToString("MM/yyyy") + " ĐẾN THÁNG " + datDThang.DateTime.ToString("MM/yyyy") + "";

                    oRow = oRow + 3;
                    int oCol = 1;
                    Range row2_TieuDeCot_BaoCao;

                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "STT";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 4.75;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Số thẻ";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 9.71;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Họ và tên";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 21.29;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Ngày vào làm";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 11.57;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Ngày công đi làm";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 6.71;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow, oCol + 7]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Vắng mặt";

                    ////lay tieu de cot nghi 
                    //DataRow[] drN = dtLoaiNghi.Select();
                    ////string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                    //foreach (DataRow rowN in drN)
                    //{
                    //    oSheet.Range[oSheet.Cells[oRow + 1, oCol], oSheet.Cells[oRow + 1, oCol]] = rowN[1].ToString();
                    //    oSheet.Range[oSheet.Cells[oRow + 1, oCol], oSheet.Cells[oRow + 1, oCol]].ColumnWidth = 6.71;
                    //    oCol++;
                    //}

                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow + 1, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Phép";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 5.71;
                    row2_TieuDeCot_BaoCao.WrapText = true;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow + 1, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Nghỉ tự do";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 5.86;
                    row2_TieuDeCot_BaoCao.WrapText = true;

                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow + 1, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Nghỉ việc riêng không lương";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 7.71;
                    row2_TieuDeCot_BaoCao.WrapText = true;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow + 1, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Nghỉ việc riêng có lương";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 7.29;
                    row2_TieuDeCot_BaoCao.WrapText = true;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow + 1, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Nghỉ ốm";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 5.71;
                    row2_TieuDeCot_BaoCao.WrapText = true;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow + 1, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Nghỉ con ốm";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 6.71;
                    row2_TieuDeCot_BaoCao.WrapText = true;



                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow + 1, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Nghỉ thai sản";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 6;
                    row2_TieuDeCot_BaoCao.WrapText = true;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow + 1, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Value2 = "Nghỉ lễ tết";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 6;
                    row2_TieuDeCot_BaoCao.WrapText = true;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Cá nhân tự xếp loại";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 9.43;
                    row2_TieuDeCot_BaoCao.WrapText = true;



                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Tổ trưởng XN xếp loại";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 9;
                    row2_TieuDeCot_BaoCao.WrapText = true;



                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Ghi chú";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 7.71;
                    row2_TieuDeCot_BaoCao.WrapText = true;


                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Ký tên";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 7.86;
                    row2_TieuDeCot_BaoCao.WrapText = true;



                    oCol++;
                    row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[oRow, oCol], oSheet.Cells[oRow + 1, oCol]];
                    row2_TieuDeCot_BaoCao.Merge();
                    row2_TieuDeCot_BaoCao.Value2 = "Mã số cũ";
                    row2_TieuDeCot_BaoCao.ColumnWidth = 0;


                    Microsoft.Office.Interop.Excel.Range row_TieuDe_Format = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow + 1, lastColumn]];
                    row_TieuDe_Format.Font.Name = fontName;
                    row_TieuDe_Format.Font.Size = 12;
                    row_TieuDe_Format.WrapText = true;
                    row_TieuDe_Format.Font.Bold = true;
                    row_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oRow = oRow + 2;
                    int rowCnt = 0;
                    int rowBD = oRow;

                    DataRow[] dr = dataTemp.Select();
                    string[,] rowData = new string[dr.Count(), dataTemp.Columns.Count];
                    foreach (DataRow row in dataTemp.Rows)
                    {
                        for (int col = 0; col < dataTemp.Columns.Count; col++)
                        {
                            rowData[rowCnt, col] = row[col].ToString();
                        }
                        rowCnt++;
                    }

                    oRow = rowBD + rowCnt - 1;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Value2 = rowData;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Size = fontSizeNoiDung;
                    oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Name = fontName;


                    // TÍNH TỔNG
                    oRow++;
                    Microsoft.Office.Interop.Excel.Range row_TinhTong = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow, 3]];
                    row_TinhTong.Merge();
                    row_TinhTong.Value2 = "Tổng cộng";
                    row_TinhTong.Font.Name = fontName;
                    row_TinhTong.Font.Size = 12;
                    row_TinhTong.WrapText = true;
                    row_TinhTong.Font.Bold = true;
                    row_TinhTong.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    row_TinhTong.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    Microsoft.Office.Interop.Excel.Range formatRange;
                    Microsoft.Office.Interop.Excel.Range formatRange1;
                    formatRange1 = oSheet.Range[oSheet.Cells[oRow, 5], oSheet.Cells[oRow, 5]];
                    formatRange1.Value2 = "=SUM(E" + rowBD + ":E" + (oRow - 1).ToString() + ")";

                    formatRange = oSheet.Range[oSheet.Cells[oRow, 5], oSheet.Cells[oRow, lastColumn - 5]];
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Bold = true;

                    if (dataTemp.Rows.Count > 1)
                    {
                        formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    }

                    formatRange = oSheet.Range[oSheet.Cells[rowBD, 4], oSheet.Cells[oRow, 4]]; //format cột ngày vào làm
                    formatRange.NumberFormat = "dd/MM/yyy";
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }

                    for (int colFormat = 5; colFormat <= lastColumn - 5; colFormat++) // format từ cột t
                    {
                        formatRange = oSheet.Range[oSheet.Cells[rowBD, colFormat], oSheet.Cells[oRow, colFormat]];
                        formatRange.NumberFormat = "0.0;-0;;@";
                        try
                        {
                            formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                        }
                        catch { }

                    }

                    Microsoft.Office.Interop.Excel.Range formatRangeTest;

                    for (int i = rowBD; i <= oRow; i++)
                    {
                        Microsoft.Office.Interop.Excel.FormatCondition condition;
                        formatRangeTest = oSheet.Range[oSheet.Cells[i, 16], oSheet.Cells[i, 16]];
                        if (formatRangeTest.Value2 == "NV")
                        {
                            formatRangeTest = oSheet.Range[oSheet.Cells[i, 2], oSheet.Cells[i, 2]];
                            formatRangeTest.Interior.Color = Color.FromArgb(255, 255, 0); // Đặt màu nền của ô B1 thành màu vàng
                        }
                    }
                    formatRangeTest = oSheet.Range[oSheet.Cells[rowBD, 16], oSheet.Cells[oRow, 16]];
                    formatRangeTest.Value2 = "";
                    BorderAround(oSheet.Range[oSheet.Cells[rowBD - 2, 1], oSheet.Cells[oRow, lastColumn]]);

                    oRow = oRow + 4;




                    //oSheet = (Excel.Worksheet)oBook.ActiveSheet;
                    //oSheet = oBook.Worksheets.Add(After: oBook.Sheets[oBook.Sheets.Count]);
                }

                Microsoft.Office.Interop.Excel.Range formatRange2;
                formatRange2 = oSheet.Range[oSheet.Cells[oRow, lastColumn - 5], oSheet.Cells[oRow, lastColumn - 5]]; // 
                formatRange2.Merge();
                formatRange2.Value2 = "Ngày .... tháng .... năm ....";
                formatRange2.Font.Size = fontSizeNoiDung;
                formatRange2.Font.Name = fontName;
                formatRange2.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange2.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                oRow++;
                formatRange2 = oSheet.Range[oSheet.Cells[oRow, lastColumn - 5], oSheet.Cells[oRow, lastColumn - 5]]; // 
                formatRange2.Merge();
                formatRange2.Value2 = "Trưởng ĐVỊ/GĐXN";
                formatRange2.Font.Size = fontSizeNoiDung;
                formatRange2.Font.Name = fontName;
                formatRange2.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange2.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;





                oSheet.PageSetup.FitToPagesWide = 1;
                oSheet.PageSetup.FitToPagesTall = false;
                oSheet.PageSetup.Zoom = false;
                oSheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;
                oSheet.PageSetup.LeftMargin = oApp.InchesToPoints(0.25);
                oSheet.PageSetup.RightMargin = oApp.InchesToPoints(0.25);
                oSheet.PageSetup.TopMargin = oApp.InchesToPoints(0.25);
                oSheet.PageSetup.BottomMargin = oApp.InchesToPoints(0.25);
                oSheet.PageSetup.HeaderMargin = oApp.InchesToPoints(0.3);
                oSheet.PageSetup.FooterMargin = oApp.InchesToPoints(0.3);
                oSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;

                oSheet.PageSetup.PrintTitleRows = "$A$9:$" + CharacterIncrement(lastColumn) + "$10";

                this.Cursor = Cursors.Default;
                oBook.Sheets[1].Activate();
                oApp.Visible = true;
                Commons.Modules.ObjSystems.HideWaitForm();
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.HideWaitForm();
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message);
            }
        }
        private void BangCongVangNam_TG()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangTHCCVangNam_TG"), conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Commons.Modules.ObjSystems.setDate1Month(datTThang.DateTime, 0);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Commons.Modules.ObjSystems.setDate1Month(datDThang.DateTime, 1);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dtTo = new DataTable();
                dtTo = ds.Tables[0].Copy();

                DataTable dtData = new DataTable();
                dtData = ds.Tables[1].Copy();

                DataTable dtLoaiNghi = new DataTable();
                dtLoaiNghi = ds.Tables[2].Copy();

                if (dtTo.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oApp;
                Microsoft.Office.Interop.Excel.Workbook oBook;
                Excel.Worksheet oSheet;
                this.Cursor = Cursors.WaitCursor;
                oApp = new Microsoft.Office.Interop.Excel.Application();
                oApp.Visible = true;
                Commons.Modules.ObjSystems.ShowWaitForm(this);
                oBook = oApp.Workbooks.Add();
                oSheet = (Excel.Worksheet)oBook.ActiveSheet;

                int lastColumn = dtData.Columns.Count - 1;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                int oRow = 1;

                TaoTTChung_TheoDV(oSheet, 1, 2, 1, 7, 0, 0);

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[5, lastColumn]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 15;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "DANH SÁCH XẾP LOẠI A,B,C TỪ THÁNG " + datTThang.DateTime.ToString("MM/yyyy") + " ĐẾN THÁNG " + datDThang.DateTime.ToString("MM/yyyy") + "";


                int oCol = 1;
                Range row2_TieuDeCot_BaoCao;

                row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[8, oCol], oSheet.Cells[9, oCol]];
                row2_TieuDeCot_BaoCao.Merge();
                row2_TieuDeCot_BaoCao.Value2 = "STT";
                row2_TieuDeCot_BaoCao.ColumnWidth = 10;

                oCol++;
                row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[8, oCol], oSheet.Cells[9, oCol]];
                row2_TieuDeCot_BaoCao.Merge();
                row2_TieuDeCot_BaoCao.Value2 = "Số thẻ";
                row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                oCol++;
                row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[8, oCol], oSheet.Cells[9, oCol]];
                row2_TieuDeCot_BaoCao.Merge();
                row2_TieuDeCot_BaoCao.Value2 = "Họ và tên";
                row2_TieuDeCot_BaoCao.ColumnWidth = 25;


                oCol++;
                row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[8, oCol], oSheet.Cells[9, oCol]];
                row2_TieuDeCot_BaoCao.Merge();
                row2_TieuDeCot_BaoCao.Value2 = "Ngày vào làm";
                row2_TieuDeCot_BaoCao.ColumnWidth = 15;

                oCol++;
                row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[8, oCol], oSheet.Cells[8, oCol + dtLoaiNghi.Rows.Count]];
                row2_TieuDeCot_BaoCao.Merge();
                row2_TieuDeCot_BaoCao.Value2 = "Vắng mặt";

                //lay tieu de cot nghi 
                DataRow[] drN = dtLoaiNghi.Select();
                //string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                foreach (DataRow rowN in drN)
                {
                    oSheet.Range[oSheet.Cells[9, oCol], oSheet.Cells[9, oCol]] = rowN[1].ToString();
                    oCol++;
                }

                row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[9, oCol], oSheet.Cells[9, oCol]];
                row2_TieuDeCot_BaoCao.Value2 = "Tổng cộng";
                row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                oCol++;
                row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[8, oCol], oSheet.Cells[9, oCol]];
                row2_TieuDeCot_BaoCao.Merge();
                row2_TieuDeCot_BaoCao.Value2 = "Xếp loại";
                row2_TieuDeCot_BaoCao.ColumnWidth = 15;

                oCol++;
                row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[8, oCol], oSheet.Cells[9, oCol]];
                row2_TieuDeCot_BaoCao.Merge();
                row2_TieuDeCot_BaoCao.Value2 = "Ký tên";
                row2_TieuDeCot_BaoCao.ColumnWidth = 15;


                oCol++;
                row2_TieuDeCot_BaoCao = oSheet.Range[oSheet.Cells[8, oCol], oSheet.Cells[9, oCol]];
                row2_TieuDeCot_BaoCao.Merge();
                row2_TieuDeCot_BaoCao.Value2 = "Mã số cũ";
                row2_TieuDeCot_BaoCao.ColumnWidth = 30;


                Microsoft.Office.Interop.Excel.Range row_TieuDe_Format = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[9, lastColumn]];
                row_TieuDe_Format.Font.Name = fontName;
                row_TieuDe_Format.Font.Size = 12;
                row_TieuDe_Format.WrapText = true;
                row_TieuDe_Format.Font.Bold = true;
                row_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                oRow = 10;

                Microsoft.Office.Interop.Excel.Range formatRange;
                int rowCnt = 0;
                int rowBD = 10;
                Int64[] TO = dtData.AsEnumerable().Select(r => r.Field<Int64>("ID_TO")).Distinct().ToArray();

                for (int j = 0; j < dtTo.Rows.Count; j++)
                {
                    DataTable dtTemp = new DataTable();
                    dtTemp = dtData.AsEnumerable().Where(r => r.Field<Int64>("ID_TO") == TO[j]).CopyToDataTable().Copy();
                    DataRow[] dr = dtTemp.Select();

                    // Tạo group tổ
                    Range row_groupXI_NGHIEP_Format = oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[rowBD, 1]];
                    row_groupXI_NGHIEP_Format.Value2 = dtTo.Rows[j]["TEN_TO"].ToString();
                    row_groupXI_NGHIEP_Format.Font.Bold = true;

                    rowBD++;
                    rowCnt = rowBD;
                    foreach (DataRow row in dr)
                    {
                        dynamic[] arr = { row["STT"].ToString(), row["MS_CN"].ToString(), row["HO_TEN"].ToString(), row["NGAY_VAO_LAM"].ToString()
                        };

                        //lay tieu de cot nghi 
                        for (int i = dtData.Columns["NGAY_VAO_LAM"].Ordinal + 1; i <= dtData.Columns["TONG_CONG"].Ordinal - 1; i++)
                        {
                            arr = arr.Append(row[dtData.Columns[i].ColumnName.ToString()]).ToArray();
                        }

                        arr = arr.Append("=SUM(" + CharacterIncrement(dtData.Columns["NGAY_VAO_LAM"].Ordinal + 1) + "" + rowCnt + " :" + CharacterIncrement(dtData.Columns["TONG_CONG"].Ordinal - 1) + "" + rowCnt + ")").ToArray();
                        arr = arr.Append(row["XEP_LOAI"]).ToArray();
                        arr = arr.Append(row["KY_TEN"]).ToArray();
                        arr = arr.Append(row["MS_CN_CU"]).ToArray();
                        Range rowData = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, lastColumn]];
                        rowData.WrapText = true;
                        rowData.Value2 = arr;
                        rowCnt++;
                    }
                    rowBD = rowCnt;
                }

                //oRow = rowBD + rowCnt - 1;
                //oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Value2 = rowData;
                //oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Size = fontSizeNoiDung;
                //oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Name = fontName;


                formatRange = oSheet.Range[oSheet.Cells[rowBD, 4], oSheet.Cells[rowCnt, 4]]; //format cột ngày vào làm
                formatRange.NumberFormat = "dd/MM/yyy";
                try
                {
                    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                catch { }

                for (int colFormat = 5; colFormat <= lastColumn - 3; colFormat++) // format từ cột t
                {
                    formatRange = oSheet.Range[oSheet.Cells[10, colFormat], oSheet.Cells[rowCnt, colFormat]];
                    formatRange.NumberFormat = "0.00;-0;;@";
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }
                }

                formatRange = oSheet.Range[oSheet.Cells[10, 1], oSheet.Cells[rowCnt, lastColumn]]; // 
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Name = fontName;

                BorderAround(oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[rowCnt, lastColumn]]);

                rowCnt++;
                rowCnt++;

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, lastColumn - 5], oSheet.Cells[rowCnt, lastColumn - 5]]; // 
                formatRange.Merge();
                formatRange.Value2 = "Ngày .... tháng .... năm ....";
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Name = fontName;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                rowCnt++;
                formatRange = oSheet.Range[oSheet.Cells[rowCnt, lastColumn - 5], oSheet.Cells[rowCnt, lastColumn - 5]]; // 
                formatRange.Merge();
                formatRange.Value2 = "Trưởng ĐVỊ/GĐXN";
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Name = fontName;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                this.Cursor = Cursors.Default;
                oBook.Sheets[1].Activate();
                oApp.Visible = true;
                Commons.Modules.ObjSystems.HideWaitForm();
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.HideWaitForm();
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message);
            }
        }
        private void BangTheoDoiPhepNam_TG()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCCongNam;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "spGetTheoDoiPhepNam_TG"), conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = Commons.Modules.ObjSystems.setDate1Month(datTThang.DateTime, 0);
                cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = Commons.Modules.ObjSystems.setDate1Month(datDThang.DateTime, 1);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCCongNam = new DataTable();
                dtBCCongNam = ds.Tables[0].Copy();

                if (dtBCCongNam.Rows.Count == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                int lastColumn = 0;
                lastColumn = dtBCCongNam.Columns.Count;

                int col = 1;

                TaoTTChung_TheoDV(oSheet, 1, 2, 1, 7, 0, 0);

                Range row2_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, lastColumn]];
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG THEO DÕI PHÉP NĂM " + Convert.ToDateTime(datNam.EditValue).Year + "";


                Range row4_TieuDe_Format = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[8, lastColumn]];
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                Range row5_TieuDe_STT = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[8, col]];
                row5_TieuDe_STT.Merge();
                row5_TieuDe_STT.Value2 = "STT";
                row5_TieuDe_STT.ColumnWidth = 10;
                col++;

                Range row5_TieuDe_MSCN = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[8, col]];
                row5_TieuDe_MSCN.Merge();
                row5_TieuDe_MSCN.Value2 = "SỐ THẺ";
                row5_TieuDe_MSCN.ColumnWidth = 12;
                col++;

                Range row5_TieuDe_HOTEN = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[8, col]];
                row5_TieuDe_HOTEN.Merge();
                row5_TieuDe_HOTEN.Value2 = "HỌ VÀ TÊN";
                row5_TieuDe_HOTEN.ColumnWidth = 25;
                col++;

                Range row5_TieuDe_XN = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[8, col]];
                row5_TieuDe_XN.Merge();
                row5_TieuDe_XN.Value2 = "NGÀY VÀO CÔNG TY";
                row5_TieuDe_XN.ColumnWidth = 10;
                col++;

                Range row5_TieuDe_TO = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[8, col]];
                row5_TieuDe_TO.Merge();
                row5_TieuDe_TO.Value2 = "HĐLĐ CHÍNH";
                row5_TieuDe_TO.ColumnWidth = 10;
                col++;

                Range row5_TieuDe_NVL = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[8, col]];
                row5_TieuDe_NVL.Merge();
                row5_TieuDe_NVL.Value2 = "HSL";
                row5_TieuDe_NVL.ColumnWidth = 10;
                col++;

                row4_TieuDe_Format = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[8, col]];
                row4_TieuDe_Format.Merge();
                row4_TieuDe_Format.Value2 = "NGÀY TIÊU CHUẨN";
                row4_TieuDe_Format.ColumnWidth = 10;
                col++;

                //lay tieu de cot thang 
                for (int iThang = 1; iThang <= 12; iThang++)
                {
                    oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[8, col]].Merge();
                    oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[8, col]] = "T " + iThang.ToString();
                    col++;
                }

                Range row5_TieuDe_TongCong = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[8, col]];
                row5_TieuDe_TongCong.Merge();
                row5_TieuDe_TongCong.Value2 = "TỔNG CỘNG";
                row5_TieuDe_TongCong.ColumnWidth = 8;

                col++;
                row5_TieuDe_TongCong = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[8, col]];
                row5_TieuDe_TongCong.Merge();
                row5_TieuDe_TongCong.Value2 = "PHÉP CÒN LẠI";
                row5_TieuDe_TongCong.ColumnWidth = 8;

                col++;
                row5_TieuDe_TongCong = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[8, col]];
                row5_TieuDe_TongCong.Merge();
                row5_TieuDe_TongCong.Value2 = "SỐ TIỀN PHẢI TRẢ";
                row5_TieuDe_TongCong.ColumnWidth = 13;

                col++;
                row5_TieuDe_TongCong = oSheet.Range[oSheet.Cells[6, col], oSheet.Cells[8, col]];
                row5_TieuDe_TongCong.Merge();
                row5_TieuDe_TongCong.Value2 = "KÝ NHẬN";
                row5_TieuDe_TongCong.ColumnWidth = 10;

                DataRow[] dr = dtBCCongNam.Select();
                string[,] rowData = new string[dr.Count(), dtBCCongNam.Columns.Count];

                int rowCnt = 0;
                int i = 0;
                foreach (DataRow row in dr)
                {
                    for (i = 0; i < dtBCCongNam.Columns.Count; i++)
                    {
                        rowData[rowCnt, i] = row[i].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 8;
                oSheet.Range[oSheet.Cells[9, 1], oSheet.Cells[rowCnt, lastColumn]].Value2 = rowData;
                Microsoft.Office.Interop.Excel.Range formatRange;
                Microsoft.Office.Interop.Excel.Range formatRange1;


                formatRange = oSheet.Range[oSheet.Cells[9, dtBCCongNam.Columns["TONG_CONG"].Ordinal + 1], oSheet.Cells[9, dtBCCongNam.Columns["TONG_CONG"].Ordinal + 1]];
                formatRange.Value2 = "=SUM(H9:S9)";
                formatRange1 = oSheet.Range[oSheet.Cells[9, dtBCCongNam.Columns["TONG_CONG"].Ordinal + 1], oSheet.Cells[rowCnt, dtBCCongNam.Columns["TONG_CONG"].Ordinal + 1]];

                if (dtBCCongNam.Rows.Count > 0)
                {
                    formatRange.AutoFill(formatRange1, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                }

                formatRange = oSheet.Range[oSheet.Cells[9, dtBCCongNam.Columns["SO_TIEN_TRA"].Ordinal + 1], oSheet.Cells[9, dtBCCongNam.Columns["SO_TIEN_TRA"].Ordinal + 1]];
                formatRange.Value2 = "=E9/208*8";
                formatRange1 = oSheet.Range[oSheet.Cells[9, dtBCCongNam.Columns["SO_TIEN_TRA"].Ordinal + 1], oSheet.Cells[rowCnt, dtBCCongNam.Columns["SO_TIEN_TRA"].Ordinal + 1]];
                formatRange.AutoFill(formatRange1, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);


                formatRange = oSheet.Range[oSheet.Cells[9, 4], oSheet.Cells[rowCnt, 4]];
                formatRange.NumberFormat = "dd/MM/yyyy";
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                formatRange = oSheet.Range[oSheet.Cells[9, 5], oSheet.Cells[rowCnt, 5]];
                formatRange.NumberFormat = "dd/MM/yyyy";
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                formatRange = oSheet.Range[oSheet.Cells[9, 6], oSheet.Cells[rowCnt, 6]];
                formatRange.NumberFormat = "#,##0;(#,##0);;";
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                for (i = 7; i <= lastColumn - 2; i++)
                {
                    formatRange = oSheet.Range[oSheet.Cells[9, i], oSheet.Cells[rowCnt, i]];
                    formatRange.NumberFormat = "0.0;-0;;@";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                formatRange = oSheet.Range[oSheet.Cells[9, lastColumn - 1], oSheet.Cells[rowCnt, lastColumn - 1]];
                formatRange.NumberFormat = "#,##0;(#,##0);;";
                try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                formatRange = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]];
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;


                BorderAround(oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[rowCnt, lastColumn]]);

                rowCnt++;
                rowCnt++;

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 1], oSheet.Cells[rowCnt, 1]]; // 
                formatRange.Value2 = "Ngày .... tháng .... năm 20";
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Name = fontName;

                rowCnt++;
                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 2], oSheet.Cells[rowCnt, 2]]; // 
                formatRange.Merge();
                formatRange.Value2 = "Lập bảng";
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Name = fontName;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, 7], oSheet.Cells[rowCnt, 7]]; // 
                formatRange.Value2 = "GIÁM ĐỐC XN";
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Name = fontName;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, lastColumn - 5], oSheet.Cells[rowCnt, lastColumn - 5]]; // 
                formatRange.Value2 = "P.TCKT";
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Name = fontName;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[rowCnt, lastColumn - 1], oSheet.Cells[rowCnt, lastColumn - 1]]; // 
                formatRange.Value2 = "TỔNG GIÁM ĐỐC";
                formatRange.Font.Size = fontSizeNoiDung;
                formatRange.Font.Name = fontName;
                formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                // filter
                this.Cursor = Cursors.Default;
                oXL.Visible = true;
                oXL.UserControl = true;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }
        public int GetWeeksInYear(int year) // Đếm số tuần trong năm
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            DateTime date1 = new DateTime(year, 12, 31);
            Calendar cal = dfi.Calendar;
            return cal.GetWeekOfYear(date1, dfi.CalendarWeekRule,
                                                dfi.FirstDayOfWeek);
        }
        public int TaoTTChung(Excel.Worksheet MWsheet, int DongBD, int CotBD, int DongKT, int CotKT, float MLeft, float MTop)
        {
            try
            {
                DataTable dtTmp = Commons.Modules.ObjSystems.DataThongTinChung(-1);
                Microsoft.Office.Interop.Excel.Range CurCell = MWsheet.Range[MWsheet.Cells[DongBD, 1], MWsheet.Cells[DongKT, 1]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);

                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT - 3]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = dtTmp.Rows[0]["TEN_CTY"];



                DongBD += 1;
                DongKT += 1;
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "diachi") + " : " + dtTmp.Rows[0]["DIA_CHI"].ToString();

                DongBD += 1;
                DongKT += 1;
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "dienthoai") + " : " + dtTmp.Rows[0]["DIEN_THOAI"] + "  " + Commons.Modules.ObjLanguages.GetLanguage("frmChung", "Fax") + " : " + dtTmp.Rows[0]["FAX"].ToString();

                //DongBD += 1;
                //DongKT += 1;
                //CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                //CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                //CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                //CurCell.Merge(true);
                //CurCell.Font.Bold = true;
                //CurCell.Borders.LineStyle = 0;
                //CurCell.Value2 = "Email : " + dtTmp.Rows[0]["EMAIL"];

                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Masters");
                GetImage((byte[])dtTmp.Rows[0]["LOGO"], System.Windows.Forms.Application.StartupPath, "logo.bmp");
                MWsheet.Shapes.AddPicture(System.Windows.Forms.Application.StartupPath + @"\logo.bmp", Office.MsoTriState.msoFalse, Office.MsoTriState.msoCTrue, MLeft, MTop, 50, 50);
                System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + @"\logo.bmp");

                return DongBD + 1;
            }
            catch
            {
                return DongBD + 1;
            }
        }
        public int TaoTTChung_TheoDV(Excel.Worksheet MWsheet, int DongBD, int CotBD, int DongKT, int CotKT, float MLeft, float MTop)
        {
            try
            {
                string sSQL = "SELECT * FROM dbo.DON_VI WHERE ID_DV = " + LK_DON_VI.EditValue;
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSQL));

                Microsoft.Office.Interop.Excel.Range CurCell = MWsheet.Range[MWsheet.Cells[DongBD, 1], MWsheet.Cells[DongKT, 1]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);

                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT - 3]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = dtTmp.Rows[0]["TEN_DV"];
                CurCell.Font.Name = "Times New Roman";
                CurCell.Font.Size = 12;


                DongBD += 1;
                DongKT += 1;
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "diachi") + " : " + dtTmp.Rows[0]["DIA_CHI"].ToString();
                CurCell.Font.Name = "Times New Roman";
                CurCell.Font.Size = 12;

                DongBD += 1;
                DongKT += 1;
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "dienthoai") + " : " + dtTmp.Rows[0]["DIEN_THOAI"] + "  " + Commons.Modules.ObjLanguages.GetLanguage("frmChung", "Fax") + " : " + dtTmp.Rows[0]["FAX"].ToString();
                CurCell.Font.Name = "Times New Roman";
                CurCell.Font.Size = 12;

                //DongBD += 1;
                //DongKT += 1;
                //CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                //CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                //CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                //CurCell.Merge(true);
                //CurCell.Font.Bold = true;
                //CurCell.Borders.LineStyle = 0;
                //CurCell.Value2 = "Email : " + dtTmp.Rows[0]["EMAIL"];

                DataTable dtLogo = Commons.Modules.ObjSystems.DataThongTinChung(-1);
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Masters");
                GetImage((byte[])dtLogo.Rows[0]["LOGO"], System.Windows.Forms.Application.StartupPath, "logo.bmp");
                MWsheet.Shapes.AddPicture(System.Windows.Forms.Application.StartupPath + @"\logo.bmp", Office.MsoTriState.msoFalse, Office.MsoTriState.msoCTrue, MLeft, MTop, 50, 50);
                System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + @"\logo.bmp");

                return DongBD + 1;
            }
            catch
            {
                return DongBD + 1;
            }
        }
        public void GetImage(byte[] Logo, string sPath, string sFile)
        {
            try
            {
                string strPath = sPath + @"\" + sFile;
                System.IO.MemoryStream stream = new System.IO.MemoryStream(Logo);
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                img.Save(strPath);
            }
            catch (Exception)
            {
            }
        }

    }
}
