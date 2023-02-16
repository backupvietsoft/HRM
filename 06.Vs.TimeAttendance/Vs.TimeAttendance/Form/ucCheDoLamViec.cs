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

namespace Vs.TimeAttendance
{
    public partial class ucCheDoLamViec : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;
        public static ucCheDoLamViec _instance;
        public static ucCheDoLamViec Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucCheDoLamViec();
                return _instance;
            }
        }

        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        RepositoryItemTextEdit txtEdit;
        public ucCheDoLamViec()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);
            repositoryItemTimeEdit1 = new RepositoryItemTimeEdit();
            txtEdit = new RepositoryItemTextEdit();
        }

        private void ucCheDoLamViec_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";

            repositoryItemTimeEdit1.TimeEditStyle = TimeEditStyle.TouchUI;
            repositoryItemTimeEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            repositoryItemTimeEdit1.Mask.EditMask = "HH:mm";

            repositoryItemTimeEdit1.NullText = "00:00";
            repositoryItemTimeEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemTimeEdit1.DisplayFormat.FormatString = "HH:mm";
            repositoryItemTimeEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemTimeEdit1.EditFormat.FormatString = "HH:mm";


            txtEdit.Properties.DisplayFormat.FormatString = "0.0";
            txtEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            txtEdit.Properties.EditFormat.FormatString = "0.0";
            txtEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            txtEdit.Properties.Mask.EditMask = "0.0";
            txtEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            

            LoaddNgayApDung();
            LoadcboNhomChamCong(cboNhomChamCong);
            LoadGrdChedochamcong();
            EnableButon();
            Commons.Modules.sLoad = "";
        }

        private void LoadGrdChedochamcong()
        {
            try
            {
                DataTable dt = new DataTable();
                if (isAdd)
                {
                    grvData.OptionsBehavior.Editable = true;
                }
                else
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCheDoLamViec", Convert.ToDateTime(cboNgay.EditValue),
                                                    cboNhomChamCong.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    dt.Columns["GIO_BD"].ReadOnly = false;
                    dt.Columns["GIO_KT"].ReadOnly = false;
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, false, true, true, this.Name);

                    //grvData.Columns["TRU_DAU_GIO"].ColumnEdit = txtEdit;
                    //grvData.Columns["TRU_CUOI_GIO"].ColumnEdit = txtEdit;
                    grvData.Columns["HE_SO_NGAY_THUONG"].ColumnEdit = txtEdit;
                    grvData.Columns["HE_SO_NGAY_CN"].ColumnEdit = txtEdit;
                    grvData.Columns["HE_SO_NGAY_LE"].ColumnEdit = txtEdit;

                    grvData.Columns["ID_CDLV"].Visible = false;
                    grvData.Columns["ID_NHOM"].Visible = false;
                    grvData.Columns["NGAY"].Visible = false;
                    grvData.Columns["PHUT_BD"].OptionsColumn.ReadOnly = true;
                    grvData.Columns["PHUT_KT"].OptionsColumn.ReadOnly = true;
                    grvData.Columns["SO_PHUT"].OptionsColumn.ReadOnly = true;
                    grvData.Columns["GIO_BD"].ColumnEdit = this.repositoryItemTimeEdit1;
                    grvData.Columns["GIO_KT"].ColumnEdit = this.repositoryItemTimeEdit1;
                }
            }
            catch
            {
            }
            
        }

        public void LoadcboNhomChamCong(SearchLookUpEdit cboNhomChamCong)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNhomChamCong", Commons.Modules.UserName, Commons.Modules.TypeLanguage, false));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNhomChamCong, dt, "ID_NHOM", "TEN_NHOM", "TEN_NHOM");
            }
            catch (Exception ex)
            {
            }
        }

        public void LoaddNgayApDung()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetdNgayApDung", Commons.Modules.UserName, Commons.Modules.TypeLanguage));

                if (grdNgay.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay, dt, false, true, true, true, true, this.Name);
                }
                else
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay, dt, false, false, true, false, false, this.Name);
                }

                if (dt.Rows.Count == 0)
                {
                    cboNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
                else
                {
                    cboNgay.EditValue = dt.Rows[0][0];
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void cboNhomChamCong_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdChedochamcong();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        isAdd = true;
                        EnableButon();
                        LoadGrdChedochamcong();
                        Commons.Modules.ObjSystems.AddnewRow(grvData, true);
                        break;
                    }
                case "xoa":
                    {
                        XoaCheDoLV();
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
                        EnableButon();
                        LoadGrdChedochamcong();
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        break;
                    }
                case "khongghi":
                    {
                        isAdd = false;
                        EnableButon();
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        LoadGrdChedochamcong();
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
            }
        }

        private void EnableButon()
        {
            btnALL.Buttons[0].Properties.Visible = !isAdd;
            btnALL.Buttons[1].Properties.Visible = !isAdd;
            btnALL.Buttons[2].Properties.Visible = !isAdd;
            btnALL.Buttons[3].Properties.Visible = !isAdd;
            btnALL.Buttons[4].Properties.Visible = isAdd;
            btnALL.Buttons[5].Properties.Visible = isAdd;
            cboNhomChamCong.Enabled = !isAdd;
            cboNgay.Enabled = !isAdd;
        }

        private void XoaCheDoLV()
        {
            if (grvData.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                string sSql = "DELETE dbo.CHE_DO_LAM_VIEC WHERE ID_CDLV = " + grvData.GetFocusedRowCellValue("ID_CDLV");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                grvData.DeleteSelectedRows();
            }
            catch
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
            }
        }

        private void grvData_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.SetFocusedRowCellValue("HE_SO_NGAY_THUONG", 1);
                view.SetFocusedRowCellValue("HE_SO_NGAY_CN", 2);
                view.SetFocusedRowCellValue("HE_SO_NGAY_LE", 3);
                view.SetFocusedRowCellValue("NGAY", Convert.ToDateTime(cboNgay.EditValue));
                view.SetFocusedRowCellValue("ID_NHOM", cboNhomChamCong.EditValue);
                view.SetFocusedRowCellValue("TRU_DAU_GIO", 0);
                view.SetFocusedRowCellValue("TRU_CUOI_GIO", 0);
                view.SetFocusedRowCellValue("PHUT_VE_SOM", 0);
                view.SetFocusedRowCellValue("PHUT_TRUOC_CA", 0);
                view.SetFocusedRowCellValue("TANG_CA", false);
                view.SetFocusedRowCellValue("CA_DEM", false);
                view.SetFocusedRowCellValue("NGAY_HOM_SAU", false);
                view.SetFocusedRowCellValue("CA_NGAY_HOM_SAU", false);
                view.SetFocusedRowCellValue("KIEM_TRA", false);
                view.SetFocusedRowCellValue("CHE_DO", false);
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

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
        }

        private bool Savedata()
        {
            string sTB = "CDLV_TMP" + Commons.Modules.UserName;
            string sSql = "";
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");

                //sSql = "DELETE CHE_DO_LAM_VIEC WHERE CONVERT(NVARCHAR, NGAY, 112) = '" + Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd") +
                //       "' AND ID_NHOM = " + cboNhomChamCong.EditValue +
                //       " INSERT INTO CHE_DO_LAM_VIEC([ID_NHOM],[CA],[NGAY],[GIO_BD],[GIO_KT],[PHUT_BD],[PHUT_KT],[SO_PHUT],[HE_SO_NGAY_THUONG]," +
                //       "[HE_SO_NGAY_CN],[HE_SO_NGAY_LE],[TRU_DAU_GIO],[TRU_CUOI_GIO],[PHUT_VE_SOM],[TANG_CA],[TC_DEM],[KIEM_TRA],[NGAY_HOM_SAU],[CA_NGAY_HOM_SAU]," +
                //       "[CA_DEM],[PHUT_TRUOC_CA],[CHE_DO]) SELECT " + cboNhomChamCong.EditValue + " AS [ID_NHOM],[CA],[NGAY],[GIO_BD],[GIO_KT],[PHUT_BD]," +
                //       "[PHUT_KT],[SO_PHUT],[HE_SO_NGAY_THUONG],[HE_SO_NGAY_CN],[HE_SO_NGAY_LE],[TRU_DAU_GIO],[TRU_CUOI_GIO],[PHUT_VE_SOM],[TANG_CA],[TC_DEM]," +
                //       "[KIEM_TRA],[NGAY_HOM_SAU],[CA_NGAY_HOM_SAU],[CA_DEM],[PHUT_TRUOC_CA],[CHE_DO] FROM " + sTB + " WHERE CA IS NOT NULL " + "";

                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "sPsaveCheDoLamViec", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboNhomChamCong.EditValue, sTB, Convert.ToDateTime(cboNgay.EditValue).ToString("MM/dd/yyyy"));
                Commons.Modules.ObjSystems.XoaTable(sTB);
                return true;
            }
            catch (Exception ex)
            {
                Commons.Modules.ObjSystems.XoaTable(sTB);
                return false;
            }
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            DateTime gioBD;
            DateTime gioKT;
            int phutBD = 0;
            int phutKT = 0;
            Boolean ngayHomSau;
            Boolean caNgayHS;
            try
            {
                var row = view.GetFocusedDataRow();

                if (e.Column.FieldName == "GIO_BD")
                {
                    //gioBD = DateTime.Parse(row["GIO_BD"].ToString());
                    gioBD = Convert.ToDateTime("1900-01-01 " + DateTime.Parse(row["GIO_BD"].ToString()).TimeOfDay);
                    phutBD = gioBD.Hour * 60 + gioBD.Minute;
                    if (!row["GIO_KT"].ToString().Equals(""))
                    {
                        //gioKT = DateTime.Parse(row["GIO_KT"].ToString());
                        gioKT = Convert.ToDateTime("1900-01-01 " + DateTime.Parse(row["GIO_KT"].ToString()).TimeOfDay);
                        phutKT = gioKT.Hour * 60 + gioKT.Minute;
                    }
                    else
                    {
                        phutKT = 0;
                    }

                    Boolean.TryParse(row["NGAY_HOM_SAU"].ToString(), out ngayHomSau);

                    if (ngayHomSau == true)
                    {
                        phutBD = phutBD + 1440;
                    }
                    row["PHUT_BD"] = phutBD;
                    row["SO_PHUT"] = phutKT - phutBD;
                }

                if (e.Column.FieldName == "GIO_KT")
                {
                    if (!row["GIO_BD"].ToString().Equals(""))
                    {
                        //gioBD = DateTime.Parse(row["GIO_BD"].ToString());
                        gioBD = Convert.ToDateTime("1900-01-01 " + DateTime.Parse(row["GIO_BD"].ToString()).TimeOfDay);
                        phutBD = gioBD.Hour * 60 + gioBD.Minute;
                    }
                    else
                    {
                        phutBD = 0;
                    }
                    gioKT = Convert.ToDateTime("1900-01-01 " + DateTime.Parse(row["GIO_KT"].ToString()).TimeOfDay);
                    //gioKT = DateTime.Parse(row["GIO_KT"].ToString());
                    phutKT = gioKT.Hour * 60 + gioKT.Minute;
                    Boolean.TryParse(row["NGAY_HOM_SAU"].ToString(), out ngayHomSau);
                    if (ngayHomSau == true)
                    {
                        phutKT = phutKT + 1440;
                    }
                    row["PHUT_KT"] = phutKT;
                    row["SO_PHUT"] = phutKT - phutBD;
                }

                // Calculating the dicsount % and detect if the cellvalue change in certain column  
                if (e.Column.FieldName == "NGAY_HOM_SAU" || e.Column.FieldName == "CA_NGAY_HOM_SAU")
                {
                    //gioBD = DateTime.Parse(row["GIO_BD"].ToString());
                    gioBD = Convert.ToDateTime("1900-01-01 " + DateTime.Parse(row["GIO_BD"].ToString()).TimeOfDay);
                    phutBD = gioBD.Hour * 60 + gioBD.Minute;
                    gioKT = Convert.ToDateTime("1900-01-01 " + DateTime.Parse(row["GIO_KT"].ToString()).TimeOfDay);
                    //gioKT = DateTime.Parse(row["GIO_KT"].ToString());
                    phutKT = gioKT.Hour * 60 + gioKT.Minute;
                    phutKT = phutKT + 1440;
                    row["PHUT_KT"] = phutKT;
                    row["SO_PHUT"] = phutKT - phutBD;
                }
            }
            catch { }
        }

        private void calNgay_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboNgay.Text = calNgay.DateTime.Date.ToShortDateString();
            }
            catch
            {
            }
            cboNgay.ClosePopup();
        }

        private void grvNgay_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboNgay.Text = Convert.ToDateTime(grv.GetFocusedRowCellValue("NGAY").ToString()).ToShortDateString();
            }
            catch { }
            cboNgay.ClosePopup();
        }

        private void cboNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdChedochamcong();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }
    }
}