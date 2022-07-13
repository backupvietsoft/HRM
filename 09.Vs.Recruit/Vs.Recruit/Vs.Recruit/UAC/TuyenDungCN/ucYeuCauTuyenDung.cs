using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace Vs.Recruit.UAC
{
    public partial class ucYeuCauTuyenDung : DevExpress.XtraEditors.XtraUserControl
    {
        private Int64 iID_YCTD, iID_VTTD = 0;
        public ucYeuCauTuyenDung()
        {
            InitializeComponent();
        }
        #region even
        private void ucYeuCauTuyenDung_Load(object sender, EventArgs e)
        {
            datTuNgay.DateTime = DateTime.Now.Date.AddDays(-DateTime.Now.Date.Day + 1);
            datDenNgay.DateTime = datTuNgay.DateTime.AddMonths(1).AddDays(-1);
            LoadCbo();
            LoadgrdPYC();
            LoadgrdViTri();
            LoadgrdThayThe();
            LoadgrdFileDinhKem();
            BindingData(false);
            enableButon(true);
        }
        private void LoadgrdPYC()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListPhieuYeuCau", datTuNgay.DateTime, datDenNgay.DateTime, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_YCTD"] };
                if (grdPYC.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdPYC, grvPYC, dt, false, false, true, true, true, this.Name);
                    grvPYC.Columns["ID_YCTD"].Visible = false;
                    grvPYC.Columns["ID_TO"].Visible = false;
                    grvPYC.Columns["ID_CN"].Visible = false;
                    grvPYC.Columns["NGAY_YEU_CAU"].Visible = false;
                    grvPYC.Columns["NGAY_NHAN_DON"].Visible = false;
                    grvPYC.Columns["GHI_CHU"].Visible = false;
                }
                else
                {
                    grdPYC.DataSource = dt;
                }
                if (iID_YCTD != -1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(iID_YCTD));
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
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_VTTD"] };
                if (grdViTri.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdViTri, grvViTri, dt, true, false, false, true, true, this.Name);
                    grvViTri.Columns["ID_YCTD"].Visible = false;
                    Commons.Modules.ObjSystems.AddCombXtra("ID_LCV", "TEN_LCV", grvViTri, Commons.Modules.ObjSystems.DataLoaiCV(false), true, "ID_LCV", this.Name, true);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_NGANH_TD", "TEN_NGANH_TD", grvViTri, Commons.Modules.ObjSystems.DataNganhTD(false), false, "ID_NGANH_TD", this.Name, true);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_LHCV", "TEN_LHCV", grvViTri, Commons.Modules.ObjSystems.DataLoaiHinhCV(false), false, "ID_LHCV", this.Name, true);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_KNLV", "TEN_KNLV", grvViTri, Commons.Modules.ObjSystems.DataKinhNghiemLV(false), false, "ID_KNLV", this.Name, true);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_LOAI_TUYEN", "TEN_LOAI_TUYEN", grvViTri, Commons.Modules.ObjSystems.DataLoaiTuyen(false), false, "ID_LOAI_TUYEN", this.Name, true);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_TTD", "TEN_TT_DUYET", grvViTri, Commons.Modules.ObjSystems.DataTinhTrangDuyet(false), false, "ID_TTD", this.Name, true);


                    //Commons.Modules.ObjSystems.AddButonEdit("DUONG_DAN_TL", grvViTri, ofileDialog, this.Name.Replace("uc", "") + '\\' + txtMA_YCTD.Text);
                    RepositoryItemButtonEdit btnEdit = new RepositoryItemButtonEdit();
                    grvViTri.Columns["DUONG_DAN_TL"].ColumnEdit = btnEdit;
                    btnEdit.ButtonClick += BtnEdit_ButtonClick;
                    btnEdit.DoubleClick += BtnEdit_DoubleClick;
                    //this.Name.Replace("uc", "") + '\\' + txtMA_YCTD.Text
                }
                else
                {
                    grdViTri.DataSource = dt;
                }

                if (iID_VTTD != -1)
                {
                    try
                    {
                        int index = dt.Rows.IndexOf(dt.Rows.Find(iID_VTTD));
                        grvViTri.FocusedRowHandle = grvViTri.GetRowHandle(index);

                    }
                    catch (Exception ex)
                    {
                    }
                }
                grvViTri_FocusedRowChanged(null, null);
            }
            catch
            {
            }
        }
        private void LoadgrdThayThe()
        {
            try
            {
                iID_VTTD = Convert.ToInt64(grvViTri.GetFocusedRowCellValue("ID_VTTD"));
            }
            catch
            {
                iID_VTTD = -1;
            }
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
                    Commons.Modules.ObjSystems.AddCombXtra("ID_LCV", "TEN_LCV", grvThayThe, Commons.Modules.ObjSystems.DataLoaiCV(false), true, "ID_LCV", this.Name, true);
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
                    btnEdit.DoubleClick += BtnEdit_DoubleClick;
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
                if (ofileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (ofileDialog.FileName.ToString().Trim() == "") return;
                    Commons.Modules.ObjSystems.LuuDuongDan(ofileDialog.FileName, ofileDialog.SafeFileName, this.Name.Replace("uc", "") + '\\' + txtMA_YCTD.Text);
                    a.Text = ofileDialog.SafeFileName;
                }
            }
            catch(Exception ex)
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
                        Commons.Modules.ObjSystems.AddnewRow(grvViTri, true);
                        Commons.Modules.ObjSystems.AddnewRow(grvThayThe, true);
                        Commons.Modules.ObjSystems.AddnewRow(grvFileDK, true);
                        enableButon(false);
                        break;
                    }
                case "xoa":
                    {
                        break;
                    }

                case "luu":
                    {
                        if (!SaveData())
                            return;
                        LoadgrdPYC();
                        Commons.Modules.ObjSystems.DeleteAddRow(grvViTri);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvThayThe);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvFileDK);
                        break;
                    }
                case "khongluu":
                    {
                        Commons.Modules.ObjSystems.DeleteAddRow(grvViTri);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvThayThe);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvFileDK);
                        enableButon(true);
                        BindingData(false);
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
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTVT" + Commons.Modules.UserName, Commons.Modules.ObjSystems.ConvertDatatable(grvViTri),"");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTThayThe" + Commons.Modules.UserName, Commons.Modules.ObjSystems.ConvertDatatable(grvThayThe), "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTFile" + Commons.Modules.UserName, Commons.Modules.ObjSystems.ConvertDatatable(grvFileDK), "");
                iID_YCTD = Convert.ToInt64(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spSaveYeuCauTuyenDung", iID_YCTD, txtMA_YCTD.EditValue, cboBPYC.EditValue, cboNguoiYC.EditValue, datNgayYC.DateTime, datNgayNhanDon.EditValue, txtLyDo.EditValue,"sBTVT" + Commons.Modules.UserName, "sBTThayThe" + Commons.Modules.UserName,"sBTFile" + Commons.Modules.UserName));

                if (iID_YCTD != -1)
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
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
            btnALL.Buttons[4].Properties.Visible = !visible;
            btnALL.Buttons[5].Properties.Visible = !visible;
            btnALL.Buttons[6].Properties.Visible = visible;

            grvThayThe.OptionsBehavior.Editable = !visible;
            grvViTri.OptionsBehavior.Editable = !visible;
            grvFileDK.OptionsBehavior.Editable = !visible;

            txtMA_YCTD.Properties.ReadOnly = visible;
            cboBPYC.Properties.ReadOnly = visible;
            datNgayYC.Properties.ReadOnly = visible;
            cboNguoiYC.Properties.ReadOnly = visible;
            datNgayNhanDon.Properties.ReadOnly = visible;
            txtLyDo.Properties.ReadOnly = visible;
        }
        private void LoadCbo()
        {
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNguoiYC, Commons.Modules.ObjSystems.DataCongNhan(false), "ID_CN", "TEN_CN", "TEN_CN", true, true);

            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboBPYC, Commons.Modules.ObjSystems.DataTo(-1, -1, false), "ID_TO", "TEN_TO", "TEN_TO", true, true);

        }
        private void BindingData(bool them)
        {

            if (them == true)
            {
                txtMA_YCTD.EditValue = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_YCTD(GETDATE())").ToString();
                cboBPYC.EditValue = -1;
                datNgayYC.EditValue = DateTime.Now;
                cboNguoiYC.EditValue = -1;
                datNgayNhanDon.EditValue = "";
                txtLyDo.EditValue = "";
            }
            else // Load data vao text
            {
                try
                {
                    DataTable dt = new DataTable();
                    //dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetYeuCauTuyenDung", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iID_YCTD));
                    txtMA_YCTD.EditValue = dt.Rows[0]["MA_YCTD"].ToString();
                    cboBPYC.EditValue = Convert.ToInt64(dt.Rows[0]["ID_TO"]);
                    datNgayYC.EditValue = Convert.ToDateTime(dt.Rows[0]["NGAY_YEU_CAU"]);
                    cboNguoiYC.EditValue = Convert.ToInt64(dt.Rows[0]["ID_CN"]);
                    datNgayNhanDon.EditValue = dt.Rows[0]["NGAY_NHAN_DON"].ToString();
                    txtLyDo.EditValue = dt.Rows[0]["GHI_CHU"].ToString();
                }
                catch { }
            }
        }
        #endregion

        private void grvViTri_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            //e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void tab_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            if (tab.SelectedTabPageIndex == 1)
            {
                LoadgrdFileDinhKem();
                if (btnALL.Buttons[4].Properties.Visible == true)
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
                txtMA_YCTD.EditValue = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_YCTD(GETDATE())").ToString();
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

        private void grvViTri_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            //e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
    }
}
