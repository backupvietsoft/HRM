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
using Excell = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Collections.Generic;
using System.Reflection;

namespace Vs.TimeAttendance
{
    public partial class ucBaoCaoTongHopThang : DevExpress.XtraEditors.XtraUserControl
    {
        private string SaveExcelFile;
        string sKyHieuDV = "";
        public ucBaoCaoTongHopThang()
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

        #region even
        private void ucBaoCaoTongHopThang_Load(object sender, EventArgs e)
        {
            try
            {

                Commons.Modules.sLoad = "0Load";
                LoadCboDonVi();
                LoadCboXiNghiep();
                LoadCboTo();
                sKyHieuDV = Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString();
                if (sKyHieuDV == "SB")
                {
                    rdo_ChonBaoCao.Properties.Items.RemoveAt(4);
                }
                else if (sKyHieuDV == "DM")
                {

                    rdo_ChonBaoCao.Properties.Items.RemoveAt(5);
                    rdo_ChonBaoCao.Properties.Items.RemoveAt(4);
                    rdo_ChonBaoCao.Properties.Items.RemoveAt(1);
                }
                else
                {
                    rdo_ChonBaoCao.Properties.Items.RemoveAt(5);
                }

                LK_Thang.EditValue = DateTime.Today;
                Commons.OSystems.SetDateEditFormat(lk_TuNgay);
                Commons.OSystems.SetDateEditFormat(lk_DenNgay);
                chkThayDoiCa.Checked = true;
                //LoadTinhTrangHopDong();
                Commons.Modules.sLoad = "";
                LoadNgay();

                //lk_DenNgay.EditValue = DateTime.Today;
                //DateTime dtTN = DateTime.Today;
                //DateTime dtDN = DateTime.Today;
                ////dTuNgay.EditValue = dtTN.AddDays((-dtTN.Day) + 1);
                //dtDN = dtDN.AddMonths(1);
                //dtDN = dtDN.AddDays(-(dtDN.Day));
                //LK_NgayXemBaoCao.EditValue = dtDN;
                NgayIn.EditValue = DateTime.Today;
            }
            catch { }

        }
        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "Print":
                    {

                        switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
                        {
                            case "rdo_BangChamCongThang":
                                {
                                    switch (sKyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                BangChamCongThang_MT();
                                                break;
                                            }
                                        case "SB":
                                            {
                                                BangChamCongThang_SB2();
                                                break;
                                            }
                                        case "DM":
                                            {
                                                BaoCaoTongHopThang_DM();
                                                break;
                                            }
                                        default:
                                            //BangChamCongThangGioCong_HN();
                                            //BangChamCongThang_HN();
                                            BangChamCongThang();
                                            break;
                                    }
                                }
                                break;
                            case "rdo_BangChamCongTangCaThang":
                                {
                                    switch (sKyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                BangChamCongTangCaThang_MT();
                                                break;
                                            }
                                        case "SB":
                                            {
                                                BangChamCongTangCaThang_SB();
                                                break;
                                            }
                                        default:
                                            BangChamCongTangCaThang();
                                            break;
                                    }
                                }
                                break;

                            case "rdo_BangTongHopDiTreVeSomThang":
                                {
                                    switch (sKyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                BangTongHopDiTreVeSomThang();
                                                break;
                                            }

                                        default:
                                            BangTongHopDiTreVeSomThang();
                                            break;
                                    }
                                    break;
                                }

