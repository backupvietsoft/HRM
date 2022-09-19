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
    public partial class frmDSPhongVan : DevExpress.XtraEditors.XtraForm
    {
        public Int64 iIDPV = -1;
        public frmDSPhongVan()
        {
            InitializeComponent();
        }
        private void LoadNN()
        {
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, btnALL);
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(grvPhongVan, this.Name);
            rdoTinhTrang.Properties.Items[0].Description = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "rdoDangSoan");
            rdoTinhTrang.Properties.Items[1].Description = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "rdoDangThucHien");
            rdoTinhTrang.Properties.Items[2].Description = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "rdoDaKetThuc");
        }
        private void frmDSPhongVan_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            datNgayLapTNgay.EditValue = DateTime.Now.AddMonths(-1);
            datDNgay.EditValue = DateTime.Now;
            Commons.Modules.sLoad = "";
            rdoTinhTrang.SelectedIndex = 1;
            LoadData();
            LoadNN();
        }
        private void datNgayLapTNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (datNgayLapTNgay.Text != "" && datNgayLapTNgay != null && datDNgay.Text != "" && datDNgay != null)
            {
                LoadData();
            }
        }
        private void datDNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (!dxValidationProvider1.Validate()) return;
            dxValidationProvider1.Validate();
            if (datNgayLapTNgay.Text != "" && datNgayLapTNgay != null && datDNgay.Text != "" && datDNgay != null)
            {
                LoadData();
            }
        }

        private void grvPhongVan_DoubleClick(object sender, EventArgs e)
        {
            if (grvPhongVan.RowCount == 0) return;
            iIDPV = Convert.ToInt64(grvPhongVan.GetRowCellValue(grvPhongVan.FocusedRowHandle, grvPhongVan.Columns["ID_PV"]));
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            DevExpress.XtraBars.Docking2010.WindowsUIButton btn = e.Button as DevExpress.XtraBars.Docking2010.WindowsUIButton;
            DevExpress.XtraEditors.XtraUserControl ctl = new DevExpress.XtraEditors.XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
                default:
                    break;
            }
        }
        #region function
        private void LoadData()
        {
            try
            {
                DataTable dt = new DataTable();

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spPhongVan", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 3;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@TINH_TRANG", SqlDbType.Int).Value = rdoTinhTrang.EditValue;
                cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = datNgayLapTNgay.EditValue;
                cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDNgay.EditValue;

                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0].Copy();
                if (grdPhongVan.DataSource == null)
                {

                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdPhongVan, grvPhongVan, dt, false, true, false, false, false, this.Name);
                    grvPhongVan.Columns["ID_PV"].Visible = false;
                    grvPhongVan.Columns["ID_TB"].Visible = false;
                }
                else
                {
                    grdPhongVan.DataSource = dt;
                }
                //DataTable dt_TinhTrang = new DataTable();
                //dt_TinhTrang.Columns.Add("TINH_TRANG", typeof(int));
                //dt_TinhTrang.Columns.Add("sTINH_TRANG", typeof(string));

                //DataRow dr = dt_TinhTrang.NewRow();
                //dr["TINH_TRANG"] = 1;
                //dr["sTINH_TRANG"] = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "ttDangSoan");
                //dt_TinhTrang.Rows.Add(dr);

                //dr = dt_TinhTrang.NewRow();
                //dr["TINH_TRANG"] = 2;
                //dr["sTINH_TRANG"] = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "ttDangThucHien");
                //dt_TinhTrang.Rows.Add(dr);

                //dr = dt_TinhTrang.NewRow();
                //dr["TINH_TRANG"] = 3;
                //dr["sTINH_TRANG"] = Commons.Modules.ObjLanguages.GetLanguage("frmChung", "ttDaKetThuc");
                //dt_TinhTrang.Rows.Add(dr);

                //dt_TinhTrang.AcceptChanges();

                //DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                //Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "TINH_TRANG", "sTINH_TRANG", grvPhongVan, dt_TinhTrang);
            }
            catch { }
        }
        #endregion

        private void rdoTinhTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sId == "0Load") return;
            LoadData();
        }

        private void grvPhongVan_MouseWheel(object sender, MouseEventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.GridView view = (sender as DevExpress.XtraGrid.Views.Grid.GridView);
            //view.LeftCoord += e.Delta;
            //(e as DevExpress.Utils.DXMouseEventArgs).Handled = true;
        }
    }
}
