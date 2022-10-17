using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Microsoft.ApplicationBlocks.Data;
using Vs.Report;
using DevExpress.XtraGrid.Views.Grid;

namespace Vs.HRM
{
    public partial class ucDaoTao : DevExpress.XtraEditors.XtraUserControl
    {
        private bool bthem = false;
        private bool them = false;
        public static ucDaoTao _instance;
        public static ucDaoTao Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDaoTao();
                return _instance;
            }
        }
        public ucDaoTao()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, tabbedControlGroup1, windowsUIButton);
        }
        private void ucDaoTao_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                int year = DateTime.Now.Year;
                DateTime firstDay = new DateTime(year, 1, 1);
                DateTime lastDay = new DateTime(year, 12, 31);
                Commons.Modules.ObjSystems.LoadCboDonVi(cboSearch_DV);
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboSearch_DV, cboSearch_XN);
                Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN, cboSearch_TO);
                //LoadGridKeHoachDaoTao(them);
                tabbedControlGroup1.SelectedTabPageIndex = 0;
                TU_NGAYDateEdit.DateTime = firstDay;
                DEN_NGAYDateEdit.EditValue = lastDay;
                Commons.Modules.sLoad = "";
                LoadGridControl(-1);
                LoadCboNoiDaoTao();
                LoadCboTheoYC();

                LoadNoiDung();

                //Format datatiem
                //Commons.OSystems.SetDateEditFormat(TU_NGAYDateEdit);
                //Commons.OSystems.SetDateEditFormat(DEN_NGAYDateEdit);
                //Commons.OSystems.SetDateEditFormat(NGAY_BDDateEdit);
                //Commons.OSystems.SetDateEditFormat(NGAY_KTDateEdit);
                //Commons.OSystems.SetTimeEditFormat(GIO_BDTimeEdit);
                //Commons.OSystems.SetTimeEditFormat(GIO_KTtimeEdit);

                //Commons.Modules.ObjSystems.MAutoCompleteTextEdit(HINH_THUC_DTTextEdit, Commons.Modules.ObjSystems.ConvertDatatable(grdKhoaHoc), "HINH_THUC_DT");
                //Commons.Modules.ObjSystems.MAutoCompleteTextEdit(LINH_VUC_DTTextEdit, Commons.Modules.ObjSystems.ConvertDatatable(grdKhoaHoc), "LINH_VUC_DT");
                //Commons.Modules.ObjSystems.MAutoCompleteTextEdit(TRUONG_DTTextEdit, Commons.Modules.ObjSystems.ConvertDatatable(grdKhoaHoc), "TRUONG_DT");
                //Commons.Modules.ObjSystems.MAutoCompleteTextEdit(GIAO_VIENTextEdit, Commons.Modules.ObjSystems.ConvertDatatable(grdKhoaHoc), "GIAO_VIEN");
                //Commons.Modules.ObjSystems.MAutoCompleteTextEdit(DIA_DIEMTextEdit, Commons.Modules.ObjSystems.ConvertDatatable(grdKhoaHoc), "DIA_DIEM");



                //load tab 1
                //Commons.Modules.ObjSystems.LoadCboDonVi(cboSearch_DV);
                //Commons.Modules.ObjSystems.LoadCboXiNghiep(cboSearch_DV,cboSearch_XN);
                //Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN,cboSearch_TO);
                enableButon(true);
                Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            }
            catch (Exception ex)
            {
            }
        }
        #region Các hàm load tab 0
        private void LoadGridControl(int ID)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListKhoaDaoTao", TU_NGAYDateEdit.DateTime, DEN_NGAYDateEdit.DateTime));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_KDT"] };
                if (grdKhoaHoc.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdKhoaHoc, grvKhoaHoc, dt, false, false, true, true, true, this.Name);
                    grvKhoaHoc.Columns["ID_KDT"].Visible = false;
                    grvKhoaHoc.Columns["NOI_DT"].Visible = false;
                    grvKhoaHoc.Columns["TRUONG_DT"].Visible = false;
                    grvKhoaHoc.Columns["TIN_CHI"].Visible = false;
                    grvKhoaHoc.Columns["GIO_BD"].Visible = false;
                    grvKhoaHoc.Columns["HOC_PHI"].Visible = false;
                    grvKhoaHoc.Columns["GIO_KT"].Visible = false;
                    grvKhoaHoc.Columns["THOI_GIAN"].Visible = false;
                    grvKhoaHoc.Columns["THOI_GIAN_HOC"].Visible = false;
                    grvKhoaHoc.Columns["HINH_THUC_DT"].Visible = false;
                    grvKhoaHoc.Columns["LINH_VUC_DT"].Visible = false;
                    grvKhoaHoc.Columns["GIAO_VIEN"].Visible = false;
                    grvKhoaHoc.Columns["DIA_DIEM"].Visible = false;
                    grvKhoaHoc.Columns["TRONG_NUOC"].Visible = false;
                    grvKhoaHoc.Columns["HANH_CHANH"].Visible = false;
                    grvKhoaHoc.Columns["THEO_YEU_CAU"].Visible = false;
                    grvKhoaHoc.Columns["PHUONG_TIEN_DI_CHUYEN"].Visible = false;
                    grvKhoaHoc.Columns["CAM_KET"].Visible = false;
                    grvKhoaHoc.Columns["GHI_CHU"].Visible = false;
                }
                else
                {
                    grdKhoaHoc.DataSource = dt;
                }

                if (ID != -1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(ID));
                    grvKhoaHoc.FocusedRowHandle = grvKhoaHoc.GetRowHandle(index);
                }

                if (dt.Rows.Count < 2)
                {
                    grvKhoaHoc_FocusedRowChanged(null, null);
                }

                grvKhoaHoc.Columns["NGAY_BD"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                grvKhoaHoc.Columns["NGAY_BD"].DisplayFormat.FormatString = "dd/MM/yyyy";
                grvKhoaHoc.Columns["NGAY_KT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                grvKhoaHoc.Columns["NGAY_KT"].DisplayFormat.FormatString = "dd/MM/yyyy";

            }
            catch { }
        }
        private void LoadCboNoiDaoTao()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoNoiDaoTao", Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadLookUpEdit(NOI_DTLookUpEdit, dt, "ID", "Name", "ID");
        }
        private void LoadCboTheoYC()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoTheoYC", Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadLookUpEdit(THEO_YEU_CAULookUpEdit, dt, "ID", "Name", "ID");
        }

        private void LoadView(bool them)
        {
            if (them == true)
            {
                //khi thêm
                TEN_KHOA_DTTextEdit.EditValue = "";
                NOI_DTLookUpEdit.EditValue = 1;
                TRUONG_DTTextEdit.EditValue = "";
                TIN_CHITextEdit.EditValue = "";
                NGAY_BDDateEdit.DateTime = DateTime.Today;
                GIO_BDTimeEdit.Time = DateTime.Today;
                NGAY_KTDateEdit.DateTime = DateTime.Today;
                GIO_KTtimeEdit.Time = DateTime.Today;
                THOI_GIANTextEdit.EditValue = "";
                THOI_GIAN_HOCTextEdit.EditValue = "";
                HINH_THUC_DTTextEdit.EditValue = "";
                LINH_VUC_DTTextEdit.EditValue = "";
                GIAO_VIENTextEdit.EditValue = "";
                DIA_DIEMTextEdit.EditValue = "";
                TRONG_NUOCCheckEdit.EditValue = false;
                HANH_CHANH.EditValue = false;
                THEO_YEU_CAULookUpEdit.EditValue = 1;
                PHUONG_TIEN_DI_CHUYENTextEdit.EditValue = "";
                CAM_KETMenoEdit.EditValue = "";
                GHI_CHUMenoEdit.EditValue = "";
            }
            else
            {
                //khi Sửa

            }

        }
        private void ReadOnlycontrol(bool themcontrol)
        {
            try
            {
                TEN_KHOA_DTTextEdit.Properties.ReadOnly = themcontrol;
                NOI_DTLookUpEdit.Properties.ReadOnly = themcontrol;
                TRUONG_DTTextEdit.Properties.ReadOnly = themcontrol;
                TIN_CHITextEdit.Properties.ReadOnly = themcontrol;
                NGAY_BDDateEdit.Properties.ReadOnly = themcontrol;
                GIO_BDTimeEdit.Properties.ReadOnly = themcontrol;
                NGAY_KTDateEdit.Properties.ReadOnly = themcontrol;
                GIO_KTtimeEdit.Properties.ReadOnly = themcontrol;
                THOI_GIANTextEdit.Properties.ReadOnly = themcontrol;
                THOI_GIAN_HOCTextEdit.Properties.ReadOnly = themcontrol;
                HINH_THUC_DTTextEdit.Properties.ReadOnly = themcontrol;
                LINH_VUC_DTTextEdit.Properties.ReadOnly = themcontrol;
                GIAO_VIENTextEdit.Properties.ReadOnly = themcontrol;
                DIA_DIEMTextEdit.Properties.ReadOnly = themcontrol;
                TRONG_NUOCCheckEdit.Properties.ReadOnly = themcontrol;
                HANH_CHANH.Properties.ReadOnly = themcontrol;
                THEO_YEU_CAULookUpEdit.Properties.ReadOnly = themcontrol;
                PHUONG_TIEN_DI_CHUYENTextEdit.Properties.ReadOnly = themcontrol;
                CAM_KETMenoEdit.Properties.ReadOnly = themcontrol;
                GHI_CHUMenoEdit.Properties.ReadOnly = themcontrol;
                HOC_PHITextEdit.Properties.ReadOnly = themcontrol;


            }
            catch
            {
            }


        }
        #endregion

        #region Các hàm xử lý
        private void enableButon(bool visible)
        {

            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = visible;
            windowsUIButton.Buttons[6].Properties.Visible = !visible;
            windowsUIButton.Buttons[7].Properties.Visible = !visible;
            windowsUIButton.Buttons[8].Properties.Visible = visible;
            groupDanhSachKhoaHoc.Enabled = visible;
            ReadOnlycontrol(visible);
            if (tabbedControlGroup1.SelectedTabPageIndex == 1)
            {
                windowsUIButton.Buttons[0].Properties.Visible = false;
            }
        }
        private void grvKhoaHoc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //bingding dữ liệu
            try
            {
                if (tabbedControlGroup1.SelectedTabPageIndex == 0)
                {
                    TEN_KHOA_DTTextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("TEN_KHOA_DT");
                    NOI_DTLookUpEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("NOI_DT");
                    TRUONG_DTTextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("TRUONG_DT");
                    TIN_CHITextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("TIN_CHI");
                    NGAY_BDDateEdit.DateTime = Convert.ToDateTime(grvKhoaHoc.GetFocusedRowCellValue("NGAY_BD"));
                    GIO_BDTimeEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("GIO_BD");
                    NGAY_KTDateEdit.DateTime = Convert.ToDateTime(grvKhoaHoc.GetFocusedRowCellValue("NGAY_KT"));
                    GIO_KTtimeEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("GIO_KT");
                    THOI_GIANTextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("THOI_GIAN");
                    THOI_GIAN_HOCTextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("THOI_GIAN_HOC");
                    HINH_THUC_DTTextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("HINH_THUC_DT");
                    LINH_VUC_DTTextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("LINH_VUC_DT");
                    GIAO_VIENTextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("GIAO_VIEN");
                    DIA_DIEMTextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("DIA_DIEM");
                    TRONG_NUOCCheckEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("TRONG_NUOC");
                    HANH_CHANH.EditValue = grvKhoaHoc.GetFocusedRowCellValue("HANH_CHANH");
                    THEO_YEU_CAULookUpEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("THEO_YEU_CAU");
                    PHUONG_TIEN_DI_CHUYENTextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("PHUONG_TIEN_DI_CHUYEN");
                    CAM_KETMenoEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("CAM_KET");
                    GHI_CHUMenoEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("GHI_CHU");
                }
                else
                {
                    LoadGridKeHoachDaoTao(false);
                }
            }
            catch
            {

            }
        }
        private void InDuLieu()
        {
            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            frmViewReport frm = new frmViewReport();
            frm.rpt = new rptKeHoachDaoTao(DateTime.Now);
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptKeHoachDaoTao", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_KDT", SqlDbType.Int).Value = Convert.ToInt64(grvKhoaHoc.GetFocusedRowCellValue("ID_KDT"));
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
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            if (btn == null || btn.Tag == null) return;
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        them = true;
                        bthem = true;
                        enableButon(false);
                        if (tabbedControlGroup1.SelectedTabPageIndex == 1)
                        {
                            if (grvKhoaHoc.RowCount == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonKhoaHoc"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                enableButon(true);
                                them = false;
                                bthem = false;
                                return;
                            }
                            LoadGridKeHoachDaoTao(true);
                        }
                        else
                        {
                            LoadView(true);
                        }
                        break;
                    }
                case "sua":
                    {
                        if (tabbedControlGroup1.SelectedTabPageIndex == 1)
                        {
                            if (grvKhoaHoc.RowCount == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonKhoaHoc"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                enableButon(true);
                                them = false;
                                bthem = false;
                                return;
                            }
                            LoadGridKeHoachDaoTao(true);
                        }
                        else
                        {
                            them = true;
                            bthem = false;
                        }
                        enableButon(false);
                        break;
                    }

                case "xoa":
                    {
                        XoaKhoaDaoTao();
                        break;
                    }
                case "In":
                    {
                        InDuLieu();
                        break;
                    }
                case "luu":
                    {
                        if (tabbedControlGroup1.SelectedTabPageIndex == 0)
                        {

                            if (!dxValidationProvider1.Validate()) return;
                            try
                            {
                                LoadGridControl(Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spGetUpdateKhoaDaoTao", grvKhoaHoc.GetFocusedRowCellValue("ID_KDT"),
                                TEN_KHOA_DTTextEdit.EditValue,
                                HINH_THUC_DTTextEdit.EditValue,
                                LINH_VUC_DTTextEdit.EditValue,
                                THOI_GIAN_HOCTextEdit.EditValue,
                                NGAY_BDDateEdit.EditValue,
                                NGAY_KTDateEdit.EditValue,
                                GIAO_VIENTextEdit.EditValue,
                                GIO_BDTimeEdit.Time.TimeOfDay,
                                GIO_KTtimeEdit.Time.TimeOfDay,
                                DIA_DIEMTextEdit.EditValue,
                                TRONG_NUOCCheckEdit.EditValue,
                                HANH_CHANH.EditValue,
                                HOC_PHITextEdit.EditValue,
                                PHUONG_TIEN_DI_CHUYENTextEdit.EditValue,
                                CAM_KETMenoEdit.EditValue,
                                GHI_CHUMenoEdit.EditValue,
                                TRUONG_DTTextEdit.EditValue,
                                THEO_YEU_CAULookUpEdit.EditValue,
                                TIN_CHITextEdit.EditValue,
                                NOI_DTLookUpEdit.EditValue,
                                THOI_GIANTextEdit.EditValue,
                                bthem
                                )));
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            grvDSCN.PostEditor();
                            grvDSCN.UpdateCurrentRow();
                            //Create bảng tạm được chon
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sbtKHDT" + Commons.Modules.UserName, Commons.Modules.ObjSystems.ConvertDatatable(grdDSCN), "");

                            string sSql = "DELETE FROM dbo.KE_HOACH_DAO_TAO WHERE EXISTS(SELECT * FROM sbtKHDT" + Commons.Modules.UserName + " B WHERE KE_HOACH_DAO_TAO.ID_KDT = B.ID_KDT AND KE_HOACH_DAO_TAO.ID_CN = B.ID_CN AND B.CHON = 0) update t1 set t1.DIEM = t2.DIEM, t1.KET_QUA = t2.ID_KQ, t1.DANH_GIA = t2.DANH_GIA,t1.HOC_PHI_CTY = t2.HOC_PHI_CTY,t1.HOC_PHI_NV = t2.HOC_PHI_NV from KE_HOACH_DAO_TAO t1 join sbtKHDT"
                                + Commons.Modules.UserName + " t2 ON t2.ID_KDT = t1.ID_KDT AND t2.ID_CN = t1.ID_CN AND CHON = 1 INSERT INTO dbo.KE_HOACH_DAO_TAO(ID_KDT, ID_CN, DIEM, KET_QUA, DANH_GIA, HOC_PHI_CTY, HOC_PHI_NV) SELECT " + grvKhoaHoc.GetFocusedRowCellValue("ID_KDT") + ",A.ID_CN,A.DIEM,A.ID_KQ,A.DANH_GIA,A.HOC_PHI_CTY,A.HOC_PHI_NV FROM sbtKHDT"
                                + Commons.Modules.UserName + " A WHERE NOT  EXISTS(SELECT B.ID_KDT, ID_CN  FROM dbo.KE_HOACH_DAO_TAO B WHERE B.ID_KDT = A.ID_KDT AND B.ID_CN = A.ID_CN)AND A.CHON = 1 ";
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                            Commons.Modules.ObjSystems.XoaTable("sbtKHDT" + Commons.Modules.UserName);
                            LoadGridKeHoachDaoTao(false);
                        }
                        enableButon(true);
                        them = false;
                        break;
                    }
                case "khongluu":
                    {
                        them = false;
                        enableButon(true);
                        if (tabbedControlGroup1.SelectedTabPageIndex == 0)
                        {
                            grvKhoaHoc_FocusedRowChanged(null, null);
                        }
                        else
                        {
                            LoadGridKeHoachDaoTao(false);
                        }
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                default:
                    break;
            }
        }
        private void XoaKhoaDaoTao()
        {
            //xóa

            if (grdDSCN.Focused)
            {
                XoaKeHoachKhoaDaoTao();
            }
            else
            {
                if (grvDSCN.RowCount > 0)
                {
                    if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteKhoaDaoTaoCoHocVien"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    try
                    {
                        SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE FROM dbo.KE_HOACH_DAO_TAO WHERE ID_KDT = " + grvKhoaHoc.GetFocusedRowCellValue("ID_KDT") + "  DELETE FROM dbo.KHOA_DAO_TAO WHERE ID_KDT = " + grvKhoaHoc.GetFocusedRowCellValue("ID_KDT") + "");
                        grvKhoaHoc.DeleteSelectedRows();
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteKhoaDaoTao"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    try
                    {
                        SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE FROM dbo.KHOA_DAO_TAO WHERE ID_KDT = " + grvKhoaHoc.GetFocusedRowCellValue("ID_KDT") + "");
                        grvKhoaHoc.DeleteSelectedRows();
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void XoaKeHoachKhoaDaoTao()
        {
            if (Convert.ToInt32(grvDSCN.GetFocusedRowCellValue("ID_CN")) > 1)
            {
                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteKeHoachDaoTao"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                //xóa
                try
                {
                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE FROM dbo.KE_HOACH_DAO_TAO WHERE ID_KDT = " + grvDSCN.GetFocusedRowCellValue("ID_KDT") + " AND ID_CN = " + grvDSCN.GetFocusedRowCellValue("ID_CN") + "");
                    grvDSCN.DeleteSelectedRows();
                    LoadGridKeHoachDaoTao(them);

                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonKhoaHoc"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void grdKhoaHoc_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                XoaKhoaDaoTao();
            }
        }

        #endregion

        #region Cac Ham Load Tab 1

        #endregion

        private void cboSearch_DV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboSearch_DV, cboSearch_XN);
            Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN, cboSearch_TO);
            LoadGridKeHoachDaoTao(them);
            Commons.Modules.sLoad = "";

        }

        private void cboSearch_XN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN, cboSearch_TO);
            LoadGridKeHoachDaoTao(them);
            Commons.Modules.sLoad = "";
        }

        private void cboSearch_TO_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGridKeHoachDaoTao(them);
            Commons.Modules.sLoad = "";
        }
        private void LoadGridKeHoachDaoTao(bool cochon)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListKeHoachDaoTao", grvKhoaHoc.GetFocusedRowCellValue("ID_KDT"),
                cboSearch_DV.EditValue, cboSearch_XN.EditValue, cboSearch_TO.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage,
                NoiDunglookUpEdit.EditValue, DiemtextEdit.EditValue, cochon));
            dt.Columns["CHON"].ReadOnly = false;
            dt.Columns["ID_CN"].ReadOnly = true;
            dt.Columns["TEN_CN"].ReadOnly = true;
            if (cochon == false)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSCN, grvDSCN, dt, false, false, false, true, true, this.Name);
                grvDSCN.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
                grvDSCN.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            }
            else
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSCN, grvDSCN, dt, true, false, false, true, true, this.Name);
                grvDSCN.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
                grvDSCN.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                grvDSCN.OptionsSelection.CheckBoxSelectorField = "CHON";
            }
            grvDSCN.Columns["CHON"].Visible = false;
            grvDSCN.Columns["ID_KDT"].Visible = false;
            grvDSCN.Columns["ID_CN"].Visible = false;

            grvDSCN.Columns["HOC_PHI_CTY"].DisplayFormat.FormatType = FormatType.Numeric;
            grvDSCN.Columns["HOC_PHI_CTY"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
            grvDSCN.Columns["HOC_PHI_NV"].DisplayFormat.FormatType = FormatType.Numeric;
            grvDSCN.Columns["HOC_PHI_NV"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;


            DataTable dtempt = new DataTable();
            dtempt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoKetQuaDT", Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.AddCombXtra("ID_KQ", "NAME_KQ", grvDSCN, dtempt, false);
        }

        private void tabbedControlGroup1_SelectedPageChanged(object sender, LayoutTabPageChangedEventArgs e)
        {
            if (tabbedControlGroup1.SelectedTabPageIndex == 0)
            {
                grvKhoaHoc_FocusedRowChanged(null, null);
            }
            else
            {
                LoadGridKeHoachDaoTao(false);
            }
            enableButon(true);
        }

        private void tabbedControlGroup1_SelectedPageChanging(object sender, LayoutTabPageChangingEventArgs e)
        {
            if (them == true)
            {
                e.Cancel = true;
            }
        }

        private void grdDSCN_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                XoaKhoaDaoTao();
            }
        }
        private void DEN_NGAYDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            LoadGridControl(-1);
        }

        private void LoadNoiDung()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNoiDungDanhGia", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
            Commons.Modules.ObjSystems.MLoadLookUpEdit(NoiDunglookUpEdit, dt, "ID_NDDG", "TEN_NDDG", "TEN_NDDG");
        }

        private void searchControl1_TextChanged(object sender, EventArgs e)
        {
            //if (windowsUIButton.Buttons[0].Properties.Visible == false)
            //{
            //    searchControl1.Client = grdDSCN;
            //}
            //else
            //{
            //    searchControl1.Client = grdKhoaHoc;
            //}
        }

        private void DiemtextEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void grvDSCN_RowCountChanged(object sender, EventArgs e)
        {
            //GridView view = sender as GridView;
            //try
            //{
            //    int index = ItemForSumNhanVien.Text.IndexOf(':');
            //    if (index > 0)
            //    {
            //        if (view.RowCount > 0)
            //        {
            //            ItemForSumNhanVien.Text = ItemForSumNhanVien.Text.Substring(0, index) + ": " + view.RowCount.ToString();
            //        }
            //        else
            //        {
            //            ItemForSumNhanVien.Text = ItemForSumNhanVien.Text.Substring(0, index) + ": 0";
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message.ToString());
            //}
        }
    }
}
