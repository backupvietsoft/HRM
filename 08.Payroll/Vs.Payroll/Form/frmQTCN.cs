using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Vs.Payroll
{
    public partial class frmQTCN : DevExpress.XtraEditors.XtraUserControl
    {
        private bool isAdd = false;
        int id_NHH = 0;
        Decimal hsBT, tgTK, tgQD, dgG, hsDG;

        //string sCnstr = "Server=192.168.2.5;database=DATA_MT;uid=sa;pwd=123;Connect Timeout=0;"; 
        public frmQTCN()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, windowsUIButton);
            optHT.SelectedIndex = 0;
        }

        private void frmQTCN_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            optHT.Properties.Items[0].Description = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "rdoDangSanXuat");
            optHT.Properties.Items[1].Description = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "rdoHoanThanh");
            try
            {

                LoadCbo();
                LoadHD(0);
                LoadLuoi();
                cboCum_EditValueChanged(null, null);
                cboChuyen_EditValueChanged(null, null);
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message.ToString()); }

            Commons.Modules.sLoad = "";
        }

        private void LoadHD(int iLoad)
        {
            Commons.Modules.sLoad = "0LoadCbo";
            String sKH, sDDH, sMH, sOrd;
            sKH = "-1"; sDDH = "-1"; sMH = "-1"; sOrd = "-1";

            try { sKH = cboKH.EditValue.ToString(); } catch { }
            try { sDDH = cboHD.EditValue.ToString(); } catch { }
            try { sMH = cboMH.EditValue.ToString(); } catch { }
            try { sOrd = cboOrd.EditValue.ToString(); } catch { }

            System.Data.SqlClient.SqlConnection conn;
            DataTable dt = new DataTable();

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spQTCNGetCbo", conn);

                cmd.Parameters.Add("@HoanThanh", SqlDbType.Int).Value = optHT.SelectedIndex;
                cmd.Parameters.Add("@sKH", SqlDbType.NVarChar, 50).Value = sKH;
                cmd.Parameters.Add("@sDDH", SqlDbType.NVarChar, 50).Value = sDDH;
                cmd.Parameters.Add("@sMH", SqlDbType.NVarChar, 50).Value = sMH;
                cmd.Parameters.Add("@sOrd", SqlDbType.NVarChar, 50).Value = sOrd;

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);

                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "KHACH_HANG";
                if (iLoad == 0) Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboKH, dt, "ID_DT", "TEN_NGAN", "TEN_NGAN", true);

                dt = new DataTable();
                dt = ds.Tables[1].Copy();
                dt.TableName = "HOP_DONG";
                if (iLoad == 0 || iLoad == 1) Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboHD, dt, "ID_DHB", "SO_DHB", "SO_DHB", true);


                dt = new DataTable();
                dt = ds.Tables[2].Copy();
                dt.TableName = "MA_HANG";
                if (iLoad == 0 || iLoad == 1 || iLoad == 2)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboMH, dt, "ID_HH", "TEN_HH", "TEN_HH", true);
                    cboMH.Properties.View.Columns["ID_NHH"].Visible = false;
                    id_NHH = Convert.ToInt32(dt.Rows[0]["ID_NHH"].ToString());
                }

                dt = new DataTable();
                dt = ds.Tables[3].Copy();
                dt.TableName = "TEN_ORDER";
                if (iLoad == 0 || iLoad == 1 || iLoad == 2 || iLoad == 3) Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboOrd, dt, "ID_DHBORD", "TEN_ORD", "TEN_ORD", true);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }

        }

        private void LoadCbo()
        {
            try
            {
                //string sSql = "SELECT ID_CHUYEN, TEN_CHUYEN FROM CHUYEN UNION SELECT '-1', '' FROM CHUYEN ORDER BY CHUYEN.TEN_CHUYEN";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCHUYEN", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboChuyen, dt, "ID_CHUYEN", "TEN_CHUYEN", "TEN_CHUYEN");
                cboChuyen.Properties.View.Columns[0].Caption = "STT Chuyền";
                cboChuyen.Properties.View.Columns[1].Caption = "Tên Chuyền";
                cboChuyen.Properties.View.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboChuyen.Properties.View.Columns[1].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cboChuyen.Properties.View.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboChuyen.Properties.View.Columns[0].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                //sSql = "SELECT ID_NHH, TEN_NHH FROM NHOM_HANG_HOA UNION SELECT '-1', '' FROM NHOM_HANG_HOA ORDER BY TEN_NHH";
                //dt = new DataTable();
                //dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNhomHangHoa", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                //Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboLMH, dt, "ID_NHH", "TEN_NHH", "TEN_NHH");
                LoadCboCum(id_NHH);
            }
            catch { }
        }

        private void LoadCboCum(int LSP)
        {
            try
            {
                //string sSql = "SELECT ID_CUM, TEN_CUM FROM CUM WHERE ID_NHH = " + cboLMH.EditValue + " UNION SELECT '-1','' FROM CUM ";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCUM", Convert.ToInt64(cboMH.EditValue), Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                if (cboCum.Properties.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboCum, dt, "ID_CUM", "TEN_CUM", "TEN_CUM");
                    cboCum.Properties.View.Columns[0].Caption = "ID cụm";
                    cboCum.Properties.View.Columns[1].Caption = "Tên cụm";
                    cboCum.Properties.View.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboCum.Properties.View.Columns[1].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    cboCum.Properties.View.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboCum.Properties.View.Columns[0].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                }
                else
                {
                    cboCum.Properties.DataSource = dt;
                }
            }
            catch { }
        }

        DataTable dtBT;
        DataTable dtCD, dtLoaiMay, dtChuyen, dtCum, dtCDTemp;
        private void LoadLuoi()
        {
            //Commons.Modules.sLoad = "0Load";
            String sDDH, sMH, sOrd;
            sDDH = "-1"; sMH = "-1"; sOrd = "-1";

            try { sDDH = cboHD.EditValue.ToString(); } catch { }
            try { sMH = cboMH.EditValue.ToString(); } catch { }
            try { sOrd = cboOrd.EditValue.ToString(); } catch { }

            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spQTCNGet", sDDH, sMH, sOrd));

            if (grdQT.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdQT, grvQT, dt, false, false, false, false, true, this.Name);
            }
            else
            {
                try { grdQT.DataSource = dt; } catch { }
            }
            try
            {
                txtNgayLap.EditValue = dt.Rows[0]["NGAY_LAP"].ToString();
            }
            catch
            {
                txtNgayLap.EditValue = null;
            }

            dtCD = new DataTable();
            dtLoaiMay = new DataTable();
            dtChuyen = new DataTable();
            dtCum = new DataTable();

            try
            {
                dtLoaiMay = new DataTable();
                dtLoaiMay.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboLoaiMay", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.AddCombXtra("ID_LM", "TEN_LOAI_MAY", grvQT, dtLoaiMay, "ID_LM", this.Name);

                dtBT = new DataTable();
                dtBT.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboBacTho", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.AddCombXtra("ID_BT", "TEN_BAC_THO", grvQT, dtBT, "ID_BT", this.Name);
            }
            catch
            {

            }

            FormatGrid();
            SetButton(isAdd);
        }

        private void FormatGrid()
        {
            //An cot
            grvQT.Columns["NGAY_LAP"].Visible = false;
            grvQT.Columns["ID_CHUYEN"].Visible = false;
            grvQT.Columns["ID_CUM"].Visible = false;
            grvQT.Columns["ID_ORD"].Visible = false;

            grvQT.Columns["THOI_GIAN_THIET_KE"].DisplayFormat.FormatType = FormatType.Numeric;
            grvQT.Columns["THOI_GIAN_THIET_KE"].DisplayFormat.FormatString = "N2";
            grvQT.Columns["THOI_GIAN_QUI_DOI"].DisplayFormat.FormatType = FormatType.Numeric;
            grvQT.Columns["THOI_GIAN_QUI_DOI"].DisplayFormat.FormatString = "N2";
            grvQT.Columns["HE_SO_BAC_THO"].DisplayFormat.FormatType = FormatType.Numeric;
            grvQT.Columns["HE_SO_BAC_THO"].DisplayFormat.FormatString = "N2";
            grvQT.Columns["HS_HT_DG"].DisplayFormat.FormatType = FormatType.Numeric;
            grvQT.Columns["HS_HT_DG"].DisplayFormat.FormatString = "N2";
            grvQT.Columns["DON_GIA_GIAY"].DisplayFormat.FormatType = FormatType.Numeric;
            grvQT.Columns["DON_GIA_GIAY"].DisplayFormat.FormatString = "N2";
            grvQT.Columns["DON_GIA_THUC_TE"].DisplayFormat.FormatType = FormatType.Numeric;
            grvQT.Columns["DON_GIA_THUC_TE"].DisplayFormat.FormatString = "N2";

            grvQT.Columns["ID_CD"].Width = 350;
            grvQT.Columns["DON_GIA_THUC_TE"].OptionsColumn.ReadOnly = true;

        }


        private void cboKH_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0LoadCbo") return;
            LoadHD(1);
            Commons.Modules.sLoad = "";
        }

        private void cboMH_EditValueChanged(object sender, EventArgs e)
        {
            //if (Commons.Modules.sLoad == "0LoadCbo") return;
            LoadHD(3);
            LoadCboCum(0);
            //GridView view = cboMH.Properties.View;
            //int rowHandle = view.FocusedRowHandle;
            //string fieldName = "ID_NHH"; // or other field name  
            //object value = view.GetRowCellValue(rowHandle, fieldName);
            //cboLMH.EditValue = Convert.ToInt32(value);
            //LoadCbo();
            LoadLuoi();
            Commons.Modules.sLoad = "";
        }

        private void cboHD_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0LoadCbo") return;
            LoadHD(2);
            LoadLuoi();
            Commons.Modules.sLoad = "";

            try
            {

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void cboOrd_EditValueChanged(object sender, EventArgs e)
        {
            LoadLuoi();
        }

        private void LocData()
        {
            if (Commons.Modules.sLoad == "0LoadCbo") return;
            DataTable dtTmp = new DataTable();
            try
            {
                dtTmp = (DataTable)grdQT.DataSource;
                int sCum, sChuyen;
                string sDK = " 1 = 1 ";
                sCum = -1; sChuyen = -1;
                try { sCum = Convert.ToInt32(cboCum.EditValue.ToString()); } catch { }
                try { sChuyen = Convert.ToInt32(cboChuyen.EditValue.ToString()); } catch { }

                if (sCum != -1) sDK = sDK + " AND ID_CUM = " + sCum;
                if (sChuyen != -1) sDK = sDK + " AND ID_CHUYEN = " + sChuyen;

                dtTmp.DefaultView.RowFilter = sDK;
            }
            catch { dtTmp.DefaultView.RowFilter = ""; }
        }

        private void LoadThongTinQT()
        {
            DataTable dtTTQT = new DataTable();
            try
            {
                dtTTQT.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetThongTinQuiTrinh", cboChuyen.EditValue, cboOrd.EditValue));
                if (dtTTQT.Rows.Count > 0)
                {
                    txtHS.EditValue = dtTTQT.Rows[0]["HS_HT_DG"].ToString();
                    txtDG.EditValue = dtTTQT.Rows[0]["DG_GIAY"].ToString();
                    txtSLCN.EditValue = dtTTQT.Rows[0]["SLCN"].ToString();
                }
                else
                {
                    txtHS.EditValue = 1;
                    DataTable dtdgg = new DataTable();
                    try
                    {
                        dtdgg.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT NGAY_QD, HS_DG_GIAY FROM DON_GIA_GIAY WHERE NGAY_QD <= " + Convert.ToDateTime(txtNgayLap.EditValue).ToString("yyyy/MM/dd") + " ORDER BY NGAY_QD DESC"));
                        txtDG.Text = dtdgg.Rows[0]["HS_DG_GIAY"].ToString();
                    }
                    catch
                    {
                        dtdgg.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT NGAY_QD, HS_DG_GIAY FROM DON_GIA_GIAY WHERE NGAY_QD >= " + Convert.ToDateTime(txtNgayLap.EditValue).ToString("yyyy/MM/dd") + " ORDER BY NGAY_QD ASC"));
                        txtDG.Text = dtdgg.Rows[0]["HS_DG_GIAY"].ToString();
                    }

                }
            }
            catch
            {
            }
        }

        private void cboCum_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            dtCDTemp = new DataTable();
            LocData();
            dtCD.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboCongDoan", 1, cboCum.EditValue));
            dtCDTemp = dtCD;
            Commons.Modules.ObjSystems.AddCombXtra("ID_CD", "TEN_CD", grvQT, dtCD, "ID_CD", this.Name);
        }

        private void cboChuyen_EditValueChanged(object sender, EventArgs e)
        {
            LocData();
            LoadThongTinQT();
        }

        /// <summary>
        /// Set btn Enable
        /// </summary>
        /// <param name="isAdd"></param>
        private void SetButton(bool isAdd)
        {
            windowsUIButton.Buttons[0].Properties.Visible = !isAdd;
            windowsUIButton.Buttons[1].Properties.Visible = !isAdd;
            windowsUIButton.Buttons[2].Properties.Visible = !isAdd;
            //windowsUIButton.Buttons[5].Properties.Visible = !isAdd;
            windowsUIButton.Buttons[8].Properties.Visible = !isAdd;

            windowsUIButton.Buttons[3].Properties.Visible = isAdd;
            windowsUIButton.Buttons[4].Properties.Visible = isAdd;
            windowsUIButton.Buttons[6].Properties.Visible = isAdd;
            windowsUIButton.Buttons[7].Properties.Visible = isAdd;

            optHT.Enabled = !isAdd;
            cboKH.Enabled = !isAdd;
            cboHD.Enabled = !isAdd;
            cboMH.Enabled = !isAdd;
            //cboLMH.Enabled = !isAdd;
            cboOrd.Enabled = !isAdd;
            cboChuyen.Enabled = !isAdd;
            txtNgayLap.Enabled = !isAdd;
            txtSLCN.Enabled = !isAdd;
            cboCum.Enabled = !isAdd;

            txtHS.Enabled = isAdd;
            txtDG.Enabled = isAdd;

        }

        int ttCD, ttChuyen;
        /// <summary>
        /// them Sua
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private void GetStt(ref int ttCD, ref int ttChuyen)
        {
            ttCD = ttChuyen = 0;

            DataTable dtTT = new DataTable();
            try
            {
                string sql = "SELECT MAX(THU_TU_CONG_DOAN) TTCD FROM QUI_TRINH_CONG_NGHE_CHI_TIET WHERE ID_CHUYEN = " + cboChuyen.EditValue + " AND ID_ORD = " + cboOrd.EditValue;
                dtTT.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sql));
                if (dtTT.Rows[0]["TTCD"].ToString() == "")
                {
                    ttCD = 0;
                    ttChuyen = 0;
                }
                else
                {
                    ttCD = Convert.ToInt32(dtTT.Rows[0]["TTCD"].ToString());
                    ttChuyen = ttCD;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                return;
            }

        }

        /// <summary>
        /// Khong ghi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>



        /// <summary>
        /// btn Xoa Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grvQT_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            grvQT.ClearColumnErrors();
            GridView view = sender as GridView;

            //foreach (DataRow r in )
            //{

            //}

            if (view.FocusedColumn.FieldName == "ID_CD")
            {
                //kiểm tra tồn tại trong combo
                if (dtCD.AsEnumerable().Count(x => x["ID_CD"].ToString().Equals(e.Value.ToString())) == 0)
                {
                    e.Valid = false;
                    e.ErrorText = "ton tai";
                    view.SetColumnError(view.Columns["ID_CD"], e.ErrorText);
                    return;
                }
                //kiểm tra không trùng trên lưới
                if (Commons.Modules.ObjSystems.ConvertDatatable(grdQT).AsEnumerable().Where(x => x["ID_CHUYEN"].ToString().Trim().Equals(grvQT.GetFocusedRowCellValue("ID_CHUYEN").ToString().Trim())).Count(x => x["ID_CD"].ToString().Trim().Equals(e.Value.ToString().Trim())) >= 1)
                {
                    e.Valid = false;
                    e.ErrorText = "trung";
                    view.SetColumnError(view.Columns["ID_CD"], e.ErrorText);
                    return;
                }
            }
        }


        private void grvQT_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (grvQT.RowCount == 0) return;
            GridView view = sender as GridView;
            try
            {

                if (e.Column.FieldName == "ID_BT")
                {
                    DataRow[] row = dtBT.Select("ID_BT = " + e.Value);
                    hsBT = Convert.ToDecimal(row[0]["HE_SO_BAC_THO"].ToString());
                    grvQT.SetFocusedRowCellValue("HE_SO_BAC_THO", hsBT);
                }

                if (e.Column.FieldName == "HE_SO_BAC_THO" || e.Column.FieldName == "THOI_GIAN_THIET_KE" || e.Column.FieldName == "HS_HT_DG" || e.Column.FieldName == "DON_GIA_GIAY")
                {
                    if (grvQT.GetFocusedRowCellValue("HE_SO_BAC_THO").ToString() == "")
                    {
                        hsBT = 0;
                    }
                    else
                    {
                        hsBT = Convert.ToDecimal(grvQT.GetFocusedRowCellValue("HE_SO_BAC_THO").ToString());
                    }
                    if (grvQT.GetFocusedRowCellValue("THOI_GIAN_THIET_KE").ToString() == "")
                    {
                        tgTK = 0;
                    }
                    else
                    {
                        tgTK = Convert.ToDecimal(grvQT.GetFocusedRowCellValue("THOI_GIAN_THIET_KE").ToString());
                    }
                    if (grvQT.GetFocusedRowCellValue("HS_HT_DG").ToString() == "")
                    {
                        hsDG = 0;
                    }
                    else
                    {
                        hsDG = Convert.ToDecimal(grvQT.GetFocusedRowCellValue("HS_HT_DG").ToString());
                    }
                    if (grvQT.GetFocusedRowCellValue("DON_GIA_GIAY").ToString() == "")
                    {
                        dgG = 0;
                    }
                    else
                    {
                        dgG = Convert.ToDecimal(grvQT.GetFocusedRowCellValue("DON_GIA_GIAY").ToString());
                    }
                    grvQT.SetFocusedRowCellValue("THOI_GIAN_QUI_DOI", hsBT * tgTK);
                    grvQT.SetFocusedRowCellValue("DON_GIA_THUC_TE", hsBT * tgTK * hsDG * dgG);
                }

                if (e.Column.FieldName == "ID_CD")
                {
                    DataTable dtCongDoan = new DataTable();
                    string sql = "SELECT ID_CD, ID_BT, ID_LM, ISNULL(TGTK,0) TGTK FROM CONG_DOAN WHERE ID_CD = " + Convert.ToInt32(grvQT.GetFocusedRowCellValue("ID_CD").ToString());
                    dtCongDoan.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sql));
                    grvQT.SetFocusedRowCellValue("ID_LM", Convert.ToInt32(dtCongDoan.Rows[0]["ID_LM"].ToString()));
                    grvQT.SetFocusedRowCellValue("ID_BT", Convert.ToInt32(dtCongDoan.Rows[0]["ID_BT"].ToString()));
                    tgTK = Convert.ToInt32(dtCongDoan.Rows[0]["TGTK"].ToString());
                    if (grvQT.GetFocusedRowCellValue("HE_SO_BAC_THO").ToString() == "")
                    {
                        hsBT = 0;
                    }
                    else
                    {
                        hsBT = Convert.ToDecimal(grvQT.GetFocusedRowCellValue("HE_SO_BAC_THO").ToString());
                    }

                    if (grvQT.GetFocusedRowCellValue("HS_HT_DG").ToString() == "")
                    {
                        hsDG = 0;
                    }
                    else
                    {
                        hsDG = Convert.ToDecimal(grvQT.GetFocusedRowCellValue("HS_HT_DG").ToString());
                    }
                    if (grvQT.GetFocusedRowCellValue("DON_GIA_GIAY").ToString() == "")
                    {
                        dgG = 0;
                    }
                    else
                    {
                        dgG = Convert.ToDecimal(grvQT.GetFocusedRowCellValue("DON_GIA_GIAY").ToString());
                    }
                    grvQT.SetFocusedRowCellValue("THOI_GIAN_THIET_KE", tgTK);
                    grvQT.SetFocusedRowCellValue("THOI_GIAN_QUI_DOI", hsBT * tgTK);
                    grvQT.SetFocusedRowCellValue("DON_GIA_THUC_TE", hsBT * tgTK * hsDG * dgG);
                }
            }
            catch { }
        }

        private void Savedata()
        {
            string stbQT = "stbQT" + Commons.Modules.UserName;
            try
            {
                //tạo một datatable 
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbQT, Commons.Modules.ObjSystems.ConvertDatatable(grdQT), "");
                //Cap nhat qui trinh cong nghe
                string sSql = "UPDATE QUI_TRINH_CONG_NGHE_CHI_TIET SET THU_TU_CONG_DOAN = tmp.THU_TU_CONG_DOAN "
                            + " , MaQL = tmp.MaQL, ID_LM = tmp.ID_LM, ID_BT = tmp.ID_BT, THOI_GIAN_THIET_KE = tmp.THOI_GIAN_THIET_KE,"
                            + " THOI_GIAN_QUI_DOI = tmp.THOI_GIAN_QUI_DOI, HS_HT_DG = tmp.HS_HT_DG, DON_GIA_GIAY = tmp.DON_GIA_GIAY, DON_GIA_THUC_TE "
                            + " = tmp.DON_GIA_THUC_TE, CD_DUNG_CHUNG = tmp.CD_DUNG_CHUNG, YEU_CAU_KT = tmp.YEU_CAU_KT "
                            + " FROM QUI_TRINH_CONG_NGHE_CHI_TIET QT "
                            + " INNER JOIN " + stbQT + " tmp ON QT.ID_CHUYEN = tmp.ID_CHUYEN "
                            + " AND QT.ID_ORD = tmp.ID_ORD AND QT.ID_CD = tmp.ID_CD "
                            + " INSERT INTO QUI_TRINH_CONG_NGHE_CHI_TIET(THU_TU_CONG_DOAN, MaQL, ID_CD, ID_LM, ID_BT, THOI_GIAN_THIET_KE, "
                            + " THOI_GIAN_QUI_DOI, HS_HT_DG, DON_GIA_GIAY, DON_GIA_THUC_TE, CD_DUNG_CHUNG, YEU_CAU_KT, ID_CHUYEN,  ID_ORD)"
                            + " SELECT THU_TU_CONG_DOAN, MaQL, ID_CD, ID_LM, ID_BT, THOI_GIAN_THIET_KE, THOI_GIAN_QUI_DOI, HS_HT_DG,"
                            + " DON_GIA_GIAY, DON_GIA_THUC_TE, CD_DUNG_CHUNG, YEU_CAU_KT, ID_CHUYEN, ID_ORD"
                            + " FROM " + stbQT + " tmp1 WHERE NOT EXISTS(SELECT* FROM QUI_TRINH_CONG_NGHE_CHI_TIET QTCT"
                            + " WHERE tmp1.ID_CHUYEN = QTCT.ID_CHUYEN "
                            + " AND tmp1.ID_ORD = QTCT.ID_ORD AND tmp1.ID_CD = QTCT.ID_CD)";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);


                string strHS = txtHS.EditValue.ToString();
                string strDG = txtDG.EditValue.ToString();
                strHS = strHS.Replace(",", ".");
                strDG = strDG.Replace(",", ".");


                sSql = "DELETE FROM THONG_TIN_QUI_TRINH WHERE ID_CHUYEN = " + Convert.ToInt32(cboChuyen.EditValue) + " AND ID_ORD = " + Convert.ToInt32(cboOrd.EditValue)
                        + " INSERT INTO THONG_TIN_QUI_TRINH (ID_CHUYEN, ID_ORD, HS_HT_DG, DG_GIAY, SLCN) "
                        + "VALUES (" + Convert.ToInt32(cboChuyen.EditValue) + ", " + Convert.ToInt32(cboOrd.EditValue) + ", "
                        + Convert.ToDecimal(strHS) + ", " + Convert.ToDecimal(strDG) + ", " + Convert.ToInt32(txtSLCN.EditValue) + ")";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);

                string strSql1 = "DROP TABLE " + stbQT;
                SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSql1);
                Commons.Modules.ObjSystems.XoaTable(stbQT);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void grvQT_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvQT_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void txtHS_KeyDown(object sender, KeyEventArgs e)

        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtHS.Text == "") return;
                hsDG = Convert.ToDecimal(txtHS.EditValue);

                for (int i = 0; i < grvQT.RowCount - 1; i++)
                {
                    if (grvQT.GetRowCellValue(i, "THOI_GIAN_QUI_DOI").ToString() == "")
                    {
                        tgQD = 0;
                    }
                    else
                    {
                        tgQD = Convert.ToDecimal(grvQT.GetRowCellValue(i, "THOI_GIAN_QUI_DOI").ToString());
                    }

                    if (grvQT.GetRowCellValue(i, "DON_GIA_GIAY").ToString() == "")
                    {
                        dgG = 0;
                    }
                    else
                    {
                        dgG = Convert.ToDecimal(grvQT.GetRowCellValue(i, "DON_GIA_GIAY").ToString());
                    }
                    grvQT.SetRowCellValue(i, "HS_HT_DG", hsDG);
                    grvQT.SetRowCellValue(i, "DON_GIA_THUC_TE", tgQD * hsDG * dgG);
                    grvQT.UpdateCurrentRow();
                }
            }
        }

        private void txtDG_KeyDown(object sender, KeyEventArgs e)

        {
            if (e.KeyCode == Keys.Enter)
            {

                if (txtDG.Text == "") return;
                dgG = Convert.ToDecimal(txtDG.EditValue);

                for (int i = 0; i < grvQT.RowCount - 1; i++)
                {
                    if (grvQT.GetRowCellValue(i, "THOI_GIAN_QUI_DOI").ToString() == "")
                    {
                        tgQD = 0;
                    }
                    else
                    {
                        tgQD = Convert.ToDecimal(grvQT.GetRowCellValue(i, "THOI_GIAN_QUI_DOI").ToString());
                    }

                    if (grvQT.GetRowCellValue(i, "HS_HT_DG").ToString() == "")
                    {
                        hsDG = 0;
                    }
                    else
                    {
                        hsDG = Convert.ToDecimal(grvQT.GetRowCellValue(i, "HS_HT_DG").ToString());
                    }
                    grvQT.SetRowCellValue(i, "DON_GIA_GIAY", dgG);
                    grvQT.SetRowCellValue(i, "DON_GIA_THUC_TE", tgQD * hsDG * dgG);
                    grvQT.UpdateCurrentRow();
                }
            }
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "in":
                    {
                        String sTongTGTK = "";
                        String sTongTGQD = "";
                        String sTongDG = "";

                        System.Data.SqlClient.SqlConnection conn;
                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                        conn.Open();

                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuiTrinhCongNgheChiTiet", conn);
                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                        cmd.Parameters.Add("@ID_CHUYEN", SqlDbType.Int).Value = cboChuyen.EditValue;
                        cmd.Parameters.Add("@ID_ORD", SqlDbType.Int).Value = cboOrd.EditValue;
                        cmd.CommandType = CommandType.StoredProcedure;
                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                        DataSet ds = new DataSet();
                        adp.Fill(ds);
                        DataTable dtCty = new DataTable();
                        DataTable dtTieuDe = new DataTable();
                        DataTable dtChiTiet = new DataTable();
                        DataTable dtDSMay = new DataTable();
                        DataTable dtTongBC = new DataTable();

                        dtCty = ds.Tables[0].Copy();
                        dtTieuDe = ds.Tables[1].Copy();
                        dtChiTiet = ds.Tables[2].Copy();
                        dtDSMay = ds.Tables[3].Copy();
                        dtTongBC = ds.Tables[4].Copy();

                        Excel.Application oXL;
                        Excel._Workbook oWB;
                        Excel._Worksheet oSheet;

                        oXL = new Excel.Application();
                        oXL.Visible = true;

                        oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                        oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                        string fontName = "Times New Roman";
                        int fontSizeTieuDe = 16;
                        int fontSizeNoiDung = 12;

                        string lastColumn = string.Empty;
                        lastColumn = "J";

                        Excel.Range row1_CongTy = oSheet.get_Range("A1", lastColumn + "1");
                        row1_CongTy.Merge();
                        row1_CongTy.Font.Size = fontSizeNoiDung;
                        row1_CongTy.Font.Name = fontName;
                        row1_CongTy.Font.Bold = true;
                        row1_CongTy.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        row1_CongTy.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        row1_CongTy.Value2 = dtCty.Rows[0]["TEN_CTY"];

                        Excel.Range row2_DiaChi = oSheet.get_Range("A2", lastColumn + "2");
                        row2_DiaChi.Merge();
                        row2_DiaChi.Font.Size = fontSizeNoiDung;
                        row2_DiaChi.Font.Name = fontName;
                        row2_DiaChi.Font.Bold = true;
                        row2_DiaChi.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        row2_DiaChi.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        row2_DiaChi.Value2 = dtCty.Rows[0]["DIA_CHI"];

                        Excel.Range row3_TieuDe = oSheet.get_Range("A3", lastColumn + "3");
                        row3_TieuDe.Merge();
                        row3_TieuDe.Font.Size = fontSizeTieuDe;
                        row3_TieuDe.Font.Name = fontName;
                        row3_TieuDe.Font.Bold = true;
                        row3_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        row3_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        row3_TieuDe.RowHeight = 50;
                        row3_TieuDe.Value2 = "QUI TRÌNH CÔNG NGHỆ";

                        Excel.Range row4_TieuDe = oSheet.get_Range("B4", "B4");
                        row4_TieuDe.Font.Size = fontSizeNoiDung;
                        row4_TieuDe.Font.Name = fontName;
                        row4_TieuDe.Font.Bold = true;
                        row4_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        row4_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        row4_TieuDe.Value2 = "Khách hàng : " + dtTieuDe.Rows[0]["TEN_KH"];

                        Excel.Range row4H_TieuDe = oSheet.get_Range("H4", "H4");
                        row4H_TieuDe.Font.Size = fontSizeNoiDung;
                        row4H_TieuDe.Font.Name = fontName;
                        row4H_TieuDe.Font.Bold = true;
                        row4H_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        row4H_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        row4H_TieuDe.Value2 = "Chuyền : " + dtTieuDe.Rows[0]["TEN_CHUYEN"];

                        Excel.Range row5_TieuDe = oSheet.get_Range("B5", "B5");
                        row5_TieuDe.Font.Size = fontSizeNoiDung;
                        row5_TieuDe.Font.Name = fontName;
                        row5_TieuDe.Font.Bold = true;
                        row5_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        row5_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        row5_TieuDe.Value2 = "Hợp đồng : " + dtTieuDe.Rows[0]["SO_DHB"];

                        Excel.Range row5H_TieuDe = oSheet.get_Range("H5", "H5");
                        row5H_TieuDe.Font.Size = fontSizeNoiDung;
                        row5H_TieuDe.Font.Name = fontName;
                        row5H_TieuDe.Font.Bold = true;
                        row5H_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        row5H_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        row5H_TieuDe.Value2 = "Loại hàng hóa : " + dtTieuDe.Rows[0]["TEN_NHH"];

                        Excel.Range row6_TieuDe = oSheet.get_Range("B6", "B6");
                        row6_TieuDe.Font.Size = fontSizeNoiDung;
                        row6_TieuDe.Font.Name = fontName;
                        row6_TieuDe.Font.Bold = true;
                        row6_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        row6_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        row6_TieuDe.Value2 = "Mã hàng : " + dtTieuDe.Rows[0]["TEN_HH"];

                        Excel.Range row7_TieuDe = oSheet.get_Range("B7", "B7");
                        row7_TieuDe.Font.Size = fontSizeNoiDung;
                        row7_TieuDe.Font.Name = fontName;
                        row7_TieuDe.Font.Bold = true;
                        row7_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        row7_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        row7_TieuDe.Value2 = "Order : " + dtTieuDe.Rows[0]["ORDER_NUMBER"];

                        Excel.Range rowFormat_TieuDe = oSheet.get_Range("A9", "J9");
                        rowFormat_TieuDe.Font.Size = fontSizeNoiDung;
                        rowFormat_TieuDe.Font.Name = fontName;
                        rowFormat_TieuDe.Font.Bold = true;
                        rowFormat_TieuDe.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        rowFormat_TieuDe.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        rowFormat_TieuDe.Interior.Color = Color.Yellow;
                        BorderAround(oSheet.get_Range("A9", "J9"));

                        Excel.Range row9A_TieuDe = oSheet.get_Range("A9", "A9");
                        row9A_TieuDe.Value2 = "Mã QL";
                        row9A_TieuDe.ColumnWidth = 8;

                        Excel.Range row9B_TieuDe = oSheet.get_Range("B9", "B9");
                        row9B_TieuDe.Value2 = "Bước công việc";
                        row9B_TieuDe.ColumnWidth = 55;

                        Excel.Range row9C_TieuDe = oSheet.get_Range("C9", "C9");
                        row9C_TieuDe.Value2 = "Yêu cầu kỹ thuật";
                        row9C_TieuDe.ColumnWidth = 15;

                        Excel.Range row9D_TieuDe = oSheet.get_Range("D9", "D9");
                        row9D_TieuDe.Value2 = "Bậc thợ";
                        row9D_TieuDe.ColumnWidth = 15;

                        Excel.Range row9E_TieuDe = oSheet.get_Range("E9", "E9");
                        row9E_TieuDe.Value2 = "TGTK";
                        row9E_TieuDe.ColumnWidth = 10;

                        Excel.Range row9F_TieuDe = oSheet.get_Range("F9", "F9");
                        row9F_TieuDe.Value2 = "TGQD";
                        row9F_TieuDe.ColumnWidth = 10;

                        Excel.Range row9G_TieuDe = oSheet.get_Range("G9", "G9");
                        row9G_TieuDe.Value2 = "DMSL";
                        row9G_TieuDe.ColumnWidth = 12;

                        Excel.Range row9H_TieuDe = oSheet.get_Range("H9", "H9");
                        row9H_TieuDe.Value2 = "Lao động";
                        row9H_TieuDe.ColumnWidth = 12;

                        Excel.Range row9I_TieuDe = oSheet.get_Range("I9", "I9");
                        row9I_TieuDe.Value2 = "Thiết bị";
                        row9I_TieuDe.ColumnWidth = 12;

                        Excel.Range row9J_TieuDe = oSheet.get_Range("J9", "J9");
                        row9J_TieuDe.Value2 = "Đơn giá";
                        row9J_TieuDe.ColumnWidth = 12;

                        DataRow[] dr = dtChiTiet.Select();
                        //string[,] rowData = new string[dr.Length, dtChiTiet.Columns.Count];
                        int idCum = 0;
                        int rowCnt = 10;
                        int vtbd = 0;
                        foreach (DataRow row in dr)
                        {
                            if (Convert.ToInt32(row["ID_CUM"].ToString()) != idCum)
                            {
                                if (idCum != 0)
                                {

                                    Excel.Range rowTong1 = oSheet.get_Range("D" + rowCnt, "D" + rowCnt);
                                    rowTong1.Value2 = "Tổng";
                                    rowTong1 = oSheet.get_Range("E" + rowCnt, "E" + rowCnt);
                                    rowTong1.Value2 = "=SUM(E" + vtbd.ToString() + ":E" + (rowCnt - 1).ToString() + ")";
                                    rowTong1 = oSheet.get_Range("F" + rowCnt, "F" + rowCnt);
                                    rowTong1.Value2 = "=SUM(F" + vtbd.ToString() + ":F" + (rowCnt - 1).ToString() + ")";
                                    rowTong1 = oSheet.get_Range("J" + rowCnt, "J" + rowCnt);
                                    rowTong1.Value2 = "=SUM(J" + vtbd.ToString() + ":J" + (rowCnt - 1).ToString() + ")";

                                    if (sTongTGTK == "")
                                    {
                                        sTongTGTK = "= E" + rowCnt;
                                        sTongTGQD = "= F" + rowCnt;
                                        sTongDG = "= J" + rowCnt;
                                    }
                                    else
                                    {
                                        sTongTGTK = sTongTGTK + " + E" + rowCnt;
                                        sTongTGQD = sTongTGQD + " + F" + rowCnt;
                                        sTongDG = sTongDG + " + J" + rowCnt;
                                    }

                                    rowTong1 = oSheet.get_Range("A" + vtbd, "A" + rowCnt);
                                    rowTong1.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                    rowTong1 = oSheet.get_Range("E" + vtbd, "H" + rowCnt);
                                    rowTong1.Cells.NumberFormat = "#,##0.00";
                                    rowTong1 = oSheet.get_Range("J" + vtbd, "J" + rowCnt);
                                    rowTong1.Cells.NumberFormat = "#,##0.00";
                                    rowTong1 = oSheet.get_Range("A" + rowCnt, "J" + rowCnt);
                                    rowTong1.Font.Bold = true;
                                    rowTong1.Font.Color = Color.Red;

                                    Excel.Range rowFormat2 = oSheet.get_Range("A" + vtbd, "J" + rowCnt);
                                    rowFormat2.Font.Size = fontSizeNoiDung;
                                    rowFormat2.Font.Name = fontName;
                                    rowFormat2.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                                    BorderAround(oSheet.get_Range("A" + vtbd, "J" + rowCnt));

                                    //rowFormat1.Font.Bold = true;
                                    //rowFormat1.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                    //rowFormat1.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                                    rowCnt++;
                                }
                                Excel.Range rowCum = oSheet.get_Range("B" + rowCnt, "B" + rowCnt);
                                rowCum.Value2 = row["TEN_CUM"].ToString();
                                rowCum.Font.Size = fontSizeNoiDung;
                                rowCum.Font.Name = fontName;
                                rowCum.Font.Bold = true;
                                rowCum.Font.Color = Color.Red;
                                BorderAround(oSheet.get_Range("A" + rowCnt, "J" + rowCnt));

                                idCum = Convert.ToInt32(row["ID_CUM"].ToString());
                                rowCnt++;
                                vtbd = rowCnt;
                            }
                            Excel.Range rowCT = oSheet.get_Range("A" + rowCnt, "A" + rowCnt);
                            rowCT.Value2 = row["MaQL"].ToString();
                            rowCT = oSheet.get_Range("B" + rowCnt, "B" + rowCnt);
                            rowCT.Value2 = row["TEN_CD"].ToString();
                            rowCT = oSheet.get_Range("C" + rowCnt, "C" + rowCnt);
                            rowCT.Value2 = row["YEU_CAU_KT"].ToString();
                            rowCT = oSheet.get_Range("D" + rowCnt, "D" + rowCnt);
                            rowCT.Value2 = row["TEN_BAC_THO"].ToString();
                            rowCT = oSheet.get_Range("E" + rowCnt, "E" + rowCnt);
                            rowCT.Value2 = row["THOI_GIAN_THIET_KE"].ToString();
                            rowCT = oSheet.get_Range("F" + rowCnt, "F" + rowCnt);
                            rowCT.Value2 = row["THOI_GIAN_QUI_DOI"].ToString();
                            rowCT = oSheet.get_Range("G" + rowCnt, "G" + rowCnt);
                            rowCT.Value2 = row["DMSL"].ToString();
                            rowCT = oSheet.get_Range("H" + rowCnt, "H" + rowCnt);
                            rowCT.Value2 = row["LD"].ToString();
                            rowCT = oSheet.get_Range("I" + rowCnt, "I" + rowCnt);
                            rowCT.Value2 = row["TEN_LOAI_MAY"].ToString();
                            rowCT = oSheet.get_Range("J" + rowCnt, "J" + rowCnt);
                            rowCT.Value2 = row["DON_GIA_THUC_TE"].ToString();

                            rowCnt++;
                        }

                        Excel.Range rowTong = oSheet.get_Range("D" + rowCnt, "D" + rowCnt);
                        rowTong.Value2 = "Tổng";
                        rowTong = oSheet.get_Range("E" + rowCnt, "E" + rowCnt);
                        rowTong.Value2 = "=SUM(E" + vtbd.ToString() + ":E" + (rowCnt - 1).ToString() + ")";
                        rowTong = oSheet.get_Range("F" + rowCnt, "F" + rowCnt);
                        rowTong.Value2 = "=SUM(F" + vtbd.ToString() + ":F" + (rowCnt - 1).ToString() + ")";
                        rowTong = oSheet.get_Range("J" + rowCnt, "J" + rowCnt);
                        rowTong.Value2 = "=SUM(J" + vtbd.ToString() + ":J" + (rowCnt - 1).ToString() + ")";

                        if (sTongTGTK == "")
                        {
                            sTongTGTK = "= E" + rowCnt;
                            sTongTGQD = "= F" + rowCnt;
                            sTongDG = "= J" + rowCnt;
                        }
                        else
                        {
                            sTongTGTK = sTongTGTK + " + E" + rowCnt;
                            sTongTGQD = sTongTGQD + " + F" + rowCnt;
                            sTongDG = sTongDG + " + J" + rowCnt;
                        }

                        rowTong = oSheet.get_Range("A" + vtbd, "A" + rowCnt);
                        rowTong.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        rowTong = oSheet.get_Range("E" + vtbd, "H" + rowCnt);
                        rowTong.Cells.NumberFormat = "#,##0.00";
                        rowTong = oSheet.get_Range("J" + vtbd, "J" + rowCnt);
                        rowTong.Cells.NumberFormat = "#,##0.00";
                        rowTong = oSheet.get_Range("A" + rowCnt, "J" + rowCnt);
                        rowTong.Font.Bold = true;
                        rowTong.Font.Color = Color.Red;

                        Excel.Range rowFormat1 = oSheet.get_Range("A" + vtbd, "J" + rowCnt);
                        rowFormat1.Font.Size = fontSizeNoiDung;
                        rowFormat1.Font.Name = fontName;
                        rowFormat1.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                        BorderAround(oSheet.get_Range("A" + vtbd, "J" + rowCnt));

                        rowCnt++;

                        Excel.Range rowTongCong = oSheet.get_Range("D" + rowCnt, "D" + rowCnt);
                        rowTongCong.Value2 = "Tổng cộng";
                        rowTongCong = oSheet.get_Range("E" + rowCnt, "E" + rowCnt);
                        rowTongCong.Value2 = sTongTGTK;
                        rowTongCong.Cells.NumberFormat = "#,##0.00";
                        rowTongCong = oSheet.get_Range("F" + rowCnt, "F" + rowCnt);
                        rowTongCong.Value2 = sTongTGQD;
                        rowTongCong.Cells.NumberFormat = "#,##0.00";
                        rowTongCong = oSheet.get_Range("J" + rowCnt, "J" + rowCnt);
                        rowTongCong.Value2 = sTongDG;
                        rowTongCong.Cells.NumberFormat = "#,##0.00";

                        rowTongCong = oSheet.get_Range("A" + rowCnt, "J" + rowCnt);
                        rowTongCong.Font.Size = fontSizeNoiDung;
                        rowTongCong.Font.Name = fontName;
                        rowTongCong.Font.Bold = true;
                        rowTongCong.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        rowTongCong.Interior.Color = Color.Yellow;

                        BorderAround(oSheet.get_Range("A" + rowCnt, "J" + rowCnt));

                        rowCnt++;
                        rowCnt++;

                        int iTongHop = rowCnt;
                        Excel.Range rowTongHop = oSheet.get_Range("B" + iTongHop, "B" + iTongHop);
                        rowTongHop.Value2 = "TG làm việc/Ngày";
                        rowTongHop = oSheet.get_Range("C" + iTongHop, "C" + iTongHop);
                        rowTongHop.Value2 = dtTongBC.Rows[0]["TGLV"];
                        rowTongHop.NumberFormat = "#,##0";
                        rowTongHop = oSheet.get_Range("D" + iTongHop, "D" + iTongHop);
                        rowTongHop.Value2 = "Giây";

                        iTongHop++;
                        rowTongHop = oSheet.get_Range("B" + iTongHop, "B" + iTongHop);
                        rowTongHop.Value2 = "Tổng thời gian may 1 sản phẩm";
                        rowTongHop = oSheet.get_Range("C" + iTongHop, "C" + iTongHop);
                        rowTongHop.Value2 = dtTongBC.Rows[0]["TongTGSP"];
                        rowTongHop.NumberFormat = "#,##0.00";
                        rowTongHop = oSheet.get_Range("D" + iTongHop, "D" + iTongHop);
                        rowTongHop.Value2 = "Giây";

                        iTongHop++;
                        rowTongHop = oSheet.get_Range("B" + iTongHop, "B" + iTongHop);
                        rowTongHop.Value2 = "Năng suất lao động bình quân đầu người";
                        rowTongHop = oSheet.get_Range("C" + iTongHop, "C" + iTongHop);
                        rowTongHop.Value2 = dtTongBC.Rows[0]["NSLDCN"];
                        rowTongHop.NumberFormat = "#,##0.00";
                        rowTongHop = oSheet.get_Range("D" + iTongHop, "D" + iTongHop);
                        rowTongHop.Value2 = "sp/lđ";

                        iTongHop++;
                        rowTongHop = oSheet.get_Range("B" + iTongHop, "B" + iTongHop);
                        rowTongHop.Value2 = "Số lao động trong tổ";
                        rowTongHop = oSheet.get_Range("C" + iTongHop, "C" + iTongHop);
                        rowTongHop.Value2 = dtTongBC.Rows[0]["SLCN"];
                        rowTongHop = oSheet.get_Range("D" + iTongHop, "D" + iTongHop);
                        rowTongHop.Value2 = "Người";

                        iTongHop++;
                        rowTongHop = oSheet.get_Range("B" + iTongHop, "B" + iTongHop);
                        rowTongHop.Value2 = "Năng suất lao động tổ";
                        rowTongHop = oSheet.get_Range("C" + iTongHop, "C" + iTongHop);
                        rowTongHop.Value2 = dtTongBC.Rows[0]["NSLDTO"];
                        rowTongHop.NumberFormat = "#,##0.00";
                        rowTongHop = oSheet.get_Range("D" + iTongHop, "D" + iTongHop);
                        rowTongHop.Value2 = "sp/tổ";

                        iTongHop++;
                        rowTongHop = oSheet.get_Range("B" + iTongHop, "B" + iTongHop);
                        rowTongHop.Value2 = "Cường độ lao động";
                        rowTongHop = oSheet.get_Range("C" + iTongHop, "C" + iTongHop);
                        rowTongHop.Value2 = dtTongBC.Rows[0]["CDLD"];
                        rowTongHop.NumberFormat = "#,##0.00";
                        rowTongHop = oSheet.get_Range("D" + iTongHop, "D" + iTongHop);
                        rowTongHop.Value2 = "Giây";

                        iTongHop++;
                        rowTongHop = oSheet.get_Range("B" + iTongHop, "B" + iTongHop);
                        rowTongHop.Value2 = "Tổng thành tiền";
                        rowTongHop = oSheet.get_Range("C" + iTongHop, "C" + iTongHop);
                        rowTongHop.Value2 = dtTongBC.Rows[0]["TongTT"];
                        rowTongHop.NumberFormat = "#,##0.00";
                        rowTongHop = oSheet.get_Range("D" + iTongHop, "D" + iTongHop);
                        rowTongHop.Value2 = "Đồng";

                        Excel.Range rowTongHop_Format = oSheet.get_Range("B" + rowCnt, "D" + iTongHop);
                        rowTongHop_Format.Font.Size = fontSizeNoiDung;
                        rowTongHop_Format.Font.Name = fontName;
                        rowTongHop_Format.Font.Bold = true;
                        rowTongHop_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                        Excel.Range rowMay_TieuDe1 = oSheet.get_Range("G" + rowCnt, "G" + rowCnt);
                        rowMay_TieuDe1.Value2 = "Thiết bị";

                        Excel.Range rowMay_TieuDe2 = oSheet.get_Range("H" + rowCnt, "H" + rowCnt);
                        rowMay_TieuDe2.Value2 = "SL";

                        Excel.Range rowMay_TieuDe3 = oSheet.get_Range("I" + rowCnt, "I" + rowCnt);
                        rowMay_TieuDe3.Value2 = "DVT";

                        Excel.Range rowMay_TieuDe4 = oSheet.get_Range("J" + rowCnt, "J" + rowCnt);
                        rowMay_TieuDe4.Value2 = "Thành tiền";

                        Excel.Range rowMay_TieuDe_Format = oSheet.get_Range("G" + rowCnt, "J" + rowCnt);
                        rowMay_TieuDe_Format.Font.Size = fontSizeNoiDung;
                        rowMay_TieuDe_Format.Font.Name = fontName;
                        rowMay_TieuDe_Format.Font.Bold = true;
                        rowMay_TieuDe_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        rowMay_TieuDe_Format.Cells.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        rowMay_TieuDe_Format.Interior.Color = Color.Yellow;

                        BorderAround(oSheet.get_Range("G" + rowCnt, "J" + rowCnt));

                        rowCnt++;
                        vtbd = rowCnt;
                        DataRow[] drM = dtDSMay.Select();
                        foreach (DataRow row in drM)
                        {
                            Excel.Range rowMCT = oSheet.get_Range("G" + rowCnt, "G" + rowCnt);
                            rowMCT.Value2 = row["TEN_LOAI_MAY"].ToString();
                            rowMCT = oSheet.get_Range("H" + rowCnt, "H" + rowCnt);
                            rowMCT.Value2 = row["TLD"].ToString();
                            rowMCT = oSheet.get_Range("I" + rowCnt, "I" + rowCnt);
                            rowMCT.Value2 = row["DVT"].ToString();
                            rowMCT = oSheet.get_Range("J" + rowCnt, "J" + rowCnt);
                            rowMCT.Value2 = row["TDG"].ToString();

                            rowCnt++;
                        }

                        rowCnt--;
                        BorderAround(oSheet.get_Range("G" + vtbd, "J" + rowCnt));
                        Excel.Range rowMay_ChiTiet_Format = oSheet.get_Range("G" + vtbd, "J" + rowCnt);
                        rowMay_ChiTiet_Format.Font.Size = fontSizeNoiDung;
                        rowMay_ChiTiet_Format.Font.Name = fontName;
                        rowMay_ChiTiet_Format.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                        rowMay_ChiTiet_Format = oSheet.get_Range("H" + vtbd, "H" + rowCnt);
                        rowMay_ChiTiet_Format.Cells.NumberFormat = "#,##0.00";
                        rowMay_ChiTiet_Format = oSheet.get_Range("J" + vtbd, "J" + rowCnt);
                        rowMay_ChiTiet_Format.Cells.NumberFormat = "#,##0.00";

                        break;
                    }
                case "xoa":
                    {
                        string sSql = "";
                        try
                        {
                            if (grvQT.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
                            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;

                            sSql = "DELETE QUI_TRINH_CONG_NGHE_CHI_TIET WHERE ID_CHUYEN = " + grvQT.GetFocusedRowCellValue("ID_CHUYEN") +
                                                                    " AND ID_ORD = " + grvQT.GetFocusedRowCellValue("ID_ORD") +
                                                                    " AND ID_CD = " + grvQT.GetFocusedRowCellValue("ID_CD");
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                            grvQT.DeleteSelectedRows();
                        }
                        catch
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
                        }
                        break;
                    }
                case "sua":
                    {
                        if (cboHD.Text == "")
                        {
                            Commons.Modules.ObjSystems.msgChung("@ChuaNhapHopDong@");
                            return;
                        }
                        if (cboMH.Text == "")
                        {
                            Commons.Modules.ObjSystems.msgChung("@ChuaNhapMaHang@");
                            return;
                        }
                        if (cboOrd.Text == "")
                        {
                            Commons.Modules.ObjSystems.msgChung("@ChuaNhapOrder@");
                            return;
                        }
                        if (cboChuyen.Text == "")
                        {
                            Commons.Modules.ObjSystems.msgChung("@ChuaNhapSttChuyen@");
                            return;
                        }
                        if (cboCum.Text == "")
                        {
                            Commons.Modules.ObjSystems.msgChung("@ChuaNhapCum@");
                            return;
                        }
                        GetStt(ref ttCD, ref ttChuyen);
                        isAdd = true;
                        SetButton(isAdd);
                        grvQT.OptionsBehavior.Editable = true;
                        Commons.Modules.ObjSystems.AddnewRow(grvQT, true);

                        break;
                    }

                case "danhlaiMQL":
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_DanhLaiMaQL"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo) == DialogResult.No)
                            {
                                return;
                            }
                            dt = (DataTable)grdQT.DataSource;
                            if (dt.Rows.Count == 0)
                            {
                                return;
                            }
                            else
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    dt.Rows[i]["MaQL"] = dt.Rows[i]["THU_TU_CONG_DOAN"];
                                }
                            }
                        }
                        catch { }
                        break;
                    }
                case "chonCD":
                    {
                        try
                        {
                            frmQTCN_View ctl1 = new frmQTCN_View(Convert.ToInt64(cboCum.EditValue), Convert.ToInt64(cboChuyen.EditValue), Convert.ToInt64(cboOrd.EditValue));

                            ctl1.Size = new Size(800, 600);
                            ctl1.StartPosition = FormStartPosition.CenterParent;
                            ctl1.Size = new Size((this.Width / 2) + (ctl1.Width / 2), (this.Height / 2) + (ctl1.Height / 2));
                            ctl1.StartPosition = FormStartPosition.Manual;
                            ctl1.Location = new Point(this.Width / 2 - ctl1.Width / 2 + this.Location.X,
                                                      this.Height / 2 - ctl1.Height / 2 + this.Location.Y);

                            DataTable dt = (DataTable)grdQT.DataSource;
                            ctl1.dt_frmQTCN_View = dt;

                            if (ctl1.ShowDialog() == DialogResult.OK)
                            {

                                DataTable dt_chon = ((frmQTCN_View)ctl1).dt_frmQTCN_View.Copy();


                                if (dt_chon == null || dt_chon.Rows.Count < 1) return;
                                DataTable dtTMP = new DataTable();
                                dtTMP = (DataTable)grdQT.DataSource;
                                //dtTMP.DefaultView.Sort = "THU_TU_CONG_DOAN DESC";

                                //STT ++
                                //int maxtt = 0;
                                //maxtt = Convert.ToInt32(dtTMP.Rows[dtTMP.Rows.Count -1]["THU_TU_CONG_DOAN"]) + 1;

                                foreach (DataRow dr1 in dt_chon.Rows)
                                {
                                    DataRow dr = ((DataTable)grdQT.DataSource).NewRow();
                                    dr["THU_TU_CONG_DOAN"] = dr1["THU_TU_CONG_DOAN"];
                                    dr["ID_CD"] = dr1["ID_CD"];
                                    dr["ID_CUM"] = dr1["ID_CUM"];
                                    dr["ID_LM"] = dr1["ID_LM"];
                                    dr["ID_BT"] = dr1["ID_BT"];
                                    dr["MaQL"] = dr1["MA_QL"];
                                    dr["THOI_GIAN_THIET_KE"] = dr1["TGTK"];
                                    dr["ID_CHUYEN"] = cboChuyen.EditValue;
                                    dr["ID_ORD"] = cboOrd.EditValue;

                                    //Load HSBT
                                    DataRow[] row = dtBT.Select("ID_BT = " + dr1["ID_BT"]);
                                    hsBT = Convert.ToDecimal(row[0]["HE_SO_BAC_THO"].ToString());
                                    dr["HE_SO_BAC_THO"] = hsBT;

                                    Decimal tgThietKe = Convert.ToDecimal(hsBT * Convert.ToDecimal(dr["THOI_GIAN_THIET_KE"]));
                                    dr["THOI_GIAN_QUI_DOI"] = hsBT * tgThietKe;
                                    dr["DON_GIA_THUC_TE"] = hsBT * tgThietKe * Convert.ToDecimal(txtHS.EditValue) * Convert.ToDecimal(txtDG.EditValue);
                                    //grvQT.SetFocusedRowCellValue("THOI_GIAN_QUI_DOI", hsBT * tgTK);
                                    //grvQT.SetFocusedRowCellValue("DON_GIA_THUC_TE", hsBT * tgTK * hsDG * dgG);


                                    dr["HS_HT_DG"] = txtHS.EditValue;
                                    dr["DON_GIA_GIAY"] = txtDG.EditValue;

                                    dt.Rows.Add(dr);
                                }
                                dt.DefaultView.Sort = "THU_TU_CONG_DOAN ASC";
                                dt = dt.DefaultView.ToTable();
                                dt.AcceptChanges();

                                //Loadcbo HSBT
                            }
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message);
                        }
                        break;
                    }
                case "luu":
                    {
                        isAdd = false;
                        SetButton(isAdd);
                        Validate();
                        if (grvQT.HasColumnErrors) return;
                        Savedata();
                        Commons.Modules.ObjSystems.DeleteAddRow(grvQT);
                        LoadLuoi();
                        LocData();
                        break;
                    }
                case "khongluu":
                    {
                        isAdd = false;
                        Commons.Modules.ObjSystems.DeleteAddRow(grvQT);
                        SetButton(isAdd);
                        LoadLuoi();
                        LocData();
                        grvQT.OptionsBehavior.Editable = false;
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

        private void optHT_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadHD(0);
        }

        private void cboLMH_EditValueChanged(object sender, EventArgs e)
        {
            LoadCboCum(0);
        }

        private void txtDG_Validated(object sender, EventArgs e)
        {

        }

        private void grvQT_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)grdQT.DataSource;
                if (dt.Rows.Count == 0)
                {
                    ttCD++;
                    ttChuyen++;
                }
                else
                {
                    ttCD = ttChuyen = string.IsNullOrEmpty((dt.Rows[dt.Rows.Count - 1]["THU_TU_CONG_DOAN"].ToString())) ? 1 : Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["THU_TU_CONG_DOAN"]) + 1;
                }
                view.SetFocusedRowCellValue("CD_DUNG_CHUNG", 0);
                view.SetFocusedRowCellValue("ID_CHUYEN", cboChuyen.EditValue);
                view.SetFocusedRowCellValue("ID_CUM", cboCum.EditValue);
                view.SetFocusedRowCellValue("ID_ORD", cboOrd.EditValue);
                view.SetFocusedRowCellValue("THU_TU_CONG_DOAN", ttCD);
                view.SetFocusedRowCellValue("MaQL", ttChuyen);
                view.SetFocusedRowCellValue("HS_HT_DG", txtHS.EditValue);
                view.SetFocusedRowCellValue("DON_GIA_GIAY", txtDG.EditValue);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void BorderAround(Excel.Range range)
        {
            Excel.Borders borders = range.Borders;
            borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders.Color = Color.Black;
            borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
            borders[Excel.XlBordersIndex.xlDiagonalUp].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
            borders[Excel.XlBordersIndex.xlDiagonalDown].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
        }
    }
}