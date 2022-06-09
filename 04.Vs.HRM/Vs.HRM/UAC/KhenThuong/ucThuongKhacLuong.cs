using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucThuongKhacLuong : DevExpress.XtraEditors.XtraUserControl
    {
        public ucThuongKhacLuong()
        {
            InitializeComponent();

            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
        }

        private void optCachThuong_Click(object sender, EventArgs e)
        {
            if(raCacTinh.SelectedIndex ==0)
            {
                txTienQuyDinh.Properties.ReadOnly = true;
                txSoThang.Properties.ReadOnly = false;
                txSoTien.Properties.ReadOnly = false;
                txSoToiThieu.Properties.ReadOnly = false;
            }
            else
            {
                txSoThang.Properties.ReadOnly = true;
                txSoTien.Properties.ReadOnly = true;
                txSoToiThieu.Properties.ReadOnly = true;
                txTienQuyDinh.Properties.ReadOnly = false;
            }
        }

        private void LoadThang(DateTime dThang)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr
                , "spThuongKhacLuong"
                , "01/01/1900"
                , "01/01/1900"
                , -1, -1, -1
                , Commons.Modules.UserName
                , Commons.Modules.TypeLanguage
                , ""
                ,"Cbo"));
            
            if (grdThang.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dt, false, true, true, true, true, this.Name);
                   if (grvThang.Columns["ID_TKL"] != null)
                    grvThang.Columns["ID_TKL"].Visible = false;
                if (grvThang.Columns["ID_NDTKL"] != null)
                    grvThang.Columns["ID_NDTKL"].Visible = false;
                if (grvThang.Columns["NGAY_TKL"] != null)
                    grvThang.Columns["NGAY_TKL"].Visible = true;
                if (grvThang.Columns["TIEN_QUY_DINH"] != null)
                    grvThang.Columns["TIEN_QUY_DINH"].Visible = false;
                if (grvThang.Columns["SO_TIEN"] != null)
                    grvThang.Columns["SO_TIEN"].Visible = false;
                if (grvThang.Columns["SO_TIEN_GH"] != null)
                    grvThang.Columns["SO_TIEN_GH"].Visible = false;
                if (grvThang.Columns["TD_BC"] != null)
                    grvThang.Columns["TD_BC"].Visible = false;
                if (grvThang.Columns["ID_CN"] != null)
                    grvThang.Columns["ID_CN"].Visible = false;
                if (grvThang.Columns["SO_TIEN_NHAN"] != null)
                    grvThang.Columns["SO_TIEN_NHAN"].Visible = true;
                if (grvThang.Columns["SO_THANG_TINH"] != null)
                    grvThang.Columns["SO_THANG_TINH"].Visible = false;
            }
            else
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dt, false, false, true, false, false, this.Name);
            
            try
            {

                if (dThang == Convert.ToDateTime("01/01/1900"))
                {
                    if (dt == null  || dt.Rows.Count <= 0) LoadNull();
                    else
                    {
                        //T1.ID_NDTKL, T1.NGAY_TKL, T1.SO_TIEN_NHAN, T1.SO_THANG_TINH, T1.SO_TIEN, T1.SO_TIEN_GH, T1.TD_BC
                        cboThang.Text = Convert.ToDateTime(dt.Rows[0]["NGAY_TKL"].ToString()).ToShortDateString();
                        txTienQuyDinh.Text = dt.Rows[0]["SO_TIEN_NHAN"].ToString();
                        txSoThang.Text = dt.Rows[0]["SO_THANG_TINH"].ToString();
                        txSoTien.Text = dt.Rows[0]["SO_TIEN"].ToString();
                        txSoToiThieu.Text = dt.Rows[0]["SO_TIEN_GH"].ToString();
                        txTieuDeBaoCao.Text = dt.Rows[0]["TD_BC"].ToString();
                    }
                }
                else
                {
                    cboThang.Text = dThang.Date.ToShortDateString();

                    DataRow[] dr;
                    dr = dt.Select("NGAY_TKL" + "='" + cboThang.Text + "'", "NGAY_TKL", DataViewRowState.CurrentRows);
                    if (dr.Count() == 1)
                    {
                        cboThang.Text = Convert.ToDateTime(dr[0]["NGAY_TKL"].ToString()).ToShortDateString();
                        txTienQuyDinh.Text = dr[0]["TIEN_QUY_DINH"].ToString();
                        txSoThang.Text = dr[0]["SO_THANG_TINH"].ToString();
                        txSoTien.Text = dr[0]["SO_TIEN"].ToString();
                        txSoToiThieu.Text = dr[0]["SO_TIEN_GH"].ToString();
                        txTieuDeBaoCao.Text = dr[0]["TD_BC"].ToString();
                    }
                    else {
                        LoadNull();
                    }
                }
            }
            catch {
                LoadNull();
            }

        }
        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = Convert.ToDateTime(grv.GetFocusedRowCellValue("NGAY_TKL").ToString()).ToShortDateString();
                txTieuDeBaoCao.Text = grv.GetFocusedRowCellValue("TD_BC").ToString();
                cbNoiDung.EditValue = grv.GetFocusedRowCellValue("ID_NDTKL").ToString();
                txTienQuyDinh.Text = grv.GetFocusedRowCellValue("TIEN_QUY_DINH").ToString();
                txSoThang.Text = grv.GetFocusedRowCellValue("SO_THANG_TINH").ToString();
                txSoTien.Text = grv.GetFocusedRowCellValue("SO_TIEN").ToString();
                txSoToiThieu.Text = grv.GetFocusedRowCellValue("SO_TIEN_GH").ToString();
            }
            catch {
                LoadNull();
            }
            cboThang.ClosePopup();
        }

        private void LoadNull()
        {
            try
            {
                if (cboThang.Text == "") cboThang.Text = DateTime.Now.ToShortDateString();
                txTienQuyDinh.Text = "0";
                txSoThang.Text = "0";
                txSoTien.Text = "0";
                txSoToiThieu.Text = "0";
                txTieuDeBaoCao.Text = "";
            }
            catch (Exception ex)
            {
                cboThang.Text = "";txTienQuyDinh.Text = "0";txSoThang.Text = "0";txSoTien.Text = "0";txSoToiThieu.Text = "0";txTieuDeBaoCao.Text = "";
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void ucThuongKhacLuong_Load(object sender, EventArgs e)
        {

            txTienQuyDinh.Text = "0";
            txSoThang.Text = "0";
            txSoTien.Text = "0";
            txSoToiThieu.Text = "0";

            txTienQuyDinh.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txTienQuyDinh.Properties.Mask.EditMask = "N0";
            txTienQuyDinh.Properties.Mask.UseMaskAsDisplayFormat = true;

            txSoThang.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txSoThang.Properties.Mask.EditMask = "N0";
            txSoThang.Properties.Mask.UseMaskAsDisplayFormat = true;

            txSoTien.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txSoTien.Properties.Mask.EditMask = "N0";
            txSoTien.Properties.Mask.UseMaskAsDisplayFormat = true;

            txSoToiThieu.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txSoToiThieu.Properties.Mask.EditMask = "N0";
            txSoToiThieu.Properties.Mask.UseMaskAsDisplayFormat = true;


            Commons.Modules.sPS = "0Load";
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spThuongKhacLuong", "01/01/1900", "01/01/1900", -1, -1, -1, Commons.Modules.UserName, Commons.Modules.TypeLanguage, "", "CboNoiDung"));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cbNoiDung, dt, "ID_NDTKL", "TEN_THUONG", "TEN_THUONG");

            LoadThang(Convert.ToDateTime("01/01/1900"));
            Commons.Modules.ObjSystems.LoadCboDonVi(cbDonVi);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cbDonVi, cbXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cbDonVi, cbXiNghiep, cbDichVu);
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cbNoiDung, Commons.Modules.ObjSystems.DataNoiDungThuongKhacLuong(true), "ID_NDTKL", "TEN_THUONG", "TEN_THUONG");
            Commons.Modules.sPS = "";
            LoadLuoi(0, -1);
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);

            //   txtTongTien.Properties.Mask.EditMask = "n" + Commons.Modules.iSoLeTT.ToString();
        }

        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            LoadLuoi(0, -1);
        }
        //0 - Load Grid, 1Them,2 CapNhap So tien
        private void LoadLuoi(int iThemSua, int id)
        {
            if (Commons.Modules.sPS == "0Load") return;
            DataTable dt = new DataTable();
            DateTime dThang = Convert.ToDateTime("01/01/1900");
            try
            {
                dThang = Convert.ToDateTime(cboThang.Text.ToString());
            }
            catch
            {

            }
            //if (!bThemSua)
            if (iThemSua == 0) //0 - Load Grid
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr
                    , "spThuongKhacLuong"
                    , dThang.Date
                    , "01/01/1900"
                    , (cbDonVi.EditValue.ToString() == "" ? -1 : cbDonVi.EditValue)
                    , (cbXiNghiep.EditValue.ToString() == "" ? -1 : cbXiNghiep.EditValue)
                    , (cbDichVu.EditValue.ToString() == "" ? -1 : cbDichVu.EditValue)
                    , Commons.Modules.UserName
                    , Commons.Modules.TypeLanguage
                    , ""
                    , "Grd"));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CN"] };

                if (grdChung.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, false, true, true, true, true, this.Name);
                }
                else
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, false, false, true, true, false, this.Name);


            }
            if (iThemSua == 1) //1 - Load Grid Them Sua
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr
                    , "spThuongKhacLuong"
                    , dThang
                    , "01/01/1900"
                    , (cbDonVi.EditValue.ToString() == "" ? -1 : cbDonVi.EditValue)
                    , (cbXiNghiep.EditValue.ToString() == "" ? -1 : cbXiNghiep.EditValue)
                    , (cbDichVu.EditValue.ToString() == "" ? -1 : cbDichVu.EditValue)
                    , Commons.Modules.UserName, Commons.Modules.TypeLanguage
                    , ""
                    , "Add"));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CN"] };


                if (grdChung.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, true, true, true, true, true, this.Name);
              
                }
                else
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, true, false, true, true, false, this.Name);

            }
            if (iThemSua == 2) //2 - Load Grid Cap nhap so tien
            {

                string sBT = "TKL" + Commons.Modules.UserName;
                DataTable tb = new DataTable();
                tb = (DataTable)grdChung.DataSource;
                try
                {
                    dThang = dThang.Date;
                  }
                catch
                {
                }
                for (int i = 0; i < tb.Rows.Count; i++)
                {

                    tb.Rows[i]["ID_NDTKL"] = (cbNoiDung.EditValue ==null || cbNoiDung.EditValue.ToString()=="" || cbNoiDung.EditValue.ToString()=="-1")? DBNull.Value: cbNoiDung.EditValue;
                    tb.Rows[i]["NGAY_TKL"] = new DateTime(dThang.Year, dThang.Month, 1);

                    tb.Rows[i]["TIEN_QUY_DINH"] = txTienQuyDinh.EditValue;
                    tb.Rows[i]["SO_THANG_TINH"] = txSoThang.EditValue;
                    tb.Rows[i]["SO_TIEN"] = txSoTien.EditValue;
                    tb.Rows[i]["SO_TIEN_GH"] = txSoToiThieu.EditValue;
                    tb.Rows[i]["TD_BC"] = txTieuDeBaoCao.Text;
                    if (raCacTinh.SelectedIndex == -1 || raCacTinh.SelectedIndex == 0)
                    {
                        tb.Rows[i]["SO_TIEN_NHAN"] = txTienQuyDinh.EditValue;
                    }
                    else
                    {
                        DateTime ngayVaoLam = (DateTime)tb.Rows[i]["NGAY_VAO_LAM"];
                        float sothanglam = (((DateTime.Today.Year - ngayVaoLam.Year) * 12) + DateTime.Now.Month - ngayVaoLam.Month);
                        float sothangduoctinh = float.Parse(txSoThang.EditValue.ToString());

                        if (sothanglam >= sothangduoctinh)
                        {
                            tb.Rows[i]["SO_TIEN_NHAN"] = float.Parse(txSoTien.EditValue.ToString());
                        }
                        else
                        {
                            tb.Rows[i]["SO_TIEN_NHAN"] = Math.Round((float.Parse(txSoTien.EditValue.ToString()) / sothangduoctinh * sothanglam)/1000,0) * 1000;
                        }
                    }

                    if (txSoToiThieu.EditValue != null)
                    {
                        float tt = float.Parse(tb.Rows[i]["SO_TIEN_NHAN"].ToString());

                        if (tt < float.Parse(txSoToiThieu.EditValue.ToString()))
                            tb.Rows[i]["SO_TIEN_NHAN"] = txSoToiThieu.EditValue;
                        else tb.Rows[i]["SO_TIEN_NHAN"] = float.Parse(tt.ToString());
                    }

                }
            }
            if (grvChung.Columns["ID_TKL"] != null)
                grvChung.Columns["ID_TKL"].Visible = false;
            if (grvChung.Columns["ID_NDTKL"] != null)
                grvChung.Columns["ID_NDTKL"].Visible = false;
            if (grvChung.Columns["NGAY_TKL"] != null)
                grvChung.Columns["NGAY_TKL"].Visible = false;
            if (grvChung.Columns["TIEN_QUY_DINH"] != null)
                grvChung.Columns["TIEN_QUY_DINH"].Visible = false;
            if (grvChung.Columns["SO_THANG_TINH"] != null)
                grvChung.Columns["SO_THANG_TINH"].Visible = false;
            if (grvChung.Columns["SO_TIEN"] != null)
                grvChung.Columns["SO_TIEN"].Visible = false;
            if (grvChung.Columns["SO_TIEN_GH"] != null)
                grvChung.Columns["SO_TIEN_GH"].Visible = false;
            if (grvChung.Columns["TD_BC"] != null)
                grvChung.Columns["TD_BC"].Visible = false;
            if (grvChung.Columns["ID_CN"] != null)
            {
                grvChung.Columns["ID_CN"].Visible = false;
            }
            grvChung.Columns["SO_TIEN"].DisplayFormat.FormatType = FormatType.Numeric;
            grvChung.Columns["SO_TIEN"].DisplayFormat.FormatString = "N0";
            grvChung.Columns["SO_TIEN_NHAN"].DisplayFormat.FormatType = FormatType.Numeric;
            grvChung.Columns["SO_TIEN_NHAN"].DisplayFormat.FormatString = "N0";






            grvChung.RefreshData();
            if (id != -1)
            {
                try
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(id));
                    grvChung.FocusedRowHandle = grvChung.GetRowHandle(index);
                }
                catch { }
            }
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            try
            {
                switch (btn.Tag.ToString())
                {
                    case "themsua":
                        {
                            enableButon(false);
                            LoadLuoi(1, -1);
                            break;

                        }
                    case "khongluu":
                        {
                            grvChung.RefreshData();
                            txTienQuyDinh.EditValue = 0;
                            txSoThang.EditValue = 0;
                            txSoTien.EditValue = 0;
                            txSoToiThieu.EditValue = 0;
                            enableButon(true);
                            LoadLuoi(0, -1);
                            break;
                        }
                    case "luu":
                        {

                            grvChung.PostEditor();
                            grvChung.UpdateCurrentRow();
                            DateTime dThang = Convert.ToDateTime(cboThang.Text);
                            int idCN = -1;
                            try {
                                idCN = int.Parse(grvChung.GetFocusedRowCellValue("ID_CN").ToString());
                            } catch { }
                            if (!SaveData()) return;
                            enableButon(true);
                            Commons.Modules.sPS = "0Load";
                            LoadThang(dThang);

                            Commons.Modules.sPS = "";
                            LoadLuoi(0, idCN);
                            break;
                        }
                    case "thoat":
                        {
                            Commons.Modules.ObjSystems.GotoHome(this);
                            break;
                        }
                    case "xoa":
                        {
                            DeleteData(-1);
                            break;
                        }
                    case "Print":
                        {
                            PrintData();
                            break;
                        }
                    case "intongquat":
                        {
                            PrintDataTongQuat();
                            break;
                        }
                    case "CapNhat":
                        {
                            LoadLuoi(2, -1);
                            break;
                        }
                    case "XepLoai":
                        {
                            XtraForm frm = new XtraForm();
                            Vs.HRM.ucXepLoaiKhenThuong uc = new Vs.HRM.ucXepLoaiKhenThuong();
                            frm.Controls.Clear();
                            frm.Controls.Add(uc);
                            frm.Size = new Size((this.Size.Width / 2), (this.Size.Height / 2));
                            frm.StartPosition = FormStartPosition.CenterParent;
                            uc.Dock = DockStyle.Fill;
                            frm.ShowDialog();
                            break;
                        }

                    default:
                        break;
                }
            }
            catch (Exception Ex)
            {
                XtraMessageBox.Show(Ex.Message.ToLower());

            }
        }
        private void enableButon(bool visible)
        {
            try
            {
                windowsUIButton.Buttons[0].Properties.Visible = !visible;
                windowsUIButton.Buttons[1].Properties.Visible = !visible;
                windowsUIButton.Buttons[2].Properties.Visible = visible;
                windowsUIButton.Buttons[3].Properties.Visible = visible;
                windowsUIButton.Buttons[4].Properties.Visible = visible;
                windowsUIButton.Buttons[5].Properties.Visible = visible;
                windowsUIButton.Buttons[6].Properties.Visible = visible;
                windowsUIButton.Buttons[7].Properties.Visible = visible;
                windowsUIButton.Buttons[8].Properties.Visible = !visible;
                windowsUIButton.Buttons[9].Properties.Visible = !visible;
                windowsUIButton.Buttons[10].Properties.Visible = visible;
                cboThang.Properties.ReadOnly = !visible;
                cbXiNghiep.Properties.ReadOnly = !visible;
                cbDonVi.Properties.ReadOnly = !visible;
                cbDichVu.Properties.ReadOnly = !visible;
                txTieuDeBaoCao.Properties.ReadOnly = visible;
            }
            catch { }
        }
        private bool SaveData()
        {
            DateTime dNgay = Convert.ToDateTime("01/01/1900");
            try
            {
                dNgay = Convert.ToDateTime(cboThang.Text.ToString());
            }
            catch { }

            try
            {
                string sBT = "TKL" + Commons.Modules.UserName;
                DataTable tb = new DataTable();
                tb = (DataTable)grdChung.DataSource;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(
                    Commons.IConnections.CNStr,
                    sBT,
                      tb,
                    "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spThuongKhacLuong", dNgay, "01/01/1900", -1, -1, -1, txTieuDeBaoCao.Text, Commons.Modules.TypeLanguage, sBT, "Save");
                Commons.Modules.ObjSystems.XoaTable(sBT);
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                return false;
            }
            
        }

        private void cboDV_Click(object sender, EventArgs e)
        {

        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cbDonVi, cbXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cbDonVi, cbXiNghiep, cbDichVu);
            cbDichVu.EditValue = -1;
            Commons.Modules.sPS = "";
            LoadLuoi(0, -1);
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cbDonVi, cbXiNghiep, cbDichVu);
            cbDichVu.EditValue = -1;
            Commons.Modules.sPS = "";
            LoadLuoi(0, -1);
        }

      


        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void grdChung_ProcessGridKey(object sender, System.Windows.Forms.KeyEventArgs e)
        {

            if (windowsUIButton.Buttons[8].Properties.Visible) return;
            if (e.KeyCode != Keys.Delete) return;

            int iIdCN = -1;
            try { iIdCN = int.Parse(grvChung.GetFocusedRowCellValue("ID_CN").ToString()); } catch { }
            if (iIdCN == -1)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgKhongCoDuLieuXoa"), "");
                return;
            }
            DeleteData(iIdCN);
        }
        private void DeleteData(int iIdCN)
        {
            if (grvChung.RowCount == 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgKhongCoDuLieuXoa"), "");
                return;
            }

            if (iIdCN == -1)
            {
                if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoaAll) == DialogResult.No) return;
            }
            else
            {
                if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            }

            DateTime dThang = Convert.ToDateTime("01/01/1900");
            try
            {
                dThang = Convert.ToDateTime(cboThang.Text.ToString());
            }
            catch { }
            //xóa
            try
            {

                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spThuongKhacLuong", dThang, "01/01/1900", iIdCN, -1, -1, Commons.Modules.UserName, Commons.Modules.TypeLanguage, "", "Delete");
                Commons.Modules.sPS = "0Load";
                if (iIdCN == -1)
                    LoadThang(Convert.ToDateTime("01/01/1900"));
                else
                    LoadThang(dThang);
                Commons.Modules.sPS = "";
                LoadLuoi(0, -1);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDelDangSuDung") + "\n" + ex.Message.ToString());
            }

        }

        private void PrintData()
        {
            if (grvChung.RowCount == 0 || grdChung.DataSource == null)
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuIn);
                return;
            }
            frmViewReport frm = new frmViewReport();
            DataTable dt = new DataTable();
            DateTime dThang = Convert.ToDateTime("01/01/1900");
            try
            {
                dThang = Convert.ToDateTime(cboThang.Text.ToString());
            }
            catch { }

            System.Data.SqlClient.SqlConnection conn;

            frm.rpt = new rptThuongKhacLuong(DateTime.Today, dThang);

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spThuongKhacLuong", conn);

                cmd.Parameters.Add("@NgayTKL", SqlDbType.Date).Value = dThang;
                cmd.Parameters.Add("@DenThang", SqlDbType.Date).Value = dThang;
                cmd.Parameters.Add("@DVi", SqlDbType.BigInt).Value = (cbDonVi.EditValue.ToString() == "" ? -1 : cbDonVi.EditValue);
                cmd.Parameters.Add("@XNghiep", SqlDbType.BigInt).Value = (cbXiNghiep.EditValue.ToString() == "" ? -1 : cbXiNghiep.EditValue);
                cmd.Parameters.Add("@To", SqlDbType.BigInt).Value = (cbDichVu.EditValue.ToString() == "" ? -1 : cbDichVu.EditValue);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar, 50).Value = "rptThuongKhacLuong";
                cmd.Parameters.Add("@Loai", SqlDbType.NVarChar, 50).Value = "Print";


                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DA_TA";
                frm.AddDataSource(dt);

            }
            catch (Exception ex)
            {

            }

            frm.ShowDialog();
        }
        private void PrintDataTongQuat()
        {
            frmViewReport frm = new frmViewReport();

            try
            {
                System.Data.SqlClient.SqlConnection conn;
                DataTable dt = new DataTable();

                DateTime dNgay = Convert.ToDateTime("01/01/1900");
                try
                {
                    dNgay = Convert.ToDateTime(cboThang.Text.ToString());
                }
                catch { }
                frm.rpt = new rptTienThuongKhacLuongTH(DateTime.Now, dNgay);
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTienThuongKhacLuongTH", conn);

                cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = dNgay;
                cmd.Parameters.Add("@DVi", SqlDbType.BigInt).Value = -1;
                cmd.Parameters.Add("@XNghiep", SqlDbType.BigInt).Value = -1;
                cmd.Parameters.Add("@To", SqlDbType.BigInt).Value = -1;
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;

                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.TableName = "DA_TA";
                frm.AddDataSource(dt);

                frm.ShowDialog();

            }
            catch (Exception ex)
            {

            }

        }

        private void raCacTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (raCacTinh.SelectedIndex == -1 || raCacTinh.SelectedIndex == 0)
            {
                txTienQuyDinh.Enabled = true;
                txTienQuyDinh.Focus();
                txSoThang.Enabled = false;
                txSoThang.EditValue = 0;
                txSoTien.Enabled = false;
                txSoTien.EditValue = 0;
            }
            else 
            {
                txTienQuyDinh.Enabled = false;
                txTienQuyDinh.EditValue = 0;
                txSoThang.Enabled = true;
                txSoThang.Focus();
                txSoTien.Enabled = true;
            }
        }

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboThang.Text = calThang.DateTime.Date.ToShortDateString();
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TKL" + "='" + cboThang.Text + "'", "NGAY_TKL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                    cboThang.Text = Convert.ToDateTime(dr[0]["NGAY_TKL"].ToString()).ToShortDateString();

                    txTienQuyDinh.Text = dr[0]["SO_TIEN_NHAN"].ToString();
                    txSoThang.Text = dr[0]["SO_THANG_TINH"].ToString();
                    txSoTien.Text = dr[0]["SO_TIEN"].ToString();
                    txSoToiThieu.Text = dr[0]["SO_TIEN_GH"].ToString();
                    txTieuDeBaoCao.Text = dr[0]["TD_BC"].ToString();

                }
                else { LoadNull(); }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                cboThang.Text = DateTime.Now.ToShortDateString();
            }
            cboThang.ClosePopup();
        }

    }
}
