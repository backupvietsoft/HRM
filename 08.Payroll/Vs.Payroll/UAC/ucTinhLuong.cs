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

namespace Vs.Payroll
{
    public partial class ucTinhLuong : DevExpress.XtraEditors.XtraUserControl
    {
        
        
        public static ucTinhLuong _instance;

        public static ucTinhLuong Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucTinhLuong();
                return _instance;
            }
        }
        
        public ucTinhLuong()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            Commons.Modules.sPS = "";

        }

        private void ucTinhLuong_Load(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";
            LoadThang();
            LoadGrdGTGC();
            Commons.Modules.sPS = "";
        }

        private void LoadGrdGTGC()
        {
            try
            {
                DataTable dt = new DataTable();
                DateTime Tngay = Convert.ToDateTime(cboThang.EditValue);
                DateTime Dngay = Convert.ToDateTime(cboThang.EditValue).AddMonths(1).AddDays(-1);
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetBangLuong", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue,Tngay,Dngay));
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, false, true, true, this.Name);
                    grvData.Columns["MS_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["HO_TEN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["TEN_TO"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["LUONG_CB"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_CB"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_KHOAN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_KHOAN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_CBQL"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_CBQL"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_TG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_TG"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_SP"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_SP"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_SP_KHAC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_SP_KHAC"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_CDPS"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_CDPS"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_TC_NT"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_TC_NT"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_TC_CN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_TC_CN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_TC_NL"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_TC_NL"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_TC_226"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_TC_226"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_TC_226_CN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_TC_226_CN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_TC_226_NL"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_TC_226_NL"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["LUONG_LAM_DEM"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["LUONG_LAM_DEM"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_PHEP"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_PHEP"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_LE"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_LE"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_VRCL"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_VRCL"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_CDLDN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_CDLDN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_CHUYEN_CAN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_CHUYEN_CAN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_THAM_NIEN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_THAM_NIEN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_DI_LAI"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_DI_LAI"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_CON_NHO"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_CON_NHO"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_NGUYET_SAN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_NGUYET_SAN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_CONG_KHAC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_CONG_KHAC"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["MUC_BU_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["MUC_BU_LUONG"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["BU_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["BU_LUONG"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TRICH_NOP_BHXH"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TRICH_NOP_BHXH"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TRICH_NOP_BHYT"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TRICH_NOP_BHYT"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TRICH_NOP_BHTN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TRICH_NOP_BHTN"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TRICH_NOP_PCD"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TRICH_NOP_PCD"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TAM_UNG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TAM_UNG"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_TRU_KHAC"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_TRU_KHAC"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_THUE"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_THUE"].DisplayFormat.FormatString = "N0";
                    grvData.Columns["TIEN_PHEP_TT"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN_PHEP_TT"].DisplayFormat.FormatString = "N0";
                }
                else
                {
                    grdData.DataSource = dt;
                }
            }
            catch
            {

            }

            grvData.Columns["ID_CN"].Visible = false;
            //for (int i = 6; i < grvData.Columns.Count; i++)
            //{

            //    grvData.Columns[i].DisplayFormat.FormatType = FormatType.Numeric;
            //    grvData.Columns[i].DisplayFormat.FormatString = "N0";
            //}
           
        }

        

        public void LoadThang()
        {
            try
            {

                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.BANG_LUONG ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang1, dtthang, false, true, true, true, true, this.Name);
                grvThang1.Columns["M"].Visible = false;
                grvThang1.Columns["Y"].Visible = false;

                cboThang.Text = grvThang1.GetFocusedRowCellValue("THANG").ToString();
            }
            catch (Exception ex)
            {
                DateTime now = DateTime.Now;
                
                cboThang.Text =  now.ToString("MM/yyyy");
            }
        }

       

        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "xoa":
                    {
                        XoaCheDoLV();
                        break;
                    }
                case "in":
                    {
                       
                        break;
                    }
               
                case "tinhluong":
                    {
                        if (grvData.RowCount != 0)
                        {
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_DaCoLuong"), Commons.Modules.msgTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                        }

                        grdData.DataSource = null;
                        DateTime Tngay = Convert.ToDateTime(cboThang.EditValue);
                        DateTime Dngay = Convert.ToDateTime(cboThang.EditValue).AddMonths(1).AddDays(-1);
                        DataTable dt = new DataTable();
                        SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetTinhLuongThang", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Convert.ToInt32(txtNgayCongLV.EditValue),  Convert.ToInt32(txtNgayCongChuan.EditValue), Tngay, Dngay);
                        LoadGrdGTGC();
                        
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
            //btnALL.Buttons[4].Properties.Visible = !visible;
            //btnALL.Buttons[5].Properties.Visible = !visible;
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
                SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "XoaTinhLuongThang", cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Convert.ToDateTime(cboThang.EditValue));
                grdData.DataSource= null;
            
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
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGrdGTGC();
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
            LoadGrdGTGC();
            //EnableButon(true);
            Commons.Modules.sPS = "";
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadGrdGTGC();
            //EnableButon(true);
            Commons.Modules.sPS = "";
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadGrdGTGC();
            //EnableButon(true);
            Commons.Modules.sPS = "";
        }

       
      

        private void grvData_RowCountChanged(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                int index = sumNV.Text.IndexOf(':');
                if (index > 0)
                {
                    if (view.RowCount > 0)
                    {
                        sumNV.Text = sumNV.Text.Substring(0, index) + ": " + view.RowCount.ToString();
                    }
                    else
                    {
                        sumNV.Text = sumNV.Text.Substring(0, index) + ": 0";
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