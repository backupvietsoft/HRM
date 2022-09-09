using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;

namespace Vs.HRM
{
    public partial class frmImportView : DevExpress.XtraEditors.XtraForm
    {
        private string sSQL = "";
        private string sImportType = "";
        private DataTable DT = new DataTable();
        public DataRow _dtrow;
        public DataRow RowSelected
        {
            get
            {
                return _dtrow;
            }
        }
        public frmImportView(string ImportType,string SQL)
        {
            sImportType = ImportType;
            sSQL = SQL;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        public frmImportView(DataTable dt)
        {
            DT = dt;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        #region Event
        private void frmImportView_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadNN();
        }
        //private void btnThoat_Click(object sender, EventArgs e)
        //{
        //    this.DialogResult = DialogResult.Cancel;
        //}
        //private void btnThucHien_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        _dtrow = ((DataRowView)grvChung.GetFocusedRow()).Row;
        //        this.DialogResult = DialogResult.OK;
        //    }
        //    catch { }

        //    this.DialogResult = DialogResult.OK;
        //}
        private void grvChung_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _dtrow = ((DataRowView)grvChung.GetFocusedRow()).Row;
                this.DialogResult = DialogResult.OK;
            }
            catch { }
        }
        #endregion
        #region Function
        public void LoadNN()
        {
            Commons.Modules.ObjSystems.ThayDoiNN(this,Root);
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(grvChung, this.Name);
        }
        private void LoadData()
        {
            try
            {

                DataTable dt = new DataTable();
                if (DT.Rows.Count == 0)
                    dt = GetData(sSQL);
                else
                    dt = DT;

                if (dt == null) return;

                grvChung.Name = string.Concat(grvChung.Name, sImportType);
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, false, true, false, false,true,this.Name);
                grvChung.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public DataTable GetData(string SQL)
        {
            var con = new SqlConnection(Commons.IConnections.CNStr);
            string cmdText = SQL;
            SqlCommand command = new SqlCommand(cmdText, con);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds, "ds1");
            return ds.Tables["ds1"];
        }
        #endregion

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "luu":
                    {
                        try
                        {
                            _dtrow = ((DataRowView)grvChung.GetFocusedRow()).Row;
                            this.DialogResult = DialogResult.OK;
                        }
                        catch { }

                        this.DialogResult = DialogResult.OK;
                        break;
                    }
                case "thoat":
                    {
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                        break;
                    }
                default: break;
            }
        }
    }
}