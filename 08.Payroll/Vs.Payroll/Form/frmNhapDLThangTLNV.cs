using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System.Linq;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using static DevExpress.XtraPrinting.Native.ExportOptionsPropertiesNames;
using static NPOI.HSSF.Util.HSSFColor;

namespace Vs.Payroll
{
    public partial class frmNhapDLThangTLNV : DevExpress.XtraEditors.XtraForm
    {
        public int iID_DV = -1;
        public int iID_XN = -1;
        public int iID_TO = -1;
        public DateTime dNgay;
        private int iThem = 0;
        public int iLoai = 0;
        public frmNhapDLThangTLNV()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, tabControl, windowsUIButton);
        }

        //sự kiên load form
        private void frmNhapDLThangTLNV_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
                Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
                cboDonVi.EditValue = Convert.ToInt64(iID_DV);
                cboXiNghiep.EditValue = Convert.ToInt64(iID_XN);
                cboTo.EditValue = Convert.ToInt64(iID_TO);
                tabControl_SelectedPageChanged(null, null);
                VisibleButton(true);
                foreach (ToolStripMenuItem item in contextMenuStrip1.Items)
                {
                    item.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, item.Name);
                }
                Commons.Modules.sLoad = "";
                Commons.Modules.ObjSystems.SetPhanQuyen(windowsUIButton);
            }
            catch { }
        }

        private void windowsUIButton_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        iThem = 1;
                        switch (tabControl.SelectedTabPage.Name)
                        {
                            case "tabHoTroLuong":
                                {
                                    LoadDataHTL(iThem);
                                    break;
                                }
                            case "tabPhanCongTo":
                                {
                                    LoadDataPCT();
                                    Commons.Modules.ObjSystems.AddnewRow(grvPCT, true);
                                    break;
                                }
                            case "TabDoanhThuNhaMay":
                                {
                                    Commons.Modules.ObjSystems.AddnewRow(grvDTNM, true);
                                    break;
                                }
                            case "tabThuongHQQLKhac":
                                {
                                    LoadDataTHQQLKhac(iThem);
                                    break;
                                }
                            case "tabDSGiamDoc":
                                {
                                    LoadDataTGDNM();
                                    Commons.Modules.ObjSystems.AddnewRow(grvTGDNM, true);
                                    break;
                                }
                            case "tabPTBuLuong":
                                {
                                    LoadDataPTBuLuong();
                                    break;
                                }
                        }
                        VisibleButton(false);
                        break;
                    }
                case "copydlcu":
                    {
                        switch (tabControl.SelectedTabPage.Name)
                        {
                            case "tabPhanCongTo":
                                {
                                    if (!CopyData("PCT", Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text), grdPCT))
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuCu"), Commons.Form_Alert.enmType.Warning);
                                    }
                                    else
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCopyThanhCong"), Commons.Form_Alert.enmType.Success);
                                    }
                                    Commons.Modules.ObjSystems.AddnewRow(grvPCT, true);
                                    break;
                                }
                            case "tabDSGiamDoc":
                                {
                                    if (!CopyData("DS_GD", Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text), grdTGDNM))
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuCu"), Commons.Form_Alert.enmType.Warning);
                                    }
                                    else
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCopyThanhCong"), Commons.Form_Alert.enmType.Success);
                                    }
                                    Commons.Modules.ObjSystems.AddnewRow(grvTGDNM, true);
                                    break;
                                }
                            case "TabDoanhThuNhaMay":
                                {
                                    if (!CopyData("DTNM", Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text), grdDTNM))
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuCu"), Commons.Form_Alert.enmType.Warning);
                                    }
                                    else
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCopyThanhCong"), Commons.Form_Alert.enmType.Success);
                                    }
                                    Commons.Modules.ObjSystems.AddnewRow(grvDTNM, true);
                                    break;
                                }
                            case "tabPTBuLuong":
                                {
                                    if (!CopyData("PT_BL", Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text), grdPTBuLuong))
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgKhongCoDuLieuCu"), Commons.Form_Alert.enmType.Warning);
                                    }
                                    else
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCopyThanhCong"), Commons.Form_Alert.enmType.Success);
                                    }
                                    Commons.Modules.ObjSystems.AddnewRow(grvPTBuLuong, true);
                                    break;
                                }
                        }
                        break;
                    }
                case "luu":
                    {
                        switch (tabControl.SelectedTabPage.Name)
                        {
                            case "tabHoTroLuong":
                                {
                                    grvHTL.CloseEditor();
                                    grvHTL.UpdateCurrentRow();
                                    if (grvHTL.HasColumnErrors) return;
                                    if (!SaveData("HTL", grdHTL, Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text)))
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                                        return;
                                    }
                                    iThem = 0;
                                    LoadDataHTL(iThem);
                                    break;
                                }
                            case "tabPhanCongTo":
                                {
                                    grvPCT.CloseEditor();
                                    grvPCT.UpdateCurrentRow();
                                    if (grvPCT.HasColumnErrors) return;
                                    if (!SaveData("PCT", grdPCT, Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text)))
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                                        return;
                                    }
                                    iThem = 0;
                                    LoadDataPCT();
                                    Commons.Modules.ObjSystems.DeleteAddRow(grvPCT);
                                    break;
                                }
                            case "TabDoanhThuNhaMay":
                                {
                                    grvDTNM.CloseEditor();
                                    grvDTNM.UpdateCurrentRow();
                                    if (grvDTNM.HasColumnErrors) return;
                                    if (!SaveData("DTNM", grdDTNM, Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text)))
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                                        return;
                                    }
                                    LoadDataDTNM();
                                    iThem = 0;
                                    Commons.Modules.ObjSystems.DeleteAddRow(grvDTNM);
                                    break;
                                }
                            case "tabThuongHQQLKhac":
                                {
                                    grvTHQQL.CloseEditor();
                                    grvTHQQL.UpdateCurrentRow();
                                    if (grvTHQQL.HasColumnErrors) return;
                                    if (!SaveData("T_HQQL_KHAC", grdTHQQL, Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text)))
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                                        return;
                                    }
                                    iThem = 0;
                                    LoadDataTHQQLKhac(iThem);
                                    break;
                                }
                            case "tabDSGiamDoc":
                                {
                                    grvTGDNM.CloseEditor();
                                    grvTGDNM.UpdateCurrentRow();
                                    if (grvTGDNM.HasColumnErrors) return;
                                    if (!SaveData("DS_GD", grdTGDNM, Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text)))
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                                        return;
                                    }
                                    iThem = 0;
                                    LoadDataPCT();
                                    Commons.Modules.ObjSystems.DeleteAddRow(grvTGDNM);
                                    break;
                                }
                            case "tabPTBuLuong":
                                {
                                    grvPTBuLuong.CloseEditor();
                                    grvPTBuLuong.UpdateCurrentRow();
                                    if (grvPTBuLuong.HasColumnErrors) return;
                                    if (!SaveData("PT_BL", grdPTBuLuong, Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text)))
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                                        return;
                                    }
                                    iThem = 0;
                                    LoadDataPTBuLuong();
                                    Commons.Modules.ObjSystems.DeleteAddRow(grvPTBuLuong);
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCong"), Commons.Form_Alert.enmType.Success);
                        VisibleButton(true);
                        break;
                    }
                case "khongluu":
                    {
                        iThem = 0;
                        switch (tabControl.SelectedTabPage.Name)
                        {
                            case "tabHoTroLuong":
                                {
                                    LoadDataHTL(iThem);
                                    break;
                                }
                            case "tabPhanCongTo":
                                {
                                    LoadDataPCT();
                                    Commons.Modules.ObjSystems.DeleteAddRow(grvPCT);
                                    break;
                                }
                            case "TabDoanhThuNhaMay":
                                {
                                    LoadDataDTNM();
                                    Commons.Modules.ObjSystems.DeleteAddRow(grvDTNM);
                                    break;
                                }
                            case "tabThuongHQQLKhac":
                                {
                                    LoadDataTHQQLKhac(iThem);
                                    break;
                                }
                            case "tabDSGiamDoc":
                                {
                                    LoadDataTGDNM();
                                    Commons.Modules.ObjSystems.DeleteAddRow(grvTGDNM);
                                    break;
                                }
                            case "tabPTBuLuong":
                                {
                                    LoadDataPTBuLuong();
                                    Commons.Modules.ObjSystems.DeleteAddRow(grvPTBuLuong);
                                    break;
                                }
                        }
                        VisibleButton(true);
                        break;
                    }
                case "xoa":
                    {
                        if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanCoMuonXoaKhong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                        switch (tabControl.SelectedTabPage.Name)
                        {
                            case "tabHoTroLuong":
                                {
                                    if (!DeleteData("HTL", Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdHTL, grvHTL), Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text)))
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                                    }
                                    else
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCong"), Commons.Form_Alert.enmType.Success);
                                    }
                                    LoadDataHTL(0);
                                    break;
                                }
                            case "tabPhanCongTo":
                                {
                                    if (!DeleteData("PCT", Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdPCT, grvPCT), Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text)))
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                                    }
                                    else
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCong"), Commons.Form_Alert.enmType.Success);
                                    }
                                    LoadDataPCT();
                                    break;
                                }
                            case "TabDoanhThuNhaMay":
                                {
                                    if (!DeleteData("DTNM", Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdDTNM, grvDTNM), Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text)))
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                                    }
                                    else
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCong"), Commons.Form_Alert.enmType.Success);
                                    }
                                    LoadDataDTNM();
                                    break;
                                }
                            case "tabThuongHQQLKhac":
                                {
                                    if (!DeleteData("T_HQQL_KHAC", Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdTHQQL, grvTHQQL), Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text)))
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                                    }
                                    else
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCong"), Commons.Form_Alert.enmType.Success);
                                    }
                                    LoadDataTHQQLKhac(0);
                                    break;
                                }
                            case "tabDSGiamDoc":
                                {
                                    if (!DeleteData("DS_GD", Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdTGDNM, grvTGDNM), Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text)))
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                                    }
                                    else
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCong"), Commons.Form_Alert.enmType.Success);
                                    }
                                    LoadDataTGDNM();
                                    break;
                                }

                            case "tabPTBuLuong":
                                {
                                    if (!DeleteData("PT_BL", Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdPTBuLuong, grvPTBuLuong), Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text)))
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                                    }
                                    else
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatThanhCong"), Commons.Form_Alert.enmType.Success);
                                    }
                                    LoadDataPTBuLuong();
                                    break;
                                }
                        }
                        break;
                    }
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            switch (tabControl.SelectedTabPage.Name)
            {
                case "tabHoTroLuong":
                    {
                        cboThang.Text = calThang.DateTime.ToString("dd/MM/yyyy");
                        cboThang.ClosePopup();
                        break;
                    }
                default:
                    {
                        cboThang.Text = calThang.DateTime.ToString("MM/yyyy");
                        cboThang.ClosePopup();
                        break;
                    }
            }

        }

        public void LoadThang(int indexTab)
        {
            try
            {
                string sSql = "";
                DataTable dtthang = new DataTable();
                switch (indexTab)
                {
                    case 0:
                        {
                            sSql = "SELECT disTINCT RIGHT(CONVERT(VARCHAR(10),NGAY,103),7) AS THANG , CONVERT(VARCHAR(10),NGAY,103) NGAY FROM dbo.HO_TRO_LUONG ORDER BY THANG DESC , NGAY DESC";
                            dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                            Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay1, dtthang, false, true, true, true, true, this.Name);
                            grvNgay1.Columns["THANG"].Visible = false;

                            try
                            {
                                cboThang.Text = grvNgay1.GetFocusedRowCellValue("NGAY").ToString();
                            }
                            catch
                            {
                                cboThang.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            }
                            break;
                        }
                    case 1:
                        {
                            sSql = "SELECT disTINCT RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG , CONVERT(VARCHAR(10),THANG,103) NGAY FROM dbo.PHAN_CONG_TO ORDER BY THANG DESC , NGAY DESC";
                            dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                            Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay1, dtthang, false, true, true, true, true, this.Name);
                            grvNgay1.Columns["NGAY"].Visible = false;

                            try
                            {
                                cboThang.Text = grvNgay1.GetFocusedRowCellValue("THANG").ToString();
                            }
                            catch
                            {
                                cboThang.Text = DateTime.Now.ToString("MM/yyyy");
                            }
                            break;
                        }
                    case 2:
                        {
                            sSql = "SELECT disTINCT RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG , CONVERT(VARCHAR(10),THANG,103) NGAY FROM dbo.DOANH_THU_NHA_MAY ORDER BY THANG DESC , NGAY DESC";
                            dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                            Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay1, dtthang, false, true, true, true, true, this.Name);
                            grvNgay1.Columns["NGAY"].Visible = false;

                            try
                            {
                                cboThang.Text = grvNgay1.GetFocusedRowCellValue("THANG").ToString();
                            }
                            catch
                            {
                                cboThang.Text = DateTime.Now.ToString("MM/yyyy");
                            }
                            break;
                        }

                    case 3:
                        {
                            sSql = "SELECT disTINCT RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG , CONVERT(VARCHAR(10),THANG,103) NGAY FROM dbo.THUONG_HQQL_KHAC ORDER BY THANG DESC , NGAY DESC";
                            dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                            Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay1, dtthang, false, true, true, true, true, this.Name);
                            grvNgay1.Columns["NGAY"].Visible = false;

                            try
                            {
                                cboThang.Text = grvNgay1.GetFocusedRowCellValue("THANG").ToString();
                            }
                            catch
                            {
                                cboThang.Text = DateTime.Now.ToString("MM/yyyy");
                            }
                            break;
                        }
                    case 4:
                        {
                            sSql = "SELECT disTINCT RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG , CONVERT(VARCHAR(10),THANG,103) NGAY FROM dbo.THUONG_GD_NHA_MAY ORDER BY THANG DESC , NGAY DESC";
                            dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                            Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay1, dtthang, false, true, true, true, true, this.Name);
                            grvNgay1.Columns["NGAY"].Visible = false;

                            try
                            {
                                cboThang.Text = grvNgay1.GetFocusedRowCellValue("THANG").ToString();
                            }
                            catch
                            {
                                cboThang.Text = DateTime.Now.ToString("MM/yyyy");
                            }
                            break;
                        }

                    case 5:
                        {
                            sSql = "SELECT disTINCT RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG , CONVERT(VARCHAR(10),THANG,103) NGAY FROM dbo.PT_BU_LUONG ORDER BY THANG DESC , NGAY DESC";
                            dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                            Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay1, dtthang, false, true, true, true, true, this.Name);
                            grvNgay1.Columns["NGAY"].Visible = false;

                            try
                            {
                                cboThang.Text = grvNgay1.GetFocusedRowCellValue("THANG").ToString();
                            }
                            catch
                            {
                                cboThang.Text = DateTime.Now.ToString("MM/yyyy");
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

            }
            catch (Exception ex)
            {
                DateTime now = DateTime.Now;
                cboThang.Text = now.ToString("dd/MM/yyyy");
            }
        }
        private void grvNgay1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                switch (tabControl.SelectedTabPage.Name)
                {
                    case "tabHoTroLuong":
                        {
                            cboThang.Text = grv.GetFocusedRowCellValue("NGAY").ToString();
                            cboThang.ClosePopup();
                            break;
                        }
                    default:
                        {
                            cboThang.Text = grv.GetFocusedRowCellValue("THANG").ToString();
                            cboThang.ClosePopup();
                            break;
                        }
                }
            }
            catch { }
        }
        private void LoadDataHTL(int iThem)
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spNhapDLThangTLNV", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "HTL";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@iThem", SqlDbType.Int).Value = iThem;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdHTL.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdHTL, grvHTL, dt, true, true, false, true, true, "frmHoTroLuong");
                    grvHTL.Columns["ID_HTL"].Visible = false;
                    grvHTL.Columns["ID_CN"].Visible = false;
                    grvHTL.Columns["SO_TIEN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvHTL.Columns["SO_TIEN"].DisplayFormat.FormatString = "N0";
                }
                else
                {
                    grdHTL.DataSource = dt;
                }
            }
            catch { }
        }
        private void LoadDataPCT()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spNhapDLThangTLNV", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "PCT";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdPCT.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdPCT, grvPCT, dt, true, true, false, true, true, "frmPhanCongTo");
                    grvPCT.Columns["ID_PCT"].Visible = false;
                }
                else
                {
                    grdPCT.DataSource = dt;
                }

                DateTime NgayDauThang = new DateTime(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text).Year, Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text).Month, 1);
                DateTime NgayCuoiThang = NgayDauThang.AddMonths(1).AddDays(-1);

                dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_CN, MS_CN, HO_TEN, MS_THE_CC FROM dbo.MGetListNhanSuFormToDate('" + Commons.Modules.UserName + "', " + Commons.Modules.TypeLanguage + ", " + cboDonVi.EditValue + ", " + cboXiNghiep.EditValue + ", " + cboTo.EditValue + ", '" + NgayDauThang.ToString("MM/dd/yyyy") + "', '" + NgayCuoiThang.ToString("MM/dd/yyyy") + "')"));

                RepositoryItemSearchLookUpEdit cbo1 = new RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo1, "ID_CN", "MS_CN", "ID_CN", grvPCT, dt, this.Name);
                cbo1.BeforePopup += cboID_CN_BeforePopup;
                cbo1.EditValueChanged += cboID_CN_EditValueChanged;

                DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_TO", "TEN_TO", "ID_TO", grvPCT, Commons.Modules.ObjSystems.DataTo(Convert.ToInt32(cboDonVi.EditValue), -1, false), "frmPhanCongTo");
            }
            catch { }
        }

        private void LoadDataTGDNM()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spNhapDLThangTLNV", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "DS_GD";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdTGDNM.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdTGDNM, grvTGDNM, dt, true, true, false, true, true, "frmThuongGDNM");
                    grvTGDNM.Columns["ID_TGD"].Visible = false;
                }
                else
                {
                    grdTGDNM.DataSource = dt;
                }

                DateTime NgayDauThang = new DateTime(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text).Year, Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text).Month, 1);
                DateTime NgayCuoiThang = NgayDauThang.AddMonths(1).AddDays(-1);

                dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_CN, MS_CN, HO_TEN, MS_THE_CC FROM dbo.MGetListNhanSuFormToDate('" + Commons.Modules.UserName + "', " + Commons.Modules.TypeLanguage + ", " + cboDonVi.EditValue + ", " + cboXiNghiep.EditValue + ", " + cboTo.EditValue + ", '" + NgayDauThang.ToString("MM/dd/yyyy") + "', '" + NgayCuoiThang.ToString("MM/dd/yyyy") + "')"));

                RepositoryItemSearchLookUpEdit cbo1 = new RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo1, "ID_CN", "MS_CN", "ID_CN", grvTGDNM, dt, this.Name);
                cbo1.BeforePopup += cboID_CN_BeforePopup;
                cbo1.EditValueChanged += cboID_CNTGDNM_EditValueChanged;

                DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_DV", "TEN_DV", "ID_DV", grvTGDNM, Commons.Modules.ObjSystems.DataDonVi(false), "frmThuongGDNM");
            }
            catch { }
        }
        private void cboID_CNTGDNM_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SearchLookUpEdit lookUp = sender as SearchLookUpEdit;
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                grvTGDNM.SetFocusedRowCellValue("ID_CN", Convert.ToInt64((dataRow.Row[0])));
                grvTGDNM.SetFocusedRowCellValue("HO_TEN", (dataRow.Row[2]).ToString());
                grvTGDNM.SetFocusedRowCellValue("PHAN_TRAM", Convert.ToDouble(100));
            }
            catch { }

        }
        private void cboID_CN_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SearchLookUpEdit lookUp = sender as SearchLookUpEdit;
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                grvPCT.SetFocusedRowCellValue("ID_CN", Convert.ToInt64((dataRow.Row[0])));
                grvPCT.SetFocusedRowCellValue("HO_TEN", (dataRow.Row[2]).ToString());
                grvPCT.SetFocusedRowCellValue("PHAN_TRAM", Convert.ToDouble(100));
            }
            catch { }

        }
        private void cboID_CN_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                DateTime NgayDauThang = new DateTime(Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text).Year, Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text).Month, 1);
                DateTime NgayCuoiThang = NgayDauThang.AddMonths(1).AddDays(-1);

                SearchLookUpEdit lookUp = sender as SearchLookUpEdit;
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_CN, MS_CN, HO_TEN, MS_THE_CC FROM dbo.MGetListNhanSuFormToDate('" + Commons.Modules.UserName + "', " + Commons.Modules.TypeLanguage + ", " + cboDonVi.EditValue + ", " + cboXiNghiep.EditValue + ", " + cboTo.EditValue + ", '" + NgayDauThang.ToString("MM/dd/yyyy") + "', '" + NgayCuoiThang.ToString("MM/dd/yyyy") + "')"));

                //DataTable dtTmp = new DataTable(); // loc du lieu, chỉ lấy những công nhân chưa có trong danh sách view
                //dtTmp = dt.Copy();
                //try
                //{
                //    var dt_temp = dtTmp.AsEnumerable().Where(row => !Commons.Modules.ObjSystems.ConvertDatatable(grvPCT).AsEnumerable()
                //                                             .Select(r => r.Field<Int64>("ID_CN"))
                //                                             .Any(x => x == row.Field<Int64>("ID_CN"))
                //                                             ).CopyToDataTable();
                //    dtTmp = new DataTable();
                //    dtTmp = (DataTable)dt_temp;
                //}
                //catch
                //{
                //    dtTmp.Clear();
                //}
                //dtTmp.AcceptChanges();

                lookUp.Properties.DataSource = dt;
            }
            catch { }
        }
        private void LoadDataDTNM()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spNhapDLThangTLNV", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "DTNM";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdDTNM.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdDTNM, grvDTNM, dt, true, true, false, true, true, "frmDoanhThuNM");
                    grvDTNM.Columns["ID_DTNM"].Visible = false;
                    grvDTNM.Columns["DT_CT"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvDTNM.Columns["DT_CT"].DisplayFormat.FormatString = "N0";
                    grvDTNM.Columns["DT_TT"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvDTNM.Columns["DT_TT"].DisplayFormat.FormatString = "N0";
                    grvDTNM.Columns["KH_CM"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvDTNM.Columns["KH_CM"].DisplayFormat.FormatString = "N0";
                }
                else
                {
                    grdDTNM.DataSource = dt;
                }
                DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_DV", "TEN_DV", "ID_DV", grvDTNM, Commons.Modules.ObjSystems.DataDonVi(false), "frmDoanhThuNM");
            }
            catch { }
        }
        private void LoadDataPTBuLuong()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spNhapDLThangTLNV", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "PT_BL";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@iThem", SqlDbType.Int).Value = iThem;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdPTBuLuong.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdPTBuLuong, grvPTBuLuong, dt, true, true, false, true, true, "frmPTBuLuong");
                    grvPTBuLuong.Columns["ID_PT_BL"].Visible = false;
                    grvPTBuLuong.Columns["ID_CN"].Visible = false;
                    grvPTBuLuong.Columns["CHON"].Visible = false;
                    grvPTBuLuong.Columns["PHAN_TRAM"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvPTBuLuong.Columns["PHAN_TRAM"].DisplayFormat.FormatString = "N0";
                    grvPTBuLuong.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                    grvPTBuLuong.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                    grvPTBuLuong.Columns["TEN_TO"].OptionsColumn.AllowEdit = false;
                    grvPTBuLuong.Columns["NGAY_VAO_LAM"].OptionsColumn.AllowEdit = false;
                }
                else
                {
                    grdPTBuLuong.DataSource = dt;
                }
                if (iThem == 0)
                {
                    grvPTBuLuong.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                }
                else
                {
                    grvPTBuLuong.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                }
                try
                {
                    grvPTBuLuong.OptionsSelection.CheckBoxSelectorField = "CHON";
                    grvPTBuLuong.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
                catch { }
            }
            catch { }
        }
        private void LoadDataTHQQLKhac(int iThem)
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spNhapDLThangTLNV", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "T_HQQL_KHAC";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@iThem", SqlDbType.Int).Value = iThem;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdTHQQL.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdTHQQL, grvTHQQL, dt, true, true, false, true, true, "frmThuongHQKhac");
                    grvTHQQL.Columns["ID_THQQL_KHAC"].Visible = false;
                    grvTHQQL.Columns["ID_CN"].Visible = false;
                    grvTHQQL.Columns["SO_TIEN"].DisplayFormat.FormatType = FormatType.Numeric;
                    grvTHQQL.Columns["SO_TIEN"].DisplayFormat.FormatString = "N0";
                }
                else
                {
                    grdTHQQL.DataSource = dt;
                }
            }
            catch { }
        }
        private bool SaveData(string sTab, DevExpress.XtraGrid.GridControl grdData, DateTime dNgay)
        {
            try
            {
                string sBTKhoiTaoTLNL = "sBTDLThangTLNL" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTKhoiTaoTLNL, (DataTable)grdData.DataSource, "");
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spNhapDLThangTLNV", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = sTab;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBTKhoiTaoTLNL;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = dNgay;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool DeleteData(string sTab, DataTable dt, DateTime dNgay)
        {
            try
            {
                string sBTKhoiTaoTLNL = "sBTDLThangTLNL" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTKhoiTaoTLNL, dt, "");
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spNhapDLThangTLNV", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = sTab;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 2;
                cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBTKhoiTaoTLNL;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = dNgay;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool CopyData(string sTab, DateTime dNgay, DevExpress.XtraGrid.GridControl grdData)
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spNhapDLThangTLNV", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = Convert.ToInt32(cboDonVi.EditValue);
                cmd.Parameters.Add("@XN", SqlDbType.Int).Value = Convert.ToInt32(cboXiNghiep.EditValue);
                cmd.Parameters.Add("@TO", SqlDbType.Int).Value = Convert.ToInt32(cboTo.EditValue);
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = sTab;
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 3;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = dNgay;
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (dt.Rows.Count > 0)
                {
                    grdData.DataSource = dt;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            switch (tabControl.SelectedTabPage.Name)
            {
                case "tabHoTroLuong":
                    {
                        LoadDataHTL(iThem);
                        break;
                    }
                case "tabPhanCongTo":
                    {
                        LoadDataPCT();
                        break;
                    }
                case "TabDoanhThuNhaMay":
                    {
                        LoadDataDTNM();
                        break;
                    }
                case "tabThuongHQQLKhac":
                    {
                        LoadDataTHQQLKhac(iThem);
                        break;
                    }
                case "tabDSGiamDoc":
                    {
                        LoadDataTGDNM();
                        break;
                    }
                case "tabPTBuLuong":
                    {
                        LoadDataPTBuLuong();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            Commons.Modules.sLoad = "";
        }

        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            switch (tabControl.SelectedTabPage.Name)
            {
                case "tabHoTroLuong":
                    {
                        LoadDataHTL(iThem);
                        break;
                    }
                case "tabPhanCongTo":
                    {
                        LoadDataPCT();
                        break;
                    }
                case "TabDoanhThuNhaMay":
                    {
                        LoadDataDTNM();
                        break;
                    }
                case "tabThuongHQQLKhac":
                    {
                        LoadDataTHQQLKhac(iThem);
                        break;
                    }
                case "tabDSGiamDoc":
                    {
                        LoadDataTGDNM();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            Commons.Modules.sLoad = "";
        }

        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);

            switch (tabControl.SelectedTabPage.Name)
            {
                case "tabHoTroLuong":
                    {
                        LoadDataHTL(iThem);
                        break;
                    }
                case "tabPhanCongTo":
                    {
                        LoadDataPCT();
                        break;
                    }
                case "TabDoanhThuNhaMay":
                    {
                        LoadDataDTNM();
                        break;
                    }
                case "tabThuongHQQLKhac":
                    {
                        LoadDataTHQQLKhac(iThem);
                        break;
                    }
                case "tabDSGiamDoc":
                    {
                        LoadDataTGDNM();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            Commons.Modules.sLoad = "";
        }

        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            switch (tabControl.SelectedTabPage.Name)
            {
                case "tabHoTroLuong":
                    {
                        LoadDataHTL(iThem);
                        break;
                    }
                case "tabPhanCongTo":
                    {
                        LoadDataPCT();
                        break;
                    }
                case "TabDoanhThuNhaMay":
                    {
                        LoadDataDTNM();
                        break;
                    }
                case "tabThuongHQQLKhac":
                    {
                        LoadDataTHQQLKhac(iThem);
                        break;
                    }
                case "tabDSGiamDoc":
                    {
                        LoadDataTGDNM();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            Commons.Modules.sLoad = "";
        }

        #region function

        private void VisibleButton(bool visible)
        {
            if (iLoai == 1)
            {
                tabHoTroLuong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                tabPhanCongTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                tabDSGiamDoc.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                TabDoanhThuNhaMay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                tabThuongHQQLKhac.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                tabHoTroLuong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                tabPhanCongTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                tabDSGiamDoc.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                TabDoanhThuNhaMay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                tabThuongHQQLKhac.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                tabPTBuLuong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
            windowsUIButton.Buttons[6].Properties.Visible = !visible;

            grvHTL.OptionsBehavior.Editable = !visible;
            grvPCT.OptionsBehavior.Editable = !visible;
            grvDTNM.OptionsBehavior.Editable = !visible;
            grvTHQQL.OptionsBehavior.Editable = !visible;
            grvTGDNM.OptionsBehavior.Editable = !visible;
            grvPTBuLuong.OptionsBehavior.Editable = !visible;

            cboThang.Enabled = visible;
            cboDonVi.Enabled = visible;
            cboXiNghiep.Enabled = visible;
            cboTo.Enabled = visible;
        }

        #endregion

        #region chuotphai
        private void toolCapNhat_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                string sCotCN = "";
                var data = (object)null;
                switch (tabControl.SelectedTabPage.Name)
                {
                    case "tabHoTroLuong":
                        {
                            sCotCN = grvHTL.FocusedColumn.FieldName;
                            data = grvHTL.GetFocusedRowCellValue(sCotCN);
                            dt1 = Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdHTL, grvHTL);
                            dt = (DataTable)grdHTL.DataSource;
                            break;
                        }
                    case "tabThuongHQQLKhac":
                        {
                            sCotCN = grvTHQQL.FocusedColumn.FieldName;
                            data = grvTHQQL.GetFocusedRowCellValue(sCotCN);
                            dt1 = Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdTHQQL, grvTHQQL);
                            dt = (DataTable)grdTHQQL.DataSource;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                dt.AsEnumerable().Where(row => dt1.AsEnumerable()
                                                         .Select(r => r.Field<Int64>("ID_CN"))
                                                         .Any(x => x == row.Field<Int64>("ID_CN"))
                                                         ).ToList<DataRow>().ForEach(r => r[sCotCN] = (data));
                dt.AcceptChanges();
            }
            catch
            {

            }
        }

        #endregion

        private void grvHTL_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (windowsUIButton.Buttons[0].Properties.Visible) return;
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            }
            catch { }
        }
        private void tabControl_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            try
            {
                switch (tabControl.SelectedTabPage.Name)
                {
                    case "tabHoTroLuong":
                        {
                            calThang.VistaCalendarViewStyle = VistaCalendarViewStyle.Default;
                            LoadThang(0);
                            LoadDataHTL(iThem);
                            searchControl1.Client = grdHTL;
                            break;
                        }
                    case "tabPhanCongTo":
                        {
                            calThang.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
                            LoadThang(1);
                            LoadDataPCT();
                            searchControl1.Client = grdPCT;
                            break;
                        }
                    case "TabDoanhThuNhaMay":
                        {
                            calThang.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
                            LoadThang(2);
                            LoadDataDTNM();
                            searchControl1.Client = grdDTNM;
                            break;
                        }
                    case "tabThuongHQQLKhac":
                        {
                            calThang.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
                            LoadThang(3);
                            LoadDataTHQQLKhac(iThem);
                            searchControl1.Client = grdTHQQL;
                            break;
                        }
                    case "tabDSGiamDoc":
                        {
                            calThang.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
                            LoadThang(4);
                            LoadDataTGDNM();
                            Commons.Modules.ObjSystems.DeleteAddRow(grvTGDNM);
                            searchControl1.Client = grdTGDNM;
                            break;
                        }
                    case "tabPTBuLuong":
                        {
                            calThang.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
                            LoadThang(5);
                            LoadDataPTBuLuong();
                            Commons.Modules.ObjSystems.DeleteAddRow(grvPTBuLuong);
                            searchControl1.Client = grdPTBuLuong;
                            break;
                        }
                }
            }
            catch { }
        }

        private void tabControl_SelectedPageChanging(object sender, DevExpress.XtraLayout.LayoutTabPageChangingEventArgs e)
        {
            if (iThem == 1) e.Cancel = true;
        }

        private void grvDTNM_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                grvDTNM.SetFocusedRowCellValue("ID_DTNM", 0);
            }
            catch { }
        }

        private void grvDTNM_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            grvDTNM.ClearColumnErrors();

            GridView view = sender as GridView;
            try
            {
                DataTable dt1 = new DataTable();
                dt1 = Commons.Modules.ObjSystems.ConvertDatatable(view);

                if (dt1.AsEnumerable().Where(x => x.Field<int>("ID_DV").Equals(view.GetFocusedRowCellValue("ID_DV"))).CopyToDataTable().Rows.Count > 1)
                {
                    e.Valid = false;
                    e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage("frmDoanhThuNM", "msgTrungDLLuoi");
                    view.SetColumnError(view.Columns["ID_DV"], e.ErrorText);
                    return;
                }
            }
            catch { }
        }
        private void grvPCT_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            grvDTNM.ClearColumnErrors();

            GridView view = sender as GridView;
            try
            {
                DataTable dt1 = new DataTable();
                dt1 = Commons.Modules.ObjSystems.ConvertDatatable(view);

                if (dt1.AsEnumerable().Where(x => x["ID_TO"].Equals(view.GetFocusedRowCellValue("ID_TO")) && x["ID_CN"].Equals(view.GetFocusedRowCellValue("ID_CN"))).CopyToDataTable().Rows.Count > 1)
                {
                    e.Valid = false;
                    e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage("frmDoanhThuNM", "msgTrungDLLuoi");
                    view.SetColumnError(view.Columns["ID_DV"], e.ErrorText);
                    return;
                }
            }
            catch { }
        }

        private void grvDTNM_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvDTNM_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvPCT_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                grvPCT.SetFocusedRowCellValue("ID_PCT", 0);
            }
            catch { }
        }

        private void grvPCT_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvPCT_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvTGDNM_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                grvTGDNM.SetFocusedRowCellValue("ID_TGD", 0);
            }
            catch { }
        }

        private void grvPTBuLuong_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                grvPTBuLuong.SetFocusedRowCellValue("ID_PT_BL", 0);
            }
            catch { }
        }

        private void grvTGDNM_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvTGDNM_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvPTBuLuong_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvPTBuLuong_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
        private void grvTGDNM_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            grvTGDNM.ClearColumnErrors();

            GridView view = sender as GridView;
            try
            {
                DataTable dt1 = new DataTable();
                dt1 = Commons.Modules.ObjSystems.ConvertDatatable(view);

                if (dt1.AsEnumerable().Where(x => x["ID_DV"].Equals(view.GetFocusedRowCellValue("ID_DV")) && x["ID_CN"].Equals(view.GetFocusedRowCellValue("ID_CN"))).CopyToDataTable().Rows.Count > 1)
                {
                    e.Valid = false;
                    e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage("frmDoanhThuNM", "msgTrungDLLuoi");
                    view.SetColumnError(view.Columns["ID_DV"], e.ErrorText);
                    return;
                }
            }
            catch { }
        }


    }
}