using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraLayout;

namespace Vs.HRM
{
    public partial class ucHopDong : DevExpress.XtraEditors.XtraUserControl
    {
        Int64 idcn = 0;
        Int64 id_HD;
        int checkSuaNay = 0;
        bool cothem = false;
        DataTable tableTTC_CN = new DataTable();
        string strDuongDan = "";
        WindowsUIButton btn1 = null;
        public ucHopDong(Int64 id)
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
            idcn = id;
        }
        private void UcHopDong_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            formatText();

            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_CVLookUpEdit, Commons.Modules.ObjSystems.DataChucVu(false, Convert.ToInt32(-1)), "ID_CV", "TEN_CV", "TEN_CV");

            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_LHDLDLookUpEdit, Commons.Modules.ObjSystems.DataLoaiHDLD(false), "ID_LHDLD", "TEN_LHDLD", "TEN_LHDLD", true);
            Commons.Modules.ObjSystems.MLoadLookUpEdit(NGUOI_KY_GIA_HANLookUpEdit, Commons.Modules.ObjSystems.DataNguoiKy(), "ID_NK", "HO_TEN", "HO_TEN");
            Commons.Modules.ObjSystems.MLoadLookUpEdit(NGUOI_KY_GIA_HANLookUpEdit, Commons.Modules.ObjSystems.DataNguoiKy(), "ID_NK", "HO_TEN", "HO_TEN");
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cboTinhTrang, Commons.Modules.ObjSystems.DataTinhTrang(false), "ID_TT", "TenTT", "TenTT");
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNgachLuong, Commons.Modules.ObjSystems.DataNgachLuong(false), "ID_NL", "MS_NL", "MS_NL", true, true);
            //Commons.Modules.ObjSystems.MLoadLookUpEdit(cboBAC_LUONG, Commons.Modules.ObjSystems.DataBacLuong(-1, DateTime.Today, true), "ID_BL", "TEN_BL", "TEN_BL", true);
            LoadgrdHopDong(-1);
            enableButon(true);
            Commons.Modules.sLoad = "";

            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
        }

        private void formatText()
        {
            Commons.OSystems.SetDateEditFormat(NGAY_BAT_DAU_HDDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_HET_HDDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_KYDateEdit);

            MUC_LUONG_CHINHTextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            CHI_SO_PHU_CAPTextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            MUC_LUONG_THUC_LINHTextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
        }

        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = visible;
            windowsUIButton.Buttons[6].Properties.Visible = visible;
            windowsUIButton.Buttons[7].Properties.Visible = visible;
            windowsUIButton.Buttons[8].Properties.Visible = !visible;
            windowsUIButton.Buttons[9].Properties.Visible = !visible;
            windowsUIButton.Buttons[10].Properties.Visible = visible;
            Commons.Modules.bEnabel = !visible;
            txtTaiLieu.Properties.ReadOnly = visible;
            grdHopDong.Enabled = visible;
            SO_HDLDTextEdit.Properties.ReadOnly = visible;
            ID_LHDLDLookUpEdit.Properties.ReadOnly = visible;
            NGAY_BAT_DAU_HDDateEdit.Properties.ReadOnly = visible;
            NGAY_HET_HDDateEdit.Properties.ReadOnly = visible;
            NGAY_KYDateEdit.Properties.ReadOnly = visible;
            HD_GIA_HANCheckEdit.Properties.ReadOnly = visible;
            cboBAC_LUONG.Properties.ReadOnly = visible;
            cboNgachLuong.Properties.ReadOnly = visible;
            MUC_LUONG_CHINHTextEdit.Properties.ReadOnly = visible;
            CHI_SO_PHU_CAPTextEdit.Properties.ReadOnly = visible;
            MUC_LUONG_THUC_LINHTextEdit.Properties.ReadOnly = visible;
            DIA_DIEM_LAM_VIECTextEdit.Properties.ReadOnly = visible;
            DIA_CHI_NOI_LAM_VIECTextEdit.Properties.ReadOnly = visible;
            CONG_VIECTextEdit.Properties.ReadOnly = visible;
            ID_CVLookUpEdit.Properties.ReadOnly = visible;
            SO_NGAY_PHEPTextEdit.Properties.ReadOnly = visible;
            NGUOI_KY_GIA_HANLookUpEdit.Properties.ReadOnly = visible;
            cboTinhTrang.Properties.ReadOnly = visible;


        }

        private void Loaddatatable()
        {
            tableTTC_CN.Clear();
            tableTTC_CN.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetThongTinKyHopDong", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Commons.Modules.iCongNhan));
        }

        private void Bindingdata(bool bthem)
        {
            if (bthem == true)
            {
                //lấy dữ liệu mặc định theo id công nhân
                try
                {
                    Loaddatatable();
                    SO_HDLDTextEdit.EditValue = "";

                    if (grvHopDong.RowCount == 0)
                    {
                        NGAY_BAT_DAU_HDDateEdit.EditValue = DateTime.Now;
                    }
                    else
                    {
                        NGAY_BAT_DAU_HDDateEdit.EditValue = Convert.ToDateTime(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnLayNgayBDHD(" + Commons.Modules.iCongNhan + ")"));
                    }
                    NGAY_KYDateEdit.EditValue = DateTime.Today;
                    MUC_LUONG_CHINHTextEdit.EditValue = Commons.Modules.ObjSystems.BLMCPC(Commons.Modules.iCongNhan, NGAY_BAT_DAU_HDDateEdit.DateTime)["ML"];
                    CHI_SO_PHU_CAPTextEdit.EditValue = Commons.Modules.ObjSystems.BLMCPC(Commons.Modules.iCongNhan, NGAY_BAT_DAU_HDDateEdit.DateTime)["PC"];
                    MUC_LUONG_THUC_LINHTextEdit.EditValue = Convert.ToDouble(MUC_LUONG_CHINHTextEdit.EditValue) + Convert.ToDouble(CHI_SO_PHU_CAPTextEdit.EditValue);
                    DIA_DIEM_LAM_VIECTextEdit.EditValue = tableTTC_CN.Rows[0]["TEN_DV"];
                    DIA_CHI_NOI_LAM_VIECTextEdit.EditValue = tableTTC_CN.Rows[0]["DIA_CHI"];
                    CONG_VIECTextEdit.EditValue = tableTTC_CN.Rows[0]["TEN_LCV"];
                    ngayhethan(Convert.ToInt32(ID_LHDLDLookUpEdit.EditValue));
                    ID_CVLookUpEdit.EditValue = tableTTC_CN.Rows[0]["ID_CV"];
                    SO_NGAY_PHEPTextEdit.EditValue = "";
                    NGUOI_KY_GIA_HANLookUpEdit.EditValue = tableTTC_CN.Rows[0]["NK"];
                    txtTaiLieu.ResetText();
                    ID_LHDLDLookUpEdit.EditValue = null;
                    cboTinhTrang.EditValue = 1;
                    cboNgachLuong.EditValue = "";
                    cboBAC_LUONG.EditValue = "";

                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                try
                {

                    SO_HDLDTextEdit.EditValue = grvHopDong.GetFocusedRowCellValue("SO_HDLD");
                    ID_LHDLDLookUpEdit.EditValue = grvHopDong.GetFocusedRowCellValue("ID_LHDLD");
                    NGAY_BAT_DAU_HDDateEdit.EditValue = grvHopDong.GetFocusedRowCellValue("NGAY_BAT_DAU_HD");
                    NGAY_HET_HDDateEdit.EditValue = grvHopDong.GetFocusedRowCellValue("NGAY_HET_HD");
                    NGAY_KYDateEdit.EditValue = Convert.ToDateTime(grvHopDong.GetFocusedRowCellValue("NGAY_KY")) == DateTime.MinValue ? NGAY_KYDateEdit.EditValue = null : Convert.ToDateTime(grvHopDong.GetFocusedRowCellValue("NGAY_KY"));
                    HD_GIA_HANCheckEdit.EditValue = Convert.ToBoolean(grvHopDong.GetFocusedRowCellValue("HD_GIA_HAN"));
                    try
                    {
                        cboNgachLuong.EditValue = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_NL FROM dbo.NGACH_LUONG WHERE MS_NL =  " + Convert.ToInt32(grvHopDong.GetFocusedRowCellValue("NGACH_LUONG")) + ""));
                    }
                    catch { }
                    cboBAC_LUONG.EditValue = grvHopDong.GetFocusedRowCellValue("BAC_LUONG");
                    MUC_LUONG_CHINHTextEdit.EditValue = grvHopDong.GetFocusedRowCellValue("MUC_LUONG_CHINH");
                    CHI_SO_PHU_CAPTextEdit.EditValue = grvHopDong.GetFocusedRowCellValue("CHI_SO_PHU_CAP");
                    MUC_LUONG_THUC_LINHTextEdit.EditValue = grvHopDong.GetFocusedRowCellValue("MUC_LUONG_THUC_LINH");
                    DIA_DIEM_LAM_VIECTextEdit.EditValue = grvHopDong.GetFocusedRowCellValue("DIA_DIEM_LAM_VIEC");
                    DIA_CHI_NOI_LAM_VIECTextEdit.EditValue = grvHopDong.GetFocusedRowCellValue("DIA_CHI_NOI_LAM_VIEC");
                    CONG_VIECTextEdit.EditValue = grvHopDong.GetFocusedRowCellValue("CONG_VIEC");
                    ID_CVLookUpEdit.EditValue = grvHopDong.GetFocusedRowCellValue("ID_CV");
                    SO_NGAY_PHEPTextEdit.EditValue = grvHopDong.GetFocusedRowCellValue("SO_NGAY_PHEP");
                    NGUOI_KY_GIA_HANLookUpEdit.EditValue = Convert.ToInt32(grvHopDong.GetFocusedRowCellValue("NGUOI_KY_GIA_HAN"));
                    cboTinhTrang.EditValue = string.IsNullOrEmpty(grvHopDong.GetFocusedRowCellValue("ID_TT").ToString()) ? cboTinhTrang.EditValue = null : Convert.ToInt32(grvHopDong.GetFocusedRowCellValue("ID_TT"));
                    txtTaiLieu.EditValue = grvHopDong.GetFocusedRowCellValue("TAI_LIEU");

                }
                catch
                {
                }
            }
        }

        private bool SaveData()
        {
            try
            {
                DateTime NGAY_HET_HD;
                bool IsCorrectFormatDateTime = false;

                if (getID_TT_HD() == 3)
                {
                    IsCorrectFormatDateTime = System.DateTime.TryParse(NGAY_HET_HDDateEdit.EditValue.ToString().Trim(), out NGAY_HET_HD);
                    if (!IsCorrectFormatDateTime)
                    {
                        return false;
                    }
                }
                int n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateHopDong",
                grvHopDong.GetFocusedRowCellValue("ID_HDLD"),
                Commons.Modules.iCongNhan,
                SO_HDLDTextEdit.EditValue,
                ID_LHDLDLookUpEdit.EditValue,
                NGAY_BAT_DAU_HDDateEdit.EditValue,
                NGAY_HET_HDDateEdit.EditValue,
                NGAY_KYDateEdit.EditValue,
                HD_GIA_HANCheckEdit.EditValue,
                NGAY_BAT_DAU_HDDateEdit.EditValue,
                NGAY_HET_HDDateEdit.EditValue,
                MUC_LUONG_CHINHTextEdit.EditValue,
                cboBAC_LUONG.EditValue,
                MUC_LUONG_CHINHTextEdit.EditValue,
                CHI_SO_PHU_CAPTextEdit.EditValue,
                MUC_LUONG_THUC_LINHTextEdit.EditValue,
                DIA_DIEM_LAM_VIECTextEdit.EditValue,
                DIA_CHI_NOI_LAM_VIECTextEdit.EditValue,
                CONG_VIECTextEdit.EditValue,
                ID_CVLookUpEdit.EditValue,
                SO_NGAY_PHEPTextEdit.EditValue,
                NGUOI_KY_GIA_HANLookUpEdit.EditValue, cboTinhTrang.EditValue, txtTaiLieu.EditValue,
                cothem,
                Convert.ToInt32(cboTinhTrang.EditValue)

                ));
                LoadgrdHopDong(n);
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                return false;
            }
        }

        private void DeleteData()
        {

            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteHopDong"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                //SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.HOP_DONG_LAO_DONG WHERE ID_HDLD =" + grvHopDong.GetFocusedRowCellValue("ID_HDLD") + "");
                SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spDeleteHopDong", grvHopDong.GetFocusedRowCellValue("ID_HDLD"), Commons.Modules.iCongNhan);
                grvHopDong.DeleteSelectedRows();
                Bindingdata(false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung") + "\n" + ex.Message.ToString());
            }
        }

        private void LoadgrdHopDong(int id)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListHopDong", idcn, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_HDLD"] };
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdHopDong, grvHopDong, dt, false, true, true, true, true, this.Name);
            //grvHopDong.Columns["SO_HDLD"].AppearanceHeader.ForeColor = Color.Red;

            grvHopDong.Columns["ID_HDLD"].Visible = false;
            grvHopDong.Columns["ID_LHDLD"].Visible = false;
            grvHopDong.Columns["HD_GIA_HAN"].Visible = false;
            grvHopDong.Columns["NGAY_BD_THU_VIEC"].Visible = false;
            grvHopDong.Columns["NGAY_KT_THU_VIEC"].Visible = false;
            grvHopDong.Columns["LUONG_THU_VIEC"].Visible = false;
            grvHopDong.Columns["BAC_LUONG"].Visible = false;
            grvHopDong.Columns["MUC_LUONG_CHINH"].Visible = false;
            grvHopDong.Columns["CHI_SO_PHU_CAP"].Visible = false;
            grvHopDong.Columns["MUC_LUONG_THUC_LINH"].Visible = false;
            grvHopDong.Columns["DIA_DIEM_LAM_VIEC"].Visible = false;
            grvHopDong.Columns["DIA_CHI_NOI_LAM_VIEC"].Visible = false;
            grvHopDong.Columns["CONG_VIEC"].Visible = false;
            grvHopDong.Columns["ID_CV"].Visible = false;
            grvHopDong.Columns["SO_NGAY_PHEP"].Visible = false;
            grvHopDong.Columns["NGUOI_KY_GIA_HAN"].Visible = false;
            grvHopDong.Columns["ID_TT"].Visible = false;
            grvHopDong.Columns["TAI_LIEU"].Visible = false;

            //format column
            grvHopDong.Columns["NGAY_BAT_DAU_HD"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            grvHopDong.Columns["NGAY_BAT_DAU_HD"].DisplayFormat.FormatString = "dd/MM/yyyy";
            grvHopDong.Columns["NGAY_HET_HD"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            grvHopDong.Columns["NGAY_HET_HD"].DisplayFormat.FormatString = "dd/MM/yyyy";
            grvHopDong.Columns["NGAY_KY"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            grvHopDong.Columns["NGAY_KY"].DisplayFormat.FormatString = "dd/MM/yyyy";

            if (id != -1)
            {
                int index = dt.Rows.IndexOf(dt.Rows.Find(id));
                grvHopDong.FocusedRowHandle = grvHopDong.GetRowHandle(index);
            }
        }

        private void GrvHopDong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Bindingdata(false);
        }

        private void GrdHopDong_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                DeleteData();
            }
        }

        private void ID_LHDLDLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            ngayhethan(Convert.ToInt32(ID_LHDLDLookUpEdit.EditValue));
            if (windowsUIButton.Buttons[2].Properties.Visible) return;
            switch (getID_TT_HD())
            {
                case 1: // hợp đồng xác định thời hạn
                    {
                        HD_GIA_HANCheckEdit.Properties.ReadOnly = true;
                        HD_GIA_HANCheckEdit.Checked = true;
                        NGAY_HET_HDDateEdit.Properties.ReadOnly = false;
                        NGAY_HET_HDDateEdit.EditValue = Convert.ToDateTime(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnNgayKetThucHD('" + Convert.ToDateTime(NGAY_BAT_DAU_HDDateEdit.DateTime.AddYears(1)).ToString("MM/dd/yyyy") + "'," + 1 + ")"));
                        break;
                    }
                case 2: // Không xác định thời hạn
                    {
                        NGAY_HET_HDDateEdit.Properties.ReadOnly = true;
                        NGAY_HET_HDDateEdit.EditValue = null;
                        HD_GIA_HANCheckEdit.Properties.ReadOnly = true;
                        HD_GIA_HANCheckEdit.Checked = true;
                        break;
                    }
                case 3: // Hợp đồng thử việc
                    {
                        try
                        {
                            NGAY_KYDateEdit.DateTime = DateTime.Now;
                            NGAY_HET_HDDateEdit.Properties.ReadOnly = false;
                            HD_GIA_HANCheckEdit.Properties.ReadOnly = true;
                            HD_GIA_HANCheckEdit.Checked = false;
                            int iNgayTV = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(SO_NGAY,0) SO_NGAY FROM dbo.LOAI_HDLD WHERE ID_LHDLD = " + Convert.ToInt32(ID_LHDLDLookUpEdit.EditValue) + ""));
                            if (iNgayTV == 0)
                            {
                                NGAY_HET_HDDateEdit.EditValue = null;
                            }
                            else
                            {

                                NGAY_BAT_DAU_HDDateEdit.EditValue = Convert.ToDateTime(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnLayNgayBDHD(" + Commons.Modules.iCongNhan + ")"));
                                NGAY_HET_HDDateEdit.EditValue = Convert.ToDateTime(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnNgayKetThucHD('" + Convert.ToDateTime(NGAY_BAT_DAU_HDDateEdit.DateTime).ToString("MM/dd/yyyy") + "'," + iNgayTV + ")"));
                            }

                        }
                        catch { }
                        break;
                    }
            }
        }

        private void ngayhethan(int thoihan)
        {
            int ithang = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(SO_THANG,0) SO_THANG FROM dbo.LOAI_HDLD WHERE ID_LHDLD = " + thoihan + ""));
        }

        private void NGAY_BD_THU_VIECDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            ////////////NGAY_KT_THU_VIECDateEdit.EditValue = NGAY_BD_THU_VIECDateEdit.DateTime.AddMonths(2);
            ////////////NGAY_KT_THU_VIECDateEdit.EditValue = NGAY_KT_THU_VIECDateEdit.DateTime.AddDays(-1);
        }

        private void MUC_LUONG_CHINHTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            MUC_LUONG_THUC_LINHTextEdit.EditValue = Convert.ToDouble(MUC_LUONG_CHINHTextEdit.EditValue) + Convert.ToDouble(CHI_SO_PHU_CAPTextEdit.EditValue);
        }

        private void windowsUIButton_ButtonClick_1(object sender, ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                btn1 = btn;
                XtraUserControl ctl = new XtraUserControl();
                if (btn == null || btn.Tag == null) return;
                switch (btn.Tag.ToString())
                {
                    case "them":
                        {
                            if (Commons.Modules.iCongNhan == -1)
                            {

                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            Bindingdata(true);
                            cothem = true;
                            enableButon(false);
                            ID_LHDLDLookUpEdit_EditValueChanged(null, null);
                            break;
                        }
                    case "sua":
                        {
                            if ((Convert.ToInt32(cboTinhTrang.EditValue) == 2 && (Commons.Modules.UserName != "admin" || Commons.Modules.UserName != "administrator")) || (Convert.ToInt32(cboTinhTrang.EditValue) == 3 && (Commons.Modules.UserName != "admin" || Commons.Modules.UserName != "administrator")))
                            {
                                XtraMessageBox.Show(cboTinhTrang.Text + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTinhTrangKhongSuaDuoc"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            if (grvHopDong.RowCount == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (grvHopDong.RowCount == 0) return;
                            cothem = false;
                            enableButon(false);
                            checkSuaNay = 1;
                            ///ID_LHDLDLookUpEdit_EditValueChanged(null, null);
                            break;
                        }

                    case "xoa":
                        {
                            if (grvHopDong.RowCount == 0)
                            {

                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (grvHopDong.RowCount == 0) return;
                            Commons.Modules.ObjSystems.Xoahinh(txtTaiLieu.Text);
                            if (Convert.ToInt32(grvHopDong.GetFocusedRowCellValue("ID_TT")) == 2)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgHopDongDaKyKhongDuocXoa"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                DeleteData();
                            }
                            break;
                        }
                    case "In":
                        {
                            int idHD = Convert.ToInt32(grvHopDong.GetFocusedRowCellValue("ID_HDLD"));
                            frmInHopDongCN InHopDongCN = new frmInHopDongCN(Commons.Modules.iCongNhan, idHD, "");
                            InHopDongCN.ShowDialog();
                            break;
                        }
                    case "luu":
                        {
                            if (!dxValidationProvider1.Validate()) return;
                            if (Convert.ToInt32(MUC_LUONG_CHINHTextEdit.EditValue) < 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgLuongKhongDuocNhoHon0"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MUC_LUONG_CHINHTextEdit.EditValue = 0;
                                return;
                            }

                            if (getID_TT_HD() == 1 || getID_TT_HD() == 2)
                            {
                                if (getID_TT_HD() == 1)
                                {
                                    if (NGAY_BAT_DAU_HDDateEdit.Text == "" || NGAY_HET_HDDateEdit.Text == "")
                                    {
                                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgNgayBDVaNgayHetHDKhongDuocTrong"));
                                        NGAY_BAT_DAU_HDDateEdit.Focus();
                                        return;
                                    }
                                }
                                else
                                {
                                    if (NGAY_BAT_DAU_HDDateEdit.Text == "")
                                    {
                                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgNgayBatDauHDKhongDuocTrong"));
                                        NGAY_BAT_DAU_HDDateEdit.Focus();
                                        return;

                                    }
                                }
                            }

                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            if (cothem == true)
                            {
                                id_HD = -1;
                            }
                            else
                            {
                                id_HD = Convert.ToInt64(grvHopDong.GetFocusedRowCellValue("ID_HDLD"));
                            }

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spkiemtrungHDLD", conn);
                            cmd.Parameters.Add("@ID_HD", SqlDbType.BigInt).Value = id_HD;
                            cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = Commons.Modules.iCongNhan;
                            cmd.Parameters.Add("@SO_HD", SqlDbType.NVarChar).Value = SO_HDLDTextEdit.Text;
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (Convert.ToInt16(cmd.ExecuteScalar()) == 1)
                            {

                                XtraMessageBox.Show(ItemForSO_HDLD.Text + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgSoQD_NayDaTonTai"));
                                SO_HDLDTextEdit.Focus();
                                return;
                            }
                            conn.Close();

                            if (Convert.ToInt32(cboTinhTrang.EditValue) != 2 && grvHopDong.RowCount > 0)
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCoHDchuaky"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.YesNo) == DialogResult.No) return;
                                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "UPDATE dbo.HOP_DONG_LAO_DONG SET ID_TT = 2 WHERE ID_CN =" + Commons.Modules.iCongNhan + "");
                            }
                            if (Convert.ToInt32(cboTinhTrang.EditValue) == 2 && txtTaiLieu.Text.Trim() == "")
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTaiLieuKhongDuocTrong"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                            }
                            if (SaveData())
                            {
                                Commons.Modules.ObjSystems.LuuDuongDan(strDuongDan, txtTaiLieu.Text);
                                enableButon(true);
                            }
                            else
                            {
                                enableButon(false);
                            }
                            break;
                        }
                    case "khongluu":
                        {
                            enableButon(true);
                            Bindingdata(false);
                            dxValidationProvider1.ValidateHiddenControls = true;
                            dxValidationProvider1.RemoveControlError(SO_HDLDTextEdit);
                            break;
                        }
                    case "thoat":
                        {
                            Commons.Modules.ObjSystems.GotoHome(this);
                            break;
                        }
                    case "phuluchd":
                        {
                            try
                            {
                                if (grvHopDong.GetFocusedRowCellValue("ID_HDLD").ToString() == "")
                                {

                                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChonHDDeXemPhuLuc"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            catch (Exception)
                            {

                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChonHDDeXemPhuLuc"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                                return;
                            }
                            frmPhuLucHDLD pl = new frmPhuLucHDLD(ItemForSO_HDLD.Text + " :" + SO_HDLDTextEdit.EditValue.ToString(), ItemForNGAY_BAT_DAU_HD.Text + " :" + NGAY_BAT_DAU_HDDateEdit.DateTime.Date.ToShortDateString(), Convert.ToInt32(grvHopDong.GetFocusedRowCellValue("ID_HDLD")));
                            pl.ShowDialog();
                            break;
                        }
                    case "thaydoitk":
                        {
                            try
                            {
                                if (grvHopDong.GetFocusedRowCellValue("ID_HDLD").ToString() == "")
                                {

                                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChonHDDeXemBHYT"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            catch (Exception)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChonHDDeXemBHYT"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                                return;
                            }
                            frmToKhaiBHXH bhyt = new frmToKhaiBHXH(ItemForSO_HDLD.Text + " :" + SO_HDLDTextEdit.EditValue.ToString(), ItemForNGAY_BAT_DAU_HD.Text + " :" + NGAY_BAT_DAU_HDDateEdit.DateTime.Date.ToShortDateString(), Convert.ToInt32(grvHopDong.GetFocusedRowCellValue("ID_HDLD")));
                            bhyt.ShowDialog();
                            break;
                        }
                    default:
                        break;
                }
            }
            catch { }
        }

        private void NGAY_BAT_DAU_HDDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                int iNgayTV = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(SO_NGAY,0) SO_NGAY FROM dbo.LOAI_HDLD WHERE ID_LHDLD = " + Convert.ToInt32(ID_LHDLDLookUpEdit.EditValue) + ""));


                if (windowsUIButton.Buttons[6].Properties.Visible == false)
                {
                    if (NGAY_HET_HDDateEdit.EditValue != null)
                    {
                        if (checkNgay(Convert.ToDateTime(NGAY_BAT_DAU_HDDateEdit.EditValue), Convert.ToDateTime(NGAY_HET_HDDateEdit.EditValue)) == 3)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgNgayBatDauKhongDuocLonHonNgayKetThuc"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            NGAY_BAT_DAU_HDDateEdit.EditValue = Convert.ToDateTime(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnLayNgayBDHD(" + Commons.Modules.iCongNhan + ")"));
                        }
                        int A = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnKiemNgayBDHD(" + Commons.Modules.iCongNhan + ",'" + Convert.ToDateTime(NGAY_BAT_DAU_HDDateEdit.EditValue).ToString("MM/dd/yyyy") + "')"));
                        if (A == 1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgNgayBatDauKhongDuocLonHonNVL"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            NGAY_BAT_DAU_HDDateEdit.EditValue = Convert.ToDateTime(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnLayNgayBDHD(" + Commons.Modules.iCongNhan + ")"));
                        }
                        else
                        {
                            NGAY_HET_HDDateEdit.EditValue = Convert.ToDateTime(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnNgayKetThucHD('" + Convert.ToDateTime(NGAY_BAT_DAU_HDDateEdit.DateTime).ToString("MM/dd/yyyy") + "'," + iNgayTV + ")"));
                            return;
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void LayDuongDan()
        {
            string strPath_DH = txtTaiLieu.Text;
            strDuongDan = ofdfile.FileName;

            var strDuongDanTmp = Commons.Modules.ObjSystems.CapnhatTL("Tai_Lieu_HD");
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
            if (e.Button.Index == 0)
            {
                try
                {
                    if (windowsUIButton.Buttons[6].Properties.Visible)
                    {
                        if (txtTaiLieu.Text == "") return;
                        Commons.Modules.ObjSystems.OpenHinh(txtTaiLieu.Text);

                    }
                    else
                    {
                        ofdfile.ShowDialog();
                        LayDuongDan();
                    }
                }
                catch
                {
                }
            }
            else
            {
                try
                {
                    if (Convert.ToInt32(cboTinhTrang.EditValue) == 2 && txtTaiLieu.Text.Trim() == "")
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgHopDongDaKyTaiLieuKhongTheXoa"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                    }
                    Commons.Modules.ObjSystems.Xoahinh(txtTaiLieu.Text);
                    txtTaiLieu.ResetText();
                    grvHopDong.SetFocusedRowCellValue("TAI_LIEU", null);
                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "UPDATE dbo.KHEN_THUONG SET TAI_LIEU = NULL WHERE ID_HDLD =" + grvHopDong.GetFocusedRowCellValue("ID_HDLD") + "");
                }
                catch
                {
                }
            }
        }

        private void cboBAC_LUONG_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            try
            {
                string strSQL = "SELECT MUC_LUONG, PC_DH + PC_KY_NANG PHU_CAP FROM dbo.BAC_LUONG WHERE ID_BL = " + cboBAC_LUONG.EditValue + "";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, strSQL));
                MUC_LUONG_CHINHTextEdit.EditValue = string.IsNullOrEmpty(dt.Rows[0]["MUC_LUONG"].ToString()) ? 0 : Convert.ToDouble(dt.Rows[0]["MUC_LUONG"]);
                CHI_SO_PHU_CAPTextEdit.EditValue = string.IsNullOrEmpty(dt.Rows[0]["PHU_CAP"].ToString()) ? 0 : Convert.ToDouble(dt.Rows[0]["PHU_CAP"]);
            }
            catch
            {
                MUC_LUONG_CHINHTextEdit.EditValue = 0;
                CHI_SO_PHU_CAPTextEdit.EditValue = 0;
                MUC_LUONG_THUC_LINHTextEdit.EditValue = 0;
            }
        }

        private int getID_TT_HD()
        {
            int id_TT_HD = -1;
            try
            {
                return id_TT_HD = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT T1.ID_TT_HD FROM dbo.TINH_TRANG_HD T1 INNER JOIN dbo.LOAI_HDLD T2 ON T2.ID_TT_HD = T1.ID_TT_HD WHERE T2.ID_LHDLD = " + (ID_LHDLDLookUpEdit.Text == "" ? -1 : Convert.ToInt32(ID_LHDLDLookUpEdit.EditValue)) + ""));
            }
            catch
            {
                return id_TT_HD;
            }
        }

        private void NGAY_KT_THU_VIECDateEdit_EditValueChanged(object sender, EventArgs e)
        {


        }

        static int checkNgay(DateTime Ngay1, DateTime Ngay2)
        {
            int result = DateTime.Compare(Ngay1, Ngay2);
            if (result < 0)
                return 1;
            else if (result == 0)
                return 2;
            else
                return 3;
        }

        private void NGAY_HET_HDDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            ngayhethan(Convert.ToInt32(ID_LHDLDLookUpEdit.EditValue));
            int iNgayTV = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(SO_NGAY,0) SO_NGAY FROM dbo.LOAI_HDLD WHERE ID_LHDLD = " + Convert.ToInt32(ID_LHDLDLookUpEdit.EditValue) + ""));
            int A = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnKiemNgayBDHD(" + Commons.Modules.iCongNhan + ",'" + Convert.ToDateTime(NGAY_BAT_DAU_HDDateEdit.EditValue).ToString("MM/dd/yyyy") + "')"));
            if (windowsUIButton.Buttons[6].Properties.Visible == false)
            {
                if (NGAY_HET_HDDateEdit.EditValue != null)
                {
                    if (checkNgay(Convert.ToDateTime(NGAY_BAT_DAU_HDDateEdit.EditValue), Convert.ToDateTime(NGAY_HET_HDDateEdit.EditValue)) == 3)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgNgayBatDauKhongDuocLonHonNgayKetThuc"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        NGAY_HET_HDDateEdit.EditValue = Convert.ToDateTime(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnNgayKetThucHD('" + Convert.ToDateTime(NGAY_BAT_DAU_HDDateEdit.DateTime).ToString("MM/dd/yyyy") + "'," + iNgayTV + ")"));
                        return;
                    }

                }
            }
        }

        private void cboNgachLuong_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            ngayhethan(Convert.ToInt32(ID_LHDLDLookUpEdit.EditValue));
            try
            {
                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboBAC_LUONG, Commons.Modules.ObjSystems.DataBacLuong(Convert.ToInt64(cboNgachLuong.EditValue), DateTime.Now, false), "ID_BL", "TEN_BL", "TEN_BL");
            }
            catch { }
        }
    }
}
