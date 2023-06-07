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
using DevExpress.CodeParser;

namespace VietSoftHRM
{
    public partial class frmNguoiDanhGia : DevExpress.XtraEditors.XtraForm
    {
        // Dữ liệu được chọn
        public Int64 iiD_DV = 0;
        public Int64 iiD_LCV = 0;
        public frmNguoiDanhGia()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, btnALL);
            this.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, this.Name);
        }
        private void frmNguoiDanhGia_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDV, Commons.Modules.ObjSystems.DataDonVi(false), "ID_DV", "TEN_DV", "TEN_DV");
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT B.ID_LCV ID_VTTD,CASE "+ Commons.Modules.TypeLanguage +" WHEN 0 THEN B.TEN_LCV WHEN 1 THEN B.TEN_LCV_A ELSE B.TEN_LCV_H END TEN_VTTD FROM  dbo.LOAI_CONG_VIEC B  WHERE B.ID_CV in (206, 208)  ORDER BY B.TEN_LCV"));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_VTTD, dt, "ID_VTTD", "TEN_VTTD", "TEN_VTTD", true, true);
            cboDV.EditValue = iiD_DV;
            cboID_VTTD.EditValue = iiD_LCV;
            Commons.Modules.sLoad = "";
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
                if (Commons.Modules.sLoad == "0Load") return;
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_NGUOI_DGTN,ID_LDG,ACTIVE FROM dbo.NGUOI_DANH_GIA_TAY_NGHE WHERE ID_DON_VI = "+cboDV.EditValue+" AND ID_LOAI_CONG_VIEC = "+cboID_VTTD.EditValue+""));
                //dt.Columns["GHI_CHU"].ReadOnly = false;
                if (grdNDG.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdNDG, grvNGD, dt, false, false, true, true, true, this.Name);
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
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteNguoiDanhGia"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                                return;
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE  dbo.NGUOI_DANH_GIA_TAY_NGHE WHERE ID_DON_VI = "+ cboDV.EditValue +" AND ID_LOAI_CONG_VIEC = "+cboID_VTTD.EditValue+" AND ID_NGUOI_DGTN = "+ grvNGD.GetFocusedRowCellValue("ID_NGUOI_DGTN") +"");
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
                            DialogResult = DialogResult.OK;
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
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveNguoiDanhGiaLCV", cboDV.EditValue,cboID_VTTD.EditValue, "sBTNDG" + Commons.Modules.iIDUser);
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
        private void grvViTri_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.SetFocusedRowCellValue(view.Columns["ACTIVE"], true);
            }
            catch
            {
            }
        }

        private void grdViTri_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && btnALL.Buttons[0].Properties.Visible == false)
            {
                if (MessageBox.Show("Bạn có chắc xóa dòng dữ liệu này ?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                //GridView view = sender as GridView;
                //view.DeleteRow(view.FocusedRowHandle);
                if (grvNGD.SelectedRowsCount != 0)
                {
                    grvNGD.GridControl.BeginUpdate();
                    List<int> selectedLogItems = new List<int>(grvNGD.GetSelectedRows());
                    for (int i = selectedLogItems.Count - 1; i >= 0; i--)
                    {
                        grvNGD.DeleteRow(selectedLogItems[i]);
                    }
                    grvNGD.GridControl.EndUpdate();
                }
                else if (grvNGD.FocusedRowHandle != GridControl.InvalidRowHandle)
                {
                    grvNGD.DeleteRow(grvNGD.FocusedRowHandle);
                }
            }
        }

        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            LoadNguoiDanhGia();
        }

        private void cboID_VTTD_EditValueChanged(object sender, EventArgs e)
        {
            LoadNguoiDanhGia();
        }

        private void grvNGD_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            //kiểm tra trùng user 
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                if (Commons.Modules.ObjSystems.IsnullorEmpty(View.GetRowCellValue(e.RowHandle, "ID_NGUOI_DGTN")) || View.GetRowCellValue(e.RowHandle, "ID_NGUOI_DGTN").ToString() == "-99")
                {
                    e.Valid = false;
                    View.SetColumnError(View.Columns["ID_NGUOI_DGTN"], Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrongDuLieu")); return;
                }
                if (Commons.Modules.ObjSystems.IsnullorEmpty(View.GetRowCellValue(e.RowHandle, "ID_LDG")) || View.GetRowCellValue(e.RowHandle, "ID_LDG").ToString() == "-99")
                {
                    e.Valid = false;
                    View.SetColumnError(View.Columns["ID_LDG"], Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrongDuLieu")); return;
                }

            }
            catch
            {
            }
        }
    }
}