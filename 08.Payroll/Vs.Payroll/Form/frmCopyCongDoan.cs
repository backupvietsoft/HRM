using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Reflection;
using System.Linq;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraGrid.Views.Grid;
using NPOI.SS.Formula.Functions;
using static DevExpress.XtraPrinting.Native.ExportOptionsPropertiesNames;
using static NPOI.HSSF.Util.HSSFColor;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraCharts.Native;
using NPOI.HSSF.Record.Chart;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace Vs.Payroll
{
    public partial class frmCopyCongDoan : DevExpress.XtraEditors.XtraForm
    {
        public int iID_DV = -1;
        public DateTime dNgay;
        public frmCopyCongDoan()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, windowsUIButton);
        }

        //sự kiên load form
        private void frmCopyCongDoan_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadCboKhachHang();
            datTuThang.DateTime = dNgay;
            datTuThang.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            datTuThang.Properties.DisplayFormat.FormatString = "MM/yyyy";
            datTuThang.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            datTuThang.Properties.EditFormat.FormatString = "MM/yyyy";
            datTuThang.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            datTuThang.Properties.Mask.EditMask = "MM/yyyy";

            datDenThang.DateTime = dNgay.AddMonths(1);
            datDenThang.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            datDenThang.Properties.DisplayFormat.FormatString = "MM/yyyy";
            datDenThang.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            datDenThang.Properties.EditFormat.FormatString = "MM/yyyy";
            datDenThang.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            datDenThang.Properties.Mask.EditMask = "MM/yyyy";
            LoadData();
            Commons.Modules.ObjSystems.DeleteAddRow(grvData);
            Commons.Modules.sLoad = "";
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "luu":
                    {
                        if (grvData.RowCount == 0)
                            return;
                        grvData.CloseEditor();
                        grvData.UpdateCurrentRow();
                        if (!SaveData())
                        {
                            Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                            return;
                        }
                        else
                        {
                            Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgLuuThanhCong"), Commons.Form_Alert.enmType.Success);
                        }
                        this.DialogResult= DialogResult.OK; 
                        this.Close();
                        break;
                    }
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
            }
        }
        private void LoadCboKhachHang()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();

                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spCopyCongDoan", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_DT, dt, "ID_DT", "TEN_DT", "TEN_DT", true);
            }
            catch { }
        }
        private void LoadData()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();
                try
                {
                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spCopyCongDoan", conn);
                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                    cmd.Parameters.Add("@ID_DT", SqlDbType.Int).Value = cboID_DT.EditValue;
                    cmd.Parameters.Add("@TThang", SqlDbType.Date).Value = datTuThang.DateTime;
                    cmd.Parameters.Add("@DThang", SqlDbType.Date).Value = datDenThang.DateTime;
                    cmd.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    dt = new DataTable();
                    dt = ds.Tables[0].Copy();
                    dt.Columns["CHON"].ReadOnly = false;
                    if (grdData.DataSource == null)
                    {
                        Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, true, false, true, true, "");
                        grvData.Columns["ID_ORD"].OptionsColumn.AllowEdit = false;
                        grvData.Columns["ID_TO"].OptionsColumn.AllowEdit = false;
                        grvData.Columns["CHON"].Visible = false;
                    }
                    else
                    {
                        grdData.DataSource = dt;
                    }

                    try
                    {
                        grvData.OptionsSelection.CheckBoxSelectorField = "CHON";
                        grvData.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    }
                    catch { }
                    DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                    Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_TO", "TEN_TO", "ID_TO", grvData, Commons.Modules.ObjSystems.DataTo(iID_DV, -1, false), this.Name);

                    dt = new DataTable();
                    dt = ds.Tables[1].Copy();
                    DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo1 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                    Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo1, "ID_ORD", "TEN_HH", "ID_ORD", grvData, dt, this.Name);
                }
                catch (Exception ex)
                {
                }
            }
            catch { }
        }
        private bool SaveData()
        {
            string sBT = "sBTQTCN" + Commons.Modules.iIDUser;

            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grdData), "");
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spCopyCongDoan", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                cmd.Parameters.Add("@TThang", SqlDbType.Date).Value = datTuThang.DateTime;
                cmd.Parameters.Add("@DThang", SqlDbType.Date).Value = datDenThang.DateTime;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (dt.Rows[0][0].ToString() == "-99")
                {
                    return false;
                }
                return true;
            }
            catch { return false; }
        }

        private void cboID_DT_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }

        private void datTuThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            datDenThang.DateTime = datTuThang.DateTime.AddMonths(1);
            Commons.Modules.sLoad = "";
            LoadData();
        }

        private void datDenThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }
    }
}
