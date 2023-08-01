using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraLayout;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Menu;
using System.Windows.Forms;

namespace Vs.HRM
{
    public partial class ucCapNhatLCB : DevExpress.XtraEditors.XtraUserControl
    {
        public static ucCapNhatLCB _instance;
        public static ucCapNhatLCB Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucCapNhatLCB();
                return _instance;
            }
        }
        private string ChuoiKT = "";

        public ucCapNhatLCB()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, windowsUIButton);
        }
        #region envent form
        private void ucCapNhatLCB_Load(object sender, EventArgs e)
        {
            formatText();
            //load đơn vị xí nghiệp tổ
            Commons.Modules.sLoad = "0Load";
            NGAY_HIEU_LUCdateEdit.EditValue = DateTime.Now;
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdCapNhatLCB();
            radBoloc_EditValueChanged(null, null);
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            Commons.Modules.sLoad = "";
        }

        private void formatText()
        {
            Commons.OSystems.SetDateEditFormat(NGAY_HIEU_LUCdateEdit);

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
                    case "themsua":
                        {
                            grvCapNhatLCB.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
                            grvCapNhatLCB.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                            grvCapNhatLCB.OptionsSelection.CheckBoxSelectorField = "CHON";
                            enableButon(false);
                            break;
                        }
                    case "thuchien":
                        {
                            try
                            {
                                if (Commons.Modules.ObjSystems.ConvertDatatable(grvCapNhatLCB).AsEnumerable().Count(x => x.Field<bool>("CHON").Equals(true)) == 0)
                                {
                                    Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgBanChuaChonDuLieu); return;
                                }
                            }
                            catch
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgBanChuaChonDuLieu); return;
                            }
                            //dxValidationProvider1.Dispose();
                            //DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
                            //conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
                            //conditionValidationRule1.ErrorText = "This value is not valid";
                            //conditionValidationRule1.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;

                            //dxValidationProvider1.SetValidationRule(this.NGAY_HIEU_LUCdateEdit, conditionValidationRule1);

                            if (CapNhapTheoDieuKien() == false)
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgBanChuaChonDuLieu); return;
                            }
                            enableButon(false);
                            break;
                        }
                    case "luu":
                        {
                            grvCapNhatLCB.CloseEditor();
                            grvCapNhatLCB.UpdateCurrentRow();
                            if (Convert.ToInt32(Commons.Modules.ObjSystems.ConvertDatatable(grvCapNhatLCB).AsEnumerable().Count(x => x["CHON"].ToString().Trim().ToUpper() == "TRUE")) == 0)
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgBanChuaChonDuLieu); return;
                            }

                            DataTable dtSoure = new DataTable();
                            dtSoure = (DataTable)grdCapNhatLCB.DataSource;

                            if (!KiemTraLuoi(dtSoure))
                            {
                                return;
                            }
                            if (Savedata() == false)
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgCapNhatThatBai); return;
                            }
                            else
                            {
                                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgLuuThanhCong"), Commons.Form_Alert.enmType.Success);
                            }
                            Uncheck();
                            LoadGrdCapNhatLCB();
                            enableButon(true);
                            break;
                        }
                    case "khongluu":
                        {
                            Uncheck();
                            LoadGrdCapNhatLCB();
                            enableButon(true);
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
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdCapNhatLCB();
            Commons.Modules.sLoad = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdCapNhatLCB();
            Commons.Modules.sLoad = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdCapNhatLCB();
            Commons.Modules.sLoad = "";
        }
        private void radBoloc_EditValueChanged(object sender, EventArgs e)
        {
            if (radBoloc.SelectedIndex == 0)
            {
                LuongTutextEdit.Enabled = false;
                LuongDentextEdit.Enabled = false;
            }
            else
            {
                LuongTutextEdit.Enabled = true;
                LuongDentextEdit.Enabled = true;
            }
        }
        #endregion

        #region funciton load data
        private void LoadGrdCapNhatLCB()
        {

            DataTable dt = new DataTable();
            try
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCapNhatLCB", Convert.ToInt64(cboDV.EditValue), Convert.ToInt64(cboXN.EditValue), Convert.ToInt64(cboTo.EditValue), radBoloc.SelectedIndex, LuongTutextEdit.EditValue, LuongDentextEdit.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, NGAY_HIEU_LUCdateEdit.DateTime));
                dt.Columns["CHON"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdCapNhatLCB, grvCapNhatLCB, dt, true, false, false, true, true, this.Name);
                grvCapNhatLCB.Columns["CHON"].Visible = false;
                grvCapNhatLCB.Columns["ID_CN"].Visible = false;
                grvCapNhatLCB.Columns["ID_TO"].Visible = false;
                grvCapNhatLCB.Columns["ID_CV"].Visible = false;

                grvCapNhatLCB.Columns["CHON"].Visible = false;
                grvCapNhatLCB.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
                grvCapNhatLCB.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;

                grvCapNhatLCB.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvCapNhatLCB.Columns["MS_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvCapNhatLCB.Columns["SO_QUYET_DINH"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                grvCapNhatLCB.Columns["LUONG_CO_BAN"].DisplayFormat.FormatType = FormatType.Numeric;
                grvCapNhatLCB.Columns["LUONG_CO_BAN"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvCapNhatLCB.Columns["PC_DOC_HAI"].DisplayFormat.FormatType = FormatType.Numeric;
                grvCapNhatLCB.Columns["PC_DOC_HAI"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvCapNhatLCB.Columns["PC_SINH_HOAT"].DisplayFormat.FormatType = FormatType.Numeric;
                grvCapNhatLCB.Columns["PC_SINH_HOAT"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvCapNhatLCB.Columns["THUONG_CHUYEN_CAN"].DisplayFormat.FormatType = FormatType.Numeric;
                grvCapNhatLCB.Columns["THUONG_CHUYEN_CAN"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvCapNhatLCB.Columns["PC_KY_NANG"].DisplayFormat.FormatType = FormatType.Numeric;
                grvCapNhatLCB.Columns["PC_KY_NANG"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvCapNhatLCB.Columns["THUONG_HT_CV"].DisplayFormat.FormatType = FormatType.Numeric;
                grvCapNhatLCB.Columns["THUONG_HT_CV"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvCapNhatLCB.Columns["MUC_LUONG_THUC"].DisplayFormat.FormatType = FormatType.Numeric;
                grvCapNhatLCB.Columns["MUC_LUONG_THUC"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                ItemForSumNhanVien.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, ItemForSumNhanVien.Name) + ": " + grvCapNhatLCB.RowCount.ToString();


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
                grvCapNhatLCB.Columns["ID_NK"].ColumnEdit = cboID_NK;


                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_NL = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                cboID_NL.NullText = "";
                cboID_NL.ValueMember = "ID_NL";
                cboID_NL.DisplayMember = "MS_NL";
                //ID_VTTD,TEN_VTTD
                cboID_NL.DataSource = Commons.Modules.ObjSystems.DataNgachLuong(false);
                cboID_NL.Columns.Clear();
                cboID_NL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MS_NL"));
                cboID_NL.Columns["MS_NL"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "MS_NL");
                cboID_NL.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboID_NL.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                //////cboID_NL.Columns["ID_NL"].Visible = false;
                grvCapNhatLCB.Columns["ID_NL"].ColumnEdit = cboID_NL;
                cboID_NL.BeforePopup += cboID_NL_BeforePopup;
                cboID_NL.EditValueChanged += cboID_NL_EditValueChanged;

                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_BL = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                cboID_BL.NullText = "";
                cboID_BL.ValueMember = "ID_BL";
                cboID_BL.DisplayMember = "TEN_BL";
                cboID_BL.DataSource = Commons.Modules.ObjSystems.DataBacLuong(-1, Convert.ToInt64(cboDV.EditValue), DateTime.Now, false);
                cboID_BL.Columns.Clear();
                cboID_BL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_BL"));
                cboID_BL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_BL"));
                cboID_BL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MUC_LUONG"));
                cboID_BL.Columns["TEN_BL"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_BL");
                cboID_BL.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboID_BL.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cboID_BL.Columns["ID_BL"].Visible = false;
                grvCapNhatLCB.Columns["ID_BL"].ColumnEdit = cboID_BL;
                cboID_BL.Columns["MUC_LUONG"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "MUC_LUONG");
                cboID_BL.Columns["MUC_LUONG"].FormatType = DevExpress.Utils.FormatType.Numeric;
                if (Commons.Modules.iHeSo == 0)
                {
                    cboID_BL.Columns["MUC_LUONG"].FormatString = "N0";
                }
                else
                {
                    cboID_BL.Columns["MUC_LUONG"].FormatString = "N2";
                }
                cboID_BL.BeforePopup += cboID_BL_BeforePopup;
                cboID_BL.EditValueChanged += cboID_BL_EditValueChanged;


            }
            catch
            {

            }
        }
        private void cboID_BL_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                grvCapNhatLCB.SetFocusedRowCellValue("ID_BL", Convert.ToInt64((dataRow.Row[0])));

                if (Commons.Modules.iHeSo == 0)
                {
                    grvCapNhatLCB.SetFocusedRowCellValue("LUONG_CO_BAN", Convert.ToDouble((dataRow.Row[2])));
                }
                else
                {
                    string sSQL = "SELECT dbo.funGetLuongToiThieu(" + grvCapNhatLCB.GetFocusedRowCellValue("ID_CN") + ",'" + NGAY_HIEU_LUCdateEdit.DateTime.ToString("MM/dd/yyyy") + "')";
                    double dLuongToiThieu = Convert.ToDouble(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSQL));
                    grvCapNhatLCB.SetFocusedRowCellValue("LUONG_CO_BAN", Convert.ToDouble((dataRow.Row[2])) * dLuongToiThieu);
                    grvCapNhatLCB.SetFocusedRowCellValue("HS_LUONG", Convert.ToDouble(dataRow.Row[2]));
                    grvCapNhatLCB.SetFocusedRowCellValue("MUC_LUONG_THUC", Convert.ToDouble((dataRow.Row[2])) * dLuongToiThieu);
                }
            }
            catch (Exception ex) { }
        }
        private void cboID_NL_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;

            }
            catch { }
        }
        private void cboID_BL_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                DataTable dt1 = new DataTable();
                //////dt1.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_BL, MUCLUONG FROM dbo.BAC_LUONG " ));
                dt1 = Commons.Modules.ObjSystems.DataBacLuong(Convert.ToInt64(grvCapNhatLCB.GetFocusedRowCellValue("ID_NL")), Convert.ToInt64(cboDV.EditValue), DateTime.Now, false);
                lookUp.Properties.DataSource = dt1;

            }
            catch { }
        }
        private void cboID_NL_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                DataTable dt1 = new DataTable();
                //////dt1.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_BL, MUCLUONG FROM dbo.BAC_LUONG " ));
                dt1 = Commons.Modules.ObjSystems.DataNgachLuong(false);
                lookUp.Properties.DataSource = dt1;

            }
            catch { }
        }
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = !visible;
            windowsUIButton.Buttons[3].Properties.Visible = !visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
            groChonDuLieu.Enabled = visible;
            searchControl.Visible = visible;
            grvCapNhatLCB.OptionsBehavior.Editable = !visible;
        }

        private void Uncheck()
        {

            NGAY_HIEU_LUCdateEdit.EditValue = DateTime.Now;
            LUONG_CO_BANtextEdit.EditValue = 0;
            NOI_DUNGtextEdit.EditValue = "";
        }
        private bool CapNhapTheoDieuKien()
        {
            DataTable dtCN = new DataTable();
            dtCN = (DataTable)grdCapNhatLCB.DataSource;
            try
            {
                dtCN.AsEnumerable().Where(x => x["CHON"].ToString().ToUpper().Trim() == "TRUE").ToList<DataRow>().ForEach(r => { r["NGAY_HIEU_LUC"] = NGAY_HIEU_LUCdateEdit.DateTime; r["LUONG_CO_BAN"] = LUONG_CO_BANtextEdit.EditValue; r["GHI_CHU"] = NOI_DUNGtextEdit.EditValue; });
                dtCN.AcceptChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        private bool Savedata()
        {
            try
            {
                DataTable dt = Commons.Modules.ObjSystems.ConvertDatatable(grvCapNhatLCB);
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sbtLuongCB" + Commons.Modules.iIDUser, dt, "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spCapNhatNhanhML", Commons.Modules.UserName, "sbtLuongCB" + Commons.Modules.iIDUser);
                Commons.Modules.ObjSystems.XoaTable("sbtLuongCB" + Commons.Modules.iIDUser);
                return true;
            }
            catch
            {
                return false;
            }

        }

        #endregion

        private void ID_NLlookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            //LoadGrdCapNhatLCB();
            Commons.Modules.sLoad = "";
        }

        private void LuongTutextEdit_Validated(object sender, EventArgs e)
        {
            if (LuongTutextEdit.EditValue == null || LuongTutextEdit.EditValue.ToString() == "")
            {
                LuongTutextEdit.Focus();
                return;
            }
            if (LuongDentextEdit.EditValue == null || LuongDentextEdit.EditValue.ToString() == "")
            {
                LuongDentextEdit.Focus();
                return;
            }
            cboTo_EditValueChanged(null, null);
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            System.Reflection.PropertyInfo[] Props = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (System.Reflection.PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        private void grvCapNhatLCB_RowCountChanged(object sender, EventArgs e)
        {
            ItemForSumNhanVien.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, ItemForSumNhanVien.Name) + ": " + grvCapNhatLCB.RowCount.ToString();

        }

        private void grvCapNhatLCB_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
        }

        #region chuột phải

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
                string sCotCN = grvCapNhatLCB.FocusedColumn.FieldName;
                var data = grvCapNhatLCB.GetFocusedRowCellValue(sCotCN);
                dt1 = Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdCapNhatLCB, grvCapNhatLCB);
                dt = (DataTable)grdCapNhatLCB.DataSource;

                dt.AsEnumerable().Where(row => dt1.AsEnumerable()
                                                         .Select(r => r.Field<Int64>("ID_CN"))
                                                         .Any(x => x == row.Field<Int64>("ID_CN"))
                                                         ).ToList<DataRow>().ForEach(r => r[sCotCN] = (data));

                dt.AcceptChanges();
            }
            catch (Exception ex)
            {

            }

        }

        private void grvCapNhatLCB_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();
                    if (windowsUIButton.Buttons[0].Properties.Visible) return;
                    DevExpress.Utils.Menu.DXMenuItem itemCapNhatAll = MCreateMenuCapNhatAll(view, irow);
                    e.Menu.Items.Add(itemCapNhatAll);
                }
            }
            catch
            {
            }
        }


        private bool KiemTraLuoi(DataTable dtSource)
        {
            int count = grvCapNhatLCB.RowCount;
            int col = 0;
            int errorCount = 0;
            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                if (Convert.ToBoolean(dr["CHON"]) == true)
                {
                    //Số qđ không được trống
                    if (!KiemDuLieu(grvCapNhatLCB, dr, "SO_QUYET_DINH", true, 250, this.Name))
                    {
                        errorCount++;
                    }

                    if (!KiemTrungDL(dr, "NGAY_HIEU_LUC", Convert.ToDateTime(dr["NGAY_HIEU_LUC"]).ToString("MM/dd/yyyy"), Convert.ToInt64(dr["ID_CN"]), this.Name))
                    {
                        errorCount++;
                    }
                    if (Commons.Modules.iHeSo == 1)
                    {
                        //Không được trống
                        if (!KiemDuLieu(grvCapNhatLCB, dr, "ID_NL", true, 250, this.Name))
                        {
                            errorCount++;
                        }

                        //Không được trống
                        if (!KiemDuLieu(grvCapNhatLCB, dr, "ID_BL", true, 250, this.Name))
                        {
                            errorCount++;
                        }
                    }

                    //Không được trống
                    if (!KiemDuLieu(grvCapNhatLCB, dr, "LUONG_CO_BAN", true, 250, this.Name))
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

        public bool KiemTrungDL(DataRow dr, string sCot, string sNgayHieuLuc, Int64 iID_CN, string sform)
        {
            string sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDL");
            try
            {
                if (string.IsNullOrEmpty(sNgayHieuLuc))
                {
                    dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongDuocTrong"));
                    return false;
                }

                if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.LUONG_CO_BAN WHERE NGAY_HIEU_LUC >= '" + sNgayHieuLuc + "' AND ID_CN = " + iID_CN + "")) > 0)
                {

                    sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgDaCoQuyetDinhLuongCuaNgayNay");
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
        #endregion
    }
}
