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
        private long iID_CN = -1;
        private int iAdd = 0;
        public AccordionControl accorMenuleft;
        private ucCTQLNS ucNS;
        private string ChuoiKT = "";
        string strDuongDan = "";
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
                Commons.OSystems.SetDateEditFormat(datTNgay);
                Commons.OSystems.SetDateEditFormat(datDNgay);
                Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
                Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
                Commons.Modules.sLoad = "";
                datTNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year));

                enabel(true);
                btnALL.Buttons[0].Properties.Visible = false;
                btnALL.Buttons[1].Properties.Visible = false;
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
        private void LoadData()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                switch (Commons.Modules.KyHieuDV){
                    case "DM":
                        {
                           
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetListCNChuaCoHD", conn);
                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDV.EditValue);
                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXN.EditValue);
                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                            cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datTNgay.Text);
                            cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datDNgay.Text);
                            cmd.Parameters.Add("@iCot1", SqlDbType.BigInt).Value = -1;
                            cmd.Parameters.Add("@iCot2", SqlDbType.BigInt).Value = -1;
                            cmd.Parameters.Add("@Them", SqlDbType.Int).Value = rdoChonXem.SelectedIndex;
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                       
                            adp.Fill(ds);
                            break;
                    }
                    case "NB":
                        {
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetListCNChuaCoHD_NB", conn);
                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDV.EditValue);
                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXN.EditValue);
                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                            cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datTNgay.Text);
                            cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datDNgay.Text);
                            cmd.Parameters.Add("@iCot1", SqlDbType.BigInt).Value = -1;
                            cmd.Parameters.Add("@iCot2", SqlDbType.BigInt).Value = -1;
                            cmd.Parameters.Add("@Them", SqlDbType.Int).Value = rdoChonXem.SelectedIndex;
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                            adp.Fill(ds);
                            break;
                        }
                    case "NC":
                        {
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetListCNChuaCoHD_NB", conn);
                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDV.EditValue);
                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXN.EditValue);
                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                            cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datTNgay.Text);
                            cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datDNgay.Text);
                            cmd.Parameters.Add("@iCot1", SqlDbType.BigInt).Value = -1;
                            cmd.Parameters.Add("@iCot2", SqlDbType.BigInt).Value = -1;
                            cmd.Parameters.Add("@Them", SqlDbType.Int).Value = rdoChonXem.SelectedIndex;
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                            adp.Fill(ds);
                            break;
                        }
                }
               
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.Columns["CHON"].ReadOnly = false;
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CN"] };
                if (rdoChonXem.SelectedIndex == 0)
                {
                    dt.Columns["TAI_LIEU"].ReadOnly = true;
                }
                else
                {
                    dt.Columns["TAI_LIEU"].ReadOnly = false;
                }
                //DataTable dt = new DataTable();
                //dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spTiepNhanUV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToInt64(cboID_PV.EditValue)));

                if (grdDSUngVien.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSUngVien, grvDSUngVien, dt, true, true, false, true, true, this.Name);
                    grvDSUngVien.BestFitColumns();
                    grvDSUngVien.Columns["CHON"].Visible = false;
                    grvDSUngVien.Columns["CONG_VIEC_ENG"].Visible = false;
                    grvDSUngVien.Columns["MO_TA_CV"].Visible = false;
                    grvDSUngVien.Columns["MO_TA_CV_A"].Visible = false;
                    grvDSUngVien.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                    grvDSUngVien.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                    grvDSUngVien.Columns["CONG_VIEC"].OptionsColumn.AllowEdit = false;
                    grvDSUngVien.Columns["SO_HDLD"].OptionsColumn.AllowEdit = false;
                    //grvDSUngVien.Columns["TEN_LHDLD"].OptionsColumn.AllowEdit = false;
                    grvDSUngVien.Columns["MO_TA_CV"].OptionsColumn.AllowEdit = false;
                    grvDSUngVien.Columns["MO_TA_CV_A"].OptionsColumn.AllowEdit = false;
                    grvDSUngVien.Columns["NGAY_VAO_LAM"].OptionsColumn.AllowEdit = false;
                    grvDSUngVien.Columns["MS_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvDSUngVien.Columns["HO_TEN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvDSUngVien.Columns["NGAY_VAO_LAM"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvDSUngVien.Columns["LUONG_THU_VIEC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvDSUngVien.Columns["LUONG_THU_VIEC"].DisplayFormat.FormatString = "n0";
                    grvDSUngVien.Columns["PHU_CAP"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvDSUngVien.Columns["PHU_CAP"].DisplayFormat.FormatString = "n0";
                }
                else
                {
                    grdDSUngVien.DataSource = dt;
                }

                if (iID_CN != -1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(iID_CN));
                    grvDSUngVien.FocusedRowHandle = grvDSUngVien.GetRowHandle(index);
                    grvDSUngVien.ClearSelection();
                }


                if (rdoChonXem.SelectedIndex == 0)
                {
                    RepositoryItemButtonEdit btnEdit = new RepositoryItemButtonEdit();
                    grvDSUngVien.Columns["TAI_LIEU"].ColumnEdit = btnEdit;
                    btnEdit.ButtonClick += BtnEdit_ButtonClick;

                    grvDSUngVien.Columns["ID_HDLD"].Visible = false;
                    grvDSUngVien.Columns["ID_UV"].Visible = false;
                    for (int i = 0; i < grvDSUngVien.Columns.Count; i++)
                    {
                        grvDSUngVien.Columns[grvDSUngVien.Columns[i].FieldName.ToString()].OptionsColumn.AllowEdit = false;
                    }
                    grvDSUngVien.Columns["TAI_LIEU"].OptionsColumn.AllowEdit = true;
                    grvDSUngVien.OptionsSelection.MultiSelect = false;
                    grvDSUngVien.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;

                }
                else
                {
                    RepositoryItemButtonEdit btnEdit = new RepositoryItemButtonEdit();
                    grvDSUngVien.Columns["TAI_LIEU"].ColumnEdit = btnEdit;
                    btnEdit.ButtonClick += BtnEdit_ButtonClick;

                    dt.Columns["TEN_DV"].ReadOnly = true;
                    grvDSUngVien.Columns["ID_CN"].Visible = false;
                    grvDSUngVien.OptionsSelection.MultiSelect = true;
                    grvDSUngVien.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;

                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_LHDLD = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                    cboID_LHDLD.NullText = "";
                    cboID_LHDLD.ValueMember = "ID_LHDLD";
                    cboID_LHDLD.DisplayMember = "TEN_LHDLD";
                    //ID_VTTD,TEN_VTTD
                    //DataTable dt1 = new DataTable();
                    //dt1.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_LHDLD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN TEN_LHDLD ELSE ISNULL(NULLIF(TEN_LHDLD_A,''),TEN_LHDLD) END TEN_LHDLD FROM dbo.LOAI_HDLD WHERE ID_TT_HD = 3"));
                    dt = ds.Tables[1].Copy();
                    cboID_LHDLD.DataSource = dt;
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
                    dt = ds.Tables[2].Copy();
                    cboID_NK.DataSource = dt;
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
                    dt = ds.Tables[3].Copy();
                    cboID_CV.DataSource = dt;
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
                    dt = ds.Tables[6].Copy();
                    cboID_TT.DataSource = dt;
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
                if (!btnALL.Buttons[2].Properties.Visible)
                {
                    ButtonEdit a = sender as ButtonEdit;
                    ofileDialog.Filter = "All Files|*.txt;*.docx;*.doc;*.pdf*.xls;*.xlsx;*.pptx;*.ppt|Text File (.txt)|*.txt|Word File (.docx ,.doc)|*.docx;*.doc|Spreadsheet (.xls ,.xlsx)|  *.xls ;*.xlsx";
                    //ofileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm|Word Documents(*.doc)|*.doc";
                    if (ofileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string sduongDan = ofileDialog.FileName.ToString().Trim();
                        if (ofileDialog.FileName.ToString().Trim() == "") return;
                        var strDuongDanTmp = Commons.Modules.ObjSystems.CapnhatTL("Tai_Lieu_HD\\HDTV" + '\\' + grvDSUngVien.GetFocusedRowCellValue("MS_CN"), false);
                        strDuongDan = ofileDialog.FileName;
                        string[] sFile;
                        string TenFile;
                        TenFile = ofileDialog.SafeFileName.ToString();
                        sFile = System.IO.Directory.GetFiles(strDuongDanTmp);
                        if (Commons.Modules.ObjSystems.KiemFileTonTai(strDuongDanTmp + @"\" + ofileDialog.SafeFileName.ToString()) == false)
                            a.Text = strDuongDanTmp + @"\" + ofileDialog.SafeFileName.ToString();
                        else
                        {
                            TenFile = Commons.Modules.ObjSystems.STTFileCungThuMuc(strDuongDanTmp, TenFile);
                            a.Text = strDuongDanTmp + @"\" + TenFile;
                        }
                        Commons.Modules.ObjSystems.LuuDuongDan(strDuongDan, a.Text);

                    }
                }
                else
                {
                    Commons.Modules.ObjSystems.OpenHinh(grvDSUngVien.GetFocusedRowCellValue("TAI_LIEU").ToString());
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgBanKhongCoQuyenTruyCapDD"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgfrmThongBao"), MessageBoxButtons.OK);
            }
        }
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
                DataTable dt1 = new DataTable();
                dt1.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_LHDLD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN TEN_LHDLD ELSE ISNULL(NULLIF(TEN_LHDLD_A,''),TEN_LHDLD) END TEN_LHDLD FROM dbo.LOAI_HDLD WHERE ID_TT_HD = 3"));
                lookUp.Properties.DataSource = dt1;
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
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataChucVu(false, Convert.ToInt32(-1));
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
            }
            catch { }
        }
        private void enabel(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = visible;
            btnALL.Buttons[3].Properties.Visible = !visible;
            btnALL.Buttons[4].Properties.Visible = !visible;
            btnALL.Buttons[5].Properties.Visible = visible;

            //grvDSUngVien.OptionsBehavior.Editable = true;
        }
        private void LayDuongDan()
        {
            //string strPath_DH = txtTaiLieu.Text;
            //strDuongDan = ofileDialog.FileName;
            ////Commons.Modules.ObjSystems.LuuDuongDan(strDuongDan, txtTaiLieu.Text, this.Name.Replace("uc", "") + '\\' + grvCongNhan.GetFocusedRowCellValue("MS_CN"));
            //var strDuongDanTmp = Commons.Modules.ObjSystems.CapnhatTL("Tai_Lieu_TV" + '\\' + grvCongNhan.GetFocusedRowCellValue("MS_CN"), false);
            //string[] sFile;
            //string TenFile;

            //TenFile = ofileDialog.SafeFileName.ToString();
            //sFile = System.IO.Directory.GetFiles(strDuongDanTmp);

            //if (Commons.Modules.ObjSystems.KiemFileTonTai(strDuongDanTmp + @"\" + ofdfile.SafeFileName.ToString()) == false)
            //    txtTaiLieu.Text = strDuongDanTmp + @"\" + ofdfile.SafeFileName.ToString();
            //else
            //{
            //    TenFile = Commons.Modules.ObjSystems.STTFileCungThuMuc(strDuongDanTmp, TenFile);
            //    txtTaiLieu.Text = strDuongDanTmp + @"\" + TenFile;
            //}
        }

        private void HopDongThuViecAll_NB(DataTable dtTemp)
        {
            DataTable dtbc = new DataTable();
            string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien), "");

                System.Data.SqlClient.SqlConnection conn1;
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDongThuViec_NB(DateTime.Now);
                dtTemp = new DataTable();
                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongThuViec_NB", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = -1;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.BigInt).Value = -1;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBTCongNhan;
                cmd1.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtTemp = new DataTable();
                dtTemp = ds.Tables[0].Copy();
                dtTemp.TableName = "DATA";
                frm.AddDataSource(dtTemp);

                dtbc = new DataTable();
                dtbc = ds.Tables[1].Copy();
                dtbc.TableName = "NOI_DUNG";
                frm.AddDataSource(dtbc);

                Commons.Modules.ObjSystems.XoaTable(sBTCongNhan);

                frm.ShowDialog();
            }
            catch
            {
                Commons.Modules.ObjSystems.XoaTable(sBTCongNhan);
            }
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

                            switch (Commons.Modules.KyHieuDV){
                                case "NB":
                                    {
                                        DataTable dt = new DataTable();
                                        try
                                        {
                                            dt = Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien);
                                            dt.DefaultView.RowFilter = grvDSUngVien.ActiveFilterString.ToString();
                                            dt = dt.DefaultView.ToTable();
                                            HopDongThuViecAll_NB(dt);
                                        }
                                        catch (Exception ex)
                                        {
                                            dt = null;
                                        }
                                        break;
                                    }
                                case "NC":
                                    {
                                        DataTable dt = new DataTable();
                                        try
                                        {
                                            dt = Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien);
                                            dt.DefaultView.RowFilter = grvDSUngVien.ActiveFilterString.ToString();
                                            dt = dt.DefaultView.ToTable();
                                            HopDongThuViecAll_NB(dt);
                                        }
                                        catch (Exception ex)
                                        {
                                            dt = null;
                                        }
                                        break;
                                    }
                                case "DM":
                                    {
                                        DataTable dt = new DataTable();
                                        try
                                        {
                                            dt = Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien);
                                            dt.DefaultView.RowFilter = grvDSUngVien.ActiveFilterString.ToString();
                                            dt = dt.DefaultView.ToTable();
                                        }
                                        catch (Exception ex)
                                        {
                                            dt = null;
                                        }

                                        frmInThuMoi frm = new frmInThuMoi(dt);
                                        frm.ShowDialog();
                                        break;
                                    }
                            }                                                   
                            break;
                        }
                    case "sua":
                        {
                            iAdd = 1;
                            enabel(false);
                            break;
                        }

                    case "xoa":
                        {
                            enabel(true);
                            break;
                        }
                    case "ghi":
                        {
                            if (grvDSUngVien.RowCount == 0)
                                return;
                            grvDSUngVien.CloseEditor();
                            grvDSUngVien.UpdateCurrentRow();
                            DataTable dt_CHON = new DataTable();
                            dt_CHON = ((DataTable)grdDSUngVien.DataSource);
                            if (dt_CHON.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).Count() == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            this.Cursor = Cursors.WaitCursor;
                            if (!KiemTraLuoi(dt_CHON))
                            {
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            this.Cursor = Cursors.Default;
                            if (!SaveData()) return;
                            rdoChonXem.SelectedIndex = 0;
                            rdoChonXem_SelectedIndexChanged(null, null);
                            enabel(true);
                            break;
                        }
                    case "khongghi":
                        {
                            rdoChonXem.SelectedIndex = 0;
                            rdoChonXem_SelectedIndexChanged(null, null);
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
            string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveHopDongThuViec", Commons.Modules.UserName, sBTCongNhan);
                Commons.Modules.ObjSystems.XoaTable(sBTCongNhan);
                return true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBTCongNhan);
                return false;
            }
        }
        private void cboID_PV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }

        private void grvDSUngVien_RowCountChanged(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                int index = ItemForSumNhanVien.Text.IndexOf(':');
                if (index > 0)
                {
                    if (view.RowCount > 0)
                    {
                        ItemForSumNhanVien.Text = ItemForSumNhanVien.Text.Substring(0, index) + ": " + view.RowCount.ToString();
                    }
                    else
                    {
                        ItemForSumNhanVien.Text = ItemForSumNhanVien.Text.Substring(0, index) + ": 0";
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }
        private void grvDSUngVien_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
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
                iID_CN = Convert.ToInt64(grvDSUngVien.GetFocusedRowCellValue("ID_CN"));
                ucNS = new HRM.ucCTQLNS(Convert.ToInt64(grvDSUngVien.GetFocusedRowCellValue("ID_CN")));
                Commons.Modules.ObjSystems.ShowWaitForm(this);
                ucNS.Refresh();
                ucNS.flag = true;
                ucNS.sTenLab = "labHopDong";
                //ns.accorMenuleft = accorMenuleft;
                dataLayoutControl1.Hide();
                this.Controls.Add(ucNS);
                ucNS.Dock = DockStyle.Fill;
                ucNS.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
                Commons.Modules.ObjSystems.HideWaitForm();
            }
            catch (Exception ex) { }
        }
        public DXMenuItem MCreateMenuCapNhatAll(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblCapNhatAll", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(CapNhatAll));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }
        public void CapNhatAll(object sender, EventArgs e)
        {
            try
            {
                string sCotCN = grvDSUngVien.FocusedColumn.FieldName.ToString();
                try
                {
                    if (grvDSUngVien.GetFocusedRowCellValue(grvDSUngVien.FocusedColumn.FieldName).ToString() == "") return;
                    string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien), "");

                    DataTable dt = new DataTable();
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateChuotPhai_TiepNhan", sBTCongNhan, sCotCN, sCotCN.Substring(0, 4) == "NGAY" ? Convert.ToDateTime(grvDSUngVien.GetFocusedRowCellValue(grvDSUngVien.FocusedColumn.FieldName)).ToString("MM/dd/yyyy") : grvDSUngVien.GetFocusedRowCellValue(grvDSUngVien.FocusedColumn.FieldName)));
                    grdDSUngVien.DataSource = dt;
                    Commons.Modules.ObjSystems.XoaTable(sCotCN);
                }
                catch (Exception ex)
                {
                    Commons.Modules.ObjSystems.XoaTable(sCotCN);
                }
            }
            catch (Exception ex) { }
        }

        // cap nhat hop dong
        public DXMenuItem MCreateMenuCapNhatTT(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblCapNhatTinhTrang", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(CapNhatTT));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }
        public void CapNhatTT(object sender, EventArgs e)
        {
            try
            {
                iID_CN = Convert.ToInt64(grvDSUngVien.GetFocusedRowCellValue("ID_CN"));
                string sSQL = "UPDATE dbo.HOP_DONG_LAO_DONG SET ID_TT = 2 WHERE ID_CN = " + grvDSUngVien.GetFocusedRowCellValue("ID_CN") + " AND ID_HDLD = " + grvDSUngVien.GetFocusedRowCellValue("ID_HDLD") + "";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSQL);
                LoadData();
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgLuuThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK);
            }
            catch (Exception ex) { }
        }
        public void BackWindowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            ucNS.Hide();
            dataLayoutControl1.Show();
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

                    if (grvDSUngVien.FocusedColumn.FieldName.ToString() == "TEN_TT")
                    {
                        DevExpress.Utils.Menu.DXMenuItem itemCapNhatTT = MCreateMenuCapNhatTT(view, irow);
                        e.Menu.Items.Add(itemCapNhatTT);
                    }

                    if (btnALL.Buttons[2].Properties.Visible || btnALL.Buttons[0].Properties.Visible) return;
                    if (grvDSUngVien.FocusedColumn.FieldName.ToString() == "MS_CN" || grvDSUngVien.FocusedColumn.FieldName.ToString() == "HO_TEN") return;
                    DevExpress.Utils.Menu.DXMenuItem itemCapNhatAll = MCreateMenuCapNhatAll(view, irow);
                    e.Menu.Items.Add(itemCapNhatAll);


                    //if (flag == false) return;
                }
            }
            catch
            {
            }
        }

        #endregion

        #endregion

        private void rdoChonXem_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rdoChonXem.SelectedIndex)
            {
                case 0:
                    {
                        grdDSUngVien.DataSource = null;
                        LoadData();
                        enabel(true);
                        btnALL.Buttons[0].Properties.Visible = false;
                        btnALL.Buttons[1].Properties.Visible = false;
                        lblTuNgay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        lblDenNgay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        break;
                    }
                case 1:
                    {
                        grdDSUngVien.DataSource = null;
                        LoadData();
                        enabel(false);
                        lblTuNgay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        lblDenNgay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void datTNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.ConvertDateTime(datTNgay.Text);
            int t = DateTime.DaysInMonth(datTNgay.DateTime.Year, datTNgay.DateTime.Month);
            DateTime secondDateTime = new DateTime(datTNgay.DateTime.Year, datTNgay.DateTime.Month, t);
            datDNgay.EditValue = secondDateTime;
            LoadData();
            Commons.Modules.sLoad = "";
        }

        private void datDNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }

        private void grvDSUngVien_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            DateTime NgayBD_HD;
            DateTime NgayKT_HD;
            double MucLuongChinh;
            try
            {
                var row = view.GetFocusedDataRow();

                if (e.Column.FieldName == "ID_LHDLD")
                {
                    int iNgayTV = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(SO_NGAY,0) SO_NGAY FROM dbo.LOAI_HDLD WHERE ID_LHDLD = " + Convert.ToInt32(grvDSUngVien.GetFocusedRowCellValue("ID_LHDLD")) + ""));
                    NgayBD_HD = Convert.ToDateTime(grvDSUngVien.GetFocusedRowCellValue("NGAY_BD_THU_VIEC"));
                    NgayKT_HD = NgayBD_HD.AddDays(iNgayTV);

                    row["NGAY_BD_THU_VIEC"] = NgayBD_HD;
                    row["NGAY_KT_THU_VIEC"] = NgayKT_HD;
                    //gioBD = DateTime.Parse(row["GIO_BD"].ToString());
                }
                if (e.Column.FieldName == "NGAY_BD_THU_VIEC")
                {
                    int iNgayTV = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(SO_NGAY,0) SO_NGAY FROM dbo.LOAI_HDLD WHERE ID_LHDLD = " + Convert.ToInt32(grvDSUngVien.GetFocusedRowCellValue("ID_LHDLD")) + ""));
                    NgayBD_HD = Convert.ToDateTime(grvDSUngVien.GetFocusedRowCellValue("NGAY_BD_THU_VIEC"));
                    NgayKT_HD = NgayBD_HD.AddDays(iNgayTV);

                    row["NGAY_BD_THU_VIEC"] = NgayBD_HD;
                    row["NGAY_KT_THU_VIEC"] = NgayKT_HD;
                }
            }
            catch (Exception ex) { }
        }

        private void grvDSUngVien_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;
            DevExpress.XtraGrid.Columns.GridColumn ngayBD = view.Columns["NGAY_BD_THU_VIEC"];
            DevExpress.XtraGrid.Columns.GridColumn ngayKT = view.Columns["NGAY_KT_THU_VIEC"];

            if (view.FocusedColumn == view.Columns["NGAY_BD_THU_VIEC"])
            {
                DateTime? fromDate = e.Value as DateTime?;
                DateTime? toDate = view.GetRowCellValue(view.FocusedRowHandle, view.Columns["NGAY_KT_THU_VIEC"]) as DateTime?;
                if (fromDate > toDate)
                {
                    e.Valid = false;
                    view.SetColumnError(ngayBD, "Ngày bắt đầu phải nhỏ hơn ngày kết thúc"); return;
                }
            }
            if (view.FocusedColumn == view.Columns["NGAY_KT_THU_VIEC"])
            {
                DateTime? fromDate = view.GetRowCellValue(view.FocusedRowHandle, view.Columns["NGAY_BD_THU_VIEC"]) as DateTime?;
                DateTime? toDate = e.Value as DateTime?;
                if (fromDate > toDate)
                {
                    e.Valid = false;
                    view.SetColumnError(ngayKT, "Ngày kết thúc phải lớn hơn ngày bắt đầu"); return;
                }
            }
        }
        #region kiemTra
        private bool KiemTraLuoi(DataTable dtSource)
        {
            int count = grvDSUngVien.RowCount;
            int col = 0;
            int errorCount = 0;
            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                if (Convert.ToBoolean(dr["CHON"]) == true)
                {
                    //Số hợp đồng lao động
                    string sMaSo = dr["SO_HDLD"].ToString();
                    if (!KiemDuLieu(grvDSUngVien, dr, "SO_HDLD", true, 250, this.Name))
                    {
                        errorCount++;
                    }
                    if (!KiemTrungDL(grvDSUngVien, dtSource, dr, "SO_HDLD", sMaSo, "HOP_DONG_LAO_DONG", "SO_HDLD", this.Name))
                    {
                        errorCount++;
                    }

                    //loại chức vụ
                    if (!KiemDuLieu(grvDSUngVien, dr, "ID_CV", true, 250, this.Name))
                    {
                        errorCount++;
                    }

                    //Ngày bắt đầu thử việc
                    if (!KiemDuLieuNgay(grvDSUngVien, dr, "NGAY_BD_THU_VIEC", true, this.Name))
                    {
                        errorCount++;
                    }

                    //Ngày kết thúc thử việc
                    if (!KiemDuLieuNgay(grvDSUngVien, dr, "NGAY_KT_THU_VIEC", true, this.Name))
                    {
                        errorCount++;
                    }

                    //LUONG_THU_VIEC
                    if (!KiemDuLieuSo(grvDSUngVien, dr, "LUONG_THU_VIEC", grvDSUngVien.Columns["LUONG_THU_VIEC"].FieldName.ToString(), 0, 0, true, this.Name))
                    {
                        errorCount++;
                    }

                    //CONG_VIEC
                    if (!KiemDuLieu(grvDSUngVien, dr, "CONG_VIEC", true, 250, this.Name))
                    {
                        errorCount++;
                    }
                }
            }
            #endregion
            Commons.Modules.ObjSystems.HideWaitForm();
            if (errorCount != 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuSanSang"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
        }
        public bool KiemDuLieu(GridView grvData, DataRow dr, string sCot, Boolean bKiemNull, int iDoDaiKiem, string sform)
        {
            string sDLKiem;
            try
            {
                sDLKiem = dr[sCot].ToString();
                if (bKiemNull)
                {
                    if (string.IsNullOrEmpty(sDLKiem))
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongDuocTrong"));
                        return false;
                    }
                    else
                    {
                        if (KiemKyTu(sDLKiem, ChuoiKT))  //KiemKyTu
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgCoChuaKyTuDB"));
                            return false;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(sDLKiem))
                    {
                        if (KiemKyTu(sDLKiem, ChuoiKT))  //KiemKyTu
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgCoChuaKyTuDB"));
                            dr["XOA"] = 1;
                            return false;
                        }
                    }
                }
                if (iDoDaiKiem != 0)
                {
                    if (sDLKiem.Length > iDoDaiKiem)
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgDoDaiKyTuVuocQua " + iDoDaiKiem));
                        return false;
                    }
                }
            }
            catch
            {
                dr.SetColumnError(sCot, "error");
                return false;
            }
            return true;
        }
        public bool KiemKyTu(string strInput, string strChuoi)
        {

            if (strChuoi == "") strChuoi = ChuoiKT;

            for (int i = 0; i < strInput.Length; i++)
            {
                for (int j = 0; j < strChuoi.Length; j++)
                {
                    if (strInput[i] == strChuoi[j])
                    {
                        return true;
                    }
                }
            }
            if (strInput.Contains("//"))
            {
                return true;
            }
            return false;
        }
        public bool KiemDuLieuNgay(GridView grvData, DataRow dr, string sCot, Boolean bKiemNull, string sform)
        {
            string sDLKiem;
            sDLKiem = dr[sCot].ToString();
            DateTime DLKiem;

            try
            {

                if (bKiemNull)
                {
                    if (string.IsNullOrEmpty(sDLKiem))
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongduocTrong"));
                        return false;
                    }
                    else
                    {
                        //sDLKiem = DateTime.ParseExact(sDLKiem, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString();
                        if (!DateTime.TryParse(sDLKiem, out DLKiem))
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
                            return false;
                        }

                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(sDLKiem))
                    {
                        if (!DateTime.TryParse(sDLKiem, out DLKiem))
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
                            return false;
                        }
                    }
                }
            }
            catch
            {
                dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
                return false;
            }
            return true;
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
        public bool KiemTrungDL(GridView grvData, DataTable dt, DataRow dr, string sCot, string sDLKiem, string tabName, string ColName, string sform)
        {
            string sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDL");
            try
            {

                if (dt.AsEnumerable().Where(x => x.Field<string>(sCot).Trim().Equals(sDLKiem)).CopyToDataTable().Rows.Count > 1)
                {
                    sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDLLuoi");
                    dr.SetColumnError(sCot, sTenKTra);
                    return false;
                }
                else
                {
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.[" + tabName + "] WHERE " + ColName + " = N'" + sDLKiem + "'")) > 0)
                    {

                        sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDLCSDL");
                        dr.SetColumnError(sCot, sTenKTra);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                dr.SetColumnError(sCot, sTenKTra);
                return false;
            }
        }
        #endregion
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grvDSUngVien_RowCountChanged_1(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                int index = ItemForSumNhanVien.Text.IndexOf(':');
                if (view.RowCount > 0)
                {
                    ItemForSumNhanVien.Text = ItemForSumNhanVien.Text.Substring(0, index) + ": " + view.RowCount.ToString();
                }
                else
                {
                    ItemForSumNhanVien.Text = ItemForSumNhanVien.Text.Substring(0, index) + ": 0";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
