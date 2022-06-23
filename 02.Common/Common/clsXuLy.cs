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

                DevExpress.Utils.Menu.DXMenuItem menuItem = new DevExpress.Utils.Menu.DXMenuItem("Reset Grid", new EventHandler(MyMenuItem));
                DevExpress.Utils.Menu.DXMenuItem menuSave = new DevExpress.Utils.Menu.DXMenuItem("Save Grid", new EventHandler(MenuSave));
                DevExpress.Utils.Menu.DXMenuItem menuItemClear = new DevExpress.Utils.Menu.DXMenuItem("Clear Grid", new EventHandler(MenuClear));
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


        public  void MFieldRequest(DevExpress.XtraEditors.LabelControl Mlayout)
        { ////red, green, blue
            //int R = 156, G = 97, B = 65;
            //try { R = int.Parse(VS.ERP.Properties.Settings.Default["ApplicationColorRed"].ToString()); } catch { R = 156; }
            //try { G = int.Parse(VS.ERP.Properties.Settings.Default["ApplicationColorGreen"].ToString()); } catch { G = 97; }
            //try { B = int.Parse(VS.ERP.Properties.Settings.Default["ApplicationColorBlue"].ToString()); } catch { B = 65; }

            //Mlayout.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(R, G, B);
            //Mlayout.AppearanceItemCaption.Options.UseForeColor = true;
            try
            {

                Mlayout.Appearance.Font = new System.Drawing.Font(DevExpress.Utils.AppearanceObject.DefaultFont.ToString(), DevExpress.Utils.AppearanceObject.DefaultFont.Size + 1, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //Mlayout.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                Mlayout.Appearance.Options.UseFont = true;
                Mlayout.Appearance.Options.UseForeColor = true;
                //Mlayout.AppearanceItemCaption.Font = new System.Drawing.Font(VS.ERP.Properties.Settings.Default["ApplicationFontRequestName"].ToString(), float.Parse(VS.ERP.Properties.Settings.Default["ApplicationFontRequestSize"].ToString()), (VS.ERP.Properties.Settings.Default["ApplicationFontRequestBold"].ToString().ToUpper() == "TRUE" ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular) | (VS.ERP.Properties.Settings.Default["ApplicationFontRequestItalic"].ToString().ToUpper() == "TRUE" ? System.Drawing.FontStyle.Italic : System.Drawing.FontStyle.Regular));
            }
            catch { Mlayout.Appearance.Font = new System.Drawing.Font("Segoe UI", float.Parse("11")); }


            //System.Drawing.FontStyle = new System.Drawing.FontStyle(Settings.Default["ApplicationFontRequestName"].ToString(), float.Parse(Settings.Default["ApplicationFontRequestSize"].ToString()));

            //Font font = new Font(VS.ERP.Properties.Settings.Default["ApplicationFontRequestName"].ToString(), FontStyle.Bold | FontStyle.Underline);


        }

    }
}
