using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
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
    public partial class ucPhongVan : DevExpress.XtraEditors.XtraUserControl
    {
        private Int64 iIDPV = -1;
        public AccordionControl accorMenuleft;
        private Int64 iID_PV = 10;
        public ucPhongVan(Int64 idpv)
        {
            InitializeComponent();
            iIDPV = idpv;
        }

        #region even
        private void ucPhongVan_Load(object sender, EventArgs e)
        {

            //nguoi quen
            DataTable dt_CN = new DataTable();
            dt_CN.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_NPV1, dt_CN, "ID_CN", "HO_TEN", "HO_TEN",false,true);
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_NPV2, dt_CN, "ID_CN", "HO_TEN", "HO_TEN",false, true);

            DataTable dt_YCTD = new DataTable();
            dt_YCTD.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboYeuCauTuyenDung", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_YCTD, dt_YCTD, "ID_YCTD", "MA_YCTD", "MA_YCTD");

            DataTable dt_KHTD = new DataTable();
            dt_KHTD.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboKHTD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_KHTD, dt_KHTD, "ID_TB", "SO_TB", "SO_TB");

            //Vi tri tuyen dung
            DataTable dt_VTTD = new DataTable();
            dt_VTTD.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboViTriTuyenDung", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_VTTD, dt_VTTD, "ID_VTTD", "TEN_VTTD", "TEN_VTTD");

            DataTable dt_TT = new DataTable();
            dt_TT.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetTinhTrang_PV", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTINH_TRANG_PV, dt_TT, "ID_TT", "TINH_TRANG", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TINH_TRANG"), true, true);

            LoadDSUV_PV();
            LoadND_PV();

            Commons.Modules.ObjSystems.AddnewRow(grvPhongVanUV, true);
            Commons.Modules.ObjSystems.AddnewRow(grvNoiDung_PV, true);
            //TaoMa();
            //datNGAY_LAP.EditValue = DateTime.Now.ToShortDateString();
            //datTG_BD.EditValue = DateTime.Now.ToShortDateString();
            //datTG_KT.EditValue = DateTime.Now.ToShortDateString();
            //Bindingdata(true);
            enableButon(true);
            BindingData(false);
            //LoadData(false, "", -1);
            //Loadcbo();
            //// AddnewRow();
            //LoadNN();
        }
        private void txtMA_SO_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                frmDanhSachPhongVan frm = new frmDanhSachPhongVan();
                if(frm.ShowDialog() == DialogResult.OK)
                {
                    iID_PV = frm.iID_PV;
                    BindingData(false);
                    LoadDSUV_PV();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        private void btnALL_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        enableButon(false);
                        BindingData(true);
                        grdPhongVanUV.DataSource = ((DataTable)grdPhongVanUV.DataSource).Clone();
                        grdNoiDung_PV.DataSource = ((DataTable)grdNoiDung_PV.DataSource).Clone();
                        break;
                    }
                case "sua":
                    {
                        enableButon(false);
                        break;
                    }

                case "luu":
                    {
                        try
                        {
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }

                        break;
                    }
                case "khongluu":
                    {
                        enableButon(true);
                        BindingData(false);
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
        #endregion

        #region function 
        //private void Loadcbo()
        //{
        //    try
        //    {
        //        System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
        //        conn.Open();

        //        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spPhongVan", conn);
        //        cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
        //        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);

        //        //Load combo ID_TB
        //        DataTable dt = new DataTable();
        //        dt = ds.Tables[0].Copy();
        //        Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_TB, dt, "ID_TB", "SO_TB", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "SO_TB"), true, true);

        //        //Load combo TINH_TRANG
        //        DataTable dt1 = new DataTable();
        //        dt1 = ds.Tables[1].Copy();
        //        Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(txtTHOI_GIAN_KT, dt1, "ID_TT", "TINH_TRANG", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TINH_TRANG"), true, true);
        //        txtTHOI_GIAN_KT.Properties.View.Columns[0].Visible = false;
        //    }
        //    catch { }
        //}
        private void TaoMa()
        {
            string Ma = "";
            try
            {
                Ma = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "MTaoSoPhieuTD", "PV", "PHONG_VAN", "MA_SO", Convert.ToDateTime(datNGAY_PV.EditValue).ToString()).ToString();
            }
            catch { Ma = ""; }
            txtMA_SO.Text = Ma;
        }
        private void enableButon(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = visible;
            btnALL.Buttons[3].Properties.Visible = visible;
            btnALL.Buttons[4].Properties.Visible = !visible;
            btnALL.Buttons[5].Properties.Visible = !visible;
            btnALL.Buttons[6].Properties.Visible = visible;

            grvPhongVanUV.OptionsBehavior.Editable = !visible;
            grvNoiDung_PV.OptionsBehavior.Editable = !visible;

            txtMA_SO.Properties.ReadOnly = visible;
            cboID_YCTD.Properties.ReadOnly = visible;
            datNGAY_PV.Enabled = !visible;
            cboID_KHTD.Properties.ReadOnly = visible;
            txtBUOC_PV.Properties.ReadOnly = visible;
            cboID_NPV1.Properties.ReadOnly = visible;
            cboID_NPV2.Properties.ReadOnly = visible;
            cboID_VTTD.Properties.ReadOnly = visible;
            txtTHOI_GIAN_BD.Properties.ReadOnly = visible;
            txtTHOI_GIAN_KT.Properties.ReadOnly = visible;
            chkPVOnOff.Properties.ReadOnly = visible;
            cboTINH_TRANG_PV.Properties.ReadOnly = visible;
        }

        private void LoadDSUV_PV()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetUngVienPV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iID_PV));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdPhongVanUV, grvPhongVanUV, dt, false, true, true, false, true, this.Name);
                grvPhongVanUV.Columns["ID_UV"].Visible = false;
                grvPhongVanUV.Columns["ID_PVUV"].Visible = false;
                grvPhongVanUV.Columns["MS_UV"].OptionsColumn.AllowEdit = false;
                grvPhongVanUV.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
            }
            catch { }
        }

        private void LoadND_PV()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetPVUV_KET_QUA", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToInt64(grvPhongVanUV.GetFocusedRowCellValue("ID_PVUV"))));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNoiDung_PV, grvNoiDung_PV, dt, false, true, true, false, true, this.Name);
                grvNoiDung_PV.Columns["ID_PVUV"].Visible = false;
                grvNoiDung_PV.Columns["ID_NDPV"].Visible = false;
            }
            catch { }
        }

        private void BindingData(bool them)
        {
            if(them == true)
            {
                txtMA_SO.EditValue = "";
                datNGAY_PV.EditValue = DateTime.Now;
                cboID_YCTD.EditValue = null;
                cboID_KHTD.EditValue = null;
                cboID_VTTD.EditValue = null;
                txtBUOC_PV.EditValue = 1;
                cboID_NPV1.EditValue = null;
                cboID_NPV2.EditValue = null;
                txtTHOI_GIAN_BD.EditValue = "";
                txtTHOI_GIAN_KT.EditValue = "";
                chkPVOnOff.EditValue = false;
                cboTINH_TRANG_PV.EditValue = null;
            }
            else
            {
                try
                {
                    DataTable dt = new DataTable();
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetChiTietPhongVan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iID_PV));
                    txtMA_SO.EditValue = dt.Rows[0]["MA_SO"];
                    datNGAY_PV.EditValue = dt.Rows[0]["NGAY_PV"];
                    cboID_YCTD.EditValue = dt.Rows[0]["ID_YCTD"];
                    cboID_KHTD.EditValue = dt.Rows[0]["ID_KHTD"];
                    cboID_VTTD.EditValue = dt.Rows[0]["ID_VTTD"];
                    txtBUOC_PV.EditValue = dt.Rows[0]["BUOC_PV"];
                    cboID_NPV1.EditValue = dt.Rows[0]["NGUOI_PV_1"];
                    cboID_NPV2.EditValue = dt.Rows[0]["NGUOI_PV_2"];
                    txtTHOI_GIAN_BD.EditValue = dt.Rows[0]["TG_BD"];
                    txtTHOI_GIAN_KT.EditValue = dt.Rows[0]["TG_KT"];
                    chkPVOnOff.EditValue = dt.Rows[0]["PV_ON_OF_LINE"];
                    cboTINH_TRANG_PV.EditValue = dt.Rows[0]["TINH_TRANG"];

                    LoadDSUV_PV();
                    LoadND_PV();

                }
                catch { }
            }
        }
        #endregion

        private void grvPhongVanUV_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadND_PV();
        }
    }
}
