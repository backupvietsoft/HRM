using System;
using System.Collections.Generic;
using System.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
namespace VietSoftHRM
{
    public partial class ucDU_LIEU : DevExpress.XtraEditors.XtraUserControl
    {
        public ucDU_LIEU()
        {
            InitializeComponent();
        }
        private void LoadTo(bool them)
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListDuLieu", Commons.Modules.sIdHT, Commons.Modules.UserName, Commons.Modules.TypeLanguage, them));
                if (them == true)
                {
                    dt.Columns["TEN_TO"].ReadOnly = true;
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdTo, grvTo, dt, true, false, true, true, true, this.Name);
                    grvTo.Columns["CHON"].Visible = true;
                    grvTo.Columns["CHON"].Width = 100;
                    grvTo.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
                    grvTo.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                    grvTo.OptionsSelection.CheckBoxSelectorField = "CHON";
                }
                else
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdTo, grvTo, dt, false, false, true, true, true,this.Name);
                    grvTo.Columns["CHON"].Visible = false;
                    grvTo.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
                    grvTo.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                }
                grvTo.Columns["CHON"].Visible = false;
                grvTo.Columns["STT_TO"].Visible = false;
                grvTo.Columns["ID_TO"].Visible = false;
                grvTo.Columns["ID_XN"].Visible = false;

                grvTo.Columns["TEN_DV"].GroupIndex = 1;
                grvTo.Columns["TEN_XN"].GroupIndex = 2;
                grvTo.ExpandAllGroups();
                grvTo.ExpandAllGroups();
            }
            catch
            {

            }
        }
        private void ucDU_LIEU_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadTo(false);
            enableButon(true);
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNhomUser, Commons.Modules.ObjSystems.DataNhomUser(false), "ID_NHOM", "TEN_NHOM", "");
            cboNhomUser.EditValue = Convert.ToInt64(Commons.Modules.sIdHT);
            Commons.Modules.sLoad = "";
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, windowsUIButton);
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        enableButon(false);
                        LoadTo(true);
                        break;
                    }
                case "xoa":
                    {
                        break;
                    }
                case "sua":
                    {
                        enableButon(false);
                        break;
                    }
                case "luu":
                    {
                        grvTo.PostEditor();
                        grvTo.UpdateCurrentRow();
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "tabTo" + Commons.Modules.UserName, Commons.Modules.ObjSystems.ConvertDatatable(grdTo), "");
                        string sSql = "DELETE dbo.NHOM_TO WHERE ID_NHOM = " + Convert.ToInt64(Commons.Modules.sIdHT) + " INSERT INTO dbo.NHOM_TO ( ID_NHOM, ID_TO ) SELECT " + Convert.ToInt64(Commons.Modules.sIdHT) + ",ID_TO FROM tabTo" + Commons.Modules.UserName + " WHERE CHON = 1";
                        SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                        Commons.Modules.ObjSystems.XoaTable("tabTo" + Commons.Modules.UserName);
                        LoadTo(false);
                        enableButon(true);
                        break;
                    }
                case "khongluu":
                    {
                        LoadTo(false);
                        enableButon(true);
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
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = !visible;
            windowsUIButton.Buttons[3].Properties.Visible = !visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            cboNhomUser.Properties.ReadOnly = !visible;
        }

        private void cboNhomUser_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sIdHT = cboNhomUser.EditValue.ToString();
            LoadTo(false);
        }
    }
}
