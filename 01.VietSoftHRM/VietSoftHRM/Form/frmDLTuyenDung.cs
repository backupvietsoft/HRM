using System;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraGrid.Views.Grid;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using DevExpress.XtraGrid;
using System.Drawing;
using DevExpress.XtraLayout;

namespace VietSoftHRM
{
    public partial class frmDLTuyenDung : DevExpress.XtraEditors.XtraForm
    {
        // Dữ liệu được chọn
        public Int64 iiD_XN = 0;
        public frmDLTuyenDung()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this,Root,tabControl, btnALL);
            this.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, this.Name);
        }
        private void frmDLTuyenDung_Load(object sender, EventArgs e)
        {
            string sSql = "SELECT TOP 1 CASE 0 WHEN 0 THEN B.TEN_DV +' - '+  A.TEN_XN  WHEN 1 THEN B.TEN_DV_A +' - '+ A.TEN_XN_A  END AS XN_DV FROM dbo.XI_NGHIEP A INNER JOIN dbo.DON_VI B ON B.ID_DV = A.ID_DV WHERE ID_XN = " + iiD_XN + "";
            try
            {
                lblNhaMayBoPhan.Text = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString();
            }
            catch
            {
                lblNhaMayBoPhan.Text = "";
            }
            LoadViTri();
            LoadNguoiTuyenDung();
            Commons.Modules.ObjSystems.DeleteAddRow(grvViTri);
            Commons.Modules.ObjSystems.DeleteAddRow(grvThamGiaTD);
            enableButon(true);
            tabControl.SelectedTabPageIndex = 0;
            searchControl1.Client = grdViTri;
        }

        private void LoadViTri()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_LCV,ID_XN,GHI_CHU FROM dbo.LOAI_CONG_VIEC_XI_NGHIEP WHERE ID_XN = " + iiD_XN + ""));

                if (grdViTri.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdViTri, grvViTri, dt, false, false, true, true, true, this.Name);
                    grvViTri.Columns["ID_XN"].Visible = false;
                    Commons.Modules.ObjSystems.AddCombXtra("ID_LCV", "TEN_LCV", grvViTri, Commons.Modules.ObjSystems.DataLoaiCV(false,-1), true, "ID_LCV", this.Name, true);
                }
                else
                {
                    grdViTri.DataSource = dt;
                }
            }
            catch
            {
            }
        }
        private void LoadNguoiTuyenDung()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_CN,HO + ' '+TEN AS HO_TEN,YC_TD,PV_TD,GHI_CHU_1 as GHI_CHU FROM dbo.CONG_NHAN WHERE ID_TO IN (SELECT ID_TO FROM dbo.[TO] WHERE ID_XN = " + iiD_XN + ") AND (YC_TD = 1 OR PV_TD = 1) ORDER BY HO_TEN"));
                dt.Columns["ID_CN"].ReadOnly = false;
                dt.Columns["HO_TEN"].ReadOnly = false;
                if (grdThamGiaTD.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdThamGiaTD, grvThamGiaTD, dt, false, false, true, true, true, this.Name);
                    DataTable CongNhan = new DataTable();
                    CongNhan.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_CN,MS_CN,HO + ' '+TEN AS HO_TEN FROM dbo.CONG_NHAN WHERE ID_TO IN (SELECT ID_TO FROM dbo.[TO] WHERE ID_XN = " + iiD_XN + ")  ORDER BY HO_TEN"));

                    Commons.Modules.ObjSystems.AddCombXtra("ID_CN", "MS_CN", grvThamGiaTD, CongNhan, true, "ID_CN", this.Name, true);
                }
                else
                {
                    grdThamGiaTD.DataSource = dt;
                }
            }
            catch
            {

            }
        }
        private void enableButon(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = !visible;
            btnALL.Buttons[2].Properties.Visible = !visible;
            btnALL.Buttons[3].Properties.Visible = visible;

            grvViTri.OptionsBehavior.Editable = !visible;
            grvThamGiaTD.OptionsBehavior.Editable = !visible;
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "sua":
                    {
                        Commons.Modules.ObjSystems.AddnewRow(grvViTri, true);
                        Commons.Modules.ObjSystems.AddnewRow(grvThamGiaTD, true);
                        enableButon(false);
                        break;
                    }
                case "luu":
                    {
                        Validate();
                        if (grvViTri.HasColumnErrors) return;
                        if (grvThamGiaTD.HasColumnErrors) return;
                        if (!SaveData()) return;
                        LoadViTri();
                        LoadNguoiTuyenDung();
                        Commons.Modules.ObjSystems.DeleteAddRow(grvViTri);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvThamGiaTD);
                        enableButon(true);
                        break;
                    }
                case "khongluu":
                    {
                        LoadViTri();
                        LoadNguoiTuyenDung();
                        Commons.Modules.ObjSystems.DeleteAddRow(grvViTri);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvThamGiaTD);
                        enableButon(true);
                        break;
                    }
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
                default:
                    break;
            }
        }

        private bool SaveData()
        {
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTLCV" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grdViTri), "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBCN" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grdThamGiaTD), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveDLTD", iiD_XN, "sBTLCV" + Commons.Modules.iIDUser, "sBCN" + Commons.Modules.iIDUser);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void grvViTri_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;

        }

        private void grvViTri_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;

        }

        private void grvViTri_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                GridView view = sender as GridView;
                DevExpress.XtraGrid.Columns.GridColumn clMaMay = view.Columns["ID_LCV"];
                if (view == null) return;
                if (view.FocusedColumn.Name == "colID_LCV")
                {//kiểm tra máy không được để trống
                    if (string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        e.Valid = false;
                        e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erLCVKhongTrong");
                        view.SetColumnError(view.Columns["ID_LCV"], e.ErrorText);
                        return;
                    }
                    else
                    {
                        dt = new DataTable();
                        dt = (DataTable)grdViTri.DataSource;
                        if (dt.AsEnumerable().Count(x => x.Field<Int64>("ID_LCV").Equals(e.Value)) > 0)
                        {
                            e.Valid = false;
                            e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrungDuLieu");
                            view.SetColumnError(view.Columns["ID_LCV"], e.ErrorText);
                            return;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void grvThamGiaTD_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;

        }

        private void grvThamGiaTD_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;

        }

        private void grvViTri_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.SetFocusedRowCellValue(view.Columns["ID_XN"], iiD_XN);

            }
            catch
            {
            }
        }

        private void grvThamGiaTD_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                GridView view = sender as GridView;
                DevExpress.XtraGrid.Columns.GridColumn clMaMay = view.Columns["ID_CN"];
                if (view == null) return;
                if (view.FocusedColumn.Name == "colID_CN")
                {//kiểm tra máy không được để trống
                    if (string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        e.Valid = false;
                        e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erCNKhongTrong");
                        view.SetColumnError(view.Columns["ID_CN"], e.ErrorText);
                        return;
                    }
                    else
                    {
                        dt = new DataTable();
                        dt = (DataTable)grdThamGiaTD.DataSource;
                        if (dt.AsEnumerable().Count(x => x.Field<Int64>("ID_CN").Equals(e.Value)) > 0)
                        {
                            e.Valid = false;
                            e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrungDuLieu");
                            view.SetColumnError(view.Columns["ID_CN"], e.ErrorText);
                            return;

                        }
                        else
                        {
                            grvThamGiaTD.SetFocusedRowCellValue("ID_CN", e.Value);
                            grvThamGiaTD.SetFocusedRowCellValue("HO_TEN", SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT HO + ' '+TEN AS HO_TEN FROM dbo.CONG_NHAN WHERE ID_CN =  " + e.Value + "").ToString());

                        }
                    }
                }
            }
            catch
            {
            }

        }

        private void tabControl_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            searchControl1.ResetText();
            if (tabControl.SelectedTabPageIndex == 0)
            {
                searchControl1.Client = grdViTri;
            }
            else
            {
                searchControl1.Client = grdThamGiaTD;
            }
        }



        private void grdViTri_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && btnALL.Buttons[0].Properties.Visible == false)
            {
                if (MessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteYeuDuLieu"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                if (grvViTri.SelectedRowsCount != 0)
                {
                    grvViTri.GridControl.BeginUpdate();
                    List<int> selectedLogItems = new List<int>(grvViTri.GetSelectedRows());
                    for (int i = selectedLogItems.Count - 1; i >= 0; i--)
                    {
                        grvViTri.DeleteRow(selectedLogItems[i]);
                    }
                    grvViTri.GridControl.EndUpdate();
                }
                else if (grvViTri.FocusedRowHandle != GridControl.InvalidRowHandle)
                {
                    grvViTri.DeleteRow(grvViTri.FocusedRowHandle);
                }
            }

        }
    }
}