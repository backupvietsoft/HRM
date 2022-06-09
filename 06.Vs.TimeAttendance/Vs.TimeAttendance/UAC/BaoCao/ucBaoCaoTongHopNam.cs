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

namespace Vs.TimeAttendance
{
    public partial class ucBaoCaoTongHopNam : DevExpress.XtraEditors.XtraUserControl
    {
        private string saveExcelFile;
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
                                        default:
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


                            case 3:
                                {

                                }
                                break;
                            case 4:
                                {

                                    break;
                                }
                            case 5:
                                {

                                    break;
                                }

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
            //switch (rdo_ChonBaoCao.SelectedIndex)
            //{
            //    case 0:
            //        {
            //            rdo_DiTreVeSom.Visible = false;

            //        }
            //        break;
            //    case 1:
            //        {
            //            rdo_DiTreVeSom.Visible = false;
            //        }
            //        break;
            //    case 2:
            //        {
            //            rdo_DiTreVeSom.Visible = true;
            //        }
            //        break;
            //    case 3:
            //        {
            //            rdo_DiTreVeSom.Visible = false;
            //        }
            //        break;
            //    case 4:
            //        {
            //            rdo_DiTreVeSom.Visible = false;
            //        }
            //        break;
            //    case 5:
            //        {
            //            rdo_DiTreVeSom.Visible = false;
            //        }
            //        break;
            //    case 6:
            //        {
            //            rdo_DiTreVeSom.Visible = false;
            //        }
            //        break;
            //    default:
            //        break;
            //}
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
                string sTieuDe = "BẢNG CHẤM CÔNG NĂM";
                frm.rpt = new rptBangTongHopCongNam(sTieuDe, Convert.ToDateTime(datTThang.EditValue), Convert.ToDateTime(datDThang.EditValue));
                try
                {
                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongNam_MT", conn);

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
        private void BangTHCongTangCaNam_MT()
        {
            try
            {
                frmViewReport frm = new frmViewReport();
                DataTable dt = new DataTable();
                System.Data.SqlClient.SqlConnection conn;
                dt = new DataTable();
                string sTieuDe = "BẢNG CHẤM CÔNG";
                frm.rpt = new rptBangTongHopTangCaNam(sTieuDe, Convert.ToDateTime(datTThang.EditValue), Convert.ToDateTime(datDThang.EditValue));
                try
                {
                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongTangCaNam_MT", conn);

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
                string sTieuDe = (Commons.Modules.TypeLanguage == 0 ? "TỔNG NGÀY NGHỈ TRONG NĂM TỪ " : "TOTAL NUMBER OF HOLIDAYS IN THE YEAR ") + Convert.ToDateTime(datTThang.EditValue).ToString("MM/yyyy") + (Commons.Modules.TypeLanguage == 0 ? " ĐẾN " : " TO ") + Convert.ToDateTime(datDThang.EditValue).ToString("MM/yyyy") + "" ;
                frm.rpt = new rptBangTongHopCongVangNam(sTieuDe);
                try
                {
                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongVangNam_MT", conn);

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
    }
}
