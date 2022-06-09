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
    public partial class ucQuyDinhTamUng : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;
        
        public static ucQuyDinhTamUng _instance;
        public static ucQuyDinhTamUng Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucQuyDinhTamUng();
                return _instance;
            }
        }
        public ucQuyDinhTamUng()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);
            
        }

        private void ucQuyDinhTamUng_Load(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonViKO(cboDonVi);
            LoadGrdQuyDinhTamUng();
            //LoadGrdQuyDinhTamUng();
            EnableButon(isAdd); 
            Commons.Modules.sPS = "";
        }

        private void LoadGrdQuyDinhTamUng()
        {
            DataTable dt = new DataTable();
            try
            {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListQuyDinhTamUng", cboDonVi.EditValue,  Commons.Modules.TypeLanguage));
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, false, false, true, this.Name);
                    dt.Columns["TT"].ReadOnly = false;
            }
            catch
            {
            }
            grvData.Columns["ID_QDTU"].Visible = false;
            grvData.Columns["ID_DV"].Visible = false;
            //grvData.Columns["THANG"].Visible = false;
            grvData.Columns["TT"].Visible = false;
            grvData.Columns["SO_TIEN"].DisplayFormat.FormatType = FormatType.Numeric;
            grvData.Columns["SO_TIEN"].DisplayFormat.FormatString = "N0";

        }
        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        Commons.Modules.ObjSystems.AddnewRow(grvData, true);
                        isAdd = true;
                        EnableButon(isAdd);
                        break;
                        
                    }
                case "xoa":
                    {
                        XoaCheDoLV();
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
                        LoadGrdQuyDinhTamUng();
                        isAdd = false;
                        EnableButon(isAdd);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        break;
                    }
                case "khongghi":
                    {
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        LoadGrdQuyDinhTamUng();
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
            //cboNgay.Enabled = !visible;
        }

        private void XoaCheDoLV()
        {
            if (grvData.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                string sSql = "DELETE dbo.QUY_DINH_TAM_UNG WHERE ID_QDTU = " + grvData.GetFocusedRowCellValue("ID_QDTU");
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
                view.SetFocusedRowCellValue("ID_DV", cboDonVi.EditValue);
                //view.SetFocusedRowCellValue("THANG", cboNgay.EditValue);
                view.SetFocusedRowCellValue("TT", true);
            }
            catch(Exception ex)
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
            string sTB = "QDTU_TMP" + Commons.Modules.UserName;
            try
            {

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveQuyDinhTamUng", sTB);
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
            
            
        }

        private void cboNgay_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
           
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGrdQuyDinhTamUng();
            //EnableButon(true);
            Commons.Modules.sPS = "";
        }

        private void grdData_Validated(object sender, EventArgs e)
        {
            //Commons.Modules.OXtraGrid.SaveRegisterGrid(grdData);
        }
    }
}