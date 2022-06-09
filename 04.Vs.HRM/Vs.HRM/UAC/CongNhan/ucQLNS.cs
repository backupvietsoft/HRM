using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraLayout.Utils;
namespace Vs.HRM
{
    public partial class ucQLNS : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable dt;
        public AccordionControl accorMenuleft;
        public ucQLNS()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root);
        }

        private void ucQLNS_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT TOP 1 MS_CN FROM dbo.CONG_NHAN ORDER BY MS_CN"));
            if (dt.Rows.Count == 0)
            {
                tileView1_DoubleClick(null, null);
                return;
            }
            Commons.Modules.sLoad = "0Load";
            LoadCboDonVi();
            LoadCboXiNghiep();
            LoadCboTo();
            LoadTinhTrangHienTai();
            LoadNhanSu(-1);
            Commons.Modules.sLoad = "";
        }

        private void LoadCboDonVi()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDV, dt, "ID_DV", "TEN_DV", "TEN_DV");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void LoadCboXiNghiep()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboXI_NGHIEP", cboDV.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboXN, dt, "ID_XN", "TEN_XN", "TEN_XN");
                cboXN.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void LoadCboTo()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTO", cboDV.EditValue, cboXN.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, 1));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboTo, dt, "ID_TO", "TEN_TO", "TEN_TO");
                cboTo.EditValue = -1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }

        }

        private void LoadTinhTrangHienTai()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboTinhTrangHT", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cbo_TTHT, dt, "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadCboXiNghiep();
            LoadCboTo();
            LoadNhanSu(-1);
            Commons.Modules.sLoad = "";
        }

        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadCboTo();
            LoadNhanSu(-1);
            Commons.Modules.sLoad = "";
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadNhanSu(-1);
            Commons.Modules.sLoad = "";
        }
        private void LoadNhanSu(Int64 iIdNs)
        {
            try
            {

                DataTable dtTmp = new DataTable();
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListNS", cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, cbo_TTHT.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dtTmp.PrimaryKey = new DataColumn[] { dtTmp.Columns["ID_CN"] };
                grdNS.DataSource = dtTmp;


                if (iIdNs != -1)
                {
                    int index = dtTmp.Rows.IndexOf(dtTmp.Rows.Find(iIdNs));
                    tileViewCN.FocusedRowHandle = tileViewCN.GetRowHandle(index);
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
                e.Item.Elements[0].Appearance.Normal.BackColor = System.Drawing.ColorTranslator.FromHtml(tileViewCN.GetRowCellValue(e.RowHandle, tileViewCN.Columns["MAU_TT"]).ToString());
            }
            catch { }
        }


        private void tileView1_DoubleClick(object sender, EventArgs e)
        {

            grdNS.Visible = false;
            ucCTQLNS dl = new ucCTQLNS(Convert.ToInt64(tileViewCN.GetFocusedRowCellValue(tileViewCN.Columns["ID_CN"])));
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

        public void BackWindowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            navigationFrame1.SelectedPage = navigationPage1;
            navigationPage2.Controls[0].Visible = false;
            navigationPage2.Controls[0].Dispose();
        //    navigationPage2.SelectedPage.Visible = false;
       //     navigationPage2.Controls.RemoveAt(0);

            accorMenuleft.Visible = true;
            LoadNhanSu(Commons.Modules.iCongNhan);
        }
        private void emptySpaceItem1_DoubleClick(object sender, EventArgs e)
        {
            ItemForDON_VI.Visibility = ItemForDON_VI.Visibility == LayoutVisibility.Never ? LayoutVisibility.Always : LayoutVisibility.Never;
            ItemForTO.Visibility = ItemForTO.Visibility == LayoutVisibility.Never ? LayoutVisibility.Always : LayoutVisibility.Never;
            ItemForTT_HT.Visibility = ItemForTT_HT.Visibility == LayoutVisibility.Never ? LayoutVisibility.Always : LayoutVisibility.Never;
            ItemForXI_NGHIEP.Visibility = ItemForXI_NGHIEP.Visibility == LayoutVisibility.Never ? LayoutVisibility.Always : LayoutVisibility.Never;
            ItemForSerchControl.Visibility = ItemForSerchControl.Visibility == LayoutVisibility.Never ? LayoutVisibility.Always : LayoutVisibility.Never;
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
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDeleteCongNhan"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeXoa"), MessageBoxButtons.YesNo) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.CONG_NHAN WHERE ID_CN  =" + Convert.ToInt64(tileViewCN.GetFocusedRowCellValue(tileViewCN.Columns["ID_CN"]) + ""));
                tileViewCN.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDelDangSuDung") + "\n" + ex.Message.ToString());
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDelDangSuDung"));

            }
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            if (btn == null || btn.Tag == null) return;
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        grdNS.Visible = false;
                        ucCTQLNS dl = new ucCTQLNS(-1);
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
                        if (tileViewCN.RowCount == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        Int64 iIDCN = Convert.ToInt64(tileViewCN.GetFocusedRowCellValue("ID_CN"));
                        if (iIDCN == 0)
                        {
                            iIDCN = -1;
                        }

                        grdNS.Visible = false;
                        ucCTQLNS dl = new ucCTQLNS(iIDCN);
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

                case "xoa":
                    {
                        if (tileViewCN.RowCount == 0)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
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
    }
}
