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
    public partial class frmNguoiDanhGia : DevExpress.XtraEditors.XtraForm
    {
        // Dữ liệu được chọn
        public Int64 iiD_DV = 0;
        public string sTEN_DV = "";
        public frmNguoiDanhGia()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, btnALL);
            this.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, this.Name);
        }
        private void frmNguoiDanhGia_Load(object sender, EventArgs e)
        {
            lblNguoiDanhGia.Text = sTEN_DV;
            LoadNguoiDanhGia();
            Commons.Modules.ObjSystems.DeleteAddRow(grvNGD);
            enableButon(true);
            searchControl1.Client = grdNDG;
            Commons.Modules.ObjSystems.SetPhanQuyen(btnALL);
        }

        private void LoadNguoiDanhGia()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_DON_VI,ID_LOAI_CONG_VIEC,ID_NGUOI_DGTN,ID_LDG,ACTIVE FROM dbo.NGUOI_DANH_GIA_TAY_NGHE WHERE ID_DON_VI = " + iiD_DV + ""));
                //dt.Columns["GHI_CHU"].ReadOnly = false;
                if (grdNDG.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdNDG, grvNGD, dt, false, false, true, true, true, this.Name);
                    grvNGD.Columns["ID_DON_VI"].Visible = false;
                    Commons.Modules.ObjSystems.AddCombXtra("ID_LCV", "TEN_LCV", "ID_LOAI_CONG_VIEC", grvNGD, Commons.Modules.ObjSystems.DataLoaiCV(false, -1), true, "ID_LCV", this.Name, true);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_CN", "HO_TEN", "ID_NGUOI_DGTN", grvNGD, Commons.Modules.ObjSystems.DataCongNhan(false, 1), true, "ID_CN", this.Name, true);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_LDG", "TEN_LOAI_DANH_GIA", "ID_LDG", grvNGD, Commons.Modules.ObjSystems.DataLoaiDanhGia(false),false, "ID_LDG", this.Name, true);
                }
                else
                {
                    grdNDG.DataSource = dt;
                }
            }
            catch
            {
            }
        }

        private void enableButon(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = !visible;
            btnALL.Buttons[3].Properties.Visible = !visible;
            btnALL.Buttons[4].Properties.Visible = visible;
            btnALL.Buttons[5].Properties.Visible = visible;

            grvNGD.OptionsBehavior.Editable = !visible;
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "sua":
                        {
                            Commons.Modules.ObjSystems.AddnewRow(grvNGD, true);
                            enableButon(false);
                            break;
                        }
                    case "xoa":
                        {
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteViTri"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                                return;
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE FROM dbo.LOAI_CONG_VIEC_XI_NGHIEP WHERE ID_XN = " + iiD_DV + " AND ID_LCV = " + grvNGD.GetFocusedRowCellValue("ID_LCV") + "");
                            LoadNguoiDanhGia();
                            break;
                        }
                    case "luu":
                        {
                            Validate();
                            if (grvNGD.HasColumnErrors) return;
                            grvNGD.CloseEditor();
                            grvNGD.UpdateCurrentRow();
                            if (!SaveData()) return;
                            LoadNguoiDanhGia();
                            Commons.Modules.ObjSystems.DeleteAddRow(grvNGD);
                            enableButon(true);
                            break;
                        }
                    case "khongluu":
                        {
                            LoadNguoiDanhGia();
                            Commons.Modules.ObjSystems.DeleteAddRow(grvNGD);
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
            catch { }
        }

        private bool SaveData()
        {
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTNDG" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grdNDG), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveNguoiDanhGia", iiD_DV, "sBTNDG" + Commons.Modules.iIDUser);
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgThemKhongThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        dt = ((DataTable)grdNDG.DataSource).Copy();
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
            }
        }
        private void grvViTri_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.SetFocusedRowCellValue(view.Columns["ID_DON_VI"], iiD_DV);

            }
            catch
            {
            }
        }

        private void grdViTri_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteViTri"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return;
                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE FROM dbo.LOAI_CONG_VIEC_XI_NGHIEP WHERE ID_XN = " + iiD_DV + " AND ID_LCV = " + grvNGD.GetFocusedRowCellValue("ID_LCV") + "");
                    LoadNguoiDanhGia();
                }
            }
            catch { }
        }
    }
}