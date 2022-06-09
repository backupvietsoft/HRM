using System;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;

namespace Vs.HRM
{
    public partial class ucXepLoaiKhenThuong : DevExpress.XtraEditors.XtraUserControl
    {
        DXMenuItem[] menuItems;
        public ucXepLoaiKhenThuong()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new System.Collections.Generic.List<DevExpress.XtraLayout.LayoutControlGroup>() { Root }, windowsUIButton);

            DXMenuItem itemDelete = new DXMenuItem("Delete", ItemDelete_Click);
            DXMenuItem item = new DXMenuItem(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgCapNhapAllLoaiXepLoai"), Item_Click);
            menuItems = new DXMenuItem[] { item, itemDelete };
        }

        #region Taomenu Them all LXL
        private void Item_Click(object sender, System.EventArgs e)
        {
            Rectangle cellRect = GetCellRect(grvChung, grvChung.FocusedRowHandle, grvChung.FocusedColumn);
            popLXL.Location = new Point(cellRect.X, cellRect.Y);
            popLXL.Show();
        }

        Rectangle GetCellRect(GridView view, int rowHandle, GridColumn column)
        {
            // the GetGridViewInfo function can be found in article #2624  
            // GridViewInfo info = GetGridViewInfo(view);  
            GridViewInfo info = (GridViewInfo)view.GetViewInfo();

            GridCellInfo cell = info.GetGridCellInfo(rowHandle, column);
            if (cell != null)
                return cell.Bounds;
            return Rectangle.Empty;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            CapNhapLXL();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            popLXL.Hide();
        }
        private void ItemDelete_Click(object sender, System.EventArgs e)
        {
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgCapNhapNullLoaiXepLoai"), "", MessageBoxButtons.YesNo) == DialogResult.No) return;
            try
            {

                DataTable dtTmp = new DataTable();
                dtTmp = (DataTable)grdChung.DataSource;
                dtTmp.Columns["ID_LXL"].Expression = "";
                dtTmp.AcceptChanges();
                grvChung.RefreshData();


                //DataTable table = ((DataView)grvXLKT.DataSource).Table;
                //DataView filteredDataView = new DataView(table);
                //dtTmp.DefaultView.RowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(grvXLKT.ActiveFilterCriteria);
                //dtTmp.Columns["ID_LXL"].Expression = "";
                //dtTmp.AcceptChanges();
                //grvXLKT.RefreshData();
                //DataTable dtt = new DataTable();
                ////searchControl

                //DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(grvXLKT.ActiveFilterCriteria);

                //DataTable table = ((DataView)grvXLKT.DataSource).Table;
                //DataView filteredDataView = new DataView(table);
                ////filteredDataView.RowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(grvXLKT.ActiveFilterCriteria);
                ////searchControl.AccessibilityObject.FilterCriteria

                ////        Dim op As CriteriaOperator = chitiet.ActiveFilterCriteria 'filterControl1.FilterCriteria
                ////Dim filterString As String = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(op)
                ////Dim dv As DataView = New DataView(DatatableABC)
                ////dv.RowFilter = filterString
                ////DatatableABC = dv.ToTable
                //DevExpress.Data.Filtering.CriteriaOperator op =  grvXLKT.ActiveFilterCriteria;
                //string filterString = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(op);
                //searchControl.Client.ToString();
                ////DataTable tempt = dt.ToTable();
                ////tempt = grvXLKT.DataController.GetAllFilteredAndSortedRows();
                ////grvXLKT.DataController.GetAllFilteredAndSortedRows
                ////dtt.Columns["ID_LXL"].Expression = "";
                ////dtt.AcceptChanges();
                ////grvXLKT.RefreshData();




                //GridView gView = grdXLKT.MainView as GridView;


                //string s = searchControl.Properties.FilterCondition.ToString() ;
                //grvXLKT.RefreshData();
            }
            catch (Exception EX)
            { XtraMessageBox.Show(EX.ToString()); }
            
        }
        private void grvXLKT_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (!grvChung.OptionsBehavior.Editable) return;

