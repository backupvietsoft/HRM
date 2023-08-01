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
using DevExpress.Utils.Menu;
using DevExpress.CodeParser;

namespace Vs.HRM
{
    public partial class ucQLNS : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable dt;
        public AccordionControl accorMenuleft;
        public LabelControl labelNV;
        private Int64 ID_TT_1 = -1;
        private Int64 ID_TT_2 = -1;
        private Int64 ID_TT_3 = -1;
        private Int64 ID_TT_4 = -1;
        private Int64 ID_TT_5 = -1;
        private Int64 ID_TT_6 = -1;
        private Int64 ID_TT_7 = -1;
        private Int64 ID_TT_8 = -1;

        private int ID_LTTHT_1 = -1;
        private int ID_LTTHT_2 = -1;
        private int ID_LTTHT_3 = -1;
        private int ID_LTTHT_4 = -1;
        private int ID_LTTHT_5 = -1;
        private int ID_LTTHT_6 = -1;
        private int ID_LTTHT_7 = -1;
        private int ID_LTTHT_8 = -1;
        public ucQLNS()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButtonPanel1);
        }

        private void ucQLNS_Load(object sender, EventArgs e)
        {
            try
            {
                visibleButton();

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
                datTNgay.DateTime = DateTime.Now.AddDays(10).AddMonths(-2);
                datDNgay.DateTime = DateTime.Now.AddDays(10);
                Commons.OSystems.SetDateEditFormat(datTNgay);
                Commons.OSystems.SetDateEditFormat(datDNgay);
                lblTheoNgay.Visibility = LayoutVisibility.Never;
                lblDenNgay.Visibility = LayoutVisibility.Never;
                LoadNhanSu(-1);
                Commons.Modules.sLoad = "";
                Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButtonPanel1);
            }
            catch { }
        }

        private void visibleButton()
        {
            try
            {
                btn1.Visible = false;
                btn2.Visible = false;
                btn3.Visible = false;
                btn4.Visible = false;
                btn5.Visible = false;
                btn6.Visible = false;
                btn7.Visible = false;
                btn8.Visible = false;

                lbl1.Visible = false;
                lbl2.Visible = false;
                lbl3.Visible = false;
                lbl4.Visible = false;
                lbl5.Visible = false;
                lbl6.Visible = false;
                lbl7.Visible = false;
                lbl8.Visible = false;

                tablePanel1.Columns[1].Width = 0;
                tablePanel1.Columns[2].Width = 0;
                tablePanel1.Columns[3].Width = 0;
                tablePanel1.Columns[4].Width = 0;
                tablePanel1.Columns[5].Width = 0;
                tablePanel1.Columns[6].Width = 0;
                tablePanel1.Columns[7].Width = 0;
                tablePanel1.Columns[8].Width = 0;

                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_TT_HT, MAU_TT, TEN_TT_HT, ISNULL(ID_LTTHT,1) ID_LTTHT FROM dbo.TINH_TRANG_HT ORDER BY STT"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            {
                                tablePanel1.Columns[1].Width = 7.5F;

                                ID_LTTHT_1 = Convert.ToInt32(dt.Rows[i]["ID_LTTHT"]);
                                ID_TT_1 = Convert.ToInt64(dt.Rows[i]["ID_TT_HT"]);
                                lbl1.Visible = true;
                                lbl1.Text = dt.Rows[i]["TEN_TT_HT"].ToString();
                                btn1.Visible = true;
                                btn1.BackColor = System.Drawing.ColorTranslator.FromHtml(dt.Rows[i]["MAU_TT"].ToString());
                                break;
                            }
                        case 1:
                            {
                                tablePanel1.Columns[2].Width = 7.5F;

                                ID_LTTHT_2 = Convert.ToInt32(dt.Rows[i]["ID_LTTHT"]);
                                ID_TT_2 = Convert.ToInt64(dt.Rows[i]["ID_TT_HT"]);
                                lbl2.Visible = true;
                                lbl2.Text = dt.Rows[i]["TEN_TT_HT"].ToString();
                                btn2.Visible = true;
                                btn2.BackColor = System.Drawing.ColorTranslator.FromHtml(dt.Rows[i]["MAU_TT"].ToString());
                                break;
                            }
                        case 2:
                            {
                                tablePanel1.Columns[3].Width = 7.5F;

                                ID_LTTHT_3 = Convert.ToInt32(dt.Rows[i]["ID_LTTHT"]);
                                ID_TT_3 = Convert.ToInt64(dt.Rows[i]["ID_TT_HT"]);
                                lbl3.Visible = true;
                                lbl3.Text = dt.Rows[i]["TEN_TT_HT"].ToString();
                                btn3.Visible = true;
                                btn3.BackColor = System.Drawing.ColorTranslator.FromHtml(dt.Rows[i]["MAU_TT"].ToString());
                                break;
                            }
                        case 3:
                            {
                                tablePanel1.Columns[4].Width = 7.5F;

                                ID_LTTHT_4 = Convert.ToInt32(dt.Rows[i]["ID_LTTHT"]);
                                ID_TT_4 = Convert.ToInt64(dt.Rows[i]["ID_TT_HT"]);
                                lbl4.Visible = true;
                                lbl4.Text = dt.Rows[i]["TEN_TT_HT"].ToString();
                                btn4.Visible = true;
                                btn4.BackColor = System.Drawing.ColorTranslator.FromHtml(dt.Rows[i]["MAU_TT"].ToString());
                                break;
                            }
                        case 4:
                            {
                                tablePanel1.Columns[5].Width = 7.5F;

                                ID_LTTHT_5 = Convert.ToInt32(dt.Rows[i]["ID_LTTHT"]);
                                ID_TT_5 = Convert.ToInt64(dt.Rows[i]["ID_TT_HT"]);
                                lbl5.Visible = true;
                                lbl5.Text = dt.Rows[i]["TEN_TT_HT"].ToString();
                                btn5.Visible = true;
                                btn5.BackColor = System.Drawing.ColorTranslator.FromHtml(dt.Rows[i]["MAU_TT"].ToString());
                                break;
                            }
                        case 5:
                            {
                                tablePanel1.Columns[6].Width = 7.5F;

                                ID_LTTHT_6 = Convert.ToInt32(dt.Rows[i]["ID_LTTHT"]);
                                ID_TT_6 = Convert.ToInt64(dt.Rows[i]["ID_TT_HT"]);
                                lbl6.Visible = true;
                                lbl6.Text = dt.Rows[i]["TEN_TT_HT"].ToString();
                                btn6.Visible = true;
                                btn6.BackColor = System.Drawing.ColorTranslator.FromHtml(dt.Rows[i]["MAU_TT"].ToString());
                                break;
                            }
                        case 6:
                            {
                                tablePanel1.Columns[7].Width = 7.5F;

                                ID_LTTHT_7 = Convert.ToInt32(dt.Rows[i]["ID_LTTHT"]);
                                ID_TT_7 = Convert.ToInt64(dt.Rows[i]["ID_TT_HT"]);
                                lbl7.Visible = true;
                                lbl7.Text = dt.Rows[i]["TEN_TT_HT"].ToString();
                                btn7.Visible = true;
                                btn7.BackColor = System.Drawing.ColorTranslator.FromHtml(dt.Rows[i]["MAU_TT"].ToString());
                                break;
                            }
                        case 7:
                            {
                                tablePanel1.Columns[8].Width = 7.5F;

                                ID_LTTHT_8 = Convert.ToInt32(dt.Rows[i]["ID_LTTHT"]);
                                ID_TT_8 = Convert.ToInt64(dt.Rows[i]["ID_TT_HT"]);
                                lbl8.Visible = true;
                                lbl8.Text = dt.Rows[i]["TEN_TT_HT"].ToString();
                                btn8.Visible = true;
                                btn8.BackColor = System.Drawing.ColorTranslator.FromHtml(dt.Rows[i]["MAU_TT"].ToString());
                                break;
                            }
                    }
                }
            }
            catch { }
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
            try
            {
                DataRowView drv = (DataRowView)cbo_TTHT.GetSelectedDataRow();
                if ((drv == null ? "" : drv.Row["KY_HIEU"].ToString().Trim()) != "SHHHD")
                {
                    lblTheoNgay.Visibility = LayoutVisibility.Never;
                    lblDenNgay.Visibility = LayoutVisibility.Never;
                }
                else
                {
                    lblTheoNgay.Visibility = LayoutVisibility.Always;
                    lblDenNgay.Visibility = LayoutVisibility.Always;
                }
                LoadNhanSu(-1);
            }
            catch { }

            Commons.Modules.sLoad = "";
        }
        private void LoadNhanSu(Int64 iIdNs)
        {
            try
            {
                DataTable dtTmp = new DataTable();
                DataRowView drv = (DataRowView)cbo_TTHT.GetSelectedDataRow(); // lấy ký hiệu của tình trạng đang chọn
                dtTmp.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListNS_DanhSach", cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, cbo_TTHT.EditValue, drv.Row["KY_HIEU"].ToString().Trim(), cboID_LTTHT.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, datTNgay.DateTime, datDNgay.DateTime));
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
            catch (Exception ex) { XtraMessageBox.Show(ex.Message.ToString()); }
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
                DevExpress.Utils.DXMouseEventArgs ea = e as DevExpress.Utils.DXMouseEventArgs;
                GridView view = sender as GridView;
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = view.CalcHitInfo(ea.Location);
                if (info.InRow || info.InRowCell)
                {
                    try
                    {
                        labelNV.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        labelNV.ForeColor = System.Drawing.Color.FromArgb(0, 0, 255);
                        labelNV.Text = grvDSCongNhan.GetFocusedRowCellValue(grvDSCongNhan.Columns["MS_CN"]).ToString() + " - " + grvDSCongNhan.GetFocusedRowCellValue(grvDSCongNhan.Columns["HO_TEN"]).ToString() + " - " + grvDSCongNhan.GetFocusedRowCellValue(grvDSCongNhan.Columns["TEN_TO"]).ToString();
                    }
                    catch (Exception ex) { }
                    grdDSCongNhan.Visible = false;
                    ucCTQLNS dl = new ucCTQLNS(Convert.ToInt64(grvDSCongNhan.GetFocusedRowCellValue(grvDSCongNhan.Columns["ID_CN"])));
                    dl.labelNV = labelNV;
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
            }
            catch { }

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
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "UPDATE dbo.CONG_NHAN SET USER_DEL = '" + Commons.Modules.UserName + "' WHERE ID_CN = " + Commons.Modules.iCongNhan);
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
                    case "import":
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
                            try
                            {
                                labelNV.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                labelNV.ForeColor = System.Drawing.Color.FromArgb(0, 0, 255);
                                labelNV.Text = grvDSCongNhan.GetFocusedRowCellValue(grvDSCongNhan.Columns["MS_CN"]).ToString() + " - " + grvDSCongNhan.GetFocusedRowCellValue(grvDSCongNhan.Columns["HO_TEN"]).ToString() + " - " + grvDSCongNhan.GetFocusedRowCellValue(grvDSCongNhan.Columns["TEN_TO"]).ToString();
                            }
                            catch (Exception ex) { }
                            grdDSCongNhan.Visible = false;
                            ucCTQLNS dl = new ucCTQLNS(iIDCN);
                            Commons.Modules.ObjSystems.ShowWaitForm(this);
                            dl.Refresh();
                            dl.labelNV = labelNV;
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
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)grdDSCongNhan.DataSource;
                ItemForSumNhanVien.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "SumNhanVien") + ": " + dt.Rows.Count;
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
            GridView view = sender as GridView;
            try
            {
                if (view.IsRowVisible(e.RowHandle - 1) != RowVisibleState.Visible && view.IsRowVisible(e.RowHandle + 1) != RowVisibleState.Visible) return;
                if (grvDSCongNhan.RowCount == 0)
                    return;
                if (grvDSCongNhan.GetRowCellValue(e.RowHandle, grvDSCongNhan.Columns["MAU_TT"]).ToString().Trim() == "#FFFFFF") return;
                {
                    e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml(grvDSCongNhan.GetRowCellValue(e.RowHandle, grvDSCongNhan.Columns["MAU_TT"]).ToString());
                }
            }
            catch
            {
            }
        }
        #region click mau
        private void btn1_Click(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            cboID_LTTHT.EditValue = ID_LTTHT_1;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cbo_TTHT, Commons.Modules.ObjSystems.DataTinHTrangHT(Convert.ToInt32(cboID_LTTHT.EditValue), true), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
            Commons.Modules.sLoad = "";
            cbo_TTHT.EditValue = ID_TT_1;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            cboID_LTTHT.EditValue = ID_LTTHT_2;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cbo_TTHT, Commons.Modules.ObjSystems.DataTinHTrangHT(Convert.ToInt32(cboID_LTTHT.EditValue), true), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
            Commons.Modules.sLoad = "";
            cbo_TTHT.EditValue = ID_TT_2;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            cboID_LTTHT.EditValue = ID_LTTHT_3;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cbo_TTHT, Commons.Modules.ObjSystems.DataTinHTrangHT(Convert.ToInt32(cboID_LTTHT.EditValue), true), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
            Commons.Modules.sLoad = "";
            cbo_TTHT.EditValue = ID_TT_3;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            cboID_LTTHT.EditValue = ID_LTTHT_4;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cbo_TTHT, Commons.Modules.ObjSystems.DataTinHTrangHT(Convert.ToInt32(cboID_LTTHT.EditValue), true), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
            Commons.Modules.sLoad = "";
            cbo_TTHT.EditValue = ID_TT_4;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            cboID_LTTHT.EditValue = ID_LTTHT_5;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cbo_TTHT, Commons.Modules.ObjSystems.DataTinHTrangHT(Convert.ToInt32(cboID_LTTHT.EditValue), true), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
            Commons.Modules.sLoad = "";
            cbo_TTHT.EditValue = ID_TT_5;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            cboID_LTTHT.EditValue = ID_LTTHT_6;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cbo_TTHT, Commons.Modules.ObjSystems.DataTinHTrangHT(Convert.ToInt32(cboID_LTTHT.EditValue), true), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
            Commons.Modules.sLoad = "";
            cbo_TTHT.EditValue = ID_TT_6;

        }

        private void btn7_Click(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            cboID_LTTHT.EditValue = ID_LTTHT_7;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cbo_TTHT, Commons.Modules.ObjSystems.DataTinHTrangHT(Convert.ToInt32(cboID_LTTHT.EditValue), true), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
            Commons.Modules.sLoad = "";
            cbo_TTHT.EditValue = ID_TT_7;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            cboID_LTTHT.EditValue = ID_LTTHT_8;
            Commons.Modules.ObjSystems.MLoadLookUpEdit(cbo_TTHT, Commons.Modules.ObjSystems.DataTinHTrangHT(Convert.ToInt32(cboID_LTTHT.EditValue), true), "ID_TT_HT", "TEN_TT_HT", "TEN_TT_HT");
            Commons.Modules.sLoad = "";
            cbo_TTHT.EditValue = ID_TT_8;
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
        public DXMenuItem MCreateMenuLapHopDong(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblLapHopDong", Commons.Modules.TypeLanguage);
            DXMenuItem menuLapHopDong = new DXMenuItem(sStr, new EventHandler(LapHopDong));
            menuLapHopDong.Tag = new RowInfo(view, rowHandle);
            return menuLapHopDong;
        }
        public DXMenuItem MCreateCNQuaTrinhCT(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblLapCongTac", Commons.Modules.TypeLanguage);
            DXMenuItem menuLapCongTac = new DXMenuItem(sStr, new EventHandler(LoadCNCongTac));
            menuLapCongTac.Tag = new RowInfo(view, rowHandle);
            return menuLapCongTac;
        }
        public void LapHopDong(object sender, EventArgs e)
        {
            try
            {
                frmTaoHDLD frm = new frmTaoHDLD();
                frm.dt_temp = new DataTable();
                frm.dt_temp = Commons.Modules.ObjSystems.ConvertDatatable(grdDSCongNhan);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadNhanSu(-1);
                }
                else
                {
                    LoadNhanSu(-1);
                }
            }
            catch (Exception ex) { }
        }
        public void LoadCNCongTac(object sender, EventArgs e)
        {
            try
            {
                frmCNQuaTrinhCongTac frm = new frmCNQuaTrinhCongTac();
                frm.dtTmp = new DataTable();
                frm.dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdDSCongNhan);
                frm.ShowDialog();
            }
            catch (Exception ex) { }
        }
        private void grvDSCongNhan_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (Commons.Modules.iPermission != 1) return;
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();
                    DataRowView drv = (DataRowView)cbo_TTHT.GetSelectedDataRow();
                    if (drv.Row["KY_HIEU"].ToString().Trim() == "SHHHD")
                    {
                        DevExpress.Utils.Menu.DXMenuItem itemLapHopDong = MCreateMenuLapHopDong(view, irow);
                        e.Menu.Items.Add(itemLapHopDong);
                    }
                    DevExpress.Utils.Menu.DXMenuItem itemCNQD = MCreateCNQuaTrinhCT(view, irow);
                    e.Menu.Items.Add(itemCNQD);
                }
            }
            catch
            {
            }
        }

        #endregion

        private void searchControl2_EditValueChanged(object sender, EventArgs e)
        {
            switch (Commons.Modules.KyHieuDV)
            {
                         
                case "NB":
                    {
                        DataTable dtTmp = new DataTable();
                        dtTmp = (DataTable)grdDSCongNhan.DataSource;
                        //dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grvTo);
                        //String sMSCN;
                        try
                        {
                            string sDK = "";
                            //sMSCN = "";
                            if (searchControl2.Text != "")
                                sDK = "MS_CN = '" + searchControl2.Text + "'";
                            dtTmp.DefaultView.RowFilter = sDK;
                        }
                        catch (Exception ex)
                        {
                            dtTmp.DefaultView.RowFilter = "";
                        }
                        grvDSCongNhan.ExpandAllGroups();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            grvDSCongNhan.ExpandAllGroups();
        }
    }
}
