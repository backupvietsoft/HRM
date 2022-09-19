
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Vs.Recruit
{
    public partial class ucCTQLUV : DevExpress.XtraEditors.XtraUserControl
    {
        List<LabelControl> List;
        private string tab = "";
        public Int64 iIDTB = -1;
        public ucCTQLUV(Int64 iIdUV)
        {
            InitializeComponent();
       
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            Commons.Modules.iUngVien = iIdUV;
        }
        private void ucCTQLUV_Load(object sender, EventArgs e)
        {
            XuLyTab();
            Lb_Click(labLyLich, null);
        } 
        private void XuLyTab()
        {
            List = new List<LabelControl>() { labLyLich,  labLSTuyenDung};
            foreach (LabelControl lb in List)
            {
                lb.Click += Lb_Click;
            }
        }
        private void Lb_Click(object sender, EventArgs e)
        {
            try
            {
                var lable = sender as LabelControl;
                if (Commons.Modules.iUngVien == 0 && lable.Name != "labLyLich") return;
                if (tab == lable.Name) return;
                foreach (LabelControl lc in List)
                {
                    if (lable.Name == lc.Name)
                    {
                        lc.Appearance.ForeColor = Color.FromArgb(0, 0, 192);
                        LoaduacCongNhan(lc.Name);
                        tab = lable.Name;
                    }
                    if(lable.Name!=lc.Name)
                    {
                        lc.Appearance.ForeColor = Color.Empty;
                    }
                }
                Commons.Modules.ObjSystems.HideWaitForm();
            }
            catch
            {
            }
        }
        private void Selecttab(NavigationPage page)
        {

            Thread thread = new Thread(delegate ()
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        navigationFrame1.SelectedPage = page;
                    }));
                }
            }, 100); thread.Start();
        }
        private ucLyLichUV ll;
        private void LoaduacCongNhan(string tenlable)
        {
            switch (tenlable)
            {
                case "labLyLich":
                    {
                        if (pageLylich.Controls.Count == 0)
                        {
                            ll = new ucLyLichUV(Commons.Modules.iUngVien);
                            ll.Dock = DockStyle.Fill;
                            pageLylich.Controls.Add(ll);
                        }
                        else
                        {
                            ll.BinDingData(false);
                        }
                        Selecttab(pageLylich);
                        break;
                    }

                case "labLSTuyenDung":
                    {
                        if (pageLSTD.Controls.Count == 0)
                        {
                            ucTuyenDung ct = new ucTuyenDung(Commons.Modules.iUngVien);
                            ct.Dock = DockStyle.Fill;
                            pageLSTD.Controls.Add(ct);
                        }
                        Selecttab(pageLSTD);
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
