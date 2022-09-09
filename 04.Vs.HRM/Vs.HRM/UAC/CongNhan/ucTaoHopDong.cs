using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucTaoHopDong : DevExpress.XtraEditors.XtraUserControl
    {
        private bool flag = false;
        private int iAdd = 0;
        public AccordionControl accorMenuleft;
        private ucCTQLNS ucNS;
        public ucTaoHopDong()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, btnALL);
        }
        #region even
        private void ucTaoHopDong_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                LoadThang();
                Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
                Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
                Commons.Modules.sLoad = "";
                LoadData();
                enabel(true);
            }
            catch (Exception ex)
            {
            }
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadData();
            Commons.Modules.sLoad = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadData();
            Commons.Modules.sLoad = "";

        }
        private void cboTo_EditValueChanged_1(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadData();
            Commons.Modules.sLoad = "";

        }
        #endregion
        public void LoadThang()
        {
            try
            {
                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT DISTINCT SUBSTRING(CONVERT(VARCHAR(10),NGAY_CHUYEN,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),NGAY_CHUYEN,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),NGAY_CHUYEN,103),10) AS NGAY ,RIGHT(CONVERT(VARCHAR(10),NGAY_CHUYEN,103),7) AS THANG  FROM dbo.UV_CHUYEN_NHAN_SU ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                if (grdThang.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                    grvThang.Columns["M"].Visible = false;
                    grvThang.Columns["Y"].Visible = false;
                    grvThang.Columns["THANG"].Visible = false;
                }
                else
                {
                    grdThang.DataSource = dtthang;
                }
                cboThang.Text = grvThang.GetFocusedRowCellValue("NGAY").ToString();
            }
            catch
            {
                DateTime now = DateTime.Now;
                cboThang.Text = now.ToString("dd/MM/yyyy");
            }
        }
        private void LoadData()
        {
            try
            {
                //DataTable dt = new DataTable();
                //dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spTiepNhanUV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToInt64(cboID_PV.EditValue)));
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetListCNChuaCoHD", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDV.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXN.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                cmd.Parameters.Add("@NGAY_CHUYEN", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                cmd.Parameters.Add("@Them", SqlDbType.Int).Value = iAdd;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.Columns["CHON"].ReadOnly = false;
                dt.Columns["TAI_LIEU"].ReadOnly = true;
                //dt.Columns["XAC_NHAN_DL"].ReadOnly = false;
                //dt.Columns["NGAY_CO_THE_DI_LAM"].ReadOnly = false;
                //dt.Columns["XAC_NHAN_DL"].ReadOnly = false;
                //dt.Columns["NGAY_NHAN_VIEC"].ReadOnly = false;
                //dt.Columns["ID_DGTN"].ReadOnly = false;
                //dt.Columns["XAC_NHAN_DTDH"].ReadOnly = false;
                //dt.Columns["DA_GIOI_THIEU"].ReadOnly = false;
                //dt.Columns["HUY_TUYEN_DUNG"].ReadOnly = false;

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSUngVien, grvDSUngVien, dt, true, true, false, true, true, this.Name);
                grvDSUngVien.BestFitColumns();
                grvDSUngVien.Columns["CHON"].Visible = false;
                grvDSUngVien.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["NGAY_VAO_LAM"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["TAI_LIEU"].OptionsColumn.AllowEdit = true;
                grvDSUngVien.Columns["MS_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvDSUngVien.Columns["HO_TEN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvDSUngVien.Columns["NGAY_VAO_LAM"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvDSUngVien.Columns["MUC_LUONG_CHINH"].DisplayFormat.FormatType = FormatType.Numeric;
                grvDSUngVien.Columns["MUC_LUONG_CHINH"].DisplayFormat.FormatString = "n0";
                grvDSUngVien.Columns["PHU_CAP"].DisplayFormat.FormatType = FormatType.Numeric;
                grvDSUngVien.Columns["PHU_CAP"].DisplayFormat.FormatString = "n0";

                if (iAdd == 0)
                {
                    grvDSUngVien.Columns["ID_HDLD"].Visible = false;
                    grvDSUngVien.Columns["ID_UV"].Visible = false;
                    grvDSUngVien.OptionsSelection.MultiSelect = false;
                    grvDSUngVien.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;

                    RepositoryItemButtonEdit btnEdit = new RepositoryItemButtonEdit();
                    grvDSUngVien.Columns["TAI_LIEU"].ColumnEdit = btnEdit;
                    btnEdit.ButtonClick += BtnEdit_ButtonClick;
                }
                else
                {
                    RepositoryItemButtonEdit btnEdit = new RepositoryItemButtonEdit();
                    grvDSUngVien.Columns["TAI_LIEU"].ColumnEdit = btnEdit;
                    btnEdit.ButtonClick += BtnEdit_ButtonClick;

                    grvDSUngVien.Columns["ID_CN"].Visible = false;
                    grvDSUngVien.OptionsSelection.MultiSelect = true;
                    grvDSUngVien.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;

                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_LHDLD = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                    cboID_LHDLD.NullText = "";
                    cboID_LHDLD.ValueMember = "ID_LHDLD";
                    cboID_LHDLD.DisplayMember = "TEN_LHDLD";
                    //ID_VTTD,TEN_VTTD
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


                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_NK = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                    cboID_NK.NullText = "";
                    cboID_NK.ValueMember = "ID_NK";
                    cboID_NK.DisplayMember = "HO_TEN";
                    //ID_VTTD,TEN_VTTD
                    cboID_NK.DataSource = Commons.Modules.ObjSystems.DataNguoiKy();
                    cboID_NK.Columns.Clear();
                    cboID_NK.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_NK"));
                    cboID_NK.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HO_TEN"));
                    cboID_NK.Columns["HO_TEN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "HO_TEN");
                    cboID_NK.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboID_NK.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    cboID_NK.Columns["ID_NK"].Visible = false;
                    grvDSUngVien.Columns["ID_NK"].ColumnEdit = cboID_NK;
                    cboID_NK.BeforePopup += cboID_NK_BeforePopup;
                    cboID_NK.EditValueChanged += cboID_NK_EditValueChanged;


                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_CV = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                    cboID_CV.NullText = "";
                    cboID_CV.ValueMember = "ID_CV";
                    cboID_CV.DisplayMember = "TEN_CV";
                    //ID_VTTD,TEN_VTTD
                    cboID_CV.DataSource = Commons.Modules.ObjSystems.DataChucVu(false,-1);
                    cboID_CV.Columns.Clear();
                    cboID_CV.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_CV"));
                    cboID_CV.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_CV"));
                    cboID_CV.Columns["TEN_CV"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_CV");
                    cboID_CV.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboID_CV.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    cboID_CV.Columns["ID_CV"].Visible = false;
                    grvDSUngVien.Columns["ID_CV"].ColumnEdit = cboID_CV;
                    cboID_CV.BeforePopup += cboID_CV_BeforePopup;
                    cboID_CV.EditValueChanged += cboID_CV_EditValueChanged;

                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_TT = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                    cboID_TT.NullText = "";
                    cboID_TT.ValueMember = "ID_TT";
                    cboID_TT.DisplayMember = "TenTT";
                    //ID_VTTD,TEN_VTTD
                    cboID_TT.DataSource = Commons.Modules.ObjSystems.DataTinhTrang(false);
                    cboID_TT.Columns.Clear();
                    cboID_TT.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_TT"));
                    cboID_TT.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTT"));
                    cboID_TT.Columns["TenTT"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TenTT");
                    cboID_TT.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboID_TT.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    cboID_TT.Columns["ID_TT"].Visible = false;
                    grvDSUngVien.Columns["ID_TT"].ColumnEdit = cboID_TT;
                    cboID_TT.BeforePopup += cboID_TT_BeforePopup;
                    cboID_TT.EditValueChanged += cboID_TT_EditValueChanged;
                }
                try
                {
                    grvDSUngVien.OptionsSelection.CheckBoxSelectorField = "CHON";
                    grvDSUngVien.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
                catch { }
                //Commons.Modules.ObjSystems.AddCombXtra("ID_DGTN", "TEN_DGTN", "TEN_DGTN", grvDSUngVien, Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false), true, "ID_DGTN", this.Name, true);

                //ID_YCTD,MA_YCTD
            }
            catch (Exception ex) { }
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
                    Commons.Modules.ObjSystems.LuuDuongDan(ofileDialog.FileName, ofileDialog.SafeFileName, this.Name.Replace("uc", "") + '\\' + grvDSUngVien.GetFocusedRowCellValue("SO_HDLD"));
                    a.Text = ofileDialog.SafeFileName;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgBanKhongCoQuyenTruyCapDD"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgfrmThongBao"), MessageBoxButtons.OK);
            }
        }
        //private void LayDuongDan()
        //{
        //    string strPath_DH = txtTaiLieu.Text;
        //    strDuongDan = ofdfile.FileName;

        //    var strDuongDanTmp = Commons.Modules.ObjSystems.CapnhatTL("Tai_Lieu_HD");
        //    string[] sFile;
        //    string TenFile;

        //    TenFile = ofdfile.SafeFileName.ToString();
        //    sFile = System.IO.Directory.GetFiles(strDuongDanTmp);

        //    if (Commons.Modules.ObjSystems.KiemFileTonTai(strDuongDanTmp + @"\" + ofdfile.SafeFileName.ToString()) == false)
        //        txtTaiLieu.Text = strDuongDanTmp + @"\" + ofdfile.SafeFileName.ToString();
        //    else
        //    {
        //        TenFile = Commons.Modules.ObjSystems.STTFileCungThuMuc(strDuongDanTmp, TenFile);
        //        txtTaiLieu.Text = strDuongDanTmp + @"\" + TenFile;
        //    }
        //}
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
        private void cboID_NK_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSUngVien.SetFocusedRowCellValue("ID_NK", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboID_NK_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataNguoiKy();
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

        private void cboID_CV_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSUngVien.SetFocusedRowCellValue("ID_CV", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboID_CV_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataChucVu(false,-1);
            }
            catch { }
        }

        private void cboID_TT_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSUngVien.SetFocusedRowCellValue("ID_TT", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboID_TT_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataTinhTrang(false);
            }
            catch { }
        }

        private void LoadCbo()
        {
            //try
            //{
            //    System.Data.SqlClient.SqlConnection conn;
            //    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            //    conn.Open();
            //    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetComboPV_TheoNgay", conn);

            //    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
            //    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            //    cmd.Parameters.Add("@CoAll", SqlDbType.Bit).Value = true;
            //    cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datTNgay.EditValue);
            //    cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datDNgay.EditValue);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
            //    DataSet ds = new DataSet();
            //    adp.Fill(ds);
            //    DataTable dt = new DataTable();
            //    dt = ds.Tables[0].Copy();
            //    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_PV, dt, "ID_PV", "MA_SO", "MA_SO");
            //    if (dt.Rows.Count == 1)
            //    {
            //        cboID_PV.Properties.DataSource = dt.Clone();
            //        cboID_PV.EditValue = 0;
            //    }
            //}
            //catch { }
        }

        private void enabel(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = visible;
            btnALL.Buttons[3].Properties.Visible = !visible;
            btnALL.Buttons[4].Properties.Visible = !visible;
            btnALL.Buttons[5].Properties.Visible = visible;

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
                    case "InHDThuViec":
                        {
                            DataTable dt = new DataTable();
                            dt = (DataTable)grdDSUngVien.DataSource;
                            frmInThuMoi frm = new frmInThuMoi(dt);
                            frm.ShowDialog();
                            //HopDongThuViecAll_DM();
                            break;
                        }
                    case "sua":
                        {
                            iAdd = 1;
                            LoadData();
                            enabel(false);
                            break;
                        }

                    case "xoa":
                        {
                            grvDSUngVien_FocusedRowChanged(null, null);
                            enabel(true);
                            break;
                        }
                    case "ghi":
                        {
                            if (grvDSUngVien.RowCount == 0)
                                return;
                            DataTable dt_CHON = new DataTable();
                            dt_CHON = ((DataTable)grdDSUngVien.DataSource);
                            if (dt_CHON.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).Count() == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (flag == true) return;
                            if (!SaveData()) return;
                            iAdd = 0;
                            LoadData();
                            grvDSUngVien_FocusedRowChanged(null, null);
                            enabel(true);
                            break;
                        }
                    case "khongghi":
                        {
                            iAdd = 0;
                            LoadData();
                            grvDSUngVien_FocusedRowChanged(null, null);
                            enabel(true);
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

        private void grvDSUngVien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }
        private bool SaveData()
        {
            string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien), "");
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spSaveHopDongThuViec", Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text), sBTCongNhan));
                Commons.Modules.ObjSystems.XoaTable(sBTCongNhan);
                return true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBTCongNhan);
                return false;
            }
        }

        private void grvNoiDung_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                if (grvDSUngVien.RowCount == 0)
                {
                    return;
                }
            }
            catch
            {
            }
        }

        private void cboID_PV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }

        private void grvDSUngVien_RowCountChanged(object sender, EventArgs e)
        {
            grvDSUngVien_FocusedRowChanged(null, null);
        }
        private void calendarControl1_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboThang.Text = calThangc.DateTime.ToString("dd/MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grvThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + cboThang.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                }
                else { }
            }
            catch (Exception ex)
            {
                cboThang.Text = calThangc.DateTime.ToString("dd/MM/yyyy");
            }
            cboThang.ClosePopup();
        }

        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = grvThang.GetFocusedRowCellValue("NGAY").ToString();
            }
            catch { }
            cboThang.ClosePopup();

        }

        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }
        private void grvDSUngVien_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                //int ngay = 0;
                DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                DevExpress.XtraGrid.Columns.GridColumn SO_HDLD = View.Columns["SO_HDLD"];
                DevExpress.XtraGrid.Columns.GridColumn ID_CV = View.Columns["ID_CV"];
                //DevExpress.XtraGrid.Columns.GridColumn MS_CN = View.Columns["MS_CN"];
                //DevExpress.XtraGrid.Columns.GridColumn MS_THE_CC = View.Columns["MS_THE_CC"];
                //DevExpress.XtraGrid.Columns.GridColumn ngayvaolam = View.Columns["NGAY_VAO_LAM_LAI"];
                //if (View.GetRowCellValue(e.RowHandle, mslydovang).ToString() == "")
                //{
                //    e.Valid = false;
                //    View.SetColumnError(mslydovang, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraTenUserNULL", Commons.Modules.TypeLanguage)); return;
                //}
                if (View.GetRowCellValue(e.RowHandle, SO_HDLD).ToString() == "")
                {
                    flag = true;
                    e.Valid = false;
                    View.SetColumnError(SO_HDLD, "Số hợp đồng lao động không được trống"); return;
                }
                if (View.GetRowCellValue(e.RowHandle, ID_CV).ToString() == "")
                {
                    flag = true;
                    e.Valid = false;
                    View.SetColumnError(ID_CV, "Chức vụ không được trống"); return;
                }
                flag = false;

                //CheckDuplicateKHNP(grvKHNP, (DataTable)grdKHNP.DataSource, e);
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

        #region function 

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
        //Thong tin nhân sự
        public DXMenuItem MCreateMenuThongTinNS(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblThongTinNS", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(ThongTinNS));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }
        public void ThongTinNS(object sender, EventArgs e)
        {
            try
            {
                ucNS = new HRM.ucCTQLNS(Convert.ToInt64(grvDSUngVien.GetFocusedRowCellValue("ID_CN")));
                Commons.Modules.ObjSystems.ShowWaitForm(this);
                ucNS.Refresh();

                //ns.accorMenuleft = accorMenuleft;
                tableLayoutPanel1.Hide();
                this.Controls.Add(ucNS);
                ucNS.Dock = DockStyle.Fill;
                ucNS.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
                Commons.Modules.ObjSystems.HideWaitForm();
            }
            catch (Exception ex) { }
        }
        public void BackWindowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            ucNS.Hide();
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
                    DevExpress.Utils.Menu.DXMenuItem itemTTNS = MCreateMenuThongTinNS(view, irow);
                    e.Menu.Items.Add(itemTTNS);
                    //if (flag == false) return;
                }
            }
            catch
            {
            }
        }

        #endregion
        #endregion

    }
}
