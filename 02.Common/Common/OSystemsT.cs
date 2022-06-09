
using DevExpress.Utils;
using DevExpress.Utils.Layout;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraLayout;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Commons
{
    public class OSystems
    {

        private string strSql;
        public DataTable MOpenData()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD,VIETNAM AS NN FROM dbo.LANGUAGES WHERE FORM = 'ucLyLich'"));
            return dt;
        }

        public static void SetDateEditFormat(DateEdit dateEdit)
        {
            dateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateEdit.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            dateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateEdit.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            dateEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            dateEdit.Properties.Mask.EditMask = "dd/MM/yyyy";
        }

        public static void SetTimeEditFormat(TimeEdit timeEdit)
        {
            timeEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            timeEdit.Properties.DisplayFormat.FormatString = "HH:mm:ss";
            timeEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            timeEdit.Properties.EditFormat.FormatString = "HH:mm:ss";
            timeEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            //timeEdit.Properties.Mask.EditMask = "00:00:00";
        }

        public static void SetDateRepositoryItemDateEdit(RepositoryItemDateEdit dateEdit)
        {
            dateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateEdit.DisplayFormat.FormatString = "dd/MM/yyyy";
            dateEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateEdit.EditFormat.FormatString = "dd/MM/yyyy";
            dateEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            dateEdit.Mask.EditMask = "dd/MM/yyyy";
        }

        public int TinhSoNgayTruLeChuNhat(DateTime TNgay, DateTime DNgay)
        {
            int resulst = 0;

            string sSql = "";
            sSql = "SELECT [dbo].[fnGetSoNgayTruLeChuNhat]('" + Convert.ToDateTime(TNgay).ToString("yyyyMMdd") + "','" + Convert.ToDateTime(DNgay).ToString("yyyyMMdd") + "')";
            resulst = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql)); //* Commons.Modules.iGio
            return resulst;
        }

        #region LoadLookupedit
        public bool MLoadLookUpEdit(DevExpress.XtraEditors.LookUpEdit cbo, string sQuery, string Ma, string Ten, string TenCot)
        {
            try
            {
                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, CommandType.Text, sQuery));
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.Columns.Clear();
                cbo.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(Ten));
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool MLoadComboboxEdit(DevExpress.XtraEditors.ComboBoxEdit cbo, DataTable dt, string cot)
        {
            try
            {
                cbo.Properties.Items.Clear();
                foreach (DataRow item in dt.Rows)
                {
                    cbo.Properties.Items.Add(item[cot]);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool MLoadComboboxEdit(DevExpress.XtraEditors.ComboBoxEdit cbo, DataRow[] dr, string cot)
        {
            try
            {
                cbo.Properties.Items.Clear();
                foreach (DataRow item in dr)
                {
                    cbo.Properties.Items.Add(item[cot]);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool MLoadLookUpEdit(DevExpress.XtraEditors.LookUpEdit cbo, DataTable dtTmp, string Ma, string Ten, string TenCot)
        {
            try
            {
                cbo.Properties.DataSource = null;
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.Columns.Clear();
                cbo.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(Ten));
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;
                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool MLoadLookUpEdit(DevExpress.XtraEditors.LookUpEdit cbo, DataTable dtTmp, string Ma, string Ten, string TenCot, bool CoNull)
        {
            try
            {
                if (CoNull)
                    dtTmp.Rows.Add(-99, "");
                cbo.Properties.DataSource = null;
                //cbo.Properties.DisplayMember = "";
                //cbo.Properties.ValueMember = "";
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.Columns.Clear();
                cbo.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(Ten));
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                if (CoNull)
                    cbo.EditValue = dtTmp.Rows[dtTmp.Rows.Count - 1][Ma];
                else
                    cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;
                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }




        public bool MLoadLookUpEdit(DevExpress.XtraEditors.LookUpEdit cbo, string sStored, string Ma, string Ten, string TenCot, bool bStored)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                if (bStored)
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, sStored));
                else
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, CommandType.Text, sStored));
                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.Columns.Clear();
                cbo.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(Ten));
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;
                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool MLoadLookUpEdit(DevExpress.XtraEditors.LookUpEdit cbo, string sStored, string Ma, string Ten, string TenCot, bool bStored, string Param)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                if (bStored)
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, sStored, Param));
                else
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, CommandType.Text, sStored));
                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";

                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.Columns.Clear();
                cbo.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(Ten));
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;
                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool MLoadLookUpEdit(DevExpress.XtraEditors.LookUpEdit cbo, string sStored, string Ma, string Ten, string TenCot, bool bStored, string Param, string Param1)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                if (bStored)
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, sStored, Param, Param1));
                else
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, CommandType.Text, sStored));
                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.Columns.Clear();
                cbo.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(Ten));
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;
                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool MLoadLookUpEditNoRemove(DevExpress.XtraEditors.LookUpEdit cbo, string sQuery, string Ma, string Ten, string TenCot)
        {
            try
            {
                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";

                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, CommandType.Text, sQuery));
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;
                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool MLoadLookUpEditNoRemove(DevExpress.XtraEditors.LookUpEdit cbo, DataTable dtTmp, string Ma, string Ten, string TenCot)
        {
            try
            {
                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                cbo.EditValue = dtTmp.Rows[0][Ma];
                cbo.Properties.Columns.Clear();
                DevExpress.XtraEditors.Controls.LookUpColumnInfo column;
                for (int intColumn = 0; intColumn <= dtTmp.Columns.Count - 1; intColumn++)
                {
                    column = new DevExpress.XtraEditors.Controls.LookUpColumnInfo();
                    //column.Caption = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, sForm, dtTmp.Columns(intColumn).ColumnName, Commons.Modules.TypeLanguage);
                    column.FieldName = dtTmp.Columns[intColumn].ColumnName;
                    cbo.Properties.Columns.Add(column);
                }


                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;


                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);


                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool MLoadLookUpEditNoRemove(DevExpress.XtraEditors.LookUpEdit cbo, string sStored, string Ma, string Ten, string TenCot, bool bStored)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                if (bStored)
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, sStored));
                else
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, CommandType.Text, sStored));
                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;
                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool MLoadLookUpEditNoRemove(DevExpress.XtraEditors.LookUpEdit cbo, string sStored, string Ma, string Ten, string TenCot, bool bStored, string Param)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                if (bStored)
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, sStored, Param));
                else
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, CommandType.Text, sStored));
                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";

                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;
                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool MLoadLookUpEditNoRemove(DevExpress.XtraEditors.LookUpEdit cbo, string sStored, string Ma, string Ten, string TenCot, bool bStored, string Param, string Param1)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                if (bStored)
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, sStored, Param, Param1));
                else
                    dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, CommandType.Text, sStored));
                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;

                cbo.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cbo.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                cbo.Properties.SearchMode = SearchMode.AutoComplete;
                cbo.EditValue = dtTmp.Rows[0][Ma];
                if (dtTmp.Rows.Count > 10)
                    cbo.Properties.DropDownRows = 15;
                else
                    cbo.Properties.DropDownRows = 10;
                cbo.Properties.Columns[Ten].Caption = TenCot;
                if (TenCot.Trim() == "")
                    cbo.Properties.ShowHeader = false;
                else
                    cbo.Properties.ShowHeader = true;
                cbo.Properties.Columns[Ten].Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "LookUpEdit", Ten, Modules.TypeLanguage);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region AutoComplete
        public bool MAutoCompleteTextEdit(DevExpress.XtraEditors.TextEdit txt, string sQuery, string Ma)
        {
            try
            {
                txt.MaskBox.AutoCompleteCustomSource = null;
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, CommandType.Text, sQuery));
                string[] postSource;
                dtTmp = dtTmp.DefaultView.ToTable(true, Ma);
                postSource = dtTmp.Rows.Cast<DataRow>().Select(dr => dr[Ma].ToString()).ToArray();
                var source = new AutoCompleteStringCollection();
                source.AddRange(postSource);
                txt.MaskBox.AutoCompleteCustomSource = source;
                txt.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txt.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool MAutoCompleteTextEdit(DevExpress.XtraEditors.TextEdit txt, DataTable dtData, string Ma)
        {
            try
            {
                txt.MaskBox.AutoCompleteCustomSource = null;
                string[] postSource;
                dtData = dtData.DefaultView.ToTable(true, Ma);
                postSource = dtData.Rows.Cast<DataRow>().Select(dr => dr[Ma].ToString()).ToArray();
                var source = new AutoCompleteStringCollection();
                source.AddRange(postSource);
                txt.MaskBox.AutoCompleteCustomSource = source;
                txt.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txt.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Load xtraserch
        
        public void MLoadSearchLookUpEdit(DevExpress.XtraEditors.SearchLookUpEdit cbo, DataTable dtTmp, string Ma, string Ten, string TenCot, bool isNgonNgu = true, bool CoNull = false)
        {
            try
            {
                if (CoNull)
                    dtTmp.Rows.Add(-99, "");

                cbo.Properties.DataSource = null;
                cbo.Properties.DisplayMember = "";
                cbo.Properties.ValueMember = "";
                cbo.Properties.DataSource = dtTmp;
                cbo.Properties.DisplayMember = Ten;
                cbo.Properties.ValueMember = Ma;
                cbo.Properties.BestFitMode = BestFitMode.BestFit;
                if (CoNull)
                    cbo.EditValue = dtTmp.Rows[dtTmp.Rows.Count - 1][Ma];
                else
                    cbo.EditValue = dtTmp.Rows[0][Ma];

                cbo.Properties.PopulateViewColumns();
                cbo.Properties.View.Columns[0].Visible = false;
                if (isNgonNgu)
                {
                    DevExpress.XtraGrid.Views.Grid.GridView grv = (DevExpress.XtraGrid.Views.Grid.GridView)cbo.Properties.PopupView;
                    foreach (DevExpress.XtraGrid.Columns.GridColumn col in grv.Columns)
                    {
                        if (col.Visible)
                        {

                            col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                            col.AppearanceHeader.Options.UseTextOptions = true;
                            col.Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "SearchLookUpEdit", col.FieldName, Modules.TypeLanguage);
                        }
                    }
                    cbo.Refresh();
                }

            }
            catch { }
        }

        #endregion

        #region Load xtragrid
        public bool MLoadXtraGrid(DevExpress.XtraGrid.GridControl grd, DevExpress.XtraGrid.Views.Grid.GridView grv, string sQuery, bool MEditable, bool MPopulateColumns, bool MColumnAutoWidth, bool MBestFitColumns)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, CommandType.Text, sQuery));

                grd.DataSource = dtTmp;
                grv.OptionsBehavior.Editable = MEditable;
                grv.OptionsView.RowAutoHeight = true;
                if (MPopulateColumns == true)
                    grv.PopulateColumns();
                grv.OptionsView.ColumnAutoWidth = MColumnAutoWidth;
                grv.OptionsView.AllowHtmlDrawHeaders = true;
                grv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                grv.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                grv.OptionsBehavior.FocusLeaveOnTab = true;
                if (MBestFitColumns)
                    grv.BestFitColumns();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool MLoadXtraGrid(DevExpress.XtraGrid.GridControl grd, DevExpress.XtraGrid.Views.Grid.GridView grv, DataTable dtTmp, bool MEditable, bool MPopulateColumns, bool MColumnAutoWidth, bool MBestFitColumns)
        {
            try
            {
                grd.DataSource = dtTmp;
                grv.OptionsBehavior.Editable = MEditable;
                grv.OptionsView.RowAutoHeight = true;

                if (MPopulateColumns == true)
                    grv.PopulateColumns();
                grv.OptionsView.ColumnAutoWidth = MColumnAutoWidth;
                grv.OptionsView.AllowHtmlDrawHeaders = true;
                grv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                grv.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                if (MBestFitColumns)
                    grv.BestFitColumns();
                grv.OptionsBehavior.FocusLeaveOnTab = true;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool MLoadXtraGrid(DevExpress.XtraGrid.GridControl grd, DevExpress.XtraGrid.Views.Grid.GridView grv, string sQuery, bool MEditable, bool MPopulateColumns, bool MColumnAutoWidth, bool MBestFitColumns, bool MloadNNgu, string fName)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(IConnections.CNStr, CommandType.Text, sQuery));

                grd.DataSource = dtTmp;
                grv.OptionsBehavior.Editable = MEditable;
                grv.OptionsView.RowAutoHeight = true;

                if (MPopulateColumns == true)
                    grv.PopulateColumns();
                grv.OptionsView.ColumnAutoWidth = MColumnAutoWidth;

                grv.OptionsView.AllowHtmlDrawHeaders = true;
                grv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                grv.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
                if (MBestFitColumns)
                    grv.BestFitColumns();
                if (MloadNNgu)
                    MLoadNNXtraGrid(grv, fName);
                grv.OptionsBehavior.FocusLeaveOnTab = true;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool MLoadXtraGrid(DevExpress.XtraGrid.GridControl grd, DevExpress.XtraGrid.Views.Grid.GridView grv, DataTable dtTmp, bool MEditable, bool MPopulateColumns, bool MColumnAutoWidth, bool MBestFitColumns, bool MloadNNgu, string fName)
        {
            try
            {
                grd.DataSource = dtTmp;
                grv.OptionsBehavior.Editable = MEditable;
                grv.OptionsView.RowAutoHeight = true;

                if (MPopulateColumns == true)
                    grv.PopulateColumns();
                grv.OptionsView.ColumnAutoWidth = MColumnAutoWidth;
                grv.OptionsView.AllowHtmlDrawHeaders = true;
                grv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                grv.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;

                if (MBestFitColumns)
                    grv.BestFitColumns();

                if (MloadNNgu)
                    MLoadNNXtraGrid(grv, fName);

                grv.OptionsBehavior.FocusLeaveOnTab = true;
                //Commons.Modules.OXtraGrid.loadXmlgrd(grd);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void MLoadNNXtraGrid(DevExpress.XtraGrid.Views.Grid.GridView grv, string fName)
        {
            //DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repoMemo = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            //repoMemo.WordWrap = true;
            grv.OptionsView.RowAutoHeight = true;

            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + fName + "' "));


            foreach (DevExpress.XtraGrid.Columns.GridColumn col in grv.Columns)
            {
                if (col.Visible)
                {
                    //if (col.ColumnType.ToString() == "System.String")
                    //    col.ColumnEdit = repoMemo;
                    col.AppearanceHeader.Options.UseTextOptions = true;
                    col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    col.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
                    col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    col.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                    //col.Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, fName, col.FieldName, Modules.TypeLanguage);
                    col.Caption = GetNN(dtTmp, col.FieldName, fName);

                }
            }


        }

        public void MLoadNNXtraGrid(DevExpress.XtraGrid.Views.Grid.GridView grv, string fName, int NN)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + fName + "' "));

            foreach (DevExpress.XtraGrid.Columns.GridColumn col in grv.Columns)
            {
                if (col.Visible)
                {
                    col.AppearanceHeader.Options.UseTextOptions = true;
                    col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    col.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
                    col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    col.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

                    //col.Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, fName, col.FieldName, NN);
                    col.Caption = GetNN(dtTmp, col.FieldName, fName);
                }
            }
        }
        #endregion

        #region thay doi nn
        public void ThayDoiNN(Form frm)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + frm.Name + "' "));
            frm.Text = GetNN(dtTmp, frm.Name, frm.Name);
            List<Control> resultControlList = new List<Control>();
            GetControlsCollection(frm, ref resultControlList, null);
     
            foreach (Control control1 in resultControlList)
            {
                try
                {
                    DoiNN(control1, frm, dtTmp);
                }
                catch
                { }
            }
        }

        public void ThayDoiNN(XtraReport report)
        {
             DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + report.Tag.ToString() + "' "));

            foreach (DevExpress.XtraReports.UI.Band band in report.Bands)
            {
                foreach (DevExpress.XtraReports.UI.SubBand subband in band.SubBands)
                {
                    foreach (DevExpress.XtraReports.UI.XRControl control in subband)
                    {
                        if (control.GetType() == typeof(DevExpress.XtraReports.UI.XRTable))
                        {
                            DevExpress.XtraReports.UI.XRTable table = (DevExpress.XtraReports.UI.XRTable)control;
                            foreach (DevExpress.XtraReports.UI.XRTableRow row in table)
                            {
                                foreach (DevExpress.XtraReports.UI.XRTableCell cell in row)
                                {
                                    try {
                                        if (cell.Name.Substring(0, 3).ToString() == "tiN") break;
                                        cell.Text = GetNN(dtTmp, cell.Name, report.Tag.ToString());// translation processing here

                                    }
                                    catch
                                    {
                                        MessageBox.Show("err language substring");
                                    }
                                    
                                    
                                }
                            }
                        }
                        else
                        {
                            control.Text = GetNN(dtTmp, control.Name, report.Tag.ToString());
                        }
                    }
                }
                foreach (DevExpress.XtraReports.UI.XRControl control in band)
                {
                    if (control.GetType() == typeof(DevExpress.XtraReports.UI.XRTable))
                    {
                        DevExpress.XtraReports.UI.XRTable table = (DevExpress.XtraReports.UI.XRTable)control;
                        foreach (DevExpress.XtraReports.UI.XRTableRow row in table)
                        {
                            foreach (DevExpress.XtraReports.UI.XRTableCell cell in row)
                            {
                                try
                                {

                                    if (cell.Name.Substring(0, 3).ToString() == "tiN") break;
                                    cell.Text = GetNN(dtTmp, cell.Name, report.Tag.ToString());// translation processing here

                                }
                                catch
                                {
                                    MessageBox.Show("err language substring");
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        control.Text = GetNN(dtTmp, control.Name, report.Tag.ToString());
                    }

                }

            }
        }
        public void GetPhanQuyen(AccordionControlElement button)
        {
            if (button != null && button.Name != null)
                GetPhanQuyen(button.Name.ToString());
        }
        public void GetPhanQuyen(string button)
        {
            string sSql = " SELECT T1.ID_PERMISION FROM dbo.NHOM_MENU T1 INNER JOIN dbo.MENU T2 ON T2.ID_MENU = T1.ID_MENU INNER JOIN dbo.USERS T3 ON T3.ID_NHOM = T1.ID_NHOM WHERE	T2.KEY_MENU = N'" + button.ToString() + "' AND T3.USER_NAME = N'" + Commons.Modules.UserName + "' ";
            Commons.Modules.iPermission = Convert.ToInt16(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString());
        }
        public void SetPhanQuyen(DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel windowsUIButton)
        {
            int is_line = 1;
            for (int i = 0; i < windowsUIButton.Buttons.Count; i++)
            {
                WindowsUIButton btn = windowsUIButton.Buttons[i] as WindowsUIButton;
                try
                {
                    if (btn.Tag != null)
                    {
                        is_line = 1;
                        if (Commons.Modules.iPermission == 1)
                        {

                            windowsUIButton.Buttons[i].Properties.Enabled = true;

                        }
                        else if (Commons.Modules.iPermission == 2)
                        {

                            switch (btn.Tag)
                            {
                                // edit
                                case "them":
                                case "themsua":
                                case "capnhatphep":
                                case "xoa":
                                case "delete":
                                case "sua":
                                case "luu":
                                case "capnhat":
                                case "update":
                                case "resetpass":
                                case "CapNhap":
                                case "thuchien":
                                    //    windowsUIButton.Buttons[i].Properties.Visible = false;
                                    windowsUIButton.Buttons[i].Properties.Enabled = false;
                                    windowsUIButton.Buttons[i].Properties.ToolTip = "Chức năng chưa được phân quyền";
                                    break;
                                // viiew
                                case "in":
                                case "In":
                                case "intongquat":
                                case "print":
                                case "Print":
                                case "khongluu":
                                case "thoat":
                                case "trove":
                                    //  windowsUIButton.Buttons[i].Properties.Visible = true;
                                    windowsUIButton.Buttons[i].Properties.Enabled = true;
                                    break;
                                default:
                                    windowsUIButton.Buttons[i].Properties.Enabled = true;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (is_line == 1)
                            windowsUIButton.Buttons[i].Properties.Visible = true;
                        else
                        {
                            windowsUIButton.Buttons[i].Properties.Visible = false;
                            is_line++;
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        public static void DinhDangNgayThang(GridColumn gridcol)
        {
            switch (gridcol.FieldName)
            {
                case "CAP_NGAY":
                case "DEN_NGAY":
                case "DEN_THANG":
                case "NGAY_BAT_DAU_HD":
                case "NGAY_BD":
                case "NGAY_BD_THU_VIEC":
                case "NGAY_BI_TAI_NAN":
                case "NGAY_CAP":
                case "NGAY_CAP_CUU_TAI_CHO":
                case "NGAY_CAP_GP":
                case "NGAY_CHAM_DUT_NOP_BHXH":
                case "NGAY_DANH_GIA":
                case "NGAY_DBHXH":
                case "NGAY_DBHXH_DT":
                case "NGAY_HET_HAN":
                case "NGAY_HET_HD":
                case "NGAY_HH_GP":
                case "NGAY_HIEU_LUC":
                case "NGAY_HOC_VIEC":
                case "NGAY_KN_DANG":
                case "NGAY_KT":
                case "NGAY_KT_THU_VIEC":
                case "NGAY_KY":
                case "NGAY_NGHI_VIEC":
                case "NGAY_NGUNG_BHXH":
                case "NGAY_NHAN_DON":
                case "NGAY_NHAP_NGU":
                case "NGAY_QD":
                case "NGAY_RA_KHOI_DANG":
                case "NGAY_RA_KHOI_DOAN":
                case "NGAY_RA_VIEN":
                case "NGAY_SINH":
                case "NGAY_THAM_GIA_BHXH":
                case "NGAY_THOI_VIEC":
                case "NGAY_THU_HOI_BHYT":
                case "NGAY_THU_VIEC":
                case "NGAY_TKL":
                case "NGAY_TTXL":
                case "NGAY_VAO_CONG_DOAN":
                case "NGAY_VAO_CTY":
                case "NGAY_VAO_DANG":
                case "NGAY_VAO_DOAN":
                case "NGAY_VAO_LAM":
                case "NGAY_VAO_LAM_LAI":
                case "NGAY_VAO_VIEN":
                case "NGAY_XUAT_NGU":
                case "NgayBHXH":
                case "NGHI_DEN_NGAY":
                case "NGHI_TU_NGAY":
                case "THANG":
                case "THANG_KTT":
                case "THANG_LXL":
                case "THANG_TINH_LUONG_TC":
                case "TIME_LOGIN":
                case "TU_NGAY":
                case "TU_THANG":

                    gridcol.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    gridcol.DisplayFormat.FormatType = FormatType.DateTime;
                    gridcol.DisplayFormat.FormatString = "d";
                    break;
                default: break;
            }
        }
        public static void DinhDangNgayThang(TileView grvMain)
        {
            foreach (GridColumn gridcol in grvMain.Columns)
            {
                DinhDangNgayThang(gridcol);
            }
        }
        public static void DinhDangNgayThang(GridView grvMain)
        {
            foreach (GridColumn gridcol in grvMain.Columns)
            {
                DinhDangNgayThang(gridcol);
            }
        }
        public void ThayDoiNN(XtraUserControl frm)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + frm.Name + "' "));
            frm.Text = GetNN(dtTmp, frm.Name, frm.Name);
            List<Control> resultControlList = new List<Control>();
            GetControlsCollection(frm, ref resultControlList, null);
            foreach (Control control1 in resultControlList)
            {
                try
                {
                    DoiNN(control1, frm, dtTmp);
                }
                catch
                { }
            }
            try
            {
                //MTabOrder MTab = new MTabOrder(frm);
                //MTab.MSetTabOrder(MTabOrder.TabScheme.AcrossFirst);
            }
            catch
            {
            }
        }

        public void ThayDoiNN(XtraUserControl frm, WindowsUIButtonPanel btnWinUIB)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + frm.Name + "' "));
            frm.Text = GetNN(dtTmp, frm.Name, frm.Name);
            List<Control> resultControlList = new List<Control>();
            GetControlsCollection(frm, ref resultControlList, null);
            foreach (Control control in resultControlList)
            {
                try
                {
                    DoiNN(control, frm, dtTmp);
                }
                catch
                { }
            }
            foreach (Control control1 in resultControlList)
            {
                try
                {
                    DoiNN(control1, frm, dtTmp);
                }
                catch
                { }
            }
            try
            {
                //foreach (WindowsUIButton btn in btnWinUIB.Buttons.but)
                //{
                //    btn.Caption = GetNN(dtTmp, btn.Tag.ToString(), frm.Name);
                //}
                for (int i = 0; i < btnWinUIB.Buttons.Count; i++)
                {
                    try
                    {
                        if (btnWinUIB.Buttons[i].Properties.Tag.ToString() != null)
                        {
                            btnWinUIB.Buttons[i].Properties.Caption = "";
                            btnWinUIB.Buttons[i].Properties.ToolTip = GetNN(dtTmp, btnWinUIB.Buttons[i].Properties.Tag.ToString(), frm.Name);
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch
            { }
        }
        public void ThayDoiNN(XtraForm frm, LayoutControlGroup group, WindowsUIButtonPanel btnWinUIB)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + frm.Name + "' "));
            frm.Text = GetNN(dtTmp, frm.Name, frm.Name);
            //load nn control bên trong
            LoadNNGroupControl(frm, group, dtTmp);
            //load nn windowbutton
            try
            {
                //foreach (WindowsUIButton btn in btnWinUIB.Buttons.but)
                //{
                //    btn.Caption = GetNN(dtTmp, btn.Tag.ToString(), frm.Name);
                //}
                for (int i = 0; i < btnWinUIB.Buttons.Count; i++)
                {
                    try
                    {
                        if (btnWinUIB.Buttons[i].Properties.Tag.ToString() != null)
                        {
                            btnWinUIB.Buttons[i].Properties.Caption = "";
                            btnWinUIB.Buttons[i].Properties.ToolTip = GetNN(dtTmp, btnWinUIB.Buttons[i].Properties.Tag.ToString(), frm.Name);
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch
            { }
        }

        private void LoadNNGroupControl(XtraForm frm, LayoutControlGroup group, DataTable dtTmp)
        {
            foreach (var gr in group.Items)
            {
                if (gr.GetType().Name == "LayoutControlGroup")
                {
                    LayoutControlGroup gro = (LayoutControlGroup)gr;
                    gro.Text = GetNN(dtTmp, gro.Name, frm.Name);
                    LoadNNGroupControl(frm, (LayoutControlGroup)gr, dtTmp);
                }
                else
                {
                    try
                    {
                        LayoutControlItem control1 = (LayoutControlItem)gr;
                        try
                        {
                            if (control1.Control.GetType().Name.ToLower() == "checkedit")
                            {
                                control1.Control.Text = GetNN(dtTmp, control1.Name, frm.Name);
                            }
                            else
                            if (control1.Control.GetType().Name.ToLower() == "radiogroup")
                            {
                                DoiNN(control1.Control, frm, dtTmp);
                            }

                            else
                            {
                                control1.Text = GetNN(dtTmp, control1.Name, frm.Name);
                            }
                            control1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 2, 2);
                            ((DevExpress.XtraEditors.BaseEdit)control1.Control).EnterMoveNextControl = true;
                        }
                        catch
                        { }
                    }
                    catch (Exception)
                    {
                    }
                }

            }
        }

        public void ThayDoiNN(XtraUserControl frm, LayoutControlGroup group)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + frm.Name + "' "));
            frm.Text = GetNN(dtTmp, frm.Name, frm.Name);
            //load nn control bên trong
            LoadNNGroupControl(frm, group, dtTmp);
            //load nn windowbitton
        }

        public void ThayDoiNN(XtraForm frm, LayoutControlGroup group)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + frm.Name + "' "));
            frm.Text = GetNN(dtTmp, frm.Name, frm.Name);
            //load nn control bên trong
            LoadNNGroupControl(frm, group, dtTmp);
            //load nn windowbitton
        }


        private void LoadNNGroupControl(XtraUserControl frm, LayoutControlGroup group, DataTable dtTmp)
        {
            //TabbedControlGroup
            foreach (var gr in group.Items)
            {
                if (gr.GetType().Name == "LayoutControlGroup")
                {
                    LayoutControlGroup gro = (LayoutControlGroup)gr;
                    gro.Text = GetNN(dtTmp, gro.Name, frm.Name);
                    LoadNNGroupControl(frm, (LayoutControlGroup)gr, dtTmp);
                }
                else
                {
                    try
                    {
                        LayoutControlItem control1 = (LayoutControlItem)gr;
                        try
                        {
                            if (control1.Control.GetType().Name.ToLower() == "checkedit")
                            {
                                control1.Control.Text = GetNN(dtTmp, control1.Name, frm.Name);
                            }
                            else
                            if (control1.Control.GetType().Name.ToLower() == "radiogroup")
                            {
                                DoiNN(control1.Control, frm, dtTmp);
                            }

                            else
                            {
                                control1.Text = GetNN(dtTmp, control1.Name, frm.Name);
                            }
                            control1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 2, 2);
                            ((DevExpress.XtraEditors.BaseEdit)control1.Control).EnterMoveNextControl = true;
                        }
                        catch
                        { }
                    }
                    catch (Exception)
                    {
                    }
                }

            }
        }


        public void ThayDoiNN(XtraUserControl frm, LayoutControlGroup group, TabbedControlGroup Tab, WindowsUIButtonPanel btnWinUIB)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + frm.Name + "' "));
            frm.Text = GetNN(dtTmp, frm.Name, frm.Name);
            LoadNNGroupControl(frm, group, dtTmp);
            foreach (LayoutControlGroup item in Tab.TabPages)
            {
                LoadNNGroupControl(frm, item, dtTmp);
            }
            foreach (LayoutGroup item in Tab.TabPages)
            {
                item.Text = GetNN(dtTmp, item.Name, frm.Name);
            }
            try
            {
                //foreach (WindowsUIButton btn in btnWinUIB.Buttons.but)
                //{
                //    btn.Caption = GetNN(dtTmp, btn.Tag.ToString(), frm.Name);
                //}
                for (int i = 0; i < btnWinUIB.Buttons.Count; i++)
                {
                    try
                    {
                    if(btnWinUIB.Buttons[i].Properties.Tag.ToString() != null)
                    {
                            btnWinUIB.Buttons[i].Properties.Caption = "";
                            btnWinUIB.Buttons[i].Properties.ToolTip = GetNN(dtTmp, btnWinUIB.Buttons[i].Properties.Tag.ToString(), frm.Name);
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch
            { }
        }
        private void LoadNNGroupControl(LayoutControlGroup group, DataTable dtTmp, string name)
        {
            group.Text = GetNN(dtTmp, group.Name, name);
            foreach (var gr in group.Items)
            {
                if (gr.GetType().Name == "LayoutControlGroup")
                    LoadNNGroupControl((LayoutControlGroup)gr, dtTmp, name);
                else
                {
                    try
                    {
                        LayoutControlItem control1 = (LayoutControlItem)gr;
                        control1.Text = GetNN(dtTmp, control1.Name, name);
                        control1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 2, 2);
                        ((DevExpress.XtraEditors.BaseEdit)control1.Control).EnterMoveNextControl = true;

                    }
                    catch (Exception)
                    {
                    }
                }

            }
        }
        public void ThayDoiNN(XtraUserControl frm, List<LayoutControlGroup> group, WindowsUIButtonPanel btnWinUIB)
        {
            DataTable dtTmp = new DataTable();
            dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT KEYWORD , CASE " + Modules.TypeLanguage + " WHEN 0 THEN VIETNAM WHEN 1 THEN ENGLISH ELSE CHINESE END AS NN  FROM LANGUAGES WHERE FORM = N'" + frm.Name + "' "));
            frm.Text = GetNN(dtTmp, frm.Name, frm.Name);
            List<Control> resultControlList = new List<Control>();
            GetControlsCollection(frm, ref resultControlList, null);
            foreach (Control control in resultControlList)
            {
                try
                {
                    DoiNN(control, frm, dtTmp);
                }
                catch
                { }
            }

            try
            {
                foreach (LayoutControlGroup gr in group)
                {
                    LoadNNGroupControl(gr, dtTmp, frm.Name);
                }
            }
            catch
            {
            }
            try
            {
                //foreach (WindowsUIButton btn in btnWinUIB.Buttons.but)
                //{
                //    btn.Caption = GetNN(dtTmp, btn.Tag.ToString(), frm.Name);
                //}
                for (int i = 0; i < btnWinUIB.Buttons.Count; i++)
                {
                    try
                    {
                        if (btnWinUIB.Buttons[i].Properties.Tag.ToString() != null)
                        {
                            btnWinUIB.Buttons[i].Properties.Caption = "";
                            btnWinUIB.Buttons[i].Properties.ToolTip = GetNN(dtTmp, btnWinUIB.Buttons[i].Properties.Tag.ToString(), frm.Name);
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch
            { }
        }

        public void DoiNN(Control Ctl, Form frm, DataTable dtNgu)
        {
            // iFontsize
            // sFontForm
            try
            {
                switch (Ctl.GetType().Name.ToString())
                {
                    case "LookUpEdit":
                        {
                            DevExpress.XtraEditors.LookUpEdit CtlDev;
                            CtlDev = (DevExpress.XtraEditors.LookUpEdit)Ctl;
                            CtlDev.Properties.NullText = "";
                            break;
                        }
                    case "Label":
                    case "RadioButton":
                    case "CheckBox":
                        {
                            if (Ctl.Name.ToUpper().Substring(0, 4) != "NONN" & Ctl.Name.Length > 4)
                                Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);// Modules.ObjLanguages.GetLanguage(Modules.ModuleName, frm.Name, Ctl.Name, Modules.TypeLanguage)

                            if (Ctl.GetType().Name.ToString() == "Label")
                            {
                                try
                                {
                                    //Ctl.MouseDoubleClick -= this.Label_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                                try
                                {
                                    //Ctl.MouseDoubleClick += this.Label_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                            }

                            if (Ctl.GetType().Name.ToString() == "RadioButton")
                            {
                                try
                                {
                                    //Ctl.MouseDoubleClick -= this.RadioButton_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                                try
                                {
                                    //Ctl.MouseDoubleClick += this.RadioButton_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                            }

                            if (Ctl.GetType().Name.ToString() == "CheckBox")
                            {
                                try
                                {
                                    //Ctl.MouseDoubleClick -= this.CheckBox_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                                try
                                {
                                    //Ctl.MouseDoubleClick += this.CheckBox_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                            }

                            break;
                        }

                    //case "GroupBox":
                    //    {
                    //        Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);
                    //        if ((Ctl.Name == "grbList"))
                    //        {
                    //            DataTable dtItem = new DataTable();
                    //            try
                    //            {
                    //                dtItem.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "Get_lstDanhsachbaocao", Commons.Modules.UserName, -1, Commons.Modules.TypeLanguage, 1));
                    //            }
                    //            catch (Exception ex)
                    //            {
                    //            }
                    //            foreach (Control ctl1 in Ctl.Controls)
                    //            {
                    //                if ((ctl1.GetType().Name.ToLower() == "navbarcontrol"))
                    //                {
                    //                    foreach (NavBarGroup cl in (NavBarControl)ctl1.Groups)
                    //                        cl.Caption = GetNN(dtNgu, cl.Name, frm.Name);
                    //                    foreach (NavBarItem cl in (NavBarControl)ctl1.Items)
                    //                    {
                    //                        try
                    //                        {
                    //                            cl.Caption = dtItem.Select().Where(x => x("REPORT_NAME").ToString().Trim() == cl.Name.Trim()).Take(1).Single()("TEN_REPORT");
                    //                        }
                    //                        catch (Exception ex)
                    //                        {
                    //                            cl.Caption = GetNN(dtNgu, cl.Name, frm.Name);
                    //                        }
                    //                    }
                    //                    break;
                    //                }
                    //            }
                    //        }

                    //        break;
                    //    }

                    case "TabPage":
                        {
                            Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);          // Modules.ObjLanguages.GetLanguage(Modules.ModuleName, frm.Name, Ctl.Name, Modules.TypeLanguage)
                            break;
                        }

                    case "LabelControl":
                    case "CheckButton":
                    case "CheckEdit":
                    case "XtraTabPage":
                    case "GroupControl":
                        {
                            if (Ctl.Name.ToUpper().Substring(0, 4) != "NONN" & Ctl.Name.Length > 4)
                                Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);// Modules.ObjLanguages.GetLanguage(Modules.ModuleName, frm.Name, Ctl.Name, Modules.TypeLanguage)
                            if (Ctl.GetType().Name.ToString() == "CheckEdit")
                            {
                                try
                                {
                                    //Ctl.MouseDoubleClick -= this.CheckEdit_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                                try
                                {
                                    //Ctl.MouseDoubleClick += this.CheckEdit_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                            }
                            if (Ctl.GetType().Name.ToString() == "LabelControl")
                            {
                                try
                                {
                                    //Ctl.MouseDoubleClick -= this.LabelControl_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                                try
                                {
                                    //Ctl.MouseDoubleClick += this.LabelControl_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                            }

                            break;
                        }

                    case "Button":
                        {
                            if (Ctl.Name.ToUpper().Substring(0, 4) != "NONN" & Ctl.Name.Length > 4)
                            {
                                Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);
                                //LoadImage(Ctl);
                            }

                            break;
                        }

                    case "SimpleButton":
                        {
                            DevExpress.XtraEditors.SimpleButton CtlDev;
                            CtlDev = (DevExpress.XtraEditors.SimpleButton)Ctl;
                            if (Ctl.Name.ToUpper().Substring(0, 4) != "NONN" & Ctl.Name.Length > 4)
                            {
                                Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);
                                //LoadImageDev(CtlDev);
                            }

                            break;
                        }

                    case "RadioGroup":
                        {
                            DevExpress.XtraEditors.RadioGroup radGroup;
                            radGroup = (DevExpress.XtraEditors.RadioGroup)Ctl;
                            for (int i = 0; i <= radGroup.Properties.Items.Count - 1; i++)
                            {
                                if (string.IsNullOrEmpty(radGroup.Properties.Items[i].Value.ToString()))
                                    radGroup.Properties.Items[i].Value = radGroup.Properties.Items[i].Description;
                                radGroup.Properties.Items[i].Description = GetNN(dtNgu, radGroup.Properties.Items[i].Value.ToString(), frm.Name);          // Modules.ObjLanguages.GetLanguage(Modules.ModuleName, frm.Name, radGroup.Properties.Items(i).Description, Modules.TypeLanguage)
                            }
                            /*
                            try
                            {
                                if (radGroup.SelectedIndex == -1)
                                    radGroup.SelectedIndex = 0;
                            }
                            catch
                            {
                            }
                            */
                            break;
                        }

                    case "CheckedListBoxControl":
                        {
                            DevExpress.XtraEditors.CheckedListBoxControl chkGroup;
                            chkGroup = (DevExpress.XtraEditors.CheckedListBoxControl)Ctl;

                            for (int i = 0; i <= chkGroup.Items.Count - 1; i++)
                                chkGroup.Items[i].Description = GetNN(dtNgu, chkGroup.Items[i].Description, frm.Name);// Modules.ObjLanguages.GetLanguage(Modules.ModuleName, frm.Name, chkGroup.Items(i).Description, Modules.TypeLanguage)
                            break;
                        }

                    case "XtraTabControl":
                        {
                            DevExpress.XtraTab.XtraTabControl tabControl;
                            tabControl = (DevExpress.XtraTab.XtraTabControl)Ctl;
                            for (int i = 0; i <= tabControl.TabPages.Count - 1; i++)
                                tabControl.TabPages[i].Text = GetNN(dtNgu, tabControl.TabPages[i].Name, frm.Name);
                            break;
                        }

                    case "GridControl":
                        {
                            DevExpress.XtraGrid.GridControl grid;
                            grid = (DevExpress.XtraGrid.GridControl)Ctl;
                            DevExpress.XtraGrid.Views.Grid.GridView mainView = (DevExpress.XtraGrid.Views.Grid.GridView)grid.MainView;
                            try { Commons.Modules.OXtraGrid.CreateMenuReset(grid); }
                            catch { }

                            foreach (DevExpress.XtraGrid.Views.Base.ColumnView view in grid.ViewCollection)
                            {
                                if ((view) is DevExpress.XtraGrid.Views.Grid.GridView)
                                {
                                    foreach (DevExpress.XtraGrid.Columns.GridColumn col in view.Columns)
                                    {
                                        if (col.Visible)
                                        {
                                            col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                            col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                                            col.AppearanceHeader.Options.UseTextOptions = true;
                                            col.Caption = GetNN(dtNgu, col.FieldName, frm.Name);
                                            AutoCotDev(col);
                                        }
                                    }
                                    MVisGrid((DevExpress.XtraGrid.Views.Grid.GridView)view, frm.Name, view.Name.ToString(), Commons.Modules.UserName, true);
                                    try
                                    {
                                        //view.MouseUp -= this.GridView_MouseUp;
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        //view.MouseUp += this.GridView_MouseUp;
                                    }
                                    catch
                                    {
                                    }

                                    try
                                    {
                                        //view.DoubleClick -= this.GridView_DoubleClick;
                                    }
                                    catch
                                    {
                                    }

                                    try
                                    {
                                        //view.DoubleClick += this.GridView_DoubleClick;
                                    }
                                    catch
                                    {
                                    }
                                }
                            }

                            break;
                        }

                }
            }
            catch
            {
            }
        }
        public void DoiNN(Control Ctl, XtraUserControl frm, DataTable dtNgu)
        {
            // iFontsize
            // sFontForm
            try
            {
                switch (Ctl.GetType().Name.ToString())
                {
                    case "LookUpEdit":
                        {
                            DevExpress.XtraEditors.LookUpEdit CtlDev;
                            CtlDev = (DevExpress.XtraEditors.LookUpEdit)Ctl;
                            CtlDev.Properties.NullText = "";
                            break;
                        }
                    case "Label":
                    case "LayoutControlGroup":
                    case "LabelControl":
                    case "GroupControl":
                    case "TextBoxMaskBox":
                    case "RadioButton":
                    case "CheckEdit":
                    case "CheckBox":

                        {
                            if (Ctl.Name.ToUpper().Substring(0, 4) != "NONN" & Ctl.Name.Length >= 4)
                                Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);// Modules.ObjLanguages.GetLanguage(Modules.ModuleName, frm.Name, Ctl.Name, Modules.TypeLanguage)

                            if (Ctl.GetType().Name.ToString() == "Label")
                            {
                                try
                                {
                                    //Ctl.MouseDoubleClick -= this.Label_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                                try
                                {
                                    //Ctl.MouseDoubleClick += this.Label_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                            }

                            if (Ctl.GetType().Name.ToString() == "RadioButton")
                            {
                                try
                                {
                                    //Ctl.MouseDoubleClick -= this.RadioButton_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                                try
                                {
                                    //Ctl.MouseDoubleClick += this.RadioButton_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                            }

                            if (Ctl.GetType().Name.ToString() == "CheckBox")
                            {
                                try
                                {
                                    //Ctl.MouseDoubleClick -= this.CheckBox_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                                try
                                {
                                    //Ctl.MouseDoubleClick += this.CheckBox_MouseDoubleClick;
                                }
                                catch
                                {
                                }
                            }

                            break;
                        }

                    case "TabPage":
                        {
                            Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);          // Modules.ObjLanguages.GetLanguage(Modules.ModuleName, frm.Name, Ctl.Name, Modules.TypeLanguage)
                            break;
                        }
                    case "Button":
                        {
                            if (Ctl.Name.ToUpper().Substring(0, 4) != "NONN" & Ctl.Name.Length > 4)
                            {
                                Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);
                                //LoadImage(Ctl);
                            }

                            break;
                        }

                    case "SimpleButton":
                        {
                            DevExpress.XtraEditors.SimpleButton CtlDev;
                            CtlDev = (DevExpress.XtraEditors.SimpleButton)Ctl;
                            if (Ctl.Name.ToUpper().Substring(0, 4) != "NONN" & Ctl.Name.Length > 4)
                            {
                                Ctl.Text = GetNN(dtNgu, Ctl.Name, frm.Name);
                                //LoadImageDev(CtlDev);
                            }

                            break;
                        }

                    case "RadioGroup":
                        {
                            DevExpress.XtraEditors.RadioGroup radGroup;
                            radGroup = (DevExpress.XtraEditors.RadioGroup)Ctl;
                            for (int i = 0; i <= radGroup.Properties.Items.Count - 1; i++)
                            {
                                if (string.IsNullOrEmpty(radGroup.Properties.Items[i].Tag.ToString()))
                                    radGroup.Properties.Items[i].Tag = radGroup.Properties.Items[i].Description;
                                radGroup.Properties.Items[i].Description = GetNN(dtNgu, radGroup.Properties.Items[i].Tag.ToString(), frm.Name);          // Modules.ObjLanguages.GetLanguage(Modules.ModuleName, frm.Name, radGroup.Properties.Items(i).Description, Modules.TypeLanguage)
                            }
                            try
                            {
                                if (radGroup.SelectedIndex == -1)
                                    radGroup.SelectedIndex = 0;
                            }
                            catch
                            {
                            }

                            break;
                        }

                    case "CheckedListBoxControl":
                        {
                            DevExpress.XtraEditors.CheckedListBoxControl chkGroup;
                            chkGroup = (DevExpress.XtraEditors.CheckedListBoxControl)Ctl;

                            for (int i = 0; i <= chkGroup.Items.Count - 1; i++)
                                chkGroup.Items[i].Description = GetNN(dtNgu, chkGroup.Items[i].Description, frm.Name);// Modules.ObjLanguages.GetLanguage(Modules.ModuleName, frm.Name, chkGroup.Items(i).Description, Modules.TypeLanguage)
                            break;
                        }

                    case "XtraTabControl":
                        {
                            DevExpress.XtraTab.XtraTabControl tabControl;
                            tabControl = (DevExpress.XtraTab.XtraTabControl)Ctl;
                            for (int i = 0; i <= tabControl.TabPages.Count - 1; i++)
                                tabControl.TabPages[i].Text = GetNN(dtNgu, tabControl.TabPages[i].Name, frm.Name);
                            break;
                        }

                    case "GridControl":
                        {
                            DevExpress.XtraGrid.GridControl grid;
                            grid = (DevExpress.XtraGrid.GridControl)Ctl;
                            DevExpress.XtraGrid.Views.Grid.GridView mainView = (DevExpress.XtraGrid.Views.Grid.GridView)grid.MainView;
                            try { Commons.Modules.OXtraGrid.CreateMenuReset(grid); } catch { }

                            foreach (DevExpress.XtraGrid.Views.Base.ColumnView view in grid.ViewCollection)
                            {
                                if ((view) is DevExpress.XtraGrid.Views.Grid.GridView)
                                {
                                    foreach (DevExpress.XtraGrid.Columns.GridColumn col in view.Columns)
                                    {
                                        if (col.Visible)
                                        {
                                            col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                            col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                                            col.AppearanceHeader.Options.UseTextOptions = true;
                                            col.Caption = GetNN(dtNgu, col.FieldName, frm.Name);      // Modules.ObjLanguages.GetLanguage(Modules.ModuleName, frm.Name, col.Name, Modules.TypeLanguage),

                                            AutoCotDev(col);
                                        }
                                    }
                                    MVisGrid((DevExpress.XtraGrid.Views.Grid.GridView)view, frm.Name, view.Name.ToString(), Commons.Modules.UserName, true);
                                    try
                                    {
                                        //view.MouseUp -= this.GridView_MouseUp;
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        //view.MouseUp += this.GridView_MouseUp;
                                    }
                                    catch
                                    {
                                    }

                                    try
                                    {
                                        //view.DoubleClick -= this.GridView_DoubleClick;
                                    }
                                    catch
                                    {
                                    }

                                    try
                                    {
                                        //view.DoubleClick += this.GridView_DoubleClick;
                                    }
                                    catch
                                    {
                                    }
                                }
                            }

                            break;
                        }

                        //case "DataGridView":
                        //    {
                        //        foreach (DataGridViewColumn cl in (DataGridView)Ctl.Columns)
                        //        {
                        //            cl.HeaderText = GetNN(dtNgu, cl.Name, frm.Name);
                        //            AutoCotGrid(cl);
                        //        }
                        //        (DataGridView)Ctl.ColumnHeadersDefaultCellStyle = Commons.Modules.DataGridViewCellStyle1;
                        //        (DataGridView)Ctl.DefaultCellStyle = Commons.Modules.DataGridViewCellStyle2;
                        //        MVisGrid((DataGridView)Ctl, frm.Name, (DataGridView)Ctl.Name.ToString(), Commons.Modules.UserName);
                        //        break;
                        //    }

                        //case "DataGridViewNew":
                        //    {
                        //        foreach (DataGridViewColumn cl in (DataGridView)Ctl.Columns)
                        //        {
                        //            cl.HeaderText = GetNN(dtNgu, cl.Name, frm.Name);
                        //            AutoCotGrid(cl);
                        //        }

                        //        MVisGrid((DataGridView)Ctl, frm.Name, (DataGridView)Ctl.Name.ToString(), Commons.Modules.UserName);
                        //        break;
                        //    }

                        //case "DataGridViewEditor":
                        //    {
                        //        foreach (DataGridViewColumn cl in (DataGridView)Ctl.Columns)
                        //        {
                        //            cl.HeaderText = GetNN(dtNgu, cl.Name, frm.Name);
                        //            AutoCotGrid(cl);
                        //        }

                        //        (DataGridView)Ctl.ColumnHeadersDefaultCellStyle = Commons.Modules.DataGridViewCellStyle1;
                        //        (DataGridView)Ctl.DefaultCellStyle = Commons.Modules.DataGridViewCellStyle2;

                        //        MVisGrid((DataGridView)Ctl, frm.Name, (DataGridView)Ctl.Name.ToString(), Commons.Modules.UserName);
                        //        break;
                        //    }

                        //case object _ when "NavBarControl" | "navBarControl":
                        //    {
                        //        foreach (NavBarGroup cl in (NavBarControl)Ctl.Groups)
                        //            cl.Caption = GetNN(dtNgu, cl.Name, frm.Name);
                        //        foreach (NavBarItem cl in (NavBarControl)Ctl.Items)
                        //            cl.Caption = GetNN(dtNgu, cl.Name, frm.Name);
                        //        break;
                        //    }
                }
            }
            catch
            {
            }
        }

        public void MVisGrid(DevExpress.XtraGrid.Views.Grid.GridView grv, string sForm, string sControl, string UName, bool MDev)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                string sDLieuForm = "";
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "MGetDsCotVis", sForm, sControl, UName));
                if (dtTmp.Rows.Count <= 0)
                    return;

                sDLieuForm = Convert.ToString(dtTmp.Rows[0]["COL_VIS"].ToString());
                if (sDLieuForm.ToUpper() == "ALL")
                    return;


                string[] chuoi_tach = sDLieuForm.Split(new Char[] { '@' });

                foreach (string s in chuoi_tach)
                {
                    if (s.ToString().Trim() != "")
                    {
                        try
                        {
                            grv.Columns[s].Visible = false;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public void AutoCotDev(DevExpress.XtraGrid.Columns.GridColumn col)
        {
            try
            {
                if (col.ColumnType.ToString() == typeof(DateTime).ToString())
                    col.BestFit();
                else if (col.Name.Contains("MS_MAY"))
                    col.BestFit();
                else if (col.Name.Contains("MS_PT"))
                    col.BestFit();
            }
            catch
            {
            }
        }
        public string ConvertNumberToText(double number, string tiente)
        {
            string text = number.ToString("#");
            string[] array = new string[]
            {
        "không",
        "một",
        "hai",
        "ba",
        "bốn",
        "năm",
        "sáu",
        "bảy",
        "tám",
        "chín"
            };
            string[] array2 = new string[]
            {
        "",
        "nghìn",
        "triệu",
        "tỷ"
            };
            string text2 = " ";
            bool flag = false;
            double num = 0.0;
            try
            {
                num = Convert.ToDouble(text.ToString());
            }
            catch
            {
            }
            if (num < 0.0)
            {
                num = -num;
                text = num.ToString();
                flag = true;
            }
            int i = text.Length;
            if (i == 0)
            {
                text2 = array[0] + text2;
            }
            else
            {
                int num2 = 0;
                while (i > 0)
                {
                    int num3 = Convert.ToInt32(text.Substring(i - 1, 1));
                    i--;
                    int num4;
                    if (i > 0)
                    {
                        num4 = Convert.ToInt32(text.Substring(i - 1, 1));
                    }
                    else
                    {
                        num4 = -1;
                    }
                    i--;
                    int num5;
                    if (i > 0)
                    {
                        num5 = Convert.ToInt32(text.Substring(i - 1, 1));
                    }
                    else
                    {
                        num5 = -1;
                    }
                    i--;
                    if (num3 > 0 || num4 > 0 || num5 > 0 || num2 == 3)
                    {
                        text2 = array2[num2] + text2;
                    }
                    num2++;
                    if (num2 > 3)
                    {
                        num2 = 1;
                    }
                    if (num3 == 1 && num4 > 1)
                    {
                        text2 = "một " + text2;
                    }
                    else if (num3 == 5 && num4 > 0)
                    {
                        text2 = "lăm " + text2;
                    }
                    else if (num3 > 0)
                    {
                        text2 = array[num3] + " " + text2;
                    }
                    if (num4 < 0)
                    {
                        break;
                    }
                    if (num4 == 0 && num3 > 0)
                    {
                        text2 = "lẻ " + text2;
                    }
                    if (num4 == 1)
                    {
                        text2 = "mười " + text2;
                    }
                    if (num4 > 1)
                    {
                        text2 = array[num4] + " mươi " + text2;
                    }
                    if (num5 < 0)
                    {
                        break;
                    }
                    if (num5 > 0 || num4 > 0 || num3 > 0)
                    {
                        text2 = array[num5] + " trăm " + text2;
                    }
                    text2 = " " + text2;
                }
            }
            if (flag)
            {
                text2 = "Âm " + text2;
            }
            return text2.Replace("  ", " ") + tiente;
        }


        public string GetNN(DataTable dtNN, string sKeyWord, string sFormName)
        {
            string sNN = "";
            try
            {
                sNN = dtNN.Select("KEYWORD = '" + sKeyWord.ToUpper().Replace("ItemFor".ToUpper(), "") + "' OR KEYWORD = '" + sKeyWord + "' ")[0][1].ToString();
            }
            catch
            {
                sNN = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, sFormName, sKeyWord, Modules.TypeLanguage);
            }
            return sNN;
        }
        public void GetControlsCollection(Control root, ref List<Control> AllControls, Func<Control, Control> filter)
        {
            foreach (Control child in root.Controls)
            {
                if (Commons.Modules.lstControlName.Any(x => x.ToString() == child.GetType().Name))
                    AllControls.Add(child);
                if (child.Controls.Count > 0)
                    GetControlsCollection(child, ref AllControls, filter);
            }
        }

        //public string GetNN(LayoutControlGroup layout, string sKeyWord, string sFormName)
        //{
        //}
        #endregion

        #region MA HOA

        static string SecurityKey = "vietsoft.com.vn";
        static string chuoi = "_13579_";
        /// <summary>
        /// Encrypt a string using dual encryption method. Return a encrypted cipher Text
        /// </summary>
        /// <param name="toEncrypt">string to be encrypted</param>
        /// <param name="useHashing">use hashing? send to for extra secirity</param>
        /// <returns></returns>
        /// 
        public string Encrypt(string toEncrypt, bool useHashing)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(chuoi + toEncrypt + chuoi);

                System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
                // Get the key from config file
                string key = SecurityKey; /*(string)settingsReader.GetValue("SecurityKey", typeof(String));*/
                                          //System.Windows.Forms.MessageBox.Show(key);
                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch
            {
                byte[] byteData = Encoding.Unicode.GetBytes("");
                return Convert.ToBase64String(byteData);
            }
        }
        /// <summary>
        /// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
        /// </summary>
        /// <param name="cipherString">encrypted string</param>
        /// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
        /// <returns></returns>
        public string Decrypt(string cipherString, bool useHashing)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(cipherString);

                System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
                //Get your key from config file to open the lock!
                string key = SecurityKey;//(string)settingsReader.GetValue("SecurityKey", typeof(String));

                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                return UTF8Encoding.UTF8.GetString(resultArray).Split(new string[] { chuoi }, StringSplitOptions.None)[1];
            }
            catch
            {
                byte[] byteData = Encoding.Unicode.GetBytes("");
                //return UTF8Encoding.UTF8.GetString(byteData).Split(new string[] { chuoi }, StringSplitOptions.None)[1];
                return Convert.ToBase64String(byteData);
            }
        }


        #endregion

        #region creatbt
        public bool MCreateTableToDatatable(string connectionString, string tableSQLName, DataTable table, string sTaoTable)
        {
            try
            {
                if (sTaoTable == "")
                {
                    if (!MCreateTable(tableSQLName, table, connectionString))
                        return false;
                }
                else
                {
                    Commons.Modules.ObjSystems.XoaTable(tableSQLName, connectionString);
                    SqlHelper.ExecuteReader(connectionString, CommandType.Text, sTaoTable);
                }

                using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    System.Data.SqlClient.SqlBulkCopy bulkCopy = new System.Data.SqlClient.SqlBulkCopy(connection, System.Data.SqlClient.SqlBulkCopyOptions.TableLock | System.Data.SqlClient.SqlBulkCopyOptions.FireTriggers | System.Data.SqlClient.SqlBulkCopyOptions.UseInternalTransaction, null);

                    bulkCopy.DestinationTableName = tableSQLName;
                    connection.Open();

                    bulkCopy.WriteToServer(table);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                return false;
            }
            return true;
        }
        public bool MCreateTable(string tableName, DataTable table, string connectionString)
        {
            int i = 1;
            try
            {
                string sql = "CREATE TABLE " + tableName + " (" + "\n";

                // columns
                foreach (DataColumn col in table.Columns)
                {
                    sql += "[" + col.ColumnName + "] " + MGetTypeSql(col.DataType, col.MaxLength, 10, 2) + "," + "\n";
                    i += 1;
                }
                sql += ")";

                Commons.Modules.ObjSystems.XoaTable(tableName);
                SqlHelper.ExecuteReader(connectionString, CommandType.Text, sql);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void XoaTable(string strTableName)
        {
            try
            {
                strSql = "DROP TABLE " + strTableName;
                SqlHelper.ExecuteScalar(IConnections.CNStr, CommandType.Text, strSql);
            }
            catch
            {
            }
        }

        public void XoaTable(string strTableName, string sCNStr)
        {
            try
            {
                strSql = "DROP TABLE " + strTableName;
                SqlHelper.ExecuteScalar(sCNStr, CommandType.Text, strSql);
            }
            catch
            {
            }
        }

        public string MGetTypeSql(object type, int columnSize, int numericPrecision, int numericScale)
        {
            switch (type.ToString())
            {
                case "System.String":
                    {
                        if ((columnSize >= 2147483646))
                            return "NVARCHAR(MAX)";
                        else
                            return (columnSize == -1) ? "NVARCHAR(MAX)" : "NVARCHAR(" + columnSize.ToString() + ")";
                    }

                case "System.Decimal":
                    {
                        if (numericScale > 0)
                            return "REAL";
                        else if (numericPrecision > 10)
                            return "BIGINT";
                        else
                            return "INT";
                    }

                case "System.Boolean":
                    {
                        return "BIT";
                    }

                case "System.Double":
                    {
                        return "FLOAT";
                    }

                case "System.Single":
                    {
                        return "REAL";
                    }

                case "System.Int64":
                    {
                        return "BIGINT";
                    }

                case "System.Int16":
                    {
                        return "INT";
                    }

                case "System.Int32":
                    {
                        return "INT";
                    }

                case "System.DateTime":
                    {
                        return "DATETIME";
                    }

                case "System.Byte[]":
                    {
                        return "IMAGE";
                    }
                case "System.Byte":
                    {
                        return "tinyint";
                    }

                case "System.Drawing.Image":
                    {
                        return "IMAGE";
                    }

                default:
                    {
                        throw new Exception(type.ToString() + " not implemented.");
                    }
            }
        }
        #endregion
        #region add combobox search
        public void AddCombSearchLookUpEdit(RepositoryItemSearchLookUpEdit cboSearch, string Value, string Display, GridView grv, DataTable dtTmp)
        {
            cboSearch.NullText = "";
            cboSearch.ValueMember = Value;
            cboSearch.DisplayMember = Display;
            cboSearch.DataSource = dtTmp;
            grv.Columns[Value].ColumnEdit = cboSearch;

            foreach (DevExpress.XtraGrid.Columns.GridColumn col in grv.Columns)
            {
                if (col.Visible)
                {

                    col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    col.AppearanceHeader.Options.UseTextOptions = true;
                    col.Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "RepositoryItemSearchLookUpEdit", col.FieldName, Modules.TypeLanguage);
                }
            }


        }

        public void AddCombXtra(string Value, string Display, GridView grv, string sSql,string cotan,string fName)
        {
            DataTable tempt = new DataTable();
            tempt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, sSql, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
            cbo.NullText = "";
            cbo.ValueMember = Value;
            cbo.DisplayMember = Display;
            cbo.DataSource = tempt;
            grv.Columns[Value].ColumnEdit = cbo;
            cbo.View.PopulateColumns(cbo.DataSource);
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(cbo.View, fName);
            cbo.View.Columns[cotan].Visible = false;
        }
        public void AddCombXtra(string Value, string Display, GridView grv, string sSql)
        {
            DataTable tempt = new DataTable();
            tempt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, sSql, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
            cbo.NullText = "";
            cbo.ValueMember = Value;
            cbo.DisplayMember = Display;
            cbo.DataSource = tempt;

            grv.Columns[Value].ColumnEdit = cbo;
            /*
            DevExpress.XtraGrid.Views.Grid.GridView grv2 = (DevExpress.XtraGrid.Views.Grid.GridView)cbo.DataSource;
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in grv2.Columns)
            {
                if (col.Visible)
                {

                    col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    col.AppearanceHeader.Options.UseTextOptions = true;
                    col.Caption = Modules.ObjLanguages.GetLanguage(Modules.ModuleName, "RepositoryItemSearchLookUpEdit", col.FieldName, Modules.TypeLanguage);
                }
            }
            */
        }
       
        public void AddCombXtra(string Value, string Display, GridView grv, DataTable dt)
        {
            RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
            cbo.NullText = "";
            cbo.ValueMember = Value;
            cbo.DisplayMember = Display;
            cbo.DataSource = dt;
            grv.Columns[Value].ColumnEdit = cbo;
        }

        public void AddCombXtra(string Value, string Display, GridView grv,DataTable dt, string cotan, string fName)
        {
            RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
            cbo.NullText = "";
            cbo.ValueMember = Value;
            cbo.DisplayMember = Display;
            cbo.DataSource = dt;
            grv.Columns[Value].ColumnEdit = cbo;
            cbo.View.PopulateColumns(cbo.DataSource);
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(cbo.View, fName);
            cbo.View.Columns[cotan].Visible = false;
        }


        public void AddCombXtra(string Value, string Display, GridView grv, DataTable tempt, bool Search, string cotan, string form)
        {
            if (Search == true)
            {
                RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
                cbo.NullText = "";
                cbo.ValueMember = Value;
                cbo.DisplayMember = Display;
                cbo.DataSource = tempt;
                Commons.Modules.ObjSystems.MLoadNNXtraGrid(cbo.View, form);
                grv.Columns[Value].ColumnEdit = cbo;
            }
            else
            {
                RepositoryItemLookUpEdit cbo = new RepositoryItemLookUpEdit();
                cbo.NullText = "";
                cbo.ValueMember = Value;
                cbo.DisplayMember = Display;
                cbo.DataSource = tempt;
                grv.Columns[Value].ColumnEdit = cbo;
                cbo.PopulateColumns();
                cbo.Columns[cotan].Visible = false;
                cbo.Columns[Display].Caption = Commons.Modules.ObjLanguages.GetLanguage(form, Display);
            }
        }

        public void AddCombXtra(string Value, string Display, GridView grv, DataTable tempt, bool Search)
        {
            if (Search == true)
            {
                RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
                cbo.NullText = "";
                cbo.ValueMember = Value;
                cbo.DisplayMember = Display;
                cbo.DataSource = tempt;
                grv.Columns[Value].ColumnEdit = cbo;
            }
            else
            {
                RepositoryItemLookUpEdit cbo = new RepositoryItemLookUpEdit();
                cbo.NullText = "";
                cbo.ValueMember = Value;
                cbo.DisplayMember = Display;
                cbo.DataSource = tempt;
                grv.Columns[Value].ColumnEdit = cbo;
            }
        }
        public void AddCombo(string Value, string Display, GridView grv, DataTable tempt)
        {
            try
            {
                RepositoryItemLookUpEdit cbo = new RepositoryItemLookUpEdit();
                cbo.NullText = "";
                cbo.ValueMember = Value;
                cbo.DisplayMember = Display;
                cbo.DataSource = tempt;
                //cbo.Columns[Value].Visible = false;
                cbo.BestFitMode = BestFitMode.BestFitResizePopup;
                cbo.DropDownRows = tempt.Rows.Count;
                cbo.SearchMode = SearchMode.AutoComplete;
                cbo.AutoSearchColumnIndex = 1;
                cbo.PopulateColumns();
                
                grv.Columns[Value].ColumnEdit = cbo;
                grv.BestFitColumns();
                
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void AddComboAnID(string Value, string Display, GridView grv, DataTable tempt)
        {
            try
            {
                RepositoryItemLookUpEdit cbo = new RepositoryItemLookUpEdit();
                cbo.NullText = "";
                cbo.ValueMember = Value;
                cbo.DisplayMember = Display;
                cbo.DataSource = tempt;
                //cbo.Columns[Value].Visible = false;
                cbo.BestFitMode = BestFitMode.BestFitResizePopup;
                cbo.DropDownRows = tempt.Rows.Count;
                cbo.SearchMode = SearchMode.AutoComplete;
                cbo.AutoSearchColumnIndex = 1;
                cbo.PopulateColumns();
                cbo.Columns[0].Visible = false;
                cbo.Columns[1].Caption = Commons.Modules.ObjLanguages.GetLanguage("frmDanhgia", "Ten_NDDG");
                grv.Columns[Value].ColumnEdit = cbo;
                grv.BestFitColumns();

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void AddCombo(string Value, string Display, GridView grv, DataTable tempt, bool FontVni)
        {
            try
            {
                RepositoryItemLookUpEdit cbo = new RepositoryItemLookUpEdit();
                cbo.AppearanceDropDown.Options.UseFont = true;
                cbo.AppearanceDropDown.Font = new System.Drawing.Font("VNI-Times", 12);
                cbo.NullText = "";
                cbo.ValueMember = Value;
                cbo.DisplayMember = Display;
                cbo.DataSource = tempt;
                cbo.BestFitMode = BestFitMode.BestFitResizePopup;
                cbo.DropDownRows = tempt.Rows.Count;
                cbo.SearchMode = SearchMode.AutoComplete;
                cbo.AutoSearchColumnIndex = 1;
                cbo.PopulateColumns();
                cbo.Columns[0].Visible = false;


                grv.Columns[Value].ColumnEdit = cbo;
                grv.BestFitColumns();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void AddCombobyTree(string Value, string Display, TreeList tree, DataTable tempt)
        {
            RepositoryItemLookUpEdit cbo = new RepositoryItemLookUpEdit();
            cbo.NullText = "";
            cbo.ValueMember = Value;
            cbo.DisplayMember = Display;
            cbo.DataSource = tempt;
            tree.Columns[Value].ColumnEdit = cbo;
        }
        #endregion
        public void AddnewRow(GridView view, bool add)
        {
            view.OptionsBehavior.Editable = true;
            if (add == true)
            {
                view.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;

            }
        }
        public void DeleteAddRow(GridView view)
        {
            view.OptionsBehavior.Editable = false;
            view.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
        }
        #region lấy table từ grid
        public DataTable ConvertDatatable(GridControl grid)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)grid.DataSource;
            return dt;
        }
        public DataTable ConvertDatatable(GridView view)
        {
            view.PostEditor();
            view.UpdateCurrentRow();
            DataView dt = (DataView)view.DataSource;
            if (dt == null || dt.Count == 0)
                return null;
            DataTable tempt = dt.ToTable();
            return tempt;
        }


        public DataRow ThongTinChung()
        {
            DataTable tempt = new DataTable();
            tempt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM dbo.THONG_TIN_CHUNG"));
            return tempt.Rows[0];
        }

        public DataRow BLMCPC(Int64 idcn, DateTime ngayhd)
        {
            if (ngayhd > DateTime.MinValue)
            {
                DataTable tempt = new DataTable();
                tempt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM [funGetLuongKyHopDong](" + idcn + ",'" + ngayhd.ToString("MM/dd/yyyy") + "')"));
                if (tempt.Rows.Count == 0)
                    tempt.Rows.Add(idcn, 0, 0, 0);
                return tempt.Rows[0]; ;
            }
            return null;
        }
        public DataRow TienTroCap(Int64 idcn, DateTime ngaynv, int idldtv)
        {
            //ID_CN	LUONG_TRO_CAP	TIEN_TRO_CAP
            DataTable tempt = new DataTable();
            tempt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM [dbo].[GetTienTroCap]('" + ngaynv.ToString("MM/dd/yyyy") + "'," + idcn + "," + idldtv + ")"));
            return tempt.Rows[0];
        }

        public DataRow TienPhep(Int64 idcn, DateTime ngaynv)
        {
            //ID_CN	LUONG_TP	SO_NGAY_PHEP	TIEN_PHEP
            DataTable tempt = new DataTable();
            tempt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM [dbo].[GetTienPhep]('" + ngaynv.ToString("MM/dd/yyyy") + "'," + idcn + ")"));
            return tempt.Rows[0];
        }



        #endregion

        #region Loadcombo phân quyền
        public void LoadCboDonVi(SearchLookUpEdit cboSearch_DV)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_DV, dt, "ID_DV", "TEN_DV", "TEN_DV");
                //Modules.ObjLanguages.GetLanguage(Modules.ModuleName, fName, col.FieldName, Modules.TypeLanguage);
                //abc

                cboSearch_DV.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboDonViKO(SearchLookUpEdit cboSearch_DV)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_DV, dt, "ID_DV", "TEN_DV", "TEN_DV");
                //Modules.ObjLanguages.GetLanguage(Modules.ModuleName, fName, col.FieldName, Modules.TypeLanguage);
                //abc

                cboSearch_DV.EditValue = 1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboNguyenQuan(SearchLookUpEdit cboSearch)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNguyenQuan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch, dt, "NGUYEN_QUAN", "NGUYEN_QUAN2", "NGUYEN_QUAN2");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboTruongDaoTao(SearchLookUpEdit cboSearch)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTruongDaoTao", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch, dt, "TRUONG_DT", "TRUONG_DT2", "TRUONG_DT2");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboLinhVucDaoTao(SearchLookUpEdit cboSearch)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLinhcVucDaoTao", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch, dt, "LINH_VUC_DT", "LINH_VUC_DT2", "LINH_VUC_DT2");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboHinhThucDaoTao(SearchLookUpEdit cboSearch)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboHinhThucDaoTao", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch, dt, "HINH_THUC_DT", "HINH_THUC_DT2", "HINH_THUC_DT2");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboKhoaDaoTao(SearchLookUpEdit cboSearch)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboKhoaDaoTao", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch, dt, "ID_KDT", "TEN_KHOA_DT", "TEN_KHOA_DT");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboXiNghiep(SearchLookUpEdit cboSearch_DV, SearchLookUpEdit cboSearch_XN)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboXI_NGHIEP", cboSearch_DV.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_XN, dt, "ID_XN", "TEN_XN", "TEN_XN");
                cboSearch_XN.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        
        public void LoadCboTo(SearchLookUpEdit cboSearch_DV, SearchLookUpEdit cboSearch_XN, SearchLookUpEdit cboSearch_TO)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTO", cboSearch_DV.EditValue, cboSearch_XN.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_TO, dt, "ID_TO", "TEN_TO", "TEN_TO");
                cboSearch_TO.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboLDV(SearchLookUpEdit cboSearch_LDV)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLDV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_LDV, dt, "ID_LDV", "TEN_LDV", "TEN_LDV");
                cboSearch_LDV.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboCN(SearchLookUpEdit cboSearch_CN)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_CN, dt, "ID_CN", "HO_TEN", "HO_TEN");
                cboSearch_CN.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public void LoadCboQHGD(SearchLookUpEdit cboSearch_QHGD)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboQH_GD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_QHGD, dt, "ID_QH", "TEN_QH", "TEN_QH");
                cboSearch_QHGD.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion
        public void LoadCboTTHD(SearchLookUpEdit cboSearch_TTHD)
        {
            try
            {
                DataTable dt = new DataTable();

                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTinhTrangHD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_TTHD, dt, "ID_TT_HD", "TEN_TT_HD", "TEN_TT_HD");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        public void LoadCboTTHT(SearchLookUpEdit cboSearch_TTHT)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTinhTrangHT", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearch_TTHT, dt, "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        
        #region data combobox hay dùng
        public DataTable DataLyDoVang(bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLDV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataLoaiDieuChinh(bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListLOAI_DIEU_CHINH", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataDanToc(bool coAll)
        {
            //ID_DT,TEN_DT
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDanToc", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
               
        public DataTable DataThanhPho(int ID_QG, bool coAll)
        {
            //ID_TP,TEN_TP
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboThanhPho", ID_QG, Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataLoaiSanPham(bool coAll)
        {
            //ID_LSP,TEN_SP
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiSanPham", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataLoaiHangHoa(bool coAll)
        {
            //ID_LSP,TEN_SP
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiHangHoa", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataNhomHangHoa(bool coAll)
        {
            //ID_LSP,TEN_SP
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNhomHangHoa", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataToChuyen(bool coAll)
        {
            //ID_LSP,TEN_SP
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTOCHUYEN", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataCUM(int ID_LSP, bool coAll)
        {
            //ID_CUM,TEN_CUM
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCUM", ID_LSP, Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataBacTho(bool coAll)
        {
            //ID_LSP,TEN_SP
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboBacTho", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataPhuCap(string ngay)
        {
            //ID_LSP,TEN_SP
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetTenPC", Convert.ToDateTime(ngay)));
            return dt;
        }
        public DataTable DataLoaiMay(bool coAll)
        {
            //ID_LSP,TEN_SP
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiMay", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataBenhVien(bool coAll)
        {
            //ID_BV,TEN_BV
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboBenhVien", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataQuan(int ID_TP, bool coAll)
        {
            //ID_QUAN,TEN_QUAN
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboQuan", ID_TP, Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataPhuongXa(int ID_QUAN, bool coAll)
        {
            //ID_QUAN,TEN_QUAN
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboPhuongXa", ID_QUAN, Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }



        public DataTable DataLyDoThoiViec()
        {
            //ID_LD_TV,TEN_LD_TV
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoLyDoThoiViec", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            return dt;
        }

        public DataTable DataChucVu(bool coAll)
        {
            //ID_CV,TEN_CV
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboChucVu", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataNgachLuong(bool coAll)
        {
            //"ID_NL","TEN_NL"
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNgachLuong", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataCotCapNhat(bool coAll)
        {
            //"ID_COT","TEN_COT"
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCotCapNhat", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataBacLuong(int idnl, bool coAll)
        {
            //ID_CV,TEN_CV
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboBacLuong", idnl, Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataNhom(bool coAll)
        {
            //ID_NHOM,TEN_NHOM
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNhomChamCong", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataCa(int ID_NHOM)
        {
            //ID_CA,CA
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT CA AS ID_CA,CA FROM CHE_DO_LAM_VIEC WHERE ID_NHOM = " + ID_NHOM + " OR " + ID_NHOM + " = -1 ORDER BY CA"));
            return dt;
        }




        public DataTable DataThongTinChung()
        {
            //ID_CV,TEN_CV
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetThongTinChung", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            dt.TableName = "TTC";
            return dt;
        }
        public DataTable DataKhenThuongKyLuat(bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboKhenThuongKyLuat", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataLoaiKhenThuong(bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiKhenThuong", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataNguoiKy()
        {
            //ID_NK, HO_TEN
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNguoiKy", Commons.Modules.UserName));
            return dt;
        }
        public DataTable DataCongNhanTheoDK(bool coAll, Int32 ID_DV, Int32 ID_XN, Int32 ID_TO)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongNhanTheoDieuKien", ID_DV, ID_XN, ID_TO, Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public DataTable DataQuocGia(bool coAll)
        {
            //ID_QG,TEN_QG
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboQuocGia", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataTinHTrangHD(bool coAll)
        {
            //"ID_TT_HD", "TEN_TT_HD",
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTinHTrangHD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataTinHTrangHT(bool coAll)
        {
            //"ID_TT_HT", "TEN_TT_HT,
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTinHTrangHT", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataTinHTrangHN(bool coAll)
        {
            //"ID_TT_HT", "TEN_TT_HT,
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTinHTrangHN", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }


        public DataTable DataNguyenNhanTN(bool coAll)
        {
            //         ID_NGUYEN_NHAN,TEN_NGUYEN_NHAN
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNguyenNhanTN", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataYeuToTN(bool coAll)
        {
            //ID_GAY_TAI_NAN,TEN_YEU_TO
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboYeuToTN", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataNgheNghiep(bool coAll)
        {
            //ID_NGHE_NGHIEP,TEN_NGHE_NGHIEP
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNgheNghiep", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataMucDoTN(bool coAll)
        {
            //ID_MUCDO,TEN_MUCDO
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoMucDo", Commons.Modules.TypeLanguage));
            return dt;
        }
        public DataTable DataTinhTrangGiaDinh(bool coAll)
        {
            //ID_TT_HN,TEN_TT_HN
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoTinhTrangHonNhan", Commons.Modules.TypeLanguage));
            return dt;
        }


        public DataTable DataNoiDungDanhGia(bool coAll)
        {
            //ID_NDDG,TEN_NDDG
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNoiDungDanhGia", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }


        public DataTable DataLoaiCV(bool coAll)
        {
            //ID_LCV,TEN_LCV
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiCV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }

        public DataTable DataLoaiHDLD(bool coAll)
        {
            //ID_LHDLD,TEN_LHDLD
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiHopDongLD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }


        public DataTable DataLoaiTrinhDo(bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiTrinhDo", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataChuyenMon(bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboChuyenMon", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataNoiDungThuongKhacLuong(bool coAll, int id = -1)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNoiDungThuongKhacLuong", id, Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataLoaiQuyetDinh(bool CoAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoLoaiQuyetDinh", Commons.Modules.TypeLanguage));
            return dt;
        }
        public DataTable DataHinhThucTroCap(int id, bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboHTNhanTC", Commons.Modules.UserName, Commons.Modules.TypeLanguage, id, coAll));
            return dt;
        }
        public DataTable DataCongNhan(bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataDonVi(bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDV, dt, "ID_DV", "TEN_DV", "TEN_DV");
            return dt;

        }
        public DataTable DataXiNghiep(int iddv, bool coAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboXI_NGHIEP", iddv, Commons.Modules.UserName, Commons.Modules.TypeLanguage, coAll));
            return dt;
        }
        public DataTable DataTo(int iddv, int idxn, bool CoAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTO", iddv, idxn, Commons.Modules.UserName, Commons.Modules.TypeLanguage, CoAll));
            return dt;
        }

        public DataTable DataTDVH(int LoaiTD, bool CoAll)
        {
            //ID_TDVH,TEN_TDVH
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTrinhDo", LoaiTD, Commons.Modules.UserName, Commons.Modules.TypeLanguage, CoAll));
            return dt;
        }
        public DataTable DataQHGD(bool CoAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboQuanHeGD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, CoAll));
            return dt;
        }

        public DataTable DataLoaiQuocTich(bool CoAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiQuocTich", Commons.Modules.UserName, Commons.Modules.TypeLanguage, CoAll));
            return dt;
        }
        public DataTable DataCapGiayPhep(bool CoAll)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCapGiayPhep", Commons.Modules.UserName, Commons.Modules.TypeLanguage, CoAll));
            return dt;
        }

        public DataTable DataLyDoGiamLDNN(bool CoAll)
        {
            //ID_LDG_LDNN,TEN_LDG_LDNN
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLyDoGiamLDNN", Commons.Modules.UserName, Commons.Modules.TypeLanguage, CoAll));
            return dt;
        }

        #endregion

        #region Định dạng
        public string sDinhDangSoLe(int iSoLe)
        {
            string sChuoi = "#,##0";
            if (iSoLe != 0)
            {
                sChuoi = sChuoi + ".";
                for (int i = 0; i <= iSoLe - 1; i++)
                    sChuoi = sChuoi + "0";
            }
            return sChuoi;
        }

        public string sDinhDangSoLe(int iSoLe, string sChuoi)
        {
            if (iSoLe != 0)
            {
                sChuoi = sChuoi + ".";
                for (int i = 0; i <= iSoLe - 1; i++)
                    sChuoi = sChuoi + "0";
            }
            return sChuoi;
        }
        #endregion
        #region MessageChung
        //xoa
        public DialogResult msgHoi(string sThongBao)
        {
            //ThongBao.Thông_Báo

            DialogResult dl = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", sThongBao),
                 (Commons.Modules.TypeLanguage == 0 ? ThongBao.msgTBV.ToString() : ThongBao.msgTBA), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return dl;
        }

        public void msgChung(string sThongBao)
        {
            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", sThongBao), (Commons.Modules.TypeLanguage == 0 ? ThongBao.msgTBV.ToString() : ThongBao.msgTBA), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void msgChung(string sThongBao, string sLoi)
        {
            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", sThongBao) + "\n" + sLoi, (Commons.Modules.TypeLanguage == 0 ? ThongBao.msgTBV.ToString() : ThongBao.msgTBA), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
        public void MChooseGrid(bool bChose, string sCot, DevExpress.XtraGrid.Views.Grid.GridView grv)
        {
            try
            {
                int i;
                i = 0;
                for (i = 0; i <= grv.RowCount; i++)
                {
                    grv.SetRowCellValue(i, sCot, bChose);
                    grv.UpdateCurrentRow();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void GotoHome(XtraUserControl uc)
        {
            try
            {
                foreach (Control c in uc.ParentForm.Controls)
                {
                    if (c.GetType().Name.ToString() == "TablePanel")
                    {
                        TablePanel table = c as TablePanel;
                        foreach (Control item in table.Controls)
                        {
                            if (item.GetType().Name.ToString() == "TileBar")
                            {
                                TileBar tb = item as TileBar;
                                tb.SelectedItem = tb.GetTileGroupByName("titlegroup").GetTileItemByName("58");
                            }
                        }

                    }

                }
            }
            catch (Exception ex) { }
        }
        public void GotoCongNhan(NavigationFrame uc)
        {
            try
            {
                foreach (Control c in uc.Controls)
                {
                    if (c.GetType().Name.ToString() == "TablePanel")
                    {
                        TablePanel table = c as TablePanel;
                        foreach (Control item in table.Controls)
                        {
                            if (item.GetType().Name.ToString() == "TileBar")
                            {
                                TileBar tb = item as TileBar;
                                tb.SelectedItem = tb.GetTileGroupByName("titlegroup").GetTileItemByName("45");
                            }
                        }

                    }

                }
            }
            catch (Exception ex) { }
        }

        public SplashScreenManager splashScreenManager1;
        public SplashScreenManager ShowWaitForm(XtraUserControl a)
        {
            if (splashScreenManager1 != null) splashScreenManager1.Dispose();
            splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(a.ParentForm, typeof(frmWaitForm), true, true, true);
            splashScreenManager1.ShowWaitForm();
            Thread.Sleep(100);
            return splashScreenManager1;
        }
        public SplashScreenManager ShowWaitForm(XtraForm a)
        {
            if (splashScreenManager1 != null) splashScreenManager1.Dispose();
            splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(a, typeof(frmWaitForm), true, true, true);
            splashScreenManager1.ShowWaitForm();
            Thread.Sleep(100);
            return splashScreenManager1;
        }
        public void HideWaitForm()
        {
            splashScreenManager1.CloseWaitForm();
        }
    }
}
