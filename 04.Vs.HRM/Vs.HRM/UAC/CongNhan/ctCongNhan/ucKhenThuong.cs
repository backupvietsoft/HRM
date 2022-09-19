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
    public partial class ucKhenThuong : DevExpress.XtraEditors.XtraUserControl
    {
        static Int64 idcn = 0;
        Int64 id_kT = -1;
        bool cothem = false;
        string strDuongDan = "";
        WindowsUIButton btn1 = null;
        public ucKhenThuong(Int64 id)
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
            idcn = id;
        }
        private void UcKhenThuong_Load(object sender, EventArgs e)
        {
            Commons.OSystems.SetDateEditFormat(NGAY_HIEU_LUCDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_KYDateEdit);
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_KT_KLLookUpEdit, Commons.Modules.ObjSystems.DataKhenThuongKyLuat(false), "ID_KT_KL", "TEN_KT_KL", "TEN_KT_KL");
            Commons.Modules.ObjSystems.MLoadLookUpEdit(LOAI_KTLookUpEdit, Commons.Modules.ObjSystems.DataLoaiKhenThuong(false), "ID_LOAI_KT", "TEN_LOAI_KT", "TEN_LOAI_KT");
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_NKLookUpEdit, Commons.Modules.ObjSystems.DataNguoiKy(), "ID_NK", "HO_TEN", "HO_TEN");
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cboTinhTrang, Commons.Modules.ObjSystems.DataTinhTrang(false), "ID_TT", "TenTT", "TenTT");
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            LoadgrdKhenThuong(-1);
        }
        private void LoadgrdKhenThuong(int id)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListKhenThuong", idcn, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_KTHUONG"] };
            if (grdKhenThuong.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdKhenThuong, grvKhenThuong, dt, false, false, true, false, true, this.Name);
                //grvKhenThuong.Columns["SO_QUYET_DINH"].AppearanceHeader.ForeColor = Color.Red;
                grvKhenThuong.Columns["ID_KTHUONG"].Visible = false;
                grvKhenThuong.Columns["ID_CN"].Visible = false;
                grvKhenThuong.Columns["ID_NK"].Visible = false;
                grvKhenThuong.Columns["ID_KT_KL"].Visible = false;
                grvKhenThuong.Columns["LOAI_KT"].Visible = false;
                grvKhenThuong.Columns["NGAY_HIEU_LUC"].Visible = false;
                grvKhenThuong.Columns["GHI_CHU"].Visible = false;
                grvKhenThuong.Columns["DINH_CHI"].Visible = false;
                grvKhenThuong.Columns["LAN_CANH_CAO"].Visible = false;
                grvKhenThuong.Columns["VP_TRUOC_DO"].Visible = false;
                grvKhenThuong.Columns["THOI_HAN_DC"].Visible = false;
                grvKhenThuong.Columns["KH_SUA_DOI"].Visible = false;
                grvKhenThuong.Columns["THOI_HAN_SD"].Visible = false;
                grvKhenThuong.Columns["ID_TT"].Visible = false;
                grvKhenThuong.Columns["TAI_LIEU"].Visible = false;
            }
            else
            {
                grdKhenThuong.DataSource = dt;
            }


            if (id != -1)
            {
                int index = dt.Rows.IndexOf(dt.Rows.Find(id));
                grvKhenThuong.FocusedRowHandle = grvKhenThuong.GetRowHandle(index);
            }
        }
        private void GrdKhenThuong_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                DeleteData();
            }

        }



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
            Commons.Modules.bEnabel = !visible;
            grdKhenThuong.Enabled = visible;
            SO_QUYET_DINHTextEdit.Properties.ReadOnly = visible;
            NGAY_HIEU_LUCDateEdit.Properties.ReadOnly = visible;
            NGAY_KYDateEdit.Properties.ReadOnly = visible;
            ID_NKLookUpEdit.Properties.ReadOnly = visible;
            NOI_DUNGTextEdit.Properties.ReadOnly = visible;
            ID_KT_KLLookUpEdit.Properties.ReadOnly = visible;
            LOAI_KTLookUpEdit.Properties.ReadOnly = visible;
            GHI_CHUTextEdit.Properties.ReadOnly = visible;
            txtLAN_CANH_CAO.Properties.ReadOnly = visible;
            txtTHOI_HAN_DC.Properties.ReadOnly = visible;
            txtTHOI_HAN_SD.Properties.ReadOnly = visible;
            txtVP_TRUOC_DO.Properties.ReadOnly = visible;
            txtKH_SUA_DOI.Properties.ReadOnly = visible;
            chkDINH_CHI.Properties.ReadOnly = visible;
            cboTinhTrang.Properties.ReadOnly = visible;

        }
        private void Bindingdata(bool bthem)
        {
            if (bthem == true)
            {
                SO_QUYET_DINHTextEdit.EditValue = "";
                NGAY_HIEU_LUCDateEdit.EditValue = DateTime.Today;
                NGAY_KYDateEdit.EditValue = DateTime.Today;
                txtLAN_CANH_CAO.EditValue = "";
                txtVP_TRUOC_DO.EditValue = "";
                chkDINH_CHI.Checked = false;
                txtTHOI_HAN_DC.EditValue = "";
                txtTHOI_HAN_SD.EditValue = "";
                NOI_DUNGTextEdit.EditValue = "";
                GHI_CHUTextEdit.EditValue = "";
                cboTinhTrang.EditValue = 1;

            }
            else
            {
                SO_QUYET_DINHTextEdit.EditValue = grvKhenThuong.GetFocusedRowCellValue("SO_QUYET_DINH");
                NGAY_HIEU_LUCDateEdit.EditValue = Convert.ToDateTime(grvKhenThuong.GetFocusedRowCellValue("NGAY_HIEU_LUC")).Date == DateTime.MinValue ? NGAY_HIEU_LUCDateEdit.EditValue = null : Convert.ToDateTime(grvKhenThuong.GetFocusedRowCellValue("NGAY_HIEU_LUC")).Date;
                NGAY_KYDateEdit.EditValue = Convert.ToDateTime(grvKhenThuong.GetFocusedRowCellValue("NGAY_KY")).Date == DateTime.MinValue ? NGAY_KYDateEdit.EditValue = null : Convert.ToDateTime(grvKhenThuong.GetFocusedRowCellValue("NGAY_KY")).Date;
                ID_NKLookUpEdit.EditValue = grvKhenThuong.GetFocusedRowCellValue("ID_NK");
                NOI_DUNGTextEdit.EditValue = grvKhenThuong.GetFocusedRowCellValue("NOI_DUNG");
                ID_KT_KLLookUpEdit.EditValue = Convert.ToInt32(grvKhenThuong.GetFocusedRowCellValue("ID_KT_KL"));
                LOAI_KTLookUpEdit.EditValue = Convert.ToInt32(grvKhenThuong.GetFocusedRowCellValue("LOAI_KT"));
                GHI_CHUTextEdit.EditValue = grvKhenThuong.GetFocusedRowCellValue("GHI_CHU");
                txtLAN_CANH_CAO.EditValue = grvKhenThuong.GetFocusedRowCellValue("LAN_CANH_CAO");
                txtVP_TRUOC_DO.EditValue = grvKhenThuong.GetFocusedRowCellValue("VP_TRUOC_DO");
                chkDINH_CHI.EditValue = Convert.ToBoolean(grvKhenThuong.GetFocusedRowCellValue("DINH_CHI"));
                txtTHOI_HAN_DC.EditValue = grvKhenThuong.GetFocusedRowCellValue("THOI_HAN_DC");
                txtTHOI_HAN_SD.EditValue = grvKhenThuong.GetFocusedRowCellValue("THOI_HAN_SD");
                txtKH_SUA_DOI.EditValue = grvKhenThuong.GetFocusedRowCellValue("KH_SUA_DOI");
                //cboTinhTrang.EditValue = string.IsNullOrEmpty(grvKhenThuong.GetFocusedRowCellValue("ID_TT").ToString()) ? cboTinhTrang.EditValue : Convert.ToInt32(grvKhenThuong.GetFocusedRowCellValue("ID_TT"));
                cboTinhTrang.EditValue = (grvKhenThuong.GetFocusedRowCellValue("ID_TT") == null || grvKhenThuong.GetFocusedRowCellValue("ID_TT").ToString().Trim() == "") ? cboTinhTrang.EditValue : Convert.ToInt32(grvKhenThuong.GetFocusedRowCellValue("ID_TT"));
                txtTaiLieu.EditValue = grvKhenThuong.GetFocusedRowCellValue("TAI_LIEU");
            }
        }
        private void SaveData()
        {
            try
            {
                int n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateKhenThuong",
                        grvKhenThuong.GetFocusedRowCellValue("ID_KTHUONG"),
                        idcn,
                        SO_QUYET_DINHTextEdit.EditValue,
                        NGAY_HIEU_LUCDateEdit.EditValue,
                        NGAY_KYDateEdit.EditValue,
                        ID_NKLookUpEdit.EditValue,
                        NOI_DUNGTextEdit.EditValue,
                        ID_KT_KLLookUpEdit.EditValue,
                        LOAI_KTLookUpEdit.EditValue,
                        GHI_CHUTextEdit.EditValue,
                        chkDINH_CHI.EditValue,
                        txtLAN_CANH_CAO.EditValue,
                        txtVP_TRUOC_DO.EditValue,
                        txtTHOI_HAN_DC.EditValue,
                        txtKH_SUA_DOI.EditValue,
                        txtTHOI_HAN_SD.EditValue, cboTinhTrang.EditValue, txtTaiLieu.EditValue,
                          cothem));
                LoadgrdKhenThuong(n);
            }
            catch
            { }
        }
        private void DeleteData()
        {

            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteKhenThuong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.KHEN_THUONG WHERE ID_KTHUONG = " + grvKhenThuong.GetFocusedRowCellValue("ID_KTHUONG") + "");
                grvKhenThuong.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        private void GrvKhenThuong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Bindingdata(false);
        }

        private void windowsUIButton_ButtonClick_1(object sender, ButtonEventArgs e)
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
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        id_kT = -1;
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
                        if (grvKhenThuong.RowCount == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        cothem = false;
                        enableButon(false);
                        break;
                    }

                case "xoa":
                    {
                        if (grvKhenThuong.RowCount == 0)
                        {

                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        Commons.Modules.ObjSystems.Xoahinh(txtTaiLieu.Text);
                        DeleteData();
                        break;
                    }
                case "In":
                    {
                        frmInKhenThuongKyLuatCN InKTKLCN = new frmInKhenThuongKyLuatCN(idcn, "", Convert.ToInt64(grvKhenThuong.GetFocusedRowCellValue("ID_KTHUONG")));
                        InKTKLCN.ShowDialog();
                        break;
                    }
                case "luu":
                    {
                        if (!dxValidationProvider1.Validate()) return;

                        //kiem trung
                        System.Data.SqlClient.SqlConnection conn;
                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                        conn.Open();
                        if (cothem == true)
                        {
                            id_kT = -1;
                        }
                        else { id_kT = Convert.ToInt64(grvKhenThuong.GetFocusedRowCellValue("ID_KTHUONG")); }
                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spkiemtrungKhenThuong", conn);
                        cmd.Parameters.Add("@ID_HD", SqlDbType.BigInt).Value = id_kT;
                        cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = idcn;
                        cmd.Parameters.Add("@SO_HD", SqlDbType.NVarChar).Value = SO_QUYET_DINHTextEdit.Text;
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (Convert.ToInt16(cmd.ExecuteScalar()) == 1)
                        {
                            XtraMessageBox.Show(ItemForSO_QUYET_DINH.Text + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgSoQD_NayDaTonTai"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        Commons.Modules.ObjSystems.ClearValidationProvider(dxValidationProvider1);
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
        private void LayDuongDan()
        {
            string strPath_DH = txtTaiLieu.Text;
            strDuongDan = ofdfile.FileName;

            var strDuongDanTmp = Commons.Modules.ObjSystems.CapnhatTL("Tai_Lieu_KT");
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
                        if ((cboTinhTrang.EditValue == null || cboTinhTrang.EditValue.ToString().Trim() != "2") && btn1.Tag.ToString() == "sua")
                        {
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTinhTrangHopDong"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                            if (ofdfile.ShowDialog() == DialogResult.Cancel) return;
                            LayDuongDan();
                            cboTinhTrang.EditValue = 2;
                        }
                        else
                        {
                            ofdfile.ShowDialog();
                            LayDuongDan();
                        }
                    }
                    else
                    {
                        if (txtTaiLieu.Text == "")
                            return;
                        Commons.Modules.ObjSystems.OpenHinh(txtTaiLieu.Text);
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
                    Commons.Modules.ObjSystems.Xoahinh(txtTaiLieu.Text);
                    txtTaiLieu.ResetText();
                    grvKhenThuong.SetFocusedRowCellValue("TAI_LIEU", null);
                    cboTinhTrang.EditValue = 1;
                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "UPDATE dbo.KHEN_THUONG SET TAI_LIEU = NULL WHERE ID_KTHUONG =" + grvKhenThuong.GetFocusedRowCellValue("ID_KTHUONG") + "");
                }
                catch
                {
                }
            }    
        }
    }
}
