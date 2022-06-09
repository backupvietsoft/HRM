using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;

namespace Commons
{
    public class OXtraGrid
    {
        public static string iId;
        private string ID
        {
            get
            {
                return ID;
            }
            set
            {
                ID = value;
            }
        }
        DevExpress.XtraGrid.GridControl grd_DonVi = new DevExpress.XtraGrid.GridControl();
        public void CreateMenuReset(DevExpress.XtraGrid.GridControl grd)
        {
            grd_DonVi = grd;
            DevExpress.XtraGrid.Views.Grid.GridView grv = grd.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            grv.ShowGridMenu += grv_ShowGridMenu;

        }
        private void grv_ShowGridMenu(object sender, GridMenuEventArgs e)
        {
            if (e.MenuType != DevExpress.XtraGrid.Views.Grid.GridMenuType.Column)
                return;
            try
            {
                DevExpress.XtraGrid.Menu.GridViewMenu headerMenu = (DevExpress.XtraGrid.Menu.GridViewMenu)e.Menu;
                if (headerMenu.Items.Count > 11) return;

                DevExpress.Utils.Menu.DXMenuItem menuItem = new DevExpress.Utils.Menu.DXMenuItem("Reset Layout Grid", new EventHandler(MyMenuItem));
                DevExpress.Utils.Menu.DXMenuItem menuSave = new DevExpress.Utils.Menu.DXMenuItem("Save Layout Grid", new EventHandler(MenuSave));
                DevExpress.Utils.Menu.DXMenuItem menuItemClear = new DevExpress.Utils.Menu.DXMenuItem("Clear Layout Grid", new EventHandler(MenuClear));
                menuItem.BeginGroup = true;
                menuItem.Tag = e.Menu;
                menuItem.BeginGroup = true;
                menuItemClear.Tag = e.Menu;
                menuSave.Tag = e.Menu;

                headerMenu.Items.Add(menuItem);
                headerMenu.Items.Add(menuItemClear);
                headerMenu.Items.Add(menuSave);
                
            }
            catch 
            {

            }
        }
        private void MyMenuItem(System.Object sender, System.EventArgs e)
        {
            try
            {
                DevExpress.Utils.OptionsLayoutGrid opt = new DevExpress.Utils.OptionsLayoutGrid();
                opt.Columns.StoreAllOptions = true;
                grd_DonVi.MainView.RestoreLayoutFromXml(Application.StartupPath + "\\" + (Commons.Modules.TypeLanguage == 0 ? "XML_VN" : "XML_EN") + "\\grd" + Commons.Modules.sPS.Replace("spGetList", "") + ".xml", opt);
            }catch {

                DevExpress.XtraGrid.Views.Grid.GridView grv = grd_DonVi.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                grv.PopulateColumns();
            }
        }
        private void MenuClear(System.Object sender, System.EventArgs e)
        {
            try
            {
                if(File.Exists(Application.StartupPath + "\\" + (Commons.Modules.TypeLanguage == 0 ? "XML_VN" : "XML_EN") + "\\grd" + Commons.Modules.sPS.Replace("spGetList", "") + ".xml"))
                    File.Delete(Application.StartupPath + "\\" + (Commons.Modules.TypeLanguage == 0 ? "XML_VN" : "XML_EN") + "\\grd" + Commons.Modules.sPS.Replace("spGetList", "") + ".xml");

                DeleteRegisterGrid();
            }
            catch
            {
            }
        }
        private void MenuSave(System.Object sender, System.EventArgs e)
        {
            try
            {
                SaveXmlGrid(grd_DonVi);
            }
            catch
            {
            }
        }



        public void SaveRegisterGrid(DevExpress.XtraGrid.GridControl grdDanhMuc)
        {
            try
            {
                DevExpress.Utils.OptionsLayoutGrid opt = new DevExpress.Utils.OptionsLayoutGrid();
                opt.Columns.StoreAllOptions = true;
                grdDanhMuc.MainView.SaveLayoutToRegistry("DevExpress\\XtraGrid\\Layouts\\HRM\\grd" + Commons.Modules.sPS.Replace("spGetList", ""), opt);
            }
            catch
            { }

        }

        public void DeleteRegisterGrid()
        {
            try
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey("DevExpress\\XtraGrid\\Layouts\\HRM",true);
                reg.DeleteSubKeyTree("grd" + Commons.Modules.sPS.Replace("spGetList", ""));
                reg.Close();
            }catch
            {}
            
        }


        public void SaveXmlGrid(DevExpress.XtraGrid.GridControl grdDanhMuc)
        {
            DeleteRegisterGrid();
            DevExpress.Utils.OptionsLayoutGrid opt = new DevExpress.Utils.OptionsLayoutGrid();
            opt.Columns.StoreAllOptions = true;
            grdDanhMuc.MainView.SaveLayoutToXml(Application.StartupPath + "\\"+ (Commons.Modules.TypeLanguage==0 ? "XML_VN": "XML_EN") + "\\grd" + Commons.Modules.sPS.Replace("spGetList", "") + ".xml", opt);
            SaveRegisterGrid(grdDanhMuc);
        }

        private bool bCheckReg()
        {
            try
            {
                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"DevExpress\XtraGrid\Layouts\HRM\grd" + Commons.Modules.sPS.Replace("spGetList", "")))
                {
                    string tmp = (string)registryKey.GetValue("(Default)");
                }
            }
            catch { return false; }
            return true;
        }

      
        public void loadXmlgrd(DevExpress.XtraGrid.GridControl grdDanhMuc)
        {
            try
            {
                DevExpress.Utils.OptionsLayoutGrid opt = new DevExpress.Utils.OptionsLayoutGrid();
                opt.Columns.StoreAllOptions = true;

                //if (!bCheckReg())
                //{
                //    grdDanhMuc.MainView.RestoreLayoutFromXml(Application.StartupPath + "\\lib\\" + (Commons.Modules.TypeLanguage == 0 ? "XML_VN" : "XML_EN") + "\\grd" + Commons.Modules.sPS.Replace("spGetList", "") + ".xml", opt);
                //    SaveRegisterGrid(grdDanhMuc);
                //}
                //else
                //    grdDanhMuc.MainView.RestoreLayoutFromRegistry("DevExpress\\XtraGrid\\Layouts\\HRM\\grd" + Commons.Modules.sPS.Replace("spGetList", ""), opt);

             // BỎ LOAD TỪ REGISTRY, CHỈ LOAD TỪ XML
                    grdDanhMuc.MainView.RestoreLayoutFromXml(Application.StartupPath + "\\" + (Commons.Modules.TypeLanguage == 0 ? "XML_VN" : "XML_EN") + "\\grd" + Commons.Modules.sPS.Replace("spGetList", "") + ".xml", opt);
                    SaveRegisterGrid(grdDanhMuc);
                
            }
            catch (Exception)
            {
                SaveXmlGrid(grdDanhMuc);
                loadXmlgrd(grdDanhMuc);
            }
        }
    }
}
