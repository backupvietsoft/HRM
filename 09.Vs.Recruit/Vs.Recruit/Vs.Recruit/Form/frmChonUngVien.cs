using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Vs.Recruit
{
    public partial class frmChonUngVien : DevExpress.XtraEditors.XtraForm
    {
        private ucCTQLUV ucUV;
        public AccordionControl accorMenuleft;
        public Int64 iID_VTTD = 0;
        public Int64 iID_YCTD = 0;
        public frmChonUngVien()
        {
            InitializeComponent();
        }

        #region even
        private void frmChonUngVien_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadCombo();
            cboID_VTTD.EditValue = iID_VTTD;
            Commons.Modules.sLoad = "";
            LoadData();
            cboID_VTTD_EditValueChanged(null, null);
        }

        private void LoadCombo()
        {
            //Vi Tri Tuyen Dung 
    
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_VTTD, Commons.Modules.ObjSystems.DataLoaiCV(true), "ID_LCV", "TEN_LCV", "TEN_LCV");
            //Nguon tuyen dung
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cboID_NTD , Commons.Modules.ObjSystems.DataNguonTD(true), "ID_NTD", "TEN_NTD", "TEN_NTD");
            // Trinh do//ID_TDVH,TEN_TDVH
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cboID_TD, Commons.Modules.ObjSystems.DataTDVH(-1, true), "ID_TDVH", "TEN_TDVH", "TEN_TDVH");
            // Kinh nghiem lam việc//ID_KNLV,TEN_KNLV
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cboID_KNLV, Commons.Modules.ObjSystems.DataKinhNghiemLV(true), "ID_KNLV", "TEN_KNLV", "TEN_KNLV");

        }

        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "ghi":
                        {
                            DataTable dt = Commons.Modules.ObjSystems.ConvertDatatable(grdChonUV);
                            if(dt.AsEnumerable().Count(x=>Convert.ToBoolean(x["CHON"]) == true)== 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonUV"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            //lưu dữ liệu chọn lại và cập nhật vào bảng tạm
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTUV" + Commons.Modules.UserName, dt, "");

                            DialogResult =DialogResult.OK;
                            break;
                        }
                    case "khongghi":
                        {
                            DialogResult = DialogResult.Cancel;
                            this.Close();
                            break;
                        }
                }
            }
            catch
            {
            }
        }
        #endregion

        #region function
        private void LoadData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListUngVienChon",iID_YCTD,iID_VTTD,Commons.Modules.UserName,Commons.Modules.TypeLanguage, "sBTChonUV"+Commons.Modules.UserName));
                dt.Columns["CHON"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdChonUV, grvChonUV, dt, false, true, true, false, true, this.Name);
                grvChonUV.Columns["ID_TDVH"].Visible = false;
                grvChonUV.Columns["ID_KNLV"].Visible = false;
                grvChonUV.Columns["ID_NTD"].Visible = false;
                grvChonUV.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
                grvChonUV.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                grvChonUV.OptionsSelection.CheckBoxSelectorField = "CHON";
            }
            catch { }
        }
        #endregion

        private void grvChonUV_DoubleClick(object sender, EventArgs e)
        {
            if (grvChonUV.RowCount == 0)
            {
                return;
            }
            this.WindowState = FormWindowState.Maximized;
            ucUV = new ucCTQLUV(Convert.ToInt64(grvChonUV.GetFocusedRowCellValue("ID_UV")));
            Commons.Modules.ObjSystems.ShowWaitForm(this);
            ucUV.Refresh();
            tablePanel1.Hide();
            this.Controls.Add(ucUV);
            ucUV.Dock = DockStyle.Fill;
            ucUV.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
            Commons.Modules.ObjSystems.HideWaitForm();
        }

        public void BackWindowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            ucUV.Hide();
            tablePanel1.Show();
            LoadData();
        }

        private void cboID_VTTD_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            DataTable dt = new DataTable();
            dt = Commons.Modules.ObjSystems.ConvertDatatable(grdChonUV);
            if (dt == null) return;
            try
            {
                dt.DefaultView.RowFilter = "((VI_TRI1 = '" + cboID_VTTD.Text.ToString()+ "' OR VI_TRI2 = '" + cboID_VTTD.Text.ToString()+"') OR "+ cboID_VTTD.EditValue + " = -1) AND (ID_NTD = " + cboID_NTD.EditValue + " OR " + cboID_NTD.EditValue + " = -1) AND (ID_TDVH = " + cboID_TD.EditValue + " OR " + cboID_TD.EditValue + " = -1) AND (ID_KNLV = " + cboID_KNLV.EditValue + "OR " + cboID_KNLV.EditValue + " = -1)";
                //grvChonUV.SelectRow(0);
            }
            catch
            {
                dt.DefaultView.RowFilter = "";
            }
        }
    }
}
