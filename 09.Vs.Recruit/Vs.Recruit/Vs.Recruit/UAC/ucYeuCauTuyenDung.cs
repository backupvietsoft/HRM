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
    public partial class ucYeuCauTuyenDung : DevExpress.XtraEditors.XtraUserControl
    {

        public AccordionControl accorMenuleft;
        private Int64 iID_YCTD = 1;
        public ucYeuCauTuyenDung()
        {
            InitializeComponent();
        }

        #region even
        private void ucYeuCauTuyenDung_Load(object sender, EventArgs e)
        {
            LoadCbo();
            LoadgrdThayThe();
            LoadgrdYeuCauTuyenDung();
            LoadgrdFileDinhKem();
            txtMLDuKien.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            BindingData(false);
            Commons.Modules.ObjSystems.AddnewRow(grvThayThe, true);
            Commons.Modules.ObjSystems.AddnewRow(grvDSNDPV, true);
            Commons.Modules.ObjSystems.AddnewRow(grvFileDK, true);
            enableButon(true);
        }
        private void LoadgrdThayThe()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListYCTDThayThe", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iID_YCTD));
            if(grdThayThe.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThayThe, grvThayThe, dt, true, true, true, true, true, this.Name);
                grvThayThe.Columns["ID_YCTD"].Visible = false;
                //grvThayThe.Columns["ID_CN"].Visible = false;
                grvThayThe.Columns["ID_LCV"].Visible = false;
                grvThayThe.Columns["MS_CN"].Visible = false;
            }
            else
            {
                grdThayThe.DataSource = dt;
            }
            

            DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_CN = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            DataTable dID_NHOM = new DataTable();
            dID_NHOM.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            cboID_CN.NullText = "";
            cboID_CN.ValueMember = "ID_CN";
            cboID_CN.DisplayMember = "MS_CN";
            cboID_CN.DataSource = dID_NHOM;
            cboID_CN.Columns.Clear();
            cboID_CN.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_CN"));
            cboID_CN.Columns["ID_CN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "ID_CN");
            //cboID_CN.Columns["ID_CN"].Visible = false;

            cboID_CN.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MS_CN"));
            cboID_CN.Columns["MS_CN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "MS_CN");

            cboID_CN.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            cboID_CN.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            grvThayThe.Columns["ID_CN"].ColumnEdit = cboID_CN;
            cboID_CN.BeforePopup += CboID_PC_BeforePopup;

            //DataTable dt1 = new DataTable();
            //string SQL = "SELECT ID_CN, MS_CN FROM CONG_NHAN";
            //dt1.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, SQL));
            //Commons.Modules.ObjSystems.AddCombXtra("ID_CN", "MS_CN", grvThayThe, dt1, false, "ID_CN", "CONG_NHAN");

            //DataTable dID_NHOM = new DataTable();
            //dID_NHOM.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongNhan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            //Commons.Modules.ObjSystems.AddCombXtra("ID_CN", "TEN_CN", grvThayThe, dID_NHOM, false, "ID_CN", this.Name);
        }
        private void CboID_PC_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                string id_cv = grvThayThe.GetFocusedRowCellValue("MS_CN").ToString();
                if (sender is LookUpEdit cbo)
                {
                    try
                    {
                        DataTable DataCombo = (DataTable)cbo.Properties.DataSource;
                        DataTable DataLuoi = Commons.Modules.ObjSystems.ConvertDatatable(grdThayThe);
                        var DataNewCombo = DataCombo.AsEnumerable().Where(r => !DataLuoi.AsEnumerable()
                        .Any(r2 => r["MS_CN"].ToString().Trim() == r2["MS_CN"].ToString().Trim())).CopyToDataTable();
                        cbo.Properties.DataSource = null;
                        cbo.Properties.DataSource = DataNewCombo;
                    }
                    catch
                    {
                        cbo.Properties.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void LoadgrdYeuCauTuyenDung()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListNoiDungPhongVan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iID_YCTD));
            if(grdDSNDPV.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSNDPV, grvDSNDPV, dt, true, true, true, true, true, this.Name);
                grvDSNDPV.Columns["ID_YCTD"].Visible = false;
                grvDSNDPV.Columns["ID_NDPV"].Visible = false;
            }
            else
            {
                grdDSNDPV.DataSource = dt;
            }
        }
        private void LoadgrdFileDinhKem()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListFileDinhKem", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iID_YCTD));
            if(grdFileDK.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdFileDK, grvFileDK, dt, true, true, true, true, true, this.Name);
                grvFileDK.Columns["ID_YCTD"].Visible = false;
                grvFileDK.Columns["ID_VT_FL"].Visible = false;
            }
            else
            {
                grdFileDK.DataSource = dt;
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
                        break;
                    }
                case "sua":
                    {
                        enableButon(false);
                        break;
                    }
                case "xoa":
                    {
                        break;
                    }

                case "luu":
                    {
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

        private void enableButon(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = visible;
            btnALL.Buttons[3].Properties.Visible = visible;
            btnALL.Buttons[4].Properties.Visible = !visible;
            btnALL.Buttons[5].Properties.Visible = !visible;
            btnALL.Buttons[6].Properties.Visible = visible;

            grvThayThe.OptionsBehavior.Editable = !visible;
            grvDSNDPV.OptionsBehavior.Editable = !visible;
            grvFileDK.OptionsBehavior.Editable = !visible;

            txtMA_YCTD.Properties.ReadOnly = visible;
            cboBPYC.Properties.ReadOnly = visible;
            datNgayYC.Properties.ReadOnly = visible;
            cboNguoiYC.Properties.ReadOnly = visible;
            cboViTriYC.Properties.ReadOnly = visible;
            txtNgayNhanDon.Properties.ReadOnly = visible;
            txtSLTuyen.Properties.ReadOnly = visible;
            txtMLDuKien.Properties.ReadOnly = visible;
            datNgayDiLam.Properties.ReadOnly = visible;
            cboTinhTrang.Properties.ReadOnly = visible;
            cboLinhVucTD.Properties.ReadOnly = visible;
            cboLoaiHinhCongViec.Properties.ReadOnly = visible;
            cboKinhNghiemLV.Properties.ReadOnly = visible;
            txtLyDo.Properties.ReadOnly = visible;
            txtHocVan.Properties.ReadOnly = visible;
            txtKinhNghiem.Properties.ReadOnly = visible;
            txtNgoaiNgu.Properties.ReadOnly = visible;
            txtKyNang.Properties.ReadOnly = visible;
            txtTrachNhiem.Properties.ReadOnly = visible;
            txtKhac.Properties.ReadOnly = visible;
        }

        private void LoadCbo()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetComBoTab_YCTD", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                // LoadcboTo
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboBPYC, dt, "ID_TO", "TEN_TO", "TEN_TO");

                // LoadcboNguoiQuen
                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[1].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNguoiYC, dt1, "ID_CN", "HO_TEN", "HO_TEN");

                // LoadcboViTriYeuCau
                DataTable dt2 = new DataTable();
                dt2 = ds.Tables[2].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboViTriYC, dt2, "ID_VTTD", "TEN_VTTD", "TEN_VTTD");

                // LoadcboTinhTrangDuyet
                DataTable dt3 = new DataTable();
                dt3 = ds.Tables[3].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTinhTrang, dt3, "ID_TTD", "TEN_TT_DUYET", "TEN_TT_DUYET");

                // LoadcboLinhVuc
                DataTable dt4 = new DataTable();
                dt4 = ds.Tables[4].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboLinhVucTD, dt4, "ID_NGANH_TD", "TEN_NGANH_TD", "TEN_NGANH_TD");

                // LoadcboLHCV
                DataTable dt5 = new DataTable();
                dt5 = ds.Tables[5].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboLoaiHinhCongViec, dt5, "ID_LHCV", "TEN_LHCV", "TEN_LHCV");

                // LoadcboKNLV
                DataTable dt6 = new DataTable();
                dt6 = ds.Tables[6].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboKinhNghiemLV, dt6, "ID_KNLV", "TEN_KNLV", "TEN_KNLV");
            }
            catch { }
        }
        private void BindingData(bool them)
        {
            
            if(them == true)
            {
                txtMA_YCTD.EditValue = string.Empty;
                cboBPYC.EditValue = -1;
                datNgayYC.EditValue = DateTime.Now;
                cboNguoiYC.EditValue = -1;
                cboViTriYC.EditValue = -1;
                txtNgayNhanDon.EditValue = "";
                txtSLTuyen.EditValue = "";
                txtMLDuKien.EditValue = "";
                datNgayDiLam.EditValue = DateTime.Now;
                cboTinhTrang.EditValue = -1;
                cboLinhVucTD.EditValue = -1;
                cboLoaiHinhCongViec.EditValue = -1;
                cboKinhNghiemLV.EditValue = -1;
                txtLyDo.EditValue = "";
                txtHocVan.EditValue = "";
                txtKinhNghiem.EditValue = "";
                txtNgoaiNgu.EditValue = "";
                txtKyNang.EditValue = "";
                txtTrachNhiem.EditValue = "";
                txtKhac.EditValue = "";

                chkViTriMoi.Checked = false;
                txtLyDoTuyenMoi.EditValue = "";
                chkThayThe.Checked = false;
                txtLyDoThayThe.EditValue = "";
            }
            else // Load data vao text
            {
                try
                {
                    DataTable dt = new DataTable();
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetYeuCauTuyenDung", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iID_YCTD));

                    txtMA_YCTD.EditValue = dt.Rows[0]["MA_YCTD"].ToString();
                    cboBPYC.EditValue = Convert.ToInt64(dt.Rows[0]["ID_TO"]);
                    datNgayYC.EditValue = Convert.ToDateTime(dt.Rows[0]["NGAY_YEU_CAU"]);
                    cboNguoiYC.EditValue = Convert.ToInt64(dt.Rows[0]["ID_CN"]);
                    cboViTriYC.EditValue = Convert.ToInt64(dt.Rows[0]["ID_VTTD"]);
                    txtNgayNhanDon.EditValue = dt.Rows[0]["NGAY_NHAN_DON"].ToString();
                    txtSLTuyen.EditValue = Convert.ToInt32(dt.Rows[0]["SL_TUYEN"]);
                    txtMLDuKien.EditValue = Convert.ToInt32(dt.Rows[0]["MUC_LUONG_DK"]);
                    datNgayDiLam.EditValue = Convert.ToDateTime(dt.Rows[0]["NGAY_DI_LAM"]);
                    cboTinhTrang.EditValue = Convert.ToInt32(dt.Rows[0]["ID_TTD"]);
                    cboLinhVucTD.EditValue = Convert.ToInt64(dt.Rows[0]["ID_NGANH_TD"]);
                    cboLoaiHinhCongViec.EditValue = Convert.ToInt64(dt.Rows[0]["ID_LHCV"]);
                    cboKinhNghiemLV.EditValue = Convert.ToInt64(dt.Rows[0]["ID_KNLV"]);
                    txtLyDo.EditValue = dt.Rows[0]["GHI_CHU"].ToString();
                    txtHocVan.EditValue = dt.Rows[0]["HOC_VAN"].ToString();
                    txtKinhNghiem.EditValue = dt.Rows[0]["KINH_NGHIEM"].ToString();
                    txtNgoaiNgu.EditValue = dt.Rows[0]["NGOAI_NGU"].ToString();
                    txtKyNang.EditValue = dt.Rows[0]["KY_NANG"].ToString();
                    txtTrachNhiem.EditValue = dt.Rows[0]["TRACH_NHIEM"].ToString();
                    txtKhac.EditValue = dt.Rows[0]["KHAC"].ToString();
                    chkViTriMoi.EditValue = Convert.ToBoolean(dt.Rows[0]["VI_TRI_MOI"]);
                    txtLyDoTuyenMoi.EditValue = dt.Rows[0]["LD_TUYEN_MOI"].ToString();
                    chkThayThe.EditValue = Convert.ToBoolean(dt.Rows[0]["THAY_THE"]);
                    txtLyDoThayThe.EditValue = dt.Rows[0]["LD_THAY_THE"].ToString();
                }
                catch { }
            }
        }
        #endregion

        private void txtMA_YCTD_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmDanhSachYCTD frm = new frmDanhSachYCTD();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                iID_YCTD = frm.iID_YCTD;
                BindingData(false);
                LoadgrdThayThe();
                LoadgrdYeuCauTuyenDung();
                LoadgrdFileDinhKem();
            }
        }
    }
}
