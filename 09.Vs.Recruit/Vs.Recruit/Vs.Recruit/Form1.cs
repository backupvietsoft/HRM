using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vs.HRM;
using Vs.Recruit.UAC;

namespace Vs.Recruit
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
            TinhSoTuanCuaTHang();
            Commons.Modules.iUngVien = -1;
            ucQLUV uac = new ucQLUV();
            this.Controls.Add(uac);
            uac.Dock = DockStyle.Fill;
        }
        private void TinhSoTuanCuaTHang()
        {
            try
            {
                //CultureInfo _culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                //CultureInfo _uiculture = (CultureInfo)CultureInfo.CurrentUICulture.Clone();

                //_culture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
                //_uiculture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;

                //System.Threading.Thread.CurrentThread.CurrentCulture = _culture;
                //System.Threading.Thread.CurrentThread.CurrentUICulture = _uiculture;

                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("Tuan", typeof(Int32));
                dt.Columns.Add("TNgay", typeof(DateTime));
                dt.Columns.Add("DNgay", typeof(DateTime));

                DateTime TN, DN;
                //lấy ngày bắc đầu và ngày kết thúc của tháng
                TN = DateTime.Now.Date.AddDays(-DateTime.Now.Date.Day + 1);
                DN = TN.AddMonths(1).AddDays(-1);
                //kiểm tra ngày bắc đầu có phải thứ 2 không
             
                for (int i = 1; i <= 4; i++)
                {
                    if(i == 1)
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
                    if(i == 2 || i == 3)
                    {
                        dt.Rows.Add(i, TN, TN.AddDays(6));
                        TN = TN.AddDays(7);
                        continue;
                    }
                    if (i==4)
                    {
                        dt.Rows.Add(i, TN, DN);
                        break;
                    }
                }

                DataTable dtap = dt;

            }
            catch
            {
            }

        }

    }
}
