using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vs.Recruit
{
    public partial class frmEditTHONG_BAO_TUYEN_DUNG_VIEW : DevExpress.XtraEditors.XtraForm
    {
        private Int64 iID_TB = -1;
        public Int64 iID_TBTMP = -1;
        private Int64 iID_UV = -1;
        public frmEditTHONG_BAO_TUYEN_DUNG_VIEW(Int64 ID_TB)
        {
            InitializeComponent();
            iID_TB = ID_TB;
        }
        #region even

        private void frmEditTHONG_BAO_TUYEN_DUNG_VIEW_Load(object sender, EventArgs e)
        {
            datNGAY_LAP.EditValue = DateTime.Now;
            datNGAY_BAT_DAU.EditValue = DateTime.Now;
            datHET_HAN_NOP.EditValue = DateTime.Now;
            datNGAY_KET_THUC.EditValue = DateTime.Now.AddDays(7);
            if (iID_TB == -1)
            {
                Loadcbo();
                TaoMa();
            }
            else
            {
                Loadcbo();
                LoadData(iID_TB);
            }
            //enableButon(true);

            iID_TBTMP = iID_TB;
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, windowsUIButton);
        }
        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    //case "them":
                    //    {
                    //        iID_TB = -1;
                    //        iID_UV = 0;
                    //        enableButon(false);
                    //        Bindingdata(true);
                    //        break;
                    //    }
                    //case "sua":
                    //    {
                    //        iID_TB = iID_TBTMP;
                    //        if (iID_TB == -1)
                    //        {
                    //            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //            return;
                    //        }
                    //        enableButon(false);
                    //        break;
                    //    }
                    case "ghi":
                        {
                            if (KiemTrong()) return;
                            if (KiemTrung()) return;
                            if (!dxValidationProvider1.Validate()) return;
                            dxValidationProvider1.Validate();
                            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTBTuyenDung", conn);
                            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 6;
                            cmd.Parameters.Add("@ID_TB", SqlDbType.BigInt).Value = iID_TB;
                            cmd.Parameters.Add("@SO_TB", SqlDbType.NVarChar).Value = txtSO_TB.EditValue;
                            cmd.Parameters.Add("@TIEU_DE", SqlDbType.NVarChar).Value = txtTIEU_DE_TD.EditValue;
                            cmd.Parameters.Add("@NGAY_LAP", SqlDbType.Date).Value = datNGAY_LAP.EditValue;
                            cmd.Parameters.Add("@ID_VT", SqlDbType.BigInt).Value = cboID_VT.EditValue;
                            
                            cmd.Parameters.Add("@ID_TO", SqlDbType.BigInt).Value = cboID_TO.EditValue;
                            cmd.Parameters.Add("@NGUOI_YC", SqlDbType.NVarChar).Value = txtNGUOI_YEU_CAU.EditValue;
                            cmd.Parameters.Add("@NGAY_BAT_DAU", SqlDbType.Date).Value = datNGAY_BAT_DAU.EditValue;
                            cmd.Parameters.Add("@HET_HAN_NOP", SqlDbType.Date).Value = datHET_HAN_NOP.EditValue;
                            cmd.Parameters.Add("@NGAY_KET_THUC", SqlDbType.Date).Value = datNGAY_KET_THUC.EditValue;
                            cmd.Parameters.Add("@TINH_TRANG", SqlDbType.Int).Value = cboTINH_TRANG.EditValue;
                            cmd.Parameters.Add("@NGUOI_LIEN_HE", SqlDbType.NVarChar).Value = txtNGUOI_LIEN_HE.EditValue;
                            cmd.Parameters.Add("@HOT_LINE", SqlDbType.NVarChar).Value = txtHOT_LINE.EditValue;
                            cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar).Value = txtEMAIL.EditValue;
                            cmd.Parameters.Add("@PHONE", SqlDbType.NVarChar).Value = txtPHONE.EditValue;

                            if (cboID_TDVH.Text.Trim() == "")
                                cboID_TDVH.EditValue = null;

                            cmd.Parameters.Add("@ID_TDVH", SqlDbType.BigInt).Value = cboID_TDVH.EditValue;
                            cmd.Parameters.Add("@ID_KNLV", SqlDbType.BigInt).Value = cboID_KNLV.EditValue;
                            cmd.Parameters.Add("@ID_LHCV", SqlDbType.BigInt).Value = cboID_LHCV.EditValue;
                            cmd.Parameters.Add("@NOI_DUNG_CV", SqlDbType.NText).Value = txtNOI_DUNG_CV.EditValue;
                            cmd.Parameters.Add("@YEU_CAU", SqlDbType.NText).Value = txtYEU_CAU.EditValue;
                            cmd.Parameters.Add("@QUYEN_LOI", SqlDbType.NText).Value = txtQUYEN_LOI.EditValue;
                            cmd.Parameters.Add("@THONG_TIN_KHAC", SqlDbType.NText).Value = txtTHONG_TIN_KHAC.EditValue;
                            cmd.Parameters.Add("@MUC_LUONG", SqlDbType.NVarChar).Value = txtMUC_LUONG.EditValue;
                            cmd.Parameters.Add("@GHI_CHU", SqlDbType.NVarChar).Value = txtGHI_CHU.EditValue;
                            cmd.CommandType = CommandType.StoredProcedure;
                            iID_TB = Convert.ToInt64(cmd.ExecuteScalar());
                            iID_TBTMP = iID_TB;
                            if (iID_TB != -1)
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_ThemThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    iID_TB = -1;
                                    Bindingdata(true);
                                    return;
                                }
                            }
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            break;
                        }
                    case "khongghi":
                        {

                            //enableButon(true);
                            if (iID_TBTMP == -1)
                            {
                                Bindingdata(true);
                            }
                            else
                            {
                                Bindingdata(false);
                            }
                            break;
                        }
                    case "thoat":
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            break;
                        }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        #endregion

        #region function
        private void Bindingdata(bool bthem)
        {
            if (bthem == true)
            {
                try
                {
                    TaoMa();
                    datNGAY_LAP.EditValue = DateTime.Now;
                    datNGAY_BAT_DAU.EditValue = DateTime.Now;
                    datNGAY_KET_THUC.EditValue = DateTime.Now.AddDays(7);
                    datHET_HAN_NOP.EditValue = DateTime.Now;
                    txtTIEU_DE_TD.EditValue = String.Empty;
                    txtNGUOI_YEU_CAU.EditValue = String.Empty;
                    txtNGUOI_LIEN_HE.EditValue = String.Empty;
                    txtHOT_LINE.EditValue = String.Empty;
                    txtEMAIL.EditValue = String.Empty;
                    txtPHONE.EditValue = String.Empty;
                    txtNOI_DUNG_CV.EditValue = String.Empty;
                    txtYEU_CAU.EditValue = String.Empty;
                    txtQUYEN_LOI.EditValue = String.Empty;
                    txtTHONG_TIN_KHAC.EditValue = String.Empty;
                    txtMUC_LUONG.EditValue = String.Empty;
                    txtGHI_CHU.EditValue = String.Empty;
                    cboTINH_TRANG.EditValue = 0;
                    cboID_VT.EditValue = 0;
                    cboID_TO.EditValue = 0;
                    cboID_TDVH.EditValue = 0;
                    cboID_KNLV.EditValue = 0;
                    cboID_LHCV.EditValue = 0;
                }
                catch { }
            }
            else
            {
                LoadData(iID_TBTMP);
            }
        }
        private void Loadcbo()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTBTuyenDung", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 3;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                //Load combo TINH_TRANG
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTINH_TRANG, dt, "ID_TT", "TINH_TRANG", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TINH_TRANG"), true, true);
                cboTINH_TRANG.EditValue = 2;
                //Load combo ID_VT
                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[1].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_VT, dt1, "ID_VT", "TEN_VI_TRI", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_VI_TRI"), true, true);

                //Load combo ID_TO
                DataTable dt2 = new DataTable();
                dt2 = ds.Tables[2].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_TO, dt2, "ID_TO", "TEN_TO", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_TO"), true, true);

                //Load combo ID_TDVH
                DataTable dt3 = new DataTable();
                dt3 = ds.Tables[3].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_TDVH, dt3, "ID_TDVH", "TEN_TDVH", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_TDVH"), true, true);

                //Load combo ID_KNLV
                DataTable dt4 = new DataTable();
                dt4 = ds.Tables[4].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_KNLV, dt4, "ID_KNLV", "TEN_KNLV", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_KNLV"), true, true);

                //Load combo ID_LHCV
                DataTable dt5 = new DataTable();
                dt5 = ds.Tables[5].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_LHCV, dt5, "ID_LHCV", "TEN_LHCV", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_LHCV"), true, true);
            }
            catch { }
        }

        private void LoadData(Int64 Id)
        {
            try
            {
                iID_UV = Convert.ToInt64(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT TOP 1 ID_UV FROM dbo.UNG_VIEN_TB_TUYEN_DUNG WHERE ID_TB = " + iID_TB + ""));

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTBTuyenDung", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 4;
                cmd.Parameters.Add("@ID_TB", SqlDbType.BigInt).Value = Id;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();

                txtSO_TB.Text = dt.Rows[0]["SO_TB"].ToString();
                datNGAY_LAP.EditValue = Convert.ToDateTime(dt.Rows[0]["NGAY_LAP"]);
                txtTIEU_DE_TD.Text = dt.Rows[0]["TIEU_DE"].ToString();
                cboTINH_TRANG.EditValue = dt.Rows[0]["TINH_TRANG"].ToString();
                cboID_VT.EditValue = dt.Rows[0]["ID_VT"].ToString();
                cboID_TO.EditValue = dt.Rows[0]["ID_TO"].ToString();
                datNGAY_BAT_DAU.EditValue = Convert.ToDateTime(dt.Rows[0]["NGAY_BAT_DAU"]);
                datHET_HAN_NOP.EditValue = Convert.ToDateTime(dt.Rows[0]["HET_HAN_NOP"]);
                datNGAY_KET_THUC.EditValue = Convert.ToDateTime(dt.Rows[0]["NGAY_KET_THUC"]);
                txtNGUOI_YEU_CAU.Text = dt.Rows[0]["NGUOI_YEU_CAU"].ToString();
                txtNGUOI_LIEN_HE.Text = dt.Rows[0]["NGUOI_LIEN_HE"].ToString();
                txtHOT_LINE.Text = dt.Rows[0]["HOT_LINE"].ToString();
                txtEMAIL.Text = dt.Rows[0]["EMAIL"].ToString();
                txtPHONE.Text = dt.Rows[0]["PHONE"].ToString();
                cboID_TDVH.EditValue = dt.Rows[0]["ID_TDVH"].ToString();
                cboID_KNLV.EditValue = dt.Rows[0]["ID_KNLV"].ToString();
                cboID_LHCV.EditValue = dt.Rows[0]["ID_LHCV"].ToString();
                txtNOI_DUNG_CV.Text = dt.Rows[0]["NOI_DUNG_CV"].ToString();
                txtYEU_CAU.Text = dt.Rows[0]["YEU_CAU"].ToString();
                txtQUYEN_LOI.Text = dt.Rows[0]["QUYEN_LOI"].ToString();
                txtTHONG_TIN_KHAC.Text = dt.Rows[0]["THONG_TIN_KHAC"].ToString();
                txtMUC_LUONG.Text = dt.Rows[0]["MUC_LUONG"].ToString();
                txtGHI_CHU.Text = dt.Rows[0]["GHI_CHU"].ToString();


            }
            catch { }
        }

        private void TaoMa()
        {
            string Ma = "";
            try
            {
                Ma = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "MTaoSoPhieuTD", "TB", "THONG_BAO_TUYEN_DUNG", "SO_TB", Convert.ToDateTime(datNGAY_LAP.EditValue).ToString()).ToString();
            }
            catch { Ma = ""; }
            txtSO_TB.Text = Ma;
        }

        private bool KiemTrung()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTBTuyenDung", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 5;
                cmd.Parameters.Add("@SO_TB", SqlDbType.NVarChar).Value = txtSO_TB.EditValue;
                cmd.Parameters.Add("@ID_TB", SqlDbType.BigInt).Value = iID_TB;
                cmd.CommandType = CommandType.StoredProcedure;
                if (Convert.ToInt16(cmd.ExecuteScalar()) == 1)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrung"));
                    txtSO_TB.Focus();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool KiemTrong()
        {
            try
            {
                if (string.IsNullOrEmpty(txtSO_TB.Text.Trim()))
                {
                    XtraMessageBox.Show(lblSO_TB.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                    txtSO_TB.Focus();
                    return true;
                }

                if (string.IsNullOrEmpty(datNGAY_LAP.EditValue.ToString()))
                {
                    XtraMessageBox.Show(lblNGAY_LAP.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                    datNGAY_LAP.Focus();
                    return true;
                }

                if (string.IsNullOrEmpty(txtTIEU_DE_TD.Text.Trim()))
                {
                    XtraMessageBox.Show(lblTIEU_DE_TD.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                    txtTIEU_DE_TD.Focus();
                    return true;
                }

                if (Convert.ToInt32(cboTINH_TRANG.EditValue) < 1)
                {
                    XtraMessageBox.Show(lblTINH_TRANG.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                    cboTINH_TRANG.Focus();
                    return true;
                }

                if (Convert.ToInt32(cboID_VT.EditValue) < 1)
                {
                    XtraMessageBox.Show(lblID_VT.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                    cboID_VT.Focus();
                    return true;
                }

                if (Convert.ToInt32(cboID_TO.EditValue) < 1)
                {
                    XtraMessageBox.Show(lblID_TO.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                    cboID_TO.Focus();
                    return true;
                }

                if (string.IsNullOrEmpty(datNGAY_BAT_DAU.EditValue.ToString()))
                {
                    XtraMessageBox.Show(lblNGAY_BAT_DAU.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                    datNGAY_BAT_DAU.Focus();
                    return true;
                }

                if (string.IsNullOrEmpty(datNGAY_KET_THUC.EditValue.ToString()))
                {
                    XtraMessageBox.Show(lblNGAY_KET_THUC.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                    datNGAY_KET_THUC.Focus();
                    return true;
                }

                if (string.IsNullOrEmpty(datHET_HAN_NOP.EditValue.ToString()))
                {
                    XtraMessageBox.Show(lblHET_HAN_NOP.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                    datHET_HAN_NOP.Focus();
                    return true;
                }

                if (Convert.ToInt32(cboID_KNLV.EditValue) < 1)
                {
                    XtraMessageBox.Show(lblID_KNLV.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                    cboID_KNLV.Focus();
                    return true;
                }

                if (Convert.ToInt32(cboID_LHCV.EditValue) < 1)
                {
                    XtraMessageBox.Show(lblID_LHCV.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                    cboID_LHCV.Focus();
                    return true;
                }

                if (string.IsNullOrEmpty(txtNOI_DUNG_CV.Text.Trim()))
                {
                    XtraMessageBox.Show(lblNOI_DUNG_CV.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                    txtNOI_DUNG_CV.Focus();
                    return true;
                }

                if (string.IsNullOrEmpty(txtYEU_CAU.Text.Trim()))
                {
                    XtraMessageBox.Show(lblYEU_CAU.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                    txtYEU_CAU.Focus();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;

            txtSO_TB.Properties.ReadOnly = visible;
            datNGAY_LAP.Properties.ReadOnly = visible;
            txtTIEU_DE_TD.Properties.ReadOnly = visible;
            cboTINH_TRANG.Properties.ReadOnly = visible;
            if (iID_UV != 0)
            {
                cboID_VT.ReadOnly = true;
            }
            else
            {
                cboID_VT.Properties.ReadOnly = visible;
            }
            cboID_TO.Properties.ReadOnly = visible;
            datNGAY_BAT_DAU.Properties.ReadOnly = visible;
            datHET_HAN_NOP.Properties.ReadOnly = visible;
            datNGAY_KET_THUC.Properties.ReadOnly = visible;
            txtNGUOI_YEU_CAU.Properties.ReadOnly = visible;
            txtNGUOI_LIEN_HE.Properties.ReadOnly = visible;
            txtHOT_LINE.Properties.ReadOnly = visible;
            txtEMAIL.Properties.ReadOnly = visible;
            txtPHONE.Properties.ReadOnly = visible;
            cboID_TDVH.Properties.ReadOnly = visible;
            cboID_KNLV.Properties.ReadOnly = visible;
            cboID_LHCV.Properties.ReadOnly = visible;
            txtNOI_DUNG_CV.Properties.ReadOnly = visible;
            txtYEU_CAU.Properties.ReadOnly = visible;
            txtQUYEN_LOI.Properties.ReadOnly = visible;
            txtTHONG_TIN_KHAC.Properties.ReadOnly = visible;
            txtMUC_LUONG.Properties.ReadOnly = visible;
            txtGHI_CHU.Properties.ReadOnly = visible;

        }
        #endregion


    }
}
