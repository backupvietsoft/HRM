using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using VietSoftHRM.Properties;

namespace VietSoftHRM
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        public frmMain()
        {
            InitializeComponent();
        }
        //load menugroup
        private void frmMain_Load(object sender, EventArgs e)
        {
            timer1.Start();
            SetThongTinChung();
            UpdateTinhTrangNghiPhep(-1);
            Commons.Modules.ObjSystems.ShowWaitForm(this);
            LoadMenuCha();
            Commons.Modules.ObjSystems.ThayDoiNN(this);

            LoadNNMenu();
            btnUserName.Text = Commons.Modules.UserName;
            //radialMenu1.AddItems(GetRadialMenuItems(barManager1));
            //Load Biểu đồ
            //loadcharTinhTrangCN();
            Commons.Modules.ObjSystems.HideWaitForm();
            barServer.Caption = "Server : " + Commons.IConnections.Server + "- Database : " + Commons.IConnections.Database;
            barTTC.Caption = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "select TEN_CTY from THONG_TIN_CHUNG").ToString().ToUpper();
            barVer.Caption = "Version Curent: " + Commons.Modules.sInfoSer + "";
            barLogin.Caption = "Total " + SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, " SELECT COUNT(*) FROM dbo.LOGIN ").ToString() + "/" + Commons.Modules.iLic + " user login";
        }

        private void SetThongTinChung()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(LOAI_LINK,1) AS LINK,CON_NECT,HIDE_MENU FROM dbo.THONG_TIN_CHUNG"));
                Commons.Modules.iLink = Convert.ToInt32(dt.Rows[0]["LINK"]);
                //Commons.Modules.connect = dt.Rows[0]["CON_NECT"].ToString();
                Commons.Modules.sHideMenu = Commons.Modules.ObjSystems.Decrypt(dt.Rows[0]["HIDE_MENU"].ToString(), true);
            }
            catch
            { }
        }
        //private void loadcharTinhTrangCN()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetChartUserTinhTrangHT", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
        //    pieChart.Series.Clear();
        //    pieChart.Titles.Clear();

        //    pieChart.Titles.Add(new ChartTitle() { Text = Commons.Modules.TypeLanguage==1?"EMPLOYEE STATUS CHART":"Biểu Đồ Tình Trạng Hiện Tại Công Nhân" });
        //    // Create a pie series.
        //    Series series1 = new Series("charpie", ViewType.Pie);
        //    // Bind the series to data.
        //    series1.DataSource = dt;
        //    series1.ArgumentDataMember = "TEN_TT_HT";
        //    series1.ValueDataMembers.AddRange(new string[] { "SL_CN" });


        //    // Format the the series labels.
        //    series1.Label.TextPattern = "{VP:p0} ({V:0})";
        //    // Format the series legend items.
        //    series1.LegendTextPattern = "{A}";



        //    // Adjust the position of series labels. 
        //    ((PieSeriesLabel)series1.Label).Position = PieSeriesLabelPosition.TwoColumns;

        //    // Detect overlapping of series labels.
        //    ((PieSeriesLabel)series1.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;

        //    // Access the view-type-specific options of the series.
        //    PieSeriesView myView = (PieSeriesView)series1.View;

        //    // Specify a data filter to explode points.
        //    myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Value_1,
        //        DataFilterCondition.GreaterThanOrEqual, 9));
        //    myView.ExplodedPointsFilters.Add(new SeriesPointFilter(SeriesPointKey.Argument,
        //        DataFilterCondition.NotEqual, "Làm việc"));
        //    myView.ExplodeMode = PieExplodeMode.UseFilters;
        //    myView.ExplodedDistancePercentage = 30;
        //    myView.RuntimeExploding = true;

        //    // Customize the legend.
        //    pieChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

        //    // Add the series to the chart.
        //    pieChart.Series.Add(series1);
        //}
        private void LoadMenuCha()
        {

            LoadTitleBar(titlegroup);
            tileBar.ItemSize = 30;
            tileBar.WideTileWidth = 150;
            tileBar.SelectedItem = titlegroup.GetTileItemByName("58");
        }
        private void LoadTitleBar(TileBarGroup group)
        {

            DataTable dt = new DataTable();
            try
            {
                String sSql;
                sSql = "SELECT DISTINCT T3.[ID_MENU],[KEY_MENU],CASE " + Commons.Modules.TypeLanguage.ToString() + " WHEN 0 THEN T3.[TEN_MENU] WHEN 1 THEN ISNULL(NULLIF(T3.[TEN_MENU_A],''),T3.[TEN_MENU]) ELSE ISNULL(NULLIF(T3.[TEN_MENU_H],''),T3.[TEN_MENU]) END AS NAME,[ROOT],[HIDE],[BACK_COLOR],[IMG],[STT_MENU],[CONTROLS],[DROPDOW] FROM NHOM_MENU T1 INNER JOIN dbo.USERS T2 ON T1.ID_NHOM = T2.ID_NHOM INNER JOIN dbo.MENU T3 ON T1.ID_MENU = T3.ID_MENU  WHERE T2.USER_NAME = '" + Commons.Modules.UserName.Trim() + "' AND [ROOT] = 0 AND T3.ID_MENU NOT IN (" + Commons.Modules.sHideMenu + ") ORDER BY[STT_MENU],[NAME]";
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                foreach (DataRow item in dt.Rows)
                {
                    TileBarItem itembar = new TileBarItem();
                    itembar.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopLeft;
                    itembar.ItemSize = TileBarItemSize.Wide;
                    itembar.Text = item["NAME"].ToString();
                    itembar.AppearanceItem.Normal.BackColor = System.Drawing.ColorTranslator.FromHtml(item["BACK_COLOR"].ToString());
                    itembar.AppearanceItem.Normal.FontStyleDelta = FontStyle.Bold;
                    //itembar.Image = (Image)Properties.Resources.ResourceManager.GetObject(item["IMG"].ToString());
                    itembar.TextAlignment = TileItemContentAlignment.MiddleCenter;
                    itembar.Tag = item["ID_MENU"].ToString();
                    itembar.Name = item["ID_MENU"].ToString();
                    titlegroup.Items.Add(itembar);
                }
            }

            catch (Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());
            }
        }
        private void tileBar_SelectedItemChanged(object sender, TileItemEventArgs e)
        {

            switch (Convert.ToInt32(e.Item.Tag))
            {
                case 58:
                    {
                        navigationFrame.SelectedPage = navigationPageHome;
                        //loadcharTinhTrangCN();
                        break;
                    }
                case 1:
                    {
                        LoaducHeThong(e.Item);
                        break;
                    }
                case 2:
                    {
                        LoaducDanhMuc(e.Item);
                        break;
                    }
                case 159:
                    {
                        LoaducUngVien(e.Item);
                        break;
                    }
                case 14:
                    {
                        LoaducCongNhan(e.Item);
                        break;
                    }
                case 61:
                    {
                        LoaducBaoCao(e.Item);
                        break;
                    }

                case 15:
                    {
                        LoaducChamCong(e.Item);
                        break;
                    }
                case 16:
                    {
                        LoaducLuong(e.Item);
                        break;
                    }
                default:
                    break;
            }
        }
        private void ResetMenu(TileItem e)
        {
            DataTable dt = new DataTable();
            String sSql;
            sSql = "SELECT T3.[ID_MENU],[KEY_MENU],CASE " + Commons.Modules.TypeLanguage.ToString() + " WHEN 0 THEN T3.[TEN_MENU] WHEN 1 THEN ISNULL(NULLIF(T3.[TEN_MENU_A],''),T3.[TEN_MENU]) ELSE ISNULL(NULLIF(T3.[TEN_MENU_H],''),T3.[TEN_MENU]) END AS NAME,[ROOT],[HIDE],[BACK_COLOR],[IMG],[STT_MENU],[CONTROLS],[DROPDOW] FROM NHOM_MENU T1 INNER JOIN dbo.USERS T2 ON T1.ID_NHOM = T2.ID_NHOM INNER JOIN dbo.MENU T3 ON T1.ID_MENU = T3.ID_MENU  WHERE T2.USER_NAME = '" + Commons.Modules.UserName + "' AND [ROOT] = 0 AND T3.ID_MENU NOT IN (" + Commons.Modules.sHideMenu + ") ORDER BY[STT_MENU],[NAME]";
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
            foreach (TileBarItem item in titlegroup.Items)
            {
                try
                {
                    DataTable row = dt.AsEnumerable().Where(x => x["ID_MENU"].ToString().Equals(item.Name)).CopyToDataTable();
                    item.Text = row.Rows[0]["NAME"].ToString();
                }
                catch
                {
                }
            }

            try
            {
                foreach (NavigationPage item in navigationFrame.Pages)
                {
                    if (item.Name != "navigationPageHome")
                    {
                        navigationFrame.Pages.Remove(item);
                    }
                }
            }
            catch
            {
            }
            switch (Convert.ToInt32(e.Tag))
            {
                case 58:
                    navigationFrame.SelectedPage = navigationPageHome;
                    break;
                case 1:
                    LoaducHeThong(e);
                    break;
                case 2:
                    LoaducDanhMuc(e);
                    break;
                case 159:
                    LoaducUngVien(e);
                    break;
                case 14:
                    LoaducCongNhan(e);
                    break;
                case 61:
                    LoaducBaoCao(e);
                    break;
                case 15:
                    LoaducChamCong(e);
                    break;
                case 16:
                    LoaducLuong(e);
                    break;
                default:
                    break;

            }
        }



        private void LoaducHeThong(TileItem e)
        {
            ucSystems ucsymstem = new ucSystems();
            ucsymstem.Dock = DockStyle.Fill;
            ucsymstem.iLoai = Convert.ToInt32(e.Tag);
            ucsymstem.NONNlab_Link.Text = e.Text;
            ucsymstem.color = e.AppearanceItem.Normal.BackColor;
            LoadUac(ucsymstem);
        }
        private void LoaducUngVien(TileItem e)
        {
            ucUngVien uacTD = new ucUngVien(tileBar);
            uacTD.Dock = DockStyle.Fill;
            uacTD.iLoai = Convert.ToInt32(e.Tag);
            uacTD.NONNlab_Link.Text = e.Text;
            LoadUac(uacTD);
        }
        private void LoaducCongNhan(TileItem e)
        {
            ucCongNhan uacCN = new ucCongNhan(tileBar);
            uacCN.Dock = DockStyle.Fill;
            uacCN.iLoai = Convert.ToInt32(e.Tag);
            uacCN.NONNlab_Link.Text = e.Text;
            LoadUac(uacCN);
        }
        private void LoaducDanhMuc(TileItem e)
        {
            ucListDMuc uacDM = new ucListDMuc();
            uacDM.Dock = DockStyle.Fill;
            uacDM.iLoai = Convert.ToInt32(e.Tag);
            uacDM.NONNlab_Link.Text = e.Text;
            //uacDM.color = e.Item.AppearanceItem.Normal.BackColor;
            LoadUac(uacDM);
        }
        private void LoaducBaoCao(TileItem e)
        {
            ucListBaoCao ucListBC = new ucListBaoCao(tileBar);
            ucListBC.Dock = DockStyle.Fill;
            ucListBC.iLoai = Convert.ToInt32(e.Tag);
            ucListBC.NONNlab_Link.Text = e.Text;
            ucListBC.color = e.AppearanceItem.Normal.BackColor;
            LoadUac(ucListBC);
        }
        private void LoaducChamCong(TileItem e)
        {
            ucListChamCong ucListCC = new ucListChamCong(tileBar);
            ucListCC.Dock = DockStyle.Fill;
            ucListCC.iLoai = Convert.ToInt32(e.Tag);
            ucListCC.NONNlab_Link.Text = e.Text;
            ucListCC.color = e.AppearanceItem.Normal.BackColor;
            LoadUac(ucListCC);
        }
        private void LoaducLuong(TileItem e)
        {
            ucListLuong ucListL = new ucListLuong(tileBar);
            ucListL.Dock = DockStyle.Fill;
            ucListL.iLoai = Convert.ToInt32(e.Tag);
            ucListL.NONNlab_Link.Text = e.Text;
            ucListL.color = e.AppearanceItem.Normal.BackColor;
            LoadUac(ucListL);
        }
        private void LoadUac(XtraUserControl uac)
        {
            //kiểm tra tồn tại chưa nếu tồn tại rồi thì select page ngược lại thì load

            if (checkfameexits(uac.Name).Tag != null && Commons.Modules.ChangLanguage == false)
            {
                navigationFrame.SelectedPage = checkfameexits(uac.Name);
            }
            else
            {
                NavigationPage page = new NavigationPage();
                page.Tag = uac.Name;
                page.Controls.Add(uac);
                navigationFrame.Pages.Add(page);
                navigationFrame.SelectedPage = page;
            }
        }
        private NavigationPage checkfameexits(string tab)
        {
            NavigationPage page = new NavigationPage();
            foreach (NavigationPage item in navigationFrame.Pages)
            {
                if (item.Tag == tab)
                {
                    page = item;
                }

            }
            return page;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void LoadNN(int NNgu)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (Commons.Modules.TypeLanguage == NNgu)
            {
                LoadNNMenu();
                return;
            }
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\savelogin.xml");
                ds.Tables[0].Rows[0]["N"] = NNgu.ToString();
                ds.WriteXml(AppDomain.CurrentDomain.BaseDirectory + "\\savelogin.xml");
                Commons.Modules.TypeLanguage = NNgu;
                Commons.Modules.ChangLanguage = true;
                ResetMenu(tileBar.SelectedItem);
                Commons.Modules.ObjSystems.ThayDoiNN(this);
                LoadNNMenu();
            }
            catch { }
        }

        private void LoadNNMenu()
        {
            Commons.Modules.sLoad = "0Load";
            switch (Commons.Modules.TypeLanguage)
            {
                case 0:
                    {
                        barVietnam.Caption = "Việt nam";
                        barEnglish.Caption = "Tiếng anh";
                        barExits.Caption = "Thoát";
                        barlanguage.Caption = "Ngôn ngữ";
                        barLogout.Caption = "Đăng xuất";
                        barVietnam.Checked = true;
                        barEnglish.Checked = false;
                        this.barlanguage.ImageOptions.Image = Properties.Resources.vietnamflag;
                        break;
                    }
                case 1:
                    {
                        barVietnam.Caption = "Vietnamese";
                        barEnglish.Caption = "English";
                        barlanguage.Caption = "Language";
                        barExits.Caption = "Exits";
                        barLogout.Caption = "Logout";
                        barVietnam.Checked = false;
                        barEnglish.Checked = true;
                        this.barlanguage.ImageOptions.Image = Properties.Resources.usflag;
                        break;
                    }
                default: break;
            }
            Commons.Modules.sLoad = "";
        }

        public Control FindFocusedControl(Control control)
        {
            var container = control as IContainerControl;
            while (container != null)
            {
                control = container.ActiveControl;
                container = control as IContainerControl;
            }
            return control;
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            switch (keyData)
            {
                //case Keys.Escape:
                //    Control cl = FindFocusedControl(this);
                //    if (cl != null && cl.GetType() == typeof(TextBox))
                //        return base.ProcessCmdKey(ref msg, keyData);
                //    else
                //        if (this.Name != "frmMain")
                //        this.Close();
                //    return true;
                case (Keys.Shift | Keys.F1):
                    Vs.HRM.frmHelp_View fr = new Vs.HRM.frmHelp_View();
                    fr.ShowDiaLogControls(this);
                    return true;
                default: return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void barLogout_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Hide();
            Commons.Modules.ObjSystems.User(Commons.Modules.UserName, 2);
            timer1.Stop();
            frmLogin login = new frmLogin();
            login.ShowDialog();
            this.Close();
        }

        private void barExits_ItemClick(object sender, ItemClickEventArgs e)
        {
            Commons.Modules.ObjSystems.User(Commons.Modules.UserName, 2);
            Application.Exit();
        }



        private void barEnglish_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            LoadNN(1);
        }

        private void barVietnam_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            LoadNN(0);
        }





        private void barSkin_DownChanged(object sender, ItemClickEventArgs e)
        {

        }

        private void popupMenu1_CloseUp(object sender, EventArgs e)
        {
            if (Settings.Default["ApplicationSkinName"].ToString() == UserLookAndFeel.Default.SkinName) return;
            Settings.Default["ApplicationSkinName"] = UserLookAndFeel.Default.SkinName;
            Settings.Default.Save();
        }

        private void UpdateTinhTrangNghiPhep(int ID_CN)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spCapNhatTinhTrangNghiPhep", DateTime.Now, ID_CN);
            }
            catch
            {
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {

                //cập nhật số ngường dùng
                barLogin.Caption = "Total " + SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, " SELECT COUNT(*) FROM dbo.LOGIN ").ToString() + "/" + Commons.Modules.iLic + " user login";
                //cập nhật thời gian login
                if (Commons.Modules.ObjSystems.checkExitsUserLG(Commons.Modules.UserName))
                {
                    Thread thread = new Thread(delegate ()
                    {
                        timer1.Stop();
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgPhanMemTuDongThoatsau5p"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Thread.Sleep(300000);//chi nghỉ 5 phút
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                this.Hide();
                                Commons.Modules.ObjSystems.User(Commons.Modules.UserName, 2);
                                timer1.Stop();
                                frmLogin login = new frmLogin();
                                login.ShowDialog();
                                this.Close();
                            }));
                        }
                    }, Convert.ToInt32(TimeSpan.FromMinutes(5).TotalMilliseconds));
                    thread.Start();
                }
                else
                {
                    Commons.Modules.ObjSystems.User(Commons.Modules.UserName, 1);
                }    
            }
            catch { }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Commons.Modules.ObjSystems.User(Commons.Modules.UserName, 2);
        }
    }
}
