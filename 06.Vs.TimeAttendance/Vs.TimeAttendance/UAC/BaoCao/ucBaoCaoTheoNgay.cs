using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using Vs.Report;

namespace Vs.TimeAttendance
{
    public partial class ucBaoCaoTheoNgay : DevExpress.XtraEditors.XtraUserControl
    {
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
                                        case "_MT":
                                            {
                                                DSVangDauGioTheoNgay_MT();
                                                break;
                                            }
                                        default:
                                            DSVangDauGioTheoNgay();
                                            break;
                                    }
                                }
                                break;
                            case 1:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)))
                                    {
                                        case "_MT":
                                            {
                                                DSCongNhanVangDauGioNgay_MT();
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
                                        case "_MT":
                                            {
                                                DSCongNhanThieuNhomCa_MT();
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
                                        case "_MT":
                                            {
                                                DSDiTreVeSomNgay_MT();
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
                                        case "_MT":
                                            {
                                                //DSDiTreVeSomNgay_MT();
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
                                        case "_MT":
                                            {
                                                //DSDiTreVeSomNgay_MT();
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
                                        case "_MT":
                                            {
                                                //DSDiTreVeSomNgay_MT();
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
                                        case "_MT":
                                            {
                                                //DSDiTreVeSomNgay_MT();
                                                break;
                                            }
                                        default:
                                            BieuMauDangKyLamThemGio();
                                            break;
                                    }
                                    break;
                                }

                            case 8:
                                {
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(LK_DON_VI.EditValue)))
                                    {
                                        case "_MT":
                                            {
                                                //DSDiTreVeSomNgay_MT();
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
            string sTieuDe = "DANH SÁCH VẮNG ĐẦU GIỜ THEO NGÀY VÀ ĐƠN VỊ";
            frm.rpt = new rptDSVangDauGioTheoDV(lk_NgayIn.DateTime, sTieuDe, Convert.ToDateTime(LK_NgayXemBaoCao.EditValue));

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSVangNgayDV", conn);

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
            string sTieuDe1 = "DANH SÁCH CÔNG NHÂN VẮNG ĐẦU GIỜ NGÀY";
            frm.rpt = new rptDSVangDauGioTheoNgay(lk_NgayIn.DateTime, sTieuDe1, Convert.ToDateTime(LK_NgayXemBaoCao.EditValue));

            try
            {
                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSVangNgayDV", conn1);

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
            string sTieuDe2 = "DANH SÁCH NHÂN VIÊN THIẾU NHÓM CA";
            frm.rpt = new rptDSNVThieuNhomCa(Convert.ToDateTime(LK_NgayXemBaoCao.EditValue), sTieuDe2);

            try
            {
                conn2 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn2.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSNVThieuNhomCa", conn2);

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
                        sTieuDe2 = "DANH SÁCH NHÂN VIÊN ĐI TRỄ";
                    }
                    break;
                case 1:
                    {
                        sTieuDe2 = "DANH SÁCH NHÂN VIÊN VỀ SỚM";
                    }
                    break;
                case 2:
                    {
                        sTieuDe2 = "DANH SÁCH NHÂN VIÊN ĐI TRỄ, VỀ SỚM";
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

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSDiTreVeSom", conn2);

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
            string sTieuDe = "DANH SÁCH NHÂN VIÊN TRÙNG GIỜ THEO NGÀY";
            frm.rpt = new rptDSNVTrungGio(Convert.ToDateTime(LK_NgayXemBaoCao.EditValue), lk_NgayIn.DateTime, sTieuDe);
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSNVTrungGio", conn);
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
            string sTieuDe = "DANH SÁCH NHÂN VIÊN CÓ TRÊN 2 CẶP GIỜ";
            frm.rpt = new rptDSNVCoTren2CapGio(Convert.ToDateTime(LK_NgayXemBaoCao.EditValue), Convert.ToDateTime(lk_NgayIn.EditValue), sTieuDe);

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSNVCoTren2CapGio", conn);

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
            string sTieuDe = "DANH SÁCH NHÂN VIÊN CHƯA ĐỦ DỮ LIỆU";
            frm.rpt = new rptDSNVVachTheLoi(Convert.ToDateTime(LK_NgayXemBaoCao.EditValue), lk_NgayIn.DateTime);

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSNVVachTheLoi", conn);

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
            string sTieuDe = "ĐĂNG KÝ LÀM THÊM GIỜ";
            frm.rpt = new rptDKLamThemGio(lk_NgayIn.DateTime, sTieuDe, LK_XI_NGHIEP.Text.ToString());

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
        private void DSNVTangCaNgay()
        {
            frmViewReport frm = new frmViewReport();
            DataTable dt;
            System.Data.SqlClient.SqlConnection conn;
            dt = new DataTable();
            string sTieuDe = "DANH SÁCH NHÂN VIÊN TĂNG CA";
            frm.rpt = new rptDSNVTangCaTheoNgay(Convert.ToDateTime(datNgayTangCa.EditValue), lk_NgayIn.DateTime, sTieuDe);

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSTangCaNgay", conn);

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
        #endregion


    }
}
