﻿using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using Vs.Report;
using System.Reflection;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Vs.TimeAttendance
{
    public partial class ucBaoCaoTheoNgay : DevExpress.XtraEditors.XtraUserControl
    {
        private string SaveExcelFile;

        public ucBaoCaoTheoNgay()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
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
                                                //DSVangDauGioTheoNgay();
                                                BangChamCongNgay_DM();
                                                break;
                                            }
                                        default:
                                            BangChamCongNgay_DM();
                                            //DSVangDauGioTheoNgay();
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
                                                DSCongNhanVangDauGioNgay();
                                                break;
                                            }
                                        default:
                                            DSCongNhanVangDauGioNgay();
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
                                                DSCongNhanThieuNhomCa();
                                                break;
                                            }
                                        default:
                                            DSCongNhanThieuNhomCa();
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
                                                DSDiTreVeSomNgay();
                                                break;
                                            }
                                        default:
                                            DSDiTreVeSomNgay();
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
                                                DSNVTrungGioNgay();
                                                break;
                                            }
                                        default:
                                            DSNVTrungGioNgay();
                                            break;
                                    }
                                    break;
                                }
                            case 5:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)))
                                    {
                                        case "MT":
                                            {
                                                DSNVCoTren2CapGioChinh();
                                                break;
                                            }
                                        default:
                                            DSNVCoTren2CapGioChinh();
                                            break;
                                    }
                                    break;
                                }
                            case 6:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)))
                                    {
                                        case "MT":
                                            {
                                                DSNVVachTheLoiNgay();
                                                break;
                                            }
                                        default:
                                            DSNVVachTheLoiNgay();
                                            break;
                                    }
                                    break;
                                }

                            case 7:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)))
                                    {
                                        case "MT":
                                            {
                                                BieuMauDangKyLamThemGio();
                                                break;
                                            }
                                        default:
                                            BieuMauDangKyLamThemGio_DM();
                                            //BieuMauDangKyLamThemGio();
                                            break;
                                    }
                                    break;
                                }

                            case 8:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)))
                                    {
                                        case "MT":
                                            {
                                                DSNVTangCaNgay();
                                                break;
                                            }
                                        case "SB":
                                            {
                                                DanhSachTangCaNgay_SB();
                                                break;
                                            }
                                        default:
                                            DSNVTangCaNgay();
                                            break;
                                    }
                                    break;
                                }
                        }

                        break;
                    }
                default:
                    break;
            }
        }

        private void ucBaoCaoTheoNgay_Load(object sender, EventArgs e)
        {
            LoadCboDonVi();
            LoadCboXiNghiep();
            LoadCboTo();
            LoadNgay();
            LoadTinhTrangHopDong();
            rdo_DiTreVeSom.Visible = false;
            datNgayTangCa.Enabled = false;
            datNgayTangCa.EditValue = DateTime.Now;
            lk_NgayIn.EditValue = DateTime.Today;
            Commons.OSystems.SetDateEditFormat(lk_NgayIn);
        }


        private void LK_DON_VI_EditValueChanged(object sender, EventArgs e)
        {
            LoadCboXiNghiep();
            LoadCboTo();
        }

        private void LK_XI_NGHIEP_EditValueChanged(object sender, EventArgs e)
        {
            LoadCboTo();
        }

        private void rdo_ChonBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rdo_ChonBaoCao.SelectedIndex)
            {

                case 3:
                    {
                        rdo_DiTreVeSom.Visible = true;
                        break;
                    }
                case 8:
                    {
                        datNgayTangCa.Enabled = true;
                        break;
                    }

                default:
                    rdo_DiTreVeSom.Visible = false;
                    datNgayTangCa.Enabled = false;
                    break;
            }
        }

        private void grvThang_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                LK_NgayXemBaoCao.Text = grvThang.GetFocusedRowCellValue("NGAY_T").ToString();
            }
            catch { }
            LK_NgayXemBaoCao.ClosePopup();

        }

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                LK_NgayXemBaoCao.Text = calThang.DateTime.Date.ToShortDateString();
            }
            catch
            {
            }
            LK_NgayXemBaoCao.ClosePopup();
        }


        private void windowsUIButton_Click(object sender, EventArgs e)
        {

        }

        private void rdo_DiTreVeSom_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rdo_ChonBaoCao.SelectedIndex)
            {
                case 0:
                    {
                        rdo_DiTreVeSom.Visible = false;
                    }
                    break;
                case 1:
                    {
                        rdo_DiTreVeSom.Visible = false;
                    }
                    break;
                case 2:
                    {
                        rdo_DiTreVeSom.Visible = false;
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region function
        private void LoadNgay()
        {
            try
            {
                DataTable dtthang = new DataTable();
                string sSql = "SELECT DISTINCT  NGAY, CONVERT(NVARCHAR(10), A.NGAY, 103) AS NGAY_T FROM DU_LIEU_QUET_THE A ORDER BY NGAY DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);

                LK_NgayXemBaoCao.Text = grvThang.GetFocusedRowCellValue("NGAY_T").ToString();


            }
            catch (Exception ex)
            {

            }
            grvThang.Columns["NGAY"].Visible = false;
        }

        private void LoadCboDonVi()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_DON_VI, dt, "ID_DV", "TEN_DV", "TEN_DV");
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
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_XI_NGHIEP, dt, "ID_XN", "TEN_XN", "TEN_XN");
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
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(LK_TO, dt, "ID_TO", "TEN_TO", "TEN_TO");
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
        #endregion

        #region functionInTheoDV
        private void DSVangDauGioTheoNgay()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            frm.rpt = new rptDSVangDauGioTheoDV(lk_NgayIn.DateTime, Convert.ToDateTime(LK_NgayXemBaoCao.EditValue));

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptDSVangNgayDV"), conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(LK_NgayXemBaoCao.EditValue);
                cmd.Parameters.Add("@LoaiBC", SqlDbType.Int).Value = 1;
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
        private void DSVangDauGioTheoNgay_MT()
        {

        }
        private void DSCongNhanVangDauGioNgay()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn1;
            dt = new DataTable();
            frm.rpt = new rptDSVangDauGioTheoNgay(lk_NgayIn.DateTime, Convert.ToDateTime(LK_NgayXemBaoCao.EditValue));

            try
            {
                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptDSVangNgayDV"), conn1);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(LK_NgayXemBaoCao.EditValue);
                cmd.Parameters.Add("@LoaiBC", SqlDbType.Int).Value = 0;
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
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            frm.ShowDialog();
        }
        private void DSCongNhanVangDauGioNgay_MT()
        {

        }
        private void DSCongNhanThieuNhomCa()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn2;
            dt = new DataTable();
            frm.rpt = new rptDSNVThieuNhomCa(Convert.ToDateTime(LK_NgayXemBaoCao.EditValue));

            try
            {
                conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn2.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptDSNVThieuNhomCa"), conn2);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@NGAY", SqlDbType.Date).Value = Convert.ToDateTime(LK_NgayXemBaoCao.EditValue);
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
        private void DSCongNhanThieuNhomCa_MT()
        {

        }
        private void DSDiTreVeSomNgay()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn2;
            dt = new DataTable();
            string sTieuDe2 = "";
            switch (rdo_DiTreVeSom.SelectedIndex)
            {
                case 0:
                    {
                        sTieuDe2 = Commons.Modules.ObjLanguages.GetLanguage("rptDSDiTreVeSom", "lblDSNhanVienDiTre");
                    }
                    break;
                case 1:
                    {
                        sTieuDe2 = Commons.Modules.ObjLanguages.GetLanguage("rptDSDiTreVeSom", "lblDSNhanVienVeSom");
                    }
                    break;
                case 2:
                    {
                        sTieuDe2 = Commons.Modules.ObjLanguages.GetLanguage("rptDSDiTreVeSom", "lblDSNhanVienDiTreVeSom");
                    }
                    break;
                default:
                    break;
            }

            frm.rpt = new rptDSDiTreVeSom(Convert.ToDateTime(LK_NgayXemBaoCao.EditValue), sTieuDe2, Convert.ToDateTime(lk_NgayIn.EditValue));

            try
            {
                conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn2.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptDSDiTreVeSom"), conn2);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@NGAY", SqlDbType.Date).Value = Convert.ToDateTime(LK_NgayXemBaoCao.EditValue);
                switch (rdo_DiTreVeSom.SelectedIndex)
                {
                    case 0:
                        {
                            cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = 1;
                        }
                        break;
                    case 1:
                        {
                            cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = 2;
                        }
                        break;
                    case 2:
                        {
                            cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = 3;
                        }
                        break;
                    default:
                        break;
                }
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);


                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DA_TA";
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sbT" + Commons.Modules.UserName, dt, "");
                dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, " select ID_CN,MS_CN,HO_TEN,TEN_XN,TEN_TO,GIO_DEN,PHUT_TRE,GIO_VE,case PHUT_VS WHEN 0 THEN null else  PHUT_VS END as PHUT_VS from sbT" + Commons.Modules.UserName + ""));
                frm.AddDataSource(dt);
                frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                Commons.Modules.ObjSystems.XoaTable("sbT" + Commons.Modules.UserName);
            }
            catch
            { }


            frm.ShowDialog();
        }
        private void DSDiTreVeSomNgay_MT()
        {
        }
        private void DSNVTrungGioNgay()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            string sTieuDe = Commons.Modules.ObjLanguages.GetLanguage("rptDSNVTrungGio", "lblDSNhanVienTrungGioTheoNgay"); ;
            frm.rpt = new rptDSNVTrungGio(Convert.ToDateTime(LK_NgayXemBaoCao.EditValue), lk_NgayIn.DateTime, sTieuDe);
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptDSNVTrungGio"), conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = Convert.ToDateTime(LK_NgayXemBaoCao.EditValue);
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
        private void DSNVCoTren2CapGioChinh()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            frm.rpt = new rptDSNVCoTren2CapGio(Convert.ToDateTime(LK_NgayXemBaoCao.EditValue), Convert.ToDateTime(lk_NgayIn.EditValue));

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptDSNVCoTren2CapGio"), conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@NGAY", SqlDbType.Date).Value = Convert.ToDateTime(LK_NgayXemBaoCao.EditValue).ToString("yyyy/MM/dd");
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
        private void DSNVVachTheLoiNgay()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            frm.rpt = new rptDSNVVachTheLoi(Convert.ToDateTime(LK_NgayXemBaoCao.EditValue), lk_NgayIn.DateTime);
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptDSNVVachTheLoi"), conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@NGAY", SqlDbType.DateTime).Value = Convert.ToDateTime(LK_NgayXemBaoCao.EditValue);  //Convert.ToDateTime(LK_NgayXemBaoCao.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                //DataSet ds = new DataSet();
                dt = new DataTable();
                adp.Fill(dt);

                //dt = ds.Tables[0].Copy();
                dt.TableName = "DA_TA";
                frm.AddDataSource(dt);
                frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
            }
            catch (Exception ex)
            { }
            frm.ShowDialog();
        }
        private void BieuMauDangKyLamThemGio()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            frm.rpt = new rptDKLamThemGio(lk_NgayIn.DateTime, LK_XI_NGHIEP.Text.ToString());

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBieuMauDangKyLamThemGio", conn);

                cmd.Parameters.Add("@ID_DV", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@ID_XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@ID_TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = Convert.ToDateTime(lk_NgayIn.EditValue);

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                //DataSet ds = new DataSet();
                dt = new DataTable();
                adp.Fill(dt);

                //dt = ds.Tables[0].Copy();
                dt.TableName = "DA_TA";
                frm.AddDataSource(dt);
                frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
            }
            catch (Exception ex)
            { }
            frm.ShowDialog();
        }
        private void BieuMauDangKyLamThemGio_DM()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            frm.rpt = new rptDKLamThemGio_DM(lk_NgayIn.DateTime, LK_XI_NGHIEP.Text.ToString());

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBieuMauDangKyLamThemGio_DM", conn);

                cmd.Parameters.Add("@ID_DV", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@ID_XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@ID_TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = Convert.ToDateTime(lk_NgayIn.EditValue);

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                //DataSet ds = new DataSet();
                dt = new DataTable();
                adp.Fill(dt);

                //dt = ds.Tables[0].Copy();
                dt.TableName = "DA_TA";
                frm.AddDataSource(dt);
                frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
            }
            catch
            { }
            frm.ShowDialog();
        }
        private void DSNVTangCaNgay()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            frm.rpt = new rptDSNVTangCaTheoNgay(Convert.ToDateTime(datNgayTangCa.EditValue), lk_NgayIn.DateTime);

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.ObjSystems.returnSps(Commons.Modules.chamCongK, "rptDSTangCaNgay"), conn);

                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@NGAY", SqlDbType.Date).Value = Convert.ToDateTime(datNgayTangCa.EditValue);

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                //DataSet ds = new DataSet();
                dt = new DataTable();
                adp.Fill(dt);

                //dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);
            }
            catch
            { }
            frm.ShowDialog();
        }
        private void DanhSachTangCaNgay_SB()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSHangNgay_SB", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.Parameters.Add("@NGAY_TC", SqlDbType.Date).Value = Convert.ToDateTime(datNgayTangCa.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();


                //DataTable dtSLXN = new DataTable(); // Lấy số lượng xí nghiệp
                //dtSLXN = ds.Tables[1].Copy();
                //int slxn = Convert.ToInt32(dtSLXN.Rows[0][0]);

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
                int fontSizeNoiDung = 9;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 4);

                Range row1_TenDV = oSheet.get_Range("B1");
                row1_TenDV.Merge();
                row1_TenDV.Font.Size = 9;
                row1_TenDV.Font.Name = fontName;
                row1_TenDV.Value2 = dtBCThang.Rows[0]["TEN_DV"];
                row1_TenDV.WrapText = false;
                row1_TenDV.Style.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;


                Range row1_DiaChiDV = oSheet.get_Range("B2");
                row1_DiaChiDV.Merge();
                row1_DiaChiDV.Font.Size = 9;
                row1_DiaChiDV.Font.Name = fontName;
                row1_DiaChiDV.Font.Italic = true;
                row1_DiaChiDV.Value2 = dtBCThang.Rows[0]["DIA_CHI_DV"];
                row1_DiaChiDV.WrapText = false;
                row1_DiaChiDV.Style.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;


                Range row1_TieuDe_BaoCao = oSheet.get_Range("F1", "M1");
                row1_TieuDe_BaoCao.Merge();
                row1_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row1_TieuDe_BaoCao.Font.Name = fontName;
                row1_TieuDe_BaoCao.Font.Bold = true;
                row1_TieuDe_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row1_TieuDe_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row1_TieuDe_BaoCao.RowHeight = 15;
                row1_TieuDe_BaoCao.Value2 = "STILL NEED MORE HOURS  JUNE " + Convert.ToDateTime(datNgayTangCa.EditValue).Year + "";
                row1_TieuDe_BaoCao.Font.Color = Color.FromArgb(255, 0, 255);

                Range row2_TieuDe_BaoCao = oSheet.get_Range("F2", "M2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold Italic";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 15;
                row2_TieuDe_BaoCao.Value2 = " VĂN BẢN THỎA THUẬN LÀM THÊM GIỜ ………/  " + Convert.ToDateTime(datNgayTangCa.EditValue).Month + "  NĂM " + Convert.ToDateTime(datNgayTangCa.EditValue).Year + "";
                row2_TieuDe_BaoCao.Font.Color = Color.FromArgb(255, 0, 255);

                Range row1_MauSo = oSheet.get_Range("R1", "S1");
                row1_MauSo.Merge();
                row1_MauSo.Font.Name = fontName;
                row1_MauSo.Font.Size = 9;
                row1_MauSo.Value2 = "Mẫu số 01/PLIV";
                row1_MauSo.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row1_MauSo.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row1_MauSo.WrapText = false;

                Range row2_CHXH = oSheet.get_Range("P2", "S2");
                row2_CHXH.Merge();
                row2_CHXH.Font.Size = 9;
                row2_CHXH.Font.Name = fontName;
                row2_CHXH.Value2 = "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM";
                row2_CHXH.WrapText = false;
                row2_CHXH.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row2_CHXH.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                Range row3_DLTDHP = oSheet.get_Range("P3", "S3");
                row3_DLTDHP.Merge();
                row3_DLTDHP.Font.Size = 9;
                row3_DLTDHP.Font.Name = fontName;
                row3_DLTDHP.Font.Italic = true;
                row3_DLTDHP.Value2 = "Độc lập - Tự do - Hạnh phúc";
                row3_DLTDHP.WrapText = false;
                row3_DLTDHP.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row3_DLTDHP.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                Range row4_TieuDe_Format = oSheet.get_Range("A4", "S4"); //27 + 31
                row4_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Font.Color = Color.FromArgb(255, 0, 0);

                Range row5_TieuDe_Format = oSheet.get_Range("A5", "S5"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;


                //Range row7_groupXI_NGHIEP_Format = oSheet.get_Range("A7", lastColumn + "7"); //27 + 31
                //row7_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(0, 255, 255);
                //oSheet.Cells[7, 1] = "BỘ PHẬN";
                //oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 2]].Merge();
                //oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 2]].Font.Bold = true;




                //BorderAround(oSheet.get_Range("A5", lastColumn + "6"));


                Range row4_TieuDe_Stt = oSheet.get_Range("A4");
                row4_TieuDe_Stt.Merge();
                row4_TieuDe_Stt.Value2 = "STT";
                row4_TieuDe_Stt.ColumnWidth = 5;

                Range row5_TieuDe_Stt = oSheet.get_Range("A5");
                row5_TieuDe_Stt.Font.Color = Color.FromArgb(0, 0, 255);
                row5_TieuDe_Stt.Value2 = "No";
                row5_TieuDe_Stt.ColumnWidth = 5;

                Range row4_TieuDe_HoTen = oSheet.get_Range("B4");
                row4_TieuDe_HoTen.Value2 = "HỌ TÊN";
                row4_TieuDe_HoTen.ColumnWidth = 25;

                Range row6_TieuDe_HoTen = oSheet.get_Range("B5");
                row6_TieuDe_HoTen.Value2 = "フールネーム";
                row6_TieuDe_HoTen.ColumnWidth = 25;

                Range row4_TieuDe_MST = oSheet.get_Range("C4");
                row4_TieuDe_MST.Value2 = "MST";
                row4_TieuDe_MST.ColumnWidth = 8;

                Range row5_TieuDe_MST = oSheet.get_Range("C5");
                row5_TieuDe_MST.Value2 = "コード";
                row5_TieuDe_MST.ColumnWidth = 8;

                Range row4_TieuDe_GL = oSheet.get_Range("D4");
                row4_TieuDe_GL.Value2 = "Giờ làm";
                row4_TieuDe_GL.ColumnWidth = 7;

                Range row5_TieuDe_GL = oSheet.get_Range("D5");
                row5_TieuDe_GL.Value2 = "Time";
                row5_TieuDe_GL.ColumnWidth = 7;

                Range row4_TieuDe_NP = oSheet.get_Range("E4");
                row4_TieuDe_NP.Value2 = "Nghỉ phép";
                row4_TieuDe_NP.ColumnWidth = 7;

                Range row5_TieuDe_NP = oSheet.get_Range("E5");
                row5_TieuDe_NP.Value2 = "受給休暇";
                row5_TieuDe_NP.ColumnWidth = 7;

                Range row4_TieuDe_NKL = oSheet.get_Range("F4");
                row4_TieuDe_NKL.Value2 = "Nghỉ KL";
                row4_TieuDe_NKL.ColumnWidth = 7;

                Range row5_TieuDe_NKL = oSheet.get_Range("F5");
                row5_TieuDe_NKL.Value2 = "無給料";
                row5_TieuDe_NKL.ColumnWidth = 7;

                Range row4_TieuDe_NVLD = oSheet.get_Range("G4");
                row4_TieuDe_NVLD.Value2 = "Nghỉ VLD";
                row4_TieuDe_NVLD.ColumnWidth = 7;

                Range row5_TieuDe_NVLD = oSheet.get_Range("G5");
                row5_TieuDe_NVLD.Value2 = "無駄欠勤";
                row5_TieuDe_NVLD.ColumnWidth = 7;

                Range row4_TieuDe_20h = oSheet.get_Range("H4");
                row4_TieuDe_20h.Value2 = "16:30-20h";
                row4_TieuDe_20h.ColumnWidth = 10;

                Range row5_TieuDe_20h = oSheet.get_Range("H5");
                row5_TieuDe_20h.Value2 = "3.5";
                row5_TieuDe_20h.Interior.Color = Color.FromArgb(255, 255, 0);
                row5_TieuDe_20h.ColumnWidth = 10;

                Range row4_TieuDe_18h = oSheet.get_Range("I4");
                row4_TieuDe_18h.Value2 = "Giờ 16:30-18h";
                row4_TieuDe_18h.ColumnWidth = 10;

                Range row5_TieuDe_18h = oSheet.get_Range("I5");
                row5_TieuDe_18h.Value2 = "1.5";
                row5_TieuDe_18h.Interior.Color = Color.FromArgb(255, 255, 0);
                row5_TieuDe_18h.ColumnWidth = 10;

                Range row4_TieuDe_KHAC = oSheet.get_Range("J4");
                row4_TieuDe_KHAC.Value2 = "Khác";
                row4_TieuDe_KHAC.ColumnWidth = 12;

                Range row5_TieuDe_KHAC = oSheet.get_Range("J5");
                row5_TieuDe_KHAC.Value2 = "その他";
                row5_TieuDe_KHAC.ColumnWidth = 12;

                Range row4_TieuDe_LuyKe = oSheet.get_Range("K4");
                row4_TieuDe_LuyKe.Value2 = "Lũy kế tháng";
                row4_TieuDe_LuyKe.ColumnWidth = 7;

                Range row5_TieuDe_LuyKe = oSheet.get_Range("K5");
                row5_TieuDe_LuyKe.Value2 = "累計";
                row5_TieuDe_LuyKe.ColumnWidth = 7;


                Range row4_TieuDe_KyTen = oSheet.get_Range("L4");
                row4_TieuDe_KyTen.Value2 = "Đồng ý ký tên";
                row4_TieuDe_KyTen.ColumnWidth = 10;

                Range row4_TieuDe_CVDangLam = oSheet.get_Range("M4");
                row4_TieuDe_CVDangLam.Value2 = "Nghề công việc đang làm";
                row4_TieuDe_CVDangLam.ColumnWidth = 25;

                Range row5_TieuDe_CVDangLam = oSheet.get_Range("M5");
                row5_TieuDe_CVDangLam.Value2 = "工程ネーム";
                row5_TieuDe_CVDangLam.ColumnWidth = 25;

                Range row4_TieuDe_KH = oSheet.get_Range("N4");
                row4_TieuDe_KH.Value2 = "KH";
                row4_TieuDe_KH.ColumnWidth = 7;

                Range row5_TieuDe_KH = oSheet.get_Range("N5");
                row5_TieuDe_KH.Value2 = "計画";
                row5_TieuDe_KH.ColumnWidth = 7;

                Range row4_TieuDe_TTE = oSheet.get_Range("O4");
                row4_TieuDe_TTE.Value2 = "TTẾ";
                row4_TieuDe_TTE.ColumnWidth = 7;

                Range row5_TieuDe_TTE = oSheet.get_Range("O5");
                row5_TieuDe_TTE.Value2 = "実行";
                row5_TieuDe_TTE.ColumnWidth = 7;

                Range row4_TieuDe_BCTRUA = oSheet.get_Range("P4");
                row4_TieuDe_BCTRUA.Value2 = "Báo cơm trưa";
                row4_TieuDe_BCTRUA.ColumnWidth = 8;

                Range row5_TieuDe_BCTRUA = oSheet.get_Range("P5");
                row5_TieuDe_BCTRUA.Value2 = "昼食事";
                row5_TieuDe_BCTRUA.ColumnWidth = 8;

                Range row4_TieuDe_BCTC = oSheet.get_Range("Q4");
                row4_TieuDe_BCTC.Value2 = "Báo cơm tăng ca";
                row4_TieuDe_BCTC.ColumnWidth = 8;

                Range row5_TieuDe_BCTC = oSheet.get_Range("Q5");
                row5_TieuDe_BCTC.Value2 = "残業食事";
                row5_TieuDe_BCTC.ColumnWidth = 8;

                Range row4_TieuDe_PCSL = oSheet.get_Range("R4", "S4");
                row4_TieuDe_PCSL.Merge();
                row4_TieuDe_PCSL.Value2 = "P.chuyển -SL";
                row4_TieuDe_PCSL.ColumnWidth = 12;

                Range row5_TieuDe_PCSL_S = oSheet.get_Range("R5");
                row5_TieuDe_PCSL_S.Merge();
                row5_TieuDe_PCSL_S.Value2 = "  午前";
                row5_TieuDe_PCSL_S.ColumnWidth = 7;

                Range row5_TieuDe_PCSL_C = oSheet.get_Range("S5");
                row5_TieuDe_PCSL_C.Merge();
                row5_TieuDe_PCSL_C.Value2 = "  午後";
                row5_TieuDe_PCSL_C.ColumnWidth = 12;

                int col = 0;
                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                //int rowCONG = 0; // Row để insert dòng tổng
                //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                int rowBD = 6;
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
                        if (dr_Cu < 14)
                        {
                            rowBD_XN = 16 - dr_Cu;
                        }
                        else
                        {
                            rowBD_XN = 1;
                        }
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

                    Range row_20h_Format = oSheet.get_Range("Q" + rowBD + "".ToString()); //27 + 31
                    row_20h_Format.Interior.Color = Color.FromArgb(255, 255, 0);
                    row_20h_Format.Font.Color = Color.FromArgb(0, 0, 0);
                    row_20h_Format.Font.Name = fontName;
                    row_20h_Format.Font.Bold = true;
                    row_20h_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    row_20h_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    oSheet.Cells[rowBD, 17] = "20h";


                    Range row_SangChieu_Format = oSheet.get_Range("S" + rowBD + "".ToString()); //27 + 31
                    row_SangChieu_Format.Font.Name = fontName;
                    row_SangChieu_Format.Font.Color = Color.FromArgb(0, 0, 0);
                    row_SangChieu_Format.Font.Bold = true;
                    row_SangChieu_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    row_SangChieu_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    oSheet.Cells[rowBD, 19] = "Sáng-Chiều";

                    Range row_InDamDongDau_Format = oSheet.get_Range("A" + (rowBD + 1) + "".ToString(), lastColumn + "" + (rowBD + 1) + "".ToString()); //27 + 31
                    row_InDamDongDau_Format.Font.Bold = true;

                    //Đổ dữ liệu của tổ
                    oSheet.get_Range("A" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;

                    // Format col 16 - P
                    Range row_Chay_Col16 = oSheet.get_Range("P" + (rowBD + 1) + "".ToString()); //27 + 31
                    row_Chay_Col16.Font.Name = fontName;
                    oSheet.Cells[rowBD + 1, 16] = "Chay:";

                    Range row_Thit_Col16 = oSheet.get_Range("P" + (rowBD + 2) + "".ToString()); //27 + 31
                    row_Thit_Col16.Font.Name = fontName;
                    oSheet.Cells[rowBD + 2, 16] = "Thịt:";

                    Range row_Ca_Col16 = oSheet.get_Range("P" + (rowBD + 3) + "".ToString()); //27 + 31
                    row_Ca_Col16.Font.Name = fontName;
                    oSheet.Cells[rowBD + 3, 16] = "Cá:";

                    Range row_Chao_Col16 = oSheet.get_Range("P" + (rowBD + 4) + "".ToString()); //27 + 31
                    row_Chao_Col16.Font.Name = fontName;
                    oSheet.Cells[rowBD + 4, 16] = "Cháo T:";

                    Range row_CTrung_Col16 = oSheet.get_Range("P" + (rowBD + 5) + "".ToString()); //27 + 31
                    row_CTrung_Col16.Font.Name = fontName;
                    oSheet.Cells[rowBD + 5, 16] = "C.Trứng:";

                    Range row_TC_Col16 = oSheet.get_Range("P" + (rowBD + 6) + "".ToString()); //27 + 31
                    row_TC_Col16.Font.Name = fontName;
                    row_TC_Col16.Font.Bold = true;
                    oSheet.Cells[rowBD + 6, 16] = "TC:";

                    Range row_HoTen_Col16 = oSheet.get_Range("P" + (rowBD + 13) + "".ToString(), "Q" + (rowBD + 13) + "".ToString()); //27 + 31
                    row_HoTen_Col16.Font.Name = fontName;
                    row_HoTen_Col16.Merge();
                    oSheet.Cells[rowBD + 13, 16] = "Họ và tên  T.Việc+T.Vụ";

                    // Format col 17 - Q
                    Range row_Chay_Col17 = oSheet.get_Range("Q" + (rowBD + 1) + "".ToString()); //27 + 31
                    row_Chay_Col17.Font.Name = fontName;
                    oSheet.Cells[rowBD + 1, 17] = "Chay:";

                    Range row_Man_Col17 = oSheet.get_Range("Q" + (rowBD + 2) + "".ToString()); //27 + 31
                    row_Man_Col17.Font.Name = fontName;
                    oSheet.Cells[rowBD + 2, 17] = "Mặn:";

                    Range row_Sua_Col17 = oSheet.get_Range("Q" + (rowBD + 3) + "".ToString()); //27 + 31
                    row_Sua_Col17.Font.Name = fontName;
                    oSheet.Cells[rowBD + 3, 17] = "Sữa:";

                    Range row_Chao_Col17 = oSheet.get_Range("Q" + (rowBD + 4) + "".ToString()); //27 + 31
                    row_Chao_Col17.Font.Name = fontName;
                    oSheet.Cells[rowBD + 4, 17] = "Cháo:";

                    Range row_MiGoi_Col17 = oSheet.get_Range("Q" + (rowBD + 5) + "".ToString()); //27 + 31
                    row_MiGoi_Col17.Font.Name = fontName;
                    oSheet.Cells[rowBD + 5, 17] = "Mì gói:";

                    Range row_BanhSua_Col17 = oSheet.get_Range("Q" + (rowBD + 6) + "".ToString()); //27 + 31
                    row_BanhSua_Col17.Font.Name = fontName;
                    oSheet.Cells[rowBD + 6, 17] = "Bánh+Sữa:";

                    Range row_Com_Col17 = oSheet.get_Range("Q" + (rowBD + 7) + "".ToString()); //27 + 31
                    row_Com_Col17.Font.Name = fontName;
                    oSheet.Cells[rowBD + 7, 17] = "Cơm:";

                    Range row_TC_Col17 = oSheet.get_Range("Q" + (rowBD + 8) + "".ToString()); //27 + 31
                    row_TC_Col17.Font.Name = fontName;
                    row_TC_Col17.Font.Bold = true;
                    oSheet.Cells[rowBD + 8, 17] = "TC:";


                    // Format col 19 - S
                    Range row_Chay_Col19 = oSheet.get_Range("S" + (rowBD + 1) + "".ToString()); //27 + 31
                    row_Chay_Col19.Font.Name = fontName;
                    oSheet.Cells[rowBD + 1, 19] = "L1:";

                    Range row_Man_Col19 = oSheet.get_Range("S" + (rowBD + 2) + "".ToString()); //27 + 31
                    row_Man_Col19.Font.Name = fontName;
                    oSheet.Cells[rowBD + 2, 19] = "L2:";

                    Range row_Sua_Col19 = oSheet.get_Range("S" + (rowBD + 3) + "".ToString()); //27 + 31
                    row_Sua_Col19.Font.Name = fontName;
                    oSheet.Cells[rowBD + 3, 19] = "L3:";

                    Range row_L4_Col19 = oSheet.get_Range("S" + (rowBD + 4) + "".ToString()); //27 + 31
                    row_L4_Col19.Font.Name = fontName;
                    oSheet.Cells[rowBD + 4, 19] = "L4:";

                    Range row_L5_Col19 = oSheet.get_Range("S" + (rowBD + 5) + "".ToString()); //27 + 31
                    row_L5_Col19.Font.Name = fontName;
                    oSheet.Cells[rowBD + 5, 19] = "L5:";

                    Range row_L6_Col19 = oSheet.get_Range("S" + (rowBD + 6) + "".ToString()); //27 + 31
                    row_L6_Col19.Font.Name = fontName;
                    oSheet.Cells[rowBD + 6, 19] = "L6:";


                    Range row_sup_Col19 = oSheet.get_Range("S" + (rowBD + 7) + "".ToString()); //27 + 31
                    row_sup_Col19.Font.Name = fontName;
                    oSheet.Cells[rowBD + 7, 19] = "Sup:";

                    Range row_button_Col19 = oSheet.get_Range("S" + (rowBD + 8) + "".ToString()); //27 + 31
                    row_button_Col19.Font.Name = fontName;
                    oSheet.Cells[rowBD + 8, 19] = "Button:";

                    Range row_cutting_Col19 = oSheet.get_Range("S" + (rowBD + 9) + "".ToString()); //27 + 31
                    row_cutting_Col19.Font.Name = fontName;
                    oSheet.Cells[rowBD + 9, 19] = "Cutting:";

                    Range row_FN2_Col19 = oSheet.get_Range("S" + (rowBD + 10) + "".ToString()); //27 + 31
                    row_FN2_Col19.Font.Name = fontName;
                    oSheet.Cells[rowBD + 10, 19] = "FN2:";

                    Range row_Tvu_Col19 = oSheet.get_Range("S" + (rowBD + 11) + "".ToString()); //27 + 31
                    row_Tvu_Col19.Font.Name = fontName;
                    oSheet.Cells[rowBD + 11, 19] = "Tvụ:";

                    Range row_Tviec_Col19 = oSheet.get_Range("S" + (rowBD + 12) + "".ToString()); //27 + 31
                    row_Tviec_Col19.Font.Name = fontName;
                    oSheet.Cells[rowBD + 12, 19] = "Tviệc:";

                    Range row_KyTen_Col19 = oSheet.get_Range("S" + (rowBD + 13) + "".ToString()); //27 + 31
                    row_KyTen_Col19.Font.Name = fontName;
                    row_KyTen_Col19.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    row_KyTen_Col19.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    oSheet.Cells[rowBD + 13, 19] = "Ký tên:";




                    dr_Cu = current_dr;
                    keepRowCnt = rowCnt;
                    rowCnt = 0;
                }

                Excel.Range formatRange;
                rowCnt = keepRowCnt + 2;

                //dịnh dạng
                //Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 7);

                string CurentColumn = string.Empty;
                int colBD = 4;
                int colKT = dtBCThang.Columns.Count;
                //format

                //for (col = colBD; col < dtBCThang.Columns.Count - 3; col++)
                //{
                //    CurentColumn = CharacterIncrement(col);
                //    formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
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
                int ke_khung = -1;

                if (dr_Cu < 15)
                {
                    ke_khung = 14 - dr_Cu;
                }
                formatRange = oSheet.get_Range("A6", lastColumn + (rowCnt + ke_khung).ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                BorderAround(oSheet.get_Range("A4", lastColumn + (rowCnt + ke_khung).ToString()));
                // filter
                oSheet.Application.ActiveWindow.SplitColumn = 4;
                oSheet.Application.ActiveWindow.FreezePanes = true;
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
        private void BangChamCongNgay_DM()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangCongNgay_DM", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();

                DataTable dtSLXN = new DataTable(); // Lấy số lượng xí nghiệp
                dtSLXN = ds.Tables[1].Copy();
                int slXN = Convert.ToInt32(dtSLXN.Rows[0][0]);

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

                oWB = (Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
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



                Range row2_TieuDe = oSheet.get_Range("B2", "N2");
                row2_TieuDe.Font.Bold = true;
                row2_TieuDe.Merge();
                row2_TieuDe.Font.Size = 12;
                row2_TieuDe.Font.Name = fontName;
                row2_TieuDe.Value2 = "BÁO CÁO HÀNG NGÀY/ DAILY ATTENDANCE REPORT";
                row2_TieuDe.WrapText = false;
                row2_TieuDe.RowHeight = 33;
                row2_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe.Interior.Color = Color.FromArgb(248, 203, 173);


                Range row3_Date = oSheet.get_Range("L3", "N3");
                row3_Date.Font.Bold = true;
                row3_Date.Merge();
                row3_Date.Font.Size = 12;
                row3_Date.Font.Name = fontName;
                row3_Date.Value2 = "Ngày/ Date:19-05-2022";
                row3_Date.WrapText = false;
                row3_Date.RowHeight = 24;
                row3_Date.Style.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row3_Date.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;


                Range row4 = oSheet.get_Range("B4");
                row4.RowHeight = 66;

                Range row5 = oSheet.get_Range("B5");
                row5.RowHeight = 79;

                Range row5_TieuDe_Format = oSheet.get_Range("B4", lastColumn + "5"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeTieuDe;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;


                Range row1_TieuDe_Stt = oSheet.get_Range("A1");
                row1_TieuDe_Stt.ColumnWidth = 2;

                Range row5_TieuDe_Stt = oSheet.get_Range("B4", "B5");
                row5_TieuDe_Stt.Merge();
                row5_TieuDe_Stt.Value2 = "Phòng ban/ Section";
                row5_TieuDe_Stt.Interior.Color = Color.FromArgb(198, 224, 180);

                Range row6_TieuDe_Stt = oSheet.get_Range("C4", "C5");
                row6_TieuDe_Stt.Merge();
                row6_TieuDe_Stt.Value2 = "Đơn vị/ Department Vị trí/ Position";
                row6_TieuDe_Stt.ColumnWidth = 30;
                row6_TieuDe_Stt.Interior.Color = Color.FromArgb(198, 224, 180);


                Range row5_TieuDe_MaSo = oSheet.get_Range("D4", "D5");
                row5_TieuDe_MaSo.Merge();
                row5_TieuDe_MaSo.Value2 = "Tổng lao động hôm trước/ Total employees y";
                row5_TieuDe_MaSo.ColumnWidth = 15;
                row5_TieuDe_MaSo.Interior.Color = Color.FromArgb(226, 239, 218);


                Range row6_TieuDe_MaSo = oSheet.get_Range("E4", "E5");
                row6_TieuDe_MaSo.Merge();
                row6_TieuDe_MaSo.Value2 = "Nghỉ việc/ Resigned";
                row6_TieuDe_MaSo.ColumnWidth = 12;
                row6_TieuDe_MaSo.Interior.Color = Color.FromArgb(226, 239, 218);


                Range row5_TieuDe_HoTen = oSheet.get_Range("F4", "F5");
                row5_TieuDe_HoTen.Merge();
                row5_TieuDe_HoTen.Value2 = "Người mới/ New comer";
                row5_TieuDe_HoTen.ColumnWidth = 14;
                row5_TieuDe_HoTen.Interior.Color = Color.FromArgb(226, 239, 218);


                Range row6_TieuDe_HoTen = oSheet.get_Range("G4", "G5");
                row6_TieuDe_HoTen.Merge();
                row6_TieuDe_HoTen.Value2 = "Tổng lao động/ Total employees";
                row6_TieuDe_HoTen.ColumnWidth = 12;
                row6_TieuDe_HoTen.Interior.Color = Color.FromArgb(255, 255, 0);



                Range row5_TieuDe_To = oSheet.get_Range("H4", "L4");
                row5_TieuDe_To.Merge();
                row5_TieuDe_To.Value2 = "Tổng lao động vắng mặt/ Total Absence";
                row5_TieuDe_To.Interior.Color = Color.FromArgb(255, 230, 153);


                Range row6_TieuDe_TS = oSheet.get_Range("H5");
                row6_TieuDe_TS.Merge();
                row6_TieuDe_TS.Value2 = "Thai sản/ Maternity";
                row6_TieuDe_TS.ColumnWidth = 12;
                row6_TieuDe_TS.Interior.Color = Color.FromArgb(255, 230, 153);


                Range row6_TieuDe_LDNCL = oSheet.get_Range("I5");
                row6_TieuDe_LDNCL.Merge();
                row6_TieuDe_LDNCL.Value2 = "LĐ nghỉ cách ly";
                row6_TieuDe_LDNCL.ColumnWidth = 12;
                row6_TieuDe_LDNCL.Interior.Color = Color.FromArgb(255, 230, 153);


                Range row6_TieuDe_VCLD = oSheet.get_Range("J5");
                row6_TieuDe_VCLD.Merge();
                row6_TieuDe_VCLD.Value2 = "Vắng có lý do/ Absence have reason";
                row6_TieuDe_VCLD.ColumnWidth = 10;
                row6_TieuDe_VCLD.Interior.Color = Color.FromArgb(255, 230, 153);


                Range row6_TieuDe_VKLD = oSheet.get_Range("K5");
                row6_TieuDe_VKLD.Merge();
                row6_TieuDe_VKLD.Value2 = "Vắng không có lý do/ Absence no reason";
                row6_TieuDe_VKLD.ColumnWidth = 10;
                row6_TieuDe_VKLD.Interior.Color = Color.FromArgb(255, 230, 153);


                Range row6_TieuDe_Tong = oSheet.get_Range("L5");
                row6_TieuDe_Tong.Merge();
                row6_TieuDe_Tong.Value2 = "Tổng/ Total";
                row6_TieuDe_Tong.ColumnWidth = 8;
                row6_TieuDe_Tong.Interior.Color = Color.FromArgb(255, 230, 153);


                Range row6_TieuDe_SLD = oSheet.get_Range("M4", "M5");
                row6_TieuDe_SLD.Merge();
                row6_TieuDe_SLD.Value2 = "Số lao động có mặt/ Total employees present";
                row6_TieuDe_SLD.ColumnWidth = 13;
                row6_TieuDe_SLD.Interior.Color = Color.FromArgb(189, 215, 238);


                Range row6_TieuDe_TLV = oSheet.get_Range("N4", "N5");
                row6_TieuDe_TLV.Merge();
                row6_TieuDe_TLV.Value2 = "Tỷ lệ vắng (%)";
                row6_TieuDe_TLV.ColumnWidth = 11;
                row6_TieuDe_TLV.Interior.Color = Color.FromArgb(255, 255, 0);


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
                string sTenCotD = "=";
                string sTenCotE = "=";
                string sTenCotF = "=";
                string sTenCotH = "=";
                string sTenCotI = "=";
                string sTenCotJ = "=";
                string sTenCotK = "=";
                string sTenCotL = "=";
                string sTenCotM = "=";

                string sRowXN = "";
                string s = int_to_Roman(9);
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
                            //Excel.Range formatRange7;
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
                    oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 2]].Font.Size = fontSizeNoiDung;
                    oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 2]].Font.Name = fontName;

                    oSheet.Cells[rowBD, 3] = "Sub-Total " + int_to_Roman(i + 1) + "";
                    oSheet.Cells[rowBD, 3].Font.Bold = true;
                    oSheet.Cells[rowBD, 3].Font.Size = fontSizeNoiDung;
                    oSheet.Cells[rowBD, 3].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowBD, 3].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    oSheet.Cells[rowBD, 3].Font.Name = fontName;

                    oSheet.Cells[rowBD, 4] = "=SUM(" + CharacterIncrement(3) + "" + (rowBD + 1) + ":" + CharacterIncrement(3) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 4].Font.Bold = true;
                    oSheet.Cells[rowBD, 4].Font.Color = Color.FromArgb(255, 0, 0);
                    oSheet.Cells[rowBD, 4].Font.Size = fontSizeNoiDung;
                    oSheet.Cells[rowBD, 4].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowBD, 4].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    oSheet.Cells[rowBD, 4].Font.Name = fontName;
                    sTenCotD = sTenCotD + CharacterIncrement(3) + rowBD + "+";
                    sRowXN = sRowXN + rowBD + ",";

                    oSheet.Cells[rowBD, 5] = "=SUM(" + CharacterIncrement(4) + "" + (rowBD + 1) + ":" + CharacterIncrement(4) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 5].Font.Bold = true;
                    oSheet.Cells[rowBD, 5].Font.Color = Color.FromArgb(255, 0, 0);
                    oSheet.Cells[rowBD, 5].Font.Size = fontSizeNoiDung;
                    oSheet.Cells[rowBD, 5].Font.Name = fontName;
                    oSheet.Cells[rowBD, 5].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowBD, 5].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    sTenCotE = sTenCotE + CharacterIncrement(4) + rowBD + "+";

                    oSheet.Cells[rowBD, 6] = "=SUM(" + CharacterIncrement(5) + "" + (rowBD + 1) + ":" + CharacterIncrement(5) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 6].Font.Bold = true;
                    oSheet.Cells[rowBD, 6].Font.Color = Color.FromArgb(255, 0, 0);
                    oSheet.Cells[rowBD, 6].Font.Size = fontSizeNoiDung;
                    oSheet.Cells[rowBD, 6].Font.Name = fontName;
                    oSheet.Cells[rowBD, 6].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowBD, 6].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    sTenCotF = sTenCotF + CharacterIncrement(5) + rowBD + "+";

                    //oSheet.Cells[rowBD, 7] = "=SUM(" + CharacterIncrement(6) + "" + (rowBD + 1) + ":" + CharacterIncrement(6) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 7] = "=D" + rowBD + "+F" + rowBD + "-E" + rowBD + "";
                    oSheet.Cells[rowBD, 7].Font.Bold = true;
                    oSheet.Cells[rowBD, 7].Font.Size = fontSizeNoiDung;
                    oSheet.Cells[rowBD, 7].Font.Name = fontName;
                    oSheet.Cells[rowBD, 7].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowBD, 7].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                    oSheet.Cells[rowBD, 8] = "=SUM(" + CharacterIncrement(7) + "" + (rowBD + 1) + ":" + CharacterIncrement(7) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 8].Font.Bold = true;
                    oSheet.Cells[rowBD, 8].Font.Size = fontSizeNoiDung;
                    oSheet.Cells[rowBD, 8].Font.Color = Color.FromArgb(255, 0, 0);
                    oSheet.Cells[rowBD, 8].Font.Name = fontName;
                    oSheet.Cells[rowBD, 8].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowBD, 8].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    sTenCotH = sTenCotH + CharacterIncrement(7) + rowBD + "+";


                    oSheet.Cells[rowBD, 9] = "=SUM(" + CharacterIncrement(8) + "" + (rowBD + 1) + ":" + CharacterIncrement(8) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 9].Font.Bold = true;
                    oSheet.Cells[rowBD, 9].Font.Size = fontSizeNoiDung;
                    oSheet.Cells[rowBD, 9].Font.Color = Color.FromArgb(255, 0, 0);
                    oSheet.Cells[rowBD, 9].Font.Name = fontName;
                    oSheet.Cells[rowBD, 9].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowBD, 9].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    sTenCotI = sTenCotI + CharacterIncrement(8) + rowBD + "+";


                    oSheet.Cells[rowBD, 10] = "=SUM(" + CharacterIncrement(9) + "" + (rowBD + 1) + ":" + CharacterIncrement(9) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 10].Font.Bold = true;
                    oSheet.Cells[rowBD, 10].Font.Size = fontSizeNoiDung;
                    oSheet.Cells[rowBD, 10].Font.Color = Color.FromArgb(255, 0, 0);
                    oSheet.Cells[rowBD, 10].Font.Name = fontName;
                    oSheet.Cells[rowBD, 10].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowBD, 10].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    sTenCotJ = sTenCotJ + CharacterIncrement(9) + rowBD + "+";


                    oSheet.Cells[rowBD, 11] = "=SUM(" + CharacterIncrement(10) + "" + (rowBD + 1) + ":" + CharacterIncrement(10) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 11].Font.Bold = true;
                    oSheet.Cells[rowBD, 11].Font.Size = fontSizeNoiDung;
                    oSheet.Cells[rowBD, 11].Font.Color = Color.FromArgb(255, 0, 0);
                    oSheet.Cells[rowBD, 11].Font.Name = fontName;
                    oSheet.Cells[rowBD, 11].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowBD, 11].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    sTenCotK = sTenCotK + CharacterIncrement(10) + rowBD + "+";


                    oSheet.Cells[rowBD, 12] = "=SUM(I" + rowBD + ":K" + rowBD + ")";
                    oSheet.Cells[rowBD, 12].Font.Bold = true;
                    oSheet.Cells[rowBD, 12].Font.Size = fontSizeNoiDung;
                    oSheet.Cells[rowBD, 12].Font.Color = Color.FromArgb(255, 0, 0);
                    oSheet.Cells[rowBD, 12].Font.Name = fontName;
                    oSheet.Cells[rowBD, 12].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowBD, 12].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    sTenCotL = sTenCotL + CharacterIncrement(11) + rowBD + "+";


                    oSheet.Cells[rowBD, 13] = "=G" + rowBD + "-L" + rowBD + "-H" + rowBD + "";
                    oSheet.Cells[rowBD, 13].Font.Bold = true;
                    oSheet.Cells[rowBD, 13].Font.Size = fontSizeNoiDung;
                    oSheet.Cells[rowBD, 13].Font.Name = fontName;
                    oSheet.Cells[rowBD, 13].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowBD, 13].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    sTenCotM = sTenCotM + CharacterIncrement(12) + rowBD + "+";


                    oSheet.Cells[rowBD, 14] = "=IFERROR(L" + rowBD + "/G" + rowBD + ",0)";
                    oSheet.Cells[rowBD, 14].Font.Bold = true;
                    oSheet.Cells[rowBD, 14].Font.Size = fontSizeNoiDung;
                    oSheet.Cells[rowBD, 14].Font.Color = Color.FromArgb(255, 0, 0);
                    oSheet.Cells[rowBD, 14].Font.Name = fontName;
                    oSheet.Cells[rowBD, 14].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowBD, 14].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    //Đổ dữ liệu của xí nghiệp
                    oSheet.get_Range("B" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;

                    ////Tính tổng xí nghiệp
                    //Range row_groupTONG_Format = oSheet.get_Range("A" + (rowBD + current_dr + 1) + "".ToString(), lastColumn + "" + (rowBD + current_dr + 1) + "".ToString()); //27 + 31 // (rowBD + current_dr +1) sẽ lấy cái dòng bắt đầu (7) + dòng dữ liệu (ví dụ là 2 dòng) = 9 thì cột cộng sẽ + thêm 1 dòng nữa  = 10
                    //row_groupTONG_Format.Interior.Color = Color.Yellow;
                    //row_groupTONG_Format.Font.Bold = true;
                    //oSheet.Cells[(rowBD + current_dr + 1), 1] = "Cộng";
                    //oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 1], oSheet.Cells[Convert.ToInt32(rowBD + current_dr + 1), 4]].Merge();

                    //for (int colSUM = 5; colSUM < dtBCThang.Columns.Count - 2; colSUM++)
                    //{
                    //    oSheet.Cells[(rowBD + current_dr + 1), colSUM] = "=SUM(" + CellAddress(oSheet, rowBD + 1, colSUM) + ":" + CellAddress(oSheet, (rowBD + current_dr), colSUM) + ")";
                    //}
                    dr_Cu = current_dr;
                    keepRowCnt = rowCnt;
                    rowCnt = 0;
                }
                rowCnt = keepRowCnt;
                Excel.Range formatRange;
                Excel.Range formatRange1;
                Excel.Range formatRange3;
                string CurentColumn = string.Empty;
                int rowbd;
                int colKT = dtBCThang.Columns.Count;
                for (rowbd = 8; rowbd <= rowCnt; rowbd++)
                {
                    formatRange = oSheet.get_Range("B" + rowbd + "");
                    formatRange1 = oSheet.get_Range("B" + (rowbd + 1).ToString());

                    if (formatRange.Value == formatRange1.Value)
                    {
                        formatRange1.Value = null;
                        formatRange3 = oSheet.get_Range("B" + rowbd + "", "B" + (rowbd + 1).ToString());
                        formatRange3.Merge();
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


                sTenCotD = sTenCotD.Substring(0, sTenCotD.Length - 1);
                sTenCotE = sTenCotE.Substring(0, sTenCotE.Length - 1);
                sTenCotF = sTenCotF.Substring(0, sTenCotF.Length - 1);
                sTenCotH = sTenCotH.Substring(0, sTenCotH.Length - 1);
                sTenCotI = sTenCotI.Substring(0, sTenCotI.Length - 1);
                sTenCotJ = sTenCotJ.Substring(0, sTenCotJ.Length - 1);
                sTenCotK = sTenCotK.Substring(0, sTenCotK.Length - 1);
                sTenCotL = sTenCotL.Substring(0, sTenCotL.Length - 1);
                sTenCotM = sTenCotM.Substring(0, sTenCotM.Length - 1);

                oSheet.Cells[rowCnt, 4] = sTenCotD;
                oSheet.Cells[rowCnt, 4].Font.Bold = true;
                oSheet.Cells[rowCnt, 4].Font.Color = Color.FromArgb(255, 0, 0);
                oSheet.Cells[rowCnt, 4].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[rowCnt, 4].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[rowCnt, 5] = sTenCotE;
                oSheet.Cells[rowCnt, 5].Font.Bold = true;
                oSheet.Cells[rowCnt, 5].Font.Color = Color.FromArgb(255, 0, 0);
                oSheet.Cells[rowCnt, 5].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[rowCnt, 5].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[rowCnt, 6] = sTenCotF;
                oSheet.Cells[rowCnt, 6].Font.Bold = true;
                oSheet.Cells[rowCnt, 6].Font.Color = Color.FromArgb(255, 0, 0);
                oSheet.Cells[rowCnt, 6].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[rowCnt, 6].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[rowCnt, 7] = "=D" + rowCnt + "+F" + rowCnt + "-E" + rowCnt + "";
                oSheet.Cells[rowCnt, 7].Font.Bold = true;
                oSheet.Cells[rowCnt, 7].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[rowCnt, 7].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[rowCnt, 8] = sTenCotH;
                oSheet.Cells[rowCnt, 8].Font.Bold = true;
                oSheet.Cells[rowCnt, 8].Font.Color = Color.FromArgb(255, 0, 0);
                oSheet.Cells[rowCnt, 8].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[rowCnt, 8].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[rowCnt, 9] = sTenCotI;
                oSheet.Cells[rowCnt, 9].Font.Bold = true;
                oSheet.Cells[rowCnt, 9].Font.Color = Color.FromArgb(255, 0, 0);
                oSheet.Cells[rowCnt, 9].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[rowCnt, 9].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[rowCnt, 10] = sTenCotJ;
                oSheet.Cells[rowCnt, 10].Font.Bold = true;
                oSheet.Cells[rowCnt, 10].Font.Color = Color.FromArgb(255, 0, 0);
                oSheet.Cells[rowCnt, 10].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[rowCnt, 10].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[rowCnt, 11] = sTenCotK;
                oSheet.Cells[rowCnt, 11].Font.Bold = true;
                oSheet.Cells[rowCnt, 11].Font.Color = Color.FromArgb(255, 0, 0);
                oSheet.Cells[rowCnt, 11].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[rowCnt, 11].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[rowCnt, 12] = sTenCotL;
                oSheet.Cells[rowCnt, 12].Font.Bold = true;
                oSheet.Cells[rowCnt, 12].Font.Color = Color.FromArgb(255, 0, 0);
                oSheet.Cells[rowCnt, 12].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[rowCnt, 12].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[rowCnt, 13] = sTenCotM;
                oSheet.Cells[rowCnt, 13].Font.Bold = true;
                oSheet.Cells[rowCnt, 13].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[rowCnt, 13].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                oSheet.Cells[rowCnt, 14] = "=IFERROR(L" + rowCnt + "/G" + rowCnt + ",0)";
                oSheet.Cells[rowCnt, 14].Font.Bold = true;
                oSheet.Cells[rowCnt, 14].Font.Color = Color.FromArgb(255, 0, 0);
                oSheet.Cells[rowCnt, 14].Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oSheet.Cells[rowCnt, 14].Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                Excel.Range formatRange4;
                formatRange4 = oSheet.get_Range("D6", "G" + (rowCnt - 1).ToString());
                formatRange4.Interior.Color = Color.FromArgb(226, 239, 218);

                Excel.Range formatRange5;
                formatRange5 = oSheet.get_Range("H6", "L" + (rowCnt - 1).ToString());
                formatRange5.Interior.Color = Color.FromArgb(255, 230, 153);

                Excel.Range formatRange6;
                formatRange6 = oSheet.get_Range("M6", "M" + (rowCnt - 1).ToString());
                formatRange6.Interior.Color = Color.FromArgb(189, 215, 238);

                Excel.Range formatRange7;
                formatRange7 = oSheet.get_Range("N6", "N" + (rowCnt - 1).ToString());
                formatRange7.Interior.Color = Color.FromArgb(255, 255, 0);

                Excel.Range formatRange8;
                sRowXN =  sRowXN.Substring(0,sRowXN.Length -1);
                string[] strGetRowXN = sRowXN.Split(',');
                for (int i = 0; i < slXN; i++)
                {
                    formatRange8 = oSheet.get_Range("B"+ strGetRowXN[i] + "",lastColumn + ""+ strGetRowXN[i] + "");
                    formatRange8.Interior.Color = Color.FromArgb(255, 255, 0);
                }
                
                //Excel.Range formatRange;
                //rowCnt = keepRowCnt + 2; // Cộng 2 vì ở trên thêm 2 dòng xí nghiệp và cộng
                ////formatRange = oSheet.get_Range("G7", "G" + rowCnt.ToString());
                ////formatRange.NumberFormat = "dd/MM/yyyy";
                ////formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ////formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                ////formatRange = oSheet.get_Range("H7", "H" + rowCnt.ToString());
                ////formatRange.NumberFormat = "dd/MM/yyyy";
                ////formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ////formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                ////formatRange = oSheet.get_Range("I7", lastColumNgay + rowCnt.ToString());
                ////formatRange.NumberFormat = "@";
                ////formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

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
                //    try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
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
                oSheet.Application.ActiveWindow.SplitColumn = 4;
                oSheet.Application.ActiveWindow.FreezePanes = true;
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
        private string CellAddress(Excel.Worksheet sht, int row, int col)
        {
            return RangeAddress(sht.Cells[row, col]);
        }
        private string RangeAddress(Excel.Range rng)
        {
            object missing = null;
            return rng.get_AddressLocal(false, false, Excel.XlReferenceStyle.xlA1,
                   missing, missing);
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
    }
    #endregion
}