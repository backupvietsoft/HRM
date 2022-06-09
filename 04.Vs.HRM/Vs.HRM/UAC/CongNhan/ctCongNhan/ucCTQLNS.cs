
using DevExpress.Skins;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Vs.HRM
{
    public partial class ucCTQLNS : DevExpress.XtraEditors.XtraUserControl
    {
        List<LabelControl> List;
        private string tab = "";
        public DataTable dt;

        public ucCTQLNS(Int64 iIdCN)
        {
            InitializeComponent();

            navigationFrame1.AllowTransitionAnimation = DevExpress.Utils.DefaultBoolean.True;
            navigationFrame1.TransitionAnimationProperties.FrameCount = 0;
            navigationFrame1.TransitionAnimationProperties.FrameInterval = 0;

            Commons.Modules.ObjSystems.ThayDoiNN(this);
            Commons.Modules.iCongNhan = iIdCN;
        }
        private void ucCTQLNS_Load(object sender, EventArgs e)
        {
            XuLyTab();
            Lb_Click(labLyLich, null);
        }
        private void XuLyTab()
        {
            List = new List<LabelControl>() { labLyLich, labCongTac, labHopDong, labTienLuong, labKhanThuong, labTaiNan, labDanhGia, labBangCap };
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
                if (Commons.Modules.iCongNhan == 0 && lable.Name != "labLyLich") return;
                if (tab == lable.Name) return;
                foreach (LabelControl lc in List)
                {
                    if (lable.Name == lc.Name)
                    {
                        lc.Appearance.ForeColor = Color.FromArgb(240, 128, 25);   //CommonColors.GetQuestionColor(DevExpress.LookAndFeel.UserLookAndFeel.Default);
                        LoaduacCongNhan(lc.Name);
                        tab = lable.Name;
                        //return;
                    }
                    if (lable.Name != lc.Name)
                    {
                        lc.Appearance.ForeColor = Color.Empty;
                        //lc.Appearance.Font = this.Font;
                    }
                }
            }
            catch
            {
            }
        }
        private void LoaduacCongNhan(string tenlable)
        {
            switch (tenlable)
            {
                case "labLyLich":
                    {
                        if (navigationPage1.Controls.Count == 0)
                        {
                            ucLyLich ll = new ucLyLich(Commons.Modules.iCongNhan);
                            ll.Dock = DockStyle.Fill;
                            navigationPage1.Controls.Add(ll);
                        }
                        navigationFrame1.SelectedPage = navigationPage1;
                        break;
                    }
                case "labCongTac":
                    {
                        if (navigationPage2.Controls.Count == 0)
                        {
                            ucQTCongTac ct = new ucQTCongTac(Commons.Modules.iCongNhan);
                            ct.Dock = DockStyle.Fill;
                            navigationPage2.Controls.Add(ct);
                        }
                        navigationFrame1.SelectedPage = navigationPage2;
                        break;
                    }
                case "labTienLuong":
                    {
                        if (navigationPage3.Controls.Count == 0)
                        {
                            ucTienLuong tl = new ucTienLuong(Commons.Modules.iCongNhan);
                            tl.Dock = DockStyle.Fill;
                            navigationPage3.Controls.Add(tl);
                        }
                        navigationFrame1.SelectedPage = navigationPage3;
                        break;
                    }
                case "labHopDong":
                    {
                        if (navigationPage4.Controls.Count == 0)
                        {
                            ucHopDong hd = new ucHopDong(Commons.Modules.iCongNhan);
                            hd.Dock = DockStyle.Fill;
                            navigationPage4.Controls.Add(hd);
                        }
                        navigationFrame1.SelectedPage = navigationPage4;
                        break;
                    }
               
                case "labKhanThuong":
                    {
                        if (navigationPage5.Controls.Count == 0)
                        {
                            ucKhenThuong kt = new ucKhenThuong(Commons.Modules.iCongNhan);
                            kt.Dock = DockStyle.Fill;
                            navigationPage5.Controls.Add(kt);
                        }
                        navigationFrame1.SelectedPage = navigationPage5;
                        break;
                    }
                case "labTaiNan":
                    {
                        if (navigationPage6.Controls.Count == 0)
                        {
                            ucTaiNanLD tn = new ucTaiNanLD(Commons.Modules.iCongNhan);
                            tn.Dock = DockStyle.Fill;
                            navigationPage6.Controls.Add(tn);
                        }
                        navigationFrame1.SelectedPage = navigationPage6;

                        break;
                    }
                case "labDanhGia":
                    {
                        if (navigationPage7.Controls.Count == 0)
                        {
                            ucDanhGia dg = new ucDanhGia(Commons.Modules.iCongNhan);
                            dg.Dock = DockStyle.Fill;
                            navigationPage7.Controls.Add(dg);
                        }
                        navigationFrame1.SelectedPage = navigationPage7;
                        break;
                    }
                case "labBangCap":
                    {
                        if (navigationPage8.Controls.Count == 0)
                        {
                            ucBangCap ll = new ucBangCap(Commons.Modules.iCongNhan);
                            ll.Dock = DockStyle.Fill;
                            navigationPage8.Controls.Add(ll);
                        }
                        navigationFrame1.SelectedPage = navigationPage8;
                        break;
                    }
                default:
                    break;
            }
        }
        private void LoadUac(XtraUserControl uac, NavigationPage page)
        {
 
        }
        private NavigationPage checkfameexits(string tab)
        {
            try
            {
                NavigationPage page = new NavigationPage();
                foreach (NavigationPage item in navigationFrame1.Pages)
                {
                    if (item.Tag.ToString() == tab)
                    {
                        page = item;
                        return page;
                    }
                    else
                    {
                        return null;
                    }
                }
                return page;
            }
            catch
            {
                return null;
            }
        }

    }
}
