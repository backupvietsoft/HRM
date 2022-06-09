using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Vs.Payroll
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string connection = @"OLEDB;Provider=SQLOLEDB.1;Integrated Security=SSPI;Server=192.168.0.1\SQL2005;DataBase=Test;UID=sa;PWD=pass@123";
            string command = "SELECT MS_CN, MS_HIEN_THI, THANH_TIEN FROM TEST_PIVOT";
            Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook workbook = (Microsoft.Office.Interop.Excel.Workbook)app.Workbooks.Add(Type.Missing);
            Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;
            Excel.PivotCache pivotCache = app.ActiveWorkbook.PivotCaches().Add(Excel.XlPivotTableSourceType.xlExternal, (Excel.Range)sheet.get_Range("A1", "E10"));
            pivotCache.Connection = Commons.IConnections.CNStr;
            pivotCache.MaintainConnection = true;
            pivotCache.CommandText = command;
            pivotCache.CommandType = Excel.XlCmdType.xlCmdSql;
            Excel.PivotTables pivotTables = (Excel.PivotTables)sheet.PivotTables(Type.Missing);
            Excel.PivotTable pivotTable = pivotTables.Add(pivotCache, app.ActiveCell, "PivotTable1", Type.Missing, Type.Missing);
            pivotTable.SmallGrid = false;
            pivotTable.ShowTableStyleRowStripes = true;
            pivotTable.TableStyle2 = "PivotStyleLight1";
            Excel.PivotFields rowField = (Excel.PivotFields)pivotTable.PivotFields(Type.Missing);
            int fieldCount = rowField.Count;

            //for (int i = 1; i <= fieldCount; i++)
            //{
            //    if ("Colunm" + i != "Colunm2" && "Colunm" + i != "Colunm5")
            //    {
                    Excel.PivotField field = (Excel.PivotField)pivotTable.PivotFields("MS_HIEN_THI");
                    field.Orientation = Excel.XlPivotFieldOrientation.xlRowField;
            //    }
            //}

            pivotTable.AddDataField(pivotTable.PivotFields("THANH_TIEN"), "Sum of Column4", Excel.XlConsolidationFunction.xlSum);
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
