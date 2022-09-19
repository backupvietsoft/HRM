using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vs.Category
{
    public partial class frmDuyetQuyDinh : DevExpress.XtraEditors.XtraUserControl
    {
        static int iPQ = -1; // =1: full, <>1: read only
        private int iID_DQD = -1;
        private DataTable dt_USER;
        IEnumerable<Control> allControls;
        public frmDuyetQuyDinh(int PQ)
        {
            iPQ = PQ;
            InitializeComponent();

            if (iPQ != 1)
            {
                btnALL.Buttons[0].Properties.Visible = false;
                btnALL.Buttons[1].Properties.Visible = false;
                btnALL.Buttons[3].Properties.Visible = false;
                btnALL.Buttons[4].Properties.Visible = false;
            }
            else
            {
                btnALL.Buttons[0].Properties.Visible = true;
                btnALL.Buttons[1].Properties.Visible = true;
                btnALL.Buttons[3].Properties.Visible = true;
            }

            var typeToBeSelected = new List<Type>
            {

                typeof(DevExpress.XtraEditors.TextEdit)
                , typeof(DevExpress.XtraEditors.MemoEdit)
                , typeof(DevExpress.XtraEditors.ButtonEdit)
            };

            allControls = Commons.Modules.ObjSystems.GetAllConTrol(dataLayoutControl1, typeToBeSelected);

            //VsMain.MFieldRequest(lblID_DTL);
            //VsMain.MFieldRequest(lblSO_DQD);
            //VsMain.MFieldRequest(lblNGAY_HIEU_LUC);
            //VsMain.MFieldRequest(lblID_DTL);
            //VsMain.MFieldRequest(lblTEN_QUY_DINH);

            txtSO_DQD.ReadOnly = true;
        }

        #region Event
        private void frmDuyetQuyDinh_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                LoadCbo();
                LoadData();
                Commons.Modules.sLoad = "";
                LoadNN();

                Commons.Modules.ObjSystems.ThayDoiNN(this, new List<DevExpress.XtraLayout.LayoutControlGroup> { Root }, btnALL);

            }
            catch { }
            Commons.Modules.sLoad = "";
        }

        private void cboID_DTL_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                if (cboID_DTL.EditValue == null) return;
                DataTable dt = new DataTable();
                dt = ((DataTable)cboID_DTL.Properties.DataSource).Copy();

                try
                {
                    dt = dt.AsEnumerable().Where(r => r.Field<int>("ID_DTL").Equals(cboID_DTL.EditValue)).CopyToDataTable();
                }
                catch { dt = dt.Clone(); }

                txtDIEU_KIEN_DUYET.Text = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["QUERY"])) ? "" : Convert.ToString(dt.Rows[0]["QUERY"]);
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
        }

        private void grvUserDuyet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "ID_USER")
                {
                    int ID_USER = string.IsNullOrEmpty(grvUserDuyet.GetRowCellValue(grvUserDuyet.FocusedRowHandle, "ID_USER").ToString()) ? 0 : Convert.ToInt32(grvUserDuyet.GetRowCellValue(grvUserDuyet.FocusedRowHandle, "ID_USER"));

                    for (int i = 0; i < dt_USER.Rows.Count; i++)
                    {
                        if ((string.IsNullOrEmpty(dt_USER.Rows[i]["ID_USER"].ToString()) ? 0 : Convert.ToInt32(dt_USER.Rows[i]["ID_USER"])) == ID_USER)
                        {
                            string FULL_NAME = string.IsNullOrEmpty(dt_USER.Rows[i]["FULL_NAME"].ToString()) ? "" : dt_USER.Rows[i]["FULL_NAME"].ToString();
                            grvUserDuyet.SetRowCellValue(grvUserDuyet.FocusedRowHandle, "FULL_NAME", FULL_NAME);
                            return;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void grvBuocDuyet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "ID_USER_DUYET")
                {
                    int ID_USER = string.IsNullOrEmpty(grvBuocDuyet.GetRowCellValue(grvBuocDuyet.FocusedRowHandle, "ID_USER_DUYET").ToString()) ? 0 : Convert.ToInt32(grvBuocDuyet.GetRowCellValue(grvBuocDuyet.FocusedRowHandle, "ID_USER_DUYET"));

                    for (int i = 0; i < dt_USER.Rows.Count; i++)
                    {
                        if ((string.IsNullOrEmpty(dt_USER.Rows[i]["ID_USER"].ToString()) ? 0 : Convert.ToInt32(dt_USER.Rows[i]["ID_USER"])) == ID_USER)
                        {
                            string FULL_NAME = string.IsNullOrEmpty(dt_USER.Rows[i]["FULL_NAME"].ToString()) ? "" : dt_USER.Rows[i]["FULL_NAME"].ToString();
                            grvBuocDuyet.SetRowCellValue(grvBuocDuyet.FocusedRowHandle, "FULL_NAME_DUYET", FULL_NAME);
                            return;
                        }
                        else
                        {
                            grvBuocDuyet.SetRowCellValue(grvBuocDuyet.FocusedRowHandle, "FULL_NAME_DUYET", "");
                        }
                    }
                }

                if (e.Column.FieldName == "ID_USER_THAY_THE")
                {
                    int ID_USER = string.IsNullOrEmpty(grvBuocDuyet.GetRowCellValue(grvBuocDuyet.FocusedRowHandle, "ID_USER_THAY_THE").ToString()) ? 0 : Convert.ToInt32(grvBuocDuyet.GetRowCellValue(grvBuocDuyet.FocusedRowHandle, "ID_USER_THAY_THE"));

                    for (int i = 0; i < dt_USER.Rows.Count; i++)
                    {
                        if ((string.IsNullOrEmpty(dt_USER.Rows[i]["ID_USER"].ToString()) ? 0 : Convert.ToInt32(dt_USER.Rows[i]["ID_USER"])) == ID_USER)
                        {
                            string FULL_NAME = string.IsNullOrEmpty(dt_USER.Rows[i]["FULL_NAME"].ToString()) ? "" : dt_USER.Rows[i]["FULL_NAME"].ToString();
                            grvBuocDuyet.SetRowCellValue(grvBuocDuyet.FocusedRowHandle, "FULL_NAME_THAY_THE", FULL_NAME);
                            return;
                        }
                        else
                        {
                            grvBuocDuyet.SetRowCellValue(grvBuocDuyet.FocusedRowHandle, "FULL_NAME_THAY_THE", "");
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void txtSO_TEMP_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            LoadView();
        }

        private void grvUserDuyet_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                grvUserDuyet.UpdateCurrentRow();

                int ID_USER = string.IsNullOrEmpty(grvUserDuyet.GetRowCellValue(grvUserDuyet.FocusedRowHandle, "ID_USER").ToString()) ? 0 : Convert.ToInt32(grvUserDuyet.GetRowCellValue(e.RowHandle, "ID_USER"));

                DataTable dt = new DataTable();
                dt = (DataTable)grdUserDuyet.DataSource;
                int Count = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if ((string.IsNullOrEmpty(dt.Rows[i]["ID_USER"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[i]["ID_USER"])) == ID_USER)
                    {
                        Count++;

                    }
                }

                if (grvUserDuyet.IsNewItemRow(grvUserDuyet.FocusedRowHandle) && Count >= 1)
                {
                    e.Valid = false;
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgUserDaTonTai"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Count >= 2)
                {
                    e.Valid = false;
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgUserDaTonTai"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void grvUserDuyet_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvBuocDuyet_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                if ((string.IsNullOrEmpty(grvBuocDuyet.GetRowCellValue(e.RowHandle, grvBuocDuyet.Columns["BUOC_DUYET"]).ToString()) ? 0 : Convert.ToInt32(grvBuocDuyet.GetRowCellValue(e.RowHandle, grvBuocDuyet.Columns["BUOC_DUYET"]))) <= 0)
                {
                    e.Valid = false;
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBuocDuyetPhaiLonHon0"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void grvBuocDuyet_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvUserDuyet_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    if (KiemSuDung(iID_DQD)) return;


                    if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoChacXoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo) == DialogResult.No) return;
                    int iID_DU = string.IsNullOrEmpty(Convert.ToString(grvUserDuyet.GetFocusedRowCellValue("ID_DU"))) ? 0 : Convert.ToInt32(grvUserDuyet.GetFocusedRowCellValue("ID_DU"));

                    System.Data.SqlClient.SqlConnection conn;
                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                    cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 8;
                    cmd.Parameters.Add("@ID_DQD", SqlDbType.Int).Value = iID_DQD;
                    cmd.Parameters.Add("@iID", SqlDbType.Int).Value = iID_DU;
                    cmd.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    DataTable dtTEMP = new DataTable();
                    dtTEMP = ds.Tables[0].Copy();

                    int iTEMP = string.IsNullOrEmpty(Convert.ToString(dtTEMP.Rows[0][0])) ? 0 : Convert.ToInt32(dtTEMP.Rows[0][0]);
                    string sTEMP = string.IsNullOrEmpty(Convert.ToString(dtTEMP.Rows[0][1])) ? "" : Convert.ToString(dtTEMP.Rows[0][1]);

                    if (iTEMP >= 0)
                    {
                        grvUserDuyet.DeleteSelectedRows();
                        ((DataTable)grdUserDuyet.DataSource).AcceptChanges();
                    }
                    else
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgXoaThatBai") + "\n" + sTEMP, Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgXoaThatBai") + "\n" + ex.Message, Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void grvBuocDuyet_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    if (KiemSuDung(iID_DQD)) return;

                    if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoChacXoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo) == DialogResult.No) return;

                    int iID_DB = string.IsNullOrEmpty(Convert.ToString(grvBuocDuyet.GetFocusedRowCellValue("ID_DB"))) ? 0 : Convert.ToInt32(grvBuocDuyet.GetFocusedRowCellValue("ID_DB"));

                    System.Data.SqlClient.SqlConnection conn;
                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                    cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 9;
                    cmd.Parameters.Add("@ID_DQD", SqlDbType.Int).Value = iID_DQD;
                    cmd.Parameters.Add("@iID", SqlDbType.Int).Value = iID_DB;
                    cmd.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    DataTable dtTEMP = new DataTable();
                    dtTEMP = ds.Tables[0].Copy();

                    int iTEMP = string.IsNullOrEmpty(Convert.ToString(dtTEMP.Rows[0][0])) ? 0 : Convert.ToInt32(dtTEMP.Rows[0][0]);
                    string sTEMP = string.IsNullOrEmpty(Convert.ToString(dtTEMP.Rows[0][1])) ? "" : Convert.ToString(dtTEMP.Rows[0][1]);

                    if (iTEMP >= 0)
                    {
                        //grvBuocDuyet.DeleteSelectedRows();
                        //((DataTable)grdBuocDuyet.DataSource).AcceptChanges();
                        LoadData();
                    }
                    else
                    {
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgXoaThatBai") + "\n" + sTEMP, Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgXoaThatBai") + "\n" + ex.Message, Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void grvUserDuyet_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvUserDuyet.SetRowCellValue(e.RowHandle, "INACTIVE", 0);
        }

        private void grvBuocDuyet_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvBuocDuyet.SetRowCellValue(e.RowHandle, "BUOC_DUYET", Tinh_Max_Cot((DataTable)grdBuocDuyet.DataSource, "BUOC_DUYET") + 1);
            grvBuocDuyet.SetRowCellValue(e.RowHandle, "QUYET_DINH", 0);
            grvBuocDuyet.SetRowCellValue(e.RowHandle, "BAT_BUOC", 0);
            grvBuocDuyet.SetRowCellValue(e.RowHandle, "VANG_MAT", 0);
            grvBuocDuyet.SetRowCellValue(e.RowHandle, "INACTIVE", 0);
        }
        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            switch (btn.Tag.ToString())
            {
                case "ghi":
                    {
                        if (!dxValidationProvider1.Validate()) return;
                        if (KiemTrung()) return;

                        if (grdUserDuyet.DataSource == null || grdBuocDuyet.DataSource == null || ((DataTable)grdUserDuyet.DataSource).Rows.Count == 0 || ((DataTable)grdBuocDuyet.DataSource).Rows.Count == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaNhapChiTiet"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (KiemTrong()) return;

                        try
                        {
                            //Truyền datatable xuống CSDL
                            string sBT_DU = "[TMPDU" + Commons.Modules.iIDUser + "]";
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT_DU, Commons.Modules.ObjSystems.ConvertDatatable(grdUserDuyet), "");
                            string sBT_DB = "[TMPDB" + Commons.Modules.iIDUser + "]";
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT_DB, Commons.Modules.ObjSystems.ConvertDatatable(grdBuocDuyet), "");

                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 5;
                            cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT_DU;
                            cmd.Parameters.Add("@sBT1", SqlDbType.NVarChar).Value = sBT_DB;
                            cmd.Parameters.Add("@ID_DQD", SqlDbType.Int).Value = iID_DQD;
                            cmd.Parameters.Add("@ID_USER", SqlDbType.BigInt).Value = Commons.Modules.iIDUser;
                            cmd.Parameters.Add("@SO_DQD", SqlDbType.NVarChar).Value = txtSO_DQD.Text;
                            cmd.Parameters.Add("@TEN_QUY_DINH", SqlDbType.NVarChar).Value = txtTEN_QUY_DINH.Text;
                            cmd.Parameters.Add("@TEN_QUY_DINH_A", SqlDbType.NVarChar).Value = txtTEN_QUY_DINH_A.Text;
                            cmd.Parameters.Add("@TEN_QUY_DINH_H", SqlDbType.NVarChar).Value = txtTEN_QUY_DINH_H.Text;
                            cmd.Parameters.Add("@NGAY_HIEU_LUC", SqlDbType.NVarChar).Value = Commons.Modules.ObjSystems.ConvertDateTime(datNGAY_HIEU_LUC.Text);
                            cmd.Parameters.Add("@ID_DTL", SqlDbType.BigInt).Value = cboID_DTL.EditValue;
                            cmd.Parameters.Add("@DIEU_KIEN_DUYET", SqlDbType.BigInt).Value = cboID_DTL.EditValue;
                            cmd.Parameters.Add("@INACTIVE", SqlDbType.Bit).Value = chkINACTIVE.Checked;
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            DataTable dt_TEMP = new DataTable();
                            dt_TEMP = ds.Tables[0].Copy();

                            int iTEMP = string.IsNullOrEmpty(Convert.ToString(dt_TEMP.Rows[0][0])) ? 0 : Convert.ToInt32(dt_TEMP.Rows[0][0]);
                            string sTEMP = string.IsNullOrEmpty(Convert.ToString(dt_TEMP.Rows[0][1])) ? "" : Convert.ToString(dt_TEMP.Rows[0][1]);
                            if (iTEMP > 0)
                            {
                                if (iID_DQD == -1)
                                {
                                    try
                                    {
                                        iID_DQD = -1;
                                        LoadData();
                                    }
                                    catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
                                }
                                else
                                {
                                    iID_DQD = iTEMP;
                                    LoadData();
                                }
                            }
                            else
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgGhiKhongThanhCong") + "\n" + sTEMP, Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgGhiKhongThanhCong") + ex.Message, Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    }
                case "khongghi":
                    {
                        try
                        {
                            iID_DQD = -1;
                            LoadData();
                        }
                        catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
                        break;
                    }
                case "xoa":
                    {
                        try
                        {
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoChacXoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                return;
                            }

                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 7;
                            cmd.Parameters.Add("@ID_DQD", SqlDbType.Int).Value = iID_DQD;
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            DataTable dt_TEMP = new DataTable();
                            dt_TEMP = ds.Tables[0].Copy();

                            Int64 iTEMP = string.IsNullOrEmpty(Convert.ToString(dt_TEMP.Rows[0][0])) ? 0 : Convert.ToInt64(dt_TEMP.Rows[0][0]);
                            string sTEMP = string.IsNullOrEmpty(Convert.ToString(dt_TEMP.Rows[0][1])) ? "" : Convert.ToString(dt_TEMP.Rows[0][1]);


                            if (iTEMP >= 0)
                            {
                                try
                                {
                                    iID_DQD = -1;
                                    LoadData();
                                }
                                catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgXoaKhongThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgXoaKhongThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgXoaKhongThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                default: break;
            }
        }
        #endregion

        #region Function
        private void LoadNN()
        {
            try
            {
                tabUserDuyet.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "tabUserDuyet");
                tabUserDuyet.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "tabUserDuyet");
            }
            catch { }

            //try
            //{

            //}
            //catch
            //{ }
            //Commons.Modules.ObjSystems.ThayDoiNN(this, dataLayoutControl1);
            //Commons.Modules.ObjSystems.MLoadNNXtraGrid(grvBuocDuyet, this.Name);
            //Commons.Modules.ObjSystems.MLoadNNXtraGrid(grvUserDuyet, this.Name);
            //chkINACTIVE.Text = Commons.Modules.ObjSystems.GetLanguage(this.Name, "chkINACTIVE");
        }

        private void LoadCbo()
        {
            try
            {

                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 3;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                try
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_DTL, dt, "ID_DTL", "TEN_TAI_LIEU", this.Name, true, false);
                    cboID_DTL.Properties.View.Columns["QUERY"].Visible = false;
                }
                catch { }
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
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_DQD", SqlDbType.Int).Value = iID_DQD;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();

                if (dt.Rows.Count > 1) return;

                if (dt.Rows.Count == 0)
                {
                    foreach (var ctrl in allControls)
                    {
                        try
                        {
                            if (ctrl.Name != "")
                            {
                                ctrl.Text = "";
                            }
                        }
                        catch { }
                    }
                    cboID_DTL.EditValue = null;
                    datNGAY_HIEU_LUC.EditValue = DateTime.Now;
                    chkINACTIVE.Checked = false;
                    //txtSO_DQD.Text = Commons.Modules.ObjSystems.Ta("", this.Name.ToString(), "DUYET_QUY_DINH", "SO_DQD", Convert.ToDateTime(datNGAY_HIEU_LUC.EditValue));//"DT",
                    txtSO_DQD.Text = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_YCTD(" + datNGAY_HIEU_LUC.DateTime.ToString("MM/dd/yyyy") + ")").ToString();
                }

                if (dt.Rows.Count == 1 && dt != null)
                {
                    datNGAY_HIEU_LUC.EditValue = string.IsNullOrEmpty(dt.Rows[0]["NGAY_HIEU_LUC"].ToString()) ? DBNull.Value : dt.Rows[0]["NGAY_HIEU_LUC"];
                    cboID_DTL.EditValue = string.IsNullOrEmpty(dt.Rows[0]["ID_DTL"].ToString()) ? DBNull.Value : dt.Rows[0]["ID_DTL"];
                    chkINACTIVE.Checked = string.IsNullOrEmpty(dt.Rows[0]["INACTIVE"].ToString()) ? false : Convert.ToBoolean(dt.Rows[0]["INACTIVE"]);

                    foreach (var ctrl in allControls)
                    {
                        try
                        {
                            if (ctrl.Name != ""/* && !string.IsNullOrEmpty(ctrl.Text)*/)
                            {
                                ctrl.Text = string.IsNullOrEmpty(dt.Rows[0][ctrl.Name.Substring(3)].ToString()) ? "" : dt.Rows[0][ctrl.Name.Substring(3)].ToString();
                            }
                        }
                        catch { ctrl.Text = ""; }
                    }
                }

                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[1].Copy();

                if (grdUserDuyet.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdUserDuyet, grvUserDuyet, dt1, true, true, false, false, true, this.Name);

                    //Editable
                    for (int i = 0; i < grvUserDuyet.Columns.Count; i++)
                    {
                        grvUserDuyet.Columns[i].OptionsColumn.AllowEdit = false;
                    }
                    grvUserDuyet.Columns["ID_USER"].OptionsColumn.AllowEdit = true;
                    grvUserDuyet.Columns["INACTIVE"].OptionsColumn.AllowEdit = true;

                    //Visible
                    grvUserDuyet.Columns["ID_DQD"].Visible = false;
                    grvUserDuyet.Columns["ID_DU"].Visible = false;
                    grvUserDuyet.Columns["ID_DTL"].Visible = false;
                }
                else
                    grdUserDuyet.DataSource = dt1;

                DataTable dt2 = new DataTable();
                dt2 = ds.Tables[2].Copy();

                if (grdBuocDuyet.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdBuocDuyet, grvBuocDuyet, dt2, true, true, false, false, true, this.Name);

                    //Editable
                    for (int i = 0; i < grvBuocDuyet.Columns.Count; i++)
                    {
                        grvBuocDuyet.Columns[i].OptionsColumn.AllowEdit = false;
                    }
                    grvBuocDuyet.Columns["BUOC_DUYET"].OptionsColumn.AllowEdit = true;
                    grvBuocDuyet.Columns["ID_USER_DUYET"].OptionsColumn.AllowEdit = true;
                    grvBuocDuyet.Columns["QUYET_DINH"].OptionsColumn.AllowEdit = true;
                    grvBuocDuyet.Columns["BAT_BUOC"].OptionsColumn.AllowEdit = true;
                    grvBuocDuyet.Columns["VANG_MAT"].OptionsColumn.AllowEdit = true;
                    grvBuocDuyet.Columns["ID_USER_THAY_THE"].OptionsColumn.AllowEdit = true;
                    grvBuocDuyet.Columns["INACTIVE"].OptionsColumn.AllowEdit = true;
                    grvBuocDuyet.Columns["GHI_CHU"].OptionsColumn.AllowEdit = true;


                    //Visible
                    grvBuocDuyet.Columns["ID_DQD"].Visible = false;
                    grvBuocDuyet.Columns["ID_DB"].Visible = false;
                    grvBuocDuyet.Columns["ID_DTL"].Visible = false;
                }
                else
                    grdBuocDuyet.DataSource = dt2;

                LoadCbo_GridView();
                StatusControl();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void LoadCbo_GridView()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 4;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                dt_USER = new DataTable();
                dt_USER = ds.Tables[0].Copy();

                try
                {
                    //Load combo USER
                    RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
                    Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_USER", "USER_NAME", grvUserDuyet, dt_USER);
                    cbo.View.Columns["ID_USER"].Visible = false;
                }
                catch { }


                try
                {
                    //Load combo USER
                    RepositoryItemSearchLookUpEdit cbo1 = new RepositoryItemSearchLookUpEdit();
                    Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo1, "ID_USER", "USER_NAME", "ID_USER_DUYET", grvBuocDuyet, dt_USER, this.Name);

                    RepositoryItemSearchLookUpEdit cbo2 = new RepositoryItemSearchLookUpEdit();
                    Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo1, "ID_USER", "USER_NAME", "ID_USER_THAY_THE", grvBuocDuyet, dt_USER, this.Name);
                }
                catch { }
            }
            catch { }
        }

        private void LoadView()
        {
            try
            {
                XtraForm ctl = new XtraForm();
                Type newType = Type.GetType("Vs.Category.frmView", true, true);
                object o1 = Activator.CreateInstance(newType, 1, "", "spDuyetQuyDinh");
                ctl = o1 as XtraForm;
                ctl.Tag = "mnuDuyetQuyDinhView";
                ctl.Text = Commons.Modules.ObjLanguages.GetLanguage("frmDuyetQuyDinhView", "frmDuyetQuyDinhView");
                ctl.Name = "frmDuyetQuyDinhView";
                Commons.Modules.sPS = "mnuDuyetQuyDinhView";
                Commons.Modules.ObjSystems.LocationSizeForm(this, ctl);
                if (ctl.ShowDialog() == DialogResult.OK)
                {
                    iID_DQD = Convert.ToInt32(Commons.Modules.sId);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private bool KiemTrung()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 9;
                cmd.Parameters.Add("@SO_DQD", SqlDbType.NVarChar).Value = txtSO_DQD.Text;
                cmd.Parameters.Add("@ID_DQD", SqlDbType.Int).Value = iID_DQD;
                cmd.CommandType = CommandType.StoredProcedure;
                if (Convert.ToInt16(cmd.ExecuteScalar()) == 1)
                {
                    if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgSoDuyetQuyDinhBiTrungBanCoMunTangSo"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return true;
                    }
                    else
                    {
                        txtSO_DQD.Text = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_YCTD(" + datNGAY_HIEU_LUC.DateTime.ToString("MM/dd/yyyy") + ")").ToString();
                        return false;
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool KiemTrong()
        {
            try
            {
                grvUserDuyet.ClearFindFilter();
                for (int i = 0; i < grvUserDuyet.RowCount; i++)
                {
                    if ((string.IsNullOrEmpty(grvUserDuyet.GetRowCellValue(i, "ID_USER").ToString()) ? 0 : Convert.ToInt32(grvUserDuyet.GetRowCellValue(i, "ID_USER"))) == 0)
                    {
                        grvUserDuyet.FocusedRowHandle = i;
                        grvUserDuyet.FocusedColumn = grvUserDuyet.Columns["ID_USER"];
                        tabChung.SelectedTabPage = tabUserDuyet;
                        XtraMessageBox.Show(grvUserDuyet.Columns["ID_USER"].Caption.Trim() + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongDuocTrong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }
                }

                //Buoc duyet
                grvBuocDuyet.ClearFindFilter();
                for (int i = 0; i < grvBuocDuyet.RowCount; i++)
                {
                    if ((string.IsNullOrEmpty(grvBuocDuyet.GetRowCellValue(i, "BUOC_DUYET").ToString())))
                    {
                        grvBuocDuyet.FocusedRowHandle = i;
                        grvBuocDuyet.FocusedColumn = grvBuocDuyet.Columns["BUOC_DUYET"];
                        tabChung.SelectedTabPage = tabBuocDuyet;
                        XtraMessageBox.Show(grvBuocDuyet.Columns["BUOC_DUYET"].Caption.Trim() + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongDuocTrong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }

                    if ((string.IsNullOrEmpty(grvBuocDuyet.GetRowCellValue(i, "ID_USER_DUYET").ToString()) ? 0 : Convert.ToInt32(grvBuocDuyet.GetRowCellValue(i, "ID_USER_DUYET"))) == 0)
                    {
                        grvBuocDuyet.FocusedRowHandle = i;
                        grvBuocDuyet.FocusedColumn = grvBuocDuyet.Columns["ID_USER_DUYET"];
                        tabChung.SelectedTabPage = tabBuocDuyet;
                        XtraMessageBox.Show(grvBuocDuyet.Columns["ID_USER_DUYET"].Caption.Trim() + " " + Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongDuocTrong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }

                    //if ((string.IsNullOrEmpty(grvBuocDuyet.GetRowCellValue(i, "VANG_MAT").ToString()) ? 0 : Convert.ToInt32(grvBuocDuyet.GetRowCellValue(i, "VANG_MAT"))) == 1 && (string.IsNullOrEmpty(grvBuocDuyet.GetRowCellValue(i, "ID_USER_THAY_THE").ToString()) ? 0 : Convert.ToInt32(grvBuocDuyet.GetRowCellValue(i, "ID_USER_THAY_THE"))) == 0)
                    if ((string.IsNullOrEmpty(grvBuocDuyet.GetRowCellValue(i, "ID_USER_THAY_THE").ToString()) ? 0 : Convert.ToInt32(grvBuocDuyet.GetRowCellValue(i, "ID_USER_THAY_THE"))) == 0)
                    {
                        grvBuocDuyet.FocusedRowHandle = i;
                        grvBuocDuyet.FocusedColumn = grvBuocDuyet.Columns["ID_USER_THAY_THE"];
                        tabChung.SelectedTabPage = tabBuocDuyet;
                        XtraMessageBox.Show(grvBuocDuyet.Columns["ID_USER_THAY_THE"].Caption.Trim() + " " + Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgKhongDuocTrong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }

            return false;
        }

        public bool IsNumber(string Value)
        {
            string _Value = Value;
            foreach (Char c in _Value)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }

        private int Tinh_Max_Cot(DataTable dt, string Column)
        {
            DataTable _dt = dt;
            string _Column = Column;

            int Max = 0;
            for (int i = 0; i < +_dt.Rows.Count; i++)
            {
                if (Max < (string.IsNullOrEmpty(_dt.Rows[i][_Column].ToString()) ? 0 : Convert.ToInt32(_dt.Rows[i][_Column])))
                    Max = (string.IsNullOrEmpty(_dt.Rows[i][_Column].ToString()) ? 0 : Convert.ToInt32(_dt.Rows[i][_Column]));
            }
            return Max;
        }

        private void StatusControl()
        {
            try
            {
                chkINACTIVE.Enabled = true;


                if (KiemSuDung(iID_DQD))
                {
                    datNGAY_HIEU_LUC.ReadOnly = true;
                    txtTEN_QUY_DINH.ReadOnly = true;
                    txtTEN_QUY_DINH_A.ReadOnly = true;
                    txtTEN_QUY_DINH_H.ReadOnly = true;
                    cboID_DTL.ReadOnly = true;
                    txtDIEU_KIEN_DUYET.ReadOnly = true;

                    for (int i = 0; i < grvBuocDuyet.Columns.Count; i++)
                    {
                        grvBuocDuyet.Columns[i].OptionsColumn.AllowEdit = false;
                    }
                    grvBuocDuyet.Columns["INACTIVE"].OptionsColumn.AllowEdit = true;
                    grvBuocDuyet.Columns["VANG_MAT"].OptionsColumn.AllowEdit = true;
                    grvBuocDuyet.Columns["ID_USER_THAY_THE"].OptionsColumn.AllowEdit = true;

                    for (int i = 0; i < grvUserDuyet.Columns.Count; i++)
                    {
                        grvUserDuyet.Columns[i].OptionsColumn.AllowEdit = false;
                    }

                    grvBuocDuyet.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

                    grvUserDuyet.Columns["INACTIVE"].OptionsColumn.AllowEdit = true;
                    grvUserDuyet.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
                }
                else
                {
                    datNGAY_HIEU_LUC.ReadOnly = false;
                    txtTEN_QUY_DINH.ReadOnly = false;
                    txtTEN_QUY_DINH_A.ReadOnly = false;
                    txtTEN_QUY_DINH_H.ReadOnly = false;
                    cboID_DTL.ReadOnly = false;
                    txtDIEU_KIEN_DUYET.ReadOnly = false;

                    for (int i = 0; i < grvBuocDuyet.Columns.Count; i++)
                    {
                        grvBuocDuyet.Columns[i].OptionsColumn.AllowEdit = false;
                    }
                    grvBuocDuyet.Columns["BUOC_DUYET"].OptionsColumn.AllowEdit = true;
                    grvBuocDuyet.Columns["ID_USER_DUYET"].OptionsColumn.AllowEdit = true;
                    grvBuocDuyet.Columns["QUYET_DINH"].OptionsColumn.AllowEdit = true;
                    grvBuocDuyet.Columns["BAT_BUOC"].OptionsColumn.AllowEdit = true;
                    grvBuocDuyet.Columns["VANG_MAT"].OptionsColumn.AllowEdit = true;
                    grvBuocDuyet.Columns["ID_USER_THAY_THE"].OptionsColumn.AllowEdit = true;
                    grvBuocDuyet.Columns["INACTIVE"].OptionsColumn.AllowEdit = true;
                    grvBuocDuyet.Columns["GHI_CHU"].OptionsColumn.AllowEdit = true;

                    grvBuocDuyet.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;

                    for (int i = 0; i < grvUserDuyet.Columns.Count; i++)
                    {
                        grvUserDuyet.Columns[i].OptionsColumn.AllowEdit = false;
                    }
                    grvUserDuyet.Columns["ID_USER"].OptionsColumn.AllowEdit = true;
                    grvUserDuyet.Columns["INACTIVE"].OptionsColumn.AllowEdit = true;

                    grvUserDuyet.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
                }
            }
            catch { }
        }

        private bool KiemSuDung(Int64 ID_DQD)
        {
            try
            {
                List<SqlParameter> lPar = new List<SqlParameter>
                {
                    new SqlParameter("@iLoai", 10),
                    new SqlParameter("@ID_DQD", ID_DQD),
                };

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 10;
                cmd.Parameters.Add("@ID_DQD", SqlDbType.NVarChar).Value = iID_DQD;
                cmd.CommandType = CommandType.StoredProcedure;

                int iTemp = Convert.ToInt32(cmd.ExecuteScalar());
                if (iTemp == 1)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

       
    }
}
