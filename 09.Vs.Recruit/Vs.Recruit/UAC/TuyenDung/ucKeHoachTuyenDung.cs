using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
namespace Vs.Recruit
{
    public partial class ucKeHoachTuyenDung : DevExpress.XtraEditors.XtraUserControl
    {
        int tuanHT = 0;
        int SLDATA = 0;
        public ucKeHoachTuyenDung()
        {
            Commons.Modules.sLoad = "0Load";
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<DevExpress.XtraLayout.LayoutControlGroup> { Root }, btnALL);
        }
        private void ucKeHoachTuyenDung_Load(object sender, EventArgs e)
        {
            datNam.EditValue = DateTime.Now;
            LoadCbo();
            enableButon(true);
            Commons.Modules.sLoad = "";
            LoadgrdVTYC();
            LoadgrdKHTuan();
            grvVTYC_FocusedRowChanged(null, null);
            Commons.Modules.ObjSystems.SetPhanQuyen(btnALL);
            foreach (ToolStripMenuItem item in contextMenuStrip1.Items)
            {
                item.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, item.Name);
            }
        }
        private void LoadCbo()
        {
            try
            {
                //ID_TT_VT, Ten_TT_VT
                Commons.Modules.ObjSystems.MLoadCheckedComboBoxEdit(cboTinhTrang, Commons.Modules.ObjSystems.DataTinhTrangCVYC(false), "ID_TT_VT", "TEN_TT_VT", this.Name, true);
                cboTinhTrang.SetEditValue(3);
                DataTable tb = new DataTable();
                tb = SqlHelper.ExecuteDataset(Commons.IConnections.CNStr, "GetTUAN_TRONG_NAM", DateTime.Now.Year).Tables[0];
                int Maxtuan = tb.AsEnumerable().Max(x => Convert.ToInt32(x["TUAN"]));
                CultureInfo ciCurr = CultureInfo.CurrentCulture;
                int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now.Date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                tuanHT = weekNum;

                //nếu tuần hiện tại nhỏ hơn 4 thì từ tuần lùi về một năm
                if (weekNum < 4)
                {
                    datNamTuan.EditValue = DateTime.Now.AddYears(-1);
                    DataTable tb1 = new DataTable();
                    tb1 = SqlHelper.ExecuteDataset(Commons.IConnections.CNStr, "GetTUAN_TRONG_NAM", datNamTuan.DateTime.Year).Tables[0];
                    //lấy tuần max trong năm
                    Maxtuan = tb1.AsEnumerable().Max(x => Convert.ToInt32(x["TUAN"]));
                    spinTuTuan.EditValue = Maxtuan - (5 - weekNum);
                    spinDenTuan.EditValue = weekNum + 4;
                }
                else
                {
                    if (weekNum > Maxtuan - 4)
                    {

                        datNamTuan.EditValue = DateTime.Now;
                        spinTuTuan.EditValue = weekNum - 4;
                        spinDenTuan.EditValue = (weekNum + 4) - Maxtuan;

                    }
                    else
                    {
                        datNamTuan.EditValue = DateTime.Now;
                        spinTuTuan.EditValue = weekNum - 4;
                        spinDenTuan.EditValue = weekNum + 4;
                    }
                }

            }
            catch
            {
            }
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
                //kiểm tra số lượn kế hoạch hoạch không lớn hơn số lượng truyển
                //lấy số lượng hiện có trên lưới
                int SLTL = 0;
                foreach (DevExpress.XtraGrid.Columns.GridColumn col in grvTuan.Columns)
                {
                    try
                    {
                        if (Convert.ToInt32(grvTuan.GetRowCellValue(0, col)) > 0)
                        {
                            SLTL += Convert.ToInt32(grvTuan.GetRowCellValue(0, col));
                        }
                    }
                    catch { }
                }
                //kiểm tra số lượn 
                if (SLTL + SLDATA > Convert.ToInt32(grvVTYC.GetFocusedRowCellValue("SL_TUYEN")))
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgSoLuongPhanBoKhongLonHonSLKH"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                Int64 iID_YCTD = Convert.ToInt64(grvVTYC.GetFocusedRowCellValue("ID_YCTD"));
                Int64 iID_VTTD = Convert.ToInt64(grvVTYC.GetFocusedRowCellValue("ID_VTTD"));
                foreach (DevExpress.XtraGrid.Columns.GridColumn col in grvTuan.Columns)
                {
                    try
                    {
                        int nam = Convert.ToInt32(col.FieldName.Substring(0, 4));
                        int tuan = Convert.ToInt32(col.FieldName.Substring(5, 2));
                        DateTime TN = Convert.ToDateTime(col.FieldName.Substring(9, 5) + "/" + nam);
                        DateTime DN = Convert.ToDateTime(col.FieldName.Substring(15, 5) + "/" + nam);
                        SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spUpdateKeHoachTuyenDungTuan", iID_YCTD, iID_VTTD, nam, tuan, TN, DN, Convert.ToInt32(grvTuan.GetRowCellValue(0, col)));
                    }
                    catch { }
                }
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
            btnALL.Buttons[2].Properties.Visible = visible;
            btnALL.Buttons[3].Properties.Visible = !visible;
            btnALL.Buttons[4].Properties.Visible = !visible;
            btnALL.Buttons[5].Properties.Visible = visible;
            grdVTYC.Enabled = visible;
            grvTuan.OptionsBehavior.Editable = !visible;
            searchControl1.Properties.ReadOnly = !visible;
            datNamTuan.Properties.ReadOnly = !visible;
            spinTuTuan.Properties.ReadOnly = !visible;
            spinDenTuan.Properties.ReadOnly = !visible;
            datNam.Properties.ReadOnly = !visible;
            cboTinhTrang.Properties.ReadOnly = !visible;
        }
        private void LoadgrdVTYC()
        {
            Int64 ID_YCTD = -1, ID_VTTD = -1;

            try
            {
                ID_YCTD = Convert.ToInt64(grvVTYC.GetFocusedRowCellValue("ID_YCTD"));
                ID_VTTD = Convert.ToInt64(grvVTYC.GetFocusedRowCellValue("ID_VTTD"));
            }
            catch (Exception)
            {
                ID_YCTD = -1;
                ID_VTTD = -1;
            }

            if (Commons.Modules.sLoad == "0Load") return;
            string a = cboTinhTrang.EditValue.ToString();
            DateTime TN = Convert.ToDateTime("01/01/" + datNam.DateTime.Year);
            DateTime DN = Convert.ToDateTime("31/12/" + datNam.DateTime.Year);
            try
            {
                DataTable dt = new DataTable();
                string[] arrMS_MAY = cboTinhTrang.EditValue.ToString().Split(',');
                DataTable dt_TT = new DataTable();
                try
                {
                    dt_TT.Columns.Add("ID_TT");
                    foreach (string MS_MAY in arrMS_MAY)
                    {
                        dt_TT.Rows.Add(MS_MAY.Trim());
                    }
                }
                catch { }

                string sBTTT = "sBTTT" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTTT, dt_TT, "");
                dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListKeHoachTuyenDung", TN, DN, Commons.Modules.UserName, Commons.Modules.TypeLanguage, sBTTT));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_YCTD"], dt.Columns["ID_VTTD"] };
                if (grdVTYC.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdVTYC, grvVTYC, dt, false, true, false, true, true, this.Name);
                    grvVTYC.ClearSorting();
                    grvVTYC.Columns["ID_YCTD"].Visible = false;
                    grvVTYC.Columns["ID_VTTD"].Visible = false;
                    grvVTYC.Columns["ID_TT"].Visible = false;
                }
                else
                {
                    grdVTYC.DataSource = dt;
                }

                if (ID_YCTD > 1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(new object[] { ID_YCTD, ID_VTTD }));
                    grvVTYC.ClearSelection();
                    grvVTYC.FocusedRowHandle = index;
                    grvVTYC.SelectRow(index);
                }
                else
                {
                    //grvVTYC.FocusedRowHandle = 0;
                }
            }
            catch
            {
            }
        }

        private void LoadgrdKHTuan()
        {
            if (Commons.Modules.sLoad == "0Load") return;
            try
            {
                DataTable tuan = new DataTable();
                DataTable namdau = new DataTable();

                //nếu den tuan > tu tuan
                if (Convert.ToInt32(spinTuTuan.EditValue) <= Convert.ToInt32(spinDenTuan.EditValue))
                {
                    //trong cùng một năm
                    namdau.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM [dbo].[fnTUAN_TRONG_NAM](" + datNamTuan.DateTime.Year + ")"));
                    tuan = namdau.AsEnumerable().Where(x => Convert.ToInt32(x["TUAN"]) >= Convert.ToInt32(spinTuTuan.EditValue) && Convert.ToInt32(x["TUAN"]) <= Convert.ToInt32(spinDenTuan.EditValue)).CopyToDataTable();
                }
                else
                {
                    //năm sau lớn hơn năm đầu
                    tuan.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM [dbo].[fnTUAN_TRONG_NAM](" + datNamTuan.DateTime.Year + ")"));
                    tuan = tuan.AsEnumerable().Where(x => Convert.ToInt32(x["TUAN"]) >= Convert.ToInt32(spinTuTuan.EditValue)).CopyToDataTable();

                    namdau.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM [dbo].[fnTUAN_TRONG_NAM](" + datNamTuan.DateTime.AddYears(1).Year + ")"));
                    namdau = namdau.AsEnumerable().Where(x => Convert.ToInt32(x["TUAN"]) <= Convert.ToInt32(spinDenTuan.EditValue)).CopyToDataTable();

                    tuan.Merge(namdau);
                }

                //tính SL trong DB.


                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTTuanNam" + Commons.Modules.iIDUser, tuan, "");
                DataSet set = new DataSet();
                DataTable dt = new DataTable();
                set = SqlHelper.ExecuteDataset(Commons.IConnections.CNStr, "spGetKeHoachTuyenDungTuan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, grvVTYC.GetFocusedRowCellValue("ID_YCTD"), grvVTYC.GetFocusedRowCellValue("ID_VTTD"), "sBTTuanNam" + Commons.Modules.iIDUser);
                dt = set.Tables[0];

                foreach (DataColumn item in dt.Columns)
                {
                    item.ReadOnly = false;
                }
                Commons.Modules.ObjSystems.MLoadXtraGridIP(grdTuan, grvTuan, dt, false, true, false, true);
                foreach (DevExpress.XtraGrid.Columns.GridColumn col in grvTuan.Columns)
                {
                    col.Caption = col.FieldName.Substring(5, col.FieldName.Length - 5);
                    if (Convert.ToInt32(col.FieldName.Substring(5, 2)) < tuanHT)
                    {
                        col.OptionsColumn.AllowEdit = false;
                        col.AppearanceHeader.BackColor = Color.Transparent;
                    }
                }
                try
                {
                    SLDATA = 0;
                    SLDATA = Convert.ToInt32(set.Tables[1].Rows[0][0]);
                }
                catch
                {
                    SLDATA = 0;
                }
            }
            catch
            {
            }
        }

        private void grvVTYC_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            try
            {
                if (Convert.ToInt32(grvVTYC.GetFocusedRowCellValue("ID_TT_VT")) != 3)
                {
                    btnALL.Buttons[0].Properties.Visible = false;
                }
                else
                {
                    btnALL.Buttons[0].Properties.Visible = true;

                }
            }
            catch
            {
                btnALL.Buttons[0].Properties.Visible = true;
            }
            LoadgrdKHTuan();
        }

        private void datNam_EditValueChanged(object sender, EventArgs e)
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
                        try
                        {
                            if (Convert.ToInt32(grvVTYC.GetFocusedRowCellValue("ID_TT_VT")) != 3)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChiSuaPhieuYeuCauDaDuyet"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        catch (Exception)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChiSuaPhieuYeuCauDaDuyet"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        grvTuan.OptionsBehavior.Editable = true;
                        foreach (DevExpress.XtraGrid.Columns.GridColumn col in grvTuan.Columns)
                        {
                            if (Convert.ToInt32(col.FieldName.Substring(5, 2)) < tuanHT)
                            {
                                col.OptionsColumn.AllowEdit = false;
                                col.AppearanceHeader.BackColor = Color.PaleVioletRed;
                            }
                        }
                        enableButon(false);
                        break;
                    }
                case "xoa":
                    {
                        XoaKeHoach();
                        break;
                    }
                case "in":
                    {
                        frmInKeHoachTD frm = new frmInKeHoachTD();
                        frm.ShowDialog();
                        break;
                    }

                case "luu":
                    {
                        //if (Convert.ToInt32(grvVTYC.GetFocusedRowCellValue("ID_TT")) != 1)
                        //{
                        //    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuDaPhatSinhKhongSua"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    return;
                        //}
                        Validate();
                        if (grvTuan.HasColumnErrors) return;
                        int n = grvVTYC.FocusedRowHandle;
                        if (!SaveData()) return;
                        LoadgrdVTYC();
                        LoadgrdKHTuan();
                        //grvVTYC.FocusedRowHandle = n;
                        //grvVTYC.SelectRow(n);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvTuan);
                        enableButon(true);
                        break;
                    }
                case "khongluu":
                    {
                        LoadgrdVTYC();
                        LoadgrdKHTuan();
                        enableButon(true);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvTuan);
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

        private void XoaKeHoach()
        {
            if (Convert.ToInt32(grvVTYC.GetFocusedRowCellValue("ID_TT")) != 1)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuDaPhatSinhKhongXoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteYeuCauTuyenDung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.KHTD_TUAN WHERE ID_YCTD = " + grvVTYC.GetFocusedRowCellValue("ID_YCTD") + " AND ID_VTTD = " + grvVTYC.GetFocusedRowCellValue("ID_VTTD") + "");
                LoadgrdVTYC();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void grdVTYC_ProcessGridKey(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                XoaKeHoach();
            }
        }

        private void grvTuan_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvTuan_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void cboTinhTrang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadgrdVTYC();
        }

        private void spinDenTuan_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadgrdKHTuan();
        }

        private void grvVTYC_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.HitInfo.InDataRow)
                {
                    contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                }
                else
                {
                    contextMenuStrip1.Hide();
                }
            }
            catch
            {
            }
        }
        private void mnuCapNhapMucUuTienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList rows = new ArrayList();
                frmUpdateMucUuTien frm = new frmUpdateMucUuTien();
                Int32[] selectedRowHandles = grvVTYC.GetSelectedRows();
                for (int i = 0; i < selectedRowHandles.Length; i++)
                {
                    int selectedRowHandle = selectedRowHandles[i];
                    if (selectedRowHandle >= 0)
                        rows.Add(grvVTYC.GetDataRow(selectedRowHandle));
                }
                frm.listChon = rows;
                frm.ShowDialog();
                LoadgrdVTYC();

            }
            catch
            {
            }
        }
        private void mnuHuyTuyenDungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoMuonHuyTuyenDung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                Int32[] selectedRowHandles = grvVTYC.GetSelectedRows();
                for (int i = 0; i < selectedRowHandles.Length; i++)
                {
                    int selectedRowHandle = selectedRowHandles[i];
                    if (selectedRowHandle >= 0)
                    {

                        SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "UPDATE dbo.YCTD_VI_TRI_TUYEN SET ID_TT_VT = 7 WHERE ID_YCTD = " + grvVTYC.GetRowCellValue(selectedRowHandle, "ID_YCTD") + " AND ID_VTTD = " + grvVTYC.GetRowCellValue(selectedRowHandle, "ID_VTTD") + "");
                    }
                }
                LoadgrdVTYC();
            }
            catch
            {

            }
        }
    }
}
