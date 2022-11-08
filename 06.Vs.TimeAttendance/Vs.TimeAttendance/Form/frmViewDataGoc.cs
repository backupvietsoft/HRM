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
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraBars.Docking2010;
using Vs.Report;
using DevExpress.Utils;
using DevExpress.XtraLayout;

namespace Vs.TimeAttendance
{
    
    public partial class frmViewDataGoc : DevExpress.XtraEditors.XtraForm
    {
        public DateTime dNgayCC = DateTime.Now;
        public Int64 iIDCN = -1;

        public frmViewDataGoc()
        {
            InitializeComponent();
           
        }

        private void frmViewDataGoc_Load(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.ThayDoiNN(this);
            DataTable dt = new DataTable();
            try
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT  CONVERT(DATETIME,CONVERT(NVARCHAR(10),NGAY_DEN,101) + ' ' + CONVERT(NVARCHAR(8),GIO_DEN,108)) AS NGAY_GIO_DEN, CONVERT(DATETIME, CONVERT(NVARCHAR(10),T1.NGAY_VE,101) + ' ' +  CONVERT(NVARCHAR(8),T1.GIO_VE,108) ) AS NGAY_GIO_VE FROM DU_LIEU_QUET_THE_K T1 WHERE (CONVERT(NVARCHAR(10),T1.NGAY,101) = CONVERT(NVARCHAR(10),'" +  dNgayCC.Date.ToString("MM/dd/yyyy") + "',101)) AND (ID_CN = "  + iIDCN.ToString() + ") AND (ISNULL(CHINH_SUA,0) = 0) ORDER BY T1.NGAY_DEN, T1.GIO_DEN, T1.NGAY_VE, T1.GIO_VE"));

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, true, false, true, true, this.Name);
                grvData.Columns["NGAY_GIO_DEN"].DisplayFormat.FormatType = FormatType.DateTime;
                grvData.Columns["NGAY_GIO_DEN"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                grvData.Columns["NGAY_GIO_VE"].DisplayFormat.FormatType = FormatType.DateTime;
                grvData.Columns["NGAY_GIO_VE"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";


            }
            catch
            {

            }
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {    
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