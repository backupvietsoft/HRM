using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;
using System.Drawing;
using System.Windows.Forms;

namespace VietSoftHRM.Class
{
    public class CustomFlyoutDialog:FlyoutDialog
    {
        public CustomFlyoutDialog(Form owner, FlyoutAction actions, XtraUserControl UserControleToShow)
          : base(owner, actions)
        {
            this.Properties.HeaderOffset = 0;

            //UserControleToShow.Width = owner.Width;
            //UserControleToShow.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, UserControleToShow.Height);

            double iW, iH;
            iW = Screen.PrimaryScreen.WorkingArea.Width / 2;
            iH = Screen.PrimaryScreen.WorkingArea.Height / 2;
            if (iW < 800)
            {
                iW = iW * 1.2;
                iH = iH * 1.2;
            }
            UserControleToShow.Size = new Size((int)iW, (int)iH);

            this.Properties.Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.Properties.Style = FlyoutStyle.Popup;
            this.FlyoutControl = UserControleToShow;
            
        }
        //public static DialogResult ShowForm(Form owner, FlyoutAction actions, Control UserControlToShow)        
        public static DialogResult ShowForm(Form owner, FlyoutAction actions, XtraUserControl UserControlToShow)
        {
            CustomFlyoutDialog customFlyout = new CustomFlyoutDialog(owner, actions, UserControlToShow);
            return customFlyout.ShowDialog();
        }
    }
}
