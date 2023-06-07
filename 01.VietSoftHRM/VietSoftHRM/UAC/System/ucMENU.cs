using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraLayout;
using System.Collections.Generic;

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
            Commons.Modules.ObjSystems.SetPhanQuyen(windowButton);
            Commons.Modules.sLoad = "0Load";
            LoadTreeMenu(false);
            enableButon(true);
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNhomUser, Commons.Modules.ObjSystems.DataNhomUser(false), "ID_NHOM", "TEN_NHOM", "");
            cboNhomUser.EditValue = Convert.ToInt64(Commons.Modules.sIdHT);
            Commons.Modules.sLoad = "";
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, windowButton);
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
            treeListMenu.OptionsView.ShowCheckBoxes = enable;
        }

        private void LoadTreeMenu(bool them)
        {
            try
            {
                if (Commons.Modules.sHideMenu == "0")
                {
                    Commons.Modules.sHideMenu = "-2";
                }
                string strSQL = "SELECT ID_MENU FROM dbo.MENU WHERE ROOT IN (" + Commons.Modules.sHideMenu + ")";
                DataTable dtChuoi = new DataTable();
                dtChuoi.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, strSQL));
                string sID_MENU = "-2";
                if (dtChuoi.Rows.Count > 0 && Commons.Modules.sHideMenu != "-2")
                {
                    for (int i = 0; i < dtChuoi.Rows.Count; i++)
                    {
                        sID_MENU += dtChuoi.Rows[i][0].ToString() + ",";
                    }
                    sID_MENU = sID_MENU.Remove(sID_MENU.Length - 1).Replace("\r\n", string.Empty);
                }
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetMenuPQ", Commons.Modules.sIdHT, them, Commons.Modules.TypeLanguage, Commons.Modules.sHideMenu, sID_MENU));
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
            catch (Exception EX)
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
                        //using (SaveFileDialog saveDialog = new SaveFileDialog())
                        //{
                        //    saveDialog.Filter = "Excel file (*.xlsx)|*.xlsx";
                        //    if (saveDialog.ShowDialog() != DialogResult.Cancel)
                        //    {
                        //        string exportFilePath = saveDialog.FileName;
                        //        string fileExtenstion = new System.IO.FileInfo(exportFilePath).Extension;
                        //        treeListMenu.ExportToXlsx(exportFilePath);
                        //    }
                        //}
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
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "tabMenu" + Commons.Modules.UserName, (DataTable)treeListMenu.DataSource, "");
                            string sSql = "DELETE  FROM dbo.NHOM_MENU WHERE ID_NHOM = " + Commons.Modules.sIdHT + " INSERT INTO dbo.NHOM_MENU (ID_NHOM, ID_MENU, ID_PERMISION) SELECT " + Commons.Modules.sIdHT + ", ID_MENU, ID_PERMISION FROM tabMenu" + Commons.Modules.UserName + " WHERE ISNULL(CHON,1) = 1 AND ID_MENU != -1";
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                            Commons.Modules.ObjSystems.XoaTable("tabMenu" + Commons.Modules.UserName);
                        }
                        catch
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgThemKhongThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK);
                        }
                        EnableControl(false);
                        LoadTreeMenu(false);

                        //treeListMenu.ExportToXlsx(@"C:\Users\PC\Desktop\Sheet1.xlsx");
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
            
            cboNhomUser.Properties.ReadOnly = !visible;
        }

        private void treeListMenu_RowCellClick(object sender, DevExpress.XtraTreeList.RowCellClickEventArgs e)
        {
            setValue_treeListMenu(e.Node);
        }

        private void treeListMenu_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            setValue_treeListMenu(e.Node);
        }

        private void setValue_treeListMenu(TreeListNode node)
        {
            try
            {
                if (int.Parse(node.GetValue(treeListMenu.Columns["ID_PERMISION"]).ToString()) < int.Parse(node.ParentNode.GetValue(treeListMenu.Columns["ID_PERMISION"]).ToString()))
                {
                    node.ParentNode.SetValue(treeListMenu.Columns["ID_PERMISION"], node.GetValue(treeListMenu.Columns["ID_PERMISION"]));
                    setValue_treeListMenu(node.ParentNode);
                }
            }
            catch { }

            try
            {
                foreach (TreeListNode ChildNode in node.Nodes)
                {
                    if (int.Parse(node.GetValue(treeListMenu.Columns["ID_PERMISION"]).ToString()) > int.Parse(ChildNode.GetValue(treeListMenu.Columns["ID_PERMISION"]).ToString()))
                    {
                        ChildNode.SetValue(treeListMenu.Columns["ID_PERMISION"], node.GetValue(treeListMenu.Columns["ID_PERMISION"]));
                        setValue_treeListMenu(ChildNode);
                    }
                }
            }
            catch { }
        }

        private void cboNhomUser_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sIdHT = cboNhomUser.EditValue.ToString();
            LoadTreeMenu(false);
        }
    }
}
