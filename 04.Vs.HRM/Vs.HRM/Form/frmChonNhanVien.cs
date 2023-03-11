using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Vs.HRM
{
    public partial class frmChonNhanVien : DevExpress.XtraEditors.XtraForm
    {
        public AccordionControl accorMenuleft;
        public frmChonNhanVien()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, btnALL);
        }

        #region even
        private void frmChonNhanVien_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadCombo();
            Commons.Modules.sLoad = "";
            LoadData();
            foreach (ToolStripMenuItem item in contextMenuStrip1.Items)
            {
                item.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, item.Name);
            }
        } 

        private void LoadCombo()
        {
            Commons.Modules.ObjSystems.LoadCboDonVi(cboSearch_DV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboSearch_DV, cboSearch_XN);
            Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN, cboSearch_TO);
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNoiDungDanhGia", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
            //Commons.Modules.ObjSystems.MLoadLookUpEdit(cboNoiDung, dt, "ID_NDDG", "TEN_NDDG", "TEN_NDDG");
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
                            DataTable dt = Commons.Modules.ObjSystems.ConvertDatatable(grdChonNV);
                            if(dt.AsEnumerable().Count(x=>Convert.ToBoolean(x["CHON"]) == true)== 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonUV"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            //lưu dữ liệu chọn lại và cập nhật vào bảng tạm
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTNV" + Commons.Modules.iIDUser, dt, "");

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
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListNhanVienChon", Commons.Modules.UserName,Commons.Modules.TypeLanguage, "sBTChonNV"+ Commons.Modules.iIDUser));
                dt.Columns["CHON"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdChonNV, grvChonNV, dt,false, true, true, true, true, this.Name);
                grvChonNV.Columns["ID_XN"].Visible = false;
                grvChonNV.Columns["ID_DV"].Visible = false;
                grvChonNV.Columns["ID_TO"].Visible = false;
                grvChonNV.Columns["ID_CN"].Visible = false;
                grvChonNV.Columns["CHON"].Visible = false;
                grvChonNV.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
                grvChonNV.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                grvChonNV.OptionsSelection.CheckBoxSelectorField = "CHON";


            }
            catch { }
        }
        #endregion


        public void BackWindowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            LoadData();
        }

      

        private void grvChonUV_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.HitInfo.InDataRow)
                {
                    contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                }
                else
                {
                    contextMenuStrip1.Hide();
                }
            }
            catch
            {
            }
        }

        private void cboSearch_DV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboSearch_DV, cboSearch_XN);
            Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN, cboSearch_TO);
            FillData();
            Commons.Modules.sLoad = "";
        }

        private void cboSearch_XN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN, cboSearch_TO);
            FillData();
            Commons.Modules.sLoad = "";
        }

        private void cboSearch_TO_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            FillData();
            Commons.Modules.sLoad = "";
        }

        private void FillData()
        {
            DataTable dt = new DataTable();
            dt = Commons.Modules.ObjSystems.ConvertDatatable(grdChonNV);
            if (dt == null) return;
            try
            {
                dt.DefaultView.RowFilter = "(ID_DV = '" + cboSearch_DV.EditValue + "' OR " + cboSearch_DV.EditValue + " = -1) AND (ID_XN = " + cboSearch_XN.EditValue + " OR " + cboSearch_XN.EditValue + " = -1) AND (ID_TO = " + cboSearch_TO.EditValue + " OR " + cboSearch_TO.EditValue + " = -1)";
                //grvChonUV.SelectRow(0);
            }
            catch(Exception ex)
            {
                dt.DefaultView.RowFilter = "";
            }
        }

    }
}
