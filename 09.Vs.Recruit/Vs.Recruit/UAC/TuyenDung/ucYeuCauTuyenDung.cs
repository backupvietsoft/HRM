using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Vs.Report;

namespace Vs.Recruit
{
    public partial class ucYeuCauTuyenDung : DevExpress.XtraEditors.XtraUserControl
    {
        private Int64 iID_YCTD = 0;
        private Int64 iID_LCV = -1;
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
            BindingData(false);
            Commons.Modules.sLoad = "";
            enableButon(true);
            cboTrangThai_EditValueChanged(null, null);
            Commons.Modules.ObjSystems.SetPhanQuyen(btnALL);
            foreach (ToolStripMenuItem item in contextMenuStrip1.Items)
            {
                item.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, item.Name);
            }
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
            if (Commons.Modules.sLoad == "0Load") return;
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListYCTDVT", iID_YCTD, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dt.Columns["ID_LCV"].ReadOnly = false;
                dt.Columns["SL_HC"].ReadOnly = false;
                dt.Columns["ID_LCV"].ReadOnly = false;
                dt.Columns["SL_DAT"].ReadOnly = false;
                dt.Columns["SL_CL"].ReadOnly = false;
                dt.Columns["SL_DINH_BIEN"].ReadOnly = false;
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_LCV"] };
                if (grdViTri.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdViTri, grvViTri, dt, true, false, false, true, true, this.Name);
                    grvViTri.Columns["ID_YCTD"].Visible = false;
                    //ID_MUT,TEN_MUT

                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboViTri = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                    cboViTri.NullText = "";
                    cboViTri.ValueMember = "ID_LCV";
                    cboViTri.DisplayMember = "TEN_LCV";

                    //ID_LCV,TEN_LCV
                    cboViTri.DataSource = Commons.Modules.ObjSystems.DataLoaiCV(false, -1);