                            case "rdo_BangTongHopCongThang":
                                {
                                    switch (sKyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                BangTongHopCongThang();
                                                break;
                                            }
                                        case "SB":
                                            {
                                                BangTongHopCongThang_SB();
                                                break;
                                            }
                                        default:
                                            BangTongHopCongThang();
                                            break;
                                    }
                                }
                                break;
                            case "rdo_BangChenhLechTangCaThang":
                                {
                                    switch (sKyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                BangChenhLechTangCaThang_MT();
                                                break;
                                            }
                                        default:
                                            BangChenhLechTangCaThang_MT();
                                            break;
                                    }
                                    break;

                                }
                            case "rdo_DanhSachThang":
                                {
                                    switch (sKyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                DanhSachThang_SB();
                                                break;
                                            }
                                        case "SB":
                                            {
                                                DanhSachThang_SB();
                                                break;
                                            }
                                        default:
                                            DanhSachThang_SB();
                                            break;
                                    }
                                    break;
                                }

                            case "rdo_BaoCaoNghiBoViecThang":
                                {
                                    switch (sKyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                BaoCaoNghiBoViecThang();
                                                break;
                                            }
                                        default:
                                            BaoCaoNghiBoViecThang();
                                            break;
                                    }
                                    break;
                                }
                            case "rdo_DanhSachChuyenCongTac":
                                {
                                    DanhSachChuyenCongTac();
                                    break;
                                }
                            case "rdo_ThongTinNhomCCThang":
                                {
                                    frmViewReport frm = new frmViewReport();
                                    DataTable dt;
                                    System.Data.SqlClient.SqlConnection conn;
                                    dt = new DataTable();
                                    frm.rpt = new rptThongTinNhomCaThang_DM(Convert.ToDateTime(lk_TuNgay.EditValue), Convert.ToDateTime(lk_DenNgay.EditValue), Convert.ToDateTime(NgayIn.EditValue));
                                    try
                                    {
                                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                        conn.Open();

                                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBCNhomCaTheoThang_DM"), conn);

                                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                        cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                        cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                        cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                        cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                                        cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                                        cmd.Parameters.Add("@KiemTra", SqlDbType.Bit).Value = chkThayDoiCa.Checked;
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
                                    break;
                                }

                            case "rdo_BCSoLanXacNhanCongThang":
                                {
                                    //frmViewReport frm = new frmViewReport();
                                    //DataTable dt;
                                    //System.Data.SqlClient.SqlConnection conn;
                                    //dt = new DataTable();
                                    //string sTieuDe = Commons.Modules.ObjLanguages.GetLanguage("rptBCSLXacNhanCongThang_DM", "lblTIEU_DE") + " " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy");
                                    //frm.rpt = new rptBCSLXacNhanCongThang_DM(Convert.ToDateTime(lk_TuNgay.EditValue), Convert.ToDateTime(lk_DenNgay.EditValue), Convert.ToDateTime(NgayIn.EditValue), sTieuDe);
                                    //try
                                    //{
                                    //    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                    //    conn.Open();

                                    //    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBaoCaoSLXacNhanCong"), conn);
                                    //    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                    //    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                    //    cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                                    //    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                                    //    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                                    //    cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                                    //    cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                                    //    cmd.CommandType = CommandType.StoredProcedure;
                                    //    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                    //    DataSet ds = new DataSet();
                                    //    adp.Fill(ds);
                                    //    dt = new DataTable();
                                    //    dt = ds.Tables[0].Copy();
                                    //    dt.TableName = "DATA";
                                    //    frm.AddDataSource(dt);
                                    //    frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                                    //}
                                    //catch
                                    //{ }
                                    //frm.ShowDialog();

                                    BaoXacNhanCongThang_DM();
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    break;
            }
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
            switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
            {
                case "rdo_BangTongHopDiTreVeSomThang":
                    {
                        rdo_DiTreVeSom.Visible = true;
                        chkThayDoiCa.Enabled = false;
                        lblThayDoiCa.Enabled = false;
                    }
                    break;
                case "rdo_ThongTinNhomCCThang":
                    {
                        rdo_DiTreVeSom.Visible = false;
                        chkThayDoiCa.Enabled = true;
                        lblThayDoiCa.Enabled = true;
                        break;
                    }
                default:
                    {
                        rdo_DiTreVeSom.Visible = false;
                        chkThayDoiCa.Enabled = false;
                        lblThayDoiCa.Enabled = false;
                    }
                    break;
            }
        }

        private void grvThang_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                LK_Thang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch { }
            LK_Thang.ClosePopup();

        }

        private void rdo_DiTreVeSom_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
            {
                case "rdo_BangChamCongThang":
                    {
                        rdo_DiTreVeSom.Visible = true;

                    }
                    break;
                case "rdo_BangChamCongTangCaThang":
                    {
                        rdo_DiTreVeSom.Visible = true;
                    }
                    break;
                case "rdo_BangTongHopDiTreVeSomThang":
                    {
                        rdo_DiTreVeSom.Visible = true;
                    }
                    break;
                default:
                    break;
            }
        }

        private void calThang_DateTimeCommit_1(object sender, EventArgs e)
        {
            try
            {
                LK_Thang.Text = calThang.DateTime.ToString("MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + LK_Thang.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                }
                else { }
            }
            catch (Exception ex)
            {
                LK_Thang.Text = calThang.DateTime.ToString("MM/yyyy");
            }
            LK_Thang.ClosePopup();
        }
        private void LK_Thang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            DateTime tungay = Convert.ToDateTime(LK_Thang.EditValue);
            DateTime denngay = Convert.ToDateTime(LK_Thang.EditValue).AddMonths(+1);
            lk_TuNgay.EditValue = Convert.ToDateTime("01/" + tungay.Month + "/" + tungay.Year);
            lk_DenNgay.EditValue = Convert.ToDateTime("01/" + denngay.Month + "/" + tungay.Year).AddDays(-1);
        }
        private void LK_Thang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (e.Button.Tag != null && e.Button.Tag.Equals("muiten"))
            {
                LoadNgay();
            }
        }
        #endregion

        #region function

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
        private void LoadNgay()
        {
            try
            {
                string sNam = Convert.ToDateTime(LK_Thang.EditValue).Year.ToString();
                string sThang = "";
                DataTable dtthang = new DataTable();
                dtthang.Columns.Add("M", typeof(string));
                dtthang.Columns.Add("Y", typeof(string));
                dtthang.Columns.Add("THANG", typeof(string));

                for (int col = 1; col <= 12; col++)
                {
                    sThang = "0" + col.ToString();
                    sThang = sThang.Substring(sThang.Length - 2, 2);
                    dtthang.Rows.Add(sThang, sNam, sThang + "/" + sNam);
                }
                dtthang.PrimaryKey = new DataColumn[] { dtthang.Columns["M"] };


                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                //DataTable dtthang = new DataTable();
                //string sSql = "SELECT DISTINCT SUBSTRING(CONVERT(VARCHAR(10),TU_NGAY,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),TU_NGAY,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),TU_NGAY,103),7) AS THANG FROM dbo.CHAM_CONG ORDER BY Y DESC , M DESC";
                //dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                int index = dtthang.Rows.IndexOf(dtthang.Rows.Find(grvThang.GetFocusedRowCellValue("M")));
                if (grdThang.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                    grvThang.Columns["M"].Visible = false;
                    grvThang.Columns["Y"].Visible = false;
                }
                else
                {
                    grdThang.DataSource = dtthang;
                }
                if (index == -1)
                {
                    DataTable dt = new DataTable();
                    dt = (DataTable)grdThang.DataSource;
                    LK_Thang.EditValue = dt.Rows[DateTime.Now.Month - 1]["THANG"];
                }
                else
                {
                    grvThang.FocusedRowHandle = grvThang.GetRowHandle(index);
                    LK_Thang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
                }

            }
            catch (Exception ex)
            {
            }
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
        #endregion
        #region functionInTheoDonVi
        private void BangChamCongThang()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangCongThang"), conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
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
                Excell.Application oXL;
                Excell._Workbook oWB;
                Excell._Worksheet oSheet;

                oXL = new Excell.Application();
                oXL.Visible = false;

                oWB = (Excell._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excell._Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay) + 1;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);
                string lastColumNgay = string.Empty;
                lastColumNgay = CharacterIncrement(iSoNgay + 7);
                string firstColumTT = string.Empty;
                firstColumTT = CharacterIncrement(iSoNgay + 8);

                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", lastColumn + "2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THÁNG " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy");

                Range row5_TieuDe_Format = oSheet.get_Range("A4", lastColumn + "7"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Interior.Color = Color.Yellow;

                Range row5_TieuDe = oSheet.get_Range("A4", "H4");
                row5_TieuDe.Merge();
                row5_TieuDe.Value2 = "Thông tin nhân viên (Staff information)";

                Range row5_TieuDe2 = oSheet.get_Range("I4", lastColumNgay + "4");
                row5_TieuDe2.Merge();
                row5_TieuDe2.Value2 = "Ngày làm việc (Date working)";

                Range row5_TieuDe3 = oSheet.get_Range(firstColumTT + "4", lastColumn + "4");
                row5_TieuDe3.Merge();
                row5_TieuDe3.Value2 = "Thông tin chấm công tháng (Monthly attendance information)";

                Range row5_TieuDe_Stt = oSheet.get_Range("A5", "A6");
                row5_TieuDe_Stt.Merge();
                row5_TieuDe_Stt.Value2 = "Stt";
                row5_TieuDe_Stt.ColumnWidth = 6;

                Range row5_TieuDe_Stt_A = oSheet.get_Range("A7");
                row5_TieuDe_Stt_A.Value2 = "No.";

                Range row5_TieuDe_MaSo = oSheet.get_Range("B5", "B6");
                row5_TieuDe_MaSo.Merge();
                row5_TieuDe_MaSo.Value2 = "MSCN";
                row5_TieuDe_MaSo.ColumnWidth = 15;

                Range row5_TieuDe_MS_A = oSheet.get_Range("B7");
                row5_TieuDe_MS_A.Value2 = "Employee code";

                Range row5_TieuDe_HoTen = oSheet.get_Range("C5", "C6");
                row5_TieuDe_HoTen.Merge();
                row5_TieuDe_HoTen.Value2 = "Họ và tên";
                row5_TieuDe_HoTen.ColumnWidth = 30;

                Range row5_TieuDe_HO_TEN_A = oSheet.get_Range("C7");
                row5_TieuDe_HO_TEN_A.Value2 = "Full name";

                Range row5_TieuDe_ChucDanh = oSheet.get_Range("D5", "D6");
                row5_TieuDe_ChucDanh.Merge();
                row5_TieuDe_ChucDanh.Value2 = "Chức vụ";
                row5_TieuDe_ChucDanh.ColumnWidth = 20;

                Range row5_TieuDe_CV_A = oSheet.get_Range("D7");
                row5_TieuDe_CV_A.Value2 = "Position";

                Range row5_TieuDe_BoPhan = oSheet.get_Range("E5", "E6");
                row5_TieuDe_BoPhan.Merge();
                row5_TieuDe_BoPhan.Value2 = "Xưởng/Phòng ban";
                row5_TieuDe_BoPhan.ColumnWidth = 20;

                Range row5_TieuDe_XN_A = oSheet.get_Range("E7");
                row5_TieuDe_XN_A.Value2 = "Workshop/Department";

                Range row5_TieuDe_To = oSheet.get_Range("F5", "F6");
                row5_TieuDe_To.Merge();
                row5_TieuDe_To.Value2 = "Chuyền/Phòng";
                row5_TieuDe_To.ColumnWidth = 20;

                Range row5_TieuDe_TO_A = oSheet.get_Range("F7");
                row5_TieuDe_TO_A.Value2 = "Team";

                Range row5_TieuDe_NgayTV = oSheet.get_Range("G5", "G6");
                row5_TieuDe_NgayTV.Merge();
                row5_TieuDe_NgayTV.Value2 = "Ngày thử việc";
                row5_TieuDe_NgayTV.ColumnWidth = 12;

                Range row5_TieuDe_NTV_A = oSheet.get_Range("G7");
                row5_TieuDe_NTV_A.Value2 = "Start probation date";

                Range row5_TieuDe_NgayVL = oSheet.get_Range("H5", "H6");
                row5_TieuDe_NgayVL.Merge();
                row5_TieuDe_NgayVL.Value2 = "Ngày vào làm";
                row5_TieuDe_NgayVL.ColumnWidth = 12;

                Range row5_TieuDe_NVL_A = oSheet.get_Range("H7");
                row5_TieuDe_NVL_A.Value2 = "Start working date";

                int col = 9;
                while (iTNgay <= iDNgay)
                {
                    oSheet.Cells[5, col] = iTNgay;
                    oSheet.Cells[7, col] = iTNgay;
                    oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();

                    col++;
                    iTNgay++;
                }

                oSheet.Cells[5, col] = "Tổng số ngày làm việc";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 1]].Merge();
                oSheet.Cells[6, col] = "Ngày thường";
                oSheet.Cells[7, col] = "Weekday";
                oSheet.Cells[6, col + 1] = "Ngày nghỉ";
                oSheet.Cells[7, col + 1] = "Weekend";

                col = col + 2;
                oSheet.Cells[5, col] = "Số ngày nghỉ phép";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Annual leave";

                col = col + 1;
                oSheet.Cells[5, col] = "Số ngày nghỉ bù";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Rostered leave";

                col = col + 1;
                oSheet.Cells[5, col] = "Số ngày nghỉ lễ";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "S.I. paid leave";

                col = col + 1;
                oSheet.Cells[5, col] = "Số ngày nghỉ hưởng BHXH";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "S.I. paid leave";


                col = col + 1;
                oSheet.Cells[5, col] = "Số ngày nghỉ được hưởng lương";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Paid leave";


                col = col + 1;
                oSheet.Cells[5, col] = "Số ngày nghỉ không lương, nghỉ tự do";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Unpaid leave";


                col = col + 1;
                oSheet.Cells[5, col] = "Số ngày nghỉ việc";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Number of days out job";

                col = col + 1;
                oSheet.Cells[5, col] = "Tổng số công đi muộn, về sớm, ra ngoài việc riêng";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Short leave";

                col = col + 1;
                oSheet.Cells[5, col] = "TI (Số lần đi muộn)";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "# of late arrival";


                col = col + 1;
                oSheet.Cells[5, col] = "Số giờ đi muộn";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Hours of late arrival";

                col = col + 1;
                oSheet.Cells[5, col] = "EO (Số lần về sớm)";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "# of early leave";


                col = col + 1;
                oSheet.Cells[5, col] = "Số giờ về sớm";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Hours of early leave";

                col = col + 1;
                oSheet.Cells[5, col] = "Số lần ra ngoài việc riêng";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "# of special leave";


                col = col + 1;
                oSheet.Cells[5, col] = "Số giờ ra ngoài";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Hours of special leave";

                col = col + 1;
                oSheet.Cells[5, col] = "Tổng ngày công";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Total work days";


                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCThang.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }

                    rowCnt++;
                }
                rowCnt = rowCnt + 6;
                oSheet.get_Range("A8", lastColumn + rowCnt.ToString()).Value2 = rowData;
                Excell.Range formatRange;
                formatRange = oSheet.get_Range("G8", "G" + rowCnt.ToString());
                formatRange.NumberFormat = "dd/MM/yyyy";
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);
                formatRange = oSheet.get_Range("H8", "H" + rowCnt.ToString());
                formatRange.NumberFormat = "dd/MM/yyyy";
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);
                formatRange = oSheet.get_Range("I8", lastColumNgay + rowCnt.ToString());
                formatRange.NumberFormat = "@";
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;

                string CurentColumn = string.Empty;
                int colBD = iSoNgay + 8;
                int colKT = colBD + 9;

                for (col = colBD; col <= colKT; col++)
                {
                    CurentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                    formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);
                }

                //so lan di muon
                colKT++;
                CurentColumn = CharacterIncrement(colKT);
                formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);
                //so gio di muon
                colKT++;
                CurentColumn = CharacterIncrement(colKT);
                formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);
                //so lan ve som
                colKT++;
                CurentColumn = CharacterIncrement(colKT);
                formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);
                //so gio ve som
                colKT++;
                CurentColumn = CharacterIncrement(colKT);
                formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);
                //so lan ra ngoai
                colKT++;
                CurentColumn = CharacterIncrement(colKT);
                formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);
                //so gio ra ngoai
                colKT++;
                CurentColumn = CharacterIncrement(colKT);
                formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);
                //tong ngay cong
                colKT++;
                CurentColumn = CharacterIncrement(colKT);
                formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);

                ////Kẻ khung toàn bộ
                formatRange = oSheet.get_Range("A8", lastColumn + rowCnt.ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                BorderAround(oSheet.get_Range("A4", lastColumn + rowCnt.ToString()));

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excell.XlSaveAsAccessMode.xlShared);
            }
            catch { }
        }

        //In Excel
        private void BangChamCongThang_SB2()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongThang_SB", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();

                DataTable dtSLTO = new DataTable(); // Lấy số lượng xí nghiệp
                dtSLTO = ds.Tables[1].Copy();
                int slTO = Convert.ToInt32(dtSLTO.Rows[0][0]);

                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }
                Excell.Application oXL;
                Excell.Workbook oWB;
                Excell.Worksheet oSheet;
                oXL = new Excell.Application();
                oXL.Visible = false;

                oWB = (Excell.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excell.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 18;
                int fontSizeNoiDung = 9;
                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay) + 1;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 4);
                string lastColumNgay = string.Empty;
                lastColumNgay = CharacterIncrement(iSoNgay + 7);
                string firstColumTT = string.Empty;
                firstColumTT = CharacterIncrement(iSoNgay + 8);

                Range row1_TieuDe = oSheet.get_Range("A1", "J1");
                row1_TieuDe.Merge();
                row1_TieuDe.Font.Bold = true;
                row1_TieuDe.Value2 = dtBCThang.Rows[0]["TEN_DV"];
                row1_TieuDe.WrapText = false;
                row1_TieuDe.Style.VerticalAlignment = Excell.XlHAlign.xlHAlignCenter;


                Range row2_TieuDe = oSheet.get_Range("A2", "J2");
                row2_TieuDe.Merge();
                row2_TieuDe.Font.Bold = true;
                row2_TieuDe.Value2 = dtBCThang.Rows[0]["DIA_CHI_DV"];
                row2_TieuDe.WrapText = false;
                row2_TieuDe.Style.VerticalAlignment = Excell.XlHAlign.xlHAlignCenter;


                Range row2_TieuDe_BaoCao = oSheet.get_Range("A3", lastColumn + "3");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THÁNG " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy");

                Range row5_TieuDe_Format = oSheet.get_Range("A5", lastColumn + "6"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Interior.Color = Color.FromArgb(255, 128, 192);

                //Range row7_groupXI_NGHIEP_Format = oSheet.get_Range("A7", lastColumn + "7"); //27 + 31
                //row7_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(0, 255, 255);
                //oSheet.Cells[7, 1] = "BỘ PHẬN";
                //oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 2]].Merge();
                //oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 2]].Font.Bold = true;




                //BorderAround(oSheet.get_Range("A5", lastColumn + "6"));


                Range row5_TieuDe_Stt = oSheet.get_Range("A5");
                row5_TieuDe_Stt.Merge();
                row5_TieuDe_Stt.Value2 = "Stt";
                row5_TieuDe_Stt.ColumnWidth = 5;

                Range row6_TieuDe_Stt = oSheet.get_Range("A6");
                row6_TieuDe_Stt.Merge();
                row6_TieuDe_Stt.Value2 = "No";
                row6_TieuDe_Stt.ColumnWidth = 5;

                Range row5_TieuDe_MaSo = oSheet.get_Range("B5");
                row5_TieuDe_MaSo.Merge();
                row5_TieuDe_MaSo.Value2 = "MSCN";
                row5_TieuDe_MaSo.ColumnWidth = 12;

                Range row6_TieuDe_MaSo = oSheet.get_Range("B6");
                row6_TieuDe_MaSo.Merge();
                row6_TieuDe_MaSo.Value2 = "CODE";
                row6_TieuDe_MaSo.ColumnWidth = 12;

                Range row5_TieuDe_HoTen = oSheet.get_Range("C5");
                row5_TieuDe_HoTen.Merge();
                row5_TieuDe_HoTen.Value2 = "HỌ TÊN";
                row5_TieuDe_HoTen.ColumnWidth = 25;

                Range row6_TieuDe_HoTen = oSheet.get_Range("C6");
                row6_TieuDe_HoTen.Merge();
                row6_TieuDe_HoTen.Value2 = "FULL NAME";
                row6_TieuDe_HoTen.ColumnWidth = 25;

                //Range row5_TieuDe_XiNgiep = oSheet.get_Range("D5");
                //row5_TieuDe_XiNgiep.Merge();
                //row5_TieuDe_XiNgiep.Value2 = "XÍ NGHIỆP";
                //row5_TieuDe_XiNgiep.ColumnWidth = 12;

                //Range row6_TieuDe_XiNgiep = oSheet.get_Range("D6");
                //row6_TieuDe_XiNgiep.Merge();
                //row6_TieuDe_XiNgiep.Value2 = "ENTERPRISE";
                //row6_TieuDe_XiNgiep.ColumnWidth = 12;

                Range row5_TieuDe_To = oSheet.get_Range("D5");
                row5_TieuDe_To.Merge();
                row5_TieuDe_To.Value2 = "Chuyền/Phòng";
                row5_TieuDe_To.ColumnWidth = 12;

                Range row6_TieuDe_To = oSheet.get_Range("D6");
                row6_TieuDe_To.Merge();
                row6_TieuDe_To.Value2 = "DEP";
                row6_TieuDe_To.ColumnWidth = 12;

                int col = 5;
                while (iTNgay <= iDNgay)
                {
                    oSheet.Cells[5, col] = iTNgay;
                    oSheet.Cells[6, col] = "a";
                    oSheet.Cells[6, col].Interior.Color = Color.White;

                    //Range row6_b = oSheet.get_Range(oSheet.Cells[6, col + 1]);
                    //row6_b.Value2 = "b";
                    //row6_b.Interior.Color = Color.FromArgb(128, 255, 128);

                    oSheet.Cells[6, col + 1] = "b";
                    oSheet.Cells[6, col + 1].Interior.Color = Color.FromArgb(187, 255, 187);
                    oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col)], oSheet.Cells[5, Convert.ToInt32(col + 1)]].Merge();
                    //oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 1]].Merge();
                    col += 2;
                    iTNgay++;
                }

                oSheet.Cells[5, col] = "Ngày công";
                oSheet.Cells[6, col] = "Workday";

                col = col + 1;
                oSheet.Cells[5, col] = "Tăng ca";
                oSheet.Cells[6, col] = "Overtime";

                col = col + 1;
                oSheet.Cells[5, col] = "Tăng ca đêm";
                oSheet.Cells[6, col] = "Night OT";

                col = col + 1;
                oSheet.Cells[5, col] = "Chủ nhật";
                oSheet.Cells[6, col] = "Sunday";

                col = col + 1;
                oSheet.Cells[5, col] = "Ngày lễ";
                oSheet.Cells[6, col] = "Holidays";

                col = col + 1;
                oSheet.Cells[5, col] = "Ghi chú (Notes)";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 5]].Merge();
                oSheet.Cells[6, col] = "P Anmual";
                oSheet.Cells[6, col + 1] = "CĐ Policy";
                oSheet.Cells[6, col + 2] = "KL Comp";
                oSheet.Cells[6, col + 3] = "01";
                oSheet.Cells[6, col + 4] = "03";
                oSheet.Cells[6, col + 5] = "VLD Unreasonab";

                col = col + 6;
                oSheet.Cells[6, col] = "TR/S Late";

                col = col + 1;
                oSheet.Cells[6, col] = "QBT Forget";

                col = col + 1;
                oSheet.Cells[6, col] = "count overtime";


                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                int rowCONG = 0; // Row để insert dòng tổng
                //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                int rowBD = 7;
                string cotCN_A = "";
                string cotCN_B = "";
                string[] TEN_TO = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_TO")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[0].Copy(); // Dữ row count data


                for (int i = 0; i < TEN_TO.Count(); i++)
                {
                    dtBCThang = ds.Tables[0].Copy();
                    dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_TO") == TEN_TO[i]).CopyToDataTable().Copy();
                    DataRow[] dr = dtBCThang.Select();
                    current_dr = dr.Count();
                    string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                    foreach (DataRow row in dr)
                    {
                        for (col = 0; col < dtBCThang.Columns.Count; col++)
                        {
                            if (Convert.ToInt32(row[0]) == 1)
                            {
                                if (row[col].ToString() == "CN")
                                {
                                    //cotCN = cotCN + (col + 1) + ",";
                                    cotCN_A = CharacterIncrement(col);
                                    cotCN_B = CharacterIncrement(col + 1);
                                    Range ToMau = oSheet.get_Range("" + cotCN_A + "5", cotCN_B + "" + (dt_temp.Rows.Count + 6 + (slTO * 2)) + ""); //vi du slxn = 3 , 3 dong ten xi + 3 dong tong cua xi nghiep do nen 3*2
                                    ToMau.Interior.Color = Color.FromArgb(255, 128, 0);
                                    //ToMau.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                }
                            }
                            rowData[rowCnt, col] = row[col].ToString();
                        }
                        rowCnt++;
                    }
                    if (chanVongDau == "Chan") // Chạy vòng đầu tiên, rowBD_XN = 0, vì nó nằm dòng đầu tiên thì rowBD lúc này sẽ  = 7, các vòng tiếp theo sẽ lấy cái dòng BĐ của + thêm rowBD_XN = 1 vào để không bị nằm đè lên dòng thứ 9
                    {
                        dr_Cu = 0;
                        rowBD_XN = 0;
                        rowCONG = 0;
                        chanVongDau = "";
                    }
                    else
                    {
                        rowBD_XN = 1;
                        rowCONG = 1;
                    }
                    rowBD = rowBD + dr_Cu + rowBD_XN + rowCONG;
                    //rowCnt = rowCnt + 6 + dr_Cu;
                    rowCnt = rowBD + current_dr - 1;


                    // Tạo group xí nghiệp
                    Range row_groupXI_NGHIEP_Format = oSheet.get_Range("A" + rowBD + "".ToString(), lastColumn + "" + rowBD + "".ToString()); //27 + 31
                    row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(0, 255, 255);
                    oSheet.Cells[rowBD, 1] = "Chuyền/Phòng";
                    oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 2]].Merge();
                    oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 2]].Font.Bold = true;
                    oSheet.Cells[rowBD, 3] = TEN_TO[i].ToString();

                    //Đổ dữ liệu của xí nghiệp
                    oSheet.get_Range("A" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;

                    //Tính tổng xí nghiệp
                    Range row_groupTONG_Format = oSheet.get_Range("A" + (rowBD + current_dr + 1) + "".ToString(), lastColumn + "" + (rowBD + current_dr + 1) + "".ToString()); //27 + 31 // (rowBD + current_dr +1) sẽ lấy cái dòng bắt đầu (7) + dòng dữ liệu (ví dụ là 2 dòng) = 9 thì cột cộng sẽ + thêm 1 dòng nữa  = 10
                    row_groupTONG_Format.Interior.Color = Color.Yellow;
                    row_groupTONG_Format.Font.Bold = true;
                    oSheet.Cells[(rowBD + current_dr + 1), 1] = "Cộng";
                    oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 1], oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 4]].Merge();

                    for (int colSUM = 5; colSUM < dtBCThang.Columns.Count - 2; colSUM++)
                    {
                        oSheet.Cells[(rowBD + current_dr + 1), colSUM] = "=SUM(" + CellAddress(oSheet, rowBD + 1, colSUM) + ":" + CellAddress(oSheet, (rowBD + current_dr), colSUM) + ")";
                    }

                    dr_Cu = current_dr;
                    keepRowCnt = rowCnt;
                    rowCnt = 0;
                }

                Excell.Range formatRange;
                rowCnt = keepRowCnt + 2; // Cộng 2 vì ở trên thêm 2 dòng xí nghiệp và cộng
                //formatRange = oSheet.get_Range("G7", "G" + rowCnt.ToString());
                //formatRange.NumberFormat = "dd/MM/yyyy";
                //formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                //formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);
                //formatRange = oSheet.get_Range("H7", "H" + rowCnt.ToString());
                //formatRange.NumberFormat = "dd/MM/yyyy";
                //formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                //formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);
                //formatRange = oSheet.get_Range("I7", lastColumNgay + rowCnt.ToString());
                //formatRange.NumberFormat = "@";
                //formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;

                //dịnh dạng
                //Commons.Modules.MExcel.ThemDong(oSheet, Excell.XlInsertShiftDirection.xlShiftDown, 1, 7);

                string CurentColumn = string.Empty;
                int colBD = 4;
                int colKT = dtBCThang.Columns.Count;
                //format

                for (col = colBD; col < dtBCThang.Columns.Count - 3; col++)
                {
                    CurentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    formatRange.NumberFormat = "0.00;-0;;@";
                    try { formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                //colKT++;
                //CurentColumn = CharacterIncrement(colKT);
                //formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                ////formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);
                ////Kẻ khung toàn bộ
                formatRange = oSheet.get_Range("A7", lastColumn + rowCnt.ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                BorderAround(oSheet.get_Range("A5", lastColumn + rowCnt.ToString()));
                // filter
                oSheet.Application.ActiveWindow.SplitColumn = 4;
                oSheet.Application.ActiveWindow.FreezePanes = true;
                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excell.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BangChamCongThang_MT()
        {
            frmViewReport frm = new frmViewReport();
            string sTieuDe = "BẢNG CHẤM CÔNG TỔNG HỢP THÁNG " + LK_Thang.EditValue.ToString();
            frm.rpt = new rptBangCongTongHopThang_MT(sTieuDe, Convert.ToDateTime(NgayIn.EditValue), Convert.ToDateTime(lk_TuNgay.EditValue), Convert.ToDateTime(lk_DenNgay.EditValue));
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangCongThang_MT"), conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                //DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                adp.Fill(dt);

                //dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);
            }
            catch
            { }
            frm.ShowDialog();
        }
        private void BangChamCongThangGioCong_HN()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongThangGioCong_HN", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
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
                Excell.Application oXL;
                Excell._Workbook oWB;
                Excell._Worksheet oSheet;

                oXL = new Excell.Application();
                oXL.Visible = true;

                oWB = (Excell._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excell._Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay) + 1;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 2);
                string lastColumNgay = string.Empty;
                lastColumNgay = CharacterIncrement(iSoNgay + 7);
                string firstColumTT = string.Empty;
                firstColumTT = CharacterIncrement(iSoNgay + 8);

                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", lastColumn + "2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THÁNG " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy");

                Range row5_TieuDe_Format = oSheet.get_Range("A5", lastColumn + "7"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Interior.Color = Color.Yellow;

                Range row5_TieuDe_Stt = oSheet.get_Range("A5", "A6");
                row5_TieuDe_Stt.Merge();
                row5_TieuDe_Stt.Value2 = "Stt";
                row5_TieuDe_Stt.ColumnWidth = 6;

                Range row5_TieuDe_Stt_A = oSheet.get_Range("A7");
                row5_TieuDe_Stt_A.Value2 = "No.";

                Range row5_TieuDe_MaSo = oSheet.get_Range("B5", "B6");
                row5_TieuDe_MaSo.Merge();
                row5_TieuDe_MaSo.Value2 = "MSCN";
                row5_TieuDe_MaSo.ColumnWidth = 15;

                Range row5_TieuDe_MS_A = oSheet.get_Range("B7");
                row5_TieuDe_MS_A.Value2 = "Employee code";

                Range row5_TieuDe_HoTen = oSheet.get_Range("C5", "C6");
                row5_TieuDe_HoTen.Merge();
                row5_TieuDe_HoTen.Value2 = "Họ và tên";
                row5_TieuDe_HoTen.ColumnWidth = 30;

                Range row5_TieuDe_HO_TEN_A = oSheet.get_Range("C7");
                row5_TieuDe_HO_TEN_A.Value2 = "Full name";

                int col = 4;
                while (iTNgay <= iDNgay)
                {
                    oSheet.Cells[5, col] = iTNgay;
                    oSheet.Cells[7, col] = iTNgay;
                    oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();

                    col++;
                    iTNgay++;
                }

                oSheet.Cells[5, col] = "Qui ra công để trả lương";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 7]].Merge();
                oSheet.Cells[6, col] = "Công TTTổ";
                oSheet.Cells[6, col + 1] = "P";
                oSheet.Cells[7, col + 1] = "P";
                oSheet.Cells[6, col + 2] = "Ô";
                oSheet.Cells[7, col + 2] = "Ô";
                oSheet.Cells[6, col + 3] = "Cô";
                oSheet.Cells[7, col + 3] = "Cô";
                oSheet.Cells[6, col + 4] = "Ro";
                oSheet.Cells[7, col + 4] = "Ro";
                oSheet.Cells[6, col + 5] = "O";
                oSheet.Cells[7, col + 5] = "O";
                oSheet.Cells[6, col + 6] = "V";
                oSheet.Cells[7, col + 6] = "V";
                oSheet.Cells[6, col + 7] = "70";
                oSheet.Cells[7, col + 7] = "70";

                col = col + 8;

                oSheet.Cells[5, col] = "Tổng giờ làm việc";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Total working hours";

                col++;
                oSheet.Cells[5, col] = "TC 9h";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Overtime 9h";

                col++;
                oSheet.Cells[5, col] = "TC > 9h";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                oSheet.Cells[7, col] = "Overtime > 9h";

                col++;
                oSheet.Cells[5, col] = "Phụ";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 1]].Merge();
                oSheet.Cells[6, col] = "50 %";
                oSheet.Cells[6, col + 1] = "100 %";
                oSheet.Cells[7, col] = "50 %";
                oSheet.Cells[7, col + 1] = "100 %";


                //DataRow[] dr = dtBCThang.Select();
                //string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                //int rowCnt = 0;
                //foreach (DataRow row in dr)
                //{
                //    for (col = 0; col < dtBCThang.Columns.Count; col++)
                //    {
                //        rowData[rowCnt, col] = row[col].ToString();
                //    }

                //    rowCnt++;
                //}
                //rowCnt = rowCnt + 6;
                //oSheet.get_Range("A8", lastColumn + rowCnt.ToString()).Value2 = rowData;
                //Excell.Range formatRange;

                Excell.Range formatRange;
                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                int rowCONG = 0; // Row để insert dòng tổng
                //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                string sRowBD_DV = ";"; // Lưu lại các dòng của row đơn vị
                string sRowBD_XN = ";"; // Lưu lại các dòng của row xí nghiệp
                int rowBD = 8;
                string[] TEN_TO = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_TO")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[0].Copy(); // Dữ row count data
                string sRowBD_XN_Temp = "";
                for (int j = 0; j < TEN_TO.Count(); j++)
                {
                    dtBCThang = ds.Tables[0].Copy();
                    dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_TO") == TEN_TO[j]).CopyToDataTable().Copy();
                    DataRow[] dr = dtBCThang.Select();
                    current_dr = dr.Count();
                    string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                    foreach (DataRow row in dr)
                    {
                        for (col = 0; col < dtBCThang.Columns.Count; col++)
                        {
                            rowData[rowCnt, col] = row[col].ToString();
                        }
                        rowCnt++;
                    }
                    if (chanVongDau == "Chan") // Chạy vòng đầu tiên, rowBD_XN = 0, vì nó nằm dòng đầu tiên thì rowBD lúc này sẽ  = 7, các vòng tiếp theo sẽ lấy cái dòng BĐ của + thêm rowBD_XN = 1 vào để không bị nằm đè lên dòng thứ 9
                    {
                        dr_Cu = 0;
                        rowBD_XN = 0;
                        rowCONG = 0;
                        chanVongDau = "";
                    }
                    else
                    {
                        rowBD_XN = 1;
                        rowCONG = 1;
                    }
                    //rowBD = rowBD + dr_Cu + rowBD_XN;
                    rowBD = rowBD + dr_Cu + rowBD_XN + rowCONG;
                    rowCnt = rowBD + current_dr - 1;

                    // Tạo group tổ
                    Range row_groupXI_NGHIEP_Format = oSheet.get_Range("A" + rowBD + "".ToString(), lastColumn + "" + rowBD + "".ToString()); //27 + 31
                    row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(146, 208, 80);
                    row_groupXI_NGHIEP_Format.Merge();
                    oSheet.Cells[rowBD, 1] = TEN_TO[j].ToString();
                    oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Bold = true;

                    //for (col = 3; col < dtBCThang.Columns.Count - 2; col++)
                    //{
                    //    oSheet.Cells[rowBD, col] = "=+SUM(" + CharacterIncrement(col - 1) + "" + (rowBD + 1).ToString() + ":" + CharacterIncrement(col - 1) + "" + (rowCnt + 1).ToString() + ")";
                    //    oSheet.Cells[rowBD, col].Font.Bold = true;
                    //    oSheet.Cells[rowBD, col].Font.Size = 12;
                    //}

                    //sRowBD_XN = sRowBD_XN + rowBD.ToString() + "+;";
                    //sRowBD_XN_Temp = sRowBD_XN;
                    //Đổ dữ liệu của xí nghiệp
                    oSheet.get_Range("A" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;

                    //// Dữ liệu cột tổng tăng
                    //for (int k = rowBD + 1; k <= rowCnt + 1; k++)
                    //{
                    //    oSheet.Cells[k, 3] = "=D" + k + "+E" + k + "";
                    //    oSheet.Cells[k, 6] = "=M" + k + "+N" + k + "";
                    //    oSheet.Cells[k, 15] = "=C" + k + "-F" + k + "";
                    //}

                    //Tính tổng xí nghiệp
                    Range row_groupTONG_Format = oSheet.get_Range("A" + (rowBD + current_dr + 1) + "".ToString(), lastColumn + "" + (rowBD + current_dr + 1) + "".ToString()); //27 + 31 // (rowBD + current_dr +1) sẽ lấy cái dòng bắt đầu (7) + dòng dữ liệu (ví dụ là 2 dòng) = 9 thì cột cộng sẽ + thêm 1 dòng nữa  = 10
                    row_groupTONG_Format.Interior.Color = Color.Yellow;
                    row_groupTONG_Format.Font.Bold = true;
                    oSheet.Cells[(rowBD + current_dr + 1), 1] = "Cộng";
                    oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 1], oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 4]].Merge();

                    //for (int colSUM = 5; colSUM < dtBCThang.Columns.Count - 2; colSUM++)
                    //{
                    oSheet.Cells[rowCnt + 2, 4 + iDNgay] = "=SUM(" + CharacterIncrement(3 + iDNgay) + "" + (rowBD + 1).ToString() + ":" + CharacterIncrement(3 + iDNgay) + "" + (rowCnt + 1).ToString() + ")";
                    //}
                    dr_Cu = current_dr;
                    keepRowCnt = rowCnt;
                    rowCnt = 0;
                }

                rowCnt = keepRowCnt + 2;
                //formatRange = oSheet.get_Range("G8", "G" + rowCnt.ToString());
                //formatRange.NumberFormat = "dd/MM/yyyy";
                //formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                //formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);
                //formatRange = oSheet.get_Range("H8", "H" + rowCnt.ToString());
                //formatRange.NumberFormat = "dd/MM/yyyy";
                //formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                //formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);
                //formatRange = oSheet.get_Range("I8", lastColumNgay + rowCnt.ToString());
                //formatRange.NumberFormat = "@";
                //formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;

                string CurentColumn = string.Empty;
                int colBD = iSoNgay + 8;
                int colKT = colBD + 9;

                for (col = colBD; col <= colKT; col++)
                {
                    CurentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                    formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    try { formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }
                ////Kẻ khung toàn bộ

                BorderAround(oSheet.get_Range("A5", lastColumn + rowCnt.ToString()));

                rowCnt = rowCnt + 2;
                oSheet.Cells[rowCnt, 2].Font.Bold = true;
                oSheet.Cells[rowCnt, 2] = "Mã số";
                oSheet.Cells[rowCnt, 3].Font.Bold = true;
                oSheet.Cells[rowCnt, 3] = "Lý do";

                oSheet.Cells[rowCnt, 8].Font.Bold = true;
                oSheet.Cells[rowCnt, 8] = "Mã số";
                oSheet.Cells[rowCnt, 9].Font.Bold = true;
                oSheet.Cells[rowCnt, 9] = "Lý do";

                oSheet.Cells[rowCnt, 14].Font.Bold = true;
                oSheet.Cells[rowCnt, 14] = "Mã số";
                oSheet.Cells[rowCnt, 15].Font.Bold = true;
                oSheet.Cells[rowCnt, 15] = "Lý do";

                oSheet.Cells[rowCnt, 20].Font.Bold = true;
                oSheet.Cells[rowCnt, 20] = "Mã số";
                oSheet.Cells[rowCnt, 21].Font.Bold = true;
                oSheet.Cells[rowCnt, 21] = "Lý do";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "B";
                oSheet.Cells[rowCnt, 3] = "Mưa bão";
                oSheet.Cells[rowCnt, 8] = "C";
                oSheet.Cells[rowCnt, 9] = "Việc công";
                oSheet.Cells[rowCnt, 14] = "CB";
                oSheet.Cells[rowCnt, 15] = "Con bú";
                oSheet.Cells[rowCnt, 20] = "Cơ";
                oSheet.Cells[rowCnt, 21] = "Nghỉ con ốm";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "CV";
                oSheet.Cells[rowCnt, 3] = "Chờ việc";
                oSheet.Cells[rowCnt, 8] = "D1";
                oSheet.Cells[rowCnt, 9] = "DSPHSK sau ốm đau tại gia đình";
                oSheet.Cells[rowCnt, 14] = "D2";
                oSheet.Cells[rowCnt, 15] = "DSPHSK sau ốm đau nghỉ tập trung";
                oSheet.Cells[rowCnt, 20] = "D3";
                oSheet.Cells[rowCnt, 21] = "DSPHSK sau thai sản tại gia đình";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "D4";
                oSheet.Cells[rowCnt, 3] = "DSPHSK sau thai sản nghỉ tập trung";
                oSheet.Cells[rowCnt, 8] = "D5";
                oSheet.Cells[rowCnt, 9] = "DSPHSK sau tai nạn nghỉ tập trung";
                oSheet.Cells[rowCnt, 14] = "D6";
                oSheet.Cells[rowCnt, 15] = "DSPHSK sau ốm đau nghỉ tập trung";
                oSheet.Cells[rowCnt, 20] = "DH";
                oSheet.Cells[rowCnt, 21] = "Nghỉ dài hạn";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "F";
                oSheet.Cells[rowCnt, 3] = "Nghỉ phép";
                oSheet.Cells[rowCnt, 8] = "H";
                oSheet.Cells[rowCnt, 9] = "Nghỉ hội họp, học tập, công tác";
                oSheet.Cells[rowCnt, 14] = "L";
                oSheet.Cells[rowCnt, 15] = "Ngày nghỉ lễ, tết theo BLLĐ";
                oSheet.Cells[rowCnt, 20] = "O";
                oSheet.Cells[rowCnt, 21] = "Nghỉ không lý do";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "Ơ";
                oSheet.Cells[rowCnt, 3] = "Nghỉ ốm";
                oSheet.Cells[rowCnt, 8] = "O1";
                oSheet.Cells[rowCnt, 9] = "Bản thân ốm ngắn ngày";
                oSheet.Cells[rowCnt, 14] = "O2";
                oSheet.Cells[rowCnt, 15] = "Bản thân ốm dài ngày";
                oSheet.Cells[rowCnt, 20] = "O3";
                oSheet.Cells[rowCnt, 21] = "Con ốm";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "Q";
                oSheet.Cells[rowCnt, 3] = "Di chuyển";
                oSheet.Cells[rowCnt, 8] = "R";
                oSheet.Cells[rowCnt, 9] = "Nghỉ việc riêng có lương";
                oSheet.Cells[rowCnt, 14] = "Ro";
                oSheet.Cells[rowCnt, 15] = "Nghỉ việc riêng không lương";
                oSheet.Cells[rowCnt, 20] = "T";
                oSheet.Cells[rowCnt, 21] = "Tai nạn lao động";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "T1";
                oSheet.Cells[rowCnt, 3] = "Khám thai";
                oSheet.Cells[rowCnt, 8] = "T2";
                oSheet.Cells[rowCnt, 9] = "Sẩy thai, nạo hút thai, thai chết lưu";
                oSheet.Cells[rowCnt, 14] = "T3";
                oSheet.Cells[rowCnt, 15] = "Sinh con, nuôi con nuôi";
                oSheet.Cells[rowCnt, 20] = "T4";
                oSheet.Cells[rowCnt, 21] = "Thực hiện các biện pháp tránh thai";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "T5";
                oSheet.Cells[rowCnt, 3] = "Lao động nghỉ việc khi vợ sinh con";
                oSheet.Cells[rowCnt, 8] = "T6";
                oSheet.Cells[rowCnt, 9] = "Lao động nữ mang thai hộ sinh con";
                oSheet.Cells[rowCnt, 14] = "T7";
                oSheet.Cells[rowCnt, 15] = "Lao động nữ nhờ mang thai hộ nhận con";
                oSheet.Cells[rowCnt, 20] = "T8";
                oSheet.Cells[rowCnt, 21] = "Lao động nam hưởng trợ cấp 1 lần khi vợ sinh con";

                rowCnt++;
                oSheet.Cells[rowCnt, 2] = "V";
                oSheet.Cells[rowCnt, 3] = "Ngừng việc";

                rowCnt++;
                oSheet.Cells[rowCnt, 8].Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                oSheet.Cells[rowCnt, 8].Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                oSheet.Cells[rowCnt, 8] = "Ngày......tháng......năm.......";
                oSheet.Cells[rowCnt, 20] = "Người duyệt";
                rowCnt++;
                oSheet.Cells[rowCnt, 8].Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                oSheet.Cells[rowCnt, 8].Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                oSheet.Cells[rowCnt, 8] = "Người lập biểu";

                formatRange = oSheet.get_Range("A8", lastColumn + rowCnt.ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excell.XlSaveAsAccessMode.xlExclusive);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BangChamCongThang_HN()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCLaoDongThang_HN", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
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
                Excell.Application oXL;
                Excell.Workbook oWB;
                Excell.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;

                oWB = (Excell.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excell.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 12;
                int fontSizeNoiDung = 9;


                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 3);

                Range row1_TieuDe_BaoCao = oSheet.get_Range("A1");
                row1_TieuDe_BaoCao.Value = "Công Ty Cổ Phần May Hữu Nghị";
                row1_TieuDe_BaoCao.Font.Size = 10;
                row1_TieuDe_BaoCao.Font.Name = fontName;
                row1_TieuDe_BaoCao.Font.Bold = true;

                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", lastColumn + "2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = 24;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                row2_TieuDe_BaoCao.Value2 = "BÁO CÁO LAO ĐỘNG THÁNG " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy") + "";

                Excell.Range row5_TieuDe_Format = oSheet.get_Range("A3", lastColumn + "5"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Interior.Color = Color.FromArgb(255, 255, 0);


                Excell.Range row5_TieuDe_DV = oSheet.get_Range("A3", "A5");
                row5_TieuDe_DV.Merge();
                row5_TieuDe_DV.Value2 = "ĐƠN VỊ";
                row5_TieuDe_DV.ColumnWidth = 12;

                Excell.Range row5_TieuDe_LDBQ = oSheet.get_Range("B3", "B5");
                row5_TieuDe_LDBQ.Merge();
                row5_TieuDe_LDBQ.Value2 = "LĐ BQ";
                row5_TieuDe_LDBQ.ColumnWidth = 6;

                Range row5_TieuDe_LDT = oSheet.get_Range("C3", "F3");
                row5_TieuDe_LDT.Merge();
                row5_TieuDe_LDT.Value2 = "LAO ĐỘNG TĂNG";

                Range row6_TieuDe_TT = oSheet.get_Range("C4", "C5");
                row6_TieuDe_TT.Merge();
                row6_TieuDe_TT.Value2 = "TỔNG TĂNG";
                row6_TieuDe_TT.ColumnWidth = 11;
                row6_TieuDe_TT.RowHeight = 30;

                Range row5_TieuDe_DT = oSheet.get_Range("D4", "D5");
                row5_TieuDe_DT.Merge();
                row5_TieuDe_DT.Value2 = "ĐÀO TẠO";
                row5_TieuDe_DT.ColumnWidth = 11;

                Range row5_TieuDe_TN = oSheet.get_Range("E4", "E5");
                row5_TieuDe_TN.Merge();
                row5_TieuDe_TN.Value2 = "THỬ VIỆC";
                row5_TieuDe_TN.ColumnWidth = 11;

                Range row5_TieuDe_CTT = oSheet.get_Range("F4", "F5");
                row5_TieuDe_CTT.Merge();
                row5_TieuDe_CTT.Value2 = "TS+Ô ChTổ";
                row5_TieuDe_CTT.ColumnWidth = 11;


                Range row5_TieuDe_LDG = oSheet.get_Range("G3", "J3");
                row5_TieuDe_LDG.Merge();
                row5_TieuDe_LDG.Value2 = "LAO ĐỘNG GIẢM";

                Range row6_TieuDe_TG = oSheet.get_Range("G4", "G5");
                row6_TieuDe_TG.Merge();
                row6_TieuDe_TG.Value2 = "TỔNG GIẢM";
                row6_TieuDe_TG.ColumnWidth = 11;

                Range row6_TieuDe_BV = oSheet.get_Range("H4", "H5");
                row6_TieuDe_BV.Merge();
                row6_TieuDe_BV.Value2 = "BV";
                row6_TieuDe_BV.ColumnWidth = 7.6;

                Range row6_TieuDe_NV = oSheet.get_Range("I4", "I5");
                row6_TieuDe_NV.Merge();
                row6_TieuDe_NV.Value2 = "NV";
                row6_TieuDe_NV.ColumnWidth = 11;

                Range row5_TieuDe_J = oSheet.get_Range("J4", "J5");
                row5_TieuDe_J.Merge();
                row5_TieuDe_J.Value2 = "TS+Ô ChTổ";
                row5_TieuDe_J.ColumnWidth = 11;

                Range row6_TieuDe_CONG = oSheet.get_Range("K3");
                row6_TieuDe_CONG.Value2 = "Công";
                row6_TieuDe_CONG.ColumnWidth = 7;

                Range row6_TieuDe_LDCK = oSheet.get_Range("K4", "K5");
                row6_TieuDe_LDCK.Merge();
                row6_TieuDe_LDCK.Value2 = "Chế độ";


                Range row6_TieuDe_GC = oSheet.get_Range("L3", "R3");
                row6_TieuDe_GC.Merge();
                row6_TieuDe_GC.Value2 = "CÔNG THỰC TẾ";

                Range row6_TieuDe_TRONG_GIO = oSheet.get_Range("L4", "N4");
                row6_TieuDe_TRONG_GIO.Merge();
                row6_TieuDe_TRONG_GIO.Value2 = "Trong giờ";

                Range row6_TieuDe_NGOAI_GIO = oSheet.get_Range("P4", "Q4");
                row6_TieuDe_NGOAI_GIO.Merge();
                row6_TieuDe_NGOAI_GIO.Value2 = "Ngoài giờ";

                Range row6_TieuDe_TCTT = oSheet.get_Range("R4", "R5");
                row6_TieuDe_TCTT.Merge();
                row6_TieuDe_TCTT.ColumnWidth = 10;
                row6_TieuDe_TCTT.Value2 = "+";

                Range row6_TieuDe_NC = oSheet.get_Range("L5", "M5");
                row6_TieuDe_NC.Merge();
                row6_TieuDe_NC.ColumnWidth = 14;
                row6_TieuDe_NC.Value2 = "Ngày công";

                Range row6_TieuDe_OVER = oSheet.get_Range("N5");
                row6_TieuDe_OVER.ColumnWidth = 10;
                row6_TieuDe_OVER.Value2 = "1.5";

                Range row6_TieuDe_OVERCN = oSheet.get_Range("O5");
                row6_TieuDe_OVERCN.ColumnWidth = 10;
                row6_TieuDe_OVERCN.Value2 = "CN";

                Range row6_TieuDe_OVERNG = oSheet.get_Range("P5");
                row6_TieuDe_OVERNG.ColumnWidth = 10;
                row6_TieuDe_OVERNG.Value2 = "1.5";

                Range row6_TieuDe_OVERCN_NG = oSheet.get_Range("Q5");
                row6_TieuDe_OVERCN_NG.ColumnWidth = 10;
                row6_TieuDe_OVERCN_NG.Value2 = "CN";

                Range row6_TieuDe_CTTE = oSheet.get_Range("S3");
                row6_TieuDe_CTTE.ColumnWidth = 10;
                row6_TieuDe_CTTE.Value2 = "% Công thực tế";

                Range row6_TieuDe_SC = oSheet.get_Range("S4");
                row6_TieuDe_SC.Value2 = "So công";

                Range row6_TieuDe_CDP = oSheet.get_Range("S5");
                row6_TieuDe_CDP.Value2 = "C.độ - Phép";

                Range row6_TieuDe_CONGV = oSheet.get_Range("T3", "V3");
                row6_TieuDe_CONGV.Merge();
                row6_TieuDe_CONGV.Value2 = "CÔNG VẮNG MẶT";

                Range row6_TieuDe_TONG_CV = oSheet.get_Range("T4", "T5");
                row6_TieuDe_TONG_CV.Merge();
                row6_TieuDe_TONG_CV.ColumnWidth = 10;
                row6_TieuDe_TONG_CV.Value2 = "+";

                Range row6_TieuDe_CV_F = oSheet.get_Range("U4", "U5");
                row6_TieuDe_CV_F.Merge();
                row6_TieuDe_CV_F.ColumnWidth = 10;
                row6_TieuDe_CV_F.Value2 = "F";

                Range row6_TieuDe_CV_OO = oSheet.get_Range("V4", "V5");
                row6_TieuDe_CV_OO.Merge();
                row6_TieuDe_CV_OO.ColumnWidth = 10;
                row6_TieuDe_CV_OO.Value2 = "Ô";

                Range row6_TieuDe_CV_CO = oSheet.get_Range("W4", "W5");
                row6_TieuDe_CV_CO.Merge();
                row6_TieuDe_CV_CO.ColumnWidth = 10;
                row6_TieuDe_CV_CO.Value2 = "CÔ";

                Range row6_TieuDe_CV_RO = oSheet.get_Range("X4", "X5");
                row6_TieuDe_CV_RO.Merge();
                row6_TieuDe_CV_RO.ColumnWidth = 10;
                row6_TieuDe_CV_RO.Value2 = "Ro";

                Range row6_TieuDe_CV_O = oSheet.get_Range("Y4", "Y5");
                row6_TieuDe_CV_O.Merge();
                row6_TieuDe_CV_O.ColumnWidth = 10;
                row6_TieuDe_CV_O.Value2 = "O";

                Range row6_TieuDe_CV_RF = oSheet.get_Range("Z4", "Z5");
                row6_TieuDe_CV_RF.Merge();
                row6_TieuDe_CV_RF.ColumnWidth = 10;
                row6_TieuDe_CV_RF.Value2 = "RF";

                Range row6_TieuDe_CV_CD = oSheet.get_Range("AA4", "AA5");
                row6_TieuDe_CV_CD.Merge();
                row6_TieuDe_CV_CD.ColumnWidth = 10;
                row6_TieuDe_CV_CD.Value2 = "CD";

                Range row6_TieuDe_DT = oSheet.get_Range("AB3", "AE3");
                row6_TieuDe_DT.Merge();
                row6_TieuDe_DT.Value2 = "DOANH THU (theo Cty)";

                Range row6_TieuDe_DT_KH = oSheet.get_Range("AB4", "AB5");
                row6_TieuDe_DT_KH.Merge();
                row6_TieuDe_DT_KH.ColumnWidth = 10;
                row6_TieuDe_DT_KH.Value2 = "KH";

                Range row6_TieuDe_DT_TH = oSheet.get_Range("AC4", "AC5");
                row6_TieuDe_DT_TH.Merge();
                row6_TieuDe_DT_TH.ColumnWidth = 10;
                row6_TieuDe_DT_TH.Value2 = "TH";

                Range row6_TieuDe_DT_PT = oSheet.get_Range("AD4", "AD5");
                row6_TieuDe_DT_PT.Merge();
                row6_TieuDe_DT_PT.ColumnWidth = 10;
                row6_TieuDe_DT_PT.Value2 = "%";

                Range row6_TieuDe_DT_NSLD = oSheet.get_Range("AE4", "AE5");
                row6_TieuDe_DT_NSLD.Merge();
                row6_TieuDe_DT_NSLD.ColumnWidth = 10;
                row6_TieuDe_DT_NSLD.Value2 = "NSLĐ";

                oSheet.Application.ActiveWindow.SplitColumn = 2;
                oSheet.Application.ActiveWindow.SplitRow = 5;
                oSheet.Application.ActiveWindow.FreezePanes = true;


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
                int rowBD = 6;
                string[] TEN_DV = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_DV")).Distinct().ToArray();
                string[] TEN_XN = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_XN")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[0].Copy(); // Dữ row count data


                for (int i = 0; i < TEN_DV.Count(); i++)
                {
                    // Tạo group đơn vị
                    Range row_groupDON_VI_Format = oSheet.get_Range("A" + rowBD + "".ToString(), lastColumn + "" + rowBD + "".ToString()); //27 + 31
                    row_groupDON_VI_Format.Interior.Color = Color.FromArgb(255, 255, 0);
                    oSheet.Cells[rowBD, 1] = TEN_DV[i].ToString();
                    oSheet.Cells[rowBD, 1].Font.Bold = true;
                    oSheet.Cells[rowBD, 1].Font.Underline = true;
                    oSheet.Cells[rowBD, 1].Font.Size = 14;
                    sRowBD_DV = sRowBD_DV + rowBD.ToString() + "+;";
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
                            for (col = 0; col < dtBCThang.Columns.Count; col++)
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
                        Range row_groupXI_NGHIEP_Format = oSheet.get_Range("A" + rowBD + "".ToString(), lastColumn + "" + rowBD + "".ToString()); //27 + 31
                        row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(146, 208, 80);
                        oSheet.Cells[rowBD, 1] = TEN_XN[j].ToString();
                        oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Bold = true;
                        oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Underline = true;
                        oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 1]].Font.Italic = true;

                        for (col = 2; col < dtBCThang.Columns.Count - 1; col++)
                        {
                            oSheet.Cells[rowBD, col] = "=+SUM(" + CharacterIncrement(col - 1) + "" + (rowBD + 1).ToString() + ":" + CharacterIncrement(col - 1) + "" + (rowCnt + 1).ToString() + ")";
                            oSheet.Cells[rowBD, col].Font.Bold = true;
                            oSheet.Cells[rowBD, col].Font.Underline = true;
                            oSheet.Cells[rowBD, col].Font.Italic = true;
                            oSheet.Cells[rowBD, col].Font.Size = 12;
                        }

                        sRowBD_XN = sRowBD_XN + rowBD.ToString() + "+;";

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
                }
                Excell.Range formatRange;
                //Sum đơn vị
                string[] strGetRowDV = sRowBD_DV.Split(';');
                string sRowBD_DV_Temp = sRowBD_DV;
                string sRowBD_XN_Temp = sRowBD_XN; // Lưu giá trị cũ
                for (int i = 0; i < strGetRowDV.Count(); i++)
                {
                    if (strGetRowDV[i].ToString() != "")
                    {
                        for (col = 0; col < dtBCThang.Columns.Count - 3; col++) // Bỏ thêm 2 cột ghi chú và lao động cuối kỳ
                        {
                            formatRange = oSheet.get_Range("" + CharacterIncrement(col + 1) + "" + strGetRowDV[i].Substring(0, strGetRowDV[i].Length - 1).ToString() + "");
                            formatRange.Font.Bold = true;
                            formatRange.Font.Underline = true;
                            formatRange.Font.Size = 14;
                            sRowBD_XN = sRowBD_XN.Substring(0, sRowBD_XN.Length - 2);
                            sRowBD_XN = sRowBD_XN.Replace(@";", CharacterIncrement(col + 1));
                            formatRange.Value = "=" + sRowBD_XN;
                            sRowBD_XN = sRowBD_XN_Temp;
                        }
                    }
                }

                rowCnt = keepRowCnt + 1; // Cộng 2 vì ở trên thêm 2 dòng xí nghiệp và cộng

                formatRange = oSheet.get_Range("C8", "" + "C" + rowCnt + "");
                formatRange.Font.Bold = true;
                formatRange = oSheet.get_Range("F8", "" + "F" + rowCnt + "");
                formatRange.Font.Bold = true;

                rowCnt++;
                formatRange = oSheet.get_Range("A" + rowCnt + "", "" + lastColumn + "" + rowCnt + "");
                formatRange.Interior.Color = Color.FromArgb(255, 255, 0);
                formatRange.Font.Size = 14;
                formatRange.Font.Bold = true;
                formatRange.Font.Underline = true;
                formatRange = oSheet.get_Range("A" + rowCnt + "");
                formatRange.Value = "TỔNG";

                for (col = 0; col < dtBCThang.Columns.Count - 3; col++) // Bỏ thêm 2 cột ghi chú và lao động cuối kỳ
                {
                    formatRange = oSheet.get_Range("" + CharacterIncrement(col + 1) + "" + rowCnt + "");
                    sRowBD_DV = sRowBD_DV.Substring(0, sRowBD_DV.Length - 2);
                    sRowBD_DV = sRowBD_DV.Replace(@";", CharacterIncrement(col + 1));
                    formatRange.Value = "=" + sRowBD_DV;
                    sRowBD_DV = sRowBD_DV_Temp;
                }

                for (col = 2; col < dtBCThang.Columns.Count - 2; col++)
                {

                    formatRange = oSheet.get_Range(CharacterIncrement(col - 1) + "8", CharacterIncrement(col - 1) + rowCnt.ToString());
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    formatRange.NumberFormat = "0";
                    try { formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                formatRange = oSheet.get_Range("A6", "" + lastColumn + "" + rowCnt + "");
                formatRange.Font.Name = fontName;
                formatRange.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;


                BorderAround(oSheet.get_Range("A3", lastColumn + rowCnt.ToString()));

                rowCnt++;
                rowCnt++;
                formatRange = oSheet.get_Range("K" + rowCnt + "", "" + lastColumn + "" + rowCnt + "");
                formatRange.Merge();
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 12;
                formatRange.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                //formatRange.Value = "Tp.HCM , ngày " + lk_NgayIn.DateTime.Day.ToString() + " tháng " + lk_NgayIn.DateTime.Month.ToString() + " năm " + lk_NgayIn.DateTime.Year.ToString() + "";
                rowCnt++;
                formatRange = oSheet.get_Range("E" + rowCnt + "");
                formatRange.Value = "P.TCLĐ";
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 12;
                formatRange.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                formatRange = oSheet.get_Range("K" + rowCnt + "", "" + lastColumn + "" + rowCnt + "");
                formatRange.Merge();
                formatRange.Font.Bold = true;
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = 12;
                formatRange.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                formatRange.Value = "Tổng giám đốc";


                ////dịnh dạng
                ////Commons.Modules.MExcel.ThemDong(oSheet, Excell.XlInsertShiftDirection.xlShiftDown, 1, 7);

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
                //    try { formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                //}

                ////colKT++;
                ////CurentColumn = CharacterIncrement(colKT);
                ////formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                ////formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                //////formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);
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
                    AccessMode: Excell.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // In Xtrareport
        private void BangChamCongThang_SB()
        {
            frmViewReport frm = new frmViewReport();
            string sTieuDe = "BẢNG CHẤM CÔNG TỔNG HỢP THÁNG " + LK_Thang.EditValue.ToString();
            frm.rpt = new rptBangCongTongHopThang_SB(sTieuDe, Convert.ToDateTime(NgayIn.EditValue), Convert.ToDateTime(lk_TuNgay.EditValue), Convert.ToDateTime(lk_DenNgay.EditValue));
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongThang_MT", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                //DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                adp.Fill(dt);

                //dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);
            }
            catch
            { }
            frm.ShowDialog();
        }
        private void BangChamCongTangCaThang()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangCongTCThang"), conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
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
                Excell.Application oXL;
                Excell._Workbook oWB;
                Excell._Worksheet oSheet;

                oXL = new Excell.Application();
                oXL.Visible = false;

                oWB = (Excell._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excell._Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay) + 1;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);
                string lastColumNgay = string.Empty;
                lastColumNgay = CharacterIncrement(iSoNgay + 7);
                string firstColumTT = string.Empty;
                firstColumTT = CharacterIncrement(iSoNgay + 8);

                Range row2_TieuDe_BaoCao = oSheet.get_Range("A2", lastColumn + "2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THÁNG NGOÀI GIỜ THÁNG " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy");

                Range row5_TieuDe_Format = oSheet.get_Range("A4", lastColumn + "7"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Interior.Color = Color.Yellow;

                Range row5_TieuDe = oSheet.get_Range("A4", "H4");
                row5_TieuDe.Merge();
                row5_TieuDe.Value2 = "Thông tin nhân viên (Staff information)";

                Range row5_TieuDe2 = oSheet.get_Range("I4", lastColumNgay + "4");
                row5_TieuDe2.Merge();
                row5_TieuDe2.Value2 = "Ngày tăng ca (Overtime day)";

                Range row5_TieuDe3 = oSheet.get_Range(firstColumTT + "4", lastColumn + "4");
                row5_TieuDe3.Merge();
                row5_TieuDe3.Value2 = "Thông tin tăng ca tháng (Information about monthly overtime)";

                Range row5_TieuDe_Stt = oSheet.get_Range("A5", "A6");
                row5_TieuDe_Stt.Merge();
                row5_TieuDe_Stt.Value2 = "Stt";
                row5_TieuDe_Stt.ColumnWidth = 6;

                Range row5_TieuDe_Stt_A = oSheet.get_Range("A7");
                row5_TieuDe_Stt_A.Value2 = "No.";

                Range row5_TieuDe_MaSo = oSheet.get_Range("B5", "B6");
                row5_TieuDe_MaSo.Merge();
                row5_TieuDe_MaSo.Value2 = "MSCN";
                row5_TieuDe_MaSo.ColumnWidth = 15;

                Range row5_TieuDe_MS_CN_A = oSheet.get_Range("B7");
                row5_TieuDe_MS_CN_A.Value2 = "Employee code";

                Range row5_TieuDe_HoTen = oSheet.get_Range("C5", "C6");
                row5_TieuDe_HoTen.Merge();
                row5_TieuDe_HoTen.Value2 = "Họ và tên";
                row5_TieuDe_HoTen.ColumnWidth = 30;

                Range row5_TieuDe_HO_TEN_A = oSheet.get_Range("C7");
                row5_TieuDe_HO_TEN_A.Value2 = "Full name";

                Range row5_TieuDe_ChucDanh = oSheet.get_Range("D5", "D6");
                row5_TieuDe_ChucDanh.Merge();
                row5_TieuDe_ChucDanh.Value2 = "Chức vụ";
                row5_TieuDe_ChucDanh.ColumnWidth = 20;

                Range row5_TieuDe_CV_A = oSheet.get_Range("D7");
                row5_TieuDe_CV_A.Value2 = "Position";

                Range row5_TieuDe_BoPhan = oSheet.get_Range("E5", "E6");
                row5_TieuDe_BoPhan.Merge();
                row5_TieuDe_BoPhan.Value2 = "Xưởng/Phòng ban";
                row5_TieuDe_BoPhan.ColumnWidth = 20;

                Range row5_TieuDe_XN_A = oSheet.get_Range("E7");
                row5_TieuDe_XN_A.Value2 = "Workshop/Department";

                Range row5_TieuDe_To = oSheet.get_Range("F5", "F6");
                row5_TieuDe_To.Merge();
                row5_TieuDe_To.Value2 = "Tổ";
                row5_TieuDe_To.ColumnWidth = 20;

                Range row5_TieuDe_T_A = oSheet.get_Range("F7");
                row5_TieuDe_T_A.Value2 = "Team";

                Range row5_TieuDe_NgayTV = oSheet.get_Range("G5", "G6");
                row5_TieuDe_NgayTV.Merge();
                row5_TieuDe_NgayTV.Value2 = "Ngày thử việc";
                row5_TieuDe_NgayTV.ColumnWidth = 12;

                Range row5_TieuDe_NTV_A = oSheet.get_Range("G7");
                row5_TieuDe_NTV_A.Value2 = "Start probation date";

                Range row5_TieuDe_NgayVL = oSheet.get_Range("H5", "H6");
                row5_TieuDe_NgayVL.Merge();
                row5_TieuDe_NgayVL.Value2 = "Ngày vào làm";
                row5_TieuDe_NgayVL.ColumnWidth = 12;

                Range row5_TieuDe_NVL_A = oSheet.get_Range("H7");
                row5_TieuDe_NVL_A.Value2 = "Start working date";

                int col = 9;
                while (iTNgay <= iDNgay)
                {
                    oSheet.Cells[5, col] = iTNgay;
                    oSheet.Cells[7, col] = iTNgay;
                    oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();

                    col++;
                    iTNgay++;
                }

                oSheet.Cells[5, col] = "Tổng số giờ tăng ca (đối với ngày thường)";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 1]].Merge();
                oSheet.Cells[6, col] = "Tăng ca ban ngày";
                oSheet.Cells[7, col] = "Overtime during the day";
                oSheet.Cells[6, col + 1] = "Tăng ca ban đêm";
                oSheet.Cells[7, col + 1] = "Overtime at night";


                col = col + 2;
                oSheet.Cells[5, col] = "Tổng số giờ tăng ca (đối với ngày chủ nhật)";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 1]].Merge();
                oSheet.Cells[6, col] = "Tăng ca ban ngày";
                oSheet.Cells[7, col] = "Overtime during the day";
                oSheet.Cells[6, col + 1] = "Tăng ca ban đêm";
                oSheet.Cells[7, col + 1] = "Overtime at night";


                col = col + 2;
                oSheet.Cells[5, col] = "Tổng số giờ tăng ca (đối với ca đêm)";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 1]].Merge();
                oSheet.Cells[6, col] = "Số giờ ca đêm";
                oSheet.Cells[7, col] = "Night shift hours";
                oSheet.Cells[6, col + 1] = "Tăng ca ca đêm";
                oSheet.Cells[7, col + 1] = "Night shift overtime ";

                col = col + 2;
                oSheet.Cells[5, col] = "Tổng số giờ tăng ca (ngày thường)";
                oSheet.Cells[7, col] = "Total overtime hours (weekdays)";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();

                col = col + 1;
                oSheet.Cells[5, col] = "Tổng số giờ tăng ca (ngày nghỉ)";
                oSheet.Cells[7, col] = "Total overtime hours (Weekend)";
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();

                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                int rowCnt = 0;
                //int redRows = 7;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCThang.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }

                    rowCnt++;
                }
                rowCnt = rowCnt + 6;
                oSheet.get_Range("A8", lastColumn + rowCnt.ToString()).Value2 = rowData;
                Excell.Range formatRange;
                formatRange = oSheet.get_Range("G8", "G" + rowCnt.ToString());
                formatRange.NumberFormat = "dd/MM/yyyy";
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);
                formatRange = oSheet.get_Range("H8", "H" + rowCnt.ToString());
                formatRange.NumberFormat = "dd/MM/yyyy";
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);

                string CurentColumn = string.Empty;
                for (col = 8; col < dtBCThang.Columns.Count; col++)
                {
                    CurentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                    formatRange.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                    formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);
                }
                ////Kẻ khung toàn bộ
                formatRange = oSheet.get_Range("A8", lastColumn + rowCnt.ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                BorderAround(oSheet.get_Range("A4", lastColumn + rowCnt.ToString()));

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excell.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BangChamCongTangCaThang_MT()
        {
            frmViewReport frm = new frmViewReport();
            string sTieuDe = Commons.Modules.ObjLanguages.GetLanguage("rptBangCongTangCaThang_MT", "lblTIEU_DE") + " " + LK_Thang.EditValue.ToString(); // BẢNG CÔNG TĂNG CA THÁNG
            frm.rpt = new rptBangCongTangCaThang_MT(sTieuDe, Convert.ToDateTime(NgayIn.EditValue), Convert.ToDateTime(lk_TuNgay.EditValue), Convert.ToDateTime(lk_DenNgay.EditValue));
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangCongThangTangCa_MT"), conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                //DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                adp.Fill(dt);

                //dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);
            }
            catch
            { }
            frm.ShowDialog();
        }
        private void BangChamCongTangCaThang_SB()
        {
            frmViewReport frm = new frmViewReport();
            string sTieuDe = "BẢNG CHẤM CÔNG NGOÀI GIỜ THÁNG " + LK_Thang.EditValue.ToString();
            frm.rpt = new rptBangCongTangCaThang_SB(sTieuDe, Convert.ToDateTime(NgayIn.EditValue), Convert.ToDateTime(lk_TuNgay.EditValue), Convert.ToDateTime(lk_DenNgay.EditValue));
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongThangTangCa_SB", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                //DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                adp.Fill(dt);

                //dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);
            }
            catch
            { }
            frm.ShowDialog();
        }
        private void BangTongHopDiTreVeSomThang()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCGaiDoan;

                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangXacNhanGioQuetThe", conn);


                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangTHDiTreVeSomThang"), conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = rdo_DiTreVeSom.SelectedIndex;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                if (Convert.ToDateTime(lk_TuNgay.EditValue).Month != Convert.ToDateTime(lk_DenNgay.EditValue).Month)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTu ngay den ngay khong hop le"));
                    return;
                }
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCGaiDoan = new DataTable();
                dtBCGaiDoan = ds.Tables[0].Copy();



                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "") return;
                Excell.Application oXL;
                Excell._Workbook oWB;
                Excell._Worksheet oSheet;

                oXL = new Excell.Application();
                oXL.Visible = false;

                oWB = (Excell._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excell._Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 11;
                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay);

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCGaiDoan.Columns.Count - 1);
                Excell.Range row2_TieuDe_BaoCao0 = oSheet.get_Range("A1", lastColumn + "2");
                row2_TieuDe_BaoCao0.Merge();
                row2_TieuDe_BaoCao0.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao0.Font.Name = fontName;
                row2_TieuDe_BaoCao0.Font.Bold = true;
                row2_TieuDe_BaoCao0.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignLeft;
                row2_TieuDe_BaoCao0.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                //=====




                Excell.Range row2_TieuDe_BaoCao = oSheet.get_Range("A3", lastColumn + "3");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                if (rdo_DiTreVeSom.SelectedIndex == 0)
                {
                    row2_TieuDe_BaoCao.Value2 = "BẢNG TỔNG HỢP ĐI TRỄ (" + Convert.ToDateTime(LK_Thang.EditValue).ToString("MM/yyyy") + ")";
                }
                if (rdo_DiTreVeSom.SelectedIndex == 1)
                {
                    row2_TieuDe_BaoCao.Value2 = "BẢNG TỔNG HỢP VỀ SỚM (" + Convert.ToDateTime(LK_Thang.EditValue).ToString("MM/yyyy") + ")";
                }
                else
                {
                    row2_TieuDe_BaoCao.Value2 = "BẢNG TỔNG HỢP ĐI TRỄ, VỀ SỚM (" + Convert.ToDateTime(LK_Thang.EditValue).ToString("MM/yyyy") + ")";
                }
                Excell.Range row5_TieuDe = oSheet.get_Range("A4", "A5");
                row5_TieuDe.Merge();
                row5_TieuDe.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDe.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row5_TieuDe.Font.Name = fontName;
                row5_TieuDe.Font.Bold = true;
                row5_TieuDe.Value2 = "Stt";
                row5_TieuDe.Interior.Color = Color.FromArgb(198, 224, 180);

                //Excell.Range row5_STT = oSheet.get_Range("A6");
                //row5_STT.Value2 = "No.";
                //row5_STT.Font.Name = fontName;
                //row5_STT.Font.Bold = true;
                //row5_STT.Interior.Color = Color.Yellow;

                Excell.Range row5_TieuDe1 = oSheet.get_Range("B4", "B5");
                row5_TieuDe1.Merge();
                row5_TieuDe1.Font.Name = fontName;
                row5_TieuDe1.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDe1.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row5_TieuDe1.Font.Bold = true;
                row5_TieuDe1.Interior.Color = Color.FromArgb(198, 224, 180);
                row5_TieuDe1.Value2 = "Mã số NV";

                //Excell.Range row5_MS = oSheet.get_Range("B6");
                //row5_MS.Value2 = "Employee code";
                //row5_MS.Font.Name = fontName;
                //row5_MS.Font.Bold = true;
                //row5_MS.Interior.Color = Color.Yellow;


                Excell.Range row5_TieuDe2 = oSheet.get_Range("C4", "C5");
                row5_TieuDe2.Merge();
                row5_TieuDe2.Font.Name = fontName;
                row5_TieuDe2.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDe2.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row5_TieuDe2.Font.Bold = true;
                row5_TieuDe2.Interior.Color = Color.FromArgb(198, 224, 180);
                row5_TieuDe2.Value2 = "Họ tên";

                //Excell.Range row5_HT = oSheet.get_Range("C6");
                //row5_HT.Value2 = "Full name";
                //row5_HT.Font.Name = fontName;
                //row5_HT.Font.Bold = true;
                //row5_HT.Interior.Color = Color.Yellow;

                Excell.Range row5_TieuDe3 = oSheet.get_Range("D4", "D5");
                row5_TieuDe3.Merge();
                row5_TieuDe3.Font.Name = fontName;
                row5_TieuDe3.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDe3.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row5_TieuDe3.Font.Bold = true;
                row5_TieuDe3.Interior.Color = Color.FromArgb(198, 224, 180);
                row5_TieuDe3.Value2 = "Xưởng/Phòng ban";

                //Excell.Range row5_XN = oSheet.get_Range("D6");
                //row5_XN.Value2 = "Workshop/Department";
                //row5_XN.Font.Name = fontName;
                //row5_XN.Font.Bold = true;
                //row5_XN.Interior.Color = Color.Yellow;

                Excell.Range row5_TieuDe4 = oSheet.get_Range("E4", "E5");
                row5_TieuDe4.Merge();
                row5_TieuDe4.Font.Name = fontName;
                row5_TieuDe4.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDe4.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row5_TieuDe4.Font.Bold = true;
                row5_TieuDe4.Interior.Color = Color.FromArgb(198, 224, 180);
                row5_TieuDe4.Value2 = "Chuyền/Phòng";

                //Excell.Range row5_TO = oSheet.get_Range("E6");
                //row5_TO.Value2 = "Team";
                //row5_TO.Font.Name = fontName;
                //row5_TO.Font.Bold = true;
                //row5_TO.Interior.Color = Color.Yellow;


                //tô màu
                //Range range = oSheet.get_Range("A" + redRows.ToString(), "J" + redRows.ToString());
                //range.Cells.Interior.Color = System.Drawing.Color.Red;


                Excell.Range formatRange;
                int col = 6;

                while (iTNgay <= iDNgay)
                {
                    oSheet.Cells[4, col] = Convert.ToDateTime(lk_TuNgay.EditValue).AddDays(iTNgay - 1);
                    oSheet.Cells[4, col].Font.Name = fontName;
                    oSheet.Cells[4, col].Font.Bold = true;
                    oSheet.Cells[4, col].Interior.Color = Color.FromArgb(198, 224, 180);
                    oSheet.Cells[4, col].Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignRight;
                    oSheet.Cells[4, col].Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;


                    oSheet.Cells[5, col] = "Đi trễ";
                    //oSheet.Cells[6, col] = "Being late";

                    oSheet.Cells[5, col].Font.Bold = true;
                    //oSheet.Cells[6, col].Font.Bold = true;
                    oSheet.Cells[5, col].Interior.Color = Color.FromArgb(198, 224, 180);
                    //oSheet.Cells[6, col].Interior.Color = Color.Yellow;
                    oSheet.Cells[5, col].Font.Name = fontName;
                    //oSheet.Cells[6, col].Font.Name = fontName;
                    oSheet.Cells[5, col].Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignRight;
                    oSheet.Cells[5, col].Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                    //oSheet.Cells[6, col].Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignRight;
                    //oSheet.Cells[6, col].Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;



                    oSheet.Cells[5, col + 1] = "Về sớm";
                    //oSheet.Cells[6, col + 1] = "Early leave";
                    oSheet.Cells[5, col + 1].Interior.Color = Color.FromArgb(198, 224, 180);
                    //oSheet.Cells[6, col + 1].Interior.Color = Color.Yellow;
                    oSheet.Cells[5, col + 1].Font.Bold = true;
                    //oSheet.Cells[6, col + 1].Font.Bold = true;
                    oSheet.Cells[5, col + 1].Font.Name = fontName;
                    //oSheet.Cells[6, col + 1].Font.Name = fontName;
                    oSheet.Cells[5, col + 1].HorizontalAlignment = Excell.XlHAlign.xlHAlignRight;
                    oSheet.Cells[5, col + 1].VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                    //oSheet.Cells[6, col + 1].HorizontalAlignment = Excell.XlHAlign.xlHAlignRight;
                    //oSheet.Cells[6, col + 1].VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;


                    oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[4, col + 1]].Merge();
                    oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col]].Merge();
                    oSheet.Range[oSheet.Cells[5, col + 1], oSheet.Cells[5, col + 1]].Merge();

                    col = col + 2;
                    iTNgay++;
                }
                oSheet.Cells[4, col] = "Đi trễ";
                oSheet.Cells[4, col].Font.Name = fontName;
                oSheet.Cells[4, col].Font.Bold = true;
                oSheet.Cells[4, col].Interior.Color = Color.FromArgb(198, 224, 180);
                oSheet.Cells[4, col].Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignRight;
                oSheet.Cells[4, col].Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;


                oSheet.Cells[5, col] = "Số lần đi trễ";
                //oSheet.Cells[6, col] = "Number of being late";
                //oSheet.Cells[6, col].Interior.Color = Color.Yellow;
                oSheet.Cells[5, col].Font.Bold = true;
                //oSheet.Cells[6, col].Font.Bold = true;
                oSheet.Cells[5, col].RowHeight = 20;
                oSheet.Cells[5, col].Interior.Color = Color.FromArgb(198, 224, 180);
                oSheet.Cells[5, col].Font.Name = fontName;
                //oSheet.Cells[6, col].Font.Name = fontName;
                oSheet.Cells[5, col].Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignRight;
                oSheet.Cells[5, col].Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                //oSheet.Cells[6, col].Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignRight;
                //oSheet.Cells[6, col].Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;


                oSheet.Cells[5, col + 1] = "Số phút đi trễ";
                //oSheet.Cells[6, col + 1] = "Number of minutes late";
                oSheet.Cells[5, col + 1].Interior.Color = Color.FromArgb(198, 224, 180);
                //oSheet.Cells[6, col + 1].Interior.Color = Color.Yellow;
                oSheet.Cells[5, col + 1].Font.Bold = true;
                //oSheet.Cells[6, col + 1].Font.Bold = true;
                oSheet.Cells[5, col + 1].RowHeight = 20;
                oSheet.Cells[5, col + 1].Font.Name = fontName;
                //oSheet.Cells[6, col + 1].Font.Name = fontName;
                oSheet.Cells[5, col + 1].HorizontalAlignment = Excell.XlHAlign.xlHAlignRight;
                oSheet.Cells[5, col + 1].VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                //oSheet.Cells[6, col + 1].HorizontalAlignment = Excell.XlHAlign.xlHAlignRight;
                //oSheet.Cells[6, col + 1].VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[4, col + 1]].Merge();
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col]].Merge();
                formatRange = oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col]];
                formatRange.ColumnWidth = 15;

                oSheet.Range[oSheet.Cells[5, col + 1], oSheet.Cells[5, col + 1]].Merge();
                formatRange = oSheet.Range[oSheet.Cells[5, col + 1], oSheet.Cells[5, col + 1]];
                formatRange.ColumnWidth = 15;


                col = col + 2;
                oSheet.Cells[4, col] = "Về sớm";
                oSheet.Cells[4, col].Font.Name = fontName;
                oSheet.Cells[4, col].Font.Bold = true;

                oSheet.Cells[4, col].Interior.Color = Color.FromArgb(198, 224, 180);
                oSheet.Cells[4, col].Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignRight;
                oSheet.Cells[4, col].Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;


                oSheet.Cells[5, col] = "Số lần Về sớm";
                //oSheet.Cells[6, col] = "Number of early leave";
                oSheet.Cells[5, col].Font.Bold = true;
                //oSheet.Cells[6, col].Font.Bold = true;
                oSheet.Cells[5, col].Interior.Color = Color.FromArgb(198, 224, 180);
                //oSheet.Cells[6, col].Interior.Color = Color.Yellow;

                oSheet.Cells[5, col].RowHeight = 20;
                oSheet.Cells[5, col].Font.Name = fontName;
                oSheet.Cells[5, col].Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignRight;
                oSheet.Cells[5, col].Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                //oSheet.Cells[6, col].Font.Name = fontName;
                //oSheet.Cells[6, col].Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignRight;
                //oSheet.Cells[6, col].Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;



                oSheet.Cells[5, col + 1] = "Số phút Về sớm";
                //oSheet.Cells[6, col + 1] = "Minutes leaving early";
                oSheet.Cells[5, col + 1].Interior.Color = Color.FromArgb(198, 224, 180);
                //oSheet.Cells[6, col + 1].Interior.Color = Color.Yellow;
                oSheet.Cells[5, col + 1].RowHeight = 20;
                oSheet.Cells[5, col + 1].Font.Bold = true;
                oSheet.Cells[5, col + 1].Font.Name = fontName;
                oSheet.Cells[5, col + 1].HorizontalAlignment = Excell.XlHAlign.xlHAlignRight;
                oSheet.Cells[5, col + 1].VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                //oSheet.Cells[6, col + 1].Font.Bold = true;
                //oSheet.Cells[6, col + 1].Font.Name = fontName;
                //oSheet.Cells[6, col + 1].HorizontalAlignment = Excell.XlHAlign.xlHAlignRight;
                //oSheet.Cells[6, col + 1].VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;


                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[4, col + 1]].Merge();
                oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col]].Merge();
                formatRange = oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col]];
                formatRange.ColumnWidth = 15;


                oSheet.Range[oSheet.Cells[5, col + 1], oSheet.Cells[5, col + 1]].Merge();
                formatRange = oSheet.Range[oSheet.Cells[5, col + 1], oSheet.Cells[5, col + 1]];
                formatRange.ColumnWidth = 15;


                col = col + 2;

                oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]].Merge();
                oSheet.Cells[4, col] = "Tổng số lần";
                //oSheet.Cells[6, col] = "Total number of times";
                oSheet.Cells[4, col].Font.Name = fontName;
                oSheet.Cells[4, col].Font.Bold = true;
                //oSheet.Cells[6, col].Font.Name = fontName;
                //oSheet.Cells[6, col].Font.Bold = true;
                oSheet.Cells[4, col].Interior.Color = Color.FromArgb(198, 224, 180);
                //oSheet.Cells[6, col].Interior.Color = Color.Yellow;
                oSheet.Cells[4, col].Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignRight;
                oSheet.Cells[4, col].Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                //oSheet.Cells[6, col].Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignRight;
                //oSheet.Cells[6, col].Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                formatRange = oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[5, col]];
                formatRange.ColumnWidth = 15;

                oSheet.Range[oSheet.Cells[4, col + 1], oSheet.Cells[5, col + 1]].Merge();
                oSheet.Cells[4, col + 1] = "Tổng số phút";
                //oSheet.Cells[6, col + 1] = "Total minutes";
                oSheet.Cells[4, col + 1].Font.Name = fontName;
                oSheet.Cells[4, col + 1].Font.Bold = true;
                //oSheet.Cells[6, col + 1].Font.Name = fontName;
                //oSheet.Cells[6, col + 1].Font.Bold = true;
                oSheet.Cells[4, col + 1].Interior.Color = Color.FromArgb(198, 224, 180);
                //oSheet.Cells[6, col + 1].Interior.Color = Color.Yellow;
                oSheet.Cells[4, col + 1].Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignRight;
                oSheet.Cells[4, col + 1].Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                //oSheet.Cells[6, col + 1].Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignRight;
                //oSheet.Cells[6, col + 1].Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                formatRange = oSheet.Range[oSheet.Cells[4, col + 1], oSheet.Cells[5, col + 1]];
                formatRange.ColumnWidth = 15;

                oSheet.Range[oSheet.Cells[4, col + 1], oSheet.Cells[5, col + 1]].Merge();





                DataRow[] dr = dtBCGaiDoan.Select();
                string[,] rowData = new string[dr.Length, dtBCGaiDoan.Columns.Count];

                int rowCnt = 0;
                //int redRows = 7;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCGaiDoan.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }

                    rowCnt++;
                }
                oSheet.get_Range("A6", lastColumn + (rowCnt + 5).ToString()).Value2 = rowData;
                rowCnt = rowCnt + 7;
                string CurentColumn = string.Empty;
                for (col = 5; col < dtBCGaiDoan.Columns.Count; col++)
                {
                    CurentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(CurentColumn + "6", CurentColumn + rowCnt.ToString());
                    formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                    try { formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                //Range row5_TieuDe_Format = oSheet.get_Range("A4", lastColumn + "6"); //27 + 31
                //row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                //row5_TieuDe_Format.Font.Name = fontName;
                //row5_TieuDe_Format.Font.Bold = true;
                //row5_TieuDe_Format.WrapText = true;
                //row5_TieuDe_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                //row5_TieuDe_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                //row5_TieuDe_Format.Interior.Color = Color.Yellow;

                ////Kẻ khung toàn bộ
                formatRange = oSheet.get_Range("A4", "BU" + rowCnt.ToString());
                formatRange.Borders.Color = Color.Black;
                //dữ liệu
                formatRange = oSheet.get_Range("A6", lastColumn + rowCnt.ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                //stt

                formatRange = oSheet.get_Range("A6", "A" + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange.ColumnWidth = 5;
                //ma nv
                formatRange = oSheet.get_Range("B6", "B" + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignLeft;
                formatRange.ColumnWidth = 15;
                //ho ten
                formatRange = oSheet.get_Range("C6", "C" + (rowCnt + 5).ToString());
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignLeft;
                formatRange.ColumnWidth = 35;
                //xí nghiệp
                formatRange = oSheet.get_Range("D6", "D" + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignLeft;
                formatRange.ColumnWidth = 20;
                //tổ
                formatRange = oSheet.get_Range("E6", "E" + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignLeft;
                formatRange.ColumnWidth = 20;

                //CẠNH giữ côt động
                formatRange = oSheet.get_Range("F4", lastColumn + "5");
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;

                if (rdo_DiTreVeSom.SelectedIndex == 0)
                {
                    oWB.SaveAs(SaveExcelFile,
                AccessMode: Excell.XlSaveAsAccessMode.xlShared);
                }
                if (rdo_DiTreVeSom.SelectedIndex == 1)
                {
                    oWB.SaveAs(SaveExcelFile,
                  AccessMode: Excell.XlSaveAsAccessMode.xlShared);
                }
                else
                {
                    oWB.SaveAs(SaveExcelFile,
                  AccessMode: Excell.XlSaveAsAccessMode.xlShared);
                }

                oXL.Visible = true;

            }
            catch (Exception ex)
            {

            }
        }
        private void BangTongHopDiTreVeSomThang_MT()
        {

        }
        private void BangTongHopCongThang()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangXacNhanGioQuetThe", conn);


                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, sKyHieuDV == "DM" ? "rptBangTongCongThang_DM" : "rptBangTongCongThang_DM"), conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                if (Convert.ToDateTime(lk_TuNgay.EditValue).Month != Convert.ToDateTime(lk_DenNgay.EditValue).Month)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTu ngay den ngay khong hop le"));
                    return;
                }
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();

                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }
                Excell.Application oXL;
                Excell._Workbook oWB;
                Excell._Worksheet oSheet;

                oXL = new Excell.Application();
                oXL.Visible = true;

                oWB = (Excell._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excell._Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;

                int TotalColumn = 23;


                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(TotalColumn - 1);

                Excell.Range row2_TieuDe_BaoCao0 = oSheet.get_Range("A1", lastColumn + "2");
                row2_TieuDe_BaoCao0.Merge();
                row2_TieuDe_BaoCao0.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao0.Font.Name = fontName;
                row2_TieuDe_BaoCao0.Font.Bold = true;
                row2_TieuDe_BaoCao0.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao0.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao0.Value2 = "BÁO CÁO LAO ĐỘNG THÁNG (" + Convert.ToDateTime(LK_Thang.EditValue).ToString("MM/yyyy") + ")";

                //=====

                Excell.Range row3_TieuDe_BaoCao = oSheet.get_Range("A3", lastColumn + "3");
                row3_TieuDe_BaoCao.Merge();
                row3_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                row3_TieuDe_BaoCao.Font.Name = fontName;
                row3_TieuDe_BaoCao.Font.Bold = true;
                row3_TieuDe_BaoCao.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row3_TieuDe_BaoCao.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row3_TieuDe_BaoCao.Value2 = "Công trong tháng (" + Convert.ToInt16((Convert.ToDateTime(LK_Thang.EditValue).AddMonths(1).AddDays(-1)).Day) + ")";

                Range row5_TieuDe_Format = oSheet.get_Range("A5", lastColumn + "6"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Interior.Color = Color.Yellow;

                oSheet.get_Range("A6").RowHeight = 40;
                Excell.Range row5_TieuDe_Cot1 = oSheet.get_Range("A5", "A6");
                row5_TieuDe_Cot1.Merge();
                row5_TieuDe_Cot1.Value2 = "Stt";
                row5_TieuDe_Cot1.ColumnWidth = 8;

                Excell.Range row5_TieuDe_Cot2 = oSheet.get_Range("B5", "B6");
                row5_TieuDe_Cot2.Merge();
                row5_TieuDe_Cot2.Value2 = "Xí nghiệp/P.ban";
                row5_TieuDe_Cot2.ColumnWidth = 20;

                Excell.Range row5_TieuDe_Cot3 = oSheet.get_Range("C5", "C6");
                row5_TieuDe_Cot3.Merge();
                row5_TieuDe_Cot3.Value2 = "Chuyền/Phòng";
                row5_TieuDe_Cot3.ColumnWidth = 20;

                Excell.Range row5_TieuDe_Cot4 = oSheet.get_Range("D5", "D6");
                row5_TieuDe_Cot4.Merge();
                row5_TieuDe_Cot4.Value2 = "Công trong tháng";
                row5_TieuDe_Cot4.ColumnWidth = 8;

                Excell.Range row5_TieuDe_Cot5 = oSheet.get_Range("E5", "E6");
                row5_TieuDe_Cot5.Merge();
                row5_TieuDe_Cot5.Value2 = "LĐ T.tế";
                row5_TieuDe_Cot5.ColumnWidth = 8;

                Excell.Range row5_TieuDe_Cot6 = oSheet.get_Range("F5", "F6");
                row5_TieuDe_Cot6.Merge();
                row5_TieuDe_Cot6.Value2 = "LĐ BQ";
                row5_TieuDe_Cot6.ColumnWidth = 8;

                Excell.Range row5_TieuDe_Cot79 = oSheet.get_Range("G5", "I5");
                row5_TieuDe_Cot79.Merge();
                row5_TieuDe_Cot79.Value2 = "Lao động tăng";
                Excell.Range row5_TieuDe_Cot7 = oSheet.get_Range("G6", "G6");
                row5_TieuDe_Cot7.ColumnWidth = 6;
                row5_TieuDe_Cot7.Value2 = "+";
                Excell.Range row5_TieuDe_Cot8 = oSheet.get_Range("H6", "H6");
                row5_TieuDe_Cot8.ColumnWidth = 6;
                row5_TieuDe_Cot8.Value2 = "CN";
                Excell.Range row5_TieuDe_Cot9 = oSheet.get_Range("I6", "I6");
                row5_TieuDe_Cot9.ColumnWidth = 6;
                row5_TieuDe_Cot9.Value2 = "Đào tạo";

                Excell.Range row5_TieuDe_Cot1012 = oSheet.get_Range("J5", "L5");
                row5_TieuDe_Cot1012.Merge();
                row5_TieuDe_Cot1012.Value2 = "Lao động giảm";
                Excell.Range row5_TieuDe_Cot10 = oSheet.get_Range("J6");
                row5_TieuDe_Cot10.ColumnWidth = 6;
                row5_TieuDe_Cot10.Value2 = "+";
                Excell.Range row5_TieuDe_Cot11 = oSheet.get_Range("K6");
                row5_TieuDe_Cot11.ColumnWidth = 6;
                row5_TieuDe_Cot11.Value2 = "BV";
                Excell.Range row5_TieuDe_Cot12 = oSheet.get_Range("L6");
                row5_TieuDe_Cot12.ColumnWidth = 6;
                row5_TieuDe_Cot12.Value2 = "NV";

                Excell.Range row5_TieuDe_Cot13 = oSheet.get_Range("M5", "M6");
                row5_TieuDe_Cot13.Merge();
                row5_TieuDe_Cot13.ColumnWidth = 8;
                row5_TieuDe_Cot13.Value2 = "Công chế độ";

                Excell.Range row5_TieuDe_Cot1417 = oSheet.get_Range("N5", "Q5");
                row5_TieuDe_Cot1417.Merge();
                row5_TieuDe_Cot1417.Value2 = "Công thực tế ngoài giờ";
                Excell.Range row5_TieuDe_Cot14 = oSheet.get_Range("N6");
                row5_TieuDe_Cot14.ColumnWidth = 8;
                row5_TieuDe_Cot14.Value2 = "Trong giờ";
                Excell.Range row5_TieuDe_Cot15 = oSheet.get_Range("O6");
                row5_TieuDe_Cot15.ColumnWidth = 8;
                row5_TieuDe_Cot15.Value2 = "1,5";
                Excell.Range row5_TieuDe_Cot16 = oSheet.get_Range("P6");
                row5_TieuDe_Cot16.ColumnWidth = 8;
                row5_TieuDe_Cot16.Value2 = "2";
                Excell.Range row5_TieuDe_Cot17 = oSheet.get_Range("Q6");
                row5_TieuDe_Cot17.ColumnWidth = 8;
                row5_TieuDe_Cot17.Value2 = "+";

                Excell.Range row5_TieuDe_Cot18 = oSheet.get_Range("R5", "R6");
                row5_TieuDe_Cot18.Merge();
                row5_TieuDe_Cot18.ColumnWidth = 8;
                row5_TieuDe_Cot18.Value2 = "% Công thực tế so công chế độ";

                Excell.Range row5_TieuDe_Cot1926 = oSheet.get_Range("S5", "W5");
                row5_TieuDe_Cot1926.Merge();
                row5_TieuDe_Cot1926.Value2 = "Các loại công vắng mặt";
                Excell.Range row5_TieuDe_Cot19 = oSheet.get_Range("S6");
                row5_TieuDe_Cot19.Value2 = "+";
                Excell.Range row5_TieuDe_Cot20 = oSheet.get_Range("T6");
                row5_TieuDe_Cot20.Value2 = "HL";
                Excell.Range row5_TieuDe_Cot21 = oSheet.get_Range("U6");
                row5_TieuDe_Cot21.Value2 = "KL";
                Excell.Range row5_TieuDe_Cot22 = oSheet.get_Range("V6");
                row5_TieuDe_Cot22.Value2 = "O";
                Excell.Range row5_TieuDe_Cot23 = oSheet.get_Range("W6");
                row5_TieuDe_Cot23.Value2 = "P";

                Excell.Range formatRange;
                formatRange = oSheet.get_Range("S5", "W5");
                formatRange.ColumnWidth = 6;


                DataRow[] dr = dtBCThang.Select();
                int sDonVi = 0;
                int rowCnt = 7;
                int dem = 1;
                foreach (DataRow row in dr)
                {
                    if (Convert.ToInt32(row["ID_DV"].ToString()) != sDonVi)
                    {
                        Excell.Range row_DonVi = oSheet.get_Range("B" + rowCnt, "C" + rowCnt);
                        row_DonVi.Merge();
                        row_DonVi.Value2 = row["TEN_DV"].ToString();
                        rowCnt++;
                    }

                    Excell.Range row_A = oSheet.get_Range("A" + rowCnt);
                    row_A.Value2 = dem;
                    Excell.Range row_B = oSheet.get_Range("B" + rowCnt);
                    row_B.Value2 = row["TEN_XN"].ToString();
                    Excell.Range row_C = oSheet.get_Range("C" + rowCnt);
                    row_C.Value2 = row["TEN_TO"].ToString();
                    Excell.Range row_D = oSheet.get_Range("D" + rowCnt);
                    row_D.Value2 = row["CONG_CHUAN"].ToString();
                    Excell.Range row_E = oSheet.get_Range("E" + rowCnt);
                    row_E.Value2 = row["LDTT"].ToString();
                    Excell.Range row_F = oSheet.get_Range("F" + rowCnt);
                    row_F.Value2 = "=M" + rowCnt + "/D" + rowCnt;
                    Excell.Range row_G = oSheet.get_Range("G" + rowCnt);
                    row_G.Value2 = "=SUM(H" + rowCnt + ":I" + rowCnt + ")";
                    Excell.Range row_H = oSheet.get_Range("H" + rowCnt);
                    row_H.Value2 = row["LD_TANG_CN"].ToString();
                    Excell.Range row_I = oSheet.get_Range("I" + rowCnt);
                    row_I.Value2 = row["LD_TANG_DT"].ToString();
                    Excell.Range row_J = oSheet.get_Range("J" + rowCnt);
                    row_J.Value2 = "=SUM(K" + rowCnt + ":L" + rowCnt + ")";
                    Excell.Range row_K = oSheet.get_Range("K" + rowCnt);
                    row_K.Value2 = row["LD_GIAM_BV"].ToString();
                    Excell.Range row_L = oSheet.get_Range("L" + rowCnt);
                    row_L.Value2 = row["LD_GIAM_NV"].ToString();
                    Excell.Range row_M = oSheet.get_Range("M" + rowCnt);
                    row_M.Value2 = "=N" + rowCnt + "+S" + rowCnt;
                    Excell.Range row_N = oSheet.get_Range("N" + rowCnt);
                    row_N.Value2 = row["SN_LV"].ToString();
                    Excell.Range row_O = oSheet.get_Range("O" + rowCnt);
                    row_O.Value2 = row["SN_TC_NT"].ToString();
                    Excell.Range row_P = oSheet.get_Range("P" + rowCnt);
                    row_P.Value2 = row["SN_TC_CN"].ToString();
                    Excell.Range row_Q = oSheet.get_Range("Q" + rowCnt);
                    row_Q.Value2 = "=SUM(N" + rowCnt + ":P" + rowCnt + ")";
                    Excell.Range row_R = oSheet.get_Range("R" + rowCnt);
                    row_R.Value2 = "=Q" + rowCnt + "/M" + rowCnt + "*100";
                    Excell.Range row_S = oSheet.get_Range("S" + rowCnt);
                    row_S.Value2 = "=SUM(T" + rowCnt + ":W" + rowCnt + ")"; ;
                    Excell.Range row_T = oSheet.get_Range("T" + rowCnt);
                    row_T.Value2 = row["SNV_HL"].ToString();
                    Excell.Range row_U = oSheet.get_Range("U" + rowCnt);
                    row_U.Value2 = row["SNV_KL"].ToString();
                    Excell.Range row_V = oSheet.get_Range("V" + rowCnt);
                    row_V.Value2 = row["SNV_O"].ToString();
                    Excell.Range row_W = oSheet.get_Range("W" + rowCnt);
                    row_W.Value2 = row["SNV_P"].ToString();

                    dem++;
                    rowCnt++;
                    sDonVi = Convert.ToInt32(row["ID_DV"].ToString());

                }

                //Kẻ khung toàn bộ
                //Excell.Range formatRange;
                rowCnt--;
                formatRange = oSheet.get_Range("A5", lastColumn + rowCnt.ToString());


                formatRange.Borders.Color = Color.Black;

                formatRange = oSheet.get_Range("F7", "F" + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";

                formatRange = oSheet.get_Range("G7", "L" + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";

                formatRange = oSheet.get_Range("M7", lastColumn + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";

                //dữ liệu
                formatRange = oSheet.get_Range("A7", lastColumn + rowCnt.ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;


                ////CẠNH giữ côt động
                formatRange = oSheet.get_Range("F3", lastColumn + "4");
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                oWB.SaveAs(SaveExcelFile,
                AccessMode: Excell.XlSaveAsAccessMode.xlShared);
                oXL.Visible = true;
            }
            catch
            {

            }
        }
        private void BangTongHopCongThang_MT()
        {

        }
        private void BaoCaoNghiBoViecThang()
        {
            try
            {
                frmViewReport frm = new frmViewReport();
                DataTable dt;
                System.Data.SqlClient.SqlConnection conn;
                dt = new DataTable();
                string sTieuDe = "BÁO CÁO NGHỈ BỎ VIỆC";
                frm.rpt = new rptBaoCaoNghiBoViecThang(Convert.ToDateTime(LK_Thang.EditValue), sTieuDe);
                try
                {
                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBaoCaoNghiBoViecThang", conn);
                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                    cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue).ToString("yyyy/MM/dd");
                    cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue).ToString("yyyy/MM/dd");
                    cmd.CommandType = CommandType.StoredProcedure;


                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    dt = new DataTable();
                    dt = ds.Tables[0].Copy();
                    dt.TableName = "DA_TA";
                    frm.AddDataSource(dt);
                    frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                }
                catch
                { }
                frm.ShowDialog();
            }
            catch { }
        }
        private void BaoCaoNghiBoViecThang_MT()
        {

        }
        private void DanhSachChuyenCongTac()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            string sTieuDe = "DANH SÁCH CHUYỂN CÔNG TÁC THÁNG";
            frm.rpt = new rptDSChuyenCongTac(lk_DenNgay.DateTime, sTieuDe, Convert.ToDateTime(NgayIn.EditValue));

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptDanhSachChuyenCongTac"), conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DA_TA";
                frm.AddDataSource(dt);
                frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
            }
            catch
            { }
            frm.ShowDialog();

        }
        private void DanhSachChuyenCongTac_MT()
        {

        }
        private void BangChenhLechTangCaThang()
        {

        }
        private void BangChenhLechTangCaThang_MT()
        {
            frmViewReport frm = new frmViewReport();
            string sTieuDe = "BẢNG CHÊNH LỆCH TĂNG CA ";
            frm.rpt = new rptBCChenhLechTangCa(sTieuDe, Convert.ToDateTime(NgayIn.EditValue), Convert.ToDateTime(lk_TuNgay.EditValue), Convert.ToDateTime(lk_DenNgay.EditValue));
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongChenhLechThang_MT", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                //DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                adp.Fill(dt);

                //dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);
            }
            catch
            { }
            frm.ShowDialog();
        }
        private void DanhSachThang_SB()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDanhSachThang_SB", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();

                DataTable dt_TongGioiTinh_Nu = new DataTable();
                dt_TongGioiTinh_Nu = ds.Tables[1].Copy();

                DataTable dt_TongGioiTinh_Nam = new DataTable();
                dt_TongGioiTinh_Nam = ds.Tables[2].Copy();

                DataTable dtSLTO = new DataTable(); // Lấy số lượng xí nghiệp
                dtSLTO = ds.Tables[3].Copy();
                int slto = Convert.ToInt32(dtSLTO.Rows[0][0]);

                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }
                Excell.Application oXL;
                Excell.Workbook oWB;
                Excell.Worksheet oSheet;
                oXL = new Excell.Application();
                oXL.Visible = false;


                //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                oWB = (Excell.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excell.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 11;
                int fontSizeNoiDung = 11;
                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay) + 1;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 4);
                string lastColumNgay = string.Empty;
                lastColumNgay = CharacterIncrement(iSoNgay + 7);
                string firstColumTT = string.Empty;
                firstColumTT = CharacterIncrement(iSoNgay + 8);

                Range row1_TenDV = oSheet.get_Range("B1");
                row1_TenDV.Merge();
                row1_TenDV.Font.Size = 9;
                row1_TenDV.Font.Name = fontName;
                row1_TenDV.Value2 = dtBCThang.Rows[0]["TEN_DV"];
                row1_TenDV.WrapText = false;
                row1_TenDV.Style.VerticalAlignment = Excell.XlHAlign.xlHAlignCenter;


                Range row1_DiaChiDV = oSheet.get_Range("B2");
                row1_DiaChiDV.Merge();
                row1_DiaChiDV.Font.Size = 9;
                row1_DiaChiDV.Font.Name = fontName;
                row1_DiaChiDV.Font.Italic = true;
                row1_DiaChiDV.Value2 = dtBCThang.Rows[0]["DIA_CHI_DV"];
                row1_DiaChiDV.WrapText = false;
                row1_DiaChiDV.Style.VerticalAlignment = Excell.XlHAlign.xlHAlignCenter;


                Range row1_TieuDe_BaoCao = oSheet.get_Range("H2", "U2");
                row1_TieuDe_BaoCao.Merge();
                row1_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row1_TieuDe_BaoCao.Font.Name = fontName;
                row1_TieuDe_BaoCao.Font.Bold = true;
                row1_TieuDe_BaoCao.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row1_TieuDe_BaoCao.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row1_TieuDe_BaoCao.RowHeight = 15;
                row1_TieuDe_BaoCao.Value2 = "LIST OF WORKER  JUNE  " + Convert.ToDateTime(lk_TuNgay.EditValue).Year + "";
                row1_TieuDe_BaoCao.Font.Color = Color.FromArgb(0, 0, 255);

                Range row2_TieuDe_BaoCao = oSheet.get_Range("H3", "U3");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold Italic";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 15;
                row2_TieuDe_BaoCao.Value2 = "DANH SÁCH NHÂN VIÊN " + Convert.ToDateTime(lk_TuNgay.EditValue).Month + "  NĂM " + Convert.ToDateTime(lk_TuNgay.EditValue).Year + "";
                row2_TieuDe_BaoCao.Font.Color = Color.FromArgb(0, 0, 255);

                Range row2_TieuDe_THANG = oSheet.get_Range("AH2", "AI2");
                row2_TieuDe_THANG.Merge();
                row2_TieuDe_THANG.Value2 = "THÁNG " + Convert.ToDateTime(lk_TuNgay.EditValue).Month + "/" + Convert.ToDateTime(lk_TuNgay.EditValue).Year + "";

                Range row2_TieuDe_Nam = oSheet.get_Range("AD3");
                row2_TieuDe_Nam.Value2 = "Nam";

                // Tinh trong sql
                Range row2_TieuDe_CountNam = oSheet.get_Range("AE3");
                row2_TieuDe_CountNam.Value2 = dt_TongGioiTinh_Nam.Rows.Count == 0 ? 0 : dt_TongGioiTinh_Nam.Rows[0][0];

                Range row2_TieuDe_NU = oSheet.get_Range("AF3");
                row2_TieuDe_NU.Value2 = "Nữ";

                // Tinh trong sql
                Range row2_TieuDe_CountNu = oSheet.get_Range("AG3");
                row2_TieuDe_CountNu.Value2 = dt_TongGioiTinh_Nu.Rows.Count == 0 ? 0 : dt_TongGioiTinh_Nu.Rows[0][0];

                // SUM
                Range row2_TieuDe_Tong = oSheet.get_Range("AI3");
                row2_TieuDe_Tong.Value2 = "=SUM(AE3,AG3)";
                row2_TieuDe_Tong.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row2_TieuDe_Tong.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                //Range row4_TieuDe_Format = oSheet.get_Range("A4", "S4"); //27 + 31
                //row4_TieuDe_Format.Font.Size = fontSizeNoiDung;
                //row4_TieuDe_Format.Font.Name = fontName;
                //row4_TieuDe_Format.Font.Bold = true;
                //row4_TieuDe_Format.WrapText = true;
                //row4_TieuDe_Format.NumberFormat = "@";
                //row4_TieuDe_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                //row4_TieuDe_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                //row4_TieuDe_Format.Font.Color = Color.FromArgb(255, 0, 0);

                Range row5_TieuDe_Format = oSheet.get_Range("A5", "AI5"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Font.Color = Color.FromArgb(0, 0, 255);


                Range row5_TieuDe_STT = oSheet.get_Range("A5");
                row5_TieuDe_STT.Value2 = "No";

                Range row5_TieuDe_HoTen = oSheet.get_Range("B5");
                row5_TieuDe_HoTen.Value2 = "Fullname(Họ tên)";
                row5_TieuDe_HoTen.ColumnWidth = 25;

                Range row5_TieuDe_CODE = oSheet.get_Range("C5");
                row5_TieuDe_CODE.Value2 = "CODE";

                Range row5_TieuDe_gioitinh = oSheet.get_Range("D5");
                row5_TieuDe_gioitinh.Value2 = "Giới tính";

                int col = 5;
                while (iTNgay <= iDNgay)
                {
                    oSheet.Cells[5, col] = iTNgay;
                    //Range row6_b = oSheet.get_Range(oSheet.Cells[6, col + 1]);
                    //row6_b.Value2 = "b";
                    //row6_b.Interior.Color = Color.FromArgb(128, 255, 128);
                    //oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col + 1]];
                    col += 1;
                    iTNgay++;
                }

                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                //int rowCONG = 0; // Row để insert dòng tổng
                //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                int rowBD = 6;
                string cotCN = "";
                string[] TEN_XN = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_TO")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[0].Copy(); // Dữ row count data


                for (int i = 0; i < TEN_XN.Count(); i++)
                {
                    dtBCThang = ds.Tables[0].Copy();
                    dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_TO") == TEN_XN[i]).CopyToDataTable().Copy();
                    DataRow[] dr = dtBCThang.Select();
                    current_dr = dr.Count();
                    string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                    foreach (DataRow row in dr)
                    {
                        for (col = 0; col < dtBCThang.Columns.Count; col++)
                        {
                            if (Convert.ToInt32(row[0]) == 1)
                            {
                                if (row[col].ToString() == "0")
                                {
                                    //cotCN = cotCN + (col + 1) + ",";
                                    cotCN = CharacterIncrement(col);
                                    Range ToMau = oSheet.get_Range("" + cotCN + "5", cotCN + "" + (dt_temp.Rows.Count + 5 + (slto)) + ""); //vi du slxn = 3 , 3 dong ten xi + 3 dong tong cua xi nghiep do nen 3*2
                                    //Range ToMau = oSheet.get_Range("" + cotCN + "5", cotCN + "" + (dt_temp.Rows.Count + 6) + ""); //vi du slxn = 3 , 3 dong ten xi + 3 dong tong cua xi nghiep do nen 3*2
                                    ToMau.Interior.Color = Color.FromArgb(0, 176, 80);
                                    //ToMau.NumberFormat = "#,##0.0;(#,##0.0); ; ";
                                }
                            }
                            rowData[rowCnt, col] = row[col].ToString();
                        }
                        rowCnt++;
                    }
                    if (chanVongDau == "Chan") // Chạy vòng đầu tiên, rowBD_XN = 0, vì nó nằm dòng đầu tiên thì rowBD lúc này sẽ  = 7, các vòng tiếp theo sẽ lấy cái dòng BĐ của + thêm rowBD_XN = 1 vào để không bị nằm đè lên dòng thứ 9
                    {
                        dr_Cu = 0;
                        rowBD_XN = 0;
                        //rowCONG = 0;
                        chanVongDau = "";
                    }
                    else
                    {
                        rowBD_XN = 1;
                    }
                    //rowBD = rowBD + dr_Cu + rowBD_XN + rowCONG;
                    rowBD = rowBD + dr_Cu + rowBD_XN;
                    //rowCnt = rowCnt + 6 + dr_Cu;
                    rowCnt = rowBD + current_dr - 1;


                    // Tạo group tổ
                    Range row_groupTO_Format = oSheet.get_Range("A" + rowBD + "".ToString(), lastColumn + "" + rowBD + "".ToString()); //27 + 31
                    row_groupTO_Format.Font.Color = Color.FromArgb(0, 0, 255);
                    row_groupTO_Format.Font.Name = fontName;
                    row_groupTO_Format.Font.Bold = true;
                    oSheet.Cells[rowBD, 1] = TEN_XN[i].ToString();

                    //Đổ dữ liệu của tổ
                    oSheet.get_Range("A" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;


                    dr_Cu = current_dr;
                    keepRowCnt = rowCnt;
                    rowCnt = 0;
                }

                Excell.Range formatRange;
                rowCnt = keepRowCnt + 2;

                //dịnh dạng
                //Commons.Modules.MExcel.ThemDong(oSheet, Excell.XlInsertShiftDirection.xlShiftDown, 1, 7);

                string CurentColumn = string.Empty;
                int colBD = 5;
                int colKT = dtBCThang.Columns.Count;
                //format

                for (col = colBD; col < dtBCThang.Columns.Count - 3; col++)
                {
                    CurentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    formatRange.NumberFormat = "0.00;-0;;@";
                    try { formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                //colKT++;
                //CurentColumn = CharacterIncrement(colKT);
                //formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                ////formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote);
                ////Kẻ khung toàn bộ
                //int ke_khung = -1;

                //if (dr_Cu < 15)
                //{
                //    ke_khung = 14 - dr_Cu;
                //}
                formatRange = oSheet.get_Range("A6", lastColumn + (rowCnt - 1).ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                BorderAround(oSheet.get_Range("A5", lastColumn + (rowCnt - 1).ToString()));
                // filter
                oSheet.Application.ActiveWindow.SplitColumn = 4;
                oSheet.Application.ActiveWindow.FreezePanes = true;
                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excell.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BaoCaoTongHopThang_DM()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangChamCongThang_DM", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
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
                Excell.Application oXL;
                Excell.Workbook oWB;
                Excell.Worksheet oSheet;
                oXL = new Excell.Application();
                oXL.Visible = false;

                //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                oWB = (Excell.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excell.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 11;
                int fontSizeNoiDung = 11;
                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay) + 1;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);
                string lastColumNgayCT = string.Empty;
                lastColumNgayCT = CharacterIncrement(iSoNgay + 22); // 23 cột đầu là cố định tinh tu 0
                string firstColumTC = string.Empty;
                firstColumTC = CharacterIncrement(iSoNgay + 23); // 23 cột đầu là cố định + 1 sau dòng cuối ngày công thưong tin tu 0

                Range ROWA1 = oSheet.get_Range("A1");
                ROWA1.Font.Size = 9;
                ROWA1.Font.Bold = true;
                ROWA1.Font.Name = fontName;
                ROWA1.Value2 = "P: Nghỉ phép năm";
                ROWA1.WrapText = false;


                Range ROWA2 = oSheet.get_Range("A2");
                ROWA2.Font.Size = 9;
                ROWA2.Font.Bold = true;
                ROWA2.Font.Name = fontName;
                ROWA2.Value2 = "KL: nghỉ không lương (đơn vị Ngày";
                ROWA2.WrapText = false;

                Range ROWA3 = oSheet.get_Range("A3");
                ROWA3.Font.Size = 9;
                ROWA3.Font.Bold = true;
                ROWA3.Font.Name = fontName;
                ROWA3.Value2 = "O: Nghỉ vô lý do (đơn vị Ngày)";
                ROWA3.WrapText = false;

                Range ROWA4 = oSheet.get_Range("A3");
                ROWA4.Font.Size = 9;
                ROWA4.Font.Bold = true;
                ROWA4.Font.Name = fontName;
                ROWA4.Value2 = "HL: Nghỉ có hưởng lương";
                ROWA4.WrapText = false;

                Range ROWD1 = oSheet.get_Range("D1");
                ROWD1.Font.Size = 9;
                ROWD1.Font.Bold = true;
                ROWD1.Font.Name = fontName;
                ROWD1.RowHeight = 24;
                ROWD1.Value2 = "1/2P,4.5: nghỉ nửa ngày phép,làm 4.5 giờ";
                ROWD1.WrapText = false;
                ROWD1.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                ROWD1.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;


                Range ROWD2 = oSheet.get_Range("D2");
                ROWD2.Font.Size = 9;
                ROWD2.Font.Bold = true;
                ROWD2.Font.Name = fontName;
                ROWD2.RowHeight = 24;
                ROWD2.Value2 = "1/2P,5.1: nghỉ nửa ngày phép,làm 5.1giờ";
                ROWD2.WrapText = false;
                ROWD2.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                ROWD2.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                Range ROWD3 = oSheet.get_Range("D3");
                ROWD3.Font.Size = 9;
                ROWD3.Font.Bold = true;
                ROWD3.RowHeight = 24;
                ROWD3.Font.Name = fontName;
                ROWD3.Value2 = "1/2P,5.1: nghỉ nửa ngày phép,làm 5.1giờ";
                ROWD3.WrapText = false;
                ROWD3.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                ROWD3.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                Range ROWD4 = oSheet.get_Range("D4");
                ROWD4.Font.Size = 9;
                ROWD4.Font.Bold = true;
                ROWD4.RowHeight = 24;
                ROWD4.Font.Name = fontName;
                ROWD4.Value2 = "1/2P,5.35: nghỉ nửa ngày phép,làm 5.35 giờ";
                ROWD4.WrapText = false;
                ROWD4.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                ROWD4.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                Range ROWV1 = oSheet.get_Range("V1");
                ROWV1.Font.Size = 9;
                ROWV1.Font.Bold = true;
                ROWV1.Font.Name = fontName;
                ROWV1.Value2 = "BẢNG CHÂM CÔNG";
                ROWV1.WrapText = false;

                Range ROWV2 = oSheet.get_Range("V2");
                ROWV2.Font.Size = 9;
                ROWV2.Font.Bold = true;
                ROWV2.Font.Name = fontName;
                ROWV2.Value2 = "Tháng";
                ROWV2.WrapText = false;

                Range ROWW2 = oSheet.get_Range("W2");
                ROWW2.Font.Size = 9;
                ROWW2.Font.Name = fontName;
                ROWW2.Font.Bold = true;
                ROWW2.Value2 = Convert.ToDateTime(lk_TuNgay.EditValue).Month;
                ROWW2.WrapText = false;

                Range ROWX2 = oSheet.get_Range("X2");
                ROWX2.Font.Size = 9;
                ROWX2.Font.Bold = true;
                ROWX2.Font.Name = fontName;
                ROWX2.Value2 = "Năm";
                ROWX2.WrapText = false;

                Range ROWY2 = oSheet.get_Range("Y2");
                ROWY2.Font.Size = 9;
                ROWY2.Font.Bold = true;
                ROWY2.Font.Name = fontName;
                ROWY2.Value2 = Convert.ToDateTime(lk_TuNgay.EditValue).Year;
                ROWY2.WrapText = false;


                Range row3_TieuDeCT_Format = oSheet.get_Range("X3", lastColumNgayCT + "3"); //27 + 31
                row3_TieuDeCT_Format.Font.Size = 9;
                row3_TieuDeCT_Format.Font.Name = fontName;
                row3_TieuDeCT_Format.Merge();
                row3_TieuDeCT_Format.Font.Bold = true;
                row3_TieuDeCT_Format.WrapText = true;
                row3_TieuDeCT_Format.Value2 = "NGÀY CÔNG THƯỜNG TRONG THÁNG (ĐƠN VỊ TÍNH GIỜ)";
                row3_TieuDeCT_Format.NumberFormat = "@";
                row3_TieuDeCT_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row3_TieuDeCT_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;


                //Cột ngày công thường
                Range row4_TieuDeCT_Format = oSheet.get_Range("X4", lastColumNgayCT + "4"); //27 + 31
                row4_TieuDeCT_Format.Font.Size = 9;
                row4_TieuDeCT_Format.Font.Name = fontName;
                row4_TieuDeCT_Format.NumberFormat = "d";
                row4_TieuDeCT_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row4_TieuDeCT_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                int col = 24;
                bool vongdau = false;
                string sTenCot = "";
                while (iTNgay < iDNgay)
                {
                    if (vongdau == false)
                    {
                        oSheet.Cells[4, col] = Convert.ToDateTime(lk_TuNgay.Text).ToString("MM/dd/yyyy");
                        vongdau = true;
                    }
                    else
                    {
                        sTenCot = CharacterIncrement(col - 2);
                        oSheet.Cells[4, col] = "=" + sTenCot + "4 + 1";
                    }
                    col += 1;
                    iTNgay++;
                }
                sTenCot = CharacterIncrement(col - 2);
                oSheet.Cells[4, col] = "=" + sTenCot + "4 + 1";

                string LastColumn_Temp = "";
                LastColumn_Temp = CharacterIncrement(dtBCThang.Columns.Count - 2);

                Range row3_TieuDeTC_Format = oSheet.get_Range(firstColumTC + "3", LastColumn_Temp + "3"); //27 + 31
                row3_TieuDeTC_Format.Font.Size = 9;
                row3_TieuDeTC_Format.Font.Name = fontName;
                row3_TieuDeTC_Format.Merge();
                row3_TieuDeTC_Format.Font.Bold = true;
                row3_TieuDeTC_Format.WrapText = true;
                row3_TieuDeTC_Format.Value2 = "LÀM THÊM GIỜ (ĐƠN VỊ TÍNH GIỜ)";
                row3_TieuDeTC_Format.NumberFormat = "@";
                row3_TieuDeTC_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row3_TieuDeTC_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row3_TieuDeTC_Format.Interior.Color = Color.FromArgb(248, 203, 173);



                //Cột tăng ca

                Range row4_TieuDeTC_Format = oSheet.get_Range(firstColumTC + "4", LastColumn_Temp + "4"); //27 + 31
                row4_TieuDeTC_Format.Font.Size = 9;
                row4_TieuDeTC_Format.Font.Name = fontName;
                row4_TieuDeTC_Format.NumberFormat = "d";
                row4_TieuDeTC_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row4_TieuDeTC_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;


                col += 1;
                vongdau = false;
                sTenCot = "";
                iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                while (iTNgay < iDNgay)
                {
                    if (vongdau == false)
                    {
                        oSheet.Cells[4, col] = Convert.ToDateTime(lk_TuNgay.Text).ToString("MM/dd/yyyy");
                        vongdau = true;
                    }
                    else
                    {
                        sTenCot = CharacterIncrement(col - 2);
                        oSheet.Cells[4, col] = "=" + sTenCot + "4 + 1";
                    }

                    col += 1;
                    iTNgay++;
                }
                sTenCot = CharacterIncrement(col - 2);
                oSheet.Cells[4, col] = "=" + sTenCot + "4 + 1";


                Range row3Ky_Nhan = oSheet.get_Range(lastColumn + "3");
                row3Ky_Nhan.Value2 = "Ký nhận";
                row3Ky_Nhan.ColumnWidth = 11;
                row3Ky_Nhan.Font.Bold = true;
                row3Ky_Nhan.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row3Ky_Nhan.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;



                Range row5_TieuDe_Format = oSheet.get_Range("A5", "W5"); //27 + 31
                row5_TieuDe_Format.Font.Size = 9;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.RowHeight = 60;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Interior.Color = Color.FromArgb(198, 224, 180);

                Range row6_TieuDe_Format = oSheet.get_Range("A6", "W6"); //27 + 31
                row6_TieuDe_Format.Interior.Color = Color.FromArgb(198, 224, 180);

                Range row5_TieuDe_STT = oSheet.get_Range("A5");
                row5_TieuDe_STT.Value2 = "STT";
                row5_TieuDe_STT.ColumnWidth = 4;
                row5_TieuDe_STT.Interior.Color = Color.FromArgb(255, 255, 255);

                Range row5_TieuDe_A6 = oSheet.get_Range("A6");
                row5_TieuDe_A6.Interior.Color = Color.FromArgb(255, 255, 255);


                Range row5_TieuDe_HoTen = oSheet.get_Range("B5");
                row5_TieuDe_HoTen.Value2 = "Họ tên";
                row5_TieuDe_HoTen.ColumnWidth = 30;


                Range row5_TieuDe_MSCN = oSheet.get_Range("C5");
                row5_TieuDe_MSCN.Value2 = "Mã nhân viên";
                row5_TieuDe_MSCN.ColumnWidth = 15;

                Range row5_TieuDe_BP = oSheet.get_Range("D5");
                row5_TieuDe_BP.Value2 = "Bộ phận";
                row5_TieuDe_BP.ColumnWidth = 12;

                Range row5_TieuDe_PB = oSheet.get_Range("E5");
                row5_TieuDe_PB.Value2 = "Phân bổ";
                row5_TieuDe_PB.ColumnWidth = 12;

                Range row5_TieuDe_CV = oSheet.get_Range("F5");
                row5_TieuDe_CV.Value2 = "Chức vụ";
                row5_TieuDe_CV.ColumnWidth = 15;

                Range row5_TieuDe_TT = oSheet.get_Range("G5");
                row5_TieuDe_TT.Value2 = "Tình trạng";
                row5_TieuDe_TT.ColumnWidth = 15;

                Range row5_TieuDe_GT = oSheet.get_Range("H5");
                row5_TieuDe_GT.Value2 = "Giới tính";
                row5_TieuDe_GT.ColumnWidth = 10;

                Range row5_TieuDe_NL = oSheet.get_Range("I5");
                row5_TieuDe_NL.Value2 = "Ngày vào";
                row5_TieuDe_NL.ColumnWidth = 10;

                Range row5_TieuDe_NKHDLD = oSheet.get_Range("J5");
                row5_TieuDe_NKHDLD.Value2 = "Ngày ký HĐLĐ";
                row5_TieuDe_NKHDLD.ColumnWidth = 10;

                Range row5_TieuDe_NLVC = oSheet.get_Range("K5");
                row5_TieuDe_NLVC.Value2 = "Ngày làm việc cuối cùng";
                row5_TieuDe_NLVC.ColumnWidth = 12;

                Range row5_TieuDe_TGNN = oSheet.get_Range("L5");
                row5_TieuDe_TGNN.Value2 = "Thời gian nghỉ ngắn";
                row5_TieuDe_TGNN.ColumnWidth = 8;
                row5_TieuDe_TGNN.Font.Color = Color.FromArgb(255, 0, 0);


                Range row5_TieuDe_TNCLV = oSheet.get_Range("M5");
                row5_TieuDe_TNCLV.Value2 = "Tổng Ngày công làm việc";
                row5_TieuDe_TNCLV.ColumnWidth = 9;
                row5_TieuDe_TNCLV.Font.Color = Color.FromArgb(255, 0, 0);


                Range row5_TieuDe_CGLVTT = oSheet.get_Range("N5");
                row5_TieuDe_CGLVTT.Value2 = "Giờ công làm việc thực tế";
                row5_TieuDe_CGLVTT.ColumnWidth = 8;

                Range row5_TieuDe_GCCD = oSheet.get_Range("O5");
                row5_TieuDe_GCCD.Value2 = "Giờ công chế độ";
                row5_TieuDe_GCCD.ColumnWidth = 8;

                Range row5_TieuDe_HL = oSheet.get_Range("P5");
                row5_TieuDe_HL.Value2 = "HL";
                row5_TieuDe_HL.ColumnWidth = 8;

                Range row5_TieuDe_KL = oSheet.get_Range("Q5");
                row5_TieuDe_KL.Value2 = "KL";
                row5_TieuDe_KL.ColumnWidth = 6;

                Range row5_TieuDe_O = oSheet.get_Range("R5");
                row5_TieuDe_O.Value2 = "O";
                row5_TieuDe_O.ColumnWidth = 8;

                Range row5_TieuDe_P = oSheet.get_Range("S5");
                row5_TieuDe_P.Value2 = "P";
                row5_TieuDe_P.ColumnWidth = 8;

                Range row5_TieuDe_GLTNT = oSheet.get_Range("T5");
                row5_TieuDe_GLTNT.Value2 = "Giờ làm thêm ngày thường";
                row5_TieuDe_GLTNT.ColumnWidth = 9;

                Range row5_TieuDe_GLTNNHT = oSheet.get_Range("U5");
                row5_TieuDe_GLTNNHT.Value2 = "Giờ làm thêm ngày nghỉ hàng tuần";
                row5_TieuDe_GLTNNHT.ColumnWidth = 9;

                Range row5_TieuDe_V5 = oSheet.get_Range("V5");
                row5_TieuDe_V5.Value2 = "Tăng ca 150% k làm ra SP";
                row5_TieuDe_V5.ColumnWidth = 9;

                Range row5_TieuDe_W5 = oSheet.get_Range("W5");
                row5_TieuDe_W5.Value2 = "Tổng giờ ko làm ra sản phẩm (Offtime)";
                row5_TieuDe_W5.ColumnWidth = 10;

                // Thứ ngày cho cột công thường
                Range row5_TieuDeCT_Format = oSheet.get_Range("X5", lastColumNgayCT + "5"); //27 + 31
                row5_TieuDeCT_Format.Font.Size = 9;
                row5_TieuDeCT_Format.Font.Name = fontName;
                row5_TieuDeCT_Format.Orientation = 90;
                row5_TieuDeCT_Format.ColumnWidth = 5;
                row5_TieuDeCT_Format.NumberFormat = "dd";
                row5_TieuDeCT_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDeCT_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignBottom;

                string sThu = "";
                int col_r5 = 24;
                iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                sTenCot = "";
                while (iTNgay <= iDNgay)
                {
                    DateTime dt = new DateTime(Convert.ToDateTime(lk_TuNgay.EditValue).Year, Convert.ToDateTime(lk_TuNgay.EditValue).Month, iTNgay);
                    sThu = dt.DayOfWeek.ToString();

                    sTenCot = CharacterIncrement(col_r5 - 1);
                    oSheet.Cells[5, col_r5] = "=IF(WEEKDAY(" + sTenCot + "4)=1 " + @" ,""Chủ Nhật"",""Thứ ""& WEEKDAY(" + sTenCot + "4))";
                    if (sThu == "Sunday")
                    {
                        oSheet.Cells[5, col_r5].Interior.Color = Color.FromArgb(255, 204, 204);
                        oSheet.Cells[5, col_r5].Font.Color = Color.FromArgb(156, 0, 6);
                    }
                    col_r5 += 1;
                    iTNgay++;
                }

                //sTenCot = CharacterIncrement(col_r5 - 1);
                //oSheet.Cells[5, col_r5] = "=IF(WEEKDAY(" + sTenCot + "4)=1 " + @" ,""Chủ Nhật"",""Thứ ""& WEEKDAY(" + sTenCot + "4))";



                //Thứ ngày cho cột tăng ca

                Range row5_TieuDeTC_Format = oSheet.get_Range(firstColumTC + "5", LastColumn_Temp + "5"); //27 + 31
                row5_TieuDeTC_Format.Font.Size = 9;
                row5_TieuDeTC_Format.Font.Name = fontName;
                row5_TieuDeTC_Format.Orientation = 90;
                row5_TieuDeTC_Format.ColumnWidth = 5;
                row5_TieuDeTC_Format.NumberFormat = "dd";
                row5_TieuDeTC_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDeTC_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignBottom;

                //col_r5 += 1;
                iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                sTenCot = "";
                while (iTNgay <= iDNgay)
                {
                    DateTime dt = new DateTime(Convert.ToDateTime(lk_TuNgay.EditValue).Year, Convert.ToDateTime(lk_TuNgay.EditValue).Month, iTNgay);
                    sThu = dt.DayOfWeek.ToString();

                    sTenCot = CharacterIncrement(col_r5 - 1);
                    oSheet.Cells[5, col_r5] = "=IF(WEEKDAY(" + sTenCot + "4)=1 " + @" ,""Chủ Nhật"",""Thứ ""& WEEKDAY(" + sTenCot + "4))";
                    if (sThu == "Sunday")
                    {
                        oSheet.Cells[5, col_r5].Interior.Color = Color.FromArgb(255, 204, 204);
                        oSheet.Cells[5, col_r5].Font.Color = Color.FromArgb(156, 0, 6);

                    }
                    col_r5 += 1;
                    iTNgay++;
                }

                //sTenCot = CharacterIncrement(col_r5 - 1);
                //oSheet.Cells[5, col_r5] = "=IF(WEEKDAY(" + sTenCot + "4)=1 " + @" ,""Chủ Nhật"",""Thứ ""& WEEKDAY(" + sTenCot + "4))";


                oSheet.Application.ActiveWindow.SplitColumn = 4;
                oSheet.Application.ActiveWindow.SplitRow = 5;
                oSheet.Application.ActiveWindow.FreezePanes = true;

                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                //int redRows = 7;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCThang.Columns.Count; col++)
                    {
                        //if (col == 10 && row[10].ToString() != "")
                        //{
                        //    sTenCot = CharacterIncrement(6);
                        //    Excell.Range formatRange7;
                        //    formatRange7 = oSheet.get_Range(sTenCot + ((rowCnt + 1) + 6).ToString());
                        //    formatRange7.Interior.Color = Color.FromArgb(255, 204, 204);
                        //    formatRange7.Font.Color = Color.FromArgb(156, 0, 6);
                        //}
                        rowData[rowCnt, col] = row[col].ToString();
                    }

                    rowCnt++;
                }
                rowCnt = rowCnt + 6;
                oSheet.get_Range("A7", lastColumn + rowCnt.ToString()).Value2 = rowData;


                col_r5 = 24;
                sThu = "";
                iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                sTenCot = "";
                while (iTNgay <= iDNgay)
                {
                    DateTime dt = new DateTime(Convert.ToDateTime(lk_TuNgay.EditValue).Year, Convert.ToDateTime(lk_TuNgay.EditValue).Month, iTNgay);
                    sThu = dt.DayOfWeek.ToString();

                    //sTenCot = CharacterIncrement(col_r5 - 1);
                    //oSheet.Cells[5, col_r5] = "=IF(WEEKDAY(" + sTenCot + "4)=1 " + @" ,""Chủ Nhật"",""Thứ ""& WEEKDAY(" + sTenCot + "4))";
                    if (sThu == "Sunday")
                    {
                        sTenCot = CharacterIncrement(col_r5 - 1);
                        Excell.Range formatRange5;
                        formatRange5 = oSheet.get_Range(sTenCot + "7", sTenCot + (rowCnt).ToString());
                        formatRange5.Interior.Color = Color.FromArgb(248, 203, 173);
                        sTenCot = CharacterIncrement(col_r5 - 2);
                        formatRange5 = oSheet.get_Range(sTenCot + "7", sTenCot + (rowCnt).ToString());
                        formatRange5.Interior.Color = Color.FromArgb(248, 203, 173);
                    }
                    col_r5 += 1;
                    iTNgay++;
                }

                //col_r5 += 1;
                iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                while (iTNgay <= iDNgay)
                {
                    DateTime dt = new DateTime(Convert.ToDateTime(lk_TuNgay.EditValue).Year, Convert.ToDateTime(lk_TuNgay.EditValue).Month, iTNgay);
                    sThu = dt.DayOfWeek.ToString();

                    //sTenCot = CharacterIncrement(col_r5 - 1);
                    //oSheet.Cells[5, col_r5] = "=IF(WEEKDAY(" + sTenCot + "4)=1 " + @" ,""Chủ Nhật"",""Thứ ""& WEEKDAY(" + sTenCot + "4))";
                    if (sThu == "Sunday")
                    {
                        sTenCot = CharacterIncrement(col_r5 - 1);
                        Excell.Range formatRange5;
                        formatRange5 = oSheet.get_Range(sTenCot + "7", sTenCot + (rowCnt).ToString());
                        formatRange5.Interior.Color = Color.FromArgb(198, 224, 180);
                        sTenCot = CharacterIncrement(col_r5 - 2);
                        formatRange5 = oSheet.get_Range(sTenCot + "7", sTenCot + (rowCnt).ToString());
                        formatRange5.Interior.Color = Color.FromArgb(198, 224, 180);
                    }
                    col_r5 += 1;
                    iTNgay++;
                }

                Excell.Range formatRange;
                rowCnt++;
                rowCnt++;
                Range rowTONG_CONG = oSheet.get_Range("B" + rowCnt);
                rowTONG_CONG.Value2 = "Tổng";
                rowTONG_CONG.Font.Bold = true;

                for (int colSUM = 12; colSUM < dtBCThang.Columns.Count - 1; colSUM++)
                {
                    oSheet.Cells[rowCnt, colSUM] = "=SUBTOTAL(9," + CellAddress(oSheet, 7, colSUM) + ":" + CellAddress(oSheet, rowCnt - 2, colSUM) + ")";
                    oSheet.Cells[rowCnt, colSUM].Font.Bold = true;
                }

                //dịnh dạng
                //Commons.Modules.MExcel.ThemDong(oSheet, Excell.XlInsertShiftDirection.xlShiftDown, 1, 7);

                rowCnt++;

                string CurentColumn = string.Empty;
                int colBD = 11;
                int colKT = dtBCThang.Columns.Count;


                //format
                for (col = colBD; col < dtBCThang.Columns.Count; col++)
                {
                    CurentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    formatRange.NumberFormat = "0.00;-0;;@";
                    try { formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }



                //Từ stt đến - Chức vụ
                formatRange = oSheet.get_Range("A6", "F" + (rowCnt - 1).ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;


                //Từ STT - Mã số CN
                Excell.Range formatRange3;
                formatRange3 = oSheet.get_Range("A6", "C" + (rowCnt - 1).ToString());
                formatRange3.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange3.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                formatRange3 = oSheet.get_Range("B6", "B" + (rowCnt - 1).ToString());
                formatRange3.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignLeft;
                formatRange3.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                // PHANBO -  đến hết
                Excell.Range formatRange1;
                formatRange1 = oSheet.get_Range("E6", lastColumn + (rowCnt - 1).ToString());
                formatRange1.Font.Name = fontName;
                formatRange1.Font.Size = 9;

                // COT X -  đến hết
                Excell.Range formatRange4;
                formatRange4 = oSheet.get_Range("X6", lastColumn + (rowCnt - 1).ToString());
                formatRange4.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange4.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;


                //object result = myRange.AutoFilter(1,"HO_TEN");

                BorderAround(oSheet.get_Range("X3", lastColumn + "4"));
                BorderAround(oSheet.get_Range("A5", lastColumn + (rowCnt - 1).ToString()));
                Excell.Range myRange = oSheet.get_Range("A5", lastColumn + (rowCnt - 1).ToString());
                myRange.AutoFilter("1", "<>", Excell.XlAutoFilterOperator.xlOr, "", true);

                // filter
                //oSheet.Application.ActiveWindow.SplitColumn = 4;
                //oSheet.Application.ActiveWindow.FreezePanes = true;
                oXL.Visible = true;
                oXL.UserControl = true;
                //oWB.SaveAs("TheSavePath", Excell.XlFileFormat.xlWorkbookDefaul);
                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excell.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string CellAddress(Excell.Worksheet sht, int row, int col)
        {
            return RangeAddress(sht.Cells[row, col]);
        }
        private string RangeAddress(Excell.Range rng)
        {
            object missing = null;
            return rng.get_AddressLocal(false, false, Excell.XlReferenceStyle.xlA1,
                   missing, missing);
        }
        private void BangTongHopCongThang_SB()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangXacNhanGioQuetThe", conn);


                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangTongCongThang_SB", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                if (Convert.ToDateTime(lk_TuNgay.EditValue).Month != Convert.ToDateTime(lk_DenNgay.EditValue).Month)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTu ngay den ngay khong hop le"));
                    return;
                }
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();

                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }
                Excell.Application oXL;
                Excell._Workbook oWB;
                Excell._Worksheet oSheet;

                oXL = new Excell.Application();
                oXL.Visible = false;
                oXL.Cursor = XlMousePointer.xlWait;
                oWB = (Excell._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excell._Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;

                int TotalColumn = 24;


                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(TotalColumn - 1);

                Excell.Range row2_TieuDe_BaoCao0 = oSheet.get_Range("A1", lastColumn + "2");
                row2_TieuDe_BaoCao0.Merge();
                row2_TieuDe_BaoCao0.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao0.Font.Name = fontName;
                row2_TieuDe_BaoCao0.Font.Bold = true;
                row2_TieuDe_BaoCao0.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao0.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao0.Value2 = "BÁO CÁO LAO ĐỘNG THÁNG (" + Convert.ToDateTime(LK_Thang.EditValue).ToString("MM/yyyy") + ")";

                //=====

                Excell.Range row3_TieuDe_BaoCao = oSheet.get_Range("A3", lastColumn + "3");
                row3_TieuDe_BaoCao.Merge();
                row3_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                row3_TieuDe_BaoCao.Font.Name = fontName;
                row3_TieuDe_BaoCao.Font.Bold = true;
                row3_TieuDe_BaoCao.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row3_TieuDe_BaoCao.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row3_TieuDe_BaoCao.Value2 = "Công trong tháng (" + Convert.ToInt16((Convert.ToDateTime(LK_Thang.EditValue).AddMonths(1).AddDays(-1)).Day) + ")";

                Range row5_TieuDe_Format = oSheet.get_Range("A5", lastColumn + "6"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row5_TieuDe_Format.Interior.Color = Color.Yellow;

                oSheet.get_Range("A6").RowHeight = 40;
                Excell.Range row5_TieuDe_Cot1 = oSheet.get_Range("A5", "A6");
                row5_TieuDe_Cot1.Merge();
                row5_TieuDe_Cot1.Value2 = "Stt";
                row5_TieuDe_Cot1.ColumnWidth = 8;

                Excell.Range row5_TieuDe_Cot2 = oSheet.get_Range("B5", "B6");
                row5_TieuDe_Cot2.Merge();
                row5_TieuDe_Cot2.Value2 = "Xí nghiệp/P.ban";
                row5_TieuDe_Cot2.ColumnWidth = 20;

                Excell.Range row5_TieuDe_Cot3 = oSheet.get_Range("C5", "C6");
                row5_TieuDe_Cot3.Merge();
                row5_TieuDe_Cot3.Value2 = "Chuyền/Phòng";
                row5_TieuDe_Cot3.ColumnWidth = 20;

                Excell.Range row5_TieuDe_Cot4 = oSheet.get_Range("D5", "D6");
                row5_TieuDe_Cot4.Merge();
                row5_TieuDe_Cot4.Value2 = "Công trong tháng";
                row5_TieuDe_Cot4.ColumnWidth = 8;

                Excell.Range row5_TieuDe_Cot5 = oSheet.get_Range("E5", "E6");
                row5_TieuDe_Cot5.Merge();
                row5_TieuDe_Cot5.Value2 = "LĐ T.tế";
                row5_TieuDe_Cot5.ColumnWidth = 8;

                Excell.Range row5_TieuDe_Cot6 = oSheet.get_Range("F5", "F6");
                row5_TieuDe_Cot6.Merge();
                row5_TieuDe_Cot6.Value2 = "LĐ BQ";
                row5_TieuDe_Cot6.ColumnWidth = 8;

                Excell.Range row5_TieuDe_Cot79 = oSheet.get_Range("G5", "I5");
                row5_TieuDe_Cot79.Merge();
                row5_TieuDe_Cot79.Value2 = "Lao động tăng";
                Excell.Range row5_TieuDe_Cot7 = oSheet.get_Range("G6", "G6");
                row5_TieuDe_Cot7.ColumnWidth = 6;
                row5_TieuDe_Cot7.Value2 = "+";
                Excell.Range row5_TieuDe_Cot8 = oSheet.get_Range("H6", "H6");
                row5_TieuDe_Cot8.ColumnWidth = 6;
                row5_TieuDe_Cot8.Value2 = "CN";
                Excell.Range row5_TieuDe_Cot9 = oSheet.get_Range("I6", "I6");
                row5_TieuDe_Cot9.ColumnWidth = 6;
                row5_TieuDe_Cot9.Value2 = "Đào tạo";

                Excell.Range row5_TieuDe_Cot1012 = oSheet.get_Range("J5", "L5");
                row5_TieuDe_Cot1012.Merge();
                row5_TieuDe_Cot1012.Value2 = "Lao động giảm";
                Excell.Range row5_TieuDe_Cot10 = oSheet.get_Range("J6");
                row5_TieuDe_Cot10.ColumnWidth = 6;
                row5_TieuDe_Cot10.Value2 = "+";
                Excell.Range row5_TieuDe_Cot11 = oSheet.get_Range("K6");
                row5_TieuDe_Cot11.ColumnWidth = 6;
                row5_TieuDe_Cot11.Value2 = "BV";
                Excell.Range row5_TieuDe_Cot12 = oSheet.get_Range("L6");
                row5_TieuDe_Cot12.ColumnWidth = 6;
                row5_TieuDe_Cot12.Value2 = "NV";

                Excell.Range row5_TieuDe_Cot13 = oSheet.get_Range("M5", "M6");
                row5_TieuDe_Cot13.Merge();
                row5_TieuDe_Cot13.ColumnWidth = 8;
                row5_TieuDe_Cot13.Value2 = "Công chế độ";

                Excell.Range row5_TieuDe_Cot1417 = oSheet.get_Range("N5", "Q5");
                row5_TieuDe_Cot1417.Merge();
                row5_TieuDe_Cot1417.Value2 = "Công thực tế ngoài giờ";
                Excell.Range row5_TieuDe_Cot14 = oSheet.get_Range("N6");
                row5_TieuDe_Cot14.ColumnWidth = 8;
                row5_TieuDe_Cot14.Value2 = "Trong giờ";
                Excell.Range row5_TieuDe_Cot15 = oSheet.get_Range("O6");
                row5_TieuDe_Cot15.ColumnWidth = 8;
                row5_TieuDe_Cot15.Value2 = "1,5";
                Excell.Range row5_TieuDe_Cot16 = oSheet.get_Range("P6");
                row5_TieuDe_Cot16.ColumnWidth = 8;
                row5_TieuDe_Cot16.Value2 = "2";
                Excell.Range row5_TieuDe_Cot17 = oSheet.get_Range("Q6");
                row5_TieuDe_Cot17.ColumnWidth = 8;
                row5_TieuDe_Cot17.Value2 = "+";

                Excell.Range row5_TieuDe_Cot18 = oSheet.get_Range("R5", "R6");
                row5_TieuDe_Cot18.Merge();
                row5_TieuDe_Cot18.ColumnWidth = 8;
                row5_TieuDe_Cot18.Value2 = "% Công thực tế so công chế độ";

                Excell.Range row5_TieuDe_Cot1926 = oSheet.get_Range("S5", "X5");
                row5_TieuDe_Cot1926.Merge();
                row5_TieuDe_Cot1926.Value2 = "Các loại công vắng mặt";
                Excell.Range row5_TieuDe_Cot19 = oSheet.get_Range("S6");
                row5_TieuDe_Cot19.Value2 = "+";
                Excell.Range row5_TieuDe_Cot20 = oSheet.get_Range("T6");
                row5_TieuDe_Cot20.Value2 = "F";
                Excell.Range row5_TieuDe_Cot21 = oSheet.get_Range("U6");
                row5_TieuDe_Cot21.Value2 = "CĐ";
                Excell.Range row5_TieuDe_Cot22 = oSheet.get_Range("V6");
                row5_TieuDe_Cot22.Value2 = "KL";
                Excell.Range row5_TieuDe_Cot23 = oSheet.get_Range("W6");
                row5_TieuDe_Cot23.Value2 = "BHXH";
                Excell.Range row5_TieuDe_Cot24 = oSheet.get_Range("X6");
                row5_TieuDe_Cot24.Value2 = "VLD";

                Excell.Range formatRange;
                formatRange = oSheet.get_Range("S5", "X5");
                formatRange.ColumnWidth = 6;


                DataRow[] dr = dtBCThang.Select();
                int sDonVi = 0;
                int rowCnt = 7;
                int dem = 1;
                foreach (DataRow row in dr)
                {
                    if (Convert.ToInt32(row["ID_DV"].ToString()) != sDonVi)
                    {
                        Excell.Range row_DonVi = oSheet.get_Range("B" + rowCnt, "C" + rowCnt);
                        row_DonVi.Merge();
                        row_DonVi.Value2 = row["TEN_DV"].ToString();
                        rowCnt++;
                    }

                    Excell.Range row_A = oSheet.get_Range("A" + rowCnt);
                    row_A.Value2 = dem;
                    Excell.Range row_B = oSheet.get_Range("B" + rowCnt);
                    row_B.Value2 = row["TEN_XN"].ToString();
                    Excell.Range row_C = oSheet.get_Range("C" + rowCnt);
                    row_C.Value2 = row["TEN_TO"].ToString();
                    Excell.Range row_D = oSheet.get_Range("D" + rowCnt);
                    row_D.Value2 = row["CONG_CHUAN"].ToString();
                    Excell.Range row_E = oSheet.get_Range("E" + rowCnt);
                    row_E.Value2 = row["LDTT"].ToString();
                    Excell.Range row_F = oSheet.get_Range("F" + rowCnt);
                    row_F.Value2 = "=M" + rowCnt + "/D" + rowCnt;
                    Excell.Range row_G = oSheet.get_Range("G" + rowCnt);
                    row_G.Value2 = "=SUM(H" + rowCnt + ":I" + rowCnt + ")";
                    Excell.Range row_H = oSheet.get_Range("H" + rowCnt);
                    row_H.Value2 = row["LD_TANG_CN"].ToString();
                    Excell.Range row_I = oSheet.get_Range("I" + rowCnt);
                    row_I.Value2 = row["LD_TANG_DT"].ToString();
                    Excell.Range row_J = oSheet.get_Range("J" + rowCnt);
                    row_J.Value2 = "=SUM(K" + rowCnt + ":L" + rowCnt + ")";
                    Excell.Range row_K = oSheet.get_Range("K" + rowCnt);
                    row_K.Value2 = row["LD_GIAM_BV"].ToString();
                    Excell.Range row_L = oSheet.get_Range("L" + rowCnt);
                    row_L.Value2 = row["LD_GIAM_NV"].ToString();
                    Excell.Range row_M = oSheet.get_Range("M" + rowCnt);
                    row_M.Value2 = "=N" + rowCnt + "+S" + rowCnt;
                    Excell.Range row_N = oSheet.get_Range("N" + rowCnt);
                    row_N.Value2 = row["SN_LV"].ToString();
                    Excell.Range row_O = oSheet.get_Range("O" + rowCnt);
                    row_O.Value2 = row["SN_TC_NT"].ToString();
                    Excell.Range row_P = oSheet.get_Range("P" + rowCnt);
                    row_P.Value2 = row["SN_TC_CN"].ToString();
                    Excell.Range row_Q = oSheet.get_Range("Q" + rowCnt);
                    row_Q.Value2 = "=SUM(N" + rowCnt + ":P" + rowCnt + ")";
                    Excell.Range row_R = oSheet.get_Range("R" + rowCnt);
                    row_R.Value2 = "=Q" + rowCnt + "/M" + rowCnt + "*100";
                    Excell.Range row_S = oSheet.get_Range("S" + rowCnt);
                    row_S.Value2 = "=SUM(T" + rowCnt + ":X" + rowCnt + ")"; ;
                    Excell.Range row_T = oSheet.get_Range("T" + rowCnt);
                    row_T.Value2 = row["SNV_P"].ToString();
                    Excell.Range row_U = oSheet.get_Range("U" + rowCnt);
                    row_U.Value2 = row["SNV_CD"].ToString();
                    Excell.Range row_V = oSheet.get_Range("V" + rowCnt);
                    row_V.Value2 = row["SNV_KL"].ToString();
                    Excell.Range row_W = oSheet.get_Range("W" + rowCnt);
                    row_W.Value2 = row["SNV_BHXH"].ToString();
                    Excell.Range row_X = oSheet.get_Range("X" + rowCnt);
                    row_X.Value2 = row["SNV_VLD"].ToString();

                    dem++;
                    rowCnt++;
                    sDonVi = Convert.ToInt32(row["ID_DV"].ToString());

                }

                //Kẻ khung toàn bộ
                //Excell.Range formatRange;
                rowCnt--;
                formatRange = oSheet.get_Range("A5", lastColumn + rowCnt.ToString());


                formatRange.Borders.Color = Color.Black;

                formatRange = oSheet.get_Range("F7", "F" + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";

                formatRange = oSheet.get_Range("G7", "L" + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0;(#,##0); ; ";

                formatRange = oSheet.get_Range("M7", lastColumn + rowCnt.ToString());
                formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";

                //dữ liệu
                formatRange = oSheet.get_Range("A7", lastColumn + rowCnt.ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;


                ////CẠNH giữ côt động
                formatRange = oSheet.get_Range("F3", lastColumn + "4");
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                oWB.SaveAs(SaveExcelFile,
                AccessMode: Excell.XlSaveAsAccessMode.xlShared);
                oXL.Visible = true;
            }
            catch
            {

            }
        }
        private void BaoXacNhanCongThang_DM()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCSLXacNhanCongThang_DM", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_TuNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(lk_DenNgay.EditValue);
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
                this.Cursor = Cursors.WaitCursor;
                Excell.Application oXL;
                Excell.Workbook oWB;
                Excell.Worksheet oSheet;
                oXL = new Excell.Application();
                oXL.Visible = false;

                //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                oWB = (Excell.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excell.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 11;
                int fontSizeNoiDung = 11;
                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay) + 1;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);
                string lastColumNgayCT = string.Empty;
                lastColumNgayCT = CharacterIncrement(iSoNgay + 22); // 23 cột đầu là cố định tinh tu 0
                string firstColumTC = string.Empty;
                firstColumTC = CharacterIncrement(iSoNgay + 23); // 23 cột đầu là cố định + 1 sau dòng cuối ngày công thưong tin tu 0

                Range ROWA1 = oSheet.get_Range("A1");
                ROWA1.Font.Size = 9;
                ROWA1.Font.Bold = true;
                ROWA1.Font.Name = fontName;
                ROWA1.Value2 = "Xác nhận công giờ đến: Đ";
                ROWA1.WrapText = false;


                Range ROWA2 = oSheet.get_Range("A2");
                ROWA2.Font.Size = 9;
                ROWA2.Font.Bold = true;
                ROWA2.Font.Name = fontName;
                ROWA2.Value2 = "Xác nhận công giờ về: V";
                ROWA2.WrapText = false;

                Range ROWA3 = oSheet.get_Range("A3");
                ROWA3.Font.Size = 9;
                ROWA3.Font.Bold = true;
                ROWA3.Font.Name = fontName;
                ROWA3.Value2 = "Xác nhận công buổi trưa ăn cơm: T";
                ROWA3.WrapText = false;

                Range ROWA4 = oSheet.get_Range("A4");
                ROWA4.Font.Size = 9;
                ROWA4.Font.Bold = true;
                ROWA4.Font.Name = fontName;
                ROWA4.Value2 = "Quên thẻ,mất thẻ cả ngày: Q";
                ROWA4.WrapText = false;

                Range ROWD1 = oSheet.get_Range("D1");
                ROWD1.Font.Size = 9;
                ROWD1.Font.Bold = true;
                ROWD1.Font.Name = fontName;
                ROWD1.RowHeight = 24;
                ROWD1.Value2 = "11:30";
                ROWD1.WrapText = false;
                ROWD1.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                ROWD1.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                Range ROWD2 = oSheet.get_Range("D2");
                ROWD2.Font.Size = 9;
                ROWD2.Font.Bold = true;
                ROWD2.Font.Name = fontName;
                ROWD2.RowHeight = 24;
                ROWD2.Value2 = "T";
                ROWD2.WrapText = false;
                ROWD2.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                ROWD2.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                Range ROWE1 = oSheet.get_Range("E1");
                ROWE1.Font.Size = 9;
                ROWE1.Font.Bold = true;
                ROWE1.Font.Name = fontName;
                ROWE1.Value2 = "11:45";
                ROWE1.WrapText = false;
                ROWE1.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                ROWE1.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                Range ROWE2 = oSheet.get_Range("E2");
                ROWE2.Font.Size = 9;
                ROWE2.Font.Bold = true;
                ROWE2.Font.Name = fontName;
                ROWE2.RowHeight = 24;
                ROWE2.Value2 = "T";
                ROWE2.WrapText = false;
                ROWE2.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                ROWE2.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                Range ROWF1 = oSheet.get_Range("F1");
                ROWF1.Font.Size = 9;
                ROWF1.Font.Bold = true;
                ROWF1.Font.Name = fontName;
                ROWF1.Value2 = "12:00";
                ROWF1.WrapText = false;
                ROWF1.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                ROWF1.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                Range ROWF2 = oSheet.get_Range("F2");
                ROWF2.Font.Size = 9;
                ROWF2.Font.Bold = true;
                ROWF2.Font.Name = fontName;
                ROWF2.Value2 = "T";
                ROWF2.WrapText = false;
                ROWF2.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                ROWF2.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                Range ROWG1 = oSheet.get_Range("G1");
                ROWG1.Font.Size = 9;
                ROWG1.Font.Bold = true;
                ROWG1.Font.Name = fontName;
                ROWG1.Value2 = "17:36";
                ROWG1.WrapText = false;
                ROWG1.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                ROWG1.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                Range ROWG2 = oSheet.get_Range("G2");
                ROWG2.Font.Size = 9;
                ROWG2.Font.Bold = true;
                ROWG2.Font.Name = fontName;
                ROWG2.Value2 = "V";
                ROWG2.WrapText = false;
                ROWG2.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                ROWG2.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                Range ROWH1 = oSheet.get_Range("H1");
                ROWH1.Font.Size = 9;
                ROWH1.Font.Bold = true;
                ROWH1.Font.Name = fontName;
                ROWH1.Value2 = "07:30";
                ROWH1.WrapText = false;
                ROWH1.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                ROWH1.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                Range ROWH2 = oSheet.get_Range("H2");
                ROWH2.Font.Size = 9;
                ROWH2.Font.Bold = true;
                ROWH2.Font.Name = fontName;
                ROWH2.Value2 = "Đ";
                ROWH2.WrapText = false;
                ROWH2.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                ROWH2.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                Range ROWI1 = oSheet.get_Range("I1");
                ROWI1.Font.Size = 9;
                ROWI1.Font.Bold = true;
                ROWI1.Font.Name = fontName;
                ROWI1.Value2 = "12:15";
                ROWI1.WrapText = false;
                ROWI1.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                ROWI1.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                Range ROWI2 = oSheet.get_Range("I2");
                ROWI2.Font.Size = 9;
                ROWI2.Font.Bold = true;
                ROWI2.Font.Name = fontName;
                ROWI2.Value2 = "T";
                ROWI2.WrapText = false;
                ROWI2.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                ROWI2.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                Range ROWV1 = oSheet.get_Range("V1");
                ROWV1.Font.Size = 9;
                ROWV1.Font.Bold = true;
                ROWV1.Font.Name = fontName;
                ROWV1.Value2 = "BẢNG XÁC NHẬN CÔNG THÁNG";
                ROWV1.WrapText = false;

                Range ROWV2 = oSheet.get_Range("V2");
                ROWV2.Font.Size = 9;
                ROWV2.Font.Bold = true;
                ROWV2.Font.Name = fontName;
                ROWV2.Value2 = "Tháng";
                ROWV2.WrapText = false;

                Range ROWW2 = oSheet.get_Range("W2");
                ROWW2.Font.Size = 9;
                ROWW2.Font.Name = fontName;
                ROWW2.Font.Bold = true;
                ROWW2.Value2 = Convert.ToDateTime(lk_TuNgay.EditValue).Month;
                ROWW2.WrapText = false;

                Range ROWX2 = oSheet.get_Range("X2");
                ROWX2.Font.Size = 9;
                ROWX2.Font.Bold = true;
                ROWX2.Font.Name = fontName;
                ROWX2.Value2 = "Năm";
                ROWX2.WrapText = false;

                Range ROWY2 = oSheet.get_Range("Y2");
                ROWY2.Font.Size = 9;
                ROWY2.Font.Bold = true;
                ROWY2.Font.Name = fontName;
                ROWY2.Value2 = Convert.ToDateTime(lk_TuNgay.EditValue).Year;
                ROWY2.WrapText = false;


                Range ROWtIEUDE = oSheet.get_Range("A5", "K5");
                ROWtIEUDE.Font.Size = fontSizeTieuDe;
                ROWtIEUDE.Font.Bold = true;
                ROWtIEUDE.Font.Name = fontName;
                ROWtIEUDE.WrapText = false;
                ROWtIEUDE.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                ROWtIEUDE.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                Range row5_TieuDe_STT = oSheet.get_Range("A5");
                row5_TieuDe_STT.Value2 = "STT";
                row5_TieuDe_STT.ColumnWidth = 4;

                Range row5_TieuDe_HoTen = oSheet.get_Range("B5");
                row5_TieuDe_HoTen.Value2 = "Mã nhân viên";
                row5_TieuDe_HoTen.ColumnWidth = 16;


                Range row5_TieuDe_MSCN = oSheet.get_Range("C5");
                row5_TieuDe_MSCN.Value2 = "Họ tên";
                row5_TieuDe_MSCN.ColumnWidth = 30;

                Range row5_TieuDe_BP = oSheet.get_Range("D5");
                row5_TieuDe_BP.Value2 = "Bộ phận";
                row5_TieuDe_BP.ColumnWidth = 25;

                Range row5_TieuDe_PB = oSheet.get_Range("E5");
                row5_TieuDe_PB.Value2 = "Ngày vào làm";
                row5_TieuDe_PB.ColumnWidth = 16;

                Range row5_TieuDe_lcv = oSheet.get_Range("F5");
                row5_TieuDe_lcv.Value2 = "Công nhân nhân viên";
                row5_TieuDe_lcv.ColumnWidth = 40;

                Range row5_TieuDe_G = oSheet.get_Range("G5");
                row5_TieuDe_G.Value2 = "Tổng số lần xác nhận";
                row5_TieuDe_G.ColumnWidth = 12;

                Range row5_TieuDe_H = oSheet.get_Range("H5");
                row5_TieuDe_H.Value2 = "Giờ đến";
                row5_TieuDe_H.ColumnWidth = 12;

                Range row5_TieuDe_I = oSheet.get_Range("I5");
                row5_TieuDe_I.Value2 = "Giờ về";
                row5_TieuDe_I.ColumnWidth = 12;

                Range row5_TieuDe_J = oSheet.get_Range("J5");
                row5_TieuDe_J.Value2 = "Giờ đi ăn trưa";
                row5_TieuDe_J.ColumnWidth = 12;

                Range row5_TieuDe_K = oSheet.get_Range("K5");
                row5_TieuDe_K.Value2 = "Quên quẹt thẻ";
                row5_TieuDe_K.ColumnWidth = 12;

                //Cột ngày
                Range row4_TieuDeCT_Format = oSheet.get_Range("L4", lastColumn + "4"); //27 + 31
                row4_TieuDeCT_Format.Font.Size = 9;
                row4_TieuDeCT_Format.Font.Name = fontName;
                row4_TieuDeCT_Format.NumberFormat = "d";
                row4_TieuDeCT_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row4_TieuDeCT_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                int col = 12;
                bool vongdau = false;
                string sTenCot = "";
                while (iTNgay < iDNgay)
                {
                    if (vongdau == false)
                    {
                        oSheet.Cells[4, col] = Convert.ToDateTime(lk_TuNgay.Text).ToString("MM/dd/yyyy");
                        vongdau = true;
                    }
                    else
                    {
                        sTenCot = CharacterIncrement(col - 2);
                        oSheet.Cells[4, col] = "=" + sTenCot + "4 + 1";
                    }
                    col += 1;
                    iTNgay++;
                }
                sTenCot = CharacterIncrement(col - 2);
                oSheet.Cells[4, col] = "=" + sTenCot + "4 + 1";

                // Thứ ngày cho cột công thường
                Range row5_TieuDeCT_Format = oSheet.get_Range("L5", lastColumn + "5"); //27 + 31
                row5_TieuDeCT_Format.Font.Size = 9;
                row5_TieuDeCT_Format.Font.Name = fontName;
                row5_TieuDeCT_Format.Orientation = 90;
                row5_TieuDeCT_Format.ColumnWidth = 5;
                row5_TieuDeCT_Format.NumberFormat = "dd";
                row5_TieuDeCT_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDeCT_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignBottom;

                string sThu = "";
                int col_r5 = 12;
                iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                sTenCot = "";
                while (iTNgay <= iDNgay)
                {
                    DateTime dt = new DateTime(Convert.ToDateTime(lk_TuNgay.EditValue).Year, Convert.ToDateTime(lk_TuNgay.EditValue).Month, iTNgay);
                    sThu = dt.DayOfWeek.ToString();

                    sTenCot = CharacterIncrement(col_r5 - 1);
                    oSheet.Cells[5, col_r5] = "=IF(WEEKDAY(" + sTenCot + "4)=1 " + @" ,""Chủ Nhật"",""Thứ ""& WEEKDAY(" + sTenCot + "4))";
                    if (sThu == "Sunday")
                    {
                        oSheet.Cells[5, col_r5].Interior.Color = Color.FromArgb(255, 204, 204);
                        oSheet.Cells[5, col_r5].Font.Color = Color.FromArgb(156, 0, 6);
                    }
                    if (sThu == "Saturday")
                    {
                        oSheet.Cells[5, col_r5].Interior.Color = Color.FromArgb(255, 204, 204);
                        oSheet.Cells[5, col_r5].Font.Color = Color.FromArgb(156, 0, 6);
                    }
                    col_r5 += 1;
                    iTNgay++;
                }


                oSheet.Application.ActiveWindow.SplitColumn = 4;
                oSheet.Application.ActiveWindow.SplitRow = 5;
                oSheet.Application.ActiveWindow.FreezePanes = true;

                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                DataRow[] dr = dtBCThang.Select();
                string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];

                //int redRows = 7;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCThang.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }

                    rowCnt++;
                }
                rowCnt = rowCnt + 5;
                oSheet.get_Range("A6", lastColumn + rowCnt.ToString()).Value2 = rowData;


                col_r5 = 12;
                sThu = "";
                iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                sTenCot = "";
                while (iTNgay <= iDNgay)
                {
                    DateTime dt = new DateTime(Convert.ToDateTime(lk_TuNgay.EditValue).Year, Convert.ToDateTime(lk_TuNgay.EditValue).Month, iTNgay);
                    sThu = dt.DayOfWeek.ToString();

                    //sTenCot = CharacterIncrement(col_r5 - 1);
                    //oSheet.Cells[5, col_r5] = "=IF(WEEKDAY(" + sTenCot + "4)=1 " + @" ,""Chủ Nhật"",""Thứ ""& WEEKDAY(" + sTenCot + "4))";
                    if (sThu == "Sunday")
                    {
                        sTenCot = CharacterIncrement(col_r5 - 1);
                        Excell.Range formatRange5;
                        formatRange5 = oSheet.get_Range(sTenCot + "6", sTenCot + (rowCnt).ToString());
                        formatRange5.Interior.Color = Color.FromArgb(248, 203, 173);
                        sTenCot = CharacterIncrement(col_r5 - 2);
                        formatRange5 = oSheet.get_Range(sTenCot + "6", sTenCot + (rowCnt).ToString());
                        formatRange5.Interior.Color = Color.FromArgb(248, 203, 173);
                    }
                    col_r5 += 1;
                    iTNgay++;
                }
                Excell.Range formatRange;
                rowCnt++;
                string CurentColumn = string.Empty;
                int colBD = 11;
                int colKT = dtBCThang.Columns.Count;

                formatRange = oSheet.get_Range("B5", lastColumn + (rowCnt + 1).ToString());
                formatRange.Columns.AutoFit();

                //format
                for (col = colBD; col < dtBCThang.Columns.Count; col++)
                {
                    CurentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(CurentColumn + "6", CurentColumn + rowCnt.ToString());
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    formatRange.NumberFormat = "0.00;-0;;@";
                    formatRange.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignBottom;
                    formatRange.WrapText = true;
                    formatRange.ColumnWidth = 8;
                    try { formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                //Từ stt đến - Chức vụ
                formatRange = oSheet.get_Range("A6", "F" + (rowCnt - 1).ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;


                //Từ STT - Mã số CN
                Excell.Range formatRange3;
                formatRange3 = oSheet.get_Range("A6", "B" + (rowCnt - 1).ToString());
                formatRange3.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange3.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                formatRange3 = oSheet.get_Range("C6", "D" + (rowCnt - 1).ToString());
                formatRange3.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignLeft;
                formatRange3.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                formatRange3 = oSheet.get_Range("E6", "E" + (rowCnt - 1).ToString());
                formatRange3.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange3.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                formatRange3 = oSheet.get_Range("F6", "F" + (rowCnt - 1).ToString());
                formatRange3.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignLeft;
                formatRange3.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                formatRange3 = oSheet.get_Range("G6", "K" + (rowCnt - 1).ToString());
                formatRange3.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange3.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                // PHANBO -  đến hết
                Excell.Range formatRange1;
                formatRange1 = oSheet.get_Range("E6", lastColumn + (rowCnt - 1).ToString());
                formatRange1.Font.Name = fontName;
                formatRange1.Font.Size = 9;

                // COT L -  đến hết

                formatRange3 = oSheet.get_Range("L6", lastColumn + (rowCnt - 1).ToString());
                formatRange3.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange3.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                //object result = myRange.AutoFilter(1,"HO_TEN");

                BorderAround(oSheet.get_Range("L4", lastColumn + "4"));
                BorderAround(oSheet.get_Range("D1", "I2"));
                BorderAround(oSheet.get_Range("A5", lastColumn + (rowCnt - 1).ToString()));
                Excell.Range myRange = oSheet.get_Range("A5", lastColumn + (rowCnt - 1).ToString());
                myRange.AutoFilter("1", "<>", Excell.XlAutoFilterOperator.xlOr, "", true);
                this.Cursor = Cursors.Default;
                // filter
                oXL.Visible = true;
                oXL.UserControl = true;
                //oWB.SaveAs("TheSavePath", Excell.XlFileFormat.xlWorkbookDefaul);
                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excell.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void lk_TuNgay_EditValueChanged(object sender, EventArgs e)
        {
            DateTime tungay = Convert.ToDateTime(lk_TuNgay.EditValue);
            lk_DenNgay.EditValue = Convert.ToDateTime(DateTime.DaysInMonth(tungay.Year, tungay.Month) + "/" + tungay.Month + "/" + tungay.Year);
        }
    }
}
