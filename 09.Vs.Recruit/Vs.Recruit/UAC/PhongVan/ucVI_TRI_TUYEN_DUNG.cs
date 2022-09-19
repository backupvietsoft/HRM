using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
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
    public partial class ucVI_TRI_TUYEN_DUNG : DevExpress.XtraEditors.XtraUserControl
    {
        Int64 iID_VTTD = -1;
        Int64 iID_VTPV = -1;
        Int64 iID_VTFL = -1;

        Boolean AddEdit = true;  // true la add false la edit

     

        public ucVI_TRI_TUYEN_DUNG(Int64 id_vttd, Int64 id_vtpv, Int64 id_vtfl, Boolean bAddEdit)
        {
            InitializeComponent();
            iID_VTTD = id_vttd;
            iID_VTPV = id_vtpv;
            iID_VTFL = id_vtfl;
            AddEdit = bAddEdit;
        }

        #region even

        private void ucVI_TRI_TUYEN_DUNG_Load(object sender, EventArgs e)
        {
            //xTraTabControl.SelectedTabPage = tabVI_TRI_PHONG_VAN;
            Loadcbo();
            Loadgrv(-1, -1, -1);
            enableButon(true);
            if (!AddEdit) LoadText();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root, layoutControlGroup1, layoutControlGroup2 }, btnALL);
        }
        private void btnALL_ButtonClick(object sender, ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {

                    case "them":
                        {
                            iID_VTTD = -1;
                            iID_VTPV = -1;
                            iID_VTFL = -1;
                           
                            enableTab(true);

                            Bindingdata(true);
                            enableButon(false);
                            break;
                        }
                    case "sua":
                        {
                            if (xTraTabControl.SelectedTabPage == tabVI_TRI_TUYEN_DUNG)
                            {
                                if (grvViTriTD.RowCount == 0)
                                {
                                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            if (xTraTabControl.SelectedTabPage == tabVI_TRI_PHONG_VAN)
                            {
                                if (grvViTriPV.RowCount == 0)
                                {
                                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            if (xTraTabControl.SelectedTabPage == tabVI_TRI_FILE)
                            {
                                if (grvViTriFile.RowCount == 0)
                                {
                                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            enableTab(true);
                            enableButon(false);
                            break;
                        }
                    case "luu":
                        {
                            try
                            {
                                //  if (!dxValidationProvider1.Validate()) return;

                                if (bKiemTrung()) return;
                                bool flag_VI_TRI_TUYEN_DUNG = false;
                                
                                if (!string.IsNullOrEmpty(txtTEN_VI_TRI.Text.Trim()) && Convert.ToInt32(cboID_CV.EditValue) > 1)
                                {
                                    flag_VI_TRI_TUYEN_DUNG = true;
                                }
                                else
                                {
                                    if (xTraTabControl.SelectedTabPage == tabVI_TRI_TUYEN_DUNG)
                                    {
                                        if (string.IsNullOrEmpty(txtTEN_VI_TRI.Text.Trim()))
                                        {
                                            XtraMessageBox.Show(lblTEN_VI_TRI.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                                            txtTEN_VI_TRI.Focus();
                                            return;
                                        }
                                        if (Convert.ToInt32(cboID_CV.EditValue) < 1)
                                        {
                                            XtraMessageBox.Show(lblID_CV.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                                            cboID_CV.Focus();
                                            return;
                                        }
                                    }
                                }

                                bool flag_VI_TRI_PHONG_VAN = false;
                                if (Convert.ToInt32(cboID_VTTD_PV.EditValue) >= 1 && Convert.ToInt32(txtDOT.EditValue) > 0)
                                {
                                    flag_VI_TRI_PHONG_VAN = true;
                                }
                                else
                                {
                                    if (xTraTabControl.SelectedTabPage == tabVI_TRI_PHONG_VAN)
                                    {
                                        if (Convert.ToInt32(cboID_VTTD_PV.EditValue) < 1)
                                        {
                                            XtraMessageBox.Show(lblID_VTTD_PV.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                                            cboID_VTTD_PV.Focus();
                                            return;
                                        }

                                        if (string.IsNullOrEmpty(txtDOT.Text.Trim()))
                                        {
                                            XtraMessageBox.Show(lblDOT.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                                            txtDOT.Focus();
                                            return;
                                        }

                                        if (Convert.ToInt32(txtDOT.EditValue) < 1)
                                        {
                                            XtraMessageBox.Show(lblDOT.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocNhoHon0"));
                                            txtDOT.Focus();
                                            return;
                                        }
                                    }
                                }

                                bool flag_VI_TRI_FILE = false;
                                if (Convert.ToInt32(cboID_VTTD_FL.EditValue) >= 1 && !string.IsNullOrEmpty(txtPATH.Text.Trim()))
                                {
                                    flag_VI_TRI_FILE = true;
                                }
                                else
                                {
                                    if (xTraTabControl.SelectedTabPage == tabVI_TRI_FILE)
                                    {
                                        if (Convert.ToInt32(cboID_VTTD_FL.EditValue) < 1)
                                        {
                                            XtraMessageBox.Show(lblID_VTTD_FL.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                                            cboID_VTTD_FL.Focus();
                                            return;
                                        }

                                        if (string.IsNullOrEmpty(txtPATH.Text.Trim()))
                                        {
                                            XtraMessageBox.Show(lblPATH.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                                            txtPATH.Focus();
                                            return;
                                        }
                                    }
                                }
                                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();
                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spViTriTuyenDung", conn);
                                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 3;
                                //para VI_TRI_TUYEN_DUNG
                                if (flag_VI_TRI_TUYEN_DUNG)
                                {
                                    cmd.Parameters.Add("@Flag_VTTD", SqlDbType.Bit).Value = flag_VI_TRI_TUYEN_DUNG;
                                    cmd.Parameters.Add("@ID_VT", SqlDbType.BigInt).Value = iID_VTTD;
                                    cmd.Parameters.Add("@TEN_VI_TRI", SqlDbType.NVarChar).Value = txtTEN_VI_TRI.EditValue;
                                    cmd.Parameters.Add("@TEN_VI_TRI_A", SqlDbType.NVarChar).Value = txtTEN_VI_TRI_A.EditValue;
                                    cmd.Parameters.Add("@ID_CV", SqlDbType.BigInt).Value = cboID_CV.EditValue;
                                    cmd.Parameters.Add("@ID_NGANH_TD", SqlDbType.BigInt).Value = cboID_NGANH_TD.EditValue;
                                    cmd.Parameters.Add("@ID_TDVH", SqlDbType.BigInt).Value = cboID_TDVH.EditValue;
                                    cmd.Parameters.Add("@ID_KNLV", SqlDbType.BigInt).Value = cboID_KNLV.EditValue;
                                    cmd.Parameters.Add("@ID_LHCV", SqlDbType.BigInt).Value = cboID_LHCV.EditValue;
                                    cmd.Parameters.Add("@NOI_DUNG_CV", SqlDbType.NText).Value = txtNOI_DUNG_CV.EditValue;
                                    cmd.Parameters.Add("@YEU_CAU", SqlDbType.NText).Value = txtYEU_CAU.EditValue;
                                    cmd.Parameters.Add("@QUYEN_LOI", SqlDbType.NText).Value = txtQUYEN_LOI.EditValue;
                                    cmd.Parameters.Add("@THONG_TIN_KHAC", SqlDbType.NText).Value = txtTHONG_TIN_KHAC.EditValue;
                                    cmd.Parameters.Add("@MUC_LUONG", SqlDbType.NVarChar).Value = txtMUC_LUONG.EditValue;
                                    cmd.Parameters.Add("@GHI_CHU", SqlDbType.NVarChar).Value = txtGHI_CHU.EditValue;
                                }



                                //para VI_TRI_PHONG_VAN
                                if (flag_VI_TRI_PHONG_VAN)
                                {
                                    cmd.Parameters.Add("@Flag_VTPV", SqlDbType.Bit).Value = flag_VI_TRI_PHONG_VAN;
                                    cmd.Parameters.Add("@ID_VT_PV", SqlDbType.BigInt).Value = iID_VTPV;
                                    cmd.Parameters.Add("@ID_VTTD_PV", SqlDbType.BigInt).Value = cboID_VTTD_PV.EditValue;
                                    cmd.Parameters.Add("@DOT", SqlDbType.Int).Value = txtDOT.EditValue;
                                    cmd.Parameters.Add("@NOI_DUNG1", SqlDbType.NText).Value = txtNOI_DUNG1.EditValue;
                                    cmd.Parameters.Add("@NOI_DUNG2", SqlDbType.NText).Value = txtNOI_DUNG2.EditValue;
                                    cmd.Parameters.Add("@NOI_DUNG3", SqlDbType.NText).Value = txtNOI_DUNG3.EditValue;
                                    cmd.Parameters.Add("@NOI_DUNG4", SqlDbType.NText).Value = txtNOI_DUNG4.EditValue;
                                    cmd.Parameters.Add("@NOI_DUNG5", SqlDbType.NText).Value = txtNOI_DUNG5.EditValue;
                                }


                                // para VI_TRI_FILE
                                if (flag_VI_TRI_FILE)
                                {
                                    cmd.Parameters.Add("@Flag_VTFL", SqlDbType.Bit).Value = flag_VI_TRI_FILE;
                                    cmd.Parameters.Add("@ID_VT_FL", SqlDbType.BigInt).Value = iID_VTFL;
                                    cmd.Parameters.Add("@ID_VTTD_FL", SqlDbType.BigInt).Value = cboID_VTTD_FL.EditValue;
                                    cmd.Parameters.Add("@PATH", SqlDbType.NVarChar).Value = txtPATH.EditValue;
                                    cmd.Parameters.Add("@MO_TA", SqlDbType.NVarChar).Value = txtMO_TA.EditValue;
                                    cmd.Parameters.Add("@GHI_CHU_FL", SqlDbType.NVarChar).Value = txtGHI_CHU_F.EditValue;
                                }


                                cmd.CommandType = CommandType.StoredProcedure;

                                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                DataSet ds = new DataSet();
                                da.Fill(ds);

                                DataTable dt = new DataTable();
                                dt = ds.Tables[0].Copy();

                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    iID_VTTD = string.IsNullOrEmpty(dt.Rows[0]["ID_VT"].ToString()) ? 0 : Convert.ToInt64(dt.Rows[0]["ID_VT"]);
                                    iID_VTPV = string.IsNullOrEmpty(dt.Rows[0]["ID_VT_PV"].ToString()) ? -1 : Convert.ToInt64(dt.Rows[0]["ID_VT_PV"]);
                                    iID_VTFL = string.IsNullOrEmpty(dt.Rows[0]["ID_VT_FL"].ToString()) ? -1 : Convert.ToInt64(dt.Rows[0]["ID_VT_FL"]);
                                    enableTab(false);
                                }
                                else
                                {
                                    //XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_ThemThanhCong"), "", MessageBoxButtons.OK);
                                    return;
                                }
                                enableButon(true);
                                Bindingdata(false);
                                Loadcbo();
                                Loadgrv(iID_VTTD, iID_VTPV, iID_VTFL);
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message); }
                            break;
                        }
                    case "xoa":
                        {

                            DeleteData();
                            break;
                        }
                    case "khongluu":
                        {
                            enableTab(false);
                            enableButon(true);
                            Bindingdata(false);
                            dxValidationProvider1.Validate();
                            break;
                        }
                    case "thoat":
                        {
                            Commons.Modules.ObjSystems.GotoHome(this);
                            break;
                        }
                    default: break;
                }
            }
            catch (Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());
            }
        }
        private void ucVI_TRI_TUYEN_DUNG_Resize(object sender, EventArgs e) => xTraTabControl.Refresh();
        #endregion

        #region function
        private void Loadgrv(Int64 idtd, Int64 idpv, Int64 idfl)
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spViTriTuyenDung", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 4;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_VT"] };
                if (grdViTriTD.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdViTriTD, grvViTriTD, dt, false, true, true, false, false, this.Name);
                    grvViTriTD.Columns["ID_VT"].Visible = false;
                    grvViTriTD.Columns["ID_CV"].Visible = false;
                    grvViTriTD.Columns["ID_NGANH_TD"].Visible = false;
                    grvViTriTD.Columns["ID_TDVH"].Visible = false;
                    grvViTriTD.Columns["ID_KNLV"].Visible = false;
                    grvViTriTD.Columns["ID_LHCV"].Visible = false;
                }
                else
                {
                    grdViTriTD.DataSource = dt;
                }

                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[1].Copy();
                dt1.PrimaryKey = new DataColumn[] { dt1.Columns["ID_VT_PV"] };

                if (grdViTriPV.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdViTriPV, grvViTriPV, dt1, false, true, true, false, false, this.Name);
                    grvViTriPV.Columns["ID_VT_PV"].Visible = false;
                    grvViTriPV.Columns["ID_VT"].Visible = false;
                }
                else
                {
                    grdViTriPV.DataSource = dt1;
                }

                DataTable dt2 = new DataTable();
                dt2 = ds.Tables[2].Copy();
                dt2.PrimaryKey = new DataColumn[] { dt2.Columns["ID_VT_FL"] };
                if (grdViTriFile.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdViTriFile, grvViTriFile, dt2, false, true, true, false, false, this.Name);
                    grvViTriFile.Columns["ID_VT_FL"].Visible = false;
                    grvViTriFile.Columns["ID_VT"].Visible = false;
                }
                else
                {
                    grdViTriFile.DataSource = dt2;
                }
                if (idtd != -1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(idtd));
                    grvViTriTD.FocusedRowHandle = grvViTriTD.GetRowHandle(index);
                }
                if (idpv != -1)
                {
                    int index = dt1.Rows.IndexOf(dt1.Rows.Find(idpv));
                    grvViTriPV.FocusedRowHandle = grvViTriPV.GetRowHandle(index);
                }
                if (idfl != -1)
                {
                    int index = dt2.Rows.IndexOf(dt2.Rows.Find(idfl));
                    grvViTriFile.FocusedRowHandle = grvViTriFile.GetRowHandle(index);
                }
                grvViTriTD_FocusedRowChanged(null, null);
                grvViTriPV_FocusedRowChanged(null, null);
                grvViTriFile_FocusedRowChanged(null, null);
            }
            catch  { }
        }

        private void Loadcbo()
        {
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spViTriTuyenDung", conn);
            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            cmd.CommandType = CommandType.StoredProcedure;
            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            //Load combo CHUC_VU
            DataTable dt = new DataTable();
            dt = ds.Tables[0].Copy();
            if (cboID_CV.Properties.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_CV, dt, "ID_CV", "TEN_CV", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_CV"));
            }
            else
            {
                cboID_CV.Properties.DataSource = dt;
            }

            //Load cbo NGANH_TD
            DataTable dt1 = new DataTable();
            dt1 = ds.Tables[1].Copy();
            if (cboID_NGANH_TD.Properties.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_NGANH_TD, dt1, "ID_NGANH_TD", "TEN_NGANH_TD", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_NGANH_TD"));
            }
            else
            {
                cboID_NGANH_TD.Properties.DataSource = dt1;
            }
            //Load cbo TRINH_DO_VAN_HOA
            DataTable dt2 = new DataTable();
            dt2 = ds.Tables[2].Copy();
            if (cboID_TDVH.Properties.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_TDVH, dt2, "ID_TDVH", "TEN_TDVH", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_TDVH"));
            }
            else
            {
                cboID_TDVH.Properties.DataSource = dt2;
            }


            //Load cbo KINH_NGHIEM_LV
            DataTable dt3 = new DataTable();
            dt3 = ds.Tables[3].Copy();
            if (cboID_KNLV.Properties.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_KNLV, dt3, "ID_KNLV", "TEN_KNLV", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_KNLV"));
            }
            else
            {
                cboID_KNLV.Properties.DataSource = dt3;
            }

            //Load cbo LHCV
            DataTable dt4 = new DataTable();
            dt4 = ds.Tables[4].Copy();
            if (cboID_LHCV.Properties.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_LHCV, dt4, "ID_LHCV", "TEN_LHCV", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_LHCV"));
            }
            else
            {
                cboID_LHCV.Properties.DataSource = dt4;
            }
            //Load cboID_VTTD_PV, cboID_VTTD_FILE
            DataTable dt5 = new DataTable();
            dt5 = ds.Tables[5].Copy();
            if (cboID_VTTD_PV.Properties.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_VTTD_PV, dt5, "ID_VT", "TEN_VI_TRI", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_VI_TRI"), true, true);
            }
            else
            {
                cboID_VTTD_PV.Properties.DataSource = dt5;
            }
            //Load cboID_VTTD_PV, cboID_VTTD_FILE
            DataTable dt6 = new DataTable();
            dt6 = ds.Tables[5].Copy();
            if (cboID_VTTD_FL.Properties.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_VTTD_FL, dt6, "ID_VT", "TEN_VI_TRI", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_VI_TRI"), true, true);
            }
            else
            {
                cboID_VTTD_FL.Properties.DataSource = dt6;
            }
        }
        private void LoadText()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spViTriTuyenDung", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@ID_VT", SqlDbType.BigInt).Value = grvViTriTD.GetFocusedRowCellValue("ID_VT");
                cmd.Parameters.Add("@ID_VT_PV", SqlDbType.BigInt).Value = grvViTriPV.GetFocusedRowCellValue("ID_VT_PV");
                cmd.Parameters.Add("@ID_VT_FL", SqlDbType.BigInt).Value = grvViTriFile.GetFocusedRowCellValue("ID_VT_FL");
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                try
                {
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0].Copy();
                    txtTEN_VI_TRI.Text = dt.Rows[0]["TEN_VI_TRI"].ToString();
                    txtTEN_VI_TRI_A.Text = dt.Rows[0]["TEN_VI_TRI_A"].ToString();
                    cboID_CV.EditValue = Convert.ToInt64(dt.Rows[0]["ID_CV"]);
                    cboID_NGANH_TD.EditValue = Convert.ToInt64(dt.Rows[0]["ID_NGANH_TD"]);
                    cboID_TDVH.EditValue = Convert.ToInt64(dt.Rows[0]["ID_TDVH"]);
                    cboID_KNLV.EditValue = Convert.ToInt64(dt.Rows[0]["ID_KNLV"]);
                    cboID_LHCV.EditValue = Convert.ToInt64(dt.Rows[0]["ID_LHCV"]);
                    txtNOI_DUNG_CV.Text = dt.Rows[0]["NOI_DUNG_CV"].ToString();
                    txtYEU_CAU.Text = dt.Rows[0]["YEU_CAU"].ToString();
                    txtQUYEN_LOI.Text = dt.Rows[0]["QUYEN_LOI"].ToString();
                    txtTHONG_TIN_KHAC.Text = dt.Rows[0]["THONG_TIN_KHAC"].ToString();
                    txtMUC_LUONG.Text = dt.Rows[0]["MUC_LUONG"].ToString();
                    txtGHI_CHU.Text = dt.Rows[0]["GHI_CHU"].ToString();
                }
                catch { }

                try
                {
                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables[1].Copy();
                    cboID_VTTD_PV.EditValue = Convert.ToInt64(dt1.Rows[0]["ID_VT"]);
                    txtDOT.EditValue = Convert.ToInt32(dt1.Rows[0]["DOT"]);
                    txtNOI_DUNG1.Text = dt1.Rows[0]["NOI_DUNG1"].ToString();
                    txtNOI_DUNG2.Text = dt1.Rows[0]["NOI_DUNG2"].ToString();
                    txtNOI_DUNG3.Text = dt1.Rows[0]["NOI_DUNG3"].ToString();
                    txtNOI_DUNG4.Text = dt1.Rows[0]["NOI_DUNG4"].ToString();
                    txtNOI_DUNG5.Text = dt1.Rows[0]["NOI_DUNG5"].ToString();
                }
                catch { }

                try
                {
                    DataTable dt2 = new DataTable();
                    dt2 = ds.Tables[2].Copy();
                    cboID_VTTD_FL.EditValue = Convert.ToInt64(dt2.Rows[0]["ID_VT"]);
                    txtPATH.Text = dt2.Rows[0]["PATH"].ToString();
                    txtMO_TA.Text = dt2.Rows[0]["MO_TA"].ToString();
                    txtGHI_CHU.Text = dt2.Rows[0]["GHI_CHU"].ToString();
                }
                catch { }

            }
            catch (Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());
            }
        }
        private void LoadTextNull()
        {
            try
            {
                txtTEN_VI_TRI.Text = string.Empty;
                txtTEN_VI_TRI_A.Text = string.Empty;
                cboID_CV.EditValue = 0;
                cboID_KNLV.EditValue = 0;
                cboID_LHCV.EditValue = 0;
                cboID_NGANH_TD.EditValue = 0;
                cboID_TDVH.EditValue = 0;
                cboID_VTTD_FL.EditValue = 0;
                cboID_VTTD_PV.EditValue = 0;
                txtNOI_DUNG_CV.Text = string.Empty;
                txtYEU_CAU.Text = string.Empty;
                txtQUYEN_LOI.Text = string.Empty;
                txtTHONG_TIN_KHAC.Text = string.Empty;
                txtMUC_LUONG.Text = string.Empty;
                txtGHI_CHU.Text = string.Empty;
                txtDOT.EditValue = 0;
                txtNOI_DUNG1.Text = string.Empty;
                txtNOI_DUNG2.Text = string.Empty;
                txtNOI_DUNG3.Text = string.Empty;
                txtNOI_DUNG4.Text = string.Empty;
                txtNOI_DUNG5.Text = string.Empty;
                txtPATH.Text = string.Empty;
                txtMO_TA.Text = string.Empty;
                txtGHI_CHU_F.Text = string.Empty;
            }
            catch { }
        }

        private bool bKiemTrung()
        {
            //try
            //{
            //    DataTable dtTmp = new DataTable();
            //    Int16 iKiem = 0;

            //    iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_KNLV",
            //        (AddEdit ? "-1" : Id.ToString()), "KINH_NGHIEM_LV", "TEN_KNLV", TEN_KNLVTextEdit.EditValue.ToString(),
            //        "", "", "", ""));
            //    if (iKiem > 0)
            //    {
            //        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
            //        TEN_KNLVTextEdit.Focus();
            //        return true;
            //    }

            //    iKiem = 0;

            //    if (!string.IsNullOrEmpty(TEN_KNLV_ATextEdit.Text))
            //    {
            //        iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_KNLV",
            //            (AddEdit ? "-1" : Id.ToString()), "KINH_NGHIEM_LV", "TEN_KNLV_A", TEN_KNLV_ATextEdit.EditValue.ToString(),
            //            "", "", "", ""));
            //        if (iKiem > 0)
            //        {
            //            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
            //            TEN_KNLV_ATextEdit.Focus();
            //            return true;
            //        }
            //    }

            //    iKiem = 0;
            //    if (!string.IsNullOrEmpty(TEN_KNLV_HTextEdit.Text))
            //    {
            //        iKiem = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spCheckData", "ID_KNLV",
            //            (AddEdit ? "-1" : Id.ToString()), "KINH_NGHIEM_LV", "TEN_KNLV_H", TEN_KNLV_HTextEdit.EditValue.ToString(),
            //            "", "", "", ""));
            //        if (iKiem > 0)
            //        {
            //            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TenTrung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"));
            //            TEN_KNLV_HTextEdit.Focus();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message.ToString());
            //    return true;
            //}
            return false;
        }
        private void enableButon(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = visible;
            btnALL.Buttons[3].Properties.Visible = !visible;
            btnALL.Buttons[4].Properties.Visible = !visible;
            btnALL.Buttons[5].Properties.Visible = !visible;
            btnALL.Buttons[6].Properties.Visible = visible;

            grdViTriTD.Enabled = visible;
            txtTEN_VI_TRI.Properties.ReadOnly = visible;
            txtTEN_VI_TRI_A.Properties.ReadOnly = visible;
            cboID_CV.Properties.ReadOnly = visible;
            cboID_NGANH_TD.Properties.ReadOnly = visible;
            cboID_TDVH.Properties.ReadOnly = visible;
            cboID_KNLV.Properties.ReadOnly = visible;
            cboID_LHCV.Properties.ReadOnly = visible;
            txtNOI_DUNG_CV.Properties.ReadOnly = visible;
            txtYEU_CAU.Properties.ReadOnly = visible;
            txtQUYEN_LOI.Properties.ReadOnly = visible;
            txtTHONG_TIN_KHAC.Properties.ReadOnly = visible;
            txtMUC_LUONG.Properties.ReadOnly = visible;
            txtGHI_CHU.Properties.ReadOnly = visible;

            grdViTriPV.Enabled = visible;
            cboID_VTTD_PV.Properties.ReadOnly = visible;
            txtDOT.Properties.ReadOnly = visible;
            txtNOI_DUNG1.Properties.ReadOnly = visible;
            txtNOI_DUNG2.Properties.ReadOnly = visible;
            txtNOI_DUNG3.Properties.ReadOnly = visible;
            txtNOI_DUNG4.Properties.ReadOnly = visible;
            txtNOI_DUNG5.Properties.ReadOnly = visible;


            grdViTriFile.Enabled = visible;
            cboID_VTTD_FL.Properties.ReadOnly = visible;
            txtPATH.Properties.ReadOnly = visible;
            txtMO_TA.Properties.ReadOnly = visible;
            txtGHI_CHU_F.Properties.ReadOnly = visible;






        }
        private void Bindingdata(bool bthem)
        {
            if (bthem == true)
            {

                txtTEN_VI_TRI.EditValue = "";
                txtTEN_VI_TRI_A.EditValue = "";
                cboID_CV.EditValue = 1;
                cboID_TDVH.EditValue = 1;
                cboID_KNLV.EditValue = 1;
                cboID_LHCV.EditValue = 1;
                txtNOI_DUNG_CV.EditValue = "";
                txtYEU_CAU.EditValue = "";
                txtQUYEN_LOI.EditValue = "";
                txtTHONG_TIN_KHAC.EditValue = "";
                txtMUC_LUONG.EditValue = "";
                txtGHI_CHU.EditValue = "";

                cboID_VTTD_PV.EditValue = -99;
                txtDOT.EditValue = 0;
                txtNOI_DUNG1.EditValue = "";
                txtNOI_DUNG2.EditValue = "";
                txtNOI_DUNG3.EditValue = "";
                txtNOI_DUNG4.EditValue = "";
                txtNOI_DUNG5.EditValue = "";

                cboID_VTTD_FL.EditValue = -99;
                txtPATH.EditValue = "";
                txtMO_TA.EditValue = "";
                txtGHI_CHU_F.EditValue = "";

            }
            else
            {
                // LoadText();

                txtTEN_VI_TRI.EditValue = grvViTriTD.GetFocusedRowCellValue("TEN_VI_TRI");
                txtTEN_VI_TRI_A.EditValue = grvViTriTD.GetFocusedRowCellValue("TEN_VI_TRI_A");
                cboID_CV.EditValue = grvViTriTD.GetFocusedRowCellValue("ID_CV");
                cboID_TDVH.EditValue = grvViTriTD.GetFocusedRowCellValue("ID_TDVH");
                cboID_KNLV.EditValue = grvViTriTD.GetFocusedRowCellValue("ID_KNLV");
                cboID_LHCV.EditValue = grvViTriTD.GetFocusedRowCellValue("ID_LHCV");
                txtNOI_DUNG_CV.EditValue = grvViTriTD.GetFocusedRowCellValue("NOI_DUNG_CV");
                txtYEU_CAU.EditValue = grvViTriTD.GetFocusedRowCellValue("YEU_CAU");
                txtQUYEN_LOI.EditValue = grvViTriTD.GetFocusedRowCellValue("QUYEN_LOI");
                txtTHONG_TIN_KHAC.EditValue = grvViTriTD.GetFocusedRowCellValue("THONG_TIN_KHAC");
                txtMUC_LUONG.EditValue = grvViTriTD.GetFocusedRowCellValue("MUC_LUONG");
                txtGHI_CHU.EditValue = grvViTriTD.GetFocusedRowCellValue("GHI_CHU");

                cboID_VTTD_PV.EditValue = grvViTriPV.GetFocusedRowCellValue("ID_VT");
                txtDOT.EditValue = grvViTriPV.GetFocusedRowCellValue("DOT");
                txtNOI_DUNG1.EditValue = grvViTriPV.GetFocusedRowCellValue("NOI_DUNG1");
                txtNOI_DUNG2.EditValue = grvViTriPV.GetFocusedRowCellValue("NOI_DUNG2");
                txtNOI_DUNG3.EditValue = grvViTriPV.GetFocusedRowCellValue("NOI_DUNG3");
                txtNOI_DUNG4.EditValue = grvViTriPV.GetFocusedRowCellValue("NOI_DUNG4");
                txtNOI_DUNG5.EditValue = grvViTriPV.GetFocusedRowCellValue("NOI_DUNG5");

                cboID_VTTD_FL.EditValue = grvViTriFile.GetFocusedRowCellValue("ID_VT");
                txtPATH.EditValue = grvViTriFile.GetFocusedRowCellValue("PATH");
                txtMO_TA.EditValue = grvViTriFile.GetFocusedRowCellValue("MO_TA");
                txtGHI_CHU_F.EditValue = grvViTriFile.GetFocusedRowCellValue("GHI_CHU");

            }
        }

        private void LoadgrvViTriTD()
        {
            try
            {
                txtTEN_VI_TRI.EditValue = grvViTriTD.GetFocusedRowCellValue("TEN_VI_TRI");
                txtTEN_VI_TRI_A.EditValue = grvViTriTD.GetFocusedRowCellValue("TEN_VI_TRI_A");
                cboID_CV.EditValue = grvViTriTD.GetFocusedRowCellValue("ID_CV");
                cboID_TDVH.EditValue = grvViTriTD.GetFocusedRowCellValue("ID_TDVH");
                cboID_KNLV.EditValue = grvViTriTD.GetFocusedRowCellValue("ID_KNLV");
                cboID_LHCV.EditValue = grvViTriTD.GetFocusedRowCellValue("ID_LHCV");
                txtNOI_DUNG_CV.EditValue = grvViTriTD.GetFocusedRowCellValue("NOI_DUNG_CV");
                txtYEU_CAU.EditValue = grvViTriTD.GetFocusedRowCellValue("YEU_CAU");
                txtQUYEN_LOI.EditValue = grvViTriTD.GetFocusedRowCellValue("QUYEN_LOI");
                txtTHONG_TIN_KHAC.EditValue = grvViTriTD.GetFocusedRowCellValue("THONG_TIN_KHAC");
                txtMUC_LUONG.EditValue = grvViTriTD.GetFocusedRowCellValue("MUC_LUONG");
                txtGHI_CHU.EditValue = grvViTriTD.GetFocusedRowCellValue("GHI_CHU");
            }
            catch { }
        }

        private void LoadgrvViTriPV()
        {
            try
            {
                cboID_VTTD_PV.EditValue = grvViTriPV.GetFocusedRowCellValue("ID_VT");
                txtDOT.EditValue = grvViTriPV.GetFocusedRowCellValue("DOT");
                txtNOI_DUNG1.EditValue = grvViTriPV.GetFocusedRowCellValue("NOI_DUNG1");
                txtNOI_DUNG2.EditValue = grvViTriPV.GetFocusedRowCellValue("NOI_DUNG2");
                txtNOI_DUNG3.EditValue = grvViTriPV.GetFocusedRowCellValue("NOI_DUNG3");
                txtNOI_DUNG4.EditValue = grvViTriPV.GetFocusedRowCellValue("NOI_DUNG4");
                txtNOI_DUNG5.EditValue = grvViTriPV.GetFocusedRowCellValue("NOI_DUNG5");
            }
            catch { }
        }
        private void LoadgrvViTriFile()
        {
            try
            {
                cboID_VTTD_FL.EditValue = grvViTriFile.GetFocusedRowCellValue("ID_VT");
                txtPATH.EditValue = grvViTriFile.GetFocusedRowCellValue("PATH");
                txtMO_TA.EditValue = grvViTriFile.GetFocusedRowCellValue("MO_TA");
                txtGHI_CHU_F.EditValue = grvViTriFile.GetFocusedRowCellValue("GHI_CHU");
            }
            catch { }
        }
        private void enableTab(bool ena)
        {
            if (ena == true)
            {
                if (xTraTabControl.SelectedTabPage == tabVI_TRI_TUYEN_DUNG)
                {
                    tabVI_TRI_PHONG_VAN.PageEnabled = false;
                    tabVI_TRI_FILE.PageEnabled = false;
                }
                if (xTraTabControl.SelectedTabPage == tabVI_TRI_PHONG_VAN)
                {
                    tabVI_TRI_TUYEN_DUNG.PageEnabled = false;
                }
                if (xTraTabControl.SelectedTabPage == tabVI_TRI_FILE)
                {
                    tabVI_TRI_TUYEN_DUNG.PageEnabled = false;
                }
            }
            else
            {
                tabVI_TRI_TUYEN_DUNG.PageEnabled = true;
                tabVI_TRI_PHONG_VAN.PageEnabled = true;
                tabVI_TRI_FILE.PageEnabled = true;
            }
        }

        private void DeleteData()
        {
            //xóa
            try
            {
                if (xTraTabControl.SelectedTabPage == tabVI_TRI_TUYEN_DUNG)
                {
                    if (grvViTriTD.RowCount == 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msg_Xoa"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeXoa"), MessageBoxButtons.YesNo) == DialogResult.No) return;
                    //if (Convert.ToInt64(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT TOP 1 * FROM dbo.VI_TRI_PHONG_VAN WHERE ID_VT = " + grvViTriTD.GetFocusedRowCellValue("ID_VT") + "")) != 0)
                    //{
                    //    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDelDangSuDung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}
                    //if (Convert.ToInt64(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT TOP 1 * FROM dbo.VI_TRI_FILE WHERE ID_VT = " + grvViTriTD.GetFocusedRowCellValue("ID_VT") + "")) != 0)
                    //{
                    //    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDelDangSuDung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}
                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE	dbo.TIEP_NHAN_TUYEN_DUNG WHERE ID_VT = " + grvViTriTD.GetFocusedRowCellValue("ID_VT") + "");
                    grvViTriTD.DeleteSelectedRows();
                }
                if (xTraTabControl.SelectedTabPage == tabVI_TRI_PHONG_VAN)
                {
                    if (grvViTriPV.RowCount == 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msg_Xoa"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeXoa"), MessageBoxButtons.YesNo) == DialogResult.No) return;
                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE	dbo.VI_TRI_PHONG_VAN WHERE ID_VT_PV = " + grvViTriPV.GetFocusedRowCellValue("ID_VT_PV") + "");
                    grvViTriPV.DeleteSelectedRows();
                }
                if (xTraTabControl.SelectedTabPage == tabVI_TRI_FILE)
                {
                    if (grvViTriFile.RowCount == 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msg_Xoa"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeXoa"), MessageBoxButtons.YesNo) == DialogResult.No) return;
                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE	dbo.VI_TRI_FILE WHERE ID_VT_FL = " + grvViTriFile.GetFocusedRowCellValue("ID_VT_FL") + "");
                    grvViTriFile.DeleteSelectedRows();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        private void grvViTriTD_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadgrvViTriTD();
            iID_VTTD = Convert.ToInt64(grvViTriTD.GetFocusedRowCellValue("ID_VT"));
            //Bindingdata(false);
        }
        private void grvViTriPV_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadgrvViTriPV();
            iID_VTPV = Convert.ToInt64(grvViTriPV.GetFocusedRowCellValue("ID_VT_PV"));
            //Bindingdata(false);
        }
        private void grvViTriFile_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadgrvViTriFile();
            iID_VTFL = Convert.ToInt64(grvViTriFile.GetFocusedRowCellValue("ID_VT_FL"));
            //Bindingdata(false);
        }

        private void grdViTriTD_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                DeleteData();
            }
        }

        private void grdViTriPV_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                DeleteData();
            }
        }

        private void grdViTriFile_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                DeleteData();
            }
        }
    }
}
