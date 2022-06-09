using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Spreadsheet;
using DevExpress.DataAccess.Excel;
using System.Collections;
using Microsoft.ApplicationBlocks.Data;

namespace Vs.Payroll
{
    public partial class frmLayDuLieuLuongT13 : DevExpress.XtraEditors.XtraForm
    {
        public DataTable dt;
        public string ColName;
        public frmLayDuLieuLuongT13()
        {
            InitializeComponent();
        }

        #region even
        private void frmLayDuLieuLuongT13_Load(object sender, EventArgs e)
        {
            txtChonFile.Focus();
            LoadCbo();
            LoadNN();
        }
        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "laydulieu":
                    {
                        if (string.IsNullOrEmpty(cboSheet.Text.Trim()))
                        {
                            XtraMessageBox.Show(lblSheet.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                            cboSheet.Focus();
                            return;
                        }

                        if (string.IsNullOrEmpty(cboCotLayDL.Text.Trim()))
                        {
                            XtraMessageBox.Show(lblCotLayDL.Text + " " + Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgKhongDuocTrong"));
                            cboCotLayDL.Focus();
                            return;
                        }

                        try
                        {

                            var source = new ExcelDataSource();
                            source.FileName = txtChonFile.Text;
                            var worksheetSettings = new ExcelWorksheetSettings(cboSheet.Text);
                            source.SourceOptions = new ExcelSourceOptions(worksheetSettings);
                            source.Fill();
                            dt = new DataTable();
                            dt = ToDataTable(source);
                            ColName = cboCotLayDL.EditValue.ToString();
                            //dtemp.Columns.Add("XOA", System.Type.GetType("System.Boolean"));
                            ////grdChung.DataSource = dtemp;
                            
                            ////Commons.Mod.OS.MLoadXtraGrid(grdChung, grvChung, dtemp, true, true, false, true);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        { XtraMessageBox.Show(ex.Message); }
                        break;
                    }

                case "thoat":
                    {
                        this.Close();
                        break;
                    }
            }
        }

        private void txtChonFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string sPath = "";
            sPath = Commons.Modules.ObjSystems.OpenFiles("All Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|" + "All Files (*.*)|*.*");


            if (sPath == "") return;
            txtChonFile.Text = sPath;
            try
            {
                cboSheet.Properties.Items.Clear();
                Workbook workbook = new Workbook();

                string ext = System.IO.Path.GetExtension(sPath);
                if (ext.ToLower() == ".xlsx")
                    workbook.LoadDocument(txtChonFile.Text, DevExpress.Spreadsheet.DocumentFormat.Xlsx);
                else
                    workbook.LoadDocument(txtChonFile.Text, DevExpress.Spreadsheet.DocumentFormat.Xls);
                List<string> wSheet = new List<string>();
                for (int i = 0; i < workbook.Worksheets.Count; i++)
                {
                    wSheet.Add(workbook.Worksheets[i].Name.ToString());
                }
                cboSheet.Properties.Items.AddRange(wSheet);

                cboSheet.EditValue = wSheet[0].ToString();
            }
            catch (InvalidOperationException ex)
            { XtraMessageBox.Show(ex.Message); }

        }
        private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            //grdChung.DataSource = null;
            
        }
        #endregion

        #region function
        private void LoadNN()
        {
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, btnALL);
        }
        private void LoadCbo()
        {
            #region loadCboTay
            //System.Data.DataTable dt = new DataTable();
            //DataColumn dtC;
            //DataRow dtR;
            //dtC = new DataColumn();
            //dtC.DataType = typeof(string);
            //dtC.ColumnName = "TEN_COL";
            //dtC.Caption = "TEN_COL";
            //dtC.ReadOnly = false;
            //dtC.Unique = true;
            //dt.Columns.Add(dtC);

            //dtC = new DataColumn();
            //dtC.DataType = typeof(string);
            //dtC.ColumnName = "Value";
            //dtC.Caption = "Value";
            //dtC.ReadOnly = false;
            //dtC.Unique = true;
            //dt.Columns.Add(dtC);

            //dtR = dt.NewRow();
            //dtR["TEN_COL"] = "T_1";
            //dtR["Value"] = "Tháng 1";
            //dt.Rows.Add(dtR);

            //dtR = dt.NewRow();
            //dtR["TEN_COL"] = "T_2";
            //dtR["Value"] = "Tháng 2";
            //dt.Rows.Add(dtR);

            //dtR = dt.NewRow();
            //dtR["TEN_COL"] = "T_3";
            //dtR["Value"] = "Tháng 3";
            //dt.Rows.Add(dtR);

            //dtR = dt.NewRow();
            //dtR["TEN_COL"] = "T_4";
            //dtR["Value"] = "Tháng 4";
            //dt.Rows.Add(dtR);

            //dtR = dt.NewRow();
            //dtR["TEN_COL"] = "T_5";
            //dtR["Value"] = "Tháng 5";
            //dt.Rows.Add(dtR);

            //dtR = dt.NewRow();
            //dtR["TEN_COL"] = "T_6";
            //dtR["Value"] = "Tháng 6";
            //dt.Rows.Add(dtR);

            //dtR = dt.NewRow();
            //dtR["TEN_COL"] = "T_7";
            //dtR["Value"] = "Tháng 7";
            //dt.Rows.Add(dtR);

            //dtR = dt.NewRow();
            //dtR["TEN_COL"] = "T_8";
            //dtR["Value"] = "Tháng 8";
            //dt.Rows.Add(dtR);

            //dtR = dt.NewRow();
            //dtR["TEN_COL"] = "T_9";
            //dtR["Value"] = "Tháng 9";
            //dt.Rows.Add(dtR);

            //dtR = dt.NewRow();
            //dtR["TEN_COL"] = "T_10";
            //dtR["Value"] = "Tháng 10";
            //dt.Rows.Add(dtR);

            //dtR = dt.NewRow();
            //dtR["TEN_COL"] = "T_11";
            //dtR["Value"] = "Tháng 11";
            //dt.Rows.Add(dtR);

            //dtR = dt.NewRow();
            //dtR["TEN_COL"] = "T_12";
            //dtR["Value"] = "Tháng 12";
            //dt.Rows.Add(dtR);

            //dtR = dt.NewRow();
            //dtR["TEN_COL"] = "LUONG_T13";
            //dtR["Value"] = "Thưởng T13";
            //dt.Rows.Add(dtR);

            //dtR = dt.NewRow();
            //dtR["TEN_COL"] = "THUONG_HQ_KD";
            //dtR["Value"] = "Thưởng HQKD";
            //dt.Rows.Add(dtR);


            //cboCotLayDL.Properties.DataSource = dt;
            //cboCotLayDL.Properties.DisplayMember = "Value";
            //cboCotLayDL.Properties.ValueMember = "ID";
            //cboCotLayDL.Properties.PopulateColumns();
            //cboCotLayDL.Properties.Columns["ID"].Visible = false;
            #endregion

            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCotLayDL", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.MLoadLookUpEditN(cboCotLayDL, dt, "TEN_COT", "NAME_COL", "NAME_COL", "");
            }
            catch
            {

            }
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
                table.Columns.Add(prop.Name.Trim(), prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (DevExpress.DataAccess.Native.Excel.ViewRow item in list)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        #endregion
    }
}
