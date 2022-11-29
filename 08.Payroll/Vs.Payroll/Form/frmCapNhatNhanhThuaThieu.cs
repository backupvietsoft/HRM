using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Reflection;
using System.Linq;
using Microsoft.ApplicationBlocks.Data;

namespace Vs.Payroll
{
    public partial class frmCapNhatNhanhThuaThieu : DevExpress.XtraEditors.XtraForm
    {
        public Int64 iID_CHUYEN_SD = -1;
        public Int64 iID_ORD = -1;
        public Int64 iID_CD = -1;
        public DateTime dTNgay;
        public DateTime dDNgay;
        public frmCapNhatNhanhThuaThieu()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, windowsUIButton);
        }

        //sự kiên load form
        private void frmCapNhatNhanhThuaThieu_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "luu":
                    {

                        break;
                    }
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
            }
        }

        private void LoadData()
        {
            try
            {
                string sSQL = "SELECT CN.MS_CN, CN.HO + ' ' + CN.TEN HO_TEN, CONVERT(FLOAT, 0) AS SO_LUONG FROM dbo.PHIEU_CONG_DOAN PCD INNER JOIN dbo.CONG_NHAN CN ON CN.ID_CN = PCD.ID_CN WHERE ID_CHUYEN_SD  = " + iID_CHUYEN_SD + " AND ID_CD = " + iID_CD + " AND ID_ORD = " + iID_ORD + " AND NGAY BETWEEN '" + dTNgay.ToString("MM/dd/yyyy") + "'  AND '" + dDNgay.ToString("MM/dd/yyyy") + "'";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSQL));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, true, false, true, true, "");
                grvData.Columns["ID_CN"].Visible = false;
            }
            catch { }
        }
    }
}