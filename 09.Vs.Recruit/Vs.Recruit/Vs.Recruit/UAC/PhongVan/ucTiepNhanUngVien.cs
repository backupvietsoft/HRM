using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
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

namespace Vs.Recruit
{
    public partial class ucTiepNhanUngVien : DevExpress.XtraEditors.XtraUserControl
    {
        public AccordionControl accorMenuleft;
        public ucTiepNhanUngVien()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, btnALL);
        }
        private void ucTiepNhanUngVien_Load(object sender, EventArgs e)
        {
            try
            {

                Commons.Modules.sLoad = "0Load";
                Commons.OSystems.SetDateEditFormat(datTNgay);
                Commons.OSystems.SetDateEditFormat(datDNgay);
                Commons.Modules.sLoad = "";
                datTNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year));
                LoadCbo();
                LoadData();
                LoadLuoiND();
                grvDSUngVien_FocusedRowChanged(null, null);

                enabel(true);
            }
            catch (Exception ex)
            {
            }
        }
        private void LoadData()
        {
            try
            {
                //DataTable dt = new DataTable();
                //dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spTiepNhanUV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToInt64(cboID_PV.EditValue)));
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUV", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_PV", SqlDbType.BigInt).Value = Convert.ToInt64(cboID_PV.EditValue);
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datTNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datDNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.Columns["XAC_NHAN_DL"].ReadOnly = false;
                dt.Columns["NGAY_CO_THE_DI_LAM"].ReadOnly = false;
                dt.Columns["XAC_NHAN_DL"].ReadOnly = false;
                dt.Columns["NGAY_NHAN_VIEC"].ReadOnly = false;
                dt.Columns["ID_DGTN"].ReadOnly = false;
                dt.Columns["XAC_NHAN_DTDH"].ReadOnly = false;
                dt.Columns["DA_GIOI_THIEU"].ReadOnly = false;
                dt.Columns["HUY_TUYEN_DUNG"].ReadOnly = false;
                if (grdDSUngVien.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSUngVien, grvDSUngVien, dt, true, true, false, true, true, this.Name);
                    grvDSUngVien.Columns["ID_UV"].Visible = false;
                    grvDSUngVien.Columns["MS_UV"].OptionsColumn.AllowEdit = false;
                    grvDSUngVien.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                }
                else
                {
                    grdDSUngVien.DataSource = dt;
                }
                //Commons.Modules.ObjSystems.AddCombXtra("ID_DGTN", "TEN_DGTN", "TEN_DGTN", grvDSUngVien, Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false), true, "ID_DGTN", this.Name, true);

                //ID_YCTD,MA_YCTD
                //Danh sach benh vien
                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboDGTN = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                cboDGTN.NullText = "";
                cboDGTN.ValueMember = "ID_DGTN";
                cboDGTN.DisplayMember = "TEN_DGTN";
                //ID_VTTD,TEN_VTTD
                cboDGTN.DataSource = Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false);
                cboDGTN.Columns.Clear();
                cboDGTN.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_DGTN"));
                cboDGTN.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_DGTN"));
                cboDGTN.Columns["TEN_DGTN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_DGTN");
                cboDGTN.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboDGTN.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cboDGTN.Columns["ID_DGTN"].Visible = false;
                grvDSUngVien.Columns["ID_DGTN"].ColumnEdit = cboDGTN;
                cboDGTN.BeforePopup += cboDGTN_BeforePopup;
                cboDGTN.EditValueChanged += cboDGTN_EditValueChanged;
            }
            catch (Exception ex) { }
        }

        private void cboDGTN_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSUngVien.SetFocusedRowCellValue("ID_DGTN", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboDGTN_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false);
            }
            catch { }
        }
        private void LoadCbo()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetComboPV_TheoNgay", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@CoAll", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datTNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datDNgay.EditValue);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_PV, dt, "ID_PV", "MA_SO", "MA_SO");
                if (dt.Rows.Count == 1)
                {
                    cboID_PV.Properties.DataSource = dt.Clone();
                    cboID_PV.EditValue = 0;
                }
            }
            catch { }
        }
        private void LoadLuoiND()
        {
            string sBTUngVien = "sBTUngVien" + Commons.Modules.iIDUser;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTUngVien, Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien), "");
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListNoiDungDaoTao", Commons.Modules.UserName, Commons.Modules.TypeLanguage, sBTUngVien));
                Commons.Modules.ObjSystems.XoaTable(sBTUngVien);
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNoiDung, grvNoiDung, dt, true, true, true, false, true, this.Name);
                //grvNoiDung.Columns["ID_NDDT"].Visible = false;
                grvNoiDung.Columns["ID_UV"].Visible = false;

                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboNDDT = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                cboNDDT.NullText = "";
                cboNDDT.ValueMember = "ID_NDDT";
                cboNDDT.DisplayMember = "TEN_NDDT";
                //ID_VTTD,TEN_VTTD

                cboNDDT.DataSource = Commons.Modules.ObjSystems.DataDanhNoiDungDT(false);
                cboNDDT.Columns.Clear();
                cboNDDT.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_NDDT"));
                cboNDDT.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_NDDT"));
                cboNDDT.Columns["TEN_NDDT"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_NDDT");
                cboNDDT.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboNDDT.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cboNDDT.Columns["ID_NDDT"].Visible = false;
                grvNoiDung.Columns["ID_NDDT"].ColumnEdit = cboNDDT;
                cboNDDT.BeforePopup += cboNDDT_BeforePopup;
                cboNDDT.EditValueChanged += cboNDDT_EditValueChanged;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBTUngVien);
            }
        }
        private void cboNDDT_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSUngVien.SetFocusedRowCellValue("ID_NDDT", Convert.ToInt64((dataRow.Row[0])));
        }

        private void cboNDDT_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataDanhNoiDungDT(false);
            }
            catch { }
        }
        private void enabel(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = visible;
            btnALL.Buttons[3].Properties.Visible = !visible;
            btnALL.Buttons[4].Properties.Visible = !visible;
            btnALL.Buttons[5].Properties.Visible = visible;

            grvDSUngVien.OptionsBehavior.Editable = !visible;
            grvNoiDung.OptionsBehavior.Editable = !visible;

        }

        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "them":
                        {
                            enabel(false);
                            break;
                        }
                    case "sua":
                        {
                            enabel(false);
                            Commons.Modules.ObjSystems.AddnewRow(grvNoiDung, true);
                            break;
                        }
                    case "Ingiayhen":
                        {
                            if (grvDSUngVien.RowCount == 0) return;
                            frmViewReport frm = new frmViewReport();
                            System.Data.SqlClient.SqlConnection conn;
                            DataTable dt = new DataTable();
                            frm.rpt = new rptGiayHenDiLam();
                            try
                            {
                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();

                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptGiayHenDiLam", conn);
                                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                cmd.Parameters.Add("@ID_PV", SqlDbType.Int).Value = Convert.ToInt64(cboID_PV.EditValue);
                                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datTNgay.EditValue);
                                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datDNgay.EditValue);
                                cmd.CommandType = CommandType.StoredProcedure;
                                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                DataSet ds = new DataSet();
                                adp.Fill(ds);
                                dt = new DataTable();
                                dt = ds.Tables[0].Copy();
                                dt.TableName = "DATA";
                                frm.AddDataSource(dt);

                            }
                            catch
                            {
                            }

                            frm.ShowDialog();
                            break;
                        }
                    case "xoa":
                        {
                            xoaNoiDungDT();
                            grvDSUngVien_FocusedRowChanged(null, null);
                            enabel(true);
                            break;
                        }
                    case "ghi":
                        {
                            if (!SaveData()) return;
                            LoadData();
                            LoadLuoiND();
                            grvDSUngVien_FocusedRowChanged(null, null);
                            Commons.Modules.ObjSystems.DeleteAddRow(grvNoiDung);
                            enabel(true);
                            break;
                        }
                    case "khongghi":
                        {
                            LoadData();
                            LoadLuoiND();
                            grvDSUngVien_FocusedRowChanged(null, null);
                            Commons.Modules.ObjSystems.DeleteAddRow(grvNoiDung);
                            enabel(true);
                            break;
                        }
                    case "thoat":
                        {
                            Commons.Modules.ObjSystems.GotoHome(this);
                            break;
                        }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void grvDSUngVien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //LoadLuoiND();
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                //Commons.Modules.ObjSystems.RowFilter(grdTuan, grvTuan.Columns["ID_YCTD"], grvTuan.Columns["ID_VTTD"], grvVTYC.GetFocusedRowCellValue("ID_YCTD").ToString(), grvVTYC.GetFocusedRowCellValue("ID_VTTD").ToString());

                Commons.Modules.ObjSystems.RowFilter(grdNoiDung, grvNoiDung.Columns["ID_UV"], grvDSUngVien.GetFocusedRowCellValue("ID_UV").ToString());
            }
            catch (Exception ex)
            {
            }
        }

        private void datTNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            int t = DateTime.DaysInMonth(datTNgay.DateTime.Year, datTNgay.DateTime.Month);
            DateTime secondDateTime = new DateTime(datTNgay.DateTime.Year, Convert.ToInt32(datTNgay.DateTime.Month), t);
            datDNgay.EditValue = secondDateTime;
            LoadCbo();
            //cboID_KHPV_EditValueChanged(null, null);
            //LoadData();
        }
        private void datDNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadCbo();
            //cboID_KHPV_EditValueChanged(null, null);
            //LoadData();
        }
        private bool SaveData()
        {
            string sBTUngVien = "sBTUngVien" + Commons.Modules.iIDUser;
            string sBTNoiDung = "sBTNoiDung" + Commons.Modules.iIDUser;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTUngVien, Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien), "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTNoiDung, Commons.Modules.ObjSystems.ConvertDatatable(grdNoiDung), "");

                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spSaveTiepNhanUngVien", Commons.Modules.UserName, Commons.Modules.TypeLanguage, sBTUngVien, sBTNoiDung));
                Commons.Modules.ObjSystems.XoaTable(sBTUngVien);
                Commons.Modules.ObjSystems.XoaTable(sBTNoiDung);
                return true;
            }
            catch
            {
                Commons.Modules.ObjSystems.XoaTable(sBTUngVien);
                Commons.Modules.ObjSystems.XoaTable(sBTNoiDung);
                return false;
            }
        }
        private void xoaNoiDungDT()
        {
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteNoiDungDaoTao"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.UV_ND_DAO_TAO WHERE ID_NDDT = " + grvNoiDung.GetFocusedRowCellValue("ID_NDDT") + " AND ID_UV = " + grvNoiDung.GetFocusedRowCellValue("ID_UV") + "");
                LoadLuoiND();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void grvNoiDung_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                if (grvDSUngVien.RowCount == 0)
                {
                    grvNoiDung.DeleteSelectedRows();
                    return;
                }
                grvNoiDung.SetFocusedRowCellValue("ID_UV", grvDSUngVien.GetFocusedRowCellValue("ID_UV"));
            }
            catch
            {
            }
        }

        private void grvNoiDung_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            grvDSUngVien.ClearColumnErrors();
            try
            {
                DataTable dt = new DataTable();
                if (grvNoiDung == null) return;
                if (grvNoiDung.FocusedColumn.FieldName == "ID_NDDT")
                {//kiểm tra máy không được để trống
                    if (string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        e.Valid = false;
                        e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erMayKhongTrong");
                        grvNoiDung.SetColumnError(grvNoiDung.Columns["ID_NDDT"], e.ErrorText);
                        return;
                    }
                    else
                    {
                        grvNoiDung.SetFocusedRowCellValue("ID_NDDT", e.Value);
                        dt = new DataTable();
                        dt = Commons.Modules.ObjSystems.ConvertDatatable(grvNoiDung);
                        if (dt.AsEnumerable().Count(x => x.Field<Int64>("ID_NDDT").Equals(e.Value)) > 1)
                        {
                            e.Valid = false;
                            e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrungDuLieu");
                            grvNoiDung.SetColumnError(grvNoiDung.Columns["ID_NDDT"], e.ErrorText);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void grdNoiDung_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (btnALL.Buttons[3].Properties.Visible == false && e.KeyData == System.Windows.Forms.Keys.Delete)
            {
                grvNoiDung.DeleteSelectedRows();
            }
        }
        private void grvNoiDung_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvNoiDung_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void cboID_PV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }

        private void grvDSUngVien_RowCountChanged(object sender, EventArgs e)
        {
            grvDSUngVien_FocusedRowChanged(null, null);
        }
    }
}
