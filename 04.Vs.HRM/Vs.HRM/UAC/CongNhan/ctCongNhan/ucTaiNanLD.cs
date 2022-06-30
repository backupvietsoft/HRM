using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraLayout;

namespace Vs.HRM
{
    public partial class ucTaiNanLD : DevExpress.XtraEditors.XtraUserControl
    {
        static Int64 idcn = 0;
        bool cothem = false;
        public ucTaiNanLD(Int64 id)
        {
            idcn = id;
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
        }
        #region sự kiện form
        private void ucTaiNanLD_Load(object sender, EventArgs e)
        {
            formatText();
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_MUCDOLookUpEdit, Commons.Modules.ObjSystems.DataMucDoTN(false), "ID_MUCDO", "TEN_MUCDO", "TEN_MUCDO");
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_NGUYEN_NHANLookUpEdit, Commons.Modules.ObjSystems.DataNguyenNhanTN(false), "ID_NGUYEN_NHAN", "TEN_NGUYEN_NHAN", "TEN_NGUYEN_NHAN");
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_GAY_TAI_NANLookUpEdit, Commons.Modules.ObjSystems.DataYeuToTN(false), "ID_GAY_TAI_NAN", "TEN_YEU_TO", "TEN_YEU_TO");
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_NGHE_NGHIEPLookUpEdit, Commons.Modules.ObjSystems.DataNgheNghiep(false), "ID_NGHE_NGHIEP", "TEN_NGHE_NGHIEP", "TEN_NGHE_NGHIEP");
            LoadgrdTaiNan(-1);
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
        }
        private void formatText()
        {
            Commons.OSystems.SetDateEditFormat(NGAY_BI_TAI_NANDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_CAP_CUU_TAI_CHODateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_RA_VIENDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_VAO_VIENDateEdit);

            Commons.OSystems.SetTimeEditFormat(GIO_BI_TAI_NANTimeEdit);
            Commons.OSystems.SetTimeEditFormat(GIO_CAP_CUU_TAI_CHOTimeEdit);


            CP_Y_TETextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            TRA_LUONGTextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            BOI_THUONG_TCTextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            GTRI_TB_THIET_HAITextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            SO_NGAY_NGHITextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeSL.ToString() + "";
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
                        if (Commons.Modules.iCongNhan == -1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        Bindingdata(true);
                        cothem = true;
                        enableButon(false);
                        break;
                    }
                case "sua":
                    {
                        if (grvTaiNan.RowCount == 0)
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
                        if (grvTaiNan.RowCount == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        DeleteData();
                        break;
                    }
                case "luu":
                    {
                        if (!dxValidationProvider1.Validate()) return;
                        //kiem trung tai nan
                        SaveData();
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
        private void grvTanNan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Bindingdata(false);
        }
        #endregion

        #region function Load form
        private void LoadgrdTaiNan(int id)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListTaiNanLD", idcn, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_TNLD"] };
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdTaiNan, grvTaiNan, dt, false, true, true, true, true, this.Name);
            grvTaiNan.Columns["ID_TNLD"].Visible = false;
            grvTaiNan.Columns["ID_CN"].Visible = false;
            grvTaiNan.Columns["ID_NGUYEN_NHAN"].Visible = false;
            grvTaiNan.Columns["ID_GAY_TAI_NAN"].Visible = false;
            grvTaiNan.Columns["ID_NGHE_NGHIEP"].Visible = false;
            grvTaiNan.Columns["GIO_BI_TAI_NAN"].Visible = false;
            grvTaiNan.Columns["TINH_TRANG"].Visible = false;
            grvTaiNan.Columns["NGAY_CAP_CUU_TAI_CHO"].Visible = false;
            grvTaiNan.Columns["GIO_CAP_CUU_TAI_CHO"].Visible = false;
            grvTaiNan.Columns["PHUONG_PHAP_CAP_CUU"].Visible = false;
            grvTaiNan.Columns["SO_NGAY_NGHI"].Visible = false;
            grvTaiNan.Columns["GTRI_TB_THIET_HAI"].Visible = false;
            grvTaiNan.Columns["NGAY_VAO_VIEN"].Visible = false;
            grvTaiNan.Columns["NGAY_RA_VIEN"].Visible = false;
            grvTaiNan.Columns["DIEN_GIAI"].Visible = false;
            grvTaiNan.Columns["TRA_LUONG"].Visible = false;
            grvTaiNan.Columns["BOI_THUONG_TC"].Visible = false;
            grvTaiNan.Columns["CP_Y_TE"].Visible = false;
            grvTaiNan.Columns["GIAI_QUYET"].Visible = false;

            if (id != -1)
            {
                int index = dt.Rows.IndexOf(dt.Rows.Find(id));
                grvTaiNan.FocusedRowHandle = grvTaiNan.GetRowHandle(index);
            }
            Commons.Modules.ObjSystems.AddCombo("ID_MUCDO", "TEN_MUCDO", grvTaiNan, Commons.Modules.ObjSystems.DataMucDoTN(false));
        }
        #endregion

        #region function dung chung
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
            windowsUIButton.Buttons[6].Properties.Visible = visible;

            grdTaiNan.Enabled = visible;

            ID_NGUYEN_NHANLookUpEdit.Properties.ReadOnly = visible;
            ID_GAY_TAI_NANLookUpEdit.Properties.ReadOnly = visible;
            ID_NGHE_NGHIEPLookUpEdit.Properties.ReadOnly = visible;
            ID_MUCDOLookUpEdit.Properties.ReadOnly = visible;

            NGAY_BI_TAI_NANDateEdit.Properties.ReadOnly = visible;
            GIO_BI_TAI_NANTimeEdit.Properties.ReadOnly = visible;
            NOI_XAY_RATextEdit.Properties.ReadOnly = visible;
            TINH_TRANGTextEdit.Properties.ReadOnly = visible;
            GIAI_QUYETTextEdit.Properties.ReadOnly = visible;
            NGAY_CAP_CUU_TAI_CHODateEdit.Properties.ReadOnly = visible;
            GIO_CAP_CUU_TAI_CHOTimeEdit.Properties.ReadOnly = visible;
            PHUONG_PHAP_CAP_CUUTextEdit.Properties.ReadOnly = visible;
            SO_NGAY_NGHITextEdit.Properties.ReadOnly = visible;
            KET_QUA_QDINHTextEdit.Properties.ReadOnly = visible;
            GTRI_TB_THIET_HAITextEdit.Properties.ReadOnly = visible;
            NGAY_VAO_VIENDateEdit.Properties.ReadOnly = visible;
            NGAY_RA_VIENDateEdit.Properties.ReadOnly = visible;
            DIEN_GIAITextEdit.Properties.ReadOnly = visible;
            CP_Y_TETextEdit.Properties.ReadOnly = visible;
            TRA_LUONGTextEdit.Properties.ReadOnly = visible;
            BOI_THUONG_TCTextEdit.Properties.ReadOnly = visible;
        }
        private void Bindingdata(bool bthem)
        {
            if (bthem == true)
            {
                //ID_NGUYEN_NHANLookUpEdit.EditValue = "" ;
                //ID_GAY_TAI_NANLookUpEdit.EditValue = "" ;
                //ID_NGHE_NGHIEPLookUpEdit.EditValue = "" ;
                //MUC_DOLookUpEdit.EditValue = "";

                NGAY_BI_TAI_NANDateEdit.EditValue = DateTime.Today;
                GIO_BI_TAI_NANTimeEdit.EditValue = DateTime.Now;
                NOI_XAY_RATextEdit.EditValue = "";
                TINH_TRANGTextEdit.EditValue = "";
                GIAI_QUYETTextEdit.EditValue = "";
                NGAY_CAP_CUU_TAI_CHODateEdit.EditValue = DateTime.Today;
                GIO_CAP_CUU_TAI_CHOTimeEdit.EditValue = DateTime.Now;
                PHUONG_PHAP_CAP_CUUTextEdit.EditValue = "";
                SO_NGAY_NGHITextEdit.EditValue = "";
                KET_QUA_QDINHTextEdit.EditValue = "";
                GTRI_TB_THIET_HAITextEdit.EditValue = "";
                NGAY_VAO_VIENDateEdit.EditValue = DateTime.Today;
                NGAY_RA_VIENDateEdit.EditValue = DateTime.Today;
                DIEN_GIAITextEdit.EditValue = "";
                CP_Y_TETextEdit.EditValue = "";
                TRA_LUONGTextEdit.EditValue = "";
                BOI_THUONG_TCTextEdit.EditValue = "";
            }
            else
            {
                try
                {
                    ID_NGUYEN_NHANLookUpEdit.EditValue = grvTaiNan.GetFocusedRowCellValue("ID_NGUYEN_NHAN");
                    ID_GAY_TAI_NANLookUpEdit.EditValue = grvTaiNan.GetFocusedRowCellValue("ID_GAY_TAI_NAN");
                    ID_NGHE_NGHIEPLookUpEdit.EditValue = grvTaiNan.GetFocusedRowCellValue("ID_NGHE_NGHIEP");
                    NGAY_BI_TAI_NANDateEdit.EditValue = grvTaiNan.GetFocusedRowCellValue("NGAY_BI_TAI_NAN");
                    NOI_XAY_RATextEdit.EditValue = grvTaiNan.GetFocusedRowCellValue("NOI_XAY_RA");
                    TINH_TRANGTextEdit.EditValue = grvTaiNan.GetFocusedRowCellValue("TINH_TRANG");
                    GIAI_QUYETTextEdit.EditValue = grvTaiNan.GetFocusedRowCellValue("GIAI_QUYET");

                    GIO_BI_TAI_NANTimeEdit.EditValue = grvTaiNan.GetFocusedRowCellValue("GIO_BI_TAI_NAN");
                    NGAY_CAP_CUU_TAI_CHODateEdit.EditValue = grvTaiNan.GetFocusedRowCellValue("NGAY_CAP_CUU_TAI_CHO");
                    GIO_CAP_CUU_TAI_CHOTimeEdit.EditValue = grvTaiNan.GetFocusedRowCellValue("GIO_CAP_CUU_TAI_CHO");
                    NGAY_VAO_VIENDateEdit.EditValue = grvTaiNan.GetFocusedRowCellValue("NGAY_VAO_VIEN");
                    NGAY_RA_VIENDateEdit.EditValue = grvTaiNan.GetFocusedRowCellValue("NGAY_RA_VIEN");

                    PHUONG_PHAP_CAP_CUUTextEdit.EditValue = grvTaiNan.GetFocusedRowCellValue("PHUONG_PHAP_CAP_CUU");
                    SO_NGAY_NGHITextEdit.EditValue = grvTaiNan.GetFocusedRowCellValue("SO_NGAY_NGHI");
                    KET_QUA_QDINHTextEdit.EditValue = grvTaiNan.GetFocusedRowCellValue("KET_QUA_QDINH");
                    ID_MUCDOLookUpEdit.EditValue = Convert.ToInt32(grvTaiNan.GetFocusedRowCellValue("ID_MUCDO"));
                    GTRI_TB_THIET_HAITextEdit.EditValue = grvTaiNan.GetFocusedRowCellValue("GTRI_TB_THIET_HAI");
                    DIEN_GIAITextEdit.EditValue = grvTaiNan.GetFocusedRowCellValue("DIEN_GIAI");
                    CP_Y_TETextEdit.EditValue = grvTaiNan.GetFocusedRowCellValue("CP_Y_TE");
                    TRA_LUONGTextEdit.EditValue = grvTaiNan.GetFocusedRowCellValue("TRA_LUONG");
                    BOI_THUONG_TCTextEdit.EditValue = grvTaiNan.GetFocusedRowCellValue("BOI_THUONG_TC");
                }
                catch (Exception)
                {

                }
            }
        }
        private void SaveData()
        {
            try
            {
                int n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateTaiNanLD",
                grvTaiNan.GetFocusedRowCellValue("ID_TNLD"),
                idcn,
                ID_NGUYEN_NHANLookUpEdit.EditValue,
                ID_GAY_TAI_NANLookUpEdit.EditValue,
                ID_NGHE_NGHIEPLookUpEdit.EditValue,
                NGAY_BI_TAI_NANDateEdit.EditValue,
                GIO_BI_TAI_NANTimeEdit.Time.TimeOfDay,
                NOI_XAY_RATextEdit.EditValue,
                TINH_TRANGTextEdit.EditValue,
                GIAI_QUYETTextEdit.EditValue,
                NGAY_CAP_CUU_TAI_CHODateEdit.EditValue,
                GIO_CAP_CUU_TAI_CHOTimeEdit.Time.TimeOfDay,
                PHUONG_PHAP_CAP_CUUTextEdit.EditValue,
                SO_NGAY_NGHITextEdit.EditValue.ToString() == "" ? 0 : SO_NGAY_NGHITextEdit.EditValue,
                KET_QUA_QDINHTextEdit.EditValue,
                ID_MUCDOLookUpEdit.EditValue,
                GTRI_TB_THIET_HAITextEdit.EditValue.ToString() == "" ? 0 : GTRI_TB_THIET_HAITextEdit.EditValue,
                NGAY_VAO_VIENDateEdit.EditValue,
                NGAY_RA_VIENDateEdit.EditValue,
                DIEN_GIAITextEdit.EditValue,
                CP_Y_TETextEdit.EditValue.ToString() == "" ? 0 : CP_Y_TETextEdit.EditValue,
                TRA_LUONGTextEdit.EditValue.ToString() == "" ? 0 : TRA_LUONGTextEdit.EditValue,
                BOI_THUONG_TCTextEdit.EditValue.ToString() == "" ? 0 : BOI_THUONG_TCTextEdit.EditValue, cothem));
                LoadgrdTaiNan(n);
            }
            catch (Exception ex)
            { }
        }
        private void DeleteData()
        {

            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteTaiNanLaoDong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE	 dbo.TAI_NAN_LD WHERE ID_TNLD = " + grvTaiNan.GetFocusedRowCellValue("ID_TNLD") + "");
                grvTaiNan.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
        private void grdTaiNan_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                DeleteData();
            }
        }
    }
}
