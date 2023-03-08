using Commons;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using Excell = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

using Vs.Report;
//using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils.Menu;
using DevExpress.XtraDataLayout;

namespace Vs.TimeAttendance
{
    public partial class ucPhepTon : DevExpress.XtraEditors.XtraUserControl
    {
        public static ucPhepTon _instance;
        public static ucPhepTon Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucPhepTon();
                return _instance;
            }
        }

        static string CharacterIncrement(int colCount)
        {
            int TempCount = 0;
            string returnCharCount = string.Empty;

            if (colCount <= 25)
            {
                TempCount = colCount;
                char CharCount = Convert.ToChar((Convert.ToInt32('A') + TempCount));
                returnCharCount += CharCount;
                return returnCharCount;
            }
            else
            {
                var rev = 0;

                while (colCount >= 26)
                {
                    colCount = colCount - 26;
                    rev++;
                }

                returnCharCount += CharacterIncrement(rev - 1);
                returnCharCount += CharacterIncrement(colCount);
                return returnCharCount;
            }
        }

        string sBT = "tabKeHoachDiCa" + Commons.Modules.ModuleName;
        private DateTime dNgayDauNam;
        private DateTime dNgayCuoiNam;
        private int bThem = 0;
        // private SqlConnection conn;

        public ucPhepTon()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
        }
        private void ucPhepTon_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadThang();

            string datetime = "01/01/" + Convert.ToString(cboNam.Text);
            dNgayCuoiNam = Convert.ToDateTime(datetime);
            datetime = "31/12/" + Convert.ToString(cboNam.Text);
            dNgayCuoiNam = Convert.ToDateTime(datetime);

            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDV, dt, "ID_DV", "TEN_DV", "TEN_DV");
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadData(bThem);
            Commons.Modules.sLoad = "";
            enableButon(true);
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadData(bThem);
            Commons.Modules.sLoad = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadData(bThem);
            Commons.Modules.sLoad = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadData(bThem);
            Commons.Modules.sLoad = "";
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "tinhphepton":
                    {
                        Commons.Modules.ObjSystems.ShowWaitForm(this);
                        try
                        {
                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTinhPhepTon", conn);
                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = cboDV.EditValue;
                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = cboXN.EditValue;
                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = cboTo.EditValue;
                            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                            cmd.Parameters.Add("@NAM", SqlDbType.Int).Value = Convert.ToInt32(cboNam.Text);
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[1].Copy();
                            grdPhepTon.DataSource = dt;
                        }
                        catch (Exception)
                        {
                        }
                        Commons.Modules.ObjSystems.HideWaitForm();
                        break;
                    }
                case "thanhtoanphep":
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            dt = (DataTable)grdPhepTon.DataSource;

                            dt.AsEnumerable().Where(row => dt.AsEnumerable()
                                                                     .Select(r => r.Field<Int64>("ID_CN"))
                                                                     .Any(x => x == row.Field<Int64>("ID_CN"))
                                                                     ).ToList<DataRow>().ForEach(r => r["PHEP_THANH_TOAN"] = Convert.ToDouble(r["PHEP_TON"]));

                            dt.AsEnumerable().Where(row1 => dt.AsEnumerable()
                                                                     .Select(r => r.Field<Int64>("ID_CN"))
                                                                     .Any(x => x == row1.Field<Int64>("ID_CN"))
                                                                     ).ToList<DataRow>().ForEach(r => r["PHEP_CON_LAI"] = Convert.ToDouble(r["PHEP_TON"]) - Convert.ToDouble(r["PHEP_THANH_TOAN"]));
                            dt.AcceptChanges();
                        }
                        catch
                        {

                        }
                        break;
                    }
                case "themsua":
                    {
                        bThem = 1;
                        LoadData(bThem);
                        enableButon(false);
                        break;
                    }
                case "xoa":
                    {
                        if (grvPhepTon.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
                        if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
                        //xóa
                        try
                        {
                            string sBT = "sBTPhepTon" + Commons.Modules.iIDUser;
                            //tạo bảng tạm
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdPhepTon,grvPhepTon), "");

                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTinhPhepTon", conn);
                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                            cmd.Parameters.Add("@NAM", SqlDbType.Int).Value = Convert.ToInt32(cboNam.Text);
                            cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                            cmd.Parameters.Add("@bThem", SqlDbType.Int).Value = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0].Copy();
                            if (dt.Rows[0][0].ToString() == "-99")
                            {
                                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgXoaKhongThanhCong"), Commons.Form_Alert.enmType.Error);
                                XtraMessageBox.Show(dt.Rows[0][1].ToString());
                            }
                            else
                            {
                                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_XoaThanhCong"), Commons.Form_Alert.enmType.Success);
                            }
                            Commons.Modules.ObjSystems.XoaTable(sBT);
                            bThem = 0;
                            LoadData(bThem);
                        }
                        catch
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
                        }
                        LoadThang();
                        LoadData(bThem);
                        break;
                    }
                case "luu":
                    {

                        //DataTable tb = new DataTable();
                        //tb = (DataTable)grdPhepThang.DataSource;

                        string sBT = "sBTPhepTon" + Commons.Modules.iIDUser;
                        //tạo bảng tạm
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grvPhepTon), "");

                        System.Data.SqlClient.SqlConnection conn;
                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                        conn.Open();
                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTinhPhepTon", conn);
                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                        cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                        cmd.Parameters.Add("@NAM", SqlDbType.Int).Value = Convert.ToInt32(cboNam.Text);
                        cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                        cmd.Parameters.Add("@bThem", SqlDbType.Int).Value = 1;
                        cmd.CommandType = CommandType.StoredProcedure;
                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adp.Fill(ds);
                        DataTable dt = new DataTable();
                        dt = ds.Tables[0].Copy();
                        if (dt.Rows[0][0].ToString() == "-99")
                        {
                            Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                            XtraMessageBox.Show(dt.Rows[0][1].ToString());
                        }
                        else
                        {
                            Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCong"), Commons.Form_Alert.enmType.Success);
                        }
                        Commons.Modules.ObjSystems.XoaTable(sBT);
                        bThem = 0;
                        LoadData(bThem);
                        enableButon(true);
                        break;
                    }
                case "khongluu":
                    {
                        bThem = 0;
                        LoadData(bThem);
                        //Commons.Modules.ObjSystems.DeleteAddRow(grvPhepThang);
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
        private void GrvPhepThang_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            throw new NotImplementedException();
        }
        #region hàm xử lý dữ liệu
        private void LoadData(int bThem)
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTinhPhepTon", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = cboDV.EditValue;
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = cboXN.EditValue;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = cboTo.EditValue;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@bThem", SqlDbType.Int).Value = bThem;
                cmd.Parameters.Add("@NAM", SqlDbType.Int).Value = Convert.ToInt32(cboNam.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.Columns["PHEP_CON_LAI"].ReadOnly = false;
                if (grdPhepTon.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdPhepTon, grvPhepTon, dt, true, true, false, true, true, this.Name);
                    grvPhepTon.Columns["ID_CN"].Visible = false;
                    grvPhepTon.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                    grvPhepTon.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                    grvPhepTon.Columns["TEN_TO"].OptionsColumn.AllowEdit = false;
                    grvPhepTon.Columns["NGAY_VAO_LAM"].OptionsColumn.AllowEdit = false;
                    grvPhepTon.Columns["PHEP_CON_LAI"].OptionsColumn.AllowEdit = false;
                    //grvPhepTon.Columns["PHEP_TON"].OptionsColumn.AllowEdit = false;
                }
                else
                {
                    grdPhepTon.DataSource = dt;
                }
            }
            catch (Exception)
            {
            }
        }

        private bool Savedata()
        {
            try
            {
                //Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grvKeHoachDiCa), "");
                //string sSql = "DELETE KE_HOACH_dI_CA WHERE ID_CN = " + grvCongNhan.GetFocusedRowCellValue("ID_CN") + " INSERT INTO KE_HOACH_dI_CA(ID_CN,ID_NHOM,CA,TU_NGAY,DEN_NGAY,GHI_CHU) SELECT ID_CN,ID_NHOM,CA,TU_NGAY,DEN_NGAY,GHI_CHU FROM " + sBT + "";
                //SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                return true;
            }
            catch
            {
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
            windowsUIButton.Buttons[6].Properties.Visible = !visible;
            windowsUIButton.Buttons[7].Properties.Visible = !visible;
            windowsUIButton.Buttons[8].Properties.Visible = !visible;

            //searchControl.Visible = visible;
            cboNam.Properties.ReadOnly = !visible;
            cboDV.Properties.ReadOnly = !visible;
            cboXN.Properties.ReadOnly = !visible;
            cboTo.Properties.ReadOnly = !visible;

            grvPhepTon.OptionsBehavior.Editable = !visible;

        }
        #endregion
        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData(bThem);
            Commons.Modules.sLoad = "";
        }
        public void LoadThang()
        {
            try
            {
                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT DISTINCT NAM FROM dbo.PHEP_TON T1 ORDER BY NAM";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);

                cboNam.Text = grvThang.GetFocusedRowCellValue("NAM").ToString();
            }
            catch (Exception ex)
            {
                DateTime now = DateTime.Now;

                cboNam.Text = now.ToString("yyyy");
            }
        }
        private void calendarControl1_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboNam.Text = calThangc.DateTime.ToString("yyyy");
            }
            catch (Exception ex)
            {
                cboNam.Text = calThangc.DateTime.ToString("yyyy");
            }
            cboNam.ClosePopup();
        }

        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboNam.Text = grvThang.GetFocusedRowCellValue("NAM").ToString();
            }
            catch { }
            cboNam.ClosePopup();

        }

        private void BorderAround(Excell.Range range)
        {
            Excell.Borders borders = range.Borders;
            borders[Excell.XlBordersIndex.xlEdgeLeft].LineStyle = Excell.XlLineStyle.xlContinuous;
            borders[Excell.XlBordersIndex.xlEdgeTop].LineStyle = Excell.XlLineStyle.xlContinuous;
            borders[Excell.XlBordersIndex.xlEdgeBottom].LineStyle = Excell.XlLineStyle.xlContinuous;
            borders[Excell.XlBordersIndex.xlEdgeRight].LineStyle = Excell.XlLineStyle.xlContinuous;
            borders.Color = Color.Black;
            borders[Excell.XlBordersIndex.xlInsideVertical].LineStyle = Excell.XlLineStyle.xlContinuous;
            borders[Excell.XlBordersIndex.xlInsideHorizontal].LineStyle = Excell.XlLineStyle.xlContinuous;
            borders[Excell.XlBordersIndex.xlDiagonalUp].LineStyle = Excell.XlLineStyle.xlLineStyleNone;
            borders[Excell.XlBordersIndex.xlDiagonalDown].LineStyle = Excell.XlLineStyle.xlLineStyleNone;
        }
        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                obj = null;
            }
            finally
            { GC.Collect(); }
        }

        private void grvPhepThang_RowCountChanged(object sender, EventArgs e)
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

        private void grvPhepTon_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            double dPhepTon;
            try
            {
                var row = view.GetFocusedDataRow();

                if (e.Column.FieldName == "PHEP_THANH_TOAN")
                {
                    dPhepTon = Convert.ToDouble(grvPhepTon.GetFocusedRowCellValue("PHEP_TON"));
                    row["PHEP_CON_LAI"] = dPhepTon - Convert.ToDouble(e.Value);
                }
            }
            catch (Exception ex) { }
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
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                string sCotCN = "";
                double data = 0;

                sCotCN = grvPhepTon.FocusedColumn.FieldName;
                data = Convert.ToDouble(grvPhepTon.GetFocusedRowCellValue(sCotCN));
                dt1 = Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdPhepTon, grvPhepTon);
                dt = (DataTable)grdPhepTon.DataSource;

                dt.AsEnumerable().Where(row => dt1.AsEnumerable()
                                                         .Select(r => r.Field<Int64>("ID_CN"))
                                                         .Any(x => x == row.Field<Int64>("ID_CN"))
                                                         ).ToList<DataRow>().ForEach(r => r[sCotCN] = (data));

                dt.AsEnumerable().Where(row1 => dt1.AsEnumerable()
                                                         .Select(r => r.Field<Int64>("ID_CN"))
                                                         .Any(x => x == row1.Field<Int64>("ID_CN"))
                                                         ).ToList<DataRow>().ForEach(r => r["PHEP_CON_LAI"] =  Convert.ToDouble(r["PHEP_TON"]) - (data));
                dt.AcceptChanges();
            }
            catch
            {

            }
        }
        private void grvPhepTon_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();
                    
                    if (windowsUIButton.Buttons[0].Properties.Visible) return;
                    if (grvPhepTon.FocusedColumn.FieldName.ToString() != "PHEP_THANH_TOAN") return;
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

        private void grvPhepTon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.I)
            {
                frmImportPhepTon frm = new frmImportPhepTon();
                frm.Nam = Convert.ToInt32(cboNam.Text);
                if(frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData(bThem);
                }
                else
                {
                    LoadData(bThem);
                }

            }
        }
    }
}
