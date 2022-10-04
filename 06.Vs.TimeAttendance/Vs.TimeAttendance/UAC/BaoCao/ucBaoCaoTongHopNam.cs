using DevExpress.CodeParser;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Linq;
using Vs.Payroll;
using Vs.Report;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Collections.Generic;
using System.Reflection;
using System.Globalization;

namespace Vs.TimeAttendance
{
    public partial class ucBaoCaoTongHopNam : DevExpress.XtraEditors.XtraUserControl
    {
        private string SaveExcelFile;
        public ucBaoCaoTongHopNam()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
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
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)))
                                    {
                                        case "MT":
                                            {
                                                BangTHChamCongNam_MT();
                                                break;
                                            }
                                        case "SB":
                                            {
                                                BangTHChamCongNam_SB();
                                                break;
                                            }
                                        default:
                                            //DanhGiaTinhTrangThuViec_DM();
                                            BangTHChamCongNam_MT();
                                            break;
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)))
                                    {
                                        case "MT":
                                            {
                                                BangTHCongTangCaNam_MT();
                                                break;
                                            }
                                        case "SB":
                                            {
                                                BangTHCongTangCaNam_SB();
                                                break;
                                            }
                                        case "DM":
                                            {
                                                InBaoCaoTongHop_DM();
                                                break;
                                            }
                                        default:
                                            BangTHCongTangCaNam_MT();
                                            break;
                                    }
                                }
                                break;

                            case 2:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)))
                                    {
                                        case "MT":
                                            {
                                                BangTHCongVangNam_MT();
                                                break;
                                            }
                                        default:
                                            BangTHCongVangNam_MT();
                                            break;
                                    }
                                }
                                break;
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

            Commons.Modules.sLoad = "";

            //LoadTinhTrangHopDong();
        }

        private void LoadCboDonVi()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
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
            if (Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString() != "DM")
            {
                return;
            }
            switch (rdo_ChonBaoCao.SelectedIndex)
            {
                case 1:
                    {
                        rdo_ChonNam.Visible = true;
                        break;
                    }
                default:
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
                    cmd.Parameters.Add("@LamTron", SqlDbType.Int).Value = Commons.Modules.iLamTronGio;
                    cmd.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    dt = new DataTable();
                    dt = ds.Tables[0].Copy();
                    dt.TableName = "DATA";
                    frm.AddDataSource(dt);
                    frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                }
                catch
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
                    dt.TableName = "DATA";
                    frm.AddDataSource(dt);
                    frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
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
                    dt.TableName = "DATA";
                    frm.AddDataSource(dt);
                    frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
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
                    dt.TableName = "DATA";
                    frm.AddDataSource(dt);
                    frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
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
                    dt.TableName = "DATA";
                    frm.AddDataSource(dt);
                    frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
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
                    dt.TableName = "DATA";
                    frm.AddDataSource(dt);
                    frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                }
                catch
                { }
                frm.ShowDialog();
            }
            catch { }
        }
        private void InBaoCaoTongHop_DM()
        {
            switch (rdo_ChonNam.SelectedIndex)
            {
                case 0:
                    {
                        BangCongTangCaTuan_DM();
                        break;
                    }
                case 1:
                    {
                        BangCongTangCaQuy_DM();
                        break;
                    }
                case 2:
                    {
                        BangCongTangCaNam_DM();
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


                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }
                Excel.Application oXL;
                Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Excel.Application();
                oXL.Visible = true;


                //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                oWB = (Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", "AA2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THÁNG NGOÀI GIỜ NĂM " + Convert.ToDateTime(datNam.EditValue).Year + "";


                Range row4_TieuDe_Format = oSheet.get_Range("A4", "AA6"); //27 + 31
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);

                Range row4_TieuDe_TTNV = oSheet.get_Range("A4", "G4");
                row4_TieuDe_TTNV.Merge();
                row4_TieuDe_TTNV.Value2 = "Thông tin nhân viên (Staff information)";

                Range row4_TieuDe_TTC = oSheet.get_Range("H4", "S4");
                row4_TieuDe_TTC.Merge();
                row4_TieuDe_TTC.Value2 = "Tháng tăng ca (Overtime day)";

                Range row4_TieuDe_TTTCT = oSheet.get_Range("T4", "AA4");
                row4_TieuDe_TTTCT.Merge();
                row4_TieuDe_TTTCT.Value2 = "Thông tin tăng ca tháng (Information about monthly overtime)";

                Range row5_TieuDe_STT = oSheet.get_Range("A5", "A6");
                row5_TieuDe_STT.Merge();
                row5_TieuDe_STT.Value2 = "STT";
                row5_TieuDe_STT.ColumnWidth = 5;

                Range row6_TieuDe_STT = oSheet.get_Range("A6");
                row6_TieuDe_STT.RowHeight = 54;

                Range row5_TieuDe_MSCN = oSheet.get_Range("B5", "B6");
                row5_TieuDe_MSCN.Merge();
                row5_TieuDe_MSCN.Value2 = "MSCN";
                row5_TieuDe_MSCN.ColumnWidth = 10;

                Range row5_TieuDe_HOTEN = oSheet.get_Range("C5", "C6");
                row5_TieuDe_HOTEN.Merge();
                row5_TieuDe_HOTEN.Value2 = "Họ và tên";
                row5_TieuDe_HOTEN.ColumnWidth = 15;

                Range row5_TieuDe_XN = oSheet.get_Range("D5", "D6");
                row5_TieuDe_XN.Merge();
                row5_TieuDe_XN.Value2 = "Xưởng/Phòng ban";
                row5_TieuDe_XN.ColumnWidth = 15;


                Range row5_TieuDe_TO = oSheet.get_Range("E5", "E6");
                row5_TieuDe_TO.Merge();
                row5_TieuDe_TO.Value2 = "Tổ";
                row5_TieuDe_TO.ColumnWidth = 15;

                Range row5_TieuDe_NTV = oSheet.get_Range("F5", "F6");
                row5_TieuDe_NTV.Merge();
                row5_TieuDe_NTV.Value2 = "Ngày thử việc";
                row5_TieuDe_NTV.ColumnWidth = 10;

                Range row5_TieuDe_NVL = oSheet.get_Range("G5", "G6");
                row5_TieuDe_NVL.Merge();
                row5_TieuDe_NVL.Value2 = "Ngày vào làm";
                row5_TieuDe_NVL.ColumnWidth = 10;

                int thang_bd = 1;
                int thang_kt = 12;
                int col_bd = 8;
                while (thang_bd <= thang_kt)
                {
                    Range rowtemp = oSheet.get_Range(CharacterIncrement(col_bd - 1) + "6");
                    rowtemp.Value2 = null;
                    Range row6_b = oSheet.get_Range(CharacterIncrement(col_bd - 1) + "5");
                    //Range row6_b = oSheet.get_Range(CharacterIncrement(col_bd - 1) + "5", "" + CharacterIncrement(col_bd - 1) + "6");
                    row6_b.Value2 = "TH" + thang_bd;
                    row6_b.ColumnWidth = 6;
                    row6_b = oSheet.get_Range(CharacterIncrement(col_bd - 1) + "5", "" + CharacterIncrement(col_bd - 1) + "6");
                    row6_b.Merge();
                    col_bd += 1;
                    thang_bd++;
                }

                Range row5_TieuDe_TSGTC = oSheet.get_Range("T5", "U5");
                row5_TieuDe_TSGTC.Merge();
                row5_TieuDe_TSGTC.Value2 = "Tổng số giờ tăng ca (đối với ngày thường)";

                Range row5_TieuDe_TSGTCCN = oSheet.get_Range("V5", "W5");
                row5_TieuDe_TSGTCCN.Merge();
                row5_TieuDe_TSGTCCN.Value2 = "Tổng số giờ tăng ca (đối với ngày chủ nhật)";

                Range row5_TieuDe_TSGTCCD = oSheet.get_Range("X5", "Y5");
                row5_TieuDe_TSGTCCD.Merge();
                row5_TieuDe_TSGTCCD.Value2 = "Tổng số giờ tăng ca (đối với ca đêm)";

                Range row5_TieuDe_TSGTCNT = oSheet.get_Range("Z5", "Z6");
                row5_TieuDe_TSGTCNT.Merge();
                row5_TieuDe_TSGTCNT.Value2 = "Tổng số giờ tăng ca (ngày thường)";

                Range row5_TieuDe_TSGTCNN = oSheet.get_Range("AA5", "AA6");
                row5_TieuDe_TSGTCNN.Merge();
                row5_TieuDe_TSGTCNN.Value2 = "Tổng số giờ tăng ca (ngày nghỉ)";

                Range row5_TieuDe_TCBN1 = oSheet.get_Range("T6");
                row5_TieuDe_TCBN1.Value2 = "Tăng ca ban ngày";
                row5_TieuDe_TCBN1.ColumnWidth = 20;

                Range row5_TieuDe_TCBD1 = oSheet.get_Range("U6");
                row5_TieuDe_TCBD1.Value2 = "Tăng ca ban đêm";
                row5_TieuDe_TCBD1.ColumnWidth = 20;

                Range row5_TieuDe_TCBN2 = oSheet.get_Range("V6");
                row5_TieuDe_TCBN2.Value2 = "Tăng ca ban ngày";
                row5_TieuDe_TCBN2.ColumnWidth = 20;

                Range row5_TieuDe_TCBD2 = oSheet.get_Range("W6");
                row5_TieuDe_TCBD2.Value2 = "Tăng ca ban đêm";
                row5_TieuDe_TCBD2.ColumnWidth = 20;

                Range row5_TieuDe_TCBN3 = oSheet.get_Range("X6");
                row5_TieuDe_TCBN3.Value2 = "Số giờ ca đêm";
                row5_TieuDe_TCBN3.ColumnWidth = 20;

                Range row5_TieuDe_TCBD3 = oSheet.get_Range("Y6");
                row5_TieuDe_TCBD3.Value2 = "Tăng ca ca đêm";
                row5_TieuDe_TCBD3.ColumnWidth = 20;

                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                oSheet.Application.ActiveWindow.SplitColumn = 5;
                oSheet.Application.ActiveWindow.SplitRow = 6;
                oSheet.Application.ActiveWindow.FreezePanes = true;

                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                    {
                        rowData[rowCnt, col_bd] = row[col_bd].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 6;
                oSheet.get_Range("A7", lastColumn + rowCnt.ToString()).Value2 = rowData;
                Excel.Range formatRange;
                //rowCnt = keepRowCnt + 2;

                ////dịnh dạng
                ////Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 7);

                //string CurentColumn = string.Empty;
                //int colBD = 4;
                //int colKT = dtBCThang.Columns.Count;
                ////format

                //for (col = colBD; col < dtBCThang.Columns.Count - 3; col++)
                //{
                //    CurentColumn = CharacterIncrement(col);
                //    formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                //    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                //    formatRange.NumberFormat = "0.00;-0;;@";
                //    try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                //}

                //colKT++;
                //CurentColumn = CharacterIncrement(colKT);
                //formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                ////formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                ////Kẻ khung toàn bộ
                //int ke_khung = -1;

                //if (dr_Cu < 15)
                //{
                //    ke_khung = 14 - dr_Cu;
                //}
                formatRange = oSheet.get_Range("A7", lastColumn + (rowCnt).ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                formatRange = oSheet.get_Range("F7", "AA" + (rowCnt).ToString());
                formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                BorderAround(oSheet.get_Range("A4", lastColumn + (rowCnt).ToString()));
                // filter

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excel.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BangCongTangCaQuy_DM()
        {
            try
            {
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


                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }
                Excel.Application oXL;
                Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Excel.Application();
                oXL.Visible = true;


                //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                oWB = (Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", "S2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THÁNG NGOÀI GIỜ NĂM " + Convert.ToDateTime(datNam.EditValue).Year + " THEO QUÝ";


                Range row4_TieuDe_Format = oSheet.get_Range("A4", "S6"); //27 + 31
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);

                Range row4_TieuDe_TTNV = oSheet.get_Range("A4", "G4");
                row4_TieuDe_TTNV.Merge();
                row4_TieuDe_TTNV.Value2 = "Thông tin nhân viên (Staff information)";

                Range row4_TieuDe_TTC = oSheet.get_Range("H4", "K4");
                row4_TieuDe_TTC.Merge();
                row4_TieuDe_TTC.Value2 = "Qúy tăng ca (Overtime day)";

                Range row4_TieuDe_TTTCT = oSheet.get_Range("L4", "S4");
                row4_TieuDe_TTTCT.Merge();
                row4_TieuDe_TTTCT.Value2 = "Thông tin tăng ca tháng (Information about monthly overtime)";

                Range row5_TieuDe_STT = oSheet.get_Range("A5", "A6");
                row5_TieuDe_STT.Merge();
                row5_TieuDe_STT.Value2 = "STT";
                row5_TieuDe_STT.ColumnWidth = 5;

                Range row6_TieuDe_STT = oSheet.get_Range("A6");
                row6_TieuDe_STT.RowHeight = 54;

                Range row5_TieuDe_MSCN = oSheet.get_Range("B5", "B6");
                row5_TieuDe_MSCN.Merge();
                row5_TieuDe_MSCN.Value2 = "MSCN";
                row5_TieuDe_MSCN.ColumnWidth = 10;

                Range row5_TieuDe_HOTEN = oSheet.get_Range("C5", "C6");
                row5_TieuDe_HOTEN.Merge();
                row5_TieuDe_HOTEN.Value2 = "Họ và tên";
                row5_TieuDe_HOTEN.ColumnWidth = 15;

                Range row5_TieuDe_XN = oSheet.get_Range("D5", "D6");
                row5_TieuDe_XN.Merge();
                row5_TieuDe_XN.Value2 = "Xưởng/Phòng ban";
                row5_TieuDe_XN.ColumnWidth = 15;


                Range row5_TieuDe_TO = oSheet.get_Range("E5", "E6");
                row5_TieuDe_TO.Merge();
                row5_TieuDe_TO.Value2 = "Tổ";
                row5_TieuDe_TO.ColumnWidth = 15;

                Range row5_TieuDe_NTV = oSheet.get_Range("F5", "F6");
                row5_TieuDe_NTV.Merge();
                row5_TieuDe_NTV.Value2 = "Ngày thử việc";
                row5_TieuDe_NTV.ColumnWidth = 10;

                Range row5_TieuDe_NVL = oSheet.get_Range("G5", "G6");
                row5_TieuDe_NVL.Merge();
                row5_TieuDe_NVL.Value2 = "Ngày vào làm";
                row5_TieuDe_NVL.ColumnWidth = 10;

                int thang_bd = 1;
                int thang_kt = 4;
                int col_bd = 8;
                while (thang_bd <= thang_kt)
                {
                    Range rowtemp = oSheet.get_Range(CharacterIncrement(col_bd - 1) + "6");
                    rowtemp.Value2 = null;
                    Range row6_b = oSheet.get_Range(CharacterIncrement(col_bd - 1) + "5");
                    //Range row6_b = oSheet.get_Range(CharacterIncrement(col_bd - 1) + "5", "" + CharacterIncrement(col_bd - 1) + "6");
                    row6_b.Value2 = "Q" + thang_bd;
                    row6_b.ColumnWidth = 6;
                    row6_b = oSheet.get_Range(CharacterIncrement(col_bd - 1) + "5", "" + CharacterIncrement(col_bd - 1) + "6");
                    row6_b.Merge();
                    col_bd += 1;
                    thang_bd++;
                }

                Range row5_TieuDe_TSGTC = oSheet.get_Range("L5", "M5");
                row5_TieuDe_TSGTC.Merge();
                row5_TieuDe_TSGTC.Value2 = "Tổng số giờ tăng ca (đối với ngày thường)";

                Range row5_TieuDe_TSGTCCN = oSheet.get_Range("N5", "O5");
                row5_TieuDe_TSGTCCN.Merge();
                row5_TieuDe_TSGTCCN.Value2 = "Tổng số giờ tăng ca (đối với ngày chủ nhật)";

                Range row5_TieuDe_TSGTCCD = oSheet.get_Range("P5", "Q5");
                row5_TieuDe_TSGTCCD.Merge();
                row5_TieuDe_TSGTCCD.Value2 = "Tổng số giờ tăng ca (đối với ca đêm)";

                Range row5_TieuDe_TSGTCNT = oSheet.get_Range("R5", "R6");
                row5_TieuDe_TSGTCNT.Merge();
                row5_TieuDe_TSGTCNT.Value2 = "Tổng số giờ tăng ca (ngày thường)";

                Range row5_TieuDe_TSGTCNN = oSheet.get_Range("S5", "S6");
                row5_TieuDe_TSGTCNN.Merge();
                row5_TieuDe_TSGTCNN.Value2 = "Tổng số giờ tăng ca (ngày nghỉ)";

                Range row5_TieuDe_TCBN1 = oSheet.get_Range("L6");
                row5_TieuDe_TCBN1.Value2 = "Tăng ca ban ngày";
                row5_TieuDe_TCBN1.ColumnWidth = 20;

                Range row5_TieuDe_TCBD1 = oSheet.get_Range("M6");
                row5_TieuDe_TCBD1.Value2 = "Tăng ca ban đêm";
                row5_TieuDe_TCBD1.ColumnWidth = 20;

                Range row5_TieuDe_TCBN2 = oSheet.get_Range("N6");
                row5_TieuDe_TCBN2.Value2 = "Tăng ca ban ngày";
                row5_TieuDe_TCBN2.ColumnWidth = 20;

                Range row5_TieuDe_TCBD2 = oSheet.get_Range("O6");
                row5_TieuDe_TCBD2.Value2 = "Tăng ca ban đêm";
                row5_TieuDe_TCBD2.ColumnWidth = 20;

                Range row5_TieuDe_TCBN3 = oSheet.get_Range("P6");
                row5_TieuDe_TCBN3.Value2 = "Số giờ ca đêm";
                row5_TieuDe_TCBN3.ColumnWidth = 20;

                Range row5_TieuDe_TCBD3 = oSheet.get_Range("Q6");
                row5_TieuDe_TCBD3.Value2 = "Tăng ca ca đêm";
                row5_TieuDe_TCBD3.ColumnWidth = 20;

                oSheet.Application.ActiveWindow.SplitColumn = 5;
                oSheet.Application.ActiveWindow.SplitRow = 6;
                oSheet.Application.ActiveWindow.FreezePanes = true;

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
                rowCnt = rowCnt + 6;
                oSheet.get_Range("A7", lastColumn + rowCnt.ToString()).Value2 = rowData;
                Excel.Range formatRange;
                //rowCnt = keepRowCnt + 2;

                ////dịnh dạng
                ////Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 7);

                //string CurentColumn = string.Empty;
                //int colBD = 4;
                //int colKT = dtBCThang.Columns.Count;
                ////format

                //for (col = colBD; col < dtBCThang.Columns.Count - 3; col++)
                //{
                //    CurentColumn = CharacterIncrement(col);
                //    formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                //    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                //    formatRange.NumberFormat = "0.00;-0;;@";
                //    try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                //}

                //colKT++;
                //CurentColumn = CharacterIncrement(colKT);
                //formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                ////formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                ////Kẻ khung toàn bộ
                //int ke_khung = -1;

                //if (dr_Cu < 15)
                //{
                //    ke_khung = 14 - dr_Cu;
                //}
                formatRange = oSheet.get_Range("F7", "S" + (rowCnt).ToString());
                formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                formatRange = oSheet.get_Range("A7", lastColumn + (rowCnt).ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                BorderAround(oSheet.get_Range("A4", lastColumn + (rowCnt).ToString()));
                // filter

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excel.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BangCongTangCaTuan_DM()
        {
            try
            {
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
                Excel.Application oXL;
                Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Excel.Application();
                oXL.Visible = true;


                //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                oWB = (Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", "BP2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THÁNG NGOÀI GIỜ NĂM " + Convert.ToDateTime(datNam.EditValue).Year + " THEO TUẦN";


                Range row4_TieuDe_Format = oSheet.get_Range("A4", "BP6"); //27 + 31
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);

                Range row4_TieuDe_TTNV = oSheet.get_Range("A4", "G4");
                row4_TieuDe_TTNV.Merge();
                row4_TieuDe_TTNV.Value2 = "Thông tin nhân viên (Staff information)";

                Range row4_TieuDe_TTC = oSheet.get_Range("H4", "BH4");
                row4_TieuDe_TTC.Merge();
                row4_TieuDe_TTC.Value2 = "Tuần tăng ca (Overtime day)";

                Range row4_TieuDe_TTTCT = oSheet.get_Range("BI4", "BP4");
                row4_TieuDe_TTTCT.Merge();
                row4_TieuDe_TTTCT.Value2 = "Thông tin tăng ca tháng (Information about monthly overtime)";

                Range row5_TieuDe_STT = oSheet.get_Range("A5", "A6");
                row5_TieuDe_STT.Merge();
                row5_TieuDe_STT.Value2 = "STT";
                row5_TieuDe_STT.ColumnWidth = 5;

                Range row6_TieuDe_STT = oSheet.get_Range("A6");
                row6_TieuDe_STT.RowHeight = 54;

                Range row5_TieuDe_MSCN = oSheet.get_Range("B5", "B6");
                row5_TieuDe_MSCN.Merge();
                row5_TieuDe_MSCN.Value2 = "MSCN";
                row5_TieuDe_MSCN.ColumnWidth = 10;

                Range row5_TieuDe_HOTEN = oSheet.get_Range("C5", "C6");
                row5_TieuDe_HOTEN.Merge();
                row5_TieuDe_HOTEN.Value2 = "Họ và tên";
                row5_TieuDe_HOTEN.ColumnWidth = 15;

                Range row5_TieuDe_XN = oSheet.get_Range("D5", "D6");
                row5_TieuDe_XN.Merge();
                row5_TieuDe_XN.Value2 = "Xưởng/Phòng ban";
                row5_TieuDe_XN.ColumnWidth = 15;


                Range row5_TieuDe_TO = oSheet.get_Range("E5", "E6");
                row5_TieuDe_TO.Merge();
                row5_TieuDe_TO.Value2 = "Tổ";
                row5_TieuDe_TO.ColumnWidth = 15;

                Range row5_TieuDe_NTV = oSheet.get_Range("F5", "F6");
                row5_TieuDe_NTV.Merge();
                row5_TieuDe_NTV.Value2 = "Ngày thử việc";
                row5_TieuDe_NTV.ColumnWidth = 10;

                Range row5_TieuDe_NVL = oSheet.get_Range("G5", "G6");
                row5_TieuDe_NVL.Merge();
                row5_TieuDe_NVL.Value2 = "Ngày vào làm";
                row5_TieuDe_NVL.ColumnWidth = 10;

                int thang_bd = 1;
                int thang_kt = 53;
                int col_bd = 8;
                while (thang_bd <= thang_kt)
                {
                    Range rowtemp = oSheet.get_Range(CharacterIncrement(col_bd - 1) + "6");
                    rowtemp.Value2 = null;
                    Range row6_b = oSheet.get_Range(CharacterIncrement(col_bd - 1) + "5");
                    //Range row6_b = oSheet.get_Range(CharacterIncrement(col_bd - 1) + "5", "" + CharacterIncrement(col_bd - 1) + "6");
                    row6_b.Value2 = thang_bd;
                    row6_b.ColumnWidth = 4;
                    row6_b = oSheet.get_Range(CharacterIncrement(col_bd - 1) + "5", "" + CharacterIncrement(col_bd - 1) + "6");
                    row6_b.Merge();
                    col_bd += 1;
                    thang_bd++;
                }

                Range row5_TieuDe_TSGTC = oSheet.get_Range("BI5", "BJ5");
                row5_TieuDe_TSGTC.Merge();
                row5_TieuDe_TSGTC.Value2 = "Tổng số giờ tăng ca (đối với ngày thường)";

                Range row5_TieuDe_TSGTCCN = oSheet.get_Range("BK5", "BL5");
                row5_TieuDe_TSGTCCN.Merge();
                row5_TieuDe_TSGTCCN.Value2 = "Tổng số giờ tăng ca (đối với ngày chủ nhật)";

                Range row5_TieuDe_TSGTCCD = oSheet.get_Range("BM5", "BN5");
                row5_TieuDe_TSGTCCD.Merge();
                row5_TieuDe_TSGTCCD.Value2 = "Tổng số giờ tăng ca (đối với ca đêm)";

                Range row5_TieuDe_TSGTCNT = oSheet.get_Range("BO5", "BO6");
                row5_TieuDe_TSGTCNT.Merge();
                row5_TieuDe_TSGTCNT.Value2 = "Tổng số giờ tăng ca (ngày thường)";

                Range row5_TieuDe_TSGTCNN = oSheet.get_Range("BP5", "BP6");
                row5_TieuDe_TSGTCNN.Merge();
                row5_TieuDe_TSGTCNN.Value2 = "Tổng số giờ tăng ca (ngày nghỉ)";

                Range row5_TieuDe_TCBN1 = oSheet.get_Range("BI6");
                row5_TieuDe_TCBN1.Value2 = "Tăng ca ban ngày";
                row5_TieuDe_TCBN1.ColumnWidth = 20;

                Range row5_TieuDe_TCBD1 = oSheet.get_Range("BJ6");
                row5_TieuDe_TCBD1.Value2 = "Tăng ca ban đêm";
                row5_TieuDe_TCBD1.ColumnWidth = 20;

                Range row5_TieuDe_TCBN2 = oSheet.get_Range("BK6");
                row5_TieuDe_TCBN2.Value2 = "Tăng ca ban ngày";
                row5_TieuDe_TCBN2.ColumnWidth = 20;

                Range row5_TieuDe_TCBD2 = oSheet.get_Range("BL6");
                row5_TieuDe_TCBD2.Value2 = "Tăng ca ban đêm";
                row5_TieuDe_TCBD2.ColumnWidth = 20;

                Range row5_TieuDe_TCBN3 = oSheet.get_Range("BM6");
                row5_TieuDe_TCBN3.Value2 = "Số giờ ca đêm";
                row5_TieuDe_TCBN3.ColumnWidth = 20;

                Range row5_TieuDe_TCBD3 = oSheet.get_Range("BN6");
                row5_TieuDe_TCBD3.Value2 = "Tăng ca ca đêm";
                row5_TieuDe_TCBD3.ColumnWidth = 20;

                oSheet.Application.ActiveWindow.SplitColumn = 5;
                oSheet.Application.ActiveWindow.SplitRow = 6;
                oSheet.Application.ActiveWindow.FreezePanes = true;

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
                rowCnt = rowCnt + 6;
                oSheet.get_Range("A7", lastColumn + rowCnt.ToString()).Value2 = rowData;
                Excel.Range formatRange;

                //rowCnt = keepRowCnt + 2;

                ////dịnh dạng
                ////Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 7);

                //string CurentColumn = string.Empty;
                //int colBD = 4;
                //int colKT = dtBCThang.Columns.Count;
                ////format

                //for (col = colBD; col < dtBCThang.Columns.Count - 3; col++)
                //{
                //    CurentColumn = CharacterIncrement(col);
                //    formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                //    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                //    formatRange.NumberFormat = "0.00;-0;;@";
                //    try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                //}

                //colKT++;
                //CurentColumn = CharacterIncrement(colKT);
                //formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                ////formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                ////Kẻ khung toàn bộ
                //int ke_khung = -1;

                //if (dr_Cu < 15)
                //{
                //    ke_khung = 14 - dr_Cu;
                //}
                formatRange = oSheet.get_Range("A7", lastColumn + (rowCnt).ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                formatRange = oSheet.get_Range("F7", "BP" + (rowCnt).ToString());
                formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                BorderAround(oSheet.get_Range("A4", lastColumn + (rowCnt).ToString()));
                // filter

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excel.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
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
        private void DanhGiaTinhTrangThuViec_DM()
        {
            int id_cv = 1;
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCDanhGiaTTThuViec", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = "2022-08-01";
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = "2022-08-01";
                cmd.Parameters.Add("@ID_CV", SqlDbType.Int).Value = 206;

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
                Excel.Application oXL;
                Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Excel.Application();
                oXL.Visible = true;


                //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                oWB = (Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 10;
                int fontSizeNoiDung = 10;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", "" + lastColumn + "2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 16;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 20;
                row2_TieuDe_BaoCao.Value2 = "BÁO CÁO ĐÁNH GIÁ TÌNH TRẠNG THỬ VIỆC";

                Range rowTuNgay = oSheet.get_Range("E3", "" + lastColumn + "3");
                rowTuNgay.Merge();
                rowTuNgay.Font.Size = 12;
                rowTuNgay.Font.Name = fontName;
                rowTuNgay.Font.FontStyle = "Bold";
                rowTuNgay.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowTuNgay.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                //rowTuNgay.Value = "Từ ngày "++""


                Range row4_TieuDe_Format = oSheet.get_Range("A5", "" + lastColumn + "5"); //27 + 31
                row4_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);
               

                Range row5_TieuDe_STT = oSheet.get_Range("A5");
                row5_TieuDe_STT.Merge();
                row5_TieuDe_STT.Value2 = "STT";
                row5_TieuDe_STT.ColumnWidth = 5;


                Range row5_TieuDe_MSCN = oSheet.get_Range("B5");
                row5_TieuDe_MSCN.Value2 = "Mã số thẻ";
                row5_TieuDe_MSCN.ColumnWidth = 13;

                Range row5_TieuDe_HOTEN = oSheet.get_Range("C5");
                row5_TieuDe_HOTEN.Value2 = "Họ và tên";
                row5_TieuDe_HOTEN.ColumnWidth = 25;

                Range row5_TieuDe_XN = oSheet.get_Range("D5");
                row5_TieuDe_XN.Value2 = "Xưởng/Phòng ban";
                row5_TieuDe_XN.ColumnWidth = 20;


                Range row5_TieuDe_TO = oSheet.get_Range("E5");
                row5_TieuDe_TO.Value2 = "Tổ";
                row5_TieuDe_TO.ColumnWidth = 15;

                Range row5_TieuDe_NTV = oSheet.get_Range("F5");
                row5_TieuDe_NTV.Value2 = "Vị trí công việc";
                row5_TieuDe_NTV.ColumnWidth = 20;

                Range row5_TieuDe_NVL = oSheet.get_Range("G5");
                row5_TieuDe_NVL.Value2 = "Loại hợp đồng";
                row5_TieuDe_NVL.ColumnWidth = 15;

                Range row5_TieuDe_NBD = oSheet.get_Range("H5");
                row5_TieuDe_NBD.Value2 = "Ngày bắt đầu hiệu lực";
                row5_TieuDe_NBD.ColumnWidth = 13;

                Range row5_TieuDe_NHHL = oSheet.get_Range("I5");
                row5_TieuDe_NHHL.Value2 = "Ngày hết hiệu lực";
                row5_TieuDe_NHHL.ColumnWidth = 13;

                Range row5_TieuDe_NDG = oSheet.get_Range("J5");
                row5_TieuDe_NDG.Value2 = "Người đánh giá";
                row5_TieuDe_NDG.ColumnWidth = 25;

                Range row5_TieuDe_NDGG = oSheet.get_Range("K5");
                row5_TieuDe_NDGG.Value2 = "Ngày đánh giá";
                row5_TieuDe_NDGG.ColumnWidth = 13;

                Range row5_TieuDe_KHD = oSheet.get_Range("L5");
                row5_TieuDe_KHD.Value2 = "Ký hợp đồng";
                row5_TieuDe_KHD.ColumnWidth = 10;

                Range row5_TieuDe_M = oSheet.get_Range("M5");
                row5_TieuDe_M.Value2 = "Kết thúc hợp đồng";
                row5_TieuDe_M.ColumnWidth = 10;

                Range row5_TieuDe_N = oSheet.get_Range("N5");
                row5_TieuDe_N.Value2 = "Đã ký";
                row5_TieuDe_N.ColumnWidth = 10;

                if(Convert.ToInt32(id_cv) == -1)
                {
                    Range row5_TieuDe_KTCV = oSheet.get_Range("O5");
                    row5_TieuDe_KTCV.Value2 = "Kiến thức công việc";
                    row5_TieuDe_KTCV.ColumnWidth = 13;

                    Range row5_TieuDe_HQCV = oSheet.get_Range("P5");
                    row5_TieuDe_HQCV.Value2 = "Hiệu quả công việc";
                    row5_TieuDe_HQCV.ColumnWidth = 13;

                    Range row5_TieuDe_TDCV = oSheet.get_Range("Q5");
                    row5_TieuDe_TDCV.Value2 = "Thái độ công việc";
                    row5_TieuDe_TDCV.ColumnWidth = 13;

                    Range row5_TieuDe_TTNQ = oSheet.get_Range("R5");
                    row5_TieuDe_TTNQ.Value2 = "Thái độ công việc";
                    row5_TieuDe_TTNQ.ColumnWidth = 13;
                }

                //oSheet.Application.ActiveWindow.SplitColumn = 5;
                //oSheet.Application.ActiveWindow.SplitRow = 6;
                //oSheet.Application.ActiveWindow.FreezePanes = true;

                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (int col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                    {
                        rowData[rowCnt, col_bd] = row[col_bd].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 5;
                oSheet.get_Range("A6", lastColumn + rowCnt.ToString()).Value2 = rowData;
                Excel.Range formatRange;
                //rowCnt = keepRowCnt + 2;

                ////dịnh dạng
                ////Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 7);

                //string CurentColumn = string.Empty;
                //int colBD = 4;
                //int colKT = dtBCThang.Columns.Count;
                ////format

                //for (col = colBD; col < dtBCThang.Columns.Count - 3; col++)
                //{
                //    CurentColumn = CharacterIncrement(col);
                //    formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                //    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                //    formatRange.NumberFormat = "0.00;-0;;@";
                //    try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                //}

                //colKT++;
                //CurentColumn = CharacterIncrement(colKT);
                //formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                ////formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                ////Kẻ khung toàn bộ
                //int ke_khung = -1;

                //if (dr_Cu < 15)
                //{
                //    ke_khung = 14 - dr_Cu;
                //}
                //formatRange = oSheet.get_Range("F7", "S" + (rowCnt).ToString());
                //formatRange.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //formatRange.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                formatRange = oSheet.get_Range("A6", lastColumn + (rowCnt).ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                BorderAround(oSheet.get_Range("A5", lastColumn + (rowCnt).ToString()));
                // filter

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excel.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
