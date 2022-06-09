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
    public partial class ucDangKiKhongTinhChuyenCan : DevExpress.XtraEditors.XtraUserControl
    {
        private bool isAdd = false;
        public static ucDangKiKhongTinhChuyenCan _instance;
        public static ucDangKiKhongTinhChuyenCan Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDangKiKhongTinhChuyenCan();
                return _instance;
            }
        }


        public ucDangKiKhongTinhChuyenCan()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this,new List<LayoutControlGroup>{ Root}, windowsUIButton);
        }
        #region Mã thẻ chấm công
        private void ucDangKiKhongTinhChuyenCan_Load(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadThang();

            enableButon(true);
            LoadGridDSCNKhongTinhCC(isAdd);
            Commons.Modules.sPS = "";
            enableButon(true);
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridDSCNKhongTinhCC(isAdd);
            Commons.Modules.sPS = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridDSCNKhongTinhCC(isAdd);
            Commons.Modules.sPS = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGridDSCNKhongTinhCC(isAdd);
            Commons.Modules.sPS = "";
        }
        private void dNgayXem_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGridDSCNKhongTinhCC(isAdd);
            Commons.Modules.sPS = "";
        }

        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGridDSCNKhongTinhCC(isAdd);
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
                        LoadGridDSCNKhongTinhCC(isAdd);
                        Commons.Modules.ObjSystems.AddnewRow(grvDSCNKTCC, false);
                        break;
                    }
                case "xoa":
                    {
                        if (grvDSCNKTCC.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDeleteThang"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeXoa"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                        XoaData();
                        LoadGridDSCNKhongTinhCC(isAdd);
                        enableButon(true);
                        break;
                    }
                case "luu":
                    {
                        Validate();
                        if (grvDSCNKTCC.HasColumnErrors) return;
                        Savedata();
                        LoadGridDSCNKhongTinhCC(false);
                        enableButon(true);
                        break;
                    }
                case "khongluu":
                    {
                        LoadGridDSCNKhongTinhCC(false);
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
            string stbDanhSachXoa = "XOA_DS" + Commons.Modules.UserName;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbDanhSachXoa, 
                                                                   Commons.Modules.ObjSystems.ConvertDatatable(grvDSCNKTCC), "");
                string sSql = "DELETE DS_CN_KHONG_TINH_CC WHERE LEFT(CONVERT(NVARCHAR,THANG,112),6) = '" 
                               + Convert.ToDateTime(cboThang.EditValue).ToString("yyyyMM") + "' AND ID_CN IN (SELECT ID_CN FROM "+ stbDanhSachXoa  + ")" + "";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                Commons.Modules.ObjSystems.XoaTable(stbDanhSachXoa);
            }
            catch
            {

            }
        }

        #region hàm xử lý dữ liệu
        private void LoadGridDSCNKhongTinhCC(bool isAdd)
        {
            DataTable dt = new DataTable();
            if (isAdd)
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetEditDSCNKhongTinhCC", cboThang.EditValue, cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSCNKTCC, grvDSCNKTCC, dt, true, false, true, true, true, this.Name);
                dt.Columns["CHUYEN_CAN"].ReadOnly = false;
            }
            else
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetDSCNKhongTinhCC", cboThang.EditValue, cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSCNKTCC, grvDSCNKTCC, dt, false, false, true, true, true, this.Name);
            }

            grvDSCNKTCC.Columns["ID_CN"].OptionsColumn.ReadOnly = true;
            grvDSCNKTCC.Columns["MS_CN"].OptionsColumn.ReadOnly = true;
            grvDSCNKTCC.Columns["HO_TEN"].OptionsColumn.ReadOnly = true;
            grvDSCNKTCC.Columns["TEN_XN"].OptionsColumn.ReadOnly = true;
            grvDSCNKTCC.Columns["TEN_TO"].OptionsColumn.ReadOnly = true;
            grvDSCNKTCC.Columns["ID_CN"].Visible = false;
        }
        private void Savedata()
        {
            string stbDSCNKTCC = "tabDSCNKTCC" + Commons.Modules.UserName;
            try
            {
                //tạo một datatable 
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbDSCNKTCC, Commons.Modules.ObjSystems.ConvertDatatable(grvDSCNKTCC), "");
                string sSql = "DELETE DS_CN_KHONG_TINH_CC WHERE LEFT(CONVERT(NVARCHAR,THANG,112),6) = '"
                              + Convert.ToDateTime(cboThang.EditValue).ToString("yyyyMM") +"' AND ID_CN IN (SELECT ID_CN FROM " + stbDSCNKTCC 
                              +") INSERT INTO DS_CN_KHONG_TINH_CC (THANG, ID_CN, CHUYEN_CAN) "
                              +" SELECT '"+ Convert.ToDateTime(cboThang.EditValue).ToString("MM/dd/yyyy") + "',ID_CN, CHUYEN_CAN FROM "+ stbDSCNKTCC + " B WHERE B.CHUYEN_CAN =1";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                Commons.Modules.ObjSystems.XoaTable(stbDSCNKTCC);
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
            cboThang.Enabled = visible;

            searchControl.Visible = true;
            isAdd = !windowsUIButton.Buttons[0].Properties.Visible;
        }
        #endregion

        private void grvDSCNKTCC_RowCountChanged(object sender, EventArgs e)
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
                cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdThang);
            }
            catch
            {
                cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
            }
            cboThang.ClosePopup();
        }

        private void LoadThang()
        {
            try
            {
                DataTable dtthang = new DataTable();
                string sSql = " SELECT DISTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.DS_CN_KHONG_TINH_CC ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                grvThang.Columns["M"].Visible = false;
                grvThang.Columns["Y"].Visible = false;

                if(dtthang.Rows.Count > 0)
                {
                    cboThang.EditValue = dtthang.Rows[0][2];
                }
                else
                {
                    cboThang.Text = DateTime.Now.ToString("MM/yyyy");
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
                cboThang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch { }
            cboThang.ClosePopup();
        }

        private void grvDSCNKTCC_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            if(view.GetRowCellValue(e.RowHandle, "CHUYEN_CAN") == null)
            {
                view.SetRowCellValue(e.RowHandle, "CHUYEN_CAN", false);
            }
        }
    }
}
