using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraLayout.Utils;
using Vs.Recruit;

namespace Vs.Recruit
{
    public partial class ucQLUV : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable dt;
        public AccordionControl accorMenuleft;
        private int Temp = 0; // 0 Load những nhân viên đã nộp hồ vào thông báo tuyển dụng, 1 Load những nhân viên chưa nộp hồ sơ vào thông báo tuyển dụng.
        private int TTTuyenDung = 0;
        public ucQLUV()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root);
        }

        private void ucQLUV_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT TOP 1 TEN FROM dbo.UNG_VIEN ORDER BY TEN"));
            Commons.Modules.sLoad = "0Load";
            LoadcboKHTD();
            LoadcboVTTD();
            LoadCboTuyenDung();
            LoadUNG_VIEN(-1);
            Commons.Modules.sLoad = "";
        }
        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        //grdTD.Visible = false;
                        ucCTQLUV dl = new ucCTQLUV(-1);
                        Commons.Modules.ObjSystems.ShowWaitForm(this);
                        dl.Refresh();
                        dt = dl.dt;
                        navigationFrame1.SelectedPage.Visible = false;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            try
                            {
                                string str = dt.Rows[0]["HO"] + " " + dt.Rows[0]["TEN"];
                            }
                            catch
                            {

                            }
                        }
                        navigationPage2.Controls.Add(dl);
                        dl.Dock = DockStyle.Fill;
                        dl.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
                        navigationFrame1.SelectedPage = navigationPage2;
                        accorMenuleft.Visible = false;
                        Commons.Modules.ObjSystems.HideWaitForm();
                        break;
                    }
                case "sua":
                    {
                        //if (tileViewTD.RowCount == 0)
                        //{
                        //    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    return;
                        //}
                        //Int64 iIDUV = Convert.ToInt64(tileViewTD.GetFocusedRowCellValue("ID_UV"));
                        //if (iIDUV == 0)
                        //{
                        //    iIDUV = -1;
                        //}

                        //grdTD.Visible = false;
                        //ucCTQLUV dl = new ucCTQLUV(iIDUV);
                        //Commons.Modules.ObjSystems.ShowWaitForm(this);
                        //dl.Refresh();
                        //dt = dl.dt;
                        //navigationFrame1.SelectedPage.Visible = false;
                        //if (dt != null && dt.Rows.Count > 0)
                        //{
                        //    try
                        //    {
                        //        string str = dt.Rows[0]["HO"] + " " + dt.Rows[0]["TEN"];
                        //    }
                        //    catch
                        //    {

                        //    }
                        //}
                        //navigationPage2.Controls.Add(dl);
                        //dl.Dock = DockStyle.Fill;
                        //dl.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
                        //navigationFrame1.SelectedPage = navigationPage2;
                        //accorMenuleft.Visible = false;
                        //Commons.Modules.ObjSystems.HideWaitForm();
                        break;
                    }

                case "xoa":
                    {
                        //if (tileViewTD.RowCount == 0)
                        //{
                        //    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    return;
                        //}
                        DeleteData();
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
        private void LoadcboKHTD()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboKHTD", Commons.Modules.UserName ,Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_KHTD, dt, "ID_TB", "SO_TB", "SO_TB");
                cboID_KHTD.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void LoadcboVTTD()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboViTriTuyenDung", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_VTTD, dt, "ID_VTTD", "TEN_VTTD", "TEN_VTTD");
                cboID_VTTD.EditValue = -1;
            }
            catch { }
        }
        private void LoadCboTuyenDung()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDaTuyenDung",  Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDA_TUYEN_DUNG, dt, "ID_TTTD", "TT_TUYEN_DUNG", "TT_TUYEN_DUNG");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadUNG_VIEN(-1);
            Commons.Modules.sLoad = "";
        }
        private void LoadUNG_VIEN(Int64 iIdUV)
        {
            //try
            //{

            //    DataTable dtTmp = new DataTable();
            //    dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListUV", Convert.ToInt64(cboID_TB.EditValue) ,Convert.ToInt32(cboDA_TUYEN_DUNG.EditValue), Commons.Modules.UserName, Commons.Modules.TypeLanguage, Temp));
                
            //    dtTmp.PrimaryKey = new DataColumn[] { dtTmp.Columns["ID_UV"] };
            //    grdTD.DataSource = dtTmp;


            //    if (iIdUV != -1)
            //    {
            //        int index = dtTmp.Rows.IndexOf(dtTmp.Rows.Find(iIdUV));
            //        tileViewTD.FocusedRowHandle = tileViewTD.GetRowHandle(index);
            //    }
            //}
            //catch  { }

            try
            {
                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListUngVien",  Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dtTmp.PrimaryKey = new DataColumn[] { dtTmp.Columns["ID_UV"] };
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdUngVien, grvUngVien, dtTmp, false, true, false, true, true, this.Name);
                grvUngVien.Columns["ID_UV"].Visible = false;
                grvUngVien.Columns["VI_TRI_TD_1"].Visible = false;
                grvUngVien.Columns["VI_TRI_TD_2"].Visible = false;

                if (iIdUV != -1)
                {
                    int index = dtTmp.Rows.IndexOf(dtTmp.Rows.Find(iIdUV));
                    grvUngVien.FocusedRowHandle = grvUngVien.GetRowHandle(index);
                }
            }
            catch { }
        }
        private void tileView1_ItemCustomize(object sender, TileViewItemCustomizeEventArgs e)
        {
            try
            {
                if (e.Item == null || e.Item.Elements.Count == 0)
                    return;
                if(Convert.ToInt32(cboDA_TUYEN_DUNG.EditValue) == 1)
                {
                    e.Item.Elements[0].Appearance.Normal.BackColor = System.Drawing.ColorTranslator.FromHtml("#A9F5BC");
                }
                if (Convert.ToInt32(cboDA_TUYEN_DUNG.EditValue) == 2)
                {
                    e.Item.Elements[0].Appearance.Normal.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
                }
            }
            catch { }
        }


        private void tileView1_DoubleClick(object sender, EventArgs e)
        {
            //    Int64 iIDUV = Convert.ToInt64(grvUngVien.GetFocusedRowCellValue("ID_UV"));
            //    if(iIDUV == 0)
            //    {
            //        iIDUV = -1;
            //    }

            //    //grdTD.Visible = false;
            //    ucCTQLUV dl = new ucCTQLUV(iIDUV);
            //    Commons.Modules.ObjSystems.ShowWaitForm(this);
            //    dl.Refresh();
            //    dt = dl.dt;
            //    navigationFrame1.SelectedPage.Visible = false;
            //    if (dt != null && dt.Rows.Count > 0)
            //    {
            //        try
            //        {
            //            string str = dt.Rows[0]["HO"] + " " + dt.Rows[0]["TEN"];
            //        }
            //        catch
            //        {

            //        }
            //    }
            //    navigationPage2.Controls.Add(dl);
            //    dl.Dock = DockStyle.Fill;
            //    dl.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
            //    navigationFrame1.SelectedPage = navigationPage2;
            //    accorMenuleft.Visible = false;
            //    Commons.Modules.ObjSystems.HideWaitForm();
        } 

        public void BackWindowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            navigationFrame1.SelectedPage = navigationPage1;
            navigationPage2.Controls[0].Visible = false;
            navigationPage2.Controls[0].Dispose();
        //    navigationPage2.SelectedPage.Visible = false;
       //     navigationPage2.Controls.RemoveAt(0);

            accorMenuleft.Visible = true;
            LoadUNG_VIEN(Commons.Modules.iUngVien);
        }
        private void emptySpaceItem1_DoubleClick(object sender, EventArgs e)
        {
            //ItemForDON_VI.Visibility = ItemForDON_VI.Visibility == LayoutVisibility.Never ? LayoutVisibility.Always : LayoutVisibility.Never;
            //ItemForTO.Visibility = ItemForTO.Visibility == LayoutVisibility.Never ? LayoutVisibility.Always : LayoutVisibility.Never;
            //ItemForTT_HT.Visibility = ItemForTT_HT.Visibility == LayoutVisibility.Never ? LayoutVisibility.Always : LayoutVisibility.Never;
            //ItemForXI_NGHIEP.Visibility = ItemForXI_NGHIEP.Visibility == LayoutVisibility.Never ? LayoutVisibility.Always : LayoutVisibility.Never;
            //ItemForSerchControl.Visibility = ItemForSerchControl.Visibility == LayoutVisibility.Never ? LayoutVisibility.Always : LayoutVisibility.Never;
        }

        private void grdNS_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                DeleteData();
            }
        }
        private void DeleteData()
        {
            //if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDeleteCongNhan"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeXoa"), MessageBoxButtons.YesNo) == DialogResult.No) return;
            ////xóa
            //try
            //{
            //    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.UNG_VIEN WHERE ID_UV  =" + Convert.ToInt64(tileViewTD.GetFocusedRowCellValue("ID_UV")) + "");
            //    tileViewTD.DeleteSelectedRows();
            //}
            //catch (Exception ex)
            //{
            //    //XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDelDangSuDung") + "\n" + ex.Message.ToString());
            //    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDelDangSuDung")); 

            //}
        }

        private void cboID_TB_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            //if (Convert.ToInt32(cboDA_TUYEN_DUNG.EditValue) == 2)
            //{
            //    Temp = 1;
            //    TTTuyenDung = 2;
            //}
            //else
            //{
            //    Temp = 0;
            //    TTTuyenDung = Convert.ToInt32(cboDA_TUYEN_DUNG.EditValue);
            //}
            LoadUNG_VIEN(-1);
            Commons.Modules.sLoad = "";
        }

        private void cboDA_TUYEN_DUNG_EditValueChanged(object sender, EventArgs e)
        {
            //if (Commons.Modules.sLoad == "0Load") return;
            //Commons.Modules.sLoad = "0Load";
            //if(Convert.ToInt32(cboDA_TUYEN_DUNG.EditValue) == 2)
            //{
            //    cboID_TB.EditValue = -1;
            //    cboID_TB.Enabled = false;
            //}
            //else
            //{
            //    cboID_TB.Enabled = true;
            //}
            //LoadUNG_VIEN(-1);
            //Commons.Modules.sLoad = "";
        }

        private void grvUngVien_DoubleClick(object sender, EventArgs e)
        {
            Int64 iIDUV = Convert.ToInt64(grvUngVien.GetFocusedRowCellValue("ID_UV"));
            if (iIDUV == 0)
            {
                iIDUV = -1;
            }
            //grdTD.Visible = false;
            ucCTQLUV dl = new ucCTQLUV(iIDUV);
            Commons.Modules.ObjSystems.ShowWaitForm(this);
            dl.Refresh();
            dt = dl.dt;
            navigationFrame1.SelectedPage.Visible = false;
            if (dt != null && dt.Rows.Count > 0)
            {
                try
                {
                    string str = dt.Rows[0]["HO"] + " " + dt.Rows[0]["TEN"];
                }
                catch
                {

                }
            }
            navigationPage2.Controls.Add(dl);
            dl.Dock = DockStyle.Fill;
            dl.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
            navigationFrame1.SelectedPage = navigationPage2;
            accorMenuleft.Visible = false;
            Commons.Modules.ObjSystems.HideWaitForm();
        }
    }
}
