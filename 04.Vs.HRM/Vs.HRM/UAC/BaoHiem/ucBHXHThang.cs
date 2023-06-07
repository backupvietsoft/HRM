using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Vs.HRM
{
    public partial class ucBHXHThang : DevExpress.XtraEditors.XtraUserControl
    {
        RepositoryItemTimeEdit repositoryItemTimeEdit1;

        private static bool isAdd = false;
        DataTable dtThang = null;
        int iIDCN = 0;
        public ucBHXHThang()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
            repositoryItemTimeEdit1 = new RepositoryItemTimeEdit();
        }

        private void ucBHXHThang_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";

                repositoryItemTimeEdit1.TimeEditStyle = TimeEditStyle.TouchUI;
                repositoryItemTimeEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                repositoryItemTimeEdit1.Mask.EditMask = "MM/yyyy";

                repositoryItemTimeEdit1.NullText = "";
                repositoryItemTimeEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                repositoryItemTimeEdit1.DisplayFormat.FormatString = "MM/yyyy";
                repositoryItemTimeEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                repositoryItemTimeEdit1.EditFormat.FormatString = "MM/yyyy";

                DateTime t = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                formatText();

                LoadThang(t);
                LoadGrdBHXHThang();
                LoadGrdBHXHThangDieuChinh();

                RepositoryItemLookUpEdit cboLDC = new RepositoryItemLookUpEdit();
                cboLDC.NullText = "";
                cboLDC.ValueMember = "ID_LOAI_DIEU_CHINH";
                cboLDC.DisplayMember = "TEN_LOAI_DIEU_CHINH";
                cboLDC.DataSource = Commons.Modules.ObjSystems.DataLoaiDieuChinh(false);
                cboLDC.Columns.Clear();
                cboLDC.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_LOAI_DIEU_CHINH", "Loại điều chỉnh"));
                cboLDC.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboLDC.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                grvDieuChinh.Columns["ID_LOAI_DIEU_CHINH"].ColumnEdit = cboLDC;

                Commons.Modules.sLoad = "";
                enableButon(true);

                Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            }
            catch { }
        }


        private void formatText()
        {
            Commons.OSystems.SetDateEditFormat(dtTuNgay);
            Commons.OSystems.SetDateEditFormat(dtDenNgay);
        }

        void LoadTuNgayDenNgay(DateTime dt)
        {
            try
            {
                DateTime tuNgay = new DateTime();
                if (dt.Month == 1)
                {
                    tuNgay = new DateTime(dt.Year - 1, 12, 16);
                }
                else
                {
                    tuNgay = new DateTime(dt.Year, dt.Month - 1, 16);
                }
                DateTime denNgay = new DateTime(dt.Year, dt.Month, 15);

                dtTuNgay.EditValue = tuNgay;
                dtDenNgay.EditValue = denNgay;
            }
            catch
            {

            }

        }

        public void LoadDot(DateTime thang)
        {
            try
            {
                DataRow[] dr;
                // tất cả các dòng cùng tháng
                dr = dtThang.Select(" THANG " + "='" + thang + "'  ", "THANG", DataViewRowState.CurrentRows);
                // chọn lại  cá đợt duy nhất của tháng
                Commons.Modules.ObjSystems.MLoadComboboxEdit(cbDot, dr, "DOT");
                cbDot.SelectedIndex = 0;
                if (dr.Count() >= 1)
                {
                    cboThang.Text = Convert.ToDateTime(dr[0]["THANG"].ToString()).ToString("MM/yyyy");
                    cbDot.EditValue = dr[0]["Dot"].ToString();
                    dtTuNgay.EditValue = dr[0]["TU_NGAY"];
                    dtDenNgay.EditValue = dr[0]["DEN_NGAY"];
                }
                else
                {
                    cbDot.EditValue = 1;
                    LoadTuNgayDenNgay(thang);
                }
            }
            catch (Exception ex)
            {
                LoadNull();
            }

        }

        private void LoadThang(DateTime thang)
        {
            try
            {
                DataTable dtthang = new DataTable();
                dtThang = new DataTable();

                //dtThang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetThangBHXHThang", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                string sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.BHXH_THANG ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, false, true, true, true, this.Name);
                grvThang.Columns["M"].Visible = false;
                grvThang.Columns["Y"].Visible = false;
                cboThang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
                LoadDot(Convert.ToDateTime(cboThang.EditValue));
            }
            catch (Exception ex)
            {
                LoadNull();
            }
        }

        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            LoadTuNgayDenNgay(Convert.ToDateTime(cboThang.EditValue));
            if (Commons.Modules.sLoad == "0Load") return;

            LoadDot(Convert.ToDateTime(cboThang.EditValue));
            LoadTuNgayDenNgay(Convert.ToDateTime(cboThang.EditValue));
            DateTime thang = Convert.ToDateTime("01/01/1900");
            try
            {
                thang = Convert.ToDateTime(cboThang.Text.ToString());
                thang = new DateTime(thang.Year, thang.Month, 1);
            }
            catch
            {

            }
            LoadGrdBHXHThang();
            LoadGrdBHXHThangDieuChinh();
            Commons.Modules.sLoad = "";
        }

        private void cboDot_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            DateTime thang = Convert.ToDateTime("01/01/1900");
            try
            {
                thang = Convert.ToDateTime(cboThang.Text.ToString());
                thang = new DateTime(thang.Year, thang.Month, 1);
            }
            catch
            {

            }
            LoadGrdBHXHThang();
            LoadGrdBHXHThangDieuChinh();
            Commons.Modules.sLoad = "";
        }

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            try
            {
                cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
                DateTime thang = Convert.ToDateTime("01/01/1900");
                try
                {
                    thang = Convert.ToDateTime(cboThang.Text.ToString());
                    thang = new DateTime(thang.Year, thang.Month, 1);
                }
                catch { }
            }
            catch (Exception ex)
            {
                cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
            }
            cboThang.ClosePopup();

            Commons.Modules.sLoad = "";
        }

        //==========Tung sua 23/09/2021

        private void LoadGrdBHXHThang()
        {
            DataTable dt = new DataTable();
            try
            {

                if (isAdd)
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetEditBHXH", Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd"),
                            cbDot.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, dtTuNgay.EditValue, dtDenNgay.EditValue));
                }
                else
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListBHXHThang", Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd"), cbDot.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdTrongThang, grvTrongThang, dt, false, false, true, false, true, this.Name);
                grvTrongThang.Columns["ID_CN"].Visible = false;
                grvTrongThang.Columns["DOT"].Visible = false;
                grvTrongThang.Columns["THANG"].Visible = false;
                grvTrongThang.Columns["ID_LDC"].Visible = false;
                grvTrongThang.Columns["HS_LUONG_CU"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                grvTrongThang.Columns["HS_LUONG_CU"].DisplayFormat.FormatString = Commons.Modules.sSoLeSL;
                grvTrongThang.Columns["HS_LUONG"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                grvTrongThang.Columns["HS_LUONG"].DisplayFormat.FormatString = Commons.Modules.sSoLeSL;
                grvTrongThang.Columns["PHU_CAP_CU"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                grvTrongThang.Columns["PHU_CAP_CU"].DisplayFormat.FormatString = Commons.Modules.sSoLeSL;
                grvTrongThang.Columns["PHU_CAP"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                grvTrongThang.Columns["PHU_CAP"].DisplayFormat.FormatString = Commons.Modules.sSoLeSL;
            }
            catch (Exception ex) { }
        }

        private void LoadGrdBHXHThangDieuChinh()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetDieuChinhBHXH", Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd"), cbDot.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDieuChinh, grvDieuChinh, dt, false, true, true, true, true, this.Name);
                grvDieuChinh.Columns["DOT"].Visible = false;
                grvDieuChinh.Columns["THANG"].Visible = false;
                grvDieuChinh.Columns["LY_DO_TRICH_NOP"].Visible = false;
                grvDieuChinh.Columns["HS_LUONG_CU"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                grvDieuChinh.Columns["HS_LUONG_CU"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvDieuChinh.Columns["HS_LUONG_MOI"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                grvDieuChinh.Columns["HS_LUONG_MOI"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvDieuChinh.Columns["HS_PHU_CAP_CU"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                grvDieuChinh.Columns["HS_PHU_CAP_CU"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvDieuChinh.Columns["HS_PHU_CAP_MOI"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                grvDieuChinh.Columns["HS_PHU_CAP_MOI"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvDieuChinh.Columns["PHAN_TRAM_TRICH_NOP"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                grvDieuChinh.Columns["PHAN_TRAM_TRICH_NOP"].DisplayFormat.FormatString = Commons.Modules.sSoLeSL;

                grvDieuChinh.Columns["TU_THANG"].ColumnEdit = this.repositoryItemTimeEdit1;
                grvDieuChinh.Columns["DEN_THANG"].ColumnEdit = this.repositoryItemTimeEdit1;

                Commons.Modules.ObjSystems.AddCombXtra("ID_CN", "MS_CN", grvDieuChinh, Commons.Modules.ObjSystems.DataCongNhan(false), "ID_CN", "CONG_NHAN");
                //Commons.Modules.ObjSystems.AddCombXtra("MS_CN", "MS_CN", grvDieuChinh, "spGetCongNhan");
                Commons.Modules.ObjSystems.AddCombXtra("ID_LOAI_DIEU_CHINH", "TEN_LOAI_DIEU_CHINH", grvDieuChinh, Commons.Modules.ObjSystems.DataLoaiDieuChinh(false));
            }
            catch (Exception ex) { }
        }

        private void AddnewRow(GridView view, bool add)
        {
            view.OptionsBehavior.Editable = add;
            if (add == true)
            {
                view.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            }
        }

        private void LoadNull()
        {
            try
            {
                if (cboThang.Text == "") cboThang.Text = DateTime.Now.ToString("MM/yyyy");
                cbDot.EditValue = 1;
                //dtTuNgay.EditValue = null;
                //dtDenNgay.EditValue = null;
            }
            catch (Exception ex)
            {
                cboThang.Text = "";
                cbDot.EditValue = null;
                dtTuNgay.EditValue = null;
                dtDenNgay.EditValue = null;
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }



        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        if (string.IsNullOrEmpty(cboThang.Text) || cbDot.EditValue == null || cbDot.EditValue.ToString() == "-1")
                        {
                            Commons.Modules.ObjSystems.msgChung("msgThangkhongduocdetrong");
                            return;
                        }
                        isAdd = true;
                        LoadGrdBHXHThang();

                        Commons.Modules.ObjSystems.AddnewRow(grvDieuChinh, true);
                        enableButon(false);
                        break;
                    }
                case "xoa":
                    {
                        if (grvDieuChinh.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
                        if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
                        //xóa bhxh thang
                        try
                        {
                            string sSql = "DELETE dbo.BHXH_THANG WHERE THANG = '" + Convert.ToDateTime(cboThang.EditValue).ToString("yyyyMMdd") + "' AND DOT = " + cbDot.EditValue;
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                            sSql = "DELETE dbo.DIEU_CHINH_BHXH WHERE THANG = '" + Convert.ToDateTime(cboThang.EditValue).ToString("yyyyMMdd") + "' AND DOT = " + cbDot.EditValue;
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                            sSql = "DELETE dbo.TIEN_BHXH_THANG WHERE THANG = '" + Convert.ToDateTime(cboThang.EditValue).ToString("yyyyMMdd") + "' AND DOT = " + cbDot.EditValue;
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                        }
                        catch
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
                        }
                        LoadGrdBHXHThang();
                        LoadGrdBHXHThangDieuChinh();
                        break;
                    }
                case "In":
                    {

                        frmInBHXH InHopDongCN = new frmInBHXH(Convert.ToDateTime(cboThang.EditValue), Convert.ToInt32(cbDot.EditValue));
                        InHopDongCN.ShowDialog();
                        break;
                    }
                case "luu":
                    {
                        isAdd = false;
                        Validate();
                        if (grvTrongThang.HasColumnErrors) return;
                        if (grvTrongThang.HasColumnErrors) return;
                        ThaoTac(1);
                        enableButon(true);
                        LoadThang(Convert.ToDateTime(cboThang.EditValue));
                        LoadGrdBHXHThang();
                        LoadGrdBHXHThangDieuChinh();
                        break;
                    }
                case "tinhtong":
                    {
                        Validate();
                        if (grvTrongThang.HasColumnErrors) return;
                        if (grvTrongThang.HasColumnErrors) return;
                        ThaoTac(2);
                        enableButon(false);
                        break;
                    }

                case "khongluu":
                    {
                        isAdd = false;
                        Commons.Modules.ObjSystems.DeleteAddRow(grvDieuChinh);
                        LoadGrdBHXHThang();
                        LoadGrdBHXHThangDieuChinh();

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


        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
            windowsUIButton.Buttons[6].Properties.Visible = visible;
            cboThang.ReadOnly = !visible;
            cbDot.ReadOnly = !visible;
            AddnewRow(grvDieuChinh, !visible);

        }

        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch { }
            cboThang.ClosePopup();
        }

        private void grvDieuChinh_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            string sSql = "";
            GridView view = sender as GridView;
            if (view == null) return;
            DateTime dtThang = Convert.ToDateTime("01/01/1900");
            int dot = 1;
            try
            {
                dtThang = Convert.ToDateTime("01/" + cboThang.Text.ToString());
                dot = int.Parse(cbDot.Text);
            }
            catch
            {
            }
            if (e.Column.Name == "colID_CN")
            {
                if (view.GetRowCellValue(e.RowHandle, view.Columns["ID_CN"]).ToString() == "")
                {
                    return;
                }
                iIDCN = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["ID_CN"]));
                sSql = "SELECT  HO +' '+ TEN HO_TEN FROM dbo.CONG_NHAN WHERE ID_CN = " + iIDCN;
                string s = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString();
                view.SetRowCellValue(e.RowHandle, view.Columns["HO_TEN"], s);
                view.SetRowCellValue(e.RowHandle, view.Columns["THANG"], dtThang);
                view.SetRowCellValue(e.RowHandle, view.Columns["DOT"], dot);
            }

            if (e.Column.Name == "colID_LOAI_DIEU_CHINH")
            {
                var va1 = "";
                object va2 = null;
                if (view.GetRowCellValue(e.RowHandle, view.Columns["ID_LOAI_DIEU_CHINH"]) != DBNull.Value)
                {
                    sSql = "SELECT TEN_LOAI_DIEU_CHINH, PHAN_TRAM_DONG FROM dbo.LOAI_DIEU_CHINH  WHERE ID_LOAI_DIEU_CHINH = " + view.GetRowCellValue(e.RowHandle, view.Columns["ID_LOAI_DIEU_CHINH"]) + "";
                    DataTable dtTmp = new DataTable();
                    dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                    va1 = dtTmp.Rows[0]["TEN_LOAI_DIEU_CHINH"].ToString();
                    va2 = dtTmp.Rows[0]["PHAN_TRAM_DONG"];

                }
                view.SetRowCellValue(e.RowHandle, view.Columns["LY_DO_TRICH_NOP"], va1);
                view.SetRowCellValue(e.RowHandle, view.Columns["PHAN_TRAM_TRICH_NOP"], va2);
                view.SetRowCellValue(e.RowHandle, view.Columns["THANG"], dtThang);
                view.SetRowCellValue(e.RowHandle, view.Columns["DOT"], dot);
                return;
            }
            return;
        }


        private bool ThaoTac(int Save = 1)
        {
            try
            {
                string sBT1 = "BHXH_THANG" + Commons.Modules.UserName;
                string sBT2 = "DIEU_CHINH_BHXH" + Commons.Modules.UserName;
                DateTime thang = Convert.ToDateTime("01/01/1900");
                try
                {
                    thang = Convert.ToDateTime("01/" + cboThang.Text.ToString());
                }
                catch { }
                if (string.IsNullOrEmpty(dtTuNgay.Text) || dtTuNgay.EditValue == null || dtTuNgay.EditValue.ToString() == "")
                {

                    Commons.Modules.ObjSystems.msgChung("msgTuNgayKhongDeTrong");
                    dtTuNgay.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(dtDenNgay.Text) || dtDenNgay.EditValue == null || dtDenNgay.EditValue.ToString() == "")
                {
                    Commons.Modules.ObjSystems.msgChung("msgDenNgayKhongDeTrong");
                    dtDenNgay.Focus();
                    return false;
                }
                DataTable tb1 = Commons.Modules.ObjSystems.ConvertDatatable(grvTrongThang);
                if (tb1 != null && tb1.Rows.Count > 0)
                {
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT1, tb1, "");
                }
                else
                {
                    sBT1 = "";
                }
                DataTable tb2 = Commons.Modules.ObjSystems.ConvertDatatable(grvDieuChinh);
                if (tb2 != null && tb2.Rows.Count > 0)
                {
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT2, tb2, "");
                }
                else
                {
                    sBT2 = "";
                }

                // lưu = 1 Tính tổng =2
                if (Save == 1)
                {
                    SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateTienBHXHThang",
                        thang, int.Parse(cbDot.Text), dtTuNgay.DateTime, dtDenNgay.DateTime, sBT1, sBT2, Save.ToString());
                }
                Commons.Modules.ObjSystems.XoaTable(sBT1);
                Commons.Modules.ObjSystems.XoaTable(sBT2);
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                return false;
            }
        }

        private void grdDieuChinh_ProcessGridKey(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                XoaDieuChinhBHXH();
            }
        }

        private void XoaDieuChinhBHXH()
        {
            //xóa
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteDieuChinhBHXH"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE FROM dbo.DIEU_CHINH_BHXH WHERE THANG = '" + Convert.ToDateTime(grvDieuChinh.GetFocusedRowCellValue("THANG")).ToString("yyyy-MM-dd") + "' AND DOT = " + grvDieuChinh.GetFocusedRowCellValue("DOT") + " AND ID_CN = " + grvDieuChinh.GetFocusedRowCellValue("ID_CN") + "");
                grvDieuChinh.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void grvTrongThang_RowCountChanged(object sender, EventArgs e)
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
    }
}