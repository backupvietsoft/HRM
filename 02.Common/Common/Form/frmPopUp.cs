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
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Grid;

namespace Commons
{
    public partial class frmPopUp : DevExpress.XtraEditors.XtraForm
    {
        private DataTable _tbsource = new DataTable();
        public DataTable TableSource
        {
            set
            {
                _tbsource = value;
            }
        }
        // Dữ liệu được chọn
        private DataRow _dtrow;
        public DataRow RowSelected
        {
            get
            {
                return _dtrow;
            }
        }
        public frmPopUp()
        {
            InitializeComponent();
        }

        private void frmPopUp_Load(object sender, EventArgs e)
        {
            if (_tbsource.Columns.Count < 6)
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdSource, grvSource, _tbsource, false, true, true, true,true,this.Name);
            else
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdSource, grvSource, _tbsource, false, true, false,true, true, this.Name);
            grvSource.Columns[0].Visible = false;
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "thuchien":
                    {
                        try
                        {
                            _dtrow = ((DataRowView)grvSource.GetFocusedRow()).Row;
                            this.DialogResult = DialogResult.OK;
                        }
                        catch { }
                        this.Close();
                        break;
                    }
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
                default:
                    break;
            }
        }

        private void grvSource_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _dtrow = ((DataRowView)grvSource.GetFocusedRow()).Row;
                this.DialogResult = DialogResult.OK;
            }
            catch { }

            this.Close();

        }
        GridView viewChung;
        Point ptChung;

        private void grvSource_ShowingEditor(object sender, CancelEventArgs e)
        {
            viewChung = (GridView)sender;
            ptChung = viewChung.GridControl.PointToClient(Control.MousePosition);
            viewChung.ActiveEditor.DoubleClick += new EventHandler(ActiveEditor_DoubleClick);
        }
        private void ActiveEditor_DoubleClick(object sender, System.EventArgs e)
        {
            DoRowDoubleClick(viewChung, ptChung);
            grvSource.RefreshData();
        }
        private void DoRowDoubleClick(GridView view, Point pt)
        {
        }
    }
}