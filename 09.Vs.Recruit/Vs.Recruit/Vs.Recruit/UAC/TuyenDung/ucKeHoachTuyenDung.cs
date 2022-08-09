using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Vs.Recruit
{
    public partial class ucKeHoachTuyenDung : DevExpress.XtraEditors.XtraUserControl
    {
        public ucKeHoachTuyenDung()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<DevExpress.XtraLayout.LayoutControlGroup> { Root }, btnALL);
        }

        private void ucKeHoachTuyenDung_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            datThang.EditValue = DateTime.Now;
            Commons.Modules.sLoad = "";
            enableButon(true);
            LoadgrdVTYC();
            Commons.Modules.ObjSystems.SetPhanQuyen(btnALL);
        }

        private DataTable TinhSoTuanCuaTHang(DateTime TN, DateTime DN)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("Tuan", typeof(Int32));
                dt.Columns.Add("TNgay", typeof(DateTime));
                dt.Columns.Add("DNgay", typeof(DateTime));

                //kiểm tra ngày bắc đầu có phải thứ 2 không

                for (int i = 1; i <= 4; i++)
                {
                    if (i == 1)
                    {
                        if (TN.DayOfWeek == DayOfWeek.Monday)
                        {
                            dt.Rows.Add(i, TN, TN.AddDays(7));
                            TN = TN.AddDays(8);
                            continue;
                        }
                        else
                        {
                            dt.Rows.Add(i, TN, TN.AddDays(7 + (7 - (int)TN.DayOfWeek)));
                            TN = TN.AddDays(8 + (7 - (int)TN.DayOfWeek));
                            continue;
                        }
                    }
                    if (i == 2 || i == 3)
                    {
                        dt.Rows.Add(i, TN, TN.AddDays(6));
                        TN = TN.AddDays(7);
                        continue;
                    }
                    if (i == 4)
                    {
                        dt.Rows.Add(i, TN, DN);
                        break;
                    }
                }

                return dt;
            }
            catch
            {
                return null;
            }
        }

        private bool SaveData()
        {
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTKHT" + Commons.Modules.UserName, Commons.Modules.ObjSystems.ConvertDatatable(grdTuan), "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTNT" + Commons.Modules.UserName, Commons.Modules.ObjSystems.ConvertDatatable(grdNguonTuyen), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveKeHoachTuyenDung", datThang.DateTime, "sBTKHT" + Commons.Modules.UserName, "sBTNT" + Commons.Modules.UserName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void enableButon(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = !visible;
            btnALL.Buttons[3].Properties.Visible = !visible;
            btnALL.Buttons[4].Properties.Visible = visible;

            grvNguonTuyen.OptionsBehavior.Editable = !visible;
            grvTuan.OptionsBehavior.Editable = !visible;
        }

        private void LoadgrdVTYC()
        {
            DateTime TN = datThang.DateTime.Date.AddDays(-datThang.DateTime.Date.Day + 1);
            DateTime DN = TN.AddMonths(1).AddDays(-1);
            try
            {
                Commons.Modules.sLoad = "0Load";
                //tạo bảng tạm tuần trong tháng
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTTuan" + Commons.Modules.UserName, TinhSoTuanCuaTHang(TN, DN), "");
                DataSet set = new DataSet();
                set = SqlHelper.ExecuteDataset(Commons.IConnections.CNStr, "spGetListKeHoachTuyenDung", TN, DN, Commons.Modules.UserName, Commons.Modules.TypeLanguage, "sBTTuan" + Commons.Modules.UserName);
                DataTable dt = set.Tables[0];
                if (grdVTYC.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdVTYC, grvVTYC, dt, false, false, false, true, true, this.Name);
                    grvVTYC.Columns["ID_YCTD"].Visible = false;
                    grvVTYC.Columns["ID_VTTD"].Visible = false;
                }
                else
                {
                    grdVTYC.DataSource = dt;
                }
                //Load Tuần
                if (grdTuan.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdTuan, grvTuan, set.Tables[1], false, false, true, true, true, this.Name);
                    grvTuan.Columns["ID_YCTD"].Visible = false;
                    grvTuan.Columns["ID_VTTD"].Visible = false;
                    grvTuan.Columns["THANG"].Visible = false;
                    grvTuan.Columns["TUAN"].Visible = false;
                }
                else
                {
                    grdTuan.DataSource = set.Tables[1];
                }
                Commons.Modules.sLoad = "";
                grvVTYC_FocusedRowChanged(null, null);


                if (grdNguonTuyen.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdNguonTuyen, grvNguonTuyen, set.Tables[2], false, false, true, true, true, this.Name);
                    grvNguonTuyen.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                    grvNguonTuyen.Columns["ID_YCTD"].Visible = false;
                    grvNguonTuyen.Columns["ID_VTTD"].Visible = false;
                    Commons.Modules.ObjSystems.AddCombXtra("ID_NTD", "TEN_NTD", grvNguonTuyen, Commons.Modules.ObjSystems.DataNguonTD(false), false, "ID_NTD", this.Name, true);
                }
                else
                {
                    grdTuan.DataSource = set.Tables[1];
                }
            }
            catch
            {
            }
        }

        private void grvVTYC_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {

                if (Commons.Modules.sLoad == "0Load") return;
                Commons.Modules.ObjSystems.RowFilter(grdTuan, grvTuan.Columns["ID_YCTD"], grvTuan.Columns["ID_VTTD"], grvVTYC.GetFocusedRowCellValue("ID_YCTD").ToString(), grvVTYC.GetFocusedRowCellValue("ID_VTTD").ToString());

                Commons.Modules.ObjSystems.RowFilter(grdNguonTuyen, grvNguonTuyen.Columns["ID_YCTD"], grvNguonTuyen.Columns["ID_VTTD"], grvVTYC.GetFocusedRowCellValue("ID_YCTD").ToString(), grvVTYC.GetFocusedRowCellValue("ID_VTTD").ToString());
            }
            catch
            {
            }
        }

        private void datThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadgrdVTYC();
        }
        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            switch (btn.Tag.ToString())
            {

                case "sua":
                    {
                        Commons.Modules.ObjSystems.AddnewRow(grvTuan, false);
                        Commons.Modules.ObjSystems.AddnewRow(grvNguonTuyen, true);
                        enableButon(false);
                        break;
                    }
                case "xoa":
                    {

                        break;
                    }

                case "luu":
                    {
                        if (grvNguonTuyen.HasColumnErrors) return;
                        int n = grvVTYC.FocusedRowHandle;
                        if (!SaveData()) return;
                        LoadgrdVTYC();
                        grvVTYC.FocusedRowHandle = n;
                        grvVTYC.SelectRow(n);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvTuan);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvNguonTuyen);
                        enableButon(true);
                        break;
                    }
                case "khongluu":
                    {
                        LoadgrdVTYC();
                        enableButon(true);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvTuan);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvNguonTuyen);
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

        private void grvNguonTuyen_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                if (grvVTYC.RowCount == 0)
                {
                    grvNguonTuyen.DeleteSelectedRows();
                    return;
                }
                grvNguonTuyen.SetFocusedRowCellValue("ID_YCTD", grvVTYC.GetFocusedRowCellValue("ID_YCTD"));
                grvNguonTuyen.SetFocusedRowCellValue("ID_VTTD", grvVTYC.GetFocusedRowCellValue("ID_VTTD"));
            }
            catch
            {
            }
        }

        private void searchControl1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            grvVTYC_FocusedRowChanged(null, null);
        }

        private void grvNguonTuyen_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            grvNguonTuyen.ClearColumnErrors();
            try
            {
                DataTable dt = new DataTable();
                if (grvNguonTuyen == null) return;
                if (grvNguonTuyen.FocusedColumn.FieldName == "ID_NTD")
                {//kiểm tra máy không được để trống
                    if (string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        e.Valid = false;
                        e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erMayKhongTrong");
                        grvNguonTuyen.SetColumnError(grvNguonTuyen.Columns["ID_NTD"], e.ErrorText);
                        return;
                    }
                    else
                    {
                        dt = new DataTable();
                        dt = Commons.Modules.ObjSystems.ConvertDatatable(grvNguonTuyen);
                        if (dt.AsEnumerable().Count(x => x.Field<Int64>("ID_NTD").Equals(e.Value)) > 0)
                        {
                            e.Valid = false;
                            e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrungDuLieu");
                            grvNguonTuyen.SetColumnError(grvNguonTuyen.Columns["ID_NTD"], e.ErrorText);
                            return;
                        }
                    }
                }
            }
            catch
            { }
        }

        private void grdNguonTuyen_ProcessGridKey(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (btnALL.Buttons[0].Properties.Visible == false && e.KeyData == System.Windows.Forms.Keys.Delete)
            {
                grvNguonTuyen.DeleteSelectedRows();
            }
        }
    }
}
