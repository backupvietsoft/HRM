using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Vs.Recruit
{
    public partial class ucKeHoachPhongVan : DevExpress.XtraEditors.XtraUserControl
    {
        private Int64 iID_KHPV = 0;
        public ucKeHoachPhongVan()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>{Root},btnALL);
        }
        #region even
        private void ucKeHoachPhongVan_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            datTuNgay.DateTime = DateTime.Now.Date.AddDays(-DateTime.Now.Date.Day + 1);
            datDenNgay.DateTime = datTuNgay.DateTime.AddMonths(1).AddDays(-1);
            LoadCbo();
            LoadgrdKHPV(-1);
            Commons.Modules.sLoad = "";
            grvKHPV_FocusedRowChanged(null, null);
            enableButon(true);
            radioGroup1_SelectedIndexChanged(null, null);
            Commons.Modules.ObjSystems.SetPhanQuyen(btnALL);
        }
        private void LoadgrdKHPV(Int64 iID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListKHPV", datTuNgay.DateTime, datDenNgay.DateTime, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_KHPV"] };
                if (grdKHPV.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdKHPV, grvKHPV, dt, false, false, false, true, true, this.Name);
                    grvKHPV.Columns["ID_KHPV"].Visible = false;
                    grvKHPV.Columns["TINH_TRANG"].Visible = false;
                    grvKHPV.Columns["NGAY_LAP"].Visible = false;
                    grvKHPV.Columns["GHI_CHU"].Visible = false;
                    grvKHPV.Columns["PV_ON_OF_LINE"].Visible = false;
                    grvKHPV.Columns["NGUOI_PV_1"].Visible = false;
                    grvKHPV.Columns["NGUOI_PV_2"].Visible = false;
                }
                else
                {
                    grdKHPV.DataSource = dt;
                }
                if (iID != -1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(iID));
                    grvKHPV.FocusedRowHandle = grvKHPV.GetRowHandle(index);
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
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr,CommandType.Text, "SELECT ID_KHPV,ID_YCTD,ID_VTTD  FROM dbo.KHPV_VTTD WHERE ID_KHPV = " + iID_KHPV +""));
                if (grdViTri.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdViTri, grvViTri, dt, true, false, false, true, true, this.Name);
                    grvViTri.Columns["ID_KHPV"].Visible = false;

                    //ID_YCTD,MA_YCTD
                    Commons.Modules.ObjSystems.AddCombXtra("ID_YCTD", "MA_YCTD", grvViTri, Commons.Modules.ObjSystems.DataYeuCauTD(false, 1), true, "ID_YCTD", this.Name, true);
                    //Danh sach benh vien
                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboYCTD = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                    cboYCTD.NullText = "";
                    cboYCTD.ValueMember = "ID_YCTD";
                    cboYCTD.DisplayMember = "MA_YCTD";
                    //ID_YCTD,MA_YCTD
                    cboYCTD.DataSource = Commons.Modules.ObjSystems.DataYeuCauTD(false, 1);
                    cboYCTD.Columns.Clear();
                    cboYCTD.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_YCTD"));
                    cboYCTD.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MA_YCTD"));
                    cboYCTD.Columns["MA_YCTD"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "MA_YCTD");
                    cboYCTD.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboYCTD.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    cboYCTD.Columns["ID_YCTD"].Visible = false;
                    grvViTri.Columns["ID_YCTD"].ColumnEdit = cboYCTD;
                    cboYCTD.EditValueChanged += CboYCTD_EditValueChanged;


                    //ID_YCTD,MA_YCTD
                    //Danh sach benh vien
                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboViTri = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                    cboViTri.NullText = "";
                    cboViTri.ValueMember = "ID_VTTD";
                    cboViTri.DisplayMember = "TEN_VTTD";
                    //ID_VTTD,TEN_VTTD
                    cboViTri.DataSource = Commons.Modules.ObjSystems.DataViTri(-1);
                    cboViTri.Columns.Clear();
                    cboViTri.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_VTTD"));
                    cboViTri.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_VTTD"));
                    cboViTri.Columns["TEN_VTTD"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_VTTD");
                    cboViTri.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboViTri.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    cboViTri.Columns["ID_VTTD"].Visible = false;
                    grvViTri.Columns["ID_VTTD"].ColumnEdit = cboViTri;
                    cboViTri.BeforePopup += CboViTri_BeforePopup;
                    cboViTri.EditValueChanged += CboViTri_EditValueChanged;
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
        private void CboYCTD_EditValueChanged(object sender, EventArgs e)
        {
            grvViTri.PostEditor();
            grvViTri.SetFocusedRowCellValue("ID_VTTD",-99);
        }

        private void CboViTri_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvViTri.SetFocusedRowCellValue("ID_VTTD",Convert.ToInt64((dataRow.Row[0])));
        }

        private void CboViTri_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataViTri(Convert.ToInt64(grvViTri.GetFocusedRowCellValue("ID_YCTD")));
            }
            catch { }
        }

        private void LoadgrdUVPV(Boolean bBT)
        {
            try
            {
                DataTable dt = new DataTable();
                try
                {
                    if (bBT == false)
                    {
                        dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListUngVienPV", iID_KHPV, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    }
                    else
                    {
                        dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr,CommandType.Text, "SELECT * FROM dbo.sBTChonUV"+ Commons.Modules.UserName));
                    }    
                }
                catch
                {
                }
                if (grdUVPV.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdUVPV, grvUVPV, dt, false, false, true, true, true, this.Name);
                    grvUVPV.Columns["ID_KHPV"].Visible = false;
                    grvUVPV.Columns["ID_YCTD"].Visible = false;
                    grvUVPV.Columns["ID_VTTD"].Visible = false;
                    grvUVPV.Columns["ID_UV"].Visible = false;
                }
                else
                {
                    grdUVPV.DataSource = dt;
                }
            }
            catch
            {
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
                        iID_KHPV = -1;
                        enableButon(false);
                        BindingData(true);
                        break;
                    }
                case "sua":
                    {
                        if (txtSO_KH.EditValue.ToString() == "") return;
                        Commons.Modules.ObjSystems.AddnewRow(grvViTri, true);
                        enableButon(false);
                        break;
                    }
                case "xoa":
                    {
                        XoaKeHoachPhongVan();
                        break;
                    }
                case "ChonUV":
                    {
                        Int64 iID_VTTD =0,iID_YCTD = 0;
                        try
                        {
                            iID_VTTD = Convert.ToInt64(grvViTri.GetFocusedRowCellValue("ID_VTTD"));
                            if(iID_VTTD == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonViTri") , Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            iID_YCTD = Convert.ToInt64(grvViTri.GetFocusedRowCellValue("ID_YCTD"));
                            if (iID_VTTD == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonViTri"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        catch(Exception ex)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonViTri") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr,"sBTChonUV"+Commons.Modules.UserName,Commons.Modules.ObjSystems.ConvertDatatable(grdUVPV),"");
                        frmChonUngVien uv = new frmChonUngVien();
                        uv.iID_VTTD = iID_VTTD;
                        uv.iID_YCTD = iID_YCTD;
                        if (uv.ShowDialog() == DialogResult.OK)
                        {
                            string sSql = "DELETE A FROM sBTChonUV"+Commons.Modules.UserName+" A WHERE A.ID_YCTD = "+ grvViTri.GetFocusedRowCellValue("ID_YCTD") + " AND A.ID_VTTD = "+ grvViTri.GetFocusedRowCellValue("ID_VTTD") + " AND NOT EXISTS(SELECT * FROM dbo.sBTUV"+ Commons.Modules.UserName + " B WHERE B.CHON = 1 AND B.ID_UV = A.ID_UV)";
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr,CommandType.Text,sSql);

                            sSql = "INSERT INTO dbo.sBTChonUV"+ Commons.Modules.UserName + "(ID_KHPV,ID_YCTD,ID_VTTD,ID_UV,HO_TEN,GHI_CHU,NGUOI_YCTD_CHON)SELECT "+iID_KHPV+","+grvViTri.GetFocusedRowCellValue("ID_YCTD") + "," + grvViTri.GetFocusedRowCellValue("ID_VTTD") + ",ID_UV,HO_TEN,'',0 FROM dbo.sBTUV" + Commons.Modules.UserName + " A WHERE A.CHON = 1 AND NOT EXISTS (SELECT * FROM dbo.sBTChonUV" + Commons.Modules.UserName + " B WHERE B.ID_KHPV = "+ iID_KHPV + " AND B.ID_YCTD = "+ grvViTri.GetFocusedRowCellValue("ID_YCTD") + " AND B.ID_VTTD = " + grvViTri.GetFocusedRowCellValue("ID_VTTD") + " AND B.ID_UV = A.ID_UV)";
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                            LoadgrdUVPV(true);
                            grvViTri_FocusedRowChanged(null,null) ;
                        }    
                        break;
                    }

                case "luu":
                    {
                        if (!dxValidationProvider1.Validate()) return;
                        grvViTri.ValidateEditor();
                        if (grvViTri.HasColumnErrors) return;
                        if (!SaveData()) return;
                        Commons.Modules.ObjSystems.XoaTable("sBTChonUV" + Commons.Modules.UserName);
                        Commons.Modules.ObjSystems.XoaTable("sBTUV" + Commons.Modules.UserName);
                        LoadgrdKHPV(iID_KHPV);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvViTri);
                        enableButon(true);
                        break;
                    }
                case "khongluu":
                    {
                        Commons.Modules.ObjSystems.ClearValidationProvider(dxValidationProvider1);
                        Commons.Modules.ObjSystems.XoaTable("sBTChonUV" + Commons.Modules.UserName);
                        Commons.Modules.ObjSystems.XoaTable("sBTUV" + Commons.Modules.UserName);
                        BindingData(false);
                        enableButon(true);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvViTri);
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
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTVT" + Commons.Modules.UserName, Commons.Modules.ObjSystems.ConvertDatatable(grvViTri), "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTUVPV" + Commons.Modules.UserName, Commons.Modules.ObjSystems.ConvertDatatable(grdUVPV), "");
                iID_KHPV = Convert.ToInt64(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spSaveKeHoachPhongVan",
                    iID_KHPV, 
                    txtSO_KH.EditValue,
                    txtTieuDe.EditValue,
                    datNgayLap.DateTime,
                    cboTinhTrang.EditValue,
                    txtGhiChu.EditValue, 
                    chkKieuPV.Checked,
                    cboNguoiPV1.EditValue,
                    cboNguoiPV2.EditValue,
                    datNgayPV.Text.ToString() == "" ? DBNull.Value : datNgayPV.EditValue,
                    "sBTVT" + Commons.Modules.UserName, 
                    "sBTUVPV" + Commons.Modules.UserName));
                if (iID_KHPV != -1)
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
            btnALL.Buttons[6].Properties.Visible = !visible;
            btnALL.Buttons[7].Properties.Visible = visible;

            grvViTri.OptionsBehavior.Editable = !visible;

            txtTieuDe.Properties.ReadOnly = visible;
            datNgayLap.Properties.ReadOnly = visible;
            cboNguoiPV1.Properties.ReadOnly = visible;
            cboNguoiPV2.Properties.ReadOnly = visible;
            datNgayPV.Properties.ReadOnly = visible;
            chkKieuPV.Properties.ReadOnly = visible;
            txtGhiChu.Properties.ReadOnly = visible;

            //cboTinhTrang.Properties.ReadOnly = visible;

            rdoDK.Properties.ReadOnly = !visible;
            groDSPYC.Enabled = visible;
            datTuNgay.Properties.ReadOnly = !visible;
            datDenNgay.Properties.ReadOnly = !visible;
        }
        private void LoadCbo()
        {
            try
            {
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNguoiPV1, Commons.Modules.ObjSystems.DataCongNhan(false), "ID_CN", "TEN_CN", "TEN_CN", true, true);
                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboTinhTrang, Commons.Modules.ObjSystems.DataTinhTrangPV(false), "ID_TT_KHPV", "TEN_TT_KHPV", "TEN_TT_KHPV");
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNguoiPV2, Commons.Modules.ObjSystems.DataCongNhan(false), "ID_CN", "TEN_CN", "TEN_CN", true, true);
            }
            catch
            {
            }
        }
        private void BindingData(bool them)
        {
            if (them == true)
            {
                txtSO_KH.EditValue = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_KHPV(" + datNgayLap.DateTime.ToString("MM/dd/yyyy") + ")").ToString();
                datNgayLap.EditValue = DateTime.Now;
                cboNguoiPV1.EditValue = -1;
                cboNguoiPV2.EditValue = -1;
                txtGhiChu.EditValue = "";
                cboTinhTrang.EditValue = 1;
                iID_KHPV = -1;
            }
            else // Load data vao text
            {
                try
                {
                    txtSO_KH.EditValue = grvKHPV.GetFocusedRowCellValue("SO_KHPV").ToString();
                    txtTieuDe.EditValue = grvKHPV.GetFocusedRowCellValue("TIEU_DE").ToString();
                    cboTinhTrang.EditValue = Convert.ToInt32(grvKHPV.GetFocusedRowCellValue("TINH_TRANG"));
                    datNgayLap.EditValue = Convert.ToDateTime(grvKHPV.GetFocusedRowCellValue("NGAY_LAP"));
                    cboNguoiPV1.EditValue = Convert.ToInt64(grvKHPV.GetFocusedRowCellValue("NGUOI_PV_1"));
                    cboNguoiPV2.EditValue = Convert.ToInt64(grvKHPV.GetFocusedRowCellValue("NGUOI_PV_2"));
                    txtGhiChu.EditValue = grvKHPV.GetFocusedRowCellValue("GHI_CHU").ToString();
                    iID_KHPV = Convert.ToInt64(grvKHPV.GetFocusedRowCellValue("ID_KHPV"));
                    try
                    {
                        datNgayPV.EditValue = Convert.ToDateTime(grvKHPV.GetFocusedRowCellValue("NGAY_PV"));
                    }
                    catch
                    {
                        datNgayPV.EditValue = "";
                    }
                    chkKieuPV.EditValue = Convert.ToBoolean(grvKHPV.GetFocusedRowCellValue("PV_ON_OF_LINE"));
               
                }
                catch
                {
                    txtSO_KH.EditValue = "";
                    cboNguoiPV1.EditValue = -1;
                    cboNguoiPV2.EditValue = -1;
                    txtGhiChu.EditValue = "";
                    cboTinhTrang.EditValue = 1;
                    iID_KHPV = -1;
                }
            }
            Commons.Modules.sLoad = "0Load";
            LoadgrdViTri();
            LoadgrdUVPV(false);
            Commons.Modules.sLoad = "";
            grvViTri_FocusedRowChanged(null, null);
        }
        #endregion
        private void datNgayLap_EditValueChanged(object sender, EventArgs e)
        {
            if (iID_KHPV == -1 && btnALL.Buttons[0].Properties.Visible == true)
            {
                txtSO_KH.EditValue = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_KHPV("+  datNgayLap.DateTime.ToString("MM/dd/yyyy") +")").ToString();
            }
        }
        private void grvViTri_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                grvViTri.SetFocusedRowCellValue("ID_KHPV", iID_KHPV);
            }
            catch
            {
            }
        }

        private void grvUVPV_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                grvUVPV.SetFocusedRowCellValue("ID_KHPV", iID_KHPV);
                grvUVPV.SetFocusedRowCellValue("ID_VTTD", grvViTri.GetFocusedRowCellValue("ID_VTTD"));
            }
            catch
            {
            }
        }

        private void grvKHPV_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load" || btnALL.Buttons[0].Properties.Visible ==false) return;
            BindingData(false);
        }

        private void grvViTri_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void XoaKeHoachPhongVan()
        {
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteKeHoachPhongVan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                //kiểm tra ID_KHPV có trong phỏng vấn không

                if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.PHONG_VAN WHERE ID_KHPV ="+iID_KHPV+"")) > 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE FROM dbo.UNG_VIEN_TUYEN_DUNG WHERE ID_KHPV = "+ iID_KHPV +"");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE FROM dbo.KHPV_VTTD WHERE ID_KHPV = " + iID_KHPV + "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DBCC CHECKIDENT (KE_HOACH_PHONG_VAN,RESEED,0)DBCC CHECKIDENT (KE_HOACH_PHONG_VAN,RESEED) DELETE FROM dbo.KE_HOACH_PHONG_VAN WHERE ID_KHPV = " + iID_KHPV + "");
                //xóa file trên server
                grvKHPV.DeleteSelectedRows();
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
                XoaKeHoachPhongVan();
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
                grvUVPV.DeleteSelectedRows();
            }
        }
       
        private void datTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadgrdKHPV(iID_KHPV);
        }
        private void searchControl1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            grvKHPV_FocusedRowChanged(null, null);
        }

        private void grvViTri_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }


        private void grvViTri_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (Commons.Modules.sLoad == "0Load") return;
            //GridView view = sender as GridView;
            //if (e.Column.FieldName == "ID_YCTD")
            //{
            //    view.SetRowCellValue(e.RowHandle, view.Columns["ID_VTTD"], -99);
            //    Commons.Modules.sLoad = "0Load";
            //    grvViTri.SetFocusedRowCellValue("ID_YCTD", e.Value);
            //    Commons.Modules.sLoad = "";
            //}
        }

        private void grvViTri_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            try
            {
                Commons.Modules.ObjSystems.RowFilter(grdUVPV, grvUVPV.Columns["ID_YCTD"], grvUVPV.Columns["ID_VTTD"], grvViTri.GetFocusedRowCellValue("ID_YCTD").ToString(), grvViTri.GetFocusedRowCellValue("ID_VTTD").ToString());
            }
            catch
            {
                Commons.Modules.ObjSystems.RowFilter(grdUVPV, grvUVPV.Columns["ID_YCTD"], grvUVPV.Columns["ID_VTTD"], "-1","-1");
            }
            if (btnALL.Buttons[0].Properties.Visible == false)
            {
                if (grvUVPV.RowCount > 0)
                {
                    grvViTri.OptionsBehavior.Editable = false;
                }
                else
                {
                    grvViTri.OptionsBehavior.Editable = true;
                }
            }
        }

        private void grvViTri_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            //kiểm tra null
            
            DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            if (Commons.Modules.ObjSystems.IsnullorEmpty(View.GetRowCellValue(e.RowHandle,"ID_YCTD")) || View.GetRowCellValue(e.RowHandle, "ID_YCTD").ToString() == "-99")
            {
                e.Valid = false;
                View.SetColumnError(View.Columns["ID_YCTD"], Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrongDuLieu")); return;
            }
            if (Commons.Modules.ObjSystems.IsnullorEmpty(View.GetRowCellValue(e.RowHandle, "ID_VTTD")) || View.GetRowCellValue(e.RowHandle, "ID_VTTD").ToString() == "-99")
            {
                e.Valid = false;
                View.SetColumnError(View.Columns["ID_VTTD"], Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrongDuLieu")); return;
            }
            DataTable dt = Commons.Modules.ObjSystems.ConvertDatatable(grvViTri);
            int n = dt.AsEnumerable().Count(x => x["ID_YCTD"].ToString().Equals(grvViTri.GetFocusedRowCellValue("ID_YCTD").ToString()) && x["ID_VTTD"].ToString().Equals(grvViTri.GetFocusedRowCellValue("ID_VTTD").ToString()));
            if(n > 1)
            {
                e.Valid = false;
                View.SetColumnError(View.Columns["ID_YCTD"], Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrungDuLieu")); 
                View.SetColumnError(View.Columns["ID_VTTD"], Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrungDuLieu")); return;
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.RowFilter(grdKHPV, grvKHPV.Columns["TINH_TRANG"],(rdoDK.SelectedIndex + 1).ToString());

        }
    }
}
