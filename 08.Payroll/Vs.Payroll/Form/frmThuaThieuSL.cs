using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vs.Report;

namespace Vs.Payroll.Form
{
    public partial class frmThuaThieuSL : DevExpress.XtraEditors.XtraForm
    {
        public int iID_DV = -1;
        public Int64 iID_CHUYEN = -1;
        public Int64 iID_CHUYEN_SD = -1;
        public Int64 iID_ORD = -1;
        public int iID_DT = -1;
        public int slChot = 0;
        public DateTime Ngay;

        public Int64 iID_CD_TMP = -1;

        public frmThuaThieuSL()
        {
            InitializeComponent();

        }

        #region even
        private void frmThuaThieuSL_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                LoadcboKH_CHUYEN();
                LoadcboORD();

                cboID_DT.EditValue = Convert.ToInt64(iID_DT);
                cboID_ORD.EditValue = iID_ORD;
                cboID_CHUYEN.EditValue = iID_CHUYEN_SD;
                datTNgay.EditValue = Ngay;
                datDNgay.EditValue = Ngay;
                Commons.OSystems.SetDateEditFormat(datTNgay);
                Commons.OSystems.SetDateEditFormat(datDNgay);

                LoadgrvCDThuaThieu();
                LoadgrvCN();
                Commons.Modules.sLoad = "";

                grvCDThuaThieu_FocusedRowChanged(null, null);

