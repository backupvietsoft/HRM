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
    public partial class ucMucBuLuong : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;
        
        public static ucMucBuLuong _instance;
        public static ucMucBuLuong Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucMucBuLuong();
                return _instance;
            }
        }

        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        public ucMucBuLuong()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);
            
        }

        private void ucMucBuLuong_Load(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";
            LoadThang();
            Commons.Modules.ObjSystems.LoadCboDonViKO(cboDonVi);
            LoadGrdMucBuLuong();
            LoadGrdMucBuLuong();
            EnableButon(isAdd); 
            Commons.Modules.sPS = "";
        }

        private void LoadGrdMucBuLuong()
        {
            DataTable dt = new DataTable();
            try
            {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListMucBuLuong", Convert.ToDateTime(cboNgay.EditValue),
                                                    cboDonVi.EditValue,  Commons.Modules.TypeLanguage));
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, false, true, true, this.Name);
                    dt.Columns["TT"].ReadOnly = false;
            }
            catch
            {
            }
            grvData.Columns["ID_MBL"].Visible = false;
            grvData.Columns["ID_DV"].Visible = false;
            grvData.Columns["THANG"].Visible = false;
            grvData.Columns["TT"].Visible = false;
            grvData.Columns["MBL_CT"].DisplayFormat.FormatType = FormatType.Numeric;
            grvData.Columns["MBL_CT"].DisplayFormat.FormatString = "N0";
            grvData.Columns["MBL_TV"].DisplayFormat.FormatType = FormatType.Numeric;
            grvData.Columns["MBL_TV"].DisplayFormat.FormatString = "N0";
            grvData.Columns["MBL_HV"].DisplayFormat.FormatType = FormatType.Numeric;
            grvData.Columns["MBL_HV"].DisplayFormat.FormatString = "N0";

        }

        //public void LoadDonVi()
        //{
        //    //try
        //    //{
        //    //    DataTable dtdv = new DataTable();
        //    //    dtdv.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_DV, TEN_DV FROM DON_VI  ORDER BY TEN_DV"));
        //    //    // Commons.Modules.ObjSystems.MLoadComboboxEdit(cboDonVi, dtdv, "TEN_DV");
        //    //    Commons.Modules.ObjSystems.MLoadLookUpEdit(cboDonVi, dtdv, "ID_DV", "TEN_DV", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_DV"), false);
        //    //    //cboDonVi.SelectedIndex = 0;
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //}
        //}

        public void LoadThang()
        {
            try
            {

                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.MUC_BU_LUONG_XN ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                grvThang.Columns["M"].Visible = false;
                grvThang.Columns["Y"].Visible = false;

                cboNgay.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch (Exception ex)
            {
                cboNgay.Text = DateTime.Now.Month + "/" + DateTime.Now.Year;
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
                        Commons.Modules.ObjSystems.AddnewRow(grvData, true);
                        isAdd = true;
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
                        LoadGrdMucBuLuong();
                        LoadThang();
                        isAdd = false;
                        EnableButon(isAdd);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        break;
                    }
                case "khongghi":
                    {
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        LoadGrdMucBuLuong();
                        isAdd = false;
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
            cboDonVi.Enabled = !visible;
            cboNgay.Enabled = !visible;
        }

        private void XoaCheDoLV()
        {
            if (grvData.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                string sSql = "DELETE dbo.MUC_BU_LUONG_XN WHERE ID_MBL = " + grvData.GetFocusedRowCellValue("ID_MBL");
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
            try
            {
                GridView view = sender as GridView;
                view.SetFocusedRowCellValue("ID_DV", cboDonVi.EditValue);
                view.SetFocusedRowCellValue("THANG", cboNgay.EditValue);
                view.SetFocusedRowCellValue("TT", true);
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
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
            string sTB = "MBL_TMP" + Commons.Modules.UserName;
            try
            {

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveMucBuLuong", sTB);
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
            
        }

        
        private void grvNgay_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboNgay.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch { }
            cboNgay.ClosePopup();
            
        }

        private void cboNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGrdMucBuLuong();
            //EnableButon(true);
            Commons.Modules.sPS = "";
        }

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboNgay.Text = calThang.DateTime.ToString("MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + cboNgay.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                }
                else { }
            }
            catch (Exception ex)
            {
                cboNgay.Text = calThang.DateTime.ToString("MM/yyyy");
            }
            cboNgay.ClosePopup();
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGrdMucBuLuong();
            //EnableButon(true);
            Commons.Modules.sPS = "";
        }
    }
}