using DevExpress.XtraBars.Docking2010;
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
    public partial class frmKeHoachTuyenDung_Edit : DevExpress.XtraEditors.XtraForm
    {
        public Int64 iID_TBTMP = -1;
        public Int64 iID_KHTD = -1;
        private ucCTQLUV ucUV;
        public frmKeHoachTuyenDung_Edit()
        {
            InitializeComponent();
            //iID_TB = ID_TB;
        }
        #region even

        private void frmKeHoachTuyenDung_Edit_Load(object sender, EventArgs e)
        {
            enabel(true);
            try
            {
                //nguoi quen
                DataTable dt_CN = new DataTable();
                dt_CN.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNguoiPV_On1, dt_CN, "ID_CN", "HO_TEN", "HO_TEN");
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNguoiPV_On2, dt_CN, "ID_CN", "HO_TEN", "HO_TEN");
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNguoiPV_Off1, dt_CN, "ID_CN", "HO_TEN", "HO_TEN");
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNguoiPV_Off2, dt_CN, "ID_CN", "HO_TEN", "HO_TEN");
                LoadCboTinhTrang();

                DataTable dt_YCTD = new DataTable();
                dt_YCTD.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboYeuCauTuyenDung", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_YCTD, dt_YCTD, "ID_YCTD", "MA_YCTD", "MA_YCTD");

                //Vi tri tuyen dung
                DataTable dt_VTTD = new DataTable();
                dt_VTTD.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboViTriTuyenDung", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_VTTD, dt_VTTD, "ID_VTTD", "TEN_VTTD", "TEN_VTTD");

                LoadData();
                Bindingdata(false);
            }
            catch
            {

            }
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUngVienKeHoachTD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iID_KHTD));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdUV, grvUV, dt, false, true, true, false, true, this.Name);
                grvUV.Columns["ID_UV"].Visible = false;
            }
            catch { }

        }
        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "chonUV":
                        {
                            frmChonUngVien frm = new frmChonUngVien();
                            frm.ShowDialog();
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
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            break;
                        }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        #endregion

        #region function
        private void enabel(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = visible;
            windowsUIButton.Buttons[6].Properties.Visible = visible;
            windowsUIButton.Buttons[7].Properties.Visible = !visible;
            windowsUIButton.Buttons[8].Properties.Visible = !visible;
            windowsUIButton.Buttons[9].Properties.Visible = visible;

            txtSO_TB.Properties.ReadOnly = visible;
            datNGAY_LAP.Properties.ReadOnly = visible;
            txtTIEU_DE_TD.Properties.ReadOnly = visible;
            cboTINH_TRANG.Properties.ReadOnly = visible;
            cboID_YCTD.Properties.ReadOnly = visible;
            cboID_VTTD.Properties.ReadOnly = visible;
            chkOnline.Properties.ReadOnly = visible;
            chkOffline.Properties.ReadOnly = visible;
            datNgayPV_Off.Properties.ReadOnly = visible;
            datNgayPV_On.Properties.ReadOnly = visible;
            cboNguoiPV_On1.Properties.ReadOnly = visible;
            cboNguoiPV_On2.Properties.ReadOnly = visible;
            cboNguoiPV_Off1.Properties.ReadOnly = visible;
            cboNguoiPV_Off2.Properties.ReadOnly = visible;
            txtGHI_CHU.Properties.ReadOnly = visible;
        }
        #endregion

        private void grvUV_DoubleClick(object sender, EventArgs e)
        {
            if (grvUV.RowCount == 0)
            {
                return;
            }
            ucUV = new ucCTQLUV(Convert.ToInt64(grvUV.GetFocusedRowCellValue("ID_UV")));
            Commons.Modules.ObjSystems.ShowWaitForm(this);
            ucUV.Refresh();
            //ns.accorMenuleft = accorMenuleft;
            tablePanel1.Hide();
            this.Controls.Add(ucUV);
            ucUV.Dock = DockStyle.Fill;
            ucUV.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
            //accorMenuleft.Visible = false;
            Commons.Modules.ObjSystems.HideWaitForm();
        }

        public void BackWindowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            ucUV.Hide();
            tablePanel1.Show();
            LoadData();

            //DataTable dtmp = new DataTable();
            //dtmp = (DataTable)grdChonUV.DataSource;
            //if (dtmp.Rows.Count == 0) return;
            //string chuoiIDUV_tmp = "";
            //for (int i = 0; i < dtmp.Rows.Count; i++)
            //{
            //    chuoiIDUV_tmp += dtmp.Rows[i]["ID_UV"].ToString() + ",";
            //}
            //string chuoiIDUV = chuoiIDUV_tmp.Remove(chuoiIDUV_tmp.Length - 1);

            //LoadData(true, chuoiIDUV, iIDPV);
            //accorMenuleft.Visible = true;

        }

        private void Bindingdata(bool them)
        {
            if (them == true)
            {

            }
            else
            {
                try
                {

                    DataTable dt = new DataTable();
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetChiTietKHTD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iID_KHTD));

                    txtSO_TB.EditValue = dt.Rows[0]["SO_TB"];
                    datNGAY_LAP.EditValue = dt.Rows[0]["NGAY_LAP"];
                    txtTIEU_DE_TD.EditValue = dt.Rows[0]["TIEU_DE"];
                    cboTINH_TRANG.EditValue = dt.Rows[0]["TINH_TRANG"];
                    cboID_YCTD.EditValue = dt.Rows[0]["ID_YCTD"];
                    cboID_VTTD.EditValue = dt.Rows[0]["ID_VTTD"];
                    if (Convert.ToInt32(dt.Rows[0]["PV_ONLINE"]) == 1)
                    {
                        chkOnline.Checked = true;
                    }
                    else
                    {
                        chkOnline.Checked = false;
                    }
                    if (Convert.ToInt32(dt.Rows[0]["PV_OFFLINE"]) == 0)
                    {
                        chkOffline.Checked = true;
                    }
                    else
                    {
                        chkOffline.Checked = false;
                    }
                    datNgayPV_On.EditValue = dt.Rows[0]["NGAY_PV_ONLINE_DK"];
                    datNgayPV_Off.EditValue = dt.Rows[0]["NGAY_PV_OFLINE_DK"];
                    cboNguoiPV_On1.EditValue = dt.Rows[0]["NGUOI_PV_ONLINE_1"];
                    cboNguoiPV_On2.EditValue = dt.Rows[0]["NGUOI_PV_ONLINE_2"];
                    cboNguoiPV_Off1.EditValue = dt.Rows[0]["NGUOI_PV_OFLINE_1"];
                    cboNguoiPV_Off2.EditValue = dt.Rows[0]["NGUOI_PV_OFLINE_2"];
                    txtGHI_CHU.EditValue = dt.Rows[0]["GHI_CHU"];
                }
                catch (Exception ex){ }
            }
        }

        private void LoadCboTinhTrang()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetTinhTrang_KHTD", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTINH_TRANG, dt, "ID_TT", "TINH_TRANG", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TINH_TRANG"), true, true);
            }
            catch { }
        }

        private void txtSO_TB_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmDanhSachKHTD frm = new frmDanhSachKHTD();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                iID_KHTD = frm.iID_KHTD;
                Bindingdata(false);
                LoadData();
            }
        }
    }
}
