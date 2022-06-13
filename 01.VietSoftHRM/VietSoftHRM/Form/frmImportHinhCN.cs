using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VietSoftHRM
{
    public partial class frmImportHinhCN : DevExpress.XtraEditors.XtraForm
    {
        static int iPQ = -1; // 1 - FULL, 2 - READONLY
        private int ID = 1; // Mặc định là 1
        public frmImportHinhCN(int PQ)
        {
            iPQ = PQ;
            InitializeComponent();
        }

        private void frmImportHinhCN_Load(object sender, EventArgs e)
        {
            LoadData();
            //LoadNN();
            //VsMain.MFieldRequest(lblFont);

        }

        private byte[] imgToByteConverter(Image inImg)
        {

            ImageConverter imgCon = new ImageConverter();
            byte[] imgConvert = (byte[])imgCon.ConvertTo(inImg, typeof(byte[]));
            byte[] currentByteImageArray = imgConvert;
            double scale = 1f;
            try
            {
                System.IO.MemoryStream inputMemoryStream = new System.IO.MemoryStream(imgConvert);
                Image fullsizeImage = Image.FromStream(inputMemoryStream);
                while (currentByteImageArray.Length > 20000)
                {
                    Bitmap fullSizeBitmap = new Bitmap(fullsizeImage, new Size((int)(fullsizeImage.Width * scale), (int)(fullsizeImage.Height * scale)));
                    System.IO.MemoryStream resultStream = new System.IO.MemoryStream();

                    fullSizeBitmap.Save(resultStream, fullsizeImage.RawFormat);

                    currentByteImageArray = resultStream.ToArray();
                    resultStream.Dispose();
                    resultStream.Close();

                    scale -= 0.05f;
                }
            }
            catch
            {

            }

            return currentByteImageArray;
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spUpdateImageCN", conn);
                cmd.Parameters.Add("@HINH_CN", SqlDbType.Image).Value = imgToByteConverter(pteLogo.Image);
                cmd.Parameters.Add("@ID_CN", SqlDbType.BigInt).Value = Convert.ToInt64(grvCN.GetFocusedRowCellValue("ID_CN"));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                if (conn.State == ConnectionState.Open)
                    conn.Close();

                MessageBox.Show("GhiThanhCong");
                pteLogo.EditValue = null;
                LoadData();
                searchControl1.Focus();
                //Program.MBarCapNhapThanhCong();
                //VsMain.LoadThongTinChung();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgGhiKhongThanhCong") + "\n" + ex.Message);
                //Program.MBarCapNhapKhongThanhCong();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnChonLogo_Click(object sender, EventArgs e)
        {
            this.pteLogo.LoadImage();
        }

        #region Event
        #endregion

        #region Function
        private void LoadNN()
        {
            //var typeToBeSelected = new List<Type>
            //    {typeof(DevExpress.XtraDataLayout.DataLayoutControl)};
            //IEnumerable<Control> allCon;
            //allCon = Commons.Mod.OS.GetAllConTrol(this, typeToBeSelected);
            //Commons.Mod.OS.ThayDoiNN(this, allCon);

            //gcLogo.Text = Commons.Mod.OS.GetLanguage(this.Name, "gcLogo");
            //gcSoLe.Text = Commons.Mod.OS.GetLanguage(this.Name, "gcSoLe");
        }

        private void LoadData()
        {
            DataTable dt = new DataTable();

            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetCongNhan_image", conn);
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            cmd.Parameters.Add("@CoAll", SqlDbType.Bit).Value = 0;

            cmd.CommandType = CommandType.StoredProcedure;
            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0].Copy();
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdCN, grvCN, dt, false, false, true, true, true, this.Name);
            grvCN.Columns["HINH_CN"].Visible = false;
        }


        #endregion

        private void pteLogo_DoubleClick(object sender, EventArgs e)
        {
            try { pteLogo.ShowImageEditorDialog(); }
            catch { }
        }

        private void grvCN_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                Byte[] data = new Byte[0];
                data = (Byte[])(grvCN.GetFocusedRowCellValue("HINH_CN"));
                System.IO.MemoryStream mem = new System.IO.MemoryStream(data);
                pteLogo.EditValue = Image.FromStream(mem);
                //pteLogo.EditValue = Commons.Modules.OS.LoadHinh((byte[])dt.Rows[0]["LOGO"]);
            }
            catch
            {
                pteLogo.EditValue = null;
            }
        }
    }
}
