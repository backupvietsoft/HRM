using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Vs.Payroll
{
    public partial class ucQuyDinhHQSX : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;

        public static ucQuyDinhHQSX _instance;
        public static ucQuyDinhHQSX Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucQuyDinhHQSX();
                return _instance;
            }
        }

        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        public ucQuyDinhHQSX()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);

        }

        private void ucQuyDinhHQSX_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadThang();
            LoadCboDonvi();
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadGrdHQSX();
            EnableButon(isAdd);
            Commons.Modules.sLoad = "";
        }

        private void LoadCboDonvi()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDonVi, dt, "ID_DV", "TEN_DV", "TEN_DV");
        }
        private void LoadGrdHQSX()
        {
            try
            {


                DataTable dt = new DataTable();
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListQuyDinhHQSX", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToDateTime(cboThang.EditValue),
                                           cboDonVi.EditValue , cboXiNghiep.EditValue ,cboTo.EditValue ,chkNhinNuoc.EditValue , 1));
                    
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, true, true, true, this.Name);
                    dt.Columns["MS_CN"].ReadOnly = true;
                    dt.Columns["HO_TEN"].ReadOnly = true;
                    grvData.Columns["CHUYEN_CAN_TU"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["CHUYEN_CAN_TU"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["CHUYEN_CAN_DEN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["CHUYEN_CAN_DEN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["MUC_CT_DEN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["MUC_CT_DEN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["MUC_CT_TU"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["MUC_CT_TU"].DisplayFormat.FormatString = "N0";
                   

                }
                else
                {
                    grdData.DataSource = dt;
                    grvData.Columns["CHUYEN_CAN_TU"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["CHUYEN_CAN_TU"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["CHUYEN_CAN_DEN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["CHUYEN_CAN_DEN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["MUC_CHI_TIEU_DEN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["MUC_CHI_TIEU_DEN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["MUC_CHI_TIEU_TU"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["MUC_CHI_TIEU_TU"].DisplayFormat.FormatString = "N0";
                }
            }
            catch
            {

            }
            //////grvData.Columns["ID_GTGC"].Visible = false;
            //////grvData.Columns["THANG"].Visible = false;
            //////grvData.Columns["ID_CN"].Visible = false;
            //////grvData.Columns["SO_TIEN"].DisplayFormat.FormatType = FormatType.Numeric;
            //////grvData.Columns["SO_TIEN"].DisplayFormat.FormatString = "N0";
            //grvData.Columns["THANG"].Visible = false;
            //grvData.Columns["TT"].Visible = false;

        }



        public void LoadThang()
        {
            try
            {
                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT DISTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG_AD,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG_AD,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG_AD,103),7) AS THANG FROM dbo.QUY_DINH_THUONG_HQSX ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang1, dtthang, false, true, true, true, true, this.Name);
                grvThang1.Columns["M"].Visible = false;
                grvThang1.Columns["Y"].Visible = false;

                cboThang.Text = grvThang1.GetFocusedRowCellValue("THANG").ToString();
            }
            catch
            {
                cboThang.Text = DateTime.Now.ToString("MM/yyyy");
            }
        }



        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        isAdd = true;
                        LoadGrdHQSX();
                        Commons.Modules.ObjSystems.AddnewRow(grvData, true);
                        EnableButon(isAdd);
                        break;

                    }
                case "xoa":
                    {
                        XoaHQSX();
                        break;
                    }
                case "ghi":
                    {
                        Validate();
                        if (grvData.HasColumnErrors) return;
                        if (Savedata() == false)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                        }
                        isAdd = false;
                        LoadGrdHQSX();

                        EnableButon(isAdd);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        break;
                    }
                case "khongghi":
                    {
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        isAdd = false;
                        LoadGrdHQSX();
                        EnableButon(isAdd);
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
            }
        }

        private void EnableButon(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = !visible;
            btnALL.Buttons[1].Properties.Visible = !visible;
            btnALL.Buttons[2].Properties.Visible = !visible;
            btnALL.Buttons[3].Properties.Visible = !visible;
            btnALL.Buttons[4].Properties.Visible = visible;
            btnALL.Buttons[5].Properties.Visible = visible;
            cboTo.Enabled = !visible;
            cboThang.Enabled = !visible;
            cboDonVi.Enabled = !visible;
            cboXiNghiep.Enabled = !visible;
        }

        private void XoaHQSX()
        {
            if (grvData.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                string sSql = "DELETE dbo.QUY_DINH_THUONG_HQSX WHERE ID = " + grvData.GetFocusedRowCellValue("ID");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                grvData.DeleteSelectedRows();
            }
            catch
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
            }
        }

        private void grvData_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //try
            //{
            //    GridView view = sender as GridView;
            //    view.SetFocusedRowCellValue("THANG", cboThang.EditValue);
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message.ToString());
            //}
        }

        private void grvData_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
        }

        private bool Savedata()
        {
            string sTB = "HQSX_Tam" + Commons.Modules.UserName;
            try
            {
                if(Convert.ToInt32(cboTo.EditValue) !=  -1)
                {
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveQuyDinhHQSX", sTB, Convert.ToInt32(cboTo.EditValue));

                }
                else
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgVuilongchonto"));
                }
                

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;

        }


        private void grvNgay_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = grvThang1.GetFocusedRowCellValue("THANG").ToString();
            }
            catch { }
            cboThang.ClosePopup();

        }

        private void cboNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdHQSX();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + cboThang.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                }
                else { }
            }
            catch (Exception ex)
            {
                cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
            }
            cboThang.ClosePopup();
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdHQSX();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadGrdHQSX();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadGrdHQSX();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }
        #region chuotphai
        class RowInfo
        {
            public RowInfo(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
            {
                this.RowHandle = rowHandle;
                this.View = view;
            }


            public DevExpress.XtraGrid.Views.Grid.GridView View;
            public int RowHandle;
        }
        //Nhap ung vien
        public DXMenuItem MCreateMenuNhapUngVien(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "ucGiamTruGiaCanh", "CapNhatSoTienAll", Commons.Modules.TypeLanguage);
            DXMenuItem menuCapNhatSoTienAll = new DXMenuItem(sStr, new EventHandler(CapNhatSoTienAll));
            menuCapNhatSoTienAll.Tag = new RowInfo(view, rowHandle);
            return menuCapNhatSoTienAll;
        }
        public void CapNhatSoTienAll(object sender, EventArgs e)
        {
            try
            {
                string sCotCN = grvData.FocusedColumn.FieldName;
                if (grvData.GetFocusedRowCellValue("SO_TIEN").ToString() == "") return;
                string sBTCongNhan = "sBTCongNhan" + Commons.Modules.UserName;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, (DataTable)grdData.DataSource, "");

                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateSO_TIEN", sBTCongNhan, sCotCN, Convert.ToDouble(grvData.GetFocusedRowCellValue("SO_TIEN"))));
                grdData.DataSource = dt;
            }
            catch { }
        }

        private void grvData_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (btnALL.Buttons[0].Properties.Visible == true) return;
                if (grvData.FocusedColumn.FieldName != "SO_TIEN") return;
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();

                    DevExpress.Utils.Menu.DXMenuItem itemNhap = MCreateMenuNhapUngVien(view, irow);
                    e.Menu.Items.Add(itemNhap);
                }
            }
            catch
            {
            }
        }

        #endregion

        private void grvData_RowCountChanged(object sender, EventArgs e)
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

        private void chkNhinNuoc_CheckedChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdHQSX();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }
    }
}