﻿using System;
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
using System.Globalization;
using DevExpress.Utils.Menu;

namespace Vs.Payroll
{
    public partial class ucDanhSachTamUng : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;
        private static int type = 1;

        public static ucDanhSachTamUng _instance;
        public static ucDanhSachTamUng Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDanhSachTamUng();
                return _instance;
            }
        }
        public ucDanhSachTamUng()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);
           
        }

        private void ucDanhSachTamUng_Load(object sender, EventArgs e)
        {
            try
            {

                Commons.Modules.sPS = "0Load";
                LoadThang();
                LoadDot();
                string strSQL = "SELECT DISTINCT NGAY_TINH_CONG FROM dbo.TAM_UNG WHERE THANG = '" + Convert.ToDateTime(cboThang.EditValue).ToString("MM/dd/yyyy") + "' AND DOT = " + cboDot.EditValue + "";
                try { datTINH_CONG_DN.EditValue = Convert.ToDateTime(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL)) == DateTime.MinValue ? DateTime.Now : Convert.ToDateTime(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL)); } catch { datTINH_CONG_DN.EditValue = DateTime.Now; }
                Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
                Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
                LoadDanhSachTamUng();

                EnableButon(isAdd);
                Commons.Modules.sPS = "";
            }
            catch { }
        }

        private void LoadDanhSachTamUng()
        {
            DataTable dt = new DataTable();
            try
            {
                
                if (isAdd)
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spAddDanhSachTamUng", Convert.ToDateTime(cboThang.EditValue), Convert.ToDateTime(datTINH_CONG_DN.EditValue),
                                                cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, cboDot.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, type));

                    dt.Columns["MS_CN"].ReadOnly = true;
                    dt.Columns["HO_TEN"].ReadOnly = true;
                    dt.Columns["SO_TIEN"].ReadOnly = false;
                    dt.Columns["NGAY_CONG"].ReadOnly = true;
                    dt.Columns["MUC_LUONG"].ReadOnly = true;
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, true, true, true, this.Name);
                    
                }
                else
                {
                 
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetlistDanhSachTamUng", Convert.ToDateTime(cboThang.EditValue),
                                                cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, cboDot.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    dt.Columns["MS_CN"].ReadOnly = true;
                    dt.Columns["HO_TEN"].ReadOnly = true;
                    dt.Columns["SO_TIEN"].ReadOnly = false;
                    dt.Columns["NGAY_CONG"].ReadOnly = true;
                    dt.Columns["MUC_LUONG"].ReadOnly = true;
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, true, true, true, this.Name);
                }
                
                
            }
            catch
            {

            }
            grvData.Columns["ID_TU"].Visible = false;
            grvData.Columns["THANG"].Visible = false;
            grvData.Columns["ID_CN"].Visible = false;
            grvData.Columns["DOT"].Visible = false;
            grvData.Columns["SO_TIEN"].DisplayFormat.FormatType = FormatType.Numeric;
            grvData.Columns["SO_TIEN"].DisplayFormat.FormatString = "N0";
            grvData.Columns["MUC_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
            grvData.Columns["MUC_LUONG"].DisplayFormat.FormatString = "N0";
            //grvData.Columns["THANG"].Visible = false;
            int tt =0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lblTien.Text = dt.Rows[i]["SO_TIEN"].ToString();
                tt = tt + Int32.Parse(dt.Rows[i]["SO_TIEN"].ToString());
            }

            lblTien.Text = String.Format(CultureInfo.InvariantCulture,
                                 "{0:0,0}", tt);

        }

        public void LoadThang()
        {
            try
            {

                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.TAM_UNG ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                grvThang.Columns["M"].Visible = false;
                grvThang.Columns["Y"].Visible = false;

                cboThang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch (Exception ex)
            {
                cboThang.Text = DateTime.Now.Month + "/" + DateTime.Now.Year;
            }
        }
        public void LoadDot()
        {
            try
            {
                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtdot = new DataTable();
                string sSql = "SELECT disTINCT DOT FROM dbo.TAM_UNG ORDER BY DOT ASC";
                dtdot.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadComboboxEdit(cboDot, dtdot, "DOT");
                cboDot.EditValue = 1;
            }
            catch (Exception ex)
            {
                cboDot.Text = "1";
            }
        }


        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "tinhtamung":
                    {
                        isAdd = true;
                        EnableButon(isAdd);
                        type = 2;
                        LoadDanhSachTamUng();
                        break;
                    }
                case "them":
                    {
                        isAdd = true;
                        type = 1;
                        LoadDanhSachTamUng();
                        Commons.Modules.ObjSystems.AddnewRow(grvData,false);
                        EnableButon(isAdd);
                        break;
                        
                    }
                case "sua":
                    {
                        isAdd = false;
                        LoadDanhSachTamUng();
                        Commons.Modules.ObjSystems.AddnewRow(grvData, false);
                        EnableButon(true);
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
                        LoadThang();
                        LoadDot();

                        LoadDanhSachTamUng();
                        EnableButon(isAdd);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        break;
                    }
                case "khongghi":
                    {
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        isAdd = false;
                        LoadDanhSachTamUng();                        
                        EnableButon(isAdd);
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                case "in":
                    {
                        int Dot = Convert.ToInt32(grvData.GetFocusedRowCellValue("DOT"));
                        DateTime thang = Convert.ToDateTime(grvData.GetFocusedRowCellValue("THANG"));
                        int to = Convert.ToInt32(cboTo.EditValue);
                        frmInDanhSachTamUng InDanhSachTamUng = new frmInDanhSachTamUng(Dot, thang, to);
                        InDanhSachTamUng.ShowDialog();
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
            btnALL.Buttons[4].Properties.Visible = !visible;
            btnALL.Buttons[5].Properties.Visible = !visible;
            btnALL.Buttons[6].Properties.Visible = visible;
            btnALL.Buttons[7].Properties.Visible = visible;
            btnALL.Buttons[8].Properties.Visible = !visible;
            btnALL.Buttons[9].Properties.Visible = !visible;
            btnALL.Buttons[10].Properties.Visible = !visible;
            cboTo.Enabled = !visible;
            cboThang.Enabled = !visible;
            cboDonVi.Enabled = !visible;
            cboXiNghiep.Enabled = !visible;
            cboDot.Enabled = !visible;
        }

        private void XoaCheDoLV()
        {
            if (grvData.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                string sSql = "DELETE dbo.TAM_UNG WHERE ID_TU = " + grvData.GetFocusedRowCellValue("ID_TU");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                grvData.DeleteSelectedRows();
            }
            catch
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
            }
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
            string sTB = "DSTU_Tam" + Commons.Modules.UserName;
            try
            {
                
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveDanhSachTamUng", sTB, Convert.ToDateTime(cboThang.EditValue),cboDot.EditValue, Convert.ToDateTime(datTINH_CONG_DN.EditValue));
                Commons.Modules.ObjSystems.XoaTable(sTB);

                return true;
            }
            catch (Exception ex)
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
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadDanhSachTamUng();
            string strSQL = "SELECT DISTINCT NGAY_TINH_CONG FROM dbo.TAM_UNG WHERE THANG = '" + Convert.ToDateTime(cboThang.EditValue).ToString("MM/dd/yyyy") + "' AND DOT = " + cboDot.EditValue + "";
            datTINH_CONG_DN.EditValue = Convert.ToDateTime(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL)) == DateTime.MinValue ? DateTime.Now : Convert.ToDateTime(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));
            //EnableButon(true);
            Commons.Modules.sPS = "";
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
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadDanhSachTamUng();
            //EnableButon(true);
            Commons.Modules.sPS = "";
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadDanhSachTamUng();
            //EnableButon(true);
            Commons.Modules.sPS = "";
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadDanhSachTamUng();
            //EnableButon(true);
            Commons.Modules.sPS = "";
        }

        private void cboDot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               
                if (Commons.Modules.sPS == "0Load") return;
                Commons.Modules.sPS = "0Load";
                LoadDanhSachTamUng();
                Commons.Modules.sPS = "";
            }
        }

        private void cboDot_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadDanhSachTamUng();
            string strSQL = "SELECT DISTINCT NGAY_TINH_CONG FROM dbo.TAM_UNG WHERE THANG = '"+Convert.ToDateTime(cboThang.EditValue).ToString("MM/dd/yyyy")+"' AND DOT = "+cboDot.EditValue+"";
            datTINH_CONG_DN.EditValue = Convert.ToDateTime(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL)) == DateTime.MinValue ? DateTime.Now : Convert.ToDateTime(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));
            Commons.Modules.sPS = "";
        }


        private bool SuaSoTien()
        {
            string sTB = "DSTU_SuaTien" + Commons.Modules.UserName;
            try
            {
                //Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                //DataTable dt = new DataTable();
                //dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spSuaSoTienDanhSachTamUng", txtSoTien.EditValue, sTB));
                //dt.Columns["MS_CN"].ReadOnly = true;
                //dt.Columns["HO_TEN"].ReadOnly = true;
                //dt.Columns["SO_TIEN"].ReadOnly = false;
                //dt.Columns["NGAY_CONG"].ReadOnly = false;
                //dt.Columns["MUC_LUONG"].ReadOnly = false;
                //Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, true, true, true, this.Name);

                //Commons.Modules.ObjSystems.XoaTable(sTB);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void txtSoTien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SuaSoTien();
                Commons.Modules.ObjSystems.AddnewRow(grvData, false);
            }
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
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "ucDanhSachTamUng", "CapNhatTamUng", Commons.Modules.TypeLanguage);
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
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateChuotPhaiTAM_UNG", sBTCongNhan, sCotCN, Convert.ToDouble(grvData.GetFocusedRowCellValue(grvData.FocusedColumn.FieldName))));
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
    }
}