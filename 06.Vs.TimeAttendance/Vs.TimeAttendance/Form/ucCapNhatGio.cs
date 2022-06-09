using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraLayout;
using System.Threading;
using Microsoft.ApplicationBlocks.Data;
namespace Vs.HRM
{
    public partial class ucCapNhatGio : DevExpress.XtraEditors.XtraUserControl
    {
        public static ucCapNhatGio _instance;
        public static ucCapNhatGio Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucCapNhatGio();
                return _instance;
            }
        }


        public ucCapNhatGio()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, windowsUIButton);
        }
        #region Cập nhật giờ
        private void ucCapNhatGio_Load(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            Commons.Modules.sPS = "0Load";
            DateTime dtTN = DateTime.Today;
            dtTN = dtTN.AddDays(-dtTN.Day + 1);
            DateTime dtDN = dtTN.AddMonths(1);
            dtDN = dtDN.AddDays(-1);
            dTuNgay.EditValue = dtTN;
            dDenNgay.EditValue = dtDN;
            Commons.OSystems.SetDateEditFormat(dTuNgay);
            Commons.OSystems.SetDateEditFormat(dDenNgay);

            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            Commons.Modules.sPS = "";
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            Commons.Modules.sPS = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            Commons.Modules.sPS = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.sPS = "";
        }
        private bool kiemtrangay()
        {
            DateTime t = Convert.ToDateTime(dTuNgay.EditValue);
            DateTime d = Convert.ToDateTime(dDenNgay.EditValue);
            if(t>d)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_TuNgayDenNgay"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                dDenNgay.Focus();
                return false;
            }
            return true;
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "chamtudong":
                    {
                        try
                        {
                            if (!kiemtrangay()) return;

                            int iDay = 0;

                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            for (DateTime dt = Convert.ToDateTime(dTuNgay.EditValue); dt <= Convert.ToDateTime(dDenNgay.EditValue); dt = dt.AddDays(1))
                            {
                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spAutoUpdateTimekeeping", conn);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@UName", Commons.Modules.UserName);
                                cmd.Parameters.AddWithValue("@NNgu", Commons.Modules.TypeLanguage);
                                cmd.Parameters.AddWithValue("@ID_DV", cboDV.EditValue);
                                cmd.Parameters.AddWithValue("@ID_XN", cboXN.EditValue);
                                cmd.Parameters.AddWithValue("@ID_TO", cboTo.EditValue);
                                cmd.Parameters.AddWithValue("@DDate", dt);
                                cmd.ExecuteNonQuery();
                            }

                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_CapNhatThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                        }
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
            }
        }
        #endregion
    }
}
