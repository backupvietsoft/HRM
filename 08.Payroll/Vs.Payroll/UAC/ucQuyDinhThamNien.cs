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
using DevExpress.Utils;

namespace Vs.Payroll
{
    public partial class ucQuyDinhThamNien : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;

        public static ucQuyDinhThamNien _instance;
        public static ucQuyDinhThamNien Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucQuyDinhThamNien();
                return _instance;
            }
        }

        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        public ucQuyDinhThamNien()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);

        }

        private void ucQuyDinhThamNien_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            LoadThang();
            Commons.Modules.ObjSystems.LoadCboDonViKO(cboDonVi);
            LoadGrdQuyDinhThamNien();
            EnableButon(isAdd);
            Commons.Modules.sLoad = "";
        }

        private void LoadGrdQuyDinhThamNien()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListQuyDinhThamNien", Convert.ToDateTime(cboNgay.EditValue),
                                                cboDonVi.EditValue, Commons.Modules.TypeLanguage));
                if (grdData.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdData, grvData, dt, false, false, false, true, true, this.Name);
                    dt.Columns["TT"].ReadOnly = false;
                    grvData.Columns["ID_TN"].Visible = false;
                    grvData.Columns["ID_DV"].Visible = false;
                    grvData.Columns["THANG"].Visible = false;
                    grvData.Columns["TT"].Visible = false;
                    grvData.Columns["TIEN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvData.Columns["TIEN"].DisplayFormat.FormatString = "N0";
                }
                else
                {
                    grdData.DataSource = dt;
                }
            }
            catch
            {
            }
            

        }

        //public void LoadDonVi()
        //{
        //    //try
        //    //{
        //    //    DataTable dtdv = new DataTable();
        //    //    dtdv.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_DV, TEN_DV FROM DON_VI  ORDER BY TEN_DV"));
        //    //    // Commons.Modules.ObjSystems.MLoadComboboxEdit(cboDonVi, dtdv, "TEN_DV");
        //    //    Commons.Modules.ObjSystems.MLoadLookUpEdit(cboDonVi, dtdv, "ID_DV", "TEN_DV", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_DV"), false);
        //    //    //cboDonVi.SelectedIndex = 0;
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //}
        //}

        public void LoadThang()
        {
            try
            {

                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.THAM_NIEN ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                grvThang.Columns["M"].Visible = false;
                grvThang.Columns["Y"].Visible = false;

                cboNgay.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch
            {
                cboNgay.Text = DateTime.Now.ToString("MM/yyyy");
            }
        }



        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        Commons.Modules.ObjSystems.AddnewRow(grvData, true);
                        isAdd = true;
                        EnableButon(isAdd);
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
                        LoadGrdQuyDinhThamNien();
                        isAdd = false;
                        EnableButon(isAdd);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        break;
                    }
                case "khongghi":
                    {
                        Commons.Modules.ObjSystems.DeleteAddRow(grvData);
                        LoadGrdQuyDinhThamNien();
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

        private void EnableButon(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = !visible;
            btnALL.Buttons[1].Properties.Visible = !visible;
            btnALL.Buttons[2].Properties.Visible = !visible;
            btnALL.Buttons[3].Properties.Visible = !visible;
            btnALL.Buttons[4].Properties.Visible = visible;
            btnALL.Buttons[5].Properties.Visible = visible;
            cboDonVi.Enabled = !visible;
            cboNgay.Enabled = !visible;
        }

        private void XoaCheDoLV()
        {
            if (grvData.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                string sSql = "DELETE dbo.THAM_NIEN WHERE ID_TN = " + grvData.GetFocusedRowCellValue("ID_TN");
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
                view.SetFocusedRowCellValue("ID_DV", cboDonVi.EditValue);
                view.SetFocusedRowCellValue("THANG", cboNgay.EditValue);
                view.SetFocusedRowCellValue("TT", true);
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
            string sTB = "QDTN_TMP" + Commons.Modules.UserName;
            try
            {

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sTB, Commons.Modules.ObjSystems.ConvertDatatable(grvData), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveThamNien1", sTB);
                Commons.Modules.ObjSystems.XoaTable(sTB);
                return true;
            }
            catch
            {
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
                if (DateTime.TryParse(view.GetFocusedRowCellValue("GIO_BD").ToString(), out gioBD))
                {
                    gioBD = DateTime.Parse(view.GetFocusedRowCellValue("GIO_BD").ToString());
                    phutBD = gioBD.Hour * 60 + gioBD.Minute;
                }

                if (DateTime.TryParse(view.GetFocusedRowCellValue("GIO_KT").ToString(), out gioKT))
                {
                    gioKT = DateTime.Parse(view.GetFocusedRowCellValue("GIO_KT").ToString());
                    phutKT = gioKT.Hour * 60 + gioKT.Minute;
                }
                Boolean.TryParse(view.GetFocusedRowCellValue("NGAY_HOM_SAU").ToString(), out ngayHomSau);
                Boolean.TryParse(view.GetFocusedRowCellValue("CA_NGAY_HOM_SAU").ToString(), out caNgayHS);

                if (e.Column.FieldName == "GIO_BD")
                {
                    if (ngayHomSau == true)
                    {
                        phutBD = phutBD + 1440;
                    }

                    if (caNgayHS == true)
                    {
                        phutBD = phutBD + 1440;
                    }
                    view.SetFocusedRowCellValue("PHUT_BD", phutBD);
                }
                if (e.Column.FieldName == "GIO_KT")
                {
                    if (ngayHomSau == true)
                    {
                        phutKT = phutKT + 1440;
                    }

                    if (caNgayHS == true)
                    {
                        phutKT = phutKT + 1440;
                    }
                    view.SetFocusedRowCellValue("PHUT_KT", phutKT);
                }
                if (e.Column.FieldName == "PHUT_BD" || e.Column.FieldName == "PHUT_KT")
                {
                    if (phutBD > 0)
                        view.SetFocusedRowCellValue("SO_PHUT", phutKT - phutBD);
                }
                if (e.Column.FieldName == "NGAY_HOM_SAU" || e.Column.FieldName == "CA_NGAY_HOM_SAU")
                {
                    if (ngayHomSau == true)
                    {
                        if (caNgayHS == true)
                        {
                            view.SetFocusedRowCellValue("PHUT_BD", phutBD + 1440 + 1440);
                            view.SetFocusedRowCellValue("PHUT_KT", phutKT + 1440 + 1440);
                            view.SetFocusedRowCellValue("SO_PHUT", phutKT - phutBD);
                        }
                        else
                        {
                            view.SetFocusedRowCellValue("PHUT_BD", phutBD + 1440);
                            view.SetFocusedRowCellValue("PHUT_KT", phutKT + 1440);
                            view.SetFocusedRowCellValue("SO_PHUT", phutKT - phutBD);
                        }
                    }
                    else
                    {
                        if (caNgayHS == true)
                        {
                            view.SetFocusedRowCellValue("PHUT_BD", phutBD + 1440);
                            view.SetFocusedRowCellValue("PHUT_KT", phutKT + 1440);
                            view.SetFocusedRowCellValue("SO_PHUT", phutKT - phutBD);
                        }
                        else
                        {
                            view.SetFocusedRowCellValue("PHUT_BD", phutBD);
                            view.SetFocusedRowCellValue("PHUT_KT", phutKT);
                            view.SetFocusedRowCellValue("SO_PHUT", phutKT - phutBD);
                        }
                    }
                }
            }
            catch { }
        }


        private void grvNgay_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboNgay.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch { }
            cboNgay.ClosePopup();

        }

        private void cboNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdQuyDinhThamNien();
            //EnableButon(true);
            Commons.Modules.sLoad = "";
        }

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboNgay.Text = calThang.DateTime.ToString("MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdThang);
                DataRow[] dr;
                dr = dtTmp.Select("NGAY_TTXL" + "='" + cboNgay.Text + "'", "NGAY_TTXL", DataViewRowState.CurrentRows);
                if (dr.Count() == 1)
                {
                }
                else { }
            }
            catch (Exception ex)
            {
                cboNgay.Text = calThang.DateTime.ToString("MM/yyyy");
            }
            cboNgay.ClosePopup();
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdQuyDinhThamNien();
            //EnableButon(true);
            Commons.Modules.sPS = "";
        }
    }
}