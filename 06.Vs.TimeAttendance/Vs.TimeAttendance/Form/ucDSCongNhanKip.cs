using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using System.Xml.Linq;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraLayout;

namespace Vs.TimeAttendance
{
    public partial class ucDSCongNhanKip : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;
        public static ucDSCongNhanKip _instance;
        public static ucDSCongNhanKip Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDSCongNhanKip();
                return _instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ucDSCongNhanKip()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucDSCongNhanKip_Load(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";
            EnableButon(true);
            LoadNgay(Convert.ToDateTime("01/01/1900"));
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);

            LoadGrdDSCNKip(isAdd);
            Commons.Modules.sPS = "";
        }

        /// <summary>
        /// load Grid
        /// </summary>
        private void LoadGrdDSCNKip(bool isAdd)
        {
            if (isAdd)
            {
                ThemSuaCongNhanKip();
                return;
            }
            try
            {
                Commons.Modules.sPS = "0Load";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCongNhanKip", cboNgay.EditValue, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvCNKip, dt, false, false, false, false, true, this.Name);

                DataTable dID_KIP = new DataTable();
                dID_KIP.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetNhomKIP", cboNgay.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.AddCombXtra("ID_KIP", "TEN_KIP", grvCNKip, dID_KIP, false);

                FormatGridView();
                Commons.Modules.sPS = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        #region Combobox Changed
        /// <summary>
        /// cbo Don vi Change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadGrdDSCNKip(isAdd);
            Commons.Modules.sPS = "";
        }

        /// <summary>
        /// cbo Xi nghiep Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadGrdDSCNKip(isAdd);
            Commons.Modules.sPS = "";
        }

        /// <summary>
        /// cbo To Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGrdDSCNKip(isAdd);
            Commons.Modules.sPS = "";
        }

        /// <summary>
        /// combo date change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGrdDSCNKip(isAdd);
            Commons.Modules.sPS = "";
        }
        #endregion

        /// <summary>
        /// Format Grid
        /// </summary>
        private void FormatGridView()
        {
            grvCNKip.Columns["ID_CN"].OptionsColumn.ReadOnly = true;
            grvCNKip.Columns["HO_TEN"].OptionsColumn.ReadOnly = true;
            grvCNKip.Columns["TEN_TO"].OptionsColumn.ReadOnly = true;

            grvCNKip.Columns["ID_CN"].Width = 100;
            grvCNKip.Columns["HO_TEN"].Width = 300;
            grvCNKip.Columns["ID_KIP"].Width = 200;
            grvCNKip.Columns["TEN_TO"].Width = 200;
        }

        /// <summary>
        /// windows button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        EnableButon(false);
                        ThemSuaCongNhanKip();
                        break;
                    }
                case "xoa":
                    {
                        XoaCNKip();
                        break;
                    }
                case "ghi":
                    {
                        Validate();
                        if (grvCNKip.HasColumnErrors) return;
                        if (Savedata() == false)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                        }
                        EnableButon(true);
                        LoadGrdDSCNKip(isAdd);
                        break;
                    }
                case "khongghi":
                    {
                        EnableButon(true);
                        LoadGrdDSCNKip(isAdd);
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                case "capnhatnhom":
                    {
                        Validate();
                        if (grvCNKip.HasColumnErrors) return;
                        CapNhatNhom();
                        break;
                    }
                case "xoatrangnhom":
                    {
                        Validate();
                        if (grvCNKip.HasColumnErrors) return;
                        XoaTrangNhom();
                        break;
                    }
            }
        }

        #region Xu ly button

        /// <summary>
        /// Them sua
        /// </summary>
        private void ThemSuaCongNhanKip()
        {
            try
            {
                Commons.Modules.sPS = "0Load";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListEditCNKip", cboNgay.EditValue, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvCNKip, dt, true, false, false, false, true, this.Name);

                DataTable dID_KIP = new DataTable();
                dID_KIP.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetNhomKIP", cboNgay.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.AddCombXtra("ID_KIP", "TEN_KIP", grvCNKip, dID_KIP, false);

                FormatGridView();
                Commons.Modules.sPS = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        /// <summary>
        /// btn cap nhat nhom
        /// </summary>
        /// <returns></returns>
        private void CapNhatNhom()
        {
            int idKip, temp;
            Int32.TryParse(grvCNKip.GetFocusedRowCellValue("ID_KIP").ToString(),out idKip);
            if (idKip == 0) return;
            for (int i = 0; i < grvCNKip.DataRowCount; i++)
            {
                DataRow row = grvCNKip.GetDataRow(i);
                if (!Int32.TryParse(row["ID_KIP"].ToString(), out temp))
                {
                    grvCNKip.SetRowCellValue(i, "ID_KIP", idKip);
                }
            }
        }

        /// <summary>
        /// Xoa trang nhom 
        /// </summary>
        /// <returns></returns>
        private void XoaTrangNhom()
        {
            int idKip;
            int temp;
            Int32.TryParse(grvCNKip.GetFocusedRowCellValue("ID_KIP").ToString(), out idKip);
            if (idKip == 0) return;
            for (int i = 0; i < grvCNKip.DataRowCount; i++)
            {
                DataRow row = grvCNKip.GetDataRow(i);
                Int32.TryParse(row["ID_KIP"].ToString(), out temp);

                if (temp == idKip)
                {
                    grvCNKip.SetRowCellValue(i, "ID_KIP", null);
                }
            }
        }

        /// <summary>
        /// Xoa dong
        /// </summary>
        private void XoaCNKip()
        {
            if (grvCNKip.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                string sSql = "DELETE dbo.CONG_NHAN_KIP WHERE ID_CN = " + grvCNKip.GetFocusedRowCellValue("ID_CN") +
                                                        " AND ID_KIP = " + grvCNKip.GetFocusedRowCellValue("ID_KIP") +
                                                        " AND NGAY_AD = '" + Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd") + "'";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                grvCNKip.DeleteSelectedRows();
            }
            catch
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
            }
        }

        /// <summary>
        /// Save data
        /// </summary>
        /// <returns></returns>
        private bool Savedata()
        {
            string sTB = "CN_KIP_TMP" + Commons.Modules.UserName;
            string sSql = "";
            try
            {
                var test = cboNgay.EditValue;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grvCNKip), "");
                sSql = "DELETE CONG_NHAN_KIP WHERE CONVERT(NVARCHAR, NGAY_AD, 112) = '" + Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd") + "' AND ID_CN IN (SELECT ID_CN FROM "+ sTB + ") " +
                          "INSERT INTO CONG_NHAN_KIP(NGAY_AD, ID_CN, ID_KIP) SELECT '" + Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd") + "' ,ID_CN,ID_KIP FROM " + sTB + " WHERE ID_KIP IS NOT NULL " + "";


                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                Commons.Modules.ObjSystems.XoaTable(sTB);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
        }
        #endregion Xu ly button

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visible"></param>
        private void EnableButon(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = visible;
            btnALL.Buttons[3].Properties.Visible = visible;
            btnALL.Buttons[4].Properties.Visible = !visible;
            btnALL.Buttons[5].Properties.Visible = !visible;
            btnALL.Buttons[6].Properties.Visible = !visible;
            btnALL.Buttons[7].Properties.Visible = !visible;
            btnALL.Buttons[8].Properties.Visible = !visible;

            isAdd = !btnALL.Buttons[0].Properties.Visible;
        }

        private void grvData_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void grvData_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        #region Xu Ly Ngay
        /// <summary>
        /// Load Ngay
        /// </summary>
        /// <param name="dNgay"></param>
        private void LoadNgay(DateTime dNgay)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListNgayCNKip", Commons.Modules.UserName, Commons.Modules.TypeLanguage));

            if (grdNgay.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay, dt, false, true, true, true, true, this.Name);
            }
            else
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay, dt, false, false, true, false, false, this.Name);
            if(dt.Rows.Count > 0)
            {
                cboNgay.EditValue = dt.Rows[0]["NGAY_AD"];
            }
            else
            {
                cboNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        /// <summary>
        /// calNgay commit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calNgay_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboNgay.Text = calNgay.DateTime.Date.ToShortDateString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                cboNgay.Text = DateTime.Now.ToShortDateString();
            }
            cboNgay.ClosePopup();
        }

        /// <summary>
        /// load null cboNgay
        /// </summary>
        private void LoadNull()
        {
            try
            {
                if (cboNgay.Text == "") cboNgay.Text = DateTime.Now.ToShortDateString();
            }
            catch (Exception ex)
            {
                cboNgay.Text = "";
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        /// <summary>
        /// grid view combo ngay change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grvNgay_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboNgay.Text = Convert.ToDateTime(grv.GetFocusedRowCellValue("NGAY_AD").ToString()).ToShortDateString();
            }
            catch { LoadNull(); }
            cboNgay.ClosePopup();
        }
        #endregion



        /// <summary>
        /// count Nhan vien
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grvCNKip_RowCountChanged(object sender, EventArgs e)
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
    }
}