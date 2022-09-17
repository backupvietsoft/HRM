using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vs.Category
{
    public partial class frmXetDuyet : DevExpress.XtraEditors.XtraUserControl
    {
        static int iPQ = -1; //1:full, 2:readonly
        private Vs.Recruit.ucYeuCauTuyenDung ucYCTD;
        public frmXetDuyet(int PQ)
        {
            iPQ = PQ;
            InitializeComponent();
        }

        #region Create chuot phai


        private void grvChung_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            return;
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int rowHandle = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();

                    string sStr = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "btnLinkDocument");
                    DevExpress.Utils.Menu.DXMenuItem itemLinkDocument = new DevExpress.Utils.Menu.DXMenuItem(sStr, new EventHandler(LinkDocument));
                    itemLinkDocument.Tag = string.IsNullOrEmpty(Convert.ToString(view.GetFocusedRowCellValue("ID_DQT"))) ? 0 : Convert.ToInt64(view.GetFocusedRowCellValue("ID_DQT"));
                    e.Menu.Items.Add(itemLinkDocument);

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void grvDaDuyet_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int rowHandle = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();

                    string sStr = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "btnLinkDocument");
                    DevExpress.Utils.Menu.DXMenuItem itemLinkDocument = new DevExpress.Utils.Menu.DXMenuItem(sStr, new EventHandler(LinkDocument));
                    itemLinkDocument.Tag = string.IsNullOrEmpty(Convert.ToString(view.GetFocusedRowCellValue("ID_DQT"))) ? 0 : Convert.ToInt64(view.GetFocusedRowCellValue("ID_DQT"));
                    e.Menu.Items.Add(itemLinkDocument);

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void LinkDocument(object sender, EventArgs e)
        {
            #region a Minh
            //Cursor.Current = Cursors.WaitCursor;
            ////Cập nhập dữ liệu dòng hiện tại cho các dòng khác nếu != null

            //DevExpress.Utils.Menu.DXMenuItem menuItem = sender as DevExpress.Utils.Menu.DXMenuItem;

            //if (menuItem.Tag != null)
            //{
            //    try
            //    {
            //        System.Data.SqlClient.SqlConnection conn;
            //        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            //        conn.Open();
            //        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
            //        cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 21;
            //        cmd.Parameters.Add("@ID_DQT", SqlDbType.BigInt).Value = Convert.ToInt64(menuItem.Tag);
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
            //        DataSet ds = new DataSet();
            //        adp.Fill(ds);
            //        DataTable dt = new DataTable();
            //        dt = ds.Tables[0].Copy();


            //        if (dt == null || dt.Rows.Count == 0) return;

            //        string sFORM_NAME = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["FORM_NAME"])) ? "" : Convert.ToString(dt.Rows[0]["FORM_NAME"]);
            //        string sKEY_MENU = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["KEY_MENU"])) ? "" : Convert.ToString(dt.Rows[0]["KEY_MENU"]);
            //        Int64 iID_DOC = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ID_DOC"])) ? 0 : Convert.ToInt64(dt.Rows[0]["ID_DOC"]);

            //        if (IsFormActive(sFORM_NAME)) return;
            //        int iPQ = Commons.Modules.ObjSystems.CheckPermission(sKEY_MENU);

            //        switch (sFORM_NAME)
            //        {
            //            case "frmDonHangBan":
            //                frmDonHangBan frmDonHangBan = new frmDonHangBan(iPQ,0);
            //                frmDonHangBan.iID_DHB = iID_DOC;
            //                frmMain.ShowformFull(frmDonHangBan, true);
            //                break;
            //            case "frmDonHangBanNguyenTac":
            //                frmDonHangBanNguyenTac frmDonHangBanNguyenTac = new frmDonHangBanNguyenTac(iPQ);
            //                frmDonHangBanNguyenTac.iID_DHBNT = iID_DOC;
            //                frmMain.ShowformFull(frmDonHangBanNguyenTac, true);
            //                break;
            //            case "frmDonHangMua":
            //                frmDonHangMua frmDonHangMua = new frmDonHangMua(iPQ);
            //                frmDonHangMua.iID_DHM = iID_DOC;
            //                frmMain.ShowformFull(frmDonHangMua, true);
            //                break;
            //            case "frmDonHangMuaNguyenTac":
            //                frmDonHangMuaNguyenTac frmDonHangMuaNguyenTac = new frmDonHangMuaNguyenTac(iPQ);
            //                frmDonHangMuaNguyenTac.iID_DHMNT = iID_DOC;
            //                frmMain.ShowformFull(frmDonHangMuaNguyenTac, true);
            //                break;
            //            case "frmDonHangGiaCong":
            //                frmDonHangGiaCong frmDonHangGiaCong = new frmDonHangGiaCong(iPQ);
            //                frmDonHangGiaCong.iID_DHGC = iID_DOC;
            //                frmMain.ShowformFull(frmDonHangGiaCong, true);
            //                break;
            //            case "frmDonHangGiaCongNguyenTac":
            //                frmDonHangGiaCongNguyenTac frmDonHangGiaCongNguyenTac = new frmDonHangGiaCongNguyenTac(iPQ);
            //                frmDonHangGiaCongNguyenTac.iID_DHGCNT = iID_DOC;
            //                frmMain.ShowformFull(frmDonHangGiaCongNguyenTac, true);
            //                break;
            //            case "frmBOM":
            //                frmBOM frmBOM = new frmBOM(iPQ);
            //                frmBOM.iID_BOM = iID_DOC;
            //                frmMain.ShowformFull(frmBOM, true);
            //                break;
            //            case "frmBaoGiaBan":
            //                frmBaoGiaBan frmBaoGiaBan = new frmBaoGiaBan(iPQ);
            //                frmBaoGiaBan.iID_BGB = iID_DOC;
            //                frmMain.ShowformFull(frmBaoGiaBan, true);
            //                break;
            //            case "frmPhieuXuatKho":
            //                frmPhieuXuatKho frmPhieuXuatKho = new frmPhieuXuatKho(iPQ);
            //                frmPhieuXuatKho.iID_PXK = iID_DOC;
            //                frmMain.ShowformFull(frmPhieuXuatKho, true);
            //                break;
            //            case "frmLenhCapPhat":
            //                frmLenhCapPhat frmLenhCapPhat = new frmLenhCapPhat(iPQ);
            //                frmLenhCapPhat.iID_LCP = iID_DOC;
            //                frmMain.ShowformFull(frmLenhCapPhat, true);
            //                break;
            //            default:
            //                break;
            //        }

            //    }
            //    catch (Exception ex) { XtraMessageBox.Show(ex.Message); }
            //}
            //Cursor.Current = Cursors.Default;
            #endregion
            //ucYCTD = new Vs.Recruit.ucYeuCauTuyenDung();
            //Commons.Modules.ObjSystems.ShowWaitForm(this);
            //ucYCTD.Refresh();
            //dataLayoutControl1.Hide();
            //this.Controls.Add(ucYCTD);
            //ucYCTD.Dock = DockStyle.Fill;
            //ucYCTD.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
            //Commons.Modules.ObjSystems.HideWaitForm();
        }

        #endregion

        #region Event
        private void frmXetDuyet_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                LoadNN();
                Commons.Modules.ObjSystems.ThayDoiNN(this, new List<DevExpress.XtraLayout.LayoutControlGroup> { Root }, btnALL);
                lcgDaDuyet.DoubleClick += delegate (object a, EventArgs b) { ControlGroup_DoubleClick(lcgDaDuyet, b, this.Name); };
            }
            catch { }
        }
        private void ControlGroup_DoubleClick(object sender, EventArgs e, string sName)
        {
            if (Form.ModifierKeys == Keys.Control)
            {
                DevExpress.XtraLayout.LayoutControlGroup Ctl;
                string sText = "";
                Ctl = (DevExpress.XtraLayout.LayoutControlGroup)sender;
                try
                {
                    sText = XtraInputBox.Show(Ctl.Text, "Sửa ngôn ngữ", "");
                    if (sText == "" || sText == null)
                        return;
                    else
                        CapNhapNN(sName, Ctl.Name.ToUpper().Replace("ItemFor".ToUpper(), ""), sText, false);

                    sText = " SELECT TOP 1 " + (Commons.Modules.TypeLanguage == 0 ? "VIETNAM" : "ENGLISH") + " FROM LANGUAGES WHERE FORM = '" + sName + "' AND KEYWORD = '" + Ctl.Name.ToUpper().Replace("ItemFor".ToUpper(), "") + "' AND MS_MODULE = 'VS_HRM'";
                    sText = Convert.ToString(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sText));

                    Ctl.Text = sText;
                }
                catch
                {
                    sText = "";
                }
            }
        }
       

        private void grvChung_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Int64 ID_DQT = string.IsNullOrEmpty(grvChung.GetRowCellValue(grvChung.FocusedRowHandle, "ID_DQT").ToString()) ? 0 : Convert.ToInt64(grvChung.GetRowCellValue(grvChung.FocusedRowHandle, "ID_DQT"));

                frmXetDuyet_Confirm frm = new frmXetDuyet_Confirm(iPQ, ID_DQT);
                Commons.Modules.ObjSystems.LocationSizeForm(this, frm);
                frm.ShowDialog();
                LoadData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        private void btnALL_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            switch (btn.Tag.ToString())
            {
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
            }
        }

        private void grvChung_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if ((string.IsNullOrEmpty(view.GetRowCellValue(e.RowHandle, view.Columns["KHAN_CAP"]).ToString()) ? 0 : Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["KHAN_CAP"]))) == 1)
                    e.Appearance.BackColor = Color.FromArgb(255, 204, 255);
            }
            catch { }
        }

        private void grvDaDuyet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if ((string.IsNullOrEmpty(view.GetRowCellValue(e.RowHandle, view.Columns["KHAN_CAP"]).ToString()) ? 0 : Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["KHAN_CAP"]))) == 1 && (string.IsNullOrEmpty(view.GetRowCellValue(e.RowHandle, view.Columns["KET_THUC"]).ToString()) ? 0 : Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["KET_THUC"]))) == 0)
                    e.Appearance.BackColor = Color.FromArgb(255, 204, 255);
                if ((string.IsNullOrEmpty(view.GetRowCellValue(e.RowHandle, view.Columns["CHAP_NHAN"]).ToString()) ? 0 : Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["CHAP_NHAN"]))) == 0 && (string.IsNullOrEmpty(view.GetRowCellValue(e.RowHandle, view.Columns["BUOC_DUYET"]).ToString()) ? 0 : Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["BUOC_DUYET"]))) != 0 && (string.IsNullOrEmpty(view.GetRowCellValue(e.RowHandle, view.Columns["KET_THUC"]).ToString()) ? 0 : Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["KET_THUC"]))) == 0)
                    e.Appearance.BackColor = Color.FromArgb(141, 180, 226);
            }
            catch { }
        }

        private void tabChung_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            try
            {
                if (tabChung.SelectedTabPageIndex == 0)
                    txtTim.Client = grdChung;
                else
                    txtTim.Client = grdDaDuyet;
            }
            catch { }
        }

        #endregion

        #region Funtion
        private void LoadNN()
        {
            try
            {
                lcgXetDuyet.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lcgXetDuyet");
                lcgDaDuyet.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "lcgDaDuyet");
            }
            catch { }
        }

        private void LoadData()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spDuyetQuyDinh", conn);
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 20;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@ID_USER", SqlDbType.BigInt).Value = Commons.Modules.iIDUser;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();

                if (grdChung.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, false, true, false, false, true, this.Name);
                    grvChung.Columns["ID_DQT"].Visible = false;

                }
                else
                    grdChung.DataSource = dt;

                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[1].Copy();



                if (grdDaDuyet.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDaDuyet, grvDaDuyet, dt1, false, true, false, false, true, this.Name);
                    grvDaDuyet.Columns["ID_DQT"].Visible = false;
                }
                else
                    grdDaDuyet.DataSource = dt1;

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        private void CapNhapNN(string sForm, string sKeyWord, string sChuoi, bool bReset)
        {
            string sSql;
            if (bReset)
                sSql = "UPDATE LANGUAGES SET " + (Commons.Modules.TypeLanguage == 0 ? "VIETNAM" : "ENGLISH") + " = " + (Commons.Modules.TypeLanguage == 0 ? "VIETNAM_OR" : "ENGLISH_OR") + " WHERE FORM = '" + sForm + "' AND KEYWORD = '" + sKeyWord + "' AND MS_MODULE = 'VS_HRM'";
            else
                sSql = "UPDATE LANGUAGES SET " + (Commons.Modules.TypeLanguage == 0 ? "VIETNAM" : "ENGLISH") + " = N'" + sChuoi + "' WHERE FORM = '" + sForm + "' AND KEYWORD = '" + sKeyWord + "' AND MS_MODULE = 'VS_HRM'";
            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
        }
        public static bool IsFormActive(string sFrm)
        {
            //frmMain _instance = frmMain._instance;

            //if (_instance.MdiChildren.Count() > 0)
            //{
            //    foreach (var item in _instance.MdiChildren)
            //    {
            //        if (sFrm == item.Name)
            //        {
            //            item.Activate();
            //            return true;
            //        }
            //    }
            //}

            //#region Kiem form active
            //FormCollection frmOpen = Application.OpenForms;
            //List<Form> ListForm = new List<Form>();
            //foreach (Form frmO in frmOpen)
            //{
            //    if (frmO.Name == sFrm)
            //    {
            //        frmO.Activate();
            //        return true;
            //    }
            //}
            //return false;
            return true;
        }
        #endregion

    }
}
