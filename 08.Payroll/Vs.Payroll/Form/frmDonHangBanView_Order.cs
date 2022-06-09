using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Vs.Payroll
{
    public partial class frmDonHangBanView_Order : DevExpress.XtraEditors.XtraForm
    {
        static int iPQ = -1;
        private string sSP = "";
        private Int64 iID_DT = -1;
        private List<Int64> iList_ID_BGB = new List<Int64>();
        public DataTable dt_frmDonHangBanView_Order_CTBG;
        private bool bLoadData = false;
        public frmDonHangBanView_Order(int PQ, string SP, Int64 ID_DT)
        {
            iPQ = PQ;
            sSP = SP;
            iID_DT = ID_DT;
            InitializeComponent();
        }

        #region  Event
        private void frmDonHangBanView_Order_Load(object sender, EventArgs e)
        {
            bLoadData = true;
            LoadCbo();
            bLoadData = false;
            LoadData();
            LoadNN();
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            DevExpress.XtraBars.Docking2010.WindowsUIButton btn = e.Button as DevExpress.XtraBars.Docking2010.WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "ghi":
                    {
                        grdChung.MainView.CloseEditor();
                        grvChung.UpdateCurrentRow();

                        string List_ID_HH = "";
                        if (((DataTable)grdChung.DataSource).Select("SO_ORDER > 0").Length > 0)
                        {
                            DataTable dt = new DataTable();
                            dt = ((DataTable)grdChung.DataSource).Select("SO_ORDER > 0").CopyToDataTable().Copy(); ;

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(dt.Rows[i]["SO_ORDER"]) > 0)
                                {
                                    for (int j = 0; j < Convert.ToInt32(dt.Rows[i]["SO_ORDER"]); j++)
                                    {
                                        List_ID_HH += dt.Rows[i]["ID_HH"].ToString() + "?" + dt.Rows[i]["ID_HH"].ToString() + ";";

                                    }
                                }
                            }
                        }
                        string sBT = "HH" + Commons.Modules.UserName;
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, ((DataTable)grdChung.DataSource).Copy(), "");
                        // Lay danh sach hang hoa thong qua danh sach ID_BGB
                        try
                        {
                            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sSP, conn);
                            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 5;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@List_ID_HH", SqlDbType.NVarChar).Value = List_ID_HH;
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            dt_frmDonHangBanView_Order_CTBG = ds.Tables[0].Copy();

                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message);
                        }

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    }
                case "thoat":
                    {
                        this.DialogResult = DialogResult.Cancel;
                        break;
                    }
                default:
                    break;
            }
        }
        private void btnThucHien_Click(object sender, EventArgs e)
        {
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboID_LHH_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        #endregion
        #region  Function
        public void LoadNN()
        {
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, windowsUIButton);
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(grvChung, this.Name);
        }

        private void LoadCbo()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sSP, conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 3;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_LHH, dt, "ID_LHH", "TEN_LHH", this.Name);
                cboID_LHH.Properties.View.Columns["THU_TU"].Visible = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                if (bLoadData) return;
                if (cboID_LHH.EditValue == null)
                {
                    XtraMessageBox.Show(lblID_LHH.Text + " " + Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgKhongDuocTrong"));
                    cboID_LHH.Focus();
                    return;
                }

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sSP, conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 4;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_DT", SqlDbType.BigInt).Value = iID_DT;
                cmd.Parameters.Add("@ID_LHH", SqlDbType.Int).Value = cboID_LHH.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, true, false, true, false, true, this.Name);
                grvChung.Columns["ID_HH"].Visible = false;

                for (int i = 1; i < grvChung.Columns.Count; i++)
                {
                    grvChung.Columns[i].OptionsColumn.AllowEdit = false;
                }
                grvChung.Columns["SO_ORDER"].OptionsColumn.AllowEdit = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        #endregion

    }
}