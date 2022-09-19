using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vs.Recruit
{
    public partial class frmUngVien : DevExpress.XtraEditors.XtraForm
    {
        public frmUngVien()
        {
            InitializeComponent();
            this.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name,this.Name);
        }
        private void frmUngVien_Load(object sender, EventArgs e)
        {
            ucLyLichUV uc = new ucLyLichUV(Commons.Modules.iUngVien);
            this.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
        }
    }
}