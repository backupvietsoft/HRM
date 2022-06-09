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
    public partial class frmPhieuCongDoan : DevExpress.XtraEditors.XtraForm
    {
        string sCnstr = "Server=192.168.2.5;database=DATA_MT;uid=sa;pwd=123;Connect Timeout=0;";
        public frmPhieuCongDoan()
        {
            InitializeComponent();
            optXCLP.SelectedIndex = 1;
        }
        string sBT = "PCDTmp" + Commons.Modules.UserName;
        public void XoaTable(string strTableName)
        {
            try
            {
                string strSql = "DROP TABLE " + strTableName;
                SqlHelper.ExecuteScalar(sCnstr, CommandType.Text, strSql);
            }
            catch
            {
            }
        }
        private void LoadCbo()
        {
            try
            {
                string sSql = "SELECT STT_CHUYEN, TEN_CHUYEN FROM CHUYEN UNION SELECT '-1', ' < ALL > ' FROM CHUYEN ORDER BY CHUYEN.TEN_CHUYEN";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(sCnstr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboChuyen, dt, "STT_CHUYEN", "TEN_CHUYEN", "TEN_CHUYEN");
                searchLookUpEdit1View.Columns[0].Caption = "STT Chuyền";
                searchLookUpEdit1View.Columns[1].Caption = "Tên Chuyền";
                searchLookUpEdit1View.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                searchLookUpEdit1View.Columns[1].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                searchLookUpEdit1View.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                searchLookUpEdit1View.Columns[0].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;


                sSql = "SELECT [DON_VI].[MSDV], [DON_VI].[TEN_DON_VI], [DON_VI].[MAC_DINH] FROM DON_VI ORDER BY [MAC_DINH] DESC,[TEN_DON_VI]";
                dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(sCnstr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDV, dt, "MSDV", "TEN_DON_VI", "TEN_DON_VI");

                gridView3.Columns["MAC_DINH"].Visible = false;

                gridView3.Columns[0].Caption = "Đơn vị";
                gridView3.Columns[1].Caption = "Tên Đơn vị";
                gridView3.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridView3.Columns[1].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                gridView3.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridView3.Columns[0].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            }
            catch { }
        }
        private void LoadThang()
        {
            try
            {
                if (Commons.Modules.sPS == "0Load") return;
                Commons.Modules.sPS = "0LoadTo";
                string sSql = "SELECT DISTINCT CONVERT(NVARCHAR(10),[NGAY],103) AS NGAY_THANG,[NGAY] FROM PHIEU_CONG_DOAN ORDER BY [NGAY] DESC";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(sCnstr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboNgay, dt, "NGAY", "NGAY_THANG", "NGAY_THANG");

                cboNgay.Text = dt.Rows[0]["NGAY_THANG"].ToString();


                cboNgay.Properties.Columns["NGAY_THANG"].Caption = "Ngày";
                cboNgay.Properties.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboNgay.Properties.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            }
            catch { }
            Commons.Modules.sPS = "";
        }
        private void LoadTo()
        {
            if (Commons.Modules.sPS == "0Load") return;
            if (Commons.Modules.sPS == "0LoadTo") return;
            try
            {
                string sSql = "";
                DateTime dtNgay;
                try
                {
                    dtNgay = Convert.ToDateTime(cboNgay.EditValue.ToString());
                }
                catch { dtNgay = DateTime.Now; }

                if (optXCLP.SelectedIndex == 0)
                    sSql = "SELECT DISTINCT CONG_NHAN.MS_TO, [TO].TEN_TO FROM [TO] INNER JOIN (PHIEU_CONG_DOAN INNER JOIN CONG_NHAN ON PHIEU_CONG_DOAN.MS_CN = CONG_NHAN.MS_CN) ON [TO].MS_TO = CONG_NHAN.MS_TO WHERE  PHIEU_CONG_DOAN.NGAY = '" + dtNgay.Date.ToString("MM/dd/yyyy") + "' AND [TO].MSDV= '" + cboDV.EditValue + "' ";
                else
                    sSql = " SELECT DISTINCT CONG_NHAN.MS_TO, [TO].TEN_TO FROM [TO] INNER JOIN CONG_NHAN ON [TO].MS_TO = CONG_NHAN.MS_TO WHERE  [TO].MSDV= '" + cboDV.EditValue + "' ";

                sSql = sSql + " UNION SELECT '-1', ' < ALL > ' ORDER BY TEN_TO ";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(sCnstr, CommandType.Text, sSql));

                dt.PrimaryKey = new DataColumn[] { dt.Columns["MS_TO"] };

                //Commons.Modules.ObjSystems.MLoadXtraGrid(grdTo, grvTo, dt, false, false, true, true, true, this.Name);
                //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboT, dt, "MS_TO", "TEN_TO", "TEN_TO");

                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTo, dt, "MS_TO", "TEN_TO", "TEN_TO");
                cboTo.EditValue = -1;


                searchLookUpEdit3View.Columns[0].Caption = "MS tổ";
                searchLookUpEdit3View.Columns[1].Caption = "Tên tổ";
                searchLookUpEdit3View.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                searchLookUpEdit3View.Columns[1].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                searchLookUpEdit3View.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                searchLookUpEdit3View.Columns[0].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;


            }
            catch { }
        }


        private void frmPhieuCongDoan_Load(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";

            try
            {
                Commons.Modules.sPS = "0Load";
                LoadCbo();
                Commons.Modules.sPS = "";
                LoadThang();
                LoadTo();
                LoadPCD();
                LoadCN();
                LoadCD();
                Commons.Modules.ObjSystems.MCreateTableToDatatable(sCnstr, sBT, (DataTable)grdCD.DataSource, "");  //20213103 phong add
            }
            catch { }
        }

        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            #region KhongCo cboTo
            //LoadThang();
            //LoadTo();
            //LoadPCD();
            #endregion
            LoadTo();
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0LoadCN";
            LoadThang();
            LoadPCD();
            Commons.Modules.sPS = "";
            LoadCN();
        }
        private void optXCLP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            if (Commons.Modules.sPS == "0LoadTo") return;
            if (optXCLP.SelectedIndex == 1)
            {
                //cboNgay.Enabled = false;
                //cboNgay.Enabled = false;
                btnTSua.Enabled = true;
            }
            else {
                //cboNgay.Enabled = true;
                //cboNgay.Enabled = true;
                btnTSua.Enabled = false;
                LoadThang();
            }
            LoadTo();
            LoadPCD();
        }

        private void LoadPCD()
        {

            if (Commons.Modules.sPS == "0Load") return;
            if (Commons.Modules.sPS == "0LoadTo") return;
            Commons.Modules.sPS = "0LoadCN";
            try
            {
                DateTime dtNgay;
                try
                {
                    dtNgay = Convert.ToDateTime(cboNgay.EditValue.ToString());
                }
                catch { dtNgay = DateTime.Now; }
                //optXCLP.SelectedIndex = 0  XEM CU
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(sCnstr, "spPCDHDMH", optXCLP.SelectedIndex, cboChuyen.EditValue, dtNgay, Convert.ToDateTime("01/" + dtNgay.Month.ToString() + "/" + dtNgay.Year.ToString())));
                

                dt.PrimaryKey = new DataColumn[] { dt.Columns["KHOALST"] };
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdPCD, grvPCD, dt, false, false, true, true, true, this.Name);

                for (int i = 0; i <= 5; i++)
                {
                    grvPCD.Columns[i].Visible = false;
                }

                grvPCD.Columns["SL_CHOT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                grvPCD.Columns["SL_CHOT"].DisplayFormat.FormatString = Commons.Modules.sSoLeSL;

            }
            catch { }
            Commons.Modules.sPS = "";
        }
        private void LoadCD()
        {

            if (Commons.Modules.sPS == "0Load") return;
            if (Commons.Modules.sPS == "0LoadTo") return;
            if (Commons.Modules.sPS == "0LoadCD") return;
            try
            {
                String sChu, sDDH, sMH, sOrd, sDV, sTo, sCNhan;

                sChu = ""; sDDH = ""; sMH = ""; sOrd = ""; sDV = ""; sTo = ""; sCNhan = "";

                try
                {
                    sChu = cboChuyen.EditValue.ToString();
                    sDDH = grvPCD.GetFocusedRowCellValue("MS_DDH").ToString();
                    sMH = grvPCD.GetFocusedRowCellValue("MS_MH").ToString();
                    sOrd = grvPCD.GetFocusedRowCellValue("ORDER").ToString();
                    sDV = cboDV.EditValue.ToString();
                    //sTo = grvTo.GetFocusedRowCellValue("MS_TO").ToString();
                    //sCNhan = cboCN.EditValue.ToString();
                    sTo = cboTo.EditValue.ToString();
                    sCNhan = grvTo.GetFocusedRowCellValue("MS_CN").ToString();
                }
                catch { }

                DateTime dtNgay;
                try
                {
                    dtNgay = Convert.ToDateTime(cboNgay.EditValue.ToString());
                }
                catch { dtNgay = DateTime.Now; }


                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(sCnstr, "spPCDGetCDoan", sChu, sDDH, sMH, sOrd, dtNgay, sDV, sTo, sCNhan));

                if (btnGhi.Visible)
                {
                    string sBTCD = "CDTmp" + Commons.Modules.UserName;
                    XoaTable(sBTCD);
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(sCnstr, sBTCD, dt, "");
                    string sSql = "INSERT INTO " + sBT + " (MS_CD,TEN_CD,SO_LUONG,STT_CHUYEN,CHUYEN_SD,MS_DDH,MS_MH,[ORDER],NGAY,MS_CN,MAQL,THU_TU_CONG_DOAN)SELECT MS_CD,TEN_CD,SO_LUONG,STT_CHUYEN,CHUYEN_SD,MS_DDH,MS_MH,[ORDER],NGAY,MS_CN,MAQL,THU_TU_CONG_DOAN FROM " + sBTCD + " T1 WHERE NOT EXISTS (SELECT * FROM PCDTmpadmin T2 WHERE T1.MS_DDH = T2.MS_DDH AND T1.MS_MH = T2.MS_MH AND T1.[ORDER] = T2.[ORDER] AND T1.NGAY = T2.NGAY) ORDER BY THU_TU_CONG_DOAN";
                    SqlHelper.ExecuteNonQuery(sCnstr, CommandType.Text, sSql);

                    sSql = "SELECT DISTINCT MS_CD,TEN_CD, SO_LUONG, STT_CHUYEN, CHUYEN_SD, MS_DDH, MS_MH, [ORDER], NGAY, MS_CN, MaQL, THU_TU_CONG_DOAN FROM " + sBT + " WHERE (MS_DDH = '" + sDDH + "') AND (MS_MH = '" + sMH + "') AND ([ORDER] = '" + sOrd + "') AND (MS_CN = '" + sCNhan + "') AND (NGAY = '" + dtNgay.ToString("MM/dd/yyyy") + "' )  ORDER BY THU_TU_CONG_DOAN";
                    dt = new DataTable();
                    dt.Load(SqlHelper.ExecuteReader(sCnstr, CommandType.Text, sSql));
                    dt.Columns[0].ReadOnly = true;
                    dt.Columns[1].ReadOnly = true;
                    dt.Columns[2].ReadOnly = false;
                    XoaTable(sBTCD);
                }
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdCD, grvCD, dt, btnGhi.Visible, false, true, true, true, this.Name);
                for (int i = 3; i <= grvCD.Columns.Count - 1; i++)
                {
                    grvCD.Columns[i].Visible = false;
                }
            }
            catch (Exception EX)
            { }
           
        }

        private void LoadCN()
        {
            if (Commons.Modules.sPS == "0Load") return;
            if (Commons.Modules.sPS == "0LoadTo") return;
            if (Commons.Modules.sPS == "0LoadCN") return;

           try
            {
                String sChu, sDDH, sMH, sOrd,  sDV, sTo;

                if (grvPCD.DataSource == null)
                { sChu = ""; sDDH = ""; sMH = ""; sOrd = ""; sDV = ""; sTo = ""; }
                else
                {
                    sChu = cboChuyen.EditValue.ToString();
                    sDDH = grvPCD.GetFocusedRowCellValue("MS_DDH").ToString();
                    sMH = grvPCD.GetFocusedRowCellValue("MS_MH").ToString();
                    sOrd = grvPCD.GetFocusedRowCellValue("ORDER").ToString();
                    sDV = cboDV.EditValue.ToString();
                    //sTo = grvTo.GetFocusedRowCellValue("MS_TO").ToString();
                    sTo = cboTo.EditValue.ToString();
                }
                DateTime dtNgay;
                try
                {
                    dtNgay = Convert.ToDateTime(cboNgay.EditValue.ToString());
                }
                catch { dtNgay = DateTime.Now; }

                //optXCLP.SelectedIndex = 0  XEM CU

                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(sCnstr, "spPCDGetCNhan", optXCLP.SelectedIndex, sChu, sDDH, sMH, sOrd, dtNgay, sDV, sTo));
                //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboCN, dt, "MS_CN", "LMS", "LMS");
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdTo, grvTo, dt, false, false, true, true, true, this.Name);
                grvTo.Columns["LMS"].Visible = false;
            }
            catch { }
        }


        private void cboChuyen_EditValueChanged(object sender, EventArgs e)
        {
            LoadPCD();
        }

        private void cboNgay_EditValueChanged(object sender, EventArgs e)
        {
            //LoadTo();
            LoadPCD();
            //grvPCD_FocusedRowChanged(null, null);
        }

        private void grvPCD_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            grvCD.UpdateCurrentRow();
            Commons.Modules.sPS = "0LoadCD";
            if (!btnGhi.Visible )LoadCN();
            Commons.Modules.sPS = "";
            LoadCD();
        }

        private void grvTo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadCD();
        }
        private void btnKhong_Click(object sender, EventArgs e)
        {
            TSua(false);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnTSua_Click(object sender, EventArgs e)
        {
            TSua(true);
            XoaTable(sBT);
            Commons.Modules.ObjSystems.MCreateTableToDatatable(sCnstr, sBT, (DataTable) grdCD.DataSource, "");            
            string sSql = "SELECT MS_CD,TEN_CD, SO_LUONG, STT_CHUYEN, CHUYEN_SD, MS_DDH, MS_MH, [ORDER], NGAY, MS_CN, MaQL, THU_TU_CONG_DOAN FROM " + sBT + " ORDER BY THU_TU_CONG_DOAN";      
            DataTable dt = new DataTable();            
            dt.Load(SqlHelper.ExecuteReader(sCnstr, CommandType.Text, sSql));
            Commons.Modules.ObjSystems.MLoadXtraGrid(grdCD, grvCD, dt, true, false, true, true, true, this.Name);
        }

        
        private void TSua(Boolean TSua)
        {   
            btnChonCD.Visible = TSua;
            btnGhi.Visible = TSua;
            btnKhong.Visible = TSua;
            btnTSua.Visible = !TSua;
            btnThoat.Visible = !TSua;
            btnMH.Visible = !TSua;
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {

            try
            {
                string sSql = "UPDATE PHIEU_CONG_DOAN SET SO_LUONG = T2.SO_LUONG FROM   dbo.PHIEU_CONG_DOAN AS T1 INNER JOIN dbo.PCDTmpadmin AS T2 ON T1.MS_CD = T2.MS_CD AND T1.STT_CHUYEN = T2.STT_CHUYEN AND T1.CHUYEN_SD = T2.CHUYEN_SD AND T1.MS_DDH = T2.MS_DDH AND T1.MS_MH = T2.MS_MH AND T1.[ORDER] = T2.[ORDER] AND T1.NGAY = T2.NGAY AND T1.MS_CN = T2.MS_CN INSERT INTO PHIEU_CONG_DOAN(STT_CHUYEN, CHUYEN_SD, MS_DDH, MS_MH, [ORDER], MS_CD, NGAY, MS_CN, SO_LUONG) SELECT STT_CHUYEN, CHUYEN_SD, MS_DDH, MS_MH, [ORDER], MS_CD, NGAY, MS_CN, SO_LUONG FROM PCDTmpadmin T2 WHERE NOT EXISTS (SELECT * FROM PHIEU_CONG_DOAN T1 WHERE  T1.MS_CD = T2.MS_CD AND T1.STT_CHUYEN = T2.STT_CHUYEN AND T1.CHUYEN_SD = T2.CHUYEN_SD AND T1.MS_DDH = T2.MS_DDH AND T1.MS_MH = T2.MS_MH AND T1.[ORDER] = T2.[ORDER] AND T1.NGAY = T2.NGAY AND T1.MS_CN = T2.MS_CN) AND (ISNULL(T2.SO_LUONG,0) >0)";
                SqlHelper.ExecuteNonQuery(sCnstr, CommandType.Text, sSql);

                
            }
            catch { }
            TSua(false);
            LoadCD();
            XoaTable(sBT);
        }

        private void btnChonCD_Click(object sender, EventArgs e)
        {
            LoadThemCD();
        }
        private void LoadThemCD()
        {

            if (Commons.Modules.sPS == "0Load") return;
            if (Commons.Modules.sPS == "0LoadTo") return;
            if (Commons.Modules.sPS == "0LoadCD") return;
            try
            {
                String sSql, sChu, sDDH, sMH, sOrd,sChSD;
                string sCN;
                 sChu = ""; sDDH = ""; sMH = ""; sOrd = ""; sChSD = "";sCN = "";
                try
                {
                    sChu = cboChuyen.EditValue.ToString();
                    sDDH = grvPCD.GetFocusedRowCellValue("MS_DDH").ToString();
                    sMH = grvPCD.GetFocusedRowCellValue("MS_MH").ToString();
                    sOrd = grvPCD.GetFocusedRowCellValue("ORDER").ToString();
                    sChSD = grvPCD.GetFocusedRowCellValue("CHUYEN_SD").ToString();
                    sCN = grvTo.GetFocusedRowCellValue("MS_CN").ToString();
                }
                catch { }

               

                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(sCnstr, "spPCDGetThemCDoan", sChu, sDDH, sMH, sOrd,sBT));
                dt.Columns[0].ReadOnly = false;
                dt.Columns[1].ReadOnly = true;
                dt.Columns[2].ReadOnly = true;
                dt.Columns[3].ReadOnly = true;

                frmPCDChonCD fr = new frmPCDChonCD();
                fr.dtPCD = dt;
                
                if (fr.ShowDialog() != DialogResult.OK) return;
                string sBTCD = "CDTmp" + Commons.Modules.UserName;
                try
                {
                    sSql = "DROP TABLE " + sBTCD;
                    SqlHelper.ExecuteScalar(sCnstr, CommandType.Text, sSql);
                }
                catch { }
                XoaTable(sBTCD);
                Commons.Modules.ObjSystems.MCreateTableToDatatable(sCnstr, sBTCD, dt, "");
                DateTime dtNgay;
                try
                {
                    dtNgay = Convert.ToDateTime(cboNgay.EditValue.ToString());
                }
                catch { dtNgay = DateTime.Now; }
                string sCNhan = "";
                sCNhan = grvTo.GetFocusedRowCellValue("MS_CN").ToString();

                sSql = "INSERT INTO " + sBT + " (MS_CD,TEN_CD,SO_LUONG,STT_CHUYEN,CHUYEN_SD,MS_DDH,MS_MH,[ORDER],NGAY,MS_CN,MAQL,THU_TU_CONG_DOAN)SELECT MS_CD,TEN_CD, CONVERT(FLOAT,0)  AS SO_LUONG,STT_CHUYEN,N'" + sChSD + "' AS CHUYEN_SD,MS_DDH,MS_MH,[ORDER], '" + dtNgay.ToString("MM/dd/yyyy") + "'  NGAY, '" + sCN + "' AS MS_CN,MAQL,THU_TU_CONG_DOAN FROM " + sBTCD + " WHERE CHON = 1 ORDER BY THU_TU_CONG_DOAN";
                SqlHelper.ExecuteNonQuery(sCnstr, CommandType.Text, sSql);

                sSql = "SELECT DISTINCT MS_CD,TEN_CD, SO_LUONG, STT_CHUYEN, CHUYEN_SD, MS_DDH, MS_MH, [ORDER], NGAY, MS_CN, MaQL, THU_TU_CONG_DOAN FROM " + sBT + " WHERE (MS_DDH = '" + sDDH + "') AND (MS_MH = '" + sMH + "') AND ([ORDER] = '" + sOrd + "') AND (MS_CN = '" + sCNhan + "') AND (NGAY = '" + dtNgay.ToString("MM/dd/yyyy") + "' )  ORDER BY THU_TU_CONG_DOAN";
                dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(sCnstr, CommandType.Text, sSql));

                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                dt.Columns[2].ReadOnly = false;

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdCD, grvCD, dt, true, false, true, true, true, this.Name);
                XoaTable(sBTCD);
            }
            catch 
            { }
        }

        private void grvCD_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            grdCD.FocusedView.PostEditor();
            grdCD.FocusedView.UpdateCurrentRow();
            grvCD.UpdateCurrentRow();

           

        }

        private void grdCD_Validated(object sender, EventArgs e)
        {
            string sBTCD = "sCD" + Commons.Modules.UserName;
            XoaTable(sBTCD);
            Commons.Modules.ObjSystems.MCreateTableToDatatable(sCnstr, sBTCD, (DataTable)grdCD.DataSource, "");

            string sSql = "UPDATE " +  sBT + " SET SO_LUONG = T2.SO_LUONG FROM " + sBT + " T1 INNER JOIN " + sBTCD + " T2 ON T1.CHUYEN_SD = T2.CHUYEN_SD AND T1.STT_CHUYEN = T2.STT_CHUYEN AND T1.MS_CD = T2.MS_CD AND T1.MS_DDH = T2.MS_DDH AND T1.MS_MH = T2.MS_MH AND T1.[ORDER] = T2.[ORDER] AND T1.NGAY = T2.NGAY  ";

            SqlHelper.ExecuteNonQuery(sCnstr, CommandType.Text, sSql);
            LoadCD();
            XoaTable(sBTCD);
        }


        private void LoadLuoiCD()
        {

        }

        private void btnMH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboNgay.Text)) {
                XtraMessageBox.Show("Bạn chưa chọn ngày");
                return;
            }
            frmPCDHDMHChot frm = new frmPCDHDMHChot();
            DateTime dThang = Convert.ToDateTime(cboNgay.EditValue);

            frm.dThang = Convert.ToDateTime("01/"+dThang.Month+"/"+dThang.Year);
            frm.ShowDialog();
            cboNgay_EditValueChanged(null, null);
        }
    }
}