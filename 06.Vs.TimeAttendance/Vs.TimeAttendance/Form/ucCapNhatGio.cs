using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraLayout;
using System.Threading;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;

namespace Vs.HRM
{
    public partial class ucCapNhatGio : DevExpress.XtraEditors.XtraUserControl
    {
        public static ucCapNhatGio _instance;
        public static ucCapNhatGio Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucCapNhatGio();
                return _instance;
            }
        }


        public ucCapNhatGio()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, windowsUIButton);
        }
        RepositoryItemTimeEdit repositoryItemTimeEdit1;

        #region Cập nhật giờ
        private void ucCapNhatGio_Load(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            Commons.Modules.sLoad = "0Load";

            repositoryItemTimeEdit1 = new RepositoryItemTimeEdit();
            repositoryItemTimeEdit1.TimeEditStyle = TimeEditStyle.TouchUI;
            repositoryItemTimeEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            repositoryItemTimeEdit1.Mask.EditMask = "HH:mm:ss";

            repositoryItemTimeEdit1.NullText = "00:00:00";
            repositoryItemTimeEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemTimeEdit1.DisplayFormat.FormatString = "HH:mm:ss";
            repositoryItemTimeEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemTimeEdit1.EditFormat.FormatString = "HH:mm:ss";

            Commons.OSystems.SetDateEditFormat(dTuNgay);
            Commons.OSystems.SetDateEditFormat(dDenNgay);

            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);

            Commons.Modules.sLoad = "";
            dTuNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year));
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadData();
            Commons.Modules.sLoad = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadData();
            Commons.Modules.sLoad = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }
        private bool kiemtrangay()
        {
            DateTime t = Convert.ToDateTime(dTuNgay.EditValue);
            DateTime d = Convert.ToDateTime(dDenNgay.EditValue);
            if (t > d)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TuNgayDenNgay"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                dDenNgay.Focus();
                return false;
            }
            return true;
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "chamtudong":
                    {
                        try
                        {
                            if (!kiemtrangay()) return;

                            int iDay = 0;

                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            string sBT = "sBTCapNhatGio" + Commons.Modules.iIDUser;
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                            for (DateTime dt = Convert.ToDateTime(dTuNgay.EditValue); dt <= Convert.ToDateTime(dDenNgay.EditValue); dt = dt.AddDays(1))
                            {
                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spAutoUpdateTimekeeping", conn);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@UName", Commons.Modules.UserName);
                                cmd.Parameters.AddWithValue("@NNgu", Commons.Modules.TypeLanguage);
                                cmd.Parameters.AddWithValue("@ID_DV", cboDV.EditValue);
                                cmd.Parameters.AddWithValue("@ID_XN", cboXN.EditValue);
                                cmd.Parameters.AddWithValue("@ID_TO", cboTo.EditValue);
                                cmd.Parameters.AddWithValue("@sBT1", sBT);
                                cmd.Parameters.AddWithValue("@DDate", dt);
                                cmd.ExecuteNonQuery();
                            }
                            Commons.Modules.ObjSystems.XoaTable(sBT);
                            //XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_CapNhatThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        catch (Exception ex)
                        {
                            Commons.Modules.ObjSystems.XoaTable("sBTCapNhatGio" + Commons.Modules.iIDUser);
                        }
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
            }
        }
        private void grvData_RowCountChanged(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                int index = ItemForSumNhanVien.Text.IndexOf(':');
                if (index > 0)
                {
                    if (view.RowCount > 0)
                    {
                        ItemForSumNhanVien.Text = ItemForSumNhanVien.Text.Substring(0, index) + ": " + view.RowCount.ToString();
                    }
                    else
                    {
                        ItemForSumNhanVien.Text = ItemForSumNhanVien.Text.Substring(0, index) + ": 0";
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        #endregion

        private void LoadData()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spCapNhatGioTuDong", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDV.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXN.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(dTuNgay.Text);
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(dDenNgay.Text);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, true, false, true, true, this.Name);
                    grvData.Columns["ID_CN"].Visible = false;

                    grvData.Columns["GIO_DEN"].ColumnEdit = repositoryItemTimeEdit1;
                    grvData.Columns["GIO_VE"].ColumnEdit = repositoryItemTimeEdit1;

                    grvData.Columns["GIO_DEN"].DisplayFormat.FormatType = FormatType.DateTime;
                    grvData.Columns["GIO_DEN"].DisplayFormat.FormatString = "HH:mm:ss";

                    grvData.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["NGAY"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["GIO_DEN"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["GIO_VE"].OptionsColumn.AllowEdit = false;

                }
                else
                {
                    grdData.DataSource = dt;
                }
            }
            catch { }
        }

        private void dTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.ConvertDateTime(dTuNgay.Text);
            int t = DateTime.DaysInMonth(dTuNgay.DateTime.Year, dTuNgay.DateTime.Month);
            DateTime secondDateTime = new DateTime(dTuNgay.DateTime.Year, dTuNgay.DateTime.Month, t);
            dDenNgay.EditValue = secondDateTime;
            LoadData();
            Commons.Modules.sLoad = "";
        }

        private void dDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }
    }
}
