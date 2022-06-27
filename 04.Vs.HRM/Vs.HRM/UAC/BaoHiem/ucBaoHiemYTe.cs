using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraLayout;
using System.Threading;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace Vs.HRM
{
    public partial class ucBaoHiemYTe : DevExpress.XtraEditors.XtraUserControl
    {
        public static ucBaoHiemYTe _instance;
        DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_BV;
        int MS_TINH;
        public static ucBaoHiemYTe Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucBaoHiemYTe();
                return _instance;
            }
        }


        public ucBaoHiemYTe()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, windowsUIButton);
        }
        #region bảo hiểm y tế
        private void ucBaoHiemYTe_Load(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridBaoHiemYTe();
            Commons.Modules.sPS = "";
            enableButon(true);
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridBaoHiemYTe();
            Commons.Modules.sPS = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridBaoHiemYTe();
            Commons.Modules.sPS = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGridBaoHiemYTe();
            Commons.Modules.sPS = "";
        }
        private void grdBHYT_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                XoaBaoHiemYTe();
            }
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        enableButon(false);
                        Commons.Modules.ObjSystems.AddnewRow(grvNgungDongBHXH, false);
                        break;
                    }

                case "xoa":
                    {
                        XoaBaoHiemYTe();
                        break;
                    }
                case "luu":
                    {
                        Savedata();
                        LoadGridBaoHiemYTe();
                        enableButon(true);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvNgungDongBHXH);
                        break;
                    }
                case "khongluu":
                    {
                        Commons.Modules.ObjSystems.DeleteAddRow(grvNgungDongBHXH);
                        enableButon(true);
                        LoadGridBaoHiemYTe();
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                case "ngayhethan":
                    {
                        try
                        {
                            XtraInputBoxArgs args = new XtraInputBoxArgs();
                            // set required Input Box options
                            args.Caption = "Cập nhật ngày hết hạn";
                            args.Prompt = "Chọn ngày cập nhật";
                            args.DefaultButtonIndex = 0;

                            // initialize a DateEdit editor with custom settings
                            DateEdit editor = new DateEdit();
                            editor.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Default;
                            args.Editor = editor;
                            // a default DateEdit value
                            args.DefaultResponse = DateTime.Now.Date;
                            // display an Input Box with the custom editor
                            var result = XtraInputBox.Show(args);
                            if (result.ToString() != "")
                            {
                                //cập nhật toàn bộ ngày cho bảo hiểm y tết
                                DataTable dt1 = new DataTable();
                                dt1 = (DataTable)grdBHYT.DataSource;
                                if (dt1 == null || dt1.Rows.Count == 0)
                                {
                                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                string sBT = "sBTBHYT" + Commons.Modules.UserName;
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, dt1, "");

                                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();

                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spViewUpdateBHYT", conn);
                                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBT;
                                cmd.Parameters.Add("@NgayHetHan", SqlDbType.NVarChar).Value = Convert.ToDateTime(result).ToString("MM/dd/yyyy");
                                cmd.CommandType = CommandType.StoredProcedure;
                                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                DataSet ds = new DataSet();
                                da.Fill(ds);
                                grdBHYT.DataSource = ds.Tables[0].Copy();

                                //string sSql = "UPDATE dbo.BAO_HIEM_Y_TE SET NGAY_HET_HAN ='" + Convert.ToDateTime(result).ToString("MM/dd/yyyy") + "'";
                                //SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                                //LoadGridBaoHiemYTe();
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        break;
                    }
            }
        }
        #endregion

        #region hàm xử lý dữ liệu
        private void LoadGridBaoHiemYTe()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongNhanBHYT", cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            if (grdBHYT.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdBHYT, grvNgungDongBHXH, dt, false, false, false, false, true, this.Name);
                grvNgungDongBHXH.Columns["ID_CN"].Visible = false;
                grvNgungDongBHXH.Columns["ID_BHYT"].Visible = false;
            }
            else
            {
                grdBHYT.DataSource = dt;
            }

            DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_TP = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            DataTable dID_NHOM = new DataTable();
            dID_NHOM.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboThanhPho", -1, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            cboID_TP.NullText = "";
            cboID_TP.ValueMember = "ID_TP";
            cboID_TP.DisplayMember = "TEN_TP";
            cboID_TP.DataSource = dID_NHOM;
            cboID_TP.Columns.Clear();
            cboID_TP.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_TP"));
            cboID_TP.Columns["ID_TP"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "ID_TP");
            //cboID_CN.Columns["ID_CN"].Visible = false;

            cboID_TP.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_TP"));
            cboID_TP.Columns["TEN_TP"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_TP");

            cboID_TP.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            cboID_TP.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            cboID_TP.Columns["ID_TP"].Visible = false;
            grvNgungDongBHXH.Columns["ID_TP"].ColumnEdit = cboID_TP;
            cboID_TP.BeforePopup += CboID_TP_BeforePopup;
            cboID_TP.EditValueChanged += CboID_TP_EditValueChanged;


            //Danh sach benh vien
            cboID_BV = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            DataTable dID_BV = new DataTable();
            dID_BV.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboBenhVien", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            cboID_BV.NullText = "";
            cboID_BV.ValueMember = "ID_BV";
            cboID_BV.DisplayMember = "TEN_BV";
            cboID_BV.DataSource = dID_BV;
            cboID_BV.Columns.Clear();
            cboID_BV.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_BV"));
            cboID_BV.Columns["ID_BV"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "ID_BV");
            //cboID_CN.Columns["ID_CN"].Visible = false;

            cboID_BV.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_BV"));
            cboID_BV.Columns["TEN_BV"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_BV");

            cboID_BV.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            cboID_BV.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            cboID_BV.Columns["ID_BV"].Visible = false;
            grvNgungDongBHXH.Columns["ID_BV"].ColumnEdit = cboID_BV;
            cboID_BV.BeforePopup += CboID_BV_BeforePopup;
            cboID_BV.EditValueChanged += CboID_BV_EditValueChanged;

            //Commons.Modules.ObjSystems.AddCombXtra("ID_TP", "TEN_TP", grvNgungDongBHXH, Commons.Modules.ObjSystems.DataThanhPho(-1, false), "ID_TP", "THANH_PHO");
            //Commons.Modules.ObjSystems.AddCombXtra("ID_BV", "TEN_BV", grvNgungDongBHXH, Commons.Modules.ObjSystems.DataBenhVien(false), "ID_BV", "DANH_SACH_BENH_VIEN");
            grvNgungDongBHXH.Columns["MS_CN"].OptionsColumn.ReadOnly = true;
            //grvNgungDongBHXH.Columns["MS_CN"].Width = 50;
            //grvNgungDongBHXH.Columns["HO_TEN"].Width = 100;
            //grvNgungDongBHXH.Columns["SO_THE"].Width = 100;
            //grvNgungDongBHXH.Columns["NGAY_HET_HAN"].Width = 100;

            RepositoryItemDateEdit dEditN = new RepositoryItemDateEdit();
            Commons.OSystems.SetDateRepositoryItemDateEdit(dEditN);
            grvNgungDongBHXH.Columns["NGAY_HET_HAN"].ColumnEdit = dEditN;
        }
        private void CboID_TP_EditValueChanged(object sender, EventArgs e)
        {
            try
            {

                LookUpEdit lookUp = sender as LookUpEdit;

                //string id = lookUp.get;

                // Access the currently selected data row
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;

                grvNgungDongBHXH.SetFocusedRowCellValue("ID_TP", (dataRow.Row[0]));

                string strSQL = "SELECT MS_TINH FROM THANH_PHO WHERE ID_TP = " + Convert.ToInt32(dataRow.Row[0]) + "";
                MS_TINH = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));
                Commons.Modules.sLoad = "";
            }
            catch { }
            //DataTable dID = new DataTable();
            //dID.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboBenhVien_Loc", MS_TINH, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            //grvNgungDongBHXH.SetFocusedRowCellValue("ID_BV", dID);
            //cboID_BV.DataSource = dID;
        }
        private void CboID_TP_BeforePopup(object sender, EventArgs e)
        {
        }
        private void CboID_BV_BeforePopup(object sender, EventArgs e)
        {
            try
            {

                LookUpEdit lookUp = sender as LookUpEdit;

                //string id = lookUp.get;

                // Access the currently selected data row
                //DataRowView dataRow = lookUp.Properties.DataSource as DataRowView;

                DataTable dID = new DataTable();
                dID.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboBenhVien_Loc", MS_TINH, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                lookUp.Properties.DataSource = dID;
            }
            catch { }
        }
        private void CboID_BV_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;

            //string id = lookUp.get;

            // Access the currently selected data row
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;

            grvNgungDongBHXH.SetFocusedRowCellValue("ID_BV", (dataRow.Row[0]));
        }
        private void Savedata()
        {
            try
            {
                //tạo một datatable 
                string sBTBHTY = "sBTBHYT" + Commons.Modules.UserName;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTBHTY, Commons.Modules.ObjSystems.ConvertDatatable(grvNgungDongBHXH), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveBaoHiemYTe", sBTBHTY);
            }
            catch
            {

            }
        }
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = !visible;
            windowsUIButton.Buttons[1].Properties.Visible = !visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
            windowsUIButton.Buttons[6].Properties.Visible = !visible;
            windowsUIButton.Buttons[7].Properties.Visible = visible;
            searchControl.Visible = visible;
        }
        private void XoaBaoHiemYTe()
        {
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.BAO_HIEM_Y_TE WHERE ID_BHYT = " + grvNgungDongBHXH.GetFocusedRowCellValue("ID_BHYT") + "");
                LoadGridBaoHiemYTe();
            }
            catch
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
            }
        }
        #endregion

        private void grvNgungDongBHXH_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            //GridColumn colTuNgay = view.Columns["NGHI_TU_NGAY"];
            //GridColumn colDenNgay = view.Columns["NGHI_DEN_NGAY"];

            //GridColumn colThang = view.Columns["THANG"];
            //GridColumn colThangChuyen = view.Columns["THANG_CHUYEN"];

            //GridColumn colDot = view.Columns["DOT"];
            //GridColumn colDotChuyen = view.Columns["DOT_CHUYEN"];
            if (e.Column.Name == "colID_TP")
            {


                //view.SetRowCellValue(e.RowHandle, view.Columns["ID_BV"], 1);
            }
        }
    }
}
