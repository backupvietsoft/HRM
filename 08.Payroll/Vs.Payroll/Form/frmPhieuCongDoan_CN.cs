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
    public partial class frmPhieuCongDoan_CN : DevExpress.XtraEditors.XtraUserControl
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
        public frmPhieuCongDoan_CN()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, windowsUIButton);
        }
        string sBT = "PCDTmp" + Commons.Modules.iIDUser;
        CultureInfo cultures = new CultureInfo("en-US");

        private void frmPhieuCongDoan_CN_Load(object sender, EventArgs e)
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
                LoadCD();
                LoadCN();
                Commons.Modules.sLoad = "";
                grvPCD_FocusedRowChanged(null, null);
                grvCD_FocusedRowChanged(null, null);
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
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spPCDGetCNhan_CN", cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName,
                        Commons.Modules.TypeLanguage, XemCu, cboChuyen.EditValue, iOrd, dtNgay, sBT1));
                //dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CN"] };
                //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboCN, dt, "MS_CN", "LMS", "LMS");
                if (grdCN.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdCN, grvCN, dt, false, true, false, true, true, this.Name);
                    grvCN.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                    grvCN.Columns["HO_TEN"].OptionsColumn.AllowFocus = false;
                    grvCN.Columns["HO_TEN"].OptionsColumn.ReadOnly = true;
                    grvCN.Columns["ID_CHUYEN"].Visible = false;
                    grvCN.Columns["ID_CHUYEN_SD"].Visible = false;
                    grvCN.Columns["ID_CD"].Visible = false;
                    grvCN.Columns["ID_ORD"].Visible = false;
                }
                else
                {
                    grdCN.DataSource = dt;
                }

                cboMQL = new RepositoryItemLookUpEdit();
                dtMQL = new DataTable();
                dtMQL.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spQTCNGetCongNhan_CN", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboTo.EditValue));
                cboMQL.NullText = "";
                cboMQL.ValueMember = "ID_CN";
                cboMQL.DisplayMember = "MS_CN";
                cboMQL.DataSource = dtMQL;
                cboMQL.Columns.Clear();

                cboMQL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_CN"));
                cboMQL.Columns["ID_CN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "ID_CN");
                cboMQL.Columns["ID_CN"].Visible = false;

                cboMQL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MS_CN"));
                cboMQL.Columns["MS_CN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "MS_CN");

                cboMQL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HO_TEN"));
                cboMQL.Columns["HO_TEN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "HO_TEN");

                cboMQL.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboMQL.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                grvCN.Columns["ID_CN"].ColumnEdit = cboMQL;
                cboMQL.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
                cboMQL.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
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
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spPCDGetCDoan_CN", cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName,
                        Commons.Modules.TypeLanguage, XemCu, iChuyenSuDung, iOrd, dtNgay, sBT));
                dt.Columns["ID_CD"].ReadOnly = false;
                Commons.Modules.ObjSystems.XoaTable(sBT);
                if (grdCD.DataSource == null)
                {
                    //Commons.Modules.ObjSystems.MLoadXtraGrid(grdCD, grvCD, dt, windowsUIButton.Buttons[3].Properties.Visible, false, false, true, true, this.Name);
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdCD, grvCD, dt, false, true, false, true, true, this.Name);

                    grvCD.Columns["ID_CHUYEN_SD"].Visible = false;
                    grvCD.Columns["ID_ORD"].Visible = false;
                    grvCD.Columns["ID_CHUYEN_SD"].Visible = false;
                    grvCD.Columns["ID_CD"].Visible = false;
                    grvCD.Columns["SL_CN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvCD.Columns["SL_CN"].DisplayFormat.FormatString = "N0";
                }
                else
                {
                    grdCD.DataSource = dt;
                }
            }
            catch (Exception ex)
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
            grvCN.UpdateCurrentRow();

            iChuyenSuDung = grvPCD.GetFocusedRowCellValue("ID_CHUYEN_SD") == null ? -1 : Convert.ToInt32(grvPCD.GetFocusedRowCellValue("ID_CHUYEN_SD").ToString());
            iOrd = grvPCD.GetFocusedRowCellValue("ID_ORD") == null ? -1 : Convert.ToInt32(grvPCD.GetFocusedRowCellValue("ID_ORD").ToString());
            //LoadCN();

            #region filter CN
            if (Commons.Modules.sLoad == "0Load") return;
            DataTable dtTmp = new DataTable();
            dtTmp = (DataTable)grdCD.DataSource;
            String sIDCN;
            try
            {
                string sDK = "";
                sIDCN = "";
                try
                {
                    sDK = "ID_ORD = '" + iOrd + "' AND ID_CHUYEN_SD = '" + iChuyenSuDung + "' ";
                }
                catch
                {
                    sDK = "1 = 0";
                }
                dtTmp.DefaultView.RowFilter = sDK;
            }
            catch (Exception ex) { }
            #endregion
            cboMQL = new RepositoryItemLookUpEdit();
            dtMQL = new DataTable();
            dtMQL.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spQTCNGetCongNhan_CN", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboTo.EditValue));
            cboMQL.NullText = "";
            cboMQL.ValueMember = "ID_CN";
            cboMQL.DisplayMember = "MS_CN";
            cboMQL.DataSource = dtMQL;
            cboMQL.Columns.Clear();

            cboMQL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_CN"));
            cboMQL.Columns["ID_CN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "ID_CN");
            cboMQL.Columns["ID_CN"].Visible = false;

            cboMQL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MS_CN"));
            cboMQL.Columns["MS_CN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "MS_CN");

            cboMQL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HO_TEN"));
            cboMQL.Columns["HO_TEN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "HO_TEN");

            cboMQL.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            cboMQL.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

            grvCN.Columns["ID_CN"].ColumnEdit = cboMQL;
            cboMQL.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            cboMQL.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            grvCD_FocusedRowChanged(null, null);
        }

        private void grvCD_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            DataTable dtTmp = new DataTable();
            dtTmp = (DataTable)grdCN.DataSource;
            String sIDCD;
            try
            {
                string sDK = "";
                sIDCD = "-1";
                try
                {
                    sIDCD = grvCD.GetFocusedRowCellValue("ID_CD").ToString();
                }
                catch { }
                if (sIDCD != "-1")
                {
                    //if (XemCu == 0)
                    //{
                    //    sDK = " ID_CN = '" + sIDCN + "' AND ID_CHUYEN_SD = '" + iChuyenSuDung + "' AND ID_ORD = '" + iOrd + "' ";
                    //}
                    //else
                    //{
                    //    sDK = " ID_CN = '" + sIDCN + "' AND ID_CHUYEN_SD = '" + iChuyenSuDung + "' AND ID_ORD = '" + iOrd + "' ";
                    //}
                    sDK = " ID_CD = '" + sIDCD + "' AND ID_CHUYEN_SD = '" + iChuyenSuDung + "' AND ID_ORD = '" + iOrd + "' ";
                }
                else
                {
                    sDK = "1 = 0";
                }

                dtTmp.DefaultView.RowFilter = sDK;

            }
            catch (Exception ex) { }

            if (Commons.Modules.sPS != "0Focus")
            {
                iCN = Convert.ToInt32(grvCD.GetFocusedRowCellValue("ID_CN"));
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
                LoadCD();
                LoadCN();
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
            LoadCD();
            LoadCN();
            Commons.Modules.sLoad = "";
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadPCD();
            LoadCD();
            LoadCN();
            grvPCD_FocusedRowChanged(null, null);
            grvCD_FocusedRowChanged(null, null);
        }

        private void cboNgay_EditValueChanged_1(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadPCD();
            LoadCD();
            LoadCN();
            grvPCD_FocusedRowChanged(null, null);
            grvCD_FocusedRowChanged(null, null);
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

        private void grvCN_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            grvCN.ClearColumnErrors();
            GridView view = sender as GridView;

            if (view.FocusedColumn.FieldName == "ID_CN")
            {
                if (Commons.Modules.ObjSystems.ConvertDatatable(grdCN).AsEnumerable().Where(x => x["ID_CD"].ToString().Trim().Equals(grvCD.GetFocusedRowCellValue("ID_CD").ToString().Trim())).Count(x => x["ID_CN"].ToString().Trim().Equals(e.Value.ToString().Trim())) >= 1)
                {
                    e.Valid = false;
                    e.ErrorText = Commons.Modules.TypeLanguage == 0 ? "Trùng" : "Duplicate";
                    view.SetColumnError(view.Columns["ID_CN"], e.ErrorText);
                    return;
                }
                DataTable dt = dtMQL.AsEnumerable().Where(x => x["ID_CN"].ToString().Equals(e.Value.ToString())).CopyToDataTable();
                try
                {

                    grvCN.SetFocusedRowCellValue("HO_TEN", dt.Rows[0]["HO_TEN"]);
                    grvCN.SetFocusedRowCellValue("ID_CD", grvCD.GetFocusedRowCellValue("ID_CD"));
                    grvCN.SetFocusedRowCellValue("ID_CN", dt.Rows[0]["ID_CN"]);
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
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT_CD, Commons.Modules.ObjSystems.ConvertDatatable(grvCN), "");
                    System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetSLNhapCD", conn);
                    cmd.Parameters.Add("@BangTam", SqlDbType.NVarChar).Value = sBT_CD;
                    cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                    cmd.Parameters.Add("@SLnhap", SqlDbType.Int).Value = e.Value;
                    cmd.Parameters.Add("@ID_Chuyen", SqlDbType.BigInt).Value = Convert.ToInt64(grvCN.GetFocusedRowCellValue("ID_CHUYEN"));
                    cmd.Parameters.Add("@ID_Ord", SqlDbType.BigInt).Value = string.IsNullOrEmpty(grvCN.GetFocusedRowCellValue("ID_ORD").ToString()) ? -1 : Convert.ToInt64(grvCN.GetFocusedRowCellValue("ID_ORD"));
                    cmd.Parameters.Add("@ID_CD", SqlDbType.BigInt).Value = string.IsNullOrEmpty(grvCN.GetFocusedRowCellValue("ID_CD").ToString()) ? -1 : Convert.ToInt64(grvCN.GetFocusedRowCellValue("ID_CD"));
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
                        else
                        {
                            grvCN.SetFocusedRowCellValue("SO_LUONG", e.Value);
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
        private void grvCN_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }
        private void grvCN_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
        private void grvCN_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
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
                            //Form.frmThuaThieuSL frm = new Form.frmThuaThieuSL(Convert.ToInt64(dt.Rows[0]["ID_DHB"]), Convert.ToInt64(dt.Rows[0]["ID_HH"]), Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_CHUYEN")), Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_CHUYEN_SD")), Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_ORD")), DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures));
                            Form.frmThuaThieuSL frm = new Form.frmThuaThieuSL();
                            frm.iID_DV = Convert.ToInt32(cboDV.EditValue);
                            //frm.iID_CHUYEN = Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_CHUYEN"));
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
                            //DateTime dThang = Convert.ToDateTime(cboNgay.EditValue);
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
                            Commons.Modules.ObjSystems.AddnewRow(grvCN, true);
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
                            grvCN.CloseEditor();
                            grvCN.UpdateCurrentRow();

                            DataTable dtSoure = new DataTable();
                            dtSoure = (DataTable)grdCN.DataSource;
                            if (!KiemTraLuoi(dtSoure)) return;
                            string stbCongNhan = "BTPCD" + Commons.Modules.UserName;
                            //DateTime ngay = Convert.ToDateTime(cboNgay.EditValue);
                            DateTime ngay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                            //dtNgay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                            try
                            {
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbCongNhan, (DataTable)grdCN.DataSource, "");

                                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSavePhieuCongDoan", stbCongNhan, iChuyen, cboChuyen.EditValue, iOrd, ngay.ToString("yyyyMMdd"));
                                Commons.Modules.ObjSystems.XoaTable(stbCongNhan);
                            }
                            catch (Exception ex) { }

                            TSua(false);
                            XemCu = 0;
                            Commons.Modules.ObjSystems.DeleteAddRow(grvCN);
                            grvCN.UpdateCurrentRow();
                            cboNgay_EditValueChanged_1(null, null);
                            
                            break;
                        }
                    case "khongluu":
                        {
                            Commons.Modules.sPS = "";
                            TSua(false);
                            XemCu = 0;
                            Commons.Modules.ObjSystems.DeleteAddRow(grvCN);
                            grvCN.UpdateCurrentRow();
                            
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

        private void grvCN_KeyDown(object sender, KeyEventArgs e)
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

        private void grvCN_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                grvCN.SetFocusedRowCellValue("ID_CHUYEN_SD", grvPCD.GetFocusedRowCellValue("ID_CHUYEN_SD"));
                grvCN.SetFocusedRowCellValue("ID_ORD", grvPCD.GetFocusedRowCellValue("ID_ORD"));
                grvCD.SetFocusedRowCellValue("SL_CN", grvCN.RowCount);

            }
            catch { }
        }

        private void grdCN_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                //grvCD.SetFocusedRowCellValue("ID_CD", cboMQL.GetDataSourceValue("ID_CD", 1));
                if (e.KeyCode == Keys.Delete && !windowsUIButton.Buttons[0].Properties.Visible)
                {
                    grvCN.DeleteSelectedRows();
                    ((DataTable)grdCN.DataSource).AcceptChanges();
                    DataTable dt = new DataTable();
                    dt = Commons.Modules.ObjSystems.ConvertDatatable(grvCN);
                    grvCD.SetFocusedRowCellValue("SL_CN", dt.Rows.Count == 0 ? (object)(DBNull.Value) : dt.Rows.Count);
                }
            }
            catch { }
        }

        private void grvPCD_RowCountChanged(object sender, EventArgs e)
        {
            grvPCD_FocusedRowChanged(null, null);
        }

        private void grvCN_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //try
            //{
            //    GridView view = sender as GridView;
            //    if (e.Column.FieldName == "SO_LUONG")
            //    {
            //        int iSLNhap = 0;
            //        iSLNhap = Convert.ToInt32(e.Value);
            //        if (iSLNhap == 0)
            //        {
            //            //string sError = Commons.Modules.TypeLanguage == 0 ? "Số lượng phải lớn hơn 0" : "The number must be greater than 0";
            //            //view.SetColumnError(view.Columns["SO_LUONG"], sError);
            //            bKiemSL = true;
            //            return;
            //        }
            //    }
            //    bKiemSL = false;
            //}
            //catch
            //{
            //    bKiemSL = true;
            //}
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


                if (!KiemDuLieuSo(grvCN, dr, "SO_LUONG", grvCN.Columns["SO_LUONG"].FieldName.ToString(), 0, 0, true, this.Name))
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
                            if (DLKiem < GTSoSanh)
                            {
                                dr.SetColumnError(sCot, sTenKTra + Commons.Modules.ObjLanguages.GetLanguage(sForm, "msgKhongNhoHon") + GTSoSanh.ToString());
                                dr["XOA"] = 1;
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
        private void grvCN_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
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
                grvCN.Focus();
                grvCN.FocusedColumn = grvCN.Columns["ID_CN"];
                grvCN.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
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
    }

}