using System;
using System.Data;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;

namespace Vs.Recruit
{
    public partial class frmAuToKeHoachPhongVan : DevExpress.XtraEditors.XtraForm
    {
        public string soKH;
        public DateTime NgayLap, NgayPV;
        public Int64 NguoiPV1, NguoiPV2,IDKH;
        public bool Online;

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "luu":
                    {
                        if (!dxValidationProvider1.Validate()) return;
                        NgayLap = datNgayLap.DateTime ;
                        NgayPV = datNgayPV.DateTime;
                        NguoiPV1 = Convert.ToInt64(cboNguoiPV1.EditValue);
                        NguoiPV2 = Convert.ToInt64(cboNguoiPV2.EditValue);
                         Online = chkKieuPV.Checked;
                        soKH = txtSO_KH.Text;

                        this.Close();
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        break;
                    }
                default: break;
            }
        }


        public frmAuToKeHoachPhongVan()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root,windowsUIButton);
        }

        private void frmAuToKeHoachPhongVan_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT T1.ID_CN, T1.MS_CN, T1.HO +' '+ T1.TEN AS TEN_CN FROM dbo.CONG_NHAN T1 INNER JOIN dbo.XI_NGHIEP_NGUOI_TUYEN_DUNG T2 ON T2.ID_CN = T1.ID_CN WHERE T2.ID_XN = (SELECT TOP 1 ID_XN FROM dbo.KE_HOACH_PHONG_VAN WHERE ID_KHPV =" + IDKH + ")  AND T2.PHONG_VAN = 1 ORDER BY T1.HO + ' ' + T1.TEN"));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNguoiPV1, dt, "ID_CN", "TEN_CN", "TEN_CN", true, true);

            dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT T1.ID_CN, T1.MS_CN, T1.HO +' '+ T1.TEN AS TEN_CN FROM dbo.CONG_NHAN T1 WHERE T1.PV_TD = 1"));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNguoiPV2, dt, "ID_CN", "TEN_CN", "TEN_CN", true, true);

            txtSO_KH.EditValue = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_KHPV(" + datNgayLap.DateTime.ToString("MM/dd/yyyy") + ")").ToString();
            datNgayLap.DateTime = NgayLap;
            datNgayPV.DateTime = NgayPV;
            cboNguoiPV1.EditValue = NguoiPV1;
            cboNguoiPV2.EditValue = NguoiPV2;
            Online = chkKieuPV.Checked ;

        }

    }
}
