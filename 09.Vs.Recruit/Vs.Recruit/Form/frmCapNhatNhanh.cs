using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frmCapNhatNhanh : DevExpress.XtraEditors.XtraForm
    {
        DataTable dt1 = new DataTable();
        private bool flag = false;
        private int them = 0;
        public frmCapNhatNhanh(DataTable dttemp)
        {
            InitializeComponent();
            dt1 = dttemp;
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, btnALL);
        }


        #region even
        private void frmCapNhatNhanh_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadCombo();
            LoadData(cboTruongCapNhat.EditValue.ToString());
            Commons.Modules.sLoad = "";
            EnabelButton(true);
        }

        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "sua":
                        {
                            them = 1;
                            LoadData(cboTruongCapNhat.EditValue.ToString());
                            EnabelButton(false);
                            break;
                        }
                    case "ghi":
                        {
                            grvDSUngVien.CloseEditor();
                            grvDSUngVien.UpdateCurrentRow();
                            if (grvDSUngVien.RowCount == 0)
                                return;
                            DataTable dt_CHON = new DataTable();
                            dt_CHON = ((DataTable)grdDSUngVien.DataSource);
                            if (dt_CHON.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).Count() == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonUngVien"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            string sBT = "sBTUngVien" + Commons.Modules.iIDUser;
                            try
                            {
                                if (flag == true) return;
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien), "");

                                System.Data.SqlClient.SqlConnection conn;
                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();
                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUngVien", conn);
                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "CAP_NHAT_NHANH";
                                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 3;
                                cmd.Parameters.Add("@sBT1", SqlDbType.NVarChar).Value = sBT;
                                switch (cboTruongCapNhat.EditValue.ToString())
                                {
                                    case "DAO_TAO_DINH_HUONG":
                                        {
                                            cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "NGAY_HOAN_THANH_DT";
                                            break;
                                        }
                                    case "NGAY_HEN_DI_LAM":
                                        {
                                            cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "NGAY_HEN_DI_LAM";
                                            break;
                                        }
                                    case "NGAY_HUY_TD":
                                        {
                                            cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "NGAY_HUY_TD";
                                            break;
                                        }
                                    case "ID_DGTN":
                                        {
                                            cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "ID_DGTN";
                                            break;
                                        }
                                    case "NGAY_CHUYEN":
                                        {
                                            if (KiemSLTuyen() == "") return;
                                            cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "NGAY_CHUYEN";
                                            break;
                                        }
                                    case "ID_LHDLD":
                                        {
                                            cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "ID_LHDLD";
                                            break;
                                        }
                                    case "MUC_LUONG_DN":
                                        {
                                            cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "MUC_LUONG_DN";
                                            break;
                                        }
                                    case "NGAY_NHAN_VIEC":
                                        {
                                            cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "NGAY_NHAN_VIEC";
                                            break;
                                        }
                                }
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.ExecuteNonQuery();
                                them = 0;
                                LoadData(cboTruongCapNhat.EditValue.ToString());
                                EnabelButton(true);
                                Commons.Modules.ObjSystems.XoaTable(sBT);
                            }
                            catch (Exception ex)
                            {
                                Commons.Modules.ObjSystems.XoaTable(sBT);
                            }
                            break;
                        }

                    case "khongghi":
                        {
                            Commons.Modules.sLoad = "0Load";
                            them = 0;
                            LoadData(cboTruongCapNhat.EditValue.ToString());
                            Commons.Modules.sLoad = "";
                            EnabelButton(true);
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
            catch
            {
            }
        }
        #endregion

        #region function
        private void LoadCombo()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUngVien", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "CAP_NHAT_NHANH";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                //DataTable dt1 = new DataTable();
                //dt1 = ds.Tables[1].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTruongCapNhat, dt, "MA_DK", "TEN_DK", "TEN_DK");
                //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboMS_CV, dt1, "MS_CV", "TEN_CV", "TEN_CV");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void EnabelButton(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = !visible;
            btnALL.Buttons[3].Properties.Visible = !visible;
            btnALL.Buttons[4].Properties.Visible = visible;

            grvDSUngVien.OptionsBehavior.Editable = !visible;
            cboTruongCapNhat.Properties.ReadOnly = !visible;
        }
        private void LoadData(string sDieuKien)
        {
            try
            {
                string sBT = "sBTUngVien" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, dt1, "");

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUngVien", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "CAP_NHAT_NHANH";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                cmd.Parameters.Add("@sBT1", SqlDbType.NVarChar).Value = sBT;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.Columns["CHON"].ReadOnly = false;
                dt.Columns["MS_UV"].ReadOnly = true;
                dt.Columns["HO_TEN"].ReadOnly = true;
                dt.Columns["TEN_XN"].ReadOnly = true;
                dt.Columns["TEN_LCV"].ReadOnly = true;
                dt.Columns["MS_CN"].ReadOnly = true;
                dt.Columns["MS_THE_CC"].ReadOnly = true;
                //DataTable dt1 = new DataTable();
                //dt1 = ds.Tables[1].Copy();
                if (grdDSUngVien.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSUngVien, grvDSUngVien, dt, false, false, false, true, true, this.Name);
                    grvDSUngVien.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvDSUngVien.Columns["MS_UV"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvDSUngVien.Columns["HO_TEN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvDSUngVien.Columns["TEN_XN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvDSUngVien.Columns["TEN_LCV"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                    grvDSUngVien.BestFitColumns();
                }
                else
                {
                    grdDSUngVien.DataSource = dt;
                }

                if (them == 0)
                {
                    grvDSUngVien.Columns["CHON"].Visible = false;
                }
                else
                {
                    grvDSUngVien.Columns["CHON"].Visible = true;
                }
                grvDSUngVien.Columns["HOAN_THANH_DT"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["NGAY_HOAN_THANH_DT"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["MUC_LUONG_DN"].DisplayFormat.FormatType = FormatType.Numeric;
                grvDSUngVien.Columns["MUC_LUONG_DN"].DisplayFormat.FormatString = "n0";
                grvDSUngVien.Columns["ID_UV"].Visible = false;
                grvDSUngVien.Columns["NGAY_HEN_DI_LAM"].Visible = false;
                grvDSUngVien.Columns["NGAY_HUY_TD"].Visible = false;
                grvDSUngVien.Columns["NGAY_HOAN_THANH_DT"].Visible = false;
                grvDSUngVien.Columns["ID_DGTN"].Visible = false;
                grvDSUngVien.Columns["NGAY_CHUYEN_NS"].Visible = false;
                grvDSUngVien.Columns["ID_LHDLD"].Visible = false;
                grvDSUngVien.Columns["MUC_LUONG_DN"].Visible = false;
                grvDSUngVien.Columns["NGAY_NHAN_VIEC"].Visible = false;
                grvDSUngVien.Columns["ID_DV"].Visible = false;
                grvDSUngVien.Columns["ID_XN"].Visible = false;
                grvDSUngVien.Columns["ID_TO"].Visible = false;
                grvDSUngVien.Columns["MS_CN"].Visible = false;
                grvDSUngVien.Columns["MS_THE_CC"].Visible = false;

                //DAO TAO DINH HUONG
                grvDSUngVien.Columns["HOAN_THANH_DT"].Visible = false;
                grvDSUngVien.Columns["NGAY_HOAN_THANH_DT"].Visible = false;
                grvDSUngVien.Columns["NQ_LD"].Visible = false;
                grvDSUngVien.Columns["TL_THUONG"].Visible = false;
                grvDSUngVien.Columns["TU_LD"].Visible = false;
                grvDSUngVien.Columns["CS_TC"].Visible = false;
                grvDSUngVien.Columns["GQ_KN"].Visible = false;
                grvDSUngVien.Columns["AT_HC"].Visible = false;
                grvDSUngVien.Columns["SO_CC"].Visible = false;
                grvDSUngVien.Columns["PL_RT"].Visible = false;
                grvDSUngVien.Columns["NQ_PCCC"].Visible = false;
                grvDSUngVien.Columns["NQ_VSATLD"].Visible = false;
                grvDSUngVien.Columns["TN_HL"].Visible = false;
                grvDSUngVien.Columns["ID_NGUOI_DT"].Visible = false;
                grvDSUngVien.Columns["NGAY_DT"].Visible = false;
                grvDSUngVien.Columns["ID_VTTD"].Visible = false;
                grvDSUngVien.Columns["ID_YCTD"].Visible = false;

                switch (sDieuKien)
                {
                    case "DAO_TAO_DINH_HUONG":
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboNguoiDT = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                            cboNguoiDT.NullText = "";
                            cboNguoiDT.ValueMember = "ID_CN";
                            cboNguoiDT.DisplayMember = "HO_TEN";
                            //ID_VTTD,TEN_VTTD
                            //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_NGUOI_DT, Commons.Modules.ObjSystems.TruongBoPhan(), "ID_CN", "HO_TEN", "HO_TEN", true, true);

                            cboNguoiDT.DataSource = Commons.Modules.ObjSystems.TruongBoPhan();
                            cboNguoiDT.Columns.Clear();
                            cboNguoiDT.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_CN"));
                            cboNguoiDT.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HO_TEN"));
                            cboNguoiDT.Columns["HO_TEN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "HO_TEN");
                            cboNguoiDT.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            cboNguoiDT.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                            cboNguoiDT.Columns["ID_CN"].Visible = false;
                            grvDSUngVien.Columns["ID_NGUOI_DT"].ColumnEdit = cboNguoiDT;
                            cboNguoiDT.BeforePopup += cboNguoiDT_BeforePopup;
                            cboNguoiDT.EditValueChanged += cboNguoiDT_EditValueChanged;

                            grvDSUngVien.Columns["NGAY_DT"].Visible = true;
                            grvDSUngVien.Columns["NQ_LD"].Visible = true;
                            grvDSUngVien.Columns["TL_THUONG"].Visible = true;
                            grvDSUngVien.Columns["TU_LD"].Visible = true;
                            grvDSUngVien.Columns["CS_TC"].Visible = true;
                            grvDSUngVien.Columns["GQ_KN"].Visible = true;
                            grvDSUngVien.Columns["AT_HC"].Visible = true;
                            grvDSUngVien.Columns["SO_CC"].Visible = true;
                            grvDSUngVien.Columns["PL_RT"].Visible = true;
                            grvDSUngVien.Columns["NQ_PCCC"].Visible = true;
                            grvDSUngVien.Columns["NQ_VSATLD"].Visible = true;
                            grvDSUngVien.Columns["TN_HL"].Visible = true;
                            grvDSUngVien.Columns["ID_NGUOI_DT"].Visible = true;
                            grvDSUngVien.Columns["HOAN_THANH_DT"].Visible = true;
                            grvDSUngVien.Columns["NGAY_HOAN_THANH_DT"].Visible = true;

                            break;
                        }
                    case "NGAY_HEN_DI_LAM":
                        {
                            grvDSUngVien.Columns["NGAY_HEN_DI_LAM"].Visible = true;
                            break;
                        }
                    case "NGAY_HUY_TD":
                        {
                            grvDSUngVien.Columns["NGAY_HUY_TD"].Visible = true;
                            break;
                        }
                    case "ID_DGTN":
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_DGTN = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                            cboID_DGTN.NullText = "";
                            cboID_DGTN.ValueMember = "ID_DGTN";
                            cboID_DGTN.DisplayMember = "TEN_DGTN";
                            cboID_DGTN.DataSource = Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false);
                            cboID_DGTN.Columns.Clear();
                            cboID_DGTN.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_DGTN"));
                            cboID_DGTN.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_DGTN"));
                            cboID_DGTN.Columns["TEN_DGTN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_DGTN");
                            cboID_DGTN.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            cboID_DGTN.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                            cboID_DGTN.Columns["ID_DGTN"].Visible = false;
                            grvDSUngVien.Columns["ID_DGTN"].ColumnEdit = cboID_DGTN;
                            cboID_DGTN.BeforePopup += cboID_DGTN_BeforePopup;
                            cboID_DGTN.EditValueChanged += cboID_DGTN_EditValueChanged;
                            grvDSUngVien.Columns["ID_DGTN"].Visible = true;
                            break;
                        }
                    case "NGAY_CHUYEN":
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboTo = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                            cboTo.NullText = "";
                            cboTo.ValueMember = "ID_TO";
                            cboTo.DisplayMember = "TEN_TO";
                            //ID_VTTD,TEN_VTTD
                            cboTo.DataSource = Commons.Modules.ObjSystems.DataTo(-1, -1, false);
                            cboTo.Columns.Clear();
                            cboTo.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_TO"));
                            cboTo.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_TO"));
                            cboTo.Columns["TEN_TO"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_TO");
                            cboTo.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            cboTo.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                            cboTo.Columns["ID_TO"].Visible = false;
                            grvDSUngVien.Columns["ID_TO"].ColumnEdit = cboTo;
                            cboTo.BeforePopup += cboTo_BeforePopup;
                            cboTo.EditValueChanged += cboTo_EditValueChanged;

                            grvDSUngVien.Columns["ID_TO"].Visible = true;
                            grvDSUngVien.Columns["NGAY_NHAN_VIEC"].Visible = true;
                            grvDSUngVien.Columns["MS_THE_CC"].Visible = true;
                            grvDSUngVien.Columns["MS_CN"].Visible = true;
                            break;
                        }
                    case "ID_LHDLD":
                        {
                            DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_LHDLD = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                            cboID_LHDLD.NullText = "";
                            cboID_LHDLD.ValueMember = "ID_LHDLD";
                            cboID_LHDLD.DisplayMember = "TEN_LHDLD";
                            cboID_LHDLD.DataSource = Commons.Modules.ObjSystems.DataLoaiHDLD(false);
                            cboID_LHDLD.Columns.Clear();
                            cboID_LHDLD.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_LHDLD"));
                            cboID_LHDLD.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_LHDLD"));
                            cboID_LHDLD.Columns["TEN_LHDLD"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_LHDLD");
                            cboID_LHDLD.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            cboID_LHDLD.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                            cboID_LHDLD.Columns["ID_LHDLD"].Visible = false;
                            grvDSUngVien.Columns["ID_LHDLD"].ColumnEdit = cboID_LHDLD;
                            cboID_LHDLD.BeforePopup += cboID_LHDLD_BeforePopup;
                            cboID_LHDLD.EditValueChanged += cboID_LHDLD_EditValueChanged;

                            grvDSUngVien.Columns["ID_LHDLD"].Visible = true;
                            break;
                        }
                    case "MUC_LUONG_DN":
                        {
                            grvDSUngVien.Columns["MUC_LUONG_DN"].Visible = true;
                            break;
                        }
                    case "NGAY_NHAN_VIEC":
                        {
                            grvDSUngVien.Columns["NGAY_NHAN_VIEC"].Visible = true;
                            break;
                        }
                }
            }
            catch (Exception ex) { }
        }
        // cboID_LHDLD
        private void cboID_LHDLD_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSUngVien.SetFocusedRowCellValue("ID_LHDLD", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboID_LHDLD_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataLoaiHDLD(false);
            }
            catch { }
        }

        // cboID_DGTN
        private void cboID_DGTN_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSUngVien.SetFocusedRowCellValue("ID_DGTN", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboID_DGTN_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false);
            }
            catch { }
        }

        private void cboID_XN_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSUngVien.SetFocusedRowCellValue("ID_XN", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboID_XN_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataXiNghiep(Convert.ToInt32(grvDSUngVien.GetFocusedRowCellValue("ID_DV")), false);
            }
            catch { }
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSUngVien.SetFocusedRowCellValue("ID_TO", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboTo_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(grvDSUngVien.GetFocusedRowCellValue("ID_DV")), Convert.ToInt32(grvDSUngVien.GetFocusedRowCellValue("ID_XN")), false);
            }
            catch { }
        }

        private void cboNguoiDT_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSUngVien.SetFocusedRowCellValue("ID_NGUOI_DT", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboNguoiDT_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.TruongBoPhan();
            }
            catch { }
        }
        #region chuotphai
        class RowInfo
        {
            public RowInfo(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
            {
                this.RowHandle = rowHandle;
                this.View = view;
            }


            public DevExpress.XtraGrid.Views.Grid.GridView View;
            public int RowHandle;
        }
        //Nhap ung vien
        public DXMenuItem MCreateMenuNhapUngVien(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "NhapNhatAll", Commons.Modules.TypeLanguage);
            DXMenuItem menuCapNhat = new DXMenuItem(sStr, new EventHandler(CapNhat));
            menuCapNhat.Tag = new RowInfo(view, rowHandle);
            return menuCapNhat;
        }
        public void CapNhat(object sender, EventArgs e)
        {
            string sCotCN = grvDSUngVien.FocusedColumn.FieldName;
            try
            {
                if (grvDSUngVien.GetFocusedRowCellValue(grvDSUngVien.FocusedColumn.FieldName).ToString() == "") return;
                string sBTUngVien = "sBTUngVien" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTUngVien, Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien), "");

                DataTable dt = new DataTable();
                if (sCotCN.Substring(0, 4) == "NGAY")
                {

                }
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateChuotPhai_TiepNhan", sBTUngVien, sCotCN, sCotCN.Substring(0, 4) == "NGAY" ? Convert.ToDateTime(grvDSUngVien.GetFocusedRowCellValue(grvDSUngVien.FocusedColumn.FieldName)).ToString("MM/dd/yyyy") : grvDSUngVien.GetFocusedRowCellValue(grvDSUngVien.FocusedColumn.FieldName)));
                grdDSUngVien.DataSource = dt;
                Commons.Modules.ObjSystems.XoaTable(sCotCN);
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sCotCN);
            }
        }

        private void grvData_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (btnALL.Buttons[0].Properties.Visible == true) return;
                if (grvDSUngVien.FocusedColumn.FieldName == "CHON") return;
                if (grvDSUngVien.FocusedColumn.FieldName == "MS_UV") return;
                if (grvDSUngVien.FocusedColumn.FieldName == "HO_TEN") return;
                if (grvDSUngVien.FocusedColumn.FieldName == "TEN_XN") return;
                if (grvDSUngVien.FocusedColumn.FieldName == "TEN_LCV") return;
                if (grvDSUngVien.FocusedColumn.FieldName == "MS_CN") return;
                if (grvDSUngVien.FocusedColumn.FieldName == "MS_THE_CC") return;
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();

                    DevExpress.Utils.Menu.DXMenuItem itemNhap = MCreateMenuNhapUngVien(view, irow);
                    e.Menu.Items.Add(itemNhap);
                }
            }
            catch
            {
            }
        }
        #endregion

        #endregion

        private void cboTruongCapNhat_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData(cboTruongCapNhat.EditValue.ToString());
            flag = false;
        }

        private void grvDSUngVien_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvDSUngVien_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvDSUngVien_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;

                if (cboTruongCapNhat.EditValue.ToString() != "NGAY_CHUYEN") return;
                //int ngay = 0;
                DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                DevExpress.XtraGrid.Columns.GridColumn ID_LOAI_HDLD = View.Columns["ID_LHDLD"];
                DevExpress.XtraGrid.Columns.GridColumn idTo = View.Columns["ID_TO"];
                DevExpress.XtraGrid.Columns.GridColumn ngayNhanViec = View.Columns["NGAY_NHAN_VIEC"];
                //DevExpress.XtraGrid.Columns.GridColumn ngayvaolam = View.Columns["NGAY_VAO_LAM_LAI"];
                //if (View.GetRowCellValue(e.RowHandle, mslydovang).ToString() == "")
                //{
                //    e.Valid = false;
                //    View.SetColumnError(mslydovang, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraTenUserNULL", Commons.Modules.TypeLanguage)); return;
                //}
                if (View.GetRowCellValue(e.RowHandle, idTo).ToString() == "")
                {
                    flag = true;
                    e.Valid = false;
                    View.SetColumnError(idTo, "Tổ không được bỏ trống"); return;
                }
                if (View.GetRowCellValue(e.RowHandle, ngayNhanViec).ToString() == "")
                {
                    flag = true;
                    e.Valid = false;
                    View.SetColumnError(ngayNhanViec, "Ngày nhận việc không được bỏ trống"); return;
                }
                if (View.GetRowCellValue(e.RowHandle, ID_LOAI_HDLD).ToString() == "")
                {
                    flag = true;
                    e.Valid = false;
                    XtraMessageBox.Show("Bạn chưa chọn loại hợp đồng cho ứng viên " + View.GetRowCellValue(e.RowHandle, View.Columns["HO_TEN"]).ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                    //View.SetColumnError(ID_LOAI_HDLD, "Bạn chưa chọn loại hợp đồng cho ứng viên " + View.GetRowCellValue(e.RowHandle, View.Columns["HO_TEN"]).ToString()); return;
                }
                //if (View.GetRowCellValue(e.RowHandle, MS_CN).ToString() == "")
                //{
                //    flag = true;
                //    e.Valid = false;
                //    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgMSCNKhongDuocTrong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    View.SetColumnError(MS_CN, "Mã công nhân không được bỏ trống"); return;
                //}
                //if (View.GetRowCellValue(e.RowHandle, MS_THE_CC).ToString() == "")
                //{
                //    flag = true;
                //    e.Valid = false;
                //    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgMTCCKhongDuocTrong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    View.SetColumnError(MS_THE_CC, "Mã số thẻ chấm công không được bỏ trống"); return;
                //}

                //string strSQL = "SELECT COUNT(*) FROM dbo.CONG_NHAN WHERE MS_CN = '" + View.GetRowCellValue(e.RowHandle, MS_CN).ToString().Trim() + "'";
                //int iSL = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));
                //if (iSL > 0)
                //{
                //    flag = true;
                //    e.Valid = false;
                //    View.SetColumnError(MS_CN, "Mã số công nhân này đã có rồi"); return;
                //}

                //strSQL = "SELECT COUNT(*) FROM dbo.CONG_NHAN WHERE MS_THE_CC = '" + View.GetRowCellValue(e.RowHandle, MS_THE_CC).ToString().Trim() + "'";
                //iSL = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));
                //if (iSL > 0)
                //{
                //    flag = true;
                //    e.Valid = false;
                //    View.SetColumnError(MS_THE_CC, "Mã số thẻ chấm công này đã có rồi"); return;
                //}

                flag = false;

                //CheckDuplicateKHNP(grvKHNP, (DataTable)grdKHNP.DataSource, e);
            }
            catch (Exception ex) { }
        }

        private void grvDSUngVien_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (cboTruongCapNhat.EditValue.ToString() != "DAO_TAO_DINH_HUONG") return;
                GridView view = sender as GridView;
                view.SetRowCellValue(e.RowHandle, view.Columns[e.Column.FieldName], e.Value);
                bool NQ_LD = Convert.ToBoolean(grvDSUngVien.GetFocusedRowCellValue("NQ_LD"));
                bool TL_THUONG = Convert.ToBoolean(grvDSUngVien.GetFocusedRowCellValue("TL_THUONG"));
                bool TU_LD = Convert.ToBoolean(grvDSUngVien.GetFocusedRowCellValue("TU_LD"));
                bool CS_TC = Convert.ToBoolean(grvDSUngVien.GetFocusedRowCellValue("CS_TC"));
                bool GQ_KN = Convert.ToBoolean(grvDSUngVien.GetFocusedRowCellValue("GQ_KN"));
                bool AT_HC = Convert.ToBoolean(grvDSUngVien.GetFocusedRowCellValue("AT_HC"));
                bool SO_CC = Convert.ToBoolean(grvDSUngVien.GetFocusedRowCellValue("SO_CC"));
                bool PL_RT = Convert.ToBoolean(grvDSUngVien.GetFocusedRowCellValue("PL_RT"));
                bool NQ_PCCC = Convert.ToBoolean(grvDSUngVien.GetFocusedRowCellValue("NQ_PCCC"));
                bool NQ_VSATLD = Convert.ToBoolean(grvDSUngVien.GetFocusedRowCellValue("NQ_VSATLD"));
                bool TN_HL = Convert.ToBoolean(grvDSUngVien.GetFocusedRowCellValue("TN_HL"));
                if (e.Column.FieldName == "NQ_LD" || e.Column.FieldName == "TL_THUONG" || e.Column.FieldName == "TU_LD" || e.Column.FieldName == "CS_TC" || e.Column.FieldName == "GQ_KN"
                    || e.Column.FieldName == "AT_HC" || e.Column.FieldName == "SO_CC" || e.Column.FieldName == "PL_RT" || e.Column.FieldName == "NQ_PCCC" || e.Column.FieldName == "NQ_VSATLD" || e.Column.FieldName == "TN_HL")
                {
                    if (NQ_LD == true && TL_THUONG == true && TU_LD == true && CS_TC == true && GQ_KN == true && AT_HC == true && SO_CC == true
                        && PL_RT == true && NQ_PCCC == true && NQ_VSATLD == true && TN_HL == true)
                    {
                        view.SetRowCellValue(e.RowHandle, view.Columns["HOAN_THANH_DT"], true);
                        view.SetRowCellValue(e.RowHandle, view.Columns["NGAY_HOAN_THANH_DT"], DateTime.Now);
                    }
                    else
                    {
                        view.SetRowCellValue(e.RowHandle, view.Columns["HOAN_THANH_DT"], false);
                        view.SetRowCellValue(e.RowHandle, view.Columns["NGAY_HOAN_THANH_DT"], null);
                    }
                }
            }
            catch { }
        }
        private string KiemSLTuyen()
        {
            DataTable dt = new DataTable();
            dt = (DataTable)grdDSUngVien.DataSource;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    int Kiem = 0;
                    System.Data.SqlClient.SqlConnection conn;
                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUngVien", conn);
                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "THONG_TIN_TN_UV";
                    cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "CHUYEN_SANG_NS";
                    cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@iCot1", SqlDbType.BigInt).Value = Convert.ToInt64(dt.Rows[i]["ID_YCTD"]);
                    cmd.Parameters.Add("@iCot2", SqlDbType.BigInt).Value = Convert.ToInt64(dt.Rows[i]["ID_VTTD"]);
                    cmd.CommandType = CommandType.StoredProcedure;
                    Kiem = Convert.ToInt32(cmd.ExecuteScalar());
                    if (Kiem == 0)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgSoLuongTuyenDaHet"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return "";
                    }
                    if (Kiem == 2)
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgPhieuDaKhoaBanKhongTheChuyen"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning); return "";
                    }
                }
                catch
                {
                    return "";
                }
            }
            return "1";
        }
    }
}
