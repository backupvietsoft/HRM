using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Vs.Payroll
{
    public partial class frmDonHangBanView : DevExpress.XtraEditors.XtraForm
    {
        private int iPQ = -1;
        private string sSP = "";
        public DataRow Row;
        public frmDonHangBanView(int PQ, string SP)
        {
            InitializeComponent();

            iPQ = PQ;
            sSP = SP;
        }
        #region Event
        private void frmDonHangBanView_Load(object sender, EventArgs e)
        {
            if (datDEN_NGAY.Text == "" || datDEN_NGAY == null)
            {
                datDEN_NGAY.EditValue = DateTime.Now;
            }

            if (datDEN_NGAY.Text != "" && datDEN_NGAY != null)
            {
                //Mặc định lấy 6 tháng kể từ ngày lập trở về trước
                datTU_NGAY.EditValue = Convert.ToDateTime(datDEN_NGAY.EditValue).AddMonths(-6);
            }
            LoadNN();
        }

        private void txtDEN_NGAY_EditValueChanged(object sender, EventArgs e)
        {
            if (datTU_NGAY.Text != "" && datTU_NGAY != null && datDEN_NGAY.Text != "" && datDEN_NGAY != null)
            {
                if (!dxValidationProvider1.Validate()) return;
                dxValidationProvider1.Validate();
                LoadData();
            }
        }

        private void txtTU_NGAY_EditValueChanged(object sender, EventArgs e)
        {
            if (datTU_NGAY.Text != "" && datTU_NGAY != null && datDEN_NGAY.Text != "" && datDEN_NGAY != null)
            {
                LoadData();
            }
        }

        private void grdView_DoubleClick(object sender, EventArgs e)
        {
            Commons.Modules.sId = grvView.GetRowCellValue(grvView.FocusedRowHandle, grvView.Columns["ID_DHB"]).ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Function
        public void LoadNN()
        {
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, windowsUIButton);
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(grvView, this.Name);
        }

        private void LoadData()
        {
            try
            {
                if (datTU_NGAY.Text != "" && datTU_NGAY != null && datDEN_NGAY.Text != "" && datDEN_NGAY != null)
                {
                    System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDonHangBan", conn);
                    cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datTU_NGAY.EditValue);
                    cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datDEN_NGAY.EditValue);
                    cmd.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0].Copy();
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdView, grvView, dt, false, false, true, true, true, this.Name);

                    grvView.Columns[0].Visible = false;
                    grvView.Columns["ID_DT"].Visible = false;
                    grvView.Columns["TRANG_THAI"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            DevExpress.XtraBars.Docking2010.WindowsUIButton btn = e.Button as DevExpress.XtraBars.Docking2010.WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "thoat":
                    {
                        this.DialogResult = DialogResult.Cancel;
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
