using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
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

namespace Vs.Recruit.UAC
{
    public partial class ucTiepNhanUngVien : DevExpress.XtraEditors.XtraUserControl
    {
        public AccordionControl accorMenuleft;
        public ucTiepNhanUngVien()
        {
            InitializeComponent();
        }
        private void ucTiepNhanUngVien_Load(object sender, EventArgs e)
        {
            DataTable dt_YCTD = new DataTable();
            dt_YCTD.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboYeuCauTuyenDung", Commons.Modules.UserName, Commons.Modules.TypeLanguage, true));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_YCTD, dt_YCTD, "ID_YCTD", "MA_YCTD", "MA_YCTD");

            //Vi tri tuyen dung
            DataTable dt_VTTD = new DataTable();
            dt_VTTD.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboViTriTuyenDung", Commons.Modules.UserName, Commons.Modules.TypeLanguage, true));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_VTTD, dt_VTTD, "ID_VTTD", "TEN_VTTD", "TEN_VTTD");

            DataTable dt_KHTD = new DataTable();
            dt_KHTD.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboKHTD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, true));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_KHTD, dt_KHTD, "ID_TB", "SO_TB", "SO_TB");


            LoadData();
            LoadLuoiND();

            enabel(true);
        }
        private void LoadData()
        {
            try
            {

                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spTiepNhanUV", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dt.Columns["XAC_NHAN_DL"].ReadOnly = false;
                dt.Columns["DA_NHAN_VIEC"].ReadOnly = false;
                dt.Columns["DA_CHUYEN"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSUngVien, grvDSUngVien, dt, true, true, true, false, true, this.Name);
                grvDSUngVien.Columns["ID_UV"].Visible = false;
                grvDSUngVien.Columns["ID_PVUV"].Visible = false;
                grvDSUngVien.Columns["MS_UV"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["DIA_CHI"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["DT_DI_DONG"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["EMAIL"].OptionsColumn.AllowEdit = false;
            }
            catch { }
        }

        private void LoadLuoiND()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListNoiDungDaoTao", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToInt64(grvDSUngVien.GetFocusedRowCellValue("ID_UV"))));
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdNoiDung, grvNoiDung, dt, true, true, true, false, true, this.Name);
            grvNoiDung.Columns["ID_NDDT"].Visible = false;
            grvNoiDung.Columns["ID_UV"].Visible = false;
        }

        private void enabel(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = visible;
            btnALL.Buttons[3].Properties.Visible = visible;
            btnALL.Buttons[4].Properties.Visible = visible;
            btnALL.Buttons[5].Properties.Visible = visible;
            btnALL.Buttons[6].Properties.Visible = !visible;
            btnALL.Buttons[7].Properties.Visible = !visible;
            btnALL.Buttons[8].Properties.Visible = visible;

            grvDSUngVien.OptionsBehavior.Editable = !visible;
            grvNoiDung.OptionsBehavior.Editable = !visible;

        }

        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "inthe":
                        {
                            frmChonUngVien frm = new frmChonUngVien();
                            frm.ShowDialog();
                            break;
                        }
                    case "chuyenDuLieu":
                        {
                            break;
                        }
                    case "them":
                        {
                            enabel(false);
                            break;
                        }
                    case "sua":
                        {
                            enabel(false);
                            break;
                        }
                    case "ghi":
                        {
                            break;
                        }
                    case "khongghi":
                        {
                            enabel(true);
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

        private void grvDSUngVien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadLuoiND();
        }
    }
}
