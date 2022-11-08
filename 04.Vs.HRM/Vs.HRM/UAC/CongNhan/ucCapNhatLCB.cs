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
using DevExpress.XtraGrid.Views.Grid;

namespace Vs.HRM
{
    public partial class ucCapNhatLCB : DevExpress.XtraEditors.XtraUserControl
    {
        public static ucCapNhatLCB _instance;
        bool bSave = false;
        public static ucCapNhatLCB Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucCapNhatLCB();
                return _instance;
            }
        }

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
            LoadGrdCapNhatLCB();
            radBoloc_EditValueChanged(null, null);
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            Commons.Modules.sLoad = "";
        }

        private void formatText()
        {
            Commons.OSystems.SetDateEditFormat(NGAY_HIEU_LUCdateEdit);

        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            if (btn == null || btn.Tag == null) return;
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        grvCapNhatLCB.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
                        grvCapNhatLCB.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                        grvCapNhatLCB.OptionsSelection.CheckBoxSelectorField = "CHON";
                        enableButon(false);
                        bSave = false;
                        break;
                    }
                case "thuchien":
                    {
                        try
                        {
                            if (Commons.Modules.ObjSystems.ConvertDatatable(grvCapNhatLCB).AsEnumerable().Count(x => x.Field<bool>("CHON").Equals(true)) == 0)
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgBanChuaChonDuLieu); return;
                            }
                        }
                        catch
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgBanChuaChonDuLieu); return;
                        }
                        //dxValidationProvider1.Dispose();
                        //DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
                        //conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
                        //conditionValidationRule1.ErrorText = "This value is not valid";
                        //conditionValidationRule1.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;

                        //dxValidationProvider1.SetValidationRule(this.NGAY_HIEU_LUCdateEdit, conditionValidationRule1);

                        if (!dxValidationProvider1.Validate()) return;
                        if (CapNhapTheoDieuKien() == false)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgBanChuaChonDuLieu); return;
                        }
                        enableButon(false);
                        bSave = true;
                        break;
                    }
                case "luu":
                        {
                        if (!dxValidationProvider1.Validate()) return;
                        Commons.Modules.ObjSystems.ClearValidationProvider(dxValidationProvider1);
                        if (Convert.ToInt32(Commons.Modules.ObjSystems.ConvertDatatable(grvCapNhatLCB).AsEnumerable().Count(x => x["CHON"].ToString().Trim().ToUpper() == "TRUE")) == 0)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgBanChuaChonDuLieu); return;
                        }
                        if (!bSave) {
                            Commons.Modules.ObjSystems.msgChung("msgCanBamThucHienDeLuu"); return;
                        }
                        if (Savedata() == false)
                        {
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
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCapNhatLCB", Convert.ToInt64(cboDV.EditValue), Convert.ToInt64(cboXN.EditValue), Convert.ToInt64(cboTo.EditValue), radBoloc.SelectedIndex, LuongTutextEdit.EditValue, LuongDentextEdit.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dt.Columns["CHON"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdCapNhatLCB, grvCapNhatLCB, dt, false, false, false, true, true, this.Name);
                grvCapNhatLCB.Columns["CHON"].Visible = false;
                grvCapNhatLCB.Columns["ID_CN"].Visible = false;
                grvCapNhatLCB.Columns["ID_LCB"].Visible = false;
                grvCapNhatLCB.Columns["ID_NL"].Visible = false;
                grvCapNhatLCB.Columns["ID_BL"].Visible = false;

                grvCapNhatLCB.Columns["CHON"].Visible = false;
                grvCapNhatLCB.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
                grvCapNhatLCB.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;

                grvCapNhatLCB.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvCapNhatLCB.Columns["MS_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvCapNhatLCB.Columns["SO_QUYET_DINH"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

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
                ItemForSumNhanVien.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, ItemForSumNhanVien.Name) + ": " + grvCapNhatLCB.RowCount.ToString();
            }
            catch
            {

            }
        }
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = !visible;
            windowsUIButton.Buttons[3].Properties.Visible = !visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
            groChonDuLieu.Enabled = visible;
            searchControl.Visible = visible;
        }

        private void Uncheck()
        {

            NGAY_HIEU_LUCdateEdit.EditValue = DateTime.Now;
            LUONG_CO_BANtextEdit.EditValue = 0;
            NOI_DUNGtextEdit.EditValue = "";
        }
        private bool CapNhapTheoDieuKien()
        {
            DataTable dtCN = new DataTable();
            dtCN = (DataTable)grdCapNhatLCB.DataSource;
            int SQD = 0;
            try
            {
                SQD = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT MAX(SO_QUYET_DINH) + 1 FROM dbo.LUONG_CO_BAN"));
                dtCN.AsEnumerable().Where(x => x["CHON"].ToString().ToUpper().Trim() == "TRUE").ToList<DataRow>().ForEach(r => { r["NGAY_HIEU_LUC"] = NGAY_HIEU_LUCdateEdit.DateTime; r["LUONG_CO_BAN"] = LUONG_CO_BANtextEdit.EditValue; r["GHI_CHU"] = NOI_DUNGtextEdit.EditValue; r["SO_QUYET_DINH"] = SQD; });
                dtCN.AcceptChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        private bool Savedata()
        {
            try
            {
                DataTable dt = Commons.Modules.ObjSystems.ConvertDatatable(grvCapNhatLCB);
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sbtLuongCB" + Commons.Modules.iIDUser, dt, "");
                string sSql = "INSERT INTO dbo.LUONG_CO_BAN(ID_CN,ID_TO,ID_CV,ID_NK,NGAY_KY,SO_QUYET_DINH,NGAY_HIEU_LUC,GHI_CHU,LUONG_CO_BAN,MUC_LUONG_THUC,HS_LUONG,THUONG_CHUYEN_CAN,PC_DOC_HAI,THUONG_HT_CV,PC_KY_NANG,PC_SINH_HOAT,ID_TT) SELECT A.ID_CN,B.ID_TO,B.ID_CV,1,A.NGAY_HIEU_LUC,A.SO_QUYET_DINH,A.NGAY_HIEU_LUC,A.GHI_CHU,A.LUONG_CO_BAN,A.LUONG_CO_BAN,A.LUONG_CO_BAN,A.THUONG_CHUYEN_CAN,A.PC_DOC_HAI,A.THUONG_HT_CV,A.PC_KY_NANG,A.PC_SINH_HOAT,1 FROM dbo.sbtLuongCB35 A INNER JOIN dbo.CONG_NHAN B ON B.ID_CN = A.ID_CN WHERE A.CHON = 1 AND NOT EXISTS (SELECT * FROM dbo.LUONG_CO_BAN C WHERE A.ID_CN = C.ID_CN AND CONVERT(DATE,A.NGAY_HIEU_LUC) = CONVERT(DATE,C.NGAY_HIEU_LUC) AND A.SO_QUYET_DINH = C.SO_QUYET_DINH)";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                Commons.Modules.ObjSystems.XoaTable("sbtLuongCB" + Commons.Modules.iIDUser);
                return true;
            }
            catch
            {
                return false;
            }

        }

        #endregion

        private void ID_NLlookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            //LoadGrdCapNhatLCB();
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

        private void grvCapNhatLCB_RowCountChanged(object sender, EventArgs e)
        {
            ItemForSumNhanVien.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, ItemForSumNhanVien.Name) + ": " + grvCapNhatLCB.RowCount.ToString();

        }

        private void grvCapNhatLCB_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            bSave = false;
        }
    }
}
