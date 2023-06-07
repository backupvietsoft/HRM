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
using System.Windows.Media.Animation;

namespace Vs.Payroll
{
    public partial class frmNhapHoTroLuong : DevExpress.XtraEditors.XtraForm
    {
        public int iID_DV = -1;
        public int iID_XN = -1;
        public int iID_TO = -1;
        public DateTime dNgay;
        private int iThem = 0;
        public int iLoai = 0;
        public frmNhapHoTroLuong()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, windowsUIButton);
        }

        //sự kiên load form
        private void frmNhapHoTroLuong_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadCboLoaiHT();
            LoadThang();
            cboDonVi.EditValue = iID_DV;
            cboXiNghiep.EditValue = iID_XN;
            cboTo.EditValue = iID_TO;
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
                        Commons.Modules.ObjSystems.AddnewRow(grvData, true);
                        VisibleButton(false);
                        break;
                    }
                case "copydlcu":
                    {
                        LoadData(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text).AddMonths(-1));
                        Commons.Modules.ObjSystems.AddnewRow(grvData, true);
                        if (grvData.RowCount > 1)
                        {
                            Commons.Modules.ObjSystems.Alert("Copied", Commons.Form_Alert.enmType.Success);
                        }
                        else
                        {
                            Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblKhongCoDuLieu"), Commons.Form_Alert.enmType.Warning);
                        }
                        break;
                    }
                case "luu":
                    {
                        grvData.CloseEditor();
                        grvData.UpdateCurrentRow();
                        if (grvData.HasColumnErrors) return;
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
                        string sSBT = "sBTXoaHoTroLuong" + Commons.Modules.iIDUser;
                        try
                        {
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sSBT, Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdData, grvData), "");
                            string sSQL = "DELETE dbo.LOAI_HO_TRO_LUONG_TL FROM dbo.LOAI_HO_TRO_LUONG_TL T1 INNER JOIN " + sSBT + " T2 ON T1.ID_HTL = T2.ID_HTL";
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

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            Commons.Modules.sLoad = "";
            LoadData(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text));
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            Commons.Modules.sLoad = "";
            LoadData(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text));
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadData(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text));
            Commons.Modules.sLoad = "";

        }

        private void grvData_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetFocusedRowCellValue("ID_HTL", 0);
            view.SetFocusedRowCellValue("CHINH_SUA", 1);
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
            windowsUIButton.Buttons[2].Properties.Visible = !visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
            windowsUIButton.Buttons[6].Properties.Visible = !visible;

            cboDonVi.Properties.ReadOnly = !visible;
            cboXiNghiep.Properties.ReadOnly = !visible;
            cboTo.Properties.ReadOnly = !visible;
            cboThang.Properties.ReadOnly = !visible;
            cboLoaiHoTro.Properties.ReadOnly = !visible;
            grvData.OptionsBehavior.Editable = !visible;
        }
        public void LoadThang()
        {
            try
            {
                string sSql = "SELECT disTINCT RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.LOAI_HO_TRO_LUONG_TL T1 INNER JOIN dbo.LOAI_HO_TRO_LUONG T2 ON T2.ID_LOAI_HTL = T1.ID_LOAI_HTL AND T2.ID_LOAI_HTL = " + cboLoaiHoTro.EditValue + " ORDER BY THANG DESC";
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
        private void LoadCboLoaiHT()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spHoTroLuong", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboLoaiHoTro, dt, "ID_LOAI_HTL", "TEN_LOAI", "TEN_LOAI");

            }
            catch { }
        }
        private void createColumn()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spHoTroLuong", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@ID_LOAI_HT", SqlDbType.BigInt).Value = cboLoaiHoTro.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
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

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spHoTroLuong", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                cmd.Parameters.Add("@ID_LOAI_HT", SqlDbType.BigInt).Value = cboLoaiHoTro.EditValue;
                cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = datThang;
                cmd.Parameters.Add("@DVi", SqlDbType.BigInt).Value = Convert.ToInt64(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.BigInt).Value = Convert.ToInt64(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.BigInt).Value = Convert.ToInt64(cboTo.EditValue);
                cmd.Parameters.Add("@iThem", SqlDbType.Int).Value = iThem;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();

                dt = ds.Tables[0].Copy(); // get loai_nhập
                int iLoaiNhap = Convert.ToInt32(dt.Rows[0][0]);
                string grvName = Convert.ToString(dt.Rows[0][1]);
                grvData.Name = grvName;
                dt = ds.Tables[1].Copy();

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, true, false, true, true, this.Name);
                grvData.Columns["ID_HTL"].Visible = false;
                grvData.Columns["CHINH_SUA"].Visible = false;

                if (iLoaiNhap != 0)
                {
                    grvData.Columns["MUC_TU"].Visible = false;
                    grvData.Columns["MUC_DEN"].Visible = false;
                }
                DataTable dtTemp = new DataTable();
                dtTemp = ds.Tables[1].Copy();
                if (dtTemp.Columns[1].ColumnName == "ID_CHUNG") return;
                dt = ds.Tables[2].Copy();
                DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, dt.Columns[0].ColumnName, dt.Columns[1].ColumnName, dt.Columns[0].ColumnName, grvData, dt, this.Name);

                // Load combo lên lưới
                dt = ds.Tables[3].Copy();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string sSQL = "";
                    dtTemp = new DataTable();
                    switch (dt.Rows[i][1].ToString())
                    {
                        case "ID_LXL":
                            {
                                sSQL = "SELECT T1.ID_LXL,CASE 0 WHEN 0 THEN T1.TEN_LXL WHEN 1 THEN ISNULL(NULLIF(T1.TEN_LXL_A,''),T1.TEN_LXL) \r\n\tELSE ISNULL(NULLIF(T1.TEN_LXL_H,''),T1.TEN_LXL) END AS TEN_LXL, T1.HE_SO_LXL FROM dbo.LOAI_XEP_LOAI T1  \r\n\tWHERE T1.THANG_LXL = (SELECT MAX(THANG_LXL) FROM dbo.LOAI_XEP_LOAI WHERE THANG_LXL <= '" + Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text).ToString("MM/dd/yyyy") + "') \r\n\tORDER BY TEN_LXL";
                                break;
                            }
                        case "ID_LCV":
                            {
                                sSQL = "SELECT ID_LCV, TEN_LCV FROM dbo.LOAI_CONG_VIEC";
                                break;
                            }
                        default:
                            {
                                sSQL = "";
                                break;
                            }
                    }
                    if (sSQL == "")
                    {
                        return;
                    }
                    dtTemp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSQL));
                    cbo = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                    Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, dtTemp.Columns[0].ColumnName, dtTemp.Columns[1].ColumnName, dtTemp.Columns[0].ColumnName, grvData, dtTemp, this.Name);
                }

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
            try
            {
                string sSBT = "sBTHoTroLuong" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sSBT, Commons.Modules.ObjSystems.ConvertDatatable(grdData), "");
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spHoTroLuong", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 3;
                cmd.Parameters.Add("@ID_LOAI_HT", SqlDbType.BigInt).Value = cboLoaiHoTro.EditValue;
                cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                cmd.Parameters.Add("@DVi", SqlDbType.BigInt).Value = Convert.ToInt64(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.BigInt).Value = Convert.ToInt64(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.BigInt).Value = Convert.ToInt64(cboTo.EditValue);
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
            catch
            {
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
                if (windowsUIButton.Buttons[0].Properties.Visible)
                {
                    if (grvData.Name.ToString() != "grvMucXetXL") return;
                    contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                    contextMenuStrip1.Items[0].Visible = false;
                    contextMenuStrip1.Items[1].Visible = true;
                }
                else
                {
                    contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                    contextMenuStrip1.Items[0].Visible = true;
                    contextMenuStrip1.Items[1].Visible = false;
                }
            }
            catch { }
        }

        private void cboLoaiHoTro_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadThang();
            createColumn();
            LoadData(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text));
        }
        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.SetFocusedRowCellValue("CHINH_SUA", 1);
                view.SetFocusedRowCellValue(view.FocusedColumn.FieldName, e.Value);
            }
            catch { }
        }

        private void toolLoaiXepLoai_Click(object sender, EventArgs e)
        {
            Vs.HRM.frmLoaiXepLoai frm = new Vs.HRM.frmLoaiXepLoai();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadData(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text));
            }
            else
            {
                LoadData(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text));
            }
        }
    }
}