﻿using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Vs.Report;

namespace Vs.Recruit
{
    public partial class ucYeuCauTuyenDung : DevExpress.XtraEditors.XtraUserControl
    {
        private Int64 iID_YCTD = 0;
        public ucYeuCauTuyenDung()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, tab, btnALL);
        }
        #region even
        private void ucYeuCauTuyenDung_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            datTuNgay.DateTime = DateTime.Now.AddDays(-DateTime.Now.Date.Day + 1);
            LoadCbo();
            LoadgrdPYC(-1);
            cboTrangThai_EditValueChanged(null, null);
            BindingData(false);
            Commons.Modules.sLoad = "";
            Commons.Modules.ObjSystems.DeleteAddRow(grvViTri);
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(btnALL);
        }
        private void LoadgrdPYC(Int64 iID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListPhieuYeuCau", datTuNgay.DateTime, datTuNgay.DateTime.AddMonths(1).AddDays(-1), Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_YCTD"] };
                if (grdPYC.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdPYC, grvPYC, dt, false, false, false, true, true, this.Name);
                    grvPYC.Columns["ID_YCTD"].Visible = false;
                    grvPYC.Columns["ID_XN"].Visible = false;
                    grvPYC.Columns["ID_CN"].Visible = false;
                    grvPYC.Columns["ID_TT"].Visible = false;
                    grvPYC.Columns["NGAY_YEU_CAU"].Visible = false;
                    grvPYC.Columns["NGAY_NHAN_DON"].Visible = false;
                    grvPYC.Columns["GHI_CHU"].Visible = false;
                }
                else
                {
                    grdPYC.DataSource = dt;
                }
                if (iID != -1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(iID));
                    grvPYC.FocusedRowHandle = grvPYC.GetRowHandle(index);
                }
            }
            catch
            {
            }
        }
        private void LoadgrdViTri()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListYCTDVT", iID_YCTD, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dt.Columns["ID_LCV"].ReadOnly = false;
                if (grdViTri.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdViTri, grvViTri, dt, true, false, false, true, true, this.Name);
                    grvViTri.Columns["ID_YCTD"].Visible = false;
                    //Commons.Modules.ObjSystems.AddCombXtra("ID_LCV", "TEN_LCV", grvViTri, Commons.Modules.ObjSystems.DataLoaiCV(false), true, "ID_LCV", this.Name, true);

                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboViTri = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                    cboViTri.NullText = "";
                    cboViTri.ValueMember = "ID_LCV";
                    cboViTri.DisplayMember = "TEN_LCV";
                    //ID_LCV,TEN_LCV
                    cboViTri.DataSource = Commons.Modules.ObjSystems.DataLoaiCV(false,-1);
                    cboViTri.Columns.Clear();
                    cboViTri.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_LCV"));
                    cboViTri.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_LCV"));
                    cboViTri.Columns["TEN_LCV"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_LCV");
                    cboViTri.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboViTri.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    cboViTri.Columns["ID_LCV"].Visible = false;
                    grvViTri.Columns["ID_LCV"].ColumnEdit = cboViTri;
                    cboViTri.BeforePopup += CboViTri_BeforePopup;
                    cboViTri.EditValueChanged += CboViTri_EditValueChanged;

                    Commons.Modules.ObjSystems.AddCombXtra("ID_NGANH_TD", "TEN_NGANH_TD", grvViTri, Commons.Modules.ObjSystems.DataNganhTD(false), false, "ID_NGANH_TD", this.Name, true);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_LHCV", "TEN_LHCV", grvViTri, Commons.Modules.ObjSystems.DataLoaiHinhCV(false), false, "ID_LHCV", this.Name, true);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_KNLV", "TEN_KNLV", grvViTri, Commons.Modules.ObjSystems.DataKinhNghiemLV(false), false, "ID_KNLV", this.Name, true);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_LOAI_TUYEN", "TEN_LOAI_TUYEN", grvViTri, Commons.Modules.ObjSystems.DataLoaiTuyen(false), false, "ID_LOAI_TUYEN", this.Name, true);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_TTD", "TEN_TT_DUYET", grvViTri, Commons.Modules.ObjSystems.DataTinhTrangDuyet(false), false, "ID_TTD", this.Name, true);

                    grvViTri.Columns["SL_TUYEN"].DisplayFormat.FormatType = FormatType.None;
                    grvViTri.Columns["SL_TUYEN"].DisplayFormat.FormatString = Commons.Modules.sSoLeSL;
                    grvViTri.Columns["MUC_LUONG_DK"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvViTri.Columns["MUC_LUONG_DK"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;


                    RepositoryItemButtonEdit btnEdit = new RepositoryItemButtonEdit();
                    btnEdit.ReadOnly = true;
                    grvViTri.Columns["DUONG_DAN_TL"].ColumnEdit = btnEdit;
                    grvViTri.Columns["DUONG_DAN_TL"].OptionsColumn.AllowEdit = true;
                    btnEdit.ButtonClick += BtnEdit_ButtonClick;
                    grvViTri.Columns["DUONG_DAN_TL"].OptionsColumn.ReadOnly = false;
                    grvViTri.Columns["ID_LCV"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                    //this.Name.Replace("uc", "") + '\\' + txtMA_YCTD.Text
                }
                else
                {
                    grdViTri.DataSource = dt;
                }

            }
            catch
            {
            }

        }
        private void CboViTri_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvViTri.SetFocusedRowCellValue("ID_LCV", Convert.ToUInt64((dataRow.Row[0])));
        }


        private void CboViTri_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT A.ID_LCV,CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN TEN_LCV WHEN 1 THEN ISNULL(NULLIF(TEN_LCV_A,''),TEN_LCV) ELSE ISNULL(NULLIF(TEN_LCV_H,''),TEN_LCV) END AS TEN_LCV FROM dbo.LOAI_CONG_VIEC A INNER JOIN dbo.LOAI_CONG_VIEC_XI_NGHIEP B ON B.ID_LCV = A.ID_LCV WHERE B.ID_XN = " + cboBPYC.EditValue + ""));
                dt.Columns["ID_LCV"].ReadOnly = true;
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = dt;
            }
            catch { }
        }

        private void LoadgrdThayThe()
        {

            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListYCTDThayThe", iID_YCTD, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                if (grdThayThe.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdThayThe, grvThayThe, dt, false, false, true, true, true, this.Name);
                    grvThayThe.Columns["ID_YCTD"].Visible = false;
                    grvThayThe.Columns["ID_VTTD"].Visible = false;

                    Commons.Modules.ObjSystems.AddCombXtra("ID_CN", "TEN_CN", grvThayThe, Commons.Modules.ObjSystems.DataCongNhan(false), true, "ID_CN", this.Name, true);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_LCV", "TEN_LCV", grvThayThe, Commons.Modules.ObjSystems.DataLoaiCV(false,-1), true, "ID_LCV", this.Name, true);
                }
                else
                {
                    grdThayThe.DataSource = dt;
                }
            }
            catch
            {
            }
        }
        private void LoadgrdFileDinhKem()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListFileDinhKem", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iID_YCTD));
                if (grdFileDK.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdFileDK, grvFileDK, dt, true, true, true, true, true, this.Name);
                    grvFileDK.Columns["ID_YCTD"].Visible = false;
                    grvFileDK.Columns["ID_VT_FL"].Visible = false;
                    RepositoryItemButtonEdit btnEdit = new RepositoryItemButtonEdit();
                    grvFileDK.Columns["DUONG_DAN"].ColumnEdit = btnEdit;
                    btnEdit.ButtonClick += BtnEdit_ButtonClick;
                    //btnEdit.DoubleClick += BtnEdit_DoubleClick;
                }
                else
                {
                    grdFileDK.DataSource = dt;
                }
            }
            catch
            {
            }
        }
        private void BtnEdit_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                ButtonEdit a = sender as ButtonEdit;
                Commons.Modules.ObjSystems.OpenHinh(Commons.Modules.sDDTaiLieu + '\\' + this.Name.Replace("uc", "") + '\\' + txtMA_YCTD.Text + '\\' + a.Text);
            }
            catch
            {
            }
        }
        private void BtnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                ButtonEdit a = sender as ButtonEdit;
                ofileDialog.Filter = "All Files|*.txt;*.docx;*.doc;*.pdf*.xls;*.xlsx;*.pptx;*.ppt|Text File (.txt)|*.txt|Word File (.docx ,.doc)|*.docx;*.doc|Spreadsheet (.xls ,.xlsx)|  *.xls ;*.xlsx";
                //ofileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm|Word Documents(*.doc)|*.doc";
                if (ofileDialog.ShowDialog() == DialogResult.OK)
                {
                    string sduongDan = ofileDialog.FileName.ToString().Trim();
                    if (ofileDialog.FileName.ToString().Trim() == "") return;
                    //if (sduongDan.Substring(sduongDan.IndexOf('.'), 4).ToString() == ".xlsx") return;
                    Commons.Modules.ObjSystems.LuuDuongDan(ofileDialog.FileName, ofileDialog.SafeFileName, this.Name.Replace("uc", "") + '\\' + txtMA_YCTD.Text);
                    a.Text = ofileDialog.SafeFileName;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgBanKhongCoQuyenTruyCapDD"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgfrmThongBao"), MessageBoxButtons.OK);
            }
        }
        private void btnALL_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        Commons.Modules.ObjSystems.AddnewRow(grvViTri, true);
                        Commons.Modules.ObjSystems.AddnewRow(grvThayThe, true);
                        Commons.Modules.ObjSystems.AddnewRow(grvFileDK, true);
                        iID_YCTD = -1;
                        enableButon(false);
                        BindingData(true);
                        break;
                    }
                case "sua":
                    {
                        if (Convert.ToInt32(cboTinhTrang.EditValue) != 1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuDaPhatSinhKhongSua"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (txtMA_YCTD.EditValue.ToString() == "") return;
                        Commons.Modules.ObjSystems.AddnewRow(grvViTri, true);
                        Commons.Modules.ObjSystems.AddnewRow(grvThayThe, true);
                        Commons.Modules.ObjSystems.AddnewRow(grvFileDK, true);
                        enableButon(false);
                        break;
                    }
                case "xoa":
                    {
                       
                        XoaYeuCauTuyenDung();
                        break;
                    }

                case "In":
                    {
                        if (grvPYC.RowCount == 0) return;
                        frmViewReport frm = new frmViewReport();
                        System.Data.SqlClient.SqlConnection conn;
                        DataTable dt = new DataTable();
                        frm.rpt = new rptThongBaoTuyenDung();
                        try
                        {
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptThongBaoTuyenDung", conn);
                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@ID_YCTD", SqlDbType.BigInt).Value = Convert.ToInt64(grvPYC.GetFocusedRowCellValue("ID_YCTD"));
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            dt = new DataTable();
                            dt = ds.Tables[0].Copy();
                            dt.TableName = "DA_TA";
                            frm.AddDataSource(dt);

                            //dt = ds.Tables[1].Copy();
                            //dt.TableName = "NOI_DUNG";
                            //frm.AddDataSource(dt);
                            //frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                        }
                        catch
                        {
                        }

                        frm.ShowDialog();
                        break;
                    }

                case "luu":
                    {
                        if (!dxValidationProvider1.Validate()) return;
                        grvViTri.ValidateEditor();
                        grvThayThe.ValidateEditor();
                        if (grvViTri.HasColumnErrors || grvThayThe.HasColumnErrors) return;
                        if (!SaveData()) return;
                        LoadgrdPYC(iID_YCTD);
                        cboTrangThai_EditValueChanged(null, null);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvViTri);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvThayThe);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvFileDK);
                        enableButon(true);
                        break;
                    }
                case "khongluu":
                    {
                        Commons.Modules.ObjSystems.ClearValidationProvider(dxValidationProvider1);
                        BindingData(false);
                        enableButon(true);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvViTri);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvThayThe);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvFileDK);
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
        private bool SaveData()
        {
            try
            {
                 Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTVT" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grvViTri), "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTThayThe" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grdThayThe), "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTFile" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grvFileDK), "");
                iID_YCTD = Convert.ToInt64(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spSaveYeuCauTuyenDung", iID_YCTD, txtMA_YCTD.EditValue, cboBPYC.EditValue, cboNguoiYC.EditValue, cboTinhTrang.EditValue, datNgayYC.DateTime, datNgayNhanDon.Text.ToString() == "" ? DBNull.Value : datNgayNhanDon.EditValue, txtLyDo.EditValue, "sBTVT" + Commons.Modules.iIDUser, "sBTThayThe" + Commons.Modules.iIDUser, grvFileDK.DataSource == null ? "" : "sBTFile" + Commons.Modules.iIDUser));

                if (iID_YCTD != -1)
                    return true;
                else
                    return false;
            }
            catch(Exception EX)
            {
                return false;
            }
        }
        #endregion
        #region function 
        private void enableButon(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = visible;
            btnALL.Buttons[3].Properties.Visible = visible;
            btnALL.Buttons[4].Properties.Visible = visible;
            btnALL.Buttons[5].Properties.Visible = visible;
            btnALL.Buttons[6].Properties.Visible = !visible;
            btnALL.Buttons[7].Properties.Visible = !visible;
            btnALL.Buttons[8].Properties.Visible = visible;
            grvThayThe.OptionsBehavior.Editable = !visible;

            grvViTri.OptionsBehavior.Editable = !visible;
            grvFileDK.OptionsBehavior.Editable = !visible;

            //txtMA_YCTD.Properties.ReadOnly = visible;
            cboBPYC.Properties.ReadOnly = visible;
            datNgayYC.Properties.ReadOnly = visible;
            cboNguoiYC.Properties.ReadOnly = visible;
            datNgayNhanDon.Properties.ReadOnly = visible;
            txtLyDo.Properties.ReadOnly = visible;

            groDSPYC.Enabled = visible;
            datTuNgay.Properties.ReadOnly = !visible;
        }
        private void LoadCbo()
        {
            try
            {
                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboTrangThai, Commons.Modules.ObjSystems.DataTinhTrangYC(false), "ID_TTYC", "Ten_TTYC", "Ten_TTYC");
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNguoiYC, Commons.Modules.ObjSystems.DataCongNhan(false), "ID_CN", "TEN_CN", "TEN_CN", true, true);
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboBPYC, Commons.Modules.ObjSystems.DataXiNghiep(-1, false), "ID_XN", "TEN_XN", "TEN_XN", true, true);
                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboTinhTrang, Commons.Modules.ObjSystems.DataTinhTrangYC(false), "ID_TTYC", "Ten_TTYC", "Ten_TTYC");
            }
            catch (Exception ex)
            {
            }
        }
        private void BindingData(bool them)
        {
            if (them == true)
            {
                txtMA_YCTD.EditValue = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_YCTD(" + datNgayYC.DateTime.ToString("MM/dd/yyyy") + ")").ToString();
                cboBPYC.EditValue = -1;
                datNgayYC.EditValue = DateTime.Now;
                cboNguoiYC.EditValue = -1;
                datNgayNhanDon.EditValue = DateTime.Now;
                txtLyDo.EditValue = "";
                cboTinhTrang.EditValue = 1;
                iID_YCTD = -1;
                if (tab.SelectedTabPageIndex == 1)
                {
                    LoadgrdFileDinhKem();
                }
                else
                {
                    LoadgrdViTri();
                }
            }
            else // Load data vao text
            {
                try
                {
                    iID_YCTD = Convert.ToInt64(grvPYC.GetFocusedRowCellValue("ID_YCTD"));
                    txtMA_YCTD.EditValue = grvPYC.GetFocusedRowCellValue("MA_YCTD").ToString();
                    cboBPYC.EditValue = Convert.ToInt64(grvPYC.GetFocusedRowCellValue("ID_XN"));
                    cboTinhTrang.EditValue = Convert.ToInt32(grvPYC.GetFocusedRowCellValue("ID_TT"));
                    datNgayYC.EditValue = Convert.ToDateTime(grvPYC.GetFocusedRowCellValue("NGAY_YEU_CAU"));
                    cboNguoiYC.EditValue = Convert.ToInt64(grvPYC.GetFocusedRowCellValue("ID_CN"));
                    try
                    {
                        datNgayNhanDon.EditValue = Convert.ToDateTime(grvPYC.GetFocusedRowCellValue("NGAY_NHAN_DON"));
                    }
                    catch
                    {
                        datNgayNhanDon.EditValue = "";
                    }
                    txtLyDo.EditValue = grvPYC.GetFocusedRowCellValue("GHI_CHU").ToString();
                    if (tab.SelectedTabPageIndex == 1)
                    {
                        LoadgrdFileDinhKem();
                    }
                    grvViTri_FocusedRowChanged(null, null);
                }
                catch
                {
                    cboBPYC.EditValue = -1;
                    txtMA_YCTD.EditValue = "";
                    cboNguoiYC.EditValue = -1;
                    datNgayNhanDon.EditValue = "";
                    txtLyDo.EditValue = "";
                    cboTinhTrang.EditValue = 1;
                    iID_YCTD = -1;
                }
                if(Convert.ToInt32(cboTinhTrang.EditValue) != 1)
                {
                    cboTinhTrang.Properties.Buttons[1].Visible = true;
                }
                else
                {
                    cboTinhTrang.Properties.Buttons[1].Visible = false;
                }    
                LoadgrdViTri();
                LoadgrdThayThe();
            }
        }
        #endregion



        private void tab_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            if (tab.SelectedTabPageIndex == 1)
            {
                LoadgrdFileDinhKem();
                if (btnALL.Buttons[6].Properties.Visible == true)
                {
                    Commons.Modules.ObjSystems.AddnewRow(grvFileDK, true);
                }
                else
                {
                    Commons.Modules.ObjSystems.DeleteAddRow(grvFileDK);
                }
            }
        }
        private void datNgayYC_EditValueChanged(object sender, EventArgs e)
        {
            if (iID_YCTD == -1)
            {
                txtMA_YCTD.EditValue = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_YCTD(" + datNgayYC.DateTime.ToString("MM/dd/yyyy") + ")").ToString();
            }
        }
        private void grvViTri_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate())
                {
                    grvViTri.DeleteSelectedRows();
                    return;
                }
                grvViTri.SetFocusedRowCellValue("ID_YCTD", iID_YCTD);
                grvViTri.SetFocusedRowCellValue("ID_TTD", 2);
            }
            catch
            {
            }
        }

        private void grvViTri_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (Convert.ToInt64(grvViTri.GetFocusedRowCellValue("ID_LOAI_TUYEN")) == 3)
                {
                    groNVThayThe.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    Commons.Modules.ObjSystems.RowFilter(grdThayThe, grvThayThe.Columns["ID_VTTD"], grvViTri.GetFocusedRowCellValue("ID_LCV").ToString());
                }
                else
                {
                    groNVThayThe.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
            }
            catch
            {
                groNVThayThe.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void grvViTri_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (e.Column.FieldName == "ID_LOAI_TUYEN")
            {
                if (Convert.ToInt16(e.Value) == 3)
                {
                    groNVThayThe.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    Commons.Modules.ObjSystems.RowFilter(grdThayThe, grvThayThe.Columns["ID_VTTD"], grvViTri.GetFocusedRowCellValue("ID_LCV").ToString());
                }
                else
                {
                    groNVThayThe.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                Commons.Modules.sLoad = "0Load";
                grvViTri.SetFocusedRowCellValue("ID_LOAI_TUYEN", e.Value);
                Commons.Modules.sLoad = "";
            }
        }

        private void grvThayThe_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate())
                {
                    grvThayThe.DeleteSelectedRows();
                    return;
                }
                grvThayThe.SetFocusedRowCellValue("ID_YCTD", iID_YCTD);
                grvThayThe.SetFocusedRowCellValue("ID_VTTD", grvViTri.GetFocusedRowCellValue("ID_LCV"));
            }
            catch
            {
            }
        }
        private void grvThayThe_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "ID_CN")
            {
                try
                {
                    string sSql = "SELECT TOP 1 ID_LCV,NGAY_NGHI_VIEC,CASE 0 WHEN 0 THEN B.TEN_LD_TV ELSE B.TEN_LD_TV_A END TEN_LD_TV FROM dbo.CONG_NHAN A LEFT JOIN dbo.LY_DO_THOI_VIEC B ON B.ID_LD_TV = A.ID_LD_TV WHERE ID_CN = " + e.Value + " ";
                    DataTable dt = new DataTable();
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                    grvThayThe.SetFocusedRowCellValue("ID_LCV", Convert.ToInt64(dt.Rows[0]["ID_LCV"]));
                    grvThayThe.SetFocusedRowCellValue("NGAY_LV_CUOI", dt.Rows[0]["NGAY_NGHI_VIEC"]);
                    grvThayThe.SetFocusedRowCellValue("LY_DO_NGHI", dt.Rows[0]["TEN_LD_TV"]);
                }
                catch
                {
                }
            }
        }

        private void grvPYC_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            BindingData(false);
        }

        private void grvViTri_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            grvViTri.ClearColumnErrors();
            try
            {
                DataTable dt = new DataTable();
                if (grvViTri == null) return;
                if (grvViTri.FocusedColumn.FieldName == "ID_LCV")
                {//kiểm tra máy không được để trống
                    if (string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        e.Valid = false;
                        e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erMayKhongTrong");
                        grvViTri.SetColumnError(grvViTri.Columns["ID_LCV"], e.ErrorText);
                        return;
                    }
                    else
                    {
                        dt = new DataTable();
                        dt = Commons.Modules.ObjSystems.ConvertDatatable(grvViTri);
                        if (dt.AsEnumerable().Count(x => x.Field<Int64>("ID_LCV").Equals(e.Value)) > 1)
                        {
                            e.Valid = false;
                            e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrungDuLieu");
                            grvViTri.SetColumnError(grvViTri.Columns["ID_LCV"], e.ErrorText);
                            return;
                        }
                    }
                }
            }
            catch
            { }
        }
        private void grvThayThe_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            grvThayThe.ClearColumnErrors();
            try
            {
                DataTable dt = new DataTable();
                if (grvThayThe == null) return;
                if (grvThayThe.FocusedColumn.FieldName == "ID_CN")
                {//kiểm tra máy không được để trống
                    if (string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        e.Valid = false;
                        e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erMayKhongTrong");
                        grvThayThe.SetColumnError(grvThayThe.Columns["ID_CN"], e.ErrorText);
                        return;
                    }
                    else
                    {
                        dt = new DataTable();
                        dt = Commons.Modules.ObjSystems.ConvertDatatable(grdThayThe);
                        if (dt.AsEnumerable().Count(x => x.Field<Int64>("ID_CN").Equals(e.Value)) > 0)
                        {
                            e.Valid = false;
                            e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrungDuLieu");
                            grvThayThe.SetColumnError(grvThayThe.Columns["ID_CN"], e.ErrorText);
                            return;
                        }
                    }
                }
            }
            catch
            { }
        }
        private void grvThayThe_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
        private void grvViTri_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
        private void grvThayThe_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void XoaYeuCauTuyenDung()
        {

            if (Convert.ToInt32(cboTinhTrang.EditValue) != 1)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuDaPhatSinhKhongXoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteYeuCauTuyenDung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.VI_TRI_FILE WHERE ID_YCTD = " + iID_YCTD + "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.YCTD_THAY_THE_CN WHERE ID_YCTD = " + iID_YCTD + "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.YCTD_VI_TRI_TUYEN WHERE ID_YCTD = " + iID_YCTD + "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DBCC CHECKIDENT (YEU_CAU_TUYEN_DUNG,RESEED,0)DBCC CHECKIDENT (YEU_CAU_TUYEN_DUNG,RESEED) DELETE dbo.YEU_CAU_TUYEN_DUNG WHERE ID_YCTD = " + iID_YCTD + "");
                //xóa file trên server
                Commons.Modules.ObjSystems.DeleteDirectory(Commons.Modules.sDDTaiLieu + '\\' + this.Name.Replace("uc", "") + '\\' + txtMA_YCTD.Text);
                grvPYC.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void grdPYC_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                XoaYeuCauTuyenDung();
            }
        }

        private void grdViTri_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (btnALL.Buttons[0].Properties.Visible == false && e.KeyData == Keys.Delete)
            {
                grvViTri.DeleteSelectedRows();
            }
        }

        private void grdThayThe_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (btnALL.Buttons[0].Properties.Visible == false && e.KeyData == Keys.Delete)
            {
                grvThayThe.DeleteSelectedRows();
            }
        }
        private void grdFileDK_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (btnALL.Buttons[0].Properties.Visible == false && e.KeyData == Keys.Delete)
            {
                grvFileDK.DeleteSelectedRows();
            }
        }
        private void datTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadgrdPYC(iID_YCTD);
            cboTrangThai_EditValueChanged(null, null);
        }
        private void searchControl1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            grvPYC_FocusedRowChanged(null, null);
        }

        private void grvViTri_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);
            try
            {
                if (info.Column.FieldName == "DUONG_DAN_TL" && info.RowHandle >= 0)
                {

                    Commons.Modules.ObjSystems.OpenHinh(Commons.Modules.sDDTaiLieu + '\\' + this.Name.Replace("uc", "") + '\\' + txtMA_YCTD.Text + '\\' + grvViTri.GetFocusedRowCellValue("DUONG_DAN_TL"));
                }
            }
            catch
            {
            }
        }
        private void grvFileDK_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);

            try
            {
                if (info.Column.FieldName == "DUONG_DAN" && info.RowHandle >= 0)
                {
                    Commons.Modules.ObjSystems.OpenHinh(Commons.Modules.sDDTaiLieu + '\\' + this.Name.Replace("uc", "") + '\\' + txtMA_YCTD.Text + '\\' + grvFileDK.GetFocusedRowCellValue("DUONG_DAN"));
                }
            }
            catch
            {
            }
        }
        private void grvViTri_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

    

        private void grvViTri_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {

                DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                DevExpress.XtraGrid.Columns.GridColumn sVT = View.Columns["ID_LCV"];
                if (View.GetRowCellValue(e.RowHandle, sVT).ToString() == "" || View.GetRowCellValue(e.RowHandle, sVT).ToString() == "-99")
                {
                    e.Valid = false;
                    View.SetColumnError(sVT, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraItemNULL", Commons.Modules.TypeLanguage)); return;

                }
                DevExpress.XtraGrid.Columns.GridColumn colSL = View.Columns["SL_TUYEN"];
                if (Commons.Modules.ObjSystems.IsnullorEmpty(View.GetRowCellValue(e.RowHandle, colSL)))
                {
                    e.Valid = false;
                    View.SetColumnError(colSL, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgSoLuongLonHonKhong", Commons.Modules.TypeLanguage)); return;
                }
            }
            catch
            {
            }
        }

        private void cboNguoiYC_BeforePopup(object sender, EventArgs e)
        {
            if (cboBPYC.EditValue.ToString() == "-1") cboBPYC.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChuaChonBoPhan");
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT T1.ID_CN, T1.MS_CN, T1.HO +' '+ T1.TEN AS TEN_CN FROM dbo.CONG_NHAN T1 WHERE T1.YC_TD = 1 AND T1.ID_TO IN(SELECT ID_TO FROM dbo.[TO] WHERE ID_XN = " + cboBPYC.EditValue + ")"));
                cboNguoiYC.Properties.DataSource = dt;
                cboBPYC.ErrorText = "";
            }
            catch
            {
            }
        }
        private void cboBPYC_EditValueChanged(object sender, EventArgs e)
        {
            cboNguoiYC.EditValue = -1;
        }

        private void cboBPYC_QueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((DataTable)grdViTri.DataSource).Rows.Count > 0)
            {
                cboBPYC.Properties.ReadOnly = true;
            }
            else
            {
                cboBPYC.Properties.ReadOnly = false;
            }
        }

        private void cboTinhTrang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", Convert.ToInt32(cboTinhTrang.EditValue) ==2?"msgBanCoMuonKhoaPhieu"  : "msgBanCoMuonMoKhoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr,CommandType.Text , "UPDATE dbo.YEU_CAU_TUYEN_DUNG SET ID_TT ="+ (Convert.ToInt32(cboTinhTrang.EditValue) == 2 ? "3" : "2") +" WHERE ID_YCTD = "+ iID_YCTD +"");
                cboTinhTrang.EditValue = Convert.ToInt32(cboTinhTrang.EditValue) == 2 ? 3 : 2;
                //update trạng thái vào đây
            }    
        }

        private void cboTrangThai_EditValueChanged(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.RowFilter(grdPYC, grvPYC.Columns["ID_TT"], (cboTrangThai.EditValue).ToString());

        }
    }
}
