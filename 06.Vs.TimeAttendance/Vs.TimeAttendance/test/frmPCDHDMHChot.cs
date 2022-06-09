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

namespace VietSoftHRM
{
    public partial class frmPCDHDMHChot : DevExpress.XtraEditors.XtraForm
    {
        string sCnstr = "Server=192.168.2.5;database=DATA_MT;uid=sa;pwd=123;Connect Timeout=0;";
        public DateTime dThang = Convert.ToDateTime("2014-02-01");
        public frmPCDHDMHChot()
        {
            InitializeComponent();
        }

        private void frmPCDHDMHChot_Load(object sender, EventArgs e)
        {
            lblTD.Text = lblTD.Text + dThang.ToString("MM/yyyy");
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(sCnstr, CommandType.Text, "	SELECT DISTINCT	 [CHUYEN].[STT_CHUYEN], [CHUYEN].[TEN_CHUYEN] FROM dbo.CHUYEN	UNION SELECT	'-1',  ' < ALL > ' ORDER BY STT_CHUYEN, [TEN_CHUYEN]"));

            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboChuTH, dt, "STT_CHUYEN", "TEN_CHUYEN", "TEN_CHUYEN");

            LoadLuoi();

            LoadHD(0);


            Commons.Modules.sPS = "";
        }
        private void LoadLuoi()
        {
            Commons.Modules.sPS = "0Load" ;
            String  sChuSD, sDDH, sMH, sOrd, sChuTH;
            sChuSD = "-1"; sDDH = "-1"; sMH = "-1"; sOrd = "-1"; sChuTH = "-1"; 

            try{sChuSD = cboChuSD.EditValue.ToString();}catch { }
            try{sChuTH = cboChuTH.EditValue.ToString();}catch { }
            try{sDDH = cboHD.EditValue.ToString();}catch { }
            try{sMH = cboMH.EditValue.ToString();}catch { }
            try{sOrd = cboOrd.EditValue.ToString();}catch { }
            
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(sCnstr, "spPCDChotGet", optHT.SelectedIndex, sDDH, sMH, sOrd, sChuSD, sChuTH, dThang));
            for (int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                dt.Columns[i].ReadOnly = true;
            }
            dt.Columns["STT_CHUYEN"].ReadOnly = false;
            dt.Columns["SL_CHOT"].ReadOnly = false;
            dt.Columns["BU_THANG_TRUOC"].ReadOnly = false;
            dt.Columns["PHAT_SINH_CD_BB"].ReadOnly = false;
            dt.Columns["CHON"].ReadOnly = false;

            Commons.Modules.ObjSystems.MLoadXtraGrid(grdHD, grvHD, dt, true, false, true, true, true, this.Name);
            Commons.Modules.ObjSystems.AddCombXtra("STT_CHUYEN", "TEN_CHUYEN", grvHD, ((DataTable) cboChuTH.Properties.DataSource).Copy());


            for (int i = 9; i <= grvHD.Columns.Count - 1; i++)
            {
                grvHD.Columns[i].Visible = false;
            }

            grvHD.Columns["SL_CHOT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            grvHD.Columns["SL_CHOT"].DisplayFormat.FormatString = Commons.Modules.sSoLeSL;

            grvHD.Columns["BU_THANG_TRUOC"].Width = 50;
            grvHD.Columns["PHAT_SINH_CD_BB"].Width = 50;
            grvHD.Columns["CHON"].Width = 50;

        }

        private void cboHD_EditValueChanged(object sender, EventArgs e)
        {

        }
        private void LoadHD(int iLoad)
        {
            Commons.Modules.sPS = "0LoadCbo";
            String sChuSD, sDDH, sMH, sOrd;
            sChuSD = "-1"; sDDH = "-1"; sMH = "-1"; sOrd = "-1"; 

            try { sChuSD = cboChuSD.EditValue.ToString(); } catch { }
            try { sDDH = cboHD.EditValue.ToString(); } catch { }
            try { sMH = cboMH.EditValue.ToString(); } catch { }
            try { sOrd = cboOrd.EditValue.ToString(); } catch { }

            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(sCnstr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spPCDChotGetCbo", conn);
                
                cmd.Parameters.Add("@HoanThanh", SqlDbType.Int).Value = optHT.SelectedIndex;
                cmd.Parameters.Add("@sDDH", SqlDbType.NVarChar, 50).Value = sDDH;
                cmd.Parameters.Add("@sMH", SqlDbType.NVarChar, 50).Value = sMH;
                cmd.Parameters.Add("@sOrd", SqlDbType.NVarChar, 50).Value = sOrd;
                cmd.Parameters.Add("@sChuSD", SqlDbType.NVarChar, 50).Value = sChuSD;
                cmd.Parameters.Add("@dThang", SqlDbType.DateTime, 50).Value = dThang;

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);

                
                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "HOP_DONG";
                if (iLoad == 0 ) Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboHD, dt, "MS_DDH", "TEN_HD", "TEN_HD");
                

                dt = new DataTable();
                dt = ds.Tables[1].Copy();
                dt.TableName = "MA_HANG";
                if (iLoad == 0 || iLoad ==1 ) Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboMH, dt, "MS_MH", "MS_MHK", "MS_MHK");


                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[2].Copy();
                dt1.TableName = "TEN_ORDER";
                if (iLoad == 0 || iLoad == 1 || iLoad == 2 ) Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboOrd, dt1, "ORDER", "TEN_ORDER", "TEN_ORDER");

                

                dt = new DataTable();
                dt = ds.Tables[3].Copy();
                dt.TableName = "CHUYEN_SD";
                if (iLoad == 0 || iLoad == 1 || iLoad == 2 || iLoad == 3) Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboChuSD, dt, "CHUYEN_SD", "TENCHUYENSD", "TENCHUYENSD");
                


            }
            catch
            { }
            
        }

        private void optHT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0LoadCbo") return;
            LoadLuoi();
            LoadHD(0);
            Commons.Modules.sPS = "";
            LocData();
        }

        private void cboHD_EditValueChanged_1(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0LoadCbo") return;
            LoadHD(1);
            Commons.Modules.sPS = "";
            LocData();
        }

        private void cboMH_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0LoadCbo") return;
            LoadHD(2);
            Commons.Modules.sPS = "";
            LocData();
        }

        private void cboOrd_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0LoadCbo") return;
            LoadHD(3);
            Commons.Modules.sPS = "";
            LocData();
        }

        private void cboChuSD_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0LoadCbo") return;
            LoadHD(4);
            Commons.Modules.sPS = "";
            LocData();
        }
        
        private void LocData()
        {
            if (Commons.Modules.sPS == "0LoadCbo") return;
            DataTable dtTmp = new DataTable();
            try
            {
                dtTmp = (DataTable)grdHD.DataSource;
                String sChuTH, sDDH, sMH, sOrd, sChuSD;
                string sDK = " 1 = 1 ";
                sChuTH = "-1"; sDDH = "-1"; sMH = "-1"; sOrd = "-1"; sChuSD = "-1";
                try { sChuSD = cboChuSD.EditValue.ToString(); } catch { }
                try { sChuTH = cboChuTH.EditValue.ToString(); } catch { }
                try { sDDH = cboHD.EditValue.ToString(); } catch { }
                try { sMH = cboMH.EditValue.ToString(); } catch { }
                try { sOrd = cboOrd.EditValue.ToString(); } catch { }

                if (sDDH != "-1") sDK = sDK + " AND MS_DDH = '" + sDDH + "' ";
                if (sMH != "-1") sDK = sDK + " AND MS_MH = '" + sMH + "' ";
                if (sOrd != "-1") sDK = sDK + " AND ORDER = '" + sOrd + "' ";
                if (sChuSD != "-1") sDK = sDK + " AND CHUYEN_SD = '" + sChuSD + "' ";
                if (sChuTH != "-1") sDK = sDK + " AND STT_CHUYEN = N'" + sChuTH + "' ";

                dtTmp.DefaultView.RowFilter = sDK;
            }
            catch { dtTmp.DefaultView.RowFilter = ""; }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboHD.EditValue.ToString() == "-1" || string.IsNullOrEmpty(cboChuTH.Text)) { XtraMessageBox.Show("Bạn chưa chọn hợp đồng. Vui lòng kiểm tra lại"); cboHD.Focus(); return; }
                if (cboMH.EditValue.ToString() == "-1" || string.IsNullOrEmpty(cboMH.Text)) { XtraMessageBox.Show("Bạn chưa chọn mã hàng. Vui lòng kiểm tra lại"); cboMH.Focus(); return; }
                if (cboOrd.EditValue.ToString() == "-1" || string.IsNullOrEmpty(cboOrd.Text)) { XtraMessageBox.Show("Bạn chưa chọn order. Vui lòng kiểm tra lại"); cboOrd.Focus(); return; }
                if (cboChuSD.EditValue.ToString() == "-1" || string.IsNullOrEmpty(cboChuSD.Text)) { XtraMessageBox.Show("Bạn chưa chọn chuyền sữ dụng QTCN. Vui lòng kiểm tra lại"); cboChuSD.Focus(); return; }
                if (cboChuTH.EditValue.ToString() == "-1" || string.IsNullOrEmpty(cboChuTH.Text)) { XtraMessageBox.Show("Bạn chưa chọn chuyền thực hiện. Vui lòng kiểm tra lại"); cboChuTH.Focus(); return; }

                String sChuTH, sDDH, sMH, sOrd, sChuSD;
                sChuTH = "-1"; sDDH = "-1"; sMH = "-1"; sOrd = "-1"; sChuSD = "-1";

                try { sDDH = cboHD.EditValue.ToString(); } catch { }
                try { sMH = cboMH.EditValue.ToString(); } catch { }
                try { sOrd = cboOrd.EditValue.ToString(); } catch { }
                try { sChuSD = cboChuSD.EditValue.ToString(); } catch { }
                try { sChuTH = cboChuTH.EditValue.ToString(); } catch { }

                String sSql = "SELECT ISNULL(SUM(T1.SO_LUONG - ISNULL(T1.SL_GIAM, 0)), 0) AS TSL FROM dbo.CHI_TIET_ORDER AS T1 WHERE (MS_DDH = N'" + sDDH + "') AND (MS_MH = N'" + sMH + "') AND ([ORDER] = N'" + sOrd + "')";

                double SLDH = 0;
                double SLDaChot = 0;
                double SLCHOT = 0;

                try { SLDH = Convert.ToDouble(SqlHelper.ExecuteScalar(sCnstr, CommandType.Text, sSql)); } catch { }
                sSql = "SELECT ISNULL(SUM(SL_CHOT),0) FROM PHIEU_CONG_DOAN_CHOT_THANG  WHERE (MS_DDH = N'" + sDDH + "') AND (MS_MH = N'" + sMH + "') AND ([ORDER] = N'" + sOrd + "')";

                try { SLDaChot = Convert.ToDouble(SqlHelper.ExecuteScalar(sCnstr, CommandType.Text, sSql)); } catch { }

                if ((SLDH - SLDaChot) > 0)
                {
                    SLCHOT = SLDH - SLDaChot;
                }
                else { XtraMessageBox.Show("Order này đã được phân bổ hết cho các chuyền.\n Vui lòng chọn Order khác"); return; }

                sSql = "INSERT INTO PHIEU_CONG_DOAN_CHOT_THANG (THANG, STT_CHUYEN, MS_DDH, MS_MH, [ORDER], CHUYEN_SD, SL_CHOT, BU_THANG_TRUOC, PHAT_SINH_CD_BB, CHON)  SELECT '" + dThang.ToString("MM/dd/yyyy") + "',  '" + sChuSD + "', '" + sDDH + "', '" + sMH + "', '" + sOrd + "', '" + sChuTH + "', " + SLCHOT + ", 0, 0, 0";
                SqlHelper.ExecuteNonQuery(sCnstr, CommandType.Text, sSql);
            }
            catch { }

            try { optHT_SelectedIndexChanged(null, null); } catch { }
        }

        private void cboChuTH_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {

            
            DataTable dtTmp = new DataTable();
            grvHD.PostEditor();
            grvHD.UpdateCurrentRow();
            try {
                dtTmp = (DataTable)grdHD.DataSource;
                DataTable dt = dtTmp.GetChanges();
                string sBTCD = "CDChotTmp" + Commons.Modules.UserName;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(sCnstr, sBTCD, dt, "");

                string sSql ="UPDATE PHIEU_CONG_DOAN_CHOT_THANG SET SL_CHOT = T2.SL_CHOT, BU_THANG_TRUOC = T2.BU_THANG_TRUOC, PHAT_SINH_CD_BB = T2.PHAT_SINH_CD_BB, CHON = T2.CHON FROM                  PHIEU_CONG_DOAN_CHOT_THANG T1 INNER JOIN " + sBTCD + " T2 ON T2.MS_DDH = T1.MS_DDH AND T2.MS_MH = T1.MS_MH AND T2.[ORDER] = T1.[ORDER] AND T2.CHUYEN_SD = T1.CHUYEN_SD AND T2.STT_CHUYEN = T1.STT_CHUYEN ";
                SqlHelper.ExecuteNonQuery(sCnstr, CommandType.Text, sSql);

            } catch { }
            this.Close();
        }

        private void grvHD_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            grdHD.FocusedView.PostEditor();
            grdHD.FocusedView.UpdateCurrentRow();
            grvHD.UpdateCurrentRow();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //string sSql = "UPDATE PHIEU_CONG_DOAN_CHOT SET PHIEU_CONG_DOAN_CHOT.CHON = 1 WHERE THANG = '" + dThang.ToString("MM/dd/yyyy") + "' ";
            //SqlHelper.ExecuteNonQuery(sCnstr, CommandType.Text, sSql);
            //For i As Integer = 0 To gvPhone.RowCount - 1
            //    gvPhone.SetRowCellValue(i, gcPrimary, False)
            //Next

            for (int i = 0; i <= grvHD.RowCount - 1; i++)
            {
                grvHD.SetRowCellValue(i, "CHON", 1);
            }

        }

        private void btnKChon_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= grvHD.RowCount - 1; i++)
            {
                grvHD.SetRowCellValue(i, "CHON",0);
            }
        }
    }
}