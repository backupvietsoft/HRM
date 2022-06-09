using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;

namespace Vs.HRM
{
    public partial class ucTroCapBHXH : DevExpress.XtraEditors.XtraUserControl
    {
        string sbtDCTroCapBHXH = "tabDCTroCapBHXH" + Commons.Modules.UserName;
        string sbtChonDCTroCapBHXH = "tabChonDCTroCapBHXH" + Commons.Modules.UserName;
        bool val = true;
        public ucTroCapBHXH()
        {
            InitializeComponent();
        }
        private void ucTroCapBHXH_Load(object sender, EventArgs e)
        {
            Commons.Modules.sPS = "0Load";
            LoadData();
            Commons.Modules.sPS = "";
            LoadGrdTroCapBHXH();
            LoadGrdDCTroCapBHXH(false);

            enableButon(true);
        }

        #region function chung
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = !visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
            windowsUIButton.Buttons[6].Properties.Visible = visible;
            windowsUIButton.Buttons[7].Properties.Visible = false;

            ThangdateEdit.ReadOnly = !visible;
            spinDot.ReadOnly = !visible;
        }

        private void LoadData()
        {

            //ItemForDateThang.Visibility = LayoutVisibility.Never;
            DataTable dtthang = new DataTable();
            string sSql = "SELECT DISTINCT LEFT(THANG,2) AS M,RIGHT(THANG,4) AS Y, LEFT(THANG,2) + '/' + RIGHT(THANG,4) AS Thang FROM dbo.TRO_CAP_BHXH ORDER BY Y DESC , M DESC";
            dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
            //Commons.Modules.ObjSystems.MLoadLookUpEdit(ThangLookUpEdit, dtthang, "Thang", "Thang", "Thang");

            Commons.Modules.ObjSystems.MLoadLookUpEdit(ThangLookUpEdit1, dtthang, "Thang", "Thang", "Thang");

            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT MAX(NGHI_TU_NGAY),DOT FROM dbo.TRO_CAP_BHXH GROUP BY DOT"));
            ThangdateEdit.EditValue = Convert.ToDateTime(dt.Rows[0][0]).ToString("MM/yyyy");
            spinDot.EditValue = Convert.ToInt32(dt.Rows[0][1]);
            spinDot1.EditValue = Convert.ToInt32(dt.Rows[0][1]);
        }
        private void AddnewRow(GridView view)
        {
            view.OptionsBehavior.Editable = true;
            view.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
        }
        private void DeleteAddRow(GridView view)
        {
            view.OptionsBehavior.Editable = false;
            view.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            view.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "chon":
                    {
                        //ItemForThang.Visibility = LayoutVisibility.Never;
                        //ItemForDateThang.Visibility = LayoutVisibility.Always;
                        navigationFrame1.SelectedPage = navigationPage2;
                        enableButon(false);
                        windowsUIButton.Buttons[3].Properties.Visible = false;
                        windowsUIButton.Buttons[5].Properties.Visible = false;
                        windowsUIButton.Buttons[6].Properties.Visible = false;
                        windowsUIButton.Buttons[7].Properties.Visible = true;
                        //tạo bảng tạm trợ cấp bảo hiễm xã hội
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbtDCTroCapBHXH, Commons.Modules.ObjSystems.ConvertDatatable(grdDCTroCapBHXH), "");
                        LoadGrdChonTroCapBHXH();
                        break;
                    }
                case "trove":
                    {
                        navigationFrame1.SelectedPage = navigationPage1;
                        enableButon(false);
                        break;
                    }
                case "sua":
                    {
                        AddnewRow(grvTroCapBHXH);
                        AddnewRow(grvDCTroCapBHXH);
                        //ItemForThang.Visibility = LayoutVisibility.Never;
                        //ItemForDateThang.Visibility = LayoutVisibility.Always;
                        enableButon(false);
                        break;
                    }
                case "xoa":
                    {
                        XoaTroCapBaoHiemXH();
                        break;
                    }
                case "luu":
                    {
                        if (navigationFrame1.SelectedPage == navigationPage1)
                        {
                            LuuTroCapBaoHiem();
                            LuuDCTroCapBaoHiem();
                            DeleteAddRow(grvTroCapBHXH);
                            DeleteAddRow(grvDCTroCapBHXH);
                            //ItemForThang.Visibility = LayoutVisibility.Always;
                            //ItemForDateThang.Visibility = LayoutVisibility.Never;
                            enableButon(true);
                        }
                        else
                        {
                            //lấy dữ liệu được chọn insert vào bảng tạm điều chỉnh điều chỉnh(tabDCTroCapBHXH)
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbtChonDCTroCapBHXH, Commons.Modules.ObjSystems.ConvertDatatable(grvChonTroCapBHXH), "");

                            string sSql = "INSERT INTO dbo." + sbtDCTroCapBHXH + " (ID_HTC, MS_CN, ID_CN, ID_LDV,PHAN_TRAM_TRO_CAP, THANG, THANG_CHUYEN, DOT, DOT_CHUYEN, NGHI_TU_NGAY, NGHI_DEN_NGAY, SO_NGAY_NGHI, MUC_HUONG_CU)SELECT -1,MS_CN,ID_CN,ID_LDV,PHAN_TRAM_TRO_CAP,'" + ThangdateEdit.EditValue + "','" + ThangLookUpEdit1.EditValue + "'," + spinDot.EditValue + "," + spinDot1.EditValue + ",NGHI_TU_NGAY,NGHI_DEN_NGAY,SO_NGAY_NGHI,SO_TIEN_TC FROM dbo." + sbtChonDCTroCapBHXH + " A WHERE NOT EXISTS(SELECT * FROM dbo." + sbtDCTroCapBHXH + " B WHERE A.ID_CN = B.ID_CN AND A.NGHI_TU_NGAY = B.NGHI_TU_NGAY) AND A.CHON = 1 DELETE dbo." + sbtDCTroCapBHXH + "  WHERE  EXISTS (SELECT * FROM dbo." + sbtChonDCTroCapBHXH + " B WHERE dbo." + sbtDCTroCapBHXH + ".ID_CN = B.ID_CN AND dbo." + sbtDCTroCapBHXH + ".NGHI_TU_NGAY =B.NGHI_TU_NGAY AND B.CHON = 0)";


                            // xong rồi load lại lưới điều chỉnh theo bảng tạm vào xóa table chọn
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                            Commons.Modules.ObjSystems.XoaTable(sbtChonDCTroCapBHXH);
                            LoadGrdDCTroCapBHXH(true);
                            AddnewRow(grvDCTroCapBHXH);
                            navigationFrame1.SelectedPage = navigationPage1;
                            enableButon(false);
                        }
                        break;
                    }

                case "khongluu":
                    {
                        LoadGrdTroCapBHXH();
                        LoadGrdDCTroCapBHXH(false);
                        DeleteAddRow(grvTroCapBHXH);
                        DeleteAddRow(grvDCTroCapBHXH);
                        //ItemForThang.Visibility = LayoutVisibility.Always;
                        //ItemForDateThang.Visibility = LayoutVisibility.Never;
                        enableButon(true);
                        break;
                    }
                case "thoat":
                    {
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgBanCoMuonThoatChuongtrinh"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeThoat"), MessageBoxButtons.YesNo) == DialogResult.No) return;
                        Application.Exit();
                        break;
                    }
                default:
                    break;
            }
        }

        #endregion

        #region trợ cấp bảo hiểm y tế
        private void LoadGrdTroCapBHXH()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListTroCapBHXH", ThangdateEdit.EditValue, spinDot.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dt.Columns["SO_NGAYLK"].ReadOnly = false;
                dt.Columns["LUONG_TINH_TC"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdTroCapBHXH, grvTroCapBHXH, dt, false, false, false, true, true, this.Name);
                Commons.Modules.ObjSystems.AddCombXtra("ID_CN", "TEN_CN", grvTroCapBHXH, "spGetCongNhan");
                Commons.Modules.ObjSystems.AddCombo("ID_HTNTC", "NOI_DUNG", grvTroCapBHXH, Commons.Modules.ObjSystems.DataHinhThucTroCap(false));
                Commons.Modules.ObjSystems.AddCombo("ID_LDV", "TEN_LDV", grvTroCapBHXH, Commons.Modules.ObjSystems.DataLyDoVang(false));

                grvTroCapBHXH.Columns["PHAN_TRAM_TRO_CAP"].OptionsColumn.ReadOnly = true;
                grvTroCapBHXH.Columns["SO_NGAY_NGHI"].OptionsColumn.ReadOnly = true;
                grvTroCapBHXH.Columns["MS_CN"].OptionsColumn.ReadOnly = true;
                grvTroCapBHXH.Columns["SO_NGAYLK"].OptionsColumn.ReadOnly = true;
                grvTroCapBHXH.Columns["SO_TIEN_TC"].OptionsColumn.ReadOnly = true;

                grvTroCapBHXH.Columns["MS_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvTroCapBHXH.Columns["ID_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvTroCapBHXH.Columns["ID_HTNTC"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvTroCapBHXH.Columns["ID_LDV"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;


                grvTroCapBHXH.Columns["MS_CN"].Width = 100;
                grvTroCapBHXH.Columns["ID_CN"].Width = 200;
                grvTroCapBHXH.Columns["ID_HTNTC"].Width = 200;
                grvTroCapBHXH.Columns["ID_LDV"].Width = 200;

                grvTroCapBHXH.Columns["ID_TC_BHXH"].Visible = false;
                grvTroCapBHXH.Columns["LAN_TS"].Visible = false;
                grvTroCapBHXH.Columns["HS_LUONG"].Visible = false;
                grvTroCapBHXH.Columns["LUONG_CB"].Visible = false;

                grvTroCapBHXH.Columns["SO_TIEN_TC"].DisplayFormat.FormatType = FormatType.Numeric;
                grvTroCapBHXH.Columns["SO_TIEN_TC"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;

                grvTroCapBHXH.Columns["LUONG_TINH_TC"].DisplayFormat.FormatType = FormatType.Numeric;
                grvTroCapBHXH.Columns["LUONG_TINH_TC"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
            }
            catch { }
        }
        private void LuuTroCapBaoHiem()
        {
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "tabTroCapBHXH" + Commons.Modules.UserName, Commons.Modules.ObjSystems.ConvertDatatable(grvTroCapBHXH), "");
                string sSql = "UPDATE A SET A.ID_CN = B.ID_CN,A.ID_LDV = B.ID_LDV,A.ID_HTNTC = B.ID_HTNTC,A.NGHI_TU_NGAY = B.NGHI_TU_NGAY,A.NGHI_DEN_NGAY = B.NGHI_DEN_NGAY,A.THONG_TIN_TK = B.THONG_TIN_TK,A.CHI_TIEU_XAC_DINH = B.CHI_TIEU_XAC_DINH,A.SO_NGAY_NGHI = B.SO_NGAY_NGHI,A.SO_TIEN_TC = B.SO_TIEN_TC,A.SO_CON_SINH = B.SO_CON_SINH FROM dbo.TRO_CAP_BHXH A INNER JOIN tabTroCapBHXH" + Commons.Modules.UserName + " B ON B.ID_TC_BHXH = A.ID_TC_BHXH INSERT INTO dbo.TRO_CAP_BHXH(ID_CN, ID_LDV, ID_HTNTC, DOT, THANG, NGHI_TU_NGAY, NGHI_DEN_NGAY, THONG_TIN_TK, CHI_TIEU_XAC_DINH, SO_NGAY_NGHI, HS_LUONG, LUONG_CB, SO_TIEN_TC, SO_CON_SINH, LAN_TS)SELECT ID_CN, ID_LDV, ID_HTNTC," + spinDot.EditValue + ",'" + ThangdateEdit.DateTime.ToString("MM/yyyy") + "',NGHI_TU_NGAY,NGHI_DEN_NGAY,THONG_TIN_TK,CHI_TIEU_XAC_DINH,SO_NGAY_NGHI,HS_LUONG,LUONG_CB,SO_TIEN_TC,SO_CON_SINH,LAN_TS FROM tabTroCapBHXH" + Commons.Modules.UserName + " WHERE ID_TC_BHXH NOT IN(SELECT ID_TC_BHXH FROM dbo.TRO_CAP_BHXH)";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                Commons.Modules.ObjSystems.XoaTable("tabTroCapBHXH" + Commons.Modules.UserName);

            }
            catch (Exception exx)
            {

            }
        }
        private void grvTroCapBHXH_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            grvTroCapBHXH.ClearColumnErrors();
            DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            DevExpress.XtraGrid.Columns.GridColumn ID_CN = View.Columns["ID_CN"];
            DevExpress.XtraGrid.Columns.GridColumn NGHI_TU_NGAY = View.Columns["NGHI_TU_NGAY"];
            DevExpress.XtraGrid.Columns.GridColumn NGHI_DEN_NGAY = View.Columns["NGHI_DEN_NGAY"];
            //kiểm tra dữ liệu trùng
            DataTable tempt = Commons.Modules.ObjSystems.ConvertDatatable(grvTroCapBHXH);
            int n = 0;
            try
            {
                string sSql = "SELECT dbo.fuKiemTraTroCapBHXH(" + View.GetRowCellValue(e.RowHandle, ID_CN) + ",'" + Convert.ToDateTime(View.GetRowCellValue(e.RowHandle, NGHI_TU_NGAY)).ToString("MM/dd/yyyy") + "','" + Convert.ToDateTime(View.GetRowCellValue(e.RowHandle, NGHI_DEN_NGAY)).ToString("MM/dd/yyyy") + "'," + ThangdateEdit.DateTime.Month + "," + ThangdateEdit.DateTime.Year + ")";
                n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql));
            }
            catch
            { }
            if (n == 0)
            {
                //kiểm tra từ ngày dến ngày trên lưới
                DateTime tn = Convert.ToDateTime(View.GetRowCellValue(e.RowHandle, NGHI_TU_NGAY));
                DateTime dn = Convert.ToDateTime(View.GetRowCellValue(e.RowHandle, NGHI_DEN_NGAY));
                n = tempt.AsEnumerable().Count(x => x.Field<Int64>("ID_CN") == Convert.ToInt64(View.GetRowCellValue(e.RowHandle, ID_CN)) && x.Field<DateTime>("NGHI_TU_NGAY") > tn && x.Field<DateTime>("NGHI_DEN_NGAY") < dn);
                if (n == 1)
                {
                    e.Valid = false;
                    View.SetColumnError(ID_CN, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgTrungDuLieu", Commons.Modules.TypeLanguage));
                    View.SetColumnError(NGHI_TU_NGAY, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgTrungDuLieu", Commons.Modules.TypeLanguage));
                    val = false;
                    return;
                }
            }
        }

        private bool kiemtraNgayHopLe(DateTime ngay, DataTable dt, int idcn)
        {
            bool resulst = false;
            string sSql = "SELECT count(*) FROM dbo.TRO_CAP_BHXH WHERE  NGHI_TU_NGAY <= '" + ngay.ToString("MM/dd/yyyy") + "' AND  NGHI_DEN_NGAY > '" + ngay.ToString("MM/dd/yyyy") + "' AND ID_CN = " + idcn + " AND LEFT(THANG, 2) < " + ThangdateEdit.DateTime.Month + " AND RIGHT(THANG,4) = " + ThangdateEdit.DateTime.Year + "";
            int n = 0;
            n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql));
            if (n == 0)
            {
                n = dt.AsEnumerable().Count(x => x.Field<DateTime>("NGHI_TU_NGAY") <= ngay && x.Field<DateTime>("NGHI_DEN_NGAY") > ngay);
                if (n > 1)
                {
                    resulst = false;
                }
                else
                    resulst = true;
            }
            else
            {
                resulst = false;
            }
            return resulst;
        }

        private void grvTroCapBHXH_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                string sSql = "";
                GridView view = sender as GridView;
                GridColumn colTuNgay = view.Columns["NGHI_TU_NGAY"];
                GridColumn colDenNgay = view.Columns["NGHI_DEN_NGAY"];
                if (view == null) return;
                view.ClearColumnErrors();



                if (e.Column.Name == "colID_CN")
                {
                    sSql = "SELECT MS_CN FROM dbo.CONG_NHAN WHERE ID_CN = " + view.GetRowCellValue(e.RowHandle, view.Columns["ID_CN"]) + "";
                    string s = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString();
                    view.SetRowCellValue(e.RowHandle, view.Columns["MS_CN"], s);
                }
                if (e.Column.Name == "colID_LDV")
                {
                    sSql = "SELECT PHAN_TRAM_TRO_CAP FROM dbo.LY_DO_VANG WHERE ID_LDV = " + view.GetRowCellValue(e.RowHandle, view.Columns["ID_LDV"]) + "";
                    double d = Convert.ToDouble(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql));
                    view.SetRowCellValue(e.RowHandle, view.Columns["PHAN_TRAM_TRO_CAP"], d);
                }
                if (e.Column.Name == "colNGHI_TU_NGAY" || e.Column.Name == "colNGHI_DEN_NGAY" || e.Column.Name == "colID_CN")
                {

                    DateTime tn = Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, view.Columns["NGHI_TU_NGAY"]));
                    DateTime dn = Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, view.Columns["NGHI_DEN_NGAY"]));
                    int idcn = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["ID_CN"]));
                    TimeSpan time = dn - tn;
                    //kiểm tra từ ngày đến ngày phải không nằm trong khoảng dưới data lẫn trên view
                    if (!kiemtraNgayHopLe(tn, Commons.Modules.ObjSystems.ConvertDatatable(grvTroCapBHXH), Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["ID_CN"]))))
                    {
                        view.SetColumnError(colTuNgay, Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TuNgayDaTonTai"));
                        val = false;
                        return;
                    }
                    if (!kiemtraNgayHopLe(dn, Commons.Modules.ObjSystems.ConvertDatatable(grvTroCapBHXH), Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["ID_CN"]))))
                    {

                        view.SetColumnError(colDenNgay, Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TuNgayDaTonTai"));
                        val = false;
                        return;
                    }

                    if (time.Days >= 0)
                    {
                        //số giờ lũy kế được tính là trước tháng hiện tại cộng cho thời gian ơ
                        view.SetRowCellValue(e.RowHandle, view.Columns["SO_NGAY_NGHI"], time.Days + 1);
                        sSql = "SELECT dbo.fnGetSoGioLuyKeThangTruoc(" + ThangdateEdit.DateTime.Month + "," + ThangdateEdit.DateTime.Year + "," + view.GetRowCellValue(e.RowHandle, view.Columns["ID_CN"]) + ")";
                        if (e.RowHandle > 0)
                        {
                            view.SetRowCellValue(e.RowHandle, view.Columns["SO_NGAY_NGHI"], time.Days + 1);
                        }
                        else
                        {
                            view.SetRowCellValue(grvTroCapBHXH.RowCount - 2, view.Columns["SO_NGAY_NGHI"], time.Days + 1);

                        }
                        int n;
                        try
                        {
                            n = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql));
                        }
                        catch (Exception)
                        {
                            n = 0;
                        }
                        //tính số giờ lũy kế hiện tại trên view
                        DataTable dttc = Commons.Modules.ObjSystems.ConvertDatatable(grvTroCapBHXH);
                        for (int i = 0; i < dttc.Rows.Count; i++)
                        {
                            //nếu mà chính là công nhân đó thì gán n
                            if (Convert.ToInt64(view.GetFocusedRowCellValue(view.Columns["ID_CN"])) == Convert.ToInt64(view.GetRowCellValue(i, view.Columns["ID_CN"])))
                            {
                                n += Convert.ToInt32(view.GetRowCellValue(i, view.Columns["SO_NGAY_NGHI"]));
                                view.SetRowCellValue(i, view.Columns["SO_NGAYLK"], n);
                            }
                        }
                    }
                    else
                    {
                        view.SetColumnError(colTuNgay, Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TuNgayKhongLonHonDenNgay"));
                        view.SetColumnError(colDenNgay, Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TuNgayKhongLonHonDenNgay"));
                        val = false;
                        return;
                    }
                    //kiểm tra dòng này là mới hay là cũ   nếu mới thì cho gán các giá trị mặc định vào
                    //Load công thức tính số tiền trợ cấp và lương tính trợ cấp
                    DataTable dt = new DataTable();
                    sSql = "SELECT * from  dbo.funTinhTroCapBHXH(" + idcn + ",'" + Convert.ToDateTime(grvTroCapBHXH.GetFocusedRowCellValue("NGHI_TU_NGAY")).ToString("MM/dd/yyyy") + "')";
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                    if (e.RowHandle < 0)
                    {
                        view.SetRowCellValue(grvTroCapBHXH.RowCount - 2, view.Columns["LUONG_TINH_TC"], dt.Rows[0]["LUONG_TINH_TC"]);
                        view.SetRowCellValue(grvTroCapBHXH.RowCount - 2, view.Columns["SO_TIEN_TC"], dt.Rows[0]["SO_TIEN_TC"]);
                    }
                    else
                    {
                        view.SetRowCellValue(e.RowHandle, view.Columns["LUONG_TINH_TC"], dt.Rows[0]["LUONG_TINH_TC"]);
                        view.SetRowCellValue(e.RowHandle, view.Columns["SO_TIEN_TC"], dt.Rows[0]["SO_TIEN_TC"]);
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        #endregion

        #region Điều chỉnh trợ cấp bảo hiểm y tế
        private void LoadGrdDCTroCapBHXH(bool loadBT)
        {
            try
            {
                DataTable dt = new DataTable();
                if (loadBT == true)
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM dbo." + sbtDCTroCapBHXH + ""));
                }
                else
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListDCTroCapBHXH", ThangdateEdit.EditValue, spinDot.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                }
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdDCTroCapBHXH, grvDCTroCapBHXH, dt, false, false, false, true, true, this.Name);
                Commons.Modules.ObjSystems.AddCombXtra("ID_CN", "TEN_CN", grvDCTroCapBHXH, "spGetCongNhan");
                Commons.Modules.ObjSystems.AddCombo("ID_LDV", "TEN_LDV", grvDCTroCapBHXH, Commons.Modules.ObjSystems.DataLyDoVang(false));

                grvDCTroCapBHXH.Columns["ID_HTC"].Visible = false;

                grvDCTroCapBHXH.Columns["PHAN_TRAM_TRO_CAP"].OptionsColumn.ReadOnly = true;
                grvDCTroCapBHXH.Columns["SO_NGAY_NGHI"].OptionsColumn.ReadOnly = true;
                grvDCTroCapBHXH.Columns["MS_CN"].OptionsColumn.ReadOnly = true;
                grvDCTroCapBHXH.Columns["THANG"].OptionsColumn.ReadOnly = true;
                grvDCTroCapBHXH.Columns["THANG_CHUYEN"].OptionsColumn.ReadOnly = true;
                grvDCTroCapBHXH.Columns["DOT"].OptionsColumn.ReadOnly = true;
                grvDCTroCapBHXH.Columns["DOT_CHUYEN"].OptionsColumn.ReadOnly = true;
                grvDCTroCapBHXH.Columns["MUC_HUONG_CU"].OptionsColumn.ReadOnly = true;

                grvDCTroCapBHXH.Columns["MS_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvDCTroCapBHXH.Columns["ID_CN"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                grvDCTroCapBHXH.Columns["ID_LDV"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                grvDCTroCapBHXH.Columns["MS_CN"].Width = 100;
                grvDCTroCapBHXH.Columns["ID_CN"].Width = 200;
                grvDCTroCapBHXH.Columns["ID_LDV"].Width = 200;

                grvDCTroCapBHXH.Columns["MUC_HUONG_CU"].DisplayFormat.FormatType = FormatType.Numeric;
                grvDCTroCapBHXH.Columns["MUC_HUONG_CU"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvDCTroCapBHXH.Columns["MUC_HUONG_MOI"].DisplayFormat.FormatType = FormatType.Numeric;
                grvDCTroCapBHXH.Columns["MUC_HUONG_MOI"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
                grvDCTroCapBHXH.Columns["SO_TIEN_LECH"].DisplayFormat.FormatType = FormatType.Numeric;
                grvDCTroCapBHXH.Columns["SO_TIEN_LECH"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;

            }
            catch { }
        }

        private void grvDCTroCapBHXH_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            GridColumn colTuNgay = view.Columns["NGHI_TU_NGAY"];
            GridColumn colDenNgay = view.Columns["NGHI_DEN_NGAY"];

            GridColumn colThang = view.Columns["THANG"];
            GridColumn colThangChuyen = view.Columns["THANG_CHUYEN"];

            GridColumn colDot = view.Columns["DOT"];
            GridColumn colDotChuyen = view.Columns["DOT_CHUYEN"];
            if (e.Column.Name == "colID_CN")
            {

                string sSql = "SELECT MS_CN FROM dbo.CONG_NHAN WHERE ID_CN = " + view.GetRowCellValue(e.RowHandle, view.Columns["ID_CN"]) + "";
                string s = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql).ToString();
                view.SetRowCellValue(e.RowHandle, view.Columns["MS_CN"], s);

                //kiểm tra dòng này là mới hay là cũ nếu mới thì cho gán các giá trị mặc định vào
                int ktdong = Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.DIEU_CHINH_TRO_CAP_BHXH WHERE ID_HTC = " + view.GetRowCellValue(e.RowHandle, view.Columns["ID_HTC"]) + " "));
                if (ktdong == 0)
                {
                    view.SetRowCellValue(e.RowHandle, colThang, ThangdateEdit.DateTime.ToString("MM/yyyy"));
                    view.SetRowCellValue(e.RowHandle, colThangChuyen, ThangdateEdit.DateTime.ToString("MM/yyyy"));
                    view.SetRowCellValue(e.RowHandle, colDot, spinDot.EditValue);
                    view.SetRowCellValue(e.RowHandle, colDotChuyen, spinDot.EditValue);
                }
            }
            if (e.Column.Name == "colID_LDV")
            {
                string sSql = "SELECT PHAN_TRAM_TRO_CAP FROM dbo.LY_DO_VANG WHERE ID_LDV = " + view.GetRowCellValue(e.RowHandle, view.Columns["ID_LDV"]) + "";
                double d = Convert.ToDouble(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, sSql));
                view.SetRowCellValue(e.RowHandle, view.Columns["PHAN_TRAM_TRO_CAP"], d);
            }
            if (e.Column.Name == "colNGHI_TU_NGAY" || e.Column.Name == "colNGHI_DEN_NGAY")
            {
                DateTime tn = Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, view.Columns["NGHI_TU_NGAY"]));
                DateTime dn = Convert.ToDateTime(view.GetRowCellValue(e.RowHandle, view.Columns["NGHI_DEN_NGAY"]));
                TimeSpan time = dn - tn;
                if (time.Days >= 0)
                {
                    view.SetRowCellValue(e.RowHandle, view.Columns["SO_NGAY_NGHI"], time.Days + 1);
                }
                else
                {
                    view.SetColumnError(colTuNgay, Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TuNgayKhongLonHonDenNgay"));
                    view.SetColumnError(colDenNgay, Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TuNgayKhongLonHonDenNgay"));
                    val = false;
                }
            }
        }

        private void LuuDCTroCapBaoHiem()
        {
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sbtDCTroCapBHXH, Commons.Modules.ObjSystems.ConvertDatatable(grvDCTroCapBHXH), "");
                string sSql = "UPDATE  A SET A.ID_CN = B.ID_CN ,A.ID_LDV = B.ID_LDV ,A.NGHI_TU_NGAY = B.NGHI_TU_NGAY,A.NGHI_DEN_NGAY = B.NGHI_DEN_NGAY,A.MUC_HUONG_MOI = B.MUC_HUONG_MOI,A.SO_NGAY_LECH = B.SO_NGAY_LECH,A.SO_TIEN_LECH = B.SO_TIEN_LECH,A.GHI_CHU = B.NOI_DUNG_CHINH FROM dbo.DIEU_CHINH_TRO_CAP_BHXH A INNER JOIN dbo." + sbtDCTroCapBHXH + " B ON B.ID_HTC = A.ID_HTC INSERT INTO dbo.DIEU_CHINH_TRO_CAP_BHXH (ID_CN, ID_LDV, DOT, THANG, DOT_CHUYEN, THANG_CHUYEN, NGHI_TU_NGAY, NGHI_DEN_NGAY, MUC_HUONG_MOI, SO_NGAY_LECH, SO_TIEN_LECH, GHI_CHU ) SELECT ID_CN, ID_LDV, DOT, THANG, DOT_CHUYEN, THANG_CHUYEN, NGHI_TU_NGAY, NGHI_DEN_NGAY, MUC_HUONG_MOI, SO_NGAY_LECH, SO_TIEN_LECH, NOI_DUNG_CHINH FROM " + sbtDCTroCapBHXH + " A WHERE   NOT EXISTS(SELECT * FROM    dbo.DIEU_CHINH_TRO_CAP_BHXH B WHERE A.ID_CN = B.ID_CN AND A.NGHI_TU_NGAY = B.NGHI_TU_NGAY)  DELETE dbo.DIEU_CHINH_TRO_CAP_BHXH WHERE NOT EXISTS (SELECT * FROM " + sbtDCTroCapBHXH + " A WHERE DIEU_CHINH_TRO_CAP_BHXH.THANG = a.THANG AND DIEU_CHINH_TRO_CAP_BHXH.ID_CN = A.ID_CN AND dbo.DIEU_CHINH_TRO_CAP_BHXH.NGHI_TU_NGAY = CONVERT(DATE,A.NGHI_TU_NGAY))";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                Commons.Modules.ObjSystems.XoaTable(sbtDCTroCapBHXH);

            }
            catch
            {
            }
        }
        #endregion

        #region chọn điều chỉnh bảo hiểm y tế
        private void LoadGrdChonTroCapBHXH()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListChonTroCapBHXH", ThangLookUpEdit1.EditValue, spinDot1.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, sbtDCTroCapBHXH));
                dt.Columns["CHON"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdChonTroCapBHXH, grvChonTroCapBHXH, dt, false, false, true, true, true, "");
                Commons.Modules.ObjSystems.AddCombXtra("ID_CN", "TEN_CN", grvChonTroCapBHXH, "spGetCongNhan");
                Commons.Modules.ObjSystems.AddCombo("ID_LDV", "TEN_LDV", grvChonTroCapBHXH, Commons.Modules.ObjSystems.DataLyDoVang(false));

                grvChonTroCapBHXH.Columns["PHAN_TRAM_TRO_CAP"].Visible = false;
                grvChonTroCapBHXH.Columns["CHON"].Visible = false;
                grvChonTroCapBHXH.Columns["CHON"].Width = 100;
                grvChonTroCapBHXH.Columns["ID_CN"].Width = 200;
                grvChonTroCapBHXH.Columns["ID_LDV"].Width = 200;
                grvChonTroCapBHXH.Columns["ID_TC_BHXH"].Visible = false;
                grvChonTroCapBHXH.OptionsSelection.CheckBoxSelectorField = "CHON";
                grvChonTroCapBHXH.Columns["SO_TIEN_TC"].DisplayFormat.FormatType = FormatType.Numeric;
                grvChonTroCapBHXH.Columns["SO_TIEN_TC"].DisplayFormat.FormatString = Commons.Modules.sSoLeTT;
            }
            catch
            {

            }
        }
        #endregion

        private void grvDCTroCapBHXH_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView View = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            DevExpress.XtraGrid.Columns.GridColumn ID_CN = View.Columns["ID_CN"];
            DevExpress.XtraGrid.Columns.GridColumn NGHI_TU_NGAY = View.Columns["NGHI_TU_NGAY"];
            DevExpress.XtraGrid.Columns.GridColumn NGHI_DEN_NGAY = View.Columns["NGHI_DEN_NGAY"];
            //kiểm tra dữ liệu trùng
            DataTable tempt = Commons.Modules.ObjSystems.ConvertDatatable(grvDCTroCapBHXH);
            int n = 0;
            //kiểm tra trùng trên view
            try
            {
                n = tempt.AsEnumerable().Count(x => x.Field<Int64>("ID_CN") == Convert.ToInt64(View.GetRowCellValue(e.RowHandle, ID_CN)) && x.Field<DateTime>("NGHI_TU_NGAY") == Convert.ToDateTime(View.GetRowCellValue(e.RowHandle, NGHI_TU_NGAY)));
            }
            catch (Exception ex)
            {
            }
            if (n > 1)
            {
                e.Valid = false;
                View.SetColumnError(ID_CN, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgTrungDuLieu", Commons.Modules.TypeLanguage));
                View.SetColumnError(NGHI_TU_NGAY, Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "MsgTrungDuLieu", Commons.Modules.TypeLanguage));
                val = false;
                return;
            }


        }

        private void ThangLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            //ThangdateEdit.EditValue = ThangLookUpEdit.EditValue;
            if (Commons.Modules.sPS == "0Load") return;
            LoadGrdTroCapBHXH();
            LoadGrdDCTroCapBHXH(false);
        }

        private void ThangLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            LoadGrdChonTroCapBHXH();
        }

        private void ThangdateEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            LoadGrdTroCapBHXH();
            LoadGrdDCTroCapBHXH(false);
        }


        private void XoaTroCapBaoHiemXH()
        {
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDeleteKhoaDaoTao"), Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTieuDeXoa"), MessageBoxButtons.YesNo) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.TRO_CAP_BHXH WHERE ID_TC_BHXH = " + grvTroCapBHXH.GetFocusedRowCellValue("ID_TC_BHXH") + "");
                grvTroCapBHXH.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgDelDangSuDung") + "\n" + ex.Message.ToString());
            }
        }

        private void grdTroCapBHXH_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                XoaTroCapBaoHiemXH();
            }
        }
    }
}


