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
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;
using Vs.Report;

namespace VietSoftHRM
{
    public partial class frmQTCN : DevExpress.XtraEditors.XtraForm
    {
        private bool isAdd = false;
        string sCnstr = "Server=192.168.2.5;database=DATA_MT;uid=sa;pwd=123;Connect Timeout=0;"; 
        public frmQTCN()
        {
            InitializeComponent();
            optHT.SelectedIndex = 0;
        }
               
        private void frmQTCN_Load(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";
            
            try
            {
                LoadLuoi();
                LoadCbo();
                LoadHD(0);
            }
            catch (Exception ex) { XtraMessageBox.Show(ex.Message.ToString()); }

            Commons.Modules.sPS = "";
        }

        private void LoadHD(int iLoad)
        {
            Commons.Modules.sPS = "0LoadCbo";
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
                conn = new System.Data.SqlClient.SqlConnection(sCnstr);
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
                if (iLoad == 0) Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboKH, dt, "MS_KH", "TEN_CONG_TY", "TEN_CONG_TY");

                dt = new DataTable();
                dt = ds.Tables[1].Copy();
                dt.TableName = "HOP_DONG";
                if (iLoad == 0 || iLoad == 1) Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboHD, dt, "MS_DDH", "TEN_HD", "TEN_HD");


