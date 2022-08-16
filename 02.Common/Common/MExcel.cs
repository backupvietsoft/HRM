using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Microsoft.ApplicationBlocks.Data;
using OfficeOpenXml;
using System.Windows.Forms;
using System.Data;
using OfficeOpenXml.Style;
using DevExpress.XtraEditors;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Reflection;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using DevExpress.XtraGrid.Views.Grid;

public class MExcel
{
    //private string sFile = "";
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
        catch (Exception)
        {
            return "";
        }
    }
    public string SaveFiles(string MFilter, string MDefault)
    {
        try
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = MFilter;
            f.FileName = MDefault + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
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
        catch (Exception)
        {
            return "";
        }
    }
    public string TimDiemExcel(int Dong, int Cot)
    {
        string sTmp;
        try
        {
            sTmp = "";
            if (Cot > 26)
            {
                sTmp = char.ConvertFromUtf32((Cot - 1) / 26 + 64);

                sTmp = sTmp + char.ConvertFromUtf32((Cot - 1) % 26 + 65);
            }
            else
                sTmp = char.ConvertFromUtf32(Cot + 64);
            if (Dong <= 0)
                sTmp = sTmp;
            else
                sTmp = sTmp + Convert.ToString(Dong);
            return sTmp;
        }
        catch (Exception)
        {
            return "";
        }
    }

    public int MCot(string sCot)
    {
        int sStmp = 0;
        try
        {
            for (int i = 0; i <= sCot.Length - 1; i++)
            {
                if (sStmp == 0)
                    sStmp = MTimCot(sCot.Substring(i, 1));
                else
                    sStmp = sStmp + MTimCot(sCot.Substring(i, 1));
            }
        }
        catch (Exception)
        {
        }
        return sStmp;
    }

    private int MTimCot(string sCot)
    {
        int sTmp = 0;
        try
        {
            if (sCot == "!")
                return 1;
            if (sCot == "@")
                return 2;
            if (sCot == "#")
                return 3;
            if (sCot == "$")
                return 4;
            if (sCot == "%")
                return 5;
            if (sCot == "^")
                return 6;
            if (sCot == "&")
                return 7;
            if (sCot == "*")
                return 8;
            if (sCot == "(")
                return 9;
            if (sCot == ")")
                return 0;
        }
        catch (Exception)
        {
        }
        return sTmp;
    }
    public string getValueCell(Excel.Worksheet MWsheet,int DongBD, int CotBD)
    {
        string resulst = MWsheet.Cells[DongBD, CotBD].Value;
        MWsheet.Cells[DongBD, CotBD].Value2 ="";
        return resulst;

    }    
    public void MFuntion(Excel.Worksheet MWsheet, string MFuntion, int DongBD, int CotBD, int DongBDFuntion, int CotBDFuntion, float MFontSize, bool MFontBold, float MColumnWidth, string MNumberFormat)
    {
        try
        {
            MWsheet.Cells[DongBD, CotBD].Value2 = "=" + MFuntion + "(" + TimDiemExcel(DongBDFuntion, CotBDFuntion) + ")";
            if (MFontSize > 0)
                MWsheet.Cells[DongBD, CotBD].Font.Size = MFontSize;
            MWsheet.Cells[DongBD, CotBD].ColumnWidth = MColumnWidth;
            MWsheet.Cells[DongBD, CotBD].Font.Bold = MFontBold;
            MWsheet.Cells[DongBD, CotBD].NumberFormat = MNumberFormat;
        }
        catch (Exception)
        {
        }
    }



    public void MFuntion(Excel.Worksheet MWsheet, string MFuntion, int DongBD, int CotBD, int DongBDFuntion, int CotBDFuntion, float MFontSize, bool MFontBold, float MColumnWidth, string MNumberFormat, Excel.XlHAlign MHAlign, Excel.XlVAlign MVAlign)
    {
        try
        {
            MWsheet.Cells[DongBD, CotBD].Value2 = "=" + MFuntion + "(" + TimDiemExcel(DongBDFuntion, CotBDFuntion) + ":" + ")";
            if (MFontSize > 0)
                MWsheet.Cells[DongBD, CotBD].Font.Size = MFontSize;
            MWsheet.Cells[DongBD, CotBD].ColumnWidth = MColumnWidth;
            MWsheet.Cells[DongBD, CotBD].Font.Bold = MFontBold;
            MWsheet.Cells[DongBD, CotBD].NumberFormat = MNumberFormat;
            MWsheet.Cells[DongBD, CotBD].HorizontalAlignment = MHAlign;
            MWsheet.Cells[DongBD, CotBD].VerticalAlignment = MVAlign;
        }
        catch (Exception)
        {
        }
    }


    public void MFuntion(Excel.Worksheet MWsheet, string MFuntion, int DongBD, int CotBD, int DongKT, int CotKT, int DongBDFuntion, int CotBDFuntion, int DongKTFuntion, int CotKTFuntion, float MFontSize, bool MFontBold, float MColumnWidth, string MNumberFormat)
    {
        try
        {
            Excel.Range MRange = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
            MRange.Value2 = "=" + MFuntion + "(" + TimDiemExcel(DongBDFuntion, CotBDFuntion) + ":" + TimDiemExcel(DongKTFuntion, CotKTFuntion) + ")";
            if (MFontSize > 0)
                MRange.Font.Size = MFontSize;
            MRange.ColumnWidth = MColumnWidth;
            MRange.Font.Bold = MFontBold;
            MRange.NumberFormat = MNumberFormat;
        }
        catch (Exception)
        {
        }
    }

    public void MFuntion(Excel.Worksheet MWsheet, string MFuntion, int DongBD, int CotBD, int DongKT, int CotKT, int DongBDFuntion, int CotBDFuntion, int DongKTFuntion, int CotKTFuntion, float MFontSize, bool MFontBold, float MColumnWidth, string MNumberFormat, Excel.XlHAlign MHAlign, Excel.XlVAlign MVAlign)
    {
        try
        {
            Excel.Range MRange = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
            MRange.Value2 = "=" + MFuntion + "(" + TimDiemExcel(DongBDFuntion, CotBDFuntion) + ":" + TimDiemExcel(DongKTFuntion, CotKTFuntion) + ")";
            if (MFontSize > 0)
                MRange.Font.Size = MFontSize;
            MRange.ColumnWidth = MColumnWidth;
            MRange.Font.Bold = MFontBold;
            MRange.NumberFormat = MNumberFormat;
            MRange.HorizontalAlignment = MHAlign;
            MRange.VerticalAlignment = MVAlign;
        }
        catch (Exception)
        {
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

    public void TaoLogo(Excel.Worksheet MWsheet, float MLeft, float MTop, float MWidth, float MHeight, string sPath)
    {
        try
        {
            System.Data.DataTable dtTmp = new System.Data.DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, System.Data.CommandType.Text, " SELECT LOGO FROM THONG_TIN_CHUNG"));
            System.Data.DataView dv = new System.Data.DataView(dtTmp);
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Masters");
            GetImage((byte[])dv[0]["LOGO"], sPath, "logo.bmp");
            //MWsheet.Shapes.AddPicture(sPath + @"\logo.bmp",Office.MsoTriState.msoFalse, Office.MsoTriState.msoCTrue, MLeft, MTop, MWidth, MHeight);

            System.IO.File.Delete(sPath + @"\logo.bmp");
        }
        catch
        {
        }
    }
    public void ThemDong(Excel.Worksheet MWsheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection DangThem, int SoDongThem, int DongBDThem)
    {
        try
        {
            Excel.Range MRange = MWsheet.Range[MWsheet.Cells[DongBDThem, 1], MWsheet.Cells[DongBDThem, 1]];
            for (int i = 1; i <= SoDongThem; i++)
                MRange.EntireRow.Insert(DangThem);
        }
        catch
        {
        }
    }

    public void ThemCot(Excel.Worksheet MWsheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection DangThem, int SoCotThem, int CotBDThem)
    {
        try
        {
            Excel.Range MRange = MWsheet.Range[MWsheet.Cells[1, CotBDThem], MWsheet.Cells[1, CotBDThem]];
            for (int i = 1; i <= SoCotThem; i++)
                MRange.EntireColumn.Insert(DangThem);
        }
        catch
        {
        }
    }

    //public void ThemDong(Excel.Worksheet MWsheet, Microsoft.Office.Interop.Excel.XlInsertShiftDirection DangThem, int SoDongThem, int DongBDThem)
    //{
    //    try
    //    {
    //        Excel.Range MRange = MWsheet.Range[MWsheet.Cells[DongBDThem, 1], MWsheet.Cells[DongBDThem, 1]];
    //        for (int i = 1; i <= SoDongThem; i++)
    //            MRange.EntireRow.Insert(DangThem);
    //    }
    //    catch
    //    {
    //    }
    //}

    //public void ThemDong(Excel.Worksheet MWsheet, Excel.XlInsertShiftDirection DangThem, int SoDongThem, int DongBDThem)
    //{
    //    try
    //    {
    //        Excel.Range MRange = MWsheet.Range[MWsheet.Cells[DongBDThem, 1], MWsheet.Cells[DongBDThem, 1]];
    //        for (int i = 1; i <= SoDongThem; i++)
    //            MRange.EntireRow.Insert(DangThem);
    //    }
    //    catch
    //    {
    //    }
    //}


    public void ColumnWidth(Excel.Worksheet MWsheet, float MColumnWidth, string MNumberFormat, bool MWrapText, int DongBD, int CotBD, int DongKT, int CotKT)
    {
        try
        {
            Excel.Range MRange = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
            MRange.ColumnWidth = MColumnWidth;
            if (MNumberFormat != "")
                MRange.NumberFormat = MNumberFormat;
            MRange.WrapText = MWrapText;
        }
        catch (Exception)
        {
        }
    }

    public int TaoTTChung(Excel.Worksheet MWsheet, int DongBD, int CotBD, int DongKT, int CotKT)
    {
        try
        {
            System.Data.DataTable dtTmp = new System.Data.DataTable();
            string sSql = "";
            sSql = " SELECT CASE WHEN " + Commons.Modules.TypeLanguage + "=0 " + " THEN TEN_CTY_TIENG_VIET ELSE TEN_CTY_TIENG_ANH END AS TEN_CTY,LOGO, " + " CASE WHEN " + Commons.Modules.TypeLanguage + "=0 THEN DIA_CHI_VIET  ELSE DIA_CHI_ANH  END AS DIA_CHI,Phone," + " Fax,EMAIL FROM THONG_TIN_CHUNG ";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, System.Data.CommandType.Text, sSql));

            if (dtTmp.Rows.Count == 0 & Commons.Modules.sPrivate.ToUpper() == "GREENFEED")
            {
                sSql = " SELECT CASE WHEN " + Commons.Modules.TypeLanguage + "=0 " + " THEN TEN_CTY_TIENG_VIET ELSE TEN_CTY_TIENG_ANH END AS TEN_CTY,LOGO, " + " CASE WHEN " + Commons.Modules.TypeLanguage + "=0 THEN DIA_CHI_VIET  ELSE DIA_CHI_ANH  END AS DIA_CHI,Phone," + " Fax,EMAIL FROM THONG_TIN_CHUNG ";
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, System.Data.CommandType.Text, sSql));
            }

            Excel.Range CurCell = MWsheet.Range[MWsheet.Cells[DongBD, 1], MWsheet.Cells[DongKT, 1]];
            CurCell.EntireRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown);


            CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotKT - 2], MWsheet.Cells[DongKT, CotKT]];
            CurCell.Merge(true);
            CurCell.Font.Bold = true;
            CurCell.Borders.LineStyle = 0;
            CurCell.Value2 = "Ngày in:" + DateTime.Today.ToString("dd/MM/yyyy");
            CurCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            CurCell.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

            CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT - 3]];
            CurCell.Merge(true);
            CurCell.Font.Bold = true;
            CurCell.Borders.LineStyle = 0;
            CurCell.Value2 = dtTmp.Rows[0]["TEN_CTY"];



            DongBD += 1;
            DongKT += 1;
            CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
            CurCell.EntireRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown);
            CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
            CurCell.Merge(true);
            CurCell.Font.Bold = true;
            CurCell.Borders.LineStyle = 0;
            CurCell.Value2 = (Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "frmReportBaoTri_Huda", "diachi", Commons.Modules.TypeLanguage) + " : ") + dtTmp.Rows[0]["DIA_CHI"];

            DongBD += 1;
            DongKT += 1;
            CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
            CurCell.EntireRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown);
            CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
            CurCell.Merge(true);
            CurCell.Font.Bold = true;
            CurCell.Borders.LineStyle = 0;
            CurCell.Value2 = ((Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "frmReportBaoTri_Huda", "dienthoai", Commons.Modules.TypeLanguage) + " : ") + dtTmp.Rows[0]["phone"] + "  " + Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "frmReportBaoTri_Huda", "fax", Commons.Modules.TypeLanguage) + " : ") + dtTmp.Rows[0]["FAX"];

            DongBD += 1;
            DongKT += 1;
            CurCell = MWsheet.Range[MWsheet.Cells[DongBD, "A"], MWsheet.Cells[DongKT, "A"]];
            CurCell.EntireRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown);
            CurCell = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
            CurCell.Merge(true);
            CurCell.Font.Bold = true;
            CurCell.Borders.LineStyle = 0;
            CurCell.Value2 = "Email : " + dtTmp.Rows[0]["EMAIL"];
            return DongBD + 1;
        }
        catch
        {
            return DongBD + 1;
        }
    }

    //public int TaoShape(int DongBD, Excel.Worksheet MWsheet, float MLeft, float MTop, float MWidth, float MHeight, string sText, string MFontName, float MFontSize, bool MFontBold, Excel.XlHAlign MHAlign, Excel.XlVAlign MVAlign)
    //{
    //    try
    //    {
    //        System.Data.DataTable dtTmp = new System.Data.DataTable();
    //        dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, System.Data.CommandType.Text, " SELECT CASE WHEN " + Commons.Modules.TypeLanguage + "=0 " + " THEN TEN_CTY_TIENG_VIET ELSE TEN_CTY_TIENG_ANH END AS TEN_CTY,LOGO, " + " CASE WHEN " + Commons.Modules.TypeLanguage + "=0 THEN DIA_CHI_VIET  ELSE DIA_CHI_ANH  END AS DIA_CHI,Phone," + " Fax,EMAIL FROM THONG_TIN_CHUNG "));
    //        if (sText == "-1")
    //            sText = dtTmp.Rows[0]["TEN_CTY"].ToString() + Constants.vbCrLf + (Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "frmReportBaoTri_Huda", "diachi", Commons.Modules.TypeLanguage) + " : ") + dtTmp.Rows[0]["DIA_CHI"].ToString() + Constants.vbCrLf + ((Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "frmReportBaoTri_Huda", "dienthoai", Commons.Modules.TypeLanguage) + " : ") + dtTmp.Rows[0]["phone"].ToString() + "  " + Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "frmReportBaoTri_Huda", "fax", Commons.Modules.TypeLanguage) + " : ") + dtTmp.Rows[0]["FAX"].ToString() + Constants.vbCrLf + "Email : " + dtTmp.Rows[0]["EMAIL"].ToString();

    //        Excel.Shape aaa;
    //        aaa = MWsheet.Shapes.AddTextbox(Office.MsoTextOrientation.msoTextOrientationHorizontal, MLeft, MTop, MWidth, MHeight);
    //        aaa.TextFrame.Characters().Text = sText;
    //        aaa.TextFrame.Characters().Font.Name = MFontName;
    //        aaa.TextFrame.Characters().Font.Size = MFontSize;
    //        aaa.TextFrame.VerticalAlignment = MVAlign;
    //        aaa.TextFrame.HorizontalAlignment = MHAlign;
    //        aaa.Line.ForeColor.RGB = Information.RGB(255, 255, 255);
    //        return DongBD + 4;
    //    }
    //    catch
    //    {
    //        return DongBD + 4;
    //    }
    //}


    public void ExcelEnd(Excel.Application MApp, Excel.Workbook MWbook, Excel.Worksheet MWsheet, bool MVisible, bool MDisplayGridlines, bool MRowFit, bool MColumnsFit, Excel.XlPaperSize MPaperSize, Excel.XlPageOrientation MOrientation, float MTopMargin, float MBottomMargin, float MLeftMargin, float MRightMargin, float MHeaderMargin, float MFooterMargin, float MZoom)
    {
        try
        {
            if (MColumnsFit == true)
                MWsheet.Columns.AutoFit();
            if (MRowFit == true)
                MWsheet.Rows.AutoFit();
            MApp.ActiveWindow.DisplayGridlines = MDisplayGridlines;
            MWsheet.PageSetup.PaperSize = MPaperSize;
            MWsheet.PageSetup.Orientation = MOrientation;
            if (MTopMargin != 0)
                MWsheet.PageSetup.TopMargin = MTopMargin;
            if (MBottomMargin != 0)
                MWsheet.PageSetup.BottomMargin = MBottomMargin;
            if (MLeftMargin != 0)
                MWsheet.PageSetup.LeftMargin = MLeftMargin;
            if (MRightMargin != 0)
                MWsheet.PageSetup.RightMargin = MRightMargin;
            if (MHeaderMargin != 0)
                MWsheet.PageSetup.HeaderMargin = MHeaderMargin;
            if (MFooterMargin != 0)
                MWsheet.PageSetup.FooterMargin = MFooterMargin;
            if (MZoom != 0)
                MWsheet.PageSetup.Zoom = MZoom;
            MApp.Visible = MVisible;
            MWbook.Save();
        }
        catch
        {
        }
    }


    public void ExcelClose(Excel.Application MApp, Excel.Workbook MWbook, Excel.Worksheet MWsheet, bool MVisible, bool MClose)
    {
        try
        {
            MApp.DisplayAlerts = false;
            MWbook.Save();
            MApp.Visible = MVisible;
            if (MClose == true)
                MWbook.Close(Type.Missing, Type.Missing, Type.Missing);
            if (MClose == true)
                MApp.Quit();

            //Commons.Modules.ObjSystems.MReleaseObject(MWsheet);
            //Commons.Modules.ObjSystems.MReleaseObject(MWbook);
            //Commons.Modules.ObjSystems.MReleaseObject(MApp);
        }
        catch
        {
        }
    }

    public void MReleaseObject(object obj)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            obj = null;
        }
        catch
        {
            obj = null;
        }
        finally
        {
            GC.Collect();
        }
    }


    public Excel.Range GetRange(Excel.Worksheet MWsheet, int DongBD, int CotBD, int DongKT, int CotKT)
    {
        try
        {
            // Dim allCells = MWsheet.Cells[DongBD, CotBD, DongKT, CotKT]
            Excel.Range MRange = MWsheet.Range[MWsheet.Cells[DongBD, CotBD], MWsheet.Cells[DongKT, CotKT]];
            return MRange;
        }
        catch (Exception)
        {
            return null/* TODO Change to default(_) if this is not a reference type */;
        }
    }

    public void DinhDang(Excel.Worksheet MWsheet, string NoiDung, int Dong, int Cot)
    {
        try
        {
            Excel.Range MRange = MWsheet.Range[MWsheet.Cells[Dong, Cot], MWsheet.Cells[Dong, Cot]];
            MRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            if (NoiDung != "")
                MWsheet.Cells[Dong, Cot] = NoiDung;
            MRange.Borders.LineStyle = 0;
        }
        catch
        {
        }
    }

    public void DinhDang(Excel.Worksheet MWsheet, string NoiDung, int Dong, int Cot, String MNumberFormat)
    {
        try
        {
            Excel.Range MRange = MWsheet.Range[MWsheet.Cells[Dong, Cot], MWsheet.Cells[Dong, Cot]];
            MRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            if (NoiDung != "")
                MWsheet.Cells[Dong, Cot] = NoiDung;
            MRange.Borders.LineStyle = 0;
        }
        catch
        {
        }
    }

    public void DinhDang(Excel.Worksheet MWsheet, string NoiDung, int Dong, int Cot, String MNumberFormat, float MFontSize)
    {
        try
        {
            Excel.Range MRange = MWsheet.Range[MWsheet.Cells[Dong, Cot], MWsheet.Cells[Dong, Cot]];
            if (MFontSize > 0)
                MRange.Font.Size = MFontSize;

            MRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            if (NoiDung != "")
                MWsheet.Cells[Dong, Cot] = NoiDung;
            MRange.Borders.LineStyle = 0;
        }
        catch
        {
        }
    }

    public void DinhDang(Excel.Worksheet MWsheet, string NoiDung, int Dong, int Cot, String MNumberFormat, float MFontSize, bool MFontBold)
    {
        try
        {
            Excel.Range MRange = MWsheet.Range[MWsheet.Cells[Dong, Cot], MWsheet.Cells[Dong, Cot]];
            if (MFontSize > 0)
                MRange.Font.Size = MFontSize;
            MRange.Font.Bold = MFontBold;
            if (MNumberFormat != "")
                MRange.NumberFormat = MNumberFormat;

            MRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            if (NoiDung != "")
                MWsheet.Cells[Dong, Cot] = NoiDung;
            MRange.Borders.LineStyle = 0;
        }
        catch
        {
        }
    }

    // Sub DinhDang(ByVal MWsheet As Excel.Worksheet, ByVal NoiDung As String, ByVal Dong As Integer, ByVal Cot As Integer, _
    // ByVal MNumberFormat As [String], ByVal MFontSize As float, ByVal MFontBold As Boolean, _
    // ByVal MFontUnderline As Boolean)
    // Try
    // Dim MRange As Excel.Range = MWsheet.Range[MWsheet.Cells[Dong, Cot), MWsheet.Cells[Dong, Cot))
    // If MFontSize > 0 Then MRange.Font.Size = MFontSize
    // MRange.Font.Bold = MFontBold
    // If MNumberFormat <> "" Then MRange.NumberFormat = MNumberFormat

    // MRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
    // If NoiDung <> "" Then MWsheet.Cells[Dong, Cot) = NoiDung
    // MRange.Borders.LineStyle = 0

    // Catch
    // End Try
    // End Sub

    // Sub DinhDang(ByVal MWsheet As Excel.Worksheet, ByVal NoiDung As String, ByVal Dong As Integer, ByVal Cot As Integer, _
    // ByVal MNumberFormat As [String], ByVal MFontSize As float, ByVal MFontBold As Boolean, _
    // ByVal MFontUnderline As Boolean, ByVal MFontItalic As Boolean)
    // Try
    // Dim MRange As Excel.Range = MWsheet.Range[MWsheet.Cells[Dong, Cot), MWsheet.Cells[Dong, Cot))
    // If MFontSize > 0 Then MRange.Font.Size = MFontSize
    // MRange.Font.Bold = MFontBold
    // If MNumberFormat <> "" Then MRange.NumberFormat = MNumberFormat

    // MRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
    // If NoiDung <> "" Then MWsheet.Cells[Dong, Cot) = NoiDung
    // MRange.Borders.LineStyle = 0

    // Catch
    // End Try
    // End Sub

    public void DinhDang(Excel.Worksheet MWsheet, string NoiDung, int Dong, int Cot, String MNumberFormat, float MFontSize, bool MFontBold, Excel.XlHAlign MHAlign, Excel.XlVAlign MVAlign)
    {
        try
        {
            Excel.Range MRange = MWsheet.Range[MWsheet.Cells[Dong, Cot], MWsheet.Cells[Dong, Cot]];
            if (MFontSize > 0)
                MRange.Font.Size = MFontSize;

            MRange.Font.Bold = MFontBold;
            if (MNumberFormat != "")
                MRange.NumberFormat = MNumberFormat;

            MRange.HorizontalAlignment = MHAlign;
            MRange.VerticalAlignment = MVAlign;
            if (NoiDung != "")
                MWsheet.Cells[Dong, Cot] = NoiDung;
            MRange.Borders.LineStyle = 0;
        }
        catch
        {
        }
    }

    public void DinhDang(Excel.Worksheet MWsheet, string NoiDung, int Dong, int Cot, String MNumberFormat, float MFontSize, bool MFontBold, bool MMerge, int MDongMerge, int MCotMerge)
    {
        try
        {
            Excel.Range MRange = MWsheet.Range[MWsheet.Cells[Dong, Cot], MWsheet.Cells[MDongMerge, MCotMerge]];
            MRange.Merge(MMerge);
            if (MFontSize > 0)
                MRange.Font.Size = MFontSize;

            MRange.Font.Bold = MFontBold;

            if (MNumberFormat != "")
                MRange.NumberFormat = MNumberFormat;

            if (NoiDung != "")
                MWsheet.Cells[Dong, Cot] = NoiDung;
            MRange.Borders.LineStyle = 0;
        }
        catch
        {
        }
    }

    public void DinhDang(Excel.Worksheet MWsheet, string NoiDung, int Dong, int Cot, String MNumberFormat, float MFontSize, bool MFontBold, Excel.XlHAlign MHAlign, Excel.XlVAlign MVAlign, bool MMerge, int MDongMerge, int MCotMerge, int MRowHeight)
    {
        try
        {
            Excel.Range MRange = MWsheet.Range[MWsheet.Cells[Dong, Cot], MWsheet.Cells[MDongMerge, MCotMerge]];
            MRange.Merge(MMerge);
            if (MFontSize > 0)
                MRange.Font.Size = MFontSize;

            MRange.Font.Bold = MFontBold;
            MRange.HorizontalAlignment = MHAlign;
            MRange.VerticalAlignment = MVAlign;
            MRange.RowHeight = MRowHeight;

            if (MNumberFormat != "")
                MRange.NumberFormat = MNumberFormat;
            if (NoiDung != "")
                MWsheet.Cells[Dong, Cot] = NoiDung;
            MRange.Borders.LineStyle = 0;
        }
        catch
        {
        }
    }

    public void DinhDang(Excel.Worksheet MWsheet, string NoiDung, int Dong, int Cot, String MNumberFormat, float MFontSize, bool MFontBold, Excel.XlHAlign MHAlign, Excel.XlVAlign MVAlign, bool MMerge, int MDongMerge, int MCotMerge, bool MFontUnderline, bool MFontItalic)
    {
        try
        {
            Excel.Range MRange = MWsheet.Range[MWsheet.Cells[Dong, Cot], MWsheet.Cells[MDongMerge, MCotMerge]];
            MRange.Merge(MMerge);
            if (MFontSize > 0)
                MRange.Font.Size = MFontSize;

            MRange.Font.Bold = MFontBold;
            MRange.Font.Underline = MFontUnderline;
            MRange.Font.Italic = MFontItalic;
            MRange.HorizontalAlignment = MHAlign;
            MRange.VerticalAlignment = MVAlign;

            if (MNumberFormat != "")
                MRange.NumberFormat = MNumberFormat;
            if (NoiDung != "")
                MWsheet.Cells[Dong, Cot] = NoiDung;
            MRange.Borders.LineStyle = 0;
        }
        catch
        {
        }
    }

    public string MCotExcel(int iCot)
    {
        string sTmp = "";
        if (iCot > 26)
        {
            sTmp = Convert.ToChar(Convert.ToInt32((iCot - 1) / 26) + 64).ToString();
            sTmp = sTmp + Convert.ToChar(((Convert.ToInt32(iCot) - 1) % 26) + 65).ToString();
        }
        else
            sTmp = Convert.ToChar(64 + iCot).ToString();

        return sTmp;
    }

    public void MExportExcel(DataTable dtTmp, Excel.Worksheet ExcelSheets, Excel.Range sRange, bool bheader)
    {
        if (bheader)
        {
            object[,] rawData = new object[dtTmp.Rows.Count + 1, dtTmp.Columns.Count - 1 + 1];
            for (var col = 0; col <= dtTmp.Columns.Count - 1; col++)
                rawData[0, col] = dtTmp.Columns[col].ColumnName;
            for (var col = 0; col <= dtTmp.Columns.Count - 1; col++)
            {
                for (var row = 0; row <= dtTmp.Rows.Count - 1; row++)
                    rawData[row + 1, col] = dtTmp.Rows[row][col].ToString();
            }
            sRange.Value = rawData;
        }
        else
        {
            object[,] rawData = new object[dtTmp.Rows.Count, dtTmp.Columns.Count];
            for (var col = 0; col <= dtTmp.Columns.Count - 1; col++)
            {
                for (var row = 0; row <= dtTmp.Rows.Count - 1; row++)
                    rawData[row, col] = dtTmp.Rows[row][col].ToString();
            }
            sRange.Value = rawData;
        }
    }

    public void MExportExcel(DataTable dtTmp, Excel.Worksheet ExcelSheets, Excel.Range sRange, bool loadNN, string form)
    {
        object[,] rawData = new object[dtTmp.Rows.Count + 1, dtTmp.Columns.Count - 1 + 1];
        for (var col = 0; col <= dtTmp.Columns.Count - 1; col++)
            rawData[0, col] = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, form, dtTmp.Columns[col].ColumnName, Commons.Modules.TypeLanguage);
        for (var col = 0; col <= dtTmp.Columns.Count - 1; col++)
        {
            for (var row = 0; row <= dtTmp.Rows.Count - 1; row++)
                rawData[row + 1, col] = dtTmp.Rows[row][col].ToString();
        }
        sRange.Value = rawData;
    }

    public void MTaoSTT(Excel.Worksheet MWsheet, int DongBD, int Cot, int DongKT)
    {
        try
        {
            Excel.Range MRange = MWsheet.Range[MWsheet.Cells[DongBD, Cot], MWsheet.Cells[DongBD, Cot]];
            MRange.Value2 = 1;

            MRange = MWsheet.Range[MWsheet.Cells[DongBD + 1, Cot], MWsheet.Cells[DongKT, Cot]];
            MRange.Value2 = "=OFFSET(A" + (DongBD + 1).ToString() + ",-1,0)+1";
        }
        catch
        {
        }
    }


    public void MTTChung(int DongBD, int CotBD, int logoWidth, int logoHeight, ExcelWorksheet ws)
    {
        System.Data.DataTable dtTmp = new System.Data.DataTable();
        string sSql = "";
        if (Commons.Modules.sPrivate.ToUpper() == "GREENFEED")
            sSql = "SELECT  CASE " + Commons.Modules.TypeLanguage.ToString() + " WHEN 0 THEN C.TEN_DON_VI WHEN 1 THEN C.TEN_DON_VI_ANH ELSE C.TEN_DON_VI_HOA END AS TEN_CTY, " + " (SELECT TOP 1 LOGO   FROM THONG_TIN_CHUNG ) AS LOGO, C.DIA_CHI AS DIA_CHI, DIEN_THOAI AS Phone, FAX, '' AS EMAIL " + " FROM dbo.DON_VI AS C INNER JOIN dbo.TO_PHONG_BAN AS D ON C.MS_DON_VI = D.MS_DON_VI INNER JOIN dbo.USERS AS A " + " ON D.MS_TO = A.MS_TO WHERE(A.USERNAME = N'" + Commons.Modules.UserName.ToString() + "') ";
        else
            sSql = " SELECT CASE WHEN " + Commons.Modules.TypeLanguage + "=0 " + " THEN TEN_CTY_TIENG_VIET ELSE TEN_CTY_TIENG_ANH END AS TEN_CTY,LOGO, " + " CASE WHEN " + Commons.Modules.TypeLanguage + "=0 THEN DIA_CHI_VIET  ELSE DIA_CHI_ANH  END AS DIA_CHI,Phone," + " Fax,EMAIL FROM THONG_TIN_CHUNG ";
        dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, System.Data.CommandType.Text, sSql));

        if (dtTmp.Rows.Count == 0 & Commons.Modules.sPrivate.ToUpper() == "GREENFEED")
        {
            sSql = " SELECT CASE WHEN " + Commons.Modules.TypeLanguage + "=0 " + " THEN TEN_CTY_TIENG_VIET ELSE TEN_CTY_TIENG_ANH END AS TEN_CTY,LOGO, " + " CASE WHEN " + Commons.Modules.TypeLanguage + "=0 THEN DIA_CHI_VIET  ELSE DIA_CHI_ANH  END AS DIA_CHI,Phone," + " Fax,EMAIL FROM THONG_TIN_CHUNG ";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, System.Data.CommandType.Text, sSql));
        }

        AddImage(ws, DongBD, CotBD, logoWidth, logoHeight, dtTmp, "LOGO");

        ws.Cells["B1"].Value = dtTmp.Rows[0]["TEN_CTY"].ToString();
        ws.Cells["B2"].Value = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "frmReportBaoTri_Huda", "diachi", Commons.Modules.TypeLanguage) + " : " + dtTmp.Rows[0]["DIA_CHI"].ToString();
        ws.Cells["B3"].Value = ((Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "frmReportBaoTri_Huda", "dienthoai", Commons.Modules.TypeLanguage) + " : ") + dtTmp.Rows[0]["phone"] + "  " + Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "frmReportBaoTri_Huda", "fax", Commons.Modules.TypeLanguage) + " : ") + dtTmp.Rows[0]["FAX"];
    }


    public void MTTChung(int DongBD, int CotBD, int logoWidth, int logoHeight, ExcelWorksheet ws, string CotTTBD)
    {
        System.Data.DataTable dtTmp = new System.Data.DataTable();
        string sSql = "";
        if (Commons.Modules.sPrivate.ToUpper() == "GREENFEED")
            sSql = "SELECT  CASE " + Commons.Modules.TypeLanguage.ToString() + " WHEN 0 THEN C.TEN_DON_VI WHEN 1 THEN C.TEN_DON_VI_ANH ELSE C.TEN_DON_VI_HOA END AS TEN_CTY, " + " (SELECT TOP 1 LOGO   FROM THONG_TIN_CHUNG ) AS LOGO, C.DIA_CHI AS DIA_CHI, DIEN_THOAI AS Phone, FAX, '' AS EMAIL " + " FROM dbo.DON_VI AS C INNER JOIN dbo.TO_PHONG_BAN AS D ON C.MS_DON_VI = D.MS_DON_VI INNER JOIN dbo.USERS AS A " + " ON D.MS_TO = A.MS_TO WHERE(A.USERNAME = N'" + Commons.Modules.UserName.ToString() + "') ";
        else
            sSql = " SELECT CASE WHEN " + Commons.Modules.TypeLanguage + "=0 " + " THEN TEN_CTY_TIENG_VIET ELSE TEN_CTY_TIENG_ANH END AS TEN_CTY,LOGO, " + " CASE WHEN " + Commons.Modules.TypeLanguage + "=0 THEN DIA_CHI_VIET  ELSE DIA_CHI_ANH  END AS DIA_CHI,Phone," + " Fax,EMAIL FROM THONG_TIN_CHUNG ";
        dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, System.Data.CommandType.Text, sSql));

        if (dtTmp.Rows.Count == 0 & Commons.Modules.sPrivate.ToUpper() == "GREENFEED")
        {
            sSql = " SELECT CASE WHEN " + Commons.Modules.TypeLanguage + "=0 " + " THEN TEN_CTY_TIENG_VIET ELSE TEN_CTY_TIENG_ANH END AS TEN_CTY,LOGO, " + " CASE WHEN " + Commons.Modules.TypeLanguage + "=0 THEN DIA_CHI_VIET  ELSE DIA_CHI_ANH  END AS DIA_CHI,Phone," + " Fax,EMAIL FROM THONG_TIN_CHUNG ";
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, System.Data.CommandType.Text, sSql));
        }

        AddImage(ws, DongBD, CotBD, logoWidth, logoHeight, dtTmp, "LOGO");
        if ((CotTTBD == ""))
            return;
        ws.Cells[CotTTBD + "1"].Value = dtTmp.Rows[0]["TEN_CTY"].ToString();
        ws.Cells[CotTTBD + "2"].Value = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "frmReportBaoTri_Huda", "diachi", Commons.Modules.TypeLanguage) + " : " + dtTmp.Rows[0]["DIA_CHI"].ToString();

        ws.Cells[CotTTBD + "3"].Value = ((Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "frmReportBaoTri_Huda", "dienthoai", Commons.Modules.TypeLanguage) + " : ") + dtTmp.Rows[0]["phone"] + "  " + Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "frmReportBaoTri_Huda", "fax", Commons.Modules.TypeLanguage) + " : ") + dtTmp.Rows[0]["FAX"];
    }


    public void AddImage(ExcelWorksheet ws, int DongBD, int CotBD, int logoWidth, int logoHeight, DataTable dtLogo, string sCotHinh)
    {
        System.Drawing.Image img;
        OfficeOpenXml.Drawing.ExcelPicture excelImage = null/* TODO Change to default(_) if this is not a reference type */;
        if (dtLogo.Rows.Count > 0)
        {
            Byte[] data = new Byte[0] { };
            data = (Byte[])dtLogo.Rows[0][sCotHinh];
            System.IO.MemoryStream mem = new System.IO.MemoryStream(data);
            img = System.Drawing.Image.FromStream(mem);

            if (logoWidth == 0)
                logoWidth = 110;
            if (logoHeight == 0)
                logoHeight = 45;
            excelImage = ws.Drawings.AddPicture(Commons.Modules.sPrivate, img);
            excelImage.From.Column = CotBD;
            excelImage.From.Row = DongBD;
            excelImage.SetSize(logoWidth, logoHeight);
            excelImage.From.ColumnOff = Pixel2MTU(2);
            excelImage.From.RowOff = Pixel2MTU(2);
        }
    }


    public void AddImage(ExcelWorksheet ws, int DongBD, int CotBD, int logoWidth, int logoHeight, string sPath)
    {
        System.Drawing.Bitmap image = new System.Drawing.Bitmap(sPath);
        OfficeOpenXml.Drawing.ExcelPicture excelImage = null/* TODO Change to default(_) if this is not a reference type */;

        if (image != null)
        {
            if (logoWidth == 0)
                logoWidth = 110;
            if (logoHeight == 0)
                logoHeight = 45;
            excelImage = ws.Drawings.AddPicture(Commons.Modules.sPrivate, image);
            excelImage.From.Column = CotBD;
            excelImage.From.Row = DongBD;
            excelImage.SetSize(logoWidth, logoHeight);
            excelImage.From.ColumnOff = Pixel2MTU(2);
            excelImage.From.RowOff = Pixel2MTU(2);
        }
    }

    private int Pixel2MTU(int pixels)
    {
        int mtus = pixels * 9525;
        return mtus;
    }

    public void MFormatExcel(ExcelWorksheet ws, DataTable dtData, int iRow, string sBC, bool mNNgu = true, bool mAutoFitColumns = true, bool mWrapText = true)
    {
        try
        {
            int columnCount = dtData.Columns.Count;
            int rowCount = dtData.Rows.Count;

            var allCells = ws.Cells[iRow, 1, iRow + rowCount, columnCount];
            var border = allCells.Style.Border;

            border.Top.Style = ExcelBorderStyle.Thin;
            border.Left.Style = ExcelBorderStyle.Thin;
            border.Bottom.Style = ExcelBorderStyle.Thin;
            border.Right.Style = ExcelBorderStyle.Thin;


            if (mAutoFitColumns)
                allCells.AutoFitColumns();
            allCells.Style.WrapText = mWrapText;
            allCells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            allCells = ws.Cells[iRow, 1, iRow, columnCount];
            allCells.Style.Font.Bold = true;
            allCells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;


            for (int i = 1; i <= columnCount + 1; i++)
            {
                try
                {
                    if (mNNgu)
                        ws.Cells[iRow, i].Value = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, sBC, dtData.Columns[i - 1].ColumnName, Commons.Modules.TypeLanguage);
                }
                catch
                {
                }
            }
        }
        catch (Exception ex)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
        }
    }

    public void MFormatExcel(ExcelWorksheet ws, DataTable dtData, int iRow, string sBC, List<string> sCotNgay, string sDateFormat, bool mNNgu = true, bool mAutoFitColumns = true, bool mWrapText = true)
    {
        try
        {
            int columnCount = dtData.Columns.Count;
            int rowCount = dtData.Rows.Count;

            var allCells = ws.Cells[iRow, 1, iRow + rowCount, columnCount];
            var border = allCells.Style.Border;

            border.Top.Style = ExcelBorderStyle.Thin;
            border.Left.Style = ExcelBorderStyle.Thin;
            border.Bottom.Style = ExcelBorderStyle.Thin;
            border.Right.Style = ExcelBorderStyle.Thin;


            if (mAutoFitColumns)
                allCells.AutoFitColumns();
            allCells.Style.WrapText = mWrapText;
            allCells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            allCells = ws.Cells[iRow, 1, iRow, columnCount];
            allCells.Style.Font.Bold = true;
            allCells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;


            for (int i = 1; i <= columnCount + 1; i++)
            {
                try
                {
                    if (sCotNgay != null)
                    {
                        if (sCotNgay.Contains(ws.Cells[iRow, i].Value.ToString()))
                        {
                            ws.Column(i).Style.Numberformat.Format = sDateFormat;
                            ws.Column(i).Width = 13;
                        }
                    }
                }
                catch
                {
                }

                try
                {
                    if (mNNgu)
                        ws.Cells[iRow, i].Value = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, sBC, dtData.Columns[i - 1].ColumnName, Commons.Modules.TypeLanguage);
                }
                catch
                {
                }
            }
        }
        catch (Exception ex)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
        }
    }

    public void MFormatExcel(ExcelWorksheet ws, DataTable dtData, int iRow, string sBC, List<List<Object>> WidthColumns, bool mNNgu = true, bool mAutoFitColumns = true, bool mWrapText = true)
    {
        try
        {
            int columnCount = dtData.Columns.Count;
            int rowCount = dtData.Rows.Count;

            var allCells = ws.Cells[iRow, 1, iRow + rowCount, columnCount];
            var border = allCells.Style.Border;

            border.Top.Style = ExcelBorderStyle.Thin;
            border.Left.Style = ExcelBorderStyle.Thin;
            border.Bottom.Style = ExcelBorderStyle.Thin;
            border.Right.Style = ExcelBorderStyle.Thin;


            if (mAutoFitColumns)
                allCells.AutoFitColumns();
            allCells.Style.WrapText = mWrapText;
            allCells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            allCells = ws.Cells[iRow, 1, iRow, columnCount];
            allCells.Style.Font.Bold = true;
            allCells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;


            for (int i = 1; i <= columnCount + 1; i++)
            {
                try
                {
                    if (WidthColumns != null)
                    {
                        for (int j = 0; j <= WidthColumns.Count; j++)
                        {
                            if (WidthColumns[j][0].ToString().Contains(ws.Cells[iRow, i].Value.ToString()))
                            {
                                ws.Column(i).Width = int.Parse(WidthColumns[j][1].ToString());
                                break;
                            }
                        }
                    }
                }
                catch
                {
                }

                try
                {
                    if (mNNgu)
                        ws.Cells[iRow, i].Value = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, sBC, dtData.Columns[i - 1].ColumnName, Commons.Modules.TypeLanguage);
                }
                catch
                {
                }
            }
        }
        catch (Exception ex)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
        }
    }

    public void MFormatExcel(ExcelWorksheet ws, DataTable dtData, int iRow, string sBC, List<string> sCotHide, bool mNNgu = true, bool mAutoFitColumns = true, bool mWrapText = true)
    {
        try
        {
            int columnCount = dtData.Columns.Count;
            int rowCount = dtData.Rows.Count;

            var allCells = ws.Cells[iRow, 1, iRow + rowCount, columnCount];
            var border = allCells.Style.Border;

            border.Top.Style = ExcelBorderStyle.Thin;
            border.Left.Style = ExcelBorderStyle.Thin;
            border.Bottom.Style = ExcelBorderStyle.Thin;
            border.Right.Style = ExcelBorderStyle.Thin;

            if (mAutoFitColumns)
                allCells.AutoFitColumns();
            allCells.Style.WrapText = mWrapText;
            allCells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            allCells = ws.Cells[iRow, 1, iRow, columnCount];
            allCells.Style.Font.Bold = true;
            allCells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;


            for (int i = 1; i <= columnCount + 1; i++)
            {
                try
                {
                    if (sCotHide != null)
                    {
                        if (sCotHide.Contains(ws.Cells[iRow, i].Value.ToString()))
                            ws.Column(i).Hidden = true;
                    }
                }
                catch
                {
                }

                try
                {
                    if (mNNgu)
                        ws.Cells[iRow, i].Value = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, sBC, dtData.Columns[i - 1].ColumnName, Commons.Modules.TypeLanguage);
                }
                catch
                {
                }
            }
        }
        catch (Exception ex)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
        }
    }

    public void MFormatExcel(ExcelWorksheet ws, DataTable dtData, int iRow, string sBC, List<string> sCotNgay, string sDateFormat, List<List<Object>> WidthColumns, bool mNNgu = true, bool mAutoFitColumns = true, bool mWrapText = true)
    {
        try
        {
            int columnCount = dtData.Columns.Count;
            int rowCount = dtData.Rows.Count;

            var allCells = ws.Cells[iRow, 1, iRow + rowCount, columnCount];
            var border = allCells.Style.Border;

            border.Top.Style = ExcelBorderStyle.Thin;
            border.Left.Style = ExcelBorderStyle.Thin;
            border.Bottom.Style = ExcelBorderStyle.Thin;
            border.Right.Style = ExcelBorderStyle.Thin;

            if (mAutoFitColumns)
                allCells.AutoFitColumns();
            allCells.Style.WrapText = mWrapText;
            allCells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            allCells = ws.Cells[iRow, 1, iRow, columnCount];
            allCells.Style.Font.Bold = true;
            allCells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;


            for (int i = 1; i <= columnCount + 1; i++)
            {
                try
                {
                    if (sCotNgay != null)
                    {
                        if (sCotNgay.Contains(ws.Cells[iRow, i].Value.ToString()))
                        {
                            ws.Column(i).Style.Numberformat.Format = sDateFormat;
                            ws.Column(i).Width = 13;
                        }
                    }
                    if (WidthColumns != null)
                    {
                        for (int j = 0; j <= WidthColumns.Count; j++)
                        {
                            if (WidthColumns[j][0].ToString().Contains(ws.Cells[iRow, i].Value.ToString()))
                            {
                                ws.Column(i).Width = int.Parse(WidthColumns[j][1].ToString());
                                break;
                            }
                        }
                    }
                }
                catch
                {
                }

                try
                {
                    if (mNNgu)
                        ws.Cells[iRow, i].Value = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, sBC, dtData.Columns[i - 1].ColumnName, Commons.Modules.TypeLanguage);
                }
                catch
                {
                }
            }
        }
        catch (Exception ex)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
        }
    }

    public void MFormatExcel(ExcelWorksheet ws, DataTable dtData, int iRow, string sBC, List<string> sCotNgay, string sDateFormat, List<string> sCotHide, bool mNNgu = true, bool mAutoFitColumns = true, bool mWrapText = true)
    {
        try
        {
            int columnCount = dtData.Columns.Count;
            int rowCount = dtData.Rows.Count;

            var allCells = ws.Cells[iRow, 1, iRow + rowCount, columnCount];
            var border = allCells.Style.Border;

            border.Top.Style = ExcelBorderStyle.Thin;
            border.Left.Style = ExcelBorderStyle.Thin;
            border.Bottom.Style = ExcelBorderStyle.Thin;
            border.Right.Style = ExcelBorderStyle.Thin;


            if (mAutoFitColumns)
                allCells.AutoFitColumns();
            allCells.Style.WrapText = mWrapText;
            allCells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            allCells = ws.Cells[iRow, 1, iRow, columnCount];
            allCells.Style.Font.Bold = true;
            allCells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;


            for (int i = 1; i <= columnCount + 1; i++)
            {
                try
                {
                    if (sCotNgay != null)
                    {
                        if (sCotNgay.Contains(ws.Cells[iRow, i].Value.ToString()))
                        {
                            ws.Column(i).Style.Numberformat.Format = sDateFormat;
                            ws.Column(i).Width = 13;
                        }
                    }

                    if (sCotHide != null)
                    {
                        if (sCotHide.Contains(ws.Cells[iRow, i].Value.ToString()))
                            ws.Column(i).Hidden = true;
                    }
                }
                catch
                {
                }

                try
                {
                    if (mNNgu)
                        ws.Cells[iRow, i].Value = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, sBC, dtData.Columns[i - 1].ColumnName, Commons.Modules.TypeLanguage);
                }
                catch
                {
                }
            }
        }
        catch (Exception ex)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
        }
    }

    public void MFormatExcel(ExcelWorksheet ws, DataTable dtData, int iRow, string sBC, List<string> sCotNgay, string sDateFormat, List<string> sCotHide, List<List<Object>> WidthColumns, bool mNNgu = true, bool mAutoFitColumns = true, bool mWrapText = true)
    {
        try
        {
            int columnCount = dtData.Columns.Count;
            int rowCount = dtData.Rows.Count;

            var allCells = ws.Cells[iRow, 1, iRow + rowCount, columnCount];
            var border = allCells.Style.Border;

            border.Top.Style = ExcelBorderStyle.Thin;
            border.Left.Style = ExcelBorderStyle.Thin;
            border.Bottom.Style = ExcelBorderStyle.Thin;
            border.Right.Style = ExcelBorderStyle.Thin;


            if (mAutoFitColumns)
                allCells.AutoFitColumns();
            allCells.Style.WrapText = mWrapText;
            allCells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            allCells = ws.Cells[iRow, 1, iRow, columnCount];
            allCells.Style.Font.Bold = true;
            allCells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;


            for (int i = 1; i <= columnCount + 1; i++)
            {
                try
                {
                    if (sCotNgay != null)
                    {
                        if (sCotNgay.Contains(ws.Cells[iRow, i].Value.ToString()))
                        {
                            ws.Column(i).Style.Numberformat.Format = sDateFormat;
                            ws.Column(i).Width = 13;
                        }
                    }

                    if (sCotHide != null)
                    {
                        if (sCotHide.Contains(ws.Cells[iRow, i].Value.ToString()))
                            ws.Column(i).Hidden = true;
                    }

                    if (WidthColumns != null)
                    {
                        for (int j = 0; j <= WidthColumns.Count; j++)
                        {
                            if (WidthColumns[j][0].ToString().Contains(ws.Cells[iRow, i].Value.ToString()))
                            {
                                ws.Column(i).Width = int.Parse(WidthColumns[j][1].ToString());
                                break;
                            }
                        }
                    }
                }
                catch
                {
                }

                try
                {
                    if (mNNgu)
                        ws.Cells[iRow, i].Value = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, sBC, dtData.Columns[i - 1].ColumnName, Commons.Modules.TypeLanguage);
                }
                catch
                {
                }
            }
        }
        catch (Exception ex)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
        }
    }



    public void MText(ExcelWorksheet ws, string sBC, string sKeyWord, int DongBD, int CotBD)
    {
        if (sBC == "")
            ws.Cells[DongBD, CotBD].Value = sKeyWord;
        else
            ws.Cells[DongBD, CotBD].Value = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, sBC, sKeyWord, Commons.Modules.TypeLanguage);
    }

    public void MText(ExcelWorksheet ws, string sBC, string sKeyWord, int DongBD, int CotBD, bool mBold)
    {
        var allCells = ws.Cells[DongBD, CotBD];
        allCells.Style.Font.Bold = mBold;
        if (sBC == "")
            allCells.Value = sKeyWord;
        else
            allCells.Value = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, sBC, sKeyWord, Commons.Modules.TypeLanguage);
    }

    public void MText(ExcelWorksheet ws, string sBC, string sKeyWord, int DongBD, int CotBD, float mSize)
    {
        var allCells = ws.Cells[DongBD, CotBD];
        allCells.Style.Font.Size = mSize;
        if (sBC == "")
            allCells.Value = sKeyWord;
        else
            allCells.Value = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, sBC, sKeyWord, Commons.Modules.TypeLanguage);
    }

    public void MText(ExcelWorksheet ws, string sBC, string sKeyWord, int DongBD, int CotBD, bool mBold, float mSize)
    {
        var allCells = ws.Cells[DongBD, CotBD];
        allCells.Style.Font.Bold = mBold;
        allCells.Style.Font.Size = mSize;
        if (sBC == "")
            allCells.Value = sKeyWord;
        else
            allCells.Value = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, sBC, sKeyWord, Commons.Modules.TypeLanguage);
    }

    public void MText(ExcelWorksheet ws, string sBC, string sKeyWord, int DongBD, int CotBD, bool mBold, float mSize, OfficeOpenXml.Style.ExcelHorizontalAlignment mHorAli, OfficeOpenXml.Style.ExcelVerticalAlignment mVerAli)
    {
        var allCells = ws.Cells[DongBD, CotBD];
        allCells.Style.Font.Bold = mBold;
        allCells.Style.Font.Size = mSize;
        allCells.Style.HorizontalAlignment = mHorAli;
        allCells.Style.VerticalAlignment = mVerAli;
        if (sBC == "")
            allCells.Value = sKeyWord;
        else
            allCells.Value = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, sBC, sKeyWord, Commons.Modules.TypeLanguage);
    }

    public void MText(ExcelWorksheet ws, string sBC, string sKeyWord, int DongBD, int CotBD, int DongKT, int CotKT, bool mMerge)
    {
        var allCells = ws.Cells[DongBD, CotBD, DongKT, CotKT];
        allCells.Merge = mMerge;
        if (sBC == "")
            allCells.Value = sKeyWord;
        else
            allCells.Value = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, sBC, sKeyWord, Commons.Modules.TypeLanguage);
    }

    public void MText(ExcelWorksheet ws, string sBC, string sKeyWord, int DongBD, int CotBD, int DongKT, int CotKT, bool mMerge, bool mBold)
    {
        var allCells = ws.Cells[DongBD, CotBD, DongKT, CotKT];
        allCells.Merge = mMerge;
        allCells.Style.Font.Bold = mBold;
        if (sBC == "")
            allCells.Value = sKeyWord;
        else
            allCells.Value = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, sBC, sKeyWord, Commons.Modules.TypeLanguage);
    }

    public void MText(ExcelWorksheet ws, string sBC, string sKeyWord, int DongBD, int CotBD, int DongKT, int CotKT, bool mMerge, bool mBold, float mSize)
    {
        var allCells = ws.Cells[DongBD, CotBD, DongKT, CotKT];
        allCells.Merge = mMerge;
        allCells.Style.Font.Bold = mBold;
        allCells.Style.Font.Size = mSize;
        if (sBC == "")
            allCells.Value = sKeyWord;
        else
            allCells.Value = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, sBC, sKeyWord, Commons.Modules.TypeLanguage);
    }

    public void MText(ExcelWorksheet ws, string sBC, string sKeyWord, int DongBD, int CotBD, int DongKT, int CotKT, bool mMerge, bool mBold, float mSize, OfficeOpenXml.Style.ExcelHorizontalAlignment mHorAli, OfficeOpenXml.Style.ExcelVerticalAlignment mVerAli)
    {
        var allCells = ws.Cells[DongBD, CotBD, DongKT, CotKT];
        allCells.Merge = mMerge;
        allCells.Style.Font.Bold = mBold;
        allCells.Style.Font.Size = mSize;
        allCells.Style.HorizontalAlignment = mHorAli;
        allCells.Style.VerticalAlignment = mVerAli;
        if (sBC == "")
            allCells.Value = sKeyWord;
        else
            allCells.Value = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, sBC, sKeyWord, Commons.Modules.TypeLanguage);
    }
    public bool MGetSheetNames(string sFilePath, LookUpEdit cboChonSheet)
    {

        try
        {
            DataTable dt = new DataTable();
            DataColumn dtColID = new DataColumn();
            dtColID.DataType = System.Type.GetType("System.Int16");
            dtColID.ColumnName = "ID";
            dt.Columns.Add(dtColID);

            DataColumn dtColName = new DataColumn();
            dtColName.DataType = System.Type.GetType("System.String");
            dtColName.ColumnName = "Name";
            dt.Columns.Add(dtColName);

            dt.Rows.Add(-1, "");



            byte[] CSVBytes = File.ReadAllBytes(sFilePath);
            var excelStream = new MemoryStream(CSVBytes);
            string FileName = Path.GetFileName(sFilePath);
            var FileExt = Path.GetExtension(FileName);


            if (FileExt == ".xls")
            {
                HSSFWorkbook hssfwb = new HSSFWorkbook(excelStream);
                for (int i = 0; i < hssfwb.NumberOfSheets; i++)
                {
                    string SheetName = hssfwb.GetSheetName(i);
                    if (!string.IsNullOrEmpty(SheetName))
                        dt.Rows.Add(i, SheetName);
                }
            }
            else if (FileExt == ".xlsx")
            {
                XSSFWorkbook hssfwb = new XSSFWorkbook(excelStream);
                for (int i = 0; i < hssfwb.NumberOfSheets; i++)
                {
                    string SheetName = hssfwb.GetSheetName(i);
                    if (!string.IsNullOrEmpty(SheetName))
                        dt.Rows.Add(i, SheetName);
                }
            }

            Commons.Modules.sLoad = "0Load";
            if (dt.Rows.Count > 0)
                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboChonSheet, dt, "ID", "Name", "");

            Commons.Modules.sLoad = "";
            return true;
        }
        catch (Exception ex)
        {
            cboChonSheet.Properties.DataSource = null;
            Commons.Modules.sLoad = "";
            XtraMessageBox.Show(ex.Message.ToString());
            return false;
        }

    }

    public DataTable MGetData2xls(String Path, string sheet)
    {
        HSSFWorkbook wb;
        HSSFSheet sh;
        try
        {

            using (var fs = new FileStream(Path, FileMode.Open, FileAccess.Read))
            {
                wb = new HSSFWorkbook(fs);
                fs.Close();
            }
            DataTable DT = new DataTable();
            DT.Rows.Clear();
            DT.Columns.Clear();
            System.Globalization.DateTimeFormatInfo dtF = new System.Globalization.DateTimeFormatInfo();
            sh = (HSSFSheet)wb.GetSheetAt(int.Parse(sheet));
            HSSFFormulaEvaluator formula = new HSSFFormulaEvaluator(wb);
            formula.EvaluateAll();
            int i = 0;
            int j1 = 0;
            if (DT.Columns.Count < sh.GetRow(i).Cells.Count)
            {
                try
                {
                    for (j1 = 0; j1 < sh.GetRow(i).Cells.Count; j1++)
                    {
                        var cell = sh.GetRow(i).GetCell(j1);
                        if (cell != null)
                        {

                            try
                            {
                                DT.Columns.Add(sh.GetRow(i).GetCell(j1).StringCellValue, typeof(string));
                            }
                            catch
                            { DT.Columns.Add(sh.GetRow(i).GetCell(j1).StringCellValue + "F" + j1.ToString(), typeof(string)); }
                        }
                        else
                        {
                            DT.Columns.Add("NULL" + j1.ToString(), typeof(string));
                        }
                    }
                }
                catch (Exception ex12)
                {

                    XtraMessageBox.Show(ex12.Message.ToString());
                    return null;
                }
            }
            int iTongCot = sh.GetRow(i).Cells.Count;
            i = 1;
            int j;
            while (sh.GetRow(i) != null)
            {
                DT.Rows.Add();
                // write row value
                for (j = 0; j < iTongCot; j++)
                {
                    var cell = sh.GetRow(i).GetCell(j);

                    if (cell != null)
                    {

                        try
                        {
                            formula.EvaluateInCell(cell);
                            switch (cell.CellType)
                            {


                                case NPOI.SS.UserModel.CellType.Numeric:

                                    try
                                    {
                                        string sFormat = cell.CellStyle.GetDataFormatString().ToUpper();
                                        if (sFormat.Contains("M") || sFormat.Contains("D") || sFormat.Contains("Y") || sFormat.Contains("H") || sFormat.Contains("M") || sFormat.Contains("S") || sFormat.Contains(":") || sFormat.Contains("/"))
                                        {
                                            DateTime dtNgay;
                                            try
                                            {
                                                //dtNgay = DateTime.Parse(cell.DateCellValue.ToString(), dtF, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                                dtNgay = cell.DateCellValue;
                                            }
                                            catch { DateTime.TryParse(cell.DateCellValue.ToString(), out dtNgay); }

                                            try
                                            {
                                                DT.Rows[i - 1][j] = dtNgay;
                                            }
                                            catch
                                            {
                                                DT.Rows[i - 1][j] = sh.GetRow(i).GetCell(j).NumericCellValue;
                                            }
                                        }
                                        else
                                        {
                                            double dGTi = 0;
                                            sFormat = "0.000000";
                                            int index = sFormat.IndexOf(".");
                                            if (index > 0)
                                                dGTi = Math.Round(sh.GetRow(i).GetCell(j).NumericCellValue, sFormat.Substring(index).Length);
                                            else
                                                dGTi = sh.GetRow(i).GetCell(j).NumericCellValue;

                                            DT.Rows[i - 1][j] = dGTi;
                                        }


                                    }
                                    catch { DT.Rows[i - 1][j] = sh.GetRow(i).GetCell(j).NumericCellValue; }

                                    break;
                                case NPOI.SS.UserModel.CellType.Boolean:
                                    DT.Rows[i - 1][j] = sh.GetRow(i).GetCell(j).BooleanCellValue.ToString();
                                    break;

                                default:
                                    DT.Rows[i - 1][j] = sh.GetRow(i).GetCell(j).StringCellValue;
                                    break;
                            }

                        }
                        catch (Exception ex1)
                        {

                            XtraMessageBox.Show(ex1.Message.ToString() + "\n " + " row : " + i.ToString() + " col : " + j.ToString());
                            return null;
                        }





                    }
                }

                i++;
                #region prb
                try
                {

                }
                catch { }
                #endregion
            }
            sh.CloneSheet(wb);
            wb.Close();
            return DT;
        }
        catch (Exception ex)
        {

            XtraMessageBox.Show(ex.Message.ToString());
            return null;
        }
    }

    public DataTable MGetData2xlsx(String Path, string sheet)
    {
        XSSFWorkbook wb;
        XSSFSheet sh;
        int i = 0;

        try
        {

            using (var fs = new FileStream(Path, FileMode.Open, FileAccess.Read))
            {
                wb = new XSSFWorkbook(fs);
                fs.Close();
            }

            DataTable DT = new DataTable();
            DT.Rows.Clear();
            DT.Columns.Clear();
            System.Globalization.DateTimeFormatInfo dtF = new System.Globalization.DateTimeFormatInfo();
            // get sheet
            sh = (XSSFSheet)wb.GetSheetAt(int.Parse(sheet));

            i = 0;
            if (DT.Columns.Count < sh.GetRow(i).Cells.Count)
            {
                for (int j = 0; j < sh.GetRow(i).Cells.Count; j++)
                {
                    var cell = sh.GetRow(i).GetCell(j);
                    try
                    {
                        if (sh.GetRow(i).GetCell(j).StringCellValue.ToString().ToUpper() == "STT")
                        { DT.Columns.Add(sh.GetRow(i).GetCell(j).StringCellValue, typeof(float)); }
                        else
                        {
                            DT.Columns.Add(sh.GetRow(i).GetCell(j).StringCellValue, typeof(string));
                        }
                    }
                    catch
                    { DT.Columns.Add(sh.GetRow(i).GetCell(j).StringCellValue + "F" + j.ToString(), typeof(string)); }
                }
            }
            int iTongCot = sh.GetRow(i).Cells.Count;

            i = 1;
            while (sh.GetRow(i) != null)
            {
                DT.Rows.Add();
                // write row value
                for (int j = 0; j < iTongCot; j++)
                {

                    var cell = sh.GetRow(i).GetCell(j);

                    if (cell != null)
                    {
                        switch (cell.CellType)
                        {
                            case NPOI.SS.UserModel.CellType.Numeric:

                                try
                                {
                                    string sFormat = cell.CellStyle.GetDataFormatString().ToUpper();
                                    if (sFormat.Contains("M") || sFormat.Contains("D") || sFormat.Contains("Y") || sFormat.Contains("H") || sFormat.Contains("M") || sFormat.Contains("S") || sFormat.Contains(":") || sFormat.Contains("/"))
                                    {
                                        DateTime dtNgay;
                                        try
                                        {
                                            //dtNgay = DateTime.Parse(cell.DateCellValue.ToString(), dtF, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                                            dtNgay = cell.DateCellValue;
                                        }
                                        catch { DateTime.TryParse(cell.DateCellValue.ToString(), out dtNgay); }

                                        try
                                        {
                                            DT.Rows[i - 1][j] = dtNgay;
                                        }
                                        catch
                                        {
                                            DT.Rows[i - 1][j] = sh.GetRow(i).GetCell(j).NumericCellValue;
                                        }
                                    }
                                    else
                                    {
                                        double dGTi = 0;
                                        sFormat = "0.000000";
                                        int index = sFormat.IndexOf(".");
                                        if (index > 0)
                                            dGTi = Math.Round(sh.GetRow(i).GetCell(j).NumericCellValue, sFormat.Substring(index).Length);
                                        else
                                            dGTi = sh.GetRow(i).GetCell(j).NumericCellValue;

                                        DT.Rows[i - 1][j] = dGTi;
                                    }


                                }
                                catch { DT.Rows[i - 1][j] = sh.GetRow(i).GetCell(j).NumericCellValue; }

                                break;
                            case NPOI.SS.UserModel.CellType.Boolean:
                                DT.Rows[i - 1][j] = sh.GetRow(i).GetCell(j).BooleanCellValue.ToString();
                                break;

                            default:
                                try
                                {
                                    DT.Rows[i - 1][j] = sh.GetRow(i).GetCell(j).StringCellValue;
                                }
                                catch { }
                                break;
                        }

                    }
                }

                i++;
                #region prb
                try
                {
                }
                catch { }
                #endregion
            }
            wb.Close();
            return DT;
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message.ToString() + " - ROW : " + i.ToString());
            return null;
        }
    }

    #region kiểm dữ liệu

    public int CheckLen(GridView grvData, DataRow dr, int col, int giatri, int chieudai, string thongbao)
    {
        try
        {
            if (dr[grvData.Columns[col].FieldName.ToString()] == DBNull.Value || dr[grvData.Columns[col].FieldName.ToString()].ToString() == String.Empty)
            { giatri += 1; }
            else
                if (dr[grvData.Columns[col].FieldName.ToString()].ToString().Length > chieudai)
            {
                dr.SetColumnError(grvData.Columns[col].FieldName.ToString(), thongbao + " dài hơn " + chieudai + " ký tự." + "(" + dr[grvData.Columns[col].FieldName.ToString()].ToString().Length.ToString() + ")");
                dr["XOA"] = 1;
            }
            else
                giatri += 1;
            return giatri;
        }
        catch { return giatri; }
    }
    private string ChuoiKT = "";
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

    public bool KiemDuLieu(GridView grvData, DataRow dr, int iCot, Boolean bKiemNull, int iDoDaiKiem, string sform)
    {
        string sDLKiem;
        try
        {
            sDLKiem = dr[grvData.Columns[iCot].FieldName.ToString()].ToString();
            if (bKiemNull)
            {
                if (string.IsNullOrEmpty(sDLKiem))
                {
                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongDuocTrong"));
                    dr["XOA"] = 1;
                    return false;
                }
                else
                {
                    if (KiemKyTu(sDLKiem, ChuoiKT))  //KiemKyTu
                    {
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(sform, "msgCoChuaKyTuDB"));
                        dr["XOA"] = 1;
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
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(sform, "msgCoChuaKyTuDB"));
                        dr["XOA"] = 1;
                        return false;
                    }
                }
            }
            if (iDoDaiKiem != 0)
            {
                if (sDLKiem.Length > iDoDaiKiem)
                {
                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(sform, "msgDoDaiKyTuVuocQua " + iDoDaiKiem));
                    return false;
                }
            }
        }
        catch
        {
            dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), "error");
            dr["XOA"] = 1;
            return false;
        }
        return true;
    }

    public bool KiemTrungDL(GridView grvData, DataTable dt, DataRow dr, int iCot, string sDLKiem, string tabName, string ColName, string sform)
    {
        string sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDL");
        try
        {

            if (dt.AsEnumerable().Where(x => x[iCot].Equals(sDLKiem)).CopyToDataTable().Rows.Count > 1)
            {
                sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDLLuoi");
                dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra);
                dr["XOA"] = 1;
                return false;
            }
            else
            {
                if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.[" + tabName + "] WHERE " + ColName + " = N'" + sDLKiem + "'")) > 0)
                {

                    sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDLCSDL");
                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra);
                    dr["XOA"] = 1;
                    return false;
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra);
            dr["XOA"] = 1;
            return false;
        }
    }
    public bool KiemTonTai(GridView grvData, DataRow dr, int iCot, string sDLKiem, string tabName, string ColName, Boolean bKiemNull = true, string sform = "")
    {
        //null không kiểm
        if (bKiemNull)
        {//nếu null
            if (string.IsNullOrEmpty(sDLKiem))
            {
                dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongduocTrong"));
                dr["XOA"] = 1;
                return false;
            }
            //khác null
            {
                if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo." + tabName + " WHERE " + ColName + " = N'" + sDLKiem + "'")) == 0)
                {
                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(sform, "msgChuaTonTaiCSDL"));
                    dr["XOA"] = 1;
                    return false;
                }
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(sDLKiem))
            {
                if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo." + tabName + " WHERE " + ColName + " = N'" + sDLKiem + "'")) == 0)
                {
                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(sform, "msgChuaTonTaiCSDL"));
                    dr["XOA"] = 1;
                    return false;
                }
            }
        }
        return true;
    }

    public bool KiemTonTai(GridView grvData, DataRow dr, int iCot, string sDLKiem, string tabName, string ColName, string ColName1,string sform)
    {
        //null không kiểm
        if (!string.IsNullOrEmpty(sDLKiem))
        {
            if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo." + tabName + " WHERE " + ColName + "+ ' ' +" + ColName1 + " = N'" + sDLKiem + "'")) == 0)
            {
                dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(sform, "msgChuaTonTaiCSDL"));
                dr["XOA"] = 1;
                return false;
            }
        }
        return true;
    }


    public bool KiemDuLieuNgay(GridView grvData, DataRow dr, int iCot, Boolean bKiemNull, string sform)
    {
        string sDLKiem;
        sDLKiem = dr[grvData.Columns[iCot].FieldName.ToString()].ToString();
        DateTime DLKiem;

        try
        {

            if (bKiemNull)
            {
                if (string.IsNullOrEmpty(sDLKiem))
                {
                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongduocTrong"));
                    dr["XOA"] = 1;
                    return false;
                }
                else
                {
                    //sDLKiem = DateTime.ParseExact(sDLKiem, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString();
                    if (!DateTime.TryParse(sDLKiem, out DLKiem))
                    {
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
                        dr["XOA"] = 1;
                        return false;
                    }

                }
            }
            else
            {
                if (!string.IsNullOrEmpty(sDLKiem))
                {
                    if (!DateTime.TryParse(sDLKiem, out DLKiem))
                    {
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
                        dr["XOA"] = 1;
                        return false;
                    }
                }
            }
        }
        catch
        {
            dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
            dr["XOA"] = 1;
            return false;
        }
        return true;
    }

    public bool KiemDuLieuNgay(GridView grvData, DataRow dr, int iCot, string sTenKTra, Boolean bKiemNull, string GTSoSanh, int iKieuSS)
    {
        // iKieuSS = 1 la so sanh = 
        // iKieuSS = 2 la so sanh nho hon giá trị so sanh
        // iKieuSS = 3 la so sanh nho hon hoac bang
        // iKieuSS = 4 la so sanh lon hon
        // iKieuSS = 5 la so sanh lon hon hoac bang
        try
        {
            string sDLKiem;
            sDLKiem = DateTime.Parse(dr[grvData.Columns[iCot].FieldName.ToString()].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
            DateTime DLKiem;
            DateTime DLSSanh;
            DateTime.TryParse(GTSoSanh, out DLSSanh);

            if (bKiemNull)
            {
                if (string.IsNullOrEmpty(sDLKiem))
                {
                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được để trống");
                    dr["XOA"] = 1;
                    return false;
                }
                else
                {
                    if (!DateTime.TryParse(sDLKiem, out DLKiem))
                    {
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " phải là datetime");
                        dr["XOA"] = 1;
                        return false;
                    }
                    else
                    {
                        if (DateTime.Parse(GTSoSanh) != DateTime.Parse("01/01/1900"))
                        {
                            #region Giá trị so sánh
                            //iKieuSS = 1 la so sanh = 
                            if (iKieuSS == 1)
                            {
                                if (DLKiem == DLSSanh)
                                {
                                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được bằng " + DLSSanh.ToShortDateString());
                                    dr["XOA"] = 1;
                                    return false;
                                }
                            }
                            // iKieuSS = 2 la so sanh nho hon giá trị so sanh
                            if (iKieuSS == 2)
                            {
                                if (DLKiem < DLSSanh)
                                {
                                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được nhỏ hơn " + DLSSanh.ToShortDateString());
                                    dr["XOA"] = 1;
                                    return false;
                                }
                            }
                            // iKieuSS = 3 la so sanh nho hon hoac bang
                            if (iKieuSS == 3)
                            {
                                if (DLKiem <= DLSSanh)
                                {
                                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được nhỏ hơn hay bằng " + DLSSanh.ToShortDateString());
                                    dr["XOA"] = 1;
                                    return false;
                                }
                            }
                            // iKieuSS = 4 la so sanh lon hon
                            if (iKieuSS == 4)
                            {
                                if (DLKiem > DLSSanh)
                                {
                                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được lớn hơn " + DLSSanh.ToShortDateString());
                                    dr["XOA"] = 1;
                                    return false;
                                }
                            }
                            // iKieuSS = 5 la so sanh lon hon hoac bang
                            if (iKieuSS >= 5)
                            {
                                if (DLKiem < DLSSanh)
                                {
                                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được lớn hơn hay bằng " + DLSSanh.ToShortDateString());
                                    dr["XOA"] = 1;
                                    return false;
                                }
                            }
                            #endregion
                        }
                    }

                }
            }
            else
            {
                if (!string.IsNullOrEmpty(sDLKiem))
                {
                    if (!DateTime.TryParse(sDLKiem, out DLKiem))
                    {
                        dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " phải là datetime");
                        dr["XOA"] = 1;
                        return false;
                    }
                    else
                    {
                        if (GTSoSanh != "01/01/1900")
                        {
                            #region Giá trị so sánh
                            //iKieuSS = 1 la so sanh = 
                            if (iKieuSS == 1)
                            {
                                if (DLKiem == DLSSanh)
                                {
                                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được bằng " + DLSSanh.ToShortDateString());
                                    dr["XOA"] = 1;
                                    return false;
                                }
                            }
                            // iKieuSS = 2 la so sanh nho hon giá trị so sanh
                            if (iKieuSS == 2)
                            {
                                if (DLKiem < DLSSanh)
                                {
                                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được nhỏ hơn " + DLSSanh.ToShortDateString());
                                    dr["XOA"] = 1;
                                    return false;
                                }
                            }
                            // iKieuSS = 3 la so sanh nho hon hoac bang
                            if (iKieuSS == 3)
                            {
                                if (DLKiem <= DLSSanh)
                                {
                                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được nhỏ hơn hay bằng " + DLSSanh.ToShortDateString());
                                    dr["XOA"] = 1;
                                    return false;
                                }
                            }
                            // iKieuSS = 4 la so sanh lon hon
                            if (iKieuSS == 4)
                            {
                                if (DLKiem > DLSSanh)
                                {
                                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được lớn hơn " + DLSSanh.ToShortDateString());
                                    dr["XOA"] = 1;
                                    return false;
                                }
                            }
                            // iKieuSS = 5 la so sanh lon hon hoac bang
                            if (iKieuSS >= 5)
                            {
                                if (DLKiem < DLSSanh)
                                {
                                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " không được lớn hơn hay bằng " + DLSSanh.ToShortDateString());
                                    dr["XOA"] = 1;
                                    return false;
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
        }
        catch
        {
            dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + " phải là datetime");
            dr["XOA"] = 1;
            return false;
        }
        return true;
    }

    public bool KiemDuLieuSo(GridView grvData, DataRow dr, int iCot, string sTenKTra, double GTSoSanh, double GTMacDinh, Boolean bKiemNull, string sForm)
    {
        string sDLKiem;
        sDLKiem = dr[grvData.Columns[iCot].FieldName.ToString()].ToString();
        double DLKiem;
        if (bKiemNull)
        {
            if (string.IsNullOrEmpty(sDLKiem))
            {
                dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongduocTrong"));
                dr["XOA"] = 1;
                return false;
            }
            else
            {
                if (!double.TryParse(dr[grvData.Columns[iCot].FieldName.ToString()].ToString(), out DLKiem))
                {
                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongPhaiSo"));
                    dr["XOA"] = 1;
                    return false;
                }
                else
                {
                    if (GTSoSanh != -999999)
                    {
                        if (DLKiem < GTSoSanh)
                        {
                            dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongNhoHon") + GTSoSanh.ToString());
                            dr["XOA"] = 1;
                            return false;
                        }

                        DLKiem = Math.Round(DLKiem, 8);
                        dr[grvData.Columns[iCot].FieldName.ToString()] = DLKiem.ToString();

                    }
                }
            }
        }
        else
        {
            if (string.IsNullOrEmpty(sDLKiem) && GTMacDinh != -999999)
            {
                dr[grvData.Columns[iCot].FieldName.ToString()] = GTMacDinh;
                DLKiem = GTMacDinh;
                sDLKiem = GTMacDinh.ToString();
            }

            if (!string.IsNullOrEmpty(sDLKiem))
            {
                if (!double.TryParse(dr[grvData.Columns[iCot].FieldName.ToString()].ToString(), out DLKiem))
                {
                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongPhaiSo"));
                    dr["XOA"] = 1;
                    return false;
                }
                else
                {
                    if (GTSoSanh != -999999)
                    {
                        if (DLKiem < GTSoSanh)
                        {
                            dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongNhoHon") + GTSoSanh.ToString());
                            dr["XOA"] = 1;
                            return false;
                        }

                        DLKiem = Math.Round(DLKiem, 8);
                        dr[grvData.Columns[iCot].FieldName.ToString()] = DLKiem.ToString();
                    }

                }
            }


        }



        return true;
    }

    public bool KiemDuLieuBool(GridView grvData, DataRow dr, int iCot, string sTenKTra, string GTMacDinh)
    {
        if (string.IsNullOrEmpty(sTenKTra))
        {
            dr[grvData.Columns[iCot].FieldName.ToString()] = GTMacDinh;
            sTenKTra = GTMacDinh.ToString();
            dr[grvData.Columns[iCot].FieldName.ToString()] = sTenKTra;

        }

        if (!string.IsNullOrEmpty(sTenKTra))
        {
            try
            {
                sTenKTra = sTenKTra.Trim() == "1" ? "True" : "False";
            }
            catch
            {
                dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "KhongPhaiKieuBool"));
                dr["XOA"] = 1;
                return false; ;
            }
        }
        return true;
    }

    public bool KiemDuLieuSo(GridView grvData, DataRow dr, int iCot, string sTenKTra, double GTSoSanh, double GTMacDinh, Boolean bKiemNull, double GTTKhoang, string sForm)
    {
        double DLKiem;
        if (bKiemNull)
        {
            if (string.IsNullOrEmpty(sTenKTra))
            {
                dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongduocTrong"));
                dr["XOA"] = 1;
                return false;
            }
            else
            {
                if (!double.TryParse(sTenKTra, out DLKiem))
                {
                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongPhaiSo"));
                    dr["XOA"] = 1;
                    return false;
                }
                else
                {
                    if (GTSoSanh != -999999)
                    {
                        if (DLKiem < GTSoSanh || DLKiem > GTTKhoang)
                        {
                            dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongNhoHon") +
                                GTSoSanh.ToString() + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgVaLonHon") + GTTKhoang.ToString());
                            dr["XOA"] = 1;
                            return false;
                        }
                    }
                }
            }
        }
        else
        {
            if (string.IsNullOrEmpty(sTenKTra) && GTMacDinh != -999999)
            {
                dr[grvData.Columns[iCot].FieldName.ToString()] = GTMacDinh;
                DLKiem = GTMacDinh;
                sTenKTra = GTMacDinh.ToString();
            }

            if (!string.IsNullOrEmpty(sTenKTra))
            {
                if (!double.TryParse(sTenKTra, out DLKiem))
                {
                    dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongPhaiSo"));
                    dr["XOA"] = 1;
                    return false;
                }
                else
                {
                    if (GTSoSanh != -999999)
                    {
                        if (DLKiem < GTSoSanh || DLKiem > GTTKhoang)
                        {
                            dr.SetColumnError(grvData.Columns[iCot].FieldName.ToString(), sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongNhoHon") +
                                   GTSoSanh.ToString() + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgVaLonHon") + GTTKhoang.ToString());
                            dr["XOA"] = 1;
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    public void KiemData(string Table, string Field, int dong, int Cot, DataRow row)
    {
        try
        {
            Commons.frmPopUp frmPopUp = new Commons.frmPopUp();
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "select * from " + Table));
            frmPopUp.TableSource = dt;
            if (frmPopUp.ShowDialog() == DialogResult.OK)
                row[Cot] = frmPopUp.RowSelected[Field].ToString();
        }
        catch { }
    }

    public void KiemData(string squery, string Field, int Cot, DataRow row)
    {
        try
        {
            Commons.frmPopUp frmPopUp = new Commons.frmPopUp();
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, squery));
            frmPopUp.TableSource = dt;
            if (frmPopUp.ShowDialog() == DialogResult.OK)
                row[Cot] = frmPopUp.RowSelected[Field].ToString();
        }
        catch { }
    }


    #endregion

}
