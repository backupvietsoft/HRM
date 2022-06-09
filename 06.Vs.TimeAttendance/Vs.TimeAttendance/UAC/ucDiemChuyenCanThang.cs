using Commons;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

using Vs.Report;
//using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;


namespace Vs.TimeAttendance
{
    public partial class ucDiemChuyenCanThang : DevExpress.XtraEditors.XtraUserControl
    {
        public static ucDiemChuyenCanThang _instance;
        public static ucDiemChuyenCanThang Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDiemChuyenCanThang();
                return _instance;
            }
        }

        static string CharacterIncrement(int colCount)
        {
            int TempCount = 0;
            string returnCharCount = string.Empty;

            if (colCount <= 25)
            {
                TempCount = colCount;
                char CharCount = Convert.ToChar((Convert.ToInt32('A') + TempCount));
                returnCharCount += CharCount;
                return returnCharCount;
            }
            else
            {
                var rev = 0;

                while (colCount >= 26)
                {
                    colCount = colCount - 26;
                    rev++;
                }

                returnCharCount += CharacterIncrement(rev - 1);
                returnCharCount += CharacterIncrement(colCount);
                return returnCharCount;
            }
        }

       // private SqlConnection conn;

        public ucDiemChuyenCanThang()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(grvDiemThang, "Diem_thang");
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(grvThang, "Diem_thang");
        }
        private void ucPhepThang_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            txtNgayCongQD.EditValue = 18;
            LoadThang();
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdDiemThang();
            Commons.Modules.sLoad = "";
            enableButon(true);
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdDiemThang();
            Commons.Modules.sLoad = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdDiemThang();
            Commons.Modules.sLoad = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdDiemThang();
            Commons.Modules.sLoad = "";
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "tinhdiemthang":
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spTinhDiemThang", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToInt32(cboDV.EditValue), Convert.ToInt32(cboXN.EditValue), Convert.ToInt32(cboTo.EditValue), Convert.ToInt32(txtNgayCongQD.EditValue) ,Convert.ToDateTime(cboThang.EditValue)));
                            grdDiemThang.DataSource = dt;
                            enableButon(false);
                        }
                        catch { }
                        break;
                    }
                case "sua":
                    {
                        Commons.Modules.ObjSystems.AddnewRow(grvDiemThang, false);
                        enableButon(false);
                        break;
                    }
                case "ghi":
                    {
                        if (grvDiemThang.RowCount == 0)
                            return;
                        if(Savedata() == true)
                        {
                            LoadThang();
                            LoadGrdDiemThang();
                        }
                        enableButon(true);
                        break;
                    }
                
                case "khongghi":
                    {
                        LoadGrdDiemThang();
                        //Commons.Modules.ObjSystems.DeleteAddRow(grvPhepThang);
                        txtNgayCongQD.EditValue = 18;
                        enableButon(true);
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
            }
        }
        private void GrvPhepThang_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            throw new NotImplementedException();
        }
        #region hàm xử lý dữ liệu
        private void LoadGrdDiemThang()
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListDiemThang", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToInt32(cboDV.EditValue), Convert.ToInt32(cboXN.EditValue), Convert.ToInt32(cboTo.EditValue), Convert.ToDateTime(cboThang.EditValue)));

                for (int i = 4; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ReadOnly = false;
                }
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                dt.Columns[2].ReadOnly = true;
                dt.Columns[3].ReadOnly = true;
                if(grdDiemThang.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDiemThang, grvDiemThang, dt, true, true, false, true, true, this.Name);
                    grvDiemThang.Columns["ID_CN"].Visible = false;
                    grvDiemThang.Columns["THANG"].Visible = false;
                    grvDiemThang.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                    grvDiemThang.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                    grvDiemThang.Columns["TEN_TO"].OptionsColumn.AllowEdit = false;
                    grvDiemThang.Columns["NGAY_VAO_CTY"].OptionsColumn.AllowEdit = false;
                    grvDiemThang.Columns["NGAY_CONG"].OptionsColumn.AllowEdit = false;
                    grvDiemThang.Columns["NGHI_VR"].OptionsColumn.AllowEdit = false;
                    grvDiemThang.Columns["DT_VS"].OptionsColumn.AllowEdit = false;
                    grvDiemThang.Columns["SP_DT_VS"].OptionsColumn.AllowEdit = false;
                    grvDiemThang.Columns["VP_NQ"].OptionsColumn.AllowEdit = false;
                    grvDiemThang.Columns["NGHI_P"].OptionsColumn.AllowEdit = false;
                    grvDiemThang.Columns["NGHI_KP"].OptionsColumn.AllowEdit = false;
                    grvDiemThang.Columns["NGHI_VRKL"].OptionsColumn.AllowEdit = false;

                }
                else
                {
                    grdDiemThang.DataSource = dt;
                }
                
                ////visible tháng lớn hơn tháng đang chọn
                //for (int i = Convert.ToDateTime(cboThang.EditValue).Month + 1; i <= 12; i++)
                //{
                //    grvDiemThang.Columns["T_" + i + ""].Visible = false;
                //    grvDiemThang.Columns["TT_" + i + ""].Visible = false;
                //}
                Commons.Modules.sLoad = "";
            }
            catch (Exception)
            {
            }
        }

        private bool Savedata()
        {
            try
            {
                string sBT = "sBTDiemThang" + Commons.Modules.UserName;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grvDiemThang), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveTinhDiemThang", sBT, Convert.ToDateTime(cboThang.EditValue));
                Commons.Modules.ObjSystems.XoaTable(sBT);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = visible;
            windowsUIButton.Buttons[6].Properties.Visible = visible;

            windowsUIButton.Buttons[3].Properties.Visible = !visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            grvDiemThang.OptionsBehavior.Editable = !visible;
            //searchControl.Visible = visible;
            cboThang.Properties.ReadOnly = !visible;
            cboDV.Properties.ReadOnly = !visible;
            cboXN.Properties.ReadOnly = !visible;
            cboTo.Properties.ReadOnly = !visible;
        }
        #endregion
        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdDiemThang();
            Commons.Modules.sLoad = "";
        }
        public void LoadThang()
        {
            try
            {
                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.DIEM_THANG ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                if(grdThang.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                    grvThang.Columns["M"].Visible = false;
                    grvThang.Columns["Y"].Visible = false;
                }
                else
                {
                    grdThang.DataSource = dtthang;
                }
                

                cboThang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch 
            {
                DateTime now = DateTime.Now;

                cboThang.Text = now.ToString("MM/yyyy");
            }
        }
        private void calendarControl1_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboThang.Text = calThangc.DateTime.ToString("MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grvThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + cboThang.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                }
                else { }
            }
            catch (Exception ex)
            {
                cboThang.Text = calThangc.DateTime.ToString("MM/yyyy");
            }
            cboThang.ClosePopup();
        }

        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch { }
            cboThang.ClosePopup();

        }
    }
}
