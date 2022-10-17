﻿using System;
using System.Collections.Generic;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraGrid.Views.Grid;
using System.Linq;
using System.Windows.Forms;
using Excel;
using DataTable = System.Data.DataTable;
using System.Reflection;
using System.Drawing;
using DevExpress.Utils;

namespace Vs.Recruit
{
    public partial class ucDinhBien : DevExpress.XtraEditors.XtraUserControl
    {
        private string SaveExcelFile;
        public static ucDinhBien _instance;
        public static ucDinhBien Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDinhBien();
                return _instance;
            }
        }
        public ucDinhBien()
        {
            InitializeComponent();
            Commons.Modules.sLoad = "0Load";
            datNam.EditValue = DateTime.Now;
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDV, Commons.Modules.ObjSystems.DataDonVi(false), "ID_DV", "TEN_DV", "TEN_DV");
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, windowsUIButton);
            Commons.Modules.sLoad = "";
            LoadgrvDinhBien(-1, "");
        }
        private void LoadgrvDinhBien(Int64 iID_LCV, string KieuLoad)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListDinhBien", datNam.DateTime.Year, cboDV.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, KieuLoad));
                dt.Columns["DINH_BIEN"].ReadOnly = false;
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_LCV"] };
                if (grdDinhBien.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDinhBien, grvDinhBien, dt, false, false, false, true, true, this.Name);


                    DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cboLCV = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();

                    //ID_LCV,TEN_LCV

                    cboLCV.NullText = "";
                    cboLCV.ValueMember = "ID_LCV";
                    cboLCV.DisplayMember = "TEN_LCV";
                    cboLCV.DataSource = Commons.Modules.ObjSystems.DataLoaiCV(false, -1);
                    cboLCV.View.PopulateColumns(cboLCV.DataSource);
                    cboLCV.View.Columns["TEN_LCV"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_LCV");
                    cboLCV.View.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboLCV.View.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    cboLCV.View.Columns["ID_LCV"].Visible = false;
                    grvDinhBien.Columns["ID_LCV"].ColumnEdit = cboLCV;
                    cboLCV.BeforePopup += cboLCV_BeforePopup;
                    cboLCV.EditValueChanged += cboLCV_EditValueChanged;

                    grvDinhBien.Columns["DINH_BIEN"].DisplayFormat.FormatType = FormatType.None;
                    grvDinhBien.Columns["DINH_BIEN"].DisplayFormat.FormatString = Commons.Modules.sSoLeSL;

                    //Commons.Modules.ObjSystems.AddCombXtra("ID_LCV", "TEN_LCV", grvDinhBien, Commons.Modules.ObjSystems.DataLoaiCV(false, Convert.ToInt32(-1)), true, "ID_LCV", this.Name);
                    Commons.Modules.ObjSystems.DeleteAddRow(grvDinhBien);
                }
                else
                {
                    grdDinhBien.DataSource = dt;
                }

                if (iID_LCV != -1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(iID_LCV));
                    grvDinhBien.FocusedRowHandle = grvDinhBien.GetRowHandle(index);
                    grvDinhBien.SelectRow(index);
                }

            }
            catch
            {
            }
        }
        private void cboLCV_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                grvDinhBien.SetFocusedRowCellValue("ID_LCV", Convert.ToUInt64((dataRow.Row[0])));
            }
            catch
            {
            }
        }
        private void cboLCV_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                SearchLookUpEdit lookUp = sender as SearchLookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataLoaiCV(false, -1, Convert.ToInt64(cboDV.EditValue));
                DataTable dt = new DataTable();
                dt = (DataTable)lookUp.Properties.DataSource;
                DataTable dtTmp = new DataTable();
                string sdkien = "( 1 = 1 )";
                try
                {
                    string sID = "";
                    DataTable dtTemp = new DataTable();
                    dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grvDinhBien).Copy();
                    for (int i = 0; i < dtTmp.Rows.Count; i++)
                    {
                        sID = sID + dtTmp.Rows[i]["ID_LCV"].ToString() + ",";
                    }
                    sID = sID.Substring(0, sID.Length - 1);
                    sdkien = "ID_LCV NOT IN (" + sID + ")";
                    dt.DefaultView.RowFilter = sdkien;
                }
                catch
                {
                    try
                    {
                        dtTmp.DefaultView.RowFilter = "";
                    }
                    catch { }
                }

            }
            catch { }
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            if (btn == null || btn.Tag == null) return;
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        enableButon(false);
                        grvDinhBien.Columns["ID_LCV"].OptionsColumn.ReadOnly = false;
                        LoadgrvDinhBien(Convert.ToInt64(grvDinhBien.GetFocusedRowCellValue("ID_LCV")), "THEM");
                        grvDinhBien.OptionsBehavior.Editable = true;
                        grvDinhBien.Columns["ID_LCV"].OptionsColumn.ReadOnly = true;
                        break;
                    }
                case "sua":
                    {
                        enableButon(false);
                        Commons.Modules.ObjSystems.AddnewRow(grvDinhBien, false);
                        grvDinhBien.Columns["ID_LCV"].OptionsColumn.ReadOnly = true;
                        break;
                    }
                case "xoa":
                    {
                        XoaDinhBien();
                        break;
                    }
                case "In":
                    {
                        if (grvDinhBien.RowCount == 0)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuIn);
                            return;
                        }
                        InDinhBien();
                        break;
                    }
                case "luu":
                    {
                        Validate();
                        if (grvDinhBien.HasColumnErrors) return;
                        try
                        {
                            string sbt = "sBTDB" + Commons.Modules.iIDUser;
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, Commons.Modules.ObjSystems.ConvertDatatable(grvDinhBien), "");
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spGetListDinhBien", datNam.DateTime.Year, cboDV.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, sbt);
                        }
                        catch
                        {
                            return;
                        }
                        enableButon(true);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvDinhBien);
                        LoadgrvDinhBien(Convert.ToInt64(grvDinhBien.GetFocusedRowCellValue("ID_LCV")), "");
                        break;
                    }
                case "khongluu":
                    {
                        enableButon(true);
                        LoadgrvDinhBien(Convert.ToInt64(grvDinhBien.GetFocusedRowCellValue("ID_LCV")), "");
                        Commons.Modules.ObjSystems.DeleteAddRow(grvDinhBien);
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
        private void InDinhBien()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                DataTable dtBCThang;

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptBangDinhBienLD", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.BigInt).Value = Convert.ToInt64(cboDV.EditValue);
                cmd.Parameters.Add("@NAM", SqlDbType.Int).Value = Convert.ToInt32(datNam.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dtBCThang = new DataTable();
                dtBCThang = ds.Tables[0].Copy();

                SaveExcelFile = SaveFiles("Excel Workbook |*.xlsx|Excel 97-2003 Workbook |*.xls|Word Document |*.docx|Rich Text Format |*.rtf|PDF File |*.pdf|Web Page |*.html|Single File Web Page |*.mht");
                if (SaveExcelFile == "")
                {
                    return;
                }
                Excel.Application oXL;
                Excel.Workbook oWB;
                Excel.Worksheet oSheet;
                oXL = new Excel.Application();
                oXL.Visible = true;

                oWB = (Excel.Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel.Worksheet)oWB.ActiveSheet;

                string fontName = "Times New Roman";
                int fontSizeTieuDe = 18;
                int fontSizeNoiDung = 11;

                int DONG = 0;
                DONG = Commons.Modules.MExcel.TaoTTChung(oSheet, 1, 2, 1, 8, 0, 0);
                DONG = 4;

                string lastColumn = string.Empty;
                lastColumn = CharacterIncrement(dtBCThang.Columns.Count - 2);


                Excel.Range row1_TieuDe = oSheet.get_Range("A" + (DONG + 1).ToString() + "", "E" + (DONG + 1).ToString() + "");
                row1_TieuDe.Merge();
                row1_TieuDe.Font.Bold = true;
                row1_TieuDe.Value2 = "CÔNG TY CỔ PHẦN MAY DUY MINH";
                row1_TieuDe.Font.Size = fontSizeTieuDe;
                row1_TieuDe.Font.Name = fontName;
                row1_TieuDe.WrapText = false;
                row1_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row1_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                oXL.Visible = true;

                Range row2_TieuDe = oSheet.get_Range("A" + (DONG + 2).ToString() + "", "E" + (DONG + 3).ToString() + "");
                row2_TieuDe.Merge();
                row2_TieuDe.Font.Size = fontSizeTieuDe;
                row2_TieuDe.Font.Name = fontName;
                row2_TieuDe.Font.Bold = true;
                row2_TieuDe.Value2 = "BẢNG ĐỊNH BIÊN LAO ĐỘNG";
                row2_TieuDe.WrapText = false;
                row2_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row2_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                Range rowSubHeader1 = oSheet.get_Range("A" + (DONG + 4).ToString() + "", "B" + (DONG + 4).ToString() + "");
                rowSubHeader1.Merge();
                rowSubHeader1.Font.Name = fontName;
                rowSubHeader1.Font.Bold = true;
                rowSubHeader1.Value2 = "Năm :" + datNam.Text;
                rowSubHeader1.WrapText = false;
                rowSubHeader1.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowSubHeader1.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;


                Range rowSubHeader2 = oSheet.get_Range("C" + (DONG + 4).ToString() + "", "E" + (DONG + 4).ToString() + "");
                rowSubHeader2.Merge();
                rowSubHeader2.Font.Name = fontName;
                rowSubHeader2.Font.Bold = true;
                rowSubHeader2.Value2 = "Nhà máy :" + cboDV.Text;
                rowSubHeader2.WrapText = false;
                rowSubHeader2.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                rowSubHeader2.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;


                DONG = 6;

                Range row5_TieuDe_Format = oSheet.get_Range("A" + (DONG + 4).ToString() + "", "E" + (DONG + 4).ToString() + ""); //27 + 31
                row5_TieuDe_Format.Font.Size = fontSizeNoiDung;
                row5_TieuDe_Format.Font.Name = fontName;
                row5_TieuDe_Format.Font.Bold = true;
                row5_TieuDe_Format.WrapText = true;
                row5_TieuDe_Format.NumberFormat = "@";
                row5_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                row5_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                Range row5_TieuDe_Stt = oSheet.get_Range("A" + (DONG + 4).ToString() + "");
                row5_TieuDe_Stt.Value2 = "Stt";
                row5_TieuDe_Stt.ColumnWidth = 9;

                Range row5_TieuDe_MaSo = oSheet.get_Range("B" + (DONG + 4).ToString() + "");
                row5_TieuDe_MaSo.Value2 = "TỔNG SỐ LAO ĐỘNG";
                row5_TieuDe_MaSo.ColumnWidth = 32;
                row5_TieuDe_MaSo.Font.Color = Color.FromArgb(255, 0, 0);

                Range row6_TieuDe_MaSo = oSheet.get_Range("C" + (DONG + 4).ToString() + "");
                row6_TieuDe_MaSo.Value2 = "Định biên";
                row6_TieuDe_MaSo.ColumnWidth = 12;

                Range row5_TieuDe_HoTen = oSheet.get_Range("D" + (DONG + 4).ToString() + "");
                row5_TieuDe_HoTen.Value2 = "Số lượng hiện tại";
                row5_TieuDe_HoTen.ColumnWidth = 15;

                Range row6_TieuDe_HoTen = oSheet.get_Range("E" + (DONG + 4).ToString() + "");
                row6_TieuDe_HoTen.Value2 = "Thừa thiếu";
                row6_TieuDe_HoTen.ColumnWidth = 20;

                int col = 0;
                int rowCnt = 0;
                int keepRowCnt = 0; // Biến này dùng để lưu lại giá trị của biến rowCnt
                int dr_Cu = 0; // Count số nhân viên của xí nghiệp đổ dữ liệu trước
                int current_dr = 0; // Count số nhân viên của xí nghiệp đang được đổ dữ liệu
                int rowBD_XN = 0; // Row để insert dòng xí nghiệp
                int rowCONG = 0; // Row để insert dòng tổng
                //int rowBD_XN = 7; // Row bắt đầu đổ dữ liệu group XI_NGHIEP
                int rowBD = 5 + DONG;
                string[] TEN_CV = dtBCThang.AsEnumerable().Select(r => r.Field<string>("TEN_CV")).Distinct().ToArray();
                string chanVongDau = "Chan";// chặn lần đầu để lần đầu tiên sẽ load data từ cột số 7 trở đi, các vòng lặp tiếp theo bỏ chặn
                DataTable dt_temp = new DataTable();
                dt_temp = ds.Tables[0].Copy(); // Dữ row count data

                for (int i = 0; i < TEN_CV.Count(); i++)
                {
                    dtBCThang = ds.Tables[0].Copy();
                    dtBCThang = dtBCThang.AsEnumerable().Where(r => r.Field<string>("TEN_CV") == TEN_CV[i]).CopyToDataTable().Copy();
                    DataRow[] dr = dtBCThang.Select();
                    current_dr = dr.Count();
                    string[,] rowData = new string[dr.Count(), dtBCThang.Columns.Count];
                    foreach (DataRow row in dr)
                    {
                        for (col = 0; col < dtBCThang.Columns.Count; col++)
                        {
                            rowData[rowCnt, col] = row[col].ToString();
                        }
                        rowCnt++;
                    }
                    if (chanVongDau == "Chan") // Chạy vòng đầu tiên, rowBD_XN = 0, vì nó nằm dòng đầu tiên thì rowBD lúc này sẽ  = 7, các vòng tiếp theo sẽ lấy cái dòng BĐ của + thêm rowBD_XN = 1 vào để không bị nằm đè lên dòng thứ 9
                    {
                        dr_Cu = 0;
                        rowBD_XN = 0;
                        chanVongDau = "";
                    }
                    else
                    {
                        rowBD_XN = 1;
                    }
                    rowBD = rowBD + dr_Cu + rowBD_XN;
                    //rowCnt = rowCnt + 6 + dr_Cu;
                    rowCnt = rowBD + current_dr - 1;

                    // Tạo group xí nghiệp
                    Range row_groupXI_NGHIEP_Format = oSheet.get_Range("A" + rowBD + "".ToString(), lastColumn + "" + rowBD + "".ToString()); //27 + 31
                    row_groupXI_NGHIEP_Format.Interior.Color = Color.FromArgb(217, 225, 242);
                    row_groupXI_NGHIEP_Format.Font.Bold = true;
                    //oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 2]].Merge();
                    //oSheet.Range[oSheet.Cells[Convert.ToInt32(rowBD), 1], oSheet.Cells[Convert.ToInt32(rowBD), 2]].Font.Bold = true;
                    oSheet.Cells[rowBD, 2] = TEN_CV[i] == null ? "" : TEN_CV[i].ToString();

                    //Đổ dữ liệu của xí nghiệp
                    oSheet.get_Range("A" + (rowBD + 1) + "", lastColumn + (rowCnt + 1).ToString()).Value2 = rowData;


                    for (int colSUM = 3; colSUM < dtBCThang.Columns.Count - 1; colSUM++)
                    {
                        oSheet.Cells[rowBD, colSUM] = "=SUM(" + CellAddress(oSheet, rowBD + 1, colSUM) + ":" + CellAddress(oSheet, (rowBD + current_dr), colSUM) + ")";
                    }

                    for (int rowG = rowBD; rowG < (rowBD + current_dr + 1); rowG++)
                    {
                        oSheet.Cells[rowG, 5] = "=C" + rowG + "-D" + rowG + "";
                    }
                    dr_Cu = current_dr;
                    keepRowCnt = rowCnt;
                    rowCnt = 0;
                }

                Excel.Range formatRange;
                rowCnt = keepRowCnt + 1; // Cộng 2 vì ở trên thêm 2 dòng xí nghiệp và cộng
                //formatRange = oSheet.get_Range("G7", "G" + rowCnt.ToString());
                //formatRange.NumberFormat = "dd/MM/yyyy";
                //formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                //formatRange = oSheet.get_Range("H7", "H" + rowCnt.ToString());
                //formatRange.NumberFormat = "dd/MM/yyyy";
                //formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                //formatRange = oSheet.get_Range("I7", lastColumNgay + rowCnt.ToString());
                //formatRange.NumberFormat = "@";
                //formatRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                //dịnh dạng
                //Commons.Modules.MExcel.ThemDong(oSheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, 1, 7);

                string CurentColumn = string.Empty;
                int colBD = 2;
                //format
                for (col = colBD; col < dtBCThang.Columns.Count; col++)
                {
                    CurentColumn = CharacterIncrement(col);
                    formatRange = oSheet.get_Range(CurentColumn + "6", CurentColumn + rowCnt.ToString());
                    //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                    formatRange.NumberFormat = "0.00;-0;;@";
                    try { formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote); } catch { }
                }

                //colKT++;
                //CurentColumn = CharacterIncrement(colKT);
                //formatRange = oSheet.get_Range(CurentColumn + "7", CurentColumn + rowCnt.ToString());
                //formatRange.NumberFormat = "#,##0.00;(#,##0.00); ; ";
                ////formatRange.TextToColumns(Type.Missing, Excel.XlTextParsingType.xlDelimited, Excel.XlTextQualifier.xlTextQualifierDoubleQuote);
                ////Kẻ khung toàn bộ
                formatRange = oSheet.get_Range("A" + (DONG + 5).ToString() + "", lastColumn + rowCnt.ToString());
                formatRange.Font.Name = fontName;
                formatRange.Font.Size = fontSizeNoiDung;
                BorderAround(oSheet.get_Range("A" + (DONG + 4).ToString() + "", lastColumn + rowCnt.ToString()));
                // filter
                //oSheet.Application.ActiveWindow.SplitRow = 4;
                //oSheet.Application.ActiveWindow.FreezePanes = true;
                oXL.Visible = true;
                oXL.UserControl = true;

                oWB.SaveAs(SaveExcelFile,
                    AccessMode: Excel.XlSaveAsAccessMode.xlShared);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
        private void ucDinhBien_Load(object sender, EventArgs e)
        {
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
        }
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = false;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = visible;
            windowsUIButton.Buttons[6].Properties.Visible = !visible;
            windowsUIButton.Buttons[7].Properties.Visible = !visible;
            windowsUIButton.Buttons[8].Properties.Visible = visible;
            datNam.ReadOnly = !visible;
            cboDV.ReadOnly = !visible;
        }

        private void grvDinhBien_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            try
            {
                e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
            }
            catch
            { }
        }
        private void datNam_EditValueChanged(object sender, EventArgs e)
        {
            LoadgrvDinhBien(-1, "");
        }
        private void XoaDinhBien()
        {
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteDinhBien"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.DINH_BIEN WHERE NAM = " + Convert.ToInt32(datNam.DateTime.Year) + " AND ID_DV = " + Convert.ToInt32(cboDV.EditValue) + " AND ID_LCV = " + Convert.ToInt32(grvDinhBien.GetFocusedRowCellValue("ID_LCV")) + "");
                grvDinhBien.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void grvDinhBien_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                grvDinhBien.ClearColumnErrors();
                DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                if (Commons.Modules.ObjSystems.IsnullorEmpty(view.GetRowCellValue(e.RowHandle, "ID_LCV")))
                {
                    e.Valid = false;
                    e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erLDVKhongTrong");
                    view.SetColumnError(view.Columns["ID_LCV"], e.ErrorText);
                    return;
                }
                if (Commons.Modules.ObjSystems.IsnullorEmpty(view.GetRowCellValue(e.RowHandle, "DINH_BIEN")))
                {
                    e.Valid = false;
                    e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erSoLuongKhongTrong");
                    view.SetColumnError(view.Columns["DINH_BIEN"], e.ErrorText);
                    return;
                }
                else
                {
                    //không nhỏ hơn không
                    if (Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "DINH_BIEN")) <= 0)
                    {
                        e.Valid = false;
                        e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erSoLuongNhoHonKhong");
                        view.SetColumnError(view.Columns["DINH_BIEN"], e.ErrorText);
                        return;
                    }
                }
            }
            catch
            {
            }
        }
        private void grdDinhBien_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                XoaDinhBien();
            }
        }
        private void grvDinhBien_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {

                GridView view = sender as GridView;
                if (view == null) return;
                var row = view.GetFocusedDataRow();

                if (e.Column.Name == "colSL_CHUYEN")
                {
                    double sl = Convert.ToDouble(row["SL_CHUYEN"].ToString());
                    double db = Convert.ToDouble(row["DINH_BIEN"].ToString());
                    if (sl != null && db != null)
                    {
                        double tongso = Convert.ToDouble(db * sl);
                        row["TONG_SO"] = tongso;
                        //view.SetRowCellValue(e.RowHandle, view.Columns["TONG_SO"], tongso);
                    }
                }
                if (e.Column.Name == "colDINH_BIEN")
                {
                    double sl = Convert.ToDouble(row["SL_CHUYEN"].ToString());
                    double db = Convert.ToDouble(row["DINH_BIEN"].ToString());
                    if (sl != null && db != null)
                    {
                        double tongso = Convert.ToDouble(db * sl);
                        row["TONG_SO"] = tongso;
                        //view.SetRowCellValue(e.RowHandle, view.Columns["TONG_SO"], tongso);
                    }
                }

            }
            catch
            {

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
        private string CellAddress(Worksheet sht, int row, int col)
        {
            return RangeAddress(sht.Cells[row, col]);
        }
        private string RangeAddress(Range rng)
        {
            object missing = null;
            return rng.get_AddressLocal(false, false, Excel.XlReferenceStyle.xlA1,
                   missing, missing);
        }
        private void BorderAround(Range range)
        {
            Borders borders = range.Borders;
            borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
            borders.Color = Color.Black;
            borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;
            borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
            borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;
        }

    }
}
