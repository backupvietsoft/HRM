﻿using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vs.Recruit
{
    public partial class frmDanhSachYCTD : DevExpress.XtraEditors.XtraForm
    {
        public Int64 iID_YCTD = -1;

        public frmDanhSachYCTD()
        {
            InitializeComponent();
        }

        #region even
        private void frmDanhSachYCTD_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "thoat":
                        {
                            this.Close();
                            break;
                        }
                }
            }
            catch { }
        }

        
        private void grvDSUV_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                iID_YCTD = Convert.ToInt64(grvDS_YCTD.GetFocusedRowCellValue("ID_YCTD"));
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch { }
        }
        #endregion

        #region function
        private void LoadData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListChonYeuCauTD", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDS_YCTD, grvDS_YCTD, dt, false, true, false, false, true, this.Name);
                grvDS_YCTD.Columns["ID_YCTD"].Visible = false;
            }
            catch { }
        }

        #endregion

        private void grvDSUV_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Delete)
            //{
            //    try
            //    {
            //        if (!ChonUngVien()) return;
            //        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Xoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        {
            //            for (int i = 0; i < dt_CHON.Rows.Count; i++)
            //            {
            //                String sSql = "DELETE FROM dbo.UNG_VIEN_TB_TUYEN_DUNG WHERE ID_UV IN (" + dt_CHON.Rows[i]["ID_UV"] + ") AND ID_TB = " + iID_TB + "";
            //                Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
            //                grvDS_YCTD.DeleteSelectedRows();
            //            }

            //        }
            //        else
            //            return;
            //        ((DataTable)grdDS_YCTD.DataSource).AcceptChanges();
            //    }
            //    catch { }
            //}
        }

        private void grvDSUV_MouseWheel(object sender, MouseEventArgs e)
        {
            //grvDSUV.OptionsView.ColumnAutoWidth = false;

            //grvDSUV.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll;
            //grvDSUV.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;

            //DevExpress.XtraGrid.Views.Grid.GridView view = (sender as DevExpress.XtraGrid.Views.Grid.GridView);
            //view.LeftCoord += e.Delta;
            //(e as DevExpress.Utils.DXMouseEventArgs).Handled = true;
        }
      
    }
}