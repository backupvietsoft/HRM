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

namespace Vs.HRM
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm1()
        {
            InitializeComponent();
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {

            Commons.Modules.iCongNhan = 161;
            ucThuongKhacLuong ns = new ucThuongKhacLuong();
            //ucDaoTao ns = new ucDaoTao();
            //ucDanhGia ns = new ucDanhGia(161);
            //ucHopDong ns = new ucHopDong(161);
            //ucLyLich ns = new ucLyLich(161);
            //ucTaiNanLD ns = new ucTaiNanLD(24);
            //ucBHXHThang ns = new ucBHXHThang();
            this.Controls.Clear();
            this.Controls.Add(ns);
            ns.Dock = DockStyle.Fill;
        }
    }
}