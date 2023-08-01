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
using Microsoft.Office.Interop;
using DevExpress.Utils.Menu;
using System.Reflection;

namespace Vs.Payroll
{
    public partial class ucBangLuongThang13 : DevExpress.XtraEditors.XtraUserControl
    {
        private bool ColFlag = false;  // false update cột PT_HQKD , true update cột PT_TL
        private int iLoai = 1; // 1 tính tổng lương else tính thuế TNCN
        public static ucBangLuongThang13 _instance;
        public static ucBangLuongThang13 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucBangLuongThang13();
                return _instance;
            }
        }
        public ucBangLuongThang13()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);

        }

        private void ucBangLuongThang13_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                datNam.EditValue = DateTime.Now;
                Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
                Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);

                switch (Commons.Modules.KyHieuDV)
                {
                    case "TG":
                        {
                            LoadData_TG();
                            break;
                        }
                }


                EnableButon();
                Commons.Modules.sLoad = "";
                Commons.Modules.ObjSystems.SetPhanQuyen(btnALL);
            }
            catch { }

        }

        private void LoadData_TG()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetBangLuong13_TG", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToInt64(cboDonVi.EditValue), Convert.ToInt64(cboXiNghiep.EditValue), Convert.ToInt64(cboTo.EditValue), Convert.ToDateTime(datNam.EditValue).Year));
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, false, false, true, true, this.Name); grvData.Columns["ID_CN"].Visible = false;
                    grvData.Columns["MS_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["HO_TEN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["TEN_TO"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["NGAY_VAO_CTY"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                    for (int i = 5; i < grvData.Columns.Count - 1; i++)
                    {
                        grvData.Columns[i].OptionsColumn.AllowEdit = false;
                        switch (grvData.Columns[i].FieldName.Substring(0, 2))
                        {
                            case "DL":

                                {
                                    try
                                    {
                                        grvData.Columns[grvData.Columns[i].FieldName].DisplayFormat.FormatType = FormatType.Numeric;
                                        grvData.Columns[grvData.Columns[i].FieldName].DisplayFormat.FormatString = "N1";
                                    }
                                    catch
                                    {
                                    }
                                    break;
                                }
                            case "HS":
                                {
                                    try
                                    {
                                        grvData.Columns[grvData.Columns[i].FieldName].DisplayFormat.FormatType = FormatType.Numeric;
                                        grvData.Columns[grvData.Columns[i].FieldName].DisplayFormat.FormatString = "N1";
                                        grvData.Columns[i].OptionsColumn.AllowEdit = true;
                                    }
                                    catch
                                    {
                                    }
                                    break;
                                }
                            case "TY":
                                {
                                    try
                                    {
                                        grvData.Columns[grvData.Columns[i].FieldName].DisplayFormat.FormatType = FormatType.Custom;
                                        grvData.Columns[grvData.Columns[i].FieldName].DisplayFormat.FormatString = "0%";
                                        grvData.Columns[i].OptionsColumn.AllowEdit = true;
                                    }
                                    catch
                                    {
                                    }
                                    break;
                                }
                            case "BU":
                            case "TI":
                                {
                                    try
                                    {
                                        grvData.Columns[grvData.Columns[i].FieldName].DisplayFormat.FormatType = FormatType.Numeric;
                                        grvData.Columns[grvData.Columns[i].FieldName].DisplayFormat.FormatString = "N0";
                                        grvData.Columns[i].OptionsColumn.AllowEdit = true;
                                    }
                                    catch
                                    {
                                    }
                                    break;
                                }
                            case "XL":
                                {
                                    grvData.Columns[i].OptionsColumn.AllowEdit = true;
                                    break;
                                }
                            case "CM":
                            case "ST":
                                {
                                    grvData.Columns[i].OptionsColumn.AllowEdit = true;
                                    break;
                                }
                            default:
                                {
                                    try
                                    {
                                        grvData.Columns[grvData.Columns[i].FieldName].DisplayFormat.FormatType = FormatType.Numeric;
                                        grvData.Columns[grvData.Columns[i].FieldName].DisplayFormat.FormatString = "N0";
                                    }
                                    catch
                                    {
                                    }
                                }
                                break;
                        }
                    }
                }
                else
                {
                    grdData.DataSource = dt;
                }
            }
            catch
            {

            }
        }

        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "export":
                    {
                        Export();
                        break;
                    }
                case "import":
                    {
                        if (datNam.Text == "")
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonNam"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        frmImportTinhLuong_TG frm = new frmImportTinhLuong_TG();
                        frm.iID_DV = Convert.ToInt32(cboDonVi.EditValue);
                        frm.iID_XN = Convert.ToInt32(cboXiNghiep.EditValue);
                        frm.iID_TO = Convert.ToInt32(cboTo.EditValue);
                        frm.iloai = 2;
                        frm.dtThang = DateTime.ParseExact("01/01/" + datNam.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        frm.dtDThang = DateTime.ParseExact("31/12/"+ datNam.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        double iW, iH;
                        iW = Screen.PrimaryScreen.WorkingArea.Width / 1.5;
                        iH = Screen.PrimaryScreen.WorkingArea.Height / 1.5;
                        frm.Size = new Size((int)iW, (int)iH);
                        if (frm.ShowDialog() == DialogResult.OK)
                        {

                            switch (Commons.Modules.KyHieuDV)
                            {
                                case "TG":
                                    {
                                        LoadData_TG();
                                        break;
                                    }
                            }
                            TinhLuong();
                        }
                        break;
                    }
                case "tinhluong":
                    {
                        try
                        {
                            TinhLuong();
                        }
                        catch
                        {
                        }
                        break;
                    }
              


                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
            }
        }


        private void Export()
        {

            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            DataTable dtBCLuong;
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spImportExportLuong13_TG", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToDateTime(datNam.EditValue).Year;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCLuong = new DataTable();
                dtBCLuong = ds.Tables[0].Copy();

                string SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel._Workbook oWB;
                Microsoft.Office.Interop.Excel._Worksheet oSheet;

                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 12;
                int fontSizeNoiDung = 12;
                int iTNgay = 1;
                int iDNgay = 20;
                int iSoNgay = (iDNgay - iTNgay);


                Microsoft.Office.Interop.Excel.Range row4_A = oSheet.get_Range("A1");
                row4_A.ColumnWidth = 16;
                row4_A.Value2 = "Mã nhân viên";

                Microsoft.Office.Interop.Excel.Range row4_B = oSheet.get_Range("B1");
                row4_B.ColumnWidth = 33;
                row4_B.Value2 = "Họ tên";

                Microsoft.Office.Interop.Excel.Range row4_C = oSheet.get_Range("C1");
                row4_C.ColumnWidth = 15;
                row4_C.Value2 = "Bù HĐLĐ T1";

                Microsoft.Office.Interop.Excel.Range row4_D = oSheet.get_Range("D1");
                row4_D.ColumnWidth = 15;
                row4_D.Value2 = "Bù HS T1";

                Microsoft.Office.Interop.Excel.Range row4_E = oSheet.get_Range("E1");
                row4_E.ColumnWidth = 15;
                row4_E.Value2 = "Đề Nghị T1";

              
                Microsoft.Office.Interop.Excel.Range row4_F = oSheet.get_Range("F1");
                row4_F.ColumnWidth = 15;
                row4_F.Value2 = "Bù HĐLĐ T2";

                Microsoft.Office.Interop.Excel.Range row4_G = oSheet.get_Range("G1");
                row4_G.ColumnWidth = 15;
                row4_G.Value2 = "Bù HS T2";

                Microsoft.Office.Interop.Excel.Range row4_H = oSheet.get_Range("H1");
                row4_H.ColumnWidth = 15;
                row4_H.Value2 = "Đề Nghị T2";


                Microsoft.Office.Interop.Excel.Range row4_I = oSheet.get_Range("I1");
                row4_I.ColumnWidth = 15;
                row4_I.Value2 = "Bù HĐLĐ T3";
                Microsoft.Office.Interop.Excel.Range row4_J = oSheet.get_Range("J1");
                row4_J.ColumnWidth = 15;
                row4_J.Value2 = "Bù HS T3";
                Microsoft.Office.Interop.Excel.Range row4_K = oSheet.get_Range("K1");
                row4_K.ColumnWidth = 15;
                row4_K.Value2 = "Đề Nghị T3";


                Microsoft.Office.Interop.Excel.Range row4_L = oSheet.get_Range("L1");
                row4_L.ColumnWidth = 15;
                row4_L.Value2 = "Bù HĐLĐ T4";
                Microsoft.Office.Interop.Excel.Range row4_M = oSheet.get_Range("M1");
                row4_M.ColumnWidth = 15;
                row4_M.Value2 = "Bù HS T4";
                Microsoft.Office.Interop.Excel.Range row4_N = oSheet.get_Range("N1");
                row4_N.ColumnWidth = 15;
                row4_N.Value2 = "Đề Nghị T4";

                Microsoft.Office.Interop.Excel.Range row4_O = oSheet.get_Range("O1");
                row4_O.ColumnWidth = 15;
                row4_O.Value2 = "Bù HĐLĐ T5";
                Microsoft.Office.Interop.Excel.Range row4_P = oSheet.get_Range("P1");
                row4_P.ColumnWidth = 15;
                row4_P.Value2 = "Bù HS T5";
                Microsoft.Office.Interop.Excel.Range row4_Q = oSheet.get_Range("Q1");
                row4_Q.ColumnWidth = 15;
                row4_Q.Value2 = "Đề Nghị T5";

                Microsoft.Office.Interop.Excel.Range row4_R = oSheet.get_Range("R1");
                row4_R.ColumnWidth = 15;
                row4_R.Value2 = "Bù HĐLĐ T6";
                Microsoft.Office.Interop.Excel.Range row4_S = oSheet.get_Range("S1");
                row4_S.ColumnWidth = 15;
                row4_S.Value2 = "Bù HS T6";
                Microsoft.Office.Interop.Excel.Range row4_T = oSheet.get_Range("T1");
                row4_T.ColumnWidth = 15;
                row4_T.Value2 = "Đề Nghị T6";

                Microsoft.Office.Interop.Excel.Range row4_U = oSheet.get_Range("U1");
                row4_U.ColumnWidth = 15;
                row4_U.Value2 = "Bù HĐLĐ T7";
                Microsoft.Office.Interop.Excel.Range row4_V = oSheet.get_Range("V1");
                row4_V.ColumnWidth = 15;
                row4_V.Value2 = "Bù HS T7";
                Microsoft.Office.Interop.Excel.Range row4_W = oSheet.get_Range("W1");
                row4_W.ColumnWidth = 15;
                row4_W.Value2 = "Đề Nghị T7";

                Microsoft.Office.Interop.Excel.Range row4_X = oSheet.get_Range("X1");
                row4_X.ColumnWidth = 15;
                row4_X.Value2 = "Bù HĐLĐ T8";
                Microsoft.Office.Interop.Excel.Range row4_Y = oSheet.get_Range("Y1");
                row4_Y.ColumnWidth = 15;
                row4_Y.Value2 = "Bù HS T8";
                Microsoft.Office.Interop.Excel.Range row4_Z = oSheet.get_Range("Z1");
                row4_Z.ColumnWidth = 15;
                row4_Z.Value2 = "Đề Nghị T8";


                Microsoft.Office.Interop.Excel.Range row4_AA = oSheet.get_Range("AA1");
                row4_AA.ColumnWidth = 15;
                row4_AA.Value2 = "Bù HĐLĐ T9";
                Microsoft.Office.Interop.Excel.Range row4_AB = oSheet.get_Range("AB1");
                row4_AB.ColumnWidth = 15;
                row4_AB.Value2 = "Bù HS T9";
                Microsoft.Office.Interop.Excel.Range row4_AC = oSheet.get_Range("AC1");
                row4_AC.ColumnWidth = 15;
                row4_AC.Value2 = "Đề Nghị T9";


                Microsoft.Office.Interop.Excel.Range row4_AD = oSheet.get_Range("AD1");
                row4_AD.ColumnWidth = 15;
                row4_AD.Value2 = "Bù HĐLĐ T10";
                Microsoft.Office.Interop.Excel.Range row4_AE = oSheet.get_Range("AE1");
                row4_AE.ColumnWidth = 15;
                row4_AE.Value2 = "Bù HS T10";
                Microsoft.Office.Interop.Excel.Range row4_AF = oSheet.get_Range("AF1");
                row4_AF.ColumnWidth = 15;
                row4_AF.Value2 = "Đề Nghị T10";

                Microsoft.Office.Interop.Excel.Range row4_AG = oSheet.get_Range("AG1");
                row4_AG.ColumnWidth = 15;
                row4_AG.Value2 = "Bù HĐLĐ T11";
                Microsoft.Office.Interop.Excel.Range row4_AH = oSheet.get_Range("AH1");
                row4_AH.ColumnWidth = 15;
                row4_AH.Value2 = "Bù HS T11";
                Microsoft.Office.Interop.Excel.Range row4_AI = oSheet.get_Range("AI1");
                row4_AI.ColumnWidth = 15;
                row4_AI.Value2 = "Đề Nghị T11";


                Microsoft.Office.Interop.Excel.Range row4_AJ = oSheet.get_Range("AJ1");
                row4_AJ.ColumnWidth = 15;
                row4_AJ.Value2 = "Bù HĐLĐ T12";
                Microsoft.Office.Interop.Excel.Range row4_AK = oSheet.get_Range("AK1");
                row4_AK.ColumnWidth = 15;
                row4_AK.Value2 = "Bù HS T12";
                Microsoft.Office.Interop.Excel.Range row4_AL = oSheet.get_Range("AL1");
                row4_AL.ColumnWidth = 15;
                row4_AL.Value2 = "Đề Nghị T12";

                Microsoft.Office.Interop.Excel.Range row4_AM = oSheet.get_Range("AM1");
                row4_AM.ColumnWidth = 15;
                row4_AM.Value2 = "Tỷ lệ hoàn thành";
                Microsoft.Office.Interop.Excel.Range row4_AN = oSheet.get_Range("AN1");
                row4_AN.ColumnWidth = 15;
                row4_AN.Value2 = "Xếp loại";
                Microsoft.Office.Interop.Excel.Range row4_AO = oSheet.get_Range("AO1");
                row4_AO.ColumnWidth = 15;
                row4_AO.Value2 = "Hệ số";




                Microsoft.Office.Interop.Excel.Range row4_FormatTieuDe = oSheet.get_Range("A1", "AO1");
                row4_FormatTieuDe.Font.Size = fontSizeTieuDe;
                row4_FormatTieuDe.Font.Name = fontName;
                row4_FormatTieuDe.Font.Bold = true;
                row4_FormatTieuDe.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_FormatTieuDe.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                DataRow[] dr = dtBCLuong.Select();
                string[,] rowData = new string[dr.Length, dtBCLuong.Columns.Count];

                int col = 0;
                int rowCnt = 0;
                foreach (DataRow row in dr)
                {
                    for (col = 0; col < dtBCLuong.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }
                    rowCnt++;
                }
                rowCnt = rowCnt + 1;
                oSheet.get_Range("A2", "AO" + rowCnt.ToString()).Value2 = rowData;
                oSheet.get_Range("A2", "AO" + rowCnt.ToString()).Font.Name = fontName;
                oSheet.get_Range("A2", "AO" + rowCnt.ToString()).Font.Size = fontSizeNoiDung;
                ////Kẻ khung toàn bộ
                //formatRange = oSheet.get_Range("C2", "C" + (rowCnt ).ToString());
                //formatRange.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                //formatRange.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                //formatRange.NumberFormat = "#,##0;(#,##0); ;";
                //try
                //{
                //    formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                //}
                //catch(Exception ex) { }

                Microsoft.Office.Interop.Excel.Range formatRange;
                for (int colFormat = 3; colFormat < dtBCLuong.Columns.Count - 1; colFormat++) // format từ cột t
                {
                    formatRange = oSheet.Range[oSheet.Cells[2, colFormat], oSheet.Cells[dtBCLuong.Rows.Count + 2, colFormat]];
                    formatRange.NumberFormat = "#,##0;(#,##0);;";
                    try
                    {
                        formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                    }
                    catch { }

                }

                this.Cursor = Cursors.Default;

                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }



        private void TinhLuong()
        {
            try
            {

                if (grvData.RowCount != 0)
                {
                    if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_DaCoLuong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                }
                this.Cursor = Cursors.WaitCursor;

                switch (Commons.Modules.KyHieuDV)
                {
                    case "TG":
                        {
                            DataTable dt = new DataTable();
                            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetTinhLuongThang13_TG", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToInt32(cboDonVi.EditValue), Convert.ToInt32(cboXiNghiep.EditValue), Convert.ToInt32(cboTo.EditValue), Convert.ToDateTime(datNam.EditValue).Year));
                            LoadData_TG();
                            break;
                        }
                }

                this.Cursor = Cursors.Default;
                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTinhLuongThanhCong"), Commons.Form_Alert.enmType.Success);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTinhLuongKhongThanhCong"), Commons.Form_Alert.enmType.Error);
                MessageBox.Show(ex.Message);
            }

        }


        private void EnableButon()
        {
        }
        private void grvData_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            //e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {

        }

        private bool Savedata()
        {
            string sTB = "sBTLuongT13" + Commons.Modules.UserName;
            try
            {

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveBangLuongT13", sTB);
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
        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            switch (Commons.Modules.KyHieuDV)
            {
                case "TG":
                    {
                        LoadData_TG();
                        break;
                    }
            }
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            switch (Commons.Modules.KyHieuDV)
            {
                case "TG":
                    {
                        LoadData_TG();
                        break;
                    }
            }
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            switch (Commons.Modules.KyHieuDV)
            {
                case "TG":
                    {
                        LoadData_TG();
                        break;
                    }
            }
            //EnableButon(true);
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            switch (Commons.Modules.KyHieuDV)
            {
                case "TG":
                    {
                        LoadData_TG();
                        break;
                    }
            }
            //EnableButon(true);
        }
        private void TinhTongLuongThueTNCN()
        {
            string sBT = "sBTBangTam" + Commons.Modules.UserName;
            try
            {
                DataTable dt1 = new DataTable();
                dt1 = (DataTable)grdData.DataSource;

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, dt1, "");

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTinhTongLuongThueTNCN", conn);
                cmd.Parameters.Add("@BangTam", SqlDbType.NVarChar).Value = sBT;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = Convert.ToDateTime(datNam.EditValue).Year;
                cmd.Parameters.Add("@Loai", SqlDbType.Int).Value = iLoai;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                grdData.DataSource = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.XoaTable(sBT);
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBT);
            }
        }
        private bool KiemTrong_grvData()
        {
            try
            {
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    //Kiểm trống theo từng cột
                    for (int j = 0; j < grvData.Columns.Count; j++)
                    {
                        if (grvData.Columns[j].FieldName == "PT_TL" && (grvData.GetRowCellValue(i, grvData.Columns[j])).ToString() == "")
                        {
                            XtraMessageBox.Show(grvData.Columns[j].Caption + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongDuocTrong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            grvData.FocusedRowHandle = i;
                            grvData.FocusedColumn = grvData.Columns[j];
                            return true;
                        }

                        if (grvData.Columns[j].FieldName == "PT_HQ_KD" && (grvData.GetRowCellValue(i, grvData.Columns[j])).ToString() == "")
                        {
                            XtraMessageBox.Show(grvData.Columns[j].Caption + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongDuocTrong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            grvData.FocusedRowHandle = i;
                            grvData.FocusedColumn = grvData.Columns[j];
                            return true;
                        }
                    }
                }
            }
            catch { return true; }
            return false;
        }

        private void grvData_MouseWheel(object sender, MouseEventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.GridView view = (sender as DevExpress.XtraGrid.Views.Grid.GridView);
            //view.LeftCoord += e.Delta;
            //(e as DevExpress.Utils.DXMouseEventArgs).Handled = true;
        }
        public string SaveFiles(string MFilter)
        {
            try
            {
                SaveFileDialog f = new SaveFileDialog();
                f.Filter = MFilter;
                f.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                try
                {
                    DialogResult res = f.ShowDialog();
                    if (res == DialogResult.OK)
                        return f.FileName;
                    return "";
                }
                catch
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        private void MExportExcel(DataTable dtTmp, Microsoft.Office.Interop.Excel.Worksheet ExcelSheets, Microsoft.Office.Interop.Excel.Range sRange)
        {
            object[,] rawData = new object[dtTmp.Rows.Count + 1, dtTmp.Columns.Count - 1 + 1];
            for (var col = 0; col <= dtTmp.Columns.Count - 1; col++)
                rawData[0, col] = dtTmp.Columns[col].Caption;
            for (var col = 0; col <= dtTmp.Columns.Count - 1; col++)
            {
                for (var row = 0; row <= dtTmp.Rows.Count - 1; row++)
                    rawData[row + 1, col] = dtTmp.Rows[row][col].ToString();
            }
            sRange.Value = rawData;
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
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "ucBangLuongThang13", "CapNhatPhamTramThuong", Commons.Modules.TypeLanguage);
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
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateChuotPhaiLUONG_T13", sBTCongNhan, sCotCN, Convert.ToDouble(grvData.GetFocusedRowCellValue(grvData.FocusedColumn.FieldName))));
                grdData.DataSource = dt;
            }
            catch (Exception EX) { }
        }

        private void grvData_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (btnALL.Buttons[0].Properties.Visible == true) return;
                if (grvData.FocusedColumn.FieldName.Substring(0, 3) != "PT_") return;
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