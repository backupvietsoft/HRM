using DevExpress.DataAccess.Excel;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using DevExpress.Utils.Menu;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace Vs.Payroll
{
    public partial class frmQTCN : DevExpress.XtraEditors.XtraUserControl
    {
        private bool bCheckCopy = false;
        private bool isAdd = false;
        private string ChuoiKT = "";
        string LOAI_HH = "";
        DataTable dtTempCopy;
        //int id_NHH = 0;
        //Decimal hsBT, tgTK, tgQD, dgG, hsDG;

        //string sCnstr = "Server=192.168.2.5;database=DATA_MT;uid=sa;pwd=123;Connect Timeout=0;"; 
        public frmQTCN()
        {
            InitializeComponent();
        }

        private void frmQTCN_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            try
            {
                datNgayLap.DateTime = DateTime.Now;
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDV, Commons.Modules.ObjSystems.DataDonVi(false), "ID_DV", "TEN_DV", "TEN_DV");
                Commons.OSystems.SetDateEditFormat(datTNgay);
                Commons.OSystems.SetDateEditFormat(datDNgay);
                datTNgay.EditValue = DateTime.Now.AddMonths(-4);
                datDNgay.EditValue = DateTime.Now;
                LoadCboDoiTac();
                LoadCboHangHoa();
                LoadCboTo();
                LoadCboCum();
                LoadData();
                rdoXemCuLapMoi.SelectedIndex = 1;
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message.ToString()); }

            Commons.Modules.sLoad = "";
            Commons.Modules.ObjSystems.ThayDoiNN(this, windowsUIButton);
        }

        private void LoadCboDoiTac()
        {
            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spQTCN", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@ID_DV", SqlDbType.BigInt).Value = cboDV.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboKH, dt, "ID_DT", "TEN_NGAN", "TEN_NGAN", true);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void LoadCboHangHoa()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                try
                {
                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spQTCN", conn);
                    cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@ID_DT", SqlDbType.BigInt).Value = cboKH.Text == "" ? -99 : cboKH.EditValue;
                    cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datTNgay.Text);
                    cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datDNgay.Text);
                    cmd.Parameters.Add("@bCot1", SqlDbType.Bit).Value = rdoXemCuLapMoi.SelectedIndex;
                    cmd.Parameters.Add("@ID_DV", SqlDbType.BigInt).Value = cboDV.EditValue;
                    cmd.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    dt = new DataTable();
                    dt = ds.Tables[1].Copy();
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboMH, dt, "ID_ORD", "TEN_HH", "TEN_HH", true);
                    if (dt.Rows.Count == 0)
                    {
                        cboMH_EditValueChanged(null, null);
                    }
                }
                catch (Exception ex)
                {
                }
            }
            catch { }
        }
        private void LoadCboTo()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spQTCN", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@ID_DV", SqlDbType.BigInt).Value = cboDV.EditValue;
                cmd.Parameters.Add("@bCot1", SqlDbType.Bit).Value = rdoXemCuLapMoi.SelectedIndex;
                cmd.Parameters.Add("@ID_MH", SqlDbType.BigInt).Value = cboMH.Text == "" ? -99 : cboMH.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[2].Copy();
                Commons.Modules.ObjSystems.MLoadCheckedComboBoxEdit(chkCboEditChuyen, dt, "ID_TO", "TEN_TO", "TEN_TO", true);
                chkCboEditChuyen.SetEditValue(dt.Rows[0]["ID_TO"]);
                //chkCboEditChuyen.EditValue
                //LoadCboCum(id_NHH);
            }
            catch (Exception ex) { }
        }

        private void LoadCboCum()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCUM", chkCboEditChuyen.EditValue.ToString(), cboMH.Text == "" ? -99 : Convert.ToInt64(cboMH.EditValue), 1));
                if (cboCum.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboCum, dt, "ID_NHOM", "NHOM_CD", "NHOM_CD");
                    cboCum.Properties.View.Columns[0].Caption = "ID cụm";
                    cboCum.Properties.View.Columns[1].Caption = "Tên cụm";
                    cboCum.Properties.View.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboCum.Properties.View.Columns[1].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    cboCum.Properties.View.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboCum.Properties.View.Columns[0].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                }
                else
                {
                    cboCum.Properties.DataSource = dt;
                }
            }
            catch (Exception ex) { }
        }

        private void LoadData()
        {
            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spQTCN", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@ID_TO", SqlDbType.NVarChar).Value = chkCboEditChuyen.EditValue.ToString();
                cmd.Parameters.Add("@ID_MH", SqlDbType.BigInt).Value = cboMH.Text == "" ? -99 : cboMH.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.Columns["ID_CD"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdQT, grvQT, dt, false, true, false, false, true, this.Name);
                //if (grdQT.DataSource == null)
                //{
                //    Commons.Modules.ObjSystems.MLoadXtraGrid(grdQT, grvQT, dt, false, true, false, false, true, this.Name);
                //}
                //else
                //{
                //    try { grdQT.DataSource = dt; } catch { }
                //}
                if (!isAdd)
                {
                    grvQT.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                }
                FormatGrid();
                SetButton(isAdd);
                LoadTextTongDonGia();
            }
            catch (Exception ex)
            {
            }

        }

        private void FormatGrid()
        {
            //An cot
            grvQT.Columns["ID_CD"].Visible = false;
            grvQT.Columns["ID_TO"].Visible = false;
            grvQT.Columns["ID_ORD"].Visible = false;
            grvQT.Columns["QUI_TRINH_HOAN_CHINH"].Visible = false;

            grvQT.Columns["THOI_GIAN_THIET_KE"].DisplayFormat.FormatType = FormatType.Numeric;
            grvQT.Columns["THOI_GIAN_THIET_KE"].DisplayFormat.FormatString = "N3";

            grvQT.Columns["BAC_THO_DM"].DisplayFormat.FormatType = FormatType.Numeric;
            grvQT.Columns["BAC_THO_DM"].DisplayFormat.FormatString = "N3";

            grvQT.Columns["DON_GIA_GIAY"].DisplayFormat.FormatType = FormatType.Numeric;
            grvQT.Columns["DON_GIA_GIAY"].DisplayFormat.FormatString = "N2";

            grvQT.Columns["DON_GIA_THUC_TE"].DisplayFormat.FormatType = FormatType.Numeric;
            grvQT.Columns["DON_GIA_THUC_TE"].DisplayFormat.FormatString = "N2";

            grvQT.Columns["DMLD"].DisplayFormat.FormatType = FormatType.Numeric;
            grvQT.Columns["DMLD"].DisplayFormat.FormatString = "N2";
        }


        private void cboKH_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboHangHoa();
            LoadCboTo();
            Commons.Modules.sLoad = "";
        }

        private void cboMH_EditValueChanged(object sender, EventArgs e)
        {
            //if (rdoXemCuLapMoi.SelectedIndex == 1) return;
            LoadCboTo();
            chkCboEditChuyen_EditValueChanged(null, null);
            //LoadCboCum(s);
            //LoadData();
        }
        private void LocData()
        {
            if (Commons.Modules.sLoad == "0Load") return;
            DataTable dtTmp = new DataTable();
            try
            {
                dtTmp = (DataTable)grdQT.DataSource;
                string sCum = "-1";
                string sDK = "";
                try { sCum = cboCum.EditValue.ToString(); } catch { }

                if (sCum != "-1") sDK = "NHOM_CD LIKE '" + sCum + "'";

                dtTmp.DefaultView.RowFilter = sDK;

                LoadTextTongDonGia();
            }
            catch { dtTmp.DefaultView.RowFilter = ""; }
        }


        private void cboCum_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LocData();
        }

        private void SetButton(bool isAdd)
        {
            windowsUIButton.Buttons[0].Properties.Visible = !isAdd;
            windowsUIButton.Buttons[1].Properties.Visible = !isAdd;
            windowsUIButton.Buttons[2].Properties.Visible = !isAdd;
            windowsUIButton.Buttons[4].Properties.Visible = true;
            windowsUIButton.Buttons[9].Properties.Visible = !isAdd;

            windowsUIButton.Buttons[3].Properties.Visible = isAdd;
            windowsUIButton.Buttons[5].Properties.Visible = isAdd;
            windowsUIButton.Buttons[6].Properties.Visible = isAdd;
            windowsUIButton.Buttons[7].Properties.Visible = isAdd;
            windowsUIButton.Buttons[8].Properties.Visible = isAdd;

            cboKH.Enabled = !isAdd;
            cboDV.Enabled = !isAdd;
            cboMH.Enabled = !isAdd;
            chkCboEditChuyen.Enabled = !isAdd;
            datNgayLap.Enabled = isAdd;
            datTNgay.Enabled = !isAdd;
            datDNgay.Enabled = !isAdd;
            cboCum.Enabled = !isAdd;
        }

        int ttCD, ttChuyen;


        private void Savedata()
        {
            grvQT.CloseEditor();
            grvQT.UpdateCurrentRow();
            string stbQT = "stbQT" + Commons.Modules.UserName;
            try
            {
                //tạo một datatable 
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbQT, Commons.Modules.ObjSystems.ConvertDatatable(grvQT), "");
                //Cap nhat qui trinh cong nghe

                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                try
                {
                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spQTCN", conn);
                    cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                    cmd.Parameters.Add("@NGAY_LAP", SqlDbType.DateTime).Value = Convert.ToDateTime(datNgayLap.Text);
                    cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = stbQT;
                    cmd.Parameters.Add("@ID_TO", SqlDbType.NVarChar).Value = chkCboEditChuyen.EditValue.ToString();
                    cmd.Parameters.Add("@ID_MH", SqlDbType.BigInt).Value = cboMH.EditValue;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message.ToString());
                }
                Commons.Modules.ObjSystems.XoaTable(stbQT);
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(stbQT);
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void grvQT_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvQT_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }


        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {


                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "in":
                        {
                            //String sTongTGTK = "";
                            //String sTongTGQD = "";
                            //String sTongDG = "";

                            //System.Data.SqlClient.SqlConnection conn;
                            //conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            //conn.Open();

                            //System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuiTrinhCongNgheChiTiet", conn);
                            //cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            //cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            //cmd.Parameters.Add("@ID_ORD", SqlDbType.Int).Value = cboOrd.EditValue;
                            //cmd.CommandType = CommandType.StoredProcedure;
                            //System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                            //DataSet ds = new DataSet();
                            //adp.Fill(ds);
                            //DataTable dtCty = new DataTable();
                            //DataTable dtTieuDe = new DataTable();
                            //DataTable dtChiTiet = new DataTable();
                            //DataTable dtDSMay = new DataTable();
                            //DataTable dtTongBC = new DataTable();

                            //dtCty = ds.Tables[0].Copy();
                            //dtTieuDe = ds.Tables[1].Copy();
                            //dtChiTiet = ds.Tables[2].Copy();
                            //dtDSMay = ds.Tables[3].Copy();
                            //dtTongBC = ds.Tables[4].Copy();

                            //Excel.Application oXL;
                            //Excel._Workbook oWB;
                            //Excel._Worksheet oSheet;

                            //oXL = new Excel.Application();
                            //oXL.Visible = true;

                            //oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                            //oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                            //string fontName = "Times New Roman";
                            //int fontSizeTieuDe = 16;
                            //int fontSizeNoiDung = 12;

                            //string lastColumn = string.Empty;
                            //lastColumn = "J";

                            //Excel.Range row1_CongTy = oSheet.get_Range("A1", lastColumn + "1");
                            //row1_CongTy.Merge();
                            //row1_CongTy.Font.Size = fontSizeNoiDung;
                            //row1_CongTy.Font.Name = fontName;
                            //row1_CongTy.Font.Bold = true;
                            //row1_CongTy.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            //row1_CongTy.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                            //row1_CongTy.Value2 = dtCty.Rows[0]["TEN_CTY"];

                            //Excel.Range row2_DiaChi = oSheet.get_Range("A2", lastColumn + "2");
                            //row2_DiaChi.Merge();
                            //row2_DiaChi.Font.Size = fontSizeNoiDung;
                            //row2_DiaChi.Font.Name = fontName;
                            //row2_DiaChi.Font.Bold = true;
                            //row2_DiaChi.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            //row2_DiaChi.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                            //row2_DiaChi.Value2 = dtCty.Rows[0]["DIA_CHI"];

                            //Excel.Range row3_TieuDe = oSheet.get_Range("A3", lastColumn + "3");
                            //row3_TieuDe.Merge();
                            //row3_TieuDe.Font.Size = fontSizeTieuDe;
                            //row3_TieuDe.Font.Name = fontName;
                            //row3_TieuDe.Font.Bold = true;
                            //row3_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            //row3_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                            //row3_TieuDe.RowHeight = 50;
                            //row3_TieuDe.Value2 = "QUI TRÌNH CÔNG NGHỆ";

                            //Excel.Range row4_TieuDe = oSheet.get_Range("B4", "B4");
                            //row4_TieuDe.Font.Size = fontSizeNoiDung;
                            //row4_TieuDe.Font.Name = fontName;
                            //row4_TieuDe.Font.Bold = true;
                            //row4_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            //row4_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                            //row4_TieuDe.Value2 = "Khách hàng : " + dtTieuDe.Rows[0]["TEN_KH"];

                            //Excel.Range row4H_TieuDe = oSheet.get_Range("H4", "H4");
                            //row4H_TieuDe.Font.Size = fontSizeNoiDung;
                            //row4H_TieuDe.Font.Name = fontName;
                            //row4H_TieuDe.Font.Bold = true;
                            //row4H_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            //row4H_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                            //row4H_TieuDe.Value2 = "Chuyền : " + dtTieuDe.Rows[0]["TEN_CHUYEN"];

                            //Excel.Range row5_TieuDe = oSheet.get_Range("B5", "B5");
                            //row5_TieuDe.Font.Size = fontSizeNoiDung;
                            //row5_TieuDe.Font.Name = fontName;
                            //row5_TieuDe.Font.Bold = true;
                            //row5_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            //row5_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                            //row5_TieuDe.Value2 = "Hợp đồng : " + dtTieuDe.Rows[0]["SO_DHB"];

                            //Excel.Range row5H_TieuDe = oSheet.get_Range("H5", "H5");
                            //row5H_TieuDe.Font.Size = fontSizeNoiDung;
                            //row5H_TieuDe.Font.Name = fontName;
                            //row5H_TieuDe.Font.Bold = true;
                            //row5H_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            //row5H_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                            //row5H_TieuDe.Value2 = "Loại hàng hóa : " + dtTieuDe.Rows[0]["TEN_NHH"];

                            //Excel.Range row6_TieuDe = oSheet.get_Range("B6", "B6");
                            //row6_TieuDe.Font.Size = fontSizeNoiDung;
                            //row6_TieuDe.Font.Name = fontName;
                            //row6_TieuDe.Font.Bold = true;
                            //row6_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            //row6_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                            //row6_TieuDe.Value2 = "Mã hàng : " + dtTieuDe.Rows[0]["TEN_HH"];

                            //Excel.Range row7_TieuDe = oSheet.get_Range("B7", "B7");
                            //row7_TieuDe.Font.Size = fontSizeNoiDung;
                            //row7_TieuDe.Font.Name = fontName;
                            //row7_TieuDe.Font.Bold = true;
                            //row7_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            //row7_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                            //row7_TieuDe.Value2 = "Order : " + dtTieuDe.Rows[0]["ORDER_NUMBER"];

                            //Excel.Range rowFormat_TieuDe = oSheet.get_Range("A9", "J9");
                            //rowFormat_TieuDe.Font.Size = fontSizeNoiDung;
                            //rowFormat_TieuDe.Font.Name = fontName;
                            //rowFormat_TieuDe.Font.Bold = true;
                            //rowFormat_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            //rowFormat_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                            //rowFormat_TieuDe.Interior.Color = Color.Yellow;
                            //BorderAround(oSheet.get_Range("A9", "J9"));

                            //Excel.Range row9A_TieuDe = oSheet.get_Range("A9", "A9");
                            //row9A_TieuDe.Value2 = "Mã QL";
                            //row9A_TieuDe.ColumnWidth = 8;

                            //Excel.Range row9B_TieuDe = oSheet.get_Range("B9", "B9");
                            //row9B_TieuDe.Value2 = "Bước công việc";
                            //row9B_TieuDe.ColumnWidth = 55;

                            //Excel.Range row9C_TieuDe = oSheet.get_Range("C9", "C9");
                            //row9C_TieuDe.Value2 = "Yêu cầu kỹ thuật";
                            //row9C_TieuDe.ColumnWidth = 15;

                            //Excel.Range row9D_TieuDe = oSheet.get_Range("D9", "D9");
                            //row9D_TieuDe.Value2 = "Bậc thợ";
                            //row9D_TieuDe.ColumnWidth = 15;

                            //Excel.Range row9E_TieuDe = oSheet.get_Range("E9", "E9");
                            //row9E_TieuDe.Value2 = "TGTK";
                            //row9E_TieuDe.ColumnWidth = 10;

                            //Excel.Range row9F_TieuDe = oSheet.get_Range("F9", "F9");
                            //row9F_TieuDe.Value2 = "TGQD";
                            //row9F_TieuDe.ColumnWidth = 10;

                            //Excel.Range row9G_TieuDe = oSheet.get_Range("G9", "G9");
                            //row9G_TieuDe.Value2 = "DMSL";
                            //row9G_TieuDe.ColumnWidth = 12;

                            //Excel.Range row9H_TieuDe = oSheet.get_Range("H9", "H9");
                            //row9H_TieuDe.Value2 = "Lao động";
                            //row9H_TieuDe.ColumnWidth = 12;

                            //Excel.Range row9I_TieuDe = oSheet.get_Range("I9", "I9");
                            //row9I_TieuDe.Value2 = "Thiết bị";
                            //row9I_TieuDe.ColumnWidth = 12;

                            //Excel.Range row9J_TieuDe = oSheet.get_Range("J9", "J9");
                            //row9J_TieuDe.Value2 = "Đơn giá";
                            //row9J_TieuDe.ColumnWidth = 12;

                            //DataRow[] dr = dtChiTiet.Select();
                            ////string[,] rowData = new string[dr.Length, dtChiTiet.Columns.Count];
                            //int idCum = 0;
                            //int rowCnt = 10;
                            //int vtbd = 0;
                            //foreach (DataRow row in dr)
                            //{
                            //	if (Convert.ToInt32(row["ID_CUM"].ToString()) != idCum)
                            //	{
                            //		if (idCum != 0)
                            //		{

                            //			Excel.Range rowTong1 = oSheet.get_Range("D" + rowCnt, "D" + rowCnt);
                            //			rowTong1.Value2 = "Tổng";
                            //			rowTong1 = oSheet.get_Range("E" + rowCnt, "E" + rowCnt);
                            //			rowTong1.Value2 = "=SUM(E" + vtbd.ToString() + ":E" + (rowCnt - 1).ToString() + ")";
                            //			rowTong1 = oSheet.get_Range("F" + rowCnt, "F" + rowCnt);
                            //			rowTong1.Value2 = "=SUM(F" + vtbd.ToString() + ":F" + (rowCnt - 1).ToString() + ")";
                            //			rowTong1 = oSheet.get_Range("J" + rowCnt, "J" + rowCnt);
                            //			rowTong1.Value2 = "=SUM(J" + vtbd.ToString() + ":J" + (rowCnt - 1).ToString() + ")";

                            //			if (sTongTGTK == "")
                            //			{
                            //				sTongTGTK = "= E" + rowCnt;
                            //				sTongTGQD = "= F" + rowCnt;
                            //				sTongDG = "= J" + rowCnt;
                            //			}
                            //			else
                            //			{
                            //				sTongTGTK = sTongTGTK + " + E" + rowCnt;
                            //				sTongTGQD = sTongTGQD + " + F" + rowCnt;
                            //				sTongDG = sTongDG + " + J" + rowCnt;
                            //			}

                            //			rowTong1 = oSheet.get_Range("A" + vtbd, "A" + rowCnt);
                            //			rowTong1.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            //			rowTong1 = oSheet.get_Range("E" + vtbd, "H" + rowCnt);
                            //			rowTong1.Cells.NumberFormat = "#,##0.00";
                            //			rowTong1 = oSheet.get_Range("J" + vtbd, "J" + rowCnt);
                            //			rowTong1.Cells.NumberFormat = "#,##0.00";
                            //			rowTong1 = oSheet.get_Range("A" + rowCnt, "J" + rowCnt);
                            //			rowTong1.Font.Bold = true;
                            //			rowTong1.Font.Color = Color.Red;

                            //			Excel.Range rowFormat2 = oSheet.get_Range("A" + vtbd, "J" + rowCnt);
                            //			rowFormat2.Font.Size = fontSizeNoiDung;
                            //			rowFormat2.Font.Name = fontName;
                            //			rowFormat2.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                            //			BorderAround(oSheet.get_Range("A" + vtbd, "J" + rowCnt));

                            //			//rowFormat1.Font.Bold = true;
                            //			//rowFormat1.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            //			//rowFormat1.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                            //			rowCnt++;
                            //		}
                            //		Excel.Range rowCum = oSheet.get_Range("B" + rowCnt, "B" + rowCnt);
                            //		rowCum.Value2 = row["TEN_CUM"].ToString();
                            //		rowCum.Font.Size = fontSizeNoiDung;
                            //		rowCum.Font.Name = fontName;
                            //		rowCum.Font.Bold = true;
                            //		rowCum.Font.Color = Color.Red;
                            //		BorderAround(oSheet.get_Range("A" + rowCnt, "J" + rowCnt));

                            //		idCum = Convert.ToInt32(row["ID_CUM"].ToString());
                            //		rowCnt++;
                            //		vtbd = rowCnt;
                            //	}
                            //	Excel.Range rowCT = oSheet.get_Range("A" + rowCnt, "A" + rowCnt);
                            //	rowCT.Value2 = row["MaQL"].ToString();
                            //	rowCT = oSheet.get_Range("B" + rowCnt, "B" + rowCnt);
                            //	rowCT.Value2 = row["TEN_CD"].ToString();
                            //	rowCT = oSheet.get_Range("C" + rowCnt, "C" + rowCnt);
                            //	rowCT.Value2 = row["YEU_CAU_KT"].ToString();
                            //	rowCT = oSheet.get_Range("D" + rowCnt, "D" + rowCnt);
                            //	rowCT.Value2 = row["TEN_BAC_THO"].ToString();
                            //	rowCT = oSheet.get_Range("E" + rowCnt, "E" + rowCnt);
                            //	rowCT.Value2 = row["THOI_GIAN_THIET_KE"].ToString();
                            //	rowCT = oSheet.get_Range("F" + rowCnt, "F" + rowCnt);
                            //	rowCT.Value2 = row["THOI_GIAN_QUI_DOI"].ToString();
                            //	rowCT = oSheet.get_Range("G" + rowCnt, "G" + rowCnt);
                            //	rowCT.Value2 = row["DMSL"].ToString();
                            //	rowCT = oSheet.get_Range("H" + rowCnt, "H" + rowCnt);
                            //	rowCT.Value2 = row["LD"].ToString();
                            //	rowCT = oSheet.get_Range("I" + rowCnt, "I" + rowCnt);
                            //	rowCT.Value2 = row["TEN_LOAI_MAY"].ToString();
                            //	rowCT = oSheet.get_Range("J" + rowCnt, "J" + rowCnt);
                            //	rowCT.Value2 = row["DON_GIA_THUC_TE"].ToString();

                            //	rowCnt++;
                            //}

                            //Excel.Range rowTong = oSheet.get_Range("D" + rowCnt, "D" + rowCnt);
                            //rowTong.Value2 = "Tổng";
                            //rowTong = oSheet.get_Range("E" + rowCnt, "E" + rowCnt);
                            //rowTong.Value2 = "=SUM(E" + vtbd.ToString() + ":E" + (rowCnt - 1).ToString() + ")";
                            //rowTong = oSheet.get_Range("F" + rowCnt, "F" + rowCnt);
                            //rowTong.Value2 = "=SUM(F" + vtbd.ToString() + ":F" + (rowCnt - 1).ToString() + ")";
                            //rowTong = oSheet.get_Range("J" + rowCnt, "J" + rowCnt);
                            //rowTong.Value2 = "=SUM(J" + vtbd.ToString() + ":J" + (rowCnt - 1).ToString() + ")";

                            //if (sTongTGTK == "")
                            //{
                            //	sTongTGTK = "= E" + rowCnt;
                            //	sTongTGQD = "= F" + rowCnt;
                            //	sTongDG = "= J" + rowCnt;
                            //}
                            //else
                            //{
                            //	sTongTGTK = sTongTGTK + " + E" + rowCnt;
                            //	sTongTGQD = sTongTGQD + " + F" + rowCnt;
                            //	sTongDG = sTongDG + " + J" + rowCnt;
                            //}

                            //rowTong = oSheet.get_Range("A" + vtbd, "A" + rowCnt);
                            //rowTong.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            //rowTong = oSheet.get_Range("E" + vtbd, "H" + rowCnt);
                            //rowTong.Cells.NumberFormat = "#,##0.00";
                            //rowTong = oSheet.get_Range("J" + vtbd, "J" + rowCnt);
                            //rowTong.Cells.NumberFormat = "#,##0.00";
                            //rowTong = oSheet.get_Range("A" + rowCnt, "J" + rowCnt);
                            //rowTong.Font.Bold = true;
                            //rowTong.Font.Color = Color.Red;

                            //Excel.Range rowFormat1 = oSheet.get_Range("A" + vtbd, "J" + rowCnt);
                            //rowFormat1.Font.Size = fontSizeNoiDung;
                            //rowFormat1.Font.Name = fontName;
                            //rowFormat1.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                            //BorderAround(oSheet.get_Range("A" + vtbd, "J" + rowCnt));

                            //rowCnt++;

                            //Excel.Range rowTongCong = oSheet.get_Range("D" + rowCnt, "D" + rowCnt);
                            //rowTongCong.Value2 = "Tổng cộng";
                            //rowTongCong = oSheet.get_Range("E" + rowCnt, "E" + rowCnt);
                            //rowTongCong.Value2 = sTongTGTK;
                            //rowTongCong.Cells.NumberFormat = "#,##0.00";
                            //rowTongCong = oSheet.get_Range("F" + rowCnt, "F" + rowCnt);
                            //rowTongCong.Value2 = sTongTGQD;
                            //rowTongCong.Cells.NumberFormat = "#,##0.00";
                            //rowTongCong = oSheet.get_Range("J" + rowCnt, "J" + rowCnt);
                            //rowTongCong.Value2 = sTongDG;
                            //rowTongCong.Cells.NumberFormat = "#,##0.00";

                            //rowTongCong = oSheet.get_Range("A" + rowCnt, "J" + rowCnt);
                            //rowTongCong.Font.Size = fontSizeNoiDung;
                            //rowTongCong.Font.Name = fontName;
                            //rowTongCong.Font.Bold = true;
                            //rowTongCong.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                            //rowTongCong.Interior.Color = Color.Yellow;

                            //BorderAround(oSheet.get_Range("A" + rowCnt, "J" + rowCnt));

                            //rowCnt++;
                            //rowCnt++;

                            //int iTongHop = rowCnt;
                            //Excel.Range rowTongHop = oSheet.get_Range("B" + iTongHop, "B" + iTongHop);
                            //rowTongHop.Value2 = "TG làm việc/Ngày";
                            //rowTongHop = oSheet.get_Range("C" + iTongHop, "C" + iTongHop);
                            //rowTongHop.Value2 = dtTongBC.Rows[0]["TGLV"];
                            //rowTongHop.NumberFormat = "#,##0";
                            //rowTongHop = oSheet.get_Range("D" + iTongHop, "D" + iTongHop);
                            //rowTongHop.Value2 = "Giây";

                            //iTongHop++;
                            //rowTongHop = oSheet.get_Range("B" + iTongHop, "B" + iTongHop);
                            //rowTongHop.Value2 = "Tổng thời gian may 1 sản phẩm";
                            //rowTongHop = oSheet.get_Range("C" + iTongHop, "C" + iTongHop);
                            //rowTongHop.Value2 = dtTongBC.Rows[0]["TongTGSP"];
                            //rowTongHop.NumberFormat = "#,##0.00";
                            //rowTongHop = oSheet.get_Range("D" + iTongHop, "D" + iTongHop);
                            //rowTongHop.Value2 = "Giây";

                            //iTongHop++;
                            //rowTongHop = oSheet.get_Range("B" + iTongHop, "B" + iTongHop);
                            //rowTongHop.Value2 = "Năng suất lao động bình quân đầu người";
                            //rowTongHop = oSheet.get_Range("C" + iTongHop, "C" + iTongHop);
                            //rowTongHop.Value2 = dtTongBC.Rows[0]["NSLDCN"];
                            //rowTongHop.NumberFormat = "#,##0.00";
                            //rowTongHop = oSheet.get_Range("D" + iTongHop, "D" + iTongHop);
                            //rowTongHop.Value2 = "sp/lđ";

                            //iTongHop++;
                            //rowTongHop = oSheet.get_Range("B" + iTongHop, "B" + iTongHop);
                            //rowTongHop.Value2 = "Số lao động trong tổ";
                            //rowTongHop = oSheet.get_Range("C" + iTongHop, "C" + iTongHop);
                            //rowTongHop.Value2 = dtTongBC.Rows[0]["SLCN"];
                            //rowTongHop = oSheet.get_Range("D" + iTongHop, "D" + iTongHop);
                            //rowTongHop.Value2 = "Người";

                            //iTongHop++;
                            //rowTongHop = oSheet.get_Range("B" + iTongHop, "B" + iTongHop);
                            //rowTongHop.Value2 = "Năng suất lao động tổ";
                            //rowTongHop = oSheet.get_Range("C" + iTongHop, "C" + iTongHop);
                            //rowTongHop.Value2 = dtTongBC.Rows[0]["NSLDTO"];
                            //rowTongHop.NumberFormat = "#,##0.00";
                            //rowTongHop = oSheet.get_Range("D" + iTongHop, "D" + iTongHop);
                            //rowTongHop.Value2 = "sp/tổ";

                            //iTongHop++;
                            //rowTongHop = oSheet.get_Range("B" + iTongHop, "B" + iTongHop);
                            //rowTongHop.Value2 = "Cường độ lao động";
                            //rowTongHop = oSheet.get_Range("C" + iTongHop, "C" + iTongHop);
                            //rowTongHop.Value2 = dtTongBC.Rows[0]["CDLD"];
                            //rowTongHop.NumberFormat = "#,##0.00";
                            //rowTongHop = oSheet.get_Range("D" + iTongHop, "D" + iTongHop);
                            //rowTongHop.Value2 = "Giây";

                            //iTongHop++;
                            //rowTongHop = oSheet.get_Range("B" + iTongHop, "B" + iTongHop);
                            //rowTongHop.Value2 = "Tổng thành tiền";
                            //rowTongHop = oSheet.get_Range("C" + iTongHop, "C" + iTongHop);
                            //rowTongHop.Value2 = dtTongBC.Rows[0]["TongTT"];
                            //rowTongHop.NumberFormat = "#,##0.00";
                            //rowTongHop = oSheet.get_Range("D" + iTongHop, "D" + iTongHop);
                            //rowTongHop.Value2 = "Đồng";

                            //Excel.Range rowTongHop_Format = oSheet.get_Range("B" + rowCnt, "D" + iTongHop);
                            //rowTongHop_Format.Font.Size = fontSizeNoiDung;
                            //rowTongHop_Format.Font.Name = fontName;
                            //rowTongHop_Format.Font.Bold = true;
                            //rowTongHop_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                            //Excel.Range rowMay_TieuDe1 = oSheet.get_Range("G" + rowCnt, "G" + rowCnt);
                            //rowMay_TieuDe1.Value2 = "Thiết bị";

                            //Excel.Range rowMay_TieuDe2 = oSheet.get_Range("H" + rowCnt, "H" + rowCnt);
                            //rowMay_TieuDe2.Value2 = "SL";

                            //Excel.Range rowMay_TieuDe3 = oSheet.get_Range("I" + rowCnt, "I" + rowCnt);
                            //rowMay_TieuDe3.Value2 = "DVT";

                            //Excel.Range rowMay_TieuDe4 = oSheet.get_Range("J" + rowCnt, "J" + rowCnt);
                            //rowMay_TieuDe4.Value2 = "Thành tiền";

                            //Excel.Range rowMay_TieuDe_Format = oSheet.get_Range("G" + rowCnt, "J" + rowCnt);
                            //rowMay_TieuDe_Format.Font.Size = fontSizeNoiDung;
                            //rowMay_TieuDe_Format.Font.Name = fontName;
                            //rowMay_TieuDe_Format.Font.Bold = true;
                            //rowMay_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                            //rowMay_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                            //rowMay_TieuDe_Format.Interior.Color = Color.Yellow;

                            //BorderAround(oSheet.get_Range("G" + rowCnt, "J" + rowCnt));

                            //rowCnt++;
                            //vtbd = rowCnt;
                            //DataRow[] drM = dtDSMay.Select();
                            //foreach (DataRow row in drM)
                            //{
                            //	Excel.Range rowMCT = oSheet.get_Range("G" + rowCnt, "G" + rowCnt);
                            //	rowMCT.Value2 = row["TEN_LOAI_MAY"].ToString();
                            //	rowMCT = oSheet.get_Range("H" + rowCnt, "H" + rowCnt);
                            //	rowMCT.Value2 = row["TLD"].ToString();
                            //	rowMCT = oSheet.get_Range("I" + rowCnt, "I" + rowCnt);
                            //	rowMCT.Value2 = row["DVT"].ToString();
                            //	rowMCT = oSheet.get_Range("J" + rowCnt, "J" + rowCnt);
                            //	rowMCT.Value2 = row["TDG"].ToString();

                            //	rowCnt++;
                            //}

                            //rowCnt--;
                            //BorderAround(oSheet.get_Range("G" + vtbd, "J" + rowCnt));
                            //Excel.Range rowMay_ChiTiet_Format = oSheet.get_Range("G" + vtbd, "J" + rowCnt);
                            //rowMay_ChiTiet_Format.Font.Size = fontSizeNoiDung;
                            //rowMay_ChiTiet_Format.Font.Name = fontName;
                            //rowMay_ChiTiet_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                            //rowMay_ChiTiet_Format = oSheet.get_Range("H" + vtbd, "H" + rowCnt);
                            //rowMay_ChiTiet_Format.Cells.NumberFormat = "#,##0.00";
                            //rowMay_ChiTiet_Format = oSheet.get_Range("J" + vtbd, "J" + rowCnt);
                            //rowMay_ChiTiet_Format.Cells.NumberFormat = "#,##0.00";

                            break;
                        }
                    case "export":
                        {
                            try
                            {

                                DataSet ds = new DataSet();
                                SaveFileDialog saveFileDialog = new SaveFileDialog();
                                saveFileDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
                                saveFileDialog.FilterIndex = 0;
                                saveFileDialog.RestoreDirectory = true;
                                //saveFileDialog.CreatePrompt = true;
                                saveFileDialog.CheckFileExists = false;
                                saveFileDialog.CheckPathExists = false;
                                saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                                saveFileDialog.Title = "Export Excel File To";
                                DialogResult res = saveFileDialog.ShowDialog();
                                // If the file name is not an empty string open it for saving.
                                if (res == DialogResult.OK)
                                {
                                    Commons.TemplateExcel.FillReport(saveFileDialog.FileName, System.Windows.Forms.Application.StartupPath + "\\Template\\Teamplate_QTCN.xlsx", ds, new string[] { "{", "}" });
                                    Process.Start(saveFileDialog.FileName);
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                            break;
                        }
                    case "import":
                        {
                            string sPath = "";
                            sPath = Commons.Modules.ObjSystems.OpenFiles("All Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|" + "All Files (*.*)|*.*");
                            string sBTQTCN = "sBTQTCN" + Commons.Modules.iIDUser;
                            string sBTQTCN_Current = "sBTQTCN_Current" + Commons.Modules.iIDUser;
                            if (sPath == "") return;
                            try
                            {
                                //Lấy đường dẫn
                                var source = new ExcelDataSource();
                                source.FileName = sPath;

                                //Lấy worksheet
                                DevExpress.Spreadsheet.Workbook workbook = new DevExpress.Spreadsheet.Workbook();
                                string ext = System.IO.Path.GetExtension(sPath);
                                if (ext.ToLower() == ".xlsx")
                                    workbook.LoadDocument(sPath, DevExpress.Spreadsheet.DocumentFormat.Xlsx);
                                else
                                    workbook.LoadDocument(sPath, DevExpress.Spreadsheet.DocumentFormat.Xls);
                                List<string> wSheet = new List<string>();
                                for (int i = 0; i < workbook.Worksheets.Count; i++)
                                {
                                    wSheet.Add(workbook.Worksheets[i].Name.ToString());
                                }
                                //Load worksheet
                                XtraInputBoxArgs args = new XtraInputBoxArgs();
                                // set required Input Box options
                                args.Caption = "Chọn sheet cần nhập dữ liệu";
                                args.Prompt = "Chọn sheet cần nhập dữ liệu";
                                args.DefaultButtonIndex = 0;

                                // initialize a DateEdit editor with custom settings
                                ComboBoxEdit editor = new ComboBoxEdit();
                                editor.Properties.Items.AddRange(wSheet);
                                editor.EditValue = wSheet[0].ToString();

                                args.Editor = editor;
                                // a default DateEdit value
                                args.DefaultResponse = wSheet[0].ToString();
                                // display an Input Box with the custom editor
                                var result = XtraInputBox.Show(args);
                                if (result == null || result.ToString() == "") return;

                                var worksheetSettings = new ExcelWorksheetSettings(result.ToString());
                                source.SourceOptions = new ExcelSourceOptions(worksheetSettings);
                                source.Fill();
                                this.Cursor = Cursors.WaitCursor;
                                DataTable dt = new DataTable();
                                dt = new DataTable();
                                dt = ToDataTable(source);
                                if (dt == null || dt.Rows.Count <= 0)
                                {
                                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "KhongCoDuLieuImport"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"),
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTQTCN, dt, "");
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTQTCN_Current, Commons.Modules.ObjSystems.ConvertDatatable(grvQT), "");
                                dt = new DataTable();
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListQTCNImport", chkCboEditChuyen.EditValue.ToString().IndexOf(',') != -1 ? chkCboEditChuyen.EditValue.ToString().Substring(0, chkCboEditChuyen.EditValue.ToString().IndexOf(',')) : chkCboEditChuyen.EditValue.ToString(), Convert.ToInt64(cboMH.EditValue), sBTQTCN, sBTQTCN_Current));
                                //grdQT.DataSource = dt;
                                Commons.Modules.ObjSystems.MLoadXtraGrid(grdQT, grvQT, dt, true, true, false, false, true, this.Name);
                                grvQT.Columns["TINH_TRANG_CD"].Visible = false;
                                Commons.Modules.ObjSystems.XoaTable(sBTQTCN);
                                Commons.Modules.ObjSystems.XoaTable(sBTQTCN_Current);

                                grvQT.PostEditor();
                                grvQT.UpdateCurrentRow();
                                DataTable dtSource = new DataTable();
                                dtSource = (DataTable)grdQT.DataSource;
                                grvQT.Columns.View.ClearColumnErrors();
                                KiemTraLuoi(dtSource);
                                this.Cursor = Cursors.Default;
                            }
                            catch (Exception ex)
                            {
                                Commons.Modules.ObjSystems.XoaTable(sBTQTCN);
                                Commons.Modules.ObjSystems.XoaTable(sBTQTCN_Current);
                            }
                            break;
                        }
                    case "xoa":
                        {


                            DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgDeleteDangKyLamThem"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgfrmThongBao"), MessageBoxButtons.YesNoCancel);
                            if (res == DialogResult.Yes)
                            {
                                string sSql = "";
                                try
                                {
                                    if (grvQT.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
                                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.PHIEU_CONG_DOAN WHERE ID_CD = " + grvQT.GetFocusedRowCellValue("ID_CD") + "")) > 0)
                                    {
                                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                        return;
                                    }
                                    sSql = "DELETE QUI_TRINH_CONG_NGHE_CHI_TIET WHERE ID_TO = " + grvQT.GetFocusedRowCellValue("ID_TO") +
                                                                            " AND ID_ORD = " + grvQT.GetFocusedRowCellValue("ID_ORD") + "";
                                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                                    LoadData();
                                }
                                catch
                                {
                                    Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
                                }
                            }
                            else if (res == DialogResult.No)
                            {
                                string sSql = "";
                                try
                                {
                                    if (grvQT.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
                                    sSql = "DELETE QUI_TRINH_CONG_NGHE_CHI_TIET WHERE ID_TO = " + grvQT.GetFocusedRowCellValue("ID_TO") +
                                                                            " AND ID_ORD = " + grvQT.GetFocusedRowCellValue("ID_ORD") +
                                                                            " AND ID = '" + grvQT.GetFocusedRowCellValue("ID_CD") + "'";
                                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                                    grvQT.DeleteSelectedRows();
                                }
                                catch
                                {
                                    Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
                                }
                            }
                            else
                            {
                                return;
                            }


                            break;
                        }
                    case "sua":
                        {
                            if (cboDV.Text == "")
                            {
                                Commons.Modules.ObjSystems.msgChung("@ChuaNhapHopDong@");
                                return;
                            }
                            if (cboMH.Text == "")
                            {
                                Commons.Modules.ObjSystems.msgChung("@ChuaNhapMaHang@");
                                return;
                            }

                            if (chkCboEditChuyen.Text == "")
                            {
                                Commons.Modules.ObjSystems.msgChung("@ChuaNhapSttChuyen@");
                                return;
                            }

                            isAdd = true;
                            SetButton(isAdd);
                            grvQT.OptionsBehavior.Editable = true;
                            Commons.Modules.ObjSystems.AddnewRow(grvQT, true);

                            break;
                        }
                    case "danhlaiMQL":
                        {
                            try
                            {
                                DataTable dt = new DataTable();
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_DanhLaiMaQL"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo) == DialogResult.No)
                                {
                                    return;
                                }
                                dt = (DataTable)grdQT.DataSource;
                                if (dt.Rows.Count == 0)
                                {
                                    return;
                                }
                                else
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        dt.Rows[i]["MaQL"] = dt.Rows[i]["THU_TU_CONG_DOAN"];
                                    }
                                }
                            }
                            catch { }
                            break;
                        }
                    case "luu":
                        {
                            if (datNgayLap.Text == "")
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaNhapNgay"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            isAdd = false;
                            grvQT.PostEditor();
                            grvQT.UpdateCurrentRow();
                            DataTable dtSource = new DataTable();
                            dtSource = (DataTable)grdQT.DataSource;
                            grvQT.Columns.View.ClearColumnErrors();
                            this.Cursor = Cursors.WaitCursor;
                            if (!KiemTraLuoi(dtSource))
                            {
                                this.Cursor = Cursors.Default;
                                return;
                            }
                            this.Cursor = Cursors.Default;
                            SetButton(isAdd);
                            Validate();
                            if (grvQT.HasColumnErrors) return;
                            Savedata();
                            Commons.Modules.ObjSystems.DeleteAddRow(grvQT);
                            LoadData();
                            LocData();
                            break;
                        }
                    case "khongluu":
                        {
                            isAdd = false;
                            Commons.Modules.ObjSystems.DeleteAddRow(grvQT);
                            SetButton(isAdd);
                            LoadData();
                            LocData();
                            grvQT.OptionsBehavior.Editable = false;
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
            catch { }
        }
        private void LoadTextTongDonGia()
        {
            try
            {
                DataTable dt1 = new DataTable();
                dt1 = Commons.Modules.ObjSystems.ConvertDatatable(grvQT);
                lbl.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTongDonGia") + " " + Convert.ToDouble(dt1.Compute("Sum(DON_GIA_THUC_TE)", "")).ToString();
            }
            catch
            {
                lbl.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTongDonGia") + " 0";
            }
        }
        private void Save()
        {

        }
        private void BorderAround(Microsoft.Office.Interop.Excel.Range range)
        {
            Microsoft.Office.Interop.Excel.Borders borders = range.Borders;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders.Color = Color.Black;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalUp].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
        }
        public DataTable ToDataTable(ExcelDataSource excelDataSource)
        {
            IList list = ((IListSource)excelDataSource).GetList();
            DevExpress.DataAccess.Native.Excel.DataView dataView = (DevExpress.DataAccess.Native.Excel.DataView)list;
            List<PropertyDescriptor> props = dataView.Columns.ToList<PropertyDescriptor>();
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                string sTenCot = "";
                switch (i)
                {
                    case 0:
                        {
                            sTenCot = "STT";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    case 1:
                        {
                            sTenCot = "BUOC_CV";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    case 2:
                        {
                            sTenCot = "BUOC_CV_A";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    case 3:
                        {
                            sTenCot = "NHOM_CD";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    case 4:
                        {
                            sTenCot = "THIET_BI";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    case 5:
                        {
                            sTenCot = "SMV";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    case 6:
                        {
                            sTenCot = "CONG_CU_HT";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    case 7:
                        {
                            sTenCot = "BAC_CD";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    case 8:
                        {
                            sTenCot = "SMV_THEO_BAC";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    case 9:
                        {
                            sTenCot = "DON_GIA_PHUT";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    case 10:
                        {
                            sTenCot = "DON_GIA_HO_TRO";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    case 11:
                        {
                            sTenCot = "SO_CONG_NHAN";
                            table.Columns.Add(sTenCot.Trim(), typeof(float));
                            break;
                        }
                    default:
                        {
                            table.Columns.Add(prop.Name.Trim(), prop.PropertyType);
                            break;
                        }
                }
            }
            object[] values = new object[props.Count];
            foreach (DevExpress.DataAccess.Native.Excel.ViewRow item in list)
            {
                try
                {
                    for (int i = 0; i < values.Length; i++)
                    {

                        if (props[i].GetValue(item) == null || props[i].GetValue(item).ToString() == "")
                        {
                            values[i] = null;
                        }
                        else
                        {
                            values[i] = props[i].GetValue(item);
                        }
                    }
                }
                catch (Exception ex) { }
                table.Rows.Add(values);
            }
            return table;
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
        private bool KiemTraLuoi(DataTable dtSource)
        {
            int count = grvQT.RowCount;
            int errorCount = 0;
            #region kiểm tra dữ liệu
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                string sMaSo = dr["MaQL"].ToString();
                if (!KiemTrungDL(grvQT, dtSource, dr, "MaQL", sMaSo, "", "", this.Name))
                {
                    errorCount++;
                }
                //THU_TU_CONG_DOAN
                if (!KiemDuLieuSo(grvQT, dr, "MaQL", grvQT.Columns["MaQL"].FieldName.ToString(), 0, 0, true, this.Name))
                {
                    errorCount++;
                }

                if (!KiemDuLieu(grvQT, dr, "CONG_DOAN", true, 250, this.Name))
                {
                    errorCount++;
                }

                if (!KiemDuLieu(grvQT, dr, "NHOM_CD", true, 250, this.Name))
                {
                    errorCount++;
                }

                if (!KiemDuLieuSo(grvQT, dr, "THOI_GIAN_THIET_KE", grvQT.Columns["THOI_GIAN_THIET_KE"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }

                if (!KiemDuLieuSo(grvQT, dr, "BAC_THO", grvQT.Columns["BAC_THO"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }

                if (!KiemDuLieuSo(grvQT, dr, "BAC_THO_DM", grvQT.Columns["BAC_THO_DM"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }

                if (!KiemDuLieuSo(grvQT, dr, "DON_GIA_GIAY", grvQT.Columns["DON_GIA_GIAY"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }

                if (!KiemDuLieuSo(grvQT, dr, "DON_GIA_THUC_TE", grvQT.Columns["DON_GIA_THUC_TE"].FieldName.ToString(), 0, 0, true, this.Name))
                {
                    errorCount++;
                }

                if (!KiemDuLieuSo(grvQT, dr, "DMLD", grvQT.Columns["DMLD"].FieldName.ToString(), 0, 0, false, this.Name))
                {
                    errorCount++;
                }
            }
            #endregion
            Commons.Modules.ObjSystems.HideWaitForm();
            if (errorCount != 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool KiemKyTu(string strInput, string strChuoi)
        {

            if (strChuoi == "") strChuoi = ChuoiKT;

            for (int i = 0; i < strInput.Length; i++)
            {
                for (int j = 0; j < strChuoi.Length; j++)
                {
                    if (strInput[i] == strChuoi[j])
                    {
                        return true;
                    }
                }
            }
            if (strInput.Contains("//"))
            {
                return true;
            }
            return false;
        }

        public bool KiemDuLieu(GridView grvData, DataRow dr, string sCot, Boolean bKiemNull, int iDoDaiKiem, string sform)
        {
            string sDLKiem;
            try
            {
                sDLKiem = dr[sCot].ToString();
                if (bKiemNull)
                {
                    if (string.IsNullOrEmpty(sDLKiem))
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongDuocTrong"));
                        return false;
                    }
                    else
                    {
                        if (KiemKyTu(sDLKiem, ChuoiKT))  //KiemKyTu
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgCoChuaKyTuDB"));
                            return false;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(sDLKiem))
                    {
                        if (KiemKyTu(sDLKiem, ChuoiKT))  //KiemKyTu
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgCoChuaKyTuDB"));
                            dr["XOA"] = 1;
                            return false;
                        }
                    }
                }
                if (iDoDaiKiem != 0)
                {
                    if (sDLKiem.Length > iDoDaiKiem)
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgDoDaiKyTuVuocQua " + iDoDaiKiem));
                        return false;
                    }
                }
            }
            catch
            {
                dr.SetColumnError(sCot, "error");
                return false;
            }
            return true;
        }
        public bool KiemDuLieuSo(GridView grvData, DataRow dr, string sCot, string sTenKTra, double GTSoSanh, double GTMacDinh, Boolean bKiemNull, string sForm)
        {
            string sDLKiem;
            sDLKiem = dr[sCot].ToString();
            double DLKiem;
            if (bKiemNull)
            {
                if (string.IsNullOrEmpty(sDLKiem))
                {
                    dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongduocTrong"));
                    return false;
                }
                else
                {
                    if (!double.TryParse(dr[sCot].ToString(), out DLKiem))
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongPhaiSo"));
                        return false;
                    }
                    else
                    {
                        if (GTSoSanh != -999999)
                        {
                            if (DLKiem < GTSoSanh)
                            {
                                dr.SetColumnError(sCot, sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongNhoHon") + GTSoSanh.ToString());
                                dr["XOA"] = 1;
                                return false;
                            }

                            DLKiem = Math.Round(DLKiem, 8);
                            dr[sCot] = DLKiem.ToString();

                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(sDLKiem) && GTMacDinh != -999999)
                {
                    dr[sCot] = (GTMacDinh == 0 ? (object)DBNull.Value : GTMacDinh);
                    DLKiem = GTMacDinh;
                    sDLKiem = GTMacDinh.ToString();
                }

                if (!string.IsNullOrEmpty(sDLKiem))
                {
                    if (!double.TryParse(dr[sCot].ToString() == "" ? "0" : dr[sCot].ToString(), out DLKiem))
                    {
                        dr.SetColumnError(sCot, sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongPhaiSo"));
                        return false;
                    }
                    else
                    {
                        if (GTSoSanh != -999999)
                        {
                            if (DLKiem < GTSoSanh)
                            {
                                dr.SetColumnError(sCot, sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongNhoHon") + GTSoSanh.ToString());
                                return false;
                            }

                            DLKiem = Math.Round(DLKiem, 8);
                            dr[sCot] = (DLKiem.ToString() == "0" ? (object)DBNull.Value : DLKiem.ToString());
                        }

                    }
                }


            }



            return true;
        }
        public bool KiemTrungDL(GridView grvData, DataTable dt, DataRow dr, string sCot, string sDLKiem, string tabName, string ColName, string sform)
        {
            string sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDL");
            try
            {

                if (dt.AsEnumerable().Count(x => x[sCot].Equals(sDLKiem)) > 1)
                {
                    sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDLLuoi");
                    dr.SetColumnError(sCot, sTenKTra);
                    return false;
                }
                else
                {

                    return true;

                }
            }
            catch (Exception ex)
            {
                dr.SetColumnError(sCot, sTenKTra);
                return false;
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
        public DXMenuItem MCreateMenuCapNhatAll(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblCapNhatAll", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(CapNhatAll));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }
        public void CapNhatAll(object sender, EventArgs e)
        {
            try
            {
                string sCotCN = grvQT.FocusedColumn.FieldName.ToString();
                try
                {
                    if (grvQT.GetFocusedRowCellValue(grvQT.FocusedColumn.FieldName).ToString() == "") return;
                    string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, Commons.Modules.ObjSystems.ConvertDatatable(grvQT), "");

                    DataTable dt = new DataTable();
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateChuotPhai_TiepNhan", sBTCongNhan, sCotCN, sCotCN.Substring(0, 3) == "NGA" ? Convert.ToDateTime(grvQT.GetFocusedRowCellValue(grvQT.FocusedColumn.FieldName)).ToString("MM/dd/yyyy") : grvQT.GetFocusedRowCellValue(grvQT.FocusedColumn.FieldName)));
                    grdQT.DataSource = dt;
                    Commons.Modules.ObjSystems.XoaTable(sCotCN);
                }
                catch (Exception ex)
                {
                    Commons.Modules.ObjSystems.XoaTable(sCotCN);
                }
            }
            catch (Exception ex) { }
        }
        public DXMenuItem MCreateMenuDelete(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblDeleteQTCN", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(Delete));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }
        public void Delete(object sender, EventArgs e)
        {
            string sBT = "sBTQTCN" + Commons.Modules.iIDUser;
            try
            {
                //Load worksheet
                XtraInputBoxArgs args = new XtraInputBoxArgs();
                // set required Input Box options
                args.Caption = "Chọn chuyền cần update";
                args.Prompt = "Chọn chuyền cần update";
                args.DefaultButtonIndex = 0;

                // initialize a DateEdit editor with custom settings
                CheckedComboBoxEdit editor = new CheckedComboBoxEdit();
                //editor.Properties.Items.AddRange(wSheet);
                //editor.EditValue = wSheet[0].ToString();
                Commons.Modules.ObjSystems.MLoadCheckedComboBoxEdit(editor, (DataTable)chkCboEditChuyen.Properties.DataSource, "ID_TO", "TEN_TO", "TEN_TO", true);
                editor.SetEditValue(chkCboEditChuyen.EditValue);

                args.Editor = editor;
                // a default DateEdit value
                //args.DefaultResponse = chkCboEditChuyen.EditValue;
                // display an Input Box with the custom editor
                var result = XtraInputBox.Show(args);
                if (result == null || result.ToString() == "") return;


                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdQT, grvQT), "");
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spQTCN", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 3;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd.Parameters.Add("@ID_TO", SqlDbType.NVarChar).Value = result.ToString();
                cmd.Parameters.Add("@ID_MH", SqlDbType.BigInt).Value = cboMH.Text == "" ? -99 : cboMH.EditValue;
                cmd.Parameters.Add("@ACTION", SqlDbType.NVarChar).Value = "DELETE";
                cmd.CommandType = CommandType.StoredProcedure;
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows[0][0].ToString() == "-99")
                {
                    XtraMessageBox.Show(dt.Rows[0][1].ToString());
                    return;
                }


                Commons.Modules.ObjSystems.DeleteAddRow(grvQT);
                LoadData();
                LocData();
                SetButton(false);
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgXoaThanhCongVuiLongKiemTraLai"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBT);
            }
        }
        public DXMenuItem MCreateMenuUpdate(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblUpdateQTCN", Commons.Modules.TypeLanguage);
            DXMenuItem menuPatse = new DXMenuItem(sStr, new EventHandler(Update));
            menuPatse.Tag = new RowInfo(view, rowHandle);
            return menuPatse;
        }
        public void Update(object sender, EventArgs e)
        {

            grvQT.CloseEditor();
            grvQT.UpdateCurrentRow();

            if (datNgayLap.Text == "")
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaNhapNgay"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            isAdd = false;
            grvQT.PostEditor();
            grvQT.UpdateCurrentRow();
            DataTable dtSource = new DataTable();
            dtSource = (DataTable)grdQT.DataSource;
            grvQT.Columns.View.ClearColumnErrors();
            this.Cursor = Cursors.WaitCursor;
            if (!KiemTraLuoi(dtSource))
            {
                this.Cursor = Cursors.Default;
                return;
            }
            this.Cursor = Cursors.Default;

            string sBT = "sBTQTCN" + Commons.Modules.iIDUser;
            try
            {
                //Load worksheet
                XtraInputBoxArgs args = new XtraInputBoxArgs();
                // set required Input Box options
                args.Caption = "Chọn chuyền cần update";
                args.Prompt = "Chọn chuyền cần update";
                args.DefaultButtonIndex = 0;

                // initialize a DateEdit editor with custom settings
                CheckedComboBoxEdit editor = new CheckedComboBoxEdit();
                //editor.Properties.Items.AddRange(wSheet);
                //editor.EditValue = wSheet[0].ToString();
                Commons.Modules.ObjSystems.MLoadCheckedComboBoxEdit(editor, (DataTable)chkCboEditChuyen.Properties.DataSource, "ID_TO", "TEN_TO", "TEN_TO", true);
                editor.SetEditValue(chkCboEditChuyen.EditValue);

                args.Editor = editor;
                // a default DateEdit value
                //args.DefaultResponse = chkCboEditChuyen.EditValue;
                // display an Input Box with the custom editor
                var result = XtraInputBox.Show(args);
                if (result == null || result.ToString() == "") return;


                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdQT, grvQT), "");
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spQTCN", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 3;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd.Parameters.Add("@ID_TO", SqlDbType.NVarChar).Value = result.ToString();
                cmd.Parameters.Add("@ID_MH", SqlDbType.BigInt).Value = cboMH.Text == "" ? -99 : cboMH.EditValue;
                cmd.Parameters.Add("@ACTION", SqlDbType.NVarChar).Value = "UPDATE";
                cmd.CommandType = CommandType.StoredProcedure;
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows[0][0].ToString() == "-99")
                {
                    XtraMessageBox.Show(dt.Rows[0][1].ToString());
                    return;
                }

                Commons.Modules.ObjSystems.DeleteAddRow(grvQT);
                LoadData();
                LocData();
                SetButton(false);
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCongVuiLongKiemTraLai"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBT);
            }
        }

        //Update công đoạn mã hàng
        public DXMenuItem MCreateMenuUpdateCDMaHang(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblUpdateCDMaHang", Commons.Modules.TypeLanguage);
            DXMenuItem menuPatse = new DXMenuItem(sStr, new EventHandler(UpdateCDMaHang));
            menuPatse.Tag = new RowInfo(view, rowHandle);
            return menuPatse;
        }
        public void UpdateCDMaHang(object sender, EventArgs e)
        {

            if (datNgayLap.Text == "")
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaNhapNgay"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            isAdd = false;
            grvQT.PostEditor();
            grvQT.UpdateCurrentRow();
            DataTable dtSource = new DataTable();
            dtSource = (DataTable)grdQT.DataSource;
            grvQT.Columns.View.ClearColumnErrors();
            this.Cursor = Cursors.WaitCursor;
            if (!KiemTraLuoi(dtSource))
            {
                this.Cursor = Cursors.Default;
                return;
            }
            this.Cursor = Cursors.Default;

            string sBT = "sBTQTCN" + Commons.Modules.iIDUser;
            try
            {
                frmCapNhatCDTheoMH frm = new frmCapNhatCDTheoMH();
                frm.iID_DV = Convert.ToInt32(cboDV.EditValue);
                frm.iID_CHUYEN_SD = Convert.ToInt32(chkCboEditChuyen.EditValue);
                frm.iID_ORD = Convert.ToInt32(grvQT.GetFocusedRowCellValue("ID_CD")) == 0 ? Convert.ToInt32(cboMH.EditValue) : -1;
                frm.dtTemp = new DataTable();
                frm.dtTemp = Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdQT, grvQT);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCong"), Commons.Form_Alert.enmType.Success);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBT);
            }
        }
        public DXMenuItem MCreateMenuCapNhatQuiTrinhHC(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblQuyTrinhHoanChinh", Commons.Modules.TypeLanguage);
            DXMenuItem menuPatse = new DXMenuItem(sStr, new EventHandler(UpdateQTHC));
            menuPatse.Tag = new RowInfo(view, rowHandle);
            return menuPatse;
        }
        public void UpdateQTHC(object sender, EventArgs e)
        {
            try
            {


                XtraInputBoxArgs args = new XtraInputBoxArgs();
                // set required Input Box options
                args.Caption = "Chọn chuyền cần update";
                args.Prompt = "Chọn chuyền cần update";
                args.DefaultButtonIndex = 0;

                CheckedComboBoxEdit editor = new CheckedComboBoxEdit();

                Commons.Modules.ObjSystems.MLoadCheckedComboBoxEdit(editor, (DataTable)chkCboEditChuyen.Properties.DataSource, "ID_TO", "TEN_TO", "TEN_TO", true);
                editor.SetEditValue(chkCboEditChuyen.EditValue);

                args.Editor = editor;

                var result = XtraInputBox.Show(args);
                if (result == null || result.ToString() == "") return;


                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spQTCN", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 4;
                cmd.Parameters.Add("@bCot1", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@ID_TO", SqlDbType.NVarChar).Value = result.ToString();
                cmd.Parameters.Add("@ID_MH", SqlDbType.BigInt).Value = cboMH.Text == "" ? -99 : cboMH.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows[0][0].ToString() == "-99")
                {
                    XtraMessageBox.Show(dt.Rows[0][1].ToString());
                }

                Commons.Modules.ObjSystems.DeleteAddRow(grvQT);
                LoadData();
                LocData();
                SetButton(false);
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCongVuiLongKiemTraLai"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
            }
        }
        public DXMenuItem MCreateMenuCapNhatQuiTrinhKhongHC(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblQuyTrinhKhongHoanChinh", Commons.Modules.TypeLanguage);
            DXMenuItem menuPatse = new DXMenuItem(sStr, new EventHandler(UpdateQTKHC));
            menuPatse.Tag = new RowInfo(view, rowHandle);
            return menuPatse;
        }
        public void UpdateQTKHC(object sender, EventArgs e) // qui trình không hoàn chỉnh
        {
            try
            {


                XtraInputBoxArgs args = new XtraInputBoxArgs();
                // set required Input Box options
                args.Caption = "Chọn chuyền cần update";
                args.Prompt = "Chọn chuyền cần update";
                args.DefaultButtonIndex = 0;

                CheckedComboBoxEdit editor = new CheckedComboBoxEdit();

                Commons.Modules.ObjSystems.MLoadCheckedComboBoxEdit(editor, (DataTable)chkCboEditChuyen.Properties.DataSource, "ID_TO", "TEN_TO", "TEN_TO", true);
                editor.SetEditValue(chkCboEditChuyen.EditValue);

                args.Editor = editor;

                var result = XtraInputBox.Show(args);
                if (result == null || result.ToString() == "") return;

                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spQTCN", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 4;
                cmd.Parameters.Add("@bCot1", SqlDbType.Bit).Value = false;
                cmd.Parameters.Add("@ID_TO", SqlDbType.NVarChar).Value = result.ToString();
                cmd.Parameters.Add("@ID_MH", SqlDbType.BigInt).Value = cboMH.Text == "" ? -99 : cboMH.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                if (dt.Rows[0][0].ToString() == "-99")
                {
                    XtraMessageBox.Show(dt.Rows[0][1].ToString());
                }

                Commons.Modules.ObjSystems.DeleteAddRow(grvQT);
                LoadData();
                LocData();
                SetButton(false);
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCongVuiLongKiemTraLai"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
            }
        }

        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboDoiTac();
            LoadCboHangHoa();

            LoadCboTo();
            chkCboEditChuyen_EditValueChanged(null, null);
        }

        private void grdQT_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete && !windowsUIButton.Buttons[0].Properties.Visible)
                {
                    try
                    {
                        if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.PHIEU_CONG_DOAN WHERE ID_CD = " + grvQT.GetFocusedRowCellValue("ID_CD") + "")) > 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                        else
                        {
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoMuonXoaCDNayKhong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE FROM dbo.QUI_TRINH_CONG_NGHE_CHI_TIET WHERE ID = " + grvQT.GetFocusedRowCellValue("ID_CD") + "");
                            grvQT.DeleteSelectedRows();
                            ((DataTable)grdQT.DataSource).AcceptChanges();
                        }
                    }
                    catch { }
                }
                if (e.Control && e.KeyCode == Keys.C)
                {
                    DataTable dtTemp = new DataTable();
                    dtTemp = (DataTable)grdQT.DataSource;
                    if (dtTemp.Rows.Count == 0) return;

                    dtTempCopy = new DataTable();
                    dtTempCopy = Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdQT, grvQT);
                    bCheckCopy = true;

                    XtraMessageBoxArgs args = new XtraMessageBoxArgs();
                    args.AutoCloseOptions.Delay = 500;
                    args.Caption = "";
                    args.Text = "Copied";
                    XtraMessageBox.Show(args).ToString();
                }
                if (e.Control && e.KeyCode == Keys.V)
                {
                    DataTable dtTemp = new DataTable();
                    dtTemp = (DataTable)grdQT.DataSource;
                    if (dtTemp.Rows.Count > 0) return;
                    string sBT = "sBTQTCN" + Commons.Modules.iIDUser;
                    try
                    {
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, dtTempCopy, "");
                        DataTable dt = new DataTable();
                        dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spCopyQTCN", sBT, cboMH.EditValue, chkCboEditChuyen.EditValue.ToString().IndexOf(',') != -1 ? chkCboEditChuyen.EditValue.ToString().Substring(0, chkCboEditChuyen.EditValue.ToString().IndexOf(',')) : chkCboEditChuyen.EditValue.ToString()));
                        grdQT.DataSource = dt;
                        Commons.Modules.ObjSystems.XoaTable(sBT);
                        //Commons.Modules.ObjSystems.AddnewRow(grvQT, true);
                        SetButton(true);
                    }
                    catch (Exception ex)
                    {
                        Commons.Modules.ObjSystems.XoaTable(sBT);
                    }
                }
            }
            catch { }
        }

        private void grvQT_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                grvQT.SetFocusedRowCellValue("ID_CD", 0);
            }
            catch { }
        }

        private void rdoXemCuLapMoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoXemCuLapMoi.SelectedIndex == 0)
                {
                    lblTuNgay.Visible = true;
                    lblDenNgay.Visible = true;
                    datTNgay.Visible = true;
                    datDNgay.Visible = true;
                }
                else
                {
                    lblTuNgay.Visible = false;
                    lblDenNgay.Visible = false;
                    datTNgay.Visible = false;
                    datDNgay.Visible = false;
                }
                LoadCboHangHoa();
                LoadCboTo();
                chkCboEditChuyen_EditValueChanged(null, null);
            }
            catch (Exception ex) { }
        }

        private void datTNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboHangHoa();

        }

        private void datDNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCboHangHoa();
        }

        private void chkCboEditChuyen_EditValueChanged(object sender, EventArgs e)
        {
            LoadCboCum();
            LoadData();
            datNgayLap.DateTime = Convert.ToDateTime(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT TOP 1 NGAY_LAP FROM dbo.QUI_TRINH_CONG_NGHE_CHI_TIET WHERE ID = " + (grvQT.GetFocusedRowCellValue("ID_CD") == null ? -1 : Convert.ToInt64(grvQT.GetFocusedRowCellValue("ID_CD"))).ToString() + ""));
            datNgayLap.DateTime = datNgayLap.DateTime == DateTime.MinValue ? DateTime.Now : datNgayLap.DateTime;
        }

        private void grvQT_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {

                if (Convert.ToBoolean(grvQT.GetRowCellValue(e.RowHandle, grvQT.Columns["QUI_TRINH_HOAN_CHINH"].FieldName)) != false)
                {
                    e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFF2CC");
                    e.HighPriority = true;
                }

                if (!windowsUIButton.Buttons[0].Properties.Visible)
                {
                    if (Convert.ToInt32(grvQT.GetRowCellValue(e.RowHandle, grvQT.Columns["TINH_TRANG_CD"].FieldName)) == 2)
                    {
                        e.Appearance.BackColor = Color.Salmon;
                        e.Appearance.BackColor2 = Color.SeaShell;
                        e.HighPriority = true;
                    }
                    if (grvQT.GetRowCellValue(e.RowHandle, grvQT.Columns["TINH_TRANG_CD"].FieldName).ToString() == "1")
                    {
                        e.Appearance.BackColor = Color.LightGreen;
                        e.Appearance.BackColor2 = Color.SeaShell;
                        e.HighPriority = true;
                    }

                }
            }
            catch
            {

            }
        }

        private void grvQT_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            string sMaGopCurrent;
            try
            {
                var row = view.GetFocusedDataRow();

                if (e.Column.FieldName == "MA_GOP")
                {
                    sMaGopCurrent = grvQT.GetFocusedRowCellValue("MA_GOP").ToString().Trim();
                    DataTable dt = new DataTable();
                    dt = (DataTable)grdQT.DataSource;
                    //if (dt.AsEnumerable().Count(x => x["MA_GOP"].Equals(sMaGopCurrent)) > 1)
                    //{
                    //    row["MA_GOP"] = DBNull.Value;
                    //    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDaCoMaGop"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}

                    try
                    {
                        dt = dt.AsEnumerable().Where(r => Convert.ToString(r["MA_GOP"]) != "").CopyToDataTable();

                    }
                    catch { dt.Clear(); }
                    dt.AcceptChanges();

                    if (dt.AsEnumerable().Count(x => x["MaQL"].Equals(sMaGopCurrent)) > 0)
                    {
                        row["MA_GOP"] = DBNull.Value;
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDaCoMaGop"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void grvDSUngVien_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                int irow = e.HitInfo.RowHandle;

                if (windowsUIButton.Buttons[0].Properties.Visible)
                {
                    DevExpress.Utils.Menu.DXMenuItem itemUpdate_QTHC = MCreateMenuCapNhatQuiTrinhHC(view, irow);
                    e.Menu.Items.Add(itemUpdate_QTHC);

                    DevExpress.Utils.Menu.DXMenuItem itemUpdate_QKTHC = MCreateMenuCapNhatQuiTrinhKhongHC(view, irow);
                    e.Menu.Items.Add(itemUpdate_QKTHC);
                }
                else
                {
                    if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                    {
                        e.Menu.Items.Clear();
                        if (Convert.ToString(grvQT.GetFocusedRowCellValue("MaQL")) != "")
                        {
                            DevExpress.Utils.Menu.DXMenuItem itemCapNhatCDMH = MCreateMenuUpdateCDMaHang(view, irow);
                            e.Menu.Items.Add(itemCapNhatCDMH);
                            DevExpress.Utils.Menu.DXMenuItem itemCopy = MCreateMenuUpdate(view, irow);
                            e.Menu.Items.Add(itemCopy);
                            DevExpress.Utils.Menu.DXMenuItem itemDelete = MCreateMenuDelete(view, irow);
                            e.Menu.Items.Add(itemDelete);
                        }
                    }
                }

                //else
                //{
                //    if (bCheckCopy == true)
                //    {
                //        DataTable dt = new DataTable();
                //        dt = (DataTable)grdQT.DataSource;
                //        if (dt.Rows.Count > 0 || dtTempCopy.Rows.Count == 0) return;
                //        DevExpress.Utils.Menu.DXMenuItem itemPaste = MCreateMenuPatse(view, irow);
                //        e.Menu = new DevExpress.XtraGrid.Menu.GridViewMenu(view);
                //        e.Menu.Items.Add(itemPaste);
                //    }
                //}
            }
            catch (Exception ex)
            {
            }
        }

        #endregion
    }
}