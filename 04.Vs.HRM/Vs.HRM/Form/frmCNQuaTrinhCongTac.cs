using DevExpress.CodeParser;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Vs.HRM
{
    
    public partial class frmCNQuaTrinhCongTac : DevExpress.XtraEditors.XtraForm
    {
        public DataTable dtTmp;

        public frmCNQuaTrinhCongTac()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, btnALL);
        }

        #region even
        private void frmCNQuaTrinhCongTac_Load(object sender, EventArgs e)
        {
            NGAY_KYDateEdit.DateTime = DateTime.Now;
            NGAY_HIEU_LUCDateEdit.DateTime = DateTime.Now;
            Commons.OSystems.SetDateEditFormat(NGAY_KYDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_HIEU_LUCDateEdit);
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_LQDLookUpEdit, Commons.Modules.ObjSystems.DataLoaiQuyetDinh(false), "ID_LQD", "TEN_LQD", "TEN_LQD", true);
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_NKLookUpEdit, Commons.Modules.ObjSystems.DataNguoiKy(), "ID_NK", "HO_TEN", "HO_TEN",true);
            LoadData();
            foreach (ToolStripMenuItem item in contextMenuStrip1.Items)
            {
                item.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, item.Name);
            }
            grdBandCT.Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, grdBandCT.Name);
            grdBandCD.Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, grdBandCD.Name);
        }
        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "ghi":
                        {
                            if (!dxValidationProvider1.Validate()) return;
                            DataTable dt = Commons.Modules.ObjSystems.ConvertDatatable(grdChonNV);
                            if (dt.AsEnumerable().Count(x => Convert.ToBoolean(x["CHON"]) == true) == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonNV"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            //lưu dữ liệu chọn lại và cập nhật vào bảng tạm
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTNV" + Commons.Modules.iIDUser, dt, "");
                            if(Convert.ToBoolean(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spGetListNhanVienCNQTCT", NGAY_HIEU_LUCDateEdit.DateTime,NGAY_KYDateEdit.DateTime,ID_NKLookUpEdit.EditValue,ID_LQDLookUpEdit.EditValue,chkDaKy.Checked == true ? 2 : 1, "SAVE", Commons.Modules.UserName, Commons.Modules.TypeLanguage, "sBTNV" + Commons.Modules.iIDUser)) == true )
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_CapNhatThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }    
                            else
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgCapNhatThatBai); return;
                            }
                            this.Close();
                            break;
                        }
                    case "khongghi":
                        {
                            this.Close();
                            break;
                        }
                }
            }
            catch
            {
            }
        }
        #endregion

        #region function
        private void LoadData()
        {
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTNV" + Commons.Modules.iIDUser, dtTmp, "");
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListNhanVienCNQTCT", NGAY_HIEU_LUCDateEdit.DateTime, NGAY_KYDateEdit.DateTime, ID_NKLookUpEdit.EditValue, ID_LQDLookUpEdit.EditValue, chkDaKy.Checked == true ? 2 : 1, "LOAD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, "sBTNV" + Commons.Modules.iIDUser));
                dt.Columns["CHON"].ReadOnly = false;
                dt.Columns["DV"].ReadOnly = false;
                dt.Columns["XN"].ReadOnly = false;
                dt.Columns["TO"].ReadOnly = false;
                dt.Columns["LCV"].ReadOnly = false;
                dt.Columns["CV"].ReadOnly = false;
                dt.Columns["CTL"].ReadOnly = false;
                dt.Columns["GHI_CHU"].ReadOnly = false;

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdChonNV, grvChonNV, dt, true, true, false,false, true, this.Name);

                grdBandCT.Columns.Add(grvChonNV.Columns["DV_CU"]);
                grdBandCT.Columns.Add(grvChonNV.Columns["XN_CU"]);
                grdBandCT.Columns.Add(grvChonNV.Columns["TO_CU"]);
                grdBandCT.Columns.Add(grvChonNV.Columns["LCV_CU"]);
                grdBandCT.Columns.Add(grvChonNV.Columns["CV_CU"]);
                grdBandCT.Columns.Add(grvChonNV.Columns["CTL_CU"]);


                grdBandCD.Columns.Add(grvChonNV.Columns["DV"]);
                grdBandCD.Columns.Add(grvChonNV.Columns["XN"]);
                grdBandCD.Columns.Add(grvChonNV.Columns["TO"]);
                grdBandCD.Columns.Add(grvChonNV.Columns["LCV"]);
                grdBandCD.Columns.Add(grvChonNV.Columns["CV"]);
                grdBandCD.Columns.Add(grvChonNV.Columns["CTL"]);
                grdBandCD.Columns.Add(grvChonNV.Columns["GHI_CHU"]);

                gridBand1.Columns.Add(grvChonNV.Columns["CHON"]);
                gridBand1.Columns.Add(grvChonNV.Columns["ID_CN"]);
                gridBand1.Columns.Add(grvChonNV.Columns["MS_CN"]);
                gridBand1.Columns.Add(grvChonNV.Columns["HO_TEN"]);

                grvChonNV.Columns["CHON"].Visible = false;
                grvChonNV.Columns["ID_CN"].Visible = false;

                grvChonNV.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                grvChonNV.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                grvChonNV.Columns["DV_CU"].OptionsColumn.AllowEdit = false;
                grvChonNV.Columns["XN_CU"].OptionsColumn.AllowEdit = false;
                grvChonNV.Columns["TO_CU"].OptionsColumn.AllowEdit = false;
                grvChonNV.Columns["LCV_CU"].OptionsColumn.AllowEdit = false;
                grvChonNV.Columns["CV_CU"].OptionsColumn.AllowEdit = false;
                grvChonNV.Columns["CTL_CU"].OptionsColumn.AllowEdit = false;
                grvChonNV.Columns["CV"].OptionsColumn.AllowEdit = false;


                grvChonNV.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
                grvChonNV.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                grvChonNV.OptionsSelection.CheckBoxSelectorField = "CHON";

                Commons.Modules.ObjSystems.AddCombXtra("ID_DV", "TEN_DV", "DV_CU",grvChonNV, Commons.Modules.ObjSystems.DataDonVi(false) ,true ,"ID_DV",this.Name,true);
                Commons.Modules.ObjSystems.AddCombXtra("ID_XN", "TEN_XN", "XN_CU", grvChonNV, Commons.Modules.ObjSystems.DataXiNghiep(-1,false), true, "ID_XN", this.Name, true);
                Commons.Modules.ObjSystems.AddCombXtra("ID_TO", "TEN_TO", "TO_CU", grvChonNV, Commons.Modules.ObjSystems.DataTo(-1,-1, false), true, "ID_TO", this.Name, true);
                Commons.Modules.ObjSystems.AddCombXtra("ID_LCV", "TEN_LCV", "LCV_CU", grvChonNV, Commons.Modules.ObjSystems.DataLoaiCV(false), true, "ID_LCV", this.Name, true);
                Commons.Modules.ObjSystems.AddCombXtra("ID_CV", "TEN_CV", "CV_CU", grvChonNV, Commons.Modules.ObjSystems.DataChucVu(false,-1), true, "ID_CV", this.Name, true);
                Commons.Modules.ObjSystems.AddCombXtra("ID_CTL", "TEN_CTL", "CTL_CU", grvChonNV, Commons.Modules.ObjSystems.DataCTL(false), true, "ID_CTL", this.Name, true);

                Commons.Modules.ObjSystems.AddCombXtra("ID_DV", "TEN_DV", "DV", grvChonNV, Commons.Modules.ObjSystems.DataDonVi(false), true, "ID_DV", this.Name, true);
                //Commons.Modules.ObjSystems.AddCombXtra("ID_XN", "TEN_XN", "XN", grvChonNV, Commons.Modules.ObjSystems.DataXiNghiep(-1, false), true, "ID_XN", this.Name, true);
                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboXN = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                cboXN.NullText = "";
                cboXN.ValueMember = "ID_XN";
                cboXN.DisplayMember = "TEN_XN";
                //ID_NGUOI_DGTN,TEN_NGUOI_DGTN
                cboXN.DataSource = Commons.Modules.ObjSystems.DataXiNghiep(-1, false);
                cboXN.Columns.Clear();
                cboXN.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_XN"));
                cboXN.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_XN"));
                cboXN.Columns["TEN_XN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_XN");
                cboXN.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboXN.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cboXN.Columns["ID_XN"].Visible = false;
                grvChonNV.Columns["XN"].ColumnEdit = cboXN;
                cboXN.BeforePopup += CboXN_BeforePopup;

                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboTO = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                cboTO.NullText = "";
                cboTO.ValueMember = "ID_TO";
                cboTO.DisplayMember = "TEN_TO";
                //ID_NGUOI_DGTN,TEN_NGUOI_DGTN
                cboTO.DataSource = Commons.Modules.ObjSystems.DataTo(-1, -1, false);
                cboTO.Columns.Clear();
                cboTO.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_TO"));
                cboTO.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_TO"));
                cboTO.Columns["TEN_TO"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_TO");
                cboTO.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboTO.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cboTO.Columns["ID_TO"].Visible = false;
                grvChonNV.Columns["TO"].ColumnEdit = cboTO;
                cboTO.BeforePopup += CboTO_BeforePopup;
                //Commons.Modules.ObjSystems.AddCombXtra("ID_TO", "TEN_TO", "TO", grvChonNV, Commons.Modules.ObjSystems.DataTo(-1, -1, false), true, "ID_TO", this.Name, true);
                Commons.Modules.ObjSystems.AddCombXtra("ID_LCV", "TEN_LCV", "LCV", grvChonNV, Commons.Modules.ObjSystems.DataLoaiCV(false), true, "ID_LCV", this.Name, true);
                Commons.Modules.ObjSystems.AddCombXtra("ID_CV", "TEN_CV", "CV", grvChonNV, Commons.Modules.ObjSystems.DataChucVu(false, -1), true, "ID_CV", this.Name, true);
                Commons.Modules.ObjSystems.AddCombXtra("ID_CTL", "TEN_CTL", "CTL", grvChonNV, Commons.Modules.ObjSystems.DataCTL(false), true, "ID_CTL", this.Name, true);

            }
            catch { }
        }

        private void CboTO_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(grvChonNV.GetFocusedRowCellValue("DV")), Convert.ToInt32(grvChonNV.GetFocusedRowCellValue("XN")), false);
            }
            catch { }
        }

        private void CboXN_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataXiNghiep(Convert.ToInt32(grvChonNV.GetFocusedRowCellValue("DV")),false);
            }
            catch { }
        }
        #endregion


        public void BackWindowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            LoadData();
        }



        private void grvChonNV_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (checkNhapDu(e.HitInfo.RowHandle))
                {
                    contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                }
                else
                {
                    contextMenuStrip1.Hide();
                }
            }
            catch
            {
            }
        }

        private bool checkNhapDu(int row)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView View = grvChonNV;
                if (Commons.Modules.ObjSystems.IsnullorEmpty(View.GetRowCellValue(row, "DV")) || View.GetRowCellValue(row, "DV").ToString() == "-99")
                {
                    View.SetColumnError(View.Columns["DV"], Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrongDuLieu")); return false;
                }
                else
                {
                    View.SetColumnError(View.Columns["DV"],"");
                }
                if (Commons.Modules.ObjSystems.IsnullorEmpty(View.GetRowCellValue(row, "XN")) || View.GetRowCellValue(row, "XN").ToString() == "-99")
                {
                    View.SetColumnError(View.Columns["XN"], Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrongDuLieu")); return false;
                }
                else
                {
                    View.SetColumnError(View.Columns["XN"], "");
                }
                if (Commons.Modules.ObjSystems.IsnullorEmpty(View.GetRowCellValue(row, "TO")) || View.GetRowCellValue(row, "TO").ToString() == "-99")
                {
                    View.SetColumnError(View.Columns["TO"], Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrongDuLieu")); return false;
                }
                else
                {
                    View.SetColumnError(View.Columns["TO"], "");
                }
                if (Commons.Modules.ObjSystems.IsnullorEmpty(View.GetRowCellValue(row, "LCV")) || View.GetRowCellValue(row, "LCV").ToString() == "-99")
                {
                    View.SetColumnError(View.Columns["LCV"], Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrongDuLieu")); return false;
                }
                else
                {
                    View.SetColumnError(View.Columns["LCV"], "");
                }    
                return true;
            }
            catch
            {
                return false;

            }

            
        }

        private void grvChonNV_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "LCV")
            {
                //view.SetFocusedRowCellValue("PHUT_VE", Convert.ToInt32(view.GetFocusedRowCellValue("PHUT_DEN").ToString()));
                grvChonNV.SetRowCellValue(e.RowHandle, grvChonNV.Columns["CV"], Convert.ToInt64(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr,CommandType.Text, "SELECT ID_CV FROM dbo.LOAI_CONG_VIEC WHERE ID_LCV = "+ e.Value +"")));
            }
        }

        private void grvChonNV_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            if (Commons.Modules.ObjSystems.IsnullorEmpty(View.GetRowCellValue(e.RowHandle, "DV")) || View.GetRowCellValue(e.RowHandle, "DV").ToString() == "-99")
            {
                e.Valid = false;
                View.SetColumnError(View.Columns["DV"], Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrongDuLieu")); return;
            }
            if (Commons.Modules.ObjSystems.IsnullorEmpty(View.GetRowCellValue(e.RowHandle, "XN")) || View.GetRowCellValue(e.RowHandle, "XN").ToString() == "-99")
            {
                e.Valid = false;
                View.SetColumnError(View.Columns["XN"], Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrongDuLieu")); return;
            }
            if (Commons.Modules.ObjSystems.IsnullorEmpty(View.GetRowCellValue(e.RowHandle, "TO")) || View.GetRowCellValue(e.RowHandle, "TO").ToString() == "-99")
            {
                e.Valid = false;
                View.SetColumnError(View.Columns["TO"], Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrongDuLieu")); return;
            }
            if (Commons.Modules.ObjSystems.IsnullorEmpty(View.GetRowCellValue(e.RowHandle, "LCV")) || View.GetRowCellValue(e.RowHandle, "LCV").ToString() == "-99")
            {
                e.Valid = false;
                View.SetColumnError(View.Columns["LCV"], Commons.Modules.ObjLanguages.GetLanguage(this.Name, "erTrongDuLieu")); return;
            }
        }

        private void grvChonNV_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvChonNV_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void mnumnuCapnhattatcacontextMenuStrip1(object sender, EventArgs e)
        {
            DataTable table = Commons.Modules.ObjSystems.ConvertDatatable(grdChonNV);
            table.AsEnumerable().Where(x => (Commons.Modules.ObjSystems.IsnullorEmpty(x["TO"]) == true) || (x["TO"].ToString() == "-99")).ToList().ForEach(row => row["DV"] = grvChonNV.GetFocusedRowCellValue("DV"));
            table.AsEnumerable().Where(x => (Commons.Modules.ObjSystems.IsnullorEmpty(x["TO"]) == true) || (x["TO"].ToString() == "-99")).ToList().ForEach(row => row["XN"] = grvChonNV.GetFocusedRowCellValue("XN"));
            table.AsEnumerable().Where(x => (Commons.Modules.ObjSystems.IsnullorEmpty(x["TO"]) == true) || (x["TO"].ToString() == "-99")).ToList().ForEach(row => row["TO"] = grvChonNV.GetFocusedRowCellValue("TO"));
            table.AsEnumerable().Where(x => (Commons.Modules.ObjSystems.IsnullorEmpty(x["LCV"]) == true) || (x["LCV"].ToString() == "-99")).ToList().ForEach(row => row["LCV"] = grvChonNV.GetFocusedRowCellValue("LCV"));
            table.AsEnumerable().Where(x => (Commons.Modules.ObjSystems.IsnullorEmpty(x["CV"]) == true) || (x["CV"].ToString() == "-99")).ToList().ForEach(row => row["CV"] = grvChonNV.GetFocusedRowCellValue("CV"));
            table.AsEnumerable().Where(x => (Commons.Modules.ObjSystems.IsnullorEmpty(x["CTL"]) == true) || (x["CTL"].ToString() == "-99")).ToList().ForEach(row => row["CTL"] = grvChonNV.GetFocusedRowCellValue("CTL"));
            table.AsEnumerable().Where(x => (Commons.Modules.ObjSystems.IsnullorEmpty(x["GHI_CHU"]) == true)).ToList().ForEach(row => row["GHI_CHU"] = grvChonNV.GetFocusedRowCellValue("GHI_CHU"));
            table.AcceptChanges();
        }
    }
}
