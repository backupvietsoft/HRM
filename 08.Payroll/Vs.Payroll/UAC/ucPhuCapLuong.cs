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
using System.Globalization;

namespace Vs.Payroll
{
    public partial class ucPhuCapLuong : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;
        private Int64 iIDPCL = -1;
        
        public static ucPhuCapLuong _instance;
        public static ucPhuCapLuong Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucPhuCapLuong();
                return _instance;
            }
        }
        public ucPhuCapLuong()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);
           
        }

        private void ucPhuCapLuong_Load(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";
            LoadThang();
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadPhuCapLuong();
            EnableButon(isAdd); 
            Commons.Modules.sPS = "";
        }

        private void LoadPhuCapLuong()
        {
            DataTable dt = new DataTable();
            try
            {
                
                if (isAdd)
                {

                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spPhuCapLuong", iIDPCL,Convert.ToDateTime(cboThang.EditValue), Commons.Modules.UserName, 
                        Commons.Modules.TypeLanguage, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, -1, 4, ""));
                    dt.Columns["MS_CN"].ReadOnly = true;
                    dt.Columns["HO_TEN"].ReadOnly = true;
                    dt.Columns["TEN_DV"].ReadOnly = true;
                    dt.Columns["TEN_XN"].ReadOnly = true;
                    dt.Columns["TEN_TO"].ReadOnly = true;
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, true, true, true, this.Name);
                    grvData.Columns[0].Visible = false;
                    
                }
                else
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spPhuCapLuong", iIDPCL, Convert.ToDateTime(cboThang.EditValue), Commons.Modules.UserName, 
                        Commons.Modules.TypeLanguage, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, -1, 1, ""));
                    dt.Columns["MS_CN"].ReadOnly = true;
                    dt.Columns["HO_TEN"].ReadOnly = true;
                    dt.Columns["TEN_DV"].ReadOnly = true;
                    dt.Columns["TEN_XN"].ReadOnly = true;
                    dt.Columns["TEN_TO"].ReadOnly = true;
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, true, true, true, this.Name);
                    grvData.Columns[0].Visible = false;

                }


            }
            catch
            {

            }
            grvData.Columns["ID_CN"].Visible = false;
            grvData.Columns["THANG"].Visible = false;
            grvData.Columns["TEN_DV"].Visible = false;
            grvData.Columns["TEN_XN"].Visible = false;
            grvData.Columns["TEN_TO"].Visible = false;
            grvData.Columns["COT_PC1"].DisplayFormat.FormatType = FormatType.Numeric;
            grvData.Columns["COT_PC1"].DisplayFormat.FormatString = "N0";
            grvData.Columns["COT_PC2"].DisplayFormat.FormatType = FormatType.Numeric;
            grvData.Columns["COT_PC2"].DisplayFormat.FormatString = "N0";
            grvData.Columns["COT_PC3"].DisplayFormat.FormatType = FormatType.Numeric;
            grvData.Columns["COT_PC3"].DisplayFormat.FormatString = "N0";
            grvData.Columns["COT_PC4"].DisplayFormat.FormatType = FormatType.Numeric;
            grvData.Columns["COT_PC4"].DisplayFormat.FormatString = "N0";
            grvData.Columns["COT_PC5"].DisplayFormat.FormatType = FormatType.Numeric;
            grvData.Columns["COT_PC5"].DisplayFormat.FormatString = "N0";
            grvData.Columns["COT_PC6"].DisplayFormat.FormatType = FormatType.Numeric;
            grvData.Columns["COT_PC6"].DisplayFormat.FormatString = "N0";
            grvData.Columns["COT_PC7"].DisplayFormat.FormatType = FormatType.Numeric;
            grvData.Columns["COT_PC7"].DisplayFormat.FormatString = "N0";
            grvData.Columns["COT_PC8"].DisplayFormat.FormatType = FormatType.Numeric;
            grvData.Columns["COT_PC8"].DisplayFormat.FormatString = "N0";
            grvData.Columns["COT_PC9"].DisplayFormat.FormatType = FormatType.Numeric;
            grvData.Columns["COT_PC9"].DisplayFormat.FormatString = "N0";
            grvData.Columns["COT_PC10"].DisplayFormat.FormatType = FormatType.Numeric;
            grvData.Columns["COT_PC10"].DisplayFormat.FormatString = "N0";
            lblTongCN.Text = Convert.ToString(grvData.RowCount);
        }

        public void LoadThang()
        {
            try
            {
                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.PHU_CAP_LUONG ORDER BY Y DESC , M DESC";
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

        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        isAdd = true;
                        LoadPhuCapLuong();
                        Commons.Modules.ObjSystems.AddnewRow(grvData,false);
                        EnableButon(isAdd);
                        break;
                        
                    }
                case "sua":
                    {
                        isAdd = false;
                        LoadPhuCapLuong();
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
                        LoadPhuCapLuong();
                        LoadThang();
                        EnableButon(isAdd);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        break;
                    }
                case "khongghi":
                    {
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        isAdd = false;
                        LoadPhuCapLuong();                        
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
                        //int Dot = Convert.ToInt32(grvData.GetFocusedRowCellValue("DOT"));
                        DateTime thang = Convert.ToDateTime(grvData.GetFocusedRowCellValue("THANG"));
                        int to = Convert.ToInt32(cboTo.EditValue);
                        //frmInDanhSachTamUng InDanhSachTamUng = new frmInDanhSachTamUng(Dot, thang, to);
                        //InDanhSachTamUng.ShowDialog();
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
            btnALL.Buttons[6].Properties.Visible = !visible;
            btnALL.Buttons[7].Properties.Visible = visible;
            btnALL.Buttons[8].Properties.Visible = visible;
            cboTo.Enabled = !visible;
            cboThang.Enabled = !visible;
            cboDonVi.Enabled = !visible;
            cboXiNghiep.Enabled = !visible;
            txtNhapNhanh.Enabled = visible;
        }

        private void XoaCheDoLV()
        {
            if (grvData.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                iIDPCL = Int64.Parse(grvData.GetFocusedRowCellValue("ID_PCL").ToString());
                SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spPhuCapLuong", iIDPCL, Convert.ToDateTime(cboThang.EditValue), Commons.Modules.UserName,
                      Commons.Modules.TypeLanguage, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, -1, 3, "");
                grvData.DeleteSelectedRows();
                lblTongCN.Text = Convert.ToString(grvData.RowCount);
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
            string sTB = "PCL_Tam" + Commons.Modules.UserName;
            try
            {
                
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spPhuCapLuong", iIDPCL,Convert.ToDateTime(cboThang.EditValue), Commons.Modules.UserName,
                       Commons.Modules.TypeLanguage, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, -1, 2, sTB);
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
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadPhuCapLuong();
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
            LoadPhuCapLuong();
            //EnableButon(true);
            Commons.Modules.sPS = "";
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadPhuCapLuong();
            //EnableButon(true);
            Commons.Modules.sPS = "";
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadPhuCapLuong();
            //EnableButon(true);
            Commons.Modules.sPS = "";
        }

        private void NhapNhanh()

        {
            string Nhap = grvData.FocusedColumn.FieldName.ToString();
            DataTable dt = new DataTable();
            dt = grvData.DataSource as DataTable;

            try
            {
                int i;
                i = 0;
                for (i = 0; i <= grvData.RowCount; i++)
                {
                    grvData.SetRowCellValue(i, Nhap, float.Parse(txtNhapNhanh.Text));
                    grvData.UpdateCurrentRow();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void txtNhapNhanh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NhapNhanh();
            }
        }

        private void grvData_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            string i = grvData.FocusedColumn.Name.ToString().Substring(3);
            switch (i)
            {
                case "COT_PC1":
                    txtNhapNhanh.ReadOnly = false;
                    break;
                case "COT_PC2":
                    txtNhapNhanh.ReadOnly = false;
                    break;
                case "COT_PC3":
                    txtNhapNhanh.ReadOnly = false;
                    break;
                case "COT_PC4":
                    txtNhapNhanh.ReadOnly = false;
                    break;
                case "COT_PC5":
                    txtNhapNhanh.ReadOnly = false;
                    break;
                case "COT_PC6":
                    txtNhapNhanh.ReadOnly = false;
                    break;
                case "COT_PC7":
                    txtNhapNhanh.ReadOnly = false;
                    break;
                case "COT_PC8":
                    txtNhapNhanh.ReadOnly = false;
                    break;
                case "COT_PC9":
                    txtNhapNhanh.ReadOnly = false;
                    break;
                case "COT_PC10":
                    txtNhapNhanh.ReadOnly = false;
                    break;
                default:
                    txtNhapNhanh.ReadOnly = true;
                    break;
            }
            
        }
    }
}