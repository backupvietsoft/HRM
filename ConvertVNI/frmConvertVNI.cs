using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace ConvertVNI
{
    public partial class frmConvertVNI : DevExpress.XtraEditors.XtraForm
    {
        DataTable dt;
        public frmConvertVNI()
        {
            InitializeComponent();
        }

        private void frmConvertVNI_Load(object sender, EventArgs e)
        {
            LoadcboDataBase();
            LoadcboType();
        }

        private void LoadgrdTableChua()
        {
            try
            {
                dt = new DataTable();
                string sSql = "SELECT T.CHON,T.TABLE_NAME FROM (SELECT CONVERT(BIT, CASE WHEN EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = T.TABLE_NAME AND COLUMN_NAME = 'CAP_NHAT') THEN 1 ELSE 0 END) AS CHON, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES AS T WHERE TABLE_TYPE = 'BASE TABLE' AND  TABLE_SCHEMA ='dbo') AS T WHERE T.CHON = 0 AND EXISTS (SELECT 1 FROM sys.tables AS ST INNER JOIN sys.partitions AS SP ON ST.object_id = SP.object_id WHERE ST.name = T.TABLE_NAME AND SP.rows > 0 ) ORDER BY T.TABLE_NAME";
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                dt.Columns["CHON"].ReadOnly = false;
                if (grdChua.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChua, grvChua, dt, false, true, false, true, false, "");
                    grvChua.Columns["CHON"].Visible = false;
                    grvChua.OptionsSelection.MultiSelect = true;
                    grvChua.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                    grvChua.OptionsSelection.CheckBoxSelectorField = "CHON";
                    grvChua.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                }
                else
                {
                    grdChua.DataSource = dt;
                }
            }
            catch
            {
            }



        }

        private void LoadgrdTableDa()
        {
            dt = new DataTable();
            string sSql = "SELECT T.TABLE_NAME FROM (SELECT CONVERT(BIT, CASE WHEN EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = T.TABLE_NAME AND COLUMN_NAME = 'CAP_NHAT') THEN 1 ELSE 0 END) AS CHON, TABLE_NAME FROM INFORMATION_SCHEMA.TABLES AS T WHERE TABLE_TYPE = 'BASE TABLE' AND  T.TABLE_SCHEMA ='dbo') AS T WHERE T.CHON = 1 ORDER BY T.TABLE_NAME";
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
            if (grdDa.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDa, grvDa, dt, false, true, false, true, false, "");

            }
            else
            {
                grdDa.DataSource = dt;
            }
        }



        private void LoadgrdData(string stable)
        {
            try
            {
                dt = new DataTable();
                string sSql = "SELECT TOP 1000 * FROM [" + stable + "]";
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, true, false, false, false, "");
            }
            catch
            {
            }

         
        }

        private void grvChua_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadgrdData(grvChua.GetFocusedRowCellValue("TABLE_NAME").ToString());
        }

        private void grvDa_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadgrdData(grvDa.GetFocusedRowCellValue("TABLE_NAME").ToString());
        }

        string vni2unicode(string text)
        {
            string result = text;

            string[] uniChars1 = new string[]
             {
                "Ấ","Ấ", "ấ", "Ầ","Ầ", "ầ", "Ẩ", "ẩ", "Ẫ", "ẫ", "Ậ", "ậ", "Ắ", "ắ",
                "Ằ","Ằ", "ằ", "Ẳ", "ẳ", "Ẵ", "ẵ", "Ặ", "ặ", "Ế", "ế", "Ề", "ề",
                "Ể","Ể", "ể", "Ễ", "ễ", "Ệ", "ệ", "Ố", "ố", "Ồ", "ồ", "Ổ", "ổ",
                "Ỗ","Ỗ", "ỗ", "Ộ", "ộ", "Ớ", "ớ", "Ờ", "ờ", "Ở", "ở", "Ỡ", "ỡ",
                "Ợ","Ợ", "ợ", "Ố", "ố", "Ồ", "ồ", "Ổ", "ổ", "Ỗ", "ỗ", "Ộ", "ộ",
                "Ớ","Ớ", "ớ", "Ờ", "ờ", "Ở", "ở", "Ỡ", "ỡ", "Ợ", "ợ", "Ứ", "ứ",
                "Ừ","Ừ", "ừ", "Ử", "ử", "Ữ", "ữ", "Ự", "ự"
             };
            string[] vniChars1 = new string[]
           {
        "AÁ","Aá", "aá", "AÀ","Aà", "aà", "AÅ", "aå", "AÃ", "aã", "AÄ", "aä", "AÉ", "aé",
        "AÈ","Aè", "aè", "AÚ", "aú", "AÜ", "aü", "AË", "aë", "EÁ", "eá", "EÀ", "eà",
        "EÅ","Eå", "eå", "EÃ", "eã", "EÄ", "eä", "OÁ", "oá", "OÀ", "oà", "OÅ", "oå",
        "OÃ","Oã", "oã", "OÄ", "oä", "ÔÙ", "ôù", "ÔØ", "ôø", "ÔÛ", "ôû", "ÔÕ", "ôõ",
        "ÔÏ","Ôï", "ôï", "OÁ", "oá", "OÀ", "oà", "OÅ", "oå", "OÃ", "oã", "OÄ", "oä",
        "ÔÙ","Ôù", "ôù", "ÔØ", "ôø", "ÔÛ", "ôû", "ÔÕ", "ôõ", "ÔÏ", "ôï", "ÖÙ", "öù",
        "ÖØ","Öø", "öø", "ÖÛ", "öû", "ÖÕ", "öõ", "ÖÏ", "öï"
           };

            string[] uniChars = new string[]
        {
    "Ơ", "ơ", "ĩ", "Ị", "ị",
    "À", "Á", "Â", "Ã", "È", "É", "Ê", "Ì", "Í", "Ò",
    "Ó", "Ô", "Õ", "Ù", "Ú", "Ý", "à", "á", "â", "ã",
    "è", "é", "ê", "ì", "í", "ò", "ó", "ô", "õ", "ù",
    "ú", "ý", "Ă", "ă", "Đ", "đ", "Ĩ", "Ũ", "ũ",
    "Ư", "ư", "Ạ", "ạ", "Ả", "ả", "Ẹ", "ẹ",
    "Ẻ", "ẻ", "Ẽ", "ẽ", "Ỉ", "ỉ", "Ọ", "ọ",
    "Ỏ", "ỏ", "Ụ", "ụ", "Ủ", "ủ", "Ỳ", "ỳ", "Ỵ", "ỵ",
    "Ỷ", "ỷ", "Ỹ", "ỹ"
        };

            string[] vniChars = new string[]
            {
    "Ô", "ô", "ó", "Ò", "ò",
    "AØ", "AÙ", "AÂ", "AÕ", "EØ", "EÙ", "EÂ", "Ì", "Í", "OØ",
    "OÙ", "OÂ", "OÕ", "UØ", "UÙ", "YÙ", "aø", "aù", "aâ", "aõ",
    "eø", "eù", "eâ", "ì", "í", "oø", "où", "oâ", "oõ", "uø",
    "uù", "yù", "AÊ", "aê", "Ñ", "ñ", "Ó", "UÕ", "uõ",
    "Ö", "ö", "AÏ", "aï", "AÛ", "aû", "EÏ", "eï",
    "EÛ", "eû", "EÕ", "eõ", "Æ", "æ", "OÏ", "oï",
    "OÛ", "oû", "UÏ", "uï", "UÛ", "uû", "YØ", "yø", "Î", "î",
    "YÛ", "yû", "YÕ", "yõ"
            };

            for (int i = 0; i < vniChars1.Length; i++)
            {
                result = result.Replace(vniChars1[i], uniChars1[i]);
            }

            for (int i = 0; i < vniChars.Length; i++)
            {
                result = result.Replace(vniChars[i], uniChars[i]);
            }

            return result;
        }




        private DataTable TableKey(string sTableName)
        {
            try
            {
                DataTable datakey = new DataTable();
                string sSql = "SELECT COLUMN_NAME, (SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS B WHERE B.TABLE_NAME = '"+ sTableName + "' AND B.COLUMN_NAME = A.COLUMN_NAME) AS DATA_TYPE FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE A WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA +'.' + CONSTRAINT_NAME), 'IsPrimaryKey') = 1 AND TABLE_NAME = '"+ sTableName +"'";
                datakey.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                return datakey;
            }
            catch
            {
                return null;
            }

        }

        private bool ConvertTableVNI(string sTableName)
        {
            DataTable datakey = TableKey(sTableName);
            if (datakey.Rows.Count == 0)
            {
                //Commons.Modules.ObjSystems.HideWaitForm();
                //XtraMessageBox.Show("table " + sTableName + " không có khóa!" , "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                listTrangThai.Items.Add("Table " + sTableName + " không có khóa!");
                listTrangThai.SelectedIndex = listTrangThai.ItemCount;
                listTrangThai.Refresh();
                return false;
            }
            listTrangThai.Items.Add("Đang convert table " + sTableName +".....");
            listTrangThai.SelectedIndex = listTrangThai.ItemCount;
            listTrangThai.Refresh();


            DataTable data = new DataTable();
            data = ((DataTable)grdData.DataSource);
            #region Status bar
            prbIN.Position = 0;
            prbIN.Properties.Step = 1;
            prbIN.Properties.PercentView = true;
            prbIN.Properties.Maximum = data.Rows.Count;
            prbIN.Properties.Minimum = 0;
            #endregion
            List<string> listColUD = new List<string>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                foreach (DataColumn item in data.Columns)
                {

                    if (item.DataType == typeof(string) && datakey.AsEnumerable().Count(x => x["COLUMN_NAME"].ToString().Equals(item.ColumnName)) == 0)
                    {
                        if (i == 0)
                        {
                            listColUD.Add(item.ColumnName);
                        }
                        data.Rows[i][item.ColumnName] = Convert.ToInt32(cboLoaiCV.EditValue) == 1 ? vni2unicode(data.Rows[i][item.ColumnName].ToString()) : ConvertTCVN.TCVN3ToUnicode(data.Rows[i][item.ColumnName].ToString());
                    }
                }
                #region prb
                try
                {
                    prbIN.PerformStep();
                    prbIN.Update();
                }
                catch { }
                #endregion
            }
            data.AcceptChanges();

            //tạo bảng tạm từ lưới
            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "BTCONVERT", Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
            string sWhere = "";
            foreach (DataRow item in datakey.Rows)
            {
                if (item[1].ToString() == "nvarchar")
                {
                    sWhere += "A.[" + item[0] + "] COLLATE SQL_Latin1_General_CP1_CI_AS = B.[" + item[0] + "] COLLATE SQL_Latin1_General_CP1_CI_AS AND ";
                }
                else
                {
                    sWhere += "A.[" + item[0] + "] = B.[" + item[0] + "] AND ";
                }    
            }
            string sUpDate = "";

            foreach (var item in listColUD)
            {
                sUpDate += "A.[" + item + "]  = NULLIF( B.[" + item + "],''),";
            }
            string sSql = "";
            if (listColUD.Count == 0)
            {
                sSql = "ALTER TABLE dbo.[" + sTableName + "] ADD CAP_NHAT BIT";
            }
            else
            {
                sSql = "UPDATE A SET " + sUpDate.Substring(0, sUpDate.Length - 1) + " FROM dbo.[" + sTableName + "] A INNER JOIN BTCONVERT B ON " + sWhere.Substring(0, sWhere.Length - 4) + " ALTER TABLE dbo.[" + sTableName + "] ADD CAP_NHAT BIT";
            }

            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            System.Data.SqlClient.SqlTransaction tran;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            tran = conn.BeginTransaction();
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sSql, conn);
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();

                listTrangThai.Items.Add("Table "+ sTableName + "  thành công!");
                listTrangThai.SelectedIndex = listTrangThai.ItemCount;
                listTrangThai.Refresh();

                Commons.Modules.ObjSystems.XoaTable("BTCONVERT" ,Commons.IConnections.CNStr);

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                listTrangThai.Items.Add("Table " + sTableName + " bị lỗi!" + ex.ToString());
                listTrangThai.SelectedIndex = listTrangThai.ItemCount;
                listTrangThai.Refresh();
                Commons.Modules.ObjSystems.XoaTable("BTCONVERT", Commons.IConnections.CNStr);
                return false;
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (toggleSwitch1.IsOn == false)
            {
                XtraMessageBox.Show("Bạn chưa bật kết nối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            listTrangThai.Items.Clear();
            DataTable dt_CHON = new DataTable();
            try
            {
                dt_CHON = ((DataTable)grdChua.DataSource).AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).CopyToDataTable();
            }
            catch
            {
                XtraMessageBox.Show("Bạn chưa chọn table cần convert!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dt_CHON.Rows.Count == 0)
            {
                XtraMessageBox.Show("Bạn chưa chọn table cần convert!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Commons.Modules.ObjSystems.ShowWaitForm(this);
            //
            //cập nhật trên lưới
            foreach (DataRow item in dt_CHON.Rows)
            {
                int rowHandle = grvChua.LocateByDisplayText(0, grvChua.Columns[1], item[1].ToString());
                // Focus on the row
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    grvChua.FocusedRowHandle = rowHandle;
                    grvChua.SelectRow(rowHandle);
                    LoadgrdData(item[1].ToString());
                    if (!ConvertTableVNI(item[1].ToString()))
                    {
                        continue;
                    }
                }
            }
            LoadgrdTableDa();
            LoadgrdTableChua();
            //Commons.Modules.ObjSystems.HideWaitForm();
            XtraMessageBox.Show("Convert table thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
        private void LoadcboDataBase()
        {
            //cbo_database.Properties.datas
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM sys.sysdatabases"));
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cboDatabase, dt, "name", "name", "");
        }

        private void LoadcboType()
        {
            //cbo_database.Properties.datas
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(Int32));
            dt.Columns.Add("TEN", typeof(string));
            dt.Rows.Add(new object[] { 1, "VNI" });
            dt.Rows.Add(new object[] { 2, "TCVN3" });
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cboLoaiCV, dt, "ID", "TEN", "");
        }

        private void cboDatabase_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (toggleSwitch1.IsOn == true)
                {
                    try
                    {
                        Commons.IConnections.Database = cboDatabase.Text.Trim();
                        LoadgrdTableDa();
                        LoadgrdTableChua();
                    }
                    catch
                    {
                        toggleSwitch1.IsOn = false;
                    }
                }
            }
            catch
            {
            }
        }

        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {
            if (toggleSwitch1.IsOn == true)
            {
                try
                {
                    Commons.IConnections.Database = cboDatabase.Text.Trim();
                    LoadgrdTableDa();
                    LoadgrdTableChua();
                    XtraMessageBox.Show("Connect thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch
                {
                    XtraMessageBox.Show("Connect thành thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    toggleSwitch1.IsOn = false;
                }
            }
            else
            {
                XtraMessageBox.Show("Hủy connect!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void stop_Click(object sender, EventArgs e)
        {
            prbIN.Position = 0;
            toggleSwitch1_Toggled(null,null);
        }
    }
}