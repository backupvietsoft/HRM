using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Grid;

namespace VietSoftHRM
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm1()
        {
            InitializeComponent();
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
            //CapNhapHinh();

            //VietSoftHRM.ucImportExcel ns = new ucImportExcel();
            //this.Controls.Clear();
            //this.Controls.Add(ns);
            //ns.Dock = DockStyle.Fill;

            //LoadNS();

            //gridView1.ShowFindPanel();
        }

        private void xtraTabPage1_DoubleClick(object sender, EventArgs e)
        {
        }

        private void xtraTabControl1_DoubleClick(object sender, EventArgs e)
        {

        }
        string strDuongDan = "";
        private void LayDuongDan()
        {
            string strPath_DH = txtDescription.Text;
            strDuongDan = ofdChonHinh.FileName;
            var strDuongDanTmp = Commons.Modules.ObjSystems.CapnhatTL("HH");
            string[] sFile;
            string TenFile;
            int i = 1;

            TenFile = "PT_" + ofdChonHinh.SafeFileName.ToString();
            sFile = System.IO.Directory.GetFiles(strDuongDanTmp);

            if (Commons.Modules.ObjSystems.KiemFileTonTai(strDuongDanTmp + @"\" + "MH_" + ofdChonHinh.SafeFileName.ToString()) == false)
                txtDescription.Text = strDuongDanTmp + @"\" + "MH_" + ofdChonHinh.SafeFileName.ToString();
            else
            {
                TenFile = Commons.Modules.ObjSystems.STTFileCungThuMuc(strDuongDanTmp, TenFile);
                txtDescription.Text = strDuongDanTmp + @"\" + TenFile;
            }
        }
        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                ofdChonHinh.ShowDialog();
                LayDuongDan();
                Commons.Modules.ObjSystems.LuuDuongDan(strDuongDan, txtDescription.Text);



            }
            catch
            {
            }
        }
        //private void LoadNS()
        //{
        //    DataTable dtTmp = new DataTable();
        //    string sSql = "";
        //    sSql = " SELECT MS_CN, HO + ' ' + TEN AS HO_TEN FROM dbo.CONG_NHAN  SELECT MS_CN, HO + ' ' + TEN AS HO_TEN, 	HINH_CN, MS_THE_CC, DIA_CHI_THUONG_TRU,NGAY_VAO_CTY, T2.TEN_TT_HD, T3.TEN_TT_HT,NGAY_SINH, T1.DT_DI_DONG + ' - '  + T1.DT_NHA AS SO_DT FROM dbo.CONG_NHAN T1 INNER JOIN dbo.TINH_TRANG_HD T2 ON T1.ID_TT_HD = T2.ID_TT_HD INNER JOIN dbo.TINH_TRANG_HT T3 ON T1.ID_TT_HT = T3.ID_TT_HT ";

        //    SqlConnection con = new SqlConnection(Commons.IConnections.CNStr);


        //    SqlCommand cmd = new SqlCommand(sSql);
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Connection = con;
        //    //Dim da As New SqlDataAdapter(commandcheck)
        //    SqlDataAdapter DA = new SqlDataAdapter(cmd);
        //    DataSet DS = new DataSet();
        //    DA.Fill(DS);


        //    //Dim da As New SqlDataAdapter(commandcheck)
        //    //Dim ds As New DataSet()
        //    //da.Fill(ds)
        //    if(DS.Tables[0].Rows.Count>0)
        //    {
        //        DS.Tables[0].TableName = "A";
        //        DS.Tables[1].TableName = "B";
        //    }
        //    try
        //    {
        //        con.Open();
        //        cmd.ExecuteNonQuery();

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        con.Close();
        //        con.Dispose();
        //    }






        //    gridControl1.DataSource = DS.Tables[0];
        //    gridControl1.ForceInitialize();

        //    //Assign a CardView to the relationship 
        //    CardView cardView1 = new CardView(gridControl1);
        //    gridControl1.LevelTree.Nodes.Add("MS_CN", cardView1);
        //    //Specify text to be displayed within detail tabs. 
        //    cardView1.ViewCaption = "Category Products";

        //    //Hide the CategoryID column for the master View 
        //    //gridView1.Columns["ID_CN"].VisibleIndex = -1;

        //    //Present data in the Picture column as Images 
        //    //Stretch images within cells. 





        //    //Create columns for the detail pattern View 
        //    cardView1.PopulateColumns(DS.Tables[1]);

        //}
        //private void CapNhapHinh()
        //{
        //    DataTable dtTmp = new DataTable();
        //    dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_CN,MS_CN,HINH_CN FROM CONG_NHAN"));
        //    int i = 0;
        //    int J = 0;
        //    for (i = 0; i < dtTmp.Rows.Count; i++)
        //    {
        //        String sHinh = @"D:\HinhNV\" + dtTmp.Rows[i]["MS_CN"] + ".jpg";
        //        if (!File.Exists(sHinh))
        //        {
        //            sHinh = @"D:\HinhNV\25.jpg";
        //        }

        //        if (File.Exists(sHinh))
        //        {
        //            J++;
        //            try
        //            {

        //                Image a = (Image)(new Bitmap(Image.FromFile(sHinh), new Size(150, 200)));
        //                //Image a = (Image)(new Bitmap(Image.FromFile(sHinh), new Size(100, 133)));
        //                //SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "procedure_name", dtTmp.Rows[i]["ID_CN"].ToString(), a);

        //                SqlConnection con = new SqlConnection(Commons.IConnections.CNStr);

        //                byte[] arr;
        //                ImageConverter converter = new ImageConverter();
        //                arr = (byte[])converter.ConvertTo(a, typeof(byte[]));

        //                SqlCommand cmd = new SqlCommand("procedure_name");
        //                cmd.Parameters.AddWithValue("@ID ", dtTmp.Rows[i]["ID_CN"].ToString());
        //                cmd.Parameters.AddWithValue("@Hinh", arr);
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Connection = con;
        //                try
        //                {
        //                    con.Open();
        //                    cmd.ExecuteNonQuery();

        //                }
        //                catch (Exception ex)
        //                {

        //                }
        //                finally
        //                {
        //                    con.Close();
        //                    con.Dispose();
        //                }
        //            }
        //            catch (Exception ex)
        //            {


        //            }




        //        }
        //    }



        //}

    }
}