using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Vs.HRM
{
    public partial class frmNDDanhGia : DevExpress.XtraEditors.XtraForm
    {
        Int64 ID_NDDG = -1;
        bool cothem = false;
        public frmNDDanhGia()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, windowsUIButton);
        }

        #region sự kiện form
        private void frmNDDanhGia_Load(object sender, EventArgs e)
        {
            LoadgrdNDDanhGia("-1");
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);

        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            if (btn == null || btn.Tag == null) return;
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        Bindingdata(true);
                        cothem = true;
                        enableButon(false);
                        break;
                    }
                case "sua":
                    {
                        if (grvNDDanhGia.RowCount == 0) return;
                        cothem = false;
                        enableButon(false);
                        break;
                    }

                case "xoa":
                    {
                        if (grvNDDanhGia.RowCount == 0) return;

                        int kt=Convert.ToInt32( SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "select count(*) from BANG_DANH_GIA_CHI_TIET where ID_NDDG = " + grvNDDanhGia.GetFocusedRowCellValue("ID_NDDG") + ""));

                        if (kt != 0)
                        {
                            XtraMessageBox.Show( Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDulieudangsudungDNDG"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        DeleteData();
                        break;
                    }
                case "luu":
                    {
                        if (!dxValidationProvider1.Validate()) return;
                        if (SaveData() == false) return;
                        enableButon(true);
                        break;
                    }
                case "khongluu":
                    {
                        enableButon(true);
                        Bindingdata(false);
                        dxValidationProvider1.Validate();
                        break;
                    }
                case "thoat":
                    {
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgBanCoMuonThoatChuongtrinh"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeThoat"), MessageBoxButtons.YesNo) == DialogResult.No) return;
                        this.Close();
                        break;
                    }
                default:
                    break;
            }
        }

        private void grvNDDanhGia_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Bindingdata(false);
        }

        private void grdNDDanhGia_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                DeleteData();
            }
        }
        #endregion

        #region hàm load form
        //hàm load gridview
        private void LoadgrdNDDanhGia(string id)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListNoiDungDanhGia",Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_NDDG"] };
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdNDDanhGia, grvNDDanhGia, dt, false, true, true, true, true, this.Name);
            grvNDDanhGia.Columns["TEN_NDDG"].AppearanceHeader.ForeColor = Color.Red;
            grvNDDanhGia.Columns["ID_NDDG"].Visible = false;
            grvNDDanhGia.Columns["TEN_NDDG_V"].Visible = false;
            //grvNDDanhGia.Columns["TEN_NDDG_A"].Visible = false;
            //grvNDDanhGia.Columns["TEN_NDDG_H"].Visible = false;
            if (id != "-1")
            {
                int index = dt.Rows.IndexOf(dt.Rows.Find(id));
                grvNDDanhGia.FocusedRowHandle = grvNDDanhGia.GetRowHandle(index);
            }
            if (grvNDDanhGia.RowCount == 1)
            {
                Bindingdata(false);
            }
        }
        //hàm bingding dữ liệu
        private void Bindingdata(bool bthem)
        {
            if (bthem == true)
            {
                TEN_NDDGTextEdit.EditValue = "";
                TEN_NDDG_ATextEdit.EditValue = "";
                TEN_NDDG_HTextEdit.EditValue = "";
                TEN_NDDGTextEdit.Focus();
            }
            else
            {
                TEN_NDDGTextEdit.EditValue = grvNDDanhGia.GetFocusedRowCellValue("TEN_NDDG_V");
                TEN_NDDG_ATextEdit.EditValue = grvNDDanhGia.GetFocusedRowCellValue("TEN_NDDG_A");
                TEN_NDDG_HTextEdit.EditValue = grvNDDanhGia.GetFocusedRowCellValue("TEN_NDDG_H");
            }
        }
        //hàm tắc mở control
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
            windowsUIButton.Buttons[6].Properties.Visible = visible;
            grdNDDanhGia.Enabled = visible;
            //ID_QHLookUpEdit.Properties.ReadOnly = visible;
            TEN_NDDGTextEdit.Properties.ReadOnly = visible;
            TEN_NDDG_ATextEdit.Properties.ReadOnly = visible;
            TEN_NDDG_HTextEdit.Properties.ReadOnly = visible;
        }
        #endregion

        #region hàm sử lý data
        //hàm sử lý khi lưu dữ liệu(thêm/Sửa)
        private bool SaveData()
        {
            try
            {
                string idnd = "";
                try
                {
                    idnd = grvNDDanhGia.GetFocusedRowCellValue("ID_NDDG").ToString();
                }
                catch (Exception)
                {
                    idnd = "";
                }

                string n = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateNoiDungDanhGia",
                    idnd,
                    TEN_NDDGTextEdit.EditValue,
                    TEN_NDDG_ATextEdit.EditValue,
                    TEN_NDDG_HTextEdit.EditValue,
                    cothem
                ).ToString();
                LoadgrdNDDanhGia(n);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //hàm xử lý khi xóa dữ liệu
        private void DeleteData()
        {
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDeleteNoiDungDanhGia"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeXoa"), MessageBoxButtons.YesNo) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.NOI_DUNG_DANH_GIA WHERE ID_NDDG = " + grvNDDanhGia.GetFocusedRowCellValue("ID_NDDG") + "");
                grvNDDanhGia.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
            }
        }



        #endregion

        private void frmNDDanhGia_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }
    }
}