using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraLayout;
using System.Threading;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Linq;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucInNhanVien : DevExpress.XtraEditors.XtraUserControl
    {
        public static ucInNhanVien _instance;
        DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboID_BV;
        int MS_TINH;
        public static ucInNhanVien Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucInNhanVien();
                return _instance;
            }
        }


        public ucInNhanVien()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, windowsUIButton);
        }
        #region bảo hiểm y tế
        private void ucThongTinNhanVien_Load(object sender, EventArgs e)
        {
            try
            {
                Thread.Sleep(1000);
                Commons.Modules.sLoad = "0Load";
                Commons.OSystems.SetDateEditFormat(datTuNgay);
                Commons.OSystems.SetDateEditFormat(datDNgay);
                datTuNgay.DateTime = DateTime.Now.AddMonths(-2);
                datDNgay.DateTime = DateTime.Now;
                Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
                Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
                LoadGridThongTinNhanVien();
                Commons.Modules.sLoad = "";
            }
            catch { }
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridThongTinNhanVien();
            Commons.Modules.sLoad = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridThongTinNhanVien();
            Commons.Modules.sLoad = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGridThongTinNhanVien();
            Commons.Modules.sLoad = "";
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "in":
                    {
                        grvTTNhanVien.CloseEditor();
                        grvTTNhanVien.UpdateCurrentRow();
                        try
                        {
                            DataTable dt_CHON = new DataTable();
                            dt_CHON = ((DataTable)grdTTNhanVien.DataSource);
                            //if (dt_CHON.AsEnumerable().Where(r => r.Field<Boolean>("CHON") == true).Count() == 0)

                            if (dt_CHON.AsEnumerable().Where(x => x.Field<Boolean>("CHON") == true).Count() == 0)
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgBanChuaChonDuLieu); return;
                            }
                            else
                            {
                                try
                                {
                                    //tạo một datatable 
                                    string strSaveThongTinNhanVien = "strSaveThongTinNhanVien" + Commons.Modules.UserName;
                                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, strSaveThongTinNhanVien, Commons.Modules.ObjSystems.ConvertDatatable(grvTTNhanVien), "");

                                    System.Data.SqlClient.SqlConnection conn;
                                    DataTable dt = new DataTable();
                                    DataTable dtbc = new DataTable();
                                    frmViewReport frm = new frmViewReport();
                                    //frm.rpt = new rptTheNhanVien_DM(dt);


                                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                                    conn.Open();
                                    Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(cboDV.EditValue));
                                    System.Data.SqlClient.SqlCommand cmd;
                                    switch (Commons.Modules.ObjSystems.KyHieuDV(Convert.ToInt64(cboDV.EditValue)))
                                    {
                                        case "DM":
                                            {
                                                try
                                                {
                                                    cmd = new System.Data.SqlClient.SqlCommand("spSaveThongTinNhanVienDM", conn);
                                                    cmd.Parameters.Add("@sBT", SqlDbType.NVarChar, 50).Value = strSaveThongTinNhanVien;
                                                    cmd.CommandType = CommandType.StoredProcedure;
                                                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                                    DataSet ds = new DataSet();
                                                    adp.Fill(ds);
                                                    DataTable dt1 = new DataTable();
                                                    dt1 = ds.Tables[1].Copy();
                                                    dt1.TableName = "DATA";
                                                    frm.rpt = new Vs.Recruit.rptInTheNV_DM(dt1);
             
                                                    frm.AddDataSource(dt1);

                                                    dt = new DataTable();
                                                    dt = ds.Tables[0].Copy();
                                                    dt.TableName = "DATA1";
                                                    frm.AddDataSource(dt);




                                                    DataTable dt2 = new DataTable();
                                                    dt2 = ds.Tables[2].Copy();
                                                    dt2.TableName = "DATA2";
                                                    frm.AddDataSource(dt2);

                                                    frm.ShowDialog();

                                                    Commons.Modules.ObjSystems.XoaTable(strSaveThongTinNhanVien);
                                                    conn.Close();
                                                }
                                                catch { }
                                            }
                                            break;

                                        case "NB":
                                            {
                                                try
                                                {
                                                    cmd = new System.Data.SqlClient.SqlCommand("spSaveThongTinNhanVienNB", conn);
                                                    cmd.Parameters.Add("@sBT", SqlDbType.NVarChar, 50).Value = strSaveThongTinNhanVien;
                                                    cmd.CommandType = CommandType.StoredProcedure;
                                                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                                                    DataSet ds = new DataSet();
                                                    adp.Fill(ds);
                                                    DataTable dt1 = new DataTable();
                                                    dt1 = ds.Tables[1].Copy();
                                                    dt1.TableName = "DATA";
                                                    //frm.rpt = new Vs.Recruit.rptTheNhanVien(dt1);
                                                    frm.rpt = new rptTheNhanVien(DateTime.Now);
                                                    frm.AddDataSource(dt1);

                                                    dt = new DataTable();
                                                    dt = ds.Tables[0].Copy();
                                                    dt.TableName = "DON_VI";
                                                    frm.AddDataSource(dt);

                                                    frm.ShowDialog();

                                                    Commons.Modules.ObjSystems.XoaTable(strSaveThongTinNhanVien);
                                                    conn.Close();
                                                }
                                                catch {}
                                            }
                                            break;
                                    }
                                    
                                   
                                  

                                    
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                        catch
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgBanChuaChonDuLieu); return;
                        }
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        #endregion

        #region hàm xử lý dữ liệu
        private void LoadGridThongTinNhanVien()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spLayDanhSachThongTinNhanVien", cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, datTuNgay.EditValue, datDNgay.EditValue));
                dt.Columns["CHON"].ReadOnly = false;
                if (grdTTNhanVien.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdTTNhanVien, grvTTNhanVien, dt, true, false, false, false, true, this.Name);
                    grvTTNhanVien.Columns["CHON"].Visible = false;
                    grvTTNhanVien.Columns["ID_CN"].Visible = false;
                    grvTTNhanVien.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                    grvTTNhanVien.Columns["TEN_XN"].OptionsColumn.AllowEdit = false;
                    grvTTNhanVien.Columns["TEN_TO"].OptionsColumn.AllowEdit = false;
                    grvTTNhanVien.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                }
                else
                {
                    grdTTNhanVien.DataSource = dt;
                }
                try
                {
                    grvTTNhanVien.OptionsSelection.CheckBoxSelectorField = "CHON";
                    grvTTNhanVien.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
                catch { }
            }
            catch { }
        }
        #endregion

        private void grvTTNhanVien_RowCountChanged(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                int index = ItemForSumNhanVien.Text.IndexOf(':');
                if (index > 0)
                {
                    if (view.RowCount > 0)
                    {
                        ItemForSumNhanVien.Text = ItemForSumNhanVien.Text.Substring(0, index) + ": " + view.RowCount.ToString();
                    }
                    else
                    {
                        ItemForSumNhanVien.Text = ItemForSumNhanVien.Text.Substring(0, index) + ": 0";
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void GetsThongTinNhanVienCheked()
        {

        }
        private void windowsUIButton_Click(object sender, EventArgs e)
        {

        }

        private void datTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                LoadGridThongTinNhanVien();
            }
            catch { }
        }

        private void datDNgay_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                LoadGridThongTinNhanVien();
            }
            catch { }
        }
    }
}
