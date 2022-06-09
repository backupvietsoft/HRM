using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;


namespace VietSoftHRM
{
    public partial class fdfMain : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public fdfMain()
        {
            InitializeComponent();
        }

        private void fdfMain_Load(object sender, EventArgs e)
        {
            //splashScreenManager1.ShowWaitForm();
            Thread.Sleep(500);
            LoadMenuCha();
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            //splashScreenManager1.CloseWaitForm();
        }

        private void LoadMenuCha()
        {
            TileBarGroup group1 = new TileBarGroup();
            tileBar.Groups.Add(group1);
            group1.Text = "OPERATIONS";
            LoadTitleBar(group1);
            tileBar.ItemSize = 50;
            tileBar.WideTileWidth = 150;
        }
        private void LoadTitleBar(TileBarGroup group)
        {
            DataTable dt = new DataTable();
            try
            {
                String sSql;
                sSql = "SELECT T3.[ID_MENU],[KEY_MENU],CASE " + Commons.Modules.TypeLanguage.ToString() + " WHEN 0 THEN T3.[TEN_MENU] WHEN 1 THEN ISNULL(NULLIF(T3.[TEN_MENU_A],''),T3.[TEN_MENU]) ELSE ISNULL(NULLIF(T3.[TEN_MENU_H],''),T3.[TEN_MENU]) END AS NAME,[ROOT],[HIDE],[BACK_COLOR],[IMG],[STT_MENU],[CONTROLS],[DROPDOW] FROM NHOM_MENU T1 INNER JOIN dbo.USERS T2 ON T1.ID_NHOM = T2.ID_NHOM INNER JOIN dbo.MENU T3 ON T1.ID_MENU = T3.ID_MENU  WHERE T2.USER_NAME = '" + Commons.Modules.UserName + "' AND [ROOT] = 0 ORDER BY[STT_MENU],[NAME]";
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                foreach (DataRow item in dt.Rows)
                {
                    TileBarItem itembar = new TileBarItem();
                    itembar.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopLeft;
                    itembar.ItemSize = TileBarItemSize.Wide;
                    itembar.Text = item["NAME"].ToString();
                    itembar.AppearanceItem.Normal.BackColor = System.Drawing.ColorTranslator.FromHtml(item["BACK_COLOR"].ToString());
                    itembar.Image = (Image)Properties.Resources.ResourceManager.GetObject(item["IMG"].ToString());
                    itembar.TextAlignment = TileItemContentAlignment.MiddleRight;
                    itembar.Tag = item["ID_MENU"].ToString();
                    group.Items.Add(itembar);
                }
            }
            catch (Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());
            }
        }
        private void tileBar_SelectedItemChanged(object sender, TileItemEventArgs e)
        {
            if (Convert.ToInt32(e.Item.Tag) == 2)
            {

            }
            switch (Convert.ToInt32(e.Item.Tag))
            {
                case 1:
                    {
                        LoaducHeThong(e);
                        break;
                    }
                case 2:
                    {
                        LoaducDanhMuc(e);
                        break;
                    }
                case 14:
                    {
                        LoaducCongNhan(e);
                        break;
                    }
                case 16:
                    {
                        navigationFrame.SelectedPage = navigationPage1;
                        break;
                    }
                default:
                    break;
            }
        }

        private void LoaducHeThong(TileItemEventArgs e)
        {
            ucSystems ucsymstem = new ucSystems();
            ucsymstem.Dock = DockStyle.Fill;
            ucsymstem.iLoai = Convert.ToInt32(e.Item.Tag);
            ucsymstem.lab_Link.Text = e.Item.Text;
            ucsymstem.color = e.Item.AppearanceItem.Normal.BackColor;
            LoadUac(ucsymstem);
        }

        private void LoaducCongNhan(TileItemEventArgs e)
        {
            ucCongNhan uacCN = new ucCongNhan();
            uacCN.Dock = DockStyle.Fill;
            uacCN.iLoai = Convert.ToInt32(e.Item.Tag);
            uacCN.lab_Link.Text = e.Item.Text;
            LoadUac(uacCN);
        }
        private void LoaducDanhMuc(TileItemEventArgs e)
        {

            ucListDMuc uacDM = new ucListDMuc();
            uacDM.Dock = DockStyle.Fill;
            uacDM.iLoai = Convert.ToInt32(e.Item.Tag);
            uacDM.lab_Link.Text = e.Item.Text;
            //uacDM.color = e.Item.AppearanceItem.Normal.BackColor;
            LoadUac(uacDM);
        }

        private void LoadUac(XtraUserControl uac)
        {
            NavigationPage page = new NavigationPage();
            page.Controls.Add(uac);
            navigationFrame.Pages.Add(page);
            navigationFrame.SelectedPage = page;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void navigationPage1_Paint(object sender, PaintEventArgs e)
        {

        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    Control cl = FindFocusedControl(this);
                    if (cl != null && cl.GetType() == typeof(TextBox))
                        return base.ProcessCmdKey(ref msg, keyData);
                    else
                        if (this.Name != "frmMain")
                        this.Close();
                    return true;
                case (Keys.Shift | Keys.F1):
                    Vs.HRM.frmHelp_View fr = new Vs.HRM.frmHelp_View();
                    fr.ShowDiaLogControls(this);
                    return true;
                default: return base.ProcessCmdKey(ref msg, keyData);
            }
        }
    }
}
