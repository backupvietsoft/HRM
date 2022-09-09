﻿using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Navigation;
using System.IO;
using System.Collections.Generic;
using DevExpress.XtraLayout;
using Vs.Report;
using DevExpress.XtraGrid.Views.Grid;

namespace Vs.HRM
{
    public partial class ucQuyetDinhThoiViec : DevExpress.XtraEditors.XtraUserControl
    {
        private decimal SoThangPhep = -1;
        private static int QDTV = 0;
        public static ucQuyetDinhThoiViec _instance;
        public static ucQuyetDinhThoiViec Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucQuyetDinhThoiViec();
                return _instance;
            }
        }

        public ucQuyetDinhThoiViec()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root, layoutControlGroup1 }, windowsUIButton);
        }
        private void ucQuyetDinhThoiViec_Load(object sender, EventArgs e)
        {
            enableButon(true);
            formatText();
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(cboSearch_DV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboSearch_DV, cboSearch_XN);
            Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN, cboSearch_TO);
            Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);

            DateTime dTuNgay = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime dDenNgay = new DateTime(DateTime.Now.Year, 12, 31);
            dTNgay.EditValue = dTuNgay;
            dDNgay.EditValue = dDenNgay;
            Commons.OSystems.SetDateEditFormat(dTNgay);
            Commons.OSystems.SetDateEditFormat(dDNgay);
            NGAY_VAO_LAMdateEdit.Properties.ReadOnly = true;
            LoadGridCongNhan(-1);
            LoadCboLyDoThoiViec();
            LoadNguoiKy();
            Commons.Modules.sLoad = "";

        }
        private void formatText()
        {
            TIEN_TRO_CAPTextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            LUONG_TINH_TRO_CAPTextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            SO_PHEP_HUONGTextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeSL.ToString() + "";
            LUONG_TINH_PHEPTextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            TIEN_PHEPTextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            TONG_CONGTextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";
            NGAY_PHEP_NGHITextEdit.Properties.Mask.EditMask = "N" + Commons.Modules.iSoLeTT.ToString() + "";


            Commons.OSystems.SetDateEditFormat(NGAY_NHAN_DONDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_THOI_VIECDateEdit);
            Commons.OSystems.SetDateEditFormat(NGAY_KYDateEdit);
        }
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
            windowsUIButton.Buttons[6].Properties.Visible = !visible;
            windowsUIButton.Buttons[7].Properties.Visible = visible;
            searchControl.Visible = visible;
        }
        private void InDuLieu()
        {
            if (Convert.ToInt64(string.IsNullOrEmpty(grvCongNhan.GetFocusedRowCellValue("ID_QDTV").ToString()) ? 0 : Convert.ToInt64(grvCongNhan.GetFocusedRowCellValue("ID_QDTV"))) == 0)
            {
                return;
            }
            if (Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString() == "DM")
            {
                try
                {
                    System.Data.SqlClient.SqlConnection conn;
                    frmViewReport frm = new frmViewReport();
                    frm.rpt = new rptQuyetDinhChamDutHDLD_DM(DateTime.Now);

                    conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                    conn.Open();

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptQuyetDinhChamDutHDLD_DM", conn);
                    cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                    cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                    cmd.Parameters.Add("@ID_SQD", SqlDbType.Int).Value = Convert.ToInt32(grvCongNhan.GetFocusedRowCellValue("ID_QDTV"));
                    cmd.Parameters.Add("@ID_CN", SqlDbType.Int).Value = Convert.ToInt32(grvCongNhan.GetFocusedRowCellValue("ID_CN"));
                    cmd.CommandType = CommandType.StoredProcedure;

                    System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0].Copy();
                    dt.TableName = "DATA";
                    frm.AddDataSource(dt);

                    //DataTable dt1 = new DataTable();
                    //dt1 = ds.Tables[1].Copy();
                    //dt1.TableName = "NOI_DUNG";
                    //frm.AddDataSource(dt1);

                    frm.ShowDialog();
                }
                catch (Exception ex) { }
            }
            else
            {
                DateTime datNgayThoiViec = Convert.ToDateTime(grvCongNhan.GetFocusedRowCellValue("NGAY_THOI_VIEC"));
                frmInQuyetDinhThoiViec frm = new frmInQuyetDinhThoiViec(Convert.ToInt32(grvCongNhan.GetFocusedRowCellValue("ID_QDTV")), Convert.ToInt32(grvCongNhan.GetFocusedRowCellValue("ID_CN")), datNgayThoiViec);
                frm.ShowDialog();
            }
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            if (btn == null || btn.Tag == null) return;
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        LoadText();
                        navigationFrame.SelectedPage = navigationPage2;
                        enableButon(false);
                        break;
                    }

                case "xoa":
                    {
                        XoaQuyetDinhThoiViec();
                        break;
                    }
                case "in":
                    {
                        InDuLieu();
                        break;
                    }
                case "luu":
                    {
                        Luu();
                        break;
                    }

                case "trove":
                    {
                        navigationFrame.SelectedPage = navigationPage1;
                        dxValidationProvider1.ValidateHiddenControls = false;
                        enableButon(true);
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
        private void Luu()
        {
            if (!dxValidationProvider1.Validate()) return;
            try
            {
                LoadGridCongNhan(Convert.ToInt32(
                SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spGetUpdateQuyetDinhThoiViec",
                        (grvCongNhan.GetFocusedRowCellValue("ID_QDTV").ToString() == string.Empty) ? -1 : Convert.ToInt32(grvCongNhan.GetFocusedRowCellValue("ID_QDTV")),
                        grvCongNhan.GetFocusedRowCellValue("ID_CN"),
                        SO_QDTextEdit.EditValue,
                        NGAY_NHAN_DONDateEdit.EditValue,
                        NGAY_THOI_VIECDateEdit.EditValue,
                        LUONG_TINH_TRO_CAPTextEdit.EditValue,
                        TIEN_TRO_CAPTextEdit.EditValue,
                        TIEN_PHEPTextEdit.EditValue,
                        TONG_CONGTextEdit.EditValue,
                        NGAY_KYDateEdit.EditValue,
                        ID_LD_TVLookUpEdit.EditValue,
                        NGAY_VAO_LAMdateEdit.EditValue,
                        SO_PHEP_HUONGTextEdit.EditValue,
                        NGUYEN_NHANTextEdit.EditValue,
                        ID_NKLookUpEdit.EditValue,
                        LUONG_TINH_PHEPTextEdit.EditValue,
                        SO_NAM_TRO_CAPTextEdit.EditValue,
                        NGAY_PHEP_CHUANTextEdit.EditValue,
                        NGAY_PHEP_COTextEdit.EditValue,
                        NGAY_PHEP_NGHITextEdit.EditValue,
                        SoThangPhep
                )));
            }
            catch (Exception ex)
            {
            }

            navigationFrame.SelectedPage = navigationPage1;
            enableButon(true);
        }
        private void XoaQuyetDinhThoiViec()
        {
            if (grvCongNhan.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                string sSql = "DELETE QUYET_DINH_THOI_VIEC WHERE ID_CN = " + grvCongNhan.GetFocusedRowCellValue("ID_CN") + " UPDATE dbo.CONG_NHAN SET NGAY_NGHI_VIEC = NULL ,ID_LD_TV = NULL, ID_TT_HT = 1 WHERE ID_CN = " + grvCongNhan.GetFocusedRowCellValue("ID_CN") + "";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                LoadGridCongNhan(-1);
            }
            catch
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
            }
        }

        private void cboSearch_TO_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGridCongNhan(-1);
            Commons.Modules.sLoad = "";
        }
        private void cboSearch_DV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboSearch_DV, cboSearch_XN);
            Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN, cboSearch_TO);
            LoadGridCongNhan(-1);
            Commons.Modules.sLoad = "";
        }
        private void cboSearch_XN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboSearch_DV, cboSearch_XN, cboSearch_TO);
            LoadGridCongNhan(-1);
            Commons.Modules.sLoad = "";
        }
        private void LoadGridCongNhan(int idCN)
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongNhanQDTV", Commons.Modules.UserName, Commons.Modules.TypeLanguage, cboSearch_DV.EditValue, cboSearch_XN.EditValue, cboSearch_TO.EditValue, dTNgay.DateTime, dDNgay.DateTime, radChonXem.SelectedIndex));
            if (grdCongNhan.DataSource == null)
            {
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CN"] };
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdCongNhan, grvCongNhan, dt, false, false, false, false, true, this.Name);
                grvCongNhan.Columns["ID_CN"].Visible = false;
                grvCongNhan.Columns["ID_QDTV"].Visible = false;
                grvCongNhan.Columns["TinhTrang"].Visible = false;
                grvCongNhan.Columns["ID_LD_TV"].Visible = false;

                if (idCN != -1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(idCN));
                    grvCongNhan.FocusedRowHandle = grvCongNhan.GetRowHandle(index);
                }

                grvCongNhan.Columns["NGAY_KY"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                grvCongNhan.Columns["NGAY_KY"].DisplayFormat.FormatString = "dd/MM/yyyy";
                grvCongNhan.Columns["NGAY_THOI_VIEC"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                grvCongNhan.Columns["NGAY_THOI_VIEC"].DisplayFormat.FormatString = "dd/MM/yyyy";

            }
            else
            {
                grdCongNhan.DataSource = dt;
            }

        }

        private void LoadCboLyDoThoiViec()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoLyDoThoiViec", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_LD_TVLookUpEdit, dt, "ID_LD_TV", "TEN_LD_TV", "TEN_LD_TV");
        }

        private void LoadNguoiKy()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComBoNguoiKi", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
            Commons.Modules.ObjSystems.MLoadLookUpEdit(ID_NKLookUpEdit, dt, "ID_NK", "HO_TEN", "HO_TEN");

        }

        //private void radTinhTrangLV_EditValueChanged(object sender, EventArgs e)
        //{
        //    DataTable dtTmp = new DataTable();
        //    string sdkien = "( 1 = 1 )";
        //    try
        //    {
        //        dtTmp = (DataTable)grdCongNhan.DataSource;

        //        if (radTinhTrangLV.SelectedIndex == 1) sdkien = "(TinhTrang = 0)";
        //        if (radTinhTrangLV.SelectedIndex == 2) sdkien = "(TinhTrang = 1)";
        //        dtTmp.DefaultView.RowFilter = sdkien;
        //    }
        //    catch
        //    {
        //        try
        //        {
        //            dtTmp.DefaultView.RowFilter = "";
        //        }
        //        catch { }
        //    }
        //}
        private void grvCongNhan_DoubleClick(object sender, EventArgs e)
        {

            navigationFrame.SelectedPage = navigationPage2;
            enableButon(false);
        }
        private void LoadText()
        {
            Commons.Modules.sLoad = "0Load";
            MS_CNtextEdit.EditValue = grvCongNhan.GetFocusedRowCellValue("MS_CN");
            TEN_CNtextEdit.EditValue = grvCongNhan.GetFocusedRowCellValue("HO_TEN");
            try
            {
                if (grvCongNhan.GetFocusedRowCellValue("ID_QDTV").ToString() != string.Empty)
                {
                    string sSql = "SELECT *,CN.NGAY_VAO_LAM FROM dbo.QUYET_DINH_THOI_VIEC QDTV INNER JOIN dbo.CONG_NHAN CN ON CN.ID_CN = QDTV.ID_CN WHERE ID_QDTV = " + grvCongNhan.GetFocusedRowCellValue("ID_QDTV") + "";
                    DataTable dt = new DataTable();
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                    DataRow row = dt.Rows[0];
                    SO_QDTextEdit.EditValue = row["SO_QD"];
                    NGAY_NHAN_DONDateEdit.EditValue = row["NGAY_NHAN_DON"];
                    LUONG_TINH_TRO_CAPTextEdit.EditValue = row["HS_LUONG"];
                    TIEN_TRO_CAPTextEdit.EditValue = row["TIEN_TRO_CAP"].ToString() == "" ? 0 : Convert.ToDouble(row["TIEN_TRO_CAP"]);
                    NGAY_KYDateEdit.EditValue = row["NGAY_KY"];
                    TIEN_PHEPTextEdit.EditValue = row["TIEN_PHEP"].ToString() == "" ? 0 : Convert.ToDouble(row["TIEN_PHEP"]);
                    TONG_CONGTextEdit.EditValue = row["TONG_CONG"];
                    ID_LD_TVLookUpEdit.EditValue = row["ID_LD_TV"];
                    SO_PHEP_HUONGTextEdit.EditValue = row["NGAY_PHEP"];
                    NGUYEN_NHANTextEdit.EditValue = row["NGUYEN_NHAN"];
                    ID_NKLookUpEdit.EditValue = row["ID_NK"];
                    LUONG_TINH_PHEPTextEdit.EditValue = row["LUONG_TINH_PHEP"];
                    SO_NAM_TRO_CAPTextEdit.EditValue = row["SO_NAM_TC"];
                    NGAY_PHEP_CHUANTextEdit.EditValue = row["NGAY_PHEP_CHUAN"];
                    NGAY_PHEP_COTextEdit.EditValue = row["NGAY_PHEP_CO"];
                    NGAY_PHEP_NGHITextEdit.EditValue = row["NGAY_PHEP_NGHI"];
                    NGAY_THOI_VIECDateEdit.EditValue = row["NGAY_THOI_VIEC"];
                    NGAY_VAO_LAMdateEdit.EditValue = row["NGAY_VAO_LAM"];
                }
                else
                {
                    //Commons.Modules.sLoad = "0Load";
                    SO_QDTextEdit.EditValue = "";
                    NGAY_NHAN_DONDateEdit.EditValue = DateTime.Today;
                    NGAY_THOI_VIECDateEdit.EditValue = DateTime.Today;
                    NGAY_KYDateEdit.EditValue = DateTime.Today;
                    ID_LD_TVLookUpEdit.EditValue = null;
                    //NGAY_VAO_CTYTextEdit.EditValue = DateTime.Today;
                    NGUYEN_NHANTextEdit.EditValue = "";
                }
            }
            catch (Exception ex)
            {
            }
            Commons.Modules.sLoad = "";
        }
        private void navigationFrame_SelectedPageChanging(object sender, SelectedPageChangingEventArgs e)
        {
            if (navigationFrame.SelectedPage == navigationPage1)
            {
                if (grvCongNhan.RowCount == 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoChuaChonCongNhan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
                else
                {
                    LoadText();
                    dxValidationProvider1.ValidateHiddenControls = true;
                    dxValidationProvider1.Validate();
                }
            }
        }

        private void NGAY_THOI_VIECDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            //tính lương
            try
            {
                if (Commons.Modules.sLoad == "0Load") return;
                GetTienPhep();
                GetTienTroCap();
                TONG_CONGTextEdit.EditValue = Convert.ToDouble(TIEN_TRO_CAPTextEdit.EditValue) + Convert.ToDouble(TIEN_PHEPTextEdit.EditValue);
            }
            catch
            {

            }
        }

        private void TRO_CAP_KHACTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            TONG_CONGTextEdit.EditValue = Convert.ToDouble(TIEN_TRO_CAPTextEdit.EditValue) + Convert.ToDouble(TIEN_PHEPTextEdit.EditValue);
        }

        private void grdCongNhan_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                XoaQuyetDinhThoiViec();
            }
        }

        private void radChonXem_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGridCongNhan(-1);
            Commons.Modules.sLoad = "";
        }

        private void dTNgay_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dDNgay.DateTime != Convert.ToDateTime("01/01/0001"))
            {
                TimeSpan time = dDNgay.DateTime - dTNgay.DateTime;
                if (time.Days < 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgTuNgayPhaiNhoHonDenNgay"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    dTNgay.ErrorText = "Dữ liệu không hợp lệ";
                }
            }
        }

        private void dDNgay_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dTNgay.DateTime != Convert.ToDateTime("01/01/0001"))
            {
                TimeSpan time = dDNgay.DateTime - dTNgay.DateTime;
                if (time.Days < 0)
                {
                    XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDenNayPhaiLonHonTuNgay"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    dDNgay.ErrorText = "Dữ liệu không hợp lệ";
                }
            }
        }

        private void dTNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (dTNgay.DateTime != Convert.ToDateTime("01/01/0001") || dDNgay.DateTime != Convert.ToDateTime("01/01/0001"))
            {
                LoadGridCongNhan(-1);
            }
        }

        private void dDNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (dTNgay.DateTime != Convert.ToDateTime("01/01/0001") || dDNgay.DateTime != Convert.ToDateTime("01/01/0001"))
            {
                LoadGridCongNhan(-1);
            }
        }

        private void GetTienPhep()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM dbo.GetTienPhep('" + Convert.ToDateTime(NGAY_THOI_VIECDateEdit.EditValue).ToString("MM/dd/yyyy") + "'," + Convert.ToInt32(grvCongNhan.GetFocusedRowCellValue("ID_CN")) + ")"));
                LUONG_TINH_PHEPTextEdit.EditValue = string.IsNullOrEmpty(dt.Rows[0]["LUONG_TP"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["LUONG_TP"]);
                SO_PHEP_HUONGTextEdit.EditValue = string.IsNullOrEmpty(dt.Rows[0]["SO_NGAY_PHEP"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["SO_NGAY_PHEP"]);
                NGAY_PHEP_CHUANTextEdit.EditValue = string.IsNullOrEmpty(dt.Rows[0]["NGAY_PHEP_CHUAN"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["NGAY_PHEP_CHUAN"]);
                NGAY_PHEP_COTextEdit.EditValue = string.IsNullOrEmpty(dt.Rows[0]["NGAY_PHEP_CO"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["NGAY_PHEP_CO"]);
                NGAY_PHEP_NGHITextEdit.EditValue = string.IsNullOrEmpty(dt.Rows[0]["NGAY_PHEP_NGHI"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["NGAY_PHEP_NGHI"]);
                TIEN_PHEPTextEdit.EditValue = string.IsNullOrEmpty(dt.Rows[0]["TIEN_PHEP"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["TIEN_PHEP"]);
                SoThangPhep = string.IsNullOrEmpty(dt.Rows[0]["SO_THANG_PHEP"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["SO_THANG_PHEP"]);
            }
            catch (Exception ex) { }
        }

        private void GetTienTroCap()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM dbo.GetTienTroCap('" + Convert.ToDateTime(NGAY_THOI_VIECDateEdit.EditValue).ToString("MM/dd/yyyy") + "'," + Convert.ToInt32(grvCongNhan.GetFocusedRowCellValue("ID_CN")) + "," + Convert.ToInt32(ID_LD_TVLookUpEdit.EditValue) + ")"));
                SO_NAM_TRO_CAPTextEdit.EditValue = string.IsNullOrEmpty(dt.Rows[0]["SO_NAM_TC"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["SO_NAM_TC"]);
                LUONG_TINH_TRO_CAPTextEdit.EditValue = string.IsNullOrEmpty(dt.Rows[0]["LUONG_TRO_CAP"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["LUONG_TRO_CAP"]);
                TIEN_TRO_CAPTextEdit.EditValue = string.IsNullOrEmpty(dt.Rows[0]["TIEN_TRO_CAP"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[0]["TIEN_TRO_CAP"]);
            }
            catch (Exception ex)
            {

            }
        }

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

        private void radChonXem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(radChonXem.SelectedIndex == 1)
            {
                ItemForTNgay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ItemForDNgay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                ItemForTNgay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                ItemForDNgay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }
    }
}
