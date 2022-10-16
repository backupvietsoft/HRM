using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DataTable = System.Data.DataTable;

namespace Vs.Payroll
{
    public partial class ucMaHang : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;

        public static ucMaHang _instance;
        public static ucMaHang Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucMaHang();
                return _instance;
            }
        }
        public ucMaHang()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);

        }
        private void LoadCbo()
        {
            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spMaHang", conn);
            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Commons.Modules.iIDUser;
            cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
            cmd.CommandType = CommandType.StoredProcedure;
            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0].Copy();
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboKhachHang, dt, "ID_DT", "TEN_NGAN", "TEN_NGAN");

            dt = new DataTable();
            dt = ds.Tables[1].Copy();
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboLHH, dt, "ID_LHH", "TEN_LOAI_HH", "TEN_LOAI_HH");
        }

        private void ucMaHang_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
            DateTime dNgay = DateTime.Now.AddMonths(-4);
            datTNgay.DateTime = Convert.ToDateTime(("01/" + dNgay.Month + "/" + dNgay.Year)); ;
            datDNgay.DateTime = datTNgay.DateTime.AddMonths(5).AddDays(-1);
            LoadCbo();
            EnableButon(isAdd);
            Commons.Modules.sLoad = "";
            LoadData(-1);
        }
        private void LoadData(Int64 key)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            int ID_DV = -1;
            int ID_DT = -1;
            string TEN_LHH = "-1";
            DateTime TNgay = DateTime.Now.AddMonths(-2);
            DateTime DNgay = DateTime.Now.Date;
            int @DDong = 0;
            try { ID_DV = int.Parse(cboDonVi.EditValue.ToString()); } catch { }
            try { ID_DT = int.Parse(cboKhachHang.EditValue.ToString()); } catch { }

            try { TEN_LHH = cboLHH.EditValue.ToString(); } catch { }
            try { TNgay = datTNgay.DateTime.Date; } catch { }
            try { DNgay = datDNgay.DateTime.Date; } catch { }
            try {
                if (chkDaDong.Checked) @DDong = 1; else DDong = 0;
            } catch { }

            System.Data.SqlClient.SqlConnection conn;
            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
            conn.Open();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spMaHang", conn);
            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Commons.Modules.iIDUser;
            cmd.Parameters.Add("@UName", SqlDbType.NVarChar).Value = Commons.Modules.UserName;
            cmd.Parameters.Add("@ID_DV", SqlDbType.Int).Value = ID_DV;
            cmd.Parameters.Add("@ID_DT", SqlDbType.Int).Value = ID_DT;
            cmd.Parameters.Add("@TEN_LHH", SqlDbType.NVarChar).Value = TEN_LHH;
            cmd.Parameters.Add("@TNgay", SqlDbType.Date).Value = TNgay;
            cmd.Parameters.Add("@DNgay", SqlDbType.Date).Value = DNgay;
            cmd.Parameters.Add("@DDong", SqlDbType.Int).Value = @DDong;

            cmd.CommandType = CommandType.StoredProcedure;
            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0].Copy();
            dt.PrimaryKey = new DataColumn[] { dt.Columns[0]};

            


            if (grdData.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, false, false, true, this.Name);
            }
            else
                try { grdData.DataSource = dt; } catch { }


            if (key != -1)
            {
                int index = dt.Rows.IndexOf(dt.Rows.Find(key));
                grvData.FocusedRowHandle = grvData.GetRowHandle(index);
                grvData.SelectRow(index);
            }
            else
            {
                grvData.FocusedRowHandle = 0;
                grvData.SelectRow(0);
            }
            Commons.OSystems.DinhDangNgayThang(grvData);

        }


        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        ThemSua(true);
                        break;
                    }
                case "sua":
                    {   ThemSua(false);
                        EnableButon(isAdd);
                        break;
                    }
                case "xoa":
                    {
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteMaHang"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                        //xóa
                        try
                        {
                            try
                            {
                                Commons.Modules.sId = grvData.GetFocusedRowCellValue("ID_ORD").ToString();
                            }
                            catch { Commons.Modules.sId = "-1"; }
                            if (Commons.Modules.sId == "-1") return;

                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spMaHang", conn);
                            cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 3;
                            cmd.Parameters.Add("@ID_ORD", SqlDbType.BigInt).Value = Int64.Parse(Commons.Modules.sId);
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0].Copy();
                            if (dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0][0].ToString() == "0")
                                {
                                    this.BeginInvoke(new MethodInvoker(delegate
                                    {
                                        LoadCbo();
                                        LoadData(-1);
                                    }));
                                }
                                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", dt.Rows[0][1].ToString()), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else { XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgXoaKhongThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            
                        }
                        catch 
                        {}
                        break;
                    }
                case "In":
                    {

                        break;
                    }
                case "ghi":
                    {
                        Validate();
                        if (grvData.HasColumnErrors) return;
                        if (Savedata() == false)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                        }
                        isAdd = false;

                        EnableButon(isAdd);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        break;
                    }
                case "khongghi":
                    {
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        isAdd = false;
                        EnableButon(isAdd);
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
            }
        }

        private bool Savedata()
        {
            throw new NotImplementedException();
        }

        private void EnableButon(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = !visible;
            btnALL.Buttons[1].Properties.Visible = !visible;
            btnALL.Buttons[2].Properties.Visible = !visible;
            btnALL.Buttons[3].Properties.Visible = !visible;
            btnALL.Buttons[4].Properties.Visible = !visible;
            btnALL.Buttons[5].Properties.Visible = visible;
            cboLHH.Enabled = !visible;
            cboDonVi.Enabled = !visible;
            cboKhachHang.Enabled = !visible;
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            LoadData(-1);
        }

        private void grvData_RowCountChanged(object sender, EventArgs e)
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
                //XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void grvData_DoubleClick(object sender, EventArgs e)
        {

            if (grvData.RowCount == 0)
            {
                return;
            }
            ThemSua(false);
        }


        private void ThemSua(Boolean AddEdit) //AddEdit = true --> them
        {

            try
            {
                if (Commons.Modules.iPermission != 1) return;
                if (grvData.RowCount == 0 && AddEdit == false) return;

                //frmEditMaHang ctl;
                try
                {
                    if (AddEdit)
                        Commons.Modules.sId = (-1).ToString();
                    else
                        Commons.Modules.sId = grvData.GetFocusedRowCellValue("ID_ORD").ToString();
                }
                catch { Commons.Modules.sId = (-1).ToString(); }


                frmEditMaHang ctl = new frmEditMaHang(Int64.Parse(Commons.Modules.sId), AddEdit);

                ctl.StartPosition = FormStartPosition.CenterParent;
                ctl.MinimizeBox = false;
                double iW, iH;
                iW = Screen.PrimaryScreen.WorkingArea.Width / 2.2;
                iH = Screen.PrimaryScreen.WorkingArea.Height / 2.2;
                if (iW < 800)
                {
                    iW = iW * 1.2;
                    iH = iH * 1.2;
                }
                ctl.Size = new Size((int)iW, (int)iH);
                if (ctl.ShowDialog() == DialogResult.OK)
                {
                    LoadCbo();
                    LoadData(Convert.ToInt64(Commons.Modules.sId));
                }
                else { LoadData(Convert.ToInt64(Commons.Modules.sId)); }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());

            }
        }

        private void windowsUIButton_Click(object sender, EventArgs e)
        {

        }
    }
}