using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using System.Xml.Linq;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraLayout;
using DevExpress.Utils;
using DevExpress.Utils.Menu;

namespace Vs.Payroll
{
    public partial class ucTienCongTru : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;
        
        public static ucTienCongTru _instance;
        public static ucTienCongTru Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucTienCongTru();
                return _instance;
            }
        }

        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        public ucTienCongTru()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);
           
        }

        private void ucTienCongTru_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadThang();
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadGrdGTGC();
            EnableButon(isAdd); 
            Commons.Modules.sLoad = "";
        }

        private void LoadGrdGTGC()
        {
            try
            {
                DataTable dt = new DataTable();
                if (isAdd)
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetEditTienCongTru", Convert.ToDateTime(cboThang.EditValue),
                                                cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    if(grdData.DataSource == null)
                    {
                        Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, false, false, true, true, this.Name);
                        dt.Columns["MS_CN"].ReadOnly = true;
                        dt.Columns["HO_TEN"].ReadOnly = true;
                        dt.Columns["ID_CV"].ReadOnly = true;
                    }
                    else
                    {
                        grdData.DataSource = dt;
                    }
                }
                else
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetlistTienCongTru", Convert.ToDateTime(cboThang.EditValue),
                                                cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    if(grdData.DataSource == null)
                    {
                        Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, false, true, true, this.Name);
                        grvData.Columns["ID_CN"].Visible = false;
                        grvData.Columns["THANG"].Visible = false;
                        grvData.Columns["TIEN_BO_SUNG"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["TIEN_BO_SUNG"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["TIEN_DIEU_CHINH"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["TIEN_DIEU_CHINH"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["THOI_THU_BHXH"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["THOI_THU_BHXH"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["THOI_THU_BHYT"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["THOI_THU_BHYT"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["CONG_KHAC"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["CONG_KHAC"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["TRUY_THU_BHXH"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["TRUY_THU_BHXH"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["TRUY_THU_BHYT"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["TRUY_THU_BHYT"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["TRU_KHAC"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["TRU_KHAC"].DisplayFormat.FormatString = "N0";
                        //,, , , , , , , ,

                        grvData.Columns["THUONG_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["THUONG_LUONG"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["THOI_THU_BHTN"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["THOI_THU_BHTN"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["THOI_THU_TNCN"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["THOI_THU_TNCN"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["TRUY_THU_BHTN"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["TRUY_THU_BHTN"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["TRUY_THU_TNCN"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["TRUY_THU_TNCN"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["TRUY_THU_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["TRUY_THU_LUONG"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["TRUY_THU_TIEN_CC"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["TRUY_THU_TIEN_CC"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["TRUY_THU_TIEN_TC"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["TRUY_THU_TIEN_TC"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["HO_TRO_TN"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["HO_TRO_TN"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["BS_CDLDN"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["BS_CDLDN"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["BS_VIECRIENG"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["BS_VIECRIENG"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["BS_TIENPHEP"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["BS_TIENPHEP"].DisplayFormat.FormatString = "N0";
                        //, , , , , , , , , , , ,

                        grvData.Columns["BS_LETET"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["BS_LETET"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["BS_TANGCA"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["BS_TANGCA"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["BS_CHUYENCAN"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["BS_CHUYENCAN"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["TT_CDLDN"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["TT_CDLDN"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["TT_VIECRIENG"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["TT_VIECRIENG"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["TT_TIENPHEP"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["TT_TIENPHEP"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["TT_LETET"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["TT_LETET"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["TT_TANGCA"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["TT_TANGCA"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["TT_CHUYENCAN"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["TT_CHUYENCAN"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["TT_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["TT_LUONG"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["BS_MAYMAU"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["BS_MAYMAU"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["BS_UIMAU"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["BS_UIMAU"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["TT_MAYMAU"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["TT_MAYMAU"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["TT_UIMAU"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["TT_UIMAU"].DisplayFormat.FormatString = "N0";
                        grvData.Columns["CONG_KHAC_TM"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvData.Columns["CONG_KHAC_TM"].DisplayFormat.FormatString = "N0";
                    }
                    else
                    {
                        grdData.DataSource = dt;
                    }
                }
                DataTable dID_CV = new DataTable();
                dID_CV.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCv", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.AddCombXtra("ID_CV", "TEN_CV", grvData, dID_CV, false);

            }
            catch
            {

            }
            
            //, , ,, , , 
        }



        public void LoadThang()
        {
            try
            {

                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.TIEN_CONG_TRU ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                grvThang.Columns["M"].Visible = false;
                grvThang.Columns["Y"].Visible = false;

                cboThang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch (Exception ex)
            {
                DateTime now = DateTime.Now;
                
                cboThang.Text =  now.Month+ "/"+now.Year.ToString();
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
                        LoadGrdGTGC();
                        Commons.Modules.ObjSystems.AddnewRow(grvData,false);
                        EnableButon(isAdd);
                        break;
                        
                    }
                case "xoa":
                    {
                        XoaCheDoLV();
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
                        LoadGrdGTGC();
                        LoadThang();
                        EnableButon(isAdd);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        break;
                    }
                case "khongghi":
                    {
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        isAdd = false;
                        LoadGrdGTGC();                        
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

        private void XoaCheDoLV()
        {
            if (grvData.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                string sSql = "DELETE dbo.TIEN_CONG_TRU WHERE ID_CN = " + grvData.GetFocusedRowCellValue("ID_CN") +
                                                        " AND THANG = '"
                                                        + Convert.ToDateTime(cboThang.EditValue).ToString("yyyyMMdd") + "'";
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
            string sTB = "TCT_Tam" + Commons.Modules.UserName;
            try
            {
                
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveTienCongTru", sTB);
                Commons.Modules.ObjSystems.XoaTable(sTB);

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
                cboThang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch { }
            cboThang.ClosePopup();
            
        }

        private void cboNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdGTGC();
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
            LoadGrdGTGC();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadGrdGTGC();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadGrdGTGC();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }
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
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "ucTienCongTru", "CapNhatTien", Commons.Modules.TypeLanguage);
            DXMenuItem menuCapNhat = new DXMenuItem(sStr, new EventHandler(CapNhat));
            menuCapNhat.Tag = new RowInfo(view, rowHandle);
            return menuCapNhat;
        }
        public void CapNhat(object sender, EventArgs e)
        {
            try
            {
                string sCotCN = grvData.FocusedColumn.FieldName;
                if (grvData.GetFocusedRowCellValue(grvData.FocusedColumn.FieldName).ToString() == "") return;
                string sBTCongNhan = "sBTCongNhan" + Commons.Modules.UserName;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, (DataTable)grdData.DataSource, "");

                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateChuotPhaiTIEN_CONG_TRU", sBTCongNhan, sCotCN, Convert.ToDouble(grvData.GetFocusedRowCellValue(grvData.FocusedColumn.FieldName))));
                grdData.DataSource = dt;
            }
            catch (Exception EX) { }
        }

        private void grvData_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (btnALL.Buttons[0].Properties.Visible == true) return;
                if (grvData.FocusedColumn.FieldName == "THANG" || grvData.FocusedColumn.FieldName == "MS_CN" || grvData.FocusedColumn.FieldName == "HO_TEN" || grvData.FocusedColumn.FieldName == "CONG_KHAC_TM") return;
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
    }
}