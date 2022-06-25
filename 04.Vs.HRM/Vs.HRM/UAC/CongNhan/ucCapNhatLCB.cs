using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraLayout;
using System.Threading;

namespace Vs.HRM
{
    public partial class ucCapNhatLCB : DevExpress.XtraEditors.XtraUserControl
    {
        public static ucCapNhatLCB _instance;
        public static ucCapNhatLCB Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucCapNhatLCB();
                return _instance;
            }
        }

        string sbt = Commons.Modules.UserName;
        public ucCapNhatLCB()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, windowsUIButton);
        }
        #region envent form
        private void ucCapNhatLCB_Load(object sender, EventArgs e)
        {
            formatText();
            //load đơn vị xí nghiệp tổ
            Commons.Modules.sLoad = "0Load";
            NGAY_HIEU_LUCdateEdit.EditValue = DateTime.Now;
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(BAC_CVlookUpEdit, Commons.Modules.ObjSystems.DataNgachLuong(true), "ID_NL", "MS_NL", "MS_NL");
            Commons.Modules.ObjSystems.MLoadLookUpEdit(BAC_HDlookUpEdit, Commons.Modules.ObjSystems.DataBacLuong(Convert.ToInt64(BAC_CVlookUpEdit.EditValue), Convert.ToDateTime(NGAY_HIEU_LUCdateEdit.EditValue), true), "ID_BL", "TEN_BL", "TEN_BL");
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(ID_NLlookUpEdit, Commons.Modules.ObjSystems.DataNgachLuong(false), "ID_NL", "MS_NL", "MS_NL");
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_BLlookUpEdit, Commons.Modules.ObjSystems.DataBacLuong(Convert.ToInt64(ID_NLlookUpEdit.EditValue), Convert.ToDateTime(NGAY_HIEU_LUCdateEdit.EditValue), false), "ID_BL", "TEN_BL", "TEN_BL");
            LoadGrdCapNhatLCB();
            Commons.Modules.ObjSystems.MLoadLookUpEdit(COT_CAP_NHATlookUpEdit, Commons.Modules.ObjSystems.DataCotCapNhat(false), "ID_COT", "TEN_COT", "TEN_COT");
            radBoloc_EditValueChanged(null, null);
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            Commons.Modules.sLoad = "";
        }

        private void formatText()
        {
            LUONG_CO_BANtextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            SO_TIENtextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            Commons.OSystems.SetDateEditFormat(NGAY_HIEU_LUCdateEdit);

        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            if (btn == null || btn.Tag == null) return;
            switch (btn.Tag.ToString())
            {
                case "chonall":
                    {
                        Commons.Modules.ObjSystems.MChooseGrid(true, "CHON", grvCapNhatLCB);
                        break;
                    }
                case "bochon":
                    {
                        Commons.Modules.ObjSystems.MChooseGrid(false, "CHON", grvCapNhatLCB);
                        break;
                    }

                case "thuchien":
                    {
                        if (Commons.Modules.ObjSystems.ConvertDatatable(grvCapNhatLCB).AsEnumerable().Count(x => x.Field<bool>("CHON").Equals(true)) == 0)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgBanChuaChonDuLieu); return;
                        }
                        dxValidationProvider1.Dispose();
                        DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
                        conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
                        conditionValidationRule1.ErrorText = "This value is not valid";
                        conditionValidationRule1.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                        if (chkNgayHieuLuc.Checked == true)
                        {
                            dxValidationProvider1.SetValidationRule(this.NGAY_HIEU_LUCdateEdit, conditionValidationRule1);
                        }
                        if (chkNLuong.Checked == true)
                        {
                            dxValidationProvider1.SetValidationRule(this.ID_NLlookUpEdit, conditionValidationRule1);
                        }
                        if (chkBLuong.Checked == true)
                        {
                            dxValidationProvider1.SetValidationRule(this.ID_BLlookUpEdit, conditionValidationRule1);
                        }
                        if (chkLuongCoBan.Checked == true)
                        {
                            dxValidationProvider1.SetValidationRule(this.LUONG_CO_BANtextEdit, conditionValidationRule1);
                        }
                        if (chkCOT_CAP_NHAT.Checked == true)
                        {
                            dxValidationProvider1.SetValidationRule(this.SO_TIENtextEdit, conditionValidationRule1);
                        }
                        if (!dxValidationProvider1.Validate()) return;
                        if (CapNhapTheoDieuKien() == false)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgBanChuaChonDuLieu); return;

                        }
                        enableButon(false);
                        break;
                    }
                case "luu":
                    {
                        if (Savedata() == false)
                        {
                            //thất bại
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgCapNhatThatBai); return;
                        }
                        Uncheck();
                        enableButon(true);
                        LoadGrdCapNhatLCB();
                        break;
                    }
                case "khongluu":
                    {
                        enableButon(true);
                        Uncheck();
                        LoadGrdCapNhatLCB();
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                default:
                    break;
            }
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdCapNhatLCB();
            Commons.Modules.sLoad = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdCapNhatLCB();
            Commons.Modules.sLoad = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdCapNhatLCB();
            Commons.Modules.sLoad = "";
        }
        private void BAC_CVlookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.MLoadLookUpEdit(BAC_HDlookUpEdit, Commons.Modules.ObjSystems.DataBacLuong(Convert.ToInt64(BAC_CVlookUpEdit.EditValue), Convert.ToDateTime(NGAY_HIEU_LUCdateEdit.EditValue), true), "ID_BL", "TEN_BL", "TEN_BL");
            LoadGrdCapNhatLCB();
            Commons.Modules.sLoad = "";
        }
        private void BAC_HDlookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdCapNhatLCB();
            Commons.Modules.sLoad = "";
        }
        private void radBoloc_EditValueChanged(object sender, EventArgs e)
        {
            if (radBoloc.SelectedIndex == 0)
            {
                LuongTutextEdit.Enabled = false;
                LuongDentextEdit.Enabled = false;
            }
            else
            {
                LuongTutextEdit.Enabled = true;
                LuongDentextEdit.Enabled = true;
            }
        }
        #endregion

        #region funciton load data
        private void LoadGrdCapNhatLCB()
        {

            DataTable dt = new DataTable();
            try
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCapNhatLCB", Convert.ToInt64(cboDV.EditValue), Convert.ToInt64(cboXN.EditValue), Convert.ToInt64(cboTo.EditValue), Convert.ToInt64(BAC_CVlookUpEdit.EditValue), string.IsNullOrEmpty(BAC_HDlookUpEdit.Text.ToString()) ? -1 : Convert.ToInt64(BAC_HDlookUpEdit.EditValue), radBoloc.SelectedIndex, LuongTutextEdit.EditValue, LuongDentextEdit.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dt.Columns["CHON"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdCapNhatLCB, grvCapNhatLCB, dt, false, false, false, true, true, this.Name);
                grvCapNhatLCB.Columns["CHON"].Visible = false;
                grvCapNhatLCB.Columns["ID_CN"].Visible = false;
                grvCapNhatLCB.Columns["ID_LCB"].Visible = false;
                grvCapNhatLCB.Columns["ID_NL"].Visible = false;
                grvCapNhatLCB.Columns["ID_BL"].Visible = false;
                grvCapNhatLCB.OptionsSelection.CheckBoxSelectorField = "CHON";

                grvCapNhatLCB.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvCapNhatLCB.Columns["MS_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvCapNhatLCB.Columns["SO_QUYET_DINH"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                //grvCapNhatLCB.Columns["ID_LDV"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;


                grvCapNhatLCB.Columns["LUONG_CO_BAN"].DisplayFormat.FormatType = FormatType.Numeric;
                grvCapNhatLCB.Columns["LUONG_CO_BAN"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvCapNhatLCB.Columns["PC_DOC_HAI"].DisplayFormat.FormatType = FormatType.Numeric;
                grvCapNhatLCB.Columns["PC_DOC_HAI"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvCapNhatLCB.Columns["PC_SINH_HOAT"].DisplayFormat.FormatType = FormatType.Numeric;
                grvCapNhatLCB.Columns["PC_SINH_HOAT"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvCapNhatLCB.Columns["THUONG_CHUYEN_CAN"].DisplayFormat.FormatType = FormatType.Numeric;
                grvCapNhatLCB.Columns["THUONG_CHUYEN_CAN"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvCapNhatLCB.Columns["PC_KY_NANG"].DisplayFormat.FormatType = FormatType.Numeric;
                grvCapNhatLCB.Columns["PC_KY_NANG"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvCapNhatLCB.Columns["THUONG_HT_CV"].DisplayFormat.FormatType = FormatType.Numeric;
                grvCapNhatLCB.Columns["THUONG_HT_CV"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;


                //Commons.Modules.ObjSystems.
            }
            catch (Exception ex)
            {

            }
        }
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = false;
            windowsUIButton.Buttons[1].Properties.Visible = false;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
            windowsUIButton.Buttons[6].Properties.Visible = !visible;
            groChonDuLieu.Enabled = visible;
            searchControl.Visible = visible;
        }

        private void Uncheck()
        {
            chkBLuong.Checked = false;
            chkCOT_CAP_NHAT.Checked = false;
            chkLuongCoBan.Checked = false;
            chkNgayHieuLuc.Checked = false;
            chkNLuong.EditValue = false;

            NGAY_HIEU_LUCdateEdit.EditValue = DateTime.Now;
            LUONG_CO_BANtextEdit.ResetText();
            ID_NLlookUpEdit.ResetText();
            ID_BLlookUpEdit.ResetText();
            SO_TIENtextEdit.ResetText();
            NOI_DUNGtextEdit.ResetText();
        }
        private bool CapNhapTheoDieuKien()
        {
            DataTable dtDLCN = new DataTable();
            DataColumn dtC;
            DataRow dtR;

            dtC = new DataColumn();
            dtC.DataType = typeof(DateTime);
            dtC.ColumnName = "NGAY_HIEU_LUC";
            dtC.Caption = "NGAY_HIEU_LUC";
            dtC.ReadOnly = false;
            dtC.Unique = true;
            dtDLCN.Columns.Add(dtC);

            dtC = new DataColumn();
            dtC.DataType = typeof(long);
            dtC.ColumnName = "ID_NL";
            dtC.Caption = "ID_NL";
            dtC.ReadOnly = false;
            dtC.Unique = true;
            dtDLCN.Columns.Add(dtC);

            dtC = new DataColumn();
            dtC.DataType = typeof(string);
            dtC.ColumnName = "TEN_NL";
            dtC.Caption = "TEN_NL";
            dtC.ReadOnly = false;
            dtC.Unique = true;
            dtDLCN.Columns.Add(dtC);

            dtC = new DataColumn();
            dtC.DataType = typeof(long);
            dtC.ColumnName = "ID_BL";
            dtC.Caption = "ID_BL";
            dtC.ReadOnly = false;
            dtC.Unique = true;
            dtDLCN.Columns.Add(dtC);

            dtC = new DataColumn();
            dtC.DataType = typeof(string);
            dtC.ColumnName = "TEN_BL";
            dtC.Caption = "TEN_BL";
            dtC.ReadOnly = false;
            dtC.Unique = true;
            dtDLCN.Columns.Add(dtC);

            dtC = new DataColumn();
            dtC.DataType = typeof(float);
            dtC.ColumnName = "LUONG_CO_BAN";
            dtC.Caption = "LUONG_CO_BAN";
            dtC.ReadOnly = false;
            dtC.Unique = true;
            dtDLCN.Columns.Add(dtC);

            dtC = new DataColumn();
            dtC.DataType = typeof(float);
            dtC.ColumnName = COT_CAP_NHATlookUpEdit.EditValue.ToString();
            dtC.Caption = COT_CAP_NHATlookUpEdit.EditValue.ToString();
            dtC.ReadOnly = false;
            dtC.Unique = true;
            dtDLCN.Columns.Add(dtC);

            dtR = dtDLCN.NewRow();

            if (chkNgayHieuLuc.Checked == true)
            {
                dtR["NGAY_HIEU_LUC"] = NGAY_HIEU_LUCdateEdit.EditValue.ToString();
            }
            if (chkNLuong.Checked == true)
            {
                dtR["ID_NL"] = ID_NLlookUpEdit.EditValue.ToString();
                dtR["TEN_NL"] = ID_NLlookUpEdit.Text.ToString();

            }
            if (chkBLuong.Checked == true)
            {
                dtR["ID_BL"] = ID_BLlookUpEdit.EditValue.ToString();
                dtR["TEN_BL"] = ID_BLlookUpEdit.Text.ToString();

            }
            if (chkLuongCoBan.Checked == true)
            {
                dtR["LUONG_CO_BAN"] = LUONG_CO_BANtextEdit.EditValue.ToString();
            }
            if (chkCOT_CAP_NHAT.Checked == true)
            {
                dtR["" + COT_CAP_NHATlookUpEdit.EditValue.ToString() + ""] = SO_TIENtextEdit.EditValue.ToString();
            }
            dtDLCN.Rows.Add(dtR);
            if (dtDLCN.Rows.Count == 0)
            {
                return false;
            }
            else
            {

                DataTable dtCN = new DataTable();
                dtCN = (DataTable)grdCapNhatLCB.DataSource;

                string sBT = "sBTDLCN" + Commons.Modules.UserName;
                string sBT1 = "sBTCongNhan" + Commons.Modules.UserName;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, dtDLCN, "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT1, dtCN, "");

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spCapNhatLCB", conn);
                cmd.Parameters.Add("@sBTDLCN", SqlDbType.NVarChar).Value = sBT;
                cmd.Parameters.Add("@sBTCN", SqlDbType.NVarChar).Value = sBT1;
                cmd.Parameters.Add("@NAME_COL", SqlDbType.NVarChar).Value = COT_CAP_NHATlookUpEdit.EditValue.ToString();
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                //Commons.Modules.ObjSystems.XoaTable(sBT);
                grdCapNhatLCB.DataSource = dt;
                //grdData.DataSource = ds.Tables[0].Copy();
                //DataTable dt_CHON = new DataTable();
                //dt_CHON = dt.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).CopyToDataTable().Copy();

                //for (int i = 0; i < grvCapNhatLCB.RowCount; i++)
                //{
                //    if (Convert.ToBoolean(grvCapNhatLCB.GetRowCellValue(i, "CHON")) == false) continue;
                //    for (int j = 0; j < list.Count; j++)
                //    {

                //        grvCapNhatLCB.SetRowCellValue(i, list[j].col, list[j].val);
                //        grvCapNhatLCB.UpdateCurrentRow();
                //    }
                //}


                return true;
            }
        }
        private bool Savedata()
        {
            try
            {
                DataTable dt = Commons.Modules.ObjSystems.ConvertDatatable(grvCapNhatLCB);
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbt, dt, "");
                string sSql = "UPDATE a SET a.NGAY_HIEU_LUC = b.NGAY_HIEU_LUC,a.ID_NL = b.ID_NL,a.ID_BL = b.ID_BL,a.LUONG_CO_BAN = b.LUONG_CO_BAN,a.THUONG_CHUYEN_CAN = b.THUONG_CHUYEN_CAN,a.PC_DOC_HAI = b.PC_DOC_HAI,a.THUONG_HT_CV = b.THUONG_HT_CV,a.PC_KY_NANG = b.PC_KY_NANG,a.PC_SINH_HOAT = b.PC_SINH_HOAT,a.GHI_CHU = N'" + NOI_DUNGtextEdit.EditValue + "' FROM dbo.LUONG_CO_BAN a INNER JOIN " + sbt + " b ON b.ID_LCB = a.ID_LCB AND b.CHON = 1";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                Commons.Modules.ObjSystems.XoaTable(sbt);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        #endregion

        private void ID_NLlookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_BLlookUpEdit, Commons.Modules.ObjSystems.DataBacLuong(Convert.ToInt64(ID_NLlookUpEdit.EditValue), Convert.ToDateTime(NGAY_HIEU_LUCdateEdit.EditValue), false), "ID_BL", "TEN_BL", "TEN_BL");
            LoadGrdCapNhatLCB();
            Commons.Modules.sLoad = "";
        }

        private void LuongTutextEdit_Validated(object sender, EventArgs e)
        {
            if (LuongTutextEdit.EditValue == null || LuongTutextEdit.EditValue.ToString() == "")
            {
                LuongTutextEdit.Focus();
                return;
            }
            if (LuongDentextEdit.EditValue == null || LuongDentextEdit.EditValue.ToString() == "")
            {
                LuongDentextEdit.Focus();
                return;
            }
            cboTo_EditValueChanged(null, null);
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            System.Reflection.PropertyInfo[] Props = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (System.Reflection.PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}
