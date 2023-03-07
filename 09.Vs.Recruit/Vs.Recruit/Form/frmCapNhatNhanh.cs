using DevExpress.CodeParser;
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vs.Recruit
{
    public partial class frmCapNhatNhanh : DevExpress.XtraEditors.XtraForm
    {
        public DataTable dt1;
        private bool flag = false;
        private int them = 0;
        private string ChuoiKT = "";
        public frmCapNhatNhanh()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, btnALL);
        }


        #region even
        private void frmCapNhatNhanh_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadData();
            Commons.Modules.sLoad = "";
        }

        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "ghi":
                        {
                            grvDSUngVien.CloseEditor();
                            grvDSUngVien.UpdateCurrentRow();
                            if (grvDSUngVien.RowCount == 0)
                                return;
                            DataTable dt = new DataTable();
                            dt = (DataTable)grdDSUngVien.DataSource;
                            if (!KiemTraLuoi(dt)) return;
                            string sBT = "sBTUngVien" + Commons.Modules.iIDUser;
                            try
                            {
                                if (flag == true) return;
                                if (KiemSLTuyen() == "") return;
                                string sBTCNN = "sBTCNN" + Commons.Modules.iIDUser;
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, dt1, "");
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCNN, Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien), "");

                                System.Data.SqlClient.SqlConnection conn;
                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();
                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUngVien", conn);
                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "CAP_NHAT_NHANH";
                                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                                cmd.Parameters.Add("@sBT1", SqlDbType.NVarChar).Value = sBT;
                                cmd.Parameters.Add("@sBT2", SqlDbType.NVarChar).Value = sBTCNN;
                                cmd.CommandType = CommandType.StoredProcedure;
                                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                DataSet ds = new DataSet();
                                adp.Fill(ds);
                                dt = new DataTable();
                                dt = ds.Tables[0].Copy();

                                them = 0;
                                LoadData();
                                Commons.Modules.ObjSystems.XoaTable(sBT);
                            }
                            catch (Exception ex)
                            {
                                Commons.Modules.ObjSystems.XoaTable(sBT);
                            }

                            this.DialogResult = DialogResult.OK;
                            dt1 = new DataTable();
                            dt1 = dt;
                            this.Close();
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
        private void LoadData()
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
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@sBT1", SqlDbType.NVarChar).Value = sBT;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.Columns["MS_UV"].ReadOnly = true;
                dt.Columns["HO_TEN"].ReadOnly = true;
                dt.Columns["MS_CN"].ReadOnly = true;
                dt.Columns["MS_THE_CC"].ReadOnly = true;
                //DataTable dt1 = new DataTable();
                //dt1 = ds.Tables[1].Copy();
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSUngVien, grvDSUngVien, dt, true, true, false, true, true, this.Name);
                grvDSUngVien.Columns["ID_UV"].Visible = false;

                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_XN = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                cboID_XN.NullText = "";
                cboID_XN.ValueMember = "ID_XN";
                cboID_XN.DisplayMember = "TEN_XN";
                //ID_VTTD,TEN_VTTD
                cboID_XN.DataSource = Commons.Modules.ObjSystems.DataXiNghiep(-1, false);
                cboID_XN.Columns.Clear();
                cboID_XN.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_XN"));
                cboID_XN.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_XN"));
                cboID_XN.Columns["TEN_XN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_XN");
                cboID_XN.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboID_XN.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cboID_XN.Columns["ID_XN"].Visible = false;
                grvDSUngVien.Columns["ID_XN"].ColumnEdit = cboID_XN;
                cboID_XN.BeforePopup += cboID_XN_BeforePopup;
                cboID_XN.EditValueChanged += cboID_XN_EditValueChanged;

                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_LHDLD = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                cboID_LHDLD.NullText = "";
                cboID_LHDLD.ValueMember = "ID_LHDLD";
                cboID_LHDLD.DisplayMember = "TEN_LHDLD";
                //ID_VTTD,TEN_VTTD
                DataTable dt2 = new DataTable();
                dt2.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_LHDLD, CASE " + Commons.Modules.TypeLanguage + " WHEN 0 THEN TEN_LHDLD ELSE ISNULL(NULLIF(TEN_LHDLD_A,''),TEN_LHDLD) END TEN_LHDLD FROM dbo.LOAI_HDLD WHERE ID_TT_HD = 3"));
                cboID_LHDLD.DataSource = dt2;
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

            }
            catch (Exception ex) { }
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
            string sBTUngVien = "sBTUngVien" + Commons.Modules.iIDUser;
            try
            {
                if (grvDSUngVien.GetFocusedRowCellValue(grvDSUngVien.FocusedColumn.FieldName).ToString() == "") return;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTUngVien, Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien), "");

                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateChuotPhai_TiepNhan", sBTUngVien, sCotCN, sCotCN.Substring(0, 4) == "NGAY" ? Convert.ToDateTime(grvDSUngVien.GetFocusedRowCellValue(grvDSUngVien.FocusedColumn.FieldName)).ToString("MM/dd/yyyy") : grvDSUngVien.GetFocusedRowCellValue(grvDSUngVien.FocusedColumn.FieldName)));
                grdDSUngVien.DataSource = dt;
                Commons.Modules.ObjSystems.XoaTable(sBTUngVien);
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBTUngVien);
            }
        }

        private void grvData_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (grvDSUngVien.FocusedColumn.FieldName == "MS_UV") return;
                if (grvDSUngVien.FocusedColumn.FieldName == "HO_TEN") return;
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

                DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                DevExpress.XtraGrid.Columns.GridColumn idTo = View.Columns["ID_TO"];

                if (View.GetRowCellValue(e.RowHandle, idTo).ToString() == "")
                {
                    flag = true;
                    e.Valid = false;
                    View.SetColumnError(idTo, "Tổ không được bỏ trống"); return;
                }
                flag = false;

                //CheckDuplicateKHNP(grvKHNP, (DataTable)grdKHNP.DataSource, e);
            }
            catch (Exception ex) { }
        }

        private void grvDSUngVien_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {

            }
            catch { }
        }
        private string KiemSLTuyen()
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
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
                    cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "CAP_NHAT_NHANH";
                    cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@iCot1", SqlDbType.BigInt).Value = Convert.ToInt64(dt1.Rows[i]["ID_YCTD"]);
                    cmd.Parameters.Add("@iCot2", SqlDbType.BigInt).Value = Convert.ToInt64(dt1.Rows[i]["ID_VTTD"]);
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
                //Số hợp đồng lao động
                if (!KiemDuLieu(grvDSUngVien, dr, "ID_LHDLD", true, 250, this.Name))
                {
                    errorCount++;
                }
                //Ngày nhận việc
                if (!KiemDuLieuNgay(grvDSUngVien, dr, "NGAY_NHAN_VIEC", true, this.Name))
                {
                    errorCount++;
                }
                if (!KiemDuLieu(grvDSUngVien, dr, "ID_TO", true, 250, this.Name))
                {
                    errorCount++;
                }
                string sMaSo = dr[grvDSUngVien.Columns["MS_CN"].FieldName.ToString()].ToString();
                if (!KiemTrungDL(grvDSUngVien, dtSource, dr, "MS_CN", sMaSo, "CONG_NHAN", "MS_CN", this.Name))
                {
                    errorCount++;
                }
                string sMaThe = dr[grvDSUngVien.Columns["MS_THE_CC"].FieldName.ToString()].ToString();
                if (!KiemTrungDL(grvDSUngVien, dtSource, dr, "MS_CN", sMaSo, "CONG_NHAN", "MS_THE_CC", this.Name))
                {
                    errorCount++;
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
    }
}
