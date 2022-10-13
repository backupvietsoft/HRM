using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vs.Report;

namespace Vs.HRM
{
    public partial class frmTaoHDLD : DevExpress.XtraEditors.XtraForm
    {
        public DataTable dt_temp;
        private string ChuoiKT = "";
        public frmTaoHDLD()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, windowsUIButton);
        }

        #region sự kiện form
        private void frmTaoHDLD_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            }
            catch { }


        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                if (btn == null || btn.Tag == null) return;
                switch (btn.Tag.ToString())
                {

                    case "luu":
                        {
                            grvData.CloseEditor();
                            grvData.UpdateCurrentRow();
                            DataTable dt_CHON = new DataTable();
                            dt_CHON = ((DataTable)grdData.DataSource);
                            //dt_CHON = Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien);
                            if (dt_CHON.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).Count() == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            DataTable dt = new DataTable();
                            dt = (DataTable)grdData.DataSource;
                            if (!KiemTraLuoi(dt)) return;
                            if (SaveData(dt) == false)
                            {
                                return;
                            }
                            else
                            {
                                DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTaoHopDongThanhCongBanCoMuonInHopDong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (res == DialogResult.No) return;
                                InHDLD("sBTTaoHDLD" + Commons.Modules.iIDUser);
                            }
                            break;
                        }
                    case "in":
                        {
                            DataTable dt_CHON = new DataTable();
                            dt_CHON = ((DataTable)grdData.DataSource);
                            //dt_CHON = Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien);
                            if (dt_CHON.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).Count() == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            InHDLD("sBTTaoHDLD" + Commons.Modules.iIDUser);
                            break;
                        }
                    case "thoat":
                        {
                            Commons.Modules.ObjSystems.XoaTable("sBTTaoHDLD" + Commons.Modules.iIDUser);
                            this.Close();
                            break;
                        }
                    default:
                        break;
                }
            }
            catch
            {
                this.Cursor = Cursors.Default;
            }

        }

        #endregion

        #region hàm load form
        //hàm load gridview
        private void LoadData()
        {
            try
            {
                string sBT = "sBTCongNhan" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, dt_temp, "");
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTaoHDLD", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@sBT1", SqlDbType.NVarChar).Value = sBT;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@NAM", SqlDbType.NVarChar).Value = DateTime.Now.Year.ToString();
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.Columns["CHON"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, true, false, true, true, this.Name);
                //grdDSCongNhan.DataSource = dtTmp;
                grvData.Columns["ID_CN"].Visible = false;
                grvData.Columns["ID_CV"].Visible = false;
                grvData.Columns["CHON"].Visible = false;
                grvData.Columns["ID_TT"].Visible = false;
                grvData.Columns["CONG_VIEC_ENG"].Visible = false;
                grvData.Columns["MO_TA_CV_BHXH"].Visible = false;
                grvData.Columns["MO_TA_CV_BHXH_A"].Visible = false;
                grvData.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                grvData.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                grvData.Columns["SO_HDLD"].OptionsColumn.AllowEdit = false;
                grvData.Columns["NGAY_BAT_DAU_HD"].OptionsColumn.AllowEdit = false;
                grvData.Columns["NGAY_HET_HD"].OptionsColumn.AllowEdit = false;
                grvData.Columns["DIA_CHI_NOI_LAM_VIEC"].OptionsColumn.AllowEdit = false;
                grvData.Columns["TEN_CV"].OptionsColumn.AllowEdit = false;
                grvData.Columns["DIA_DIEM_LAM_VIEC"].OptionsColumn.AllowEdit = false;
                grvData.Columns["HD_GIA_HAN"].OptionsColumn.AllowEdit = false;
                grvData.Columns["CONG_VIEC"].OptionsColumn.AllowEdit = false;
                grvData.Columns["TEN_TT"].OptionsColumn.AllowEdit = false;

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
                grvData.Columns["ID_LHDLD"].ColumnEdit = cboID_LHDLD;
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
                grvData.Columns["ID_NK"].ColumnEdit = cboID_NK;
                cboID_NK.BeforePopup += cboID_NK_BeforePopup;
                cboID_NK.EditValueChanged += cboID_NK_EditValueChanged;

                try
                {
                    grvData.OptionsSelection.CheckBoxSelectorField = "CHON";
                    grvData.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
                catch { }
            }
            catch (Exception ex) { }
        }

        private void cboID_LHDLD_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvData.SetFocusedRowCellValue("ID_LHDLD", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboID_LHDLD_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                DataTable dt1 = new DataTable();
                dt1.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_LHDLD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN TEN_LHDLD ELSE ISNULL(NULLIF(TEN_LHDLD_A,''),TEN_LHDLD) END TEN_LHDLD FROM dbo.LOAI_HDLD WHERE ID_TT_HD <> 3"));
                lookUp.Properties.DataSource = dt1;
            }
            catch { }
        }
        private void cboID_NK_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvData.SetFocusedRowCellValue("ID_NK", Convert.ToInt64((dataRow.Row[0])));
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
        #endregion

        #region hàm sử lý data
        //hàm sử lý khi lưu dữ liệu(thêm/Sửa)
        private bool SaveData(DataTable dt)
        {
            try
            {
                string sBT = "sBTTaoHDLD" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, dt, "");
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTaoHDLD", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@sBT1", SqlDbType.NVarChar).Value = sBT;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                //Commons.Modules.ObjSystems.XoaTable(sBT);
                return true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable("sBTTaoHDLD" + Commons.Modules.iIDUser);
                return false;
            }
        }
        #endregion

        private void frmTaoHDLD_FormClosing(object sender, FormClosingEventArgs e)
        {
            Commons.Modules.ObjSystems.XoaTable("sBTTaoHDLD" + Commons.Modules.iIDUser);
            DialogResult = DialogResult.OK;
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            DateTime NgayBD_HD;
            object NgayKT_HD;
            double MucLuongChinh;
            try
            {
                var row = view.GetFocusedDataRow();

                if (e.Column.FieldName == "ID_LHDLD")
                {
                    int iThang = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(SO_THANG,0) SO_THANG FROM dbo.LOAI_HDLD WHERE ID_LHDLD = " + Convert.ToInt32(grvData.GetFocusedRowCellValue("ID_LHDLD")) + ""));
                    NgayBD_HD = Convert.ToDateTime(grvData.GetFocusedRowCellValue("NGAY_BAT_DAU_HD"));
                    if (iThang != 0)
                    {
                        NgayKT_HD = NgayBD_HD.AddYears(1).AddDays(1);
                    }
                    else
                    {
                        NgayKT_HD = null;
                    }

                    row["NGAY_BAT_DAU_HD"] = NgayBD_HD;
                    row["NGAY_HET_HD"] = NgayKT_HD == null ? (object)DBNull.Value : Convert.ToDateTime(NgayKT_HD);
                    //gioBD = DateTime.Parse(row["GIO_BD"].ToString());
                }
            }
            catch (Exception ex) { }
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

        private bool KiemTraLuoi(DataTable dtSource)
        {
            this.Cursor = Cursors.WaitCursor;
            int count = grvData.RowCount;
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
                    if (!KiemDuLieu(grvData, dr, "SO_HDLD", true, 250, this.Name))
                    {
                        errorCount++;
                    }
                    if (!KiemTrungDL(grvData, dtSource, dr, "SO_HDLD", sMaSo, "HOP_DONG_LAO_DONG", "SO_HDLD", this.Name))
                    {
                        errorCount++;
                    }

                    if (!KiemDuLieuSo(grvData, dr, "MUC_LUONG", grvData.Columns["MUC_LUONG"].FieldName.ToString(), 0, 0, true, this.Name))
                    {
                        errorCount++;
                    }

                    if (!KiemDuLieu(grvData, dr, "CONG_VIEC", true, 250, this.Name))
                    {
                        errorCount++;
                    }
                    // nguoi ky
                    if (!KiemDuLieu(grvData, dr, "ID_NK", true, 250, this.Name))
                    {
                        errorCount++;
                    }
                }
            }
            #endregion
            Commons.Modules.ObjSystems.HideWaitForm();
            if (errorCount != 0)
            {
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                this.Cursor = Cursors.Default;
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
        private void InHDLD(string sBT)
        {

            DataTable dt = new DataTable();
            DataTable dtbc = new DataTable();
            try
            {
                System.Data.SqlClient.SqlConnection conn1;
                dt = new DataTable();
                frmViewReport frm = new frmViewReport();
                frm.rpt = new rptHopDongLaoDong_DM(DateTime.Now);

                conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn1.Open();

                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptHopDongLaoDong_DM", conn1);
                cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd1.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = -1;
                cmd1.Parameters.Add("@ID_SQD", SqlDbType.BigInt).Value = -1;
                cmd1.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);

                dtbc = new DataTable();
                dtbc = ds.Tables[1].Copy();
                dtbc.TableName = "NOI_DUNG";
                frm.AddDataSource(dtbc);

                frm.ShowDialog();
            }
            catch
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCanTaoHopDongTruocKhiIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void grvData_RowCountChanged(object sender, EventArgs e)
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
            catch
            {
            }
        }
    }
}