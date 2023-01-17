using System;
using System.Data;
using System.Drawing;
using System.Linq;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraBars.Docking2010;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.XtraPrinting.Export;
using DevExpress.Utils.Menu;
using static NPOI.HSSF.Util.HSSFColor;

namespace Vs.Payroll
{
    public partial class frmPhieuCongDoan : DevExpress.XtraEditors.XtraUserControl
    {
        int iChuyen = -1;
        int iChuyenSuDung = -1;
        int iOrd = -1;
        int iCN = -1;
        int XemCu = 0;
        DataTable dtMQL = new DataTable();
        //private LookUpEdit lookUp;

        private DataTable dtCD;
        public static frmPhieuCongDoan _instance;
        public static frmPhieuCongDoan Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new frmPhieuCongDoan();
                return _instance;
            }
        }

        public Int64 iIDPCD_TEMP = -1;

        RepositoryItemLookUpEdit cboMQL;
        public frmPhieuCongDoan()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, windowsUIButton);
        }
        string sBT = "PCDTmp" + Commons.Modules.iIDUser;
        CultureInfo cultures = new CultureInfo("en-US");

        private void frmPhieuCongDoan_Load(object sender, EventArgs e)
        {
            try
            {
                chkKT.Checked = Commons.Modules.bKiemPCD;
                Commons.Modules.sLoad = "0Load";
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDV, Commons.Modules.ObjSystems.DataDonVi(false), "ID_DV", "TEN_DV", "TEN_DV");
                LoadCboXN();
                LoadCboTo();

                LoadThang();
                LoadChuyen();
                LoadPCD();
                LoadCN();
                LoadCD();
                //LoadCboMSCN();
                Commons.Modules.sLoad = "";
                grvPCD_FocusedRowChanged(null, null);
                grvTo_FocusedRowChanged(null, null);
                TSua(false);
                //cboMSCN.Properties.Items[2].Description = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "rdoDaDong");
                //grvCD.Columns["ID_CD"].ColumnEdit = cboMQL;
                //cboMQL.EditValueChanged += CboMQL_EditValueChanged;
                //Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, (DataTable)grdCD.DataSource, "");  //20213103 phong add
            }
            catch { }
        }



        public void XoaTable(string strTableName)
        {
            try
            {
                string strSql = "DROP TABLE " + strTableName;
                SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSql);
            }
            catch
            {
            }
        }

        private void LoadChuyen()
        {
            try
            {
                string sSql = "SELECT [TO].ID_TO, [TO].TEN_TO FROM dbo.[TO] INNER JOIN dbo.XI_NGHIEP XN ON XN.ID_XN = [TO].ID_XN WHERE [TO].ID_LOAI_CHUYEN IN (1,2,3,4,5,6,7) AND (XN.ID_DV = " + cboDV.EditValue + " OR " + cboDV.EditValue + " = -1) ORDER BY [TO].STT_TO";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboChuyen, dt, "ID_TO", "TEN_TO", "TEN_TO");
                searchLookUpEdit1View.Columns[0].Caption = "STT Chuyền";
                searchLookUpEdit1View.Columns[1].Caption = "Tên Chuyền";
                searchLookUpEdit1View.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                searchLookUpEdit1View.Columns[1].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                searchLookUpEdit1View.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                searchLookUpEdit1View.Columns[0].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

            }
            catch { }
        }

        private void LoadThang()
        {

            try
            {
                string sSql = "SELECT DISTINCT CONVERT(NVARCHAR(10),[NGAY],103) AS NGAY_THANG,[NGAY] FROM PHIEU_CONG_DOAN PCD INNER JOIN dbo.DON_HANG_BAN_ORDER DHBORD ON DHBORD.ID_ORD = PCD.ID_ORD WHERE DHBORD.ID_DV = " + cboDV.EditValue + " ORDER BY [NGAY] DESC";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay, dt, false, false, true, true, true, this.Name);


                grvNgay.Columns["NGAY"].Visible = false;

                cboNgay.EditValue = grvNgay.GetFocusedRowCellValue("NGAY_THANG").ToString();
            }
            catch
            {
                cboNgay.EditValue = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        private void LoadPCD()
        {
            try
            {
                DateTime dtNgay;
                try
                {
                    dtNgay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                }
                catch { dtNgay = DateTime.Now; }


                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spPCDHDMH", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_DV", SqlDbType.BigInt).Value = cboDV.EditValue;
                cmd.Parameters.Add("@ID_XN", SqlDbType.BigInt).Value = cboXN.EditValue;
                cmd.Parameters.Add("@ID_TO", SqlDbType.BigInt).Value = cboTo.EditValue;
                cmd.Parameters.Add("@ID_CHUYEN", SqlDbType.Int).Value = cboChuyen.EditValue;
                cmd.Parameters.Add("@XemCu", SqlDbType.Int).Value = XemCu;
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = dtNgay;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_TEMP"] };
                if (grdPCD.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdPCD, grvPCD, dt, false, false, true, true, true, this.Name);
                    grvPCD.Columns["ID_CHUYEN_SD"].Visible = false;
                    grvPCD.Columns["ID_ORD"].Visible = false;
                    grvPCD.Columns["ID_DT"].Visible = false;
                    grvPCD.Columns["ID_TEMP"].Visible = false;
                    grvPCD.Columns["QUI_TRINH_HOAN_CHINH"].Visible = false;

                    grvPCD.Columns["SL_CHOT"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvPCD.Columns["SL_CHOT"].DisplayFormat.FormatString = "N0";
                }
                else
                {
                    grdPCD.DataSource = dt;
                }


                if (grvPCD.RowCount != 0)
                {
                    iChuyenSuDung = Convert.ToInt32(grvPCD.GetFocusedRowCellValue("ID_CHUYEN_SD").ToString());
                    iOrd = Convert.ToInt32(grvPCD.GetFocusedRowCellValue("ID_ORD").ToString());
                }
                if (iIDPCD_TEMP != -1)
                {
                    try
                    {
                        int index = dt.Rows.IndexOf(dt.Rows.Find(iIDPCD_TEMP));
                        grvPCD.FocusedRowHandle = grvPCD.GetRowHandle(index);
                    }
                    catch { }
                }

                dt = new DataTable();
                dt = ds.Tables[1].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboMaHang, dt, "ID_ORD", "TEN_HH", "TEN_HH");
            }
            catch (Exception ex) { }
        }

        private void LoadCN()
        {
            string sBT1 = "sBTCD" + Commons.Modules.iIDUser;
            try
            {
                DateTime dtNgay;
                try
                {
                    dtNgay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                }
                catch { dtNgay = DateTime.Now; }

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT1, Commons.Modules.ObjSystems.ConvertDatatable(grdPCD), "");
                //optXCLP.SelectedIndex = 0  XEM CU
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spPCDGetCNhan", cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName,
                        Commons.Modules.TypeLanguage, XemCu, cboChuyen.EditValue, iOrd, dtNgay, sBT1));
                //dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CN"] };
                dt.Columns["CDL"].ReadOnly = false;
                //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboCN, dt, "MS_CN", "LMS", "LMS");
                if (grdTo.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdTo, grvTo, dt, false, false, true, true, true, this.Name);
                    grvTo.Columns["ID_CN"].Visible = false;
                    grvTo.Columns["ID_CHUYEN"].Visible = false;
                    grvTo.Columns["ID_ORD"].Visible = false;
                    grvTo.Columns["MS_CN_INT"].Visible = false;
                }
                else
                {
                    grdTo.DataSource = dt;
                }
                //if (iCN != -1)
                //{
                //    int index = dt.Rows.IndexOf(dt.Rows.Find(iCN));
                //    grvTo.FocusedRowHandle = grvTo.GetRowHandle(index);
                //    grvTo.ClearSelection();
                //    grvTo.SelectRow(index);
                //}
                Commons.Modules.ObjSystems.XoaTable(sBT1);
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBT);
            }
        }

        private void LoadCD()
        {
            sBT = "sBTCD" + Commons.Modules.iIDUser;
            try
            {
                DateTime dtNgay;
                try
                {
                    dtNgay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                }
                catch { dtNgay = DateTime.Now; }
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grdPCD), "");
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spPCDGetCDoan", cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName,
                        Commons.Modules.TypeLanguage, iChuyenSuDung, iOrd, dtNgay, sBT));
                dt.Columns["ID_CD"].ReadOnly = false;
                dt.Columns["THANH_TIEN"].ReadOnly = false;
                dtCD = dt;
                Commons.Modules.ObjSystems.XoaTable(sBT);
                if (grdCD.DataSource == null)
                {
                    //Commons.Modules.ObjSystems.MLoadXtraGrid(grdCD, grvCD, dt, windowsUIButton.Buttons[3].Properties.Visible, false, false, true, true, this.Name);
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdCD, grvCD, dt, false, true, false, true, true, this.Name);
                    grvCD.Columns["TEN_CD"].OptionsColumn.AllowFocus = false;
                    grvCD.Columns["TEN_CD"].OptionsColumn.ReadOnly = true;
                    grvCD.Columns["DON_GIA"].OptionsColumn.AllowFocus = false;
                    grvCD.Columns["DON_GIA"].OptionsColumn.ReadOnly = true;
                    grvCD.Columns["THANH_TIEN"].OptionsColumn.AllowFocus = false;
                    grvCD.Columns["THANH_TIEN"].OptionsColumn.ReadOnly = false;
                    grvCD.Columns["ID_CN"].Visible = false;
                    grvCD.Columns["ID_CHUYEN"].Visible = false;
                    grvCD.Columns["ID_CHUYEN_SD"].Visible = false;
                    grvCD.Columns["ID_ORD"].Visible = false;

                    grvCD.Columns["SO_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvCD.Columns["SO_LUONG"].DisplayFormat.FormatString = "N0";

                    grvCD.Columns["THANH_TIEN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvCD.Columns["THANH_TIEN"].DisplayFormat.FormatString = "N0";
                }
                else
                {
                    grdCD.DataSource = dt;
                }
                cboMQL = new RepositoryItemLookUpEdit();
                dtMQL = new DataTable();
                dtMQL.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spQTCNGetCDoan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iChuyenSuDung, iOrd));
                //dtMQL.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT T1.ID_CD, T1.MaQL, T1.TEN_CD_QT AS TEN_CD FROM QUI_TRINH_CONG_NGHE_CHI_TIET T1 LEFT JOIN PHIEU_CONG_DOAN T2 ON T1.ID_CD = T2.ID_CD"));
                cboMQL.NullText = "";
                cboMQL.ValueMember = "ID_CD";
                cboMQL.DisplayMember = "MaQL";
                cboMQL.DataSource = dtMQL;
                cboMQL.Columns.Clear();
                //TSua(false);

                cboMQL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_CD"));
                cboMQL.Columns["ID_CD"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "ID_CD");
                cboMQL.Columns["ID_CD"].Visible = false;

                cboMQL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaQL"));
                cboMQL.Columns["MaQL"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "MaQL");

                cboMQL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_CD"));
                cboMQL.Columns["TEN_CD"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_CD");

                cboMQL.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboMQL.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                //cboMQL.ShowLines
                grvCD.Columns["ID_CD"].ColumnEdit = cboMQL;
                cboMQL.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
                cboMQL.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            }
            catch
            {
                Commons.Modules.ObjSystems.XoaTable(sBT);
            }
        }
        private void cboChuyen_EditValueChanged(object sender, EventArgs e)
        {
            cboNgay_EditValueChanged_1(null, null);
        }

        private void grvPCD_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            grvCD.UpdateCurrentRow();

            iChuyenSuDung = grvPCD.GetFocusedRowCellValue("ID_CHUYEN_SD") == null ? -1 : Convert.ToInt32(grvPCD.GetFocusedRowCellValue("ID_CHUYEN_SD").ToString());
            iOrd = grvPCD.GetFocusedRowCellValue("ID_ORD") == null ? -1 : Convert.ToInt32(grvPCD.GetFocusedRowCellValue("ID_ORD").ToString());
            //LoadCN();

            #region filter CN
            if (Commons.Modules.sLoad == "0Load") return;
            DataTable dtTmp = new DataTable();
            dtTmp = (DataTable)grdTo.DataSource;
            String sIDCN;
            try
            {
                string sDK = "";
                sIDCN = "";
                try
                {
                    sIDCN = grvTo.GetFocusedRowCellValue("ID_ORD").ToString();
                }
                catch { }
                if (sIDCN != "")
                {
                    sDK = "ID_ORD = '" + iOrd + "' ";
                }
                dtTmp.DefaultView.RowFilter = sDK;
            }
            catch (Exception ex) { }
            #endregion

            cboMQL = new RepositoryItemLookUpEdit();
            dtMQL = new DataTable();
            dtMQL.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spQTCNGetCDoan", Commons.Modules.UserName, Commons.Modules.TypeLanguage, iChuyenSuDung, iOrd));
            //dtMQL.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT T1.ID_CD, T1.MaQL, T1.TEN_CD_QT AS TEN_CD FROM QUI_TRINH_CONG_NGHE_CHI_TIET T1 LEFT JOIN PHIEU_CONG_DOAN T2 ON T1.ID_CD = T2.ID_CD"));
            cboMQL.NullText = "";
            cboMQL.ValueMember = "ID_CD";
            cboMQL.DisplayMember = "MaQL";
            cboMQL.DataSource = dtMQL;
            cboMQL.Columns.Clear();
            //TSua(false);

            cboMQL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_CD"));
            cboMQL.Columns["ID_CD"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "ID_CD");
            cboMQL.Columns["ID_CD"].Visible = false;

            cboMQL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaQL"));
            cboMQL.Columns["MaQL"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "MaQL");

            cboMQL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_CD"));
            cboMQL.Columns["TEN_CD"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_CD");

            cboMQL.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            cboMQL.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

            //cboMQL.ShowLines
            grvCD.Columns["ID_CD"].ColumnEdit = cboMQL;
            cboMQL.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            cboMQL.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            grvTo_FocusedRowChanged(null, null);

        }

        private void grvTo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            DataTable dtTmp = new DataTable();
            dtTmp = (DataTable)grdCD.DataSource;
            String sIDCN;
            try
            {
                string sDK = "";
                sIDCN = "-1";
                try
                {
                    sIDCN = grvTo.GetFocusedRowCellValue("ID_CN").ToString();
                }
                catch { }
                if (sIDCN != "-1")
                {
                    if (XemCu == 0)
                    {
                        sDK = " ID_CN = '" + sIDCN + "' AND ID_CHUYEN_SD = '" + iChuyenSuDung + "' AND ID_ORD = '" + iOrd + "' ";
                    }
                    else
                    {
                        sDK = " ID_CN = '" + sIDCN + "' AND ID_CHUYEN_SD = '" + iChuyenSuDung + "' AND ID_ORD = '" + iOrd + "' ";
                    }
                }
                else
                {
                    sDK = "1 = 0";
                }

                dtTmp.DefaultView.RowFilter = sDK;
            }
            catch (Exception ex) { }
            LoadTextTongLSP();
            //LoadCboMSCN();
            if (Commons.Modules.sPS != "0Focus")
            {
                iCN = Convert.ToInt32(grvTo.GetFocusedRowCellValue("ID_CN"));
            }
            else return;

        }

        private void TSua(Boolean TSua)
        {
            //grdPCD.Enabled = !TSua;

            windowsUIButton.Buttons[0].Properties.Visible = !TSua;
            windowsUIButton.Buttons[1].Properties.Visible = !TSua;
            windowsUIButton.Buttons[2].Properties.Visible = !TSua;
            windowsUIButton.Buttons[3].Properties.Visible = !TSua;
            windowsUIButton.Buttons[4].Properties.Visible = !TSua;
            windowsUIButton.Buttons[7].Properties.Visible = !TSua;

            windowsUIButton.Buttons[5].Properties.Visible = TSua;
            windowsUIButton.Buttons[6].Properties.Visible = TSua;


            cboDV.Properties.ReadOnly = TSua;
            cboXN.Properties.ReadOnly = TSua;
            cboTo.Properties.ReadOnly = TSua;
            cboChuyen.Properties.ReadOnly = TSua;
            cboNgay.Properties.ReadOnly = TSua;
            lblMaHang.Enabled = TSua;
            cboMaHang.Properties.ReadOnly = !TSua;

        }

        private void btnMH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboNgay.Text))
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanChuaChonNgay"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmPCDHDMHChot frm = new frmPCDHDMHChot();
            DateTime dThang = Convert.ToDateTime(cboNgay.EditValue);

            frm.dThang = Convert.ToDateTime("01/" + dThang.Month + "/" + dThang.Year);
            frm.ShowDialog();
        }

        private void optXCLP_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Commons.Modules.sPS == "0Load") return;
            //btnTSua.Enabled = true;
            //if (optXCLP.SelectedIndex == 1)
            //{
            //    cboNgay_EditValueChanged_1(null, null);
            //}
            //else
            //{
            //    LoadPCD();
            //    LoadCD();
            //    LoadCN();
            //    LoadCboMSCN();
            //    cboNgay_EditValueChanged_1(null, null);
            //}
            //LoadThang();
        }

        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                Commons.Modules.sLoad = "0Load";
                LoadThang();
                LoadCboXN();
                LoadCboTo();
                LoadChuyen();
                LoadPCD();
                LoadCN();
                LoadCD();
                //LoadCboMSCN();
                Commons.Modules.sLoad = "";
            }
            catch { }

        }

        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadCboTo();
            LoadPCD();
            LoadCN();
            LoadCD();
            //LoadCboMSCN();
            Commons.Modules.sLoad = "";
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadPCD();
            LoadCN();
            LoadCD();
            //LoadCboMSCN();
            grvPCD_FocusedRowChanged(null, null);
            grvTo_FocusedRowChanged(null, null);
        }

        private void cboNgay_EditValueChanged_1(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadPCD();
            LoadCD();
            LoadCN();
            //LoadCboMSCN();
            grvPCD_FocusedRowChanged(null, null);
            grvTo_FocusedRowChanged(null, null);
        }

        private void cboNgay_BeforePopup(object sender, EventArgs e)
        {
            popupContainerControl1.Height = 300;
            popupContainerControl1.Width = 300;

            popupContainerControl2.Width = 300;
            popupContainerControl2.Height = 200;
            grdNgay.Height = 200;
            grdNgay.Width = 300;
        }

        private void grvNgay_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboNgay.Text = grvNgay.GetFocusedRowCellValue("NGAY_THANG").ToString();
            }
            catch { }
            cboNgay.ClosePopup();
        }

        private void calendarControl1_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboNgay.Text = calendarControl1.DateTime.ToString("dd/MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdNgay);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + cboNgay.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                }
                else { }
            }
            catch (Exception ex)
            {
                cboNgay.Text = calendarControl1.DateTime.ToString("dd/MM/yyyy");
            }
            cboNgay.ClosePopup();
        }

        private void grvCD_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            grvCD.ClearColumnErrors();
            GridView view = sender as GridView;

            if (view.FocusedColumn.FieldName == "ID_CD")
            {
                //kiểm tra tồn tại trong combo
                //if (dtMQL.AsEnumerable().Count(x => x["ID_CD"].ToString().Equals(e.Value.ToString())) == 0)
                //{
                //    e.Valid = false;
                //    e.ErrorText = "ton tai";
                //    view.SetColumnError(view.Columns["ID_CD"], e.ErrorText);
                //    return;
                //}
                //kiểm tra không trùng trên lưới


                if (Commons.Modules.ObjSystems.ConvertDatatable(grdCD).AsEnumerable().Where(x => x["ID_CN"].ToString().Trim().Equals(grvTo.GetFocusedRowCellValue("ID_CN").ToString().Trim())).Count(x => x["ID_CD"].ToString().Trim().Equals(e.Value.ToString().Trim())) >= 1)
                {
                    e.Valid = false;
                    e.ErrorText = Commons.Modules.TypeLanguage == 0 ? "Trùng" : "Duplicate";
                    view.SetColumnError(view.Columns["ID_CD"], e.ErrorText);
                    return;
                }
                DataTable dt = dtMQL.AsEnumerable().Where(x => x["ID_CD"].ToString().Equals(e.Value.ToString())).CopyToDataTable();
                try
                {

                    grvCD.SetFocusedRowCellValue("TEN_CD", dt.Rows[0]["TEN_CD"]);
                    grvCD.SetFocusedRowCellValue("DON_GIA", dt.Rows[0]["DON_GIA"]);
                    grvCD.SetFocusedRowCellValue("SO_LUONG", grvPCD.GetFocusedRowCellValue("SL_NGAY"));
                    grvCD.SetFocusedRowCellValue("THANH_TIEN", Convert.ToDouble(grvPCD.GetFocusedRowCellValue("SL_NGAY")) * Convert.ToDouble(dt.Rows[0]["DON_GIA"]));
                    grvCD.SetFocusedRowCellValue("ID_CD", dt.Rows[0]["ID_CD"]);
                    //grvCD.SetFocusedRowCellValue("ID_CN", grvTo.GetFocusedRowCellValue("ID_CN"));

                }
                catch
                {
                }
            }


            if (chkKT.Checked == false)
            {
                return;
            }
            if (view.FocusedColumn.FieldName == "SO_LUONG")
            {
                string sBT_CD = "sBT_CD" + Commons.Modules.UserName;
                try
                {
                    //Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT_CD, dtCD, "");
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT_CD, Commons.Modules.ObjSystems.ConvertDatatable(grvCD), "");
                    System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetSLNhapCD", conn);
                    cmd.Parameters.Add("@BangTam", SqlDbType.NVarChar).Value = sBT_CD;
                    cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                    cmd.Parameters.Add("@SLnhap", SqlDbType.Int).Value = e.Value;
                    cmd.Parameters.Add("@ID_Chuyen", SqlDbType.BigInt).Value = Convert.ToInt64(grvCD.GetFocusedRowCellValue("ID_CHUYEN_SD"));
                    cmd.Parameters.Add("@ID_Ord", SqlDbType.BigInt).Value = string.IsNullOrEmpty(grvCD.GetFocusedRowCellValue("ID_ORD").ToString()) ? -1 : Convert.ToInt64(grvCD.GetFocusedRowCellValue("ID_ORD"));
                    cmd.Parameters.Add("@ID_CD", SqlDbType.BigInt).Value = string.IsNullOrEmpty(grvCD.GetFocusedRowCellValue("ID_CD").ToString()) ? -1 : Convert.ToInt64(grvCD.GetFocusedRowCellValue("ID_CD"));
                    cmd.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);


                    DataTable dt = new DataTable();
                    dt = ds.Tables[0].Copy();

                    //Kiểm tra số lượng công đoạn đang nhập có vượt số lượng chốt hay không
                    if (Convert.ToInt32(dt.Rows[0]["SL_NHAP"]) > Convert.ToInt32(grvPCD.GetFocusedRowCellValue("SL_NGAY")))
                    {
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_VuotSLChot"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            e.Valid = false;
                            e.ErrorText = Commons.Modules.TypeLanguage == 0 ? "Số lượng đã vượt số lượng chốt" : "The number has exceeded the number of pins";
                            view.SetColumnError(view.Columns["SO_LUONG"], e.ErrorText);
                            return;
                        }
                        else
                        {
                            grvCD.SetFocusedRowCellValue("SO_LUONG", e.Value);
                        }
                    }
                    Commons.Modules.ObjSystems.XoaTable(sBT_CD);
                }
                catch
                {
                    Commons.Modules.ObjSystems.XoaTable(sBT_CD);
                }
            }
        }
        private void grvCD_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "SO_LUONG")
                {
                    DataTable dt = dtMQL.AsEnumerable().Where(x => x["ID_CD"].ToString().Equals(grvCD.GetFocusedRowCellValue("ID_CD").ToString())).CopyToDataTable();
                    grvCD.SetFocusedRowCellValue("THANH_TIEN", (Convert.ToDouble(e.Value) * Convert.ToDouble(dt.Rows[0]["DON_GIA"])));

                    //dt = (DataTable)grdCD.DataSource;
                    //DataTable dt1 = new DataTable();
                    //dt1 = Commons.Modules.ObjSystems.ConvertDatatable(grvCD);
                    //dt.AsEnumerable().Where(x => x["ID_CN"].ToString() == grvTo.GetFocusedRowCellValue("ID_CN").ToString() && Convert.ToString(x["ID_CHUYEN_SD"]) == Convert.ToString(grvPCD.GetFocusedRowCellValue("ID_CHUYEN_SD"))
                    //&& Convert.ToString(x["ID_ORD"]) == Convert.ToString(grvPCD.GetFocusedRowCellValue("ID_ORD"))
                    //).ToList<DataRow>().ForEach(r => r["TONG_LSP"] = (Convert.ToInt32(dt1.Compute("Sum(THANH_TIEN)", ""))));

                    //dt.AcceptChanges();
                    LoadTextTongLSP();
                }
            }
            catch { }
        }
        private void LoadTextTongLSP()
        {
            try
            {
                DataTable dt1 = new DataTable();
                dt1 = Commons.Modules.ObjSystems.ConvertDatatable(grvCD);
                lbl.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTongLSP") + " " + (Convert.ToDouble(dt1.Compute("Sum(THANH_TIEN)", "")).ToString("N0") == "" ? "0" : Convert.ToDouble(dt1.Compute("Sum(THANH_TIEN)", "")).ToString("N0")).ToString();
            }
            catch
            {
                lbl.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTongLSP") + " 0";
            }
        }

        private void grvCD_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
        private void grvCD_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {


                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "thuathieu":
                        {
                            if (grvPCD.RowCount == 0) return;
                            Form.frmThuaThieuSL frm = new Form.frmThuaThieuSL();
                            frm.iID_DV = Convert.ToInt32(cboDV.EditValue);
                            frm.iID_CHUYEN_SD = Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_CHUYEN_SD"));
                            frm.iID_ORD = Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_ORD"));
                            frm.iID_DT = Convert.ToInt32(grvPCD.GetFocusedRowCellValue("ID_DT"));
                            frm.Ngay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                            iIDPCD_TEMP = Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_TEMP"));
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                cboNgay_EditValueChanged_1(null, null);
                            }
                            else
                            {
                                cboNgay_EditValueChanged_1(null, null);
                            }
                            break;
                        }
                    case "ChonMH":
                        {
                            if (string.IsNullOrEmpty(cboNgay.Text))
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanChuaChonNgay"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            frmPCDHDMHChot frm = new frmPCDHDMHChot();
                            DateTime dThang = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                            frm.dThang = Convert.ToDateTime("01/" + dThang.Month + "/" + dThang.Year);
                            frm.iID_DV = Convert.ToInt32(cboDV.EditValue);
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                LoadPCD();
                            }
                            else
                            {
                                LoadPCD();
                            }

                            break;
                        }
                    case "themsua":
                        {
                            Commons.Modules.sPS = "0Focus";
                            XemCu = 1;
                            cboNgay_EditValueChanged_1(null, null);
                            Commons.Modules.ObjSystems.AddnewRow(grvCD, true);
                            TSua(true);
                            break;
                        }

                    case "in":
                        {
                            if (grvPCD.RowCount == 0) return;
                            DateTime Ngay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);

                            iChuyenSuDung = Convert.ToInt32(grvPCD.GetFocusedRowCellValue("ID_CHUYEN_SD"));
                            iOrd = Convert.ToInt32(grvPCD.GetFocusedRowCellValue("ID_ORD"));

                            frmInBaoCaoPCD frm = new frmInBaoCaoPCD(Ngay, Convert.ToInt64(iChuyen), Convert.ToInt64(iChuyenSuDung), Convert.ToInt64(iOrd));

                            frm.ShowDialog();
                            break;
                        }
                    case "luu":
                        {
                            grvCD.CloseEditor();
                            grvCD.UpdateCurrentRow();

                            DataTable dtSoure = new DataTable();
                            dtSoure = (DataTable)grdCD.DataSource;
                            if (!KiemTraLuoi(dtSoure)) return;
                            string stbCongNhan = "BTPCD" + Commons.Modules.UserName;
                            //DateTime ngay = Convert.ToDateTime(cboNgay.EditValue);
                            DateTime ngay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                            //dtNgay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                            try
                            {
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbCongNhan, (DataTable)grdCD.DataSource, "");

                                DataTable dt = new DataTable();
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spSavePhieuCongDoan", stbCongNhan, iChuyen, cboChuyen.EditValue, iOrd, ngay.ToString("yyyyMMdd")));
                                if (Convert.ToInt32(dt.Rows[0][0]) != 1)
                                {
                                    XtraMessageBox.Show(dt.Rows[0][1].ToString());
                                }
                                Commons.Modules.ObjSystems.XoaTable(stbCongNhan);
                            }
                            catch (Exception ex) { }

                            TSua(false);
                            XemCu = 0;
                            Commons.Modules.ObjSystems.DeleteAddRow(grvCD);
                            grvCD.UpdateCurrentRow();
                            cboNgay_EditValueChanged_1(null, null);
                            break;
                        }
                    case "khongluu":
                        {
                            Commons.Modules.sPS = "";
                            TSua(false);
                            XemCu = 0;
                            Commons.Modules.ObjSystems.DeleteAddRow(grvCD);
                            grvCD.UpdateCurrentRow();
                            cboNgay_EditValueChanged_1(null, null);
                            break;
                        }
                    case "thoat":
                        {
                            Commons.Modules.ObjSystems.GotoHome(this);
                            break;
                        }
                    default:
                        break;
                }
            }
            catch { }
        }

        private void grvCD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Home)
            {
                searchControl2.Focus();
            }
        }
        //private void cboMSCN_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        grvCD.Focus();
        //        grvCD.FocusedColumn = grvCD.Columns["ID_CD"];
        //        grvCD.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
        //    }
        //}

        private void grvCD_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                grvCD.SetFocusedRowCellValue("ID_CHUYEN", grvTo.GetFocusedRowCellValue("ID_CHUYEN"));
                grvCD.SetFocusedRowCellValue("ID_CHUYEN_SD", grvPCD.GetFocusedRowCellValue("ID_CHUYEN_SD"));
                grvCD.SetFocusedRowCellValue("ID_ORD", grvPCD.GetFocusedRowCellValue("ID_ORD"));
                grvTo.SetFocusedRowCellValue("CDL", grvCD.RowCount);
                grvCD.SetFocusedRowCellValue("ID_CN", grvTo.GetFocusedRowCellValue("ID_CN"));
            }
            catch { }
        }

        private void grdCD_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                //grvCD.SetFocusedRowCellValue("ID_CD", cboMQL.GetDataSourceValue("ID_CD", 1));
                if (e.KeyCode == Keys.Delete && !windowsUIButton.Buttons[0].Properties.Visible)
                {
                    grvCD.DeleteSelectedRows();
                    ((DataTable)grdCD.DataSource).AcceptChanges();
                    DataTable dt = new DataTable();
                    dt = Commons.Modules.ObjSystems.ConvertDatatable(grvCD);
                    grvTo.SetFocusedRowCellValue("CDL", dt.Rows.Count == 0 ? (object)(DBNull.Value) : dt.Rows.Count);

                    LoadTextTongLSP();
                }
            }
            catch { }
        }

        private void grvPCD_RowCountChanged(object sender, EventArgs e)
        {
            grvPCD_FocusedRowChanged(null, null);
        }

        private void grvCD_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        #region kiemDL
        private bool KiemTraLuoi(DataTable dtSource)
        {
            int errorCount = 0;
            #region kiểm tra dữ liệu
            this.Cursor = Cursors.WaitCursor;
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                //Số hợp đồng lao động


                if (!KiemDuLieuSo(grvCD, dr, "SO_LUONG", grvCD.Columns["SO_LUONG"].FieldName.ToString(), 0, 0, true, this.Name))
                {
                    try
                    {

                        //DataTable dt1 = new DataTable();
                        //dt1 = (DataTable)grdTo.DataSource;
                        //dt1.PrimaryKey = new DataColumn[] { dt1.Columns["STT_CN"] };
                        //int index = dt1.Rows.IndexOf(dt1.Rows.Find(dr["STT_CN"]));
                        //DataRow dr1 = dt1.Rows[index];
                        //dr1.SetColumnError("CDL", "Error");

                        DataTable dt2 = new DataTable();
                        dt2 = (DataTable)grdPCD.DataSource;
                        dt2.PrimaryKey = new DataColumn[] { dt2.Columns["ID_ORD"] };
                        int index = dt2.Rows.IndexOf(dt2.Rows.Find(dr["ID_ORD"]));
                        DataRow dr2 = dt2.Rows[index];
                        dr2.SetColumnError("TEN_HH", "Error");
                    }
                    catch (Exception ex) { }
                    errorCount++;
                }
            }
            #endregion
            Commons.Modules.ObjSystems.HideWaitForm();
            if (errorCount != 0)
            {
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                this.Cursor = Cursors.Default;
                return true;
            }
        }

        public bool KiemDuLieuSo(GridView grvData, DataRow dr, string sCot, string sTenKTra, double GTSoSanh, double GTMacDinh, Boolean bKiemNull, string sForm)
        {
            string sDLKiem;
            sDLKiem = dr[sCot].ToString();
            double DLKiem;
            if (bKiemNull)
            {
                if (string.IsNullOrEmpty(sDLKiem))
                {
                    dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongduocTrong"));
                    return false;
                }
                else
                {
                    if (!double.TryParse(dr[sCot].ToString(), out DLKiem))
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongPhaiSo"));
                        return false;
                    }
                    else
                    {
                        if (GTSoSanh != -999999)
                        {
                            if (DLKiem == 0)
                            {
                                dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgSoLuongKhongNhoHon") + GTSoSanh.ToString());
                                return false;
                            }

                            DLKiem = Math.Round(DLKiem, 8);
                            dr[sCot] = DLKiem.ToString();

                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(sDLKiem) && GTMacDinh != -999999)
                {
                    dr[sCot] = GTMacDinh;
                    DLKiem = GTMacDinh;
                    sDLKiem = GTMacDinh.ToString();
                }

                if (!string.IsNullOrEmpty(sDLKiem))
                {
                    if (!double.TryParse(dr[sCot].ToString(), out DLKiem))
                    {
                        dr.SetColumnError(sCot, sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongPhaiSo"));
                        return false;
                    }
                    else
                    {
                        if (GTSoSanh != -999999)
                        {
                            if (DLKiem < GTSoSanh)
                            {
                                dr.SetColumnError(sCot, sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongNhoHon") + GTSoSanh.ToString());
                                return false;
                            }

                            DLKiem = Math.Round(DLKiem, 8);
                            dr[sCot] = DLKiem.ToString();
                        }
                    }
                }
            }
            return true;
        }

        #endregion
        private void grvCD_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.UpdateCurrentRow();

                if (view.FocusedColumn.FieldName == "SO_LUONG")
                {
                    int iSLNhap = 0;
                    iSLNhap = view.GetFocusedRowCellValue("SO_LUONG").ToString() == "" ? 0 : Convert.ToInt32(view.GetFocusedRowCellValue("SO_LUONG"));
                    if (iSLNhap == 0)
                    {
                        e.Valid = false;
                        e.ErrorText = Commons.Modules.TypeLanguage == 0 ? "Số lượng phải lớn hơn 0" : "The number must be greater than 0";
                        view.SetColumnError(view.Columns["SO_LUONG"], e.ErrorText);
                        return;
                    }
                }
            }
            catch { }
        }

        private void searchControl2_Click(object sender, EventArgs e)
        {
            searchControl2.SelectAll();
        }

        private void searchControl2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                grvCD.Focus();
                grvCD.FocusedColumn = grvCD.Columns["ID_CD"];
                grvCD.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            }
        }
        private void LoadCboXN()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT  T.ID_XN,T.TEN_XN FROM (SELECT  DISTINCT  STT_DV, STT_XN, ID_XN, TEN_XN  AS TEN_XN  FROM dbo.MGetToUser('" + Commons.Modules.UserName + "', " + Commons.Modules.TypeLanguage + ") WHERE (ID_DV = " + cboDV.EditValue + " OR " + cboDV.EditValue + " = -1) AND ID_LOAI_CHUYEN IN(1, 2, 3, 4, 5, 6, 7) UNION SELECT - 1, -1, -1, '< All >') T ORDER BY T.STT_DV, T.STT_XN"));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboXN, dt, "ID_XN", "TEN_XN", "TEN_XN");
            }
            catch { }
        }
        private void LoadCboTo()
        {
            try
            {
                DataTable dt = new DataTable();
                string sSQL = "SELECT T.ID_TO, T.TEN_TO  FROM (SELECT T2.ID_TO, T2.TEN_TO, T2.STT_TO FROM(SELECT ID_TO, TEN_TO, STT_TO FROM dbo.MGetToUser('" + Commons.Modules.UserName + "', " + Commons.Modules.TypeLanguage + ") WHERE ID_LOAI_CHUYEN IN(1, 2, 3, 4, 5, 6, 7) AND(ID_DV = " + cboDV.EditValue + " OR " + cboDV.EditValue + " = -1) AND(ID_XN = " + cboXN.EditValue + " OR " + cboXN.EditValue + " = -1)) T2 UNION SELECT - 1, '< All >', -1) T ORDER BY STT_TO";
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSQL));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTo, dt, "ID_TO", "TEN_TO", "TEN_TO");
            }
            catch (Exception ex) { }
        }

        private void cboMaHang_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                #region filter PCD
                if (Commons.Modules.sLoad == "0Load") return;
                DataTable dtTmp = new DataTable();
                dtTmp = (DataTable)grdPCD.DataSource;
                String sIDPCD;
                try
                {
                    string sDK = "";
                    sIDPCD = "";
                    sDK = "ID_ORD = '" + cboMaHang.EditValue + "' OR '" + cboMaHang.EditValue + "' = '-1' ";
                    dtTmp.DefaultView.RowFilter = sDK;
                }
                catch (Exception ex)
                {
                    dtTmp.DefaultView.RowFilter = "1 = 0";
                }
                #endregion
                grvPCD_FocusedRowChanged(null, null);
            }
            catch { }
        }



        #region chuotphai
        class RowInfo
        {
            public RowInfo(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
            {
                this.RowHandle = rowHandle;
                this.View = view;
            }
            public DevExpress.XtraGrid.Views.Grid.GridView View;
            public int RowHandle;
        }

        public DXMenuItem MCreateMenuCapNhatSLChotNgay(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblNhapSLChotNgay", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(NhapSLChotNgay));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }
        public void NhapSLChotNgay(object sender, EventArgs e)
        {
            try
            {
                //Load worksheet
                XtraInputBoxArgs args = new XtraInputBoxArgs();
                // set required Input Box options
                args.Caption = "Nhập sản lượng ngày";
                args.Prompt = "Sản lượng ngày";
                args.DefaultButtonIndex = 0;

                // initialize a DateEdit editor with custom settings
                TextEdit editor = new TextEdit();
                editor.EditValue = 0;

                args.Editor = editor;
                // a default DateEdit value
                args.DefaultResponse = Convert.ToDouble(grvPCD.GetFocusedRowCellValue("SL_NGAY"));
                // display an Input Box with the custom editor
                var result = XtraInputBox.Show(args);
                if (result == null || result.ToString() == "") return;
                iIDPCD_TEMP = Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_TEMP"));
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spSavePhieuCDChotNgay", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToInt32(grvPCD.GetFocusedRowCellValue("ID_CHUYEN_SD")), Convert.ToInt32(grvPCD.GetFocusedRowCellValue("ID_ORD")), cboNgay.EditValue, result));
                if (dt.Rows[0][0].ToString() == "99")
                {
                    XtraMessageBox.Show(dt.Rows[0][1].ToString());
                    return;
                }
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgLuuThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK);
                cboNgay_EditValueChanged_1(null, null);
            }
            catch (Exception ex) { }
        }
        public DXMenuItem MCreateMenuNhapPCDPacking(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblNhapCongDoanPacking", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(NhapCDPacKing));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }
        public void NhapCDPacKing(object sender, EventArgs e)
        {
            try
            {
                if (!Convert.ToBoolean(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT ISNULL(TINH_DOANH_THU,0) FROM dbo.[TO] WHERE ID_TO = " + cboChuyen.EditValue + "")))
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChonChuyenTinhDoanhThu"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                frmPhieuCongDoanPacking frm = new frmPhieuCongDoanPacking();
                frm.iID_CHUYEN_SD = Convert.ToInt32(cboChuyen.EditValue);
                frm.iID_DV = Convert.ToInt32(cboDV.EditValue);
                frm.iID_XN = Convert.ToInt32(cboXN.EditValue);
                frm.IID_TO = Convert.ToInt32(cboTo.EditValue);
                frm.dNgay = Convert.ToDateTime(cboNgay.Text);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    cboNgay_EditValueChanged_1(null, null);

                }
                else
                {
                    cboNgay_EditValueChanged_1(null, null);
                }
            }
            catch (Exception ex) { }
        }
        public DXMenuItem MCreateMenuNhapMultyCD(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblChonCongDoan", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(NhapMultyCD));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }
        public void NhapMultyCD(object sender, EventArgs e)
        {
            try
            {
                grvCD.CloseEditor();
                grvCD.UpdateCurrentRow();


                DataTable dtTmp = new DataTable(); // loc du lieu, chỉ lấy những công đoạn chưa có trong danh sách view
                dtTmp = dtMQL.Copy();
                try
                {
                    var dt_temp = dtTmp.AsEnumerable().Where(row => !Commons.Modules.ObjSystems.ConvertDatatable(grvCD).AsEnumerable()
                                                             .Select(r => r.Field<Int64>("ID_CD"))
                                                             .Any(x => x == row.Field<Int64>("ID_CD"))
                                                             ).CopyToDataTable();
                    dtTmp = new DataTable();
                    dtTmp = (DataTable)dt_temp;
                }
                catch
                {
                    dtTmp.Clear();
                }
                dtTmp.AcceptChanges();


                frmCapNhatNhanhPCD frm = new frmCapNhatNhanhPCD(); // mở form set các thuộc tính
                frm.dtTemp = new DataTable();
                frm.dtTemp = dtTmp;
                frm.iLoai = 2;
                frm.isoLuong = Convert.ToInt32(grvCD.GetFocusedRowCellValue("SO_LUONG"));
                DataTable dt = new DataTable();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    dt = frm.dtTemp.Copy(); // nhận giữ liệu khi người dùng nhấn lưu
                    string sBTUpdate = "sBTUpdate" + Commons.Modules.iIDUser; // bảng tạm dữ liệu ng dùng chọn công nhân
                    string sBTCurrent = "sBTCurrent" + Commons.Modules.iIDUser; // bảng tạm cả datasoure
                    string sBTFocus = "sBTFocus" + Commons.Modules.iIDUser; // bảng tạm dữ liệud đang focus
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTUpdate, dt, "");
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCurrent, Commons.Modules.ObjSystems.ConvertDatatable(grdCD), "");
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTFocus, Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdCD, grvCD), "");
                    dt = new DataTable();
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spNhapNhanhPCD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 2, sBTUpdate, sBTCurrent, sBTFocus));
                    dt.Columns["ID_CD"].ReadOnly = false;
                    dt.Columns["THANH_TIEN"].ReadOnly = false;

                    grdCD.DataSource = dt;
                    grvTo_FocusedRowChanged(null, null);

                    dt = new DataTable();
                    dt = Commons.Modules.ObjSystems.ConvertDatatable(grvCD);
                    grvTo.SetFocusedRowCellValue("CDL", dt.Rows.Count == 0 ? (object)(DBNull.Value) : dt.Rows.Count); // set lại cột số lượng công nhân
                }
            }
            catch (Exception ex) { }
        }
        private void grvPCD_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                int irow = e.HitInfo.RowHandle;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    e.Menu.Items.Clear();
                    DevExpress.Utils.Menu.DXMenuItem itemTTNS = MCreateMenuCapNhatSLChotNgay(view, irow);
                    e.Menu.Items.Add(itemTTNS);

                    DevExpress.Utils.Menu.DXMenuItem itemPCDPacking = MCreateMenuNhapPCDPacking(view, irow);
                    e.Menu.Items.Add(itemPCDPacking);
                }
                else
                {
                    DevExpress.Utils.Menu.DXMenuItem itemPCDPacking = MCreateMenuNhapPCDPacking(view, irow);
                    e.Menu = new DevExpress.XtraGrid.Menu.GridViewMenu(view);
                    e.Menu.Items.Add(itemPCDPacking);
                }
            }
            catch
            {
            }
        }

        //chuột phải công đoạn
        private void grvCD_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (windowsUIButton.Buttons[0].Properties.Visible || Convert.ToString(grvCD.GetFocusedRowCellValue("ID_CD")) == "") return;
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                int irow = e.HitInfo.RowHandle;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    e.Menu.Items.Clear();
                    DevExpress.Utils.Menu.DXMenuItem itemTTNS = MCreateMenuNhapMultyCD(view, irow);
                    e.Menu.Items.Add(itemTTNS);
                }
            }
            catch
            {
            }
        }

        #endregion

        private void grvPCD_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(grvPCD.GetRowCellValue(e.RowHandle, grvPCD.Columns["QUI_TRINH_HOAN_CHINH"].FieldName)) == false) return;
                e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFF2CC");
                e.HighPriority = true;
            }
            catch
            {

            }
        }

        private void grvTo_RowCountChanged(object sender, EventArgs e)
        {
            grvTo_FocusedRowChanged(null, null);
        }

        private void searchControl2_EditValueChanged(object sender, EventArgs e)
        {
            DataTable dtTmp = new DataTable();
            dtTmp = (DataTable)grdTo.DataSource;
            //dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grvTo);
            String sMSCN;
            try
            {
                string sDK = "";
                sMSCN = "";
                sDK = "MS_CN_INT = '" + Convert.ToInt32(searchControl2.EditValue) + "'";
                dtTmp.DefaultView.RowFilter = sDK;
            }
            catch (Exception ex)
            {
                dtTmp.DefaultView.RowFilter = "";
            }
        }
        private void grvPCD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F3)
            {
                frmPhieuCongDoanLog frm = new frmPhieuCongDoanLog();
                frm.iID_DV = Convert.ToInt32(cboDV.EditValue);
                frm.dThang = Commons.Modules.ObjSystems.ConvertDateTime(cboNgay.Text);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    cboNgay_EditValueChanged_1(null, null);
                }
                else
                {
                    cboNgay_EditValueChanged_1(null, null);
                }
            }
        }
    }
}