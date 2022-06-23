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

namespace Vs.Payroll
{
    public partial class ucQuyDinhPhuCap : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;
        
        public static ucQuyDinhPhuCap _instance;
        public static ucQuyDinhPhuCap Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucQuyDinhPhuCap();
                return _instance;
            }
        }

        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        public ucQuyDinhPhuCap()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);
       
        }

        private void ucQuyDinhPhuCap_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonViKO(cboDonVi);
            LoaddNgayApDung();
            LoadGrdQDTPC();
            //addMay
            RepositoryItemLookUpEdit cboID_PC = new RepositoryItemLookUpEdit();
            cboID_PC.NullText = "";
            cboID_PC.ValueMember = "ID_PC";
            cboID_PC.DisplayMember = "TEN_PC";
            cboID_PC.DataSource = Commons.Modules.ObjSystems.DataPhuCap(Convert.ToString(cboNgay.EditValue));
            cboID_PC.Columns.Clear();
            cboID_PC.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_PC"));
            cboID_PC.Columns["TEN_PC"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_PC");
            cboID_PC.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            cboID_PC.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            grvData.Columns["ID_PC"].ColumnEdit = cboID_PC;
            cboID_PC.BeforePopup += CboID_PC_BeforePopup;
            EnableButon(isAdd);
            LoadGrdQDTPC();
            Commons.Modules.sLoad = "";
        }
        private void CboID_PC_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                Int64 id_pc = Convert.ToInt64(grvData.GetFocusedRowCellValue("ID_PC"));
                if (sender is LookUpEdit cbo)
                {
                    try
                    {
                        DataTable DataCombo = (DataTable)cbo.Properties.DataSource;
                        DataTable DataLuoi = Commons.Modules.ObjSystems.ConvertDatatable(grdData);
                        var DataNewCombo = DataCombo.AsEnumerable().Where(r => !DataLuoi.AsEnumerable()
                        .Any(r2 => r["ID_PC"].ToString().Trim() == r2["ID_PC"].ToString().Trim())).CopyToDataTable();
                        cbo.Properties.DataSource = null;
                        cbo.Properties.DataSource = DataNewCombo;
                    }
                    catch
                    {
                        cbo.Properties.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        private void LoadGrdQDTPC()
        {
            try
            {
                DataTable dt = new DataTable();

                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListQDTPC", Convert.ToDateTime(cboNgay.EditValue), cboDonVi.EditValue));

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, false, true, true, this.Name);
                
            }
            catch
            {

            }
            grvData.Columns["ID_QDTPC"].Visible = false;
            grvData.Columns["NGAY_AD"].Visible = false;
            grvData.Columns["ID_DV"].Visible = false;
            grvData.Columns["TT"].Visible = false;
            grvData.Columns["SO_TIEN_QD"].DisplayFormat.FormatType = FormatType.Numeric;
            grvData.Columns["SO_TIEN_QD"].DisplayFormat.FormatString = "N0";
            
        }




        public void LoaddNgayApDung()
        {
            try
            {
                DataTable dtthang = new DataTable();
                string sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),NGAY_AD,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),NGAY_AD,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),NGAY_AD,103),7) AS THANG FROM dbo.QUY_DINH_TIEN_PC ORDER BY Y DESC , M DESC";
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
                        LoadGrdQDTPC();
                        Commons.Modules.ObjSystems.AddnewRow(grvData, true);
                        EnableButon(isAdd);
                        break;
                        
                    }
                case "xoa":
                    {
                        XoaQDTPC();
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
                        LoadGrdQDTPC();
                        isAdd = false;
                        EnableButon(isAdd);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        break;
                    }
                case "khongghi":
                    {
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        LoadGrdQDTPC();
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
            btnALL.Buttons[3].Properties.Visible = !visible;
            btnALL.Buttons[4].Properties.Visible = visible;
            btnALL.Buttons[5].Properties.Visible = visible;
            cboDonVi.Enabled = !visible;
            cboNgay.Enabled = !visible;
        }

        private void XoaQDTPC()
        {
            if (grvData.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                string sSql = "DELETE dbo.QUY_DINH_TIEN_PC WHERE ID_QDTPC = " + grvData.GetFocusedRowCellValue("ID_QDTPC");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                grvData.DeleteSelectedRows();
            }
            catch
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
            }
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
            string sTB = "QDTPC_TMP" + Commons.Modules.UserName;
            try
            {

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveQuyDinhTienPC1", sTB);
                Commons.Modules.ObjSystems.XoaTable(sTB);
                return true;
            }
            catch
            {
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
            LoadGrdQDTPC();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        
        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdQDTPC();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void calThang_DateTimeCommit_1(object sender, EventArgs e)
        {
            try
            {
                cboNgay.Text = calThang.DateTime.Date.ToShortDateString();
            }
            catch
            {
            }
            cboNgay.ClosePopup();
        }
    }
}