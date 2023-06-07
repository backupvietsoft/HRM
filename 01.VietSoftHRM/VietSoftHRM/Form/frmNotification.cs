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
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraLayout;
using DevExpress.CodeParser;
using DevExpress.DataProcessing.InMemoryDataProcessor;

namespace VietSoftHRM
{
    public partial class frmNotification : DevExpress.XtraEditors.XtraForm
    {
        // Dữ liệu được chọn
        public DataTable TableSource;
        private DataRow _dtrow;
        public DataRow RowSelected
        {
            get
            {
                return _dtrow;
            }
        }
        public frmNotification()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, tabbedControlGroup1, windowsUIButton);
            Commons.Modules.ObjSystems.ThayDoiNN(this);
        }

        private void frmNotification_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT NOI_DUNG, ISNULL(T2.FULL_NAME, T2.USER_NAME) UNAME , TINH_TRANG FROM dbo.NOTIFICATION T1 LEFT JOIN dbo.USERS T2 ON T2.ID_USER = T1.ID_USER"));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdSource, grvSource, dt, true, true, false, true, true, this.Name);
                LoadCboSP();
                Commons.Modules.sLoad = "";
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
                    case "EXEC":
                        {
                            if (tabbedControlGroup1.SelectedTabPage == tabThongBao)
                            {
                                try
                                {
                                    grvSource.CloseEditor();
                                    grvSource.UpdateCurrentRow();
                                    string sSQL = "UPDATE dbo.NOTIFICATION SET NOI_DUNG = N'" + grvSource.GetFocusedRowCellValue("NOI_DUNG") + "', ID_USER = " + Commons.Modules.iIDUser + ", " +
                                        "TINH_TRANG = " + Convert.ToInt32(grvSource.GetFocusedRowCellValue("TINH_TRANG")) + "";
                                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSQL);

                                    Commons.Modules.ObjSystems.Alert("Cập nhật thành công", Commons.Form_Alert.enmType.Success);
                                }
                                catch
                                {
                                    Commons.Modules.ObjSystems.Alert("Cập nhật không thành công", Commons.Form_Alert.enmType.Error);
                                }
                            }
                            else
                            {
                                ExecQuery();
                            }
                            break;
                        }
                    case "thoat":
                        {
                            this.Close();
                            break;
                        }
                    default:
                        break;
                }
            }
            catch { }
        }

        private void txtCauQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                ExecQuery();
            }
        }

        private void ExecQuery()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (txtCauQuery.Text == "")
                {
                    grdQuery.DataSource = null;
                    return;
                }
                string sSQL = txtCauQuery.SelectedText;
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSQL));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdQuery, grvQuery, dt, false, true, false, true, false, this.Name);
                this.Cursor = Cursors.Default;
                Commons.Modules.ObjSystems.Alert("Commands completed successfully.", Commons.Form_Alert.enmType.Success);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
        }
        private void LoadCboSP()
        {
            try
            {
                string sSQL = "SELECT -1 ID ,NULL [TYPE], NULL TEN_PROCEDURES UNION SELECT ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) STT,type ,name  FROM sys.objects WHERE type IN ('P','TF','FN')";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSQL));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSearchSP, dt, "ID", "TEN_PROCEDURES", "TEN_PROCEDURES", true, false);
            }
            catch { }
        }

        private void cboSearchSP_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            string sSQL = "SELECT 'ALTER' + SUBSTRING(OBJECT_DEFINITION(object_id),7,LEN(OBJECT_DEFINITION(object_id)))  From sys.objects where name='" + cboSearchSP.Text + "'";

            string sProc = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSQL));
            //Commons.Modules.ObjSystems.MAutoCompleteMemoEdit(txtCauQuery, dt, "TEN_TABLE");
            txtCauQuery.Text = sProc;
        }
    }
}