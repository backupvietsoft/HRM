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
            Commons.Modules.ObjSystems.ThayDoiNN(this,new List<LayoutControlGroup>{ Root}, windowsUIButton);
        }
        #region Đăng kí Chấm tự động
        private void ucDangKiChamTuDong_Load(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadNgay();

            enableButon(true);
            LoadGridDKChamTuDong(isAdd);
            Commons.Modules.sPS = "";
            enableButon(true);
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridDKChamTuDong(isAdd);
            Commons.Modules.sPS = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridDKChamTuDong(isAdd);
            Commons.Modules.sPS = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGridDKChamTuDong(isAdd);
            Commons.Modules.sPS = "";
        }
        private void dNgayXem_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGridDKChamTuDong(isAdd);
            Commons.Modules.sPS = "";
        }

        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGridDKChamTuDong(isAdd);
            Commons.Modules.sPS = "";
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
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDeleteThang"), 
                        Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeXoa"), MessageBoxButtons.YesNo) == DialogResult.No) return;
                        XoaData();
                        enableButon(true);
                        LoadGridDKChamTuDong(isAdd);
                        break;
                    }
                case "luu":
                    {
                        Validate();
                        if (grvKDCTD.HasColumnErrors) return;
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
            string stbXoaData = "XOA_DANG_KY_CHAM_TU_DONG" + Commons.Modules.UserName;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbXoaData, 
                                                                   Commons.Modules.ObjSystems.ConvertDatatable(grvKDCTD), "");
                string sSql = "DELETE DANG_KY_CHAM_TU_DONG WHERE CONVERT(NVARCHAR,NGAY,112) = '"
                               + Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd") + "'"
                               + " AND ID_CN IN (SELECT ID_CN FROM " + stbXoaData + ")";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                Commons.Modules.ObjSystems.XoaTable(stbXoaData);
            }
            catch
            {

            }
        }

        #region hàm xử lý dữ liệu 
        private void LoadGridDKChamTuDong(bool isAdd)
        {
            try
            {
                DataTable dt = new DataTable();
                if (isAdd)
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetEditDKChamTuDong", cboNgay.EditValue, cboDV.EditValue, 
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
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListDKChamTuDong", cboNgay.EditValue, cboDV.EditValue, 
                                                    cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDKCTD, grvKDCTD, dt, false, false, true, true, true, this.Name);
                }
            }
            catch
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
                string sSql = "DELETE DANG_KY_CHAM_TU_DONG WHERE  CONVERT(NVARCHAR,NGAY,112) = '" + Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd") 
                            + "' AND ID_CN IN (SELECT ID_CN FROM " + stbDKCTD + ")"
                            + " INSERT INTO DANG_KY_CHAM_TU_DONG (NGAY, ID_CN) SELECT '" + Convert.ToDateTime(cboNgay.EditValue).ToString("MM/dd/yyyy") 
                            + "',ID_CN FROM "+ stbDKCTD + " B WHERE CHON = 1" + "";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
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
            cboNgay.Enabled = visible;

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

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboNgay.Text = calThang.DateTime.ToString("dd/MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdThang);
            }
            catch
            {
                cboNgay.Text = calThang.DateTime.ToString("dd/MM/yyyy");
            }
            cboNgay.ClosePopup();
        }

        private void LoadNgay()
        {
            try
            {
                DataTable dtthang = new DataTable();
                string sSql = " SELECT DISTINCT SUBSTRING(CONVERT(VARCHAR(10),NGAY,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),NGAY,103),4) AS Y " +
                              ", CONVERT(VARCHAR(10),NGAY,103) AS NGAY FROM dbo.DANG_KY_CHAM_TU_DONG ORDER BY NGAY DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                grvThang.Columns["M"].Visible = false;
                grvThang.Columns["Y"].Visible = false;

                if(dtthang.Rows.Count > 0)
                {
                    cboNgay.EditValue = dtthang.Rows[0][2];
                }
                else
                {
                    cboNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
            catch
            {
            }
        }

        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                cboNgay.Text = grvThang.GetFocusedRowCellValue("NGAY").ToString();
            }
            catch { }
            cboNgay.ClosePopup();
        }
    }
}
