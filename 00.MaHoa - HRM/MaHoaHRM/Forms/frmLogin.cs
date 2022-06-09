using System;
using System.Threading;
using System.Windows.Forms;

namespace MaHoaHRM.Forms
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        public int i = 0;



        private void button1_Click(object sender, EventArgs e)
        {
            if (i == 0)
            {
                if (textBox1.Text == "VsVietSoft")
                {
                    Thread thread = new Thread(new ThreadStart(ThreadProc));
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Pass error");
                    return;
                }
            }
            if (i == 1)
            {
                if (textBox1.Text == "VsNamViet")
                {
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Pass error");
                    DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    return;
                }
            }
            this.Close();
        }


        public static void ThreadProc()
        {
            Application.Run(new frmMHGM());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            if (i == 0)
            {
                
                textBox1.Text = "sdadasda";
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Thread thread = new Thread(new ThreadStart(ThreadProc));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                this.Close();
            }
        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            if (i == 1)
            {
                textBox1.Text = "sdadasda";
                DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
    }
}
