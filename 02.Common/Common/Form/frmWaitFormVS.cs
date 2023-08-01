using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Commons
{
    public partial class frmWaitFormVS : Form
    {
        public Action Worker { get; set; }
        public frmWaitFormVS(Action worker)
        {
            InitializeComponent();
            this.TransparencyKey = Color.White;
            this.BackColor = Color.White;
            if (worker == null)
            {
                throw new ArgumentNullException();
            }
            Worker = worker;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Task.Factory.StartNew(Worker).ContinueWith(t => { this.Close(); },TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}