using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraLayout;
using System.Threading;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraGrid.Views.Grid;
using System.Linq;

namespace Vs.TimeAttendance
{
    public partial class ucKhongTinhPhep : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool tinhThangTH = false;
        private static bool themSua = false;
        public static ucKhongTinhPhep _instance;
        public static ucKhongTinhPhep Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucKhongTinhPhep();
                return _instance;
            }
        }

        private string CreateTable()
        {
            var tbTemp = "tbtmpKhongTinhPhep" + Commons.Modules.UserName;
            try
            {

                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, tbTemp , Commons.Modules.ObjSystems.ConvertDatatable(grvKTP), "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE FROM " + tbTemp);
                
            }
            catch
            {
            }
            return tbTemp;
        }

        public ucKhongTinhPhep()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this,new List<LayoutControlGroup>{ Root}, windowsUIButton);
        }
        #region Mã thẻ chấm công
        private void ucKhongTinhPhep_Load(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);

            LoadThang();
            LoadGridKhongTinhPhep();
            Commons.Modules.sPS = "";
            enableButon(true);
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridKhongTinhPhep();
            Commons.Modules.sPS = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGridKhongTinhPhep();
            Commons.Modules.sPS = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGridKhongTinhPhep();
            Commons.Modules.sPS = "";
        }
        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sPS == "0Load") return;
            Commons.Modules.sPS = "0Load";
            LoadGridKhongTinhPhep();
            Commons.Modules.sPS = "";
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "tinhthanght":
                    {
                        enableButon(false);
                        tinhThangTH = true;
                        LoadGridKhongTinhPhep();
                        break;
                    }
                case "themsua":
                    {
                        if (grvKTP.RowCount == 0)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgBanChuaChonDuLieu);
                            return;
                        }
                        themSua = true;
                        LoadGridKhongTinhPhep();
                        enableButon(false);
                        break;
                    }
                case "xoa":
                    {
                        Xoa();
                        break;
                    }
                case "luu":
                    {
                        Validate();
                        if (grvKTP.HasColumnErrors) return;
                        Savedata();
                        tinhThangTH = false;
                        themSua = false;
                        enableButon(true);
                        LoadGridKhongTinhPhep();
                        LoadThang();
                        break;
                    }
                case "khongluu":
                    {
                        tinhThangTH = false;
                        themSua = false;
                        enableButon(true);
                        LoadGridKhongTinhPhep();
                        
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
        #endregion

        private DateTime dTNgay, dDNgay;
        #region hàm xử lý dữ liệu
        private void Xoa()
        {
            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE dbo.SO_THANG_KHONG_TP WHERE ID_CN = " 
                                         + grvKTP.GetFocusedRowCellValue("ID_CN") + " AND CONVERT(NVARCHAR(10),THANG,112) ='" 
                                         + Convert.ToDateTime(cboThang.EditValue).ToString("yyyyMMdd") + "'" + "");
                LoadGridKhongTinhPhep();
            }
            catch
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
            }
        }
        private void LoadGridKhongTinhPhep()
            {
            DataTable dt = new DataTable();
            GetDayFromTo(out dTNgay,out dDNgay);

            if (tinhThangTH)
            {               
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spTinhThangKhongTinhPhep", Commons.Modules.UserName, Commons.Modules.TypeLanguage,cboDV.EditValue,
                                                                              cboXN.EditValue, cboTo.EditValue, dTNgay, dDNgay));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdKTP, grvKTP, dt, true, true, true, true, true, this.Name);
            }
            else if (themSua)
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListKhongTinhPhep", cboThang.EditValue,
                                                cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdKTP, grvKTP, dt, true, true, true, true, true, this.Name);
            }
            else
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListKhongTinhPhep", cboThang.EditValue, 
                    cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdKTP, grvKTP, dt, false, true, true, true, true, this.Name);
            }
            grvKTP.Columns["ID_CN"].OptionsColumn.ReadOnly = true;
            grvKTP.Columns["ID_CN"].Visible = false;

            grvKTP.Columns["MS_CN"].OptionsColumn.ReadOnly = true;
            grvKTP.Columns["HO_TEN"].OptionsColumn.ReadOnly = true;
        }


        private void GetDayFromTo(out DateTime _dTNgay, out DateTime _dDNgay )
        {
            var date = Convert.ToDateTime(cboThang.EditValue);
            var dateInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            _dTNgay = new DateTime(date.Year, date.Month, 1);
            _dDNgay = new DateTime(date.Year, date.Month, dateInMonth);
        }
        private void Savedata()
        {
            var stbTemp = "SAVE_SO_THANG_KHONG_TP" + Commons.Modules.UserName;
            try
            {
                //tạo một datatable 
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbTemp, Commons.Modules.ObjSystems.ConvertDatatable(grvKTP), "");
                string sSql = "DELETE FROM SO_THANG_KHONG_TP WHERE LEFT(CONVERT(NVARCHAR(6),THANG, 112), 6) = '"
                              + Convert.ToDateTime(cboThang.EditValue).ToString("yyyyMM") + "' AND ID_CN IN (SELECT ID_CN FROM "+ stbTemp
                              + ")INSERT INTO SO_THANG_KHONG_TP (ID_CN,THANG,SO_THANG,THANG_HT,"
                              + "TONG_ST, GHI_CHU) SELECT ID_CN, '" + Convert.ToDateTime(cboThang.EditValue).ToString("yyyyMMdd") 
                              + "' THANG, SO_THANG, THANG_HT, TONG_ST, GHI_CHU FROM " + stbTemp + " WHERE TONG_ST > 0";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                Commons.Modules.ObjSystems.XoaTable(stbTemp);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[5].Properties.Visible = visible;
            windowsUIButton.Buttons[6].Properties.Visible = !visible;
            windowsUIButton.Buttons[7].Properties.Visible = !visible;

            searchControl.Visible = true;
            //isAdd = !windowsUIButton.Buttons[0].Properties.Visible;
        }
        #endregion

        private void grvMTCC_RowCountChanged(object sender, EventArgs e)
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

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
            }
            catch
            {
                cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
            }
            cboThang.ClosePopup();
        }

        private void LoadThang()
        {
            try
            {
                DataTable dtthang = new DataTable();
                string sSql = "		SELECT DISTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM SO_THANG_KHONG_TP ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                grvThang.Columns["M"].Visible = false;
                grvThang.Columns["Y"].Visible = false;

                if(dtthang.Rows.Count > 0)
                {
                    cboThang.EditValue = dtthang.Rows[0][2];
                }
                else
                {
                    cboThang.Text = DateTime.Now.ToString("MM/yyyy");
                }
            }
            catch
            {
            }
        }

        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                cboThang.Text = grvThang.GetFocusedRowCellValue("THANG").ToString();
            }
            catch { }
            cboThang.ClosePopup();
        }
    }
}
