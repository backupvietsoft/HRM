using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HRMServerTool
{
    public partial class frmMain : Form
    {
        bool bfirst = false;
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadServerInfo();
            string sHDD = "";
            try
            {
                //System.Management.ManagementObjectSearcher moSearcher = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                System.Management.ManagementObjectSearcher moSearcher = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
                foreach (System.Management.ManagementObject wmi_HD in moSearcher.Get())
                {
                    sHDD = wmi_HD["SerialNumber"].ToString();
                }
                txtHInfo.Text = sHDD;
            }catch
            {
                txtHInfo.Text = "";
            }
            if (txtHInfo.Text == "")
            {
                System.Management.ManagementObjectSearcher moSearcher = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
                foreach (System.Management.ManagementObject wmi_HD in moSearcher.Get())
                {
                    sHDD = wmi_HD["SerialNumber"].ToString();
                }
                txtHInfo.Text = sHDD;

            }

            bfirst = true;


            cboServices_SelectedIndexChanged(null, null);


        }

        private void LoadServerInfo()
        {
            DataTable vtb = new DataTable();
            vtb.Columns.Add("ServicesID", Type.GetType("System.String"));
            vtb.Columns.Add("ServicesName", Type.GetType("System.String"));

            vtb.Rows.Add("frmMain", "HRM Services");
            cboServices.DataSource = vtb;
            cboServices.DisplayMember = "ServicesName";
            cboServices.ValueMember = "ServicesID";
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (StartService("HRM Services"))
                {
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                }
                else
                {
                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + "Start không thành công");
            }
            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (StopService("HRM Services"))
                {
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                }
                else
                {
                    
                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + "Stop không thành công");
            }
        }

        private void cboServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bfirst == false) return;
            try
            {
                if (cboServices.Text == "" ) return;
                if (cboServices.Text.ToString() == "System.Data.DataRowView") return;

                
                if (IsServiceRunning("HRM Services"))
                {
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                }
                else
                {
                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }




        public static ServiceController GetService(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices(Environment.MachineName);
            
            return services.FirstOrDefault(x => x.DisplayName == serviceName);
        }

        public static bool IsServiceRunning(string serviceName)
        {
            ServiceControllerStatus status;
            uint counter = 0;
            do
            {
                ServiceController Services = GetService(serviceName);
                if (Services == null)
                {
                    return false;
                }

                Thread.Sleep(100);
                status = Services.Status;
            } while (!(status == ServiceControllerStatus.Stopped ||
                       status == ServiceControllerStatus.Running) &&
                     (++counter < 30));
            return status == ServiceControllerStatus.Running;
        }

        public static bool IsServiceInstalled(string serviceName)
        {
            return GetService(serviceName) != null;
        }

        public bool StartService(string serviceName)
        {
            ServiceController controller = GetService(serviceName);
            if (controller == null)
            {
                return false;
            }

            controller.Start();
            controller.WaitForStatus(ServiceControllerStatus.Running);
            return true;
        }

        public bool StopService(string serviceName)
        {
            ServiceController controller = GetService(serviceName);
            if (controller == null)
            {
                return false;
            }

            controller.Stop();
            controller.WaitForStatus(ServiceControllerStatus.Stopped);
            return true;
        }

        private void btnLic_Click(object sender, EventArgs e)
        {
            try
            {
                string sPath = "";
                sPath = "";
                OpenFileDialog f = new OpenFileDialog();
                f.Filter = "Xml file (*.xml)|*.xml";
                if (f.ShowDialog() == DialogResult.Cancel) return;
                
                sPath = f.FileName;
                if (sPath == "") return;
                //string directoryPath = System.IO.Path.GetDirectoryName(f.FileName);
                string destFile = System.IO.Path.Combine(Application.StartupPath, "config.xml");

                if (File.Exists(destFile))
                {
                    File.SetAttributes(destFile, FileAttributes.Normal);
                    File.Delete(destFile);
                }
                File.Copy(sPath, destFile, true);
                File.SetAttributes(destFile, FileAttributes.Normal);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
