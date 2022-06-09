using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using Vs.Report;
using DevExpress.XtraLayout.Utils;
using System.Drawing;

namespace Vs.HRM
{
    public partial class ucDanhGia : DevExpress.XtraEditors.XtraUserControl
    {
        Int64 idcn = 0;
        bool cothem = false;
        public ucDanhGia(Int64 id)
        {
            idcn = id;
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>(){Root}, windowsUIButton);
        }
        #region sự kiện form
        private void ucDanhGia_Load(object sender, EventArgs e)
        {
            NGAY_DGDateEdit.EditValue = DateTime.Today;
            Commons.OSystems.SetDateEditFormat(NGAY_DGDateEdit);

            LoadGrdBangDanhGia(-1);
            LoadGrdBangDanhGiaCT(false);

            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
        }
        private void Readonlyedit(bool lag)
        {
            
            NGAY_DGDateEdit.Properties.ReadOnly = lag;
            NGUOI_DGTextEdit.Properties.ReadOnly = lag;
            NOI_DUNGMemoEdit.Properties.ReadOnly = lag;
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
                        if (Commons.Modules.iCongNhan == -1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        layoutControlItem5.Visibility = LayoutVisibility.OnlyInCustomization;

                        grdDanhGia.Visible = false;
                        LoadGrdBangDanhGiaCT(true);
                        AddnewRow();
                        Bindingdata(true);
                        cothem = true;
                        enableButon(false);
                        break;
                    }
                case "sua":
                    {
                        if (Commons.Modules.iCongNhan == -1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        Readonlyedit(false);
                        AddnewRow();
                        cothem = false;
                        enableButon(false);
                        break;
                    }

                case "xoa":
                    {
                        if (Commons.Modules.iCongNhan == -1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        Readonlyedit(true);
                        XoaBangDanhGia();
                        break;
                    }
                case "In":
                    {
                        try
                        {
                            System.Data.SqlClient.SqlConnection conn;
                            DataTable dt = new DataTable();
                            frmViewReport frm = new frmViewReport();
                            frm.rpt = new rptBCDanhGiaCN();

                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBCDanhGiaCN", conn);
                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = idcn;
                            cmd.CommandType = CommandType.StoredProcedure;

                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            dt = new DataTable();
                            dt = ds.Tables[0].Copy();
                            dt.TableName = "DA_TA";
                            frm.AddDataSource(dt);

                          

                            frm.ShowDialog();
                        }
                        catch
                        { }

                        break;
                    }
                case "luu":
                    {
                        if (!dxValidationProvider2.Validate()) return;
                        if (!Validate()) return;
                        
                        if(grvDanhGiaCT.RowCount == 1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgchuathemnoidungCT"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            grvDanhGiaCT.Focus();
                            return;
                        }
                        layoutControlItem5.Visibility = LayoutVisibility.Always;
                        SaveData();
                        enableButon(true);
                        Bindingdata(false);
                        DeleteAddRow();
                        break;
                    }
                case "khongluu":
                    {
                        Readonlyedit(true);
                        layoutControlItem5.Visibility = LayoutVisibility.Always;
                        LoadGrdBangDanhGiaCT(false);
                        enableButon(true);
                        Bindingdata(false);
                        DeleteAddRow();
                        break;
                    }
                case "thoat":
                    {
                        Readonlyedit(true);
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                case "nddanhgia":
                    {
                        frmNDDanhGia nd = new frmNDDanhGia();
                        if(nd.ShowDialog() == DialogResult.Yes)
                        {
                            LoadGrdBangDanhGiaCT(false);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        private bool dxValidationProvider1()
        {
            throw new NotImplementedException();
        }

        private void grvDanhGia_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Bindingdata(false);
            LoadGrdBangDanhGiaCT(false);
        }
        private void grvDanhGiaCT_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {

            grvDanhGiaCT.ClearColumnErrors();
            DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            if (View == null) return;
            DataTable dt = Commons.Modules.ObjSystems.ConvertDatatable(grvDanhGiaCT);
            int n = dt.AsEnumerable().Count(x => x.Field<Int64>("ID_NDDG") == Convert.ToInt64(View.GetRowCellValue(e.RowHandle, View.Columns["ID_NDDG"])));
            if (n > 1)
            {
                e.Valid = false;
                View.SetColumnError(View.Columns["ID_NDDG"], Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraTenUserNULL", Commons.Modules.TypeLanguage), DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);
                return;
            }
        }

        private void grvDanhGiaCT_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
        }


        private void grdDanhGiaCT_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                XoaBangDanhGiaCT();
            }
        }

        private void grdDanhGia_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                XoaBangDanhGia();
            }
        }

        #endregion

        #region function Load

        private void LoadGrdBangDanhGia(int iID)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListBangDanhGia", idcn));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_DG"] };
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdDanhGia, grvDanhGia, dt, false, false, true, true, true, this.Name);
            grvDanhGia.Columns["ID_DG"].Visible = false;

            if (iID != -1)
            {
                int index = dt.Rows.IndexOf(dt.Rows.Find(iID));
                grvDanhGia.FocusedRowHandle = grvDanhGia.GetRowHandle(index);
            }

        }

        private void LoadGrdBangDanhGiaCT(bool them)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListBangDanhGiaCT", them == false ? grvDanhGia.GetFocusedRowCellValue("ID_DG") : -1, Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdDanhGiaCT, grvDanhGiaCT, dt, false, false, true, true, true, this.Name);
            Commons.Modules.ObjSystems.AddComboAnID("ID_NDDG", "TEN_NDDG", grvDanhGiaCT, Commons.Modules.ObjSystems.DataNoiDungDanhGia(false));
            //for (int i = 0; i < grvDanhGiaCT.Columns.Count; i++)
            //{
            //    grvDanhGiaCT.Columns[i].AppearanceHeader.BackColor = Color.FromArgb(240, 128, 25);
            //}
        }
        #endregion

        #region function dùng chung
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = visible;
            windowsUIButton.Buttons[6].Properties.Visible = visible;
            windowsUIButton.Buttons[7].Properties.Visible = visible;
            windowsUIButton.Buttons[8].Properties.Visible = !visible;
            windowsUIButton.Buttons[9].Properties.Visible = !visible;
            windowsUIButton.Buttons[10].Properties.Visible = visible;
            Readonlyedit(visible);
        }
        private void Bindingdata(bool bthem)
        {
            if (bthem == true)
            {
                try
                {
                    NGAY_DGDateEdit.EditValue = DateTime.Today;
                    NGUOI_DGTextEdit.EditValue = "";
                    NOI_DUNGMemoEdit.EditValue = "";

                }
                catch
                {
                }
            }
            else
            {
                NGAY_DGDateEdit.EditValue = Convert.ToDateTime(grvDanhGia.GetFocusedRowCellValue("NGAY_DANH_GIA"));
                NGUOI_DGTextEdit.EditValue = grvDanhGia.GetFocusedRowCellValue("NGUOI_DANH_GIA");
                NOI_DUNGMemoEdit.EditValue = grvDanhGia.GetFocusedRowCellValue("NOI_DUNG");
            }
        }
        private void SaveData()
        {
            DataTable tb = Commons.Modules.ObjSystems.ConvertDatatable(grvDanhGiaCT);

            if (tb!=null && tb.Rows.Count>0)
            {
                try
                {
                    grvDanhGiaCT.PostEditor();
                    grvDanhGiaCT.UpdateCurrentRow();
                    //tạo bảng tạm chi tiết
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sbtBangDanhGia" + Commons.Modules.UserName,tb, "");
                    LoadGrdBangDanhGia(
                    Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateBangDanhGia",
                        grvDanhGia.GetFocusedRowCellValue("ID_DG"),
                        idcn,
                        NGAY_DGDateEdit.EditValue,
                        NGUOI_DGTextEdit.EditValue,
                        NOI_DUNGMemoEdit.EditValue,
                        cothem,
                        "sbtBangDanhGia" + Commons.Modules.UserName
                        )));
                    LoadGrdBangDanhGiaCT(false);
                }
                catch
                { }
            }
            else
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgPhaiNhapChiTiet"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                grvDanhGiaCT.Focus();
            }
        }
        private void DeleteData()
        {
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDeleteKhoaDaoTao"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeXoa"), MessageBoxButtons.YesNo) == DialogResult.No) return;
            //xóa
            try
            {
                //SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.QUA_TRINH_CONG_TAC WHERE ID_QTCT =" + grvCongTac.GetFocusedRowCellValue("ID_QTCT") + "");
                //grvCongTac.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void AddnewRow()
        {
            grvDanhGiaCT.OptionsBehavior.Editable = true;
            grvDanhGiaCT.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            grvDanhGiaCT.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
        }
        private void DeleteAddRow()
        {
            grvDanhGiaCT.OptionsBehavior.Editable = false;
            grvDanhGiaCT.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            grvDanhGiaCT.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
        }
        private void XoaBangDanhGia()
        {
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDeleteKhoaDaoTao"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeXoa"), MessageBoxButtons.YesNo) == DialogResult.No) return;
            //xóa
            try
            {
                XoaBangDanhGiaCT();
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE FROM dbo.BANG_DANH_GIA WHERE ID_DG = " + grvDanhGia.GetFocusedRowCellValue("ID_DG") + "");
                grvDanhGia.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void XoaBangDanhGiaCT()
        {
            //if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDeleteKhoaDaoTao"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeXoa"), MessageBoxButtons.YesNo) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE FROM dbo.BANG_DANH_GIA_CHI_TIET WHERE ID_DG = " + grvDanhGia.GetFocusedRowCellValue("ID_DG") + " AND ID_NDDG = " + grvDanhGiaCT.GetFocusedRowCellValue("ID_NDDG") + " ");
                grvDanhGiaCT.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
            }
        }

        #endregion



    }
}
