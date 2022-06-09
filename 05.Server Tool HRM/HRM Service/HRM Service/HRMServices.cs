using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.Threading;
using System.Diagnostics;

namespace HRMServices
{


    public partial class HRMService : ServiceBase
    {

        string pathXml = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        SocketPermission permission;

        public HRMService()
        {
            InitializeComponent();
        }


        protected override void OnStart(string[] args)
        {
            eventLog1.Log = "Application";
            eventLog1.Source = this.ServiceName.ToString();
            // Creates one SocketPermission object for access restrictions
            permission = new SocketPermission(
            NetworkAccess.Accept,     // Allowed to accept connections 
            TransportType.Tcp,        // Defines transport types 
            "",                       // The IP addresses of local host 
            SocketPermission.AllPorts // Specifies all ports 
            );



            Thread thdUDPServer = new Thread(new ThreadStart(serverThread));
            thdUDPServer.Start();


            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = Convert.ToString(ipHostInfo.AddressList.FirstOrDefault(address => address.AddressFamily == AddressFamily.InterNetwork));
            try
            {
                eventLog1.WriteEntry("Connect HostName : " + ipHostInfo.HostName.ToString() + " - " + "IP : " + ipAddress.ToString() + " - " + "Port : 60000");
            }
            catch { }
        }
        public void serverThread()
        {

            bool done = false;
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, 60000);
            UdpClient listener = new UdpClient(60000);

            try
            {
                while (!done)
                {
                    byte[] bytes = listener.Receive(ref groupEP);
                    string sReceive = Encoding.ASCII.GetString(bytes);
                    switch (sReceive.ToUpper())
                    {
                        case "HDD":
                            Byte[] HDDdata = Encoding.ASCII.GetBytes(CheckHDD());
                            listener.Send(HDDdata, HDDdata.Length, groupEP); // reply back
                            break;
                        case "LIC":
                            Byte[] Licdata = Encoding.ASCII.GetBytes(LicNumber());
                            listener.Send(Licdata, Licdata.Length, groupEP); // reply back
                            break;
                        case "LICCOM":
                            Byte[] Liccom = Encoding.Unicode.GetBytes(LicCom());
                            listener.Send(Liccom, Liccom.Length, groupEP); // reply back
                            break;
                        case "HDDLIC":
                            Byte[] LicHDD = Encoding.ASCII.GetBytes(CheckHDD() + ";" + LicNumber());
                            listener.Send(LicHDD, LicHDD.Length, groupEP); // reply back
                            break;
                        default:
                            Byte[] Condata = Encoding.ASCII.GetBytes("CONN");
                            listener.Send(Condata, Condata.Length, groupEP); // reply back
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry("Services error : " + ex.Message);
            }
            finally
            {
                listener.Close();
            }





        }
        protected override void OnStop()
        {
            try
            {

                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                string ipAddress = Convert.ToString(ipHostInfo.AddressList.FirstOrDefault(address => address.AddressFamily == AddressFamily.InterNetwork));
                try
                {
                    eventLog1.WriteEntry("Disconnect HostName : " + ipHostInfo.HostName.ToString() + " - " + "IP : " + ipAddress.ToString() + " - " + "Port : 60000");
                }
                catch { }

            }
            catch (Exception exc) { eventLog1.WriteEntry(exc.ToString()); }
        }

        protected override void OnContinue()
        { }



        private string CheckHDD()
        {
            try
            {
                string sHDD = "";
                try
                {
                    //System.Management.ManagementObjectSearcher moSearcher = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                    System.Management.ManagementObjectSearcher moSearcher = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
                    foreach (System.Management.ManagementObject wmi_HD in moSearcher.Get())
                    {
                        sHDD = wmi_HD["SerialNumber"].ToString();
                    }
                }
                catch { sHDD = ""; }
                eventLog1.WriteEntry("HDD tmp 0 : " + sHDD);

                if (sHDD == "")
                {
                    System.Management.ManagementObjectSearcher moSearcher = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
                    try
                    {
                        foreach (System.Management.ManagementObject wmi_HD in moSearcher.Get())
                        {
                            sHDD = wmi_HD["SerialNumber"].ToString();
                        }
                    }
                    catch { sHDD = ""; }
                    eventLog1.WriteEntry("HDD tmp 1 : " + sHDD);
                }


                XDocument document = XDocument.Load(pathXml + "\\config.xml");
                var LKEY = from r in document.Descendants("VietsoftCMMS")
                           select r.Element("LKEY").Value;
                string sXML = "";
                string sDNgay = "";

                foreach (var r in LKEY)
                {
                    sXML = r.ToString();
                }
                sXML = CryptorEngine.Decrypt(sXML, true);
                sXML = sXML.Substring(2);
                sDNgay = sXML;
                sXML = sXML.Substring(0, sXML.Length - 10);
                sDNgay = sDNgay.Substring(sHDD.Length, sDNgay.Length - sHDD.Length - 2);
                eventLog1.WriteEntry("HDD : " + sXML);

                if (sXML == sHDD) return "TRUE!" + sDNgay; else return "FALSE!" + sDNgay;

            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry("CheckHDD error : " + ex.Message);
                return "FALSE!19000101";
            }

        }

        private string LicNumber()
        {
            try
            {
                XDocument document = XDocument.Load(pathXml + "\\config.xml");
                var LICLIMIT = from r in document.Descendants("VietsoftCMMS")
                               select r.Element("LICLIMIT").Value;
                string sXML = "";
                string sDNgay = "";
                foreach (var r in LICLIMIT)
                {
                    sXML = r.ToString();
                }
                sXML = CryptorEngine.Decrypt(sXML, true);
                sDNgay = sXML;
                sXML = sXML.Substring(13);
                sXML = sXML.Substring(0, sXML.Length - 5);
                sDNgay = sDNgay.Substring(5).Substring(0, 8);
                return sXML + "!" + sDNgay;
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry("LicNumber error : " + ex.Message);
                return "0!19000101";
            }

        }

        private string LicCom()
        {
            try
            {
                XDocument document = XDocument.Load(pathXml + "\\config.xml");
                var LICLIMIT = from r in document.Descendants("VietsoftCMMS")
                               select r.Element("LIC").Value;
                string sXML = "";
                foreach (var r in LICLIMIT)
                {
                    sXML = r.ToString();
                }
                sXML = CryptorEngine.Decrypt(sXML, true);
                return sXML;
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry("LicNumber error : " + ex.Message);
                return "0";
            }

        }





    }
}