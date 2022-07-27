using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System.Diagnostics;
using Vs.Report;
using DevExpress.XtraBars.Docking2010;

namespace Vs.Recruit
{
    public partial class frmInKeHoachTD : DevExpress.XtraEditors.XtraForm
    {
        public frmInKeHoachTD()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this,Root);
        }

        private void frmInKeHoachTD_Load(object sender, EventArgs e)
        {
            datTThang.EditValue = DateTime.Now;
            datDThang.EditValue = DateTime.Now;
            //DateTime TN = datThang.DateTime.Date.AddDays(-datThang.DateTime.Date.Day + 1);
            //DateTime DN = TN.AddMonths(1).AddDays(-1);
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "In":
                    {
                        Datain();
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

        private DataTable Datain()
        {

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Tuan", typeof(Int32));
            dt.Columns.Add("TNgay", typeof(DateTime));
            dt.Columns.Add("DNgay", typeof(DateTime));

            DateTime TN = datTThang.DateTime.Date.AddDays(-datTThang.DateTime.Date.Day + 1);
            DateTime DN = datDThang.DateTime.Date.AddDays(-datDThang.DateTime.Date.Day + 1);
            DN = DN.AddMonths(1).AddDays(-1);
            while (TN.Month <= DN.Month && TN.Year <= DN.Year)
            {
                dt.Merge(TinhSoTuanCuaTHang(TN, DN),true);
                TN = TN.AddMonths(1);
            }

            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTTuanThang" + Commons.Modules.UserName,dt, "");
            DataSet set = new DataSet();

            set = SqlHelper.ExecuteDataset(Commons.IConnections.CNStr, "spGetListKeHoachTuyenDung", TN, DN, Commons.Modules.UserName, Commons.Modules.TypeLanguage, "sBTTuan" + Commons.Modules.UserName);

            return null;
        }

        private DataTable TinhSoTuanCuaTHang(DateTime TN, DateTime DN)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("Tuan", typeof(Int32));
                dt.Columns.Add("TNgay", typeof(DateTime));
                dt.Columns.Add("DNgay", typeof(DateTime));

                //kiểm tra ngày bắc đầu có phải thứ 2 không

                for (int i = 1; i <= 4; i++)
                {
                    if (i == 1)
                    {
                        if (TN.DayOfWeek == DayOfWeek.Monday)
                        {
                            dt.Rows.Add(i, TN, TN.AddDays(7));
                            TN = TN.AddDays(8);
                            continue;
                        }
                        else
                        {
                            dt.Rows.Add(i, TN, TN.AddDays(7 + (7 - (int)TN.DayOfWeek)));
                            TN = TN.AddDays(8 + (7 - (int)TN.DayOfWeek));
                            continue;
                        }
                    }
                    if (i == 2 || i == 3)
                    {
                        dt.Rows.Add(i, TN, TN.AddDays(6));
                        TN = TN.AddDays(7);
                        continue;
                    }
                    if (i == 4)
                    {
                        dt.Rows.Add(i, TN, DN);
                        break;
                    }
                }

                return dt;
            }
            catch
            {
                return null;
            }
        }

    }
}