            if (e.HitInfo.InRow)
            {
                GridView view = sender as GridView;
                view.FocusedRowHandle = e.HitInfo.RowHandle;
                foreach (DXMenuItem item in menuItems)
                    e.Menu.Items.Add(item);
            }
        }
        #endregion

        #region xu ly
        private void LoadThang(DateTime dThang)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spXepLoaiKhenThuong", "01/01/1900", -1,-1, -1, Commons.Modules.UserName, Commons.Modules.TypeLanguage,"", "Cbo"));

            if (grdThang.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dt, false, true, true, true, true, this.Name);
                grvThang.Columns["THANG_KTT"].Visible = false;
            }
            else
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dt, false, false, true, false, false, this.Name);


            try
            {
                if (dThang == Convert.ToDateTime("01/01/1900"))
                    cboThang.Text = dt.Rows[0][0].ToString();
                else
                    cboThang.Text = dThang.ToString("MM/yyyy");
            }
            catch { cboThang.Text = DateTime.Now.ToString("MM/yyyy"); }
        }

        private void ucXepLoaiKhenThuong_Load(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";
            LoadThang(Convert.ToDateTime("01/01/1900"));
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            Commons.Modules.sPS = "";
            LoadLuoi(false, -1);
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
        }

        private void LoadLuoi(Boolean bThemSua, int id)
        {
            if (Commons.Modules.sPS == "0Load") return;
            DataTable dt = new DataTable();
            DateTime dtThang = Convert.ToDateTime("01/01/1900");
            try
            {
                dtThang = Convert.ToDateTime("01/" + cboThang.Text.ToString());
            }
            catch { }

            if (!bThemSua)
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spXepLoaiKhenThuong", dtThang, (cboDV.EditValue.ToString() == "" ? -1 : cboDV.EditValue), (cboXN.EditValue.ToString() == "" ? -1 : cboXN.EditValue), (cboTo.EditValue.ToString() == "" ? -1 : cboTo.EditValue), Commons.Modules.UserName, Commons.Modules.TypeLanguage, "", "Grd"));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CN"] };

                if (grdChung.DataSource == null)
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, false, true, true, true, true, this.Name);
                else
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, false, false, true, false, false, this.Name);
            }
            else
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spXepLoaiKhenThuong", dtThang, (cboDV.EditValue.ToString() == "" ? -1 : cboDV.EditValue), (cboXN.EditValue.ToString() == "" ? -1 : cboXN.EditValue), (cboTo.EditValue.ToString() == "" ? -1 : cboTo.EditValue), Commons.Modules.UserName, Commons.Modules.TypeLanguage, "", "Add"));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CN"] };

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, true, true, true, true, true, this.Name);
                if (grdChung.DataSource == null)
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, true, true, true, true, true, this.Name);
                else
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, true, false, true, false, false, this.Name);

                
                grvChung.Columns["MS_CN"].OptionsColumn.ReadOnly = true;
                grvChung.Columns["ID_CN"].OptionsColumn.ReadOnly = true;
                grvChung.Columns["HO_TEN"].OptionsColumn.ReadOnly = true;
            }
            grvChung.Columns["ID_CN"].Visible = false;
            grvChung.Columns["TS"].Visible = false;
            try
            {
                lblTongCong.Text = dt.Rows[0]["TS"].ToString();
            }
            catch { }
            if (id != -1)
            {
                try
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(id));
                    grvChung.FocusedRowHandle = grvChung.GetRowHandle(index);
                }
                catch { }
            }

            DataTable dt1 = new DataTable();
            dt1.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spXepLoaiKhenThuong", dtThang, -1, -1, -1, Commons.Modules.UserName, Commons.Modules.TypeLanguage, "", "LXL"));
            RepositoryItemSearchLookUpEdit cboLKT = new RepositoryItemSearchLookUpEdit();
            Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cboLKT, "ID_LXL", "TEN_LXL", grvChung, dt1);
            cboLKT.View.Columns.AddField("TEN_LXL").Visible = true;


            //if (grdLXL.DataSource == null)
            //{
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdLXL, grvLXL, dt1, false, true, true, true, true, this.Name);
                RepositoryItemPopupContainerEdit ritem = new RepositoryItemPopupContainerEdit();
                ritem.PopupControl = popLXL;
                grvLXL.Columns["ID_LXL"].Visible = false;
            //}
        }
        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            LoadLuoi(false, -1);
        }

        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                cboThang.Text = grvThang.GetFocusedRowCellValue("T_NAME").ToString();
            }
            catch { cboThang.Text = DateTime.Now.ToString("MM/yyyy"); }
            cboThang.ClosePopup();
        }

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboThang.Text = calThang.DateTime.Date.ToString("MM/yyyy");
            }
            catch { cboThang.Text = DateTime.Now.ToString("MM/yyyy"); }
            cboThang.ClosePopup();
        }

        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            cboTo.EditValue = -1;
            Commons.Modules.sPS = "";
            LoadLuoi(false, -1);
        }

        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            cboTo.EditValue = -1;
            Commons.Modules.sPS = "";
            LoadLuoi(false, -1);
        }

        #endregion

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            try
            {
                switch (btn.Tag.ToString())
                {
                    case "them":
                        {
                            enableButon(false);
                            LoadLuoi(true, -1);
                            break;

                        }
                    case "khongluu":
                        {
                            grvChung.RefreshData();
                            enableButon(true);
                            LoadLuoi(false, -1);
                            break;
                        }
                    case "luu":
                        {

                            grvChung.PostEditor();
                            grvChung.UpdateCurrentRow();
                            if(!SaveData()) return;
                            enableButon(true);
                            DateTime dThang = Convert.ToDateTime(cboThang.Text);
                            Commons.Modules.sPS = "0Load";
                            LoadThang(dThang);
                            int idCN = int.Parse(grvChung.GetFocusedRowCellValue("ID_CN").ToString());
                            Commons.Modules.sPS = "";
                            LoadLuoi(false, idCN);
                            break;
                        }
                    case "thoat":
                        {
                            Commons.Modules.ObjSystems.GotoHome(this);
                            break;
                        }
                    case "in":
                        {
                            int idCN = int.Parse(grvChung.GetFocusedRowCellValue("ID_CN").ToString());
                            frmXepLoaiKhenThuong gd = new frmXepLoaiKhenThuong(idCN);
                            gd.ShowDialog();
                            break;
                        }
                    case "xoa":
                        {
                            DeleteData(-1);
                            break;
                        }
                    case "CNLXL":
                        {
                            popLXL.Location = new Point(this.Location.X + (this.Size.Width / 2) - (grdLXL.Size.Width / 2),
                                                       this.Location.Y + (grdChung.Height / 4)
                                                     );
                            popLXL.Show();
                            break;
                        }
                    case "LoaIXL":
                        {
                            frmLoaiXepLoai frm = new frmLoaiXepLoai();
                            frm.ShowDialog();
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (Exception Ex)
            {
                XtraMessageBox.Show(Ex.Message.ToLower());

            }
        }
        private void enableButon(bool visible)
        {
            try
            {
                windowsUIButton.Buttons[0].Properties.Visible = visible;
                windowsUIButton.Buttons[4].Properties.Visible = visible;
                windowsUIButton.Buttons[5].Properties.Visible = visible;
                windowsUIButton.Buttons[6].Properties.Visible = visible;
                windowsUIButton.Buttons[11].Properties.Visible = visible;
                windowsUIButton.Buttons[1].Properties.Visible = false;
                windowsUIButton.Buttons[7].Properties.Visible = visible;
                windowsUIButton.Buttons[8].Properties.Visible = visible;

                windowsUIButton.Buttons[9].Properties.Visible = !visible;
                windowsUIButton.Buttons[2].Properties.Visible = !visible;
                windowsUIButton.Buttons[10].Properties.Visible = !visible;


                cboThang.Properties.ReadOnly = !visible;
                cboDV.Properties.ReadOnly = !visible;
                cboXN.Properties.ReadOnly = !visible;
                cboTo.Properties.ReadOnly = !visible;

                popLXL.Hide();
                popListThang.Hide();
                popThang.Hide();
            }
            catch { }

        }

        private bool SaveData()
        {
            try
            {
                string sBT = "XLKT" + Commons.Modules.UserName;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, (DataTable)grdChung.DataSource, "");
                DateTime dtThang = Convert.ToDateTime("01/01/1900");
                try
                {
                    dtThang = Convert.ToDateTime("01/" + cboThang.Text.ToString());
                }
                catch { }
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spXepLoaiKhenThuong", dtThang, (cboDV.EditValue.ToString() == "" ? -1 : cboDV.EditValue), (cboXN.EditValue.ToString() == "" ? -1 : cboXN.EditValue), (cboTo.EditValue.ToString() == "" ? -1 : cboTo.EditValue), Commons.Modules.UserName, Commons.Modules.TypeLanguage,sBT, "Save");

                Commons.Modules.ObjSystems.XoaTable(sBT);
                return true;
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                return false;
            }
        }




        #region Xu ly cbo tren luoi
        //////////cboCNhan.EditValueChanged += new EventHandler(cboCNhan_EditValueChanged);
        //////////private void cboCNhan_EditValueChanged(object sender, EventArgs e)
        //////////{
        //////////    // Question 1: How to get the correct value for each column from the lookUpEdits, based on the contactID? 
        //////////    // Like what is done in gridView1_CellValueChanging using GetDataSourceRowByKeyValue method.

        //////////    GridView view = (GridView)grdXLKT.FocusedView;
        //////////    if (view.FocusedColumn.FieldName == "ID_CN")
        //////////    {
        //////////        try
        //////////        {
        //////////            //view.SetRowCellValue(view.FocusedRowHandle, view.Columns["ID_CN"], "1");
        //////////            var searchLookUpEdit = sender as SearchLookUpEdit;
        //////////            if (searchLookUpEdit == null)
        //////////                return;
        //////////            RepositoryItemSearchLookUpEdit riSearchLookUpEdit = searchLookUpEdit.Properties as RepositoryItemSearchLookUpEdit;

        //////////            object displayMemeber = searchLookUpEdit.Properties.GetDisplayValueByKeyValue(searchLookUpEdit.EditValue);

        //////////            //DataRowView row = riSearchLookUpEdit.Properties.GetRowByKeyValue(searchLookUpEdit.EditValue) as DataRowView;
        //////////            var rowByKeyValue = riSearchLookUpEdit.GetRowByKeyValue(searchLookUpEdit.EditValue);
        //////////            DataRowView row = (DataRowView)rowByKeyValue;

        //////////            String Ho = (String)(row["HO"]);
        //////////            view.SetFocusedRowCellValue(view.Columns["HO"], Ho);
        //////////            String Ten = (String)(row["TEN"]);
        //////////            view.SetFocusedRowCellValue(view.Columns["TEN"], Ten);

        //////////            Int64 ID = (Int64)(row["ID_CN"]);
        //////////            view.SetFocusedRowCellValue(view.Columns["ID_CN"], ID);

        //////////        }
        //////////        catch (Exception EX)
        //////////        { }
        //////////    }

        //////////}
        #endregion

        #region "Xu Ly Luoi"
        //////////private void grvXLKT_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        //////////{
        //////////    try
        //////////    {
        //////////        GridView view = (GridView)grdXLKT.FocusedView;
        //////////        grvXLKT.ClearColumnErrors();
        //////////        if (string.IsNullOrEmpty(view.GetRowCellValue(e.RowHandle, view.Columns["ID_LXL"]).ToString()))
        //////////        {
        //////////            e.Valid = false;
        //////////            view.SetColumnError(view.Columns["ID_LXL"], Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "msgChuaNhapLoaiXepLoai", Commons.Modules.TypeLanguage));
        //////////        }
        //////////        if (string.IsNullOrEmpty(view.GetRowCellValue(e.RowHandle, view.Columns["ID_CN"]).ToString()))
        //////////        {
        //////////            e.Valid = false;
        //////////            view.SetColumnError(view.Columns["ID_CN"], Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "msgChuaNhapCongNhan", Commons.Modules.TypeLanguage));
        //////////            view.SetColumnError(view.Columns["HO"], Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "msgChuaNhapCongNhan", Commons.Modules.TypeLanguage));
        //////////            view.SetColumnError(view.Columns["TEN"], Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "msgChuaNhapCongNhan", Commons.Modules.TypeLanguage));
        //////////        }
        //////////        DataTable tempt = Commons.Modules.ObjSystems.ConvertDatatable(grvXLKT);
        //////////        int n = 0;
        //////////        //kiểm tra trùng trên view
        //////////        try
        //////////        {
        //////////            n = tempt.AsEnumerable().Count(x => x.Field<Int64>("ID_CN") == Convert.ToInt64(view.GetRowCellValue(e.RowHandle, view.Columns["ID_CN"])) && x.Field<Int64>("ID_LXL") == Convert.ToInt64(view.GetRowCellValue(e.RowHandle, view.Columns["ID_LXL"])));
        //////////        }
        //////////        catch (Exception ex)
        //////////        {
        //////////        }
        //////////        if (n > 1)
        //////////        {
        //////////            e.Valid = false;
        //////////            view.SetColumnError(view.Columns["ID_CN"], Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "msgTrungDuLieuXepLoaiKhenThuong", Commons.Modules.TypeLanguage));

        //////////        }
        //////////    }
        //////////    catch(Exception EX)
        //////////    { }
        //////////}

        //////////private void grvXLKT_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        //////////{
        //////////    e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;

        //////////}
        #endregion

        private void grvLXL_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);
            if (info.InRow || info.InRowCell)
            {
                CapNhapLXL();
            }
        }
        private void CapNhapLXL()
        {
            try
            {
                string sT = grvLXL.GetFocusedRowCellValue("TEN_LXL").ToString();
                if (sT != "") sT = " : " + sT;
                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgCapNhapLoaiXepLoai") + sT , Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                int iLXL = int.Parse(grvLXL.GetFocusedRowCellValue("ID_LXL").ToString());
                DataTable dtTmp = new DataTable();
                dtTmp = (DataTable)grdChung.DataSource;
                if (iLXL == -1)
                    dtTmp.Columns["ID_LXL"].Expression = "";
                else
                    dtTmp.Select(string.Format("ISNULL([ID_LXL],0) = 0 OR ISNULL([ID_LXL],0) = -1")).ToList<DataRow>().ForEach(r => r["ID_LXL"] = iLXL);

                dtTmp.AcceptChanges();
                grvChung.RefreshData();
            }
            catch (Exception EX)
            { XtraMessageBox.Show(EX.ToString()); }
            popLXL.Hide();
        }

        private void grdXLKT_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (windowsUIButton.Buttons[1].Properties.Visible) return;
            if (e.KeyCode != Keys.Delete) return;

            int iIdCN = -1;
            try { iIdCN = int.Parse(grvChung.GetFocusedRowCellValue("ID_CN").ToString()); } catch { }
            if (iIdCN == -1)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgKhongCoDuLieuXoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DeleteData(iIdCN);
        }

        private void DeleteData(int iIdCN)
        {
            if (grvChung.RowCount == 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgKhongCoDuLieuXoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (iIdCN == -1)
            {
                if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoaAll) == DialogResult.No) return;
            }
            else
            {
                if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            }

            DateTime dThang = Convert.ToDateTime("01/01/1900");
            try
            {
                dThang = Convert.ToDateTime(cboThang.Text.ToString());
            }
            catch { }
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spXepLoaiKhenThuong", dThang, iIdCN, -1, -1, Commons.Modules.UserName, Commons.Modules.TypeLanguage, "", "Delete");

                Commons.Modules.sPS = "0Load";
                if (iIdCN == -1)
                    LoadThang(Convert.ToDateTime("01/01/1900"));
                else
                    LoadThang(dThang);
                Commons.Modules.sPS = "";
                LoadLuoi(false, -1);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnOk_Click_1(object sender, EventArgs e)
        {
            CapNhapLXL();
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            popLXL.Hide();
        }
    }
}
