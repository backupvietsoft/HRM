using Commons;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vs.Report;

namespace Vs.TimeAttendance
{
    public partial class ucViPhamNoiQuyLD : DevExpress.XtraEditors.XtraUserControl
    {
        public static ucViPhamNoiQuyLD _instance;
        public static ucViPhamNoiQuyLD Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucViPhamNoiQuyLD();
                return _instance;
            }
        }
        string sBT = "tabKeHoachDiCa" + Commons.Modules.ModuleName;
        public ucViPhamNoiQuyLD()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
        }
        private void ucViPhamNoiQuyLD_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadThang();
            LoadGrdCongNhan();
            radTinHTrang_SelectedIndexChanged(null, null);
            LoadGrdVPNoiQuy();
            grvCongNhan_FocusedRowChanged(null, null);
            Commons.Modules.sLoad = "";
            if (Modules.iPermission != 1)
            {
                windowsUIButton.Buttons[0].Properties.Visible = false;
                windowsUIButton.Buttons[1].Properties.Visible = false;
                windowsUIButton.Buttons[2].Properties.Visible = false;
                windowsUIButton.Buttons[4].Properties.Visible = false;
                windowsUIButton.Buttons[7].Properties.Visible = false;
                windowsUIButton.Buttons[8].Properties.Visible = false;
            }
            else
            {
                enableButon(true);

            }
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdCongNhan();
            grvCongNhan_FocusedRowChanged(null, null);
            Commons.Modules.sLoad = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdCongNhan();
            grvCongNhan_FocusedRowChanged(null, null);
            Commons.Modules.sLoad = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdCongNhan();
            grvCongNhan_FocusedRowChanged(null, null);
            Commons.Modules.sLoad = "";
        }
        public void CheckDuplicateDIEM_THEO_DOI_NOP_BAI(GridView grid, DataSet GridDataSet, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow row = grid.GetDataRow(e.RowHandle);
            int count = 0;
            foreach (DataRow r in GridDataSet.Tables[0].Rows)
            {
                if (r.RowState != DataRowState.Deleted)
                {
                    if (r["NHAN_SU"].ToString() == row["NHAN_SU"].ToString() && r["NGUOI_GIAO"].ToString() == row["NGUOI_GIAO"].ToString() && r["NGAY_GIAO"].ToString() == row["NGAY_GIAO"].ToString())
                    {
                        if (grid.IsNewItemRow(grid.FocusedRowHandle))
                        {
                            r.RowError = "Dữ liệu bị trùng, xin vui lòng kiểm tra lại.";
                            grid.SetColumnError(grid.Columns["NHAN_SU"], "Nhân sự, người giao và ngày giao bị trùng, xin vui lòng kiểm tra lại.");
                            e.Valid = false;
                            return;
                        }
                        else
                        {
                            count++;
                            if (count == 2)
                            {
                                r.RowError = "Dữ liệu bị trùng, xin vui lòng kiểm tra lại.";
                                grid.SetColumnError(grid.Columns["NHAN_SU"], "Nhân sự, người giao và ngày giao bị trùng, xin vui lòng kiểm tra lại.");
                                e.Valid = false;
                                return;
                            }
                        }
                    }
                }
            }
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {

                case "themsua":
                    {
                        if (grvCongNhan.RowCount == 0)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgBanChuaChonDuLieu);
                            return;
                        }
                        Commons.Modules.ObjSystems.AddnewRow(grvViPhamNoiQuyLD, true);
                        enableButon(false);
                        break;
                    }
                case "xoa":
                    {
                        XoaKeHoachDiCa();
                        break;
                    }
                case "In":
                    {
                        System.Data.SqlClient.SqlConnection conn;
                        DataTable dt = new DataTable();
                        frmViewReport frm = new frmViewReport();
                        frm.rpt = new rptBCViPhamNoiQuyLD(Convert.ToDateTime(cboThang.Text),"");

                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                        conn.Open();

                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptViPhamNoiQuyLD", conn);
                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                        cmd.Parameters.Add("@THANG", SqlDbType.DateTime).Value = Convert.ToDateTime(cboThang.EditValue);
                        cmd.CommandType = CommandType.StoredProcedure;

                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adp.Fill(ds);
                        dt = new DataTable();
                        dt = ds.Tables[0].Copy();
                        dt.TableName = "DATA";
                        frm.AddDataSource(dt);

                        frm.ShowDialog();

                        break;
                    }
                case "luu":
                    {
                        Validate();
                        if (grvViPhamNoiQuyLD.HasColumnErrors) return;
                        if (Savedata() == false)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                        }
                        Commons.Modules.ObjSystems.DeleteAddRow(grvViPhamNoiQuyLD);
                        LoadGrdVPNoiQuy();
                        grvCongNhan_FocusedRowChanged(null, null);
                        LoadThang();
                        enableButon(true);

                        break;
                    }
                case "khongluu":
                    {
                        Commons.Modules.ObjSystems.DeleteAddRow(grvViPhamNoiQuyLD);
                        LoadGrdVPNoiQuy();
                        grvCongNhan_FocusedRowChanged(null, null);
                        enableButon(true);
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
            }
        }

        #region hàm xử lý dữ liệu
        private void LoadGrdCongNhan()
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongNhanTheoTT", cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdCongNhan, grvCongNhan, dt, false, false, true, true, true, this.Name);
                grvCongNhan.Columns["ID_CN"].Visible = false;
                grvCongNhan.Columns["TINH_TRANG"].Visible = false;
                //grvCongNhan.Appearance.HeaderPanel.BackColor = Color.FromArgb(240, 128, 25);
                //for (int i = 0; i < grvCongNhan.Columns.Count; i++)
                //{
                //    grvCongNhan.Columns[i].AppearanceHeader.BackColor = Color.FromArgb(200, 200, 200);
                //}
                Commons.Modules.sLoad = "";
            }
            catch (Exception ex)
            {
            }
        }
        private void LoadGrdVPNoiQuy()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListVI_PHAM_NQLD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToDateTime(cboThang.EditValue)));

                dt.Columns["ID_NQLD"].ReadOnly = false;
                dt.Columns["NGAY"].ReadOnly = false;
                if (grdViPhamNoiQuyLD.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdViPhamNoiQuyLD, grvViPhamNoiQuyLD, dt, true, true, false, true, true, this.Name);
                    grvViPhamNoiQuyLD.Columns["ID_CN"].Visible = false;
                    grvViPhamNoiQuyLD.Columns["ID_VPNQ"].Visible = false;
                }
                else
                {
                    grdViPhamNoiQuyLD.DataSource = dt;
                }

                //RepositoryItemLookUpEdit cboNQLD = new RepositoryItemLookUpEdit();

                //cboNQLD.NullText = "";
                //cboNQLD.ValueMember = "ID_NQLD";
                //cboNQLD.DisplayMember = "NOI_DUNG";
                DataTable dt1 = new DataTable();
                dt1.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNQLD",Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                Commons.Modules.ObjSystems.AddCombXtra("ID_NQLD", "NOI_DUNG", grvViPhamNoiQuyLD, dt1, false, "ID_NQLD", "NOI_QUY_LAO_DONG");

            }
            catch
            {

            }
        }
        private void cboNQLD_BeforePopup(object sender, EventArgs e)
        {
            //try
            //{
            //    Int64 id_cv = Convert.ToInt64(grvViPhamNoiQuyLD.GetFocusedRowCellValue("ID_NQLD"));
            //    if (sender is LookUpEdit cbo)
            //    {
            //        try
            //        {
            //            DataTable DataCombo = (DataTable)cbo.Properties.DataSource;
            //            DataTable DataLuoi = Commons.Modules.ObjSystems.ConvertDatatable(grdViPhamNoiQuyLD);
            //            var DataNewCombo = DataCombo.AsEnumerable().Where(r => !DataLuoi.AsEnumerable()
            //            .Any(r2 => r["ID_NQLD"].ToString().Trim() == r2["ID_NQLD"].ToString().Trim())).CopyToDataTable();
            //            cbo.Properties.DataSource = null;
            //            cbo.Properties.DataSource = DataNewCombo;
            //        }
            //        catch
            //        {
            //            cbo.Properties.DataSource = null;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message);
            //}

            //try
            //{
            //    if (sender is LookUpEdit cbo)
            //    {
            //        int ID_NQLD = Convert.ToInt32(grvViPhamNoiQuyLD.GetFocusedRowCellValue("ID_NQLD"));
            //        cbo.Properties.DataSource = null;
            //        DataTable dt = new DataTable();
            //        //dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT ID_NQLD  FROM VI_PHAM_NOI_QUY_LD WHERE ID_NQLD = " + ID_NHOM + " OR " + ID_NHOM + " = -1 ORDER BY CA"));
            //        cbo.Properties.DataSource = Commons.Modules.ObjSystems.DataCa(ID_NQLD);
            //    }
            //}
            //catch
            //{
            //}
        }
        private void cboNQLD_EditValueChanged(object sender, EventArgs e)
        {
            //LookUpEdit lookUp = sender as LookUpEdit;
            //DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            //try
            //{
            //    grvData.SetFocusedRowCellValue("TEN_TO", dataRow.Row["TEN_TO"]);
            //}
            //catch
            //{

            //}
        }
        
        private void CboMSCa_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                if (sender is LookUpEdit cbo)
                {
                    int IDNHOM = Convert.ToInt32(grvViPhamNoiQuyLD.GetFocusedRowCellValue("ID_NHOM"));
                    cbo.Properties.DataSource = null;
                    cbo.Properties.DataSource = Commons.Modules.ObjSystems.DataCa(IDNHOM);
                }
            }
            catch
            {
            }
        }

        private bool Savedata()
        {
            DataTable dkVPNoiQuyLD = new DataTable();
            string stbVPNoiQuyLD = "grvVPNoiQuyLD" + Commons.Modules.UserName;

            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbVPNoiQuyLD, (DataTable)grdViPhamNoiQuyLD.DataSource, "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveVPNoiQuyLD", stbVPNoiQuyLD);
                Commons.Modules.ObjSystems.XoaTable(stbVPNoiQuyLD);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                Commons.Modules.ObjSystems.XoaTable(stbVPNoiQuyLD);
                return false;
            }
        }
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
            windowsUIButton.Buttons[6].Properties.Visible = visible;

            grvViPhamNoiQuyLD.OptionsBehavior.Editable = !visible;

            searchControl.Visible = visible;
        }
        private void XoaKeHoachDiCa()
        {
            if (grvViPhamNoiQuyLD.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                string sSql = "DELETE dbo.VI_PHAM_NOI_QUY_LD WHERE ID_CN = " + grvViPhamNoiQuyLD.GetFocusedRowCellValue("ID_CN") + "  AND NGAY = '" + Convert.ToDateTime(grvViPhamNoiQuyLD.GetFocusedRowCellValue("NGAY")).ToString("MM/dd/yyyy") + "' ";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                grvViPhamNoiQuyLD.DeleteSelectedRows();
            }
            catch
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
            }
        }

        public void LoadThang()
        {
            try
            {
                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),NGAY,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),NGAY,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),NGAY,103),7) AS NGAY FROM dbo.VI_PHAM_NOI_QUY_LD ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                if(grdThang.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                    grvThang.Columns["M"].Visible = false;
                    grvThang.Columns["Y"].Visible = false;
                }
                else
                {
                    grdThang.DataSource = dtthang;
                }
                

                cboThang.Text = grvThang.GetFocusedRowCellValue("NGAY").ToString();
            }
            catch (Exception ex)
            {
                cboThang.Text = DateTime.Now.Month + "/" + DateTime.Now.Year;
            }
        }

        private void LoadNull()
        {
            try
            {
                if (cboThang.Text == "") cboThang.Text = DateTime.Now.ToShortDateString();
            }
            catch (Exception ex)
            {
                cboThang.Text = "";
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion

        private void radTinHTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtTmp = new DataTable();
            string sdkien = "( 1 = 1 )";
            try
            {
                dtTmp = (DataTable)grdCongNhan.DataSource;
                if (radTinHTrang.SelectedIndex == 1) sdkien = "(TINH_TRANG = 1)";
                if (radTinHTrang.SelectedIndex == 2) sdkien = "(TINH_TRANG = 0)";
                dtTmp.DefaultView.RowFilter = sdkien;
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

        private void grvCongNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
      {
            if (Commons.Modules.sLoad == "0Load") return;
            DataTable dtTmp = new DataTable();
            String sIDCN;
            try
            {
                dtTmp = (DataTable)grdViPhamNoiQuyLD.DataSource;

                string sDK = "";
                sIDCN = "-1";
                try { sIDCN = grvCongNhan.GetFocusedRowCellValue("ID_CN").ToString(); } catch { }
                if (sIDCN != "-1") sDK = " ID_CN = '" + sIDCN + "' ";

                dtTmp.DefaultView.RowFilter = sDK;
            }
            catch { }
        }

        private void grvKeHoachDiCa_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                //DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                //DevExpress.XtraGrid.Columns.GridColumn ID_NQLD = View.Columns["ID_NQLD"];
                //DevExpress.XtraGrid.Columns.GridColumn NGAY = View.Columns["NGAY"];

                //if (View.GetRowCellValue(e.RowHandle, ID_NQLD).ToString() == "")
                //{
                //    e.Valid = false;
                //    View.SetColumnError(ID_NQLD, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraLDVNULL", Commons.Modules.TypeLanguage)); return;
                //}

                //if (View.GetRowCellValue(e.RowHandle, NGAY).ToString() == "")
                //{
                //    e.Valid = false;
                //    View.SetColumnError(NGAY, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraLDVNULL", Commons.Modules.TypeLanguage)); return;
                //}
                  
            }
            catch { }
            
        }

        private void grdKeHoachDiCa_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                XoaKeHoachDiCa();
            }
        }

        private void grvKeHoachDiCa_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            grvViPhamNoiQuyLD.ClearColumnErrors();
            GridView view = sender as GridView;

            if (view.FocusedColumn.FieldName == "NGAY")
            {

                if (Convert.ToDateTime(e.Value) == null)
                {
                    e.Valid = false;
                    e.ErrorText = "Ngày không được trống";
                }
            }
        }

        private void grvKeHoachDiCa_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvKeHoachDiCa_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            //e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvKeHoachDiCa_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            //thêm defaulst khi add một dòng mới
            try
            {
                GridView view = sender as GridView;
                view.SetFocusedRowCellValue("TU_NGAY", Convert.ToDateTime(DateTime.Now.Date));
                view.SetFocusedRowCellValue("DEN_NGAY", Convert.ToDateTime(DateTime.Now.Date));
            }
            catch
            {
            }
        }

        private void grvCongNhan_RowCountChanged(object sender, EventArgs e)
        {
            //GridView view = sender as GridView;
            //try
            //{
            //    int index = ItemForSumNhanVien.Text.IndexOf(':');
            //    if (index > 0)
            //    {
            //        if (view.RowCount > 0)
            //        {
            //            ItemForSumNhanVien.Text = ItemForSumNhanVien.Text.Substring(0, index) + ": " + view.RowCount.ToString();
            //        }
            //        else
            //        {
            //            ItemForSumNhanVien.Text = ItemForSumNhanVien.Text.Substring(0, index) + ": 0";
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message.ToString());
            //}
        }

        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = grvThang.GetFocusedRowCellValue("NGAY").ToString();
            }
            catch { LoadNull(); }
            cboThang.ClosePopup();
        }

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + cboThang.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                }
                else { }
            }
            catch (Exception ex)
            {
                cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
            }
            cboThang.ClosePopup();
        }

        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadGrdVPNoiQuy();
            grvCongNhan_FocusedRowChanged(null, null);
            Commons.Modules.sLoad = "";
        }
    }
}
