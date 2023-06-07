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
using System.Diagnostics;
using Vs.Report;
using DevExpress.XtraBars.Docking2010;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace Vs.TimeAttendance.Form
{
    public partial class frmSaveKeHoachDiCa : DevExpress.XtraEditors.XtraForm
    {
        private Int64 idCN;
        private Int64 idNHOM;
        private string iCA;
        private DateTime dTuNgay;
        private DateTime dDenNgay;
        public bool result = false;
        public frmSaveKeHoachDiCa(Int64 IDCN, Int64 NHOM, string CA, DateTime TNgay, DateTime DenNgay)
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root);
            idCN = IDCN;
            idNHOM = NHOM;
            iCA = CA;
            dTuNgay = TNgay;
            dDenNgay = DenNgay;
        }

        private void frmSaveKeHoachDiCa_Load(object sender, EventArgs e)
        {

            txtTngay.EditValue = dTuNgay;
            txtDngay.EditValue = dDenNgay;

            Commons.OSystems.SetDateEditFormat(txtTngay);
            Commons.OSystems.SetDateEditFormat(txtDngay);

        }


        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "" +
                "Luu":
                    {
                        try
                        {
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spCapNhatDieuChinh", Commons.Modules.UserName, Commons.Modules.TypeLanguage, idCN, idNHOM, iCA, txtTngay.EditValue, txtDngay.EditValue, Commons.Modules.KyHieuDV.ToString());
                            result = true;
                        }
                        catch (Exception ex)
                        {
                            Commons.Modules.ObjSystems.MsgError(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongThanhCong")); return;
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
    }
}
