using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using Vs.Report;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;



namespace Vs.HRM
{
    public partial class ucBaoCaoGiaiDoan : DevExpress.XtraEditors.XtraUserControl
    {
        public string uFontName = "Times New Roman";
        public float uFontSize = 11.25F;
        public ucBaoCaoGiaiDoan()
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
        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "Print":
                    {
                        switch (rdo_ChonBaoCao.SelectedIndex)
                        {
                            case 0:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)))
                                    {
                                        case "MT":
                                            {
                                                DSNVDiTreVeSomGiaiDoan();
                                                break;
                                            }
                                        case "SB":
                                            {
                                                BCCThangGioDenGioVe_SB();
                                                break;
                                            }
                                        default:
                                            DSNVDiTreVeSomGiaiDoan();
                                            break;
                                    }
                                }
                                break;
                            case 1:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)))
                                    {
                                        case "MT":
                                            {
                                                DSVangDauGioGiaiDoan();
                                                break;
                                            }
                                        default:
                                            DSVangDauGioGiaiDoan();
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
                                                DSChamVangGiaiDoan();
                                                break;
                                            }
                                        default:
                                            DSChamVangGiaiDoan();
                                            break;
                                    }
                                }
                                break;
                            case 3:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)))
                                    {
                                        case "MT":
                                            {
                                                DSChamCongVangLuyKe();
                                                break;
                                            }
                                        default:
                                            DSChamCongVangLuyKe();
                                            break;
                                    }
                                }
                                break;
                            case 4:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)))
                                    {
                                        case "MT":
                                            {
                                                XacNhanQuetThe();
                                                break;
                                            }
                                        case "SB":
                                            {
                                                XacNhanQuetThe();
                                                //BCCThangGioDenGioVe_SB();
                                                break;
                                            }
                                        default:
                                            XacNhanQuetThe();
                                            break;
                                    }
                                }

                                break;
                            case 5:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)))
                                    {
                                        case "MT":
                                            {
                                                ChamCongChiTietCN();
                                                break;
                                            }
                                        default:
                                            ChamCongChiTietCN();
                                            break;
                                    }

                                }
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
            LoadCboDonVi();
            LoadCboXiNghiep();
            LoadCboTo();

            LoadGrvLydovang();

            DateTime dtTN = DateTime.Today;
            dtTN = dtTN.AddDays(-dtTN.Day + 1);
            DateTime dtDN = dtTN.AddMonths(1);
            dtDN = dtDN.AddDays(-1);
            lk_TuNgay.EditValue = dtTN;
            lk_DenNgay.EditValue = dtDN;
            lk_NgayIn.EditValue = DateTime.Today;

            LoadGrvCongNhan();
            Commons.Modules.sLoad = "";

        }
        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboXiNghiep();
            LoadCboTo();
            LoadGrvCongNhan();
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboTo();
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
            switch (rdo_ChonBaoCao.SelectedIndex)
            {
                case 0:
                    {
                        chkInTheoCongNhan.Enabled = false;
                        grdCN.Visible = false;
                    }
                    break;
                case 1:
                    {
                        chkInTheoCongNhan.Enabled = false;
                        grdCN.Visible = false;
                    }
                    break;
                case 2:
                    {
                        chkInTheoCongNhan.Enabled = false;
                        grdCN.Visible = false;
                    }
                    break;
                case 3:
                    {
                        chkInTheoCongNhan.Enabled = false;
                        grdCN.Visible = false;
                    }
                    break;
                case 4:
                    {
                        chkInTheoCongNhan.Enabled = false;
                        grdCN.Visible = false;
                    }
                    break;
                case 5:
                    {
                        chkInTheoCongNhan.Enabled = true;
                        grdCN.Visible = true;
                    }
                    break;
            }
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
        #endregion

        #region functionInTheoDV
        private void DSNVDiTreVeSomGiaiDoan()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            string sTieuDe = "DANH SÁCH NHÂN VIÊN ĐI TRỄ VỀ SỚM THEO GIAI ĐOẠN";

            frm.rpt = new rptDSDiTreVeSomGiaiDoan(lk_TuNgay.DateTime, lk_DenNgay.DateTime, lk_NgayIn.DateTime, sTieuDe);

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSDiTreVeSomGiaiDoan", conn);

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
                frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
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
            string sTieuDe1 = "DANH SÁCH VẮNG ĐẦU GIỜ THEO GIAI ĐOẠN";
            frm.rpt = new rptDSVangDauGioGiaiDoan(lk_TuNgay.DateTime, lk_DenNgay.DateTime, sTieuDe1);

            try
            {
                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSVangDauGioGiaiDoan", conn1);

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
                frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
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
            string sTieuDe2 = "DANH SÁCH CHẤM VẮNG THEO GIAI ĐOẠN";
            frm.rpt = new rptDSChamCongVang(lk_TuNgay.DateTime, lk_DenNgay.DateTime, lk_NgayIn.DateTime, sTieuDe2);

            try
            {
                conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn2.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSChamCongVang", conn2);

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
                frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
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
            string sTieuDe2 = "DANH SÁCH CHẤM CÔNG VẮNG LŨY KẾ";
            frm.rpt = new rptDSChamCongVangLuyKe(lk_TuNgay.DateTime, lk_DenNgay.DateTime, lk_NgayIn.DateTime, sTieuDe2);

            try
            {
                conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn2.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSChamCongVangLuyKe", conn2);

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
                frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
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

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangXacNhanGioQuetThe", conn);

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


                Excel.Application oXL;
                Excel._Workbook oWB;
                Excel._Worksheet oSheet;

                oXL = new Excel.Application();
                oXL.Visible = true;

                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 12;
                int iTNgay = Convert.ToDateTime(lk_TuNgay.EditValue).Day;
                int iDNgay = Convert.ToDateTime(lk_DenNgay.EditValue).Day;
                int iSoNgay = (iDNgay - iTNgay);

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCGaiDoan.Columns.Count - 1);
                Excel.Range row2_TieuDe_BaoCao0 = oSheet.get_Range("A1", lastColumn + "2");
                row2_TieuDe_BaoCao0.Merge();
                row2_TieuDe_BaoCao0.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao0.Font.Name = fontName;
                row2_TieuDe_BaoCao0.Font.Bold = true;
                row2_TieuDe_BaoCao0.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                row2_TieuDe_BaoCao0.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                //=====

                Excel.Range row2_TieuDe_BaoCao = oSheet.get_Range("A3", lastColumn + "3");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.Bold = true;
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 50;
                row2_TieuDe_BaoCao.Value2 = "BẢNG CHẤM CÔNG " + Convert.ToDateTime(lk_TuNgay.EditValue).ToString("MM/yyyy");

                Excel.Range row5_TieuDe = oSheet.get_Range("A4", "A5");
                row5_TieuDe.Merge();
                row5_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe.Font.Name = fontName;
                row5_TieuDe.Font.Bold = true;
                row5_TieuDe.Value2 = "Stt";
                row5_TieuDe.Interior.Color = Color.Yellow;

                Excel.Range row5_TieuDe1 = oSheet.get_Range("B4", "B5");
                row5_TieuDe1.Merge();
                row5_TieuDe1.Font.Name = fontName;
                row5_TieuDe1.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe1.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe1.Font.Bold = true;
                row5_TieuDe1.Interior.Color = Color.Yellow;

                row5_TieuDe1.Value2 = "Mã số NV";

                Excel.Range row5_TieuDe2 = oSheet.get_Range("C4", "C5");
                row5_TieuDe2.Merge();
                row5_TieuDe2.Font.Name = fontName;
                row5_TieuDe2.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe2.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe2.Font.Bold = true;
                row5_TieuDe2.Interior.Color = Color.Yellow;
                row5_TieuDe2.Value2 = "Họ tên";



                Excel.Range row5_TieuDe3 = oSheet.get_Range("D4", "D5");
                row5_TieuDe3.Merge();
                row5_TieuDe3.Font.Name = fontName;
                row5_TieuDe3.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe3.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe3.Font.Bold = true;
                row5_TieuDe3.Interior.Color = Color.Yellow;
                row5_TieuDe3.Value2 = "Xí nghiệp/P.ban";

                Excel.Range row5_TieuDe4 = oSheet.get_Range("E4", "E5");
                row5_TieuDe4.Merge();
                row5_TieuDe4.Font.Name = fontName;
                row5_TieuDe4.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe4.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row5_TieuDe4.Font.Bold = true;
                row5_TieuDe4.Interior.Color = Color.Yellow;
                row5_TieuDe4.Value2 = "Tổ";

                //tô màu
                //Range range = oSheet.get_Range("A" + redRows.ToString(), "J" + redRows.ToString());
                //range.Cells.Interior.Color = System.Drawing.Color.Red;


                Excel.Range formatRange;
                int col = 6;

                while (iTNgay <= iDNgay)
                {
                    oSheet.Cells[4, col] = Convert.ToDateTime(lk_TuNgay.EditValue).AddDays(iTNgay - 1);
                    oSheet.Cells[4, col].Font.Name = fontName;
                    oSheet.Cells[4, col].Font.Bold = true;
                    oSheet.Cells[4, col].Interior.Color = Color.Yellow;
                    oSheet.Cells[4, col].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[4, col].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;


                    oSheet.Cells[5, col] = "Giờ Vào";
                    oSheet.Cells[5, col].Font.Bold = true;
                    oSheet.Cells[5, col].Interior.Color = Color.Yellow;
                    oSheet.Cells[5, col].Font.Name = fontName;
                    oSheet.Cells[5, col].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[5, col].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;



                    oSheet.Cells[5, col + 1] = "Giờ ra";
                    oSheet.Cells[5, col + 1].Interior.Color = Color.Yellow;
                    oSheet.Cells[5, col + 1].Font.Bold = true;
                    oSheet.Cells[5, col + 1].Font.Name = fontName;
                    oSheet.Cells[5, col + 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[5, col + 1].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;


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
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                formatRange.ColumnWidth = 5;
                //ma nv
                formatRange = oSheet.get_Range("B6", "B" + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                formatRange.ColumnWidth = 15;
                //ho ten
                formatRange = oSheet.get_Range("C5", "C" + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                formatRange.ColumnWidth = 35;
                //xí nghiệp
                formatRange = oSheet.get_Range("D5", "D" + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                formatRange.ColumnWidth = 20;
                //tổ
                formatRange = oSheet.get_Range("E5", "E" + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                formatRange.ColumnWidth = 20;

                //CẠNH giữa côt động
                formatRange = oSheet.get_Range("F4", lastColumn + rowCnt.ToString());
                formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //oWB.SaveAs("D:\\BangCongThang.xlsx",
                //AccessMode: Excel.XlSaveAsAccessMode.xlShared);

            }
            catch (Exception ex)
            {

            }
        }
        private void ChamCongChiTietCN()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn2;
            dt = new DataTable();
            string sTieuDe2 = "CHẤM CÔNG CHI TIẾT CÔNG NHÂN THEO GIAI ĐOẠN";
            frm.rpt = new rptBangCCTheoGD(lk_TuNgay.DateTime, lk_DenNgay.DateTime, sTieuDe2, lk_NgayIn.DateTime);

            try
            {
                conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn2.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptChiTietQuetTheCNGD", conn2);

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
                frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
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

                //SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                //if (SaveExcelFile == "")
                //{
                //    return;
                //}
                Excel.Application oXL;
                Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Excel.Application();
                oXL.Visible = true;

                oWB = (Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

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
                row3_TieuDe_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row3_TieuDe_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row3_TieuDe_BaoCao.RowHeight = 38;
                row3_TieuDe_BaoCao.Value2 = "BẢNG TỔNG HỢP CHẤM CÔNG THÁNG";

                Range row4_TieuDe_Ngay = oSheet.get_Range("A4", lastColumn + "4");
                row4_TieuDe_Ngay.Merge();
                row4_TieuDe_Ngay.Font.Size = fontSizeTieuDe;
                row4_TieuDe_Ngay.Font.Name = fontName;
                row4_TieuDe_Ngay.Font.Bold = true;
                row4_TieuDe_Ngay.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Ngay.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Ngay.RowHeight = 38;
                row4_TieuDe_Ngay.Value2 = "TỪ NGÀY 01/05/2022 ĐẾN NGÀY 31/05/2022";

                Range row5_TieuDe_Format = oSheet.get_Range("A5", lastColumn + "6"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
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

                Excel.Range formatRange;
                rowCnt = keepRowCnt + 2; // Cộng 2 vì ở trên thêm 2 dòng xí nghiệp và cộng
                //formatRange = oSheet.get_Range("G7", "G" + rowCnt.ToString());
                //formatRange.NumberFormat = "dd/MM/yyyy";
                //formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                //formatRange = oSheet.get_Range("H7", "H" + rowCnt.ToString());
                //formatRange.NumberFormat = "dd/MM/yyyy";
                //formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                //formatRange = oSheet.get_Range("I7", lastColumNgay + rowCnt.ToString());
                //formatRange.NumberFormat = "@";
                //formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                //dịnh dạng
                //Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 7);

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
                    try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                //colKT++;
                //CurentColumn = CharacterIncrement(colKT);
                //formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                ////formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
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

                //oWB.SaveAs(SaveExcelFile,
                //    AccessMode: Excel.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
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

        #endregion
    }

}
