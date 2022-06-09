
using DevExpress.Skins;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Vs.Recruit
{
    public partial class ucCTQLUV : DevExpress.XtraEditors.XtraUserControl
    {
        List<LabelControl> List;
        private string tab = "";
        public DataTable dt;
        public bool flag = false;
        public bool flag_Open = false; // flag = true uc mở bằng double click từ các form khác ,  flag = false ứng viên được mở từ danh sách ứng viên


        public Int64 iIDTB = -1;
        //public Int64 iID_UV = -1;
        public ucCTQLUV(Int64 iIdUV)
        {
            InitializeComponent();

            navigationFrame1.AllowTransitionAnimation = DevExpress.Utils.DefaultBoolean.True;
            navigationFrame1.TransitionAnimationProperties.FrameCount = 0;
            navigationFrame1.TransitionAnimationProperties.FrameInterval = 0;

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
                        lc.Appearance.ForeColor = Color.FromArgb(240, 128, 25);   //CommonColors.GetQuestionColor(DevExpress.LookAndFeel.UserLookAndFeel.Default);
                        LoaduacCongNhan(lc.Name);
                        tab = lable.Name;
                        //return;
                    }
                    if(lable.Name!=lc.Name)
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
                        ucLyLichUV ll = new ucLyLichUV(Commons.Modules.iUngVien);
                        dt = ll.dt;
                        ll.flag_Open = flag_Open;
                        ll.iIDTB = iIDTB;
                        LoadUac(ll);
                        ll.back = navigationFrame1;
                        break;
                    }
                //case "labBangCap":
                //    {
                //        ucBangCapUV bc = new ucBangCapUV(Commons.Modules.iUngVien);
                //        LoadUac(bc);
                //        break;
                //    }
                case "labLSTuyenDung":
                    {
                        ucTuyenDung td = new ucTuyenDung(Commons.Modules.iUngVien);
                        LoadUac(td);
                        break;
                    }
                default:
                    break;
            }
        }
        private void LoadUac(XtraUserControl uac)
        {
            uac.Dock = DockStyle.Fill;
            NavigationPage page = new NavigationPage();
            page.Tag = uac.Name;
            page.Controls.Add(uac);
            navigationFrame1.Pages.Add(page);
            navigationFrame1.Dock = DockStyle.Fill;
            navigationFrame1.SelectedPageIndex = navigationFrame1.Pages.Count;
        }
        private NavigationPage checkfameexits(string tab)
        {
            NavigationPage page = new NavigationPage();
            foreach (NavigationPage item in navigationFrame1.Pages)
            {
                if (item.Tag == tab)
                {
                    page = item;
                }

            }
            return page;
        }

    }
}
