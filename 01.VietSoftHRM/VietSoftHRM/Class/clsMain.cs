using CredentialManagement;
using DevExpress.Charts.Native;
using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace VietSoftHRM
{
    public static class clsMain
    {
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());

            //foreach (var ip in host.AddressList)
            //{
            //    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            //    {
            //        return ip.ToString();
            //    }
            //}
            //throw new Exception("No network adapters with an IPv4 address in the system!");
            return host.HostName.ToString();
        }

        public static void setConfig()
        {
            try
            {
                Commons.Modules.ModuleName = "VS_HRM";
                Commons.Modules.UserName = "admin";
                DataSet ds = new DataSet();

                ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\vsconfig.xml");
                Commons.IConnections.Username = ds.Tables[0].Rows[0]["U"].ToString();
                Commons.IConnections.Server = Commons.Modules.ObjSystems.Decrypt(ds.Tables[0].Rows[0]["S"].ToString(), true);
                Commons.IConnections.Database = ds.Tables[0].Rows[0]["D"].ToString();
                Commons.IConnections.Password = Commons.Modules.ObjSystems.Decrypt(ds.Tables[0].Rows[0]["P"].ToString(), true);
                Commons.Modules.sIP = Commons.Modules.ObjSystems.Decrypt(ds.Tables[0].Rows[0]["IP"].ToString(), true);
                Commons.Modules.ChangLanguage = false;
                ds = new DataSet();
                ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\savelogin.xml");
                try
                {
                    Commons.Modules.TypeLanguage = int.Parse(ds.Tables[0].Rows[0]["N"].ToString());
                }
                catch { Commons.Modules.TypeLanguage = 0; }
                Commons.Modules.iSoLeSL = 1;
                Commons.Modules.iSoLeDG = 2;
                Commons.Modules.iSoLeTT = 0;
                Commons.Modules.iNNghi = 1;
                Commons.Modules.sSoLeSL = Commons.Modules.ObjSystems.sDinhDangSoLe(Commons.Modules.iSoLeSL);
                Commons.Modules.sSoLeDG = Commons.Modules.ObjSystems.sDinhDangSoLe(Commons.Modules.iSoLeDG);
                Commons.Modules.sSoLeTT = Commons.Modules.ObjSystems.sDinhDangSoLe(Commons.Modules.iSoLeTT);
                //Set số giờ tăng ca trong ngày, tuần, tháng cho chấm công khách <MT>
                Commons.Modules.iSNNgay = 4;
                Commons.Modules.iSNTuan = 12;
                Commons.Modules.iSNThang = 40;
            }
            catch
            {
            }
        }
        public static void setTTC()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT TOP 1 * FROM dbo.THONG_TIN_CHUNG"));
                Commons.Modules.sUrlCheckServer = Commons.Modules.ObjSystems.Decrypt(dt.Rows[0]["APIServer"].ToString(), true).Replace("VietSoftIP", Commons.Modules.sIP);
                Commons.Modules.iCustomerID = Convert.ToInt32(dt.Rows[0]["CustomerID"]);
                Commons.Modules.iLOAI_CN = Convert.ToInt32(dt.Rows[0]["LOAI_CN"]);//1 cập nhật trên server//2 cập nhật net.
                Commons.Modules.KyHieuDV = Convert.ToString(dt.Rows[0]["KY_HIEU_DV"]);
                Commons.Modules.sHideMenu = Commons.Modules.ObjSystems.Decrypt(dt.Rows[0]["HIDE_MENU"].ToString(), true);
                Commons.Modules.connect = Commons.Modules.ObjSystems.Decrypt(dt.Rows[0]["CON_NECT"].ToString(), true);
                Commons.Modules.iGio = Convert.ToDouble(dt.Rows[0]["SG_LV"]);
                Commons.Modules.iLamTronGio = Convert.ToInt32(dt.Rows[0]["LOAI_LT_CC"]); //0 Khong lam tron giờ//1 làm tròn giờ
                try
                {
                    Commons.Modules.loadHeader = Convert.ToInt32(dt.Rows[0]["LOAD_HEADER"]);
                }
                catch
                {
                    Commons.Modules.loadHeader = 0;
                }
                try
                {
                    Commons.Modules.bKiemPCD = Convert.ToBoolean(dt.Rows[0]["CHECK_PCD"]);
                }
                catch
                {
                }
                try
                {
                    Commons.Modules.iHeSo = Convert.ToInt32(dt.Rows[0]["HE_SO"]);
                }
                catch
                {
                    Commons.Modules.iHeSo = 0;
                }
                try
                {
                    Commons.Modules.bKiemLCVCV = Convert.ToBoolean(dt.Rows[0]["LOC_CHUC_VU"]); // có lọc chức vụ theo loại công việc không , 1 có 0 = 0
                }
                catch
                {
                    Commons.Modules.bKiemLCVCV = true;
                }

                try
                {
                    using (new ConnectToSharedFolder(dt.Rows[0]["DUONG_DAN_TL"].ToString(), new NetworkCredential(Commons.Modules.ObjSystems.Decrypt(dt.Rows[0]["USER_TL"].ToString(), true), Commons.Modules.ObjSystems.Decrypt(dt.Rows[0]["PASS_TL"].ToString(), true))))
                    {
                        Commons.Modules.sDDTaiLieu = dt.Rows[0]["DUONG_DAN_TL"].ToString();
                        bool exists = System.IO.Directory.Exists(Commons.Modules.sDDTaiLieu);
                        if (!exists)
                        {
                            System.IO.Directory.CreateDirectory(Commons.Modules.sDDTaiLieu);
                        }
                        using (var cred = new Credential())
                        {
                            cred.Username = Commons.Modules.ObjSystems.Decrypt(dt.Rows[0]["USER_TL"].ToString(), true);
                            cred.Password = Commons.Modules.ObjSystems.Decrypt(dt.Rows[0]["PASS_TL"].ToString(), true);
                            cred.Target = Commons.Modules.sDDTaiLieu.Substring(2, Commons.Modules.sDDTaiLieu.Substring(2).IndexOf("\\"));
                            cred.Type = CredentialType.DomainPassword;
                            cred.PersistanceType = PersistanceType.LocalComputer;
                            cred.Save();
                        }
                    }
                }
                catch
                {
                    Commons.Modules.sDDTaiLieu = "";
                    Commons.Modules.iLOAI_CN = 0;
                }
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
        }
        public static bool CheckServer()
        {
            //string hostName = clsMain.GetLocalIPAddress();
            //if (hostName != "NHAT-HR")
            //{
            //    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgSaiHDD"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}


            string resulst = "";
            //2.kiểm tra HHD
            resulst = Commons.Modules.ObjSystems.GetAPI("HDD");
            if (resulst.Split('!')[0].ToString() != "TRUE")
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgSaiHDD"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            String sSql = "";
            sSql = resulst.Split('!')[2];
            try
            {
                var items = sSql.Split('|');
                if (items.Length > 2)
                {
                    for (int i = 1; i < items.Length; i++)
                    {
                        string sTmp = items[i].ToString();
                        //if (sTmp.Contains(" VietSoftHRM~") == true)
                        //{
                        Commons.Modules.iLic = Commons.Modules.ObjSystems.MCot(sTmp.Split('~')[1].ToString());
                        break;
                        //}
                    }
                }
                else
                    Commons.Modules.iLic = Commons.Modules.ObjSystems.MCot(items[1].ToString().Split('~')[1].ToString());

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (sSql.Split('|')[0].ToUpper() == "DEMO")
            {
                //5.kiểm tra hết hạn
                DateTime Ngay = DateTime.ParseExact(resulst.Split('!')[1], "yyyyMMdd", CultureInfo.InvariantCulture);
                DateTime date = Convert.ToDateTime(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT GETDATE()"));
                if (Ngay.Date < date.Date)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgHetHanSuDung"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            //lấy trên net
            if (Commons.Modules.iLOAI_CN == 2)
            {
                try
                {
                    sSql = @"https://api.vietsoft.com.vn/VS.Api/Support/SumNumberlicense?SoftwareProductID=1&CustomerID=" + Commons.Modules.iCustomerID;
                    //Commons.Mod.iLic = "";
                    DataTable dtTmp = new DataTable();
                    dtTmp = Commons.Modules.ObjSystems.getDataAPI(sSql);
                    Commons.Modules.iLic = int.Parse(dtTmp.Rows[0][0].ToString());
                }
                catch
                {
                }
            }
            return true;
        }
        public static void CheckUpdate()
        {
            string sSql = "";
            try
            {
                #region Lay thong tin ver server
                sSql = "SELECT TOP 1 VER FROM dbo.THONG_TIN_CHUNG";
                sSql = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, System.Data.CommandType.Text, sSql));
                try
                {
                    Commons.Modules.sInfoSer = sSql.Substring(0, (sSql.Length - 4));
                    Commons.Modules.sInfoSer = Commons.Modules.sInfoSer.Substring(6, 2) + "/" + Commons.Modules.sInfoSer.Substring(4, 2) + "/" + Commons.Modules.sInfoSer.Substring(0, 4) + "." + sSql.Substring(8, sSql.Length - 8);
                }
                catch
                {
                    Commons.Modules.sInfoSer = "01/01/2000.0001";
                    sSql = "200001010001";
                }
                #endregion

                #region Lay thong tin ver client
                string sVerClient;
                sVerClient = LayDuLieu(@"Version.txt");
                try
                {
                    Commons.Modules.sInfoClient = sVerClient.Substring(0, (sVerClient.Length - 4));
                    Commons.Modules.sInfoClient = Commons.Modules.sInfoClient.Substring(6, 2) + "/" + Commons.Modules.sInfoClient.Substring(4, 2) + "/" + Commons.Modules.sInfoClient.Substring(0, 4) + "." + sVerClient.Substring(8, sVerClient.Length - 8);
                }
                catch
                {
                    Commons.Modules.sInfoClient = "01/01/2000.0001";
                    sVerClient = "200001010001";
                }
                #endregion
                try
                {
                    if (double.Parse(sVerClient) == double.Parse(sSql))
                    {

                        return;
                    }
                }
                catch (Exception ex)
                {
                    Commons.Modules.ObjSystems.MsgError(ex.Message);
                    return;
                }
                //sSql = "SELECT TOP 1 (CONVERT(NVARCHAR,LOAI_CN) + '!' + isnull(LINK1, '-1') + '!' + isnull(LINK2, '-1') + '!' + isnull(LINK3, '-1') + '!' + isnull(G_LINK_UD, '-1') + '!' + isnull(G_LINK_VERSION, '-1')) AS CAPNHAT FROM THONG_TIN_CHUNG";
                sSql = "SELECT TOP 1 (CONVERT(NVARCHAR,LOAI_CN) + '!' + isnull(LINK1, '-1') + '!' + isnull(LINK2, '-1') + '!' + isnull(LINK3, '-1')) AS CAPNHAT FROM THONG_TIN_CHUNG";
                sSql = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, System.Data.CommandType.Text, sSql));


                string[] sArr = sSql.Split('!');
                int loai = Convert.ToInt32(sArr[0].ToString());
                String link1 = sArr[1];
                String link2 = sArr[2];
                String link3 = sArr[3];

                //string g_link_ud = sArr[4]; // link update toàn cục (tất cả các khách hàng đều update)
                //string g_link_ver = sArr[5]; // link version toàn cục

                //Khong có loai update thi thoát
                if (loai <= -1) return;

                switch (loai) // update cho từng khách hàng
                {
                    //Loai 2 xai link1,2 : path link tren dropbox 
                    //Loai 1 xai link3: path link tren server
                    case 1:  //Update tren server voi link3
                        {
                            if (string.IsNullOrEmpty(link3)) return;
                            if (!Directory.Exists(link3))
                            {
                                XtraMessageBox.Show("Link update : " + link3 + " không tồn tại.");
                                return;
                            }
                            MUpdate(loai, ".", ".", link3);
                            break;
                        }
                    case 2: // Updatetren dropbox
                        {
                            if (string.IsNullOrEmpty(link1)) return;
                            MUpdate(loai, link1, link2, ".");
                            break;
                        }
                    default: { break; }
                }

                //// update toàn cục
                //if (string.IsNullOrEmpty(g_link_ud)) return;
                //MUpdate(loai, g_link_ud, g_link_ver, ".");
                //Commons.Modules.sInfoSer = Commons.Modules.sInfoClient;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.MsgError(ex.Message);
            }
        }
        private static void MUpdate(int loai, String link1, String link2, String link3)
        {
            try
            {
                System.Diagnostics.Process.Start("Update.exe", loai.ToString() + " " + link1 + " " + link2 + " " + link3 + " " + Application.ProductName);
                //https://www.dropbox.com/s/ntwwve7ys4awrkj/Update.zip?dl=0
                //https://www.dropbox.com/s/6gppx79hbcph1qp/Version.txt?dl=0
                //VS.OEE

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
        }
        public static string LayDuLieu(string TenFile)
        {
            StreamReader sr;
            string sText;
            sText = "";
            try
            {
                sText = Application.StartupPath.ToString() + @"\" + TenFile;
                sr = new StreamReader(sText);
                sText = "";
                sText = sr.ReadLine();
                try
                {
                    if (sText == null)
                        sText = "";
                }
                catch
                {
                    sText = "";
                }
                sr.Close();
            }
            catch
            {
            }
            return sText;
        }
    }
}
