using DevExpress.Map.Dashboard;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace Vs.HRM
{
    public partial class frmGiaDinh : DevExpress.XtraEditors.XtraForm
    {
        bool cothem = false;
        bool flag_CH = false;
        bool flag_NT = false;
        private string tenCN;
        private Int64 idCN;
        public frmGiaDinh(string tencn , Int64 id)
        {
            InitializeComponent();
            lbl_HoTenCN.Text = tencn.ToUpper();
            tenCN = tencn.ToUpper();
            idCN = id;
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, windowsUIButton);
            Commons.OSystems.SetDateEditFormat(NGAY_SINHDateEdit);
        }
        #region sự kiện form
        //sự kiên load form
        private void formGiaDinh_Load(object sender, EventArgs e)
        {

            Commons.Modules.sLoad = "0Load";
            //Commons.Modules.ObjSystems.MLoadLookUpEdit(HO_TENLookupEdit, Commons.Modules.ObjSystems.DataCongNhan(false), "ID_CN", "TEN_CN", "TEN_CN");
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_QHLookUpEdit, Commons.Modules.ObjSystems.DataQHGD(false), "ID_QH", "TEN_QH", "TEN_QH");

            // PHAILookUpEdit
            DataTable dt_Phai = new DataTable();
            dt_Phai.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboPhai", Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboGIOI_TINH, dt_Phai, "ID_PHAI", "PHAI", "PHAI", "");

            //ID_QGLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_QG, Commons.Modules.ObjSystems.DataQuocGia(false), "ID_QG", "TEN_QG", "TEN_QG", "");

            //ID_DTLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_DanToc, Commons.Modules.ObjSystems.DataDanToc(false), "ID_DT", "TEN_DT", "TEN_DT", "");

            //QHCH
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cboQH_CH, Commons.Modules.ObjSystems.DataQHGD(false), "ID_QH", "TEN_QH", "TEN_QH");

            //ID_TPLookUpEdit 
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_TP, Commons.Modules.ObjSystems.DataThanhPho(Convert.ToInt32(-1), false), "ID_TP", "TEN_TP", "TEN_TP", "");

            //ID_QUANLookEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_QUAN, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(-1), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", "");

            //ID_PXLookUpEdit
            Commons.Modules.ObjSystems.MLoadLookUpEditN(cboID_PX, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(-1), false), "ID_PX", "TEN_PX", "TEN_PX", "");
            LoadTT_CH();
            LoadgrdGiaDinh(-1);
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            Commons.Modules.sLoad = "";

        }
        //sự kiện các nút xử lí
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
                        if (grvGiaDinh.RowCount == 0) return;
                        cothem = false;
                        enableButon(false);
                        break;
                    }

                case "xoa":
                    {
                        if (grvGiaDinh.RowCount == 0) return;
                        DeleteData();
                        break;
                    }
                case "luu":
                    {
                        DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
                        conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
                        conditionValidationRule1.ErrorText = "This value is not valid";
                        conditionValidationRule1.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;

                        if (!string.IsNullOrEmpty(txtMS_HGD.Text.Trim()) && !string.IsNullOrEmpty(txtHO_TEN_CH.Text.Trim()))
                        {
                            flag_CH = true;
                        }

                        if (!string.IsNullOrEmpty(HO_TENTextEdit.Text.Trim()) && Convert.ToInt32(ID_QHLookUpEdit.EditValue) > 0)
                        {
                            flag_NT = true;
                        }

                        //if (rdo_CongTy.SelectedIndex == 1)
                        //{
                        //    //       dxValidationProvider1.SetValidationRule(this.HO_TENTextEdit, conditionValidationRule1);
                        //    dxValidationProvider1.RemoveControlError(this.HO_TENTextEdit);
                        //}
                        //else
                        //{
                        //    //     dxValidationProvider1.SetValidationRule(this.HO_TENLookupEdit, conditionValidationRule1);
                        //    dxValidationProvider1.RemoveControlError(this.HO_TENLookupEdit);
                        //}
                        if (!dxValidationProvider1.Validate()) return;
                        SaveData();
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
                        this.Close();
                        break;
                    }
                default:
                    break;
            }
        }
        //sự kiện phím delete
        private void grdGiaDinh_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                DeleteData();
            }
        }
        //sự kiện radio button cùng công ty hay không
        private void rdo_CongTy_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (rdo_CongTy.SelectedIndex == 1)
            //{
            //    ItemForHO_TEN.Visibility = LayoutVisibility.Always;
            //    ItemForHO_TEN_L.Visibility = LayoutVisibility.Never;
            //    ItemForHO_TEN_L.Enabled = true;
            //}
            //else
            //{
            //    ItemForHO_TEN.Visibility = LayoutVisibility.Never;
            //    ItemForHO_TEN_L.Visibility = LayoutVisibility.Always;
            //    ItemForHO_TEN_L.Enabled = true;
            //}
        }

        #endregion

        #region hàm load form
        //hàm load gridview
        private void LoadgrdGiaDinh(int id)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListGiaDinh", Commons.Modules.iCongNhan, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_GD"] };
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdGiaDinh, grvGiaDinh, dt, false, true, true, true, true, this.Name);
            grvGiaDinh.Columns["ID_GD"].Visible = false;
            grvGiaDinh.Columns["ID_CN"].Visible = false;
            grvGiaDinh.Columns["ID_QHGD"].Visible = false;
            grvGiaDinh.Columns["DIA_CHI"].Visible = false;
            grvGiaDinh.Columns["ID_CN_QH"].Visible = false;
            grvGiaDinh.Columns["NGUOI_GH"].Visible = false;
            grvGiaDinh.Columns["MS_BHXH"].Visible = false;
            grvGiaDinh.Columns["SO_CMND"].Visible = false;
            grvGiaDinh.Columns["ID_QG"].Visible = false;
            grvGiaDinh.Columns["ID_DT"].Visible = false;
            grvGiaDinh.Columns["QH_CH"].Visible = false;
            grvGiaDinh.Columns["GIOI_TINH"].Visible = false;

            grvGiaDinh.Columns["NGAY_SINH"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            grvGiaDinh.Columns["NGAY_SINH"].DisplayFormat.FormatString = "dd/MM/yyyy";
            lbl_HoTenCN.Text = tenCN.ToUpper();

            if (id != -1)
            {
                int index = dt.Rows.IndexOf(dt.Rows.Find(id));
                grvGiaDinh.FocusedRowHandle = grvGiaDinh.GetRowHandle(index);
            }
            if (grvGiaDinh.RowCount == 1)
            {
                Bindingdata(false);
            }
        }
        //hàm bingding dữ liệu
        private void Bindingdata(bool bthem)
        {
            if (bthem == true || grvGiaDinh.RowCount == 0)
            {
                //lấy dữ liệu mặc định theo id công nhân
                try
                {
                    NGAY_SINHDateEdit.EditValue = DateTime.Today;
                    NGHE_NGHIEPTextEdit.EditValue = "";
                    DIA_CHITextEdit.EditValue = "";
                    NGUOI_GHCheckEdit.EditValue = false;
                    HO_TENTextEdit.EditValue = "";
                    ID_QHLookUpEdit.EditValue = null;
                    //HO_TENLookupEdit.EditValue = null;
                    txtMS_BHXH.EditValue = "";
                    txtCMND.EditValue = "";
                    cboID_QG.EditValue = null;
                    cboID_DanToc.EditValue = null;
                    cboQH_CH.EditValue = null;
                    txt_GHI_CHU.EditValue = "";
                    cboGIOI_TINH.EditValue = 0;
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
                    ID_QHLookUpEdit.EditValue = Convert.ToInt64(grvGiaDinh.GetFocusedRowCellValue("ID_QHGD"));
                    NGAY_SINHDateEdit.EditValue = grvGiaDinh.GetFocusedRowCellValue("NGAY_SINH");
                    NGHE_NGHIEPTextEdit.EditValue = grvGiaDinh.GetFocusedRowCellValue("NGHE_NGHIEP");
                    DIA_CHITextEdit.EditValue = grvGiaDinh.GetFocusedRowCellValue("DIA_CHI");                  
                    HO_TENTextEdit.EditValue = grvGiaDinh.GetFocusedRowCellValue("HO_TEN");
                    txtMS_BHXH.EditValue = grvGiaDinh.GetFocusedRowCellValue("MS_BHXH");
                    txtCMND.EditValue = grvGiaDinh.GetFocusedRowCellValue("SO_CMND");
                    cboID_QG.EditValue = grvGiaDinh.GetFocusedRowCellValue("ID_QG");
                    cboID_DanToc.EditValue = grvGiaDinh.GetFocusedRowCellValue("ID_DT");
                    cboQH_CH.EditValue = string.IsNullOrEmpty(grvGiaDinh.GetFocusedRowCellValue("QH_CH").ToString()) ? -1 : Convert.ToInt64(grvGiaDinh.GetFocusedRowCellValue("QH_CH"));
                    txt_GHI_CHU.EditValue = grvGiaDinh.GetFocusedRowCellValue("GHI_CHU");
                    cboGIOI_TINH.EditValue = string.IsNullOrEmpty(grvGiaDinh.GetFocusedRowCellValue("GIOI_TINH").ToString()) ? 0 : Convert.ToInt32(grvGiaDinh.GetFocusedRowCellValue("GIOI_TINH"));
                    NGUOI_GHCheckEdit.EditValue = Convert.ToBoolean(grvGiaDinh.GetFocusedRowCellValue("NGUOI_GH"));
                    chkCungCongTy.EditValue = Convert.ToBoolean(grvGiaDinh.GetFocusedRowCellValue("CUNG_CONG_TY"));
                }
                catch { }
            }
        }
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
            windowsUIButton.Buttons[6].Properties.Visible = visible;

            //Thông tin chủ hộ
            txtMS_HGD.Properties.ReadOnly = visible;
            txtHO_TEN_CH.Properties.ReadOnly = visible;
            txtSO_HO_KHAU.Properties.ReadOnly = visible;
            txtDT_LIEN_HE.Properties.ReadOnly = visible;
            txtDIA_CHI_HK.Properties.ReadOnly = visible;
            cboID_TP.Properties.ReadOnly = visible;
            cboID_QUAN.Properties.ReadOnly = visible;
            cboID_PX.Properties.ReadOnly = visible;
            txtTHON_XOM.Properties.ReadOnly = visible;
            chkChuHo.Properties.ReadOnly = visible;
            chkChuHo.Properties.ReadOnly = visible;

            grdGiaDinh.Enabled = visible;
            ID_QHLookUpEdit.Properties.ReadOnly = visible;
            txtMS_BHXH.Properties.ReadOnly = visible;
            txtCMND.Properties.ReadOnly = visible;
            cboID_QG.Properties.ReadOnly = visible;
            cboID_DanToc.Properties.ReadOnly = visible;
            cboQH_CH.Properties.ReadOnly = visible;
            txt_GHI_CHU.Properties.ReadOnly = visible;
            NGAY_SINHDateEdit.Properties.ReadOnly = visible;
            NGHE_NGHIEPTextEdit.Properties.ReadOnly = visible;
            DIA_CHITextEdit.Properties.ReadOnly = visible;
            NGUOI_GHCheckEdit.Properties.ReadOnly = visible;
            //HO_TENLookupEdit.Properties.ReadOnly = visible;
            HO_TENTextEdit.Properties.ReadOnly = visible;
            cboGIOI_TINH.Properties.ReadOnly = visible;
            chkCungCongTy.Properties.ReadOnly= visible;
            txtCN.Properties.ReadOnly = true;



        }
        #endregion

        #region hàm sử lý data
        //hàm sử lý khi lưu dữ liệu(thêm/Sửa)
        private void SaveData()
        {
            try
            {
                int n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateGiaDinh",
              grvGiaDinh.GetFocusedRowCellValue("ID_GD"),
            Commons.Modules.iCongNhan,
            txtMS_HGD.EditValue,
            txtHO_TEN_CH.EditValue,
            txtSO_HO_KHAU.EditValue,
            txtDT_LIEN_HE.EditValue,
            txtDIA_CHI_HK.EditValue,
            cboID_TP.Text.ToString() == "" ? DBNull.Value : cboID_TP.EditValue,
            cboID_QUAN.Text.ToString() == "" ? DBNull.Value : cboID_QUAN.EditValue,
            cboID_PX.Text.ToString() == "" ? DBNull.Value : cboID_PX.EditValue,
            txtTHON_XOM.EditValue,
            ID_QHLookUpEdit.EditValue,
            HO_TENTextEdit.Text,
            NGAY_SINHDateEdit.EditValue,
            //rdo_CongTy.SelectedIndex == 0 ? HO_TENLookupEdit.EditValue : null,
            NGHE_NGHIEPTextEdit.EditValue,
            DIA_CHITextEdit.EditValue,
            NGUOI_GHCheckEdit.EditValue,
            txtMS_BHXH.EditValue,
            txtCMND.EditValue,
            cboID_QG.EditValue,
            cboID_DanToc.EditValue,
            cboQH_CH.EditValue,
            txt_GHI_CHU.EditValue,
            cboGIOI_TINH.EditValue,
            flag_CH,
            flag_NT,
            cothem, chkChuHo.EditValue, chkHoNgheo.EditValue , chkCungCongTy.EditValue
                ));
                LoadgrdGiaDinh(n);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        //hàm xử lý khi xóa dữ liệu
        private void DeleteData()
        {
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteGiaDinh"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.GIA_DINH WHERE ID_GD = " + grvGiaDinh.GetFocusedRowCellValue("ID_GD") + "");
                grvGiaDinh.DeleteSelectedRows();
            }
            catch
            {

            }
        }
        #endregion

        private void HO_TENLookupEdit_EditValueChanged(object sender, EventArgs e)
        {
            //HO_TENTextEdit.Text = HO_TENLookupEdit.Text;
        }

        private void grdGiaDinh_Click(object sender, EventArgs e)
        {

        }

        private void grvGiaDinh_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            bool tmp = cothem;
            cothem = true;
            Bindingdata(false);
            cothem = tmp;
        }

        private void LoadTT_CH()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetTT_ChuHoGD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Commons.Modules.iCongNhan));
                if (dt.Rows.Count == 0) return;
                txtMS_HGD.EditValue = dt.Rows[0]["MS_HGD"].ToString();
                txtHO_TEN_CH.EditValue = dt.Rows[0]["HO_TEN_CH"].ToString();
                txtSO_HO_KHAU.EditValue = dt.Rows[0]["SO_HO_KHAU"].ToString();
                txtDT_LIEN_HE.EditValue = dt.Rows[0]["DT_LIEN_HE"].ToString();
                txtDIA_CHI_HK.EditValue = dt.Rows[0]["DIA_CHI_HK"].ToString();
                cboID_TP.EditValue = dt.Rows[0]["ID_TP"].ToString() == "" ? DBNull.Value : dt.Rows[0]["ID_TP"];
                cboID_QUAN.EditValue = dt.Rows[0]["ID_QUAN"].ToString() == "" ? DBNull.Value : dt.Rows[0]["ID_QUAN"];
                cboID_PX.EditValue = dt.Rows[0]["ID_PX"].ToString() == "" ? DBNull.Value : dt.Rows[0]["ID_PX"];
                txtTHON_XOM.EditValue = dt.Rows[0]["THON_XOM"].ToString();
                chkChuHo.EditValue = dt.Rows[0]["CHU_HO"];
                chkHoNgheo.EditValue = dt.Rows[0]["HO_NGHEO"];
                

            }
            catch (Exception EX) { }
        }

        private void cboID_TP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (cboID_TP.EditValue == null || cboID_TP.EditValue.ToString() == "") return;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cboID_QUAN, Commons.Modules.ObjSystems.DataQuan(Convert.ToInt32(cboID_TP.EditValue), false), "ID_QUAN", "TEN_QUAN", "TEN_QUAN", true);
        }

        private void cboID_QUAN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (cboID_QUAN.EditValue == null || cboID_QUAN.EditValue.ToString() == "") return;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cboID_PX, Commons.Modules.ObjSystems.DataPhuongXa(Convert.ToInt32(cboID_QUAN.EditValue), false), "ID_PX", "TEN_PX", "TEN_PX", true);
        }

        private void chkChuHo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                string strSQL = "SELECT HO+' '+TEN HO_TEN, SO_BHXH, NGAY_SINH, PHAI, SO_CMND, ID_QG, DIA_CHI_THUONG_TRU, ID_QUAN, ID_TP, THON_XOM,ID_PX, DT_DI_DONG FROM dbo.CONG_NHAN WHERE ID_CN =  " + idCN;
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, strSQL));
                txtHO_TEN_CH.Text = Convert.ToString(dt.Rows[0]["HO_TEN"]);
                txtDT_LIEN_HE.Text = Convert.ToString(dt.Rows[0]["DT_DI_DONG"]);
                txtDIA_CHI_HK.Text = Convert.ToString(dt.Rows[0]["DIA_CHI_THUONG_TRU"]);
                cboID_TP.EditValue = Convert.ToInt32(dt.Rows[0]["ID_TP"]);
                cboID_QUAN.EditValue = Convert.ToInt32(dt.Rows[0]["ID_QUAN"]);
                cboID_PX.EditValue = Convert.ToInt32(dt.Rows[0]["ID_PX"]);
                txtTHON_XOM.Text = Convert.ToString(dt.Rows[0]["THON_XOM"]);
            }
            catch { }
        }

        private void txtCN_Validated(object sender, EventArgs e)
        {
            try
            {
                string strSQL = "SELECT HO+' '+TEN HO_TEN, SO_BHXH, NGAY_SINH, PHAI, SO_CMND, ID_QG, DIA_CHI_THUONG_TRU, ID_QUAN, ID_TP, THON_XOM, DT_DI_DONG FROM dbo.CONG_NHAN WHERE MS_CN =  N'" + Convert.ToString(txtCN.Text) + "'";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, strSQL));
                HO_TENTextEdit.Text = Convert.ToString(dt.Rows[0]["HO_TEN"]);
                NGAY_SINHDateEdit.DateTime = Convert.ToDateTime(dt.Rows[0]["NGAY_SINH"]);
                txtMS_BHXH.Text = Convert.ToString(dt.Rows[0]["SO_BHXH"]);
                cboGIOI_TINH.EditValue = Convert.ToInt32(dt.Rows[0]["PHAI"]);
                DIA_CHITextEdit.Text = Convert.ToString(dt.Rows[0]["DIA_CHI_THUONG_TRU"]);
                cboID_QG.EditValue = Convert.ToInt32(dt.Rows[0]["ID_QG"]); 
                cboID_DanToc.EditValue = Convert.ToInt32(dt.Rows[0]["ID_DT"]);
            }
            catch { }
        }

        private void chkCungCongTy_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtCN.Properties.ReadOnly = !Convert.ToBoolean(chkCungCongTy.EditValue);
            }
            catch { }
        }
    }
}