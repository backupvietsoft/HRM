using NPOI.SS.Formula;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

namespace Commons
{
    public static class TemplateExcel
    {
        public static void FillReport(string filename, string templatefilename, DataSet data)
        {
            FillReport(filename, templatefilename, data, new string[] { "%", "%" });
        }

        public static void FillReportSum(string filename, string templatefilename, DataSet data, string[] deliminator, string[] cellTD = null)
        {
            try
            {
                //AutoFill
                if (File.Exists(filename))
                    File.Delete(filename);
                using (var file = new FileStream(filename, FileMode.CreateNew))
                {
                    using (var temp = new FileStream(templatefilename, FileMode.Open))
                    {
                        using (var xls = new ExcelPackage(file, temp))
                        {
                            try
                            {

                                foreach (var ws in xls.Workbook.Worksheets)
                                {
                                    foreach (var c in cellTD)
                                    {
                                        var s = "" + ws.Cells[c].Value;
                                        if (s.StartsWith(deliminator[0]) == false &&
                                            s.EndsWith(deliminator[1]) == false)
                                            continue;
                                        s = s.Replace(deliminator[0], "").Replace(deliminator[1], "");
                                        var ss = s.Split('.');
                                        try
                                        {
                                            ws.Cells[c].Value = data.Tables[ss[0].Trim()].Rows[0][ss[1].Trim()];
                                        }
                                        catch
                                        { }
                                    }
                                    foreach (var n in ws.Names)
                                    {
                                        FillWorksheetDataSum(data, ws, n, deliminator);
                                    }
                                }
                                foreach (var n in xls.Workbook.Names)
                                {
                                    FillWorksheetDataSum(data, n.Worksheet, n, deliminator);
                                }
                            }
                            catch (System.Exception ex)
                            {
                            }

                            xls.Save();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
            }
        }
        private static void FillWorksheetDataSum(DataSet data, ExcelWorksheet ws, ExcelNamedRange n, string[] deliminator)
        {
            try
            {
                if (data.Tables.Contains(n.Name) == false)
                    return;
                var dt = data.Tables[n.Name];
                int row = n.Start.Row;
                int rowStar = n.Start.Row;
                ws.InsertRow(row + 1, data.Tables[0].Rows.Count - 2, 1);
                var cn = new string[n.Columns];
                var st = new int[n.Columns];
                int text = 0;
                try
                {
                    for (int i = 0; i < n.Columns; i++)
                    {
                        text = i;
                        cn[i] = (n.Value as object[,])[0, i].ToString().Replace(deliminator[0], "").Replace(deliminator[1], "");
                        if (cn[i].Contains("."))
                            cn[i] = cn[i].Split('.')[1];
                        st[i] = ws.Cells[row, n.Start.Column + i].StyleID;
                    }
                }
                catch
                {
                    string a = dt.Columns[text].Caption;
                }
                foreach (DataRow r in dt.Rows)
                {
                    for (int col = 0; col < n.Columns; col++)
                    {
                        try
                        {
                            if (dt.Columns.Contains(cn[col]))
                            {
                                ws.Cells[row, n.Start.Column + col].Value = r[cn[col]];
                            }
                            else
                            {
                                ws.Cells[row, n.Start.Column + col].FormulaR1C1 = ws.Cells[rowStar, n.Start.Column + col].FormulaR1C1;
                            }
                            ws.Cells[row, n.Start.Column + col].StyleID = st[col];
                        }
                        catch
                        {
                        }
                    }
                    row++;
                }
                //auto fill các công thức
                //ExcelRange sourceCell;
                //ExcelRange targetRange;
                //foreach (var item in listafill)
                //{
                //    sourceCell = ws.Cells[rowStar,item +1];
                //    targetRange = ws.Cells[rowStar, item +1, rowStar + dt.Rows.Count, item+1];
                //    sourceCell.FormulaR1C1 targetRange, ExcelAutoFillType.FillDefault);
                //}
                // extend table formatting range to all rows
                foreach (var t in ws.Tables)
                {
                    var a = t.Address;
                    if (n.Start.Row.Between(a.Start.Row, a.End.Row) &&
                        n.Start.Column.Between(a.Start.Column, a.End.Column))
                    {
                        ExtendRows(t, dt.Rows.Count - 1);
                    }

                }
            }
            catch (System.Exception ex)
            {

            }
        }


        public static void FillReport(string filename, string templatefilename, DataSet data, string[] deliminator)
        {
            try
            {

                if (File.Exists(filename))
                    File.Delete(filename);
                using (var file = new FileStream(filename, FileMode.CreateNew))
                {
                    using (var temp = new FileStream(templatefilename, FileMode.Open))
                    {
                        using (var xls = new ExcelPackage(file, temp))
                        {
                            foreach (var n in xls.Workbook.Names)
                            {
                                FillWorksheetData(data, n.Worksheet, n, deliminator);
                            }

                            foreach (var ws in xls.Workbook.Worksheets)
                            {
                                foreach (var n in ws.Names)
                                {
                                    FillWorksheetData(data, ws, n, deliminator);
                                }
                            }

                            foreach (var ws in xls.Workbook.Worksheets)
                            {
                                foreach (var c in ws.Cells)
                                {
                                    var s = "" + c.Value;
                                    if (s.StartsWith(deliminator[0]) == false &&
                                        s.EndsWith(deliminator[1]) == false)
                                        continue;
                                    s = s.Replace(deliminator[0], "").Replace(deliminator[1], "");
                                    var ss = s.Split('.');
                                    try
                                    {
                                        c.Value = data.Tables[ss[0]].Rows[0][ss[1]];
                                    }
                                    catch
                                    { }
                                }
                            }

                            xls.Save();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
            }
        }
        private static void FillWorksheetData(DataSet data, ExcelWorksheet ws, ExcelNamedRange n, string[] deliminator)
        {
            if (data.Tables.Contains(n.Name) == false)
                return;

            var dt = data.Tables[n.Name];

            int row = n.Start.Row;

            var cn = new string[n.Columns];
            var st = new int[n.Columns];
            int text = 0;
            try
            {


                for (int i = 0; i < n.Columns; i++)
                {
                    text = i;
                    cn[i] = (n.Value as object[,])[0, i].ToString().Replace(deliminator[0], "").Replace(deliminator[1], "");
                    if (cn[i].Contains("."))
                        cn[i] = cn[i].Split('.')[1];
                    st[i] = ws.Cells[row, n.Start.Column + i].StyleID;
                }
            }
            catch
            {
                string a = dt.Columns[text].Caption;
            }

            foreach (DataRow r in dt.Rows)
            {
                for (int col = 0; col < n.Columns; col++)
                {
                    if (dt.Columns.Contains(cn[col]))
                        ws.Cells[row, n.Start.Column + col].Value = r[cn[col]];
                    ws.Cells[row, n.Start.Column + col].StyleID = st[col];
                }
                row++;
            }

            // extend table formatting range to all rows
            foreach (var t in ws.Tables)
            {
                var a = t.Address;
                if (n.Start.Row.Between(a.Start.Row, a.End.Row) &&
                    n.Start.Column.Between(a.Start.Column, a.End.Column))
                {
                    ExtendRows(t, dt.Rows.Count - 1);
                }

            }
        }
        public static void ExtendRows(ExcelTable excelTable, int count)
        {

            var ad = new ExcelAddress(excelTable.Address.Start.Row,
                                      excelTable.Address.Start.Column,
                                      excelTable.Address.End.Row + count,
                                      excelTable.Address.End.Column);
            //Address = ad;
        }
        public static void FillReportSum(string filename, string templatefilename, DataSet data, string[] deliminator, string[] cellTD = null)
        {
            try
            {
                //AutoFill
                if (File.Exists(filename))
                    File.Delete(filename);
                using (var file = new FileStream(filename, FileMode.CreateNew))
                {
                    using (var temp = new FileStream(templatefilename, FileMode.Open))
                    {
                        using (var xls = new ExcelPackage(file, temp))
                        {
                            try
                            {

                                foreach (var ws in xls.Workbook.Worksheets)
                                {
                                    foreach (var c in cellTD)
                                    {
                                        var s = "" + ws.Cells[c].Value;
                                        if (s.StartsWith(deliminator[0]) == false &&
                                            s.EndsWith(deliminator[1]) == false)
                                            continue;
                                        s = s.Replace(deliminator[0], "").Replace(deliminator[1], "");
                                        var ss = s.Split('.');
                                        try
                                        {
                                            ws.Cells[c].Value = data.Tables[ss[0].Trim()].Rows[0][ss[1].Trim()];
                                        }
                                        catch
                                        { }
                                    }
                                    foreach (var n in ws.Names)
                                    {
                                        FillWorksheetDataSum(data, ws, n, deliminator);
                                    }
                                }
                                foreach (var n in xls.Workbook.Names)
                                {
                                    FillWorksheetDataSum(data, n.Worksheet, n, deliminator);
                                }
                            }
                            catch (System.Exception ex)
                            {
                            }

                            xls.Save();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
            }
        }

        private static void FillWorksheetDataSum(DataSet data, ExcelWorksheet ws, ExcelNamedRange n, string[] deliminator)
        {
            try
            {
                if (data.Tables.Contains(n.Name) == false)
                    return;
                var dt = data.Tables[n.Name];
                int row = n.Start.Row;
                int rowStar = n.Start.Row;
                ws.InsertRow(row + 1, data.Tables[0].Rows.Count - 2, 1);
                var cn = new string[n.Columns];
                var st = new int[n.Columns];
                int text = 0;
                try
                {
                    for (int i = 0; i < n.Columns; i++)
                    {
                        text = i;
                        cn[i] = (n.Value as object[,])[0, i].ToString().Replace(deliminator[0], "").Replace(deliminator[1], "");
                        if (cn[i].Contains("."))
                            cn[i] = cn[i].Split('.')[1];
                        st[i] = ws.Cells[row, n.Start.Column + i].StyleID;
                    }
                }
                catch
                {
                    string a = dt.Columns[text].Caption;
                }
                foreach (DataRow r in dt.Rows)
                {
                    for (int col = 0; col < n.Columns; col++)
                    {
                        try
                        {
                            if (dt.Columns.Contains(cn[col]))
                            {
                                ws.Cells[row, n.Start.Column + col].Value = r[cn[col]];
                            }
                            else
                            {
                                ws.Cells[row, n.Start.Column + col].FormulaR1C1 = ws.Cells[rowStar, n.Start.Column + col].FormulaR1C1;
                            }
                            ws.Cells[row, n.Start.Column + col].StyleID = st[col];
                        }
                        catch
                        {
                        }
                    }
                    row++;
                }
                //auto fill các công thức
                //ExcelRange sourceCell;
                //ExcelRange targetRange;
                //foreach (var item in listafill)
                //{
                //    sourceCell = ws.Cells[rowStar,item +1];
                //    targetRange = ws.Cells[rowStar, item +1, rowStar + dt.Rows.Count, item+1];
                //    sourceCell.FormulaR1C1 targetRange, ExcelAutoFillType.FillDefault);
                //}
                // extend table formatting range to all rows
                foreach (var t in ws.Tables)
                {
                    var a = t.Address;
                    if (n.Start.Row.Between(a.Start.Row, a.End.Row) &&
                        n.Start.Column.Between(a.Start.Column, a.End.Column))
                    {
                        ExtendRows(t, dt.Rows.Count - 1);
                    }

                }
            }
            catch (System.Exception ex)
            {

            }
        }
    }
    public static class int_between
    {
        public static bool Between(this int v, int a, int b)
        {
            return v >= a && v <= b;
        }
    }
}

