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
                Commons.Modules.sLoad = "0Load";
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDV, Commons.Modules.ObjSystems.DataDonVi(false), "ID_DV", "TEN_DV", "TEN_DV");

                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT  T.ID_XN,T.TEN_XN FROM (SELECT  DISTINCT  STT_DV, STT_XN, STT_TO ,ID_XN, TEN_XN  AS TEN_XN  FROM dbo.MGetToUser('" + Commons.Modules.UserName + "', " + Commons.Modules.TypeLanguage + ") WHERE ID_DV = " + cboDV.EditValue + " OR " + cboDV.EditValue + " = -1 AND ID_LOAI_CHUYEN IN(1, 2, 3, 4, 5, 6, 7) UNION SELECT - 1, -1, -1, -1, '< All >') T ORDER BY T.STT_DV, T.STT_XN, T.STT_TO"));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboXN, dt, "ID_XN", "TEN_XN", "TEN_XN");

                dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT T2.ID_TO, T2.TEN_TO FROM (SELECT  ID_TO, TEN_TO, STT_TO FROM dbo.MGetToUser('" + Commons.Modules.UserName + "', " + Commons.Modules.TypeLanguage + ") WHERE ID_LOAI_CHUYEN IN(1, 2, 3, 4, 5, 6, 7) AND(ID_DV = " + cboDV.EditValue + " OR " + cboDV.EditValue + " = -1) AND(ID_XN = " + cboXN.EditValue + " OR " + cboXN.EditValue + " = -1) UNION SELECT - 1, '< All >', -1) T2 ORDER BY  STT_TO "));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTo, dt, "ID_TO", "TEN_TO", "TEN_TO");


                LoadThang();
                LoadChuyen();
                LoadPCD();
                LoadCN();
                LoadCD();
                LoadCboMSCN();
                Commons.Modules.sLoad = "";

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
                string sSql = "SELECT DISTINCT CONVERT(NVARCHAR(10),[NGAY],103) AS NGAY_THANG,[NGAY] FROM PHIEU_CONG_DOAN ORDER BY [NGAY] DESC";
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
                //optXCLP.SelectedIndex = 0  XEM CU
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spPCDHDMH", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboDV.EditValue, cboXN.EditValue, cboTo.EditValue,
                         cboChuyen.EditValue, XemCu, dtNgay));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdPCD, grvPCD, dt, false, false, true, true, true, this.Name);
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_TEMP"] };
                if (grvPCD.RowCount != 0)
                {
                    iChuyenSuDung = Convert.ToInt32(grvPCD.GetFocusedRowCellValue("ID_CHUYEN_SD").ToString());
                    iChuyen = Convert.ToInt32(grvPCD.GetFocusedRowCellValue("ID_CHUYEN").ToString());
                    iOrd = Convert.ToInt32(grvPCD.GetFocusedRowCellValue("ID_ORD").ToString());
                }
                grvPCD.Columns["ID_CHUYEN"].Visible = false;
                grvPCD.Columns["ID_CHUYEN_SD"].Visible = false;
                grvPCD.Columns["ID_ORD"].Visible = false;

                grvPCD.Columns["SL_CHOT"].DisplayFormat.FormatType = FormatType.Numeric;
                grvPCD.Columns["SL_CHOT"].DisplayFormat.FormatString = "N0";

                if (iIDPCD_TEMP != -1)
                {
                    try
                    {
                        int index = dt.Rows.IndexOf(dt.Rows.Find(iIDPCD_TEMP));
                        grvPCD.FocusedRowHandle = grvPCD.GetRowHandle(index);
                    }
                    catch { }
                }
            }
            catch { }
        }

        private void LoadCN()
        {
            try
            {
                DateTime dtNgay;
                try
                {
                    dtNgay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                }
                catch { dtNgay = DateTime.Now; }

                //optXCLP.SelectedIndex = 0  XEM CU

                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spPCDGetCNhan", cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName,
                        Commons.Modules.TypeLanguage, XemCu, iChuyen, iOrd, dtNgay));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CN"] };
                //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboCN, dt, "MS_CN", "LMS", "LMS");
                if (grdTo.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdTo, grvTo, dt, false, false, true, true, true, this.Name);
                    grvTo.Columns["ID_CN"].Visible = false;
                }
                else
                {
                    grdTo.DataSource = dt;
                }
                if (iCN != -1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(iCN));
                    grvTo.FocusedRowHandle = grvTo.GetRowHandle(index);
                    grvTo.ClearSelection();
                    grvTo.SelectRow(index);
                }
            }
            catch { }
        }

        private void LoadCD()
        {
            try
            {
                DateTime dtNgay;
                try
                {
                    dtNgay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                }
                catch { dtNgay = DateTime.Now; }

                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spPCDGetCDoan", cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName,
                        Commons.Modules.TypeLanguage, iChuyenSuDung, iOrd, dtNgay));
                dtCD = dt;
                if (grdCD.DataSource == null)
                {
                    //Commons.Modules.ObjSystems.MLoadXtraGrid(grdCD, grvCD, dt, windowsUIButton.Buttons[3].Properties.Visible, false, false, true, true, this.Name);
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdCD, grvCD, dt, false, true, false, true, true, this.Name);
                    grvCD.Columns["TEN_CD"].OptionsColumn.AllowFocus = false;
                    grvCD.Columns["TEN_CD"].OptionsColumn.ReadOnly = true;
                    grvCD.Columns["ID_CN"].Visible = false;

                    grvCD.Columns["SO_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvCD.Columns["SO_LUONG"].DisplayFormat.FormatString = "N0";
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

                //cboMQL.AutoSearchColumnIndex = 1;
                //cboMQL.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
                //cboMQL.HeaderClickMode = DevExpress.XtraEditors.Controls.HeaderClickMode.AutoSearch;
                //cboMQL.CaseSensitiveSearch = true;
                //cboMQL.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered;

                //cboMQL.ShowLines
                grvCD.Columns["ID_CD"].ColumnEdit = cboMQL;
                cboMQL.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
                cboMQL.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;


                //DataTable dtMQL = new DataTable();
                //dtMQL.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT T1.ID_CD, T1.MaQL, T1.TEN_CD_QT AS TEN_CD FROM QUI_TRINH_CONG_NGHE_CHI_TIET T1 LEFT JOIN PHIEU_CONG_DOAN T2 ON T1.ID_CD = T2.ID_CD"));
                //Commons.Modules.ObjSystems.AddCombXtra("ID_CD", "MaQL", grvCD, dtMQL, "ID_CD", this.Name);

                //for (int i = 0; i < grvCD.Columns.Count; i++)
                //{
                //    grvCD.Columns["TEN_CD"].OptionsColumn.AllowEdit = false;
                //}

            }
            catch
            { }
        }

        private void LoadCboMSCN()
        {
            try
            {
                DateTime dtNgay;
                try
                {
                    dtNgay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                }
                catch { dtNgay = DateTime.Now; }

                //optXCLP.SelectedIndex = 0  XEM CU

                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spPCDGetCNhan", cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName,
                        Commons.Modules.TypeLanguage, XemCu, iChuyen, iOrd, dtNgay));
                if (cboMSCN.Properties.DataSource == null)
                {
                    //Commons.Modules.ObjSystems.MLoadLookUpEditN(cboMSCN, dt, "ID_CN", "MS_CN", "MS_CN", "");

                    cboMSCN.Properties.DataSource = dt;
                    cboMSCN.Properties.PopulateViewColumns();
                    cboMSCN.Properties.View.PopulateColumns();
                    cboMSCN.Properties.DisplayMember = "MS_CN";
                    cboMSCN.Properties.ValueMember = "ID_CN";

                    cboMSCN.Properties.View.PopulateColumns(cboMSCN.Properties.DataSource);
                    cboMSCN.Properties.View.Columns["ID_CN"].Visible = false;
                    cboMSCN.Properties.View.Columns["HO_TEN"].Visible = false;
                    cboMSCN.Properties.View.Columns["CDL"].Visible = false;
                    try { cboMSCN.Properties.View.Columns["MS_CN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "MS_CN"); } catch { }

                    cboMSCN.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
                    cboMSCN.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                    cboMSCN.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                    cboMSCN.Properties.ImmediatePopup = true;
                }
                else
                {
                    cboMSCN.Properties.DataSource = dt;
                }

                cboMSCN.GotFocus += cboMSCN_Click;
            }
            catch { }
        }
        private void cboMSCN_Click(object sender, EventArgs e)
        {
            cboMSCN.SelectAll();
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
            iChuyen = grvPCD.GetFocusedRowCellValue("ID_CHUYEN") == null ? -1 : Convert.ToInt32(grvPCD.GetFocusedRowCellValue("ID_CHUYEN").ToString());
            iOrd = grvPCD.GetFocusedRowCellValue("ID_ORD") == null ? -1 : Convert.ToInt32(grvPCD.GetFocusedRowCellValue("ID_ORD").ToString());
            LoadCD();
            LoadCN();
            LoadCboMSCN();
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
                if (sIDCN != "-1") sDK = " ID_CN = '" + sIDCN + "' ";

                dtTmp.DefaultView.RowFilter = sDK;

            }
            catch { }
            try
            {
                cboMSCN.EditValue = string.IsNullOrEmpty(grvTo.GetFocusedRowCellValue("ID_CN").ToString()) ? -1 : Convert.ToInt64(grvTo.GetFocusedRowCellValue("ID_CN"));
            }
            catch { }

            try
            {
                txtTEN_CN.EditValue = string.IsNullOrEmpty(grvTo.GetFocusedRowCellValue("HO_TEN").ToString()) ? null : grvTo.GetFocusedRowCellValue("HO_TEN").ToString();
            }
            catch { }

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
            windowsUIButton.Buttons[5].Properties.Visible = !TSua;
            windowsUIButton.Buttons[8].Properties.Visible = !TSua;

            windowsUIButton.Buttons[6].Properties.Visible = TSua;
            windowsUIButton.Buttons[7].Properties.Visible = TSua;


            cboDV.Properties.ReadOnly = TSua;
            cboXN.Properties.ReadOnly = TSua;
            cboTo.Properties.ReadOnly = TSua;
            cboChuyen.Properties.ReadOnly = TSua;
            cboNgay.Properties.ReadOnly = TSua;

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
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT  T.ID_XN,T.TEN_XN FROM (SELECT  DISTINCT  STT_DV, STT_XN, ID_XN, TEN_XN  AS TEN_XN  FROM dbo.MGetToUser('" + Commons.Modules.UserName + "', " + Commons.Modules.TypeLanguage + ") WHERE (ID_DV = " + cboDV.EditValue + " OR " + cboDV.EditValue + " = -1) AND ID_LOAI_CHUYEN IN(1, 2, 3, 4, 5, 6, 7) UNION SELECT - 1, -1, -1, '< All >') T ORDER BY T.STT_DV, T.STT_XN"));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboXN, dt, "ID_XN", "TEN_XN", "TEN_XN");

                dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT T2.ID_TO, T2.TEN_TO FROM (SELECT  ID_TO, TEN_TO, STT_TO FROM dbo.MGetToUser('" + Commons.Modules.UserName + "', " + Commons.Modules.TypeLanguage + ") WHERE ID_LOAI_CHUYEN IN(1, 2, 3, 4, 5, 6, 7) AND(ID_DV = " + cboDV.EditValue + " OR " + cboDV.EditValue + " = -1) AND(ID_XN = " + cboXN.EditValue + " OR " + cboXN.EditValue + " = -1) UNION SELECT - 1, '< All >', -1) T2 ORDER BY  STT_TO "));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTo, dt, "ID_TO", "TEN_TO", "TEN_TO");
                LoadChuyen();
            }
            catch { }

        }

        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT  ID_TO, TEN_TO FROM dbo.MGetToUser('" + Commons.Modules.UserName + "'," + Commons.Modules.TypeLanguage + ") WHERE ID_LOAI_CHUYEN IN (1,2,3,4,5,6,7) AND (ID_DV = " + cboDV.EditValue + " OR " + cboDV.EditValue + " =-1) AND (ID_XN =" + cboXN.EditValue + " OR " + cboXN.EditValue + " = -1) UNION SELECT - 1, '< All >' ORDER BY  TEN_TO"));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTo, dt, "ID_TO", "TEN_TO", "TEN_TO");
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadPCD();
            LoadCN();
            LoadCboMSCN();
            LoadCD();
            grvTo_FocusedRowChanged(null, null);
        }

        private void cboNgay_EditValueChanged_1(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadPCD();
            LoadCD();
            LoadCN();
            LoadCboMSCN();
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
                    grvCD.SetFocusedRowCellValue("ID_CD", dt.Rows[0]["ID_CD"]);
                    grvCD.SetFocusedRowCellValue("ID_CN", grvTo.GetFocusedRowCellValue("ID_CN"));
                }
                catch
                {
                }
            }

            if (chkKT.Checked == true)
            {
                return;
            }
            if (view.FocusedColumn.FieldName == "SO_LUONG")
            {
                string sBT_CD = "sBT_CD" + Commons.Modules.UserName;
                try
                {
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT_CD, dtCD, "");
                    System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetSLNhapCD", conn);
                    cmd.Parameters.Add("@BangTam", SqlDbType.NVarChar).Value = sBT_CD;
                    cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                    cmd.Parameters.Add("@SLnhap", SqlDbType.Int).Value = e.Value;
                    cmd.Parameters.Add("@ID_Chuyen", SqlDbType.BigInt).Value = Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_CHUYEN"));
                    cmd.Parameters.Add("@ID_Ord", SqlDbType.BigInt).Value = string.IsNullOrEmpty(grvPCD.GetFocusedRowCellValue("ID_ORD").ToString()) ? -1 : Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_ORD"));
                    cmd.Parameters.Add("@ID_CD", SqlDbType.BigInt).Value = string.IsNullOrEmpty(grvCD.GetFocusedRowCellValue("ID_CD").ToString()) ? -1 : Convert.ToInt64(grvCD.GetFocusedRowCellValue("ID_CD"));
                    cmd.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);


                    DataTable dt = new DataTable();
                    dt = ds.Tables[0].Copy();

                    //Kiểm tra số lượng công đoạn đang nhập có vượt số lượng chốt hay không
                    if (Convert.ToInt32(dt.Rows[0]["SL_NHAP"]) > Convert.ToInt32(grvPCD.GetFocusedRowCellValue("SL_CHOT")))
                    {
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_VuotSLChot"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            e.Valid = false;
                            e.ErrorText = Commons.Modules.TypeLanguage == 0 ? "Số lượng đã vượt số lượng chốt" : "The number has exceeded the number of pins";
                            view.SetColumnError(view.Columns["SO_LUONG"], e.ErrorText);
                            return;
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
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "thuathieu":
                    {
                        DataTable dt = new DataTable();
                        try
                        {
                            ////Load combo DHB, MH
                            string strSQL = "SELECT ID_ORD, ID_DT FROM dbo.DON_HANG_BAN_ORDER WHERE ID_ORD = " + Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_ORD")) + "";
                            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, strSQL));
                        }
                        catch
                        { }

                        //Form.frmThuaThieuSL frm = new Form.frmThuaThieuSL(Convert.ToInt64(dt.Rows[0]["ID_DHB"]), Convert.ToInt64(dt.Rows[0]["ID_HH"]), Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_CHUYEN")), Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_CHUYEN_SD")), Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_ORD")), DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures));
                        Form.frmThuaThieuSL frm = new Form.frmThuaThieuSL();
                        frm.Size = new Size(900, 600);
                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.Size = new Size((this.Width / 2) + (frm.Width / 2), (this.Height / 2) + (frm.Height / 2));
                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.Location = new Point(this.Width / 2 - frm.Width / 2 + this.Location.X,
                                                  this.Height / 2 - frm.Height / 2 + this.Location.Y);

                        frm.iID_DHB = Convert.ToInt64(dt.Rows[0]["ID_DT"]);
                        frm.iID_MH = Convert.ToInt64(dt.Rows[0]["ID_ORD"]);
                        frm.iID_CHUYEN = Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_CHUYEN"));
                        frm.iID_CHUYEN_SD = Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_CHUYEN_SD"));
                        frm.iID_ORD = -1;
                        frm.Ngay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);

                        iIDPCD_TEMP = Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_TEMP"));
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            LoadPCD();
                        }
                        else
                        {
                            if (frm.iID_CD_TMP != -1)
                            {
                                LoadPCD();
                            }
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
                        //DateTime dThang = Convert.ToDateTime(cboNgay.EditValue);
                        DateTime dThang = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                        frm.dThang = Convert.ToDateTime("01/" + dThang.Month + "/" + dThang.Year);
                        frm.ShowDialog();
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

                        iChuyen = Convert.ToInt32(grvPCD.GetFocusedRowCellValue("ID_CHUYEN"));
                        iChuyenSuDung = Convert.ToInt32(grvPCD.GetFocusedRowCellValue("ID_CHUYEN_SD"));
                        iOrd = Convert.ToInt32(grvPCD.GetFocusedRowCellValue("ID_ORD"));

                        frmInBaoCaoPCD frm = new frmInBaoCaoPCD(Ngay, Convert.ToInt64(iChuyen), Convert.ToInt64(iChuyenSuDung), Convert.ToInt64(iOrd));
                        //frm.Size = new Size(750, 213);
                        //frm.StartPosition = FormStartPosition.CenterParent;
                        //frm.Size = new Size((this.Width / 2) + (frm.Width / 2), (this.Height / 2) + (frm.Height / 2));
                        //frm.StartPosition = FormStartPosition.CenterParent;
                        //frm.Location = new Point(this.Width / 2 - frm.Width / 2 + this.Location.X,
                        //                          this.Height / 2 - frm.Height / 2 + this.Location.Y);

                        frm.ShowDialog();
                        break;
                    }
                case "luu":
                    {
                        string stbCongNhan = "BTPCD" + Commons.Modules.UserName;
                        //DateTime ngay = Convert.ToDateTime(cboNgay.EditValue);
                        DateTime ngay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                        //dtNgay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                        try
                        {
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbCongNhan, (DataTable)grdCD.DataSource, "");

                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSavePhieuCongDoan", stbCongNhan, iChuyen, iChuyenSuDung, iOrd, ngay.ToString("yyyyMMdd"));
                            Commons.Modules.ObjSystems.XoaTable(stbCongNhan);
                        }
                        catch (Exception ex) { }

                        TSua(false);
                        LoadCD();
                        LoadCN();
                        LoadCboMSCN();
                        grvTo_FocusedRowChanged(null, null);
                        break;
                    }
                case "khongluu":
                    {
                        Commons.Modules.sPS = "";
                        TSua(false);
                        XemCu = 0;
                        Commons.Modules.ObjSystems.DeleteAddRow(grvCD);
                        grvCD.UpdateCurrentRow();
                        LoadPCD();
                        LoadCD();
                        LoadCN();
                        LoadCboMSCN();
                        cboNgay_EditValueChanged_1(null, null);
                        grvTo_FocusedRowChanged(null, null);
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

        private void grvCD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (windowsUIButton.Buttons[1].Properties.Visible)
                {
                    return;
                }
                grvCD.DeleteSelectedRows();
            }
            if (e.KeyCode == Keys.Home)
            {
                cboMSCN.Focus();
            }
        }



        //private void txtMSCN_TextChanged(object sender, EventArgs e)
        //{
        //try
        //{
        //    if (txtMSCN.Text == "")
        //    {
        //        Commons.Modules.sLoad = "";
        //    }
        //    if (Commons.Modules.sLoad == "0Short") return;
        //    DataTable dt = (DataTable)(grdTo.DataSource);
        //    if (txtMSCN.Text.Length > 0)
        //    {
        //        string TextSearch = string.Format("MS_CN LIKE '%{0}%'", txtMSCN.Text);

        //        try
        //        {
        //            dt.DefaultView.RowFilter = TextSearch;
        //        }
        //        catch { dt.DefaultView.RowFilter = ""; }
        //    }
        //    else { dt.DefaultView.RowFilter = ""; }
        //}
        //catch
        //{ }
        //}
        private void cboMSCN_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                dtTmp = (DataTable)grdTo.DataSource;
                int index = dtTmp.Rows.IndexOf(dtTmp.Rows.Find(cboMSCN.EditValue));
                grvTo.FocusedRowHandle = grvTo.GetRowHandle(index);
            }
            catch { }

        }
        private void cboMSCN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                grvCD.Focus();
                grvCD.FocusedColumn = grvCD.Columns["ID_CD"];
                grvCD.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            }

        }
    }

}