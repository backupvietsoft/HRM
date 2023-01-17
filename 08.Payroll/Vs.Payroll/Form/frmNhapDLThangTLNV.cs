using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Reflection;
using System.Linq;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Menu;
using NPOI.OpenXmlFormats.Dml.Diagram;

namespace Vs.Payroll
{
    public partial class frmNhapDLThangTLNV : DevExpress.XtraEditors.XtraForm
    {
        public int iID_DV = -1;
        public int iID_XN = -1;
        public int iID_TO = -1;
        public DateTime dNgay;
        private int iThem = 0;
        public frmNhapDLThangTLNV()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, tabControl, windowsUIButton);
        }

        //sự kiên load form
        private void frmNhapDLThangTLNV_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            cboDonVi.EditValue = iID_DV;
            cboXiNghiep.EditValue = iID_XN;
            cboTo.EditValue = iID_TO;
            tabControl_SelectedPageChanged(null, null);
            VisibleButton(true);
            foreach (ToolStripMenuItem item in contextMenuStrip1.Items)
            {
                item.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, item.Name);
            }
            Commons.Modules.sLoad = "";
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        iThem = 1;
                        switch (tabControl.SelectedTabPageIndex)
                        {
                            case 0:
                                {
                                    LoadDataHTL(iThem);
                                    break;
                                }
                            case 1:
                                {
                                    LoadDataPCT(iThem);
                                    break;
                                }
                            case 2:
                                {
                                    Commons.Modules.ObjSystems.AddnewRow(grvDTNM, true);
                                    break;
                                }
                            case 3:
                                {
                                    LoadDataTHQQLKhac(iThem);
                                    break;
                                }
                        }
                        VisibleButton(false);
                        break;
                    }
                case "luu":
                    {
                        //dtTemp = Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdData, grvData);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    }
                case "khongluu":
                    {
                        iThem = 0;
                        switch (tabControl.SelectedTabPageIndex)
                        {
                            case 0:
                                {
                                    LoadDataHTL(iThem);
                                    break;
                                }
                            case 1:
                                {
                                    LoadDataPCT(iThem);
                                    break;
                                }
                            case 2:
                                {
                                    Commons.Modules.ObjSystems.DeleteAddRow(grvDTNM);
                                    break;
                                }
                            case 3:
                                {
                                    LoadDataTHQQLKhac(iThem);
                                    break;
                                }
                        }
                        VisibleButton(true);
                        break;
                    }
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
            }
        }
        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            switch (tabControl.SelectedTabPageIndex)
            {
                case 0:
                    {
                        cboThang.Text = calThang.DateTime.ToString("dd/MM/yyyy");
                        cboThang.ClosePopup();
                        break;
                    }
                default:
                    {
                        cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
                        cboThang.ClosePopup();
                        break;
                    }
            }

        }

        public void LoadThang(int indexTab)
        {
            try
            {
                string sSql = "";
                DataTable dtthang = new DataTable();
                switch (indexTab)
                {
                    case 0:
                        {
                            sSql = "SELECT disTINCT RIGHT(CONVERT(VARCHAR(10),NGAY,103),7) AS THANG , CONVERT(VARCHAR(10),NGAY,103) NGAY FROM dbo.HO_TRO_LUONG ORDER BY THANG DESC , NGAY DESC";
                            dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                            Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay1, dtthang, false, true, true, true, true, this.Name);
                            grvNgay1.Columns["THANG"].Visible = false;

                            try
                            {
                                cboThang.Text = grvNgay1.GetFocusedRowCellValue("NGAY").ToString();
                            }
                            catch
                            {
                                cboThang.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            }
                            break;
                        }
                    case 1:
                        {
                            sSql = "SELECT disTINCT RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG , CONVERT(VARCHAR(10),THANG,103) NGAY FROM dbo.PHAN_CONG_TO ORDER BY THANG DESC , NGAY DESC";
                            dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                            Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay1, dtthang, false, true, true, true, true, this.Name);
                            grvNgay1.Columns["NGAY"].Visible = false;

                            try
                            {
                                cboThang.Text = grvNgay1.GetFocusedRowCellValue("THANG").ToString();
                            }
                            catch
                            {
                                cboThang.Text = DateTime.Now.ToString("MM/yyyy");
                            }
                            break;
                        }
                    case 2:
                        {
                            sSql = "SELECT disTINCT RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG , CONVERT(VARCHAR(10),THANG,103) NGAY FROM dbo.DOANH_THU_NHA_MAY ORDER BY THANG DESC , NGAY DESC";
                            dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                            Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay1, dtthang, false, true, true, true, true, this.Name);
                            grvNgay1.Columns["NGAY"].Visible = false;

                            try
                            {
                                cboThang.Text = grvNgay1.GetFocusedRowCellValue("THANG").ToString();
                            }
                            catch
                            {
                                cboThang.Text = DateTime.Now.ToString("MM/yyyy");
                            }
                            break;
                        }

                    case 3:
                        {
                            sSql = "SELECT disTINCT RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG , CONVERT(VARCHAR(10),THANG,103) NGAY FROM dbo.THUONG_HQQL_KHAC ORDER BY THANG DESC , NGAY DESC";
                            dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                            Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay1, dtthang, false, true, true, true, true, this.Name);
                            grvNgay1.Columns["NGAY"].Visible = false;

                            try
                            {
                                cboThang.Text = grvNgay1.GetFocusedRowCellValue("THANG").ToString();
                            }
                            catch
                            {
                                cboThang.Text = DateTime.Now.ToString("MM/yyyy");
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

            }
            catch (Exception ex)
            {
                DateTime now = DateTime.Now;
                cboThang.Text = now.ToString("dd/MM/yyyy");
            }
        }
        private void LoadDataHTL(int iThem)
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spNhapDLThangTLNV", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "HTL";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@iThem", SqlDbType.Int).Value = iThem;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdHTL.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdHTL, grvHTL, dt, true, true, false, true, true, this.Name);
                    grvHTL.Columns["ID_HTL"].Visible = false;
                    grvHTL.Columns["ID_CN"].Visible = false;
                }
                else
                {
                    grdHTL.DataSource = dt;
                }
            }
            catch { }
        }
        private void LoadDataPCT(int iThem)
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spNhapDLThangTLNV", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "PCT";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@iThem", SqlDbType.Int).Value = iThem;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdPCT.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdPCT, grvPCT, dt, true, true, false, true, true, this.Name);
                    grvPCT.Columns["ID_PCT"].Visible = false;
                    grvPCT.Columns["ID_CN"].Visible = false;
                }
                else
                {
                    grdPCT.DataSource = dt;
                }
            }
            catch { }
        }
        private void LoadDataDTNM()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spNhapDLThangTLNV", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "DTNM";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdDTNM.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDTNM, grvDTNM, dt, true, true, false, true, true, this.Name);
                    grvDTNM.Columns["ID_DTNM"].Visible = false;
                }
                else
                {
                    grdDTNM.DataSource = dt;
                }
            }
            catch { }
        }
        private void LoadDataTHQQLKhac(int iThem)
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spNhapDLThangTLNV", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "T_HQQL_KHAC";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@iThem", SqlDbType.Int).Value = iThem;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdTHQQL.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdTHQQL, grvTHQQL, dt, true, true, false, true, true, this.Name);
                    grvTHQQL.Columns["ID_THQQL_KHAC"].Visible = false;
                    grvTHQQL.Columns["ID_CN"].Visible = false;
                }
                else
                {
                    grdTHQQL.DataSource = dt;
                }
            }
            catch { }
        }
        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            switch (tabControl.SelectedTabPageIndex)
            {
                case 0:
                    {
                        LoadDataHTL(iThem);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            Commons.Modules.sLoad = "";
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            switch (tabControl.SelectedTabPageIndex)
            {
                case 0:
                    {
                        LoadDataHTL(iThem);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            Commons.Modules.sLoad = "";
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            switch (tabControl.SelectedTabPageIndex)
            {
                case 0:
                    {
                        LoadDataHTL(iThem);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            Commons.Modules.sLoad = "";
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            switch (tabControl.SelectedTabPageIndex)
            {
                case 0:
                    {
                        LoadDataHTL(iThem);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            Commons.Modules.sLoad = "";
        }

        #region function

        private void VisibleButton(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
        }

        #endregion

        #region chuotphai
        private void toolCapNhat_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                string sCotCN = "";
                var data = (object)null;
                switch (tabControl.SelectedTabPageIndex)
                {
                    case 0:
                        {
                            sCotCN = grvHTL.FocusedColumn.FieldName;
                            data = grvHTL.GetFocusedRowCellValue(sCotCN);
                            dt1 = Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdHTL, grvHTL);
                            dt = (DataTable)grdHTL.DataSource;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                dt.AsEnumerable().Where(row => dt1.AsEnumerable()
                                                         .Select(r => r.Field<Int64>("ID_CN"))
                                                         .Any(x => x == row.Field<Int64>("ID_CN"))
                                                         ).ToList<DataRow>().ForEach(r => r[sCotCN] = (data));
                dt.AcceptChanges();
            }
            catch
            {

            }
        }

        #endregion

        private void grvHTL_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (windowsUIButton.Buttons[0].Properties.Visible) return;
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            }
            catch { }
        }

        private void grvNgay1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = grvNgay1.GetFocusedRowCellValue("NGAY").ToString();
            }
            catch { }
            //cboThang.ClosePopup();
        }

        private void tabControl_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            try
            {
                switch (tabControl.SelectedTabPageIndex)
                {
                    case 0:
                        {
                            calThang.VistaCalendarViewStyle = VistaCalendarViewStyle.Default;
                            LoadThang(0);
                            LoadDataHTL(iThem);
                            break;
                        }
                    case 1:
                        {
                            calThang.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
                            LoadThang(1);
                            LoadDataPCT(iThem);
                            break;
                        }
                    case 2:
                        {
                            calThang.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
                            LoadThang(2);
                            LoadDataDTNM();
                            break;
                        }
                    case 3:
                        {
                            calThang.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
                            LoadThang(3);
                            LoadDataTHQQLKhac(iThem);
                            break;
                        }
                }
            }
            catch { }
        }

        private void tabControl_SelectedPageChanging(object sender, DevExpress.XtraLayout.LayoutTabPageChangingEventArgs e)
        {
            if (iThem == 1) e.Cancel = true;
        }
    }
}