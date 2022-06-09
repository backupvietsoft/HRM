using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
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
using Vs.Report;

namespace Vs.Payroll.Form
{
    public partial class frmThuaThieuSL : DevExpress.XtraEditors.XtraForm
    {
        public Int64 iID_DHB = -1;
        public Int64 iID_MH = -1;
        public Int64 iID_CHUYEN = -1;
        public Int64 iID_CHUYEN_SD = -1;
        public Int64 iID_ORD = -1;
        public int slChot = 0;
        public DateTime Ngay = DateTime.Now;

        public Int64 iID_CD_TMP = -1;

        public frmThuaThieuSL()
        {
            InitializeComponent();
        }

        #region even
        private void frmThuaThieuSL_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadCboChuyen();
            cboID_CHUYEN.EditValue = iID_CHUYEN;
            //LoadCboHD(0);

            LoadcboDHB();

            cboID_DHB.EditValue = iID_DHB;
            cboID_MH.EditValue = iID_MH;
            cboID_ORD.EditValue = iID_ORD;

            LoadgrvCDThuaThieu();
            LoadgrvCN();
            Commons.Modules.sLoad = "";

            grvCDThuaThieu_FocusedRowChanged(null, null);

            enableButon(true);
            LoadNN();
            lblSLChot.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "SL_chot : ") + slChot.ToString("N0");
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {

                    case "sua":
                        {
                            enableButon(false);
                            break;
                        }

                    case "in":
                        {
                            InDuLieu();
                            break;
                        }

                    case "ghi":
                        {
                            try
                            {

                                grdCNThucHien.MainView.CloseEditor();
                                grvCNThucHien.UpdateCurrentRow();
                                iID_CD_TMP = Convert.ToInt64(grvCNThucHien.GetFocusedRowCellValue("ID_CD"));

                                string sBT_grvCNThucHien = "sBT_grvCNThucHien" + Commons.Modules.UserName;
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT_grvCNThucHien, Commons.Modules.ObjSystems.ConvertDatatable(grdCNThucHien), "");
                                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();
                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spUpdatePCD", conn);
                                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT_grvCNThucHien;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.ExecuteNonQuery();
                                LoadgrvCDThuaThieu();
                                enableButon(true);
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message); }
                            break;
                        }

                    case "khongghi":
                        {
                            enableButon(true);
                            break;
                        }
                    case "thoat":
                        {
                            if (iID_CD_TMP != -1)
                            {
                                DialogResult = DialogResult.OK;
                            }
                            this.Close();
                            break;
                        }

                    default: break;
                }
            }
            catch
            {

            }
        }


        private void cboID_DHB_EditValueChanged(object sender, EventArgs e)
        {
            //LoadCboHD(2);
            LoadcboHH();
            LoadcboORD();
            Commons.Modules.sLoad = "";
        }

        private void cboID_MH_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            //LoadCboHD(3);
            LoadcboORD();
            LoadSLChot();
            LoadgrvCDThuaThieu();
            LoadgrvCN();
            grvCDThuaThieu_FocusedRowChanged(null, null);
        }

        private void cboID_ORD_EditValueChanged(object sender, EventArgs e)
        {
            LoadgrvCDThuaThieu();
        }
        private void cboID_CHUYEN_EditValueChanged(object sender, EventArgs e)
        {

            LoadSLChot();
            LoadgrvCDThuaThieu();
            LoadgrvCN();
            grvCDThuaThieu_FocusedRowChanged(null, null);
        }

        private void grvCDThuaThieu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            DataTable dtTmp = new DataTable();
            String sIDCD;
            try
            {
                dtTmp = (DataTable)grdCNThucHien.DataSource;

                string sDK = "";
                sIDCD = "-1";
                try { sIDCD = grvCDThuaThieu.GetFocusedRowCellValue("ID_CD").ToString(); } catch (Exception ex) { }
                if (sIDCD != "-1") sDK = " ID_CD = '" + sIDCD + "' ";

                dtTmp.DefaultView.RowFilter = sDK;
            }
            catch { }
        }
        #endregion

        #region function
        private void LoadNN()
        {

            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, windowsUIButton);
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(grvCDThuaThieu, this.Name);
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(grvCNThucHien, this.Name);
        }

        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = !visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = visible;

            grdCDThuaThieu.Enabled = visible;
            grvCNThucHien.OptionsBehavior.Editable = !visible;
        }

        private void InDuLieu()
        {

            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            frmViewReport frm = new frmViewReport();
            frm.rpt = new rptDSCDThuaThieu(DateTime.Now, cboID_DHB.Text, cboID_MH.Text, cboID_ORD.Text, cboID_CHUYEN.Text);
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSCDThuaThieu", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Ngay;
                cmd.Parameters.Add("@ID_CHUYEN", SqlDbType.Int).Value = cboID_CHUYEN.EditValue;
                cmd.Parameters.Add("@ID_CHUYEN_SD", SqlDbType.Int).Value = Convert.ToInt32(iID_CHUYEN_SD);
                cmd.Parameters.Add("@ID_ORD", SqlDbType.Int).Value = cboID_ORD.EditValue;

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DATA";
                frm.AddDataSource(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            frm.ShowDialog();
        }

        private void LoadcboDHB()
        {
            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spThuaThieuGetCbo", conn);

                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);

                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_DHB, dt, "ID_DHB", "SO_DHB", "SO_DHB", true);

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch { }
        }

        private void LoadcboHH()
        {
            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spThuaThieuGetCbo", conn);

                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                cmd.Parameters.Add("@ID_DHB", SqlDbType.BigInt).Value = Convert.ToInt64(cboID_DHB.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);

                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_MH, dt, "ID_HH", "TEN_HH", "TEN_HH", true);
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch { }
        }
        private void LoadcboORD()
        {
            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spThuaThieuGetCbo", conn);

                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 3;
                cmd.Parameters.Add("@ID_DHB", SqlDbType.BigInt).Value = Convert.ToInt64(cboID_DHB.EditValue);
                cmd.Parameters.Add("@ID_HH", SqlDbType.BigInt).Value = Convert.ToInt64(cboID_MH.EditValue);

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);

                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_ORD, dt, "ID_DHBORD", "TEN_ORD", "TEN_ORD", true);
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch { }
        }

        private void LoadCboChuyen()
        {
            try
            {
                string sSql = "SELECT ID_CHUYEN, TEN_CHUYEN FROM CHUYEN ORDER BY CHUYEN.TEN_CHUYEN";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                if (cboID_CHUYEN.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_CHUYEN, dt, "ID_CHUYEN", "TEN_CHUYEN", "TEN_CHUYEN");
                }
                else
                {
                    cboID_CHUYEN.Properties.DataSource = dt;
                }
                cboID_CHUYEN.Properties.View.Columns[0].Caption = "STT Chuyền";
                cboID_CHUYEN.Properties.View.Columns[1].Caption = "Tên Chuyền";
                cboID_CHUYEN.Properties.View.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboID_CHUYEN.Properties.View.Columns[1].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cboID_CHUYEN.Properties.View.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboID_CHUYEN.Properties.View.Columns[0].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            }
            catch { }
        }

        private void LoadgrvCDThuaThieu()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetDSCDThuaThieu", conn);
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Ngay;
                cmd.Parameters.Add("@ID_CHUYEN", SqlDbType.BigInt).Value = cboID_CHUYEN.EditValue;
                cmd.Parameters.Add("@ID_CHUYEN_SD", SqlDbType.BigInt).Value = iID_CHUYEN_SD;
                cmd.Parameters.Add("@ID_ORD", SqlDbType.BigInt).Value = cboID_ORD.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CD"] };




                if (grdCDThuaThieu.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdCDThuaThieu, grvCDThuaThieu, dt, false, true, false, true, false, "");
                    grvCDThuaThieu.Columns["ID_CHUYEN_TH"].Visible = false;
                    grvCDThuaThieu.Columns["ID_CHUYEN_SD"].Visible = false;
                    grvCDThuaThieu.Columns["SL_CHOT"].Visible = false;
                    grvCDThuaThieu.Columns["ID_CD"].Visible = false;

                    grvCDThuaThieu.Columns["SL_TH"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    grvCDThuaThieu.Columns["SL_TH"].DisplayFormat.FormatString = "N0";

                    grvCDThuaThieu.Columns["SL_THUA"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    grvCDThuaThieu.Columns["SL_THUA"].DisplayFormat.FormatString = "N0";

                    grvCDThuaThieu.Columns["SL_THIEU"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    grvCDThuaThieu.Columns["SL_THIEU"].DisplayFormat.FormatString = "N0";

                }
                else
                {
                    grdCDThuaThieu.DataSource = dt;
                }



                if (iID_CD_TMP != -1)
                {
                    try
                    {
                        int index = dt.Rows.IndexOf(dt.Rows.Find(iID_CD_TMP));
                        grvCDThuaThieu.FocusedRowHandle = grvCDThuaThieu.GetRowHandle(index);
                    }
                    catch { }
                }
            }
            catch
            {

            }
        }

        private void LoadSLChot()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("SELECT dbo.fnGetSLChot(@Ngay,@ID_ChuyenSD,@ID_Chuyen, @ID_Ord)", conn);
                cmd.Parameters.AddWithValue("@Ngay", Ngay);
                cmd.Parameters.AddWithValue("@ID_ChuyenSD", iID_CHUYEN_SD);
                cmd.Parameters.AddWithValue("@ID_Chuyen", cboID_CHUYEN.EditValue);
                cmd.Parameters.AddWithValue("@ID_Ord", cboID_ORD.EditValue);

                slChot = string.IsNullOrEmpty(cmd.ExecuteScalar().ToString()) ? 0 : Convert.ToInt32(cmd.ExecuteScalar());
                lblSLChot.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "SL_chot : ") + slChot.ToString("N0");
            }
            catch  { }
        }

        private void LoadgrvCN()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetDSCNThucHienCD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Ngay, Convert.ToInt64(cboID_CHUYEN.EditValue), iID_CHUYEN_SD, Convert.ToInt64(cboID_ORD.EditValue)));
                if (grdCNThucHien.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdCNThucHien, grvCNThucHien, dt, false, true, false, true, false, "");
                    grvCNThucHien.Columns["ID_CHUYEN_TH"].Visible = false;
                    grvCNThucHien.Columns["ID_ORD"].Visible = false;
                    grvCNThucHien.Columns["ID_CD"].Visible = false;

                    grvCNThucHien.Columns["TEN_TO"].OptionsColumn.AllowEdit = false;
                    grvCNThucHien.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                    grvCNThucHien.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                    grvCNThucHien.Columns["NGAY"].OptionsColumn.AllowEdit = false;

                    grvCNThucHien.Columns["SO_LUONG"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    grvCNThucHien.Columns["SO_LUONG"].DisplayFormat.FormatString = "N0";
                }
                else
                {
                    grdCNThucHien.DataSource = dt;
                }
            }
            catch { }
        }


        #endregion


    }
}
