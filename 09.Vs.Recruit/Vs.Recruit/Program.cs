using DevExpress.UserSkins;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VietSoftHRM;
using Vs.Report;

namespace Vs.Recruit
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
       {
           Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            BonusSkins.Register();
            Commons.Modules.ModuleName = "VS_HRM";
            Commons.Modules.UserName = "admin";
            DataSet ds = new DataSet();
            //ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\vsconfig.xml");
            Commons.IConnections.Username = "sa";
            Commons.IConnections.Server = @"27.74.240.29";
            Commons.IConnections.Database = "VS_HRM_DM";
            Commons.IConnections.Password = "codaikadaiku";
            Commons.Modules.sPrivate = @"PILMICO";
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT TOP 1 * FROM dbo.THONG_TIN_CHUNG"));
                Commons.Modules.sUrlCheckServer = Commons.Modules.ObjSystems.Decrypt(dt.Rows[0]["APIServer"].ToString(), true).Replace("VietSoftIP", Commons.Modules.sIP);
                Commons.Modules.iCustomerID = Convert.ToInt32(dt.Rows[0]["CustomerID"]);
                Commons.Modules.iLOAI_CN = Convert.ToInt32(dt.Rows[0]["LOAI_CN"]);//1 cập nhật trên server//2 cập nhật net.
                Commons.Modules.sHideMenu = Commons.Modules.ObjSystems.Decrypt(dt.Rows[0]["HIDE_MENU"].ToString(), true);

                try
                {
                    using (new ConnectToSharedFolder(Commons.Modules.sDDTaiLieu, new NetworkCredential(dt.Rows[0]["USER_TL"].ToString(), dt.Rows[0]["PASS_TL"].ToString())))
                    {
                        Commons.Modules.sDDTaiLieu = dt.Rows[0]["DUONG_DAN_TL"].ToString();
                    }
                }
                catch
                {
                    if (Commons.Modules.iLOAI_CN == 2)
                        Commons.Modules.iLOAI_CN = 0;
                }
            }
            catch
            {

            }

            //Commons.Modules.sPrivate = @"ADC";

            Commons.Modules.iSoLeSL = 1;
            Commons.Modules.iSoLeDG = 2;
            Commons.Modules.iSoLeTT = 3;
            //Commons.Modules.sSoLeSL = Commons.Modules.ObjSystems.sDinhDangSoLe(Commons.Modules.iSoLeSL);
            //Commons.Modules.sSoLeDG = Commons.Modules.ObjSystems.sDinhDangSoLe(Commons.Modules.iSoLeDG);
            //Commons.Modules.sSoLeTT = Commons.Modules.ObjSystems.sDinhDangSoLe(Commons.Modules.iSoLeTT);

            //Commons.Modules.sFontReport = "Monotype Corsiva";

            Commons.Modules.TypeLanguage = 0;
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Commons.Modules.ObjSystems.KhoMoi = false;
            //Commons.Modules.PermisString = "Read only";
            Thread t = new Thread(new ThreadStart(MRunForm));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }
        static void MRunForm()
        {
            try
            {
                //CultureInfo ciCurr = CultureInfo.CurrentCulture;
                //int weekNum = ciCurr.Calendar.GetWeekOfYear(Convert.ToDateTime("31/12/2022"), CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                //int n = DateTime.Now.DayOfYear;
                //var firstDayWeek = ciCurr.Calendar.
                Application.Run(new frmInBDNguonTuyenDung());
                //Application.Run(new frmInBDUngVienTheoKhuVuc());
                //frmViewReport frm = new frmViewReport();
                //XtraReport1 fpt = new XtraReport1();
                //frm.rpt = fpt;
                //frm.ShowDialog();
                //Application.Run(new frmEditTHONG_BAO_TUYEN_DUNG_VIEW(-1));
                //Application.Run(new frmEditVI_TRI_TUYEN_DUNG(-1,-1,-1,true));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}