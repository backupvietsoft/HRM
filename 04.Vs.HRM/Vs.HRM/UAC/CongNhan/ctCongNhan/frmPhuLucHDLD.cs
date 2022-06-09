using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Windows.Forms;
using Vs.Report;

namespace Vs.HRM
{
    public partial class frmPhuLucHDLD : DevExpress.XtraEditors.XtraForm
    {
        bool cothem = false;
        int idhdld = 0;
        public frmPhuLucHDLD(string sohd, string ngayhd, int idhd)
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, windowsUIButton);
            lbl_SoHD.Text = sohd;
            lbl_NgayHD.Text = ngayhd;
            idhdld = idhd;
        }

        #region sự kiện của form
        private void frmPhuLucHDLD_Load(object sender, EventArgs e)
        {
            //load combobox ID_QHLookUpEdit
            Commons.OSystems.SetDateEditFormat(NGAY_KYDateEdit);
            Commons.Modules.ObjSystems.MLoadLookUpEdit(NGUOI_KYLookUpEdit, Commons.Modules.ObjSystems.DataNguoiKy(), "ID_NK", "HO_TEN", "HO_TEN");
            LoadgrdPhuLucHopDong("-1");
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
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
                        Bindingdata(true);
                        cothem = true;
                        enableButon(false);
                        break;
                    }
                case "sua":
                    {
                        if (grvPLHD.RowCount == 0) return;
                        cothem = false;
                        enableButon(false);
                        break;
                    }

                case "xoa":
                    {
                        if (grvPLHD.RowCount == 0) return;
                        DeleteData();
                        break;
                    }
                case "In":
                    {
                        switch (Commons.Modules.ObjSystems.KyHieuDV_CN(Commons.Modules.iCongNhan))
                        {
                            case "MT":
                                {
                                    InPLHD_MT();
                                    break;
                                }
                            case "SB":
                                {
                                    InPLHD_SB();
                                    break;
                                }
                            default:
                                {
                                    InPLHD_MT();
                                    break;
                                }
                        }
                        break;
                    }
                case "luu":
                    {
                        if (!dxValidationProvider1.Validate()) return;
                        if (SaveData() == false) return;
                        enableButon(true);
                        break;
                    }
                case "khongluu":
                    {
                        enableButon(true);
                        Bindingdata(false);
                        dxValidationProvider1.Validate();
                        break;
                    }
                case "thoat":
                    {
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgBanCoMuonThoatChuongtrinh"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeThoat"), MessageBoxButtons.YesNo) == DialogResult.No) return;
                        this.Close();
                        break;
                    }
                default:
                    break;
            }
        }
        private void grvPLHD_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Bindingdata(false);
        }
        private void grdPLHD_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                DeleteData();
            }
        }
        #endregion

        #region hàm load form
        //hàm load gridview
        private void LoadgrdPhuLucHopDong(string id)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListPhuLucHopDong", idhdld, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["SO_PLHD"] };
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdPLHD, grvPLHD, dt, false, true, true, true, true, this.Name);
            grvPLHD.Columns["NOI_DUNG_THAY_DOI"].Visible = false;
            grvPLHD.Columns["THOI_GIAN_THUC_HIEN"].Visible = false;
            grvPLHD.Columns["GHI_CHU"].Visible = false;
            grvPLHD.Columns["NGUOI_KY"].Visible = false;
            if (id != "-1")
            {
                int index = dt.Rows.IndexOf(dt.Rows.Find(id));
                grvPLHD.FocusedRowHandle = grvPLHD.GetRowHandle(index);
            }
            if (grvPLHD.RowCount == 1)
            {
                Bindingdata(false);
            }
        }

        private void Loadgrvtheoidcn(int id)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListPhuLucHopDong", id, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["SO_PLHD"] };
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdPLHD, grvPLHD, dt, false, true, true, true, true, this.Name);

        }
        //hàm bingding dữ liệu
        private void Bindingdata(bool bthem)
        {
            Commons.Modules.sPS = "0Load";
            if (bthem == true)
            {
                //lấy dữ liệu mặc định theo id công nhân
                try
                {
                    //string sSql = "SELECT TOP 1 *,(SELECT MAX(SO_PLHD) +1 FROM dbo.PHU_LUC_HDLD WHERE ID_HDLD = " + idhdld + ") AS SOPL FROM dbo.PHU_LUC_HDLD WHERE NGAY_KY = (SELECT MAX(NGAY_KY) FROM dbo.PHU_LUC_HDLD)";
                    //DataTable dt = new DataTable();
                    // dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                    // Loadgrvtheoidcn(idhdld);
                    SO_PLHDTextEdit.EditValue = "";
                    NOI_DUNG_THAY_DOIMemoEdit.EditValue = "";
                    THOI_GIAN_THUC_HIENMemoEdit.EditValue = "";
                    NGAY_KYDateEdit.EditValue = DateTime.Today;
                    NGUOI_KYLookUpEdit.EditValue = null;
                    GHI_CHUMemoEdit.EditValue = "";

                }
                catch (Exception ex)
                {
                    //XtraMessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                SO_PLHDTextEdit.EditValue = grvPLHD.GetFocusedRowCellValue("SO_PLHD");
                NOI_DUNG_THAY_DOIMemoEdit.EditValue = grvPLHD.GetFocusedRowCellValue("NOI_DUNG_THAY_DOI");
                THOI_GIAN_THUC_HIENMemoEdit.EditValue = grvPLHD.GetFocusedRowCellValue("THOI_GIAN_THUC_HIEN");
                NGAY_KYDateEdit.EditValue = grvPLHD.GetFocusedRowCellValue("NGAY_KY");
                NGUOI_KYLookUpEdit.EditValue = grvPLHD.GetFocusedRowCellValue("NGUOI_KY");
                GHI_CHUMemoEdit.EditValue = grvPLHD.GetFocusedRowCellValue("GHI_CHU");
            }
        }
        //hàm tắc mở control
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = visible;
            windowsUIButton.Buttons[6].Properties.Visible = visible;
            windowsUIButton.Buttons[7].Properties.Visible = !visible;
            windowsUIButton.Buttons[8].Properties.Visible = !visible;
            grdPLHD.Enabled = visible;
            //ID_QHLookUpEdit.Properties.ReadOnly = visible;
            SO_PLHDTextEdit.Properties.ReadOnly = visible;
            NOI_DUNG_THAY_DOIMemoEdit.Properties.ReadOnly = visible;
            THOI_GIAN_THUC_HIENMemoEdit.Properties.ReadOnly = visible;
            NGAY_KYDateEdit.Properties.ReadOnly = visible;
            NGUOI_KYLookUpEdit.Properties.ReadOnly = visible;
            GHI_CHUMemoEdit.Properties.ReadOnly = visible;
        }
        private void InPLHD_MT()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptPhuLucHopDongLaoDong(NGAY_KYDateEdit.DateTime);

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptPhuLucHopDong", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_HDLD", SqlDbType.Int).Value = idhdld;
                cmd.Parameters.Add("@SO_PLHD", SqlDbType.NVarChar, 30).Value = SO_PLHDTextEdit.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DA_TA";
                frm.AddDataSource(dt);

                frm.ShowDialog();
            }
            catch
            {

            }
        }
        private void InPLHD_SB()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptPhuLucHopDongLaoDong_SB(NGAY_KYDateEdit.DateTime);

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptPhuLucHopDong_SB", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_HDLD", SqlDbType.Int).Value = idhdld;
                cmd.Parameters.Add("@SO_PLHD", SqlDbType.NVarChar, 30).Value = SO_PLHDTextEdit.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DA_TA";
                frm.AddDataSource(dt);

                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[1].Copy();
                dt1.TableName = "NOI_DUNG";
                frm.AddDataSource(dt1);

                frm.ShowDialog();
            }
            catch
            {

            }
        }
        #endregion

        #region hàm sử lý data
        //hàm sử lý khi lưu dữ liệu(thêm/Sửa)
        private bool SaveData()
        {
            try
            {
                string sophieu = "";
                try
                {
                    sophieu = grvPLHD.GetFocusedRowCellValue("SO_PLHD").ToString();
                }
                catch (Exception)
                {
                    sophieu = "";
                }

                string n = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdatePhuLucHopDong",
                    idhdld,
                    sophieu,
                    SO_PLHDTextEdit.EditValue.ToString(),
                    NOI_DUNG_THAY_DOIMemoEdit.EditValue,
                    THOI_GIAN_THUC_HIENMemoEdit.EditValue,
                    NGAY_KYDateEdit.EditValue,
                    NGUOI_KYLookUpEdit.EditValue,
                    GHI_CHUMemoEdit.EditValue,
                    cothem
                ).ToString();
                LoadgrdPhuLucHopDong(n);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //hàm xử lý khi xóa dữ liệu
        private void DeleteData()
        {
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDeletePhuLucHopDong"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeXoa"), MessageBoxButtons.YesNo) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE	dbo.PHU_LUC_HDLD WHERE ID_HDLD  = " + idhdld + " AND SO_PLHD = N'" + grvPLHD.GetFocusedRowCellValue("SO_PLHD").ToString().Trim() + "'");
                grvPLHD.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
    }
}