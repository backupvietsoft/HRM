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
using DevExpress.CodeParser;
using NPOI.OpenXmlFormats.Dml.Diagram;

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
        public static frmPhieuCongDoan_CN _instance;
        public static frmPhieuCongDoan_CN Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new frmPhieuCongDoan_CN();
                return _instance;
            }
        }
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
                Commons.Modules.ObjSystems.DeleteAddRow(grvCN);
                TSua(false);

                Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
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
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboChuyen, Commons.Modules.ObjSystems.DataToTheoLoaiChuyen(Convert.ToInt32(cboDV.EditValue), Convert.ToInt32(cboXN.EditValue), false), "ID_TO", "TEN_TO", "TEN_TO");
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
                cmd.Parameters.Add("@ID_CHUYEN", SqlDbType.Int).Value = Convert.ToString(cboChuyen.EditValue) == "" ? -1 : cboChuyen.EditValue;
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
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdPCD, grvPCD, dt, false, true, true, true, true, this.Name);
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
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spPCDGetCNhan_CN", cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName,
                        Commons.Modules.TypeLanguage, XemCu, Convert.ToString(cboChuyen.EditValue) == "" ? -1 : cboChuyen.EditValue, iOrd, dtNgay, sBT1));
                dt.Columns["MS_CN"].ReadOnly = false;
                //dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CN"] };
                //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboCN, dt, "MS_CN", "LMS", "LMS");
                dt.Columns["THANH_TIEN"].ReadOnly = false;
                if (grdCN.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdCN, grvCN, dt, false, true, false, true, true, this.Name);
                    grvCN.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                    grvCN.Columns["HO_TEN"].OptionsColumn.AllowFocus = false;
                    grvCN.Columns["HO_TEN"].OptionsColumn.ReadOnly = true;
                    grvCN.Columns["DON_GIA"].OptionsColumn.AllowFocus = false;
                    grvCN.Columns["DON_GIA"].OptionsColumn.ReadOnly = true;
                    grvCN.Columns["THANH_TIEN"].OptionsColumn.AllowFocus = false;
                    grvCN.Columns["THANH_TIEN"].OptionsColumn.ReadOnly = true;
                    grvCN.Columns["ID_CHUYEN"].Visible = false;
                    grvCN.Columns["ID_CHUYEN_SD"].Visible = false;
                    grvCN.Columns["ID_CD"].Visible = false;
                    grvCN.Columns["ID_ORD"].Visible = false;
                    grvCN.Columns["ID_CN"].Visible = false;

                    grvCN.Columns["SO_LUONG"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvCN.Columns["SO_LUONG"].DisplayFormat.FormatString = "N0";

                    grvCN.Columns["THANH_TIEN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvCN.Columns["THANH_TIEN"].DisplayFormat.FormatString = "N0";
                }
                else
                {
                    grdCN.DataSource = dt;
                }

                //cboMQL = new RepositoryItemLookUpEdit();
                dtMQL = new DataTable();
                dtMQL.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spQTCNGetCongNhan_CN", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Convert.ToDateTime(cboNgay.EditValue)));
                //cboMQL.NullText = "";
                //cboMQL.ValueMember = "ID_CN";
                //cboMQL.DisplayMember = "MS_CN";
                //cboMQL.DataSource = dtMQL;
                //cboMQL.Columns.Clear();

                //cboMQL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_CN"));
                //cboMQL.Columns["ID_CN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "ID_CN");
                //cboMQL.Columns["ID_CN"].Visible = false;

                //cboMQL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MS_CN"));
                //cboMQL.Columns["MS_CN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "MS_CN");

                //cboMQL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MS_CN_4"));
                //cboMQL.Columns["MS_CN_4"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "MS_CN_4");

                //cboMQL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HO_TEN"));
                //cboMQL.Columns["HO_TEN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "HO_TEN");

                //cboMQL.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                //cboMQL.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                //grvCN.Columns["ID_CN"].ColumnEdit = cboMQL;
                //cboMQL.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
                //cboMQL.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;


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
                dt.Columns["SL_CN"].ReadOnly = false;
                dt.Columns["TONG_SAN_LUONG"].ReadOnly = false;

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
        private void LoadTextTongLSP()
        {
            try
            {
                DataView dt = (DataView)grvCN.DataSource;
                if (dt == null)
                {
                    return;
                }
                DataTable dt1 = new DataTable();
                dt1 = dt.ToTable();

                lbl.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTongLSP") + " " + (Convert.ToDouble(dt1.Compute("Sum(THANH_TIEN)", "")).ToString("N0") == "" ? "0" : Convert.ToDouble(dt1.Compute("Sum(THANH_TIEN)", "")).ToString("N0")).ToString();
            }
            catch
            {
                lbl.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lblTongLSP") + " 0";
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
                //dtTmp.DefaultView.Sort = "MaQL ASC";
            }
            catch (Exception ex) { }
            #endregion
            //cboMQL = new RepositoryItemLookUpEdit();
            dtMQL = new DataTable();
            dtMQL.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spQTCNGetCongNhan_CN", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Convert.ToDateTime(cboNgay.EditValue)));
            //cboMQL.NullText = "";
            //cboMQL.ValueMember = "ID_CN";
            //cboMQL.DisplayMember = "MS_CN";
            //cboMQL.DataSource = dtMQL;
            //cboMQL.Columns.Clear();

            //cboMQL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_CN"));
            //cboMQL.Columns["ID_CN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "ID_CN");
            //cboMQL.Columns["ID_CN"].Visible = false;

            //cboMQL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MS_CN"));
            //cboMQL.Columns["MS_CN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "MS_CN");

            //cboMQL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MS_CN_4"));
            //cboMQL.Columns["MS_CN_4"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "MS_CN_4");

            //cboMQL.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HO_TEN"));
            //cboMQL.Columns["HO_TEN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "HO_TEN");

            //cboMQL.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //cboMQL.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

            //grvCN.Columns["ID_CN"].ColumnEdit = cboMQL;
            //cboMQL.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            //cboMQL.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
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
            LoadTextTongLSP();
            if (Commons.Modules.sPS != "0Focus")
            {
                iCN = Convert.ToInt32(grvCD.GetFocusedRowCellValue("ID_CN"));
            }

            else return;
        }

        private void TSua(Boolean TSua)
        {
            //grdPCD.Enabled = !TSua;
            if (Commons.Modules.ObjSystems.DataTinhTrangBangLuong(Convert.ToInt32(cboDV.EditValue), Commons.Modules.ObjSystems.ConvertDateTime(cboNgay.Text)) == 2)
            {
                windowsUIButton.Buttons[2].Properties.Visible = false;
                windowsUIButton.Buttons[3].Properties.Visible = false;
                windowsUIButton.Buttons[4].Properties.Visible = false;
                windowsUIButton.Buttons[5].Properties.Visible = false;
                windowsUIButton.Buttons[6].Properties.Visible = false;
            }
            else
            {
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
                TSua(false);
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
            TSua(false);
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


            if (view.FocusedColumn.FieldName == "MS_CN")
            {
                DataTable dt = new DataTable();
                try
                {
                    dt = dtMQL.AsEnumerable().Where(x => x["MS_CN"].ToString().Equals(e.Value.ToString())).CopyToDataTable();
                }
                catch
                {
                    e.Valid = false;
                    e.ErrorText = Commons.Modules.TypeLanguage == 0 ? "Không có mã nhân viên" : "Not code employes";
                    view.SetColumnError(view.Columns["MS_CN"], e.ErrorText);
                    return;
                }

                try
                {
                    if (((DataTable)grdCN.DataSource).AsEnumerable().Where(x => x["ID_CD"].ToString().Trim().Equals(grvCD.GetFocusedRowCellValue("ID_CD").ToString().Trim())).Count(x => x["MS_CN"].ToString().Trim().Equals(dt.Rows[0]["MS_CN_4"].ToString().Trim())) >= 1)
                    {
                        e.Valid = false;
                        e.ErrorText = Commons.Modules.TypeLanguage == 0 ? "Trùng" : "Duplicate";
                        view.SetColumnError(view.Columns["MS_CN"], e.ErrorText);
                        return;
                    }
                }
                catch { }

                try
                {
                    grvCN.SetFocusedRowCellValue("ID_CN", dt.Rows[0]["ID_CN"]);
                    grvCN.SetFocusedRowCellValue("HO_TEN", dt.Rows[0]["HO_TEN"]);
                    grvCN.SetFocusedRowCellValue("SO_LUONG", grvPCD.GetFocusedRowCellValue("SL_NGAY"));
                    grvCN.SetFocusedRowCellValue("DON_GIA", grvCD.GetFocusedRowCellValue("DON_GIA"));
                    grvCN.SetFocusedRowCellValue("THANH_TIEN", Convert.ToDouble(grvPCD.GetFocusedRowCellValue("SL_NGAY")) * Convert.ToDouble(grvCD.GetFocusedRowCellValue("DON_GIA")));
                    grvCN.SetFocusedRowCellValue("MS_CN", dt.Rows[0]["MS_CN_4"]);
                }
                catch
                {
                }
            }

            if (chkKT.Checked == false)
            {
                e.Valid = true;
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
                    cmd.Parameters.Add("@ID_Chuyen", SqlDbType.BigInt).Value = Convert.ToInt64(grvCN.GetFocusedRowCellValue("ID_CHUYEN_SD"));
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
                    else
                    {
                        grvCN.SetFocusedRowCellValue("SO_LUONG", e.Value);
                    }
                    Commons.Modules.ObjSystems.XoaTable(sBT_CD);
                }
                catch (Exception ex)
                {
                    Commons.Modules.ObjSystems.XoaTable(sBT_CD);
                }
            }
        }
        private void grvCN_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "SO_LUONG")
                {

                    grvCN.SetFocusedRowCellValue("THANH_TIEN", (Convert.ToDouble(e.Value) * Convert.ToDouble(grvCD.GetFocusedRowCellValue("DON_GIA"))));

                    LoadTextTongLSP();

                    //DataTable tempt = grdCN.DataSource as DataTable;

                    //Int64 ID_CD = string.IsNullOrEmpty(Convert.ToString(grvPCD.GetFocusedRowCellValue("ID_CD"))) ? 0 : Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_CD"));
                    //Int64 ID_CHUYEN_SD = string.IsNullOrEmpty(Convert.ToString(grvPCD.GetFocusedRowCellValue("ID_CHUYEN_SD"))) ? 0 : Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_CHUYEN_SD"));
                    //int ii = string.IsNullOrEmpty(Convert.ToString(tempt.Compute("Sum(SO_LUONG)", "ID_CD = " + ID_CD + " AND ID_CHUYEN_SD = " + ID_CHUYEN_SD + ""))) ? 0 : Convert.ToInt32(tempt.Compute("Sum(SO_LUONG)", "ID_CD = " + ID_CD + " AND ID_CHUYEN_SD = " + ID_CHUYEN_SD + ""));

                    //grvCD.SetFocusedRowCellValue("TONG_SAN_LUONG", ii);

                }
            }
            catch { }
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
                grvCN.SetFocusedRowCellValue("ID_CD", grvCD.GetFocusedRowCellValue("ID_CD"));
            }
            catch { }
        }

        private void grdCN_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    grvCN.Focus();
                    grvCN.FocusedColumn = grvCN.Columns["MS_CN"];
                    grvCN.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;

                    DataTable dt = new DataTable();
                    dt = Commons.Modules.ObjSystems.ConvertDatatable(grvCN);
                    try
                    {
                        grvCD.SetFocusedRowCellValue("TONG_SAN_LUONG", Convert.ToInt32(dt.Compute("Sum(SO_LUONG)", "")));
                    }
                    catch { }

                    int currentRow = grvCD.FocusedRowHandle;
                    grvCD.FocusedRowHandle = currentRow + 1;
                }
                //grvCD.SetFocusedRowCellValue("ID_CD", cboMQL.GetDataSourceValue("ID_CD", 1));
                if (e.KeyCode == Keys.Delete && !windowsUIButton.Buttons[0].Properties.Visible)
                {
                    grvCN.DeleteSelectedRows();
                    ((DataTable)grdCN.DataSource).AcceptChanges();
                    DataTable dt = new DataTable();
                    dt = Commons.Modules.ObjSystems.ConvertDatatable(grvCN);
                    grvCD.SetFocusedRowCellValue("SL_CN", dt.Rows.Count == 0 ? (object)(DBNull.Value) : dt.Rows.Count);

                    LoadTextTongLSP();
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
            //            string sError = Commons.Modules.TypeLanguage == 0 ? "Số lượng phải lớn hơn 0" : "The number must be greater than 0";
            //            view.SetColumnError(view.Columns["SO_LUONG"], sError);
            //            return;
            //        }
            //    }
            //}
            //catch
            //{
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

                        DataTable dt1 = new DataTable();
                        dt1 = (DataTable)grdCD.DataSource;
                        DataRow dr1 = (dt1.AsEnumerable().Where(x => x.Field<Int64>("ID_CD").Equals(Convert.ToInt64(dr["ID_CD"])) && Convert.ToInt64(x["ID_CHUYEN_SD"]) == Convert.ToInt64(dr["ID_CHUYEN_SD"]) && Convert.ToInt64(x["ID_ORD"]) == Convert.ToInt64(dr["ID_ORD"]))).ToList<DataRow>()[0];
                        dr1.SetColumnError("TEN_CD", "Error");

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
                grvCN.FocusedColumn = grvCN.Columns["MS_CN"];
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
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTo, Commons.Modules.ObjSystems.DataToTheoLoaiChuyen(Convert.ToInt32(cboDV.EditValue), Convert.ToInt32(cboXN.EditValue), true), "ID_TO", "TEN_TO", "TEN_TO");
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
                cboTo_EditValueChanged(null, null);
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

        public DXMenuItem MCreateMenuNhapMultyCN(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblChonCongNhan", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(NhapMultyCN));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }
        public void NhapMultyCN(object sender, EventArgs e)
        {
            try
            {
                grvCN.CloseEditor();
                grvCN.UpdateCurrentRow();


                DataTable dtTmp = new DataTable(); // loc du lieu, chỉ lấy những công nhân chưa có trong danh sách view
                dtTmp = dtMQL.Copy();
                try
                {
                    var dt_temp = dtTmp.AsEnumerable().Where(row => !Commons.Modules.ObjSystems.ConvertDatatable(grvCN).AsEnumerable()
                                                             .Select(r => r.Field<Int64>("ID_CN"))
                                                             .Any(x => x == row.Field<Int64>("ID_CN"))
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
                frm.iLoai = 1;
                frm.isoLuong = Convert.ToInt32(grvCN.GetFocusedRowCellValue("SO_LUONG"));
                DataTable dt = new DataTable();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    dt = frm.dtTemp.Copy(); // nhận giữ liệu khi người dùng nhấn lưu
                    string sBTUpdate = "sBTUpdate" + Commons.Modules.iIDUser; // bảng tạm dữ liệu ng dùng chọn công nhân
                    string sBTCurrent = "sBTCurrent" + Commons.Modules.iIDUser; // bảng tạm cả datasoure
                    string sBTFocus = "sBTFocus" + Commons.Modules.iIDUser; // bảng tạm dữ liệud đang focus
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTUpdate, dt, "");
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCurrent, Commons.Modules.ObjSystems.ConvertDatatable(grdCN), "");
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTFocus, Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdCN, grvCN), "");
                    dt = new DataTable();
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spNhapNhanhPCD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1, sBTUpdate, sBTCurrent, sBTFocus));
                    grdCN.DataSource = dt;
                    grvCD_FocusedRowChanged(null, null);

                    dt = new DataTable();
                    dt = Commons.Modules.ObjSystems.ConvertDatatable(grvCN);
                    grvCD.SetFocusedRowCellValue("SL_CN", dt.Rows.Count == 0 ? (object)(DBNull.Value) : dt.Rows.Count); // set lại cột số lượng công nhân
                }
            }
            catch (Exception ex) { }
        }

        private void grvPCD_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (Commons.Modules.iPermission != 1) return;
            try
            {
                if (Commons.Modules.ObjSystems.DataTinhTrangBangLuong(Convert.ToInt32(cboDV.EditValue), Commons.Modules.ObjSystems.ConvertDateTime(cboNgay.Text)) == 2) return;
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

        // chuột phải lưới công nhân
        private void grvCN_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (Commons.Modules.ObjSystems.DataTinhTrangBangLuong(Convert.ToInt32(cboDV.EditValue), Commons.Modules.ObjSystems.ConvertDateTime(cboNgay.Text)) == 2) return;
                if (windowsUIButton.Buttons[0].Properties.Visible || Convert.ToString(grvCN.GetFocusedRowCellValue("ID_CN")) == "") return;
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                int irow = e.HitInfo.RowHandle;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    e.Menu.Items.Clear();
                    DevExpress.Utils.Menu.DXMenuItem itemTTNS = MCreateMenuNhapMultyCN(view, irow);
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

        private void grvCD_RowCountChanged(object sender, EventArgs e)
        {
            grvCD_FocusedRowChanged(null, null);
        }

        private void searchControl2_EditValueChanged(object sender, EventArgs e)
        {
            DataTable dtTmp = new DataTable();
            dtTmp = (DataTable)grdCD.DataSource;
            //dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grvTo);
            String sMSCN;
            try
            {
                string sDK = "";
                sMSCN = "";
                if (searchControl2.Text != "")
                {
                    sDK = "MaQL = '" + Convert.ToString(searchControl2.EditValue) + "' AND ID_CHUYEN_SD = '" + Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_CHUYEN_SD")) + "' AND ID_ORD = '" + Convert.ToInt64(grvPCD.GetFocusedRowCellValue("ID_ORD")) + "'";
                    dtTmp.DefaultView.RowFilter = sDK;
                }
                else
                {
                    grvPCD_FocusedRowChanged(null, null);
                }

                //grvCD_FocusedRowChanged(null, null);
            }
            catch (Exception ex)
            {
                dtTmp.DefaultView.RowFilter = "";
                grvPCD_FocusedRowChanged(null, null);
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