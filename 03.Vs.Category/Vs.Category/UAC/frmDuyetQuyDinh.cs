using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Vs.Category
{
    public partial class frmDuyetQuyDinh : DevExpress.XtraEditors.XtraUserControl
    {
        static int iPQ = -1; // =1: full, <>1: read only
        private int iID_DQD = -1;
        private DataTable dt_USER;
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
            txtSO_DQD.ReadOnly = true;
        }

        #region Event
        private void frmDuyetQuyDinh_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                LoadCbo();
                LoadgrdQuyDinh(-1);
                BindingData(false);
                enableButon(true);
                Commons.Modules.sLoad = "";
                LoadNN();
                Commons.Modules.ObjSystems.ThayDoiNN(this, Root, tabChung, btnALL);
            }
            catch { }
            Commons.Modules.sLoad = "";
        }

        private void cboID_DTL_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (Commons.Modules.sLoad == "0Load") return;
            //    if (cboID_DTL.EditValue == null) return;
            //    DataTable dt = new DataTable();
            //    dt = ((DataTable)cboID_DTL.Properties.DataSource).Copy();

            //    try
            //    {
            //        dt = dt.AsEnumerable().Where(r => r.Field<int>("ID_DTL").Equals(cboID_DTL.EditValue)).CopyToDataTable();
            //    }
            //    catch { dt = dt.Clone(); }

            //}
            //catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
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


        private void grvUserDuyet_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            //kiểm tra trùng user 
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                if (Commons.Modules.ObjSystems.IsnullorEmpty(View.GetRowCellValue(e.RowHandle, "ID_USER")) || View.GetRowCellValue(e.RowHandle, "ID_USER").ToString() == "-99")
                {
                    e.Valid = false;
                    View.SetColumnError(View.Columns["ID_USER"], Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrongDuLieu")); return;
                }
                DataTable dt = Commons.Modules.ObjSystems.ConvertDatatable(View);
                int n = dt.AsEnumerable().Count(x => x["ID_USER"].ToString().Equals(View.GetFocusedRowCellValue("ID_USER").ToString()));
                if (n > 1)
                {
                    e.Valid = false;
                    View.SetColumnError(View.Columns["ID_USER"], Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrungDuLieu"));
                    return;
                }
            }
            catch
            {
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
                DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                if ((string.IsNullOrEmpty(grvBuocDuyet.GetRowCellValue(e.RowHandle, grvBuocDuyet.Columns["BUOC_DUYET"]).ToString()) ? 0 : Convert.ToInt32(grvBuocDuyet.GetRowCellValue(e.RowHandle, grvBuocDuyet.Columns["BUOC_DUYET"]))) <= 0)
                {
                    e.Valid = false;
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBuocDuyetPhaiLonHon0"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //kiểm tra trùng user 
                if (Commons.Modules.ObjSystems.IsnullorEmpty(View.GetRowCellValue(e.RowHandle, "ID_USER_DUYET")) || View.GetRowCellValue(e.RowHandle, "ID_USER_DUYET").ToString() == "-99")
                {
                    e.Valid = false;
                    View.SetColumnError(View.Columns["ID_USER_DUYET"], Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrongDuLieu")); return;
                }
                DataTable dt = Commons.Modules.ObjSystems.ConvertDatatable(View);
                int n = dt.AsEnumerable().Count(x => x["ID_USER_DUYET"].ToString().Equals(View.GetFocusedRowCellValue("ID_USER_DUYET").ToString()));
                if (n > 1)
                {
                    e.Valid = false;
                    View.SetColumnError(View.Columns["ID_USER_DUYET"], Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrungDuLieu"));
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
                    if (KiemSuDung(iID_DQD) || grvUserDuyet.FocusedRowHandle < 0 ) return;


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
                    if (KiemSuDung(iID_DQD) ||  grvBuocDuyet.FocusedRowHandle < 0 ) return;

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
                        grvBuocDuyet.DeleteSelectedRows();
                        ((DataTable)grdBuocDuyet.DataSource).AcceptChanges();
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
                case "them":
                    {
                        enableButon(false);
                        BindingData(true);
                        Commons.Modules.ObjSystems.AddnewRow(grvUserDuyet, true);
                        Commons.Modules.ObjSystems.AddnewRow(grvBuocDuyet, true);
                        break;
                    }
                case "sua":
                    {
                        if (txtSO_DQD.Text.ToString() == "") return;
                        enableButon(false);
                        Commons.Modules.ObjSystems.AddnewRow(grvUserDuyet, true);
                        Commons.Modules.ObjSystems.AddnewRow(grvBuocDuyet, true);
                        break;
                    }

                case "luu":
                    {
                        if (!dxValidationProvider1.Validate()) return;
                        Validate();
                        if (grvUserDuyet.HasColumnErrors) return;
                        if (grvBuocDuyet.HasColumnErrors) return;
                        if (KiemTrung()) return;
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
                            cmd.Parameters.Add("@NGAY_HIEU_LUC", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datNGAY_HIEU_LUC.Text);
                            cmd.Parameters.Add("@ID_DTL", SqlDbType.BigInt).Value = cboID_DTL.EditValue;
                            cmd.Parameters.Add("@DIEU_KIEN_DUYET", SqlDbType.NVarChar).Value = txtDIEU_KIEN_DUYET.EditValue;
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
                                iID_DQD = iTEMP;
                                LoadgrdQuyDinh(iID_DQD);
                            }
                            else
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgGhiKhongThanhCong") + "\n" + sTEMP, Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            enableButon(true);
                            Commons.Modules.ObjSystems.DeleteAddRow(grvUserDuyet);
                            Commons.Modules.ObjSystems.DeleteAddRow(grvBuocDuyet);
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgGhiKhongThanhCong") + ex.Message, Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    }
                case "khongluu":
                    {
                        try
                        {
                            LoadgrdQuyDinh(iID_DQD);
                            enableButon(true);
                            Commons.Modules.ObjSystems.DeleteAddRow(grvUserDuyet);
                            Commons.Modules.ObjSystems.DeleteAddRow(grvBuocDuyet);
                        }
                        catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
                        break;
                    }
                case "xoa":
                    {
                        XoaQuyDinh();
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

        private void XoaQuyDinh()
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
                    LoadgrdQuyDinh(-1);
                    return;
                }
                else
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgXoaKhongThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgXoaKhongThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

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
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_DTL, dt, "ID_DTL", "TEN_TAI_LIEU", this.Name, true, true);
                    cboID_DTL.Properties.View.Columns["QUERY"].Visible = false;
                }
                catch { }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void LoadgrdQuyDinh(Int64 iID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_DQD, SO_DQD, TEN_QUY_DINH, TEN_QUY_DINH_A, TEN_QUY_DINH_H, NGAY_HIEU_LUC, A.ID_DTL,CASE 0 WHEN 0 THEN B.TEN_TAI_LIEU ELSE B.TEN_TAI_LIEU_A END TEN_TAI_LIEU, DIEU_KIEN_DUYET, INACTIVE, A.GHI_CHU FROM dbo.DUYET_QUY_DINH A INNER JOIN dbo.DUYET_TAI_LIEU B ON B.ID_DTL = A.ID_DTL ORDER BY SO_DQD"));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_DQD"] };
                if (grdQuyDinh.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdQuyDinh, grvQuyDinh, dt, false, false, true, true, true, this.Name);
                    grvQuyDinh.Columns["ID_DQD"].Visible = false;
                    grvQuyDinh.Columns["ID_DTL"].Visible = false;
                    grvQuyDinh.Columns["DIEU_KIEN_DUYET"].Visible = false;
                    grvQuyDinh.Columns["TEN_QUY_DINH_A"].Visible = false;
                    grvQuyDinh.Columns["TEN_QUY_DINH_H"].Visible = false;
                    grvQuyDinh.Columns["INACTIVE"].Visible = false;
                    grvQuyDinh.Columns["GHI_CHU"].Visible = false;
                    grvQuyDinh.Columns["NGAY_HIEU_LUC"].Visible = false;
                }
                else
                {
                    grdQuyDinh.DataSource = dt;
                }
                if (iID != -1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(iID));
                    grvQuyDinh.FocusedRowHandle = grvQuyDinh.GetRowHandle(index);
                }
                grvQuyDinh_FocusedRowChanged(null, null);
            }
            catch
            {
            }
        }

        private void BindingData(bool them)
        {
            if (them == true)
            {
                iID_DQD = -1;
                txtSO_DQD.Text = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_QUYDINH(" + datNGAY_HIEU_LUC.DateTime.ToString("MM/dd/yyyy") + ")").ToString();
                datNGAY_HIEU_LUC.EditValue = DateTime.Now;
                txtTEN_QUY_DINH.EditValue = "";
                txtTEN_QUY_DINH_A.EditValue = "";
                txtTEN_QUY_DINH_H.EditValue = "";
                txtDIEU_KIEN_DUYET.EditValue = "";
                cboID_DTL.EditValue = null;
                chkINACTIVE.Checked = false;
            }
            else // Load data vao text
            {
                try
                {
                    txtSO_DQD.EditValue = grvQuyDinh.GetFocusedRowCellValue("SO_DQD").ToString();
                    try
                    {
                        datNGAY_HIEU_LUC.EditValue = Convert.ToDateTime(grvQuyDinh.GetFocusedRowCellValue("NGAY_HIEU_LUC"));
                    }
                    catch
                    {
                        datNGAY_HIEU_LUC.EditValue = "";
                    }
                    iID_DQD = Convert.ToInt32(grvQuyDinh.GetFocusedRowCellValue("ID_DQD").ToString());
                    txtTEN_QUY_DINH.EditValue = grvQuyDinh.GetFocusedRowCellValue("TEN_QUY_DINH").ToString();
                    txtTEN_QUY_DINH_A.EditValue = grvQuyDinh.GetFocusedRowCellValue("TEN_QUY_DINH_A").ToString();
                    txtTEN_QUY_DINH_H.EditValue = grvQuyDinh.GetFocusedRowCellValue("TEN_QUY_DINH_H").ToString();
                    txtDIEU_KIEN_DUYET.EditValue = grvQuyDinh.GetFocusedRowCellValue("DIEU_KIEN_DUYET").ToString();
                    cboID_DTL.EditValue = Convert.ToInt32(grvQuyDinh.GetFocusedRowCellValue("ID_DTL").ToString());
                    chkINACTIVE.Checked = Convert.ToBoolean(grvQuyDinh.GetFocusedRowCellValue("INACTIVE").ToString());
                }
                catch
                {
                    iID_DQD = -1;
                    txtSO_DQD.Text = "";
                    datNGAY_HIEU_LUC.EditValue = null;
                    txtTEN_QUY_DINH.EditValue = "";
                    txtTEN_QUY_DINH_A.EditValue = "";
                    txtTEN_QUY_DINH_H.EditValue = "";
                    txtDIEU_KIEN_DUYET.EditValue = "";
                    cboID_DTL.EditValue = null;
                    chkINACTIVE.Checked = false;
                }
            }
            LoadData();
        }
        private void enableButon(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = visible;
            btnALL.Buttons[3].Properties.Visible = visible;
            btnALL.Buttons[4].Properties.Visible = !visible;
            btnALL.Buttons[5].Properties.Visible = !visible;
            btnALL.Buttons[6].Properties.Visible = visible;
            grdQuyDinh.Enabled = visible;
            txtSO_DQD.Properties.ReadOnly = visible;
            datNGAY_HIEU_LUC.Properties.ReadOnly = visible;
            datNGAY_HIEU_LUC.Properties.Buttons[0].Enabled = !datNGAY_HIEU_LUC.Properties.ReadOnly;
            txtTEN_QUY_DINH.Properties.ReadOnly = visible;
            txtTEN_QUY_DINH_A.Properties.ReadOnly = visible;
            txtTEN_QUY_DINH_H.Properties.ReadOnly = visible;
            txtDIEU_KIEN_DUYET.Properties.ReadOnly = visible;
            chkINACTIVE.Properties.ReadOnly = visible;
            cboID_DTL.Properties.ReadOnly = visible;
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
                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[1].Copy();

                if (grdUserDuyet.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdUserDuyet, grvUserDuyet, dt1, false, true, false, false, true, this.Name);

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
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdBuocDuyet, grvBuocDuyet, dt2, false, true, false, false, true, this.Name);
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
                //StatusControl();
                Commons.Modules.ObjSystems.DeleteAddRow(grvBuocDuyet);
                Commons.Modules.ObjSystems.DeleteAddRow(grvUserDuyet);
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
                    //Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_USER", "USER_NAME", grvUserDuyet, dt_USER);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_USER", "USER_NAME", grvUserDuyet,dt_USER,true,"ID_USER",this.Name);
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
                        txtSO_DQD.Text = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_QUYDINH(" + datNGAY_HIEU_LUC.DateTime.ToString("MM/dd/yyyy") + ")").ToString();
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
                    //grvBuocDuyet.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
                    grvUserDuyet.Columns["INACTIVE"].OptionsColumn.AllowEdit = true;
                    //grvUserDuyet.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
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

                    //grvBuocDuyet.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;

                    for (int i = 0; i < grvUserDuyet.Columns.Count; i++)
                    {
                        grvUserDuyet.Columns[i].OptionsColumn.AllowEdit = false;
                    }
                    grvUserDuyet.Columns["ID_USER"].OptionsColumn.AllowEdit = true;
                    grvUserDuyet.Columns["INACTIVE"].OptionsColumn.AllowEdit = true;

                    //grvUserDuyet.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
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

        private void grvQuyDinh_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            BindingData(false);
        }

        private void grdQuyDinh_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                XoaQuyDinh();
            }
        }

        private void grvBuocDuyet_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
    }
}
