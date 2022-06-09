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
using Microsoft.ApplicationBlocks.Data;

namespace Vs.Payroll
{
    public partial class frmQTCN_View : DevExpress.XtraEditors.XtraForm
    {
        private Int64 iID_CUM = -1;
        private Int64 iID_CHUYEN = -1;
        private Int64 iID_ORD = -1;

        public DataTable dt_frmQTCN_View;

        int maxTT = 0; // tìm max thứ tự của frmQTCN
        public frmQTCN_View(Int64 id_cum, Int64 id_chuyen, Int64 id_ord)
        {
            iID_CUM = id_cum;
            iID_CHUYEN = id_chuyen;
            iID_ORD = id_ord;
            InitializeComponent();
        }

        #region  Event
        private void frmQTCN_View_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadCbo();
            Commons.Modules.sLoad = "";
            cboID_CUM.EditValue = iID_CUM;
            cboID_CUM.ReadOnly = true;
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

                        dt_frmQTCN_View = new DataTable();
                        DataTable dt_temp = new DataTable();
                        dt_temp = (DataTable)grdChung.DataSource;
                        try
                        {
                            if (dt_temp.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).Count() > 0)
                            {
                                dt_frmQTCN_View = dt_temp.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).CopyToDataTable().Copy();
                            }
                            else
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChuaChonCD"));
                                return;
                            }
                        }
                        catch
                        {
                            //Trong truong hop ma no where khong ra thi no se bi catch, nen cho nay minh dung Clone()
                            dt_frmQTCN_View = dt_temp.Clone();
                        }
                        this.DialogResult = DialogResult.OK;
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

        private void chkChon_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit check = sender as CheckEdit;
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
                DataTable dt_cum = new DataTable();
                dt_cum.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCUM", -1, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_CUM, dt_cum, "ID_CUM", "TEN_CUM", "TEN_CUM", true, false);
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
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetCongDoan", conn);
                cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_CUM", SqlDbType.BigInt).Value = cboID_CUM.EditValue;
                cmd.Parameters.Add("@ID_CHUYEN", SqlDbType.BigInt).Value = iID_CHUYEN;
                cmd.Parameters.Add("@ID_ORD", SqlDbType.BigInt).Value = iID_ORD;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();

                try
                {
                    var rows = dt.AsEnumerable().Where(x => !dt_frmQTCN_View.AsEnumerable().Any(x1 => x["ID_CD"].ToString().Equals(x1["ID_CD"].ToString())));
                    if (rows.Any())
                    {
                        dt = rows.CopyToDataTable();
                    }
                    else
                        dt.Clear();


                    //dt = dt.AsEnumerable().Where(x => !dt_CHON.AsEnumerable().Any(x1 => x["ID_UV"].ToString().Equals(x1["ID_UV"].ToString()))).CopyToDataTable();
                }
                catch (Exception ex) { }


                if (grdChung.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, true, false, true, true, true, this.Name);
                    grvChung.Columns["ID_CD"].Visible = false;
                    grvChung.Columns["ID_BT"].Visible = false;
                    grvChung.Columns["ID_LM"].Visible = false;
                    grvChung.Columns["ID_CUM"].Visible = false;
                    grvChung.OptionsSelection.CheckBoxSelectorField = "CHON";
                    grvChung.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                    for (int i = 0; i < grvChung.Columns.Count; i++)
                    {
                        grvChung.Columns["MS_CD"].OptionsColumn.AllowEdit = false;
                        grvChung.Columns["TEN_CD"].OptionsColumn.AllowEdit = false;
                        grvChung.Columns["TEN_BAC_THO"].OptionsColumn.AllowEdit = false;
                        grvChung.Columns["TGTK"].OptionsColumn.AllowEdit = false;
                        grvChung.Columns["ID_LM"].OptionsColumn.AllowEdit = false;
                        grvChung.Columns["TEN_LOAI_MAY"].OptionsColumn.AllowEdit = false;
                    }
                }
                else
                {
                    grdChung.DataSource = dt;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void cboID_CUM_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }

        private void grvChung_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)grdChung.DataSource;
                int index = Convert.ToInt32(grvChung.GetFocusedDataSourceRowIndex());

                if (Convert.ToBoolean(e.Value) == true)
                {


                    //Tìm max trên lưới frmQTCN
                    //for (int i = 0; i < dt_frmQTCN_View.Rows.Count; i++)
                    //{
                    //    if (Convert.ToInt32(string.IsNullOrEmpty(dt_frmQTCN_View.Rows[i]["THU_TU_CONG_DOAN"].ToString()) ? 0 : Convert.ToInt32(dt_frmQTCN_View.Rows[i]["THU_TU_CONG_DOAN"])) > maxTT)
                    //    {
                    //        maxTT = string.IsNullOrEmpty(dt_frmQTCN_View.Rows[i]["THU_TU_CONG_DOAN"].ToString()) ? 0 : Convert.ToInt32(dt_frmQTCN_View.Rows[i]["THU_TU_CONG_DOAN"]);
                    //    }
                    //}

                    if (dt_frmQTCN_View.Rows.Count == 0)
                    {
                        maxTT = 0;
                    }
                    else
                    {
                        maxTT = string.IsNullOrEmpty(dt_frmQTCN_View.Rows[dt_frmQTCN_View.Rows.Count -1]["THU_TU_CONG_DOAN"].ToString()) ? 0 : Convert.ToInt32(dt_frmQTCN_View.Rows[dt_frmQTCN_View.Rows.Count -1]["THU_TU_CONG_DOAN"]);
                    }

                    //Tìm max trên lưới frmQTCN_View
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(string.IsNullOrEmpty(dt.Rows[i]["THU_TU_CONG_DOAN"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[i]["THU_TU_CONG_DOAN"])) > maxTT)
                        {
                            maxTT = string.IsNullOrEmpty(dt.Rows[i]["THU_TU_CONG_DOAN"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[i]["THU_TU_CONG_DOAN"]);
                        }
                    }

                    if (maxTT == 0)
                    {
                        System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                        conn.Open();
                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("SELECT dbo.fnGetMaQL(@ID_Chuyen,@ID_Ord)", conn);
                        cmd.Parameters.AddWithValue("@ID_Chuyen", iID_CHUYEN);
                        cmd.Parameters.AddWithValue("@ID_Ord", iID_ORD);
                        maxTT = Convert.ToInt32(cmd.ExecuteScalar());
                        dt.Rows[index]["THU_TU_CONG_DOAN"] = maxTT;
                        dt.Rows[index]["MA_QL"] = maxTT;
                        dt.AcceptChanges();
                    }
                    else
                    {
                        dt.Rows[index]["THU_TU_CONG_DOAN"] = maxTT + 1;
                        dt.Rows[index]["MA_QL"] = maxTT + 1;
                        dt.AcceptChanges();
                    }
                    //if (dt.Rows[index]["THU_TU_CONG_DOAN"].ToString() == "")
                    //{

                    //}
                    //return;
                }
                else
                {
                    dt.Rows[index]["THU_TU_CONG_DOAN"] = 0;
                    dt.Rows[index]["MA_QL"] = 0;
                    dt.AcceptChanges();
                }
            }
            catch { }
        }

        private void tangThuTu()
        {
            try
            {

            }
            catch
            {

            }
        }
        private void grvChung_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }
    }
}