using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;

namespace VietSoftHRM
{
    public partial class ucMENU : DevExpress.XtraEditors.XtraUserControl
    {
        public ucMENU()
        {
            InitializeComponent();
        }
        private void ucMENU_Load(object sender, EventArgs e)
        {
            LoadTreeMenu(false);
            enableButon(true);
            Commons.Modules.ObjSystems.ThayDoiNN(this, windowButton);

        }
        public void setcheck(TreeListNode node)
        {
            foreach (TreeListNode item in node.Nodes)
            {
                if (Convert.ToBoolean(item.GetValue("CHON")) == true)
                    treeListMenu.SetNodeCheckState(item, CheckState.Checked);
                setcheck(item); // recursive call
            }
        }

        private void EnableControl(bool enable)
        {
            treeListMenu.OptionsBehavior.Editable = enable;
            treeListMenu.OptionsView.ShowCheckBoxes =enable;
        }

        private void LoadTreeMenu(bool them)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetMenuPQ", Commons.Modules.sIdHT, them, Commons.Modules.TypeLanguage, Commons.Modules.sHideMenu));
                dtTmp.Columns["TEN_MENU"].ReadOnly = true;
                treeListMenu.DataSource = null;
                treeListMenu.BeginUpdate();
                treeListMenu.DataSource = dtTmp;
                treeListMenu.KeyFieldName = "ID_MENU";
                treeListMenu.ParentFieldName = "MS_CHA";
                treeListMenu.CheckBoxFieldName = "CHON";
                treeListMenu.Columns["CHON"].Visible = false;
                treeListMenu.Columns["STT_MENU"].Visible = false;
                Commons.Modules.ObjSystems.AddCombobyTree("ID_PERMISION", "PERMISION_NAME", treeListMenu, permision());
                treeListMenu.Columns["TEN_MENU"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_MENU");
                treeListMenu.Columns["ID_PERMISION"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "ID_PERMISION");
                EnableControl(false);
                treeListMenu.EndUpdate();
                treeListMenu.ExpandAll();
                TreeListColumn colum = new TreeListColumn();
                colum = treeListMenu.Columns["CHON"];
                foreach (TreeListNode item in treeListMenu.Nodes)
                {
                    setcheck(item);
                }
            }
            catch
            {
            }
        }

        private DataTable permision()
        {
            DataTable dtTempt = new DataTable();
            dtTempt.Columns.Add("ID_PERMISION", typeof(int));
            dtTempt.Columns.Add("PERMISION_NAME", typeof(string));
            dtTempt.Rows.Add(1, "Full access");
            dtTempt.Rows.Add(2, "Read Only");
            return dtTempt;
        }

        private void windowButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        LoadTreeMenu(true);
                        enableButon(false);
                        EnableControl(true);
                        break;
                    }
                case "luu":
                    {
                        enableButon(true);
                        //tạo bảng tạm từ lưới
                        try
                        {
                            treeListMenu.PostEditor();
                            treeListMenu.RefreshDataSource();
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "tabMenu" + Commons.Modules.UserName,(DataTable)treeListMenu.DataSource, "");
                            string sSql = "DELETE  FROM dbo.NHOM_MENU WHERE ID_NHOM = " + Commons.Modules.sIdHT + " INSERT INTO dbo.NHOM_MENU (ID_NHOM, ID_MENU, ID_PERMISION) SELECT " + Commons.Modules.sIdHT + ", ID_MENU, ID_PERMISION FROM tabMenu" + Commons.Modules.UserName + " WHERE ISNULL(CHON,1) = 1 AND ID_MENU != -1";
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                            Commons.Modules.ObjSystems.XoaTable("tabMenu" + Commons.Modules.UserName);
                        }
                        catch 
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgThemKhongThanhCong"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgThongBao"), MessageBoxButtons.YesNo);
                        }
                        EnableControl(false);
                        LoadTreeMenu(false);
                        break;
                    }
                case "khongluu":
                    {
                        LoadTreeMenu(false);
                        enableButon(true);
                        EnableControl(false);
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                default:
                    break;
            }
        }

        private void enableButon(bool visible)
        {
            windowButton.Buttons[0].Properties.Visible = visible;
            windowButton.Buttons[1].Properties.Visible = visible;
            windowButton.Buttons[2].Properties.Visible = visible;
            windowButton.Buttons[3].Properties.Visible = !visible;
            windowButton.Buttons[4].Properties.Visible = !visible;


        }

        private void treeListMenu_RowCellClick(object sender, DevExpress.XtraTreeList.RowCellClickEventArgs e)
        {
           // MessageBox.Show("dsds");
        }
    }
}
