using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace Vs.Category
{
    public partial class frmView : DevExpress.XtraEditors.XtraForm
    {
        string sfind = "-1";  // -1 la view menu <> -1 là view tu menu -- 
        Boolean bView = true;  //true là viwe form, faorm tu form find
        string sPS;
        public Int64 iIDGC = -1;   //dung cho form itw giao hang theo dung don hang gia cong
        // Dữ liệu được chọn
        public frmView(int PQ, string Find, string SP)
        {
            if (Find == "-1")
            { bView = true; sfind = ""; }
            else { bView = false; sfind = Find; }
            InitializeComponent();
            sPS = SP;
        }

        private void frmView_Load(object sender, EventArgs e)
        {
            LoadData(-1);
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, btnALL);
        }

        private void LoadData(Int64 iID)
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sPS, conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;


                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dtTmp = new DataTable();
                dtTmp = ds.Tables[0].Copy();
                dtTmp.TableName = "DataView";

                dtTmp.PrimaryKey = new DataColumn[] { dtTmp.Columns[0] };
                //grdChung.DataSource = dtTmp;
                Commons.Modules.ObjSystems.MLoadXtraGridDM(grdChung, grvChung, dtTmp, false, true, true, true, true);
                grvChung.Columns[0].Visible = false;
                if (iID != -1)
                {
                    int index = dtTmp.Rows.IndexOf(dtTmp.Rows.Find(iID));
                    grvChung.FocusedRowHandle = grvChung.GetRowHandle(index);
                    grvChung.SelectRow(index);
                }
                else
                {
                    grvChung.FocusedRowHandle = 0;
                    grvChung.SelectRow(0);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void grvChung_DoubleClick(object sender, EventArgs e)
        {
            if (grvChung.DataSource == null || grvChung.RowCount <= 0)
            {
                this.Close();
                return;
            }
            if (grvChung.RowCount <= 0)
            {
                this.Close();
                return;
            }
            if (!bView)
            {
                Commons.Modules.sId = grvChung.GetFocusedRowCellValue(grvChung.Columns[0]).ToString();
                this.DialogResult = DialogResult.Yes;
                this.Close();
                return;
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {

                    case "thuchien":
                        {
                            if (grvChung.DataSource == null || grvChung.RowCount <= 0)
                            {
                                this.Close();
                                break;
                            }
                            if (grvChung.RowCount <= 0)
                            {
                                this.Close();
                                break;
                            }
                            if (!bView)
                            {
                                Commons.Modules.sId = grvChung.GetFocusedRowCellValue(grvChung.Columns[0]).ToString();
                                this.DialogResult = DialogResult.Yes;
                                this.Close();
                                break;
                            }
                            break;
                        }
                    case "thoat":
                        {
                            this.Close();
                            break;
                        }
                    default: break;
                }
            }
            catch (Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());
            }
        }
    }
}
