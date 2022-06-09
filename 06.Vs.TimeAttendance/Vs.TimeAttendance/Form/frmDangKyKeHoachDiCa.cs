using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using System.Data.SqlClient;
using DevExpress.XtraEditors.Repository;
using System.Drawing;

namespace Vs.TimeAttendance
{
    public partial class frmDangKyKeHoachDiCa : DevExpress.XtraEditors.XtraForm
    {
        public frmDangKyKeHoachDiCa()
        {
            InitializeComponent();

            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, btnALL);
        }

        private void frmDangKyKeHoachDiCa_Load(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root);
            //Commons.Modules.ObjSystems.MLoadLookUpEdit(cboNhom, Commons.Modules.ObjSystems.DataNhom(), "ID_NHOM", "TEN_NHOM", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_NHOM"));
            //Commons.Modules.ObjSystems.MLoadLookUpEdit(cboCa, Commons.Modules.ObjSystems.DataCa(Convert.ToInt32(cboNhom.EditValue)), "CA", "CA", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_NHOM"));
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);

            dttTu_ngay.DateTime = DateTime.Now;
            dttDen_ngay.DateTime = DateTime.Now;

            Commons.OSystems.SetDateEditFormat(dttTu_ngay);
            Commons.OSystems.SetDateEditFormat(dttDen_ngay);

            loadcbm();
            loadcbm_ca();
            LoadGrdCongNhan();
            Commons.Modules.sPS = "";
        }
        private void LoadGrdCongNhan()
        {
            try
            {
                Commons.Modules.sPS = "0Load";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetChonCongNhan", cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dt.Columns["CHON"].ReadOnly = false;
                dt.Columns["MS_CN"].ReadOnly = true;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true,false, true, true, true, this.Name);
                                               
                grvData.Columns["CHON"].Visible = false;
                grvData.Columns["ID_CN"].Visible = false;
                grvData.Columns["CHON"].Width = 100;
                grvData.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
                grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                grvData.OptionsSelection.CheckBoxSelectorField = "CHON";

                //grvData.Appearance.HeaderPanel.BackColor = Color.FromArgb(240, 128, 25);
                //grvData.Columns["HO_TEN"].AppearanceHeader.BackColor= Color.FromArgb(240, 128, 25);
                //grvData.Columns["MS_CN"].AppearanceHeader.BackColor = Color.FromArgb(240, 128, 25);
                //grvData.Columns["CHON"].AppearanceHeader.BackColor = Color.FromArgb(240, 128, 25);
                //grvData.Columns["TEN_TO"].AppearanceHeader.BackColor = Color.FromArgb(240, 128, 25);
                //grvData.Columns["TEN_XN"].AppearanceHeader.BackColor = Color.FromArgb(240, 128, 25);
                Commons.Modules.sPS = "";
            }
            catch (Exception)
            {
            }
        }
        
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdCongNhan();
            Commons.Modules.sPS = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdCongNhan();
            Commons.Modules.sPS = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGrdCongNhan();
            Commons.Modules.sPS = "";
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "capnhat":
                    {
                        if (!kiemtrong()) return;
                        String sBT = "NVDC" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grdData), "");
                        SqlCommand cmd = new SqlCommand();
                        SqlConnection connect = new SqlConnection(Commons.IConnections.CNStr);
                        if (connect.State == ConnectionState.Closed) connect.Open();
                        cmd.Connection = connect;
                        cmd.Parameters.AddWithValue("ID_NHOM",cboNhom.EditValue);
                        cmd.Parameters.AddWithValue("CA", cboCA.Text);
                        cmd.Parameters.AddWithValue("TU_NGAY", dttTu_ngay.DateTime);
                        cmd.Parameters.AddWithValue("DEN_NGAY", dttDen_ngay.DateTime);
                        cmd.Parameters.AddWithValue("GHI_CHU", txtghichu.Text);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spUPDATE_KHDC";

                        cmd.ExecuteNonQuery();

                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "messCapNhatOk"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                case "xoa":
                    {
                        if (!kiemtrong()) return;
                        String sBT = "NVDC" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grdData), "");
                        SqlCommand cmd = new SqlCommand();
                        SqlConnection connect = new SqlConnection(Commons.IConnections.CNStr);
                        if (connect.State == ConnectionState.Closed) connect.Open();
                        cmd.Connection = connect;
                        cmd.Parameters.AddWithValue("ID_NHOM", cboNhom.EditValue);
                        cmd.Parameters.AddWithValue("CA", cboCA.Text);
                        cmd.Parameters.AddWithValue("TU_NGAY", dttTu_ngay.DateTime);
                        cmd.Parameters.AddWithValue("DEN_NGAY", dttDen_ngay.DateTime);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spDELETE_KHDC";

                        cmd.ExecuteNonQuery();

                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "messCapNhatOk"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
            }

        }
        private Boolean kiemtrong()
        {
            if (cboNhom.Text == "")
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "messchuanhapNhom"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboNhom.Focus();
                return false;
            }
            if (cboCA.Text=="")
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "messchuanhapca"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboCA.Focus();
                return false;
            }

            if(dttTu_ngay.Text=="")
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "messchuanhapngay_bd"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                dttTu_ngay.Focus();
                return false;
            }
            if(dttDen_ngay.Text=="")
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "messchuanhapngay_kt"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                dttDen_ngay.Focus();
                return false;
            }
            return true;
        }
        private void loadcbm()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr,CommandType.Text, "select ID_NHOM,TEN_NHOM from NHOM_CHAM_CONG"));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNhom, dt, "ID_NHOM", "TEN_NHOM", "Ten_nhom");
                cboNhom.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void loadcbm_ca()
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "SELECT ID_CDLV,CA,CONVERT(nvarchar(10),GIO_BD,108) as GIO_BDLV, CONVERT(nvarchar(10),GIO_KT,108) as GIO_KTLV FROM CHE_DO_LAM_VIEC WHERE ID_NHOM= " + cboNhom.EditValue;
                
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text,sql ));
             
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboCA, dt, "ID_CDLV", "CA", "Ca_lam");
                cboCA.EditValue = -1;

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void grdData_MouseHover(object sender, EventArgs e)
        {
            //grvData.Appearance.Row. = Color.Red;
            //XtraMessageBox.Show(sender.GetHashCode().ToString());
        }

        private void cboNhom_EditValueChanged(object sender, EventArgs e)
        {
            loadcbm_ca();
        }
    }
}