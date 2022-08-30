using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vs.Report;

namespace Vs.Recruit
{
    public partial class ucChuyenDuLieuNS : DevExpress.XtraEditors.XtraUserControl
    {
        private bool flag = false;
        private int iAdd = 0;
        public AccordionControl accorMenuleft;
        public ucChuyenDuLieuNS()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, btnALL);
        }
        private void ucChuyenDuLieuNS_Load(object sender, EventArgs e)
        {
            try
            {

                Commons.Modules.sLoad = "0Load";
                LoadThang();
                Commons.OSystems.SetDateEditFormat(datTNgay);
                Commons.OSystems.SetDateEditFormat(datDNgay);
                Commons.Modules.sLoad = "";
                datTNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year));
                //LoadData();
                grvDSUngVien_FocusedRowChanged(null, null);

                enabel(true);
            }
            catch (Exception ex)
            {
            }
        }
        public void LoadThang()
        {
            try
            {
                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT DISTINCT SUBSTRING(CONVERT(VARCHAR(10),NGAY_CHUYEN,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),NGAY_CHUYEN,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),NGAY_CHUYEN,103),10) AS NGAY ,RIGHT(CONVERT(VARCHAR(10),NGAY_CHUYEN,103),7) AS THANG  FROM dbo.UV_CHUYEN_NHAN_SU ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                if (grdThang.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                    grvThang.Columns["M"].Visible = false;
                    grvThang.Columns["Y"].Visible = false;
                    grvThang.Columns["THANG"].Visible = false;
                }
                else
                {
                    grdThang.DataSource = dtthang;
                }
                cboThang.Text = grvThang.GetFocusedRowCellValue("NGAY").ToString();
            }
            catch
            {
                DateTime now = DateTime.Now;
                cboThang.Text = now.ToString("dd/MM/yyyy");
            }
        }
        private void LoadData()
        {
            try
            {
                //DataTable dt = new DataTable();
                //dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spTiepNhanUV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToInt64(cboID_PV.EditValue)));
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetListUVChuyen", conn);

                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datTNgay.Text);
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(datDNgay.Text);
                cmd.Parameters.Add("@NGAY_CHUYEN", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                cmd.Parameters.Add("@Them", SqlDbType.Int).Value = iAdd;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.Columns["CHON"].ReadOnly = false;
                //dt.Columns["XAC_NHAN_DL"].ReadOnly = false;
                //dt.Columns["NGAY_CO_THE_DI_LAM"].ReadOnly = false;
                //dt.Columns["XAC_NHAN_DL"].ReadOnly = false;
                //dt.Columns["NGAY_NHAN_VIEC"].ReadOnly = false;
                //dt.Columns["ID_DGTN"].ReadOnly = false;
                //dt.Columns["XAC_NHAN_DTDH"].ReadOnly = false;
                //dt.Columns["DA_GIOI_THIEU"].ReadOnly = false;
                //dt.Columns["HUY_TUYEN_DUNG"].ReadOnly = false;

                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSUngVien, grvDSUngVien, dt, true, true, false, true, true, this.Name);
                grvDSUngVien.Columns["CHON"].Visible = false;
                grvDSUngVien.Columns["ID_UV"].Visible = false;
                grvDSUngVien.Columns["MS_UV"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["NGAY_SINH"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["GIOI_TINH"].OptionsColumn.AllowEdit = false;
                grvDSUngVien.Columns["NGAY_NHAN_VIEC"].OptionsColumn.AllowEdit = false;

                if (iAdd == 0)
                {
                    grvDSUngVien.Columns["ID_XN"].Visible = false;
                    grvDSUngVien.OptionsSelection.MultiSelect = false;
                    grvDSUngVien.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                }
                else
                {
                    grvDSUngVien.Columns["ID_YCTD"].Visible = false;
                    grvDSUngVien.Columns["ID_VTTD"].Visible = false;

                    grvDSUngVien.OptionsSelection.MultiSelect = true;
                    grvDSUngVien.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;

                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_XN = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                    cboID_XN.NullText = "";
                    cboID_XN.ValueMember = "ID_XN";
                    cboID_XN.DisplayMember = "TEN_XN";
                    //ID_VTTD,TEN_VTTD
                    cboID_XN.DataSource = Commons.Modules.ObjSystems.DataXiNghiep(-1, false);
                    cboID_XN.Columns.Clear();
                    cboID_XN.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_XN"));
                    cboID_XN.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_XN"));
                    cboID_XN.Columns["TEN_XN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_XN");
                    cboID_XN.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboID_XN.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    cboID_XN.Columns["ID_XN"].Visible = false;
                    grvDSUngVien.Columns["ID_XN"].ColumnEdit = cboID_XN;
                    cboID_XN.BeforePopup += cboID_XN_BeforePopup;
                    cboID_XN.EditValueChanged += cboID_XN_EditValueChanged;


                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboTo = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                    cboTo.NullText = "";
                    cboTo.ValueMember = "ID_TO";
                    cboTo.DisplayMember = "TEN_TO";
                    //ID_VTTD,TEN_VTTD
                    cboTo.DataSource = Commons.Modules.ObjSystems.DataTo(-1, -1, false);
                    cboTo.Columns.Clear();
                    cboTo.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_TO"));
                    cboTo.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_TO"));
                    cboTo.Columns["TEN_TO"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_TO");
                    cboTo.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboTo.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    cboTo.Columns["ID_TO"].Visible = false;
                    grvDSUngVien.Columns["ID_TO"].ColumnEdit = cboTo;
                    cboTo.BeforePopup += cboTo_BeforePopup;
                    cboTo.EditValueChanged += cboTo_EditValueChanged;


                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboTTHT = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                    cboTTHT.NullText = "";
                    cboTTHT.ValueMember = "ID_TT_HT";
                    cboTTHT.DisplayMember = "TEN_TT_HT";
                    //ID_VTTD,TEN_VTTD
                    cboTTHT.DataSource = Commons.Modules.ObjSystems.DataTinHTrangHT(false);
                    cboTTHT.Columns.Clear();
                    cboTTHT.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_TT_HT"));
                    cboTTHT.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_TT_HT"));
                    cboTTHT.Columns["TEN_TT_HT"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_TT_HT");
                    cboTTHT.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboTTHT.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    cboTTHT.Columns["ID_TT_HT"].Visible = false;
                    grvDSUngVien.Columns["ID_TT_HT"].ColumnEdit = cboTTHT;
                    cboTTHT.BeforePopup += cboTTHT_BeforePopup;
                    cboTTHT.EditValueChanged += cboTTHT_EditValueChanged;

                    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboTTHD = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                    cboTTHD.NullText = "";
                    cboTTHD.ValueMember = "ID_TT_HD";
                    cboTTHD.DisplayMember = "TEN_TT_HD";
                    //ID_VTTD,TEN_VTTD
                    cboTTHD.DataSource = Commons.Modules.ObjSystems.DataTinHTrangHD(false);
                    cboTTHD.Columns.Clear();
                    cboTTHD.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_TT_HD"));
                    cboTTHD.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_TT_HD"));
                    cboTTHD.Columns["TEN_TT_HD"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_TT_HD");
                    cboTTHD.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    cboTTHD.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    cboTTHD.Columns["ID_TT_HD"].Visible = false;
                    grvDSUngVien.Columns["ID_TT_HD"].ColumnEdit = cboTTHD;
                    cboTTHD.BeforePopup += cboTTHD_BeforePopup;
                    cboTTHD.EditValueChanged += cboTTHD_EditValueChanged;
                }
                try
                {
                    grvDSUngVien.OptionsSelection.CheckBoxSelectorField = "CHON";
                    grvDSUngVien.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
                catch { }
                //Commons.Modules.ObjSystems.AddCombXtra("ID_DGTN", "TEN_DGTN", "TEN_DGTN", grvDSUngVien, Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false), true, "ID_DGTN", this.Name, true);

                //ID_YCTD,MA_YCTD


            }
            catch (Exception ex) { }
        }
        private void cboID_XN_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSUngVien.SetFocusedRowCellValue("ID_XN", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboID_XN_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataXiNghiep(Convert.ToInt32(grvDSUngVien.GetFocusedRowCellValue("ID_DV")), false);
            }
            catch { }
        }
        private void cboTTHT_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSUngVien.SetFocusedRowCellValue("ID_TT_HT", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboTTHT_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataTinHTrangHT(false);
            }
            catch { }
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSUngVien.SetFocusedRowCellValue("ID_TO", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboTo_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(grvDSUngVien.GetFocusedRowCellValue("ID_DV")), Convert.ToInt32(grvDSUngVien.GetFocusedRowCellValue("ID_XN")), false);
            }
            catch { }
        }

        private void cboTTHD_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSUngVien.SetFocusedRowCellValue("ID_TT_HD", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboTTHD_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataTinHTrangHD(false);
            }
            catch { }
        }


        private void LoadCbo()
        {
            //try
            //{
            //    System.Data.SqlClient.SqlConnection conn;
            //    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            //    conn.Open();
            //    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spGetComboPV_TheoNgay", conn);

            //    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
            //    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            //    cmd.Parameters.Add("@CoAll", SqlDbType.Bit).Value = true;
            //    cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datTNgay.EditValue);
            //    cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datDNgay.EditValue);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
            //    DataSet ds = new DataSet();
            //    adp.Fill(ds);
            //    DataTable dt = new DataTable();
            //    dt = ds.Tables[0].Copy();
            //    Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_PV, dt, "ID_PV", "MA_SO", "MA_SO");
            //    if (dt.Rows.Count == 1)
            //    {
            //        cboID_PV.Properties.DataSource = dt.Clone();
            //        cboID_PV.EditValue = 0;
            //    }
            //}
            //catch { }
        }

        private void cboNDDT_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvDSUngVien.SetFocusedRowCellValue("ID_NDDT", Convert.ToInt64((dataRow.Row[0])));
        }

        private void cboNDDT_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataDanhNoiDungDT(false);
            }
            catch { }
        }
        private void enabel(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = visible;
            btnALL.Buttons[3].Properties.Visible = !visible;
            btnALL.Buttons[4].Properties.Visible = !visible;
            btnALL.Buttons[5].Properties.Visible = visible;

            grvDSUngVien.OptionsBehavior.Editable = !visible;
            cboThang.ReadOnly = !visible;
            datTNgay.Properties.ReadOnly = visible;
            datDNgay.Properties.ReadOnly = visible;

        }

        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "Inthenv":
                        {
                            string strSaveThongTinNhanVienDM = "strSaveThongTinNhanVienDM" + Commons.Modules.UserName;
                            try
                            {
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, strSaveThongTinNhanVienDM, Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien), "");

                                System.Data.SqlClient.SqlConnection conn;
                                DataTable dt = new DataTable();
                                DataTable dtbc = new DataTable();
                                DataTable ttc = new DataTable();
                                frmViewReport frm = new frmViewReport();

                                frm.rpt = new rptTheNhanVien_DM(DateTime.Now);

                                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                conn.Open();

                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spSaveThongTinNhanVienDM", conn);
                                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar, 50).Value = strSaveThongTinNhanVienDM;
                                cmd.CommandType = CommandType.StoredProcedure;

                                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                DataSet ds = new DataSet();
                                adp.Fill(ds);

                                dt = ds.Tables[1].Copy();
                                dt.TableName = "DA_TA";
                                frm.AddDataSource(dt);


                                ttc = ds.Tables[0].Copy();
                                ttc.TableName = "TTC";
                                frm.AddDataSource(ttc);

                                dtbc = ds.Tables[2].Copy();
                                dtbc.TableName = "NOI_DUNG";
                                frm.AddDataSource(dtbc);

                                frm.ShowDialog();

                                Commons.Modules.ObjSystems.XoaTable(strSaveThongTinNhanVienDM);
                                conn.Close();
                            }
                            catch
                            {
                                Commons.Modules.ObjSystems.XoaTable(strSaveThongTinNhanVienDM);
                            }
                            break;
                        }
                    case "themsua":
                        {
                            iAdd = 1;
                            LoadData();
                            enabel(false);
                            break;
                        }

                    case "xoa":
                        {
                            grvDSUngVien_FocusedRowChanged(null, null);
                            enabel(true);
                            break;
                        }
                    case "ghi":
                        {
                            if (grvDSUngVien.RowCount == 0)
                                return;
                            DataTable dt_CHON = new DataTable();
                            dt_CHON = ((DataTable)grdDSUngVien.DataSource);
                            if (dt_CHON.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).Count() == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChuaChonUngVien"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (flag == true) return;
                            if (!SaveData()) return;
                            iAdd = 0;
                            LoadData();
                            grvDSUngVien_FocusedRowChanged(null, null);
                            enabel(true);
                            break;
                        }
                    case "khongghi":
                        {
                            Commons.Modules.sLoad = "0Load";
                            iAdd = 0;
                            LoadData();
                            Commons.Modules.sLoad = "";
                            grvDSUngVien_FocusedRowChanged(null, null);
                            enabel(true);
                            break;
                        }
                    case "thoat":
                        {
                            Commons.Modules.ObjSystems.GotoHome(this);
                            break;
                        }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void grvDSUngVien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //LoadLuoiND();
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                //Commons.Modules.ObjSystems.RowFilter(grdTuan, grvTuan.Columns["ID_YCTD"], grvTuan.Columns["ID_VTTD"], grvVTYC.GetFocusedRowCellValue("ID_YCTD").ToString(), grvVTYC.GetFocusedRowCellValue("ID_VTTD").ToString());

            }
            catch (Exception ex)
            {
            }
        }

        private void datTNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            int t = DateTime.DaysInMonth(datTNgay.DateTime.Year, datTNgay.DateTime.Month);
            DateTime secondDateTime = new DateTime(datTNgay.DateTime.Year, Convert.ToInt32(datTNgay.DateTime.Month), t);
            datDNgay.EditValue = secondDateTime;
            LoadData();
            Commons.Modules.sLoad = "";
            //cboID_KHPV_EditValueChanged(null, null);
            //LoadData();
        }
        private void datDNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }
        private bool SaveData()
        {
            string sBTUngVien = "sBTUngVien" + Commons.Modules.iIDUser;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTUngVien, Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien), "");

                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spSaveChuyenDuLieuNS", Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text), sBTUngVien));
                Commons.Modules.ObjSystems.XoaTable(sBTUngVien);
                return true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sBTUngVien);
                return false;
            }
        }

        private void grvNoiDung_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                if (grvDSUngVien.RowCount == 0)
                {
                    return;
                }
            }
            catch
            {
            }
        }

        private void cboID_PV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }

        private void grvDSUngVien_RowCountChanged(object sender, EventArgs e)
        {
            grvDSUngVien_FocusedRowChanged(null, null);
        }
        private void calendarControl1_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboThang.Text = calThangc.DateTime.ToString("dd/MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grvThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + cboThang.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                }
                else { }
            }
            catch (Exception ex)
            {
                cboThang.Text = calThangc.DateTime.ToString("dd/MM/yyyy");
            }
            cboThang.ClosePopup();
        }

        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = grvThang.GetFocusedRowCellValue("NGAY").ToString();
            }
            catch { }
            cboThang.ClosePopup();

        }

        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }
        private void grvDSUngVien_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                //int ngay = 0;
                DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;

                DevExpress.XtraGrid.Columns.GridColumn idTo = View.Columns["ID_TO"];
                DevExpress.XtraGrid.Columns.GridColumn MS_CN = View.Columns["MS_CN"];
                DevExpress.XtraGrid.Columns.GridColumn MS_THE_CC = View.Columns["MS_THE_CC"];
                //DevExpress.XtraGrid.Columns.GridColumn ngayvaolam = View.Columns["NGAY_VAO_LAM_LAI"];
                //if (View.GetRowCellValue(e.RowHandle, mslydovang).ToString() == "")
                //{
                //    e.Valid = false;
                //    View.SetColumnError(mslydovang, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgKiemtraTenUserNULL", Commons.Modules.TypeLanguage)); return;
                //}
                if (View.GetRowCellValue(e.RowHandle, idTo).ToString() == "")
                {
                    flag = true;
                    e.Valid = false;
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgToKhongDuocTrong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    View.SetColumnError(idTo, "Tổ không được bỏ trống"); return;
                }
                if (View.GetRowCellValue(e.RowHandle, MS_CN).ToString() == "")
                {
                    flag = true;
                    e.Valid = false;
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgMSCNKhongDuocTrong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    View.SetColumnError(MS_CN, "Mã công nhân không được bỏ trống"); return;
                }
                if (View.GetRowCellValue(e.RowHandle, MS_THE_CC).ToString() == "")
                {
                    flag = true;
                    e.Valid = false;
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgMTCCKhongDuocTrong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    View.SetColumnError(MS_THE_CC, "Mã số thẻ chấm công không được bỏ trống"); return;
                }

                string strSQL = "SELECT COUNT(*) FROM dbo.CONG_NHAN WHERE MS_CN = '" + View.GetRowCellValue(e.RowHandle, MS_CN).ToString().Trim() + "'";
                int iSL = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));
                if (iSL > 0)
                {
                    flag = true;
                    e.Valid = false;
                    View.SetColumnError(MS_CN, "Mã số công nhân này đã có rồi"); return;
                }

                strSQL = "SELECT COUNT(*) FROM dbo.CONG_NHAN WHERE MS_THE_CC = '" + View.GetRowCellValue(e.RowHandle, MS_THE_CC).ToString().Trim() + "'";
                iSL = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, strSQL));
                if (iSL > 0)
                {
                    flag = true;
                    e.Valid = false;
                    View.SetColumnError(MS_THE_CC, "Mã số thẻ chấm công này đã có rồi"); return;
                }

                flag = false;

                //CheckDuplicateKHNP(grvKHNP, (DataTable)grdKHNP.DataSource, e);
            }
            catch (Exception ex) { }
        }

        private void grvDSUngVien_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvDSUngVien_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
    }
}
