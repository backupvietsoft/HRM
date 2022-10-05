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
    public partial class ucMucThuongChuyenCan : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;
        
        public static ucMucThuongChuyenCan _instance;
        public static ucMucThuongChuyenCan Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucMucThuongChuyenCan();
                return _instance;
            }
        }

        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        public ucMucThuongChuyenCan()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);
       
        }

        private void ucMucThuongChuyenCan_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonViKO(cboDonVi);
            LoaddNgayApDung();
            LoadGrdMTCC();
            //addMay
            EnableButon(isAdd);
            //LoadGrdQDTPC();
            Commons.Modules.sLoad = "";
        }
        private void LoadGrdMTCC()
        {
            try
            {
                DataTable dt = new DataTable();

                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListMucThuongChuyenCan", Commons.Modules.UserName, Commons.Modules.TypeLanguage,  cboDonVi.EditValue, Convert.ToDateTime(cboNgay.EditValue)));

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, false, true, true, this.Name);

                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboLoaiThuong = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                cboLoaiThuong.NullText = "";
                cboLoaiThuong.ValueMember = "LOAI_THUONG";
                cboLoaiThuong.DisplayMember = "TEN_LOAI_THUONG";
                //ID_VTTD,TEN_VTTD
                DataTable dt1 = new DataTable();
                dt1.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiThuong", Commons.Modules.TypeLanguage));
                cboLoaiThuong.DataSource = dt1;
                cboLoaiThuong.Columns.Clear();
                cboLoaiThuong.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LOAI_THUONG"));
                cboLoaiThuong.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_LOAI_THUONG"));
                cboLoaiThuong.Columns["TEN_LOAI_THUONG"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_LOAI_THUONG");
                cboLoaiThuong.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboLoaiThuong.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cboLoaiThuong.Columns["LOAI_THUONG"].Visible = false;
                grvData.Columns["LOAI_THUONG"].ColumnEdit = cboLoaiThuong;
                cboLoaiThuong.BeforePopup += cboLoaiThuong_BeforePopup;
                cboLoaiThuong.EditValueChanged += cboLoaiThuong_EditValueChanged;
            }
            catch (Exception ex)
            {

            }
            grvData.Columns["TIEN_THUONG"].DisplayFormat.FormatType = FormatType.Numeric;
            grvData.Columns["TIEN_THUONG"].DisplayFormat.FormatString = "N0";
            
        }
        private void cboLoaiThuong_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvData.SetFocusedRowCellValue("LOAI_THUONG", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboLoaiThuong_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                DataTable dt1 = new DataTable();
                dt1.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiThuong", Commons.Modules.TypeLanguage));
                lookUp.Properties.DataSource = dt1;
            }
            catch { }
        }



        public void LoaddNgayApDung()
        {
            try
            {
                DataTable dtthang = new DataTable();
                string sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.MUC_THUONG_CHUYEN_CAN ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                grvThang.Columns["M"].Visible = false;
                grvThang.Columns["Y"].Visible = false;
                cboNgay.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch
            {
                cboNgay.Text = DateTime.Now.ToString("MM/yyyy");
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
                        LoadGrdMTCC();
                        Commons.Modules.ObjSystems.AddnewRow(grvData, true);
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
                        LoadGrdMTCC();
                        isAdd = false;
                        EnableButon(isAdd);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        break;
                    }
                case "khongghi":
                    {
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        LoadGrdMTCC();
                        isAdd = false;
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
            cboNgay.Enabled = !visible;
        }

        private void grvData_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.SetFocusedRowCellValue("TT", false);
                view.SetFocusedRowCellValue("NGAY_AD", cboNgay.EditValue);
                view.SetFocusedRowCellValue("ID_DV", cboDonVi.EditValue);
                
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
            DateTime dt = Commons.Modules.ObjSystems.ConvertDateTime(cboNgay.Text);
            string sTB = "sBTMTChuyenCan" + Commons.Modules.iIDUser;
            try
            {

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "sLoadaveMucThuongChuyenCan", sTB,Convert.ToDateTime(cboNgay.EditValue),Convert.ToInt32(cboDonVi.EditValue));
                Commons.Modules.ObjSystems.XoaTable(sTB);
                return true;
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
                cboNgay.Text = grvThang.GetFocusedRowCellValue("NGAY").ToString();
            }
            catch { }
            cboNgay.ClosePopup();
            
        }

        private void cboNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdMTCC();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        
        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdMTCC();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void calThang_DateTimeCommit_1(object sender, EventArgs e)
        {
            try
            {
                cboNgay.Text = calThang.DateTime.ToString("MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grvThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + cboNgay.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                }
                else { }
            }
            catch (Exception ex)
            {
                cboNgay.Text = calThang.DateTime.ToString("MM/yyyy");
            }
            cboNgay.ClosePopup();
        }

        private void grdData_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (btnALL.Buttons[0].Properties.Visible == false && e.KeyData == System.Windows.Forms.Keys.Delete)
            {
                grvData.DeleteSelectedRows();
            }
        }
    }
}