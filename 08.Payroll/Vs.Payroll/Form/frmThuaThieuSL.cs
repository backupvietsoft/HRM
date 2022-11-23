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
        public int iID_DV = -1;
        public Int64 iID_CHUYEN = -1;
        public Int64 iID_CHUYEN_SD = -1;
        public Int64 iID_ORD = -1;
        public int iID_DT = -1;
        public int slChot = 0;
        public DateTime Ngay;

        public Int64 iID_CD_TMP = -1;

        public frmThuaThieuSL()
        {
            InitializeComponent();

        }

        #region even
        private void frmThuaThieuSL_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadcboKH_CHUYEN();
            LoadcboORD();

            cboID_DT.EditValue = iID_DT;
            cboID_ORD.EditValue = iID_ORD;
            cboID_CHUYEN.EditValue = iID_CHUYEN_SD;
            datTNgay.EditValue = Ngay;
            datDNgay.EditValue = Ngay;
            Commons.OSystems.SetDateEditFormat(datTNgay);
            Commons.OSystems.SetDateEditFormat(datDNgay);

            LoadgrvCDThuaThieu();
            LoadgrvCN();
            Commons.Modules.sLoad = "";

            grvCDThuaThieu_FocusedRowChanged(null, null);

            enableButon(true);
            LoadNN();
            LoadSLChot();
            lblThangDoiChieu.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblThangDoiChieu") + " : " + Ngay.ToString("MM/yyyy");
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
                            iID_CD_TMP = Convert.ToInt64(grvCNThucHien.GetFocusedRowCellValue("ID_CD"));
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
                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spThuaThieuSL", conn);
                                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT_grvCNThucHien;
                                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 4;
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
                            LoadgrvCDThuaThieu();
                            LoadgrvCN();
                            grvCDThuaThieu_FocusedRowChanged(null, null);
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
        private void cboID_DT_EditValueChanged(object sender, EventArgs e)
        {
            LoadcboORD();
        }
        private void cboID_ORD_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadgrvCDThuaThieu();
            LoadgrvCN();
            grvCDThuaThieu_FocusedRowChanged(null, null);
        }
        private void cboID_CHUYEN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
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
                if (sIDCD != "-1")
                {
                    sDK = " ID_CD = '" + sIDCD + "' ";
                }
                else
                {
                    sDK = "1 =0 ";
                }

                dtTmp.DefaultView.RowFilter = sDK;
            }
            catch { }
        }
        #endregion

        #region function
        private void LoadNN()
        {

            Commons.Modules.ObjSystems.ThayDoiNN(this, windowsUIButton);
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
            frm.rpt = new rptDSCDThuaThieu(DateTime.Now, cboID_DT.Text, cboID_ORD.Text, "ID_ORD", cboID_CHUYEN.Text);
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spThuaThieuSL", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 5;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Ngay;
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

        private void LoadcboKH_CHUYEN()
        {
            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spThuaThieuSL", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_DT, dt, "ID_DT", "TEN_KH", "TEN_KH");

                dt = new DataTable();
                dt = ds.Tables[1].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_CHUYEN, dt, "ID_TO", "TEN_TO", "TEN_TO", true);
                cboID_CHUYEN.Properties.View.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboID_CHUYEN.Properties.View.Columns[1].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cboID_CHUYEN.Properties.View.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboID_CHUYEN.Properties.View.Columns[0].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch (Exception ex) { }
        }

        private void LoadcboORD()
        {
            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();
            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spThuaThieuSL", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = iID_DV;
                cmd.Parameters.Add("@iCot1", SqlDbType.BigInt).Value = Convert.ToInt64(cboID_DT.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = new DataTable();
                dt = ds.Tables[0].Copy();

                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_ORD, dt, "ID_ORD", "TEN_HH", "TEN_HH", true);
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch { }
        }
        private void LoadgrvCDThuaThieu()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spThuaThieuSL", conn);
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = datTNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = datDNgay.DateTime;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Ngay;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                cmd.Parameters.Add("@bChon", SqlDbType.Bit).Value = rdoChonThang.SelectedIndex;
                cmd.Parameters.Add("@ID_CHUYEN_SD", SqlDbType.BigInt).Value = cboID_CHUYEN.EditValue;
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
            catch (Exception ex)
            {

            }
        }

        private void LoadSLChot()
        {
            try
            {
                if (rdoChonThang.SelectedIndex == 1)
                {
                    slChot = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnGetSLChot('" + Ngay.ToString("MM/dd/yyyy") + "', " + cboID_CHUYEN.EditValue + ", " + cboID_ORD.EditValue + ")"));
                }
                else
                {
                    slChot = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.fnGetSLChot_Ngay('" + datTNgay.DateTime.ToString("MM/dd/yyyy") + "', '" + datDNgay.DateTime.ToString("MM/dd/yyyy") + "' , " + cboID_CHUYEN.EditValue + ", " + cboID_ORD.EditValue + ")"));
                }
                lblSLChot.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "SL_chot : ") + slChot.ToString("N0");
            }
            catch { }
        }

        private void LoadgrvCN()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spThuaThieuSL", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Ngay;
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = datTNgay.DateTime;
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = datDNgay.DateTime;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 3;
                cmd.Parameters.Add("@bChon", SqlDbType.Int).Value = rdoChonThang.SelectedIndex;
                cmd.Parameters.Add("@ID_CHUYEN", SqlDbType.BigInt).Value = -1;
                cmd.Parameters.Add("@ID_CHUYEN_SD", SqlDbType.BigInt).Value = cboID_CHUYEN.EditValue;
                cmd.Parameters.Add("@ID_ORD", SqlDbType.BigInt).Value = cboID_ORD.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
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

        private void rdoChonThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSLChot();
            if (rdoChonThang.SelectedIndex == 0)
            {
                tableLayoutPanel1.RowStyles[4].Height = 25;
            }
            else
            {
                tableLayoutPanel1.RowStyles[4].Height = 0;
            }
            LoadgrvCDThuaThieu();
        }

        private void datTNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadSLChot();
            LoadgrvCDThuaThieu();
            LoadgrvCN();
            grvCDThuaThieu_FocusedRowChanged(null, null);
        }

        private void datDNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadSLChot();
            LoadgrvCDThuaThieu();
            LoadgrvCN();
            grvCDThuaThieu_FocusedRowChanged(null, null);
        }

        private void grvCDThuaThieu_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(grvCDThuaThieu.GetRowCellValue(e.RowHandle, grvCDThuaThieu.Columns["SL_TH"].FieldName)) != 0) return;
                e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFF2CC");
                e.HighPriority = true;
            }
            catch
            {

            }
        }
    }
}
