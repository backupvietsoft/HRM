using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
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
    public partial class ucTiepNhanUngVien : DevExpress.XtraEditors.XtraUserControl
    {
        private bool flag = false;
        public AccordionControl accorMenuleft;
        private long iID_UV = -1;
        public ucTiepNhanUngVien()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, btnALL);
        }
        private void ucTiepNhanUngVien_Load(object sender, EventArgs e)
        {
            try
            {

                Commons.Modules.sLoad = "0Load";
                Commons.OSystems.SetDateEditFormat(datTNgay);
                Commons.OSystems.SetDateEditFormat(datDNgay);
                Commons.Modules.sLoad = "";
                datTNgay.EditValue = Convert.ToDateTime(("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year));
                //datTNgay.EditValue = DateTime.Now;
                LoadCbo();
                LoadData();
            }
            catch (Exception ex)
            {
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
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUngVien", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "TN_UNG_VIEN";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datTNgay.EditValue);
                cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datDNgay.EditValue);
                cmd.Parameters.Add("@TEN_DK", SqlDbType.NVarChar).Value = cboLocTheoNgay.EditValue;
                cmd.Parameters.Add("@MS_CV", SqlDbType.NVarChar).Value = cboMS_CV.EditValue;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_UV"] };


                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSUngVien, grvDSUngVien, dt, false, false, false, true, true, this.Name);
                grvDSUngVien.Columns["ID_UV"].Visible = false;
                grvDSUngVien.Columns["MS_CV"].Visible = false;
                grvDSUngVien.Columns["ID_YCTD"].Visible = false;
                grvDSUngVien.Columns["ID_VTTD"].Visible = false;
                grvDSUngVien.Columns["MS_UV"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvDSUngVien.Columns["HO_TEN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvDSUngVien.Columns["MUC_LUONG_DN"].DisplayFormat.FormatType = FormatType.Numeric;
                grvDSUngVien.Columns["MUC_LUONG_DN"].DisplayFormat.FormatString = "n0";

                if (Convert.ToInt32(cboMS_CV.EditValue) == 1)
                {
                    grvDSUngVien.Columns["NGAY_KT_TAY_NGHE"].Visible = false;
                    grvDSUngVien.Columns["TEN_DGTN"].Visible = false;
                    grvDSUngVien.Columns["CAN_DT_KY_NANG"].Visible = false;
                }
                else if (Convert.ToInt32(cboMS_CV.EditValue) == 2 || Convert.ToInt32(cboMS_CV.EditValue) == 3)
                {
                    grvDSUngVien.Columns["NGAY_PV"].Visible = false;
                    grvDSUngVien.Columns["NGAY_CO_THE_DI_LAM"].Visible = false;
                }

                if (iID_UV != -1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(iID_UV));
                    grvDSUngVien.FocusedRowHandle = grvDSUngVien.GetRowHandle(index);
                    grvDSUngVien.ClearSelection();
                    grvDSUngVien.SelectRow(index);
                }

                //DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboDGTN = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
                //cboDGTN.NullText = "";
                //cboDGTN.ValueMember = "ID_DGTN";
                //cboDGTN.DisplayMember = "TEN_DGTN";
                ////ID_VTTD,TEN_VTTD
                //cboDGTN.DataSource = Commons.Modules.ObjSystems.DataDanhGiaTayNghe(false);
                //cboDGTN.Columns.Clear();
                //cboDGTN.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_DGTN"));
                //cboDGTN.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_DGTN"));
                //cboDGTN.Columns["TEN_DGTN"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_DGTN");
                //cboDGTN.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                //cboDGTN.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                //cboDGTN.Columns["ID_DGTN"].Visible = false;
                //grvDSUngVien.Columns["ID_DGTN"].ColumnEdit = cboDGTN;
                //cboDGTN.BeforePopup += cboDGTN_BeforePopup;
                //cboDGTN.EditValueChanged += cboDGTN_EditValueChanged;
            }
            catch (Exception ex) { }
        }
        private void LoadCbo()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTiepNhanUngVien", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@sDanhMuc", SqlDbType.NVarChar).Value = "TN_UNG_VIEN";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[1].Copy();
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboLocTheoNgay, dt, "MA_DK", "TEN_DK", "TEN_DK");
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboMS_CV, dt1, "MS_CV", "TEN_CV", "TEN_CV");
            }
            catch { }
        }

        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "Ingiayhen":
                        {
                            if (grvDSUngVien.RowCount == 0) return;
                            switch (Convert.ToInt32(cboMS_CV.EditValue))
                            {
                                case 1:
                                    {
                                        DataTable dtbc = new DataTable();
                                        string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
                                        try
                                        {
                                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien), "");

                                            System.Data.SqlClient.SqlConnection conn1;
                                            frmViewReport frm = new frmViewReport();
                                            frm.rpt = new rptThuMoi();
                                            conn1 = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn1.Open();

                                            System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("rptThuMoi", conn1);
                                            cmd1.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd1.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd1.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBTCongNhan;
                                            cmd1.CommandType = CommandType.StoredProcedure;

                                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd1);
                                            DataSet ds = new DataSet();
                                            adp.Fill(ds);
                                            DataTable dt = new DataTable();
                                            dt = ds.Tables[0].Copy();
                                            dt.TableName = "DATA";
                                            frm.AddDataSource(dt);

                                            dtbc = new DataTable();
                                            dtbc = ds.Tables[1].Copy();
                                            dtbc.TableName = "NOI_DUNG";
                                            frm.AddDataSource(dtbc);

                                            Commons.Modules.ObjSystems.XoaTable(sBTCongNhan);

                                            frm.ShowDialog();
                                        }
                                        catch
                                        {
                                            Commons.Modules.ObjSystems.XoaTable(sBTCongNhan);
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        frmViewReport frm = new frmViewReport();
                                        string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
                                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, Commons.Modules.ObjSystems.ConvertDatatable(grvDSUngVien), "");
                                        System.Data.SqlClient.SqlConnection conn;
                                        DataTable dt = new DataTable();
                                        frm.rpt = new rptGiayHenDiLam();
                                        try
                                        {
                                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                            conn.Open();
                                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptGiayHenDiLam", conn);
                                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                                            cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBTCongNhan;
                                            cmd.Parameters.Add("@TNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datTNgay.EditValue);
                                            cmd.Parameters.Add("@DNgay", SqlDbType.DateTime).Value = Convert.ToDateTime(datDNgay.EditValue);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                            DataSet ds = new DataSet();
                                            adp.Fill(ds);
                                            dt = new DataTable();
                                            dt = ds.Tables[0].Copy();
                                            dt.TableName = "DATA";
                                            frm.AddDataSource(dt);
                                        }
                                        catch(Exception ex)
                                        {
                                        }
                                        frm.ShowDialog();
                                        break;
                                    }
                            }
                            
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


        private void datTNgay_EditValueChanged(object sender, EventArgs e)
        {

            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.ConvertDateTime(datTNgay.Text);
            int t = DateTime.DaysInMonth(datTNgay.DateTime.Year, datTNgay.DateTime.Month);
            DateTime secondDateTime = new DateTime(datTNgay.DateTime.Year, datTNgay.DateTime.Month, t);
            datDNgay.EditValue = secondDateTime;
            LoadData();
            Commons.Modules.sLoad = "";
        }
        private void datDNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }

        private void cboLocTheoNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
            grvDSUngVien_FocusedRowChanged(null, null);
        }
        private void cboMS_CV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
            grvDSUngVien_FocusedRowChanged(null, null);
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
        // TT Thiếp nhận ứng viên
        public DXMenuItem MCreateMenuThongTinTNUV(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblThongTinTiepNhanUV", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinTNUV = new DXMenuItem(sStr, new EventHandler(TTTiepNhanUV));
            menuThongTinTNUV.Tag = new RowInfo(view, rowHandle);
            return menuThongTinTNUV;
        }
        public void TTTiepNhanUV(object sender, EventArgs e)
        {
            try
            {
                //DateTime grvDSUngVien.GetFocusedRowCellValue("NGAY_CHUYEN").ToString() = DateTime.MinValue; ;
                //try
                //{
                //    dNgayChuyen = Commons.Modules.ObjSystems.ConvertDateTime(grvDSUngVien.GetFocusedRowCellValue("NGAY_CHUYEN").ToString());
                //}
                //catch { }
                iID_UV = Convert.ToInt64(grvDSUngVien.GetFocusedRowCellValue("ID_UV"));
                frmThongTinTiepNhanUV frm = new frmThongTinTiepNhanUV(Convert.ToInt64(grvDSUngVien.GetFocusedRowCellValue("ID_UV")), Convert.ToInt32(Convert.ToInt64(grvDSUngVien.GetFocusedRowCellValue("MS_CV"))), grvDSUngVien.GetFocusedRowCellValue("NGAY_CHUYEN").ToString(), Convert.ToInt64(grvDSUngVien.GetFocusedRowCellValue("ID_YCTD")), Convert.ToInt64(grvDSUngVien.GetFocusedRowCellValue("ID_VTTD")));
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            catch { }
        }
        // Cập nhật nhanh
        public DXMenuItem MCreateMenuCapNhatNhanh(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblCapNhatNhanh", Commons.Modules.TypeLanguage);
            DXMenuItem menuCapNhatNhanh = new DXMenuItem(sStr, new EventHandler(CapNhatNhanh));
            menuCapNhatNhanh.Tag = new RowInfo(view, rowHandle);
            return menuCapNhatNhanh;
        }
        public void CapNhatNhanh(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)grdDSUngVien.DataSource;
                frmCapNhatNhanh frm = new frmCapNhatNhanh(dt);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            catch { }
        }
        //Thong tin ung vien
        public DXMenuItem MCreateMenuThongTinUV(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblThongTinUV", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinUV = new DXMenuItem(sStr, new EventHandler(ThongTinUV));
            menuThongTinUV.Tag = new RowInfo(view, rowHandle);
            return menuThongTinUV;
        }
        public void ThongTinUV(object sender, EventArgs e)
        {
            try
            {

            }
            catch { }
        }
        //Thong tin nhân sự
        public DXMenuItem MCreateMenuThongTinNS(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblThongTinNS", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(ThongTinNS));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }
        public void ThongTinNS(object sender, EventArgs e)
        {
            try
            {

            }
            catch { }
        }
        private void grvDSUngVien_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();

                    DevExpress.Utils.Menu.DXMenuItem itemTiepNhan = MCreateMenuThongTinTNUV(view, irow);
                    e.Menu.Items.Add(itemTiepNhan);

                    DevExpress.Utils.Menu.DXMenuItem itemCapNhatNhanh = MCreateMenuCapNhatNhanh(view, irow);
                    e.Menu.Items.Add(itemCapNhatNhanh);

                    DevExpress.Utils.Menu.DXMenuItem itemTTUV = MCreateMenuThongTinUV(view, irow);
                    e.Menu.Items.Add(itemTTUV);
                    if (flag == false) return;
                    DevExpress.Utils.Menu.DXMenuItem itemTTNS = MCreateMenuThongTinNS(view, irow);
                    e.Menu.Items.Add(itemTTNS);
                }
            }
            catch
            {
            }
        }

        #endregion

        private void grvDSUngVien_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(grvDSUngVien.GetFocusedRowCellValue("NGAY_CHUYEN"));
                flag = true;
            }
            catch
            {
                flag = false;
            }
        }


    }
}
