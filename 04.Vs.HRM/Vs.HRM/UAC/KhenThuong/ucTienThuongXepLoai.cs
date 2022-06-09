using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vs.Report;

namespace Vs.HRM
{
    public partial class ucTienThuongXepLoai : DevExpress.XtraEditors.XtraUserControl
    {
        public ucTienThuongXepLoai()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new System.Collections.Generic.List<DevExpress.XtraLayout.LayoutControlGroup>() { Root }, windowsUIButton);

        }

        private void LoadThang(DateTime dThang)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spTienThuongXepLoai", DateTime.Today, "01/01/1900", -1, -1, -1, Commons.Modules.UserName, Commons.Modules.TypeLanguage, "", "Cbo"));

            if (grdThang.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dt, false, true, true, true, true, this.Name);
                grvThang.Columns["TONG_TTXL"].Visible = false;
                grvThang.Columns["TU_THANG"].Visible = false;
                grvThang.Columns["DEN_THANG"].Visible = false;
                grvThang.Columns["TD_BC"].Visible = false;
            }
            else
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dt, false, false, true, false, false, this.Name);

            try
            {

                if (dThang == Convert.ToDateTime("01/01/1900"))
                {
                    if (dt is null) LoadNull();
                    else
                    {
                        cboThang.Text = Convert.ToDateTime(dt.Rows[0]["NGAY_TTXL"].ToString()).ToShortDateString();
                        txtTongTien.Text = dt.Rows[0]["TONG_TTXL"].ToString();
                        dtTuThang.EditValue = Convert.ToDateTime(dt.Rows[0]["TU_THANG"]).ToString("MM/yyyy");
                        dtDenThang.EditValue = Convert.ToDateTime(dt.Rows[0]["DEN_THANG"]).ToString("MM/yyyy");
                        txtTDBC.Text = dt.Rows[0]["TD_BC"].ToString();
                    }
                }
                else
                {
                    cboThang.Text = dThang.Date.ToShortDateString();

                    DataRow[] dr;
                    dr = dt.Select("NGAY_TTXL" + "='" + cboThang.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                    if (dr.Count() == 1)
                    {
                        cboThang.Text = Convert.ToDateTime(dr[0]["NGAY_TTXL"].ToString()).ToShortDateString();
                        txtTongTien.Text = dr[0]["TONG_TTXL"].ToString();
                        dtTuThang.EditValue = Convert.ToDateTime(dr[0]["TU_THANG"]).ToString("MM/yyyy");
                        dtDenThang.EditValue = Convert.ToDateTime(dr[0]["DEN_THANG"]).ToString("MM/yyyy");
                        txtTDBC.Text = dr[0]["TD_BC"].ToString();
                    }
                    else { LoadNull(); }
                }
            }
            catch { LoadNull(); }



        }
        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = Convert.ToDateTime(grv.GetFocusedRowCellValue("NGAY_TTXL").ToString()).ToShortDateString();
                txtTongTien.Text = grv.GetFocusedRowCellValue("TONG_TTXL").ToString();
                dtTuThang.EditValue = Convert.ToDateTime(grv.GetFocusedRowCellValue("TU_THANG")).ToString("MM/yyyy");
                dtDenThang.EditValue = Convert.ToDateTime(grv.GetFocusedRowCellValue("DEN_THANG")).ToString("MM/yyyy");
                txtTDBC.Text = grv.GetFocusedRowCellValue("TD_BC").ToString();
            }
            catch { LoadNull(); }
            cboThang.ClosePopup();
        }

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboThang.Text = calThang.DateTime.Date.ToShortDateString();
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + cboThang.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                    cboThang.Text = Convert.ToDateTime(dr[0]["NGAY_TTXL"].ToString()).ToShortDateString();
                    txtTongTien.Text = dr[0]["TONG_TTXL"].ToString();
                    dtTuThang.EditValue = Convert.ToDateTime(dr[0]["TU_THANG"]).ToString("MM/yyyy");
                    dtDenThang.EditValue = Convert.ToDateTime(dr[0]["DEN_THANG"]).ToString("MM/yyyy");
                    txtTDBC.Text = dr[0]["TD_BC"].ToString();
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
        private void LoadNull()
        {
            try
            {
                if (cboThang.Text == "") cboThang.Text = DateTime.Now.ToShortDateString();
                txtTongTien.Text = "0";
                dtTuThang.EditValue = Convert.ToDateTime(cboThang.Text).ToString("MM/yyyy");
                dtDenThang.EditValue = Convert.ToDateTime(cboThang.Text).ToString("MM/yyyy");
                txtTDBC.Text = "";
            }
            catch (Exception ex)
            {
                cboThang.Text = ""; txtTongTien.Text = ""; dtTuThang.EditValue = ""; dtDenThang.EditValue = ""; txtTDBC.Text = "";
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void ucTienThuongXL_Load(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";
            LoadThang(Convert.ToDateTime("01/01/1900"));
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            Commons.Modules.sPS = "";
            LoadLuoi(0, -1);
            enableButon(true);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);

            txtTongTien.Properties.Mask.EditMask = "n" + Commons.Modules.iSoLeTT.ToString();
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
            catch { }

            //if (!bThemSua)
            if (iThemSua == 0) //0 - Load Grid
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spTienThuongXepLoai", dThang, "01/01/1900", (cboDV.EditValue.ToString() == "" ? -1 : cboDV.EditValue), (cboXN.EditValue.ToString() == "" ? -1 : cboXN.EditValue), (cboTo.EditValue.ToString() == "" ? -1 : cboTo.EditValue), Commons.Modules.UserName, Commons.Modules.TypeLanguage, "", "Grd"));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CN"] };

                if (grdChung.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, false, true, true, false, true, this.Name);
                    grvChung.Columns["TIEN_THUONG"].Width = 50;
                    grvChung.Columns["NGAY_VAO_LAM"].Width = 80;
                    grvChung.Columns["MS_CN"].Width = 50;
                    grvChung.Columns["TIEN_THUONG"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    grvChung.Columns["TIEN_THUONG"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                }
                else
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, false, false, true, false, false, this.Name);


            }
            if (iThemSua == 1) //1 - Load Grid Them Sua
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spTienThuongXepLoai", dThang, "01/01/1900", (cboDV.EditValue.ToString() == "" ? -1 : cboDV.EditValue), (cboXN.EditValue.ToString() == "" ? -1 : cboXN.EditValue), (cboTo.EditValue.ToString() == "" ? -1 : cboTo.EditValue), Commons.Modules.UserName, Commons.Modules.TypeLanguage, "", "Add"));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CN"] };


                if (grdChung.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, true, true, true, true, true, this.Name);
                    grvChung.Columns["TIEN_THUONG"].Width = 50;
                    grvChung.Columns["MS_CN"].Width = 50;
                    grvChung.Columns["NGAY_VAO_LAM"].Width = 80;
                    grvChung.Columns["TIEN_THUONG"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    grvChung.Columns["TIEN_THUONG"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                }
                else
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, true, false, true, false, false, this.Name);

            }
            if (iThemSua == 2) //2 - Load Grid Cap nhap so tien
            {
                try
                { dThang = dtTuThang.DateTime.Date; 

                DateTime dDThang = Convert.ToDateTime("01/01/1900");
                try { dDThang = dtDenThang.DateTime.Date; } catch { }
                float fTong = 0;
                try { float.TryParse(txtTongTien.Text, out fTong); } catch { }

                string sBT = "TTXLTien" + Commons.Modules.UserName;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, (DataTable)grdChung.DataSource, "");
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spTienThuongXepLoai", dThang, dDThang, fTong, -1, -1, Commons.Modules.UserName, Commons.Modules.TypeLanguage, sBT, "TienThuong"));

                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CN"] };


                if (grdChung.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, true, true, true, true, true, this.Name);
                    grvChung.Columns["TIEN_THUONG"].Width = 50;
                    grvChung.Columns["MS_CN"].Width = 50;
                    grvChung.Columns["NGAY_VAO_LAM"].Width = 80;
                    grvChung.Columns["TIEN_THUONG"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    grvChung.Columns["TIEN_THUONG"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                }
                else
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChung, grvChung, dt, true, false, true, false, false, this.Name);
                }
                catch(Exception ex) { }
            }

            grvChung.Columns["ID_CN"].Visible = false;
            grvChung.Columns["NGAY_TTXL"].Visible = false;
            grvChung.Columns["TONG_TTXL"].Visible = false;
            grvChung.Columns["TU_THANG"].Visible = false;
            grvChung.Columns["DEN_THANG"].Visible = false;
            grvChung.Columns["TD_BC"].Visible = false;
            grvChung.Columns["MS_CN"].OptionsColumn.ReadOnly = true;
            grvChung.Columns["HO_TEN"].OptionsColumn.ReadOnly = true;

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
                            try { idCN = int.Parse(grvChung.GetFocusedRowCellValue("ID_CN").ToString()); } catch { }
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
                    case "in":
                        {
                            PrintData();
                            break;
                        }
                    case "intongquat":
                        {
                            int idLUONG = Convert.ToInt32(grvChung.GetFocusedRowCellValue("ID_LCB"));
                            frmInNhanTienThuongXepLoai InLuongCN = new frmInNhanTienThuongXepLoai(dtDenThang.DateTime);
                           InLuongCN.ShowDialog();
                            break;
                        }
                    case "CapNhap":
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
                windowsUIButton.Buttons[0].Properties.Visible = visible;
                windowsUIButton.Buttons[1].Properties.Visible = !visible;
                windowsUIButton.Buttons[2].Properties.Visible = !visible;
                windowsUIButton.Buttons[3].Properties.Visible = visible;
                windowsUIButton.Buttons[4].Properties.Visible = visible;
                windowsUIButton.Buttons[5].Properties.Visible = visible;
                windowsUIButton.Buttons[6].Properties.Visible = visible;
                windowsUIButton.Buttons[7].Properties.Visible = visible;
                windowsUIButton.Buttons[8].Properties.Visible = visible;
                windowsUIButton.Buttons[9].Properties.Visible = visible;

                windowsUIButton.Buttons[10].Properties.Visible = !visible;
                windowsUIButton.Buttons[11].Properties.Visible = !visible;

                windowsUIButton.Buttons[12].Properties.Visible = visible;
                windowsUIButton.Buttons[13].Properties.Visible = visible;





                cboThang.Properties.ReadOnly = !visible;
                cboXN.Properties.ReadOnly = !visible;
                cboDV.Properties.ReadOnly = !visible;
                cboTo.Properties.ReadOnly = !visible;

                txtTongTien.Properties.ReadOnly = visible;
                dtTuThang.Properties.ReadOnly = visible;
                dtDenThang.Properties.ReadOnly = visible;
                txtTDBC.Properties.ReadOnly = visible;
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
                string sBT = "TTXL" + Commons.Modules.UserName;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, (DataTable)grdChung.DataSource, "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spTienThuongXepLoai", dNgay, "01/01/1900", -1, -1, -1, txtTDBC.Text, Commons.Modules.TypeLanguage, sBT, "Save");
                Commons.Modules.ObjSystems.XoaTable(sBT);
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                return false;
            }
        }

        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            cboTo.EditValue = -1;
            Commons.Modules.sPS = "";
            LoadLuoi(0, -1);
        }

        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            cboTo.EditValue = -1;
            Commons.Modules.sPS = "";
            LoadLuoi(0, -1);
        }

        private void grdChung_ProcessGridKey(object sender, KeyEventArgs e)
        {

            if (windowsUIButton.Buttons[8].Properties.Visible) return;
            if (e.KeyCode != Keys.Delete) return;

            int iIdCN = -1;
            try { iIdCN = int.Parse(grvChung.GetFocusedRowCellValue("ID_CN").ToString()); } catch { }
            if (iIdCN == -1)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgKhongCoDuLieuXoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DeleteData(iIdCN);
        }

        private void DeleteData(int iIdCN)
        {
            if (grvChung.RowCount == 0)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgKhongCoDuLieuXoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spTienThuongXepLoai", dThang, "01/01/1900", iIdCN, -1, -1, Commons.Modules.UserName, Commons.Modules.TypeLanguage, "", "Delete");
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
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            frm.rpt = new rptTienThuongXepLoai(dThang);

            try
            {
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTienThuongXepLoai", conn);

                cmd.Parameters.Add("@NgayTTXL", SqlDbType.Date).Value = dThang;
                cmd.Parameters.Add("@DenThang", SqlDbType.Date).Value = dThang;
                cmd.Parameters.Add("@DVi", SqlDbType.BigInt).Value = (cboDV.EditValue.ToString() == "" ? -1 : cboDV.EditValue);
                cmd.Parameters.Add("@XNghiep", SqlDbType.BigInt).Value = (cboXN.EditValue.ToString() == "" ? -1 : cboXN.EditValue);
                cmd.Parameters.Add("@To", SqlDbType.BigInt).Value = (cboTo.EditValue.ToString() == "" ? -1 : cboTo.EditValue);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar, 50).Value = "rptTienThuongXepLoai";
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
    }
}