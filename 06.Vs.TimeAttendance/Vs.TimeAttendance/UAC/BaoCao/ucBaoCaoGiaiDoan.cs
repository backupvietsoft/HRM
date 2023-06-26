using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using Vs.Report;
using Excell = Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace Vs.HRM
{
    public partial class ucBaoCaoGiaiDoan : DevExpress.XtraEditors.XtraUserControl
    {
        private string SaveExcelFile;
        string sKyHieuDV = "";
        public string uFontName = "Times New Roman";
        public float uFontSize = 11.25F;
        public ucBaoCaoGiaiDoan()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, windowsUIButton);
            chkInTheoCongNhan.Checked = true;
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
        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "Print":
                    {
                        switch (Commons.Modules.KyHieuDV)
                        {
                            case "MT":
                                {
                                    ChamCongChiTietCN();
                                    break;
                                }
                            case "AP":
                                {
                                    ChamCongChiTietCN_AP();
                                    break;
                                }
                            case "TG":
                                {
                                    ChamCongChiTietCN_TG();
                                    break;
                                }
                            default:
                                ChamCongChiTietCN();
                                break;
                        }

                        break;
                    }
                default:
                    break;
            }
        }

        private void ucBaoCaoGiaiDoan_Load(object sender, EventArgs e)
        {


            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(LK_DON_VI);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI,LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI,LK_XI_NGHIEP,LK_TO);

            sKyHieuDV = Commons.Modules.KyHieuDV;
            //if (sKyHieuDV == "DM")
            //{
            //    rdo_ChonBaoCao.Properties.Items.RemoveAt(0);
            //}
            //else
            //{
            //    rdo_ChonBaoCao.Properties.Items.RemoveAt(6);
            //}

            LoadGrvLydovang();
            DateTime dtTN = DateTime.Today;
            dtTN = dtTN.AddDays(-dtTN.Day + 1);
            DateTime dtDN = dtTN.AddMonths(1);
            dtDN = dtDN.AddDays(-1);
            Commons.OSystems.SetDateEditFormat(lk_TuNgay);
            Commons.OSystems.SetDateEditFormat(lk_DenNgay);

            lk_TuNgay.EditValue = dtTN;
            lk_DenNgay.EditValue = dtDN;
            lk_NgayIn.EditValue = DateTime.Today;

            LoadGrvCongNhan();
            Commons.Modules.sLoad = "";

        }
        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(LK_DON_VI, LK_XI_NGHIEP);
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
            Commons.Modules.sLoad = "";
            LoadGrvCongNhan();
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(LK_DON_VI, LK_XI_NGHIEP, LK_TO);
            Commons.Modules.sLoad = "";
            LoadGrvCongNhan();
        }

        private void LK_TO_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadGrvCongNhan();
        }

        private void grvLydovang_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {

        }

        private void grvLydovang_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboLydoVang.EditValue = grvLydovang.GetFocusedRowCellValue("TEN_CHE_DO").ToString();
            }
            catch { }
        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
            //{
            //    case "rdo_congnhangiaidoan":
            //        {
            //            chkInTheoCongNhan.Enabled = true;
            //            grdCN.Visible = true;
            //            searchControl1.Visible = true;
            //            break;
            //        }

            //    default:
            //        {
            //            chkInTheoCongNhan.Enabled = false;
            //            grdCN.Visible = false;
            //            searchControl1.Visible = false;
            //            break;
            //        }
            //}
        }
        #endregion

        #region function
        private void LoadGrvCongNhan()
        {
            try
            {
                DataTable dtCongNhan = new DataTable();
                dtCongNhan.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongNhanTheoDieuKien", Commons.Modules.UserName, Commons.Modules.TypeLanguage,
                    LK_DON_VI.EditValue, LK_XI_NGHIEP.EditValue, LK_TO.EditValue, lk_TuNgay.EditValue, lk_DenNgay.EditValue, 0));
                if (grdCN.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdCN, grvCN, dtCongNhan, false, false, true, true, true, this.Name);
                    //format grid view Cong nhan
                    grvCN.Columns["ID_CN"].Visible = false;
                    //grvCN.OptionsView.ShowColumnHeaders = false;
                    grvCN.OptionsView.ShowGroupPanel = false;
                    grvCN.OptionsView.ShowFooter = true;
                }
                else
                {
                    grdCN.DataSource = dtCongNhan;
                }
            }
            catch
            {

            }
        }

        private void LoadGrvLydovang()
        {
            try
            {
                DataTable dtLydovang = new DataTable();
                dtLydovang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "rptGetListLY_DO_VANG", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdLydovang, grvLydovang, dtLydovang, false, false, false, true, true, this.Name);
                grvLydovang.Columns["ID_LDV"].Visible = false;
                dtLydovang.Columns["CHON"].ReadOnly = false;
            }
            catch
            {

            }
            grvLydovang.OptionsBehavior.Editable = true;
            grvLydovang.Columns["TEN_CHE_DO"].OptionsColumn.ReadOnly = true;
            grvLydovang.Columns["TEN_LDV"].OptionsColumn.ReadOnly = true;
            //grvLydovang.Columns["CHON"].OptionsColumn.ReadOnly = false;
            grvLydovang.OptionsView.ShowColumnHeaders = false;
            grvLydovang.OptionsSelection.MultiSelect = true;
            // Controls whether multiple cells or rows can be selected
            grvLydovang.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
        }
        #endregion

        #region functionInTheoDV
        private void DSNVDiTreVeSomGiaiDoan()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            frm.rpt = new rptDSDiTreVeSomGiaiDoan(lk_TuNgay.DateTime, lk_DenNgay.DateTime, lk_NgayIn.DateTime);

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptDSDiTreVeSomGiaiDoan"), conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = lk_TuNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_DenNgay.DateTime;
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
            {
            }


            frm.ShowDialog();
        }
        private void DSVangDauGioGiaiDoan()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn1;
            dt = new DataTable();
            frm.rpt = new rptDSVangDauGioGiaiDoan(lk_TuNgay.DateTime, lk_DenNgay.DateTime);

            try
            {
                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptDSVangDauGioGiaiDoan"), conn1);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = lk_TuNgay.DateTime;
                cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = lk_DenNgay.DateTime;
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
            {
            }


            frm.ShowDialog();
        }
        private void DSChamVangGiaiDoan()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            string id_ldv = "";
            try
            {
                DataTable ldv = new DataTable();
                ldv = Commons.Modules.ObjSystems.ConvertDatatable(grvLydovang).AsEnumerable().Where(x => x["CHON"].ToString().ToLower() == "true").CopyToDataTable();


                for (int i = 0; i < ldv.Rows.Count; i++)
                {
                    if (Convert.ToString(ldv.Rows[i]["ID_LDV"]) == "-1")
                    {
                        id_ldv = "-1";
                        break;
                    }
                    else
                    {
                        id_ldv = id_ldv + ", " + Convert.ToString(ldv.Rows[i]["ID_LDV"]);
                    }


                }
                if (id_ldv != "-1")
                {
                    id_ldv = id_ldv.Remove(0, 2);
                }
            }
            catch (Exception ex)
            {
                id_ldv = "-1";
            }


            System.Data.SqlClient.SqlConnection conn2;
            dt = new DataTable();
            frm.rpt = new rptDSChamCongVang(lk_TuNgay.DateTime, lk_DenNgay.DateTime, lk_NgayIn.DateTime);

            try
            {
                conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn2.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptDSChamCongVang"), conn2);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = lk_TuNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_DenNgay.DateTime;
                cmd.Parameters.Add("@LDV", SqlDbType.NVarChar, 50).Value = id_ldv;
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
            {
            }


            frm.ShowDialog();
        }
        private void DSChamCongVangLuyKe()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            string id_ldv = "";
            try
            {
                DataTable ldv = new DataTable();
                ldv = Commons.Modules.ObjSystems.ConvertDatatable(grvLydovang).AsEnumerable().Where(x => x["CHON"].ToString().ToLower() == "true").CopyToDataTable();


                for (int i = 0; i < ldv.Rows.Count; i++)
                {
                    if (Convert.ToString(ldv.Rows[i]["ID_LDV"]) == "-1")
                    {
                        id_ldv = "-1";
                        break;
                    }
                    else
                    {
                        id_ldv = id_ldv + ", " + Convert.ToString(ldv.Rows[i]["ID_LDV"]);
                    }


                }
                if (id_ldv != "-1")
                {
                    id_ldv = id_ldv.Remove(0, 2);
                }
            }
            catch (Exception ex)
            {
                id_ldv = "-1";
            }
            System.Data.SqlClient.SqlConnection conn2;
            dt = new DataTable();
            frm.rpt = new rptDSChamCongVangLuyKe(lk_TuNgay.DateTime, lk_DenNgay.DateTime, lk_NgayIn.DateTime);

            try
            {
                conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn2.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptDSChamCongVangLuyKe"), conn2);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNGAY", SqlDbType.Date).Value = lk_TuNgay.DateTime;
                cmd.Parameters.Add("@DNGAY", SqlDbType.Date).Value = lk_DenNgay.DateTime;
                cmd.Parameters.Add("@LDV", SqlDbType.NVarChar, 50).Value = id_ldv;
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
        private void XacNhanQuetThe()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCGaiDoan;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangXacNhanGioQuetThe"), conn);

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
                dtBCGaiDoan = new DataTable();
                dtBCGaiDoan = ds.Tables[0].Copy();


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
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy");

                Excell.Range row5_TieuDe = oSheet.get_Range("A4", "A5");
                row5_TieuDe.Merge();
                row5_TieuDe.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDe.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row5_TieuDe.Font.Name = fontName;
                row5_TieuDe.Font.Bold = true;
                row5_TieuDe.Value2 = "Stt";
                row5_TieuDe.Interior.Color = Color.Yellow;

                Excell.Range row5_TieuDe1 = oSheet.get_Range("B4", "B5");
                row5_TieuDe1.Merge();
                row5_TieuDe1.Font.Name = fontName;
                row5_TieuDe1.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDe1.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row5_TieuDe1.Font.Bold = true;
                row5_TieuDe1.Interior.Color = Color.Yellow;

                row5_TieuDe1.Value2 = "Mã số NV";

                Excell.Range row5_TieuDe2 = oSheet.get_Range("C4", "C5");
                row5_TieuDe2.Merge();
                row5_TieuDe2.Font.Name = fontName;
                row5_TieuDe2.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDe2.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row5_TieuDe2.Font.Bold = true;
                row5_TieuDe2.Interior.Color = Color.Yellow;
                row5_TieuDe2.Value2 = "Họ tên";



                Excell.Range row5_TieuDe3 = oSheet.get_Range("D4", "D5");
                row5_TieuDe3.Merge();
                row5_TieuDe3.Font.Name = fontName;
                row5_TieuDe3.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDe3.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row5_TieuDe3.Font.Bold = true;
                row5_TieuDe3.Interior.Color = Color.Yellow;
                row5_TieuDe3.Value2 = "Xí nghiệp/P.ban";

                Excell.Range row5_TieuDe4 = oSheet.get_Range("E4", "E5");
                row5_TieuDe4.Merge();
                row5_TieuDe4.Font.Name = fontName;
                row5_TieuDe4.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDe4.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row5_TieuDe4.Font.Bold = true;
                row5_TieuDe4.Interior.Color = Color.Yellow;
                row5_TieuDe4.Value2 = "Tổ";

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
                    oSheet.Cells[4, col].Interior.Color = Color.Yellow;
                    oSheet.Cells[4, col].Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[4, col].Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;


                    oSheet.Cells[5, col] = "Giờ Vào";
                    oSheet.Cells[5, col].Font.Bold = true;
                    oSheet.Cells[5, col].Interior.Color = Color.Yellow;
                    oSheet.Cells[5, col].Font.Name = fontName;
                    oSheet.Cells[5, col].Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[5, col].Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;



                    oSheet.Cells[5, col + 1] = "Giờ ra";
                    oSheet.Cells[5, col + 1].Interior.Color = Color.Yellow;
                    oSheet.Cells[5, col + 1].Font.Bold = true;
                    oSheet.Cells[5, col + 1].Font.Name = fontName;
                    oSheet.Cells[5, col + 1].HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[5, col + 1].VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;


                    oSheet.Range[oSheet.Cells[4, col], oSheet.Cells[4, col + 1]].Merge();
                    oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[5, col]].Merge();
                    oSheet.Range[oSheet.Cells[5, col + 1], oSheet.Cells[5, col + 1]].Merge();

                    col = col + 2;
                    iTNgay++;
                }


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
                rowCnt = rowCnt + 5;
                oSheet.get_Range("A6", lastColumn + rowCnt.ToString()).Value2 = rowData;


                ////Kẻ khung toàn bộ
                formatRange = oSheet.get_Range("A4", lastColumn + rowCnt.ToString());
                formatRange.Borders.Color = Color.Black;
                //dữ liệu
                formatRange = oSheet.get_Range("A6", lastColumn + rowCnt.ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                //stt
                formatRange = oSheet.get_Range("A5", "A" + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange.ColumnWidth = 5;
                //ma nv
                formatRange = oSheet.get_Range("B6", "B" + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignLeft;
                formatRange.ColumnWidth = 15;
                //ho ten
                formatRange = oSheet.get_Range("C5", "C" + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignLeft;
                formatRange.ColumnWidth = 35;
                //xí nghiệp
                formatRange = oSheet.get_Range("D5", "D" + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignLeft;
                formatRange.ColumnWidth = 20;
                //tổ
                formatRange = oSheet.get_Range("E5", "E" + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignLeft;
                formatRange.ColumnWidth = 20;

                //CẠNH giữa côt động
                formatRange = oSheet.get_Range("F4", lastColumn + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excell.XlSaveAsAccessMode.xlExclusive);
                //oWB.SaveAs("D:\\BangCongThang.xlsx",
                //AccessMode: Excell.XlSaveAsAccessMode.xlShared);

            }
            catch (Exception ex)
            {

            }
        }
        private void XacNhanQuetThe_DM()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCGaiDoan;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptBangXacNhanGioQuetThe_DM"), conn);

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
                dtBCGaiDoan = new DataTable();
                dtBCGaiDoan = ds.Tables[0].Copy();


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
                int iSoNgay = (iDNgay - iTNgay);

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCGaiDoan.Columns.Count - 1);

                //=====

                Excell.Range row2_TieuDe_BaoCao = oSheet.get_Range("A1", lastColumn + "1");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                row2_TieuDe_BaoCao.Value2 = "CHI TIẾT CHẤM CÔNG";




                Excell.Range row2_TieuDe_TUNGAY = oSheet.get_Range("A2", lastColumn + "2");
                row2_TieuDe_TUNGAY.Merge();
                row2_TieuDe_TUNGAY.Font.Size = fontSizeTieuDe;
                row2_TieuDe_TUNGAY.Font.Name = fontName;
                row2_TieuDe_TUNGAY.Font.Bold = true;
                row2_TieuDe_TUNGAY.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row2_TieuDe_TUNGAY.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row2_TieuDe_TUNGAY.RowHeight = 30;
                row2_TieuDe_TUNGAY.Value2 = "Từ ngày " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("dd/MM/yyyy") + " đến ngày " + Convert.ToDateTime(lk_DenNgay.EditValue).ToString("dd/MM/yyyy") + "";

                Excell.Range row2_Format_TieuDe = oSheet.get_Range("A3", lastColumn + "3");
                row2_Format_TieuDe.Font.Size = 12;
                row2_Format_TieuDe.Font.Name = fontName;
                row2_Format_TieuDe.Font.Bold = true;
                row2_Format_TieuDe.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row2_Format_TieuDe.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row2_Format_TieuDe.Interior.Color = Color.Yellow;


                Excell.Range row5_TieuDe1 = oSheet.get_Range("A3");
                row5_TieuDe1.Interior.Color = Color.Yellow;
                row5_TieuDe1.Value2 = "Mã số NV";

                Excell.Range row5_TieuDe2 = oSheet.get_Range("B3");
                row5_TieuDe2.Value2 = "Họ tên";


                Excell.Range row5_TieuDe3 = oSheet.get_Range("C3");
                row5_TieuDe3.Value2 = "Phòng ban";

                Excell.Range row5_TieuDe4 = oSheet.get_Range("D3");
                row5_TieuDe4.Value2 = "Chức vụ";

                Excell.Range row5_TieuDe5 = oSheet.get_Range("E3");
                row5_TieuDe5.ColumnWidth = 15;
                row5_TieuDe5.Value2 = "Ngày";

                Excell.Range row5_TieuDe6 = oSheet.get_Range("F3");
                row5_TieuDe6.Value2 = "Thứ";

                //tô màu
                //Range range = oSheet.get_Range("A" + redRows.ToString(), "J" + redRows.ToString());
                //range.Cells.Interior.Color = System.Drawing.Color.Red;


                Excell.Range formatRange;
                int col = 7;
                int colvr = 1;
                while (col < dtBCGaiDoan.Columns.Count)
                {
                    formatRange = oSheet.get_Range("" + CharacterIncrement(col - 1) + "3");
                    formatRange.Merge();
                    formatRange.Value = "Vào " + colvr.ToString();

                    formatRange = oSheet.get_Range("" + CharacterIncrement(col) + "3");
                    formatRange.Merge();
                    formatRange.Value = "Ra " + colvr.ToString();
                    //oSheet.Cells[4, col] = "Vào " + colvr.ToString();
                    //oSheet.Cells[4, col + 1] = "Ra " + colvr.ToString();

                    col = col + 2;
                    colvr++;
                }

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
                rowCnt = rowCnt + 3;
                oSheet.get_Range("A4", lastColumn + rowCnt.ToString()).Value2 = rowData;

                ////Kẻ khung toàn bộ
                BorderAround(oSheet.get_Range("A3", lastColumn + "" + rowCnt + ""));
                //dữ liệu
                formatRange = oSheet.get_Range("A4", lastColumn + rowCnt.ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;

                //stt
                formatRange = oSheet.get_Range("A4", "A" + rowCnt.ToString());
                formatRange.ColumnWidth = 15;
                //ma nv
                formatRange = oSheet.get_Range("B4", "B" + rowCnt.ToString());
                formatRange.ColumnWidth = 35;
                //ho ten
                formatRange = oSheet.get_Range("C4", "C" + rowCnt.ToString());
                formatRange.ColumnWidth = 20;
                //xí nghiệp
                formatRange = oSheet.get_Range("D4", "D" + rowCnt.ToString());
                formatRange.ColumnWidth = 20;
                //tổ
                formatRange = oSheet.get_Range("E4", "E" + rowCnt.ToString());
                formatRange.EntireColumn.NumberFormat = "DD/MM/YYYY";
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignRight;
                try { formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                formatRange.ColumnWidth = 20;

                //CẠNH giữa côt động
                formatRange = oSheet.get_Range("F4", lastColumn + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange.ColumnWidth = 15;

                for (int i = 7; i < dtBCGaiDoan.Columns.Count; i++)
                {
                    formatRange = oSheet.get_Range("" + CharacterIncrement(i - 1) + "4", "" + CharacterIncrement(i - 1) + "" + rowCnt.ToString());
                    formatRange.EntireColumn.NumberFormat = "hh:mm";
                    try { formatRange.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                Excell.Range myRange = oSheet.get_Range("A3", lastColumn + (rowCnt).ToString());
                myRange.AutoFilter("1", "<>", Excell.XlAutoFilterOperator.xlOr, "", true);

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excell.XlSaveAsAccessMode.xlExclusive);
                //oWB.SaveAs("D:\\BangCongThang.xlsx",
                //AccessMode: Excell.XlSaveAsAccessMode.xlShared);

            }
            catch (Exception ex)
            {

            }
        }

        private void ChamCongChiTietCN_AP()
        {
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn2;
            dt = new DataTable();
            try
            {
                conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn2.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptChiTietQuetTheCNGD_AP"), conn2);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = lk_TuNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_DenNgay.DateTime;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                //LOAD BÁO CÁO CỦA 1 CÔNG ANH ĐƯỢC CHỌN
                if (chkInTheoCongNhan.Checked)
                {
                    cmd.Parameters.Add("@CN", SqlDbType.Int).Value = Convert.ToInt32(grvCN.GetFocusedRowCellValue("ID_CN"));
                }
                else
                {
                    //LOAD BÁO CÁO TẤT CẢ CÔNG NHÂN
                    cmd.Parameters.Add("@CN", SqlDbType.Int).Value = -1;
                }
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                DataTable dt1 = new DataTable();

                if (dt.Rows.Count == 0)
                {
                    this.Cursor = Cursors.Default;
                    Commons.Modules.ObjSystems.HideWaitForm();
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
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 11;
                int countSheetName = 1;

                Microsoft.Office.Interop.Excel.Range formatRange;
                Microsoft.Office.Interop.Excel.Range formatRange1;
                foreach (DataRow rowC in dt.Rows)
                {
                    oSheet.Name = rowC[2].ToString();
                    TaoTTChung(oSheet, 1, 2, 1, 7, 0, 0);
                    int oRow = 10;

                    Microsoft.Office.Interop.Excel.Range row4_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[5, 10]];
                    row4_TieuDe_BaoCao.Merge();
                    row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                    row4_TieuDe_BaoCao.Font.Name = fontName;
                    row4_TieuDe_BaoCao.Font.Bold = true;
                    row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row4_TieuDe_BaoCao.RowHeight = 30;
                    row4_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THEO GIAI ĐOẠN";

                    row4_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, 10]];
                    row4_TieuDe_BaoCao.Merge();
                    row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                    row4_TieuDe_BaoCao.Font.Name = fontName;
                    row4_TieuDe_BaoCao.Font.Bold = true;
                    row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row4_TieuDe_BaoCao.RowHeight = 15;
                    row4_TieuDe_BaoCao.Value2 = "Từ ngày : " + lk_TuNgay.DateTime.ToString("dd/MM/yyyy") +  " Đến ngày: " + lk_DenNgay.DateTime.ToString("dd/MM/yyyy");


                    row4_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[8, 1]];
                    row4_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                    row4_TieuDe_BaoCao.Font.Name = fontName;
                    row4_TieuDe_BaoCao.Font.Bold = true;
                    row4_TieuDe_BaoCao.Value2 = "Mã nhân viên : " + rowC[1].ToString();


                    row4_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[8, 5], oSheet.Cells[8, 5]];
                    row4_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                    row4_TieuDe_BaoCao.Font.Name = fontName;
                    row4_TieuDe_BaoCao.Font.Bold = true;
                    row4_TieuDe_BaoCao.Value2 = "Họ tên : " + rowC[2].ToString();

                    row4_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[8, 9], oSheet.Cells[8, 9]];
                    row4_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                    row4_TieuDe_BaoCao.Font.Name = fontName;
                    row4_TieuDe_BaoCao.Font.Bold = true;
                    row4_TieuDe_BaoCao.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblBoPhan") + " : " + rowC[3].ToString();

                    oSheet.Cells[oRow, 1] = "STT";
                    oSheet.Cells[oRow, 1].ColumnWidth = 10;
                    oSheet.Cells[oRow, 2] = "Ngày";
                    oSheet.Cells[oRow, 2].ColumnWidth = 15;
                    oSheet.Cells[oRow, 3] = "Giờ vào";
                    oSheet.Cells[oRow, 3].ColumnWidth = 12;
                    oSheet.Cells[oRow, 4] = "Giờ ra";
                    oSheet.Cells[oRow, 4].ColumnWidth = 12;
                    oSheet.Cells[oRow, 5] = "Giờ LV";
                    oSheet.Cells[oRow, 5].ColumnWidth = 12;
                    oSheet.Cells[oRow, 6] = "TC thường";
                    oSheet.Cells[oRow, 6].ColumnWidth = 12;
                    oSheet.Cells[oRow, 7] = "TC chủ nhật";
                    oSheet.Cells[oRow, 7].ColumnWidth = 12;
                    oSheet.Cells[oRow, 8] = "TC lễ tết";
                    oSheet.Cells[oRow, 8].ColumnWidth = 12;
                    oSheet.Cells[oRow, 9] = "TC đêm";
                    oSheet.Cells[oRow, 9].ColumnWidth = 12;
                    oSheet.Cells[oRow, 10] = "Vắng (P/K/KL)";
                    oSheet.Cells[oRow, 10].ColumnWidth = 12;
                    oSheet.Cells[oRow, 11] = "Nghỉ chế độ";
                    oSheet.Cells[oRow, 11].ColumnWidth = 15;

                    Microsoft.Office.Interop.Excel.Range formatTieuDe = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow, 11]];
                    formatTieuDe.Font.Size = fontSizeNoiDung;
                    formatTieuDe.Font.Name = fontName;
                    formatTieuDe.Font.Bold = true;
                    formatTieuDe.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatTieuDe.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    formatTieuDe.Cells.WrapText = true;

                    int row = 11;
                    dt1 = ds.Tables[1].Copy();
                    dt1 = dt1.AsEnumerable().Where(r => r.Field<Int64>("ID_CN") == Convert.ToInt64(rowC[0])).CopyToDataTable().Copy();
                    foreach (DataRow row2 in dt1.Rows)
                    {
                        dynamic[] arr = { row2["STT"].ToString(), row2["NGAY"].ToString(), row2["GIO_DEN"].ToString(), row2["GIO_VE"].ToString(), row2["GIO_LV"].ToString()
                        , row2["GIO_NT"].ToString(), row2["GIO_CN"].ToString(), row2["GIO_NL"].ToString(), row2["GIO_DEM"].ToString(), row2["LY_DO"].ToString(), row2["NGHI_CHE_DO"].ToString()
                        };

                        Range rowData = oSheet.Range[oSheet.Cells[row, 1], oSheet.Cells[row, 11]];
                        rowData.Font.Size = fontSizeNoiDung;
                        rowData.Font.Name = fontName;
                        rowData.Value2 = arr;
                        row++;
                    }



                    oSheet.Range[oSheet.Cells[row, 1], oSheet.Cells[row, 4]].Merge();
                    oSheet.Range[oSheet.Cells[row, 1], oSheet.Cells[row, 4]] = "Tổng công";
                    oSheet.Range[oSheet.Cells[row, 1], oSheet.Cells[row, 4]].Font.Bold = true;
                    oSheet.Range[oSheet.Cells[row, 1], oSheet.Cells[row, 4]].Font.Name = fontName;
                    oSheet.Range[oSheet.Cells[row, 1], oSheet.Cells[row, 4]].Font.Size = fontSizeNoiDung;

                    formatRange1 = oSheet.Range[oSheet.Cells[row, 5], oSheet.Cells[row, 5]];
                    formatRange1.Value2 = "=SUM(E11:E" + (row - 1).ToString() + ")";
                    formatRange = oSheet.Range[oSheet.Cells[row, 5], oSheet.Cells[row, 9]]; 
                    if (row > 2)
                    {
                        formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    }

                    oSheet.Range[oSheet.Cells[11, 1], oSheet.Cells[row, 1]].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Range[oSheet.Cells[11, 1], oSheet.Cells[row, 1]].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oSheet.Range[oSheet.Cells[11, 2], oSheet.Cells[row, 2]].NumberFormat = "dd/MM/yyyy";
                    oSheet.Range[oSheet.Cells[11, 2], oSheet.Cells[row, 2]].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Range[oSheet.Cells[11, 2], oSheet.Cells[row, 2]].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oSheet.Range[oSheet.Cells[11, 3], oSheet.Cells[row, 3]].NumberFormat = "hh:mm:ss";
                    oSheet.Range[oSheet.Cells[11, 3], oSheet.Cells[row, 3]].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Range[oSheet.Cells[11, 3], oSheet.Cells[row, 3]].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oSheet.Range[oSheet.Cells[11, 4], oSheet.Cells[row, 4]].NumberFormat = "hh:mm:ss";
                    oSheet.Range[oSheet.Cells[11, 4], oSheet.Cells[row, 4]].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Range[oSheet.Cells[11, 4], oSheet.Cells[row, 4]].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oSheet.Range[oSheet.Cells[11, 5], oSheet.Cells[row, 5]].NumberFormat = "#,##0.0;(#,##0.0);;";
                    oSheet.Range[oSheet.Cells[11, 5], oSheet.Cells[row, 5]].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Range[oSheet.Cells[11, 5], oSheet.Cells[row, 5]].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oSheet.Range[oSheet.Cells[11, 6], oSheet.Cells[row, 6]].NumberFormat = "#,##0.0;(#,##0.0);;";
                    oSheet.Range[oSheet.Cells[11, 6], oSheet.Cells[row, 6]].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Range[oSheet.Cells[11, 6], oSheet.Cells[row, 6]].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oSheet.Range[oSheet.Cells[11, 7], oSheet.Cells[row, 7]].NumberFormat = "#,##0.0;(#,##0.0);;";
                    oSheet.Range[oSheet.Cells[11, 7], oSheet.Cells[row, 7]].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Range[oSheet.Cells[11, 7], oSheet.Cells[row, 7]].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oSheet.Range[oSheet.Cells[11, 8], oSheet.Cells[row, 8]].NumberFormat = "#,##0.0;(#,##0.0);;";
                    oSheet.Range[oSheet.Cells[11, 8], oSheet.Cells[row, 8]].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Range[oSheet.Cells[11, 8], oSheet.Cells[row, 8]].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oSheet.Range[oSheet.Cells[11, 9], oSheet.Cells[row, 9]].NumberFormat = "#,##0.0;(#,##0.0);;";
                    oSheet.Range[oSheet.Cells[11, 9], oSheet.Cells[row, 9]].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Range[oSheet.Cells[11, 9], oSheet.Cells[row, 9]].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oSheet.Range[oSheet.Cells[11, 10], oSheet.Cells[row, 10]].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Range[oSheet.Cells[11, 10], oSheet.Cells[row, 10]].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                    BorderAround(oSheet.Range[oSheet.Cells[10, 1], oSheet.Cells[row, 11]]);

                    oRow = 1;
                    oSheet = (Excel.Worksheet)oWB.ActiveSheet;
                    oSheet = oWB.Worksheets.Add(After: oWB.Sheets[oWB.Sheets.Count]);
                }
                oWB.Sheets[1].Activate();
                oXL.Visible = true;
                this.Cursor = Cursors.Default;

                oXL.UserControl = true;

            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
        }
        private void ChamCongChiTietCN_TG()
        {
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn2;
            dt = new DataTable();
            try
            {
                conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn2.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptChiTietQuetTheCNGD_TG"), conn2);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = lk_TuNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_DenNgay.DateTime;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                //LOAD BÁO CÁO CỦA 1 CÔNG ANH ĐƯỢC CHỌN
                if (chkInTheoCongNhan.Checked)
                {
                    cmd.Parameters.Add("@CN", SqlDbType.Int).Value = Convert.ToInt32(grvCN.GetFocusedRowCellValue("ID_CN"));
                }
                else
                {
                    //LOAD BÁO CÁO TẤT CẢ CÔNG NHÂN
                    cmd.Parameters.Add("@CN", SqlDbType.Int).Value = -1;
                }
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                DataTable dt1 = new DataTable();

                if (dt.Rows.Count == 0)
                {
                    this.Cursor = Cursors.Default;
                    Commons.Modules.ObjSystems.HideWaitForm();
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;
                this.Cursor = Cursors.WaitCursor;
                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;
                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 11;
                int countSheetName = 1;

                Microsoft.Office.Interop.Excel.Range formatRange;
                Microsoft.Office.Interop.Excel.Range formatRange1;
                foreach (DataRow rowC in dt.Rows)
                {
                    oSheet.Name = rowC[2].ToString();
                    TaoTTChung_TheoDV(oSheet, 1, 2, 1, 10, 0, 0);
                    int oRow = 10;

                    Microsoft.Office.Interop.Excel.Range row4_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[5, 10]];
                    row4_TieuDe_BaoCao.Merge();
                    row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                    row4_TieuDe_BaoCao.Font.Name = fontName;
                    row4_TieuDe_BaoCao.Font.Bold = true;
                    row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row4_TieuDe_BaoCao.RowHeight = 30;
                    row4_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG THEO GIAI ĐOẠN";

                    row4_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[6, 1], oSheet.Cells[6, 10]];
                    row4_TieuDe_BaoCao.Merge();
                    row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                    row4_TieuDe_BaoCao.Font.Name = fontName;
                    row4_TieuDe_BaoCao.Font.Bold = true;
                    row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row4_TieuDe_BaoCao.RowHeight = 15;
                    row4_TieuDe_BaoCao.Value2 = "Từ ngày : " + lk_TuNgay.DateTime.ToString("dd/MM/yyyy") + " Đến ngày: " + lk_DenNgay.DateTime.ToString("dd/MM/yyyy");


                    row4_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[8, 1], oSheet.Cells[8, 1]];
                    row4_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                    row4_TieuDe_BaoCao.Font.Name = fontName;
                    row4_TieuDe_BaoCao.Font.Bold = true;
                    row4_TieuDe_BaoCao.Value2 = "Mã nhân viên : " + rowC[1].ToString();


                    row4_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[8, 4], oSheet.Cells[8, 4]];
                    row4_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                    row4_TieuDe_BaoCao.Font.Name = fontName;
                    row4_TieuDe_BaoCao.Font.Bold = true;
                    row4_TieuDe_BaoCao.Value2 = "Họ tên : " + rowC[2].ToString();

                    row4_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[8, 7], oSheet.Cells[8, 7]];
                    row4_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                    row4_TieuDe_BaoCao.Font.Name = fontName;
                    row4_TieuDe_BaoCao.Font.Bold = true;
                    row4_TieuDe_BaoCao.Value2 = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblBoPhan") + " : " + rowC[3].ToString();

                    int col = 1;
                    oSheet.Cells[oRow, col] = "Ngày";
                    oSheet.Cells[oRow, col].ColumnWidth = 15;
                    col++;
                    oSheet.Cells[oRow, col] = "Giờ đến";
                    oSheet.Cells[oRow, col].ColumnWidth = 12;
                    col++;
                    oSheet.Cells[oRow, col] = "Giờ về";
                    oSheet.Cells[oRow, col].ColumnWidth = 12;
                    col++;
                    oSheet.Cells[oRow, col] = "Giờ LV";
                    oSheet.Cells[oRow, col].ColumnWidth = 12;
                    col++;
                    oSheet.Cells[oRow, col] = "TC thường";
                    oSheet.Cells[oRow, col].ColumnWidth = 12;
                    col++;
                    oSheet.Cells[oRow, col] = "TC chủ nhật";
                    oSheet.Cells[oRow, col].ColumnWidth = 12;
                    col++;
                    oSheet.Cells[oRow, col] = "TC lễ";
                    oSheet.Cells[oRow, col].ColumnWidth = 12;
                    col++;
                    oSheet.Cells[oRow, col] = "TC đêm";
                    oSheet.Cells[oRow, col].ColumnWidth = 12;
                    col++;
                    oSheet.Cells[oRow, col] = "Lý do vắng";
                    oSheet.Cells[oRow, col].ColumnWidth = 12;
                    col++;

                    Microsoft.Office.Interop.Excel.Range formatTieuDe = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow, 9]];
                    formatTieuDe.Font.Size = fontSizeNoiDung;
                    formatTieuDe.Font.Name = fontName;
                    formatTieuDe.Font.Bold = true;
                    formatTieuDe.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatTieuDe.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    formatTieuDe.Cells.WrapText = true;

                    int row = 11;
                    dt1 = ds.Tables[1].Copy();
                    dt1 = dt1.AsEnumerable().Where(r => r.Field<Int64>("ID_CN") == Convert.ToInt64(rowC[0])).CopyToDataTable().Copy();
                    foreach (DataRow row2 in dt1.Rows)
                    {
                        dynamic[] arr = {  row2["NGAY"].ToString(), row2["GIO_DEN"].ToString(), row2["GIO_VE"].ToString(), row2["GIO_LV"].ToString()
                        , row2["GIO_NT"].ToString(), row2["GIO_CN"].ToString(), row2["GIO_NL"].ToString(), row2["GIO_DEM"].ToString(), row2["LY_DO"].ToString()
                        };

                        Range rowData = oSheet.Range[oSheet.Cells[row, 1], oSheet.Cells[row, 9]];
                        rowData.Font.Size = fontSizeNoiDung;
                        rowData.Font.Name = fontName;
                        rowData.Value2 = arr;
                        row++;
                    }

                    oSheet.Range[oSheet.Cells[row, 1], oSheet.Cells[row, 3]].Merge();
                    oSheet.Range[oSheet.Cells[row, 1], oSheet.Cells[row, 3]] = "Tổng cộng";
                    oSheet.Range[oSheet.Cells[row, 1], oSheet.Cells[row, 3]].Font.Bold = true;
                    oSheet.Range[oSheet.Cells[row, 1], oSheet.Cells[row, 3]].Font.Name = fontName;
                    oSheet.Range[oSheet.Cells[row, 1], oSheet.Cells[row, 3]].Font.Size = fontSizeNoiDung;

                    formatRange1 = oSheet.Range[oSheet.Cells[row, 4], oSheet.Cells[row, 4]];
                    formatRange1.Value2 = "=SUM(D11:D" + (row - 1).ToString() + ")";
                    formatRange = oSheet.Range[oSheet.Cells[row, 4], oSheet.Cells[row, 8]];
                    if (row > 2)
                    {
                        formatRange1.AutoFill(formatRange, Microsoft.Office.Interop.Excel.XlAutoFillType.xlFillCopy);
                    }

                    formatRange = oSheet.Range[oSheet.Cells[row, 1], oSheet.Cells[row, 8]]; // format dòng tổng cộng
                    formatRange.Font.Bold = true;
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Size = fontSizeNoiDung;

                    oSheet.Range[oSheet.Cells[11, 1], oSheet.Cells[row, 1]].NumberFormat = "dd/MM/yyyy";
                    oSheet.Range[oSheet.Cells[11, 1], oSheet.Cells[row, 1]].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    oSheet.Range[oSheet.Cells[11, 1], oSheet.Cells[row, 1]].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oSheet.Range[oSheet.Cells[11, 2], oSheet.Cells[row, 2]].NumberFormat = "hh:mm:ss";
                    oSheet.Range[oSheet.Cells[11, 2], oSheet.Cells[row, 2]].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    oSheet.Range[oSheet.Cells[11, 2], oSheet.Cells[row, 2]].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oSheet.Range[oSheet.Cells[11, 3], oSheet.Cells[row, 3]].NumberFormat = "hh:mm:ss";
                    oSheet.Range[oSheet.Cells[11, 3], oSheet.Cells[row, 3]].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    oSheet.Range[oSheet.Cells[11, 3], oSheet.Cells[row, 3]].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oSheet.Range[oSheet.Cells[11, 4], oSheet.Cells[row, 4]].NumberFormat = "#,##0.0;(#,##0.0);;";
                    oSheet.Range[oSheet.Cells[11, 4], oSheet.Cells[row, 4]].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Range[oSheet.Cells[11, 4], oSheet.Cells[row, 4]].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oSheet.Range[oSheet.Cells[11, 5], oSheet.Cells[row, 5]].NumberFormat = "#,##0.0;(#,##0.0);;";
                    oSheet.Range[oSheet.Cells[11, 5], oSheet.Cells[row, 5]].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Range[oSheet.Cells[11, 5], oSheet.Cells[row, 5]].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oSheet.Range[oSheet.Cells[11, 6], oSheet.Cells[row, 6]].NumberFormat = "#,##0.0;(#,##0.0);;";
                    oSheet.Range[oSheet.Cells[11, 6], oSheet.Cells[row, 6]].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Range[oSheet.Cells[11, 6], oSheet.Cells[row, 6]].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oSheet.Range[oSheet.Cells[11, 7], oSheet.Cells[row, 7]].NumberFormat = "#,##0.0;(#,##0.0);;";
                    oSheet.Range[oSheet.Cells[11, 7], oSheet.Cells[row, 7]].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Range[oSheet.Cells[11, 7], oSheet.Cells[row, 7]].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oSheet.Range[oSheet.Cells[11, 8], oSheet.Cells[row, 8]].NumberFormat = "#,##0.0;(#,##0.0);;";
                    oSheet.Range[oSheet.Cells[11, 8], oSheet.Cells[row, 8]].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Range[oSheet.Cells[11, 8], oSheet.Cells[row, 8]].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oSheet.Range[oSheet.Cells[11, 9], oSheet.Cells[row, 9]].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    oSheet.Range[oSheet.Cells[11, 9], oSheet.Cells[row, 9]].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                    BorderAround(oSheet.Range[oSheet.Cells[10, 1], oSheet.Cells[row, 9]]);
                    row = row + 2;
                    formatRange = oSheet.Range[oSheet.Cells[row, 7], oSheet.Cells[row, 9]];
                    formatRange.Merge();
                    formatRange.Value2 = "Tiền Giang,Ngày ....tháng ....năm ....";
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row++;
                    formatRange = oSheet.Range[oSheet.Cells[row, 7], oSheet.Cells[row, 9]];
                    formatRange.Merge();
                    formatRange.Value2 = "Người lập biểu";
                    formatRange.Font.Name = fontName;
                    formatRange.Font.Size = fontSizeNoiDung;
                    formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    oSheet = (Excel.Worksheet)oWB.ActiveSheet;
                    oSheet = oWB.Worksheets.Add(After: oWB.Sheets[oWB.Sheets.Count]);
                }

                oWB.Sheets[1].Activate();
                oXL.Visible = true;
                this.Cursor = Cursors.Default;

                oXL.UserControl = true;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
        }

        private void ChamCongChiTietCN()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn2;
            dt = new DataTable();
            frm.rpt = new rptBangCCTheoGD(lk_TuNgay.DateTime, lk_DenNgay.DateTime, lk_NgayIn.DateTime);

            try
            {
                conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn2.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptChiTietQuetTheCNGD"), conn2);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;

                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = lk_TuNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = lk_DenNgay.DateTime;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                //LOAD BÁO CÁO CỦA 1 CÔNG ANH ĐƯỢC CHỌN
                if (chkInTheoCongNhan.Checked)
                {
                    cmd.Parameters.Add("@CN", SqlDbType.Int).Value = Convert.ToInt32(grvCN.GetFocusedRowCellValue("ID_CN"));
                }
                else
                {
                    //LOAD BÁO CÁO TẤT CẢ CÔNG NHÂN
                    cmd.Parameters.Add("@CN", SqlDbType.Int).Value = -1;
                }
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

        private void BCCThangGioDenGioVe_SB()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangXacNhanGioQuetThe_SB", conn);

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

                //SaveExcelFile = SaveFiles("Excell Workbook |*.xlsx|Excell 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                //if (SaveExcelFile == "")
                //{
                //    return;
                //}
                Excell.Application oXL;
                Excell.Workbook oWB;
                Excell.Worksheet oSheet;
                oXL = new Excell.Application();
                oXL.Visible = true;

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


                Range row2_TieuDe = oSheet.get_Range("A2", "J2");
                row2_TieuDe.Merge();
                row2_TieuDe.Font.Bold = true;
                row2_TieuDe.Value2 = dtBCThang.Rows[0]["DIA_CHI"];


                Range row3_TieuDe_BaoCao = oSheet.get_Range("A3", lastColumn + "3");
                row3_TieuDe_BaoCao.Merge();
                row3_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row3_TieuDe_BaoCao.Font.Name = fontName;
                row3_TieuDe_BaoCao.Font.Bold = true;
                row3_TieuDe_BaoCao.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row3_TieuDe_BaoCao.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row3_TieuDe_BaoCao.RowHeight = 38;
                row3_TieuDe_BaoCao.Value2 = "BẢNG TỔNG HỢP CHẤM CÔNG THÁNG";

                Range row4_TieuDe_Ngay = oSheet.get_Range("A4", lastColumn + "4");
                row4_TieuDe_Ngay.Merge();
                row4_TieuDe_Ngay.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Ngay.Font.Name = fontName;
                row4_TieuDe_Ngay.Font.Bold = true;
                row4_TieuDe_Ngay.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Ngay.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Ngay.RowHeight = 38;
                row4_TieuDe_Ngay.Value2 = "TỪ NGÀY 01/05/2022 ĐẾN NGÀY 31/05/2022";

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


                int col = 4;
                while (iTNgay <= iDNgay)
                {
                    oSheet.Cells[5, col] = iTNgay;
                    oSheet.Range[oSheet.Cells[5, col], oSheet.Cells[6, col]].Merge();
                    col += 1;
                    iTNgay++;
                }

                oSheet.Cells[5, col] = "Tổng giờ";
                oSheet.Cells[6, col] = "Total hour";

                col = col + 1;
                oSheet.Cells[5, col] = "Tổng tăng ca";
                oSheet.Cells[6, col] = "Total";

                col = col + 1;
                oSheet.Cells[5, col] = "Chủ nhật";
                oSheet.Cells[6, col] = "Sunday";

                col = col + 1;
                oSheet.Cells[5, col] = "Ký tên";
                oSheet.Cells[6, col] = "Sinature";

                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                int rowBD = 7;
                string cotCN = "";
                string[] TEN_XN = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_XN")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                for (int i = 0; i < TEN_XN.Count(); i++)
                {
                    dtBCThang = ds.Tables[0].Copy();
                    dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_XN") == TEN_XN[i]).CopyToDataTable().Copy();
                    DataRow[] dr = dtBCThang.Select();
                    current_dr = dr.Count();
                    string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                    foreach (DataRow row in dr)
                    {
                        for (col = 0; col < dtBCThang.Columns.Count; col++)
                        {
                            rowData[rowCnt, col] = row[col].ToString();
                            //if(row[col].ToString() == "CN")
                            //{
                            //    cotCN = cotCN +  (col + 1) + ",";
                            //}
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
                    row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(0, 255, 255);
                    oSheet.Cells[rowBD, 1] = "BỘ PHẬN";
                    oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 2]].Merge();
                    oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 2]].Font.Bold = true;
                    oSheet.Cells[rowBD, 3] = TEN_XN[i].ToString();

                    //Đổ dữ liệu của xí nghiệp
                    oSheet.get_Range("A" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;

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
                //Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excell.XlInsertShiftDirection.xlShiftDown, 1, 7);

                string CurentColumn = string.Empty;
                int colBD = 3;
                int colKT = dtBCThang.Columns.Count;

                //format

                for (col = colBD; col < dtBCThang.Columns.Count - 4; col++)
                {
                    CurentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    formatRange.NumberFormat = "#,##0.00";
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
                oSheet.Application.ActiveWindow.SplitColumn = 3;
                oSheet.Application.ActiveWindow.FreezePanes = true;
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
        private void BaoCaoHangNgayTheoGiaiDoan_DM()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongNgayGiaiDoan_DM", conn);

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

                DataTable dtSLXN = new DataTable(); // Lấy số lượng xí nghiệp
                dtSLXN = ds.Tables[1].Copy();
                int slXN = Convert.ToInt32(dtSLXN.Rows[0][0]);

                SaveExcelFile = SaveFiles("Excell Workbook |*.xlsx|Excell 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }
                Excell.Application oXL;
                Excell.Workbook oWB;
                Excell.Worksheet oSheet;
                oXL = new Excell.Application();
                oXL.Visible = false;
                this.Cursor = Cursors.WaitCursor;
                oWB = (Excell.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excell.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                DateTime dTNgay = Convert.ToDateTime(lk_TuNgay.EditValue);
                DateTime dDNgay = Convert.ToDateTime(lk_DenNgay.EditValue);
                int fontSizeTieuDe = 12;
                int fontSizeNoiDung = 12;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                Range row1_TieuDe = oSheet.get_Range("B1");
                row1_TieuDe.Font.Bold = true;
                row1_TieuDe.Value2 = "CÔNG TY CỔ PHẦN MAY DUY MINH";
                row1_TieuDe.WrapText = false;
                row1_TieuDe.Font.Size = 12;
                row1_TieuDe.Font.Name = fontName;
                row1_TieuDe.RowHeight = 21;
                row1_TieuDe.ColumnWidth = 43;



                Range row2_TieuDe = oSheet.get_Range("B2", "C2");
                row2_TieuDe.Font.Bold = true;
                row2_TieuDe.Merge();
                row2_TieuDe.Font.Size = 12;
                row2_TieuDe.Font.Name = fontName;
                row2_TieuDe.Value2 = "BÁO CÁO HÀNG NGÀY/ DAILY ATTENDANCE REPORT";
                row2_TieuDe.WrapText = false;
                row2_TieuDe.RowHeight = 33;
                row2_TieuDe.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row2_TieuDe.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                row2_TieuDe.Interior.Color = Color.FromArgb(248, 203, 173);


                Range row3_Date = oSheet.get_Range("L3", "N3");
                row3_Date.Font.Bold = true;
                row3_Date.Merge();
                row3_Date.Font.Size = 12;
                row3_Date.Font.Name = fontName;
                row3_Date.Value2 = "Ngày/ Date:" + Convert.ToDateTime(lk_NgayIn.EditValue).Day + "-" + (Convert.ToDateTime(lk_NgayIn.EditValue).Month.ToString().Length == 1 ? "0" + Convert.ToDateTime(lk_NgayIn.EditValue).Month.ToString() : Convert.ToDateTime(lk_NgayIn.EditValue).Month.ToString()) + "-" + Convert.ToDateTime(lk_NgayIn.EditValue).Year + "";
                row3_Date.WrapText = false;
                row3_Date.RowHeight = 24;
                row3_Date.Style.VerticalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row3_Date.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;


                Range row4 = oSheet.get_Range("B4");
                row4.RowHeight = 66;

                Range row5 = oSheet.get_Range("B5");
                row5.RowHeight = 79;


                #region table 1

                //Range row5_TieuDe_Format = oSheet.get_Range("B4", lastColumn + "5"); //27 + 31
                Range row5_TieuDe_Format = oSheet.get_Range("B4", lastColumn + "5"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;


                Range row1_TieuDe_Stt = oSheet.get_Range("A1");
                row1_TieuDe_Stt.ColumnWidth = 2;

                Range row5_TieuDe_Stt = oSheet.get_Range("B5", "B6");
                row5_TieuDe_Stt.Merge();
                row5_TieuDe_Stt.Value2 = "Phòng ban/ Section";
                row5_TieuDe_Stt.Interior.Color = Color.FromArgb(198, 224, 180);

                Range row6_TieuDe_Stt = oSheet.get_Range("C5", "C6");
                row6_TieuDe_Stt.Merge();
                row6_TieuDe_Stt.Value2 = "Đơn vị/ Department Vị trí/ Position";
                row6_TieuDe_Stt.ColumnWidth = 30;
                row6_TieuDe_Stt.Interior.Color = Color.FromArgb(198, 224, 180);

                int col_td = 4;
                Range row4_1;
                row4_1 = oSheet.get_Range("A4");
                row4_1.RowHeight = 25;
                while (dTNgay <= dDNgay)
                {
                    oSheet.Cells[4, col_td] = dTNgay.ToString("dd/MM/yyyy");
                    oSheet.Range[oSheet.Cells[4, Convert.ToInt32(col_td)], oSheet.Cells[4, Convert.ToInt32(col_td + 4)]].Merge();
                    // cột tổng lao động
                    oSheet.Cells[5, col_td] = "Tổng lao động / Total employees";
                    oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td)], oSheet.Cells[6, Convert.ToInt32(col_td)]].Merge();
                    oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td)], oSheet.Cells[6, Convert.ToInt32(col_td)]].Interior.Color = Color.FromArgb(255, 255, 0);


                    //cột số lao động vắng mặt
                    oSheet.Cells[5, col_td + 1] = "Số lao động vắng mặt";
                    oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td + 1)], oSheet.Cells[6, Convert.ToInt32(col_td + 1)]].Merge();
                    oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td + 1)], oSheet.Cells[6, Convert.ToInt32(col_td + 1)]].Interior.Color = Color.FromArgb(255, 230, 153);


                    //cột Số lao động có mặt
                    oSheet.Cells[5, col_td + 2] = "Số lao động có mặt/ Total employees present";
                    oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td + 2)], oSheet.Cells[6, Convert.ToInt32(col_td + 2)]].Merge();
                    oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td + 2)], oSheet.Cells[6, Convert.ToInt32(col_td + 2)]].Interior.Color = Color.FromArgb(189, 215, 238);


                    //cột Tỷ lệ vắng (%)
                    oSheet.Cells[5, col_td + 3] = "Tỷ lệ vắng (%)";
                    oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td + 3)], oSheet.Cells[6, Convert.ToInt32(col_td + 3)]].Merge();
                    oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td + 3)], oSheet.Cells[6, Convert.ToInt32(col_td + 3)]].Interior.Color = Color.FromArgb(255, 255, 0);



                    //cột Tỷ lệ có mặt/ tổng số (%)
                    oSheet.Cells[5, col_td + 4] = "Tỷ lệ có mặt/ tổng số (%)";
                    oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td + 4)], oSheet.Cells[6, Convert.ToInt32(col_td + 4)]].Merge();
                    oSheet.Range[oSheet.Cells[5, Convert.ToInt32(col_td + 4)], oSheet.Cells[6, Convert.ToInt32(col_td + 4)]].Interior.Color = Color.FromArgb(255, 255, 0);

                    col_td = col_td + 5;
                    dTNgay = dTNgay.AddDays(1);
                }



                oSheet.Application.ActiveWindow.SplitColumn = 3;
                oSheet.Application.ActiveWindow.SplitRow = 6;
                oSheet.Application.ActiveWindow.FreezePanes = true;


                int col = 1;
                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                int rowBD = 7;
                string cotCN_A = "";
                string cotCN_B = "";
                string[] TEN_XN = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_XN")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[0].Copy(); // Dữ row count data

                string sRowXN = "";
                string s = int_to_Roman(9);
                Range formatRange11;
                int rowSum = 8; //Row sum của cột G 
                for (int i = 0; i < TEN_XN.Count(); i++)
                {
                    dtBCThang = ds.Tables[0].Copy();
                    dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_XN") == TEN_XN[i]).CopyToDataTable().Copy();
                    DataRow[] dr = dtBCThang.Select();
                    current_dr = dr.Count();
                    string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                    foreach (DataRow row in dr)
                    {
                        for (col = 0; col < dtBCThang.Columns.Count; col++)
                        {
                            //sTenCot = CharacterIncrement(6);
                            //Excell.Range formatRange7;
                            //formatRange7 = oSheet.get_Range(sTenCot + ((rowCnt + 1) + 7).ToString());
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
                    Range row_groupXI_NGHIEP_Format = oSheet.get_Range("B" + rowBD + "".ToString(), lastColumn + "" + rowBD + "".ToString()); //27 + 31
                    row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(255, 255, 0);
                    oSheet.Cells[rowBD, 2] = TEN_XN[i].ToString();
                    oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 2]].Font.Bold = true;


                    oSheet.Cells[rowBD, 3] = "Sub-Total " + int_to_Roman(i + 1) + "";
                    oSheet.Cells[rowBD, 3].Font.Bold = true;
                    oSheet.Cells[rowBD, 3].Font.Size = fontSizeNoiDung;
                    oSheet.Cells[rowBD, 3].Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowBD, 3].Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                    oSheet.Cells[rowBD, 3].Font.Name = fontName;

                    sRowXN = sRowXN + rowBD + ",";

                    //Đổ dữ liệu của xí nghiệp
                    oSheet.get_Range("B" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;
                    col_td = 4;
                    dTNgay = Convert.ToDateTime(lk_TuNgay.EditValue);
                    //Set công thức từng row
                    while (dTNgay <= dDNgay)
                    {
                        // cột tổng lao động
                        oSheet.Cells[rowBD, col_td] = "=SUM(" + CharacterIncrement(col_td - 1) + "" + (rowBD + 1) + ":" + CharacterIncrement(col_td - 1) + "" + (rowCnt + 1) + ")";

                        //cột số lao động vắng mặt
                        oSheet.Cells[rowBD, col_td + 1] = "=SUM(" + CharacterIncrement(col_td) + "" + (rowBD + 1) + ":" + CharacterIncrement(col_td) + "" + (rowCnt + 1) + ")";


                        //cột Số lao động có mặt
                        formatRange11 = oSheet.get_Range("" + CharacterIncrement(col_td + 1) + "" + rowBD + "", "" + CharacterIncrement(col_td + 1) + "" + (rowCnt + 1) + "");
                        //oSheet.Cells[rowBD, col_td + 2] = "=" + CharacterIncrement(col_td - 1) + ""+rowBD+"-"+ CharacterIncrement(col_td) + ""+rowBD+"";
                        formatRange11.Value = "=" + CharacterIncrement(col_td - 1) + "" + rowBD + "-" + CharacterIncrement(col_td) + "" + rowBD + "";


                        //cột Tỷ lệ vắng (%)
                        formatRange11 = oSheet.get_Range("" + CharacterIncrement(col_td + 2) + "" + rowBD + "", "" + CharacterIncrement(col_td + 2) + "" + (rowCnt + 1) + "");
                        formatRange11.Value = "=IFERROR(" + CharacterIncrement(col_td) + "" + rowBD.ToString() + "/" + CharacterIncrement(col_td - 1) + "" + rowBD.ToString() + ",0)";


                        //cột Tỷ lệ có mặt/ tổng số (%)
                        formatRange11 = oSheet.get_Range("" + CharacterIncrement(col_td + 3) + "" + rowBD + "", "" + CharacterIncrement(col_td + 3) + "" + (rowCnt + 1) + "");
                        formatRange11.Value = "=IFERROR(" + CharacterIncrement(col_td + 1) + "" + rowBD + "/" + CharacterIncrement(col_td - 1) + "" + rowBD + ",0)";


                        col_td = col_td + 5;
                        dTNgay = dTNgay.AddDays(1);
                    }
                    // Fortmat từ cột đầu tới cột cuối của từng Xí nghiệp
                    Range formatRange10;
                    formatRange10 = oSheet.get_Range("D" + (rowBD) + "", lastColumn + (rowBD));
                    formatRange10.Font.Color = Color.FromArgb(255, 0, 0);
                    formatRange10.Font.Bold = true;
                    formatRange10.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                    formatRange10.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                    dr_Cu = current_dr;
                    keepRowCnt = rowCnt;
                    rowSum = rowCnt + 3;
                    rowCnt = 0;
                }
                rowCnt = keepRowCnt;
                Excell.Range formatRange;
                Excell.Range formatRange1;
                Excell.Range formatRange3;
                Excell.Range formatRange4;
                int rowbd;
                int rowDup = 0; // row bat dau của dữ liệu duplicate
                bool bChan = false;
                for (rowbd = 8; rowbd <= rowCnt; rowbd++)
                {
                    formatRange = oSheet.get_Range("B" + rowbd + "");
                    formatRange1 = oSheet.get_Range("B" + (rowbd + 1).ToString());

                    if (formatRange.Value == null)
                    {
                        formatRange = oSheet.get_Range("B" + (rowDup).ToString() + "");
                    }
                    if (formatRange.Value == formatRange1.Value)
                    {
                        if (bChan == false)
                        {
                            rowDup = rowbd;
                        }
                        bChan = true;
                        formatRange.Value = null;
                        formatRange3 = oSheet.get_Range("B" + rowbd + "", "B" + (rowbd + 1).ToString());
                        formatRange3.Merge();
                    }
                    else
                    {
                        bChan = false;
                        rowDup = 0;
                    }
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                }

                rowCnt++;
                rowCnt++;

                Range rowTONG_CONG = oSheet.get_Range("B" + rowCnt);
                rowTONG_CONG.Value2 = "Tổng/Grand Total";
                rowTONG_CONG.Font.Bold = true;

                Range rowTONG_CONG1 = oSheet.get_Range("C" + rowCnt);
                string sLama = "(";
                for (int i = 1; i <= slXN; i++)
                {
                    sLama = sLama + int_to_Roman(i) + "+";
                }
                rowTONG_CONG1.Value2 = sLama.Substring(0, sLama.Length - 1) + ")";
                rowTONG_CONG1.Font.Bold = true;
                rowTONG_CONG1.Font.Size = fontSizeNoiDung;
                rowTONG_CONG1.Font.Name = fontName;

                Range rowSumAll = oSheet.get_Range("B" + rowCnt + "", "C" + rowCnt);
                rowSumAll.Font.Bold = true;
                rowSumAll.Interior.Color = Color.FromArgb(189, 215, 238);

                rowSumAll = oSheet.get_Range("D" + rowCnt + "", lastColumn + rowCnt);
                rowSumAll.Font.Bold = true;
                rowSumAll.Font.Color = Color.FromArgb(255, 0, 0);
                rowSumAll.Interior.Color = Color.FromArgb(255, 255, 0);
                rowSumAll.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                rowSumAll.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                col_td = 4;
                dTNgay = Convert.ToDateTime(lk_TuNgay.EditValue);
                while (dTNgay <= dDNgay)
                {
                    formatRange4 = oSheet.get_Range(CharacterIncrement(col_td - 1) + "7" + "", CharacterIncrement(col_td - 1) + (rowCnt).ToString());
                    formatRange4.Interior.Color = Color.FromArgb(226, 239, 218);
                    formatRange4.NumberFormat = "0"; // format từng cột
                    try { formatRange4.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                    oSheet.Cells[rowCnt, col_td] = "=SUM(" + CellAddress(oSheet, 7, col_td) + ":" + CellAddress(oSheet, rowCnt - 1, col_td) + ")/2"; // sUM TỪNNG CỘT

                    //cột số lao động vắng mặt
                    formatRange4 = oSheet.get_Range(CharacterIncrement(col_td) + "7" + "", CharacterIncrement(col_td) + (rowCnt).ToString());
                    formatRange4.Interior.Color = Color.FromArgb(255, 230, 153);
                    formatRange4.NumberFormat = "0";
                    try { formatRange4.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                    oSheet.Cells[rowCnt, col_td + 1] = "=SUM(" + CellAddress(oSheet, 7, col_td + 1) + ":" + CellAddress(oSheet, rowCnt - 1, col_td + 1) + ")/2";


                    //cột Số lao động có mặt
                    formatRange4 = oSheet.get_Range(CharacterIncrement(col_td + 1) + "7" + "", CharacterIncrement(col_td + 1) + (rowCnt).ToString());
                    formatRange4.Interior.Color = Color.FromArgb(189, 215, 238);
                    formatRange4.NumberFormat = "0";
                    try { formatRange4.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                    oSheet.Cells[rowCnt, col_td + 2] = "=SUM(" + CellAddress(oSheet, 7, col_td + 2) + ":" + CellAddress(oSheet, rowCnt - 1, col_td + 2) + ")/2";


                    //cột Tỷ lệ vắng (%)
                    formatRange4 = oSheet.get_Range(CharacterIncrement(col_td + 2) + "7" + "", CharacterIncrement(col_td + 2) + (rowCnt).ToString());
                    formatRange4.Interior.Color = Color.FromArgb(255, 255, 0);
                    formatRange4.NumberFormat = @"0%";
                    try { formatRange4.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                    oSheet.Cells[rowCnt, col_td + 3] = "=IFERROR(" + CellAddress(oSheet, rowCnt, col_td + 1) + "/" + CellAddress(oSheet, rowCnt, col_td) + ",0)";



                    //cột Tỷ lệ có mặt/ tổng số (%)
                    formatRange4 = oSheet.get_Range(CharacterIncrement(col_td + 3) + "7" + "", CharacterIncrement(col_td + 3) + (rowCnt).ToString());
                    formatRange4.Interior.Color = Color.FromArgb(255, 255, 0);
                    formatRange4.NumberFormat = @"0%";
                    try { formatRange4.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                    oSheet.Cells[rowCnt, col_td + 4] = "=IFERROR(" + CellAddress(oSheet, rowCnt, col_td + 2) + "/" + CellAddress(oSheet, rowCnt, col_td) + ",0)";

                    col_td = col_td + 5;
                    dTNgay = dTNgay.AddDays(1);
                }

                Excell.Range formatRange8;
                sRowXN = sRowXN.Substring(0, sRowXN.Length - 1);
                string[] strGetRowXN = sRowXN.Split(',');
                for (int i = 0; i < slXN; i++)
                {
                    formatRange8 = oSheet.get_Range("B" + strGetRowXN[i] + "", lastColumn + "" + strGetRowXN[i] + "");
                    formatRange8.Interior.Color = Color.FromArgb(255, 255, 0);
                }

                //Format All
                Excell.Range formatRange9;
                formatRange9 = oSheet.get_Range("B8", "C" + (rowCnt));
                formatRange9.Font.Size = fontSizeNoiDung;
                formatRange9.Font.Name = fontName;
                formatRange9.WrapText = true;

                formatRange9 = oSheet.get_Range("D7", lastColumn + (rowCnt));
                formatRange9.Cells.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange9.Cells.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;
                formatRange9.Font.Bold = true;
                formatRange9.Font.Size = fontSizeNoiDung;
                formatRange9.Font.Name = fontName;

                BorderAround(oSheet.get_Range("B2", "C3"));
                BorderAround(oSheet.get_Range("B4", lastColumn + rowCnt.ToString()));

                #endregion

                ////////////////////////////////////////////////////////////////////////////////// TABLE 2  //////////////////////////////////////////////////////////////////////////////////
                #region table 2

                rowCnt = rowCnt + 5; // Dòng phòng ban
                int rowCnt2 = rowCnt - 1; // dòng ngày
                row5_TieuDe_Stt = oSheet.get_Range("B" + rowCnt + "", "B" + (rowCnt + 1).ToString() + "");
                row5_TieuDe_Stt.Merge();
                row5_TieuDe_Stt.Value2 = "Phòng ban/ Section";
                row5_TieuDe_Stt.Font.Name = fontName;
                row5_TieuDe_Stt.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Stt.Interior.Color = Color.FromArgb(198, 224, 180);

                row6_TieuDe_Stt = oSheet.get_Range("C" + rowCnt + "", "C" + (rowCnt + 1).ToString() + "");
                row6_TieuDe_Stt.Merge();
                row6_TieuDe_Stt.Value2 = "Đơn vị/ Department Vị trí/ Position";
                row6_TieuDe_Stt.ColumnWidth = 30;
                row6_TieuDe_Stt.Font.Name = fontName;
                row6_TieuDe_Stt.Font.Size = fontSizeNoiDung;
                row6_TieuDe_Stt.Interior.Color = Color.FromArgb(198, 224, 180);

                col_td = 4;
                row4_1 = oSheet.get_Range("A" + rowCnt2 + "");
                row4_1.RowHeight = 25;

                row4_1 = oSheet.get_Range("A" + rowCnt + "");
                row4_1.RowHeight = 79;
                dTNgay = Convert.ToDateTime(lk_TuNgay.EditValue);
                while (dTNgay <= dDNgay)
                {
                    oSheet.Cells[rowCnt - 1, col_td] = dTNgay;
                    oSheet.Range[oSheet.Cells[rowCnt - 1, Convert.ToInt32(col_td)], oSheet.Cells[rowCnt - 1, Convert.ToInt32(col_td + 2)]].Merge();
                    oSheet.Cells[rowCnt, col_td] = "Tổng lao động / Total employees";
                    oSheet.Range[oSheet.Cells[rowCnt, Convert.ToInt32(col_td)], oSheet.Cells[rowCnt + 1, Convert.ToInt32(col_td)]].Merge();
                    oSheet.Range[oSheet.Cells[rowCnt, Convert.ToInt32(col_td)], oSheet.Cells[rowCnt + 1, Convert.ToInt32(col_td)]].Interior.Color = Color.FromArgb(255, 255, 0);


                    oSheet.Cells[rowCnt, col_td + 1] = "Số lao động có mặt/ Total employees present";
                    oSheet.Range[oSheet.Cells[rowCnt, Convert.ToInt32(col_td + 1)], oSheet.Cells[rowCnt + 1, Convert.ToInt32(col_td + 1)]].Merge();
                    oSheet.Range[oSheet.Cells[rowCnt, Convert.ToInt32(col_td + 1)], oSheet.Cells[rowCnt + 1, Convert.ToInt32(col_td + 1)]].Interior.Color = Color.FromArgb(255, 230, 153);


                    oSheet.Cells[rowCnt, col_td + 2] = "Tỷ lệ có mặt/ tổng số (%)";
                    oSheet.Range[oSheet.Cells[rowCnt, Convert.ToInt32(col_td + 2)], oSheet.Cells[rowCnt + 1, Convert.ToInt32(col_td + 2)]].Merge();
                    oSheet.Range[oSheet.Cells[rowCnt, Convert.ToInt32(col_td + 2)], oSheet.Cells[rowCnt + 1, Convert.ToInt32(col_td + 2)]].Interior.Color = Color.FromArgb(189, 215, 238);

                    col_td = col_td + 3;
                    dTNgay = dTNgay.AddDays(1);
                }

                int rowCnt1 = rowCnt + 2; // dòng dữ liệu
                keepRowCnt = rowCnt + 2;// Biến này dùng để lưu lại giá trị của biến rowCnt
                col = 1;
                dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                rowBD_XN = 0; // Row để insert dòng xí nghiệp
                rowBD = rowCnt + 2;
                rowCnt = 0;
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[2].Copy();
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);
                string[] TEN_TO = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_TO")).Distinct().ToArray();
                chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                dt_temp = new DataTable();
                dt_temp = ds.Tables[2].Copy(); // Dữ row count data

                sRowXN = "";
                s = int_to_Roman(9);
                rowSum = 8; //Row sum của cột G 
                for (int i = 0; i < TEN_TO.Count(); i++)
                {
                    dtBCThang = ds.Tables[2].Copy();
                    dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_TO") == TEN_TO[i]).CopyToDataTable().Copy();
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

                    //Đổ dữ liệu của xí nghiệp
                    oSheet.get_Range("B" + (rowBD) + "", lastColumn + (rowCnt).ToString()).Value2 = rowData;

                    col_td = 4;
                    dTNgay = Convert.ToDateTime(lk_TuNgay.EditValue);
                    //Set công thức từng row
                    while (dTNgay <= dDNgay)
                    {
                        //cột Tỷ lệ vắng (%)
                        formatRange11 = oSheet.get_Range("" + CharacterIncrement(col_td + 1) + "" + rowBD + "", "" + CharacterIncrement(col_td + 1) + "" + (rowCnt + 1) + "");
                        formatRange11.Value = "=IFERROR(" + CharacterIncrement(col_td) + "" + rowBD.ToString() + "/" + CharacterIncrement(col_td - 1) + "" + rowBD.ToString() + ",0)";
                        formatRange11.NumberFormat = @"0%";
                        try { formatRange11.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                        col_td = col_td + 3;
                        dTNgay = dTNgay.AddDays(1);
                    }


                    col_td = 4;
                    dr_Cu = current_dr;
                    keepRowCnt = rowCnt;
                    rowSum = rowCnt + 3;
                    rowCnt = 0;
                }
                rowCnt = keepRowCnt;
                rowDup = 0; // row bat dau của dữ liệu duplicate
                bChan = false;
                for (rowbd = rowCnt1; rowbd <= rowCnt; rowbd++)
                {
                    formatRange = oSheet.get_Range("B" + rowbd + "");
                    formatRange1 = oSheet.get_Range("B" + (rowbd + 1).ToString());

                    if (formatRange.Value == null)
                    {
                        formatRange = oSheet.get_Range("B" + (rowDup).ToString() + "");
                    }
                    if (formatRange.Value == formatRange1.Value)
                    {
                        if (bChan == false)
                        {
                            rowDup = rowbd;
                        }
                        bChan = true;
                        formatRange.Value = null;
                        formatRange3 = oSheet.get_Range("B" + rowbd + "", "B" + (rowbd + 1).ToString());
                        formatRange3.Merge();
                    }
                    else
                    {
                        bChan = false;
                        rowDup = 0;
                    }
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                }


                rowCnt++;

                formatRange9 = oSheet.get_Range("B" + rowCnt + "");
                formatRange9.Value = "Tổng/Grand Total";

                formatRange9 = oSheet.get_Range("D" + rowCnt + "", lastColumn + rowCnt);
                formatRange9.Font.Bold = true;
                formatRange9.Font.Name = fontName;
                formatRange9.Font.Size = fontSizeNoiDung;
                formatRange9.Font.Color = Color.FromArgb(255, 0, 0);
                formatRange9.Interior.Color = Color.FromArgb(189, 215, 238);
                formatRange9.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange9.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                // SUM
                col_td = 4;
                dTNgay = Convert.ToDateTime(lk_TuNgay.EditValue);
                while (dTNgay <= dDNgay)
                {
                    formatRange4 = oSheet.get_Range(CharacterIncrement(col_td - 1) + "" + rowCnt + "" + "", CharacterIncrement(col_td - 1) + (rowCnt).ToString());
                    oSheet.Cells[rowCnt, col_td] = "=SUM(" + CellAddress(oSheet, rowCnt1, col_td) + ":" + CellAddress(oSheet, rowCnt - 1, col_td) + ")"; // sUM TỪNNG CỘT

                    formatRange4 = oSheet.get_Range(CharacterIncrement(col_td) + "" + rowCnt + "" + "", CharacterIncrement(col_td) + (rowCnt).ToString());
                    oSheet.Cells[rowCnt, col_td + 1] = "=SUM(" + CellAddress(oSheet, rowCnt1, col_td + 1) + ":" + CellAddress(oSheet, rowCnt - 1, col_td + 1) + ")";

                    //cột Tỷ lệ vắng (%)
                    formatRange4 = oSheet.get_Range(CharacterIncrement(col_td + 1) + "" + rowCnt + "" + "", CharacterIncrement(col_td + 1) + (rowCnt).ToString());
                    oSheet.Cells[rowCnt, col_td + 2] = "=IFERROR(" + CellAddress(oSheet, rowCnt, col_td + 1) + "/" + CellAddress(oSheet, rowCnt, col_td) + ",0)";

                    col_td = col_td + 3;
                    dTNgay = dTNgay.AddDays(1);
                }

                col_td = 4;
                dTNgay = Convert.ToDateTime(lk_TuNgay.EditValue);
                while (dTNgay <= dDNgay)
                {
                    formatRange4 = oSheet.get_Range(CharacterIncrement(col_td - 1) + "" + rowCnt1 + "" + "", CharacterIncrement(col_td - 1) + (rowCnt).ToString());
                    formatRange4.Interior.Color = Color.FromArgb(226, 239, 218);
                    formatRange4.NumberFormat = "0"; // format từng cột
                    try { formatRange4.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                    //cột số lao động vắng mặt
                    formatRange4 = oSheet.get_Range(CharacterIncrement(col_td) + "" + rowCnt1 + "" + "", CharacterIncrement(col_td) + (rowCnt).ToString());
                    formatRange4.Interior.Color = Color.FromArgb(255, 230, 153);
                    formatRange4.NumberFormat = "0";
                    try { formatRange4.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                    formatRange4 = oSheet.get_Range(CharacterIncrement(col_td + 1) + "" + rowCnt1 + "" + "", CharacterIncrement(col_td + 1) + (rowCnt).ToString());
                    formatRange4.Interior.Color = Color.FromArgb(189, 215, 238);
                    formatRange4.NumberFormat = @"0%";
                    try { formatRange4.TextToColumns(Type.Missing, Excell.XlTextParsingType.xlDelimited, Excell.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                    col_td = col_td + 3;
                    dTNgay = dTNgay.AddDays(1);
                }


                formatRange9 = oSheet.get_Range("D" + rowCnt2 + "", lastColumn + rowCnt);
                formatRange9.Font.Bold = true;
                formatRange9.WrapText = true;
                formatRange9.HorizontalAlignment = Excell.XlHAlign.xlHAlignCenter;
                formatRange9.VerticalAlignment = Excell.XlVAlign.xlVAlignCenter;

                formatRange9 = oSheet.get_Range("B" + rowCnt2 + "", lastColumn + rowCnt.ToString());
                formatRange9.Font.Name = fontName;
                formatRange9.Font.Size = fontSizeNoiDung;

                BorderAround(oSheet.get_Range("B" + rowCnt2 + "", lastColumn + rowCnt.ToString()));
                #endregion

                this.Cursor = Cursors.Default;
                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excell.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
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
        #endregion

        private void lk_TuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            DateTime tungay = Convert.ToDateTime(lk_TuNgay.EditValue);
            //DateTime denngay = Convert.ToDateTime(LK_Thang.EditValue).AddMonths(+1);
            //lk_TuNgay.EditValue = Convert.ToDateTime("01/" + tungay.Month + "/" + tungay.Year);
            lk_DenNgay.EditValue = Convert.ToDateTime(DateTime.DaysInMonth(tungay.Year, tungay.Month) + "/" + tungay.Month + "/" + tungay.Year);
            Commons.Modules.sLoad = "";
            LoadGrvCongNhan();
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

        private void lk_DenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadGrvCongNhan();
        }
    }

}
