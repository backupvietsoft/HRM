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
    public partial class ucDangKiCaDem : DevExpress.XtraEditors.XtraUserControl
    {
        private bool isAdd = false;
        public static ucDangKiCaDem _instance;
        public static ucDangKiCaDem Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDangKiCaDem();
                return _instance;
            }
        }


        public ucDangKiCaDem()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this,new List<LayoutControlGroup>{ Root}, windowsUIButton);
        }
        #region Đăng kí ca đêm
        private void ucDangKiCaDem_Load(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadNgay();

            enableButon(true);
            LoadGridDSDKLamDem(isAdd);
            Commons.Modules.sPS = "";
            enableButon(true);
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridDSDKLamDem(isAdd);
            Commons.Modules.sPS = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridDSDKLamDem(isAdd);
            Commons.Modules.sPS = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGridDSDKLamDem(isAdd);
            Commons.Modules.sPS = "";
        }
        private void dNgayXem_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGridDSDKLamDem(isAdd);
            Commons.Modules.sPS = "";
        }

        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGridDSDKLamDem(isAdd);
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
                        LoadGridDSDKLamDem(isAdd);
                        Commons.Modules.ObjSystems.AddnewRow(grvDSDKLD, false);
                        break;
                    }
                case "xoa":
                    {
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDeleteThang"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeXoa"), MessageBoxButtons.YesNo) == DialogResult.No) return;
                        XoaData();
                        enableButon(true);
                        LoadGridDSDKLamDem(isAdd);
                        break;
                    }
                case "ghi":
                    {
                        if (grvDSDKLD.HasColumnErrors) return;
                        Savedata();
                        enableButon(true);
                        LoadGridDSDKLamDem(isAdd);
                        break;
                    }
                case "khongghi":
                    {
                        LoadGridDSDKLamDem(false);
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
            try
            {
                string XoaData = "Xoa_DANG_KY_LAM_DEM" + Commons.Modules.UserName;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, XoaData, Commons.Modules.ObjSystems.ConvertDatatable(grvDSDKLD), "");
                string sSql = "DELETE DANG_KY_LAM_DEM WHERE CONVERT(NVARCHAR,NGAY,112) = '" + Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd") 
                             + "' AND ID_CN IN (SELECT ID_CN FROM "+ XoaData +")";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                Commons.Modules.ObjSystems.XoaTable(XoaData);
            }
            catch
            {

            }
        }

        #region hàm xử lý dữ liệu
        private void LoadGridDSDKLamDem(bool isAdd)
        {
            try
            {
                DataTable dt = new DataTable();
                if (isAdd)
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetEditDSDKLamDem", cboNgay.EditValue, cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    dt.Columns["CHON"].ReadOnly = false;
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSDKLD, grvDSDKLD, dt, true, false, true, true, true, this.Name);
                    grvDSDKLD.Columns["ID_CN"].OptionsColumn.ReadOnly = true;
                    grvDSDKLD.Columns["MS_CN"].OptionsColumn.ReadOnly = true;
                    grvDSDKLD.Columns["HO_TEN"].OptionsColumn.ReadOnly = true;
                    grvDSDKLD.Columns["TEN_XN"].OptionsColumn.ReadOnly = true;
                    grvDSDKLD.Columns["TEN_TO"].OptionsColumn.ReadOnly = true;
                    grvDSDKLD.Columns["ID_CN"].Visible = false;
                }
                else
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListDSDKLamDem", cboNgay.EditValue, cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSDKLD, grvDSDKLD, dt, false, false, true, true, true, this.Name);
                }
                //LoadNgay();
            }
            catch
            {

            }
            grvDSDKLD.Columns["ID_CN"].Visible = false;
        }
        private void Savedata()
        {
            string stbDSDKLD = "DSDKLD" + Commons.Modules.UserName;
            try
            {
                //tạo một datatable 
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbDSDKLD , Commons.Modules.ObjSystems.ConvertDatatable(grvDSDKLD), "");
                string sSql = "DELETE DANG_KY_LAM_DEM WHERE  CONVERT(NVARCHAR,NGAY,112) = '" + Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd") 
                             + "' AND ID_CN IN (SELECT ID_CN FROM " + stbDSDKLD + ")"
                             + " INSERT INTO DANG_KY_LAM_DEM (NGAY, ID_CN) SELECT '" + Convert.ToDateTime(cboNgay.EditValue).ToString("MM/dd/yyyy") 
                             + "',ID_CN FROM "+ stbDSDKLD + " B WHERE CHON = 1" + "";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                Commons.Modules.ObjSystems.XoaTable(stbDSDKLD);
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

        private void grvDSDKLD_RowCountChanged(object sender, EventArgs e)
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
                cboNgay.EditValue = calThang.DateTime.Date.ToShortDateString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                cboNgay.Text = DateTime.Now.ToShortDateString();
            }
            cboNgay.ClosePopup();

            //try
            //{
            //    cboNgay.Text = calThang.DateTime.ToString("dd/MM/yyyy");
            //    DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdThang);
            //}
            //catch
            //{
            //    cboNgay.Text = calThang.DateTime.ToString("dd/MM/yyyy");
            //}
            //cboNgay.ClosePopup();
        }

        private void LoadNgay()
        {
            try
            {
                DataTable dtthang = new DataTable();
                string sSql = " SELECT DISTINCT NGAY FROM dbo.DANG_KY_LAM_DEM ORDER BY NGAY DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));

                if (grdThang.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                }
                else
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, false, true, false, false, this.Name);

                if (dtthang.Rows.Count <= 0)
                {
                    cboNgay.EditValue = DateTime.Today;
                }
                else
                {
                    cboNgay.EditValue = dtthang.Rows[0]["NGAY"];
                }

                //Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                //grvThang.Columns["M"].Visible = false;
                //grvThang.Columns["Y"].Visible = false;

                
            }
            catch
            {
            }
        }
        private void LoadNull()
        {
            try
            {
                if (cboNgay.Text == "") cboNgay.Text = DateTime.Now.ToShortDateString();
            }
            catch (Exception ex)
            {
                cboNgay.Text = "";
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboNgay.Text = Convert.ToDateTime(grv.GetFocusedRowCellValue("NGAY").ToString()).ToShortDateString();
            }
            catch { LoadNull(); }
            cboNgay.ClosePopup();

            //try
            //{
            //    cboNgay.Text = grvThang.GetFocusedRowCellValue("NGAY").ToString();
            //}
            //catch { }
            //cboNgay.ClosePopup();
        }
    }
}
