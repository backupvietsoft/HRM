using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using Microsoft.ApplicationBlocks.Data;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucBlank : DevExpress.XtraEditors.XtraUserControl
    {
        public ucBlank()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            MessageBox.Show("Báo cáo chưa được cấu hình! [ucListBaoCao]");
        }
        
        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "Print":
                    {
                        MessageBox.Show("Báo cáo chưa được cấu hình! [ucListBaoCao]");
                        break;
                    }
                default:
                    break;
            }
        }

    }
}
