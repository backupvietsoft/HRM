using System;
using System.Windows.Forms;

namespace MaHoaHRM.Forms
{
    public partial class frmMHGM : DevExpress.XtraEditors.XtraForm
    {
        public frmMHGM()
        {
            InitializeComponent();
        }

        
        private void frmMHGM_Load(object sender, EventArgs e)
        {
            txtCanGiaiMa.Focus();
            this.ActiveControl = txtCanGiaiMa;
        }

        private void txtGiaiMa_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(txtGiaiMa.Text);
        }


        private void txtMaHoa_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(txtMaHoa.Text);
        }
        
        

        private void btnMaHoaNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCanMaHoa.Text == "")
                {
                    error.SetError(txtCanMaHoa, "Enter the text you want to encrypt");
                }
                else
                {
                    error.Clear();
                    string clearText = txtCanMaHoa.Text.Trim();
                    string cipherText = CryptorEngine.Encrypt(clearText, true);
                    txtMaHoa.Text = cipherText;
                }
            }
            catch { txtMaHoa.Text = ""; }
        }

        private void btnGMaNew_Click(object sender, EventArgs e)
        {
            try
            {
                string cipherText = txtCanGiaiMa.Text.Trim();
                string decryptedText = CryptorEngine.Decrypt(cipherText, true);
                txtGiaiMa.Text = decryptedText;
            }catch { txtGiaiMa.Text = ""; }
        }
        
    }
}