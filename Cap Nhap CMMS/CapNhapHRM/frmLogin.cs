using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace CapNhapHRM
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "vietsoft")
            {
                MessageBox.Show("Pass error");
                return;
            }
            Thread thread = new Thread(new ThreadStart(ThreadProc));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(); 

            this.Close();

            
        }

        public static void ThreadProc()
        {
            Application.Run(new frmTHien());
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }


        private void label1_DoubleClick(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad != "CAPNHAPVER")
            {
                Thread thread = new Thread(new ThreadStart(ThreadProc));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            this.Close();
        }
    }
}