                enableButon(true);
                LoadNN();
                LoadSLChot();
                lblThangDoiChieu.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblThangDoiChieu") + " : " + Ngay.ToString("MM/yyyy");
                Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            }
            catch { }
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "import":
                        {
                            frmImportCDChinhSua frm = new frmImportCDChinhSua();
                            frm.dtThang = Ngay;
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                LoadgrvCDThuaThieu();
                                LoadgrvCN();
                                grvCDThuaThieu_FocusedRowChanged(null, null);
                            }
                            else
                            {
                                LoadgrvCDThuaThieu();
                                LoadgrvCN();
                                grvCDThuaThieu_FocusedRowChanged(null, null);
                            }
                            break;
                        }
                    case "sua":
                        {
                            iID_CD_TMP = Convert.ToInt64(grvCNThucHien.GetFocusedRowCellValue("ID_CD"));
                            enableButon(false);
                            break;
                        }

                    case "in":
                        {
                            InThuaThieu();
                            //InDuLieu();
                            break;
                        }

                    case "ghi":
                        {
                            try
                            {

                                grdCNThucHien.MainView.CloseEditor();
                                grvCNThucHien.UpdateCurrentRow();
                                iID_CD_TMP = Convert.ToInt64(grvCNThucHien.GetFocusedRowCellValue("ID_CD"));

                                string sBT_grvCNThucHien = "sBT_grvCNThucHien" + Commons.Modules.UserName;
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT_grvCNThucHien, Commons.Modules.ObjSystems.ConvertDatatable(grdCNThucHien), "");
                                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();
                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spThuaThieuSL", conn);
                                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT_grvCNThucHien;
                                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 4;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.ExecuteNonQuery();
                                LoadgrvCDThuaThieu();
                                enableButon(true);
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message); }
                            break;
                        }

                    case "khongghi":
                        {
                            LoadgrvCDThuaThieu();
                            LoadgrvCN();
                            grvCDThuaThieu_FocusedRowChanged(null, null);
                            enableButon(true);
                            break;
                        }
                    case "thoat":
                        {
                            if (iID_CD_TMP != -1)
                            {
                                DialogResult = DialogResult.OK;
                            }
                            this.Close();
                            break;
                        }

                    default: break;
                }
            }
            catch
            {

            }
        }
        private void cboID_DT_EditValueChanged(object sender, EventArgs e)
        {
            LoadcboORD();
            LoadSLChot();
        }
        private void cboID_ORD_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadSLChot();
            LoadgrvCDThuaThieu();
            LoadgrvCN();
            grvCDThuaThieu_FocusedRowChanged(null, null);
        }
        private void cboID_CHUYEN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadSLChot();
            LoadgrvCDThuaThieu();
            LoadgrvCN();
            grvCDThuaThieu_FocusedRowChanged(null, null);
        }

        private void grvCDThuaThieu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            DataTable dtTmp = new DataTable();
            String sIDCD;
            try
            {
                dtTmp = (DataTable)grdCNThucHien.DataSource;

                string sDK = "";
                sIDCD = "-1";
                try { sIDCD = grvCDThuaThieu.GetFocusedRowCellValue("ID_CD").ToString(); } catch (Exception ex) { }
                if (sIDCD != "-1")
                {
                    sDK = " ID_CD = '" + sIDCD + "' ";
                }
                else
                {
                    sDK = "1 =0 ";
                }

                dtTmp.DefaultView.RowFilter = sDK;
            }
            catch { }
            LoadTextTongLSP();
        }
        #endregion

        #region function
        private void LoadNN()
        {

            Commons.Modules.ObjSystems.ThayDoiNN(this, windowsUIButton);
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(grvCDThuaThieu, this.Name);
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(grvCNThucHien, this.Name);
        }

        private void enableButon(bool visible)
        {
            if (Commons.Modules.ObjSystems.DataTinhTrangBangLuong(iID_DV, Ngay) == 2)
            {
                windowsUIButton.Buttons[0].Properties.Visible = false;
                windowsUIButton.Buttons[1].Properties.Visible = false;
                windowsUIButton.Buttons[2].Properties.Visible = false;
                windowsUIButton.Buttons[4].Properties.Visible = false;
                windowsUIButton.Buttons[5].Properties.Visible = false;

                grvCNThucHien.OptionsBehavior.Editable = false;
            }
            else
            {
                windowsUIButton.Buttons[0].Properties.Visible = visible;
                windowsUIButton.Buttons[1].Properties.Visible = visible;
                windowsUIButton.Buttons[2].Properties.Visible = visible;
                windowsUIButton.Buttons[3].Properties.Visible = visible;
                windowsUIButton.Buttons[4].Properties.Visible = !visible;
                windowsUIButton.Buttons[5].Properties.Visible = !visible;
                windowsUIButton.Buttons[6].Properties.Visible = visible;

                grdCDThuaThieu.Enabled = visible;
                grvCNThucHien.OptionsBehavior.Editable = !visible;
            }
        }

        private void InDuLieu()
        {

            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            frmViewReport frm = new frmViewReport();
            frm.rpt = new rptDSCDThuaThieu(DateTime.Now, cboID_DT.Text, cboID_ORD.Text, "ID_ORD", cboID_CHUYEN.Text);
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spThuaThieuSL", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 5;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Ngay;
                cmd.Parameters.Add("@ID_CHUYEN_SD", SqlDbType.Int).Value = cboID_CHUYEN.EditValue;
                cmd.Parameters.Add("@ID_ORD", SqlDbType.Int).Value = cboID_ORD.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            frm.ShowDialog();
        }
        private void InThuaThieu()
        {
            string sBTCongNhan = "sBTBCLSP" + Commons.Modules.iIDUser;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                Commons.Modules.ObjSystems.ShowWaitForm(this);

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dt;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spThuaThieuSL", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 5;
                cmd.Parameters.Add("@bChon", SqlDbType.Bit).Value = rdoChonThang.SelectedIndex;
                cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = Ngay;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = datTNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDNgay.DateTime;
                cmd.Parameters.Add("@ID_CHUYEN_SD", SqlDbType.Int).Value = cboID_CHUYEN.EditValue;
                cmd.Parameters.Add("@ID_ORD", SqlDbType.Int).Value = cboID_ORD.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();

                if (dt.Rows.Count == 0)
                {
                    this.Cursor = Cursors.Default;
                    Commons.Modules.ObjSystems.HideWaitForm();
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuIn"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Microsoft.Office.Interop.Excel.Application oApp;
                Microsoft.Office.Interop.Excel.Workbook oBook;
                Excel.Worksheet oSheet;

                oApp = new Microsoft.Office.Interop.Excel.Application();
                oApp.Visible = true;

                oBook = oApp.Workbooks.Add();
                oSheet = (Excel.Worksheet)oBook.Worksheets.get_Item(1);

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 16;
                int fontSizeNoiDung = 11;
                int oRow = 1;

                int lastColumn = dt.Columns.Count;

                TaoTTChung(oSheet, 1, 2, 1, 7, 0, 0);


                Microsoft.Office.Interop.Excel.Range row4_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[4, 1], oSheet.Cells[4, lastColumn]];
                row4_TieuDe_BaoCao.Merge();
                row4_TieuDe_BaoCao.Font.Size = fontSizeTieuDe;
                row4_TieuDe_BaoCao.Font.Name = fontName;
                row4_TieuDe_BaoCao.Font.Bold = true;
                row4_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row4_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row4_TieuDe_BaoCao.RowHeight = 30;
                row4_TieuDe_BaoCao.Value2 = rdoChonThang.SelectedIndex != 0 ? "BÁO CÁO SẢN LƯỢNG THỪA THIẾU THÁNG " + Ngay.ToString("MM/yyyy") + "" : (datTNgay.DateTime == datDNgay.DateTime ? "BÁO CÁO SẢN LƯỢNG THỪA THIẾU NGÀY " + datTNgay.Text : "BÁO CÁO SẢN LƯỢNG THỪA THIẾU");

                Microsoft.Office.Interop.Excel.Range row5_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[7, 1], oSheet.Cells[7, 1]];
                row5_TieuDe_BaoCao.Value = "Mã hàng: " + cboID_ORD.Text;
                row5_TieuDe_BaoCao.Font.Size = 12;
                row5_TieuDe_BaoCao.Font.Name = fontName;
                row5_TieuDe_BaoCao.Font.Bold = true;


                oRow = 7;

                oRow++;

                oSheet.Cells[oRow, 1] = "Mã công đoạn";
                oSheet.Cells[oRow, 1].ColumnWidth = 9;
                oSheet.Cells[oRow, 2] = "Tên công đoạn";
                oSheet.Cells[oRow, 2].ColumnWidth = 35;
                oSheet.Cells[oRow, 3] = "Mã thẻ CNV";
                oSheet.Cells[oRow, 3].ColumnWidth = 15;
                oSheet.Cells[oRow, 4] = "Tên CNV";
                oSheet.Cells[oRow, 4].ColumnWidth = 25;
                oSheet.Cells[oRow, 5] = "Sản lượng kê";
                oSheet.Cells[oRow, 5].ColumnWidth = 10;
                oSheet.Cells[oRow, 6] = rdoChonThang.SelectedIndex != 0 ? "Sản lượng chốt tháng" : (datTNgay.DateTime == datDNgay.DateTime ? "Sản lượng chốt ngày" : "Tổng sản lượng chốt ngày");
                oSheet.Cells[oRow, 6].ColumnWidth = 25;
                oSheet.Cells[oRow, 7] = "SL Thừa(-) Thiếu(+)";
                oSheet.Cells[oRow, 7].ColumnWidth = 15;


                Microsoft.Office.Interop.Excel.Range row_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[oRow, 1], oSheet.Cells[oRow, lastColumn]];
                row_TieuDe_BaoCao.Font.Size = fontSizeNoiDung;
                row_TieuDe_BaoCao.Font.Name = fontName;
                row_TieuDe_BaoCao.Font.Bold = true;
                row_TieuDe_BaoCao.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                row_TieuDe_BaoCao.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                row_TieuDe_BaoCao.Cells.WrapText = true;
                BorderAround(row_TieuDe_BaoCao);

                oRow++;
                DataRow[] dr = dt.Select();
                string[,] rowData = new string[dr.Count(), dt.Columns.Count];

                int rowCnt = 0;
                int rowBD = oRow;
                foreach (DataRow row in dt.Rows)
                {
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        rowData[rowCnt, col] = row[col].ToString();
                    }
                    rowCnt++;
                }
                oRow = rowBD + rowCnt - 1;
                oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Value2 = rowData;
                oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Size = fontSizeNoiDung;
                oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]].Font.Name = fontName;
                BorderAround(oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, lastColumn]]);

                Microsoft.Office.Interop.Excel.Range formatRange;


                formatRange = oSheet.Range[oSheet.Cells[rowBD, 5], oSheet.Cells[oRow, 5]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.NumberFormat = "#,##0;(#,###)";
                formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                formatRange = oSheet.Range[oSheet.Cells[rowBD, 6], oSheet.Cells[oRow, 6]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.NumberFormat = "#,##0;(#,###)";
                formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                formatRange = oSheet.Range[oSheet.Cells[rowBD, 7], oSheet.Cells[oRow, 7]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.NumberFormat = "#,##0;(#,###)";
                formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                //can giua

                // mã cd
                formatRange = oSheet.Range[oSheet.Cells[rowBD, 1], oSheet.Cells[oRow, 1]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                formatRange = oSheet.Range[oSheet.Cells[rowBD, 2], oSheet.Cells[oRow, 2]];
                formatRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                // cột thừa thiếu
                formatRange = oSheet.Range[oSheet.Cells[rowBD, 7], oSheet.Cells[oRow, 7]];
                formatRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                formatRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;


                Microsoft.Office.Interop.Excel.Range formatRange1; // range ke tiep
                Microsoft.Office.Interop.Excel.Range formatRange3;
                Microsoft.Office.Interop.Excel.Range formatColumn7;
                Microsoft.Office.Interop.Excel.Range keepColumn7;
                string CurentColumn = string.Empty;
                int rowbd;
                int rowDup = 0; // row bat dau của dữ liệu duplicate
                int colKT = dt.Columns.Count;
                bool bChan = false;
                for (int col = 1; col <= 2; col++) // merge từ cột 1 đến cột 2
                {
                    for (rowbd = 9; rowbd <= oRow; rowbd++)
                    {
                        formatRange = oSheet.Range[oSheet.Cells[rowbd, col], oSheet.Cells[rowbd, col]]; // lấy dữ liệu row bắt đầu
                        formatRange1 = oSheet.Range[oSheet.Cells[rowbd + 1, col], oSheet.Cells[rowbd + 1, col]]; // lấy dữ liệu row kế tiếp
                        keepColumn7 = oSheet.Range[oSheet.Cells[rowbd + 1, 7], oSheet.Cells[rowbd + 1, 7]];

                        if (formatRange.Value == null) // kiểm tra nếu row bắt đầu = null
                        {
                            formatRange = oSheet.Range[oSheet.Cells[rowDup, col], oSheet.Cells[rowDup, col]]; // nếu row bắt đầu  = null thì gán row bắt đầu = rowDup
                            if (col == 2)
                            {
                                keepColumn7 = oSheet.Range[oSheet.Cells[rowDup, 7], oSheet.Cells[rowDup, 7]];
                            }
                        }
                        if (formatRange.Value == formatRange1.Value)
                        {
                            if (bChan == false) // chạy vòng đầu tiên, để gán rowDup bằng rowBD
                            {
                                rowDup = rowbd;
                            }
                            bChan = true;
                            formatRange.Value = null;
                            formatRange3 = oSheet.Range[oSheet.Cells[rowbd, col], oSheet.Cells[rowbd + 1, col]];
                            formatRange3.Merge();

                            if (col == 2)
                            {
                                keepColumn7.Value = null;
                                formatColumn7 = oSheet.Range[oSheet.Cells[rowbd, 7], oSheet.Cells[rowbd + 1, 7]];
                                formatColumn7.Merge();
                            }
                        }
                        else
                        {
                            bChan = false;
                            rowDup = 0;
                        }
                    }
                }


                //// SUM
                //formatRange = oSheet.Range[oSheet.Cells[7, 9], oSheet.Cells[7, 9]];
                //formatRange.Font.Name = fontName;
                //formatRange.Font.Size = 12;
                //formatRange.Font.Bold = true;
                //formatRange.Value = "=SUBTOTAL(9,I" + rowBD + ":I" + oRow.ToString() + ")";
                //formatRange.NumberFormat = "#,##0;(#,##0); ; ";
                //formatRange.TextToColumns(Type.Missing, Microsoft.Office.Interop.Excel.XlTextParsingType.xlDelimited, Microsoft.Office.Interop.Excel.XlTextQualifier.xlTextQualifierDoubleQuote);

                if (datTNgay.DateTime != datDNgay.DateTime && rdoChonThang.SelectedIndex == 0)
                {
                    //Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 2, 6);
                    row5_TieuDe_BaoCao = oSheet.Range[oSheet.Cells[5, 1], oSheet.Cells[5, lastColumn]];
                    row5_TieuDe_BaoCao.Merge();
                    row5_TieuDe_BaoCao.Value = "Từ ngày " + datTNgay.Text + " Đến ngày " + datDNgay.Text;
                    row5_TieuDe_BaoCao.Font.Size = 12;
                    row5_TieuDe_BaoCao.Font.Name = fontName;
                    row5_TieuDe_BaoCao.Font.Bold = true;
                    row5_TieuDe_BaoCao.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    row5_TieuDe_BaoCao.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                }

                this.Cursor = Cursors.Default;
                Commons.Modules.ObjSystems.HideWaitForm();

                oApp.Visible = true;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Commons.Modules.ObjSystems.HideWaitForm();
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void LoadcboKH_CHUYEN()
        {
            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spThuaThieuSL", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = Ngay;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_DT, dt, "ID_DT", "TEN_KH", "TEN_KH");

                dt = new DataTable();
                dt = ds.Tables[1].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_CHUYEN, dt, "ID_TO", "TEN_TO", "TEN_TO", true);
                cboID_CHUYEN.Properties.View.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboID_CHUYEN.Properties.View.Columns[1].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cboID_CHUYEN.Properties.View.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboID_CHUYEN.Properties.View.Columns[0].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch (Exception ex) { }
        }

        private void LoadcboORD()
        {
            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spThuaThieuSL", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@iCot1", SqlDbType.BigInt).Value = Convert.ToInt64(cboID_DT.EditValue);
                cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = Ngay;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();

                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_ORD, dt, "ID_ORD", "TEN_HH", "TEN_HH", true);
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch { }
        }
        private void LoadgrvCDThuaThieu()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spThuaThieuSL", conn);
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = datTNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = datDNgay.DateTime;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Ngay;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                cmd.Parameters.Add("@bChon", SqlDbType.Bit).Value = rdoChonThang.SelectedIndex;
                cmd.Parameters.Add("@ID_CHUYEN_SD", SqlDbType.BigInt).Value = cboID_CHUYEN.EditValue;
                cmd.Parameters.Add("@ID_ORD", SqlDbType.BigInt).Value = cboID_ORD.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CD"] };

                if (grdCDThuaThieu.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdCDThuaThieu, grvCDThuaThieu, dt, false, true, false, true, false, "");
                    grvCDThuaThieu.Columns["ID_CHUYEN_SD"].Visible = false;
                    grvCDThuaThieu.Columns["SL_CHOT"].Visible = false;
                    grvCDThuaThieu.Columns["ID_CD"].Visible = false;

                    grvCDThuaThieu.Columns["SL_TH"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    grvCDThuaThieu.Columns["SL_TH"].DisplayFormat.FormatString = "N0";

                    grvCDThuaThieu.Columns["SL_THUA"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    grvCDThuaThieu.Columns["SL_THUA"].DisplayFormat.FormatString = "N0";

                    grvCDThuaThieu.Columns["SL_THIEU"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    grvCDThuaThieu.Columns["SL_THIEU"].DisplayFormat.FormatString = "N0";

                }
                else
                {
                    grdCDThuaThieu.DataSource = dt;
                }

                if (iID_CD_TMP != -1)
                {
                    try
                    {
                        int index = dt.Rows.IndexOf(dt.Rows.Find(iID_CD_TMP));
                        grvCDThuaThieu.FocusedRowHandle = grvCDThuaThieu.GetRowHandle(index);
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void LoadSLChot()
        {
            try
            {
                if (rdoChonThang.SelectedIndex == 1)
                {
                    slChot = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnGetSLChot('" + Ngay.ToString("MM/dd/yyyy") + "', " + cboID_CHUYEN.EditValue + ", " + cboID_ORD.EditValue + ")"));
                }
                else
                {
                    slChot = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnGetSLChot_Ngay('" + datTNgay.DateTime.ToString("MM/dd/yyyy") + "', '" + datDNgay.DateTime.ToString("MM/dd/yyyy") + "' , " + cboID_CHUYEN.EditValue + ", " + cboID_ORD.EditValue + ")"));
                }
                lblSLChot.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "SL_chot : ") + slChot.ToString("N0");
            }
            catch { }
        }

        private void LoadTextTongLSP()
        {
            try
            {

                DataTable dt1 = new DataTable();
                dt1 = Commons.Modules.ObjSystems.ConvertDatatable(grvCNThucHien);
                dt1.DefaultView.RowFilter = grvCNThucHien.ActiveFilterString.ToString();
                dt1 = dt1.DefaultView.ToTable();
                lbl.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTongSoLuong") + " " + (Convert.ToDouble(dt1.Compute("Sum(SO_LUONG)", "")).ToString("N0") == "" ? "0" : Convert.ToDouble(dt1.Compute("Sum(SO_LUONG)", "")).ToString("N0")).ToString();
            }
            catch
            {
                lbl.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTongSoLuong") + " 0";
            }
        }

        private void LoadgrvCN()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spThuaThieuSL", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Ngay;
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = datTNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = datDNgay.DateTime;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 3;
                cmd.Parameters.Add("@bChon", SqlDbType.Int).Value = rdoChonThang.SelectedIndex;
                cmd.Parameters.Add("@ID_CHUYEN", SqlDbType.BigInt).Value = -1;
                cmd.Parameters.Add("@ID_CHUYEN_SD", SqlDbType.BigInt).Value = cboID_CHUYEN.EditValue;
                cmd.Parameters.Add("@ID_ORD", SqlDbType.BigInt).Value = cboID_ORD.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdCNThucHien.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdCNThucHien, grvCNThucHien, dt, false, true, false, true, false, "");
                    grvCNThucHien.Columns["ID_CHUYEN_TH"].Visible = false;
                    grvCNThucHien.Columns["ID_ORD"].Visible = false;
                    grvCNThucHien.Columns["ID_CD"].Visible = false;

                    grvCNThucHien.Columns["TEN_TO"].OptionsColumn.AllowEdit = false;
                    grvCNThucHien.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                    grvCNThucHien.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                    grvCNThucHien.Columns["NGAY"].OptionsColumn.AllowEdit = false;

                    grvCNThucHien.Columns["SO_LUONG"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    grvCNThucHien.Columns["SO_LUONG"].DisplayFormat.FormatString = "N0";
                }
                else
                {
                    grdCNThucHien.DataSource = dt;
                }
            }
            catch { }
        }
        #endregion

        private void rdoChonThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSLChot();
            if (rdoChonThang.SelectedIndex == 0)
            {
                tableLayoutPanel1.RowStyles[4].Height = 25;
            }
            else
            {
                tableLayoutPanel1.RowStyles[4].Height = 0;
            }
            LoadgrvCDThuaThieu();
            LoadgrvCN();
            grvCDThuaThieu_FocusedRowChanged(null, null);
        }

        private void datTNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadSLChot();
            LoadgrvCDThuaThieu();
            LoadgrvCN();
            grvCDThuaThieu_FocusedRowChanged(null, null);
            enableButon(true);
        }

        private void datDNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadSLChot();
            LoadgrvCDThuaThieu();
            LoadgrvCN();
            grvCDThuaThieu_FocusedRowChanged(null, null);
        }

        private void grvCDThuaThieu_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(grvCDThuaThieu.GetRowCellValue(e.RowHandle, grvCDThuaThieu.Columns["SL_TH"].FieldName)) != 0) return;
                e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFF2CC");
                e.HighPriority = true;
            }
            catch
            {

            }
        }

        public int TaoTTChung(Excel.Worksheet MWsheet, int DongBD, int CotBD, int DongKT, int CotKT, float MLeft, float MTop)
        {
            try
            {
                DataTable dtTmp = Commons.Modules.ObjSystems.DataThongTinChung();
                Microsoft.Office.Interop.Excel.Range CurCell = MWsheet.Range[MWsheet.Cells[DongBD, 1], MWsheet.Cells[DongKT, 1]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);

                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT - 3]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = dtTmp.Rows[0]["TEN_CTY"];

                DongBD += 1;
                DongKT += 1;
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "diachi") + " : " + dtTmp.Rows[0]["DIA_CHI"].ToString();

                DongBD += 1;
                DongKT += 1;
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                CurCell.Merge(true);
                CurCell.Font.Bold = true;
                CurCell.Borders.LineStyle = 0;
                CurCell.Value2 = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "dienthoai") + " : " + dtTmp.Rows[0]["DIEN_THOAI"] + "  " + Commons.Modules.ObjLanguages.GetLanguage("frmChung", "Fax") + " : " + dtTmp.Rows[0]["FAX"].ToString();

                //DongBD += 1;
                //DongKT += 1;
                //CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
                //CurCell.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                //CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
                //CurCell.Merge(true);
                //CurCell.Font.Bold = true;
                //CurCell.Borders.LineStyle = 0;
                //CurCell.Value2 = "Email : " + dtTmp.Rows[0]["EMAIL"];

                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Masters");
                GetImage((byte[])dtTmp.Rows[0]["LOGO"], System.Windows.Forms.Application.StartupPath, "logo.bmp");
                MWsheet.Shapes.AddPicture(System.Windows.Forms.Application.StartupPath + @"\logo.bmp", Office.MsoTriState.msoFalse, Office.MsoTriState.msoCTrue, MLeft, MTop, 50, 50);
                System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + @"\logo.bmp");

                return DongBD + 1;
            }
            catch
            {
                return DongBD + 1;
            }
        }
        public void GetImage(byte[] Logo, string sPath, string sFile)
        {
            try
            {
                string strPath = sPath + @"\" + sFile;
                System.IO.MemoryStream stream = new System.IO.MemoryStream(Logo);
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                img.Save(strPath);
            }
            catch (Exception)
            {
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

        private void grvCDThuaThieu_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                frmCapNhatNhanhThuaThieu frm = new frmCapNhatNhanhThuaThieu();
                frm.iID_CHUYEN_SD = Convert.ToInt64(cboID_CHUYEN.EditValue);
                frm.iID_ORD = Convert.ToInt64(cboID_ORD.EditValue);
                frm.iID_CD = Convert.ToInt64(grvCDThuaThieu.GetFocusedRowCellValue("ID_CD"));
                frm.dTNgay = datTNgay.DateTime;
                frm.dDNgay = datDNgay.DateTime;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadgrvCDThuaThieu();
                    LoadgrvCN();
                }
                else
                {
                    LoadgrvCDThuaThieu();
                    LoadgrvCN();
                }
                grvCDThuaThieu_FocusedRowChanged(null, null);
            }
            catch (Exception ex) { }
        }

        private void grvCNThucHien_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            LoadTextTongLSP();
        }

        private void grvCNThucHien_ColumnFilterChanged(object sender, EventArgs e)
        {
            LoadTextTongLSP();
        }
    }
}
