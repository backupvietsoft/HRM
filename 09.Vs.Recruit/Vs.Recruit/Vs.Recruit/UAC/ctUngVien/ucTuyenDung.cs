﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraLayout;
using System.Drawing;
using Vs.Report;

namespace Vs.Recruit
{
    public partial class ucTuyenDung : DevExpress.XtraEditors.XtraUserControl
    {
        static Int64 iduv = -1;
        Int64 idtb = -1;
        private Int64 iIDTB_TMP;
        public ucTuyenDung(Int64 id)
        {
            InitializeComponent();
            iduv = id;
        }

        #region function form Load
        private void LoadgrvKeHoachTD()
        {
            DataTable dtKHTD = new DataTable();
            dtKHTD.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetKeHoachTD", Commons.Modules.UserName ,Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdKeHoachTD, grvKeHoachTD, dtKHTD, false, true, true, false, true, this.Name);
            grvKeHoachTD.Columns["ID_KHTD"].Visible = false;
        }

        private void LoadgrvPhongVan()
        {
            try
            {
                DataTable dtUVPV = new DataTable();
            
                dtUVPV.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetPhongVan_UV", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
               
                if (grdPhongVan.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdPhongVan, grvPhongVan, dtUVPV, false, true, false, false, false, this.Name);
                    grvPhongVan.Columns["ID_PV"].Visible = false;
                }
                else
                {
                    grdPhongVan.DataSource = dtUVPV;
                }
            }
            catch
            { }
        }

        private void LoadNN()
        {
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root, layoutControlGroup1 }, windowsUIButton);
        }
        #endregion

        #region function dung chung
        private void AddnewRowTBTD()
        {
            grvKeHoachTD.OptionsBehavior.Editable = true;
            grvKeHoachTD.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            grvKeHoachTD.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
        }
        private void AddnewRowUVPV()
        {
            grvPhongVan.OptionsBehavior.Editable = true;
            grvPhongVan.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            grvPhongVan.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
        }

        #endregion

        #region sự kiện form
        private void ucTuyenDung_Load(object sender, EventArgs e)
        {
            grvPhongVan.OptionsBehavior.ReadOnly = true;
            if (iduv == -1)
            {
                grvKeHoachTD.OptionsBehavior.Editable = false;
            }
            LoadgrvKeHoachTD();
            LoadgrvPhongVan();
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            LoadNN();
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {


                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                default:
                    break;
            }
        }
        private void grvTBTuyenDung_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadgrvPhongVan();
        }
        private void grvTBTuyenDung_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                frmKeHoachTuyenDung_Edit frm = new frmKeHoachTuyenDung_Edit();
                frm.iID_KHTD = Convert.ToInt64(grvKeHoachTD.GetFocusedRowCellValue("ID_KHTD"));
                frm.ShowDialog();
            }
            catch
            {

            }
        }
        #endregion
        
    }
}
