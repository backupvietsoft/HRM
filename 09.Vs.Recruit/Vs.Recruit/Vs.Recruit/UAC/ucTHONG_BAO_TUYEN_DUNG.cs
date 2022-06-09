using DevExpress.Utils.Menu;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Vs.Recruit
{
    public partial class ucTHONG_BAO_TUYEN_DUNG : DevExpress.XtraEditors.XtraUserControl
    {
        public static Int64 Id = -1;
        public static string sSOTB = "";
        private ucCTQLUV ucUV;
        public AccordionControl accorMenuleft;

        private Int64 iIDTB_TMP = -1;
        public ucTHONG_BAO_TUYEN_DUNG()
        {
            InitializeComponent();
        }

        private void LoadNN()
        {
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, btnALL);
            Commons.Modules.ObjSystems.MLoadNNXtraGrid(grvChung, this.Name);
            rdoTinhTrang.Properties.Items[0].Description = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "rdoDangSoan");
            rdoTinhTrang.Properties.Items[1].Description = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "rdoDangTuyen");
            rdoTinhTrang.Properties.Items[2].Description = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "rdoDaDong");
        }
        #region even

        private void ucTHONG_BAO_TUYEN_DUNG_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            rdoTinhTrang.SelectedIndex = 1;
            datTNgay.EditValue = Convert.ToDateTime(DateTime.Now.AddDays(-60));
            datDNgay.EditValue = DateTime.Now;
            Commons.Modules.sLoad = "";
            LoadData();
            LoadNN();
        }

        private void btnALL_ButtonClick(object sender, ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "them":
                        {
                            try
                            {
                                Id = -1;
                                frmEditTHONG_BAO_TUYEN_DUNG_VIEW frm = new frmEditTHONG_BAO_TUYEN_DUNG_VIEW(Id);
                                //frm.Size = new Size(900, 600);
                                //frm.StartPosition = FormStartPosition.CenterParent;
                                //frm.Size = new Size((this.Width / 2) + (frm.Width / 2), (this.Height / 2) + (frm.Height / 2));
                                //frm.StartPosition = FormStartPosition.Manual;
                                //frm.Location = new Point(this.Width / 2 - frm.Width / 2 + this.Location.X,
                                //                          this.Height / 2 - frm.Height / 2 + this.Location.Y);

                                if (frm.ShowDialog() == DialogResult.OK)
                                {
                                    iIDTB_TMP = frm.iID_TBTMP;
                                    LoadData();
                                }
                                else
                                {
                                    iIDTB_TMP = frm.iID_TBTMP;
                                    LoadData();
                                }
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message);
                            }
                            break;
                        }

                    case "xoa":
                        {
                            if (grvChung.RowCount == 0)
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
            catch (Exception EX)
            {
                XtraMessageBox.Show(EX.Message.ToString());
            }
        }
        private void rdoTinhTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadData();
        }

        private void txtTNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            if (!dxValidationProvider1.Validate()) return;
            dxValidationProvider1.Validate();
            if (datTNgay.Text != "" && datTNgay != null && datDNgay.Text != "" && datDNgay != null)
            {
                LoadData();
            }
        }
        private void txtDNgay_EditValueChanged(object sender, EventArgs e)
        {

            if (Commons.Modules.sLoad == "0Load") return;
            if (!dxValidationProvider1.Validate()) return;
            dxValidationProvider1.Validate();
            if (datTNgay.Text != "" && datTNgay != null && datDNgay.Text != "" && datDNgay != null)
            {
                LoadData();
            }
        }
        private void grvChung_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (grvChung.RowCount < 1)
                {
                    return;
                }
                Id = Convert.ToInt64(grvChung.GetFocusedRowCellValue("ID_TB"));
                frmEditTHONG_BAO_TUYEN_DUNG_VIEW frm = new frmEditTHONG_BAO_TUYEN_DUNG_VIEW(Id);
                //frm.Size = new Size(900, 600);
                //frm.StartPosition = FormStartPosition.CenterParent;
                //frm.Size = new Size((this.Width / 2) + (frm.Width / 2), (this.Height / 2) + (frm.Height / 2));
                //frm.StartPosition = FormStartPosition.Manual;
                //frm.Location = new Point(this.Width / 2 - frm.Width / 2 + this.Location.X,
                //                          this.Height / 2 - frm.Height / 2 + this.Location.Y);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    iIDTB_TMP = frm.iID_TBTMP;
                    LoadData();
                }
                else
                {
                    iIDTB_TMP = frm.iID_TBTMP;
                    LoadData();
                }
            }
            catch
            {

            }
        }
        #endregion

        #region function

        private void LoadData()
        {
            try
            {
                DataTable dt = new DataTable();
                if (Commons.Modules.sLoad == "0Load") return;
                if (datTNgay.EditValue.ToString() != "" && datTNgay != null && datDNgay.EditValue.ToString() != "" && datDNgay != null)
                {
                    System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTBTuyenDung", conn);
                    cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@TINH_TRANG", SqlDbType.Int).Value = rdoTinhTrang.EditValue;
                    cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = datTNgay.EditValue;
                    cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = datDNgay.EditValue;
                    cmd.CommandType = CommandType.StoredProcedure;
                    System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dt = ds.Tables[0].Copy();
                    dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_TB"] };
                    if (grdChung.DataSource == null)
                    {
                        Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, false, true, true, false, false, this.Name);
                        grvChung.Columns["ID_TB"].Visible = false;
                    }
                    else
                    {
                        grdChung.DataSource = dt;
                    }
                }
                if (iIDTB_TMP != -1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(iIDTB_TMP));
                    grvChung.FocusedRowHandle = grvChung.GetRowHandle(index);
                }
            }
            catch
            { }
        }

        private void DeleteData()
        {
            try
            {
                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msg_Xoa"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeXoa"), MessageBoxButtons.YesNo) == DialogResult.No) return;
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE	dbo.THONG_BAO_TUYEN_DUNG WHERE ID_TB = " + grvChung.GetFocusedRowCellValue("ID_TB") + "");
                grvChung.DeleteSelectedRows();
                ((DataTable)grdChung.DataSource).AcceptChanges();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion


        #region chuot phai
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
        //Nhap ung vien
        public DXMenuItem MCreateMenuNhapUngVien(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "ucTHONG_BAO_TUYEN_DUNG", "mnuNhapUV", Commons.Modules.TypeLanguage);
            DXMenuItem menuNhapUV = new DXMenuItem(sStr, new EventHandler(NhapUngVien));
            menuNhapUV.Tag = new RowInfo(view, rowHandle);
            return menuNhapUV;
        }
        public void NhapUngVien(object sender, EventArgs e)
        {
            ucUV = new ucCTQLUV(-1);
            ucUV.Dock = DockStyle.Fill;
            Commons.Modules.ObjSystems.ShowWaitForm(this);
            ucUV.Refresh();
            bool flagTBTD = true; // Flag = true thì Ứng viên được nhập từ form này sẽ được add tự động vào Thông báo tuyển dụng đang chọn
            ucUV.flag = flagTBTD;
            ucUV.iIDTB = Id;
            //ns.accorMenuleft = accorMenuleft;
            tablePanel1.Hide();
            this.Controls.Add(ucUV);
            this.Dock = DockStyle.Fill;
            ucUV.backWindowsUIButtonPanel.ButtonClick += BackWindowsUIButtonPanel_ButtonClick;
            accorMenuleft.Visible = false;
            Commons.Modules.ObjSystems.HideWaitForm();
        }

        public void BackWindowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            ucUV.Hide();
            tablePanel1.Show();

            accorMenuleft.Visible = true;
        }


        //Chon ung vien
        public static DXMenuItem MCreateMenuChonUngVien(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "ucTHONG_BAO_TUYEN_DUNG", "mnuChonUngVien", Commons.Modules.TypeLanguage);
            DXMenuItem menuChonUngVien = new DXMenuItem(sStr, new EventHandler(ChonUngVienRowClick));
            menuChonUngVien.Tag = new RowInfo(view, rowHandle);
            return menuChonUngVien;
        }
        static void ChonUngVienRowClick(object sender, EventArgs e)
        {
            //try
            //{
            //    frmChonUngVien frm = new frmChonUngVien(Id);
            //    //frm.Size = new Size(900, 600);
            //    //frm.StartPosition = FormStartPosition.CenterParent;
            //    //frm.Size = new Size((frm.Width / 2) + (frm.Width / 2), (frm.Height / 2) + (frm.Height / 2));
            //    //frm.StartPosition = FormStartPosition.Manual;
            //    //frm.Location = new Point(frm.Width / 2 - frm.Width / 2 + frm.Location.X,
            //    //                          frm.Height / 2 - frm.Height / 2 + frm.Location.Y);
            //    frm.ShowDialog();
            //}
            //catch { }
        }
        //Xem DS Ung Vien
        public static DXMenuItem MCreateMenuXemDSUngVien(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, "ucTHONG_BAO_TUYEN_DUNG", "mnuXemDSUngVien", Commons.Modules.TypeLanguage);
            DXMenuItem menuXemDSUngVien = new DXMenuItem(sStr, new EventHandler(XemDSUngVienRowClick));
            menuXemDSUngVien.Tag = new RowInfo(view, rowHandle);
            return menuXemDSUngVien;
        }
        static void XemDSUngVienRowClick(object sender, EventArgs e)
        {
            try
            {
                //frmXemDSUngVien frm = new frmXemDSUngVien(Id);
                //frm.sSO_TB = sSOTB;
                ////frm.Size = new Size(900, 600);
                ////frm.StartPosition = FormStartPosition.CenterParent;
                ////frm.Size = new Size((frm.Width / 2) + (frm.Width / 2), (frm.Height / 2) + (frm.Height / 2));
                ////frm.StartPosition = FormStartPosition.Manual;
                ////frm.Location = new Point(frm.Width / 2 - frm.Width / 2 + frm.Location.X,
                ////                          frm.Height / 2 - frm.Height / 2 + frm.Location.Y);
                //frm.ShowDialog();
            }
            catch { }
        }
        private void grvChung_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                Id = Convert.ToInt64(grvChung.GetFocusedRowCellValue("ID_TB"));

                sSOTB = grvChung.GetFocusedRowCellValue("SO_TB").ToString();
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;

                    e.Menu.Items.Clear();

                    DevExpress.Utils.Menu.DXMenuItem itemNhapUV = MCreateMenuNhapUngVien(view, irow);
                    e.Menu.Items.Add(itemNhapUV);

                    DevExpress.Utils.Menu.DXMenuItem itemChonUV = MCreateMenuChonUngVien(view, irow);
                    e.Menu.Items.Add(itemChonUV);

                    DevExpress.Utils.Menu.DXMenuItem itemXemDSUV = MCreateMenuXemDSUngVien(view, irow);
                    e.Menu.Items.Add(itemXemDSUV);
                }
            }
            catch
            {
            }
        }
        #endregion

        private void ucTHONG_BAO_TUYEN_DUNG_Resize(object sender, EventArgs e)
        {
            dataLayoutControl1.Refresh();
        }
        private void grvChung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteData();
            }
        }

        
    }
}