                dt = new DataTable();
                dt = ds.Tables[2].Copy();
                dt.TableName = "MA_HANG";
                if (iLoad == 0 || iLoad == 1 || iLoad == 2) Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboMH, dt, "MS_MH", "MS_MHK", "MS_MHK");

                dt = new DataTable();
                dt = ds.Tables[3].Copy();
                dt.TableName = "TEN_ORDER";
                if (iLoad == 0 || iLoad == 1 || iLoad == 2 || iLoad == 3) Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboOrd, dt, "ORDER", "TEN_ORD", "TEN_ORD");
                                             
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
                string sSql = "SELECT STT_CHUYEN, TEN_CHUYEN FROM CHUYEN UNION SELECT '-1', '' FROM CHUYEN ORDER BY CHUYEN.TEN_CHUYEN";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(sCnstr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboChuyen, dt, "STT_CHUYEN", "TEN_CHUYEN", "TEN_CHUYEN");
                cboChuyen.Properties.View.Columns[0].Caption = "STT Chuyền";
                cboChuyen.Properties.View.Columns[1].Caption = "Tên Chuyền";
                cboChuyen.Properties.View.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboChuyen.Properties.View.Columns[1].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cboChuyen.Properties.View.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboChuyen.Properties.View.Columns[0].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                
                sSql = "SELECT MS_LOAI_SP, TEN_LOAI_SAN_PHAM FROM LOAI_SAN_PHAM UNION SELECT '-1', '' FROM LOAI_SAN_PHAM ORDER BY TEN_LOAI_SAN_PHAM";
                dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(sCnstr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboLMH, dt, "MS_LOAI_SP", "TEN_LOAI_SAN_PHAM", "TEN_LOAI_SAN_PHAM");
                
            }
            catch { }
        }

        private void LoadCboCum(int LSP)
        {
            try
            {
                string sSql = "SELECT MS_CUM, TEN_CUM FROM CUM WHERE MS_LOAI_SP = " + LSP + " ORDER BY CUM.STT ";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(sCnstr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboCum, dt, "MS_CUM", "TEN_CUM", "TEN_CUM");
                cboCum.Properties.View.Columns[0].Caption = "Mã cụm";
                cboCum.Properties.View.Columns[1].Caption = "Tên cụm";
                cboCum.Properties.View.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboCum.Properties.View.Columns[1].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                cboCum.Properties.View.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboCum.Properties.View.Columns[0].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                
            }
            catch { }
        }

        DataTable dtBT;
        DataTable dtCD, dtLoaiMay;
        private void LoadLuoi()
        {
            Commons.Modules.sPS = "0Load";
            String sDDH, sMH, sOrd;
            sDDH = "-1"; sMH = "-1"; sOrd = "-1";

            try { sDDH = cboHD.EditValue.ToString(); } catch { }
            try { sMH = cboMH.EditValue.ToString(); } catch { }
            try { sOrd = cboOrd.EditValue.ToString(); } catch { }

            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(sCnstr, "spQTCNGet", sDDH, sMH, sOrd));
            dt.Columns["MS_CD"].ReadOnly = false;
            dt.Columns["CD_DUNG_CHUNG"].ReadOnly = false;
            dt.Columns["STT_CHUYEN"].ReadOnly = false;
            dt.Columns["MS_DDH"].ReadOnly = false;
            dt.Columns["MS_MH"].ReadOnly = false;
            dt.Columns["ORDER"].ReadOnly = false;

            Commons.Modules.ObjSystems.MLoadXtraGrid(grdQT, grvQT, dt, false, false, true, true, true, this.Name);

            dtCD = new DataTable();
            dtLoaiMay = new DataTable();

            dtCD.Load(SqlHelper.ExecuteReader(sCnstr, "spGetComboCongDoan", 1));
            Commons.Modules.ObjSystems.AddCombo("MS_CD", "TEN_CD", grvQT, dtCD, true);

            dtLoaiMay = new DataTable();
            dtLoaiMay.Load(SqlHelper.ExecuteReader(sCnstr, "spGetComboLoaiMay", 1));
            Commons.Modules.ObjSystems.AddCombo("MS_LOAI_MAY", "TEN_LOAI_MAY", grvQT, dtLoaiMay, true);

            dtBT = new DataTable();
            dtBT.Load(SqlHelper.ExecuteReader(sCnstr, "spGetComboBacTho", 1));
            Commons.Modules.ObjSystems.AddCombo("BAC_THO", "BAC_THO_IN", grvQT, dtBT, true);

            FormatGrid();
            SetButton(isAdd);
        }

        private void FormatGrid()
        {
            //An cot
            grvQT.Columns["STT_CHUYEN"].Visible = false;
            grvQT.Columns["MS_DDH"].Visible = false;
            grvQT.Columns["MS_MH"].Visible = false;
            grvQT.Columns["ORDER"].Visible = false;
            grvQT.Columns["MS_CUM"].Visible = false;

            grvQT.Columns["THOI_GIAN_THIET_KE"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            grvQT.Columns["THOI_GIAN_THIET_KE"].DisplayFormat.FormatString = Commons.Modules.sSoLeSL;
            grvQT.Columns["THOI_GIAN_QUI_DOI"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            grvQT.Columns["THOI_GIAN_QUI_DOI"].DisplayFormat.FormatString = Commons.Modules.sSoLeSL;
            grvQT.Columns["HS_HT_DG"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            grvQT.Columns["HS_HT_DG"].DisplayFormat.FormatString = Commons.Modules.sSoLeDG;
            grvQT.Columns["DON_GIA_GIAY"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            grvQT.Columns["DON_GIA_GIAY"].DisplayFormat.FormatString = Commons.Modules.sSoLeDG;
            grvQT.Columns["DON_GIA_THUC_TE"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            grvQT.Columns["DON_GIA_THUC_TE"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;

            grvQT.Columns["THU_TU_CONG_DOAN"].Width = 50;
            grvQT.Columns["MaQL"].Width = 50;
            grvQT.Columns["BAC_THO"].Width = 80;
            grvQT.Columns["HE_SO_BAC_THO"].Width = 70;
            grvQT.Columns["MS_CD"].Width = 400;
            grvQT.Columns["CD_DUNG_CHUNG"].Width = 90;
            grvQT.Columns["THOI_GIAN_THIET_KE"].Width = 70;
            grvQT.Columns["THOI_GIAN_QUI_DOI"].Width = 70;
            grvQT.Columns["DON_GIA_GIAY"].Width = 70;
            grvQT.Columns["DON_GIA_THUC_TE"].Width = 100;
            grvQT.Columns["MS_LOAI_MAY"].Width = 100;
            grvQT.Columns["HS_HT_DG"].UnboundExpression = txtHS.Text;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboKH_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0LoadCbo") return;
            LoadHD(1);
            Commons.Modules.sPS = "";
        }

        private void cboMH_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0LoadCbo") return;
            LoadHD(3);

            GridView view = cboMH.Properties.View;
            int rowHandle = view.FocusedRowHandle;
            string fieldName = "MS_LOAI_SP"; // or other field name  
            object value = view.GetRowCellValue(rowHandle, fieldName);
            cboLMH.EditValue = Convert.ToInt32(value);
            LoadCboCum(Convert.ToInt32(value));
            LoadLuoi();
            Commons.Modules.sPS = "";
        }

        private void cboHD_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0LoadCbo") return;
            LoadHD(2);
            LoadLuoi();
            Commons.Modules.sPS = "";

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
            if (Commons.Modules.sPS == "0LoadCbo") return;
            DataTable dtTmp = new DataTable();
            try
            {
                dtTmp = (DataTable)grdQT.DataSource;
                String sCum, sChuyen;
                string sDK = " 1 = 1 ";
                sCum = "-1"; sChuyen = "-1";
                try { sCum = cboCum.EditValue.ToString(); } catch { }
                try { sChuyen = cboChuyen.EditValue.ToString(); } catch { }

                if (sCum != "-1") sDK = sDK + " AND MS_CUM = '"+ sCum +"' ";
                if (sChuyen != "-1") sDK = sDK + " AND STT_CHUYEN = '" + sChuyen + "' ";

                dtTmp.DefaultView.RowFilter = sDK;
            }
            catch { dtTmp.DefaultView.RowFilter = ""; }
        }

        private void cboCum_EditValueChanged(object sender, EventArgs e)
        {
            LocData();
        }

        private void cboChuyen_EditValueChanged(object sender, EventArgs e)
        {
            LocData();
        }

        /// <summary>
        /// Set btn Enable
        /// </summary>
        /// <param name="isAdd"></param>
        private void SetButton(bool isAdd)
        {
            btnThemSua.Visible = !isAdd;
            btnXoa.Visible = !isAdd;
            btnIn.Visible = !isAdd;
            btnThoat.Visible = !isAdd;
            btnGhi.Visible = isAdd;
            btnKhongGhi.Visible = isAdd;

            optHT.Enabled = !isAdd;
            cboKH.Enabled = !isAdd;
            cboHD.Enabled = !isAdd;
            cboMH.Enabled = !isAdd;
            cboLMH.Enabled = !isAdd;
            cboOrd.Enabled = !isAdd;
            cboChuyen.Enabled = !isAdd;
            dNgayLap.Enabled = !isAdd;
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
        private void btnThemSua_Click(object sender, EventArgs e)
        {
            if (cboChuyen.Text == "")
            {
                XtraMessageBox.Show("Chưa nhập STT_CHUYEN");
                return;
            }
            if (cboHD.Text == "")
            {
                XtraMessageBox.Show("Chưa nhập HD");
                return;
            }
            if (cboMH.Text == "")
            {
                XtraMessageBox.Show("Chưa nhập Mã hàng");
                return;
            }
            if (cboOrd.Text == "")
            {
                XtraMessageBox.Show("Chưa nhập Order");
                return;
            }
            GetStt(ref ttCD, ref ttChuyen);
            isAdd = true;
            SetButton(isAdd);
            grvQT.OptionsBehavior.Editable = true;
            Commons.Modules.ObjSystems.AddnewRow(grvQT, true);
            grvQT.SetRowCellValue(grvQT.RowCount -1, "THU_TU_CONG_DOAN", ttCD + 1);
            grvQT.SetRowCellValue(grvQT.RowCount - 1, "MaQL", ttChuyen + 1 + 1);
        }

        private void DefaulValue()
        {

        }

        private void GetStt(ref int ttCD, ref int ttChuyen)
        {
            ttCD = ttChuyen = 0;
            try
            {
                if (grvQT.RowCount == 0)
                    return;
                Int32.TryParse(Commons.Modules.ObjSystems.ConvertDatatable(grvQT).AsEnumerable().Max(row => row["THU_TU_CONG_DOAN"]).ToString(), out ttCD);
                Int32.TryParse(Commons.Modules.ObjSystems.ConvertDatatable(grvQT).AsEnumerable().Max(row => row["MaQL"]).ToString(), out ttChuyen);
                return;
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
        private void btnKhongGhi_Click(object sender, EventArgs e)
        {
            isAdd = false;
            Commons.Modules.ObjSystems.DeleteAddRow(grvQT);
            SetButton(isAdd);
            LoadLuoi();
            grvQT.OptionsBehavior.Editable = false;
        }


        /// <summary>
        /// btn Xoa Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sSql = "";
            try
            {
                if (grvQT.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
                if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;

                sSql = "DELETE QUI_TRINH_CONG_NGHE_CHI_TIET WHERE STT_CHUYEN = '" + grvQT.GetFocusedRowCellValue("STT_CHUYEN") +
                                                        "' AND MS_DDH = '" + grvQT.GetFocusedRowCellValue("MS_DDH") +
                                                        "' AND MS_MH = '" + grvQT.GetFocusedRowCellValue("MS_MH") +
                                                        "' AND [ORDER] = '" + grvQT.GetFocusedRowCellValue("ORDER") +
                                                        "' AND MS_CD = '" + grvQT.GetFocusedRowCellValue("MS_CD") +  "'";
                SqlHelper.ExecuteNonQuery(sCnstr, CommandType.Text, sSql);
                grvQT.DeleteSelectedRows();
            }
            catch
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
            }
        }

        private void grvQT_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //Validate();
            if (grvQT.RowCount == 0) return;
            GridView view = sender as GridView;
            try
            {
                Decimal hsBT, tgTK, dgG, hsDG;
                Decimal.TryParse(grvQT.GetFocusedRowCellValue("HE_SO_BAC_THO").ToString(), out hsBT);
                Decimal.TryParse(grvQT.GetFocusedRowCellValue("THOI_GIAN_THIET_KE").ToString(), out tgTK);
                Decimal.TryParse(grvQT.GetFocusedRowCellValue("DON_GIA_GIAY").ToString(), out dgG);
                Decimal.TryParse(grvQT.GetFocusedRowCellValue("HS_HT_DG").ToString(), out hsDG);

                if (e.Column.FieldName == "BAC_THO")
                {
                    hsBT = Convert.ToDecimal((from DataRow row in dtBT.Rows
                                                      where row["BAC_THO"] == e.Value
                                                      select row["HE_SO_BAC_THO"]).FirstOrDefault());
                    grvQT.SetFocusedRowCellValue("HE_SO_BAC_THO", hsBT);
                }
                if (e.Column.FieldName == "MS_CD")
                {
                    tgTK = Convert.ToDecimal((from DataRow row in dtCD.Rows
                                                  where row["MS_CD"] == e.Value
                                                  select row["TGTK"]).FirstOrDefault());

                    grvQT.SetFocusedRowCellValue("THOI_GIAN_THIET_KE", tgTK);
                }
                if(e.Column.FieldName == "HE_SO_BAC_THO" || e.Column.FieldName == "THOI_GIAN_THIET_KE")
                {
                    grvQT.SetFocusedRowCellValue("THOI_GIAN_QUI_DOI", hsBT * tgTK );
                    grvQT.SetFocusedRowCellValue("DON_GIA_THUC_TE", hsBT * tgTK * dgG * hsDG);
                }
                if(e.Column.FieldName == "DON_GIA_GIAY" || e.Column.FieldName == "HS_HT_DG")
                {
                    grvQT.SetFocusedRowCellValue("DON_GIA_THUC_TE", hsBT * tgTK * dgG * hsDG);
                }
            }
            catch { }

        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            isAdd = false;
            SetButton(isAdd);
            Validate();
            if (grvQT.HasColumnErrors) return;
            Savedata();
            Commons.Modules.ObjSystems.DeleteAddRow(grvQT);
            LoadLuoi();
        }
        private void Savedata()
        {
            string stbQT = "stbQT" + Commons.Modules.UserName;
            try
            {
                //tạo một datatable 
                Commons.Modules.ObjSystems.MCreateTableToDatatable(sCnstr, stbQT, Commons.Modules.ObjSystems.ConvertDatatable(grvQT), "");
                string sSql = "UPDATE QUI_TRINH_CONG_NGHE_CHI_TIET SET THU_TU_CONG_DOAN = tmp.THU_TU_CONG_DOAN "
                            + " , MaQL = tmp.MaQL, MS_LOAI_MAY = tmp.MS_LOAI_MAY, BAC_THO = tmp.BAC_THO, THOI_GIAN_THIET_KE = tmp.THOI_GIAN_THIET_KE,"
                            + " THOI_GIAN_QUI_DOI = tmp.THOI_GIAN_QUI_DOI, HS_HT_DG = tmp.HS_HT_DG, DON_GIA_GIAY = tmp.DON_GIA_GIAY, DON_GIA_THUC_TE "
                            + " = tmp.DON_GIA_THUC_TE, CD_DUNG_CHUNG = tmp.CD_DUNG_CHUNG, YEU_CAU_KT = tmp.YEU_CAU_KT "
                            + " FROM QUI_TRINH_CONG_NGHE_CHI_TIET QT "
                            + " INNER JOIN "+ stbQT + " tmp ON QT.STT_CHUYEN = tmp.STT_CHUYEN AND QT.MS_DDH = tmp.MS_DDH AND QT.MS_MH = tmp.MS_MH "
                            + " AND QT.[ORDER] = tmp.[ORDER] AND QT.MS_CD = tmp.MS_CD "
                            + " INSERT INTO QUI_TRINH_CONG_NGHE_CHI_TIET(THU_TU_CONG_DOAN, MaQL, MS_CD, MS_LOAI_MAY, BAC_THO, THOI_GIAN_THIET_KE, "
                            + " THOI_GIAN_QUI_DOI, HS_HT_DG, DON_GIA_GIAY, DON_GIA_THUC_TE, CD_DUNG_CHUNG, YEU_CAU_KT, STT_CHUYEN, MS_DDH, MS_MH, [ORDER])"
                            + " SELECT THU_TU_CONG_DOAN, MaQL, MS_CD, MS_LOAI_MAY, BAC_THO, THOI_GIAN_THIET_KE, THOI_GIAN_QUI_DOI, HS_HT_DG,"
                            + " DON_GIA_GIAY, DON_GIA_THUC_TE, CD_DUNG_CHUNG, YEU_CAU_KT, STT_CHUYEN, MS_DDH, MS_MH, [ORDER]"
                            + " FROM " + stbQT + " tmp1 WHERE NOT EXISTS(SELECT* FROM QUI_TRINH_CONG_NGHE_CHI_TIET QTCT"
                            + " WHERE tmp1.STT_CHUYEN = QTCT.STT_CHUYEN AND tmp1.MS_DDH = QTCT.MS_DDH AND tmp1.MS_MH = QTCT.MS_MH"
                            + " AND tmp1.[ORDER] = QTCT.[ORDER] AND tmp1.MS_CD = QTCT.MS_CD)";

                SqlHelper.ExecuteNonQuery(sCnstr, CommandType.Text, sSql);

                    string strSql1 = "DROP TABLE " + stbQT;
                    SqlHelper.ExecuteScalar(sCnstr, CommandType.Text, strSql1);
                Commons.Modules.ObjSystems.XoaTable(stbQT);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtHS_Validated(object sender, EventArgs e)
        {
            if (txtHS.Text == "") return;
            decimal _hs;
            decimal.TryParse(txtHS.Text, out _hs);
            string sTBGrv = "QTCN_ADD_HS" + Commons.Modules.UserName;
            DataTable dt = new DataTable();
            string sSql = "";
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(sCnstr, sTBGrv, Commons.Modules.ObjSystems.ConvertDatatable(grvQT), "");
                sSql = " SELECT THU_TU_CONG_DOAN, MaQL, MS_CD, MS_LOAI_MAY, BAC_THO, HE_SO_BAC_THO, THOI_GIAN_THIET_KE, " +
                       " THOI_GIAN_QUI_DOI, " + _hs + " HS_HT_DG, DON_GIA_GIAY, " +
                       _hs + " * ISNULL(THOI_GIAN_THIET_KE,0) * ISNULL(HE_SO_BAC_THO,0) * ISNULL(DON_GIA_GIAY,0) DON_GIA_THUC_TE, CD_DUNG_CHUNG, YEU_CAU_KT, MS_CUM, STT_CHUYEN, MS_DDH, " +
                       " MS_MH, [ORDER] FROM " + sTBGrv + "";
                dt.Load(SqlHelper.ExecuteReader(sCnstr, CommandType.Text, sSql));

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdQT, grvQT, dt, true, false, true, true, true, this.Name);

                Commons.Modules.ObjSystems.XoaTable(sTBGrv);

                FormatGrid();
            }
            catch (Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());
            }
            
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "rptQuiTrinhCongNgheChiTiet", Commons.Modules.UserName, Commons.Modules.TypeLanguage,
                                //cboHD.EditValue, cboMH.EditValue, cboOrd.EditValue));
                                -1, -1, -1));

                DataTable dtQTCNLoaiMay = new DataTable();
                dtQTCNLoaiMay.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "rptQuiTrinhCongNgheLoaiMay", Commons.Modules.UserName, Commons.Modules.TypeLanguage,
                //cboHD.EditValue, cboMH.EditValue, cboOrd.EditValue));
                -1, -1, -1));


                DataTable GroupFooter = new DataTable();

                GroupFooter.Columns.Add("MaQL", typeof(String));
                GroupFooter.Columns.Add("TEN_CUM", typeof(String));
                GroupFooter.Columns.Add("SUM_THOI_GIAN_THIET_KE", typeof(double));
                GroupFooter.Columns.Add("SUM_THOI_GIAN_QUI_DOI", typeof(double));
                GroupFooter.Columns.Add("SUM_DMLD", typeof(double));
                GroupFooter.Columns.Add("SUM_DON_GIA_THUC_TE", typeof(double));

                var Gp = from d in dt.AsEnumerable()
                         group d by d.Field<String>("TEN_CUM") into g
                         select new
                         {
                             MaQL = g.First().Field<String>("MaQL"),
                             TEN_CUM = g.First().Field<String>("TEN_CUM"),
                             SUM_THOI_GIAN_THIET_KE = g.Sum(gp => gp.Field<double>("THOI_GIAN_THIET_KE")),
                             SUM_THOI_GIAN_QUI_DOI = g.Sum(gp => gp.Field<double>("THOI_GIAN_QUI_DOI")),
                             SUM_DMLD = g.Sum(gp => gp.Field<double>("DMLD")),
                             SUM_DON_GIA_THUC_TE = g.Sum(gp => gp.Field<double>("DON_GIA_THUC_TE")),
                         };

                foreach (var x in Gp)
                {
                    DataRow newRow = GroupFooter.NewRow();

   
                    newRow.SetField("MaQL", x.MaQL);
                    newRow.SetField("TEN_CUM", x.TEN_CUM);
                    newRow.SetField("SUM_THOI_GIAN_THIET_KE", x.SUM_THOI_GIAN_THIET_KE);
                    newRow.SetField("SUM_THOI_GIAN_QUI_DOI", x.SUM_THOI_GIAN_QUI_DOI);
                    newRow.SetField("SUM_DMLD", x.SUM_DMLD);
                    newRow.SetField("SUM_DON_GIA_THUC_TE", x.SUM_DON_GIA_THUC_TE);
                    GroupFooter.Rows.Add(newRow);
                };

                frmViewReport frm = new frmViewReport();
                //Convert.ToInt64(grvCongNhan.GetFocusedRowCellValue("ID_CN"))
                //string tieuDe = "QUY TRINH CONG NGHE";
                frm.rpt = new rptBCQuyTrinhCongNghe(DateTime.Now, GroupFooter, dtQTCNLoaiMay);
                if (dt == null || dt.Rows.Count == 0) return;
                dt.TableName = "DATA";
                GroupFooter.TableName = "DATA_CHILD";

                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ds.Tables.Add(GroupFooter);
                frm.AddDataSource(ds);

                //frm.AddDataSource(GroupFooter);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }

            //try
            //{
            //    //Process.Start(@"D:\02-Vietsoft\08-Phan mem Demo\03-HRMS\02-Phan mem net\ChamCong.exe");
            //    Process.Start(@"D:\VietSoft_ERP\FileExcel\QTCN.xls");
            //}
            //catch
            //{

            //}
        }

        private void txtDG_Validated(object sender, EventArgs e)
        {
            if (txtDG.Text == "") return;
            decimal _dg;
            decimal.TryParse(txtHS.Text, out _dg);
            string sTBGrv = "QTCN_ADD_DG" + Commons.Modules.UserName;
            DataTable dt = new DataTable();
            string sSql = "";
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(sCnstr, sTBGrv, Commons.Modules.ObjSystems.ConvertDatatable(grvQT), "");
                sSql = " SELECT THU_TU_CONG_DOAN, MaQL, MS_CD, MS_LOAI_MAY, BAC_THO, HE_SO_BAC_THO, THOI_GIAN_THIET_KE, " +
                       " THOI_GIAN_QUI_DOI, HS_HT_DG," + _dg + " DON_GIA_GIAY, " +
                       _dg + " * ISNULL(THOI_GIAN_THIET_KE,0) * ISNULL(HS_HT_DG,0) * ISNULL(HE_SO_BAC_THO,0) DON_GIA_THUC_TE, CD_DUNG_CHUNG, YEU_CAU_KT, MS_CUM, STT_CHUYEN, MS_DDH, " +
                       " MS_MH, [ORDER] FROM " + sTBGrv + "";
                dt.Load(SqlHelper.ExecuteReader(sCnstr, CommandType.Text, sSql));

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdQT, grvQT, dt, true, false, true, true, true, this.Name);

                Commons.Modules.ObjSystems.XoaTable(sTBGrv);

                FormatGrid();
            }
            catch (Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());
            }
        }

        private void grvQT_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                view.SetFocusedRowCellValue("CD_DUNG_CHUNG", 0);
                view.SetFocusedRowCellValue("STT_CHUYEN", cboChuyen.EditValue);
                view.SetFocusedRowCellValue("MS_DDH", cboHD.EditValue);
                view.SetFocusedRowCellValue("MS_MH", cboMH.EditValue);
                view.SetFocusedRowCellValue("ORDER", cboOrd.EditValue);
                view.SetFocusedRowCellValue("THU_TU_CONG_DOAN", ttCD + 1);
                view.SetFocusedRowCellValue("MaQL", ttChuyen + 1);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
    }
}