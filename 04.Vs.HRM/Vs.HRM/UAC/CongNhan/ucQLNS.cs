using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraLayout.Utils;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using System.Collections.Generic;
using System.Drawing;

namespace Vs.HRM
{
    public partial class ucQLNS : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable dt;
        public AccordionControl accorMenuleft;
        public LabelControl labelNV;
        public ucQLNS()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
        }

        private void ucQLNS_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT TOP 1 MS_CN FROM dbo.CONG_NHAN ORDER BY MS_CN"));
            if (dt.Rows.Count == 0)
            {
                grvDSCongNhan_DoubleClick(null, null);
                return;
            }
            Commons.Modules.sLoad = "0Load";
            LoadCboDonVi();
            LoadCboXiNghiep();
            LoadCboTo();
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cboID_LTTHT, Commons.Modules.ObjSystems.DataLoaiTinHTrangHT(false), "ID_LTTHT", "TEN_LOAI_TTHT", "TEN_LOAI_TTHT");
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cbo_TTHT, Commons.Modules.ObjSystems.DataTinHTrangHT(Convert.ToInt32(cboID_LTTHT.EditValue), true), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
            LoadNhanSu(-1);
            Commons.Modules.sLoad = "";
            setMauTT();
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
        private void cbo_TTHT_EditValueChanged(object sender, EventArgs e)
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
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListNS_DanhSach", cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, cbo_TTHT.EditValue, cboID_LTTHT.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dtTmp.PrimaryKey = new DataColumn[] { dtTmp.Columns["ID_CN"] };
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSCongNhan, grvDSCongNhan, dtTmp, false, false, false, true, true, this.Name);
                //grdDSCongNhan.DataSource = dtTmp;
                grvDSCongNhan.Columns["ID_CN"].Visible = false;
                grvDSCongNhan.Columns["MAU_TT"].Visible = false;
                grvDSCongNhan.ExpandAllGroups();
                if (iIdNs != -1)
                {
                    int index = dtTmp.Rows.IndexOf(dtTmp.Rows.Find(iIdNs));
                    grvDSCongNhan.FocusedRowHandle = grvDSCongNhan.GetRowHandle(index);
                }
            }
            catch (Exception ex) { }
        }
        private void tileView1_ItemCustomize(object sender, TileViewItemCustomizeEventArgs e)
        {
            //try
            //{
            //    if (e.Item == null || e.Item.Elements.Count == 0)
            //        return;
            //    e.Item.Elements[0].Appearance.Normal.BackColor = System.Drawing.ColorTranslator.FromHtml(tileViewCN.GetRowCellValue(e.RowHandle, tileViewCN.Columns["MAU_TT"]).ToString());
            //}
            //catch { }
        }
        private void grvDSCongNhan_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                labelNV.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelNV.ForeColor = System.Drawing.Color.FromArgb(0, 0, 255);
                labelNV.Text = grvDSCongNhan.GetFocusedRowCellValue(grvDSCongNhan.Columns["MS_CN"]).ToString() + " - " + grvDSCongNhan.GetFocusedRowCellValue(grvDSCongNhan.Columns["HO_TEN"]).ToString();
            }
            catch (Exception ex) { }
            grdDSCongNhan.Visible = false;
            ucCTQLNS dl = new ucCTQLNS(Convert.ToInt64(grvDSCongNhan.GetFocusedRowCellValue(grvDSCongNhan.Columns["ID_CN"])));
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
            Thread thread = new Thread(delegate ()
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        navigationFrame1.SelectedPage = navigationPage2;
                    }));
                }
            }, 100);
            thread.Start();
            accorMenuleft.Visible = false;
        }
        private void Selecttab()
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    navigationFrame1.SelectedPage = navigationPage2;
                }));
            }
        }
        public void BackWindowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            try { labelNV.Text = labelNV.Tag.ToString(); } catch { }
            navigationFrame1.SelectedPage = navigationPage1;
            navigationPage2.Controls[0].Visible = false;
            navigationPage2.Controls[0].Dispose();
            accorMenuleft.Visible = true;
            LoadNhanSu(Commons.Modules.iCongNhan);
        }
        private void emptySpaceItem1_DoubleClick(object sender, EventArgs e)
        {
            ItemForDON_VI.Visibility = ItemForDON_VI.Visibility == LayoutVisibility.Never ? LayoutVisibility.Always : LayoutVisibility.Never;
            ItemForTO.Visibility = ItemForTO.Visibility == LayoutVisibility.Never ? LayoutVisibility.Always : LayoutVisibility.Never;
            ItemForTT_HT.Visibility = ItemForTT_HT.Visibility == LayoutVisibility.Never ? LayoutVisibility.Always : LayoutVisibility.Never;
            ItemForXI_NGHIEP.Visibility = ItemForXI_NGHIEP.Visibility == LayoutVisibility.Never ? LayoutVisibility.Always : LayoutVisibility.Never;
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
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.BAO_HIEM_Y_TE WHERE ID_CN =  " + Commons.Modules.iCongNhan + "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.BANG_CAP WHERE ID_CN =  " + Commons.Modules.iCongNhan + "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.CONG_NHAN WHERE ID_CN  =" + Convert.ToInt64(grvDSCongNhan.GetFocusedRowCellValue(grvDSCongNhan.Columns["ID_CN"]) + ""));
                grvDSCongNhan.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung"));
            }
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            try
            {

                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "importNhanSu":
                        {
                            frmImportNhanSu frm = new frmImportNhanSu();
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                LoadNhanSu(-1);
                            }
                            break;
                        }
                    case "them":
                        {
                            grdDSCongNhan.Visible = false;
                            ucCTQLNS dl = new ucCTQLNS(-1);
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
                            break;
                        }
                    case "sua":
                        {
                            if (grvDSCongNhan.RowCount == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            Int64 iIDCN = Convert.ToInt64(grvDSCongNhan.GetFocusedRowCellValue("ID_CN"));
                            if (iIDCN == 0)
                            {
                                iIDCN = -1;
                            }

                            grdDSCongNhan.Visible = false;
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
                            if (grvDSCongNhan.RowCount == 0)
                            {
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgChonDongCanXuLy"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "sThongBao"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        {
                            break;
                        }

                }
            }
            catch { }
        }

        private void grvDSCongNhan_RowCountChanged(object sender, EventArgs e)
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
            catch
            {
            }
        }

        private void cboID_LTTHT_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cbo_TTHT, Commons.Modules.ObjSystems.DataTinHTrangHT(Convert.ToInt32(cboID_LTTHT.EditValue), true), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
            LoadNhanSu(-1);
        }

        private void grvDSCongNhan_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            try
            {
                e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml(grvDSCongNhan.GetRowCellValue(e.RowHandle, grvDSCongNhan.Columns["MAU_TT"]).ToString());
                e.HighPriority = true;
            }
            catch (Exception ex)
            {

            }
        }
        private void setMauTT()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_TT_HT, MAU_TT FROM dbo.TINH_TRANG_HT ORDER BY STT"));
                btnBinhThuong.BackColor = System.Drawing.ColorTranslator.FromHtml(dt.Rows[0]["MAU_TT"].ToString());
                btnSapNghiViec.BackColor = System.Drawing.ColorTranslator.FromHtml(dt.Rows[1]["MAU_TT"].ToString());
                btnSapNghiSinh.BackColor = System.Drawing.ColorTranslator.FromHtml(dt.Rows[2]["MAU_TT"].ToString());
                btnNghiDe.BackColor = System.Drawing.ColorTranslator.FromHtml(dt.Rows[3]["MAU_TT"].ToString());
                btnCheDo1Nam.BackColor = System.Drawing.ColorTranslator.FromHtml(dt.Rows[4]["MAU_TT"].ToString());
                btnDaNghiViec.BackColor = System.Drawing.ColorTranslator.FromHtml(dt.Rows[5]["MAU_TT"].ToString());
                btnBoViec.BackColor = System.Drawing.ColorTranslator.FromHtml(dt.Rows[6]["MAU_TT"].ToString());
                btnSapHetHanHD.BackColor = System.Drawing.ColorTranslator.FromHtml(dt.Rows[7]["MAU_TT"].ToString());
            }
            catch { }
        }
        #region click mau
        private void btnBinhThuong_Click(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            cboID_LTTHT.EditValue = Convert.ToInt32(1);
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cbo_TTHT, Commons.Modules.ObjSystems.DataTinHTrangHT(Convert.ToInt32(cboID_LTTHT.EditValue), true), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
            Commons.Modules.sLoad = "";
            cbo_TTHT.EditValue = Convert.ToInt64(1);
        }

        private void btnSapHetHanHD_Click(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            cboID_LTTHT.EditValue = Convert.ToInt32(1);
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cbo_TTHT, Commons.Modules.ObjSystems.DataTinHTrangHT(Convert.ToInt32(cboID_LTTHT.EditValue), true), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
            Commons.Modules.sLoad = "";
            cbo_TTHT.EditValue = Convert.ToInt64(8);
        }

        private void btnSapNghiViec_Click(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            cboID_LTTHT.EditValue = Convert.ToInt32(1);
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cbo_TTHT, Commons.Modules.ObjSystems.DataTinHTrangHT(Convert.ToInt32(cboID_LTTHT.EditValue), true), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
            Commons.Modules.sLoad = "";
            cbo_TTHT.EditValue = Convert.ToInt64(2);
        }

        private void btnSapNghiSinh_Click(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            cboID_LTTHT.EditValue = Convert.ToInt32(1);
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cbo_TTHT, Commons.Modules.ObjSystems.DataTinHTrangHT(Convert.ToInt32(cboID_LTTHT.EditValue), true), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
            Commons.Modules.sLoad = "";
            cbo_TTHT.EditValue = Convert.ToInt64(3);
        }

        private void btnNghiDe_Click(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            cboID_LTTHT.EditValue = Convert.ToInt32(1);
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cbo_TTHT, Commons.Modules.ObjSystems.DataTinHTrangHT(Convert.ToInt32(cboID_LTTHT.EditValue), true), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
            Commons.Modules.sLoad = "";
            cbo_TTHT.EditValue = Convert.ToInt64(4);
        }

        private void btnCheDo1Nam_Click(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            cboID_LTTHT.EditValue = Convert.ToInt32(1);
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cbo_TTHT, Commons.Modules.ObjSystems.DataTinHTrangHT(Convert.ToInt32(cboID_LTTHT.EditValue), true), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
            Commons.Modules.sLoad = "";
            cbo_TTHT.EditValue = Convert.ToInt64(5);
        }

        private void btnDaNghiViec_Click(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            cboID_LTTHT.EditValue = Convert.ToInt32(2);
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cbo_TTHT, Commons.Modules.ObjSystems.DataTinHTrangHT(Convert.ToInt32(cboID_LTTHT.EditValue), true), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
            Commons.Modules.sLoad = "";
            cbo_TTHT.EditValue = Convert.ToInt64(6);
        }

        private void btnBoViec_Click(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            cboID_LTTHT.EditValue = Convert.ToInt32(2);
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cbo_TTHT, Commons.Modules.ObjSystems.DataTinHTrangHT(Convert.ToInt32(cboID_LTTHT.EditValue), true), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
            Commons.Modules.sLoad = "";
            cbo_TTHT.EditValue = Convert.ToInt64(7);
        }
        //private void LoadMau()
        //{
        //    try
        //    {
        //        foreach (DevExpress.XtraGrid.Views.Grid.GridView row in grvDSCongNhan.)
        //            if (Convert.ToInt32(row.Cells[7].Value) < Convert.ToInt32(row.Cells[10].Value))
        //            {
        //                grvDSCon
        //                row.DefaultCellStyle.BackColor = Color.Red;
        //            }
        //        for (int i = 0; i < grvDSCongNhan.RowCount; i++)
        //        {
        //            e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml(grvDSCongNhan.GetRowCellValue(e.RowHandle, grvDSCongNhan.Columns["MAU_TT"]).ToString());
        //            e.HighPriority = true;
        //            grvDSCongNhan.SetRowCellValue(i,"ABC",1)
        //        }
        //    }
        //    catch { }
        //}
        #endregion
    }
}
