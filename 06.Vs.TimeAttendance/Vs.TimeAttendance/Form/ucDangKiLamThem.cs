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
using Vs.Report;
using System.Globalization;

namespace Vs.TimeAttendance
{
    public partial class ucDangKiLamThem : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;
        public static ucDangKiLamThem _instance;
        public static ucDangKiLamThem Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDangKiLamThem();
                return _instance;
            }
        }
        CultureInfo cultures = new CultureInfo("en-US");

        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        /// <summary>
        /// 
        /// </summary>
        public ucDangKiLamThem()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);
            repositoryItemTimeEdit1 = new RepositoryItemTimeEdit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucDangKiLamThem_Load(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";

            repositoryItemTimeEdit1.TimeEditStyle = TimeEditStyle.TouchUI;
            repositoryItemTimeEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            repositoryItemTimeEdit1.Mask.EditMask = "HH:mm";

            repositoryItemTimeEdit1.NullText = "00:00";
            repositoryItemTimeEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemTimeEdit1.DisplayFormat.FormatString = "HH:mm";
            repositoryItemTimeEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repositoryItemTimeEdit1.EditFormat.FormatString = "HH:mm";

            EnableButon();
            LoadNgay();
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);

            DataTable dtNCC = new DataTable();
            dtNCC.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNhomChamCong",Commons.Modules.UserName, Commons.Modules.TypeLanguage, false));
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_NCC, dtNCC, "ID_NHOM", "TEN_NHOM", "TEN_NHOM");


            LoadGridCongNhan();
            LoadGrdDSLamThem();

            DataTable dCa = new DataTable();
            RepositoryItemLookUpEdit cboCa = new RepositoryItemLookUpEdit();
            dCa.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT ID_CDLV, CA, GIO_BD, GIO_KT, PHUT_BD, PHUT_KT " +
                                             " FROM CHE_DO_LAM_VIEC"));
            cboCa.NullText = "";
            cboCa.ValueMember = "ID_CDLV";
            cboCa.DisplayMember = "CA";
            cboCa.DataSource = dCa;
            cboCa.Columns.Clear();

            cboCa.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CA"));
            cboCa.Columns["CA"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "CA");

            cboCa.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GIO_BD"));
            cboCa.Columns["GIO_BD"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "GIO_BD");
            cboCa.Columns["GIO_BD"].FormatType = DevExpress.Utils.FormatType.DateTime;
            cboCa.Columns["GIO_BD"].FormatString = "HH:mm";

            cboCa.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GIO_KT"));
            cboCa.Columns["GIO_KT"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "GIO_KT");
            cboCa.Columns["GIO_KT"].FormatType = DevExpress.Utils.FormatType.DateTime;
            cboCa.Columns["GIO_KT"].FormatString = "HH:mm";

            cboCa.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_CDLV"));
            cboCa.Columns["ID_CDLV"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "ID_CDLV");
            cboCa.Columns["ID_CDLV"].Visible = false;

            cboCa.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            cboCa.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            grvLamThem.Columns["CA"].ColumnEdit = cboCa;

            cboCa.BeforePopup += cboCa_BeforePopup;
            cboCa.EditValueChanged += CboCa_EditValueChanged;
            //cboCa.ButtonClick += CboCa_EditValueChanged;
            //cboCa.Click += CboCa_EditValueChanged;
            Commons.Modules.sPS = "";
            grvCongNhan_FocusedRowChanged(null, null);
        }

        private void CboCa_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;

            //string id = lookUp.get;

            // Access the currently selected data row
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;

            grvLamThem.SetFocusedRowCellValue("GIO_BD", dataRow.Row["GIO_BD"]);
            grvLamThem.SetFocusedRowCellValue("GIO_KT", dataRow.Row["GIO_KT"]);
            grvLamThem.SetFocusedRowCellValue("CA", Convert.ToInt64(dataRow.Row["ID_CDLV"]));
            //grvLamThem.SetFocusedRowCellValue("PHUT_BD", dataRow.Row["PHUT_BD"]);
            //grvLamThem.SetFocusedRowCellValue("PHUT_KT", dataRow.Row["PHUT_KT"]);
        }

        DataTable dtCaLV;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboCa_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                dtCaLV = new DataTable();
                dtCaLV.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCaLVThem", cboNgay.EditValue, grvLamThem.GetFocusedRowCellValue("ID_NHOM"), Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                if (sender is LookUpEdit cbo)
                {
                    cbo.Properties.DataSource = null;
                    cbo.Properties.DataSource = dtCaLV;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// load Grid
        /// </summary>
        private void LoadGrdDSLamThem()
        {
            decimal idCongNhan = -1;
            DataTable dt = new DataTable();
            if (isAdd)
            {
                //grvLamThem.OptionsBehavior.Editable = true;
                grdLamThem.DataSource = null;
                if (grvCongNhan.FocusedRowHandle >= 0)
                {
                    decimal.TryParse(grvCongNhan.GetFocusedRowCellValue("ID_CN").ToString(), out idCongNhan);
                }
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListLamThem", Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd"),
                                                Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dt.Columns["COM_CA"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdLamThem, grvLamThem, dt, false, false, false, false, true, this.Name);

                DataTable dID_NHOM = new DataTable();
                dID_NHOM.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetNhomCC", cboNgay.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.AddCombXtra("ID_NHOM", "TEN_NHOM", grvLamThem, dID_NHOM, false, "ID_NHOM", "NHOM_CHAM_CONG");

                grvLamThem.OptionsBehavior.Editable = true;


                FormatGrvLamThem();

                return;
            }
            try
            {
                if (grvCongNhan.FocusedRowHandle >= 0)
                {
                    decimal.TryParse(grvCongNhan.GetFocusedRowCellValue("ID_CN").ToString(), out idCongNhan);
                }
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListLamThem", Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd"),
                                                Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dt.Columns["COM_CA"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdLamThem, grvLamThem, dt, false, false, false, false, true, this.Name);

                DataTable dID_NHOM = new DataTable();
                dID_NHOM.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetNhomCC", cboNgay.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.AddCombXtra("ID_NHOM", "TEN_NHOM", grvLamThem, dID_NHOM, false);

                FormatGrvLamThem();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        /// <summary>
        /// Get List CN
        /// </summary>
        private void LoadGridCongNhan()
        {
            DataTable dt = new DataTable();
            try
            {
                if (isAdd)
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetEditListCN_LAM_THEM", Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd"),
                                      cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdCongNhan, grvCongNhan, dt, true, false, false, false, true, this.Name);
                }
                else
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCN_LAM_THEM", Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd"),
                            cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdCongNhan, grvCongNhan, dt, false, false, false, false, true, this.Name);
                    //do nothing;
                }
                dt.Columns["CHON"].ReadOnly = false;
                dt.Columns["MS_CN"].ReadOnly = true;
                dt.Columns["HO_TEN"].ReadOnly = true;

                grvCongNhan.Columns["CHON"].Visible = false;
                grvCongNhan.Columns["ID_CDLV"].Visible = false;

                grvCongNhan.OptionsSelection.CheckBoxSelectorField = "CHON";
                grvCongNhan.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                FormatGridCongNhan();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void FormatGridCongNhan()
        {
            grvCongNhan.Columns["ID_CN"].Visible = false;
            grvCongNhan.Columns["MS_CN"].Width = 100;
            grvCongNhan.Columns["HO_TEN"].Width = 300;
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
            LoadGridCongNhan();
            LoadGrdDSLamThem();
            Commons.Modules.sPS = "";
            grvCongNhan_FocusedRowChanged(null, null);
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
            LoadGridCongNhan();
            LoadGrdDSLamThem();
            Commons.Modules.sPS = "";
            grvCongNhan_FocusedRowChanged(null, null);
            if (grvCongNhan.RowCount == 0)
                grdLamThem.DataSource = null;
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
            LoadGridCongNhan();
            LoadGrdDSLamThem();

            Commons.Modules.sPS = "";
            grvCongNhan_FocusedRowChanged(null, null);
            if (grvCongNhan.RowCount == 0)
                grdLamThem.DataSource = null;
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
            LoadGridCongNhan();
            LoadGrdDSLamThem();
            Commons.Modules.sPS = "";
            grvCongNhan_FocusedRowChanged(null, null);
        }
        #endregion

        /// <summary>
        /// Format Grid
        /// </summary>
        private void FormatGrvLamThem()
        {
            try
            {
                grvLamThem.Columns["ID_CN"].Visible = false;
                grvLamThem.Columns["NGAY"].Visible = false;
                grvLamThem.Columns["GIO_BD"].ColumnEdit = this.repositoryItemTimeEdit1;
                grvLamThem.Columns["GIO_KT"].ColumnEdit = this.repositoryItemTimeEdit1;
                grvLamThem.Columns["GIO_BD"].OptionsColumn.ReadOnly = true;
                grvLamThem.Columns["PHUT_BD"].Visible = false;
                grvLamThem.Columns["PHUT_KT"].Visible = false;
            }
            catch
            {


            }
            //grvLamThem.Columns["ID_NHOM"].Visible = false;

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
                        isAdd = true;
                        EnableButon();
                        LoadGridCongNhan();
                        LoadGrdDSLamThem();
                        Commons.Modules.ObjSystems.AddnewRow(grvLamThem, true);
                        grvCongNhan_FocusedRowChanged(null, null);

                        break;
                    }
                case "xoa":
                    {
                        XoaDangKiGioLamThem();
                        LoadGridCongNhan();
                        LoadGrdDSLamThem();
                        grvCongNhan_FocusedRowChanged(null, null);
                        break;
                    }
                case "ghi":
                    {

                        Validate();
                        if (grvCongNhan.HasColumnErrors) return;
                        if (Savedata() == false)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                        }
                        Commons.Modules.ObjSystems.DeleteAddRow(grvLamThem);
                        isAdd = false;
                        EnableButon();
                        LoadGridCongNhan();
                        LoadGrdDSLamThem();
                        grvCongNhan_FocusedRowChanged(null, null);
                        break;
                    }
                case "khongghi":
                    {
                        Commons.Modules.ObjSystems.DeleteAddRow(grvLamThem);
                        isAdd = false;
                        EnableButon();
                        LoadGridCongNhan();
                        LoadGrdDSLamThem();
                        grvCongNhan_FocusedRowChanged(null, null);
                        break;
                    }
                case "in":
                    {
                        InBaoCao();
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
                        if (grvCongNhan.HasColumnErrors) return;
                        if (XtraMessageBox.Show("Bạn có muốn cập nhật nhóm: " + grvLamThem.GetFocusedRowCellDisplayText("ID_NHOM") + ", ca: " + grvLamThem.GetFocusedRowCellDisplayText("CA") + " cho các nhân viên được chọn", "", MessageBoxButtons.YesNo) == DialogResult.No)
                        {

                            return;
                        }
                        CapNhatNhom();
                        break;
                    }
                case "xoatrangnhom":
                    {
                        Validate();
                        if (grvCongNhan.HasColumnErrors) return;
                        XoaTrangNhom();
                        break;
                    }
                case "chontatca":
                    {
                        ChonTatCa();
                        break;
                    }
                case "bochontatca":
                    {
                        BoChonTatCa();
                        break;
                    }
            }
        }

        private void InBaoCao()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "rptBCDKTangCa", Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd"),
                                            cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));

            frmViewReport frm = new frmViewReport();
            //Convert.ToInt64(grvCongNhan.GetFocusedRowCellValue("ID_CN"))
            string tieuDe = "DANH SÁCH NHÂN VIÊN ĐĂNG KÍ TĂNG CA";
            frm.rpt = new rptDKTangCa(Convert.ToDateTime(cboNgay.EditValue), tieuDe);
            if (dt == null || dt.Rows.Count == 0) return;
            dt.TableName = "DATA";
            frm.AddDataSource(dt);
            frm.ShowDialog();
        }
        private void ChonTatCa()
        {
            int i;
            for (i = 0; i < grvCongNhan.RowCount; i++)
            {
                grvCongNhan.SetRowCellValue(i, "CHON", true);
                grvCongNhan.UpdateCurrentRow();
            }
        }

        private void BoChonTatCa()
        {
            int i;
            for (i = 0; i < grvCongNhan.RowCount; i++)
            {
                grvCongNhan.SetRowCellValue(i, "CHON", false);
                grvCongNhan.UpdateCurrentRow();
            }
        }

        private void CheckAllButton(bool val)
        {
            if (val)
            {
                grvCongNhan.BeginSelection();
                grvCongNhan.ClearSelection();
                grvCongNhan.SelectRange(grvCongNhan.FocusedRowHandle, grvCongNhan.FocusedRowHandle + 1);
                grvCongNhan.EndSelection();
            }
            else
            {

            }
        }

        #region Xu ly button

        /// <summary>
        /// btn cap nhat nhom
        /// </summary>
        /// <returns></returns>
        private void CapNhatNhom()
        {
            try
            {
                //lấy lướng công nhân được chọn
                DataTable dt = Commons.Modules.ObjSystems.ConvertDatatable(grdCongNhan);
                dt = dt.AsEnumerable().Where(x => x["CHON"].ToString().ToLower() == "true").CopyToDataTable();
                //lấy lưới làm thêm đã có dữ liệu 
                DataTable dtLT = Commons.Modules.ObjSystems.ConvertDatatable(grdLamThem);

                //lấy table của lưới lưới cần cập nhật
                DataTable tableLT = Commons.Modules.ObjSystems.ConvertDatatable(grdLamThem);
                DataTable data = new DataTable();
                data = tableLT.Copy();
                //lấy data dữ liệu của làm thêm cần cập nhập
                string sID_NHOM, sID_CA, sNgay;


                sID_NHOM = ""; sID_CA = ""; sNgay = "";

                try { sID_NHOM = grvLamThem.GetFocusedRowCellValue("ID_NHOM").ToString(); } catch { }
                try { sID_CA = grvLamThem.GetFocusedRowCellValue("CA").ToString(); } catch { }
                try { sNgay = Convert.ToDateTime(grvLamThem.GetFocusedRowCellValue("NGAY").ToString()).ToString("dd/MM/yyyy"); } catch { }

                foreach (DataRow dr in dt.Rows)
                {
                    string sDK = " 1 = 1 ";
                    string sID_CN = "";
                    try { sID_CN = dr["ID_CN"].ToString(); } catch { }
                    DataTable dtTM = new DataTable();
                    //dtTM = data.AsEnumerable().Where(x => x["ID_CN"].ToString().ToLower() == sID_CN).CopyToDataTable();
                    data.DefaultView.RowFilter = " ID_CN= '" + sID_CN + "' ";
                    if (data.DefaultView.ToTable().Rows.Count == 0)
                    {
                        //data.DefaultView.RowFilter = "";
                        //try
                        //{
                        //    if (sID_CN != "") sDK = sDK + " AND ID_CN = '" + sID_CN + "' ";


                        //    if (sID_NHOM != "") sDK = sDK + " AND ID_NHOM = '" + sID_NHOM + "' ";
                        //    if (sID_CA != "") sDK = sDK + " AND CA = '" + sID_CA + "' ";
                        //    if (sNgay != "") sDK = sDK + " AND NGAY  = '" + sNgay + "' ";


                        //    data.DefaultView.RowFilter = sDK;
                        //}
                        //catch { }
                        //if (data.DefaultView.ToTable().Rows.Count == 0)
                        //{

                        DataRow newRow = data.NewRow();
                        newRow.SetField("ID_CN", dr["ID_CN"]);
                        newRow.SetField("ID_NHOM", grvLamThem.GetFocusedRowCellValue("ID_NHOM"));
                        newRow.SetField("CA", grvLamThem.GetFocusedRowCellValue("CA"));
                        newRow.SetField("NGAY", grvLamThem.GetFocusedRowCellValue("NGAY"));

                        newRow.SetField("GIO_BD", grvLamThem.GetFocusedRowCellValue("GIO_BD"));
                        newRow.SetField("GIO_KT", grvLamThem.GetFocusedRowCellValue("GIO_KT"));
                        newRow.SetField("COM_CA", grvLamThem.GetFocusedRowCellValue("COM_CA"));
                        newRow.SetField("PHUT_BD", grvLamThem.GetFocusedRowCellValue("PHUT_BD"));
                        newRow.SetField("PHUT_KT", grvLamThem.GetFocusedRowCellValue("PHUT_KT"));
                        data.Rows.Add(newRow);
                        data.AcceptChanges();
                        //};
                    }

                }
                data.DefaultView.RowFilter = "";



                grdLamThem.DataSource = null;
                grdLamThem.DataSource = data;
                grvCongNhan_FocusedRowChanged(null, null);
            }
            catch (Exception ex)
            {
            }
            //
        }

        /// <summary>
        /// Xoa trang nhom 
        /// </summary>
        /// <returns></returns>
        private void XoaTrangNhom()
        {
            //int idNhom;
            //int temp;
            //Int32.TryParse(grvLamThem.GetFocusedRowCellValue("ID_NHOM").ToString(), out idNhom);
            //if (idNhom == 0) return;
            //for (int i = 0; i < grvLamThem.DataRowCount; i++)
            //{
            //    DataRow row = grvLamThem.GetDataRow(i);
            //    Int32.TryParse(row["ID_NHOM"].ToString(), out temp);

            //    if (temp == idNhom)
            //    {
            //        grvLamThem.SetRowCellValue(i, "ID_NHOM", -1);
            //        grvLamThem.SetRowCellValue(i, "CA", "");
            //    }
            //}
            //string sTB = "CDCCNV_XoaNhom" + Commons.Modules.UserName;
            //Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grdLamThem), "");

            //for (int i = 0; i < grvLamThem.RowCount - 1; i++)
            //{
            //    grvLamThem.DeleteRow(i);
            //}

            DataTable dt = Commons.Modules.ObjSystems.ConvertDatatable(grdLamThem);
            dt.AcceptChanges();
            dt = dt.AsEnumerable().Where(x => x["CA"].ToString() != "" + grvCongNhan.GetFocusedRowCellValue("ID_CDLV") + "").CopyToDataTable();
            grdLamThem.DataSource = dt;
            grvCongNhan_FocusedRowChanged(null, null);

            //grvLamThem.RefreshData();
            
            //dr =  dt.Select("CA" +"!='"+ grvCongNhan.GetFocusedRowCellValue("ID_CDLV").ToString() + "'");
            //dt.Select(string.Format("CA = "+Convert.ToInt32(grvCongNhan.GetFocusedRowCellValue("ID_CDLV"))+"")).ToList<DataRow>().ForEach(r => r["CA"] = Convert.ToInt32(grvCongNhan.GetFocusedRowCellValue("ID_CDLV")));
            //dt.Clear();
            //grdLamThem.DataSource = dt;


            //grdLamThem.DataSource=null;

        }

        /// <summary>
        /// Xoa dong
        /// </summary>
        private void XoaDangKiGioLamThem()
        {
            if (grvLamThem.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }

            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                string sSql = "DELETE dbo.DANG_KY_LAM_GIO_LAM_THEM WHERE ID_CN = " + grvCongNhan.GetFocusedRowCellValue("ID_CN") +
                                                        " AND CONVERT(NVARCHAR(10),NGAY, 112) = '"
                                                        + Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd") + "'";

                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                grvLamThem.DeleteSelectedRows();
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
            DataTable dangKiLamThemGio = new DataTable();

            string stbCongNhan = "grvCongNhanLamThemGio" + Commons.Modules.UserName;
            string stbLamThemGio = "grvLamThemGio" + Commons.Modules.UserName;
            string sSql = "";
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbCongNhan, (DataTable)grdCongNhan.DataSource, "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbLamThemGio, (DataTable)grdLamThem.DataSource, "");

                DateTime dNgay;
                dNgay = DateTime.ParseExact(cboNgay.Text, "dd/MM/yyyy", cultures);
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveDangKyLamThemGio", dNgay, stbCongNhan, stbLamThemGio);
                Commons.Modules.ObjSystems.XoaTable(stbCongNhan);
                Commons.Modules.ObjSystems.XoaTable(stbLamThemGio);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                Commons.Modules.ObjSystems.XoaTable(stbCongNhan);
                Commons.Modules.ObjSystems.XoaTable(stbLamThemGio);
                return false;
            }
        }

        #endregion Xu ly button

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visible"></param>
        private void EnableButon()
        {
            btnALL.Buttons[0].Properties.Visible = !isAdd;
            btnALL.Buttons[1].Properties.Visible = !isAdd;
            btnALL.Buttons[2].Properties.Visible = !isAdd;
            btnALL.Buttons[3].Properties.Visible = !isAdd;
            btnALL.Buttons[4].Properties.Visible = !isAdd;
            btnALL.Buttons[10].Properties.Visible = !isAdd;

            btnALL.Buttons[5].Properties.Visible = isAdd;
            btnALL.Buttons[6].Properties.Visible = isAdd;
            btnALL.Buttons[7].Properties.Visible = isAdd;
            btnALL.Buttons[8].Properties.Visible = isAdd;
            btnALL.Buttons[9].Properties.Visible = isAdd;

            cboNgay.Enabled = !isAdd;
            cboDonVi.Enabled = !isAdd;
            cboXiNghiep.Enabled = !isAdd;
            cboTo.Enabled = !isAdd;
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
        private void LoadNgay()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListNgayDKLamThem", Commons.Modules.UserName, Commons.Modules.TypeLanguage));

            if (grdNgay.DataSource == null)
            {
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay, dt, false, true, true, true, true, this.Name);
            }
            else
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay, dt, false, false, true, false, false, this.Name);

            if (dt.Rows.Count > 0)
            {
                cboNgay.EditValue = dt.Rows[0]["NGAY"];
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
                cboNgay.Text = Convert.ToDateTime(grv.GetFocusedRowCellValue("NGAY").ToString()).ToShortDateString();
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
        private void grvCongNhan_RowCountChanged(object sender, EventArgs e)
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

        private void grvCongNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            DataTable dtTmp = new DataTable();
            String sIDCN;
            try
            {
                dtTmp = (DataTable)grdLamThem.DataSource;

                string sDK = "";
                sIDCN = "-1";
                try { sIDCN = grvCongNhan.GetFocusedRowCellValue("ID_CN").ToString(); } catch { }
                if (sIDCN != "-1") sDK = " ID_CN = '" + sIDCN + "' ";

                dtTmp.DefaultView.RowFilter = sDK;
            }
            catch { }

        }

        private void grvLamThem_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            grvLamThem.SetFocusedRowCellValue("COM_CA", false);
        }

        private void grvLamThem_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DateTime gioBD = new DateTime();
            DateTime gioKT = new DateTime();
            decimal phutBD = 0;
            decimal phutKT = 0;
            GridView view = sender as GridView;
            try
            {
                if (e.Column.FieldName == "GIO_BD")
                {
                    if (DateTime.TryParse(view.GetFocusedRowCellValue("GIO_BD").ToString(), out gioBD))
                    {
                        gioBD = DateTime.Parse(view.GetFocusedRowCellValue("GIO_BD").ToString());
                        phutBD = gioBD.Hour * 60 + gioBD.Minute;
                        view.SetFocusedRowCellValue("PHUT_BD", phutBD);
                        view.SetRowCellValue(e.RowHandle, view.Columns["ID_CN"], grvCongNhan.GetFocusedRowCellValue("ID_CN").ToString());
                        view.SetRowCellValue(e.RowHandle, view.Columns["NGAY"], Convert.ToDateTime(cboNgay.EditValue).ToString());

                    }

                }

                if (e.Column.FieldName == "GIO_KT")
                {
                    if (DateTime.TryParse(view.GetFocusedRowCellValue("GIO_KT").ToString(), out gioKT))
                    {
                        gioKT = DateTime.Parse(view.GetFocusedRowCellValue("GIO_KT").ToString());
                        phutKT = gioKT.Hour * 60 + gioKT.Minute;
                        view.SetFocusedRowCellValue("PHUT_KT", phutKT);
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