                    cboViTri.Columns.Clear();
                    cboViTri.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_LCV"));
                    cboViTri.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_LCV"));
                    cboViTri.Columns["TEN_LCV"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_LCV");
                    cboViTri.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboViTri.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    cboViTri.Columns["ID_LCV"].Visible = false;
                    grvViTri.Columns["ID_LCV"].ColumnEdit = cboViTri;
                    grvViTri.Columns["ID_TT_VT"].OptionsColumn.AllowEdit = false;

                    grvViTri.Columns["SL_HC"].OptionsColumn.AllowEdit = false;
                    grvViTri.Columns["SL_DINH_BIEN"].OptionsColumn.AllowEdit = false;
                    grvViTri.Columns["SL_DAT"].OptionsColumn.AllowEdit = false;
                    grvViTri.Columns["SL_CL"].OptionsColumn.AllowEdit = false;
                    grvViTri.Columns["NGAY_DUYET"].OptionsColumn.AllowEdit = false;

                    cboViTri.BeforePopup += CboViTri_BeforePopup;
                    cboViTri.EditValueChanged += CboViTri_EditValueChanged;

                    Commons.Modules.ObjSystems.AddCombXtra("ID_NGANH_TD", "TEN_NGANH_TD", grvViTri, Commons.Modules.ObjSystems.DataNganhTD(false), false, "ID_NGANH_TD", this.Name, true, false);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_LHCV", "TEN_LHCV", grvViTri, Commons.Modules.ObjSystems.DataLoaiHinhCV(false), false, "ID_LHCV", this.Name, true, false);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_KNLV", "TEN_KNLV", grvViTri, Commons.Modules.ObjSystems.DataKinhNghiemLV(false), false, "ID_KNLV", this.Name, true, false);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_LOAI_TUYEN", "TEN_LOAI_TUYEN", grvViTri, Commons.Modules.ObjSystems.DataLoaiTuyen(false), false, "ID_LOAI_TUYEN", this.Name, true, false);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_TTD", "TEN_TT_DUYET", grvViTri, Commons.Modules.ObjSystems.DataTinhTrangDuyet(false), false, "ID_TTD", this.Name, true, false);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_TT_VT", "TEN_TT_VT", grvViTri, Commons.Modules.ObjSystems.DataTinhTrangCVYC(false), false, "ID_TT_VT", this.Name, true, false);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_MUT", "TEN_MUT", grvViTri, Commons.Modules.ObjSystems.DataMucUuTienTD(false), false, "ID_MUT", this.Name, true, false);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_ML", "TEN_ML", grvViTri, Commons.Modules.ObjSystems.DataMucLuong(false), false, "ID_ML", this.Name, true, false);

                    grvViTri.Columns["SL_TUYEN"].DisplayFormat.FormatType = FormatType.None;
                    grvViTri.Columns["SL_TUYEN"].DisplayFormat.FormatString = Commons.Modules.sSoLeSL;

                    RepositoryItemButtonEdit btnEdit = new RepositoryItemButtonEdit();
                    btnEdit.ReadOnly = true;
                    grvViTri.Columns["DUONG_DAN_TL"].ColumnEdit = btnEdit;
                    grvViTri.Columns["DUONG_DAN_TL"].OptionsColumn.AllowEdit = true;
                    btnEdit.ButtonClick += BtnEdit_ButtonClick;
                    grvViTri.Columns["DUONG_DAN_TL"].OptionsColumn.ReadOnly = false;
                    grvViTri.Columns["ID_LCV"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                    //this.Name.Replace("uc", "") + '\\' + txtMA_YCTD.Text
                    Commons.Modules.ObjSystems.DeleteAddRow(grvViTri);

                }
                else
                {
                    grdViTri.DataSource = dt;
                }
                if (iID_LCV != -1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(iID_LCV));
                    grvViTri.FocusedRowHandle = grvViTri.GetRowHandle(index);
                    grvViTri.ClearSelection();
                    grvViTri.SelectRow(index);
                }
                grvViTri_FocusedRowChanged(null, null);

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
                if (!dxValidationProvider1.Validate()) return;
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT A.ID_LCV,CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN TEN_LCV WHEN 1 THEN ISNULL(NULLIF(TEN_LCV_A,''),TEN_LCV) ELSE ISNULL(NULLIF(TEN_LCV_H,''),TEN_LCV) END AS TEN_LCV FROM dbo.LOAI_CONG_VIEC A INNER JOIN dbo.LOAI_CONG_VIEC_XI_NGHIEP B ON B.ID_LCV = A.ID_LCV WHERE B.ID_XN = " + cboBPYC.EditValue + ""));
                dt.Columns["ID_LCV"].ReadOnly = true;
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = dt;
                DataTable dtTmp = new DataTable();
                string sdkien = "( 1 = 1 )";
                try
                {
                    string sID = "";
                    DataTable dtTemp = new DataTable();
                    dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grvViTri).Copy();
                    for (int i = 0; i < dtTmp.Rows.Count; i++)
                    {
                        sID = sID + dtTmp.Rows[i]["ID_LCV"].ToString() + ",";
                    }
                    sID = sID.Substring(0, sID.Length - 1);
                    sdkien = "(ID_LCV NOT IN (" + sID + "))";
                    dt.DefaultView.RowFilter = sdkien;
                }
                catch
                {
                    try
                    {
                        dtTmp.DefaultView.RowFilter = "";
                    }
                    catch { }
                }

            }
            catch { }
        }
        private void LoadgrdThayThe()
        {
            if (Commons.Modules.sLoad == "0Load") return;
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListYCTDThayThe", iID_YCTD, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                if (grdThayThe.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdThayThe, grvThayThe, dt, false, false, true, true, true, this.Name);
                    grvThayThe.Columns["ID_YCTD"].Visible = false;
                    grvThayThe.Columns["ID_VTTD"].Visible = false;

                    //Commons.Modules.ObjSystems.AddCombXtra("ID_CN", "TEN_CN", grvThayThe, Commons.Modules.ObjSystems.DataCongNhan(false), true, "ID_CN", this.Name, true);

                    DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cboNhanVien = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                    cboNhanVien.NullText = "";
                    cboNhanVien.ValueMember = "ID_CN";
                    cboNhanVien.DisplayMember = "TEN_CN";
                    cboNhanVien.DataSource = Commons.Modules.ObjSystems.DataCongNhanTheoLoaiCV(-1);
                    cboNhanVien.View.PopulateColumns(cboNhanVien.DataSource);
                    cboNhanVien.View.Columns["ID_CN"].Visible = false;
                    cboNhanVien.View.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboNhanVien.View.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                    cboNhanVien.BeforePopup += CboNhanVien_BeforePopup;
                    Commons.Modules.ObjSystems.MLoadNNXtraGrid(cboNhanVien.View, this.Name);
                    grvThayThe.Columns["ID_CN"].ColumnEdit = cboNhanVien;

                    //cboNhanVien.EditValueChanged += CboViTri_EditValueChanged;
                    //Convert.ToInt64(grvViTri.GetFocusedRowCellValue("ID_VTTD"))


                    Commons.Modules.ObjSystems.AddCombXtra("ID_LCV", "TEN_LCV", grvThayThe, Commons.Modules.ObjSystems.DataLoaiCV(false, Convert.ToInt32(cboBPYC.EditValue)), true, "ID_LCV", this.Name, true);
                }
                else
                {
                    grdThayThe.DataSource = dt;
                }
                grvViTri_FocusedRowChanged(null, null);
            }
            catch (Exception ex)
            {
            }
        }

        private void CboNhanVien_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                SearchLookUpEdit lookUp = sender as SearchLookUpEdit;
                dt = Commons.Modules.ObjSystems.DataCongNhanTheoLoaiCV(Convert.ToInt64(grvViTri.GetFocusedRowCellValue("ID_LCV")));
                lookUp.Properties.DataSource = dt;

                DataTable dtTmp = new DataTable();
                string sdkien = "( 1 = 1 )";
                try
                {
                    string sID = "";
                    DataTable dtTemp = new DataTable();
                    dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grvThayThe).Copy();
                    for (int i = 0; i < dtTmp.Rows.Count; i++)
                    {
                        sID = sID + dtTmp.Rows[i]["ID_CN"].ToString() + ",";
                    }
                    sID = sID.Substring(0, sID.Length - 1);
                    sdkien = "(ID_CN NOT IN (" + sID + "))";
                    dt.DefaultView.RowFilter = sdkien;
                }
                catch
                {
                    try
                    {
                        dtTmp.DefaultView.RowFilter = "";
                    }
                    catch { }
                }

            }
            catch (Exception ex) { }

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
                    btnEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
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
                if (Commons.Modules.iLOAI_CN == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgBanKhongCoQuyenTruyCapDD"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgfrmThongBao"), MessageBoxButtons.OK);
                    return;
                }
                ButtonEdit a = sender as ButtonEdit;
                ofileDialog.Filter = "All Files|*.txt;*.docx;*.doc;*.pdf*.xls;*.xlsx;*.pptx;*.ppt|Text File (.txt)|*.txt|Word File (.docx ,.doc)|*.docx;*.doc|Spreadsheet (.xls ,.xlsx)|  *.xls ;*.xlsx";
                ofileDialog.FileName = "";
                if (ofileDialog.ShowDialog() == DialogResult.OK)
                {
                    string sduongDan = ofileDialog.FileName.ToString().Trim();
                    if (ofileDialog.FileName.ToString().Trim() == "") return;
                    Commons.Modules.ObjSystems.LuuDuongDan(ofileDialog.FileName, ofileDialog.SafeFileName, this.Name.Replace("uc", "") + '\\' + txtMA_YCTD.Text);
                    string folderLocation = Commons.Modules.sDDTaiLieu + '\\' + this.Name.Replace("uc", "") + '\\' + txtMA_YCTD.Text + '\\' + ofileDialog.SafeFileName;
                    a.Text = folderLocation;
                }
            }
            catch
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgBanKhongCoQuyenTruyCapDD"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgfrmThongBao"), MessageBoxButtons.OK);
            }
        }
        public void btnALL_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "chuyenduyet":
                    {
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoMuonChuyenDuyetKhong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                        try
                        {
                            for (int i = 0; i < grvViTri.RowCount; i++)
                            {
                                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spQuyDinhDuyetTaiLieu",
                                    Commons.Modules.iIDUser, 
                                    this.Name,
                                    iID_YCTD,
                                    grvViTri.GetRowCellValue(i, "ID_LCV"), 
                                    txtMA_YCTD.Text + " " + grvViTri.GetRowCellDisplayText(i, "ID_LCV").ToString(),
                                    1, 
                                    InDuLieuCD("SELECT T2.TEN_LCV, T1.SL_TUYEN AS SO_LUONG, T1.MO_TA_CV, T1.YEU_CAU, T1.YEU_CAU_KHAC, T1.THOI_GIAN_LAM_VIEC, T1.CHE_DO_PHUC_LOI FROM dbo.YCTD_VI_TRI_TUYEN T1 INNER JOIN dbo.LOAI_CONG_VIEC T2 ON T2.ID_LCV = T1.ID_VTTD WHERE T1.ID_YCTD = "+ iID_YCTD + " AND T1.ID_VTTD = "+ grvViTri.GetRowCellValue(i, "ID_LCV") + ""),
                                    Convert.ToInt32(grvViTri.GetRowCellValue(i, "ID_MUT")) == 1 ? true : false,
                                    txtLyDo.Text,
                                    Commons.Modules.UserName,
                                    Commons.Modules.TypeLanguage);
                            }
                            LoadgrdPYC(iID_YCTD);
                            cboTrangThai.EditValue = 2;
                            btnALL.Buttons[0].Properties.Visible = false;
                        }
                        catch(Exception ex)
                        {
                            XtraMessageBox.Show(ex.ToString());
                        }
                        break;
                    }
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
                        grvViTri_RowCountChanged(null, null);
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
                            if (dt.Rows.Count == 0)
                            {
                                Commons.Modules.ObjSystems.msgChung("msgChuaDuocDuyetKhongTheIn");
                                return;
                            }
                            dt.TableName = "DA_TA";
                            frm.AddDataSource(dt);
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
                        if (grvViTri.RowCount < 2 && grvViTri.GetFocusedRowCellValue("ID_LCV") == null) return;
                        Validate();
                        if (grvViTri.HasColumnErrors || grvThayThe.HasColumnErrors || grvFileDK.HasColumnErrors) return;
                        if (!KiemTraLuoi(Commons.Modules.ObjSystems.ConvertDatatable(grvViTri))) return;
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
                        grvPYC_FocusedRowChanged(null, null);
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

        private string InDuLieuCD(string sSql)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr,CommandType.Text,sSql));
            frmViewReport frm = new frmViewReport();
            frm.rpt = new rptThongBaoTuyenDung();
            frm.AddDataSource(dt);
            frm.frmViewReport_Load(null, null);
            string file = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
            frm.rpt.ExportToPdf(file, null);
            string resulst = Commons.Modules.ObjSystems.FileCopy(Application.StartupPath, file, this.Name);
            try
            {
                File.Delete(file);
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
            
            return resulst;

        }


        private bool SaveData()
        {
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTVT" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grvViTri), "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTThayThe" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grdThayThe), "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTFile" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grvFileDK), "");
                iID_YCTD = Convert.ToInt64(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spSaveYeuCauTuyenDung", iID_YCTD, txtMA_YCTD.EditValue, cboBPYC.EditValue, cboNguoiYC.EditValue, cboTinhTrang.EditValue, datNgayYC.DateTime, datNgayNhanDon.Text.ToString() == "" ? DBNull.Value : datNgayNhanDon.EditValue, txtLyDo.EditValue, "sBTVT" + Commons.Modules.iIDUser, "sBTThayThe" + Commons.Modules.iIDUser, grvFileDK.DataSource == null ? "" : "sBTFile" + Commons.Modules.iIDUser));
                try
                {


                    //xóa hết file không có trong 
                    string[] fileList = Directory.GetFiles(Commons.Modules.sDDTaiLieu + '\\' + this.Name.Replace("uc", "") + '\\' + txtMA_YCTD.Text);//lay danh sách file cho vao mảng
                                                                                                                                                     //duyet mang file trong thư mục
                                                                                                                                                     //duyệt list file không có trong lưới thì xóa
                    foreach (string item in fileList)
                    {
                        //kiểm tra item có trong table không
                        if (Commons.Modules.ObjSystems.ConvertDatatable(grvFileDK).AsEnumerable().Count(x => x["DUONG_DAN"].Equals(item)) == 0)
                        {
                            Commons.Modules.ObjSystems.Xoahinh(item);
                        }
                    }
                }
                catch
                {
                }
                if (iID_YCTD != -1)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #region function 
        private void enableButon(bool visible)
        {
            try
            {
                btnALL.Buttons[0].Properties.Visible = visible;
                btnALL.Buttons[1].Properties.Visible = visible;
                btnALL.Buttons[2].Properties.Visible = visible;
                btnALL.Buttons[3].Properties.Visible = visible;
                btnALL.Buttons[4].Properties.Visible = visible;
                btnALL.Buttons[5].Properties.Visible = visible;
                btnALL.Buttons[6].Properties.Visible = visible;
                btnALL.Buttons[7].Properties.Visible = !visible;
                btnALL.Buttons[8].Properties.Visible = !visible;
                btnALL.Buttons[9].Properties.Visible = visible;
                grvThayThe.OptionsBehavior.Editable = !visible;

                grvViTri.OptionsBehavior.Editable = !visible;
                grvFileDK.OptionsBehavior.Editable = !visible;

                //txtMA_YCTD.Properties.ReadOnly = visible;
                cboBPYC.Properties.ReadOnly = visible;
                datNgayYC.Properties.ReadOnly = visible;
                datNgayYC.Properties.Buttons[0].Enabled = !datNgayYC.Properties.ReadOnly;

                cboNguoiYC.Properties.ReadOnly = visible;
                datNgayNhanDon.Properties.ReadOnly = visible;
                datNgayNhanDon.Properties.Buttons[0].Enabled = !datNgayNhanDon.Properties.ReadOnly;

                txtLyDo.Properties.ReadOnly = visible;
                groDSPYC.Enabled = visible;
                datTuNgay.Properties.ReadOnly = !visible;
            }
            catch
            {
            }
        }
        private void LoadCbo()
        {
            try
            {
                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboTrangThai, Commons.Modules.ObjSystems.DataTinhTrangYC(false), "ID_TTYC", "Ten_TTYC", "Ten_TTYC");
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNguoiYC, Commons.Modules.ObjSystems.DataCongNhan(false), "ID_CN", "TEN_CN", "TEN_CN", true, true);
                //DataTable dt = new DataTable();
                //dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT  ID_XN,T.TEN_XN FROM (SELECT DISTINCT XN.STT_DV, XN.STT_XN, T1.ID_XN, XN.TEN_XN FROM dbo.LOAI_CONG_VIEC_XI_NGHIEP T1 INNER JOIN(SELECT DISTINCT ID_XN, TEN_XN, STT_XN, STT_DV FROM MGetToUser('" + Commons.Modules.UserName + "', " + Commons.Modules.TypeLanguage + ")) XN ON XN.ID_XN = T1.ID_XN)AS T ORDER BY T.STT_DV, T.STT_XN"));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboBPYC, Commons.Modules.ObjSystems.DataXiNghiep(-1, false), "ID_XN", "TEN_XN", "TEN_XN", true, true);
                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboTinhTrang, Commons.Modules.ObjSystems.DataTinhTrangYC(false), "ID_TTYC", "Ten_TTYC", "Ten_TTYC");
            }
            catch
            {
            }
        }
        private void BindingData(bool them)
        {
            try
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
                    LoadgrdViTri();
                    LoadgrdFileDinhKem();
                }
                else // Load data vao text
                {
                    try
                    {
                        iID_YCTD = Convert.ToInt64(grvPYC.GetFocusedRowCellValue("ID_YCTD"));
                        iID_LCV = Convert.ToInt64(grvViTri.GetFocusedRowCellValue("ID_LCV"));
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
                        //grvViTri_FocusedRowChanged(null, null);
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
                        iID_LCV = -1;
                    }
                    LoadgrdViTri();
                    LoadgrdThayThe();

                }
            }
            catch
            {
            }
        }
        #endregion
        private void tab_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            if (tab.SelectedTabPageIndex == 1)
            {
                LoadgrdFileDinhKem();
                if (btnALL.Buttons[7].Properties.Visible == true)
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

                grvViTri.SetFocusedRowCellValue("ID_YCTD", iID_YCTD);
                grvViTri.SetFocusedRowCellValue("ID_TTD", 2);
                grvViTri.SetFocusedRowCellValue("ID_TT_VT", 1);
                grvViTri.SetFocusedRowCellValue("ID_MUT", 2);
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
        private void grvThayThe_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
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
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                if (Commons.Modules.sLoad == "0FS") return;
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboBPYC, Commons.Modules.ObjSystems.DataXiNghiep(-1, false), "ID_XN", "TEN_XN", "TEN_XN", true, false);
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNguoiYC, Commons.Modules.ObjSystems.DataCongNhan(false), "ID_CN", "TEN_CN", "TEN_CN", true, true);
                //khi ở chế độ view thì thì hiện chuyển duyệt khi tình trạng đang soạn
                BindingData(false);
                if (Convert.ToInt32(cboTinhTrang.EditValue) == 1 || grvPYC.FocusedRowHandle < 0)
                {
                    if (grvPYC.FocusedRowHandle < 0)
                    {
                        btnALL.Buttons[0].Properties.Visible = false;
                        btnALL.Buttons[2].Properties.Visible = false;
                        btnALL.Buttons[3].Properties.Visible = false;
                    }
                    else
                    {
                        btnALL.Buttons[0].Properties.Visible = true;
                        btnALL.Buttons[2].Properties.Visible = true;
                        btnALL.Buttons[3].Properties.Visible = true;
                    }
                }
                else
                {
                    btnALL.Buttons[0].Properties.Visible = false;
                    btnALL.Buttons[2].Properties.Visible = false;
                    btnALL.Buttons[3].Properties.Visible = false;
                }

            }
            catch
            {
            }
        }
        private void grvViTri_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                grvViTri.ClearColumnErrors();
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

                if (grvViTri.FocusedColumn.FieldName == "SL_TUYEN")
                {//kiểm tra máy không được để trống
                    if (string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        e.Valid = false;
                        e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erSLTuyenKhongDuocTrong");
                        grvViTri.SetColumnError(grvViTri.Columns["SL_TUYEN"], e.ErrorText);
                        return;
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
                    //else
                    //{
                    //    dt = new DataTable();
                    //    dt = Commons.Modules.ObjSystems.ConvertDatatable(grdThayThe);
                    //    if (dt.AsEnumerable().Count(x => x.Field<Int64>("ID_CN").Equals(e.Value)) > 0)
                    //    {
                    //        e.Valid = false;
                    //        e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrungDuLieu");
                    //        grvThayThe.SetColumnError(grvThayThe.Columns["ID_CN"], e.ErrorText);
                    //        return;
                    //    }
                    //}
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
                if (btnALL.Buttons[9].Properties.Visible == false)
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
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void grdPYC_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && Convert.ToInt32(cboTinhTrang.EditValue) == 1)
            {
                XoaYeuCauTuyenDung();
            }
        }
        private void grdViTri_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (btnALL.Buttons[1].Properties.Visible == false && e.KeyData == Keys.Delete && Convert.ToInt32(cboTinhTrang.EditValue) == 1)
            {
                grvViTri.DeleteSelectedRows();
            }
        }
        private void grdThayThe_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (btnALL.Buttons[1].Properties.Visible == false && e.KeyData == Keys.Delete && Convert.ToInt32(cboTinhTrang.EditValue) == 1)
            {
                grvThayThe.DeleteSelectedRows();
            }
        }
        private void grdFileDK_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (btnALL.Buttons[1].Properties.Visible == false && e.KeyData == Keys.Delete && Convert.ToInt32(cboTinhTrang.EditValue) == 1)
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
                    Commons.Modules.ObjSystems.OpenHinh(grvFileDK.GetFocusedRowCellValue("DUONG_DAN").ToString());
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
                else
                {
                    if (Convert.ToInt32(View.GetRowCellValue(e.RowHandle, colSL)) <= 0)
                    {
                        e.Valid = false;
                        View.SetColumnError(colSL, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgSoLuongLonHonKhong", Commons.Modules.TypeLanguage)); return;
                    }

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
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT T1.ID_CN, T1.MS_CN, T1.HO +' '+ T1.TEN AS TEN_CN FROM dbo.CONG_NHAN T1 INNER JOIN dbo.XI_NGHIEP_NGUOI_TUYEN_DUNG T2 ON T2.ID_CN = T1.ID_CN WHERE T2.ID_XN = " + cboBPYC.EditValue + " AND T2.YEU_CAU = 1 AND T2.ACTIVE = 1 ORDER BY T1.HO +' '+ T1.TEN"));
                cboNguoiYC.Properties.DataSource = dt;
                cboNguoiYC.EditValue = -99;
                cboBPYC.ErrorText = "";
            }
            catch
            {
            }
        }
        private void cboBPYC_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                cboNguoiYC.EditValue = -1;

            }
            catch
            {
            }
        }
        //private void cboTinhTrang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    if (e.Button.Index == 1)
        //    {
        //        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", Convert.ToInt32(cboTinhTrang.EditValue) == 2 ? "msgBanCoMuonKhoaPhieu" : "msgBanCoMuonMoKhoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

        //        SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "UPDATE dbo.YEU_CAU_TUYEN_DUNG SET ID_TT =" + (Convert.ToInt32(cboTinhTrang.EditValue) == 2 ? "3" : "2") + " WHERE ID_YCTD = " + iID_YCTD + "");
        //        cboTinhTrang.EditValue = Convert.ToInt32(cboTinhTrang.EditValue) == 2 ? 3 : 2;
        //        //update trạng thái vào đây
        //    }
        //}
        private void cboTrangThai_EditValueChanged(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0FS";
            int idFC_CU = Convert.ToInt32(grvPYC.GetFocusedRowCellValue("ID_YCTD"));
            DataTable dt = new DataTable();
            dt = (DataTable)grdPYC.DataSource;
            if (dt == null) return;
            try
            {
                dt.DefaultView.RowFilter = "ID_TT = " + cboTrangThai.EditValue;
                //_view.SelectRow(0);
            }
            catch
            {
                dt.DefaultView.RowFilter = "1 = 0";
            }
            //DataView dtTemp = new DataView();
            //dtTemp = (DataView)dt.DefaultView.Table.Copy;
            try
            {

                DataTable dtTemp = new DataTable();
                dtTemp = dt.DefaultView.ToTable();
                dtTemp.PrimaryKey = new DataColumn[] { dtTemp.Columns["ID_YCTD"] };

                int index = dtTemp.Rows.IndexOf(dtTemp.Rows.Find(idFC_CU));
                grvPYC.FocusedRowHandle = grvPYC.GetRowHandle(index);
                grvPYC.ClearSelection();
                grvPYC.SelectRow(index);
            }
            catch { }
            Commons.Modules.sLoad = "";
            //grvPYC.SetFocusedRowCellValue("ID_YCTD", idFC_CU);
            //Commons.Modules.ObjSystems.RowFilter(grdPYC, grvPYC.Columns["ID_TT"], (cboTrangThai.EditValue).ToString());
            grvPYC_FocusedRowChanged(null, null);
        }
        #region function Kiểm tra
        private bool KiemTraLuoi(DataTable dtSource)
        {
            int count = grvViTri.RowCount;
            int col = 0;
            int errorCount = 0;
            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                //Số lượng tuyển
                if (!KiemDuLieuSo(grvViTri, dr, "SL_TUYEN", grvViTri.Columns["SL_TUYEN"].FieldName.ToString(), 0, 0, true, this.Name))
                {
                    errorCount++;
                }
            }
            #endregion
            Commons.Modules.ObjSystems.HideWaitForm();
            if (errorCount != 0)
            {
                //XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool KiemDuLieuSo(GridView grvData, DataRow dr, string sCot, string sTenKTra, double GTSoSanh, double GTMacDinh, Boolean bKiemNull, string sForm)
        {
            string sDLKiem;
            sDLKiem = dr[sCot].ToString();
            double DLKiem;
            if (bKiemNull)
            {
                if (string.IsNullOrEmpty(sDLKiem))
                {
                    dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongduocTrong"));
                    return false;
                }
                else
                {
                    if (!double.TryParse(dr[sCot].ToString(), out DLKiem))
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongPhaiSo"));
                        return false;
                    }
                    else
                    {
                        if (GTSoSanh != -999999)
                        {
                            if (DLKiem < GTSoSanh)
                            {
                                dr.SetColumnError(sCot, sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongNhoHon") + GTSoSanh.ToString());
                                dr["XOA"] = 1;
                                return false;
                            }

                            DLKiem = Math.Round(DLKiem, 8);
                            dr[sCot] = DLKiem.ToString();

                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(sDLKiem) && GTMacDinh != -999999)
                {
                    dr[sCot] = GTMacDinh;
                    DLKiem = GTMacDinh;
                    sDLKiem = GTMacDinh.ToString();
                }

                if (!string.IsNullOrEmpty(sDLKiem))
                {
                    if (!double.TryParse(dr[sCot].ToString(), out DLKiem))
                    {
                        dr.SetColumnError(sCot, sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongPhaiSo"));
                        return false;
                    }
                    else
                    {
                        if (GTSoSanh != -999999)
                        {
                            if (DLKiem < GTSoSanh)
                            {
                                dr.SetColumnError(sCot, sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongNhoHon") + GTSoSanh.ToString());
                                return false;
                            }

                            DLKiem = Math.Round(DLKiem, 8);
                            dr[sCot] = DLKiem.ToString();
                        }

                    }
                }


            }



            return true;
        }
        #endregion
        private void grvPYC_RowCountChanged(object sender, EventArgs e)
        {
            grvPYC_FocusedRowChanged(null, null);
        }
        private void grvFileDK_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
        private void grvFileDK_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;

        }
        private void grvFileDK_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            grvFileDK.ClearColumnErrors();
            DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            try
            {
                DataTable dt = new DataTable();
                if (grvFileDK == null) return;

                if (string.IsNullOrEmpty(view.GetRowCellValue(e.RowHandle, "DUONG_DAN").ToString()))
                {
                    e.Valid = false;
                    e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erDuongDanKhongTrong");
                    grvFileDK.SetColumnError(grvFileDK.Columns["DUONG_DAN"], e.ErrorText);
                    return;
                }
                else
                {
                    dt = new DataTable();
                    dt = Commons.Modules.ObjSystems.ConvertDatatable(grvFileDK);
                    if (dt.AsEnumerable().Count(x => x["DUONG_DAN"].Equals(view.GetRowCellValue(e.RowHandle, "DUONG_DAN").ToString())) > 1)
                    {
                        e.Valid = false;
                        e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erDuongDanTrungDuLieu");
                        grvFileDK.SetColumnError(grvFileDK.Columns["DUONG_DAN"], e.ErrorText);
                        return;
                    }
                }
            }
            catch
            { }
        }
        private void grvViTri_RowCountChanged(object sender, EventArgs e)
        {
            try
            {


                if (btnALL.Buttons[9].Properties.Visible == false)
                {
                    if (grvViTri.RowCount > 1)
                    {
                        cboBPYC.Properties.ReadOnly = true;
                    }
                    else
                    {
                        cboBPYC.Properties.ReadOnly = false;
                    }
                }
            }
            catch
            {
            }
        }
        private void munThongBaoTuyenDung_Click(object sender, EventArgs e)
        {
            frmThongBaoTuyenDung frm = new frmThongBaoTuyenDung();
            frm.iID_YCTD = Convert.ToInt64(grvViTri.GetFocusedRowCellValue("ID_YCTD"));
            frm.iID_LCV = Convert.ToInt64(grvViTri.GetFocusedRowCellValue("ID_LCV"));
            frm.SoYC = txtMA_YCTD.Text;
            frm.ShowDialog();
        }
        private void grvViTri_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            //khi không sữa mới them
            if (btnALL.Buttons[9].Properties.Visible == true)
            {
                try
                {
                    if (e.HitInfo.InDataRow)
                    {
                        contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                    }
                    else
                    {
                        contextMenuStrip1.Hide();
                    }
                }
                catch
                {
                }
            }
        }
        private void grvViTri_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "ID_LCV")
                {
                    if (Commons.Modules.sLoad == "0Load") return;
                    try
                    {
                        int n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*)  FROM  dbo.MGetListNhanSuFormToDate('" + Commons.Modules.UserName + "'," + Commons.Modules.TypeLanguage + ",-1,-1,-1,GETDATE(),GETDATE()) WHERE ID_LCV = " + e.Value + ""));
                        grvViTri.SetFocusedRowCellValue("SL_HC", n);
                    }
                    catch
                    {
                        grvViTri.SetFocusedRowCellValue("SL_HC", 0);
                    }

                    try
                    {
                        int n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT  [dbo].[fnGetSoLuongDBLD]((SELECT TOP 1 ID_DV FROM dbo.XI_NGHIEP WHERE ID_XN = "+ cboBPYC.EditValue +"),"+ e.Value +",GETDATE())"));
                        grvViTri.SetFocusedRowCellValue("SL_DINH_BIEN", n);
                    }
                    catch { grvViTri.SetFocusedRowCellValue("SL_DINH_BIEN", 0); }
                    Commons.Modules.sLoad = "0Load";
                    grvViTri.SetFocusedRowCellValue("ID_LCV", Convert.ToInt64(e.Value));
                    Commons.Modules.sLoad = "";
                }
                if (e.Column.FieldName == "SL_TUYEN")
                {
                    try
                    {
                        grvViTri.SetFocusedRowCellValue("SL_DAT", 0);
                        grvViTri.SetFocusedRowCellValue("SL_CL", e.Value);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }

        }
        private void grvViTri_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "ID_LOAI_TUYEN")
                {
                    if (Commons.Modules.sLoad == "0Load") return;
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
            catch
            {
            }
        }

        private void cboBPYC_BeforePopup(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT  ID_XN,T.TEN_XN FROM (SELECT DISTINCT XN.STT_DV, XN.STT_XN, T1.ID_XN, XN.TEN_XN FROM dbo.LOAI_CONG_VIEC_XI_NGHIEP T1 INNER JOIN(SELECT DISTINCT ID_XN, TEN_XN, STT_XN, STT_DV FROM MGetToUser('" + Commons.Modules.UserName + "', " + Commons.Modules.TypeLanguage + ")) XN ON XN.ID_XN = T1.ID_XN)AS T ORDER BY T.STT_DV, T.STT_XN"));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboBPYC, Commons.Modules.ObjSystems.DataXiNghiep(-1, false), "ID_XN", "TEN_XN", "TEN_XN", true, true);
            cboBPYC.EditValue = -99;
        }

        private void grvViTri_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!dxValidationProvider1.Validate())
            {
                e.Cancel = true;
                grvThayThe.DeleteSelectedRows();
                return;
            }

        }
    }
}
