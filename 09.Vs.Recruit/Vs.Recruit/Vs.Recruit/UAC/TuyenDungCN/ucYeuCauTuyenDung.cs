using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;

namespace Vs.Recruit.UAC
{
    public partial class ucYeuCauTuyenDung : DevExpress.XtraEditors.XtraUserControl
    {
        private Int64 iID_YCTD, iID_VTTD = 0;
        public ucYeuCauTuyenDung()
        {
            InitializeComponent();
        }
        #region even
        private void ucYeuCauTuyenDung_Load(object sender, EventArgs e)
        {
            datTuNgay.DateTime = DateTime.Now.Date.AddDays(-DateTime.Now.Date.Day + 1);
            datDenNgay.DateTime = datTuNgay.DateTime.AddMonths(1).AddDays(-1);
            LoadCbo();
            LoadgrdPYC();
            LoadgrdViTri();
            LoadgrdThayThe();
            BindingData(false);
            enableButon(true);
        }
        private void LoadgrdPYC()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListPhieuYeuCau", datTuNgay.DateTime, datDenNgay.DateTime, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                if (grdPYC.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdPYC, grvPYC, dt, false, false, true, true, true, this.Name);
                    grvPYC.Columns["ID_YCTD"].Visible = false;
                    grvPYC.Columns["ID_TO"].Visible = false;
                    grvPYC.Columns["ID_CN"].Visible = false;
                    grvPYC.Columns["NGAY_YEU_CAU"].Visible = false;
                    grvPYC.Columns["NGAY_NHAN_DON"].Visible = false;
                    grvPYC.Columns["GHI_CHU"].Visible = false;
                }
                else
                {
                    grdPYC.DataSource = dt;
                }
            }
            catch
            {
            }
        }
        private void LoadgrdViTri()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListYCTDVT", iID_YCTD, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_YCTD"] };
                if (grdViTri.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdViTri, grvViTri, dt, true, false, false, true, true, this.Name);
                    grvViTri.Columns["ID_YCTD"].Visible = false;
                    Commons.Modules.ObjSystems.AddCombXtra("ID_LCV", "TEN_LCV", grvViTri, Commons.Modules.ObjSystems.DataLoaiCV(false), true, "ID_LCV", this.Name, true);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_NGANH_TD", "TEN_NGANH_TD", grvViTri, Commons.Modules.ObjSystems.DataNganhTD(false), false, "ID_NGANH_TD", this.Name, true);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_LHCV", "TEN_LHCV", grvViTri, Commons.Modules.ObjSystems.DataLoaiHinhCV(false), false, "ID_LHCV", this.Name, true);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_KNLV", "TEN_KNLV", grvViTri, Commons.Modules.ObjSystems.DataKinhNghiemLV(false), false, "ID_KNLV", this.Name, true);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_LOAI_TUYEN", "TEN_LOAI_TUYEN", grvViTri, Commons.Modules.ObjSystems.DataLoaiTuyen(false), false, "ID_LOAI_TUYEN", this.Name, true);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_TTD", "TEN_TT_DUYET", grvViTri, Commons.Modules.ObjSystems.DataTinhTrangDuyet(false), false, "ID_TTD", this.Name, true);
                }
                else
                {
                    grdViTri.DataSource = dt;
                }

                if(iID_YCTD != -1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(iID_YCTD));
                    grvViTri.FocusedRowHandle = grvViTri.GetRowHandle(index);
                }

            }
            catch
            {
            }
        }
        private void LoadgrdThayThe()
        {
            try
            {
                iID_VTTD = Convert.ToInt64(grvViTri.GetFocusedRowCellValue("ID_VTTD"));
            }
            catch
            {
                iID_VTTD = -1;
            }
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListYCTDThayThe", iID_YCTD, iID_VTTD, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                if (grdThayThe.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdThayThe, grvThayThe, dt, false, false, true, true, true, this.Name);
                    grvThayThe.Columns["ID_YCTD"].Visible = false;
                    grvThayThe.Columns["ID_VTTD"].Visible = false;

                    Commons.Modules.ObjSystems.AddCombXtra("ID_CN", "TEN_CN", grvThayThe, Commons.Modules.ObjSystems.DataCongNhan(false), true, "ID_CN", this.Name, true);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_LCV", "TEN_LCV", grvThayThe, Commons.Modules.ObjSystems.DataLoaiCV(false), true, "ID_LCV", this.Name, true);
                }
                else
                {
                    grdThayThe.DataSource = dt;
                }
            }
            catch
            {
            }
        }

        private void LoadgrdFileDinhKem()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListFileDinhKem", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iID_YCTD));
            if (grdFileDK.DataSource == null)
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
                        Commons.Modules.ObjSystems.AddnewRow(grvViTri, true);
                        Commons.Modules.ObjSystems.AddnewRow(grvThayThe, true);
                        enableButon(false);
                        BindingData(true);
                        break;
                    }
                case "sua":
                    {
                        Commons.Modules.ObjSystems.AddnewRow(grvViTri, true);
                        Commons.Modules.ObjSystems.AddnewRow(grvThayThe, true);
                        enableButon(false);
                        break;
                    }
                case "xoa":
                    {
                        break;
                    }

                case "luu":
                    {
                        Commons.Modules.ObjSystems.DeleteAddRow(grvViTri);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvThayThe);
                        break;
                    }
                case "khongluu":
                    {
                        Commons.Modules.ObjSystems.DeleteAddRow(grvViTri);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvThayThe);
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
            grvViTri.OptionsBehavior.Editable = !visible;
            grvFileDK.OptionsBehavior.Editable = !visible;

            txtMA_YCTD.Properties.ReadOnly = visible;
            cboBPYC.Properties.ReadOnly = visible;
            datNgayYC.Properties.ReadOnly = visible;
            cboNguoiYC.Properties.ReadOnly = visible;
            cboNgayNhanDon.Properties.ReadOnly = visible;
            txtLyDo.Properties.ReadOnly = visible;
        }
        private void LoadCbo()
        {
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNguoiYC, Commons.Modules.ObjSystems.DataCongNhan(false),"ID_CN","TEN_CN","TEN_CN",true,true);

            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboBPYC, Commons.Modules.ObjSystems.DataTo(-1,-1,false), "ID_TO", "TEN_TO", "TEN_TO", true, true);

        }
        private void BindingData(bool them)
        {

            if (them == true)
            {
                txtMA_YCTD.EditValue = string.Empty;
                cboBPYC.EditValue = -1;
                datNgayYC.EditValue = DateTime.Now;
                cboNguoiYC.EditValue = -1;
                cboNgayNhanDon.EditValue = "";
                txtLyDo.EditValue = "";
            }
            else // Load data vao text
            {
                try
                {
                    DataTable dt = new DataTable();
                    //dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetYeuCauTuyenDung", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iID_YCTD));

                    txtMA_YCTD.EditValue = dt.Rows[0]["MA_YCTD"].ToString();
                    cboBPYC.EditValue = Convert.ToInt64(dt.Rows[0]["ID_TO"]);
                    datNgayYC.EditValue = Convert.ToDateTime(dt.Rows[0]["NGAY_YEU_CAU"]);
                    cboNguoiYC.EditValue = Convert.ToInt64(dt.Rows[0]["ID_CN"]);
                    cboNgayNhanDon.EditValue = dt.Rows[0]["NGAY_NHAN_DON"].ToString();
                    txtLyDo.EditValue = dt.Rows[0]["GHI_CHU"].ToString();
                }
                catch { }
            }
        }
        #endregion

        private void grvViTri_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvViTri_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
    }
}
