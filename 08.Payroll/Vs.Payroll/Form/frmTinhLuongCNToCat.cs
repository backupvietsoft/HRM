﻿using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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

namespace Vs.Payroll
{
    public partial class frmTinhLuongCNToCat : DevExpress.XtraEditors.XtraForm
    {
        public int iID_TO = -1;
        public DateTime dNgay;
        private bool isAdd = false;
        public double fTongDoanhThu = 0;
        public frmTinhLuongCNToCat()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, windowsUIButton);
        }

        private void frmTinhLuongCNToCat_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTO, Commons.Modules.ObjSystems.DataTo(-1, -1, false), "ID_TO", "TEN_TO", "TEN_TO");
                cboTO.EditValue = iID_TO;
                datThang.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                datThang.Properties.DisplayFormat.FormatString = "MM/yyyy";
                datThang.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                datThang.Properties.EditFormat.FormatString = "MM/yyyy";
                datThang.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                datThang.Properties.Mask.EditMask = "MM/yyyy";
                datThang.EditValue = dNgay.ToString("MM/yyyy");
                LoadData();
                EnabelButton(isAdd);
            }
            catch { }
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "themsua":
                        {
                            isAdd = true;
                            LoadData();
                            EnabelButton(isAdd);
                            break;
                        }
                    case "In":
                        {
                            frmViewReport frm = new frmViewReport();
                            frm.rpt = new rptDSCNToCat(dNgay);
                            DataTable dt = new DataTable();
                            dt = (DataTable)grdData.DataSource;
                            dt.TableName = "DATA";
                            frm.AddDataSource(dt);
                            frm.ShowDialog();
                            break;
                        }
                    case "thoat":
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            break;
                        }
                    case "luu":
                        {
                            grvData.CloseEditor();
                            grvData.UpdateCurrentRow();
                            Validate();
                            if (grvData.HasColumnErrors) return;
                            DataTable dt_CHON = new DataTable();
                            dt_CHON = ((DataTable)grdData.DataSource);
                            //dt_CHON = Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien);
                            if (dt_CHON.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).Count() == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (Savedata() == false)
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                            }
                            isAdd = false;
                            LoadData();
                            EnabelButton(isAdd);
                            break;
                        }
                    case "khongluu":
                        {
                            isAdd = false;
                            LoadData();
                            EnabelButton(isAdd);
                            break;
                        }
                    case "xoa":
                        {
                            if (grvData.RowCount == 0) return;
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_XoaDong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE FROM dbo.LUONG_CONG_NHAN_CAT WHERE ID = " + grvData.GetFocusedRowCellValue("ID") + "");
                            break;
                        }
                }
            }
            catch (Exception ex) { }
        }
        private void EnabelButton(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = !visible;
            windowsUIButton.Buttons[1].Properties.Visible = !visible;
            windowsUIButton.Buttons[2].Properties.Visible = !visible;
            windowsUIButton.Buttons[3].Properties.Visible = !visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = visible;
            grvData.OptionsBehavior.Editable = visible;
        }
        private void LoadData()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTinhLuongCNCat", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = iID_TO;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@iThem", SqlDbType.Int).Value = isAdd;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(dNgay.ToString("dd/MM/yyyy"));
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, true, true, true, true, this.Name);
                    grvData.Columns["ID_CN"].Visible = false;
                    grvData.Columns["ID_CNC"].Visible = false;
                    grvData.Columns["HE_SO"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["HE_SO"].DisplayFormat.FormatString = "0.0";
                    grvData.Columns["SG_LV_TT"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["SG_LV_TT"].DisplayFormat.FormatString = "0.00";
                    grvData.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["SG_LV_TT"].OptionsColumn.AllowEdit = false;
                    grvData.Columns["CHON"].Visible = false;
                }
                else
                {
                    grdData.DataSource = dt;
                }
                if (isAdd)
                {
                    grvData.OptionsSelection.MultiSelect = true;
                    grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                }
                else
                {
                    grvData.OptionsSelection.MultiSelect = false;
                    grvData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                }

                try
                {
                    grvData.OptionsSelection.CheckBoxSelectorField = "CHON";
                    grvData.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
                catch { }
            }
            catch (Exception ex)
            {
            }
        }
        private bool Savedata()
        {
            string sTB = "sBTTinhLuong" + Commons.Modules.iIDUser;
            try
            {

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grdData), "");
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTinhLuongCNCat", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 3;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sTB;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = dNgay;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                Commons.Modules.ObjSystems.XoaTable(sTB);
                return true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sTB);
                return false;
            }
        }
        private void grvData_RowCountChanged(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            try
            {

                if (view.RowCount > 0)
                {
                    ItemForSumNhanVien.Text = "Tổng số công nhân viên" + ": " + view.RowCount.ToString();
                }
                else
                {
                    ItemForSumNhanVien.Text = "Tổng số công nhân viên" + ": 0";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
    }
}