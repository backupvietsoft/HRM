﻿using System;
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
using DevExpress.Utils;

namespace Vs.Payroll
{
    public partial class ucLayChamCong : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;
        string kyHieuDV = "";
        public static ucLayChamCong _instance;
        private bool thangtruoc;

        public static ucLayChamCong Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucLayChamCong();
                return _instance;
            }
        }

        public ucLayChamCong()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);

        }

        private void ucLayChamCong_Load(object sender, EventArgs e)
        {
            try
            {
                kyHieuDV = Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString();
                Commons.Modules.sLoad = "0Load";
                LoadThang();
                Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
                Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
                LoadGrdGTGC();
                EnableButon(isAdd);
                Commons.Modules.sLoad = "";
                Commons.Modules.ObjSystems.SetPhanQuyen(btnALL);
            }
            catch { }
        }

        private void LoadGrdGTGC()
        {
            try
            {
                DataTable dt = new DataTable();

                string sPS = "spGetListLayChamCong_DM";
                switch(Commons.Modules.KyHieuDV)
                {
                    case "DM":
                        {
                            sPS = "spGetListLayChamCong_DM";
                            break;
                        }
                    default:
                        {
                            sPS = "spGetListLayChamCong";
                            break;
                        }
                }
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, sPS, Convert.ToDateTime(cboThang.EditValue),
                                            cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, false, false, true, this.Name);
                    grvData.Columns["ID_CN"].Visible = false;
                    grvData.Columns["ID_CV"].Visible = false;
                    grvData.Columns["ID_CTL"].Visible = false;
                    grvData.Columns["MS_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["HO_TEN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    grvData.Columns["TEN_TO"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
                else
                {
                    grdData.DataSource = dt;
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void LoadThang()
        {
            try
            {

                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.CHAM_CONG_THANG ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang1, dtthang, false, true, true, true, true, this.Name);
                grvThang1.Columns["M"].Visible = false;
                grvThang1.Columns["Y"].Visible = false;

                cboThang.Text = grvThang1.GetFocusedRowCellValue("THANG").ToString();
            }
            catch (Exception ex)
            {
                DateTime now = DateTime.Now;

                cboThang.Text = now.ToString("MM/yyyy");
            }
        }



        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {
                    case "themsua":
                        {
                            grvData.OptionsBehavior.Editable = true;
                            EnableButon(true);
                            break;
                        }
                    case "xoa":
                        {
                            XoaCheDoLV();
                            LoadGrdGTGC();
                            break;
                        }
                    case "ghi":
                        {
                            Validate();
                            if (grvData.HasColumnErrors) return;
                            Savedata();
                            LoadGrdGTGC();

                            EnableButon(false);
                            break;
                        }
                    case "khongghi":
                        {
                            grvData.OptionsBehavior.Editable = false;
                            LoadGrdGTGC();
                            EnableButon(false);
                            break;
                        }
                    case "laycong":
                        {
                            try
                            {
                                string sPS = "spGetChamCongThang_DM";
                                switch (Commons.Modules.KyHieuDV)
                                {
                                    case "DM":
                                        {
                                            sPS = "spGetChamCongThang_DM";
                                            break;
                                        }
                                    default:
                                        {
                                            sPS = "spGetChamCongThang";
                                            break;
                                        }
                                }
                                grdData.DataSource = null;
                                DateTime Tngay = Convert.ToDateTime(cboThang.EditValue);
                                DateTime Dngay = Convert.ToDateTime(cboThang.EditValue).AddMonths(1).AddDays(-1);
                                DataTable dt = new DataTable();
                                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, sPS, Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Tngay, Dngay));
                                Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, true, false, true, true, true, this.Name);
                                EnableButon(true);
                                dt.Columns["MS_CN"].ReadOnly = true;
                                dt.Columns["HO_TEN"].ReadOnly = true;
                                dt.Columns["TEN_TO"].ReadOnly = true;
                                dt.Columns["TEN_CV"].ReadOnly = true;
                                dt.Columns["CACH_TL"].ReadOnly = true;
                            }
                            catch { }


                            break;
                        }
                    case "thoat":
                        {
                            Commons.Modules.ObjSystems.GotoHome(this);
                            break;
                        }
                }
            }
            catch { }
        }

        private void EnableButon(bool visible)
        {
            if (Commons.Modules.ObjSystems.DataTinhTrangBangLuong(Convert.ToInt32(cboDonVi.EditValue), Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text)) == 2)
            {
                btnALL.Buttons[0].Properties.Visible = false;
                btnALL.Buttons[1].Properties.Visible = false;
                btnALL.Buttons[2].Properties.Visible = false;
                btnALL.Buttons[3].Properties.Visible = false;
                btnALL.Buttons[5].Properties.Visible = false;
                btnALL.Buttons[6].Properties.Visible = false;
            }
            else
            {
                btnALL.Buttons[0].Properties.Visible = !visible;
                btnALL.Buttons[1].Properties.Visible = !visible;
                btnALL.Buttons[2].Properties.Visible = !visible;
                btnALL.Buttons[3].Properties.Visible = !visible;
                btnALL.Buttons[4].Properties.Visible = !visible;
                btnALL.Buttons[5].Properties.Visible = visible;
                btnALL.Buttons[6].Properties.Visible = visible;
                cboTo.Enabled = !visible;
                cboThang.Enabled = !visible;
                cboDonVi.Enabled = !visible;
                cboXiNghiep.Enabled = !visible;
            }
        }

        private void XoaCheDoLV()
        {
            if (grvData.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            string sTB = "XoaChamCongThang_" + Commons.Modules.UserName;
            try
            {

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grdData), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spXoaLayChamCong", sTB, Convert.ToDateTime(cboThang.EditValue));
                Commons.Modules.ObjSystems.XoaTable(sTB);

            }
            catch
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
            }
        }

        private void grvData_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //try
            //{
            //    GridView view = sender as GridView;
            //    view.SetFocusedRowCellValue("THANG", cboThang.EditValue);
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message.ToString());
            //}
        }

        private void grvData_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
        }

        private bool Savedata()
        {
            string sTB = "LayChamCong_Tam" + Commons.Modules.UserName;
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grdData), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, (kyHieuDV == "DM" ? "spSaveLayChamCong_DM" : "spSaveLayChamCong"), sTB, Convert.ToDateTime(cboThang.EditValue));
                Commons.Modules.ObjSystems.XoaTable(sTB);

                return true;
            }
            catch (Exception EX)
            {
                Commons.Modules.ObjSystems.XoaTable(sTB);
                Commons.Modules.ObjSystems.MsgError(EX.Message);
                return false;
            }
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;

        }


        private void grvNgay_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = grvThang1.GetFocusedRowCellValue("THANG").ToString();
            }
            catch { }
            cboThang.ClosePopup();

        }

        private void cboNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdGTGC();
            EnableButon(false);
            Commons.Modules.sLoad = "";
        }

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + cboThang.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                }
                else { }
            }
            catch (Exception ex)
            {
                cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
            }
            cboThang.ClosePopup();
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdGTGC();
            Commons.Modules.sLoad = "";
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadGrdGTGC();

            EnableButon(false);
            Commons.Modules.sLoad = "";
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadGrdGTGC();
            Commons.Modules.sLoad = "";
        }




        private void grvData_RowCountChanged(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                int index = sumNV.Text.IndexOf(':');
                if (index > 0)
                {
                    if (view.RowCount > 0)
                    {
                        sumNV.Text = sumNV.Text.Substring(0, index) + ": " + view.RowCount.ToString();
                    }
                    else
                    {
                        sumNV.Text = sumNV.Text.Substring(0, index) + ": 0";
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