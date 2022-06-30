using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace Vs.HRM
{
    public partial class frmToKhaiBHXH : DevExpress.XtraEditors.XtraForm
    {
        bool cothem = false;
        int idhdld = 0;
        public frmToKhaiBHXH(string sohd, string ngayhd, int idhd)
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1, windowsUIButton);
            lbl_SoHD.Text = sohd;
            lbl_NgayHD.Text = ngayhd;
            idhdld = idhd;
        }
        #region sự kiện của form
        private void frmToKhaiBHXH_Load(object sender, EventArgs e)
        {
            LoadgrdToKhaiBHXH("-1");
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            if (btn == null || btn.Tag == null) return;
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        Bindingdata(true);
                        cothem = true;
                        enableButon(false);
                        break;
                    }
                case "sua":
                    {
                        if (grvToKhaiBHXH.RowCount == 0) return;
                        cothem = false;
                        enableButon(false);
                        break;
                    }

                case "xoa":
                    {
                        if (grvToKhaiBHXH.RowCount == 0) return;
                        DeleteData();
                        break;
                    }
                case "luu":
                    {
                        if (!dxValidationProvider1.Validate()) return;
                        if (SaveData() == false) return;
                        enableButon(true);
                        break;
                    }
                case "khongluu":
                    {
                        enableButon(true);
                        Bindingdata(false);
                        dxValidationProvider1.Validate();
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

        private void grd_ToKhaiBHXH_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Delete)
            {
                DeleteData();
            }

        }

        private void grvToKhaiBHXH_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Bindingdata(false);
        }
        #endregion

        #region hàm load form
        //hàm load gridview
        private void LoadgrdToKhaiBHXH(string id)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListToKhaiBHXH", idhdld, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["SO_TK"] };
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdToKhaiBHXH, grvToKhaiBHXH, dt, false, true, true, true, true, this.Name);
            grvToKhaiBHXH.Columns["NOI_DUNG_THAY_DOI"].AppearanceHeader.ForeColor = Color.Red;
            if (id != "-1")
            {
                int index = dt.Rows.IndexOf(dt.Rows.Find(id));
                grvToKhaiBHXH.FocusedRowHandle = grvToKhaiBHXH.GetRowHandle(index);
            }
            if (grvToKhaiBHXH.RowCount == 1)
            {
                Bindingdata(false);
            }
        }
        //hàm bingding dữ liệu
        private void Bindingdata(bool bthem)
        {
            Commons.Modules.sPS = "0Load";
            if (bthem == true)
            {
                //lấy dữ liệu mặc định theo id công nhân
                try
                {
                    SO_TKTextEdit.EditValue = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(MAX(SO_TK),0) + 1 FROM THAY_DOI_TO_KHAI_BHXH WHERE ID_HDLD = "+idhdld+" ");
                    NOI_DUNG_THAY_DOIMemoEdit.EditValue = "";
                    TAI_LIEU_KEM_THEOMemoEdit.EditValue = "";
                }
                catch 
                {
                    //XtraMessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                SO_TKTextEdit.EditValue= grvToKhaiBHXH.GetFocusedRowCellValue("SO_TK");
                NOI_DUNG_THAY_DOIMemoEdit.EditValue = grvToKhaiBHXH.GetFocusedRowCellValue("NOI_DUNG_THAY_DOI");
                TAI_LIEU_KEM_THEOMemoEdit.EditValue = grvToKhaiBHXH.GetFocusedRowCellValue("TAI_LIEU_KEM_THEO");
            }
        }
        //hàm tắc mở control
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
            windowsUIButton.Buttons[6].Properties.Visible = visible;
            grdToKhaiBHXH.Enabled = visible;
            SO_TKTextEdit.Properties.ReadOnly = visible;
            NOI_DUNG_THAY_DOIMemoEdit.Properties.ReadOnly = visible;
            TAI_LIEU_KEM_THEOMemoEdit.Properties.ReadOnly = visible;
        }
        #endregion

        #region hàm sử lý data
        //hàm sử lý khi lưu dữ liệu(thêm/Sửa)
        private bool SaveData()
        {
            try
            {
                string sophieu = "";
                try
                {
                    sophieu = grvToKhaiBHXH.GetFocusedRowCellValue("SO_TK").ToString();
                }
                catch (Exception)
                {
                    sophieu = "";
                }
                string n = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spUpdateToKhaiBHXH",
                    idhdld,
                    sophieu,
                    SO_TKTextEdit.EditValue.ToString(),
                    NOI_DUNG_THAY_DOIMemoEdit.EditValue,
                    TAI_LIEU_KEM_THEOMemoEdit.EditValue,
                    cothem
                ).ToString();
                LoadgrdToKhaiBHXH(n);
                return true;
            }
            catch 
            {
                return false;
            }
        }
        //hàm xử lý khi xóa dữ liệu
        private void DeleteData()
        {
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteToKhaiBHXH"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE	dbo.THAY_DOI_TO_KHAI_BHXH WHERE ID_HDLD  = " + idhdld + " AND SO_TK = " + grvToKhaiBHXH.GetFocusedRowCellValue("SO_TK") + "");
                grvToKhaiBHXH.DeleteSelectedRows();
            }
            catch
            {
            }
        }
        #endregion

    }
}