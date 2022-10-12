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
            Commons.Modules.ObjSystems.ThayDoiNN(this, windowsUIButton);
        }

        private void frmQTCN_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            try
            {
                datNgayLap.DateTime = DateTime.Now;
                Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
                LoadCbo();
                LoadHD(0);
                LoadLuoi();
                //cboCum_EditValueChanged(null, null);
                //cboChuyen_EditValueChanged(null, null);
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message.ToString()); }

            Commons.Modules.sLoad = "";
        }

        private void LoadHD(int iLoad)
        {
            Commons.Modules.sLoad = "0LoadCbo";
            String sKH, sDV; //, sMH, sTo;
            sKH = "-1"; sDV = "-1"; //sMH = "-1"; sTo = "-1";

            try { sKH = cboKH.EditValue.ToString(); } catch { }
            try { sDV = cboDV.EditValue.ToString(); } catch { }
            //try { sMH = cboMH.EditValue.ToString(); } catch { }
            //try { sTo = cboChuyen.EditValue.ToString(); } catch { }

            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spQTCNGetCbo", conn);
                cmd.Parameters.Add("@sDV", SqlDbType.NVarChar, 50).Value = sDV;
                cmd.Parameters.Add("@sKH", SqlDbType.NVarChar, 50).Value = sKH;
                //cmd.Parameters.Add("@sDDH", SqlDbType.NVarChar, 50).Value = sDDH;
                //cmd.Parameters.Add("@sMH", SqlDbType.NVarChar, 50).Value = sMH;
                //cmd.Parameters.Add("@sOrd", SqlDbType.NVarChar, 50).Value = sOrd;

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);

                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "KHACH_HANG";
                if (iLoad == 0) Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboKH, dt, "ID_DT", "TEN_NGAN", "TEN_NGAN", true);

                //dt = new DataTable();
                //dt = ds.Tables[1].Copy();
                //dt.TableName = "HOP_DONG";
                //if (iLoad == 0 || iLoad == 1) Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDV, dt, "ID_DHB", "SO_DHB", "SO_DHB", true);


                dt = new DataTable();
                dt = ds.Tables[1].Copy();
                dt.TableName = "MA_HANG";
                if (iLoad == 0 || iLoad == 1 || iLoad == 2)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboMH, dt, "ID_ORD", "TEN_HH", "TEN_HH", true);
                    //cboMH.Properties.DataSource
                    //cboMH.Properties.View.Columns["TEN_LOAI_HH"].Visible = false;
                    LOAI_HH = dt.Rows[0]["TEN_LOAI_HH"].ToString();
                }

                //dt = new DataTable();
                //dt = ds.Tables[3].Copy();
                //dt.TableName = "TEN_ORDER";
                //if (iLoad == 0 || iLoad == 1 || iLoad == 2 || iLoad == 3) Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboOrd, dt, "ID_DHBORD", "TEN_ORD", "TEN_ORD", true);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }

        }

        private void LoadCbo()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCHUYEN", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboDV.EditValue, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboChuyen, dt, "ID_TO", "TEN_TO", "TEN_TO");
                cboChuyen.Properties.View.Columns[0].Caption = "STT Chuyền";
                cboChuyen.Properties.View.Columns[1].Caption = "Tên Chuyền";
                cboChuyen.Properties.View.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboChuyen.Properties.View.Columns[1].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cboChuyen.Properties.View.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboChuyen.Properties.View.Columns[0].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                //LoadCboCum(id_NHH);
            }
            catch { }
        }

        private void LoadCboCum()
        {
            try
            {
                //string sSql = "SELECT ID_CUM, TEN_CUM FROM CUM WHERE ID_NHH = " + cboLMH.EditValue + " UNION SELECT '-1','' FROM CUM ";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCUM", Convert.ToInt64(cboChuyen.EditValue), Convert.ToInt64(cboMH.EditValue), 1));
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
            catch { }
        }

        //DataTable dtBT;
        //DataTable dtCD, dtLoaiMay, dtChuyen, dtCum, dtCDTemp;
        private void LoadLuoi()
        {
            //Commons.Modules.sLoad = "0Load";
            String sTo, sOrd;
            sTo = "-1"; sOrd = "-1";

            try { sTo = cboChuyen.EditValue.ToString(); } catch { }
            try { sOrd = cboMH.EditValue.ToString(); } catch { }

            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spQTCNGet", sTo, sOrd));

            if (grdQT.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdQT, grvQT, dt, false, false, false, false, true, this.Name);
            }
            else
            {
                try { grdQT.DataSource = dt; } catch { }
            }
            if (!isAdd)
            {
                grvQT.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            }

            FormatGrid();
            SetButton(isAdd);
        }

        private void FormatGrid()
        {
            //An cot
            grvQT.Columns["ID_CD"].Visible = false;
            grvQT.Columns["ID_TO"].Visible = false;
            grvQT.Columns["ID_ORD"].Visible = false;

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
            if (Commons.Modules.sLoad == "0LoadCbo") return;
            LoadHD(1);
            Commons.Modules.sLoad = "";
        }

        private void cboMH_EditValueChanged(object sender, EventArgs e)
        {
            //if (Commons.Modules.sLoad == "0LoadCbo") return;
            LoadCboCum();
            LoadLuoi();
            Commons.Modules.sLoad = "";
        }

        private void cboChuyen_EditValueChanged(object sender, EventArgs e)
        {
            LoadCboCum();
            LoadLuoi();
        }

        private void LocData()
        {
            if (Commons.Modules.sLoad == "0LoadCbo") return;
            DataTable dtTmp = new DataTable();
            try
            {
                dtTmp = (DataTable)grdQT.DataSource;
                string sCum = "-1";
                string sDK = "";
                try { sCum = cboCum.EditValue.ToString(); } catch { }

                if (sCum != "-1") sDK = "NHOM_CD LIKE '" + sCum + "'";

                dtTmp.DefaultView.RowFilter = sDK;
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
            windowsUIButton.Buttons[10].Properties.Visible = !isAdd;

            windowsUIButton.Buttons[3].Properties.Visible = isAdd;
            windowsUIButton.Buttons[5].Properties.Visible = isAdd;
            windowsUIButton.Buttons[6].Properties.Visible = isAdd;
            windowsUIButton.Buttons[7].Properties.Visible = isAdd;
            windowsUIButton.Buttons[8].Properties.Visible = isAdd;
            windowsUIButton.Buttons[9].Properties.Visible = isAdd;

            cboKH.Enabled = !isAdd;
            cboDV.Enabled = !isAdd;
            cboMH.Enabled = !isAdd;
            cboChuyen.Enabled = !isAdd;
            datNgayLap.Enabled = !isAdd;

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
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbQT, Commons.Modules.ObjSystems.ConvertDatatable(grdQT), "");
                //Cap nhat qui trinh cong nghe
                string sSql = "UPDATE QUI_TRINH_CONG_NGHE_CHI_TIET SET CONG_DOAN = tmp.CONG_DOAN, THU_TU_CONG_DOAN = tmp.THU_TU_CONG_DOAN, "
                            + " NHOM_CD = tmp.NHOM_CD, MaQL = tmp.MaQL, BAC_THO = tmp.BAC_THO, BAC_THO_DM = tmp.BAC_THO_DM, LOAI_MAY = tmp.LOAI_MAY, "
                            + " THOI_GIAN_THIET_KE = tmp.THOI_GIAN_THIET_KE, CONG_CU_HT = tmp.CONG_CU_HT, DON_GIA_GIAY = tmp.DON_GIA_GIAY, "
                            + " DON_GIA_THUC_TE = tmp.DON_GIA_THUC_TE, DMLD = tmp.DMLD "
                            + " FROM QUI_TRINH_CONG_NGHE_CHI_TIET_TEST QT "
                            + " INNER JOIN " + stbQT + " tmp ON QT.ID = tmp.ID_CD "
                            + " INSERT INTO QUI_TRINH_CONG_NGHE_CHI_TIET(ID_TO, ID_ORD, THU_TU_CONG_DOAN, CONG_DOAN, NHOM_CD, MaQL, BAC_THO, BAC_THO_DM, "
                            + " LOAI_MAY, THOI_GIAN_THIET_KE, CONG_CU_HT, DON_GIA_GIAY, DON_GIA_THUC_TE, DMLD)"
                            + " SELECT ID_TO, ID_ORD, THU_TU_CONG_DOAN, CONG_DOAN, NHOM_CD, MaQL, BAC_THO, BAC_THO_DM, LOAI_MAY, THOI_GIAN_THIET_KE, "
                            + " CONG_CU_HT, DON_GIA_GIAY, DON_GIA_THUC_TE, DMLD "
                            + " FROM " + stbQT + " tmp1 WHERE ISNULL(ID_CD,0) = 0";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);

                //string strSql1 = "DROP TABLE " + stbQT;
                //SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSql1);
                Commons.Modules.ObjSystems.XoaTable(stbQT);
            }
            catch (Exception ex)
            {
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
                        //cmd.Parameters.Add("@ID_CHUYEN", SqlDbType.Int).Value = cboChuyen.EditValue;
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
                            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListQTCNImport", Convert.ToInt64(cboChuyen.EditValue), Convert.ToInt64(cboMH.EditValue), sBTQTCN, sBTQTCN_Current));
                            grdQT.DataSource = dt;
                            //Commons.Modules.ObjSystems.MLoadXtraGrid(grdQT, grvQT, dt, true, true, false, false, true, this.Name);
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
                                sSql = "DELETE QUI_TRINH_CONG_NGHE_CHI_TIET WHERE ID_TO = " + grvQT.GetFocusedRowCellValue("ID_TO") +
                                                                        " AND ID_ORD = " + grvQT.GetFocusedRowCellValue("ID_ORD") + "";
                                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                                LoadLuoi();
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

                        if (cboChuyen.Text == "")
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
                        LoadLuoi();
                        LocData();
                        break;
                    }
                case "khongluu":
                    {
                        isAdd = false;
                        Commons.Modules.ObjSystems.DeleteAddRow(grvQT);
                        SetButton(isAdd);
                        LoadLuoi();
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
                            sTenCot = "NHOM_CD";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    case 3:
                        {
                            sTenCot = "THIET_BI";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    case 4:
                        {
                            sTenCot = "SMV";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    case 5:
                        {
                            sTenCot = "CONG_CU_HT";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    case 6:
                        {
                            sTenCot = "BAC_CD";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    case 7:
                        {
                            sTenCot = "SMV_THEO_BAC";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    case 8:
                        {
                            sTenCot = "DON_GIA_PHUT";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    case 9:
                        {
                            sTenCot = "DON_GIA_HO_TRO";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    case 10:
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

                if (!KiemDuLieuSo(grvQT, dr, "DON_GIA_THUC_TE", grvQT.Columns["DON_GIA_THUC_TE"].FieldName.ToString(), 0, 0, false, this.Name))
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
                    dr[sCot] = GTMacDinh;
                    DLKiem = GTMacDinh;
                    sDLKiem = GTMacDinh.ToString();
                }

                if (!string.IsNullOrEmpty(sDLKiem))
                {
                    if (!double.TryParse(dr[sCot].ToString(), out DLKiem))
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
                            dr[sCot] = DLKiem.ToString();
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

                if (dt.AsEnumerable().Where(x => x.Field<string>(sCot).Equals(sDLKiem)).CopyToDataTable().Rows.Count > 1)
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
        public DXMenuItem MCreateMenuCopy(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblCopy", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(Copy));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }
        public void Copy(object sender, EventArgs e)
        {
            try
            {
                dtTempCopy = new DataTable();
                dtTempCopy = Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdQT, grvQT);
                bCheckCopy = true;

                XtraMessageBoxArgs args = new XtraMessageBoxArgs();
                args.AutoCloseOptions.Delay = 500;
                args.Caption = "";
                args.Text = "Copied";
                XtraMessageBox.Show(args).ToString();

            }
            catch { }
        }
        public DXMenuItem MCreateMenuPatse(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblPaste", Commons.Modules.TypeLanguage);
            DXMenuItem menuPatse = new DXMenuItem(sStr, new EventHandler(Patse));
            menuPatse.Tag = new RowInfo(view, rowHandle);
            return menuPatse;
        }
        public void Patse(object sender, EventArgs e)
        {
            string sBT = "sBTQTCN" + Commons.Modules.iIDUser;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, dtTempCopy, "");
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spCopyQTCN", sBT, cboMH.EditValue, cboChuyen.EditValue));
                grdQT.DataSource = dt;
                Commons.Modules.ObjSystems.XoaTable(sBT);
                Commons.Modules.ObjSystems.AddnewRow(grvQT, true);
                SetButton(true);
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBT);
            }
        }

        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            LoadCbo();
        }

        private void grdQT_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
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
                        grdQT.DataSource = SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spCopyQTCN", sBT, cboMH.EditValue, cboChuyen.EditValue);
                        Commons.Modules.ObjSystems.XoaTable(sBT);
                        Commons.Modules.ObjSystems.AddnewRow(grvQT, true);
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

        private void grvDSUngVien_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                int irow = e.HitInfo.RowHandle;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    e.Menu.Items.Clear();

                    DevExpress.Utils.Menu.DXMenuItem itemCopy = MCreateMenuCopy(view, irow);
                    e.Menu.Items.Add(itemCopy);

                    if (windowsUIButton.Buttons[0].Properties.Visible) return;
                    DevExpress.Utils.Menu.DXMenuItem itemCapNhatAll = MCreateMenuCapNhatAll(view, irow);
                    e.Menu.Items.Add(itemCapNhatAll);

                    //if (flag == false) return;
                }
                else
                {
                    if (bCheckCopy == true)
                    {
                        DataTable dt = new DataTable();
                        dt = (DataTable)grdQT.DataSource;
                        if (dt.Rows.Count > 0 || dtTempCopy.Rows.Count == 0) return;
                        DevExpress.Utils.Menu.DXMenuItem itemPaste = MCreateMenuPatse(view, irow);
                        e.Menu = new DevExpress.XtraGrid.Menu.GridViewMenu(view);
                        e.Menu.Items.Add(itemPaste);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion
    }
}