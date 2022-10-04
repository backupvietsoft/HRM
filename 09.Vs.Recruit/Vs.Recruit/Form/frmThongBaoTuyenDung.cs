using System;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using Microsoft.ApplicationBlocks.Data;
using System.Windows.Forms;

namespace Vs.Recruit
{
    public partial class frmThongBaoTuyenDung : DevExpress.XtraEditors.XtraForm
    {
        // Dữ liệu được chọn
        public Int64 iID_LCV = 0;
        public Int64 iID_YCTD = 0;
        public string SoYC = "";
        public frmThongBaoTuyenDung()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, btnALL);
            this.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, this.Name);
        }
        private void frmThongBaoTuyenDung_Load(object sender, EventArgs e)
        {
            try
            {
                LoadText();
            }
            catch { }

        }
        private void LoadText()
        {
            try
            {
                string sSql = "SELECT TOP 1 CASE "+ Commons.Modules.TypeLanguage +" WHEN 0 then T2.TEN_LCV WHEN 1 THEN T2.TEN_LCV_A ELSE T2.TEN_LCV_H END TEN_LCV,T1.MO_TA_CV, T1.YEU_CAU, T1.YEU_CAU_KHAC, T1.THOI_GIAN_LAM_VIEC, T1.CHE_DO_PHUC_LOI FROM dbo.YCTD_VI_TRI_TUYEN T1 INNER JOIN dbo.LOAI_CONG_VIEC T2 ON T2.ID_LCV = T1.ID_VTTD WHERE T1.ID_YCTD = "+ iID_YCTD +" AND T1.ID_VTTD = "+ iID_LCV +"";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                lblLoaiCongViec.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblThongBaoTuyenDungCho") +" " + SoYC + " " + dt.Rows[0]["TEN_LCV"].ToString();
                txtMO_TA_CV.Text = dt.Rows[0]["MO_TA_CV"].ToString();
                txtYEU_CAU.Text = dt.Rows[0]["YEU_CAU"].ToString();
                txtYEU_CAU_KHAC.Text = dt.Rows[0]["YEU_CAU_KHAC"].ToString();
                txtTHOI_GIAN_LAM_VIEC.Text = dt.Rows[0]["THOI_GIAN_LAM_VIEC"].ToString();
                txtCHE_DO_PHUC_LOI.Text = dt.Rows[0]["CHE_DO_PHUC_LOI"].ToString();
            }
            catch { }
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {

                case "luu":
                    {
                        if (!SaveData())
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_LuuKhongThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        this.Close();
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

        private bool SaveData()
        {
            try
            {
                string strSQL = "UPDATE dbo.YCTD_VI_TRI_TUYEN SET MO_TA_CV = N'" + txtMO_TA_CV.Text + "', YEU_CAU = N'" + txtYEU_CAU.Text + "', YEU_CAU_KHAC = N'" + txtYEU_CAU_KHAC.Text + "', THOI_GIAN_LAM_VIEC = N'" + txtTHOI_GIAN_LAM_VIEC.Text + "', CHE_DO_PHUC_LOI = N'" + txtCHE_DO_PHUC_LOI.Text + "' WHERE ID_YCTD = "+ iID_YCTD +" AND ID_VTTD = " + iID_LCV + "";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, strSQL);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}