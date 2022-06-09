using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
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
    public partial class frmXemDSUngVien : DevExpress.XtraEditors.XtraForm
    {
        Int64 iID_TB = -1;
        DataTable dt_CHON;
        public string sSO_TB = "";
        private ucCTQLUV ucUV;

        public frmXemDSUngVien()
        {
            InitializeComponent();
        }

        #region even
        private void frmXemDSUngVien_Load(object sender, EventArgs e)
        {
            txtID_TB.Text = sSO_TB;
            txtID_TB.ReadOnly = true;
            LoadData();

            LoadNN();
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
                            try
                            {
                                if (!ChonUngVien()) return;

                                UAC.ctUngVien.frmUpdateTTUV frm = new UAC.ctUngVien.frmUpdateTTUV();
                                if(frm.ShowDialog() == DialogResult.OK)
                                {
                                    string sBT_grvDSUv = "sBT_grvDSUv" + Commons.Modules.UserName;
                                    if (dt_CHON == null || dt_CHON.Rows.Count == 0)
                                    {
                                        sBT_grvDSUv = "";
                                    }
                                    string chuoiIDUV_tmp = "";
                                    for (int i = 0; i < dt_CHON.Rows.Count; i++)
                                    {
                                        chuoiIDUV_tmp += dt_CHON.Rows[i]["ID_UV"].ToString() + ",";
                                    }
                                    string chuoiIDUV = chuoiIDUV_tmp.Remove(chuoiIDUV_tmp.Length - 1);
                                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT_grvDSUv, dt_CHON, "");
                                    System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                    conn.Open();
                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTBTuyenDung", conn);
                                    cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 13;
                                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                    cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT_grvDSUv;
                                    cmd.Parameters.Add("@ID_TT_HD", SqlDbType.BigInt).Value = frm.iIDTTHD;
                                    cmd.Parameters.Add("@ID_TT_HT", SqlDbType.BigInt).Value = frm.iIDTTHT;
                                    cmd.Parameters.Add("@CHUOI_ID", SqlDbType.NVarChar).Value = chuoiIDUV;

                                    cmd.CommandType = CommandType.StoredProcedure;
                                    if (Convert.ToInt32(cmd.ExecuteScalar()) == 1)
                                    {
                                        LoadData();
                                    }
                                }
                            }
                            catch
                            { }
                        }
                        break;
                    case "thoat":
                        {
                            this.Close();
                            break;
                        }
                }
            }
            catch { }
        }

        public void BackWindowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            ucUV.Hide();
            tablePanel1.Show();
            LoadData();

            // khi click nút trở lại, vẫn dữ lại được những dòng đã chọn
            DataTable dt = new DataTable();
            dt = (DataTable)grdDSUV.DataSource;
            try
            {
                dt.AsEnumerable().Where(row => dt_CHON.AsEnumerable()
                                                         .Select(r => r.Field<Int64>("ID_UV"))
                                                         .Any(x => x == row.Field<Int64>("ID_UV"))
                                                         ).ToList<DataRow>().ForEach(y => y["CHON"] = 1);
                dt.AcceptChanges();
            }
            catch { }

        }
        private void grvDSUV_DoubleClick(object sender, EventArgs e)
        {
            if (grvDSUV.RowCount < 1)
            {
                return;
            }
            ucUV = new ucCTQLUV(Convert.ToInt64(grvDSUV.GetFocusedRowCellValue("ID_UV")));
            Commons.Modules.ObjSystems.ShowWaitForm(this);
            ucUV.Refresh();

            // dt_CHON  nay de luu du lieu da~ chon lai,trươc khi double click
            dt_CHON = new DataTable();
            DataTable dt_temp = ((DataTable)grdDSUV.DataSource);
            try
            {
                if (dt_temp.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).Count() > 0)
                {
                    dt_CHON = dt_temp.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).CopyToDataTable().Copy();
                }
            }
            catch
            {
                //Trong truong hop ma no where khong ra thi no se bi catch, nen cho nay minh dung Clone()
                dt_CHON = dt_temp.Clone();
            }


            //ns.accorMenuleft = accorMenuleft;
            tablePanel1.Hide();
            this.Controls.Add(ucUV);
            ucUV.Dock = DockStyle.Fill;
            ucUV.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
            Commons.Modules.ObjSystems.HideWaitForm();
        }
        #endregion

        #region function
        private void LoadData()
        {
            try
            {
                //Select Tung UngVien
                DataTable dt = new DataTable();

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTBTuyenDung", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 12;
                cmd.Parameters.Add("@ID_TB", SqlDbType.BigInt).Value = iID_TB;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0].Copy();
                if (grdDSUV.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSUV, grvDSUV, dt, false, true, false, false, false, this.Name);
                    grvDSUV.Columns["ID_UV"].Visible = false;
                    grvDSUV.Columns["CHON"].Visible = false;
                    grvDSUV.Columns["HINH_UV"].Visible = false;
                    grvDSUV.Columns["ID_QG"].Visible = false;
                    grvDSUV.Columns["HINH_THUC_TUYEN"].Visible = false;
                    grvDSUV.Columns["GHI_CHU"].Visible = false;
                    grvDSUV.Columns["ID_DT"].Visible = false;
                    grvDSUV.Columns["TON_GIAO"].Visible = false;
                    grvDSUV.Columns["NOI_SINH"].Visible = false;
                    grvDSUV.Columns["NGUYEN_QUAN"].Visible = false;
                    grvDSUV.Columns["SO_CMND"].Visible = false;
                    grvDSUV.Columns["NGAY_CAP"].Visible = false;
                    grvDSUV.Columns["NOI_CAP"].Visible = false;
                    grvDSUV.Columns["ID_TT_HN"].Visible = false;
                    grvDSUV.Columns["LD_NN"].Visible = false;
                    grvDSUV.Columns["DT_DI_DONG"].Visible = false;
                    grvDSUV.Columns["DT_NHA"].Visible = false;
                    grvDSUV.Columns["DT_NGUOI_THAN"].Visible = false;
                    grvDSUV.Columns["EMAIL"].Visible = false;
                    grvDSUV.Columns["DIA_CHI_THUONG_TRU"].Visible = false;
                    grvDSUV.Columns["ID_TP"].Visible = false;
                    grvDSUV.Columns["ID_QUAN"].Visible = false;
                    grvDSUV.Columns["ID_PX"].Visible = false;
                    grvDSUV.Columns["THON_XOM"].Visible = false;
                    grvDSUV.Columns["DIA_CHI_TAM_TRU"].Visible = false;
                    grvDSUV.Columns["ID_TP_TAM_TRU"].Visible = false;
                    grvDSUV.Columns["ID_QUAN_TAM_TRU"].Visible = false;
                    grvDSUV.Columns["ID_PX_TAM_TRU"].Visible = false;
                    grvDSUV.Columns["THON_XOM_TAM_TRU"].Visible = false;
                    grvDSUV.Columns["ID_TDVH"].Visible = false;
                    grvDSUV.Columns["ID_LOAI_TD"].Visible = false;
                    grvDSUV.Columns["CHUYEN_MON"].Visible = false;
                    grvDSUV.Columns["ID_TO"].Visible = false;
                }
                else
                {
                    grdDSUV.DataSource = dt;
                }
                grvDSUV.OptionsSelection.CheckBoxSelectorField = "CHON";
                grvDSUV.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            }
            catch { }
        }

        private bool ChonUngVien()
        {
            try
            {
                dt_CHON = new DataTable();
                DataTable dt_temp = ((DataTable)grdDSUV.DataSource);
                if (dt_temp.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).Count() > 0)
                {
                    dt_CHON = dt_temp.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).CopyToDataTable().Copy();
                }
                else
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChuaChonUV"));
                    return false;
                }
            }
            catch
            {
                //Trong truong hop ma no where khong ra thi no se bi catch, nen cho nay minh dung Clone()
                dt_CHON = ((DataTable)grdDSUV.DataSource);
            }
            return true;
        }

        private void LoadNN()
        {
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, btnALL);
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(grvDSUV, this.Name);
        }
        #endregion

        private void grvDSUV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (!ChonUngVien()) return;
                    if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Xoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        for (int i = 0; i < dt_CHON.Rows.Count; i++)
                        {
                            String sSql = "DELETE FROM dbo.UNG_VIEN_TB_TUYEN_DUNG WHERE ID_UV IN (" + dt_CHON.Rows[i]["ID_UV"] + ") AND ID_TB = " + iID_TB + "";
                            Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                            grvDSUV.DeleteSelectedRows();
                        }

                    }
                    else
                        return;
                    ((DataTable)grdDSUV.DataSource).AcceptChanges();
                }
                catch { }
            }
        }

        private void grvDSUV_MouseWheel(object sender, MouseEventArgs e)
        {
            //grvDSUV.OptionsView.ColumnAutoWidth = false;

            //grvDSUV.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveHorzScroll;
            //grvDSUV.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;

            //DevExpress.XtraGrid.Views.Grid.GridView view = (sender as DevExpress.XtraGrid.Views.Grid.GridView);
            //view.LeftCoord += e.Delta;
            //(e as DevExpress.Utils.DXMouseEventArgs).Handled = true;
        }
      
    }
}
