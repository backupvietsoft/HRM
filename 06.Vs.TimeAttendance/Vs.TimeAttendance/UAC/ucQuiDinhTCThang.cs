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
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using System.Xml.Linq;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraLayout;
using DevExpress.Utils;

namespace Vs.TimeAttendance
{
    public partial class ucQuiDinhTCThang : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;

        public static ucQuiDinhTCThang _instance;
        public static ucQuiDinhTCThang Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucQuiDinhTCThang();
                return _instance;
            }
        }

        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        public ucQuiDinhTCThang()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);

        }

        private void ucQuiDinhTCThang_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonViKO(cboDonVi);
            LoaddNgayApDung();
            LoadData();
            EnableButon(isAdd);
            Commons.Modules.sLoad = "";
        }
        private void LoadData()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spQuiDinhTangCaThang", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = cboDonVi.EditValue;
                cmd.Parameters.Add("@NAM", SqlDbType.Int).Value = Convert.ToInt32(cboNam.Text);
                cmd.Parameters.Add("@bThem", SqlDbType.Bit).Value = isAdd;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, false, false, true, true, this.Name);
                grvData.Columns["THANG"].OptionsColumn.AllowEdit = false;
            }
            catch
            {

            }
        }
        public void LoaddNgayApDung()
        {
            try
            {
                DataTable dtthang = new DataTable();
                string sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),NAM,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),NAM,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),NAM,103),7) AS THANG FROM dbo.QUI_DINH_TANG_CA_THANG WHERE ID_DV = " + cboDonVi.EditValue + " ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                grvThang.Columns["THANG"].Visible = false;
                grvThang.Columns["M"].Visible = false;
                cboNam.Text = grvThang.GetFocusedRowCellValue("Y").ToString();
            }
            catch
            {
                cboNam.Text = DateTime.Now.ToString("yyyy");
            }
        }

        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        isAdd = true;
                        LoadData();
                        EnableButon(isAdd);
                        break;

                    }
                case "ghi":
                    {
                        Validate();
                        if (grvData.HasColumnErrors) return;
                        if (Savedata() == false)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                        }
                        isAdd = false;
                        LoadData();
                        EnableButon(isAdd);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        break;
                    }
                case "khongghi":
                    {
                        isAdd = false;
                        LoadData();
                        EnableButon(isAdd);
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
            }
        }

        private void EnableButon(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = !visible;
            btnALL.Buttons[1].Properties.Visible = !visible;
            btnALL.Buttons[2].Properties.Visible = !visible;
            btnALL.Buttons[3].Properties.Visible = visible;
            btnALL.Buttons[4].Properties.Visible = visible;
            cboDonVi.Enabled = !visible;
            cboNam.Enabled = !visible;

            grvData.OptionsBehavior.Editable = visible;
        }

        private void grvData_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void grvData_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
        }

        private bool Savedata()
        {
            string sTB = "sBTMTChuyenCan" + Commons.Modules.iIDUser;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spQuiDinhTangCaThang", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = cboDonVi.EditValue;
                cmd.Parameters.Add("@NAM", SqlDbType.Int).Value = Convert.ToInt32(cboNam.Text);
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sTB;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (dt.Rows[0][0].ToString() == "-99")
                {
                    MessageBox.Show(dt.Rows[0][1].ToString());
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                Commons.Modules.ObjSystems.XoaTable(sTB);
                return false;
            }
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }


        private void grvNgay_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboNam.Text = grvThang.GetFocusedRowCellValue("Y").ToString();
            }
            catch { }
            cboNam.ClosePopup();

        }
        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadData();
            Commons.Modules.sLoad = "";
        }

        private void calThang_DateTimeCommit_1(object sender, EventArgs e)
        {
            try
            {
                cboNam.Text = calThang.DateTime.ToString("yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grvThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + cboNam.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                }
                else { }
            }
            catch (Exception ex)
            {
                cboNam.Text = calThang.DateTime.ToString("yyyy");
            }
            cboNam.ClosePopup();
        }

        private void grdData_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (btnALL.Buttons[0].Properties.Visible == false && e.KeyData == System.Windows.Forms.Keys.Delete)
            {
                grvData.DeleteSelectedRows();
            }
        }

        private void cboNam_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadData();
            Commons.Modules.sLoad = "";
        }
    }
}