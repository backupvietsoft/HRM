using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

//using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using DevExpress.ClipboardSource.SpreadsheetML;
using System.Diagnostics;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;

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
            try
            {
                Commons.Modules.sLoad = "0Load";
                if (Commons.Modules.KyHieuDV == "DM")
                {
                    ItemForNgayCongQuyDinh.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                txtNgayCongQD.EditValue = Commons.Modules.KyHieuDV == "MT" ? 26 : 208;
                LoadThang();
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDV, dt, "ID_DV", "TEN_DV", "TEN_DV");
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
                Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
                LoadGrdDiemThang();
                Commons.Modules.sLoad = "";
                enableButon(true);
                Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            }
            catch { }
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdDiemThang();
            enableButon(true);
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
                case "In":
                    {

                        System.Data.SqlClient.SqlConnection conn;
                        DataTable dt = new DataTable();
                        try
                        {
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Commons.Modules.KyHieuDV == "DM" ? "rptTinhDiemThang_DM" : Commons.Modules.KyHieuDV == "NC" ? "rptTinhDiemThang_NC" : "rptTinhDiemThang_DM", conn);
                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = cboDV.EditValue;
                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = cboXN.EditValue;
                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = cboTo.EditValue;
                            cmd.Parameters.Add("@Thang", SqlDbType.Date).Value = Convert.ToDateTime(cboThang.EditValue).ToString("yyyy-MM-dd");
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            ds.Tables[0].TableName = "DiemChuyenCanThang";
                            SaveFileDialog saveFileDialog = new SaveFileDialog();
                            saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
                            saveFileDialog.FilterIndex = 0;
                            saveFileDialog.RestoreDirectory = true;
                            //saveFileDialog.CreatePrompt = true;
                            saveFileDialog.CheckFileExists = false;
                            saveFileDialog.CheckPathExists = false;
                            saveFileDialog.Title = "Export Excel File To";
                            saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                            DialogResult res = saveFileDialog.ShowDialog();
                            // If the file name is not an empty string open it for saving.
                            if (res == DialogResult.OK)
                            {
                                if (Commons.Modules.KyHieuDV == "DM")
                                {
                                    Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateDiemChuyenCanThang.xlsx", ds, new string[] { "{", "}" });
                                }
                                else if (Commons.Modules.KyHieuDV == "NC")
                                {
                                    Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateDiemChuyenCanThangNC.xlsx", ds, new string[] { "{", "}" });
                                }
                                else
                                {
                                    Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\Template\\TemplateDiemChuyenCanThang.xlsx", ds, new string[] { "{", "}" });
                                }

                                Process.Start(saveFileDialog.FileName);
                            }
                        }
                        catch
                        {

                        }


                        break;
                    }
                case "tinhdiemthang":
                    {
                        try
                        {
                            string sSP = "";
                            switch (Commons.Modules.KyHieuDV)
                            {
                                case "DM":
                                    {
                                        sSP = "spTinhThuongChuyenCanThang_DM";
                                        break;
                                    }
                                case "NB":
                                    {
                                        sSP = "spTinhDiemThang_NB";
                                        break;
                                    }
                                case "NC":
                                    {
                                        sSP = "spTinhDiemThang_NC";
                                        break;
                                    }
                                default:
                                    {
                                        sSP = "spTinhDiemThang";
                                        break;
                                    }
                            }
                            DataTable dt = new DataTable();
                            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, sSP, Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToInt32(cboDV.EditValue), Convert.ToInt32(cboXN.EditValue), Convert.ToInt32(cboTo.EditValue), Convert.ToInt32(txtNgayCongQD.EditValue), Convert.ToDateTime(cboThang.EditValue)));
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
                        if (Savedata() == true)
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
                        txtNgayCongQD.EditValue = 208;
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

        private void InExcel(DataTable dt)
        {

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
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, Commons.Modules.KyHieuDV == "DM" ? "spGetListDiemThang_DM" : Commons.Modules.KyHieuDV == "NB" ? "spGetListDiemThang_NB" : Commons.Modules.KyHieuDV == "NC" ? "spGetListDiemThang_NC" : "spGetListDiemThang", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToInt32(cboDV.EditValue), Convert.ToInt32(cboXN.EditValue), Convert.ToInt32(cboTo.EditValue), Convert.ToDateTime(cboThang.EditValue)));

                for (int i = 4; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ReadOnly = false;
                }
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                dt.Columns[2].ReadOnly = true;
                dt.Columns[3].ReadOnly = true;
                if (grdDiemThang.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDiemThang, grvDiemThang, dt, true, true, false, true, true, this.Name);
                    grvDiemThang.Columns["ID_CN"].Visible = false;
                    grvDiemThang.Columns["THANG"].Visible = false;
                    //grvDiemThang.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                    //grvDiemThang.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                    //grvDiemThang.Columns["TEN_TO"].OptionsColumn.AllowEdit = false;
                    //grvDiemThang.Columns["NGAY_VAO_CTY"].OptionsColumn.AllowEdit = false;
                    //grvDiemThang.Columns["NGAY_CONG"].OptionsColumn.AllowEdit = false;
                    //grvDiemThang.Columns["NGHI_VR"].OptionsColumn.AllowEdit = false;
                    //grvDiemThang.Columns["DT_VS"].OptionsColumn.AllowEdit = false;

                    foreach (GridColumn item in grvDiemThang.Columns)
                    {
                        if (item.FieldName != "TIEN_THUONG" && item.FieldName != "DIEM")
                        {
                            grvDiemThang.Columns[item.FieldName].OptionsColumn.AllowEdit = false;
                        }
                    }
                    if (Commons.Modules.KyHieuDV == "NB")
                    {
                        //nếu là nb
                        grvDiemThang.Columns["NGHI_P"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvDiemThang.Columns["NGHI_P"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;

                        grvDiemThang.Columns["TIEN_DT_VS"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvDiemThang.Columns["TIEN_DT_VS"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;

                        grvDiemThang.Columns["TIEN_GIO_NGHI"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvDiemThang.Columns["TIEN_GIO_NGHI"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;

                        grvDiemThang.Columns["TIEN_VI_PHAM"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvDiemThang.Columns["TIEN_VI_PHAM"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;

                        grvDiemThang.Columns["NGHI_VR"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvDiemThang.Columns["NGHI_VR"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;

                    }
                    if (Commons.Modules.KyHieuDV == "NC")
                    {
                        //nếu là nam co
                        grvDiemThang.Columns["TIEN_DT_VS"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvDiemThang.Columns["TIEN_DT_VS"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;

                        grvDiemThang.Columns["TIEN_GIO_NGHI"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvDiemThang.Columns["TIEN_GIO_NGHI"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;

                        grvDiemThang.Columns["TIEN_VI_PHAM"].DisplayFormat.FormatType = FormatType.Numeric;
                        grvDiemThang.Columns["TIEN_VI_PHAM"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                    }
                    Commons.Modules.ObjSystems.MFormatCol(grvDiemThang, "TIEN_THUONG", Commons.Modules.iSoLeTT);


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
            catch (Exception ex)
            {
            }
        }

        private bool Savedata()
        {
            string sBT = "sBTDiemThang" + Commons.Modules.UserName;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grvDiemThang), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, Commons.Modules.KyHieuDV == "DM" ? "sPsaveTinhDiemThang_DM" : Commons.Modules.KyHieuDV == "NB" ? "sPsaveTinhDiemThang_NB" : Commons.Modules.KyHieuDV == "NC" ? "sPsaveTinhDiemThang_NC" : "sPsaveTinhDiemThang", sBT, Convert.ToDateTime(cboThang.EditValue));
                Commons.Modules.ObjSystems.XoaTable(sBT);
                return true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBT);
                return false;
            }
        }
        private void enableButon(bool visible)
        {
            if (Commons.Modules.ObjSystems.DataTinhTrangBangLuong(Convert.ToInt32(cboDV.EditValue), Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text)) == 2)
            {
                windowsUIButton.Buttons[0].Properties.Visible = false;
                windowsUIButton.Buttons[1].Properties.Visible = false;
                windowsUIButton.Buttons[2].Properties.Visible = false;
                windowsUIButton.Buttons[3].Properties.Visible = false;
                windowsUIButton.Buttons[4].Properties.Visible = false;
                windowsUIButton.Buttons[5].Properties.Visible = false;
                windowsUIButton.Buttons[6].Properties.Visible = false;
            }
            else
            {
                windowsUIButton.Buttons[0].Properties.Visible = visible;
                windowsUIButton.Buttons[1].Properties.Visible = visible;
                windowsUIButton.Buttons[2].Properties.Visible = visible;
                windowsUIButton.Buttons[3].Properties.Visible = visible;
                windowsUIButton.Buttons[5].Properties.Visible = visible;
                windowsUIButton.Buttons[6].Properties.Visible = visible;
                windowsUIButton.Buttons[7].Properties.Visible = visible;

                windowsUIButton.Buttons[4].Properties.Visible = !visible;
                windowsUIButton.Buttons[5].Properties.Visible = !visible;
                grvDiemThang.OptionsBehavior.Editable = !visible;
                //searchControl.Visible = visible;
                cboThang.Properties.ReadOnly = !visible;
                cboDV.Properties.ReadOnly = !visible;
                cboXN.Properties.ReadOnly = !visible;
                cboTo.Properties.ReadOnly = !visible;
            }
        }
        #endregion
        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadGrdDiemThang();
            enableButon(true);
        }

        public void LoadThang()
        {
            try
            {
                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT DISTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.DIEM_THANG ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                if (grdThang.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                    grvThang.Columns["M"].Visible = false;
                    grvThang.Columns["Y"].Visible = false;
                    grvThang.Columns["NC_CHUAN"].Visible = false;
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

        private void grvDiemThang_RowCountChanged(object sender, EventArgs e)
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
    }
}
