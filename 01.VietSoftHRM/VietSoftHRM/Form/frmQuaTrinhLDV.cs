using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System.Linq;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using static DevExpress.XtraPrinting.Native.ExportOptionsPropertiesNames;
using static NPOI.HSSF.Util.HSSFColor;
using DevExpress.XtraRichEdit.Import.Html;
using Vs.Report;
using static Spire.Pdf.General.Render.Decode.Jpeg2000.j2k.codestream.HeaderInfo;

namespace Vs.Payroll
{
    public partial class frmQuaTrinhLDV : DevExpress.XtraEditors.XtraForm
    {
        public int iID_DV = -1;
        public int iID_XN = -1;
        public int iID_TO = -1;
        public DateTime dNgay;
        private int iThem = 0;
        public int iLoai = 0;
        public frmQuaTrinhLDV()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, windowsUIButton);
        }

        //sự kiên load form
        private void frmQuaTrinhLDV_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadThang();
            LoadData(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text));
            foreach (ToolStripMenuItem item in contextMenuStrip1.Items)
            {
                item.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, item.Name);
            }
            Commons.Modules.sLoad = "";
            VisibleButton(true);
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        iThem = 1;
                        LoadData(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text));
                        VisibleButton(false);
                        break;
                    }
                case "copydlcu":
                    {

                        break;
                    }
                case "luu":
                    {
                        grvData.CloseEditor();
                        grvData.UpdateCurrentRow();
                        if (grvData.HasColumnErrors) return;

                        DataTable dt = new DataTable();
                        dt = (DataTable)grdData.DataSource;

                        if (!KiemTraLuoi(dt))
                        {
                            return;
                        }
                        if (!SaveData())
                        {
                            return;
                        }
                        else
                        {
                            Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCong"), Commons.Form_Alert.enmType.Success);
                        }

                        iThem = 0;
                        LoadData(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text));
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        VisibleButton(true);
                        break;
                    }
                case "khongluu":
                    {
                        iThem = 0;
                        LoadData(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text));
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        VisibleButton(true);
                        break;
                    }
                case "xoa":
                    {
                        if (grvData.RowCount == 0) { Commons.Modules.ObjSystems.MsgWarning(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaCoDuLieu")); return; }
                        if (Commons.Modules.ObjSystems.MsgDelete(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoMuonXoaKhong")) == 0) return;
                        string sSBT = "sBTQuaTrinhLDV" + Commons.Modules.iIDUser;
                        try
                        {
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sSBT, Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdData, grvData), "");
                            string sSQL = "DELETE dbo.QUA_TRINH_LY_DO_VANG FROM dbo.QUA_TRINH_LY_DO_VANG T1 INNER JOIN " + sSBT + " T2 ON T1.ID_QTLDV = T2.ID_QTLDV";
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSQL);
                            Commons.Modules.ObjSystems.XoaTable(sSBT);
                        }
                        catch (Exception ex)
                        {
                            Commons.Modules.ObjSystems.XoaTable(sSBT);
                            Commons.Modules.ObjSystems.MsgError(ex.Message);
                        }
                        LoadData(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text));
                        break;
                    }
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
            cboThang.ClosePopup();
        }

        private void grvNgay1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = grv.GetFocusedRowCellValue("THANG").ToString();
                cboThang.ClosePopup();
            }
            catch { }
        }

        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text));
        }
        private void grvData_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            //GridView view = sender as GridView;
            //view.SetFocusedRowCellValue("ID_HTL", 0);
            //view.SetFocusedRowCellValue("CHINH_SUA", 1);
        }

        private void grvData_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
        #region function
        private void VisibleButton(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;

            cboThang.Properties.ReadOnly = !visible;
            grvData.OptionsBehavior.Editable = !visible;
        }
        public void LoadThang()
        {
            try
            {
                string sSql = "SELECT DISTINCT RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.QUA_TRINH_LY_DO_VANG T1 ORDER BY THANG DESC";
                DataTable dtthang = new DataTable();
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay1, dtthang, false, true, true, true, true, this.Name);

                try
                {
                    cboThang.Text = grvNgay1.GetFocusedRowCellValue("THANG").ToString();
                }
                catch
                {
                    cboThang.Text = DateTime.Now.ToString("MM/yyyy");
                }
            }
            catch (Exception ex)
            {
                DateTime now = DateTime.Now;
                cboThang.Text = now.ToString("MM/yyyy");
            }
        }
        private void LoadData(DateTime datThang)
        {
            try
            {

                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spQuaTrinhLyDoVang", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@iThem", SqlDbType.Int).Value = iThem;
                cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = datThang;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, true, false, true, true, this.Name);
                grvData.Columns["ID_QTLDV"].Visible = false;
                grvData.Columns["MS_LDV_CU"].OptionsColumn.AllowEdit = false;
                grvData.Columns["TEN_LDV_CU"].OptionsColumn.AllowEdit = false;
                grvData.Columns["ID_LN_CU"].OptionsColumn.AllowEdit = false;

                dt = new DataTable();
                dt = ds.Tables[1].Copy();

                DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_LOAI_NGHI", "TEN_LOAI_NGHI", "ID_LN_CU", grvData, dt, this.Name);

                cbo = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_LOAI_NGHI", "TEN_LOAI_NGHI", "ID_LN", grvData, dt, this.Name);

                //cbo.BeforePopup += cbo_BeforePopup;
                //cbo.EditValueChanged += cbo_EditValueChanged;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
        }
        private bool SaveData()
        {
            string sSBT = "sBTQuaTrinhLDV" + Commons.Modules.iIDUser;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sSBT, Commons.Modules.ObjSystems.ConvertDatatable(grdData), "");
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spQuaTrinhLyDoVang", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sSBT;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (Convert.ToString(dt.Rows[0][0]) == "-99")
                {
                    Commons.Modules.ObjSystems.MsgError(Convert.ToString(dt.Rows[0][1]));
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sSBT);
                Commons.Modules.ObjSystems.MsgError(ex.Message);
                return false;
            }
        }
        #endregion

        #region chuotphai
        private void toolCapNhat_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                string sCotCN = grvData.FocusedColumn.FieldName;
                var data = grvData.GetFocusedRowCellValue(sCotCN);

                dt1 = Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdData, grvData);
                dt = (DataTable)grdData.DataSource;

                dt.AsEnumerable().Where(row => dt1.AsEnumerable()
                                                         .Select(r => r.Field<Int64>("ID_CN"))
                                                         .Any(x => x == row.Field<Int64>("ID_CN"))
                                                         ).ToList<DataRow>().ForEach(r => r[sCotCN] = (data));

                dt.AsEnumerable().Where(row => dt1.AsEnumerable()
                                                         .Select(r => r.Field<Int64>("ID_CN"))
                                                         .Any(x => x == row.Field<Int64>("ID_CN"))
                                                         ).ToList<DataRow>().ForEach(r => r["CHINH_SUA"] = 1);
                dt.AcceptChanges();
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        private void grvHTL_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                //if (windowsUIButton.Buttons[0].Properties.Visible)
                //{
                //    if (grvData.Name.ToString() != "grvMucXetXL") return;
                //    contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                //    contextMenuStrip1.Items[0].Visible = false;
                //    contextMenuStrip1.Items[1].Visible = true;
                //}
                //else
                //{
                //    contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                //    contextMenuStrip1.Items[0].Visible = true;
                //    contextMenuStrip1.Items[1].Visible = false;
                //}
            }
            catch { }
        }
        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                //GridView view = sender as GridView;
                //view.SetFocusedRowCellValue("CHINH_SUA", 1);
                //view.SetFocusedRowCellValue(view.FocusedColumn.FieldName, e.Value);
            }
            catch { }
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
            int count = grvData.RowCount;
            int col = 0;
            int errorCount = 0;
            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;

                //mã số lý do vắng
                //string sMaSo = dr["MS_LDV"].ToString();
                //if (!KiemTrungDL(grvData, dtSource, dr, "MS_LDV", sMaSo, "HOP_DONG_LAO_DONG", "SO_HDLD", this.Name))
                //{
                //    errorCount++;
                //}
                try
                {
                    if (Convert.ToString(dr["MS_LDV"]) != "")
                    {
                        if (dtSource.AsEnumerable().Where(x => x.Field<string>("MS_LDV").Trim().Equals(dr["MS_LDV"].ToString())).CopyToDataTable().Rows.Count > 1)
                        {
                            errorCount++;
                            string sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTrungDLLuoi");
                            dr.SetColumnError("MS_LDV", sTenKTra);
                        }
                    }

                    if (Convert.ToString(dr["MS_LDV_HT"]) != "")
                    {
                        if (dtSource.AsEnumerable().Where(x => x.Field<string>("MS_LDV_HT").Trim().Equals(dr["MS_LDV_HT"].ToString())).CopyToDataTable().Rows.Count > 1)
                        {
                            errorCount++;
                            string sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTrungDLLuoi");
                            dr.SetColumnError("MS_LDV_HT", sTenKTra);
                        }
                    }
                }
                catch
                {
                    errorCount++;
                    dr.SetColumnError("MS_LDV", "msgTrungDLLuoi");
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
        private void toolLoaiXepLoai_Click(object sender, EventArgs e)
        {
            //Vs.HRM.frmLoaiXepLoai frm = new Vs.HRM.frmLoaiXepLoai();
            //if(frm.ShowDialog() == DialogResult.OK)
            //{
            //    LoadData(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text));
            //}
            //else
            //{
            //    LoadData(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text));
            //}
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                var row = view.GetFocusedDataRow();

                if (e.Column.FieldName == "MS_LDV")
                {
                    row["MS_LDV_HT"] = e.Value;
                }
            }
            catch { }
        }
    }
}