using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraLayout;
using System.Drawing;

namespace Vs.HRM
{
    public partial class ucTienLuong : DevExpress.XtraEditors.XtraUserControl
    {
        static Int64 idcn = 0;
        Int64 id_TienLuong = 0;
        bool cothem = false;
        DataTable tableTTC_CN = new DataTable();
        string strDuongDan = "";
        public ucTienLuong(Int64 id)
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
            idcn = id;
        }
        #region function form Load
        private void LoadgrdTienLuong(int id)
        {
            try
            {


                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListTienLuong", idcn, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_LCB"] };
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdTienLuong, grvTienLuong, dt, false, true, true, true, true, this.Name);

                grvTienLuong.Columns["ID_LCB"].Visible = false;
                grvTienLuong.Columns["ID_CN"].Visible = false;
                grvTienLuong.Columns["ID_TO"].Visible = false;
                grvTienLuong.Columns["ID_CV"].Visible = false;
                grvTienLuong.Columns["ID_NK"].Visible = false;
                grvTienLuong.Columns["ID_NL"].Visible = false;
                grvTienLuong.Columns["ID_BL"].Visible = false;
                grvTienLuong.Columns["HS_LUONG"].Visible = false;
                grvTienLuong.Columns["LUONG_CO_BAN"].Visible = false;
                grvTienLuong.Columns["MUC_LUONG_THUC"].Visible = false;
                grvTienLuong.Columns["THUONG_CHUYEN_CAN"].Visible = false;
                grvTienLuong.Columns["PC_DOC_HAI"].Visible = false;
                grvTienLuong.Columns["THUONG_HT_CV"].Visible = false;
                grvTienLuong.Columns["PC_KY_NANG"].Visible = false;
                grvTienLuong.Columns["PC_SINH_HOAT"].Visible = false;
                grvTienLuong.Columns["PC_CON_NHO"].Visible = false;
                grvTienLuong.Columns["ID_TT"].Visible = false;
                grvTienLuong.Columns["TAI_LIEU"].Visible = false;


                //format column
                grvTienLuong.Columns["NGAY_HIEU_LUC"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                grvTienLuong.Columns["NGAY_HIEU_LUC"].DisplayFormat.FormatString = "dd/MM/yyyy";
                grvTienLuong.Columns["NGAY_KY"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                grvTienLuong.Columns["NGAY_KY"].DisplayFormat.FormatString = "dd/MM/yyyy";

                if (id != -1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(id));
                    grvTienLuong.FocusedRowHandle = grvTienLuong.GetRowHandle(index);
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
        #region function dung chung
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

            grdTienLuong.Enabled = visible;

            ID_TOLookUpEdit.Properties.ReadOnly = visible;
            ID_CVLookUpEdit.Properties.ReadOnly = visible;
            ID_NKLookUpEdit.Properties.ReadOnly = visible;
            NGAY_KYDateEdit.Properties.ReadOnly = visible;
            SO_QUYET_DINHTextEdit.Properties.ReadOnly = visible;
            NGAY_HIEU_LUCDateEdit.Properties.ReadOnly = visible;
            NGACH_LUONGLookUpEdit.Properties.ReadOnly = visible;
            BAC_LUONGLookUpEdit.Properties.ReadOnly = visible;
            GHI_CHUTextEdit.Properties.ReadOnly = visible;
            HS_LUONGTextEdit.Properties.ReadOnly = visible;
            cboTinhTrang.Properties.ReadOnly = visible;

            LUONG_CO_BANTextEdit.Properties.ReadOnly = visible;
            MUC_LUONG_THUCTextEdit.Properties.ReadOnly = visible;
            THUONG_CHUYEN_CANTextEdit.Properties.ReadOnly = visible;
            PC_DOC_HAITextEdit.Properties.ReadOnly = visible;
            THUONG_HT_CVTextEdit.Properties.ReadOnly = visible;
            PC_KY_NANGTextEdit.Properties.ReadOnly = visible;
            PC_SINH_HOATTextEdit.Properties.ReadOnly = visible;


            //PC_CON_NHOTextEdit.Properties.ReadOnly = visible;
        }
        private void Bindingdata(bool bthem)
        {
            try
            {

                if (bthem == true)
                {
                    Loaddatatable();

                    ID_TOLookUpEdit.EditValue = tableTTC_CN.Rows[0]["ID_TO"];
                    ID_CVLookUpEdit.EditValue = tableTTC_CN.Rows[0]["ID_CV"];
                    ID_NKLookUpEdit.EditValue = tableTTC_CN.Rows[0]["ID_NK"];
                    NGAY_KYDateEdit.EditValue = DateTime.Today;
                    SO_QUYET_DINHTextEdit.EditValue = "";
                    NGAY_HIEU_LUCDateEdit.EditValue = DateTime.Today;
                    NGACH_LUONGLookUpEdit.EditValue = null;
                    BAC_LUONGLookUpEdit.EditValue = null;
                    GHI_CHUTextEdit.EditValue = "";
                    cboTinhTrang.EditValue = 1;
                    txtTaiLieu.ResetText();
                    HS_LUONGTextEdit.EditValue = 0;
                    LUONG_CO_BANTextEdit.EditValue = 0;
                    MUC_LUONG_THUCTextEdit.EditValue = 0;
                    THUONG_CHUYEN_CANTextEdit.EditValue = 0;
                    PC_DOC_HAITextEdit.EditValue = 0;
                    THUONG_HT_CVTextEdit.EditValue = 0;
                    PC_KY_NANGTextEdit.EditValue = 0;
                    PC_SINH_HOATTextEdit.EditValue = 0;
                    BAC_LUONGLookUpEdit_EditValueChanged(null, null);
                }
                else
                {
                    ID_TOLookUpEdit.EditValue = grvTienLuong.GetFocusedRowCellValue("ID_TO") == null ? null : grvTienLuong.GetFocusedRowCellValue("ID_TO");
                    ID_CVLookUpEdit.EditValue = grvTienLuong.GetFocusedRowCellValue("ID_CV") == null ? null : grvTienLuong.GetFocusedRowCellValue("ID_CV");
                    ID_NKLookUpEdit.EditValue = grvTienLuong.GetFocusedRowCellValue("ID_NK") == null ? null : grvTienLuong.GetFocusedRowCellValue("ID_NK");
                    NGAY_KYDateEdit.EditValue = Convert.ToDateTime(grvTienLuong.GetFocusedRowCellValue("NGAY_KY")).Date == DateTime.MinValue ? NGAY_KYDateEdit.EditValue = null : Convert.ToDateTime(grvTienLuong.GetFocusedRowCellValue("NGAY_KY")).Date;
                    SO_QUYET_DINHTextEdit.EditValue = grvTienLuong.GetFocusedRowCellValue("SO_QUYET_DINH") == "" ? "" : grvTienLuong.GetFocusedRowCellValue("SO_QUYET_DINH");
                    NGAY_HIEU_LUCDateEdit.EditValue = Convert.ToDateTime(grvTienLuong.GetFocusedRowCellValue("NGAY_HIEU_LUC")).Date == DateTime.MinValue ? NGAY_HIEU_LUCDateEdit.EditValue = null : Convert.ToDateTime(grvTienLuong.GetFocusedRowCellValue("NGAY_HIEU_LUC")).Date;
                    NGACH_LUONGLookUpEdit.EditValue = grvTienLuong.GetFocusedRowCellValue("ID_NL");
                    BAC_LUONGLookUpEdit.EditValue = grvTienLuong.GetFocusedRowCellValue("ID_BL") == null ? BAC_LUONGLookUpEdit.EditValue = -1 : grvTienLuong.GetFocusedRowCellValue("ID_BL");
                    GHI_CHUTextEdit.EditValue = grvTienLuong.GetFocusedRowCellValue("GHI_CHU");
                    HS_LUONGTextEdit.EditValue = grvTienLuong.GetFocusedRowCellValue("HS_LUONG");
                    LUONG_CO_BANTextEdit.EditValue = grvTienLuong.GetFocusedRowCellValue("LUONG_CO_BAN");
                    THUONG_CHUYEN_CANTextEdit.EditValue = grvTienLuong.GetFocusedRowCellValue("THUONG_CHUYEN_CAN");
                    PC_DOC_HAITextEdit.EditValue = grvTienLuong.GetFocusedRowCellValue("PC_DOC_HAI");
                    THUONG_HT_CVTextEdit.EditValue = grvTienLuong.GetFocusedRowCellValue("THUONG_HT_CV");
                    PC_KY_NANGTextEdit.EditValue = grvTienLuong.GetFocusedRowCellValue("PC_KY_NANG");
                    PC_SINH_HOATTextEdit.EditValue = grvTienLuong.GetFocusedRowCellValue("PC_SINH_HOAT");
                    PC_CON_NHOTextEdit.EditValue = Convert.ToDouble(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.[funGetPhuCapConNho](" + Commons.Modules.iCongNhan + ")"));
                    cboTinhTrang.EditValue = string.IsNullOrEmpty(grvTienLuong.GetFocusedRowCellValue("ID_TT").ToString()) ? cboTinhTrang.EditValue = null : Convert.ToInt32(grvTienLuong.GetFocusedRowCellValue("ID_TT"));
                    txtTaiLieu.EditValue = grvTienLuong.GetFocusedRowCellValue("TAI_LIEU");

                    HS_LUONGTextEdit_EditValueChanged(null, null);
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void SaveData()
        {
            double luongthucold = 0;
            try
            {
                //lay luong gan nhat cua cong nhan do
                if (cothem == true)
                {
                    luongthucold = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT MUC_LUONG_THUC FROM dbo.LUONG_CO_BAN WHERE ID_CN = " + Commons.Modules.iCongNhan + " AND  NGAY_HIEU_LUC = (SELECT MAX(NGAY_HIEU_LUC) FROM dbo.LUONG_CO_BAN WHERE ID_CN = " + Commons.Modules.iCongNhan + ")"));
                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spPhatSinhPhuLucHopDong", Commons.Modules.TypeLanguage, Convert.ToInt32(Commons.Modules.iCongNhan), Convert.ToDateTime(NGAY_HIEU_LUCDateEdit.EditValue), Convert.ToDecimal(HS_LUONGTextEdit.EditValue));
                }
                else
                {
                    luongthucold = Convert.ToDouble(grvTienLuong.GetFocusedRowCellValue("MUC_LUONG_THUC"));
                }
            }
            catch
            {
                luongthucold = 0;
            }
            try
            {

                int n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateTienLuong",
                        id_TienLuong, Commons.Modules.iCongNhan, ID_TOLookUpEdit.EditValue, ID_CVLookUpEdit.EditValue, ID_NKLookUpEdit.EditValue,
                        NGAY_KYDateEdit.EditValue, SO_QUYET_DINHTextEdit.EditValue, NGAY_HIEU_LUCDateEdit.EditValue,
                        NGACH_LUONGLookUpEdit.EditValue, BAC_LUONGLookUpEdit.EditValue, GHI_CHUTextEdit.EditValue,
                        HS_LUONGTextEdit.EditValue.ToString() == "" ? 0 : HS_LUONGTextEdit.EditValue,
                        LUONG_CO_BANTextEdit.EditValue.ToString() == "" ? 0 : LUONG_CO_BANTextEdit.EditValue,
                        MUC_LUONG_THUCTextEdit.EditValue.ToString() == "" ? 0 : MUC_LUONG_THUCTextEdit.EditValue,
                        THUONG_CHUYEN_CANTextEdit.EditValue.ToString() == "" ? 0 : THUONG_CHUYEN_CANTextEdit.EditValue,
                        PC_DOC_HAITextEdit.EditValue.ToString() == "" ? 0 : PC_DOC_HAITextEdit.EditValue,
                        THUONG_HT_CVTextEdit.EditValue.ToString() == "" ? 0 : THUONG_HT_CVTextEdit.EditValue,
                        PC_KY_NANGTextEdit.EditValue.ToString() == "" ? 0 : PC_KY_NANGTextEdit.EditValue,
                        PC_SINH_HOATTextEdit.EditValue.ToString() == "" ? 0 : PC_SINH_HOATTextEdit.EditValue,
                        PC_CON_NHOTextEdit.EditValue.ToString() == "" ? 0 : PC_CON_NHOTextEdit.EditValue, cboTinhTrang.EditValue, txtTaiLieu.EditValue,
                         cothem));
                LoadgrdTienLuong(n);
                //--thêm phụ lục hợp đồng  cho công nhân đó
                //-- lấy id hợp đông lao động theo công nhân kèm điều kiện ngày lớn nhất
                try
                {
                    int idhd = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_HDLD FROM dbo.HOP_DONG_LAO_DONG WHERE ID_CN = " + Commons.Modules.iCongNhan + " AND  NGAY_BAT_DAU_HD = (SELECT MAX(NGAY_BAT_DAU_HD) FROM dbo.HOP_DONG_LAO_DONG WHERE ID_CN = " + Commons.Modules.iCongNhan + ")"));


                    string sophieu = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT MAX(SO_PLHD) + 1 FROM dbo.PHU_LUC_HDLD WHERE ID_HDLD = " + idhd + "").ToString();

                    if (idhd != 0)
                    {
                        if (Convert.ToDouble(MUC_LUONG_THUCTextEdit.EditValue) != luongthucold)
                        {
                            //insert vào phụ hợp đồng
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spUpdatetPhuLucHopDong",
                    idhd,
                    sophieu == "" ? "1" : sophieu,
                    sophieu == "" ? "1" : sophieu,
                    string.Format(Commons.Modules.ObjSystems.ThongTinChung()["PL_NOI_DUNG_THAY_DOI"].ToString(), string.Format("{0:N" + Commons.Modules.iSoLeTT + "}", luongthucold), string.Format("{0:N" + Commons.Modules.iSoLeTT + "}", MUC_LUONG_THUCTextEdit.EditValue)),
                    string.Format(Commons.Modules.ObjSystems.ThongTinChung()["PL_THOI_GIAN_THUC_HIEN"].ToString(), DateTime.Now.Day, DateTime.Now.Date.Month, DateTime.Now.Date.Year),
                   NGAY_KYDateEdit.EditValue,
                   ID_NKLookUpEdit.EditValue,
                   Commons.Modules.ObjSystems.ThongTinChung()["PL_GHI_CHU"].ToString(),
                   1
               );
                        }
                    }
                }
                catch
                {
                }

            }
            catch
            { }
        }
        private void DeleteData()
        {

            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteTienLuong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.LUONG_CO_BAN WHERE ID_LCB = " + grvTienLuong.GetFocusedRowCellValue("ID_LCB") + "");
                grvTienLuong.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelKhongThanhCong") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
        #region sự kiện form
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            if (btn == null || btn.Tag == null) return;
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        if (Commons.Modules.iCongNhan == -1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        cothem = true;
                        Bindingdata(true);
                        enableButon(false);
                        break;
                    }
                case "sua":
                    {
                        if ((Convert.ToInt32(cboTinhTrang.EditValue) == 2 && Commons.Modules.UserName != "admin") || (Convert.ToInt32(cboTinhTrang.EditValue) == 3 && Commons.Modules.UserName != "admin"))
                        {
                            XtraMessageBox.Show(cboTinhTrang.Text + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTinhTrangKhongSuaDuoc"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (grvTienLuong.RowCount == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        try
                        {

                            if (grvTienLuong.GetFocusedRowCellValue("ID_LCB").ToString() == "")
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgVuilongchondulieucansua"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Hand); return;
                            }
                        }
                        catch (Exception)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgVuilongchondulieucansua"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Hand); return;
                        }
                        cothem = false;
                        enableButon(false);
                        break;
                    }
                case "In":
                    {
                        int idLUONG = Convert.ToInt32(grvTienLuong.GetFocusedRowCellValue("ID_LCB"));
                        DateTime dtNgayHL = Convert.ToDateTime(grvTienLuong.GetFocusedRowCellValue("NGAY_HIEU_LUC"));
                        frmInLuongCN InLuongCN = new frmInLuongCN(Commons.Modules.iCongNhan, idLUONG, dtNgayHL, "");
                        InLuongCN.ShowDialog();
                        break;
                    }
                case "xoa":
                    {
                        if (grvTienLuong.RowCount == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        Commons.Modules.ObjSystems.Xoahinh(txtTaiLieu.Text);
                        DeleteData();
                        break;
                    }
                case "luu":
                    {

                        if (!dxValidationProvider1.Validate()) return;
                        if (BAC_LUONGLookUpEdit.Text.Trim() == "")
                        {
                            BAC_LUONGLookUpEdit.ErrorText = "This value is not valid";
                            return;
                        }
                        else
                        {
                            BAC_LUONGLookUpEdit.ErrorText = "";
                        }
                        //kiem trung
                        System.Data.SqlClient.SqlConnection conn;
                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                        conn.Open();
                        if (cothem == true)
                        {
                            id_TienLuong = -1;
                        }
                        else
                        {
                            id_TienLuong = Convert.ToInt64(grvTienLuong.GetFocusedRowCellValue("ID_LCB"));
                        }
                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spUpdateQuaTrinhCongTackiemtrung", conn);
                        cmd.Parameters.Add("@ID_LCB", SqlDbType.BigInt).Value = id_TienLuong;
                        cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = Commons.Modules.iCongNhan;
                        cmd.Parameters.Add("@SO_QUYET_DINH", SqlDbType.NVarChar).Value = SO_QUYET_DINHTextEdit.Text;
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (Convert.ToInt16(cmd.ExecuteScalar()) == 1)
                        {
                            XtraMessageBox.Show(ItemForSO_QUYET_DINH.Text + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgSoQD_NayDaTonTai"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"),MessageBoxButtons.OK,MessageBoxIcon.Information);
                            SO_QUYET_DINHTextEdit.Focus();
                            return;
                        }

                        SaveData();
                        Commons.Modules.ObjSystems.LuuDuongDan(strDuongDan, txtTaiLieu.Text);
                        enableButon(true);
                        break;
                    }
                case "khongluu":
                    {
                        enableButon(true);
                        Bindingdata(false);
                        dxValidationProvider1.ValidateHiddenControls = false;
                        dxValidationProvider1.RemoveControlError(SO_QUYET_DINHTextEdit);
                        dxValidationProvider1.RemoveControlError(BAC_LUONGLookUpEdit);
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
        private void grvTienLuong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Bindingdata(false);
        }
        #endregion

        private void UcTienLuong_Load(object sender, EventArgs e)
        {
            if (Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString() == "SB")
            {
                ItemForPC_SINH_HOAT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ItemForTHUONG_CHUYEN_CAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ItemForTHUONG_HT_CV.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ItemForPC_CON_NHO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            formatText();
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_TOLookUpEdit, Commons.Modules.ObjSystems.DataTo(-1, -1, false), "ID_TO", "TEN_TO", "TEN_TO");
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_NKLookUpEdit, Commons.Modules.ObjSystems.DataNguoiKy(), "ID_NK", "HO_TEN", "HO_TEN");
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_CVLookUpEdit, Commons.Modules.ObjSystems.DataChucVu(false), "ID_CV", "TEN_CV", "TEN_CV");
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(NGACH_LUONGLookUpEdit, Commons.Modules.ObjSystems.DataNgachLuong(false), "ID_NL", "MS_NL", "MS_NL", true);
            Commons.Modules.ObjSystems.MLoadLookUpEdit(BAC_LUONGLookUpEdit, Commons.Modules.ObjSystems.DataBacLuong(-1, DateTime.Today, true), "ID_BL", "TEN_BL", "TEN_BL", true);
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cboTinhTrang, Commons.Modules.ObjSystems.DataTinhTrang(false), "ID_TT", "TenTT", "TenTT");
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            Commons.Modules.sLoad = "";
            LoadgrdTienLuong(-1);
        }

        private void formatText()
        {
            Commons.OSystems.SetDateEditFormat(NGAY_HIEU_LUCDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_KYDateEdit);
            PC_DOC_HAITextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            LUONG_CO_BANTextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            HS_LUONGTextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            PC_SINH_HOATTextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            THUONG_CHUYEN_CANTextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            THUONG_HT_CVTextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            PC_KY_NANGTextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            PC_CON_NHOTextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            MUC_LUONG_THUCTextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
        }
        private void Loaddatatable()
        {
            tableTTC_CN.Clear();
            tableTTC_CN.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM dbo.funQuaTrinhLuong(" + Commons.Modules.iCongNhan + "," + Commons.Modules.TypeLanguage + ")"));
        }

        private void NGACH_LUONGLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(BAC_LUONGLookUpEdit, Commons.Modules.ObjSystems.DataBacLuong(Convert.ToInt32(NGACH_LUONGLookUpEdit.EditValue), Convert.ToDateTime(NGAY_HIEU_LUCDateEdit.EditValue), true), "ID_BL", "TEN_BL", "TEN_BL", true);
        }
        private void BAC_LUONGLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (Convert.ToInt32(BAC_LUONGLookUpEdit.EditValue) == -99 || BAC_LUONGLookUpEdit.EditValue.ToString() == "") return;
            DataTable dt = new DataTable();
            try
            {
                string sSql = "SELECT * FROM dbo.BAC_LUONG WHERE ID_BL = " + BAC_LUONGLookUpEdit.EditValue + "";
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                HS_LUONGTextEdit.EditValue = dt.Rows[0]["MUC_LUONG"];
                PC_DOC_HAITextEdit.EditValue = dt.Rows[0]["PC_DH"];
                THUONG_HT_CVTextEdit.EditValue = dt.Rows[0]["THUONG_TC"];
                THUONG_CHUYEN_CANTextEdit.EditValue = dt.Rows[0]["THUONG_CV_CC"];
                PC_SINH_HOATTextEdit.EditValue = dt.Rows[0]["PC_SINH_HOAT"];
                PC_KY_NANGTextEdit.EditValue = dt.Rows[0]["PC_KY_NANG"];
                LUONG_CO_BANTextEdit.EditValue = Convert.ToDouble(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT [dbo].[funGetLuongToiThieu](" + Commons.Modules.iCongNhan + ",'" + NGAY_HIEU_LUCDateEdit.DateTime.ToString("MM/dd/yyyy") + "')"));
                //PC_CON_NHOTextEdit
                PC_CON_NHOTextEdit.EditValue = Convert.ToDouble(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.[funGetPhuCapConNho](" + Commons.Modules.iCongNhan + ")"));
            }
            catch
            { }
        }
        private void GrdTienLuong_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                DeleteData();
            }
        }

        private void HS_LUONGTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                MUC_LUONG_THUCTextEdit.EditValue = string.IsNullOrEmpty(HS_LUONGTextEdit.Text) ? 0 : Convert.ToDouble(HS_LUONGTextEdit.EditValue) + (string.IsNullOrEmpty(PC_DOC_HAITextEdit.Text) ? 0 : Convert.ToDouble(PC_DOC_HAITextEdit.EditValue)) + (string.IsNullOrEmpty(PC_KY_NANGTextEdit.Text) ? 0 : Convert.ToDouble(PC_KY_NANGTextEdit.EditValue)) + (string.IsNullOrEmpty(PC_SINH_HOATTextEdit.Text) ? 0 : Convert.ToDouble(PC_SINH_HOATTextEdit.EditValue)) + (string.IsNullOrEmpty(PC_CON_NHOTextEdit.Text) ? 0 : Convert.ToDouble(PC_CON_NHOTextEdit.EditValue)) + (string.IsNullOrEmpty(THUONG_CHUYEN_CANTextEdit.Text) ? 0 : Convert.ToDouble(THUONG_CHUYEN_CANTextEdit.EditValue)) + (string.IsNullOrEmpty(THUONG_HT_CVTextEdit.Text) ? 0 : Convert.ToDouble(THUONG_HT_CVTextEdit.EditValue));
            }
            catch
            {
            }
        }

        private void THUONG_CHUYEN_CANTextEdit_Spin(object sender, DevExpress.XtraEditors.Controls.SpinEventArgs e)
        {
            e.Handled = true;
        }

        private void HS_LUONGTextEdit_Spin(object sender, DevExpress.XtraEditors.Controls.SpinEventArgs e)
        {
            e.Handled = true;
        }

        private void PC_DOC_HAITextEdit_Spin(object sender, DevExpress.XtraEditors.Controls.SpinEventArgs e)
        {
            e.Handled = true;
        }

        private void PC_SINH_HOATTextEdit_Spin(object sender, DevExpress.XtraEditors.Controls.SpinEventArgs e)
        {
            e.Handled = true;
        }

        private void THUONG_HT_CVTextEdit_Spin(object sender, DevExpress.XtraEditors.Controls.SpinEventArgs e)
        {
            e.Handled = true;
        }

        private void PC_KY_NANGTextEdit_Spin(object sender, DevExpress.XtraEditors.Controls.SpinEventArgs e)
        {
            e.Handled = true;
        }

        private void NGAY_HIEU_LUCDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (NGACH_LUONGLookUpEdit.Text != "")
            {
                Commons.Modules.ObjSystems.MLoadLookUpEdit(BAC_LUONGLookUpEdit, Commons.Modules.ObjSystems.DataBacLuong(Convert.ToInt32(NGACH_LUONGLookUpEdit.EditValue), Convert.ToDateTime(NGAY_HIEU_LUCDateEdit.EditValue), true), "ID_BL", "TEN_BL", "TEN_BL", true);
            }
        }

        private void PC_CON_NHOTextEdit_Spin(object sender, DevExpress.XtraEditors.Controls.SpinEventArgs e)
        {
            e.Handled = true;
        }
        private void LayDuongDan()
        {
            string strPath_DH = txtTaiLieu.Text;
            strDuongDan = ofdfile.FileName;

            var strDuongDanTmp = Commons.Modules.ObjSystems.CapnhatTL("Tai_Lieu_TL");
            string[] sFile;
            string TenFile;

            TenFile = ofdfile.SafeFileName.ToString();
            sFile = System.IO.Directory.GetFiles(strDuongDanTmp);

            if (Commons.Modules.ObjSystems.KiemFileTonTai(strDuongDanTmp + @"\" + ofdfile.SafeFileName.ToString()) == false)
                txtTaiLieu.Text = strDuongDanTmp + @"\" + ofdfile.SafeFileName.ToString();
            else
            {
                TenFile = Commons.Modules.ObjSystems.STTFileCungThuMuc(strDuongDanTmp, TenFile);
                txtTaiLieu.Text = strDuongDanTmp + @"\" + TenFile;
            }
        }
        private void txtTaiLieu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (windowsUIButton.Buttons[6].Properties.Visible)
                {
                    ofdfile.ShowDialog();
                    LayDuongDan();
                }
                else
                {
                    if (txtTaiLieu.Text == "")
                        return;
                    Commons.Modules.ObjSystems.OpenHinh(txtTaiLieu.Text);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
