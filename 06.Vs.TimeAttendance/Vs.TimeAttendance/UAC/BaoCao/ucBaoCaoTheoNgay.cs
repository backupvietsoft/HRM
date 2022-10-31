using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using Vs.Report;
using System.Reflection;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Vs.TimeAttendance
{
    public partial class ucBaoCaoTheoNgay : DevExpress.XtraEditors.XtraUserControl
    {
        private string SaveExcelFile;
        string sKyHieuDV = "";
        public ucBaoCaoTheoNgay()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, windowsUIButton);
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
                        switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
                        {
                            case "rdo_DSVangDauGioTheoDV":
                                {
                                    DSVangDauGioTheoNgay();
                                }
                                break;
                            case "rdo_DSVangDauGioTheoNgay":
                                {
                                    DSCongNhanVangDauGioNgay();
                                }
                                break;
                            case "rdo_DSThieuNhomCa":
                                {
                                    DSCongNhanThieuNhomCa();
                                }
                                break;
                            case "rdo_DSDiTreVeSomTheoNgay":
                                {
                                    DSDiTreVeSomNgay();
                                }
                                break;
                            case "rdo_DSNVTrungGioNgay":
                                {
                                    DSNVTrungGioNgay();
                                    break;
                                }
                            case "rdo_DSNVCoTren2CapGioChinhNgay":
                                {
                                    DSNVCoTren2CapGioChinh();
                                    break;
                                }
                            case "rdo_DSNVLoiTheNgay":
                                {
                                    DSNVCoTren2CapGioChinh();
                                    break;
                                }

                            case "rdo_BieuMauDangKyLamThem":
                                {
                                    switch (sKyHieuDV)
                                    {
                                        case "MT":
                                            {
                                                BieuMauDangKyLamThemGio();
                                                break;
                                            }
                                        case "SB":
                                            {
                                                BieuMauDangKyLamThemGio();
                                                break;
                                            }
                                        case "DM":
                                            {
                                                BieuMauDangKyLamThemGio_DM();
                                                break;
                                            }
                                        default:
                                            BieuMauDangKyLamThemGio();
                                            break;
                                    }
                                    break;
                                }

                            case "rdo_DanhSachNVTangCaNgay":
                                {
                                    switch (sKyHieuDV)
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
                            case "rdo_BaoCaoNhanSuNgay":
                                {
                                    BangChamCongNgay_DM();
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
            try
            {
                sKyHieuDV = Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString();
                if (sKyHieuDV == "DM")
                {
                    rdo_ChonBaoCao.Properties.Items.RemoveAt(5);
                    rdo_ChonBaoCao.Properties.Items.RemoveAt(4);
                }
                else
                {
                    rdo_ChonBaoCao.Properties.Items.RemoveAt(9);
                }
                LoadCboDonVi();
                LoadCboXiNghiep();
                LoadCboTo();
                LoadNgay();
                LoadTinhTrangHopDong();
                rdo_DiTreVeSom.Visible = false;
                lk_NgayIn.EditValue = DateTime.Today;
                Commons.OSystems.SetDateEditFormat(lk_NgayIn);
            }
            catch { }

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
            switch (rdo_ChonBaoCao.Properties.Items[rdo_ChonBaoCao.SelectedIndex].Tag)
            {
                case "rdo_DSDiTreVeSomTheoNgay":
                    {
                        rdo_DiTreVeSom.Visible = true;
                        break;
                    }
                case "rdo_DanhSachNVTangCaNgay":
                    {
                        rdo_DiTreVeSom.Visible = false;
                        break;
                    }
                default:
                    rdo_DiTreVeSom.Visible = false;
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
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
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
            frm.rpt = new rptDSNVTangCaTheoNgay(Convert.ToDateTime(LK_NgayXemBaoCao.EditValue), lk_NgayIn.DateTime);

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
                cmd.Parameters.Add("@NGAY", SqlDbType.Date).Value = Commons.Modules.ObjSystems.ConvertDateTime(LK_NgayXemBaoCao.Text);

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
                cmd.Parameters.Add("@NGAY_TC", SqlDbType.Date).Value = Convert.ToDateTime(LK_NgayXemBaoCao.EditValue);
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
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;


                //OfficeOpenXml.ExcelPackage ExcelPkg = new OfficeOpenXml.ExcelPackage();
                //OfficeOpenXml.ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;

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
                row1_TenDV.Style.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


                Range row1_DiaChiDV = oSheet.get_Range("B2");
                row1_DiaChiDV.Merge();
                row1_DiaChiDV.Font.Size = 9;
                row1_DiaChiDV.Font.Name = fontName;
                row1_DiaChiDV.Font.Italic = true;
                row1_DiaChiDV.Value2 = dtBCThang.Rows[0]["DIA_CHI_DV"];
                row1_DiaChiDV.WrapText = false;
                row1_DiaChiDV.Style.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


                Range row1_TieuDe_BaoCao = oSheet.get_Range("F1", "M1");
                row1_TieuDe_BaoCao.Merge();
                row1_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row1_TieuDe_BaoCao.Font.Name = fontName;
                row1_TieuDe_BaoCao.Font.Bold = true;
                row1_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row1_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row1_TieuDe_BaoCao.RowHeight = 15;
                row1_TieuDe_BaoCao.Value2 = "STILL NEED MORE HOURS  JUNE " + Convert.ToDateTime(LK_NgayXemBaoCao.EditValue).Year + "";
                row1_TieuDe_BaoCao.Font.Color = Color.FromArgb(255, 0, 255);

                Range row2_TieuDe_BaoCao = oSheet.get_Range("F2", "M2");
                row2_TieuDe_BaoCao.Merge();
                row2_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row2_TieuDe_BaoCao.Font.Name = fontName;
                row2_TieuDe_BaoCao.Font.FontStyle = "Bold Italic";
                row2_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe_BaoCao.RowHeight = 15;
                row2_TieuDe_BaoCao.Value2 = " VĂN BẢN THỎA THUẬN LÀM THÊM GIỜ ………/  " + Convert.ToDateTime(LK_NgayXemBaoCao.EditValue).Month + "  NĂM " + Convert.ToDateTime(LK_NgayXemBaoCao.EditValue).Year + "";
                row2_TieuDe_BaoCao.Font.Color = Color.FromArgb(255, 0, 255);

                Range row1_MauSo = oSheet.get_Range("R1", "S1");
                row1_MauSo.Merge();
                row1_MauSo.Font.Name = fontName;
                row1_MauSo.Font.Size = 9;
                row1_MauSo.Value2 = "Mẫu số 01/PLIV";
                row1_MauSo.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row1_MauSo.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row1_MauSo.WrapText = false;

                Range row2_CHXH = oSheet.get_Range("P2", "S2");
                row2_CHXH.Merge();
                row2_CHXH.Font.Size = 9;
                row2_CHXH.Font.Name = fontName;
                row2_CHXH.Value2 = "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM";
                row2_CHXH.WrapText = false;
                row2_CHXH.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_CHXH.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                Range row3_DLTDHP = oSheet.get_Range("P3", "S3");
                row3_DLTDHP.Merge();
                row3_DLTDHP.Font.Size = 9;
                row3_DLTDHP.Font.Name = fontName;
                row3_DLTDHP.Font.Italic = true;
                row3_DLTDHP.Value2 = "Độc lập - Tự do - Hạnh phúc";
                row3_DLTDHP.WrapText = false;
                row3_DLTDHP.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row3_DLTDHP.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                Range row4_TieuDe_Format = oSheet.get_Range("A4", "S4"); //27 + 31
                row4_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row4_TieuDe_Format.Font.Name = fontName;
                row4_TieuDe_Format.Font.Bold = true;
                row4_TieuDe_Format.WrapText = true;
                row4_TieuDe_Format.NumberFormat = "@";
                row4_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_Format.Font.Color = Color.FromArgb(255, 0, 0);

                Range row5_TieuDe_Format = oSheet.get_Range("A5", "S5"); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


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
                    row_20h_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row_20h_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    oSheet.Cells[rowBD, 17] = "20h";


                    Range row_SangChieu_Format = oSheet.get_Range("S" + rowBD + "".ToString()); //27 + 31
                    row_SangChieu_Format.Font.Name = fontName;
                    row_SangChieu_Format.Font.Color = Color.FromArgb(0, 0, 0);
                    row_SangChieu_Format.Font.Bold = true;
                    row_SangChieu_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row_SangChieu_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
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
                    row_KyTen_Col19.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row_KyTen_Col19.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    oSheet.Cells[rowBD + 13, 19] = "Ký tên:";




                    dr_Cu = current_dr;
                    keepRowCnt = rowCnt;
                    rowCnt = 0;
                }

                Microsoft.Office.Interop.Excel.Range formatRange;
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
                    AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared);
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
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel.Workbook oWB;
                Microsoft.Office.Interop.Excel.Worksheet oSheet;
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;
                oSheet.Name = Convert.ToDateTime(LK_NgayXemBaoCao.EditValue).Day.ToString();
                splashScreenManager1.ShowWaitForm();
                #region TheoNgay

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
                row2_TieuDe.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row2_TieuDe.Interior.Color = Color.FromArgb(248, 203, 173);


                Range row3_Date = oSheet.get_Range("L3", "N3");
                row3_Date.Font.Bold = true;
                row3_Date.Merge();
                row3_Date.Font.Size = 12;
                row3_Date.Font.Name = fontName;
                row3_Date.Value2 = "Ngày/ Date:" + LK_NgayXemBaoCao.Text + "";
                row3_Date.WrapText = false;
                row3_Date.RowHeight = 24;
                row3_Date.Style.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row3_Date.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


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
                row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


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

                oSheet.Application.ActiveWindow.SplitColumn = 4;
                oSheet.Application.ActiveWindow.SplitRow = 7;
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
                    //oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 2]].Font.Size = fontSizeNoiDung;
                    //oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 2]].Font.Name = fontName;

                    oSheet.Cells[rowBD, 3] = "Sub-Total " + int_to_Roman(i + 1) + "";
                    oSheet.Cells[rowBD, 3].Font.Bold = true;
                    oSheet.Cells[rowBD, 3].Font.Size = fontSizeNoiDung;
                    oSheet.Cells[rowBD, 3].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowBD, 3].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    oSheet.Cells[rowBD, 3].Font.Name = fontName;

                    oSheet.Cells[rowBD, 4] = "=SUM(" + CharacterIncrement(3) + "" + (rowBD + 1) + ":" + CharacterIncrement(3) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 4].Font.Bold = true;
                    oSheet.Cells[rowBD, 4].Font.Color = Color.FromArgb(255, 0, 0);
                    oSheet.Cells[rowBD, 4].Font.Size = fontSizeNoiDung;
                    oSheet.Cells[rowBD, 4].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    oSheet.Cells[rowBD, 4].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    oSheet.Cells[rowBD, 4].Font.Name = fontName;
                    sTenCotD = sTenCotD + CharacterIncrement(3) + rowBD + "+";
                    sRowXN = sRowXN + rowBD + ",";

                    //Fortmart cột D
                    Microsoft.Office.Interop.Excel.Range formatRange10;
                    formatRange10 = oSheet.get_Range("D" + (rowBD + 1) + "", "D" + (rowCnt + 1));
                    formatRange10.Font.Bold = true;

                    //Fortmart cột G
                    formatRange10 = oSheet.get_Range("G" + (rowBD + 1) + "", "G" + (rowCnt + 1));
                    formatRange10.Font.Bold = true;
                    formatRange10.Font.Color = Color.FromArgb(255, 0, 0);

                    //Fortmart cột L
                    Microsoft.Office.Interop.Excel.Range formatRange11;
                    formatRange11 = oSheet.get_Range("L" + (rowBD + 1) + "", "L" + (rowCnt + 1));

                    //Fortmart cột M
                    Microsoft.Office.Interop.Excel.Range formatRange12;
                    formatRange12 = oSheet.get_Range("M" + (rowBD + 1) + "", "M" + (rowCnt + 1));

                    //Fortmart cột M
                    Microsoft.Office.Interop.Excel.Range formatRange13;
                    formatRange13 = oSheet.get_Range("N" + (rowBD + 1) + "", "N" + (rowCnt + 1));

                    oSheet.Cells[rowBD, 5] = "=SUM(" + CharacterIncrement(4) + "" + (rowBD + 1) + ":" + CharacterIncrement(4) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 5].Font.Bold = true;
                    oSheet.Cells[rowBD, 5].Font.Color = Color.FromArgb(255, 0, 0);
                    sTenCotE = sTenCotE + CharacterIncrement(4) + rowBD + "+";

                    oSheet.Cells[rowBD, 6] = "=SUM(" + CharacterIncrement(5) + "" + (rowBD + 1) + ":" + CharacterIncrement(5) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 6].Font.Bold = true;
                    oSheet.Cells[rowBD, 6].Font.Color = Color.FromArgb(255, 0, 0);
                    sTenCotF = sTenCotF + CharacterIncrement(5) + rowBD + "+";

                    //oSheet.Cells[rowBD, 7] = "=SUM(" + CharacterIncrement(6) + "" + (rowBD + 1) + ":" + CharacterIncrement(6) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 7] = "=D" + rowBD + "+F" + rowBD + "-E" + rowBD + "";
                    oSheet.Cells[rowBD, 7].Font.Bold = true;

                    oSheet.Cells[rowBD, 8] = "=SUM(" + CharacterIncrement(7) + "" + (rowBD + 1) + ":" + CharacterIncrement(7) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 8].Font.Bold = true;
                    oSheet.Cells[rowBD, 8].Font.Color = Color.FromArgb(255, 0, 0);
                    sTenCotH = sTenCotH + CharacterIncrement(7) + rowBD + "+";


                    oSheet.Cells[rowBD, 9] = "=SUM(" + CharacterIncrement(8) + "" + (rowBD + 1) + ":" + CharacterIncrement(8) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 9].Font.Bold = true;
                    oSheet.Cells[rowBD, 9].Font.Color = Color.FromArgb(255, 0, 0);
                    sTenCotI = sTenCotI + CharacterIncrement(8) + rowBD + "+";


                    oSheet.Cells[rowBD, 10] = "=SUM(" + CharacterIncrement(9) + "" + (rowBD + 1) + ":" + CharacterIncrement(9) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 10].Font.Bold = true;
                    oSheet.Cells[rowBD, 10].Font.Color = Color.FromArgb(255, 0, 0);
                    sTenCotJ = sTenCotJ + CharacterIncrement(9) + rowBD + "+";


                    oSheet.Cells[rowBD, 11] = "=SUM(" + CharacterIncrement(10) + "" + (rowBD + 1) + ":" + CharacterIncrement(10) + "" + (rowCnt + 1) + ")";
                    oSheet.Cells[rowBD, 11].Font.Bold = true;
                    oSheet.Cells[rowBD, 11].Font.Color = Color.FromArgb(255, 0, 0);
                    sTenCotK = sTenCotK + CharacterIncrement(10) + rowBD + "+";


                    oSheet.Cells[rowBD, 12] = "=SUM(I" + rowBD + ":K" + rowBD + ")";
                    oSheet.Cells[rowBD, 12].Font.Bold = true;
                    oSheet.Cells[rowBD, 12].Font.Color = Color.FromArgb(255, 0, 0);
                    sTenCotL = sTenCotL + CharacterIncrement(11) + rowBD + "+";


                    oSheet.Cells[rowBD, 13] = "=G" + rowBD + "-L" + rowBD + "-H" + rowBD + "";
                    oSheet.Cells[rowBD, 13].Font.Bold = true;
                    sTenCotM = sTenCotM + CharacterIncrement(12) + rowBD + "+";


                    oSheet.Cells[rowBD, 14] = "=IFERROR(L" + rowBD + "/G" + rowBD + ",0)";
                    oSheet.Cells[rowBD, 14].Font.Bold = true;
                    oSheet.Cells[rowBD, 14].Font.Color = Color.FromArgb(255, 0, 0);

                    //Đổ dữ liệu của xí nghiệp
                    oSheet.get_Range("B" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;

                    formatRange10.Value2 = "=D" + rowSum + "+F" + rowSum + "-E" + rowSum + "";
                    formatRange11.Value2 = "=SUM(I" + rowSum + ":K" + rowSum + ")";
                    formatRange12.Value2 = "=G" + rowSum + "-L" + rowSum + "-H" + rowSum + "";
                    formatRange13.Value2 = "=IFERROR(L" + rowSum + "/G" + rowSum + ",0)";



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
                    rowSum = rowCnt + 3;
                    rowCnt = 0;
                }
                rowCnt = keepRowCnt;
                Microsoft.Office.Interop.Excel.Range formatRange; // range hien tai
                Microsoft.Office.Interop.Excel.Range formatRange2; // range truoc
                Microsoft.Office.Interop.Excel.Range formatRange1; // range ke tiep
                Microsoft.Office.Interop.Excel.Range formatRange3;
                string CurentColumn = string.Empty;
                int rowbd;
                int rowDup = 0; // row bat dau của dữ liệu duplicate
                int colKT = dtBCThang.Columns.Count;
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
                //rowTONG_CONG1.Font.Size = fontSizeNoiDung;
                //rowTONG_CONG1.Font.Name = fontName;

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

                oSheet.Cells[rowCnt, 5] = sTenCotE;
                oSheet.Cells[rowCnt, 5].Font.Bold = true;
                oSheet.Cells[rowCnt, 5].Font.Color = Color.FromArgb(255, 0, 0);

                oSheet.Cells[rowCnt, 6] = sTenCotF;
                oSheet.Cells[rowCnt, 6].Font.Bold = true;
                oSheet.Cells[rowCnt, 6].Font.Color = Color.FromArgb(255, 0, 0);

                oSheet.Cells[rowCnt, 7] = "=D" + rowCnt + "+F" + rowCnt + "-E" + rowCnt + "";
                oSheet.Cells[rowCnt, 7].Font.Bold = true;

                oSheet.Cells[rowCnt, 8] = sTenCotH;
                oSheet.Cells[rowCnt, 8].Font.Bold = true;
                oSheet.Cells[rowCnt, 8].Font.Color = Color.FromArgb(255, 0, 0);

                oSheet.Cells[rowCnt, 9] = sTenCotI;
                oSheet.Cells[rowCnt, 9].Font.Bold = true;
                oSheet.Cells[rowCnt, 9].Font.Color = Color.FromArgb(255, 0, 0);

                oSheet.Cells[rowCnt, 10] = sTenCotJ;
                oSheet.Cells[rowCnt, 10].Font.Bold = true;
                oSheet.Cells[rowCnt, 10].Font.Color = Color.FromArgb(255, 0, 0);

                oSheet.Cells[rowCnt, 11] = sTenCotK;
                oSheet.Cells[rowCnt, 11].Font.Bold = true;
                oSheet.Cells[rowCnt, 11].Font.Color = Color.FromArgb(255, 0, 0);

                oSheet.Cells[rowCnt, 12] = sTenCotL;
                oSheet.Cells[rowCnt, 12].Font.Bold = true;
                oSheet.Cells[rowCnt, 12].Font.Color = Color.FromArgb(255, 0, 0);

                oSheet.Cells[rowCnt, 13] = sTenCotM;
                oSheet.Cells[rowCnt, 13].Font.Bold = true;

                oSheet.Cells[rowCnt, 14] = "=IFERROR(L" + rowCnt + "/G" + rowCnt + ",0)";
                oSheet.Cells[rowCnt, 14].Font.Bold = true;
                oSheet.Cells[rowCnt, 14].Font.Color = Color.FromArgb(255, 0, 0);


                Microsoft.Office.Interop.Excel.Range formatRange4;
                formatRange4 = oSheet.get_Range("D6", "G" + (rowCnt - 1).ToString());
                formatRange4.Interior.Color = Color.FromArgb(226, 239, 218);

                Microsoft.Office.Interop.Excel.Range formatRange5;
                formatRange5 = oSheet.get_Range("H6", "L" + (rowCnt - 1).ToString());
                formatRange5.Interior.Color = Color.FromArgb(255, 230, 153);

                Microsoft.Office.Interop.Excel.Range formatRange6;
                formatRange6 = oSheet.get_Range("M6", "M" + (rowCnt - 1).ToString());
                formatRange6.Interior.Color = Color.FromArgb(189, 215, 238);

                Microsoft.Office.Interop.Excel.Range formatRange7;
                formatRange7 = oSheet.get_Range("N6", "N" + (rowCnt - 1).ToString());
                formatRange7.Interior.Color = Color.FromArgb(255, 255, 0);

                Microsoft.Office.Interop.Excel.Range formatRange8;
                sRowXN = sRowXN.Substring(0, sRowXN.Length - 1);
                string[] strGetRowXN = sRowXN.Split(',');
                for (int i = 0; i < slXN; i++)
                {
                    formatRange8 = oSheet.get_Range("B" + strGetRowXN[i] + "", lastColumn + "" + strGetRowXN[i] + "");
                    formatRange8.Interior.Color = Color.FromArgb(255, 255, 0);
                }

                //Format All
                Microsoft.Office.Interop.Excel.Range formatRange9;
                formatRange9 = oSheet.get_Range("B7", lastColumn + (rowCnt));
                formatRange9.Font.Size = fontSizeNoiDung;
                formatRange9.Font.Name = fontName;
                formatRange9.WrapText = true;

                formatRange9 = oSheet.get_Range("D7", lastColumn + (rowCnt));
                formatRange9.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange9.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                //formatRange9.NumberFormat = "0";
                //try { formatRange9.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch (Exception ex) { }

                int colBD = 3;
                for (col = colBD; col < dtBCThang.Columns.Count - 2; col++) // không format cột tỷ lệ
                {
                    CurentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(CurentColumn + "8", CurentColumn + rowCnt.ToString());
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    formatRange.NumberFormat = "0";
                    try { formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                formatRange9 = oSheet.get_Range("N7", lastColumn + (rowCnt));
                formatRange9.NumberFormat = @"0%";
                try { formatRange9.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }


                //Fortmart cột L
                formatRange9 = oSheet.get_Range("L7", "L" + (rowCnt - 1));
                formatRange9.Font.Bold = true;
                formatRange9.Font.Color = Color.FromArgb(255, 0, 0);

                //Fortmart cột M
                formatRange9 = oSheet.get_Range("M7", "M" + (rowCnt - 1));
                formatRange9.Font.Bold = true;

                //Fortmart cột N
                formatRange9 = oSheet.get_Range("N7", "N" + (rowCnt - 1));
                formatRange9.Font.Bold = true;
                formatRange9.Font.Color = Color.FromArgb(255, 0, 0);

                formatRange9 = oSheet.get_Range("O7", "O" + (rowCnt - 1));

                //var list = new System.Collections.Generic.List<string>();
                //list.Add("Charlie");
                //list.Add("Delta");
                //list.Add("Echo");
                //var flatList = string.Join(",", list.ToArray());

                //formatRange9.Validation.Delete();
                //formatRange9.Validation.Add(
                //   XlDVType.xlValidateList,
                //   XlDVAlertStyle.xlValidAlertInformation,
                //   XlFormatConditionOperator.xlBetween,
                //   flatList,
                //   Type.Missing);

                //formatRange9.Validation.IgnoreBlank = true;
                //formatRange9.Validation.InCellDropdown = true;


                BorderAround(oSheet.get_Range("B2", lastColumn + rowCnt.ToString()));

                #endregion
                //////////////////////////////////////////////////////////////////////////////// Giai đoạn /////////////////////////////////////////////////////

                #region Theo GiaiDoan
                try
                {

                    DateTime DenNgay = Convert.ToDateTime(LK_NgayXemBaoCao.EditValue);
                    DateTime TuNgay = Convert.ToDateTime(LK_NgayXemBaoCao.EditValue).AddDays(-5);
                    int soNgayNghi = 0;
                    try
                    {
                        soNgayNghi = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnGetSoNgayTruLeNgayNghiMacDinh('" + TuNgay.ToString("yyyyMMdd") + "', '" + DenNgay.ToString("yyyyMMdd") + "')"));
                    }
                    catch { }
                    if (soNgayNghi < 5)
                    {
                        TuNgay = TuNgay.AddDays(-(5 - soNgayNghi));
                    }

                    DateTime TuNgayTemp = TuNgay;
                    DateTime DenNgayTemp = DenNgay;

                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();
                    cmd = new System.Data.SqlClient.SqlCommand("rptBangCongNgayGiaiDoan_DM", conn);
                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                    cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = TuNgay;
                    cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = Convert.ToDateTime(LK_NgayXemBaoCao.EditValue);
                    cmd.CommandType = CommandType.StoredProcedure;
                    adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    ds = new DataSet();
                    adp.Fill(ds);
                    dtBCThang = new DataTable();
                    dtBCThang = ds.Tables[0].Copy();

                    dtSLXN = new DataTable(); // Lấy số lượng xí nghiệp
                    dtSLXN = ds.Tables[1].Copy();
                    slXN = Convert.ToInt32(dtSLXN.Rows[0][0]);

                    oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;
                    oSheet = oWB.Worksheets.Add(After: oWB.Sheets[oWB.Sheets.Count]);
                    oSheet.Name = "Tổng hợp";



                    fontName = "Times New Roman";
                    fontSizeTieuDe = 12;
                    fontSizeNoiDung = 12;

                    lastColumn = string.Empty;
                    lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                    row1_TieuDe = oSheet.get_Range("B1");
                    row1_TieuDe.Font.Bold = true;
                    row1_TieuDe.Value2 = "CÔNG TY CỔ PHẦN MAY DUY MINH";
                    row1_TieuDe.WrapText = false;
                    row1_TieuDe.Font.Size = 12;
                    row1_TieuDe.Font.Name = fontName;
                    row1_TieuDe.RowHeight = 21;
                    row1_TieuDe.ColumnWidth = 43;



                    row2_TieuDe = oSheet.get_Range("B2", "C2");
                    row2_TieuDe.Font.Bold = true;
                    row2_TieuDe.Merge();
                    row2_TieuDe.Font.Size = 12;
                    row2_TieuDe.Font.Name = fontName;
                    row2_TieuDe.Value2 = "BÁO CÁO HÀNG NGÀY/ DAILY ATTENDANCE REPORT";
                    row2_TieuDe.WrapText = false;
                    row2_TieuDe.RowHeight = 33;
                    row2_TieuDe.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row2_TieuDe.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    row2_TieuDe.Interior.Color = Color.FromArgb(248, 203, 173);


                    row3_Date = oSheet.get_Range("L3", "N3");
                    row3_Date.Font.Bold = true;
                    row3_Date.Merge();
                    row3_Date.Font.Size = 12;
                    row3_Date.Font.Name = fontName;
                    row3_Date.Value2 = "Ngày/ Date:" + Convert.ToDateTime(lk_NgayIn.EditValue).Day + "-" + (Convert.ToDateTime(lk_NgayIn.EditValue).Month.ToString().Length == 1 ? "0" + Convert.ToDateTime(lk_NgayIn.EditValue).Month.ToString() : Convert.ToDateTime(lk_NgayIn.EditValue).Month.ToString()) + "-" + Convert.ToDateTime(lk_NgayIn.EditValue).Year + "";
                    row3_Date.WrapText = false;
                    row3_Date.RowHeight = 24;
                    row3_Date.Style.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row3_Date.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                    row4 = oSheet.get_Range("B4");
                    row4.RowHeight = 66;

                    row5 = oSheet.get_Range("B5");
                    row5.RowHeight = 79;

                    //Range row5_TieuDe_Format = oSheet.get_Range("B4", lastColumn + "5"); //27 + 31
                    row5_TieuDe_Format = oSheet.get_Range("B4", lastColumn + "5"); //27 + 31
                    row5_TieuDe_Format.Font.Size = fontSizeTieuDe;
                    row5_TieuDe_Format.Font.Name = fontName;
                    row5_TieuDe_Format.Font.Bold = true;
                    row5_TieuDe_Format.WrapText = true;
                    row5_TieuDe_Format.NumberFormat = "@";
                    row5_TieuDe_Format.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row5_TieuDe_Format.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                    row1_TieuDe_Stt = oSheet.get_Range("A1");
                    row1_TieuDe_Stt.ColumnWidth = 2;

                    row5_TieuDe_Stt = oSheet.get_Range("B5", "B6");
                    row5_TieuDe_Stt.Merge();
                    row5_TieuDe_Stt.Value2 = "Phòng ban/ Section";
                    row5_TieuDe_Stt.Interior.Color = Color.FromArgb(198, 224, 180);

                    row6_TieuDe_Stt = oSheet.get_Range("C5", "C6");
                    row6_TieuDe_Stt.Merge();
                    row6_TieuDe_Stt.Value2 = "Đơn vị/ Department Vị trí/ Position";
                    row6_TieuDe_Stt.ColumnWidth = 30;
                    row6_TieuDe_Stt.Interior.Color = Color.FromArgb(198, 224, 180);

                    int col_td = 4;
                    Range row4_1;
                    row4_1 = oSheet.get_Range("A4");
                    row4_1.RowHeight = 25;
                    while (TuNgayTemp <= DenNgayTemp)
                    {
                        if (TuNgayTemp.DayOfWeek.ToString() == "Sunday" || TuNgayTemp.DayOfWeek.ToString() == "Saturday")
                        {
                            TuNgayTemp = TuNgayTemp.AddDays(1);
                        }
                        else
                        {
                            oSheet.Cells[4, col_td] = TuNgayTemp.ToString("dd/MM/yyyy");
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
                            TuNgayTemp = TuNgayTemp.AddDays(1);
                        }
                    }



                    oSheet.Application.ActiveWindow.SplitColumn = 3;
                    oSheet.Application.ActiveWindow.SplitRow = 6;
                    oSheet.Application.ActiveWindow.FreezePanes = true;


                    col = 1;
                    rowCnt = 0;
                    keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                    dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                    current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                    rowBD_XN = 0; // Row để insert dòng xí nghiệp
                                  //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                    rowBD = 7;
                    cotCN_A = "";
                    cotCN_B = "";
                    TEN_XN = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_XN")).Distinct().ToArray();
                    chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                    dt_temp = new DataTable();
                    dt_temp = ds.Tables[0].Copy(); // Dữ row count data

                    sRowXN = "";
                    s = int_to_Roman(9);
                    Range formatRange11;
                    rowSum = 8; //Row sum của cột G 
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
                        oSheet.Cells[rowBD, 3].Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        oSheet.Cells[rowBD, 3].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        oSheet.Cells[rowBD, 3].Font.Name = fontName;

                        sRowXN = sRowXN + rowBD + ",";

                        //Đổ dữ liệu của xí nghiệp
                        oSheet.get_Range("B" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;
                        col_td = 4;
                        TuNgayTemp = TuNgay;
                        //Set công thức từng row
                        while (TuNgayTemp <= DenNgayTemp)
                        {
                            if (TuNgayTemp.DayOfWeek.ToString() == "Sunday" || TuNgayTemp.DayOfWeek.ToString() == "Saturday")
                            {
                                TuNgayTemp = TuNgayTemp.AddDays(1);
                            }
                            else
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
                                TuNgayTemp = TuNgayTemp.AddDays(1);
                            }
                        }
                        // Fortmat từ cột đầu tới cột cuối của từng Xí nghiệp
                        Range formatRange10;
                        formatRange10 = oSheet.get_Range("D" + (rowBD) + "", lastColumn + (rowBD));
                        formatRange10.Font.Color = Color.FromArgb(255, 0, 0);
                        formatRange10.Font.Bold = true;
                        formatRange10.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        formatRange10.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        dr_Cu = current_dr;
                        keepRowCnt = rowCnt;
                        rowSum = rowCnt + 3;
                        rowCnt = 0;
                    }
                    rowCnt = keepRowCnt;
                    rowDup = 0; // row bat dau của dữ liệu duplicate
                    bChan = false;
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

                    rowTONG_CONG = oSheet.get_Range("B" + rowCnt);
                    rowTONG_CONG.Value2 = "Tổng/Grand Total";
                    rowTONG_CONG.Font.Bold = true;

                    rowTONG_CONG1 = oSheet.get_Range("C" + rowCnt);
                    sLama = "(";
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
                    rowSumAll.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    rowSumAll.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    col_td = 4;
                    TuNgayTemp = TuNgay;
                    while (TuNgayTemp <= DenNgayTemp)
                    {
                        if (TuNgayTemp.DayOfWeek.ToString() == "Sunday" || TuNgayTemp.DayOfWeek.ToString() == "Saturday")
                        {
                            TuNgayTemp = TuNgayTemp.AddDays(1);
                        }
                        else
                        {
                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td - 1) + "7" + "", CharacterIncrement(col_td - 1) + (rowCnt).ToString());
                            formatRange4.Interior.Color = Color.FromArgb(226, 239, 218);
                            formatRange4.NumberFormat = "0"; // format từng cột
                            try { formatRange4.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                            oSheet.Cells[rowCnt, col_td] = "=SUM(" + CellAddress(oSheet, 7, col_td) + ":" + CellAddress(oSheet, rowCnt - 1, col_td) + ")/2"; // sUM TỪNNG CỘT

                            //cột số lao động vắng mặt
                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td) + "7" + "", CharacterIncrement(col_td) + (rowCnt).ToString());
                            formatRange4.Interior.Color = Color.FromArgb(255, 230, 153);
                            formatRange4.NumberFormat = "0";
                            try { formatRange4.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                            oSheet.Cells[rowCnt, col_td + 1] = "=SUM(" + CellAddress(oSheet, 7, col_td + 1) + ":" + CellAddress(oSheet, rowCnt - 1, col_td + 1) + ")/2";


                            //cột Số lao động có mặt
                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td + 1) + "7" + "", CharacterIncrement(col_td + 1) + (rowCnt).ToString());
                            formatRange4.Interior.Color = Color.FromArgb(189, 215, 238);
                            formatRange4.NumberFormat = "0";
                            try { formatRange4.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                            oSheet.Cells[rowCnt, col_td + 2] = "=SUM(" + CellAddress(oSheet, 7, col_td + 2) + ":" + CellAddress(oSheet, rowCnt - 1, col_td + 2) + ")/2";


                            //cột Tỷ lệ vắng (%)
                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td + 2) + "7" + "", CharacterIncrement(col_td + 2) + (rowCnt).ToString());
                            formatRange4.Interior.Color = Color.FromArgb(255, 255, 0);
                            formatRange4.NumberFormat = @"0%";
                            try { formatRange4.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                            oSheet.Cells[rowCnt, col_td + 3] = "=IFERROR(" + CellAddress(oSheet, rowCnt, col_td + 1) + "/" + CellAddress(oSheet, rowCnt, col_td) + ",0)";



                            //cột Tỷ lệ có mặt/ tổng số (%)
                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td + 3) + "7" + "", CharacterIncrement(col_td + 3) + (rowCnt).ToString());
                            formatRange4.Interior.Color = Color.FromArgb(255, 255, 0);
                            formatRange4.NumberFormat = @"0%";
                            try { formatRange4.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                            oSheet.Cells[rowCnt, col_td + 4] = "=IFERROR(" + CellAddress(oSheet, rowCnt, col_td + 2) + "/" + CellAddress(oSheet, rowCnt, col_td) + ",0)";

                            col_td = col_td + 5;
                            TuNgayTemp = TuNgayTemp.AddDays(1);
                        }
                    }

                    sRowXN = sRowXN.Substring(0, sRowXN.Length - 1);
                    strGetRowXN = sRowXN.Split(',');
                    for (int i = 0; i < slXN; i++)
                    {
                        formatRange8 = oSheet.get_Range("B" + strGetRowXN[i] + "", lastColumn + "" + strGetRowXN[i] + "");
                        formatRange8.Interior.Color = Color.FromArgb(255, 255, 0);
                    }

                    //Format All
                    formatRange9 = oSheet.get_Range("B8", "C" + (rowCnt));
                    formatRange9.Font.Size = fontSizeNoiDung;
                    formatRange9.Font.Name = fontName;
                    formatRange9.WrapText = true;

                    formatRange9 = oSheet.get_Range("D7", lastColumn + (rowCnt));
                    formatRange9.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange9.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    formatRange9.Font.Bold = true;
                    formatRange9.Font.Size = fontSizeNoiDung;
                    formatRange9.Font.Name = fontName;

                    BorderAround(oSheet.get_Range("B2", "C3"));
                    BorderAround(oSheet.get_Range("B4", lastColumn + rowCnt.ToString()));

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
                    TuNgayTemp = TuNgay;
                    while (TuNgayTemp <= DenNgayTemp)
                    {
                        if (TuNgayTemp.DayOfWeek.ToString() == "Sunday" || TuNgayTemp.DayOfWeek.ToString() == "Saturday")
                        {
                            TuNgayTemp = TuNgayTemp.AddDays(1);
                        }
                        else
                        {
                            oSheet.Cells[rowCnt - 1, col_td] = TuNgayTemp;
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
                            TuNgayTemp = TuNgayTemp.AddDays(1);
                        }
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
                        TuNgayTemp = TuNgay;
                        //Set công thức từng row
                        while (TuNgayTemp <= DenNgayTemp)
                        {
                            if (TuNgayTemp.DayOfWeek.ToString() == "Sunday" || TuNgayTemp.DayOfWeek.ToString() == "Saturday")
                            {
                                TuNgayTemp = TuNgayTemp.AddDays(1);
                            }
                            else
                            {
                                //cột Tỷ lệ vắng (%)
                                formatRange11 = oSheet.get_Range("" + CharacterIncrement(col_td + 1) + "" + rowBD + "", "" + CharacterIncrement(col_td + 1) + "" + (rowCnt + 1) + "");
                                formatRange11.Value = "=IFERROR(" + CharacterIncrement(col_td) + "" + rowBD.ToString() + "/" + CharacterIncrement(col_td - 1) + "" + rowBD.ToString() + ",0)";
                                formatRange11.NumberFormat = @"0%";
                                try { formatRange11.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                                col_td = col_td + 3;
                                TuNgayTemp = TuNgayTemp.AddDays(1);
                            }

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
                    formatRange9.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange9.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    // SUM
                    col_td = 4;
                    TuNgayTemp = TuNgay;
                    while (TuNgayTemp <= DenNgayTemp)
                    {
                        if (TuNgayTemp.DayOfWeek.ToString() == "Sunday" || TuNgayTemp.DayOfWeek.ToString() == "Saturday")
                        {
                            TuNgayTemp = TuNgayTemp.AddDays(1);
                        }
                        else
                        {
                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td - 1) + "" + rowCnt + "" + "", CharacterIncrement(col_td - 1) + (rowCnt).ToString());
                            oSheet.Cells[rowCnt, col_td] = "=SUM(" + CellAddress(oSheet, rowCnt1, col_td) + ":" + CellAddress(oSheet, rowCnt - 1, col_td) + ")"; // sUM TỪNNG CỘT

                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td) + "" + rowCnt + "" + "", CharacterIncrement(col_td) + (rowCnt).ToString());
                            oSheet.Cells[rowCnt, col_td + 1] = "=SUM(" + CellAddress(oSheet, rowCnt1, col_td + 1) + ":" + CellAddress(oSheet, rowCnt - 1, col_td + 1) + ")";

                            //cột Tỷ lệ vắng (%)
                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td + 1) + "" + rowCnt + "" + "", CharacterIncrement(col_td + 1) + (rowCnt).ToString());
                            oSheet.Cells[rowCnt, col_td + 2] = "=IFERROR(" + CellAddress(oSheet, rowCnt, col_td + 1) + "/" + CellAddress(oSheet, rowCnt, col_td) + ",0)";

                            col_td = col_td + 3;
                            TuNgayTemp = TuNgayTemp.AddDays(1);
                        }

                    }

                    col_td = 4;
                    TuNgayTemp = TuNgay;
                    while (TuNgayTemp <= DenNgayTemp)
                    {
                        if (TuNgayTemp.DayOfWeek.ToString() == "Sunday" || TuNgayTemp.DayOfWeek.ToString() == "Saturday")
                        {
                            TuNgayTemp = TuNgayTemp.AddDays(1);
                        }
                        else
                        {
                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td - 1) + "" + rowCnt1 + "" + "", CharacterIncrement(col_td - 1) + (rowCnt).ToString());
                            formatRange4.Interior.Color = Color.FromArgb(226, 239, 218);
                            formatRange4.NumberFormat = "0"; // format từng cột
                            try { formatRange4.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                            //cột số lao động vắng mặt
                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td) + "" + rowCnt1 + "" + "", CharacterIncrement(col_td) + (rowCnt).ToString());
                            formatRange4.Interior.Color = Color.FromArgb(255, 230, 153);
                            formatRange4.NumberFormat = "0";
                            try { formatRange4.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                            formatRange4 = oSheet.get_Range(CharacterIncrement(col_td + 1) + "" + rowCnt1 + "" + "", CharacterIncrement(col_td + 1) + (rowCnt).ToString());
                            formatRange4.Interior.Color = Color.FromArgb(189, 215, 238);
                            formatRange4.NumberFormat = @"0%";
                            try { formatRange4.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                            col_td = col_td + 3;
                            TuNgayTemp = TuNgayTemp.AddDays(1);
                        }
                    }
                    formatRange9 = oSheet.get_Range("D" + rowCnt2 + "", lastColumn + rowCnt);
                    formatRange9.Font.Bold = true;
                    formatRange9.WrapText = true;
                    formatRange9.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange9.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    formatRange9 = oSheet.get_Range("B" + rowCnt2 + "", lastColumn + rowCnt.ToString());
                    formatRange9.Font.Name = fontName;
                    formatRange9.Font.Size = fontSizeNoiDung;

                    BorderAround(oSheet.get_Range("B" + rowCnt2 + "", lastColumn + rowCnt.ToString()));

                    #endregion

                    #endregion


                    /////////////////////////////////////////////////////// DANH SÁCH NGHỈ VIỆC ///////////////////////////////////////////////////////

                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();
                    cmd = new System.Data.SqlClient.SqlCommand("rptDanhSachNghiViec", conn);
                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = LK_DON_VI.EditValue;
                    cmd.Parameters.Add("@XN", SqlDbType.Int).Value = LK_XI_NGHIEP.EditValue;
                    cmd.Parameters.Add("@TO", SqlDbType.Int).Value = LK_TO.EditValue;
                    cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = Convert.ToDateTime(LK_NgayXemBaoCao.EditValue);
                    cmd.CommandType = CommandType.StoredProcedure;
                    adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    ds = new DataSet();
                    adp.Fill(ds);
                    dtBCThang = new DataTable();
                    dtBCThang = ds.Tables[0].Copy();

                    oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;
                    oSheet = oWB.Worksheets.Add(After: oWB.Sheets[oWB.Sheets.Count]);
                    oSheet.Name = "DS nghỉ việc";

                    fontName = "Times New Roman";
                    fontSizeTieuDe = 10;
                    fontSizeNoiDung = 9;

                    lastColumn = string.Empty;
                    lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                    Range TieuDe = oSheet.get_Range("A1", "F1");
                    TieuDe.Merge();
                    TieuDe.Font.Size = 12;
                    TieuDe.Font.Name = fontName;
                    TieuDe.Font.Bold = true;
                    TieuDe.Value2 = "DANH SÁCH CÔNG NHÂN NGHỈ VIỆC";
                    TieuDe.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    TieuDe.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                    row1_TieuDe_Stt = oSheet.get_Range("A3");
                    row1_TieuDe_Stt.Value2 = "STT";
                    row1_TieuDe_Stt.ColumnWidth = 8;

                    row5_TieuDe_Stt = oSheet.get_Range("B3");
                    row5_TieuDe_Stt.Value2 = "Mã thẻ";

                    row6_TieuDe_Stt = oSheet.get_Range("C3");
                    row6_TieuDe_Stt.Value2 = "Họ tên";


                    row5_TieuDe_MaSo = oSheet.get_Range("D3");
                    row5_TieuDe_MaSo.Merge();
                    row5_TieuDe_MaSo.Value2 = "Bộ phận";


                    row6_TieuDe_MaSo = oSheet.get_Range("E3");
                    row6_TieuDe_MaSo.Merge();
                    row6_TieuDe_MaSo.Value2 = "Chuyền/Phòng";

                    row5_TieuDe_HoTen = oSheet.get_Range("F3");
                    row5_TieuDe_HoTen.Merge();
                    row5_TieuDe_HoTen.Value2 = "Lý do nghỉ việc";

                    row1_TieuDe_Stt = oSheet.get_Range("A3", "F3");
                    row1_TieuDe_Stt.Font.Bold = true;
                    row1_TieuDe_Stt.Font.Name = fontName;
                    row1_TieuDe_Stt.Font.Size = 11;
                    row1_TieuDe_Stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row1_TieuDe_Stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    DataRow[] dr1 = dtBCThang.Select();
                    string[,] rowData1 = new string[dr1.Count(), dtBCThang.Columns.Count];

                    rowCnt = 0;
                    foreach (DataRow row in dr1)
                    {
                        for (int col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                        {
                            rowData1[rowCnt, col_bd] = row[col_bd].ToString();
                        }
                        rowCnt++;
                    }
                    rowCnt = rowCnt + 3;
                    oSheet.get_Range("A4", lastColumn + rowCnt.ToString()).Value2 = rowData1;

                    formatRange9 = oSheet.get_Range("A4", lastColumn + rowCnt);
                    formatRange9 = oSheet.get_Range("D4", "E" + rowCnt.ToString());
                    formatRange9.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    formatRange9.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    BorderAround(oSheet.get_Range("A3", lastColumn + rowCnt.ToString()));
                    formatRange9 = oSheet.get_Range("A3", lastColumn + rowCnt);
                    formatRange9.Columns.AutoFit();

                    #region vắng ngày
                    //////////////////////////////////////////// DANH SÁCH VẮNG NGÀY /////////////////////////////////////////
                    dtBCThang = new DataTable();
                    dtBCThang = ds.Tables[1].Copy();

                    oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.ActiveSheet;
                    oSheet = oWB.Worksheets.Add(After: oWB.Sheets[oWB.Sheets.Count]);
                    oSheet.Name = "DS vắng";

                    fontName = "Times New Roman";
                    fontSizeTieuDe = 10;
                    fontSizeNoiDung = 9;

                    lastColumn = string.Empty;
                    lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 1);

                    TieuDe = oSheet.get_Range("A1", "G2");
                    TieuDe.Merge();
                    TieuDe.Font.Size = 12;
                    TieuDe.Font.Name = fontName;
                    TieuDe.Font.Bold = true;
                    TieuDe.Value2 = "DANH SÁCH CÔNG NHÂN VIÊN VẮNG";
                    TieuDe.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    TieuDe.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                    row1_TieuDe_Stt = oSheet.get_Range("A4");
                    row1_TieuDe_Stt.Value2 = "STT";
                    row1_TieuDe_Stt.ColumnWidth = 8;

                    row5_TieuDe_Stt = oSheet.get_Range("B4");
                    row5_TieuDe_Stt.Value2 = "Ngày";

                    row6_TieuDe_Stt = oSheet.get_Range("C4");
                    row6_TieuDe_Stt.Value2 = "Thứ";


                    row5_TieuDe_MaSo = oSheet.get_Range("D4");
                    row5_TieuDe_MaSo.Merge();
                    row5_TieuDe_MaSo.Value2 = "Mã nhân viên";


                    row6_TieuDe_MaSo = oSheet.get_Range("E4");
                    row6_TieuDe_MaSo.Merge();
                    row6_TieuDe_MaSo.Value2 = "Họ tên";

                    row5_TieuDe_HoTen = oSheet.get_Range("F4");
                    row5_TieuDe_HoTen.Merge();
                    row5_TieuDe_HoTen.Value2 = "Chuyền/Phòng";

                    row5_TieuDe_HoTen = oSheet.get_Range("G4");
                    row5_TieuDe_HoTen.Merge();
                    row5_TieuDe_HoTen.Value2 = "Ghi chú";

                    row1_TieuDe_Stt = oSheet.get_Range("A4", "G4");
                    row1_TieuDe_Stt.Font.Bold = true;
                    row1_TieuDe_Stt.Font.Name = fontName;
                    row1_TieuDe_Stt.Font.Size = 11;
                    row1_TieuDe_Stt.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row1_TieuDe_Stt.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    dr1 = dtBCThang.Select();
                    rowData1 = new string[dr1.Count(), dtBCThang.Columns.Count];

                    rowCnt = 0;
                    foreach (DataRow row in dr1)
                    {
                        for (int col_bd = 0; col_bd < dtBCThang.Columns.Count; col_bd++)
                        {
                            rowData1[rowCnt, col_bd] = row[col_bd].ToString();
                        }
                        rowCnt++;
                    }
                    rowCnt = rowCnt + 3;
                    oSheet.get_Range("A5", lastColumn + rowCnt.ToString()).Value2 = rowData1;

                    formatRange9 = oSheet.get_Range("B5", "B" + rowCnt);
                    formatRange9.NumberFormat = "dd/MM/yyyy";
                    try { formatRange9.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }

                    formatRange9 = oSheet.get_Range("A5", lastColumn + rowCnt);
                    formatRange9.Font.Name = fontName;
                    formatRange9.Font.Size = 11;

                    BorderAround(oSheet.get_Range("A4", lastColumn + rowCnt.ToString()));
                    formatRange9 = oSheet.get_Range("A4", lastColumn + rowCnt);
                    formatRange9.Columns.AutoFit();

                    #endregion
                }
                catch (Exception ex)
                {
                }


                splashScreenManager1.CloseWaitForm();
                oWB.Sheets[1].Activate();

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared);

            }
            catch (Exception ex)
            {
                splashScreenManager1.CloseWaitForm();
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
        private string CellAddress(Microsoft.Office.Interop.Excel.Worksheet sht, int row, int col)
        {
            return RangeAddress(sht.Cells[row, col]);
        }
        private string RangeAddress(Microsoft.Office.Interop.Excel.Range rng)
        {
            object missing = null;
            return rng.get_AddressLocal(false, false, Microsoft.Office.Interop.Excel.XlReferenceStyle.xlA1,
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