using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Vs.Recruit
{
    public partial class ucDanhGiaTayNghe : DevExpress.XtraEditors.XtraUserControl
    {
        private bool flag = false;
        public AccordionControl accorMenuleft;
        private long iID_UV = -1;
        private ucCTQLUV ucUV;
        private HRM.ucCTQLNS ucNS;
        private int iLoai = 0; // 1 : link ứng viên, 2 : link nhân sự
        private int iAdd = 0;
        private int LoaiCN = 0;
        private DataTable tbDat;
        public ucDanhGiaTayNghe()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, btnALL);
        }
        private void ucDanhGiaTayNghe_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                tbDat = new DataTable();
                tbDat.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT 0 AS ID_DAT,CASE 0 WHEN 0 then N'Không đạt' ELSE 'not achieved' END DAT UNION SELECT 1 AS ID_DAT, CASE 0 WHEN 0 then N'Đạt' ELSE 'Achieved' END DAT"));
                Commons.OSystems.SetDateEditFormat(datTNgay);
                Commons.OSystems.SetDateEditFormat(datDNgay);
                datTNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year));
                int t = DateTime.DaysInMonth(datTNgay.DateTime.Year, datTNgay.DateTime.Month);
                DateTime secondDateTime = new DateTime(datTNgay.DateTime.Year, datTNgay.DateTime.Month, t);
                datDNgay.EditValue = secondDateTime;
                //datTNgay.EditValue = DateTime.Now;
                LoadCbo();
                Commons.Modules.sLoad = "";
                cboID_VTTD_EditValueChanged(null, null);
                EnabelButton(true);
            }
            catch
            {
            }
        }
        private void LoadData()
        {
            try
            {
                if (LoaiCN == 1)
                {//may
                    if (Convert.ToInt32(cboTayNghe.EditValue) == 1)
                    {
                        ////đào tạo
                        grvDSUngVien.Name = "grvDSUngVienDT";
                    }
                    else
                    {
                        grvDSUngVien.Name = "grvDSUngVienTN";
                    }
                }
                else
                {
                    grvDSUngVien.Name = "grvDSUngVienKH";
                }
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spKiemTraTayNghe", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@dCot1", SqlDbType.DateTime).Value = Convert.ToDateTime(datTNgay.EditValue);
                cmd.Parameters.Add("@dCot2", SqlDbType.DateTime).Value = Convert.ToDateTime(datDNgay.EditValue);
                cmd.Parameters.Add("@iCot1", SqlDbType.BigInt).Value = cboYCTD.EditValue;
                cmd.Parameters.Add("@iCot2", SqlDbType.BigInt).Value = cboID_VTTD.EditValue;
                cmd.Parameters.Add("@iCot3", SqlDbType.BigInt).Value = cboTayNghe.Visible == false ? -1 : cboTayNghe.EditValue;
                cmd.Parameters.Add("@bCot1", SqlDbType.Bit).Value = iAdd;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSUngVien, grvDSUngVien, dt, false, true, false, true, true, this.Name);
                grvDSUngVien.Columns["ID_UV"].Visible = false;
                grvDSUngVien.Columns["TINH_TRANG_DG"].Visible = false;
                grvDSUngVien.Columns["MS_UV"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["TEN_TT_UV"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["HO_TEN_NGT"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["THUONG_TAY_NGHE"].DisplayFormat.FormatType = FormatType.Numeric;
                grvDSUngVien.Columns["THUONG_TAY_NGHE"].DisplayFormat.FormatString = "n0";

                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboDGTN = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                cboDGTN.NullText = "";
                cboDGTN.ValueMember = "ID_DGTN";
                cboDGTN.DisplayMember = "TEN_DGTN";
                //ID_VTTD,TEN_VTTD
                cboDGTN.DataSource = Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false);
                cboDGTN.Columns.Clear();
                cboDGTN.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_DGTN"));
                cboDGTN.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_DGTN"));
                cboDGTN.Columns["TEN_DGTN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_DGTN");
                cboDGTN.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboDGTN.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cboDGTN.Columns["ID_DGTN"].Visible = false;
                grvDSUngVien.Columns["ID_DGTN"].ColumnEdit = cboDGTN;
                cboDGTN.BeforePopup += cboDGTN_BeforePopup;
                cboDGTN.EditValueChanged += cboDGTN_EditValueChanged;
                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboNDGTN1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                cboNDGTN1.NullText = "";
                cboNDGTN1.ValueMember = "ID_NGUOI_DGTN";
                cboNDGTN1.DisplayMember = "TEN_NGUOI_DGTN";
                //ID_NGUOI_DGTN,TEN_NGUOI_DGTN
                cboNDGTN1.DataSource = Commons.Modules.ObjSystems.DataNguoiDanhGia(-1, -1, -1, -1, -1);
                cboNDGTN1.Columns.Clear();
                cboNDGTN1.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_NGUOI_DGTN"));
                cboNDGTN1.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_NGUOI_DGTN"));
                cboNDGTN1.Columns["TEN_NGUOI_DGTN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_NGUOI_DGTN");
                cboNDGTN1.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboNDGTN1.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cboNDGTN1.Columns["ID_NGUOI_DGTN"].Visible = false;
                grvDSUngVien.Columns["NGUOI_DANH_GIA_1"].ColumnEdit = cboNDGTN1;
                cboNDGTN1.BeforePopup += CboDGTN_BeforePopup1;
                //cboDGTN.EditValueChanged += cboDGTN_EditValueChanged;

                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboNDGTN2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                cboNDGTN2.NullText = "";
                cboNDGTN2.ValueMember = "ID_NGUOI_DGTN";
                cboNDGTN2.DisplayMember = "TEN_NGUOI_DGTN";
                //ID_NGUOI_DGTN,TEN_NGUOI_DGTN
                cboNDGTN2.DataSource = Commons.Modules.ObjSystems.DataNguoiDanhGia(-1, -1, -1, -1, -1);
                cboNDGTN2.Columns.Clear();
                cboNDGTN2.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_NGUOI_DGTN"));
                cboNDGTN2.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_NGUOI_DGTN"));
                cboNDGTN2.Columns["TEN_NGUOI_DGTN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_NGUOI_DGTN");
                cboNDGTN2.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboNDGTN2.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cboNDGTN2.Columns["ID_NGUOI_DGTN"].Visible = false;
                grvDSUngVien.Columns["NGUOI_DANH_GIA_2"].ColumnEdit = cboNDGTN2;
                cboNDGTN2.BeforePopup += CboDGTN_BeforePopup2;




                Commons.Modules.ObjSystems.AddCombXtra("ID_DAT", "DAT", "KT_NHANH_TAY", grvDSUngVien, tbDat, false, "ID_DAT", "DAT");
                Commons.Modules.ObjSystems.AddCombXtra("ID_DAT", "DAT", "DAT", grvDSUngVien, tbDat, false, "ID_DAT", "DAT");

                if (iAdd == 0)
                {
                    grvDSUngVien.Columns["CHON"].Visible = false;
                }
                else
                {
                    grvDSUngVien.Columns["CHON"].Visible = true;
                    grvDSUngVien.Columns["CHON"].VisibleIndex = 0;
                    grvDSUngVien.Columns["CHON"].Caption = Commons.Modules.TypeLanguage == 0 ? "Chọn" : "Choose";
                }
                try
                {
                    if (LoaiCN == 1)
                    {//may
                        if (Convert.ToInt32(cboTayNghe.EditValue) == 1)
                        {
                            //đào tạo
                            grvDSUngVien.Columns["KQ_VAI"].Visible = false;
                            grvDSUngVien.Columns["ID_DGTN"].Visible = false;
                            grvDSUngVien.Columns["NGUOI_DANH_GIA_2"].Visible = false;
                            grvDSUngVien.Columns["MS_CN"].Visible = true;
                            grvDSUngVien.Columns["HO_TEN_NGT"].Visible = true;
                            grvDSUngVien.Columns["THUONG_TAY_NGHE"].Visible = false;

                            grvDSUngVien.Columns["DAT"].VisibleIndex = 14;
                            grvDSUngVien.Columns["MS_CN"].VisibleIndex = 15;
                            grvDSUngVien.Columns["HO_TEN_NGT"].VisibleIndex = 16;
                            grvDSUngVien.Columns["GHI_CHU"].VisibleIndex = 17;

                        }
                        else
                        {
                            //tây nghề
                            grvDSUngVien.Columns["KQ_VAI"].Visible = true;
                            grvDSUngVien.Columns["KQ_VAI"].VisibleIndex = 11;
                            grvDSUngVien.Columns["ID_DGTN"].Visible = true;
                            grvDSUngVien.Columns["ID_DGTN"].VisibleIndex = 12;
                            grvDSUngVien.Columns["NGUOI_DANH_GIA_2"].Visible = true;
                            grvDSUngVien.Columns["NGUOI_DANH_GIA_2"].VisibleIndex = 13;
                            grvDSUngVien.Columns["THUONG_TAY_NGHE"].Visible = true;
                            grvDSUngVien.Columns["THUONG_TAY_NGHE"].VisibleIndex = 14;

                            //DAT	ID_CN	GHI_CHU
                            grvDSUngVien.Columns["MS_CN"].Visible = true;
                            grvDSUngVien.Columns["HO_TEN_NGT"].Visible = true;
                            grvDSUngVien.Columns["DAT"].VisibleIndex = 15;
                            grvDSUngVien.Columns["MS_CN"].VisibleIndex = 16;
                            grvDSUngVien.Columns["HO_TEN_NGT"].VisibleIndex = 17;
                            grvDSUngVien.Columns["GHI_CHU"].VisibleIndex = 18;
                        }
                        grvDSUngVien.Columns["KQ_GIAY"].Caption = "KQKT bài giấy";
                    }
                    else
                    {
                        //công nhân khác

                        grvDSUngVien.Columns["KQ_VAI"].Visible = false;
                        grvDSUngVien.Columns["ID_DGTN"].Visible = false;
                        grvDSUngVien.Columns["NGUOI_DANH_GIA_2"].Visible = false;
                        grvDSUngVien.Columns["MS_CN"].Visible = false;
                        grvDSUngVien.Columns["HO_TEN_NGT"].Visible = false;
                        grvDSUngVien.Columns["THUONG_TAY_NGHE"].Visible = false;
                        grvDSUngVien.Columns["DAT"].VisibleIndex = 14;
                        grvDSUngVien.Columns["GHI_CHU"].VisibleIndex = 15;
                        grvDSUngVien.Columns["KQ_GIAY"].Caption = "KQKT tay nghề";
                    }
                    grvDSUngVien_FocusedRowChanged(null, null);
                }
                catch
                {
                }
            }
            catch (Exception ex) { }
        }

        private void CboDGTN_BeforePopup1(object sender, EventArgs e)
        {
            try
            {
                //LoaiCN = 1 may, = 2 khác
                LookUpEdit lookUp = sender as LookUpEdit;
                DataTable dt = Commons.Modules.ObjSystems.DataNguoiDanhGia(Convert.ToInt64(cboYCTD.EditValue), Convert.ToInt64(cboID_VTTD.EditValue), -1, -1, 1, LoaiCN == 1 ? 1 : 3);
                lookUp.Properties.DataSource = dt;
                string sdkien = "( 1 = 1 )";
                try
                {
                    sdkien = "(ID_NGUOI_DGTN NOT IN (" + (grvDSUngVien.GetFocusedRowCellValue("NGUOI_DANH_GIA_2").ToString() == "-1" ? 0 : grvDSUngVien.GetFocusedRowCellValue("NGUOI_DANH_GIA_2")) + "))";
                    dt.DefaultView.RowFilter = sdkien;
                }
                catch
                {
                    try
                    {
                        dt.DefaultView.RowFilter = "";
                    }
                    catch { }
                }

            }
            catch { }
        }

        private void CboDGTN_BeforePopup2(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                DataTable dt = Commons.Modules.ObjSystems.DataNguoiDanhGia(Convert.ToInt64(cboYCTD.EditValue), Convert.ToInt64(cboID_VTTD.EditValue), -1, -1, 1, 2);
                lookUp.Properties.DataSource = dt;
                string sdkien = "( 1 = 1 )";
                try
                {
                    sdkien = "(ID_NGUOI_DGTN NOT IN (" + (grvDSUngVien.GetFocusedRowCellValue("NGUOI_DANH_GIA_1").ToString() == "-1" ? 0 : grvDSUngVien.GetFocusedRowCellValue("NGUOI_DANH_GIA_1")) + "))";
                    dt.DefaultView.RowFilter = sdkien;
                }
                catch
                {
                    try
                    {
                        dt.DefaultView.RowFilter = "";
                    }
                    catch { }
                }

            }
            catch { }
        }


        private void cboDGTN_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                grvDSUngVien.SetFocusedRowCellValue("ID_DGTN", Convert.ToInt64((dataRow.Row[0])));
            }
            catch { }
        }
        private void cboDGTN_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false);
            }
            catch { }
        }
        private void cboNGT_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                grvDSUngVien.SetFocusedRowCellValue("ID_CN", Convert.ToInt64((dataRow.Row[0])));
            }
            catch { }
        }
        private void cboNGT_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                DataTable dtCN = new DataTable();
                dtCN.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 3));
                lookUp.Properties.DataSource = dtCN;
            }
            catch { }
        }
        private void LoadCbo()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spKiemTraTayNghe", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboYCTD, dt, "ID_YCTD", "MA_YCTD", "");
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_VTTD, Commons.Modules.ObjSystems.DataViTri(Convert.ToInt64(cboYCTD.EditValue), false), "ID_VTTD", "TEN_VTTD", "TEN_VTTD");
                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboTayNghe, Commons.Modules.ObjSystems.DataTayNghe(false), "ID_TAY_NGHE", "TEN_TAY_NGHE", "TEN_TAY_NGHE");
            }
            catch { }
        }
        private void EnabelButton(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = visible;
            btnALL.Buttons[3].Properties.Visible = visible;
            btnALL.Buttons[4].Properties.Visible = !visible;
            btnALL.Buttons[5].Properties.Visible = !visible;

            datTNgay.Properties.ReadOnly = !visible;
            datDNgay.Properties.ReadOnly = !visible;
            cboYCTD.Properties.ReadOnly = !visible;
            cboID_VTTD.Properties.ReadOnly = !visible;
            cboTayNghe.Properties.ReadOnly = !visible;

            grvDSUngVien.OptionsBehavior.Editable = !visible;
        }

        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "them":
                        {
                            iAdd = 1;
                            LoadData();
                            EnabelButton(false);
                            break;
                        }
                    case "sua":
                        {
                            iAdd = 0;
                            LoadData();
                            EnabelButton(false);
                            break;
                        }
                    case "luu":
                        {
                            grvDSUngVien.CloseEditor();
                            grvDSUngVien.UpdateCurrentRow();
                            if (grvDSUngVien.RowCount == 0)
                            {
                                Commons.Modules.ObjSystems.msgChung("msgKhongCoDuLieu");
                                return;
                            }
                            if (flag == true) return;
                            DataTable dt_CHON = new DataTable();
                            if (!SaveData()) return;
                            iAdd = 0;
                            LoadData();
                            EnabelButton(true);
                            break;
                        }
                    case "khongluu":
                        {
                            iAdd = 0;
                            LoadData();
                            EnabelButton(true);
                            break;
                        }
                    case "thoat":
                        {
                            Commons.Modules.ObjSystems.GotoHome(this);
                            break;
                        }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private bool SaveData()
        {
            string sBT = "sBTUngVien" + Commons.Modules.iIDUser;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien), "");
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spKiemTraTayNghe", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                cmd.Parameters.Add("@iCot1", SqlDbType.BigInt).Value = cboYCTD.EditValue;
                cmd.Parameters.Add("@iCot2", SqlDbType.BigInt).Value = cboID_VTTD.EditValue;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                Commons.Modules.ObjSystems.XoaTable(sBT);
                return true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBT);
                return false;
            }
        }
        private void datTNgay_EditValueChanged(object sender, EventArgs e)
        {

            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.ConvertDateTime(datTNgay.Text);
            int t = DateTime.DaysInMonth(datTNgay.DateTime.Year, datTNgay.DateTime.Month);
            DateTime secondDateTime = new DateTime(datTNgay.DateTime.Year, datTNgay.DateTime.Month, t);
            datDNgay.EditValue = secondDateTime;
            LoadData();
        }
        private void datDNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }

        private void cboID_VTTD_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                LoaiCN = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(ID_LT,0) FROM dbo.LOAI_CONG_VIEC WHERE ID_LCV = " + cboID_VTTD.EditValue + ""));
                if (LoaiCN == 1)
                {
                    lblTayNghe.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                else
                {
                    lblTayNghe.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                LoadData();
            }
            catch (Exception)
            {
            }
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
        // Cập nhật All
        public DXMenuItem MCreateMenuCapNhatAll(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblCapNhatAll", Commons.Modules.TypeLanguage);
            DXMenuItem menuCapNhatNhanh = new DXMenuItem(sStr, new EventHandler(CapNhatAll));
            menuCapNhatNhanh.Tag = new RowInfo(view, rowHandle);
            return menuCapNhatNhanh;
        }
        public void CapNhatAll(object sender, EventArgs e)
        {
            string sCotCN = grvDSUngVien.FocusedColumn.FieldName;
            string sBTUngVien = "sBTUngVien" + Commons.Modules.iIDUser;
            try
            {
                if (grvDSUngVien.GetFocusedRowCellValue(grvDSUngVien.FocusedColumn.FieldName).ToString() == "") return;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTUngVien, Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien), "");

                if (sCotCN.Length < 4)
                {

                }
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateChuotPhai_TiepNhan", sBTUngVien, sCotCN, sCotCN.Substring(0, 3) == "NGA" ? Convert.ToDateTime(grvDSUngVien.GetFocusedRowCellValue(grvDSUngVien.FocusedColumn.FieldName)).ToString("MM/dd/yyyy") : grvDSUngVien.GetFocusedRowCellValue(grvDSUngVien.FocusedColumn.FieldName)));
                grdDSUngVien.DataSource = dt;
                Commons.Modules.ObjSystems.XoaTable(sBTUngVien);
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBTUngVien);
            }
        }
        //Thong tin ung vien
        public DXMenuItem MCreateMenuThongTinUV(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblThongTinUV", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinUV = new DXMenuItem(sStr, new EventHandler(ThongTinUV));
            menuThongTinUV.Tag = new RowInfo(view, rowHandle);
            return menuThongTinUV;
        }
        public void ThongTinUV(object sender, EventArgs e)
        {
            try
            {
                iLoai = 1;
                iID_UV = Convert.ToInt64(grvDSUngVien.GetFocusedRowCellValue("ID_UV"));
                ucUV = new ucCTQLUV(Convert.ToInt64(grvDSUngVien.GetFocusedRowCellValue("ID_UV")));
                Commons.Modules.ObjSystems.ShowWaitForm(this);
                //ucUV.Refresh();
                ucUV.Refresh();

                //ns.accorMenuleft = accorMenuleft;
                tableLayoutPanel1.Hide();
                this.Controls.Add(ucUV);
                ucUV.Dock = DockStyle.Fill;
                ucUV.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
                Commons.Modules.ObjSystems.HideWaitForm();
            }
            catch { }
        }
        public void BackWindowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            if (iLoai == 1)
            {
                ucUV.Hide();
            }
            else
            {
                ucNS.Hide();
            }
            tableLayoutPanel1.Show();
            LoadData();
        }
        private void grvDSUngVien_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();
                    DevExpress.Utils.Menu.DXMenuItem itemTTUV = MCreateMenuThongTinUV(view, irow);
                    e.Menu.Items.Add(itemTTUV);
                    if (btnALL.Buttons[0].Properties.Visible == true) return;
                    if (grvDSUngVien.FocusedColumn.FieldName != "MS_UV" && grvDSUngVien.FocusedColumn.FieldName != "HO_TEN")
                    {
                        DevExpress.Utils.Menu.DXMenuItem itemCapNhatNhanh = MCreateMenuCapNhatAll(view, irow);
                        e.Menu.Items.Add(itemCapNhatNhanh);
                    }
                    //if (flag == false) return;
                }
            }
            catch
            {
            }
        }

        #endregion

        private void grvDSUngVien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                Commons.Modules.ObjSystems.RowFilter(grdDSUngVien, grvDSUngVien.Columns["TINH_TRANG_DG"], iAdd.ToString());
            }
            catch
            {
            }
        }
        private void cboYCTD_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT B.ID_LCV ID_VTTD,CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN B.TEN_LCV WHEN 1 THEN B.TEN_LCV_A ELSE B.TEN_LCV_H END TEN_VTTD FROM dbo.YCTD_VI_TRI_TUYEN A INNER JOIN dbo.LOAI_CONG_VIEC B ON B.ID_LCV = A.ID_VTTD WHERE B.ID_CV in (206,208) AND A.ID_YCTD = " + cboYCTD.EditValue + " ORDER BY B.TEN_LCV"));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_VTTD, dt, "ID_VTTD", "TEN_VTTD", "TEN_VTTD", true, true);
            LoadData();
        }

        private void grvDSUngVien_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                //int ngay = 0;
                DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                DevExpress.XtraGrid.Columns.GridColumn NgayDG = View.Columns["NGAY_DG"];

                if (View.GetRowCellValue(e.RowHandle, NgayDG).ToString() == "")
                {
                    flag = true;
                    e.Valid = false;
                    View.SetColumnError(NgayDG, "Ngày đánh giá không được trống"); return;
                }
                flag = false;
            }
            catch (Exception ex) { }
        }

        private void grvDSUngVien_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvDSUngVien_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void cboTayNghe_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }

        private void grvDSUngVien_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {


        }

        private void grvDSUngVien_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (grvDSUngVien.RowCount == 0) return;
                e.Cancel = Convert.ToBoolean(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT XAC_NHAN_DL FROM dbo.UNG_VIEN WHERE ID_UV = " + grvDSUngVien.GetFocusedRowCellValue("ID_UV") + ""));
            }
            catch
            {
            }
        }

        //private void grvDSUngVien_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (Commons.Modules.sLoad == "0Load") return;
        //        if (e.Column.FieldName == "ID_CN")
        //        {
        //            string TenCN = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT HO + ' '+ TEN  FROM dbo.CONG_NHAN WHERE ID_CN = " + e.Value + "").ToString();
        //            grvDSUngVien.SetRowCellValue(grvDSUngVien.FocusedRowHandle, "HO_TEN_NGT", TenCN);
        //            Commons.Modules.sLoad = "0Load";
        //            grvDSUngVien.SetRowCellValue(grvDSUngVien.FocusedRowHandle, "ID_CN", e.Value);
        //            Commons.Modules.sLoad = "";
        //            return;
        //        }
        //        return;
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //    }
        //}
    }
}
