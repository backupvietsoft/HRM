using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using Vs.Report;
using System.Globalization;
using DevExpress.DataAccess.Excel;
using System.Collections;
using DevExpress.Spreadsheet;
using DevExpress.Utils;
using System.Drawing;

namespace Vs.TimeAttendance
{
    public partial class ucDangKiLamThem : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;
        private DateTime dGioBatDau;
        private DateTime dGioKetThuc;
        public static ucDangKiLamThem _instance;
        public static ucDangKiLamThem Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDangKiLamThem();
                return _instance;
            }
        }
        CultureInfo cultures = new CultureInfo("en-US");

        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        /// <summary>
        /// 
        /// </summary>
        public ucDangKiLamThem()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);
            repositoryItemTimeEdit1 = new RepositoryItemTimeEdit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucDangKiLamThem_Load(object sender, EventArgs e)
        {
            isAdd = false;
            Commons.Modules.sLoad = "0Load";
            repositoryItemTimeEdit1.TimeEditStyle = TimeEditStyle.TouchUI;
            repositoryItemTimeEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            repositoryItemTimeEdit1.Mask.EditMask = "HH:mm";

            repositoryItemTimeEdit1.NullText = "00:00";
            repositoryItemTimeEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemTimeEdit1.DisplayFormat.FormatString = "HH:mm";
            repositoryItemTimeEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemTimeEdit1.EditFormat.FormatString = "HH:mm";

            EnableButon();
            LoadNgay();
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);

            //DataTable dtNCC = new DataTable();
            //dtNCC.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNhomChamCong", Commons.Modules.UserName, Commons.Modules.TypeLanguage, false));
            //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_NCC, dtNCC, "ID_NHOM", "TEN_NHOM", "TEN_NHOM");

            LoadGridCongNhan();
            LoadGrdDSLamThem();



            //cboCa.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_CDLV"));
            //cboCa.Columns["ID_CDLV"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "ID_CDLV");
            //cboCa.Columns["ID_CDLV"].Visible = false;


            //cboCa.ButtonClick += CboCa_EditValueChanged;
            //cboCa.Click += CboCa_EditValueChanged;
            Commons.Modules.sLoad = "";
            grvCongNhan_FocusedRowChanged(null, null);

        }

        private void CboCa_EditValueChanged(object sender, EventArgs e)
        {
            try
            {


                LookUpEdit lookUp = sender as LookUpEdit;

                //string id = lookUp.get;

                // Access the currently selected data row
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;

                grvLamThem.SetFocusedRowCellValue("GIO_BD", dataRow.Row["GIO_BD"]);
                grvLamThem.SetFocusedRowCellValue("GIO_KT", dataRow.Row["GIO_KT"]);
                grvLamThem.SetFocusedRowCellValue("ID_CDLV", dataRow.Row["ID_CDLV"].ToString());

                dGioBatDau = new DateTime();
                dGioKetThuc = new DateTime();
                dGioBatDau = Convert.ToDateTime(dataRow.Row["GIO_BD"]);
                dGioKetThuc = Convert.ToDateTime(dataRow.Row["GIO_KT"]);
            }
            catch { }
        }

        DataTable dtCaLV;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboCa_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)grdCongNhan.DataSource;
                string sCa = grvCongNhan.GetFocusedRowCellValue("CA").ToString();
                if (sCa == "")
                {
                    sCa = "-1";
                }
                dtCaLV = new DataTable();
                dtCaLV.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCaLVThem", cboNgay.EditValue, grvCongNhan.GetFocusedRowCellValue("ID_NHOM"), sCa, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                if (sender is LookUpEdit cbo)
                {
                    cbo.Properties.DataSource = null;
                    cbo.Properties.DataSource = dtCaLV;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// load Grid
        /// </summary>
        private void LoadGrdDSLamThem()
        {
            try
            {
                decimal idCongNhan = -1;
                DataTable dt = new DataTable();

                //grvLamThem.OptionsBehavior.Editable = true;
                grdLamThem.DataSource = null;
                if (grvCongNhan.FocusedRowHandle >= 0)
                {
                    decimal.TryParse(grvCongNhan.GetFocusedRowCellValue("ID_CN").ToString(), out idCongNhan);
                }
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListLamThem", Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd"), cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue,
                                                Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dt.Columns["COM_CA"].ReadOnly = false;
                dt.Columns["ID_CDLV"].ReadOnly = false;
                dt.Columns["ID_NHOM"].ReadOnly = false;
                dt.Columns["GIO_BD"].ReadOnly = false;
                dt.Columns["GIO_KT"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdLamThem, grvLamThem, dt, true, true, false, false, true, this.Name);
                grvLamThem.Columns["COM_CA"].Visible = false;

                DataTable dID_NHOM = new DataTable();
                dID_NHOM.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetNhomCC", cboNgay.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.AddCombXtra("ID_NHOM", "TEN_NHOM", grvLamThem, dID_NHOM, false, "ID_NHOM", "NHOM_CHAM_CONG");


                FormatGrvLamThem();
                if (isAdd)
                {
                    grvLamThem.OptionsBehavior.Editable = true;
                }
                else
                {
                    grvLamThem.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                    grvLamThem.OptionsBehavior.Editable = false;
                }

                //grvLamThem.Columns["SO_GIO_TC"].DisplayFormat.FormatType = FormatType.Numeric;
                //grvLamThem.Columns["SO_GIO_TC"].DisplayFormat.FormatString = "0.0";

                RepositoryItemTextEdit txtEdit = new RepositoryItemTextEdit();
                txtEdit.Properties.DisplayFormat.FormatString = "00.00";
                txtEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                txtEdit.Properties.EditFormat.FormatString = "00.00";
                txtEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                txtEdit.Properties.Mask.EditMask = "00.00";
                txtEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                txtEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
                grvLamThem.Columns["SO_GIO_TC"].ColumnEdit = txtEdit;

                DataTable dCa = new DataTable();
                RepositoryItemLookUpEdit cboCa = new RepositoryItemLookUpEdit();
                dCa.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT CONCAT(CA, ';', ID_CDLV) AS ID_CDLV, CA, GIO_BD, GIO_KT, PHUT_BD, PHUT_KT  FROM dbo.CHE_DO_LAM_VIEC"));
                cboCa.NullText = "";
                cboCa.ValueMember = "ID_CDLV";
                cboCa.DisplayMember = "CA";
                cboCa.DataSource = dCa;
                cboCa.Columns.Clear();

                cboCa.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CA"));
                cboCa.Columns["CA"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "CA");

                cboCa.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GIO_BD"));
                cboCa.Columns["GIO_BD"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "GIO_BD");
                cboCa.Columns["GIO_BD"].FormatType = DevExpress.Utils.FormatType.DateTime;
                cboCa.Columns["GIO_BD"].FormatString = "HH:mm";

                cboCa.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GIO_KT"));
                cboCa.Columns["GIO_KT"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "GIO_KT");
                cboCa.Columns["GIO_KT"].FormatType = DevExpress.Utils.FormatType.DateTime;
                cboCa.Columns["GIO_KT"].FormatString = "HH:mm";

                cboCa.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboCa.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                grvLamThem.Columns["ID_CDLV"].ColumnEdit = cboCa;

                cboCa.BeforePopup += cboCa_BeforePopup;
                cboCa.EditValueChanged += CboCa_EditValueChanged;
            }
            catch { }

        }

        private void cboID_NHOM_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvCongNhan.SetFocusedRowCellValue("ID_NHOM", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboID_NHOM_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                DataTable dID_NHOM = new DataTable();
                dID_NHOM.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetNhomCC", cboNgay.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                lookUp.Properties.DataSource = dID_NHOM;
            }
            catch { }
        }

        private void LoadGridCongNhan()
        {

            try
            {
                Commons.Modules.sLoad = "0Load";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCN_DangKyLamThem", Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd"), cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, isAdd));
                dt.Columns["CHON"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdCongNhan, grvCongNhan, dt, true, true, true, true, true, this.Name);
                grvCongNhan.Columns["ID_CN"].Visible = false;
                //grvCongNhan.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                //grvCongNhan.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                grvCongNhan.Columns["TEN_NHOM"].OptionsColumn.AllowEdit = false;
                grvCongNhan.Columns["CA"].OptionsColumn.AllowEdit = false;
                grvCongNhan.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                grvCongNhan.Columns["CHON"].Visible = false;
                if (isAdd)
                {
                    dt.Columns["DA_CDL"].ReadOnly = false;
                    grvCongNhan.Columns["DA_CDL"].Visible = false;
                    grvCongNhan.OptionsSelection.MultiSelect = true;
                    grvCongNhan.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                }
                else
                {
                    grvCongNhan.OptionsSelection.MultiSelect = false;
                    grvCongNhan.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                }

                try
                {
                    grvCongNhan.OptionsSelection.CheckBoxSelectorField = "CHON";
                    grvCongNhan.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
                catch { }
                Commons.Modules.sLoad = "";
            }
            catch (Exception ex)
            {
            }
        }

        private void FormatGridCongNhan()
        {
            grvCongNhan.Columns["ID_CN"].Visible = false;
        }

        #region Combobox Changed
        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadGridCongNhan();
            LoadGrdDSLamThem();
            Commons.Modules.sLoad = "";
            grvCongNhan_FocusedRowChanged(null, null);
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadGridCongNhan();
            LoadGrdDSLamThem();
            Commons.Modules.sLoad = "";
            grvCongNhan_FocusedRowChanged(null, null);
            //if (grvCongNhan.RowCount == 0)
            //    grdLamThem.DataSource = null;
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGridCongNhan();
            LoadGrdDSLamThem();

            Commons.Modules.sLoad = "";
            grvCongNhan_FocusedRowChanged(null, null);
            //if (grvCongNhan.RowCount == 0)
            //    grdLamThem.DataSource = null;
        }

        private void cboNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGridCongNhan();
            LoadGrdDSLamThem();
            Commons.Modules.sLoad = "";
            grvCongNhan_FocusedRowChanged(null, null);
        }
        #endregion

        private void FormatGrvLamThem()
        {
            try
            {
                grvLamThem.Columns["ID_CN"].Visible = false;
                grvLamThem.Columns["NGAY"].Visible = false;
                grvLamThem.Columns["GIO_BD"].ColumnEdit = this.repositoryItemTimeEdit1;
                grvLamThem.Columns["GIO_KT"].ColumnEdit = this.repositoryItemTimeEdit1;
                grvLamThem.Columns["PHUT_BD"].Visible = false;
                grvLamThem.Columns["PHUT_KT"].Visible = false;
            }
            catch
            {


            }
            //grvLamThem.Columns["ID_NHOM"].Visible = false;

        }

        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {


                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "export":
                        {
                            try
                            {
                                string sPath = "";
                                sPath = SaveFiles("Excel file (*.xlsx)|*.xlsx");
                                if (sPath == "") return;
                                this.Cursor = Cursors.WaitCursor;
                                Microsoft.Office.Interop.Excel.Application excelApplication = new Microsoft.Office.Interop.Excel.Application();
                                excelApplication.DisplayAlerts = true;

                                excelApplication.Visible = false;


                                System.Globalization.CultureInfo oldCultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                                Microsoft.Office.Interop.Excel.Workbooks excelWorkbooks = excelApplication.Workbooks;
                                object misValue = System.Reflection.Missing.Value;
                                Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApplication.Workbooks.Add(misValue);

                                excelWorkbook.SaveAs(sPath);

                                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];

                                DataTable dt = new DataTable();
                                dt = ((DataTable)grdCongNhan.DataSource).Copy();
                                try
                                {
                                    dt = dt.AsEnumerable().Where(x => (string.IsNullOrEmpty(Convert.ToString(x["GIO_BD"])) ? "" : Convert.ToString(x["GIO_BD"])) == "").CopyToDataTable();
                                }
                                catch (Exception ex) { dt = dt.Clone(); }
                                dt.DefaultView.RowFilter = "";
                                DataView dv = dt.DefaultView;

                                DataTable dt1 = new DataTable();
                                dt1 = dv.ToTable(false, "MS_CN", "HO_TEN", "TEN_NHOM", "CA", "GIO_BD", "GIO_KT", "PHUT_AN_CA");
                                dt1.Columns["MS_CN"].ColumnName = "MSCN";
                                dt1.Columns["HO_TEN"].ColumnName = "Họ và tên";
                                dt1.Columns["TEN_NHOM"].ColumnName = "Tên nhóm";
                                dt1.Columns["CA"].ColumnName = "Ca";
                                dt1.Columns["GIO_BD"].ColumnName = "Giờ bắt đầu";
                                dt1.Columns["GIO_KT"].ColumnName = "Giờ kết thúc";
                                dt1.Columns["PHUT_AN_CA"].ColumnName = "Phút ăn ca";
                                Microsoft.Office.Interop.Excel.Range Ranges1 = excelWorkSheet.Range[excelWorkSheet.Cells[1, 1], excelWorkSheet.Cells[dt1.Rows.Count + 1, dt1.Columns.Count]];
                                Ranges1.Range["A1:G1"].Font.Bold = true;
                                Ranges1.Range["A1:G1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                                Ranges1.Range["A1:G1"].Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                                Ranges1.ColumnWidth = 20;
                                Ranges1.Range["B1"].ColumnWidth = 30;
                                MExportExcel(dt1, excelWorkSheet, Ranges1);

                                this.Cursor = Cursors.Default;
                                excelApplication.Visible = true;
                                excelWorkbook.Save();
                            }
                            catch (Exception ex)
                            {
                                this.Cursor = Cursors.Default;
                                XtraMessageBox.Show(ex.Message);
                            }
                            break;
                        }
                    case "import":
                        {
                            DataTable dt_old = new DataTable();
                            dt_old = (DataTable)grdCongNhan.DataSource;
                            string sBT_Old = "sBTCongNhanOld" + Commons.Modules.iIDUser;
                            string sBT_import = "sBTCongNhanImport" + Commons.Modules.iIDUser;
                            string SBT_grvLamThem = "SBT_grvLamThem" + Commons.Modules.iIDUser;
                            string sPath = "";
                            sPath = Commons.Modules.ObjSystems.OpenFiles("All Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|" + "All Files (*.*)|*.*");

                            DataTable dt = new DataTable();
                            if (sPath == "") return;
                            try
                            {
                                //Lấy đường dẫn
                                var source = new ExcelDataSource();
                                source.FileName = sPath;

                                //Lấy worksheet
                                Workbook workbook = new Workbook();
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
                                dt = new DataTable();
                                dt = ToDataTable(source);
                                if (dt == null) return;
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT_Old, (DataTable)grdCongNhan.DataSource, "");
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT_import, dt, "");
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, SBT_grvLamThem, (DataTable)grdLamThem.DataSource, "");

                                DateTime dNgay;
                                dNgay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);

                                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();

                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spImportDKLT", conn);

                                cmd.Parameters.Add("@sBT_Old", SqlDbType.NVarChar, 50).Value = sBT_Old;
                                cmd.Parameters.Add("@sBT_Import", SqlDbType.NVarChar, 50).Value = sBT_import;
                                cmd.Parameters.Add("@sBT_grvLamThem", SqlDbType.NVarChar, 50).Value = SBT_grvLamThem;
                                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = dNgay;
                                cmd.CommandType = CommandType.StoredProcedure;
                                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                                DataSet ds = new DataSet();
                                adp.Fill(ds);
                                DataTable dt_temp = new DataTable();
                                dt_temp = ds.Tables[0].Copy();

                                DataTable dt_temp2 = new DataTable();
                                dt_temp2 = ds.Tables[1].Copy();
                                //dt_temp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spImportDKLT", sBT_Old, sBT_import, SBT_grvLamThem));
                                grdCongNhan.DataSource = dt_temp;
                                //DataTable dtTemp2 = new DataTable();
                                //dtTemp2 = dt_temp.Copy();
                                grdLamThem.DataSource = dt_temp2;


                                grvCongNhan_FocusedRowChanged(null, null);

                                //ColName = cboCotLayDL.EditValue.ToString();
                                //dtemp.Columns.Add("XOA", System.Type.GetType("System.Boolean"));
                                ////grdChung.DataSource = dtemp;

                                ////Commons.Mod.OS.MLoadXtraGrid(grdChung, grvChung, dtemp, true, true, false, true);
                                //this.DialogResult = DialogResult.OK;
                                //this.Close();
                            }
                            catch (Exception ex)
                            { XtraMessageBox.Show(ex.Message); }
                            break;
                        }
                    case "themsua":
                        {
                            isAdd = true;
                            EnableButon();
                            LoadGridCongNhan();
                            LoadGrdDSLamThem();
                            Commons.Modules.ObjSystems.AddnewRow(grvLamThem, true);
                            grvCongNhan_FocusedRowChanged(null, null);
                            break;
                        }
                    case "xoa":
                        {
                            XoaDangKiGioLamThem();
                            LoadGridCongNhan();
                            LoadGrdDSLamThem();
                            grvCongNhan_FocusedRowChanged(null, null);
                            break;
                        }
                    case "ghi":
                        {

                            if (!Validate()) return;
                            if (grvCongNhan.HasColumnErrors) return;
                            DataTable dt = new DataTable();
                            dt = (DataTable)grdLamThem.DataSource;
                            if (!KiemTraLuoi(dt)) return;
                            if (Savedata() == false)
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                            }
                            Commons.Modules.ObjSystems.DeleteAddRow(grvLamThem);
                            isAdd = false;
                            EnableButon();
                            LoadGridCongNhan();
                            LoadGrdDSLamThem();
                            Commons.Modules.ObjSystems.DeleteAddRow(grvLamThem);
                            grvCongNhan_FocusedRowChanged(null, null);
                            break;
                        }
                    case "khongghi":
                        {
                            isAdd = false;
                            EnableButon();
                            LoadGridCongNhan();
                            LoadGrdDSLamThem();
                            grvCongNhan_FocusedRowChanged(null, null);
                            Commons.Modules.ObjSystems.DeleteAddRow(grvLamThem);
                            break;
                        }
                    case "in":
                        {
                            InBaoCao();
                            break;
                        }
                    case "thoat":
                        {
                            Commons.Modules.ObjSystems.GotoHome(this);
                            break;
                        }
                    case "capnhatnhom":
                        {
                            frmCapNhatNhom frm = new frmCapNhatNhom(Convert.ToDateTime(cboNgay.Text));
                            frm.StartPosition = FormStartPosition.CenterParent;
                            frm.MinimizeBox = false;
                            double iW, iH;
                            iW = 450;
                            iH = 310;
                            frm.Size = new Size((int)iW, (int)iH);

                            if (frm.ShowDialog() == DialogResult.OK)
                            {

                                string sBTCapNhatNhom = "sBTCapNhatNhom" + Commons.Modules.iIDUser;
                                string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
                                string sBTLamThem = "sBTLamThem" + Commons.Modules.iIDUser;
                                try
                                {
                                    DataTable dt = new DataTable();
                                    dt = frm.dtCapNhat.Copy();
                                    try
                                    {
                                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCapNhatNhom, dt, "");
                                    }
                                    catch { }
                                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, Commons.Modules.ObjSystems.ConvertDatatable(grdCongNhan), "");
                                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTLamThem, Commons.Modules.ObjSystems.ConvertDatatable(grdLamThem), "");

                                    System.Data.SqlClient.SqlConnection conn;
                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                    conn.Open();
                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spCapNhatNhomDKLT", conn);
                                    cmd.Parameters.Add("@sBTCapNhatNhom", SqlDbType.NVarChar, 50).Value = sBTCapNhatNhom;
                                    cmd.Parameters.Add("@sBTCongNhan", SqlDbType.NVarChar, 50).Value = sBTCongNhan;
                                    cmd.Parameters.Add("@sBTLamThem", SqlDbType.NVarChar, 50).Value = sBTLamThem;
                                    cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboNgay.Text);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                    DataSet ds = new DataSet();
                                    adp.Fill(ds);
                                    dt = new DataTable();
                                    dt = ds.Tables[0].Copy();
                                    if (Convert.ToInt32(dt.Rows[0][0]) > 1)
                                    {
                                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanDaChon2CaKhacDeThucHien"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }

                                    dt = new DataTable();
                                    dt = ds.Tables[1].Copy();
                                    //dt = new DataTable();
                                    //dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spCapNhatNhomDKLT", sBTCapNhatNhom, sBTCongNhan, sBTLamThem, Commons.Modules.ObjSystems.ConvertDateTime(cboNgay.Text)));
                                    grdLamThem.DataSource = dt;
                                    dt = ds.Tables[2].Copy();
                                    grdCongNhan.DataSource = dt;
                                    grvCongNhan_FocusedRowChanged(null, null);
                                    Commons.Modules.ObjSystems.XoaTable(sBTCapNhatNhom);
                                    Commons.Modules.ObjSystems.XoaTable(sBTCongNhan);
                                    Commons.Modules.ObjSystems.XoaTable(sBTLamThem);
                                }
                                catch
                                {
                                    Commons.Modules.ObjSystems.XoaTable(sBTCapNhatNhom);
                                    Commons.Modules.ObjSystems.XoaTable(sBTCongNhan);
                                    Commons.Modules.ObjSystems.XoaTable(sBTLamThem);
                                }
                            }
                            break;
                        }
                    case "xoadangky":
                        {
                            if (!Validate()) return;
                            DataTable dt_CHON = new DataTable();
                            dt_CHON = ((DataTable)grdCongNhan.DataSource);
                            try
                            {
                                if (dt_CHON.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).Count() == 0)
                                {
                                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            catch { }

                            if (grvCongNhan.HasColumnErrors) return;
                            if (grvLamThem.RowCount <= 1) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
                            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;


                            DataTable dt = new DataTable();
                            dt = Commons.Modules.ObjSystems.ConvertDatatable(grdCongNhan);
                            DataTable dangKiLamThemGio = new DataTable();
                            string stbCN_temP = "grvCongNhanLamThemGio" + Commons.Modules.UserName;
                            string stbLamThemGio = "grvLamThemGio" + Commons.Modules.UserName;

                            try
                            {
                                grvLamThem.PostEditor();
                                grvLamThem.UpdateCurrentRow();
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbLamThemGio, (DataTable)grdLamThem.DataSource, "");
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbCN_temP, dt, "");
                                DateTime dNgay;
                                dNgay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveDangKyLamThemGio", dNgay, stbLamThemGio, stbCN_temP, true);
                                Commons.Modules.ObjSystems.XoaTable(stbLamThemGio);
                                Commons.Modules.ObjSystems.XoaTable(stbCN_temP);

                                Commons.Modules.ObjSystems.DeleteAddRow(grvLamThem);
                                isAdd = false;
                                EnableButon();
                                LoadGridCongNhan();
                                LoadGrdDSLamThem();
                                grvCongNhan_FocusedRowChanged(null, null);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message.ToString());
                                Commons.Modules.ObjSystems.XoaTable(stbLamThemGio);
                            }
                            break;
                        }
                    case "chontatca":
                        {
                            ChonTatCa();
                            break;
                        }
                    case "bochontatca":
                        {
                            BoChonTatCa();
                            break;
                        }
                }
            }
            catch(Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void InBaoCao()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "rptBCDKTangCa", Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd"),
                                            cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));

            frmViewReport frm = new frmViewReport();
            //Convert.ToInt64(grvCongNhan.GetFocusedRowCellValue("ID_CN"))
            string tieuDe = "DANH SÁCH NHÂN VIÊN ĐĂNG KÍ TĂNG CA";
            frm.rpt = new rptDKTangCa(Convert.ToDateTime(cboNgay.EditValue), tieuDe);
            if (dt == null || dt.Rows.Count == 0) return;
            dt.TableName = "DATA";
            frm.AddDataSource(dt);
            frm.ShowDialog();
        }
        private void ChonTatCa()
        {
            int i;
            for (i = 0; i < grvCongNhan.RowCount; i++)
            {
                grvCongNhan.SetRowCellValue(i, "CHON", true);
                grvCongNhan.UpdateCurrentRow();
            }
        }

        private void BoChonTatCa()
        {
            int i;
            for (i = 0; i < grvCongNhan.RowCount; i++)
            {
                grvCongNhan.SetRowCellValue(i, "CHON", false);
                grvCongNhan.UpdateCurrentRow();
            }
        }

        private void CheckAllButton(bool val)
        {
            if (val)
            {
                grvCongNhan.BeginSelection();
                grvCongNhan.ClearSelection();
                grvCongNhan.SelectRange(grvCongNhan.FocusedRowHandle, grvCongNhan.FocusedRowHandle + 1);
                grvCongNhan.EndSelection();
            }
            else
            {

            }
        }

        #region Xu ly button

        /// <summary>
        /// btn cap nhat nhom
        /// </summary>
        /// <returns></returns>
        private void CapNhatNhom()
        {
            try
            {
                grvLamThem.CloseEditor();
                grvLamThem.UpdateCurrentRow();
                //lấy lướng công nhân được chọn
                DataTable dt = Commons.Modules.ObjSystems.ConvertDatatable(grdCongNhan);
                //dt = dt.AsEnumerable().Where(x => x["CHON"].ToString().ToLower() == "true").CopyToDataTable();
                //lấy lưới làm thêm đã có dữ liệu 
                DataTable dtLT = Commons.Modules.ObjSystems.ConvertDatatable(grdLamThem);
                DataTable dt_capnhat = new DataTable();
                //dt_capnhat = ((DataTable)grdLamThem.DataSource).DefaultView.ToTable().Copy();
                DataRow dr = grvLamThem.GetDataRow(grvLamThem.FocusedRowHandle);
                dt_capnhat = ((DataTable)grdLamThem.DataSource).Clone();
                DataRow row = dt_capnhat.NewRow();
                row["ID_CN"] = dr["ID_CN"];
                row["NGAY"] = dr["NGAY"];
                row["ID_NHOM"] = dr["ID_NHOM"];
                row["ID_CDLV"] = dr["ID_CDLV"];
                //row["CA"] = dr["CA"];
                row["COM_CA"] = string.IsNullOrEmpty(dr["COM_CA"].ToString()) ? 0 : dr["COM_CA"];
                row["GIO_BD"] = dr["GIO_BD"];
                row["GIO_KT"] = dr["GIO_KT"];
                row["PHUT_BD"] = dr["PHUT_BD"];
                row["PHUT_KT"] = dr["PHUT_KT"];
                row["PHUT_AN_CA"] = dr["PHUT_AN_CA"];
                row["SO_GIO_TC"] = dr["SO_GIO_TC"];


                dt_capnhat.Rows.Add(row);


                string stbCN_temP = "grvCongNhanLamThemGio" + Commons.Modules.UserName;
                string stbLamThemGio_temP = "grvLamThemGio" + Commons.Modules.UserName;
                string stbLamThemCu_temP = "grvLamThemCu" + Commons.Modules.UserName;

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbCN_temP, dt, "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbLamThemGio_temP, dt_capnhat, "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbLamThemCu_temP, dtLT, "");

                DateTime dNgay;
                dNgay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                grdLamThem.DataSource = ((DataTable)grdLamThem.DataSource).Clone();
                try
                {
                    DataTable dt_temp = new DataTable();
                    dt_temp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spCapNhatLamThem", dNgay, stbCN_temP, stbLamThemGio_temP, stbLamThemCu_temP));
                    dt_temp.Columns["ID_CDLV"].ReadOnly = false;
                    dt_temp.Columns["COM_CA"].ReadOnly = false;
                    grdLamThem.DataSource = dt_temp;
                    grvCongNhan_FocusedRowChanged(null, null);
                }
                catch (Exception ex)
                {
                    Commons.Modules.ObjSystems.XoaTable(stbCN_temP);
                    Commons.Modules.ObjSystems.XoaTable(stbLamThemGio_temP);
                    Commons.Modules.ObjSystems.XoaTable(stbLamThemCu_temP);
                }

            }
            catch (Exception ex)
            {
            }
            //
        }

        private void XoaTrangNhom()
        {
            //int idNhom;
            //int temp;
            //Int32.TryParse(grvLamThem.GetFocusedRowCellValue("ID_NHOM").ToString(), out idNhom);
            //if (idNhom == 0) return;
            //for (int i = 0; i < grvLamThem.DataRowCount; i++)
            //{
            //    DataRow row = grvLamThem.GetDataRow(i);
            //    Int32.TryParse(row["ID_NHOM"].ToString(), out temp);

            //    if (temp == idNhom)
            //    {
            //        grvLamThem.SetRowCellValue(i, "ID_NHOM", -1);
            //        grvLamThem.SetRowCellValue(i, "CA", "");
            //    }
            //}
            //string sTB = "CDCCNV_XoaNhom" + Commons.Modules.UserName;
            //Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grdLamThem), "");

            //for (int i = 0; i < grvLamThem.RowCount - 1; i++)
            //{
            //    grvLamThem.DeleteRow(i);
            //}

            DataTable dt = Commons.Modules.ObjSystems.ConvertDatatable(grdLamThem);
            dt.AcceptChanges();
            dt = dt.AsEnumerable().Where(x => x["CA"].ToString() != "" + grvCongNhan.GetFocusedRowCellValue("ID_CDLV") + "").CopyToDataTable();
            grdLamThem.DataSource = dt;
            grvCongNhan_FocusedRowChanged(null, null);

            //grvLamThem.RefreshData();

            //dr =  dt.Select("CA" +"!='"+ grvCongNhan.GetFocusedRowCellValue("ID_CDLV").ToString() + "'");
            //dt.Select(string.Format("CA = "+Convert.ToInt32(grvCongNhan.GetFocusedRowCellValue("ID_CDLV"))+"")).ToList<DataRow>().ForEach(r => r["CA"] = Convert.ToInt32(grvCongNhan.GetFocusedRowCellValue("ID_CDLV")));
            //dt.Clear();
            //grdLamThem.DataSource = dt;


            //grdLamThem.DataSource=null;

        }

        private void XoaDangKiGioLamThem()
        {
            if (grvLamThem.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }


            DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgDeleteDangKyLamThem"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgfrmThongBao"), MessageBoxButtons.YesNoCancel);
            if (res == DialogResult.Yes)
            {
                string sBT = "sBTDKLT" + Commons.Modules.iIDUser;
                try
                {
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTDKLT" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grdCongNhan), "");
                    string sSql = "DELETE dbo.DANG_KY_LAM_GIO_LAM_THEM FROM dbo.DANG_KY_LAM_GIO_LAM_THEM T1 INNER JOIN " + sBT + " T2 ON T1.ID_CN = T2.ID_CN WHERE CONVERT(NVARCHAR(10),NGAY, 112) = '"
                                                            + Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd") + "'";

                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                    Commons.Modules.ObjSystems.XoaTable(sBT);
                    DataTable dt = ((DataTable)grdCongNhan.DataSource);
                    dt = dt.Clone();
                }
                catch
                {
                    Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
                }
            }
            else if (res == DialogResult.No)
            {
                try
                {
                    string sSql = "DELETE dbo.DANG_KY_LAM_GIO_LAM_THEM WHERE ID_CN = " + grvCongNhan.GetFocusedRowCellValue("ID_CN") +
                                                            " AND CONVERT(NVARCHAR(10),NGAY, 112) = '"
                                                            + Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd") + "'";

                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                    grvLamThem.DeleteSelectedRows();
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

        }

        private bool Savedata()
        {
            DataTable dangKiLamThemGio = new DataTable();
            DataTable dt = new DataTable();
            dt = Commons.Modules.ObjSystems.ConvertDatatable(grdCongNhan);
            string stbLamThemGio = "grvLamThemGio" + Commons.Modules.UserName;
            string sSql = "";
            try
            {
                grvLamThem.PostEditor();
                grvLamThem.UpdateCurrentRow();
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbLamThemGio, (DataTable)grdLamThem.DataSource, "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "stbCN_temP" + Commons.Modules.UserName, dt, "");

                DateTime dNgay;
                dNgay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveDangKyLamThemGio", dNgay, stbLamThemGio, "stbCN_temP" + Commons.Modules.UserName);
                Commons.Modules.ObjSystems.XoaTable(stbLamThemGio);
                Commons.Modules.ObjSystems.XoaTable("stbCN_temP" + Commons.Modules.UserName);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                Commons.Modules.ObjSystems.XoaTable(stbLamThemGio);
                return false;
            }
        }

        #endregion Xu ly button

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visible"></param>
        private void EnableButon()
        {

            btnALL.Buttons[0].Properties.Visible = !isAdd;
            btnALL.Buttons[1].Properties.Visible = !isAdd;
            btnALL.Buttons[2].Properties.Visible = !isAdd;
            btnALL.Buttons[3].Properties.Visible = !isAdd;
            btnALL.Buttons[4].Properties.Visible = isAdd;
            btnALL.Buttons[5].Properties.Visible = isAdd;
            btnALL.Buttons[6].Properties.Visible = isAdd;
            btnALL.Buttons[7].Properties.Visible = isAdd;
            btnALL.Buttons[8].Properties.Visible = !isAdd;

            cboNgay.Enabled = !isAdd;
            cboDonVi.Enabled = !isAdd;
            cboXiNghiep.Enabled = !isAdd;
            cboTo.Enabled = !isAdd;
        }

        private void grvData_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void grvData_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        #region Xu Ly Ngay
        /// <summary>
        /// Load Ngay
        /// </summary>
        private void LoadNgay()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListNgayDKLamThem", Commons.Modules.UserName, Commons.Modules.TypeLanguage));

            if (grdNgay.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay, dt, false, true, true, true, true, this.Name);
            }
            else
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay, dt, false, false, true, false, false, this.Name);

            if (dt.Rows.Count > 0)
            {
                cboNgay.EditValue = dt.Rows[0]["NGAY"];
            }
            else
            {
                cboNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        /// <summary>
        /// calNgay commit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calNgay_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboNgay.Text = calNgay.DateTime.Date.ToShortDateString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                cboNgay.Text = DateTime.Now.ToShortDateString();
            }
            cboNgay.ClosePopup();
        }

        /// <summary>
        /// load null cboNgay
        /// </summary>
        private void LoadNull()
        {
            try
            {
                if (cboNgay.Text == "") cboNgay.Text = DateTime.Now.ToShortDateString();
            }
            catch (Exception ex)
            {
                cboNgay.Text = "";
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        /// <summary>
        /// grid view combo ngay change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grvNgay_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboNgay.Text = Convert.ToDateTime(grv.GetFocusedRowCellValue("NGAY").ToString()).ToShortDateString();
            }
            catch { LoadNull(); }
            cboNgay.ClosePopup();
        }
        #endregion



        /// <summary>
        /// count Nhan vien
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grvCongNhan_RowCountChanged(object sender, EventArgs e)
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
            grvCongNhan_FocusedRowChanged(null, null);
        }

        private void grvCongNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            DataTable dtTmp = new DataTable();
            String sIDCN;
            try
            {
                dtTmp = (DataTable)grdLamThem.DataSource;

                string sDK = "";
                sIDCN = "-1";
                try { sIDCN = grvCongNhan.GetFocusedRowCellValue("ID_CN").ToString(); } catch { }
                if (sIDCN != "-1")
                {
                    sDK = " ID_CN = '" + sIDCN + "' ";
                }
                else
                {
                    sDK = "1 = 0";
                }

                dtTmp.DefaultView.RowFilter = sDK;
            }
            catch { }

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
        private void grvLamThem_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                grvLamThem.SetFocusedRowCellValue("COM_CA", 0);
                grvLamThem.SetFocusedRowCellValue("ID_NHOM", grvCongNhan.GetFocusedRowCellValue("ID_NHOM"));
                grvCongNhan.SetFocusedRowCellValue("DA_CDL", 1);
            }
            catch { }
        }

        private void grvLamThem_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DateTime gioBD = new DateTime();
            DateTime gioKT = new DateTime();
            double phutBD = 0;
            double phutKT = 0;
            double phutAnCom = 0;
            double gioTangCa = 0;
            GridView view = sender as GridView;
            try
            {
                if (e.Column.FieldName == "ID_CDLV")
                {
                    gioBD = DateTime.Parse(view.GetFocusedRowCellValue("GIO_BD").ToString());
                    gioKT = DateTime.Parse(view.GetFocusedRowCellValue("GIO_KT").ToString());
                    phutAnCom = Convert.ToDouble(view.GetFocusedRowCellValue("PHUT_AN_CA").ToString() == "" ? 0 : Convert.ToDecimal(view.GetFocusedRowCellValue("PHUT_AN_CA")));
                    view.SetRowCellValue(e.RowHandle, view.Columns["SO_GIO_TC"], ((gioKT.Hour * 60 + gioKT.Minute) - (gioBD.Hour * 60 + gioBD.Minute) - phutAnCom) / 60);
                }
                if (e.Column.FieldName == "GIO_BD")
                {
                    DataTable dt = new DataTable();
                    if (DateTime.TryParse(view.GetFocusedRowCellValue("GIO_BD").ToString(), out gioBD))
                    {
                        gioBD = DateTime.Parse(view.GetFocusedRowCellValue("GIO_BD").ToString());
                        phutBD = gioBD.Hour * 60 + gioBD.Minute;
                        view.SetFocusedRowCellValue("PHUT_BD", phutBD);
                        view.SetRowCellValue(e.RowHandle, view.Columns["ID_CN"], grvCongNhan.GetFocusedRowCellValue("ID_CN").ToString());
                        view.SetRowCellValue(e.RowHandle, view.Columns["NGAY"], Convert.ToDateTime(cboNgay.EditValue).ToString());

                        try
                        {
                            gioKT = DateTime.Parse(view.GetFocusedRowCellValue("GIO_KT").ToString());
                            phutAnCom = view.GetFocusedRowCellValue("PHUT_AN_CA") == DBNull.Value ? 0 : Convert.ToDouble(view.GetFocusedRowCellValue("PHUT_AN_CA"));
                            gioTangCa = ((gioKT.Hour * 60 + gioKT.Minute) - (gioBD.Hour * 60 + gioBD.Minute) - phutAnCom) / 60;
                            view.SetRowCellValue(e.RowHandle, view.Columns["SO_GIO_TC"], gioTangCa);
                        }
                        catch { }
                    }

                }
                if (e.Column.FieldName == "PHUT_AN_CA")
                {
                    try
                    {
                        gioBD = DateTime.Parse(view.GetFocusedRowCellValue("GIO_BD").ToString());
                        gioKT = DateTime.Parse(view.GetFocusedRowCellValue("GIO_KT").ToString());
                        phutAnCom = view.GetFocusedRowCellValue("PHUT_AN_CA") == DBNull.Value ? 0 : Convert.ToDouble(view.GetFocusedRowCellValue("PHUT_AN_CA"));

                        gioTangCa = ((gioKT.Hour * 60 + gioKT.Minute) - (gioBD.Hour * 60 + gioBD.Minute) - phutAnCom) / 60;
                        view.SetRowCellValue(e.RowHandle, view.Columns["SO_GIO_TC"], gioTangCa);
                    }
                    catch { }
                }
                if (e.Column.FieldName == "GIO_KT")
                {
                    if (DateTime.TryParse(view.GetFocusedRowCellValue("GIO_KT").ToString(), out gioKT))
                    {
                        gioKT = DateTime.Parse(view.GetFocusedRowCellValue("GIO_KT").ToString());
                        phutKT = gioKT.Hour * 60 + gioKT.Minute;
                        view.SetFocusedRowCellValue("PHUT_KT", phutKT);

                        try
                        {
                            gioBD = DateTime.Parse(view.GetFocusedRowCellValue("GIO_BD").ToString());
                            phutAnCom = view.GetFocusedRowCellValue("PHUT_AN_CA") == DBNull.Value ? 0 : Convert.ToDouble(view.GetFocusedRowCellValue("PHUT_AN_CA"));
                            gioTangCa = ((gioKT.Hour * 60 + gioKT.Minute) - (gioBD.Hour * 60 + gioBD.Minute) - phutAnCom) / 60;
                            view.SetRowCellValue(e.RowHandle, view.Columns["SO_GIO_TC"], gioTangCa);
                        }
                        catch { }

                    }
                }



            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
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
                string sTenCot = "";
                switch (i)
                {
                    case 0:
                        {
                            sTenCot = "MS_CN";
                            break;
                        }
                    case 1:
                        {
                            sTenCot = "HO_TEN";
                            break;
                        }
                    case 2:
                        {
                            sTenCot = "TEN_NHOM";
                            break;
                        }
                    case 3:
                        {
                            sTenCot = "CA";
                            break;
                        }
                    case 4:
                        {
                            sTenCot = "GIO_BD";
                            break;
                        }
                    case 5:
                        {
                            sTenCot = "GIO_KT";
                            break;
                        }
                    case 6:
                        {
                            sTenCot = "PHUT_AN_CA";
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (DevExpress.DataAccess.Native.Excel.ViewRow item in list)
            {

                for (int i = 0; i < values.Length; i++)
                {
                    if (i == 4 || i == 5)
                    {
                        try
                        {
                            if ((props[i].GetValue(item) == null ? DateTime.MaxValue : Convert.ToDateTime(props[i].GetValue(item))) == DateTime.MinValue)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCot") + " " + props[i].Name + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCuaNhanVien") + " " + values[0] + "-" + values[1] + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongChinhXac"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return null;
                            }
                            values[i] = cboNgay.Text + " " + Convert.ToDateTime(props[i].GetValue(item)).TimeOfDay;
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCot") + " " + props[i].Name + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCuaNhanVien") + " " + values[0] + "-" + values[1] + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongChinhXac"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return null;
                        }
                    }
                    else if (i == 6)
                    {
                        try
                        {
                            values[i] = props[i].GetValue(item) == null ? 0 : Convert.ToInt32(props[i].GetValue(item));
                        }
                        catch
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCot") + " " + props[i].Name + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCuaNhanVien") + " " + values[0] + "-" + values[1] + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongChinhXac"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return null;
                        }
                    }
                    else
                    {
                        values[i] = props[i].GetValue(item);
                    }
                }
                table.Rows.Add(values);
            }
            return table;
        }
        private void grvLamThem_InvalidValueException(object sender, InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvLamThem_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvLamThem_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {

        }

        private void grvLamThem_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;

                DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                DevExpress.XtraGrid.Columns.GridColumn ID_CDLV = View.Columns["ID_CDLV"];
                DevExpress.XtraGrid.Columns.GridColumn gioBD = View.Columns["GIO_BD"];
                DevExpress.XtraGrid.Columns.GridColumn gioKT = View.Columns["GIO_KT"];
                try
                {
                    if (View.FocusedColumn.Name.ToString() == "colGIO_BD")
                    {
                        if (Convert.ToDateTime(grvLamThem.GetFocusedRowCellValue("GIO_BD")) > Convert.ToDateTime(grvLamThem.GetFocusedRowCellValue("GIO_KT")))
                        {
                            grvLamThem.SetColumnError(grvLamThem.Columns["GIO_BD"], "Giờ bắt đầu phải nhỏ hơn giờ kết thúc");
                            e.Valid = false;
                            View.SetColumnError(gioBD, "Giờ bắt đầu phải nhỏ hơn ngày kết thúc"); return;
                        }

                        if (Convert.ToDateTime(cboNgay.Text).DayOfWeek.ToString() != "Sunday" && Convert.ToDateTime(cboNgay.Text).DayOfWeek.ToString() != "Saturday")
                        {
                            DataTable dt = new DataTable();
                            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT NGAY FROM dbo.NGAY_NGHI_LE"));
                            try
                            {
                                if (dt.AsEnumerable().Where(x => x.Field<string>("NGAY").Trim().Equals(cboNgay.Text)).CopyToDataTable().Rows.Count > 1)
                                {
                                }
                            }
                            catch
                            {
                                try
                                {
                                    DateTime dGioBD = Convert.ToDateTime(grvLamThem.GetFocusedRowCellValue("GIO_BD"));
                                    DateTime dGioKT = Convert.ToDateTime(grvLamThem.GetFocusedRowCellValue("GIO_KT"));
                                    if (Convert.ToDateTime("01/01/1900 " + dGioBD.TimeOfDay.ToString()) < dGioBatDau || Convert.ToDateTime("01/01/1900 " + dGioBD.TimeOfDay.ToString()) > dGioKetThuc)
                                    {
                                        grvLamThem.SetColumnError(grvLamThem.Columns["GIO_BD"], "Giờ bắt đầu phải thuộc khoảng cho phép");
                                        e.Valid = false;
                                        View.SetColumnError(gioBD, "Giờ bắt đầu phải thuộc khoảng cho phép"); return;
                                    }
                                    if (Convert.ToDateTime("01/01/1900 " + dGioKT.TimeOfDay.ToString()) < dGioBatDau || Convert.ToDateTime("01/01/1900 " + dGioKT.TimeOfDay.ToString()) > dGioKetThuc)
                                    {
                                        grvLamThem.SetColumnError(grvLamThem.Columns["GIO_KT"], "Giờ kết thúc phải nằm trong khoảng cho phép");
                                        e.Valid = false;
                                        View.SetColumnError(gioKT, "Giờ kết thúc phải nằm trong khoảng cho phép"); return;
                                    }
                                }
                                catch { }

                            }

                        }

                    }
                    if (View.FocusedColumn.Name.ToString() == "colGIO_KT")
                    {
                        if (Convert.ToDateTime(grvLamThem.GetFocusedRowCellValue("GIO_KT")) < Convert.ToDateTime(grvLamThem.GetFocusedRowCellValue("GIO_BD")))
                        {
                            grvLamThem.SetColumnError(grvLamThem.Columns["GIO_KT"], "Giờ kết thúc phải lớn hơn giờ bắt đầu");
                            e.Valid = false;
                            View.SetColumnError(gioKT, "Giờ kết thúc phải lớn hơn giờ bắt đầu"); return;
                        }

                        if (Convert.ToDateTime(cboNgay.Text).DayOfWeek.ToString() != "Sunday" && Convert.ToDateTime(cboNgay.Text).DayOfWeek.ToString() != "Saturday")
                        {
                            DataTable dt = new DataTable();
                            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT NGAY FROM dbo.NGAY_NGHI_LE"));
                            try
                            {
                                if (dt.AsEnumerable().Where(x => x.Field<string>("NGAY").Trim().Equals(cboNgay.Text)).CopyToDataTable().Rows.Count > 1)
                                {
                                }
                            }
                            catch
                            {
                                try
                                {
                                    DateTime dGioBD = Convert.ToDateTime(grvLamThem.GetFocusedRowCellValue("GIO_BD"));
                                    DateTime dGioKT = Convert.ToDateTime(grvLamThem.GetFocusedRowCellValue("GIO_KT"));
                                    if (Convert.ToDateTime("01/01/1900 " + dGioBD.TimeOfDay.ToString()) < dGioBatDau || Convert.ToDateTime("01/01/1900 " + dGioBD.TimeOfDay.ToString()) > dGioKetThuc)
                                    {
                                        grvLamThem.SetColumnError(grvLamThem.Columns["GIO_BD"], "Giờ bắt đầu phải thuộc khoảng cho phép");
                                        e.Valid = false;
                                        View.SetColumnError(gioBD, "Giờ bắt đầu phải thuộc khoảng cho phép"); return;
                                    }
                                    if (Convert.ToDateTime("01/01/1900 " + dGioKT.TimeOfDay.ToString()) < dGioBatDau || Convert.ToDateTime("01/01/1900 " + dGioKT.TimeOfDay.ToString()) > dGioKetThuc)
                                    {
                                        grvLamThem.SetColumnError(grvLamThem.Columns["GIO_KT"], "Giờ kết thúc phải nằm trong khoảng cho phép");
                                        e.Valid = false;
                                        View.SetColumnError(gioKT, "Giờ kết thúc phải nằm trong khoảng cho phép"); return;
                                    }
                                }
                                catch { }

                            }

                        }
                    }

                    if (View.FocusedColumn.Name.ToString() == "colID_CDLV")
                    {
                        DataTable dt = new DataTable();
                        dt = Commons.Modules.ObjSystems.ConvertDatatable(grvLamThem);
                        string sCa = grvLamThem.GetFocusedRowCellValue("ID_CDLV").ToString();
                        if (dt.AsEnumerable().Where(x => x.Field<string>("ID_CDLV").Trim().Equals(sCa)).CopyToDataTable().Rows.Count > 1)
                        {
                            //string sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTrungDLLuoi");
                            string sTenKTra = "Trùng dữ liệu";
                            grvLamThem.SetColumnError(grvLamThem.Columns["ID_CDLV"], sTenKTra);
                            e.Valid = false;
                            View.SetColumnError(ID_CDLV, "Trùng dữ liệu"); return;
                            //dr.SetColumnError(sCot, sTenKTra);
                        }
                    }
                }
                catch { }



            }
            catch (Exception ex) { }
        }

        private void grvCongNhan_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(grvCongNhan.GetRowCellValue(e.RowHandle, grvCongNhan.Columns["DA_CDL"])) == false) return;
                e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFCC");
                e.HighPriority = true;
            }
            catch
            {
            }
        }

        private void grdLamThem_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete && btnALL.Buttons[0].Properties.Visible == false)
                {
                    grvLamThem.DeleteSelectedRows();
                    DataTable dt = new DataTable();
                    dt = Commons.Modules.ObjSystems.ConvertDatatable(grvLamThem);
                    if (dt.Rows.Count == 0)
                        grvCongNhan.SetFocusedRowCellValue("DA_CDL", 0);
                }

            }
            catch { }
        }

        private void grvCongNhan_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
            }
            catch { }
        }
        public bool KiemTrungDL(GridView grvData, DataTable dt, DataRow dr, string sCot, string sDLKiem, string tabName, string ColName, string sform)
        {
            string sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDL");
            try
            {
                //&& x["GIO_BD"].Equals(dr["GIO_BD"].ToString()
                if (dt.AsEnumerable().Where(x => x.Field<string>(sCot).Trim().Equals(sDLKiem) && x["GIO_BD"].Equals(Convert.ToDateTime(dr["GIO_BD"])) && x["ID_CN"].Equals(dr["ID_CN"])).CopyToDataTable().Rows.Count > 1)
                {
                    sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDLLuoi");
                    dr.SetColumnError(sCot, sTenKTra);
                    return false;
                }
                else
                {
                    //if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.[" + tabName + "] WHERE ID_CN = " + dr["ID_CN"] + " AND NGAY = '" + Convert.ToDateTime(cboNgay.Text).ToString("MM/dd/yyyy") + "' AND CA = N'"+sDLKiem.Substring(0, sDLKiem.IndexOf(';')) +"' AND GIO_BD = '"+ dr["GIO_BD"] + "'")) > 0)
                    //{
                    //    sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDLCSDL");
                    //    dr.SetColumnError(sCot, sTenKTra);
                    //    return false;
                    //}
                }
                return true;
            }
            catch (Exception ex)
            {
                dr.SetColumnError(sCot, sTenKTra);
                return false;
            }
        }
        private bool KiemTraLuoi(DataTable dtSource)
        {
            int errorCount = 0;
            #region kiểm tra dữ liệu
            this.Cursor = Cursors.WaitCursor;
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                //Số hợp đồng lao động
                string sID_CDLV = dr["ID_CDLV"].ToString();
                if (!KiemTrungDL(grvLamThem, dtSource, dr, "ID_CDLV", sID_CDLV, "DANG_KY_LAM_GIO_LAM_THEM", "ID_CN", this.Name))
                {
                    try
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = (DataTable)grdCongNhan.DataSource;
                        dt1.PrimaryKey = new DataColumn[] { dt1.Columns["ID_CN"] };
                        int index = dt1.Rows.IndexOf(dt1.Rows.Find(dr["ID_CN"]));
                        DataRow dr1 = dt1.Rows[index];
                        dr1.SetColumnError("MS_CN", "Error");
                    }
                    catch (Exception ex) { }
                    
                    errorCount++;
                }
            }
            #endregion
            Commons.Modules.ObjSystems.HideWaitForm();
            if (errorCount != 0)
            {
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                this.Cursor = Cursors.Default;
                return true;
            }
        }
    }
}