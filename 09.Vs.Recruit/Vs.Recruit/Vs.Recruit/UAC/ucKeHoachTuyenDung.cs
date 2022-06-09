using DevExpress.Utils.Menu;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Vs.Recruit
{
    public partial class ucKeHoachTuyenDung : DevExpress.XtraEditors.XtraUserControl
    {
        public static Int64 Id = -1;
        public static string sSOTB = "";
        private ucCTQLUV ucUV;
        public AccordionControl accorMenuleft;

        private Int64 iIDTB_TMP = -1;
        public ucKeHoachTuyenDung()
        {
            InitializeComponent();
        }
        private void ucKeHoachTuyenDung_Load(object sender, EventArgs e)
        {
            LoadCboTinhTrang();
            chkNgayLap.Checked = false;
            chkNgayPV.Checked = false;
            datTNgay.EditValue = DateTime.Now;
            datDNgay.EditValue = DateTime.Now;
            loadData();
        }
        private void loadData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListKE_HOACH_TUYEN_DUNG", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, false, true, true, false, true, this.Name);
                grvChung.Columns["ID_TB"].Visible = false;
                grvChung.Columns["ID_YCTD"].Visible = false;
                grvChung.Columns["ID_VTTD"].Visible = false;
            }
            catch { }
        }

        private void grvChung_DoubleClick(object sender, EventArgs e)
        {
            frmKeHoachTuyenDung_Edit frm = new frmKeHoachTuyenDung_Edit();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.iID_KHTD = Convert.ToInt64(grvChung.GetFocusedRowCellValue("ID_TB"));
            frm.ShowDialog();
        }

        private void btnALL_ButtonClick(object sender, ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "them":
                        {
                            frmKeHoachTuyenDung_Edit frm = new frmKeHoachTuyenDung_Edit();
                            frm.StartPosition = FormStartPosition.CenterParent;
                            frm.iID_KHTD = -1;
                            frm.ShowDialog();
                            break;
                        }
                    case "sua":
                        {
                            frmKeHoachTuyenDung_Edit frm = new frmKeHoachTuyenDung_Edit();
                            frm.StartPosition = FormStartPosition.CenterParent;
                            frm.iID_KHTD = Convert.ToInt64(grvChung.GetFocusedRowCellValue("ID_TB"));
                            frm.ShowDialog();
                            break;
                        }
                 
                    case "thoat":
                        {
                            Commons.Modules.ObjSystems.GotoHome(this);
                            break;
                        }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LoadCboTinhTrang()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetTinhTrang_KHTD", Commons.Modules.UserName,Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTINH_TRANG, dt, "ID_TT", "TINH_TRANG", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TINH_TRANG"), true, true);
                cboTINH_TRANG.EditValue = 2;
            }
            catch { }
        }
    }
}
