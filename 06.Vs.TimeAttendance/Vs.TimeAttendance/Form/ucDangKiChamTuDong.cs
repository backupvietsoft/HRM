using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraLayout;
using System.Threading;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraGrid.Views.Grid;

namespace Vs.TimeAttendance
{
    public partial class ucDangKiChamTuDong : DevExpress.XtraEditors.XtraUserControl
    {
        private bool isAdd = false;
        public static ucDangKiChamTuDong _instance;
        public static ucDangKiChamTuDong Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDangKiChamTuDong();
                return _instance;
            }
        }


        public ucDangKiChamTuDong()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, windowsUIButton);
        }
        #region Đăng kí Chấm tự động
        private void ucDangKiChamTuDong_Load(object sender, EventArgs e)
        {
            try
            {
                Thread.Sleep(1000);
                Commons.Modules.sLoad = "0Load";
                Commons.OSystems.SetDateEditFormat(datTNgay);
                Commons.OSystems.SetDateEditFormat(datDenNgay);
                Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
                Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
                Commons.Modules.sLoad = "";
                datTNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year));
                enableButon(true);
            }
            catch { }

        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridDKChamTuDong(isAdd);
            Commons.Modules.sLoad = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridDKChamTuDong(isAdd);
            Commons.Modules.sLoad = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGridDKChamTuDong(isAdd);
            Commons.Modules.sLoad = "";
        }
        private void dNgayXem_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGridDKChamTuDong(isAdd);
            Commons.Modules.sLoad = "";
        }

        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGridDKChamTuDong(isAdd);
            Commons.Modules.sLoad = "";
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        enableButon(false);
                        LoadGridDKChamTuDong(isAdd);
                        Commons.Modules.ObjSystems.AddnewRow(grvKDCTD, false);
                        break;
                    }
                case "xoa":
                    {
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoMuonXoaDuLieuCuaNgayNay"),
                        Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                        XoaData();
                        enableButon(true);
                        LoadGridDKChamTuDong(isAdd);
                        break;
                    }
                case "luu":
                    {
                        Validate();
                        if (grvKDCTD.HasColumnErrors) return;
                        DataTable dt = new DataTable();
                        dt = (DataTable)grdDKCTD.DataSource;
                        this.Cursor = Cursors.WaitCursor;
                        if (!KiemTraLuoi(dt)) return;
                        this.Cursor = Cursors.Default;
                        Savedata();
                        enableButon(true);
                        LoadGridDKChamTuDong(isAdd);
                        break;
                    }
                case "khongluu":
                    {
                        LoadGridDKChamTuDong(false);
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
        #endregion

        private void XoaData()
        {
            //string stbXoaData = "XOA_DANG_KY_CHAM_TU_DONG" + Commons.Modules.UserName;
            //try
            //{
            //    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbXoaData,
            //                                                       Commons.Modules.ObjSystems.ConvertDatatable(grvKDCTD), "");
            //    string sSql = "DELETE DANG_KY_CHAM_TU_DONG WHERE CONVERT(NVARCHAR,NGAY,112) = '"
            //                   + Convert.ToDateTime(Commons.Modules.ObjSystems.ConvertDateTime(datTNgay.Text)).ToString("yyyyMMdd") + "'"
            //                   + " AND ID_CN IN (SELECT ID_CN FROM " + stbXoaData + ")";
            //    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
            //    Commons.Modules.ObjSystems.XoaTable(stbXoaData);
            //}
            //catch
            //{

            //}
            try
            {
                string sSql = "DELETE FROM dbo.DANG_KY_CHAM_TU_DONG WHERE ID_CN = "+grvKDCTD.GetFocusedRowCellValue("ID_CN")+" AND NGAY =  '"+ Convert.ToDateTime(grvKDCTD.GetFocusedRowCellValue("NGAY")).ToString("MM/dd/yyyy") + "'";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
            }
            catch { }
        }

        #region hàm xử lý dữ liệu 
        private void LoadGridDKChamTuDong(bool isAdd)
        {
            try
            {
                DataTable dt = new DataTable();
                if (isAdd)
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetEditDKChamTuDong", Commons.Modules.ObjSystems.ConvertDateTime(datTNgay.Text), cboDV.EditValue,
                                                    cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    dt.Columns["CHON"].ReadOnly = false;
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDKCTD, grvKDCTD, dt, true, false, true, true, true, this.Name);
                    grvKDCTD.Columns["ID_CN"].OptionsColumn.ReadOnly = true;
                    grvKDCTD.Columns["MS_CN"].OptionsColumn.ReadOnly = true;
                    grvKDCTD.Columns["HO_TEN"].OptionsColumn.ReadOnly = true;
                    grvKDCTD.Columns["TEN_XN"].OptionsColumn.ReadOnly = true;
                    grvKDCTD.Columns["TEN_TO"].OptionsColumn.ReadOnly = true;
                    grvKDCTD.Columns["ID_CN"].Visible = false;
                }
                else
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListDKChamTuDong", Commons.Modules.ObjSystems.ConvertDateTime(datTNgay.Text), Commons.Modules.ObjSystems.ConvertDateTime(datDenNgay.Text), cboDV.EditValue,
                                                    cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDKCTD, grvKDCTD, dt, false, false, true, true, true, this.Name);
                }
            }
            catch (Exception ex)
            {

            }
            grvKDCTD.Columns["ID_CN"].Visible = false;
        }
        private void Savedata()
        {
            string stbDKCTD = "DKCTD" + Commons.Modules.UserName;
            try
            {
                //tạo một datatable 
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbDKCTD, Commons.Modules.ObjSystems.ConvertDatatable(grvKDCTD), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveDANG_KY_CHAM_TU_DONG", Commons.Modules.UserName, Commons.Modules.TypeLanguage, stbDKCTD);
                Commons.Modules.ObjSystems.XoaTable(stbDKCTD);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;

            cboDV.Enabled = visible;
            cboXN.Enabled = visible;
            cboTo.Enabled = visible;
            datTNgay.Enabled = visible;
            datDenNgay.Enabled = visible;

            searchControl.Visible = true;
            isAdd = !windowsUIButton.Buttons[0].Properties.Visible;
        }
        #endregion

        private void grvKDCTD_RowCountChanged(object sender, EventArgs e)
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

        private void datTNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.ConvertDateTime(datTNgay.Text);
            int t = DateTime.DaysInMonth(datTNgay.DateTime.Year, datTNgay.DateTime.Month);
            DateTime secondDateTime = new DateTime(datTNgay.DateTime.Year, datTNgay.DateTime.Month, t);
            datDenNgay.EditValue = secondDateTime;
            LoadGridDKChamTuDong(isAdd);
            Commons.Modules.sLoad = "";
        }

        private void datDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadGridDKChamTuDong(isAdd);
        }

        #region KiemTraLuoi

        private bool KiemTraLuoi(DataTable dtSource)
        {
            int count = grvKDCTD.RowCount;
            int col = 0;
            int errorCount = 0;
            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                col = 0;
                if (Convert.ToBoolean(dr["CHON"]) == true)
                {
                    //Ngày bắt đầu thử việc
                    if (!KiemDuLieuNgay(grvKDCTD, dr, "NGAY", true, this.Name))
                    {
                        errorCount++;
                    }

                    //Ngày kết thúc thử việc
                    if (!KiemDuLieuNgay(grvKDCTD, dr, "NGAY_KT", false, this.Name))
                    {
                        errorCount++;
                    }
                }
            }
            #endregion
            Commons.Modules.ObjSystems.HideWaitForm();
            if (errorCount != 0)
            {
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public bool KiemDuLieuNgay(GridView grvData, DataRow dr, string sCot, Boolean bKiemNull, string sform)
        {
            string sDLKiem;
            sDLKiem = dr[sCot].ToString();
            DateTime DLKiem;

            try
            {

                if (bKiemNull)
                {
                    if (string.IsNullOrEmpty(sDLKiem))
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongduocTrong"));
                        return false;
                    }
                    else
                    {
                        //sDLKiem = DateTime.ParseExact(sDLKiem, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString();
                        if (!DateTime.TryParse(sDLKiem, out DLKiem))
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
                            return false;
                        }

                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(sDLKiem))
                    {
                        if (!DateTime.TryParse(sDLKiem, out DLKiem))
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
                            return false;
                        }
                    }
                }
            }
            catch
            {
                dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
                return false;
            }
            return true;
        }
        #endregion
    }
}
