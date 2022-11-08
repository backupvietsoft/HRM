using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Grid;

namespace Commons
{
    public partial class frmYKienYC : DevExpress.XtraEditors.XtraForm
    {
        public frmYKienYC()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this,Root,windowsUIButton);
        }

        private void frmYKienYC_Load(object sender, EventArgs e)
        {
           
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "thuchien":
                    {
                        DialogResult = DialogResult.OK;
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

    }
}