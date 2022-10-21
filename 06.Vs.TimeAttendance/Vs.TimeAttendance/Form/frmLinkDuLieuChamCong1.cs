using DevExpress.DataAccess.Excel;
using DevExpress.Spreadsheet;
using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Vs.TimeAttendance
{
    public partial class frmLinkDuLieuChamCong1 : DevExpress.XtraEditors.XtraUserControl
    {
        private bool them = false;
        private bool bLinkOK = false;
        private int loaiLink = 1; // 1 Link bằng SQL, 2 Link bằng excel
        public static frmLinkDuLieuChamCong1 _instance;
        public static frmLinkDuLieuChamCong1 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new frmLinkDuLieuChamCong1();
                return _instance;
            }
        }
        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        public frmLinkDuLieuChamCong1()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { layoutControlGroup1 }, windowsUIButton);
            //Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1);
            repositoryItemTimeEdit1 = new RepositoryItemTimeEdit();
        }
        private void frmLinkDuLieuChamCong1_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);

                repositoryItemTimeEdit1.TimeEditStyle = TimeEditStyle.TouchUI;
                repositoryItemTimeEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                repositoryItemTimeEdit1.Mask.EditMask = "HH:mm:ss";

                repositoryItemTimeEdit1.NullText = "00:00:00";
                repositoryItemTimeEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                repositoryItemTimeEdit1.DisplayFormat.FormatString = "HH:mm:ss";
                repositoryItemTimeEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                repositoryItemTimeEdit1.EditFormat.FormatString = "HH:mm:ss";

                Commons.Modules.sLoad = "0Load";
                DataTable dt1 = new DataTable();
                dt1.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboDON_VI", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cbDonVi, dt1, "ID_DV", "TEN_DV", "TEN_DV");

                Commons.Modules.ObjSystems.LoadCboXiNghiep(cbDonVi, cbXiNghiep);
                Commons.Modules.ObjSystems.LoadCboTo(cbDonVi, cbXiNghiep, cbTo);
                Commons.Modules.sLoad = "";
                DateTime dt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
                dtNgayChamCong.EditValue = dt;
                Commons.OSystems.SetDateEditFormat(dtNgayChamCong);
                LoadLuoiNgay(dt);
                grdDSCN.DataSource = null;
                grvDSCN.RefreshData();
                enableButon(true);
                //lblTongCong.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTongSoCN") + "0";
                DateTime ngay = (DateTime)grvNgay.GetFocusedRowCellValue("NGAY");

                LoadGridCongNhan(dt);
            }
            catch (Exception ex)
            {
            }
        }

        #region Các hàm xử lý
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
            windowsUIButton.Buttons[8].Properties.Visible = visible;
            windowsUIButton.Buttons[9].Properties.Visible = visible;
            windowsUIButton.Buttons[10].Properties.Visible = visible;
            windowsUIButton.Buttons[11].Properties.Visible = visible;
            if (Commons.Modules.ObjSystems.DataThongTinChung().Rows[0]["KY_HIEU_DV"].ToString() == "DM")
            {
                windowsUIButton.Buttons[12].Properties.Visible = visible;
            }
            else
            {
                windowsUIButton.Buttons[12].Properties.Visible = false;
            }
            windowsUIButton.Buttons[13].Properties.Visible = visible;
            windowsUIButton.Buttons[14].Properties.Visible = visible;
            //      groupDanhSachKhoaHoc.Enabled = visible;
        }

        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn == null) return;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        try
                        {
                            them = true;
                            enableButon(false);
                            DateTime ngay = dtNgayChamCong.DateTime;
                            Int32 idcn = int.Parse(grvDSCN.GetFocusedRowCellValue("ID_CN").ToString());
                            LoadGridChamCong(ngay, idcn);
                            grvChamCong.AddNewRow();
                            Commons.Modules.ObjSystems.AddnewRow(grvChamCong, true);
                        }
                        catch
                        { }
                        break;
                    }
                case "sua":
                    {
                        try
                        {
                            them = true;
                            enableButon(false);
                            DateTime ngay = dtNgayChamCong.DateTime;
                            Int32 idcn = int.Parse(grvDSCN.GetFocusedRowCellValue("ID_CN").ToString());
                            LoadGridChamCong(ngay, idcn);
                        }
                        catch
                        { }
                        break;
                    }
                case "xoangay":
                    {
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_XoaDLngay"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                        System.Data.SqlClient.SqlConnection conn;
                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                        conn.Open();
                        string sLoad = "spDeleteDLChamCongNgay";
                        if (Commons.Modules.chamCongK == true) sLoad = "spDeleteDLChamCongNgay_K";
                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sLoad, conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UName", Commons.Modules.UserName);
                        cmd.Parameters.AddWithValue("@NNgu", Commons.Modules.TypeLanguage);
                        cmd.Parameters.AddWithValue("@DVi", cbDonVi.EditValue);
                        cmd.Parameters.AddWithValue("@XN", cbXiNghiep.EditValue);
                        cmd.Parameters.AddWithValue("@TO", cbTo.EditValue);
                        cmd.Parameters.AddWithValue("@Ngay", dtNgayChamCong.DateTime);
                        cmd.ExecuteNonQuery();
                        LoadLuoiNgay(dtNgayChamCong.DateTime);
                        LoadGridChamCong(dtNgayChamCong.DateTime, Convert.ToInt32(grvDSCN.GetFocusedRowCellValue("ID_CN").ToString()));

                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_XoaThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                        break;
                    }
                case "xoa":
                    {
                        Xoa();
                        //them = true;
                        //enableButon(false);
                        break;
                    }
                case "luu":
                    {
                        them = false;
                        enableButon(true);
                        if (saveChamCong() == false)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                        }
                        DateTime ngay = dtNgayChamCong.DateTime;
                        Int32 idcn = int.Parse(grvDSCN.GetFocusedRowCellValue("ID_CN").ToString());
                        LoadGridChamCong(ngay, idcn);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvChamCong);
                        break;
                    }
                case "khongluu":
                    {
                        try
                        {
                            them = false;
                            enableButon(true);
                            DateTime ngay = dtNgayChamCong.DateTime;
                            Int32 idcn = int.Parse(grvDSCN.GetFocusedRowCellValue("ID_CN").ToString());
                            LoadGridChamCong(ngay, idcn);
                            Commons.Modules.ObjSystems.DeleteAddRow(grvChamCong);
                        }
                        catch
                        { }

                        break;
                    }
                case "TongHopThongTin":
                    {
                        TongHopDuLieu();
                        LoadLuoiNgay(dtNgayChamCong.DateTime);
                        LoadGridCongNhan(dtNgayChamCong.DateTime);
                        XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_TongHopDL"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                case "LinkTay":
                    {
                        frmLinklBangTay frm = new frmLinklBangTay();
                        frm.ngaylink = dtNgayChamCong.DateTime;
                        frm.flag = 1;
                        frm.ShowDialog();
                        LoadLuoiNgay(dtNgayChamCong.DateTime);
                        break;
                    }
                case "LinkDuLieu":
                    {
                        loaiLink = 1;
                        LinkDuLieu();

                        if (bLinkOK)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_LinkThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_LinkKhongThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    }
                case "linkExcel":
                    {
                        loaiLink = 2;
                        LinkDuLieu();

                        if (bLinkOK)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_LinkThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_LinkKhongThanhCong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
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
        private void Xoa()
        {
            Int32 idcn = int.Parse(grvDSCN.GetFocusedRowCellValue("ID_CN").ToString());
            if (grvChamCong.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }

            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                string sSql = "";
                if (Commons.Modules.chamCongK == false)
                {
                    sSql = "DELETE dbo.DU_LIEU_QUET_THE WHERE ID_CN = " + idcn +
                                                        " AND NGAY = '"
                                                        + Convert.ToDateTime(dtNgayChamCong.EditValue).ToString("yyyy/MM/dd") +
                                                        "' AND CONVERT(nvarchar(10),GIO_DEN,108) = '"
                                                        + Convert.ToDateTime(grvChamCong.GetFocusedRowCellValue("GIO_DEN")).ToString("HH:mm:ss") + "'";
                }
                else
                {
                    sSql = "DELETE dbo.DU_LIEU_QUET_THE_K WHERE ID_CN = " + idcn +
                                                        " AND NGAY = '"
                                                        + Convert.ToDateTime(dtNgayChamCong.EditValue).ToString("yyyy/MM/dd") +
                                                        "' AND CONVERT(nvarchar(10),GIO_DEN,108) = '"
                                                        + Convert.ToDateTime(grvChamCong.GetFocusedRowCellValue("GIO_DEN")).ToString("HH:mm:ss") + "'";
                }
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                grvChamCong.DeleteSelectedRows();
            }
            catch
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
            }
        }
        private bool saveChamCong()
        {
            DataTable dataChamCong = new DataTable();

            string stbChamCong = "stbChamCong" + Commons.Modules.iIDUser;
            string sSql = "";
            Int32 idcn = int.Parse(grvDSCN.GetFocusedRowCellValue("ID_CN").ToString());
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbChamCong, Commons.Modules.ObjSystems.ConvertDatatable(grvChamCong), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveDuLieuQuetThe", stbChamCong, idcn, Convert.ToDateTime(dtNgayChamCong.EditValue), Commons.Modules.chamCongK);
                Commons.Modules.ObjSystems.XoaTable(stbChamCong);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                //Commons.Modules.ObjSystems.XoaTable(stbCongNhan);
                return false;
            }
        }
        #endregion

        //hàm link dữ liệu
        private void LinkDuLieu()
        {
            //tạo một table để chứa dữ liệu
            DataTable tbDLQT = new DataTable("DLQT");
            DataTable dtTTC = new DataTable(); // Lấy ký hiệu đơn vị trong thông tin chung

            dtTTC = Commons.Modules.ObjSystems.DataThongTinChung();
            switch (dtTTC.Rows[0]["KY_HIEU_DV"].ToString())
            {
                case "MT":
                    {
                        Int64 iIdCN = -1;
                        //kiem tra du lieu link da co chua
                        if (KiemDL())
                        {
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_KiemTraDuLieuLink"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                        }

                        if (NONN_TheoNhanVienCheckEdit.Checked)
                        {
                            iIdCN = Convert.ToInt64(grvDSCN.GetFocusedRowCellValue("ID_CN").ToString());
                        }

                        if (Commons.Modules.chamCongK == false)
                        {
                            tbDLQT.Columns.Add(new DataColumn("MS_THE_CC", typeof(string)));
                            tbDLQT.Columns.Add(new DataColumn("NGAY", typeof(DateTime)));
                            //load txt
                            using (OpenFileDialog openFileDialog = new OpenFileDialog())
                            {
                                //openFileDialog.InitialDirectory = "C:\\";
                                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                                openFileDialog.FilterIndex = 2;
                                openFileDialog.RestoreDirectory = true;
                                if (openFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    Convert1(openFileDialog.FileName, tbDLQT, "\t");
                                }
                                else
                                {
                                    bLinkOK = false;
                                    return;
                                }
                            }

                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("usp_InsertDLQT", conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@tableDLQT", tbDLQT);
                            cmd.Parameters.AddWithValue("@UName", Commons.Modules.UserName);
                            cmd.Parameters.AddWithValue("@NNgu", Commons.Modules.TypeLanguage);
                            cmd.Parameters.AddWithValue("@DVi", cbDonVi.EditValue);
                            cmd.Parameters.AddWithValue("@XN", cbXiNghiep.EditValue);
                            cmd.Parameters.AddWithValue("@TO", cbTo.EditValue);
                            cmd.Parameters.AddWithValue("@ID_CN", iIdCN);
                            cmd.Parameters.AddWithValue("@Ngay", dtNgayChamCong.DateTime);
                            cmd.ExecuteNonQuery();

                            if (KiemQuetTheLoi())
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_QuetTheLoi"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    Commons.Modules.bolLinkCC = true;
                                    Commons.Modules.dLinkCC = dtNgayChamCong.DateTime;
                                    frmVachTheLoi frm = new frmVachTheLoi();
                                    frm.ShowDialog();

                                    Commons.Modules.bolLinkCC = false;
                                }
                            }
                        }
                        else
                        {
                            LinkDL_KHACH();
                        }
                        LoadLuoiNgay(dtNgayChamCong.DateTime);
                        grvDSCN_FocusedRowChanged(null, null);
                        if (KiemDL())
                        {
                            bLinkOK = true;
                        }
                        else
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_KhongCoDuLieuLink"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            bLinkOK = false;
                        }
                        break;
                    }
                case "SB":
                    {
                        Int64 iIdCN = -1;
                        //kiem tra du lieu link da co chua
                        if (KiemDL())
                        {
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_KiemTraDuLieuLink"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                        }

                        if (NONN_TheoNhanVienCheckEdit.Checked)
                        {
                            iIdCN = Convert.ToInt64(grvDSCN.GetFocusedRowCellValue("ID_CN").ToString());
                        }

                        tbDLQT.Columns.Add(new DataColumn("MS_THE_CC", typeof(string)));
                        tbDLQT.Columns.Add(new DataColumn("NGAY", typeof(DateTime)));
                        //load txt
                        using (OpenFileDialog openFileDialog = new OpenFileDialog())
                        {
                            //openFileDialog.InitialDirectory = "C:\\";
                            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                            openFileDialog.FilterIndex = 2;
                            openFileDialog.RestoreDirectory = true;
                            if (openFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                Convert1(openFileDialog.FileName, tbDLQT, "\t");
                            }
                            else
                            {
                                bLinkOK = false;
                                return;
                            }
                        }

                        System.Data.SqlClient.SqlConnection conn;
                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                        conn.Open();

                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("usp_InsertDLQT", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tableDLQT", tbDLQT);
                        cmd.Parameters.AddWithValue("@UName", Commons.Modules.UserName);
                        cmd.Parameters.AddWithValue("@NNgu", Commons.Modules.TypeLanguage);
                        cmd.Parameters.AddWithValue("@DVi", cbDonVi.EditValue);
                        cmd.Parameters.AddWithValue("@XN", cbXiNghiep.EditValue);
                        cmd.Parameters.AddWithValue("@TO", cbTo.EditValue);
                        cmd.Parameters.AddWithValue("@ID_CN", iIdCN);
                        cmd.Parameters.AddWithValue("@Ngay", dtNgayChamCong.DateTime);
                        cmd.ExecuteNonQuery();

                        if (KiemQuetTheLoi())
                        {
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_QuetTheLoi"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                Commons.Modules.bolLinkCC = true;
                                Commons.Modules.dLinkCC = dtNgayChamCong.DateTime;
                                frmVachTheLoi frm = new frmVachTheLoi();
                                frm.ShowDialog();

                                Commons.Modules.bolLinkCC = false;
                            }
                        }

                        LoadLuoiNgay(dtNgayChamCong.DateTime);
                        grvDSCN_FocusedRowChanged(null, null);
                        if (KiemDL())
                        {
                            bLinkOK = true;
                        }
                        else
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_KhongCoDuLieuLink"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            bLinkOK = false;
                        }
                        break;
                    }
                case "DM":
                    {
                        Int64 iIdCN = -1;
                        //kiem tra du lieu link da co chua
                        if (KiemDL())
                        {
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_KiemTraDuLieuLink"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                        }

                        if (NONN_TheoNhanVienCheckEdit.Checked)
                        {
                            iIdCN = Convert.ToInt64(grvDSCN.GetFocusedRowCellValue("ID_CN").ToString());
                        }

                        tbDLQT.Columns.Add(new DataColumn("MS_THE_CC", typeof(string)));
                        tbDLQT.Columns.Add(new DataColumn("NGAY", typeof(DateTime)));
                        //load csdl
                        if (loaiLink == 1)
                        {
                            if(Convert.ToInt32(cbDonVi.EditValue) == 1)
                            {
                                Commons.Modules.connect = "Server=27.74.240.29;database=DATA_CHAM_CONG_DM1;uid=sa;pwd=codaikadaiku;Connect Timeout=9999;";
                            }
                            else
                            {
                                Commons.Modules.connect = "Server=27.74.240.29;database=DATA_CHAM_CONG_DM2;uid=sa;pwd=codaikadaiku;Connect Timeout=9999;";
                            }
                            tbDLQT.Load(SqlHelper.ExecuteReader(Commons.Modules.connect, CommandType.Text, "SELECT UserEnrollNumber AS MS_THE_CC,TimeStr  AS NGAY FROM dbo.CheckInOut WHERE CONVERT(NVARCHAR(13),TimeStr,103) = '" + dtNgayChamCong.Text + "'"));
                        }
                        else
                        {
                            #region LinkExcel
                            string sPath = "";
                            sPath = Commons.Modules.ObjSystems.OpenFiles("All Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|" + "All Files (*.*)|*.*");
                            string sBTNgay = "sBTNgay" + Commons.Modules.iIDUser;

                            if (sPath == "") return;
                            try
                            {
                                //Lấy đường dẫn
                                var source = new ExcelDataSource();
                                source.FileName = sPath;

                                //Lấy worksheet
                                Workbook workbook = new Workbook();
                                string ext = System.IO.Path.GetExtension(sPath);
                                if (ext.ToLower() == ".xlsx")
                                    workbook.LoadDocument(sPath, DevExpress.Spreadsheet.DocumentFormat.Xlsx);
                                else
                                    workbook.LoadDocument(sPath, DevExpress.Spreadsheet.DocumentFormat.Xls);
                                List<string> wSheet = new List<string>();
                                for (int i = 0; i < workbook.Worksheets.Count; i++)
                                {
                                    wSheet.Add(workbook.Worksheets[i].Name.ToString());
                                }
                                //Load worksheet
                                XtraInputBoxArgs args = new XtraInputBoxArgs();
                                // set required Input Box options
                                args.Caption = "Chọn sheet cần nhập dữ liệu";
                                args.Prompt = "Chọn sheet cần nhập dữ liệu";
                                args.DefaultButtonIndex = 0;

                                // initialize a DateEdit editor with custom settings
                                ComboBoxEdit editor = new ComboBoxEdit();
                                editor.Properties.Items.AddRange(wSheet);
                                editor.EditValue = wSheet[0].ToString();

                                args.Editor = editor;
                                // a default DateEdit value
                                args.DefaultResponse = wSheet[0].ToString();
                                // display an Input Box with the custom editor
                                var result = XtraInputBox.Show(args);
                                if (result == null || result.ToString() == "") return;

                                var worksheetSettings = new ExcelWorksheetSettings(result.ToString());
                                source.SourceOptions = new ExcelSourceOptions(worksheetSettings);
                                source.Fill();


                                DataTable dt = new DataTable();
                                dt = new DataTable();
                                dt = ToDataTable(source);
                                //dt.AsEnumerable().Where(x => x["NGAY"].ToString() == "" + dtNgayChamCong.EditValue + "").CopyToDataTable();
                                try
                                {
                                    dt = dt.AsEnumerable().Where(x => x["NGAY"].Equals(dtNgayChamCong.EditValue)).CopyToDataTable();
                                }
                                catch (Exception ex) { dt = dt.Clone(); }
                                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTNgay, dt, "");
                                tbDLQT.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdatetbDLQT", sBTNgay, Convert.ToInt32((dt.Columns.Count - 2) / 2)));
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message);
                                bLinkOK = false;
                                Commons.Modules.ObjSystems.XoaTable(sBTNgay);
                                return;
                            }
                            #endregion
                        }

                        System.Data.SqlClient.SqlConnection conn;
                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                        conn.Open();
                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(loaiLink == 1 ? "usp_InsertDLQT" : "usp_InsertDLQT_Excel", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tableDLQT", tbDLQT);
                        cmd.Parameters.AddWithValue("@UName", Commons.Modules.UserName);
                        cmd.Parameters.AddWithValue("@NNgu", Commons.Modules.TypeLanguage);
                        cmd.Parameters.AddWithValue("@DVi", cbDonVi.EditValue);
                        cmd.Parameters.AddWithValue("@XN", cbXiNghiep.EditValue);
                        cmd.Parameters.AddWithValue("@TO", cbTo.EditValue);
                        cmd.Parameters.AddWithValue("@ID_CN", iIdCN);
                        cmd.Parameters.AddWithValue("@Ngay", dtNgayChamCong.DateTime);
                        cmd.ExecuteNonQuery();

                        if (KiemQuetTheLoi())
                        {
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_QuetTheLoi"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                Commons.Modules.bolLinkCC = true;
                                Commons.Modules.dLinkCC = dtNgayChamCong.DateTime;
                                frmVachTheLoi frm = new frmVachTheLoi();
                                frm.ShowDialog();

                                Commons.Modules.bolLinkCC = false;
                            }
                        }

                        LoadLuoiNgay(dtNgayChamCong.DateTime);
                        grvDSCN_FocusedRowChanged(null, null);
                        if (KiemDL())
                        {
                            bLinkOK = true;
                        }
                        else
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_KhongCoDuLieuLink"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            bLinkOK = false;
                        }

                        break;
                    }

                //case 2:
                //    {
                //        //load access
                //        //Provider = Microsoft.Jet.OLEDB.4.0; Data Source = G:\READFILE\WiseEyeOn39.mdb; Persist Security Info = False; Jet OLEDB:Database Password = 12112009; Jet OLEDB:Compact Without Replica Repair = True
                //        string queryString = @"SELECT UserEnrollNumber as MS_THE_CC,TimeStr AS NGAY  FROM CheckInOut WHERE FORMAT(TimeStr,""dd/MM/yyyy"") = #" + dtNgayChamCong.Text + "#";
                //        using (OleDbConnection connection = new OleDbConnection(Commons.Modules.connect))
                //        using (OleDbCommand command = new OleDbCommand(queryString, connection))
                //        {
                //            try
                //            {
                //                connection.Open();
                //                OleDbDataReader reader = command.ExecuteReader();
                //                tbDLQT.Load(reader);
                //                reader.Close();
                //            }
                //            catch (Exception ex)
                //            {
                //                Console.WriteLine(ex.Message);
                //            }
                //        }
                //        break;
                //    }

                //case 3:
                //    {
                //        //load csdl
                //        string s = Commons.IConnections.CNStr;
                //        //Server=192.168.2.5;database=abriDBHRPro7;uid=sa;pwd=123;Connect Timeout=9999
                //        tbDLQT.Load(SqlHelper.ExecuteReader(Commons.Modules.connect, CommandType.Text, "SELECT EmployeeATID AS	MS_THE_CC,Time  AS NGAY FROM dbo.TA_TimeLog WHERE  CONVERT(NVARCHAR(13),tIME,103) = '" + dtNgayChamCong.Text + "'"));
                //        break;
                //    }
                default:
                    {
                        Int64 iIdCN = -1;
                        //kiem tra du lieu link da co chua
                        if (KiemDL())
                        {
                            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_KiemTraDuLieuLink"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                        }

                        if (NONN_TheoNhanVienCheckEdit.Checked)
                        {
                            iIdCN = Convert.ToInt64(grvDSCN.GetFocusedRowCellValue("ID_CN").ToString());
                        }

                        if (Commons.Modules.chamCongK == false)
                        {
                            tbDLQT.Columns.Add(new DataColumn("MS_THE_CC", typeof(string)));
                            tbDLQT.Columns.Add(new DataColumn("NGAY", typeof(DateTime)));
                            //load txt
                            using (OpenFileDialog openFileDialog = new OpenFileDialog())
                            {
                                //openFileDialog.InitialDirectory = "C:\\";
                                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                                openFileDialog.FilterIndex = 2;
                                openFileDialog.RestoreDirectory = true;
                                if (openFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    Convert1(openFileDialog.FileName, tbDLQT, "\t");
                                }
                                else
                                {
                                    bLinkOK = false;
                                    return;
                                }
                            }

                            System.Data.SqlClient.SqlConnection conn;
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("usp_InsertDLQT", conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@tableDLQT", tbDLQT);
                            cmd.Parameters.AddWithValue("@UName", Commons.Modules.UserName);
                            cmd.Parameters.AddWithValue("@NNgu", Commons.Modules.TypeLanguage);
                            cmd.Parameters.AddWithValue("@DVi", cbDonVi.EditValue);
                            cmd.Parameters.AddWithValue("@XN", cbXiNghiep.EditValue);
                            cmd.Parameters.AddWithValue("@TO", cbTo.EditValue);
                            cmd.Parameters.AddWithValue("@ID_CN", iIdCN);
                            cmd.Parameters.AddWithValue("@Ngay", dtNgayChamCong.DateTime);
                            cmd.ExecuteNonQuery();

                            if (KiemQuetTheLoi())
                            {
                                if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_QuetTheLoi"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    Commons.Modules.bolLinkCC = true;
                                    Commons.Modules.dLinkCC = dtNgayChamCong.DateTime;
                                    frmVachTheLoi frm = new frmVachTheLoi();
                                    frm.ShowDialog();

                                    Commons.Modules.bolLinkCC = false;
                                }
                            }
                        }
                        else
                        {
                            LinkDL_KHACH();
                        }

                        LoadLuoiNgay(dtNgayChamCong.DateTime);
                        grvDSCN_FocusedRowChanged(null, null);
                        if (KiemDL())
                        {
                            bLinkOK = true;
                        }
                        else
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_KhongCoDuLieuLink"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            bLinkOK = false;
                        }
                    }
                    break;
            }
        }
        private void LinkDL_KHACH()
        {
            try
            {
                int iLB = 0;
                if (NONN_LAM_BUCheckEdit.Checked)
                {
                    iLB = 1;
                }
                Int64 iIDCN = -1;
                if (NONN_TheoNhanVienCheckEdit.Checked)
                {
                    iIDCN = Convert.ToInt64(grvDSCN.GetFocusedRowCellValue("ID_CN").ToString());
                }
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spTaoChamCongKhach", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UName", Commons.Modules.UserName);
                cmd.Parameters.AddWithValue("@NNgu", Commons.Modules.TypeLanguage);
                cmd.Parameters.AddWithValue("@DVi", cbDonVi.EditValue);
                cmd.Parameters.AddWithValue("@XN", cbXiNghiep.EditValue);
                cmd.Parameters.AddWithValue("@TO", cbTo.EditValue);
                cmd.Parameters.AddWithValue("@CN", iIDCN);
                cmd.Parameters.AddWithValue("@TuNgay", dtNgayChamCong.DateTime);
                cmd.Parameters.AddWithValue("@DenNgay", dtNgayChamCong.DateTime);
                cmd.Parameters.AddWithValue("@SNNgay", Commons.Modules.iSNNgay);
                cmd.Parameters.AddWithValue("@SNTuan", Commons.Modules.iSNTuan);
                cmd.Parameters.AddWithValue("@SNTHANG", Commons.Modules.iSNThang);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        //ham tong hop du lieu
        private void TongHopDuLieu()
        {
            try
            {
                int iLB = 0;
                if (NONN_LAM_BUCheckEdit.Checked)
                {
                    iLB = 1;
                }
                Int64 iIDCN = -1;
                if (NONN_TheoNhanVienCheckEdit.Checked)
                {
                    iIDCN = Convert.ToInt64(grvDSCN.GetFocusedRowCellValue("ID_CN").ToString());
                }
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                conn.Open();
                string sLoad = "spTongHopDuLieu";
                if (Commons.Modules.chamCongK == true) sLoad = "spTongHopDuLieu_K";
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sLoad, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UName", Commons.Modules.UserName);
                cmd.Parameters.AddWithValue("@NNgu", Commons.Modules.TypeLanguage);
                cmd.Parameters.AddWithValue("@DVi", cbDonVi.EditValue);
                cmd.Parameters.AddWithValue("@XN", cbXiNghiep.EditValue);
                cmd.Parameters.AddWithValue("@TO", cbTo.EditValue);
                cmd.Parameters.AddWithValue("@CN", iIDCN);
                cmd.Parameters.AddWithValue("@LB", iLB);
                cmd.Parameters.AddWithValue("@LamTron", Commons.Modules.iLamTronGio);
                cmd.Parameters.AddWithValue("@Ngay", dtNgayChamCong.DateTime);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void Convert1(string File, DataTable dt, string delimiter)
        {
            StreamReader s = new StreamReader(File);
            string AllData = s.ReadToEnd();
            string[] rows = AllData.Split("\r\n".ToCharArray());
            foreach (string r in rows)
            {
                if (r.Trim() == "") continue;
                string[] items = r.Split(delimiter.ToCharArray());
                dt.Rows.Add(items[0], items[1]);
            }
        }

        #region Cac Ham kiem tra
        private bool KiemDL()
        {
            try
            {
                string sSql = "";
                if (NONN_TheoNhanVienCheckEdit.Checked)
                {
                    if (Commons.Modules.chamCongK == false)
                    {
                        sSql = "SELECT COUNT(ID_CN) FROM DU_LIEU_QUET_THE WHERE NGAY = '" + Convert.ToDateTime(dtNgayChamCong.EditValue).ToString("yyyyMMdd") +
                       "' AND ID_CN = " + Convert.ToInt32(grvDSCN.GetFocusedRowCellValue("ID_CN").ToString());
                    }
                    else
                    {
                        sSql = "SELECT COUNT(ID_CN) FROM DU_LIEU_QUET_THE_K WHERE NGAY = '" + Convert.ToDateTime(dtNgayChamCong.EditValue).ToString("yyyyMMdd") +
                       "' AND ID_CN = " + Convert.ToInt32(grvDSCN.GetFocusedRowCellValue("ID_CN").ToString());
                    }

                }
                else
                {
                    if (Commons.Modules.chamCongK == false)
                    {
                        sSql = "SELECT COUNT(T1.ID_CN) FROM DU_LIEU_QUET_THE T1 INNER JOIN (SELECT ID_CN FROM dbo.MGetListNhanSuToDate('" + Commons.Modules.UserName +
                        "', " + Commons.Modules.TypeLanguage + ", " + cbDonVi.EditValue + ", " + cbXiNghiep.EditValue + ", " + cbTo.EditValue +
                        ", '" + Convert.ToDateTime(dtNgayChamCong.EditValue).ToString("yyyyMMdd") + "')) T2 ON T1.ID_CN = T2.ID_CN " +
                        "WHERE T1.NGAY = '" + Convert.ToDateTime(dtNgayChamCong.EditValue).ToString("yyyyMMdd") + "'";
                    }
                    else
                    {
                        sSql = "SELECT COUNT(T1.ID_CN) FROM DU_LIEU_QUET_THE_K T1 INNER JOIN (SELECT ID_CN FROM dbo.MGetListNhanSuToDate('" + Commons.Modules.UserName +
                        "', " + Commons.Modules.TypeLanguage + ", " + cbDonVi.EditValue + ", " + cbXiNghiep.EditValue + ", " + cbTo.EditValue +
                        ", '" + Convert.ToDateTime(dtNgayChamCong.EditValue).ToString("yyyyMMdd") + "')) T2 ON T1.ID_CN = T2.ID_CN " +
                        "WHERE T1.NGAY = '" + Convert.ToDateTime(dtNgayChamCong.EditValue).ToString("yyyyMMdd") + "'";
                    }

                }

                if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql)) != 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                return true;
            }
            return false;
        }

        private bool KiemQuetTheLoi()
        {
            try
            {
                string sSql = "";
                if (NONN_TheoNhanVienCheckEdit.Checked)
                {
                    sSql = "SELECT COUNT(ID_CN) FROM DU_LIEU_QUET_THE WHERE NGAY = '" + Convert.ToDateTime(dtNgayChamCong.EditValue).ToString("yyyyMMdd") +
                       "' AND ID_CN = " + Convert.ToInt32(grvDSCN.GetFocusedRowCellValue("ID_CN").ToString()) + " AND PHUT_DEN = PHUT_VE";
                }
                else
                {
                    sSql = "SELECT COUNT(T1.ID_CN) FROM DU_LIEU_QUET_THE T1 INNER JOIN (SELECT ID_CN FROM dbo.MGetListNhanSuToDate('" + Commons.Modules.UserName +
                        "', " + Commons.Modules.TypeLanguage + ", " + cbDonVi.EditValue + ", " + cbXiNghiep.EditValue + ", " + cbTo.EditValue +
                        ", '" + Convert.ToDateTime(dtNgayChamCong.EditValue).ToString("yyyyMMdd") + "')) T2 ON T1.ID_CN = T2.ID_CN " +
                        "WHERE T1.NGAY = '" + Convert.ToDateTime(dtNgayChamCong.EditValue).ToString("yyyyMMdd") + "' AND T1.PHUT_DEN = T1.PHUT_VE";
                }

                if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql)) != 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                return true;
            }
            return false;
        }
        #endregion


        #region Cac Ham Load Tab 1
        private void LoadLuoiNgay(DateTime dtLoad)
        {
            try
            {
                DateTime tn = dtLoad;
                tn = tn.AddDays(-tn.Day + 1);
                DateTime dn = tn.AddMonths(1);
                dn = dn.AddDays(-1);

                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetDuLieuQuetThe", tn, dn, Commons.Modules.UserName, Commons.Modules.TypeLanguage, cbDonVi.EditValue, cbXiNghiep.EditValue, cbTo.EditValue, Commons.Modules.chamCongK));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["NGAY"] };
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay, dt, false, false, true, true, true, this.Name);
                grvNgay.Columns["NGAY"].Visible = true;
                grvNgay.Columns["NGAY"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                grvNgay.Columns["NGAY"].DisplayFormat.FormatString = "dd/MM/yyyy";

                
                grvNgay.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
                grvNgay.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                grvNgay.OptionsSelection.CheckBoxSelectorField = "TH";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                //return true;
            }
        }

        private void LoadGridCongNhan(DateTime dtNgay)
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetDuLieuQuetTheCN", dtNgay, Commons.Modules.UserName, Commons.Modules.TypeLanguage, cbDonVi.EditValue, cbXiNghiep.EditValue, cbTo.EditValue));
                dt.Columns["ID_CN"].ReadOnly = false;
                dt.Columns["HO_TEN"].ReadOnly = true;
                dt.Columns["MS_THE_CC"].ReadOnly = true;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDSCN, grvDSCN, dt, false, false, true, true, true, this.Name);
                grvDSCN.Columns["ID_CN"].Visible = false;
                grvDSCN.Columns["GIO_LV"].DisplayFormat.FormatType = FormatType.Numeric;
                grvDSCN.Columns["GIO_LV"].DisplayFormat.FormatString = "0.00;(0.00);''";
                grvDSCN.Columns["GIO_TC"].DisplayFormat.FormatType = FormatType.Numeric;
                grvDSCN.Columns["GIO_TC"].DisplayFormat.FormatString = "0.00;(0.00);''";
                grvDSCN.RefreshData();
            }
            catch
            {

            }
            //try
            //{
            //    lblTongCong.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTongSoCN") + dt.Rows.Count.ToString();
            //}
            //catch { }
        }

        private void LoadGridChamCong(DateTime dtNgay, int idCN)
        {

            try
            {
                DataTable dt = new DataTable();
                if (them)
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetDuLieuQuetTheCNCT", dtNgay, idCN, Commons.Modules.chamCongK));
                    dt.Columns["CHINH_SUA"].ReadOnly = true;
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChamCong, grvChamCong, dt, true, false, true, true, true, this.Name);
                    DataTable dID_NHOM = new DataTable();
                    dID_NHOM.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetNhomCC", dtNgayChamCong.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    Commons.Modules.ObjSystems.AddCombXtra("ID_NHOM", "TEN_NHOM", grvChamCong, dID_NHOM, false, "ID_NHOM", "NHOM_CHAM_CONG");
                    dt.Columns["ID_NHOM"].ReadOnly = false;
                }
                else
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetDuLieuQuetTheCNCT", dtNgay, idCN, Commons.Modules.chamCongK));
                    dt.Columns["CHINH_SUA"].ReadOnly = true;
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdChamCong, grvChamCong, dt, false, false, true, true, true, this.Name);
                    DataTable dID_NHOM = new DataTable();
                    dID_NHOM.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetNhomCC", dtNgayChamCong.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    Commons.Modules.ObjSystems.AddCombXtra("ID_NHOM", "TEN_NHOM", grvChamCong, dID_NHOM, false, "ID_NHOM", "NHOM_CHAM_CONG");
                    //do nothing;
                }

                RepositoryItemDateEdit dEditN = new RepositoryItemDateEdit();
                dEditN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dEditN.DisplayFormat.FormatString = "dd/MM/yyyy";
                dEditN.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dEditN.EditFormat.FormatString = "dd/MM/yyyy";
                dEditN.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                dEditN.Mask.EditMask = "dd/MM/yyyy";
                grvChamCong.Columns["NGAY_DEN"].ColumnEdit = dEditN;
                grvChamCong.Columns["NGAY_VE"].ColumnEdit = dEditN;

                grvChamCong.Columns["GIO_VE"].DisplayFormat.FormatType = FormatType.DateTime;
                grvChamCong.Columns["GIO_VE"].DisplayFormat.FormatString = "HH:mm:ss";
                grvChamCong.Columns["GIO_DEN"].DisplayFormat.FormatType = FormatType.DateTime;
                grvChamCong.Columns["GIO_DEN"].DisplayFormat.FormatString = "HH:mm:ss";
                grvChamCong.Columns["GIO_DEN"].ColumnEdit = this.repositoryItemTimeEdit1;
                grvChamCong.Columns["GIO_VE"].ColumnEdit = this.repositoryItemTimeEdit1;


                RepositoryItemSearchLookUpEdit cbo = new RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_XNG", "TEN_XNG", "ID_XNG", grvChamCong, Commons.Modules.ObjSystems.DataXacNhanGio(false), this.Name);

                grvChamCong.RefreshData();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        private void cbDonVi_EditValueChanged(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cbDonVi, cbXiNghiep);
        }

        private void cbXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            Commons.Modules.ObjSystems.LoadCboTo(cbDonVi, cbXiNghiep, cbTo);
        }

        private void cbTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            grdDSCN.DataSource = null;
            grvDSCN.RefreshData();
            //lblTongCong.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTongSoCN") + "0";
            try
            {
                //DateTime ngay = (DateTime)grvNgay.GetFocusedRowCellValue("NGAY");
                LoadLuoiNgay(dtNgayChamCong.DateTime);
                LoadGridCongNhan(dtNgayChamCong.DateTime);
            }
            catch
            {

            }
            Commons.Modules.sLoad = "";

        }

        private void dtNgayChamCong_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad != "0Load")
            {
                LoadLuoiNgay(dtNgayChamCong.DateTime);
            }
            else
            {
                Commons.Modules.sLoad = "";
            }
            grdDSCN.DataSource = null;
            grvDSCN.RefreshData();
            //lblTongCong.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTongSoCN") + "0";
            try
            {
                LoadGridCongNhan(dtNgayChamCong.DateTime);
            }
            catch (Exception ex)
            {

            }
            Commons.Modules.sLoad = "";
        }

        private void grvDSCN_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                grdChamCong.DataSource = null;
                grvChamCong.RefreshData();
                DateTime ngay = dtNgayChamCong.DateTime;
                Int32 idcn = int.Parse(grvDSCN.GetFocusedRowCellValue("ID_CN").ToString());
                LoadGridChamCong(ngay, idcn);
                loadCa();
            }
            catch
            {

            }
        }
        private void loadCa()
        {
            DataTable dCa = new DataTable();
            RepositoryItemLookUpEdit cboCa = new RepositoryItemLookUpEdit();
            dCa.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT DISTINCT ID_CDLV, CA, GIO_BD, GIO_KT, PHUT_BD, PHUT_KT " +
                                             " FROM CHE_DO_LAM_VIEC"));
            cboCa.NullText = "";
            cboCa.ValueMember = "CA";
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

            cboCa.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PHUT_BD"));
            cboCa.Columns["PHUT_BD"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "PHUT_BD");
            cboCa.Columns["PHUT_BD"].Visible = false;

            cboCa.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PHUT_KT"));
            cboCa.Columns["PHUT_KT"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "PHUT_KT");
            cboCa.Columns["PHUT_KT"].Visible = false;

            cboCa.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_CDLV"));
            cboCa.Columns["ID_CDLV"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "ID_CDLV");
            cboCa.Columns["ID_CDLV"].Visible = false;

            cboCa.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            cboCa.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            grvChamCong.Columns["CA"].ColumnEdit = cboCa;
            cboCa.BeforePopup += cboCa_BeforePopup;
            cboCa.EditValueChanged += CboCa_EditValueChanged;
        }
        private void CboCa_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;

            //string id = lookUp.get;

            // Access the currently selected data row
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;

            grvChamCong.SetFocusedRowCellValue("CA", (dataRow.Row[1]));
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
                dtCaLV.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCaLV", dtNgayChamCong.EditValue, grvChamCong.GetFocusedRowCellValue("ID_NHOM"), Commons.Modules.UserName, Commons.Modules.TypeLanguage));

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
        private void grdNgay_DoubleClick(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            DateTime ngay = (DateTime)grvNgay.GetFocusedRowCellValue("NGAY");
            dtNgayChamCong.EditValue = ngay;
            grdDSCN.DataSource = null;
            grvDSCN.RefreshData();
            LoadGridCongNhan(ngay);
        }

        private void grvChamCong_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            grvChamCong.SetFocusedRowCellValue("NGAY_DEN", dtNgayChamCong.EditValue);
            grvChamCong.SetFocusedRowCellValue("NGAY_VE", dtNgayChamCong.EditValue);
            grvChamCong.SetFocusedRowCellValue("GIO_DEN", dtNgayChamCong.EditValue);
        }

        private void grvDSCN_RowCountChanged(object sender, EventArgs e)
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
            grvDSCN_FocusedRowChanged(null, null);
        }
        public DataTable ToDataTable(ExcelDataSource excelDataSource)
        {
            DevExpress.DataAccess.Native.Excel.DataView dv_temp = ((IListSource)excelDataSource).GetList() as DevExpress.DataAccess.Native.Excel.DataView;

            excelDataSource.SourceOptions = new CsvSourceOptions() { CellRange = "A3:" + CharacterIncrement(dv_temp.Columns.Count - 1) + "" + dv_temp.Count + "" };
            excelDataSource.SourceOptions.SkipEmptyRows = false;
            excelDataSource.SourceOptions.UseFirstRowAsHeader = true;
            excelDataSource.Fill();
            DevExpress.DataAccess.Native.Excel.DataView dv = ((IListSource)excelDataSource).GetList() as DevExpress.DataAccess.Native.Excel.DataView;
            for (int i = 0; i < dv.Count; i++)
            {
                DevExpress.DataAccess.Native.Excel.ViewRow row = dv[i] as DevExpress.DataAccess.Native.Excel.ViewRow;
                foreach (DevExpress.DataAccess.Native.Excel.ViewColumn col in dv.Columns)
                {
                    object val = col.GetValue(row);
                }
            }

            IList list = ((IListSource)excelDataSource).GetList();
            DevExpress.DataAccess.Native.Excel.DataView dataView = (DevExpress.DataAccess.Native.Excel.DataView)list;
            List<PropertyDescriptor> props = dataView.Columns.ToList<PropertyDescriptor>();
            DataTable table = new DataTable();
            int cotGio = 6;
            int soCot = 1; // số cột giờ vào giờ ra
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                string sTenCot = "";
                switch (i)
                {
                    case 0:
                        {
                            sTenCot = "MS_CN";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    case 4:
                        {
                            sTenCot = "NGAY";
                            table.Columns.Add(sTenCot.Trim(), prop.PropertyType);
                            break;
                        }
                    default:
                        {
                            if (cotGio == i)
                            {
                                sTenCot = "GV_" + soCot + "";
                                table.Columns.Add(sTenCot.Trim(), typeof(DateTime));
                                cotGio++;
                                i++;
                                sTenCot = "GR_" + soCot + "";
                                table.Columns.Add(sTenCot.Trim(), typeof(DateTime));
                                soCot++;
                                cotGio++;
                            }
                            break;
                        }
                }
            }
            object[] values = new object[props.Count - 4];
            int sCot = 0;
            foreach (DevExpress.DataAccess.Native.Excel.ViewRow item in list)
            {
                for (int i = 0; i < values.Length + 4; i++)
                {
                    if (props[i] != props[1] && props[i] != props[2] && props[i] != props[3] && props[i] != props[5])
                    {
                        if (props[i] != props[0] && props[i] != props[4])
                        {
                            values[sCot] = dtNgayChamCong.Text + " " + (props[i].GetValue(item) == "" ? DateTime.MinValue.TimeOfDay : Convert.ToDateTime(props[i].GetValue(item)).TimeOfDay);
                        }
                        else
                        {
                            values[sCot] = props[i].GetValue(item);
                        }
                        sCot++;
                    }
                }
                table.Rows.Add(values);
                sCot = 0;
            }
            return table;
        }
        static string CharacterIncrement(int colCount)
        {
            int TempCount = 0;
            string returnCharCount = string.Empty;

            if (colCount <= 25)
            {
                TempCount = colCount;
                char CharCount = Convert.ToChar((Convert.ToInt32('A') + TempCount));
                returnCharCount += CharCount;
                return returnCharCount;
            }
            else
            {
                var rev = 0;

                while (colCount >= 26)
                {
                    colCount = colCount - 26;
                    rev++;
                }

                returnCharCount += CharacterIncrement(rev - 1);
                returnCharCount += CharacterIncrement(colCount);
                return returnCharCount;
            }
        }

        private void grvDSCN_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(grvDSCN.GetRowCellValue(e.RowHandle, grvDSCN.Columns["CHINH_SUA"].FieldName)) == false) return;
                e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#CC99FF");
                e.HighPriority = true;
            }
            catch
            {

            }
        }

        private void grvDSCN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F3)
            {
                frmViewDataGoc frm = new frmViewDataGoc();
                DateTime dNgay = DateTime.Now.Date;
                try { dNgay = dtNgayChamCong.DateTime; } catch { }

                Int64 iIDCN = -1;
                try { iIDCN = Int64.Parse(grvDSCN.GetFocusedRowCellValue("ID_CN").ToString()); } catch { }
                frm.iIDCN = iIDCN;
                frm.dNgayCC = dNgay;
                frm.ShowDialog();
                
            }
        }
    }
}