using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.ApplicationBlocks.Data;
using Vs.Report;
using System.IO;
using Commons;

namespace Vs.HRM
{
    public partial class ucDaoTao : DevExpress.XtraEditors.XtraUserControl
    {
        private Int64 iIDDT = 0;
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
                tabbedControlGroup1.SelectedTabPageIndex = 0;
                TU_NGAYDateEdit.DateTime = firstDay;
                DEN_NGAYDateEdit.EditValue = lastDay;
                Commons.Modules.sLoad = "";
                enableButon(true);
                LoadCboNoiDaoTao();
                LoadCboTheoYC();
                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboTinhTrang, Commons.Modules.ObjSystems.DataTinhTrangDT(false), "ID_TT_DT", "TEN_TT_DT", "TEN_TT_DT");
                LoadGridControl(-1);
                LoadGridKeHoachDaoTao();
                Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            }
            catch
            {
            }
        }
        #region Các hàm load tab 0
        private void LoadGridControl(Int64 ID)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0FS";
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
                    grvKhoaHoc.Columns["NGAY_BD"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    grvKhoaHoc.Columns["NGAY_BD"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    grvKhoaHoc.Columns["NGAY_KT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    grvKhoaHoc.Columns["NGAY_KT"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    Commons.Modules.ObjSystems.AddCombXtra("ID_TT_DT", "TEN_TT_DT", grvKhoaHoc, Commons.Modules.ObjSystems.DataTinhTrangDT(false), false, "ID_TT_DT", this.Name);
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
                Commons.Modules.sLoad = "";
                grvKhoaHoc_FocusedRowChanged(null, null);
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

        private void BindingData(bool them)
        {
            if (them == true)
            {
                //khi thêm
                iIDDT = -1;

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
                cboTinhTrang.EditValue = 1;
                TRONG_NUOCCheckEdit.EditValue = false;
                HANH_CHANH.EditValue = false;
                THEO_YEU_CAULookUpEdit.EditValue = 1;
                PHUONG_TIEN_DI_CHUYENTextEdit.EditValue = "";
                CAM_KETMenoEdit.EditValue = "";
                GHI_CHUMenoEdit.EditValue = "";
                TEN_KHOA_DTTextEdit.Focus();
            }
            else
            {
                try
                {
                    iIDDT = Convert.ToInt64(grvKhoaHoc.GetFocusedRowCellValue("ID_KDT"));
                    TEN_KHOA_DTTextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("TEN_KHOA_DT");
                    NOI_DTLookUpEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("NOI_DT");
                    TRUONG_DTTextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("TRUONG_DT");
                    TIN_CHITextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("TIN_CHI");
                    try
                    {
                        NGAY_BDDateEdit.DateTime = Convert.ToDateTime(grvKhoaHoc.GetFocusedRowCellValue("NGAY_BD"));
                    }
                    catch
                    {
                        NGAY_BDDateEdit.EditValue = "";
                    }
                    try
                    {
                        NGAY_KTDateEdit.DateTime = Convert.ToDateTime(grvKhoaHoc.GetFocusedRowCellValue("NGAY_KT"));
                    }
                    catch
                    {
                        NGAY_KTDateEdit.EditValue = "";
                    }
                    GIO_BDTimeEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("GIO_BD");
                    GIO_KTtimeEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("GIO_KT");
                    THOI_GIANTextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("THOI_GIAN");
                    THOI_GIAN_HOCTextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("THOI_GIAN_HOC");
                    HINH_THUC_DTTextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("HINH_THUC_DT");
                    LINH_VUC_DTTextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("LINH_VUC_DT");
                    GIAO_VIENTextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("GIAO_VIEN");
                    DIA_DIEMTextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("DIA_DIEM");
                    cboTinhTrang.EditValue = grvKhoaHoc.GetFocusedRowCellValue("ID_TT_DT");
                    TRONG_NUOCCheckEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("TRONG_NUOC");
                    HANH_CHANH.EditValue = grvKhoaHoc.GetFocusedRowCellValue("HANH_CHANH");
                    THEO_YEU_CAULookUpEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("THEO_YEU_CAU");
                    PHUONG_TIEN_DI_CHUYENTextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("PHUONG_TIEN_DI_CHUYEN");
                    CAM_KETMenoEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("CAM_KET");
                    GHI_CHUMenoEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("GHI_CHU");
                    HOC_PHITextEdit.EditValue = grvKhoaHoc.GetFocusedRowCellValue("HOC_PHI");
                }
                catch
                {
                }
            }
            LoadGridKeHoachDaoTao();

        }
        private void ReadOnlycontrol(bool themcontrol)
        {
            TEN_KHOA_DTTextEdit.Properties.ReadOnly = themcontrol;
            NOI_DTLookUpEdit.Properties.ReadOnly = themcontrol;
            TRUONG_DTTextEdit.Properties.ReadOnly = themcontrol;
            TIN_CHITextEdit.Properties.ReadOnly = themcontrol;
            NGAY_BDDateEdit.Properties.ReadOnly = themcontrol;
            NGAY_BDDateEdit.Properties.Buttons[0].Enabled = !themcontrol;
            GIO_BDTimeEdit.Properties.ReadOnly = themcontrol;
            NGAY_KTDateEdit.Properties.ReadOnly = themcontrol;
            NGAY_KTDateEdit.Properties.Buttons[0].Enabled = !themcontrol;
            GIO_KTtimeEdit.Properties.ReadOnly = themcontrol;
            THOI_GIANTextEdit.Properties.ReadOnly = themcontrol;
            THOI_GIAN_HOCTextEdit.Properties.ReadOnly = themcontrol;
            HINH_THUC_DTTextEdit.Properties.ReadOnly = themcontrol;
            LINH_VUC_DTTextEdit.Properties.ReadOnly = themcontrol;
            GIAO_VIENTextEdit.Properties.ReadOnly = themcontrol;
            DIA_DIEMTextEdit.Properties.ReadOnly = themcontrol;
            cboTinhTrang.Properties.ReadOnly = themcontrol;
            TRONG_NUOCCheckEdit.Properties.ReadOnly = themcontrol;
            HANH_CHANH.Properties.ReadOnly = themcontrol;
            THEO_YEU_CAULookUpEdit.Properties.ReadOnly = themcontrol;
            PHUONG_TIEN_DI_CHUYENTextEdit.Properties.ReadOnly = themcontrol;
            CAM_KETMenoEdit.Properties.ReadOnly = themcontrol;
            GHI_CHUMenoEdit.Properties.ReadOnly = themcontrol;
            HOC_PHITextEdit.Properties.ReadOnly = themcontrol;
        }
        #endregion

        #region Các hàm xử lý
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = Commons.Modules.KyHieuDV == "DM" ? visible : false;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = visible;
            windowsUIButton.Buttons[6].Properties.Visible = visible;
            windowsUIButton.Buttons[7].Properties.Visible = !visible;
            windowsUIButton.Buttons[8].Properties.Visible = !visible;
            windowsUIButton.Buttons[9].Properties.Visible = !visible;
            windowsUIButton.Buttons[10].Properties.Visible = visible;
            groupDanhSachKhoaHoc.Enabled = visible;
            ReadOnlycontrol(visible);
            if (tabbedControlGroup1.SelectedTabPageIndex == 1 && Convert.ToInt32(cboTinhTrang.EditValue) == 1 && windowsUIButton.Buttons[1].Properties.Visible == false)
            {
                windowsUIButton.Buttons[7].Properties.Visible = true;
            }
            else
            {
                windowsUIButton.Buttons[7].Properties.Visible = false;
            }
        }
        private void grvKhoaHoc_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //bingding dữ liệu
            if (Commons.Modules.sLoad == "0FS") return;
            BindingData(false);
            try
            {
                if (Convert.ToInt32(cboTinhTrang.EditValue) == 1 || cboTinhTrang.EditValue == null)
                {
                    if(cboTinhTrang.EditValue == null)
                    {
                        //nếu không có dòng nào
                        windowsUIButton.Buttons[0].Properties.Visible = false;
                        windowsUIButton.Buttons[2].Properties.Visible = false;
                        windowsUIButton.Buttons[3].Properties.Visible = false;
                    }
                    else
                    {
                        windowsUIButton.Buttons[0].Properties.Visible = Commons.Modules.KyHieuDV == "DM" ? true : false;
                        windowsUIButton.Buttons[2].Properties.Visible = true;
                        windowsUIButton.Buttons[3].Properties.Visible = true;
                    }    
                }
                else
                {
                    windowsUIButton.Buttons[0].Properties.Visible = false;
                    windowsUIButton.Buttons[2].Properties.Visible = false;
                    windowsUIButton.Buttons[3].Properties.Visible = false;
                }
            }
            catch
            {
                windowsUIButton.Buttons[0].Properties.Visible = true;
                windowsUIButton.Buttons[2].Properties.Visible = true;
                windowsUIButton.Buttons[3].Properties.Visible = true;
            }
        }

        private string InDuLieuCD()
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
            catch
            {
            }
            frm.frmViewReport_Load(null, null);
            string file = DateTime.Now.ToString("yyyyMMdd_HHmmss") +".pdf";
            frm.rpt.ExportToPdf(file,null);
            string resulst = Commons.Modules.ObjSystems.FileCopy(Application.StartupPath, file, this.Name);
            try
            {
                File.Delete(file);
            }
            catch
            {
            }
            return resulst;

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
            catch
            {
            }
            frm.ShowDialog();
        }

        private bool checkDuyetTuDong()
        {
            //user có trong duyệt user
            string sSql = "SELECT COUNT(*) FROM dbo.DUYET_USER A INNER JOIN dbo.DUYET_QUY_DINH B ON B.ID_DQD = A.ID_DQD INNER JOIN dbo.DUYET_TAI_LIEU C ON C.ID_DTL = B.ID_DTL WHERE C.FORM_NAME = '" + this.Name + "' AND A.ID_USER = " + Commons.Modules.iIDUser + "";
            int n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql));
            if(n > 0)
            {
                 return true;
            }
            //kiểm tra không có bước duyệt nào
            sSql = "SELECT COUNT(*) FROM dbo.DUYET_BUOC A INNER JOIN dbo.DUYET_QUY_DINH B ON B.ID_DQD = A.ID_DQD INNER JOIN dbo.DUYET_TAI_LIEU C ON C.ID_DTL = B.ID_DTL WHERE C.FORM_NAME = '"+ this.Name + "'";
            n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql));
            if (n == 0)
            {
                return true;
            }
            //
            sSql = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr,CommandType.Text, "SELECT TOP 1 A.DIEU_KIEN_DUYET FROM dbo.DUYET_QUY_DINH A INNER JOIN dbo.DUYET_TAI_LIEU B ON B.ID_DTL = A.ID_DTL WHERE B.FORM_NAME = '" + this.Name + "'").ToString();
            n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql.Replace("@ID_KDT",iIDDT.ToString())));
            if (n == 0)
            {
                return true;
            }
            return false;
        }    


        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            if (btn == null || btn.Tag == null) return;
            switch (btn.Tag.ToString())
            {
                case "chuyenduyet":
                    {
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoMuonChuyenDuyetKhong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                        try
                        {
                            //kiểm tra duyệt quy trình
                            string Ykien = "";
                            bool KhanCap = false;
                            if(!checkDuyetTuDong())
                            {
                                frmYKienYC frm = new frmYKienYC();
                                if(frm.ShowDialog() == DialogResult.OK)
                                {
                                    Ykien = frm.txtYKien.Text;
                                    KhanCap = frm.chkKhanCap.Checked;
                                }    
                            }    

                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spQuyDinhDuyetTaiLieu",
                                Commons.Modules.iIDUser, 
                                this.Name, 
                                iIDDT, 
                                -1, 
                                ItemForTEN_KHOA_DT.Text, 
                                1,
                                InDuLieuCD(), 
                                KhanCap,
                                Ykien,
                                Commons.Modules.UserName,
                                Commons.Modules.TypeLanguage);
                            LoadGridControl(iIDDT);
                            windowsUIButton.Buttons[0].Properties.Visible = false;
                            LoadGridControl(iIDDT);
                        }
                        catch(Exception ex)
                        {
                        }
                        break;
                    }
                case "them":
                    {
                        tabbedControlGroup1.SelectedTabPageIndex = 0;
                        BindingData(true);
                        enableButon(false);
                        Commons.Modules.ObjSystems.AddnewRow(grvDSCN, false);
                        break;
                    }
                case "sua":
                    {

                        if (grvKhoaHoc.RowCount == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonKhoaHoc"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        enableButon(false);
                        Commons.Modules.ObjSystems.AddnewRow(grvDSCN, false);
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
                case "NhanVien":
                    {
                        try
                        {
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTChonNV" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grdDSCN), "");
                            frmChonNhanVien uv = new frmChonNhanVien();
                            if (uv.ShowDialog() == DialogResult.OK)
                            {
                                string sSql = "DELETE A FROM sBTChonNV" + Commons.Modules.iIDUser + " A WHERE NOT EXISTS(SELECT * FROM dbo.sBTNV" + Commons.Modules.iIDUser + " B WHERE B.CHON = 1 AND B.ID_CN = A.ID_CN)";
                                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);

                                sSql = "INSERT INTO dbo.sBTChonNV" + Commons.Modules.iIDUser + "(ID_KDT,ID_CN,MS_CN,TEN_CN,HOC_PHI_CTY,HOC_PHI_NV,DIEM,ID_KQ,DANH_GIA)SELECT " + iIDDT + ",A.ID_CN,MS_CN,TEN_CN,NULL,NULL,NULL,NULL,NULL FROM dbo.sBTNV" + Commons.Modules.iIDUser + " A WHERE A.CHON = 1 AND NOT EXISTS (SELECT * FROM dbo.sBTChonNV" + Commons.Modules.iIDUser + " B WHERE B.ID_CN = A.ID_CN)";
                                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);


                                DataTable dt = new DataTable();
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM dbo.sBTChonNV" + Commons.Modules.iIDUser));

                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    dt.Columns[i].ReadOnly = false;
                                }
                                dt.Columns["ID_CN"].ReadOnly = true;
                                dt.Columns["TEN_CN"].ReadOnly = true;

                                grdDSCN.DataSource = dt;

                                Commons.Modules.ObjSystems.XoaTable("sBTNV" + Commons.Modules.iIDUser);
                                Commons.Modules.ObjSystems.XoaTable("sBTChonNV" + Commons.Modules.iIDUser);

                            }
                        }
                        catch
                        {

                        }
                        break;
                    }
                case "luu":
                    {

                        if (!dxValidationProvider1.Validate()) return;
                        try
                        {
                            //Create bảng tạm được chon
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTChonNV" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grdDSCN), "");

                            LoadGridControl(Convert.ToInt64(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spGetUpdateKhoaDaoTao", iIDDT,
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
                            cboTinhTrang.EditValue,
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
                            "sBTChonNV" + Commons.Modules.iIDUser
                            )));
                        }
                        catch(Exception ex)
                        {
                        }
                        enableButon(true);
                        break;
                    }
                case "khongluu":
                    {
                        enableButon(true);
                        grvKhoaHoc_FocusedRowChanged(null, null);
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

        //private void cboSearch_DV_EditValueChanged(object sender, EventArgs e)
        //{
        //    if (Commons.Modules.sLoad == "0Load") return;
        //    Commons.Modules.sLoad = "0Load";
        //    Commons.Modules.ObjSystems.LoadCboXiNghiep(cboSearch_DV, cboSearch_XN);
        //    Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN, cboSearch_TO);
        //    LoadGridKeHoachDaoTao(them);
        //    Commons.Modules.sLoad = "";

        //}

        //private void cboSearch_XN_EditValueChanged(object sender, EventArgs e)
        //{
        //    if (Commons.Modules.sLoad == "0Load") return;
        //    Commons.Modules.sLoad = "0Load";
        //    Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN, cboSearch_TO);
        //    LoadGridKeHoachDaoTao(them);
        //    Commons.Modules.sLoad = "";
        //}


        //private void cboSearch_TO_EditValueChanged(object sender, EventArgs e)
        //{
        //    if (Commons.Modules.sLoad == "0Load") return;
        //    Commons.Modules.sLoad = "0Load";
        //    LoadGridKeHoachDaoTao(them);
        //    Commons.Modules.sLoad = "";
        //}

        private void LoadGridKeHoachDaoTao()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListKeHoachDaoTao", iIDDT,
                -1, -1, -1, Commons.Modules.UserName, Commons.Modules.TypeLanguage,
                -1, 0, false));
            dt.Columns["ID_CN"].ReadOnly = true;
            dt.Columns["TEN_CN"].ReadOnly = true;

            if (grdDSCN.DataSource == null)
            {

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSCN, grvDSCN, dt, false, false, false, true, true, this.Name);
                grvDSCN.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
                grvDSCN.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;


                grvDSCN.Columns["ID_KDT"].Visible = false;
                grvDSCN.Columns["ID_CN"].Visible = false;

                grvDSCN.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                grvDSCN.Columns["TEN_CN"].OptionsColumn.AllowEdit = false;

                Commons.Modules.ObjSystems.MFormatCol(grvDSCN, "HOC_PHI_CTY", Commons.Modules.iSoLeTT);
                Commons.Modules.ObjSystems.MFormatCol(grvDSCN, "HOC_PHI_NV", Commons.Modules.iSoLeTT);

                DataTable dtempt = new DataTable();
                dtempt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoKetQuaDT", Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.AddCombXtra("ID_KQ", "NAME_KQ", grvDSCN, dtempt,false ,"ID_KQ",this.Name,true);
            }
            else
            {
                grdDSCN.DataSource = dt;
            }
            Commons.Modules.ObjSystems.DeleteAddRow(grvDSCN);
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

        private void tabbedControlGroup1_SelectedPageChanged(object sender, LayoutTabPageChangedEventArgs e)
        {
            if (windowsUIButton.Buttons[0].Properties.Visible == false)
            {
                if (tabbedControlGroup1.SelectedTabPageIndex == 1 && Convert.ToInt32(cboTinhTrang.EditValue) == 1)
                {
                    windowsUIButton.Buttons[7].Properties.Visible = true;
                }
                else
                {
                    windowsUIButton.Buttons[7].Properties.Visible = false;
                }
            }
        }
    }
}
