using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace VietSoftHRM
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        private bool setTTCSuccess = false;
        private BackgroundWorker bw;
        public string[] _args;
        Thread t;
        public Form1(string[] args)
        {
            InitializeComponent();
            this.TransparencyKey = Color.White;
            this.BackColor = Color.White;
            _args = args;

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                await Task.Run(() => clsMain.setTTC());
                await Task.Run(() => clsMain.CheckUpdate());
                await Task.Run(() => Application.EnableVisualStyles());

                setTTCSuccess = true;
                Application.EnableVisualStyles();
            }
            catch { }
        }
        private void StartApp(string[] args, Thread t)
        {
            try
            {
                if (args.Length > 0)
                {

                    Commons.Modules.ObjSystems.User(Commons.Modules.UserName, 1);
                    t = new Thread(new ThreadStart(MRunInt));
                }
                else
                {
                    t = new Thread(new ThreadStart(MRunForm));
                }
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            if (setTTCSuccess == true)
            {
                progressBar1.Increment(100);
            }
            else
            {
                progressBar1.Increment(2);
            }
            if (progressBar1.Value == 100)
            {
                timer1.Enabled = false;
                StartApp(_args, t);
                this.Close();
            }
        }

        static void MRunForm()
        {
            try
            {
                //MRunInt();
                //Application.Run(new frmMain());

                Application.Run(new frmLogin());
                //Application.Run(new frmNotification());
                //Application.Run(new XtraForm1());
                //Application.Run(new frmThongTinChung(1));
                //Application.Run(new frmImportHinhCN(1));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        static void MRunInt()
        {
            try
            {
                string strSQL = "SELECT ISNULL(USER_KHACH,0) USER_KHACH FROM dbo.USERS WHERE [USER_NAME] = '" + Commons.Modules.UserName.Trim() + "'";
                try
                {
                    if (Convert.ToBoolean(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL)) == true)
                    {
                        Commons.Modules.chamCongK = true;
                    }
                }
                catch { }

                Application.Run(new frmMain());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
