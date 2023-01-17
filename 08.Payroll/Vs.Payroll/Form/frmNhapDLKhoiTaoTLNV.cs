﻿using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using System;
using System.Data;
using Vs.Report;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using System.Drawing;
using System.Reflection;
using System.Linq;
using Microsoft.ApplicationBlocks.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Menu;
using NPOI.OpenXmlFormats.Dml.Diagram;
using static NPOI.HSSF.Util.HSSFColor;
using DevExpress.XtraEditors.DXErrorProvider;
using static DevExpress.XtraPrinting.Native.ExportOptionsPropertiesNames;

namespace Vs.Payroll
{
    public partial class frmNhapDLKhoiTaoTLNV : DevExpress.XtraEditors.XtraForm
    {
        public DateTime dNgay;
        private int iThem = 0;
        public frmNhapDLKhoiTaoTLNV()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, Root, tabControl, windowsUIButton);
        }

        //sự kiên load form
        private void frmNhapDLKhoiTaoTLNV_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            tabControl_SelectedPageChanged(null, null);
            VisibleButton(true);
            foreach (ToolStripMenuItem item in contextMenuStrip1.Items)
            {
                item.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, item.Name);
            }
            Commons.Modules.sLoad = "";
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
                        switch (tabControl.SelectedTabPageIndex)
                        {
                            case 0:
                                {
                                    Commons.Modules.ObjSystems.AddnewRow(grvPBL, true);
                                    break;
                                }
                            case 1:
                                {
                                    Commons.Modules.ObjSystems.AddnewRow(grvPTTToTruong, true);
                                    break;
                                }
                            case 2:
                                {
                                    if (grvPTTruLuong.RowCount == 0)
                                    {
                                        Commons.Modules.ObjSystems.AddnewRow(grvPTTruLuong, true);
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    Commons.Modules.ObjSystems.AddnewRow(grvTGDNM, true);
                                    break;
                                }
                        }
                        VisibleButton(false);
                        break;
                    }
                case "luu":
                    {
                        switch (tabControl.SelectedTabPageIndex)
                        {
                            case 0:
                                {
                                    grvPBL.CloseEditor();
                                    grvPBL.UpdateCurrentRow();
                                    if (grvPBL.HasColumnErrors) return;
                                    if (!SaveData("PBL", grdPBL, Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text)))
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                                        return;
                                    }
                                    LoadDataPBL();
                                    Commons.Modules.ObjSystems.DeleteAddRow(grvPBL);
                                    break;
                                }
                            case 1:
                                {
                                    grvPTTToTruong.CloseEditor();
                                    grvPTTToTruong.UpdateCurrentRow();
                                    if (grvPTTToTruong.HasColumnErrors) return;
                                    if (!SaveData("PT_THUONG_TT", grdPTTToTruong, Commons.Modules.ObjSystems.ConvertDateTime(cboThangADPTTT.Text)))
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                                        return;
                                    }
                                    LoadDataPTThuongTT();
                                    Commons.Modules.ObjSystems.DeleteAddRow(grvPTTToTruong);
                                    break;
                                }
                            case 2:
                                {
                                    grvPTTruLuong.CloseEditor();
                                    grvPTTruLuong.UpdateCurrentRow();
                                    if (grvPTTruLuong.HasColumnErrors) return;
                                    if (!SaveData("PT_TRU_LUONG", grdPTTruLuong, Commons.Modules.ObjSystems.ConvertDateTime(cboThangADPTTT.Text)))
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                                        return;
                                    }
                                    LoadDataPTTruLuong();
                                    Commons.Modules.ObjSystems.DeleteAddRow(grvPTTruLuong);
                                    break;
                                }
                            case 3:
                                {
                                    grvTGDNM.CloseEditor();
                                    grvTGDNM.UpdateCurrentRow();
                                    if (grvTGDNM.HasColumnErrors) return;
                                    if (!SaveData("THUONG_GDNM", grdTGDNM, Commons.Modules.ObjSystems.ConvertDateTime(cboThangADTGD.Text)))
                                    {
                                        Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgCapNhatKhongCong"), Commons.Form_Alert.enmType.Error);
                                        return;
                                    }
                                    LoadDataThuongGDNM();
                                    Commons.Modules.ObjSystems.DeleteAddRow(grvTGDNM);
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
                        switch (tabControl.SelectedTabPageIndex)
                        {
                            case 0:
                                {
                                    LoadDataPBL();
                                    Commons.Modules.ObjSystems.DeleteAddRow(grvPBL);
                                    break;
                                }
                            case 1:
                                {
                                    LoadDataPTThuongTT();
                                    Commons.Modules.ObjSystems.DeleteAddRow(grvPTTToTruong);
                                    break;
                                }
                            case 2:
                                {
                                    LoadDataPTTruLuong();
                                    Commons.Modules.ObjSystems.DeleteAddRow(grvPTTruLuong);
                                    break;
                                }
                            case 3:
                                {
                                    LoadDataThuongGDNM();
                                    Commons.Modules.ObjSystems.DeleteAddRow(grvTGDNM);
                                    break;
                                }
                        }
                        VisibleButton(true);
                        break;
                    }
                case "thoat":
                    {
                        this.Close();
                        break;
                    }
            }
        }
        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            switch (tabControl.SelectedTabPageIndex)
            {
                case 0:
                    {
                        cboThang.Text = calThang.DateTime.ToString("dd/MM/yyyy");
                        cboThang.ClosePopup();
                        break;
                    }
                case 1:
                    {
                        cboThangADPTTT.Text = calThang.DateTime.ToString("MM/yyyy");
                        cboThangADPTTT.ClosePopup();
                        break;
                    }
                case 2:
                    {
                        cboNgayADTruLuong.Text = calThang.DateTime.ToString("dd/MM/yyyy");
                        cboNgayADTruLuong.ClosePopup();
                        break;
                    }
                case 3:
                    {
                        cboThangADTGD.Text = calThang.DateTime.ToString("MM/yyyy");
                        cboThangADTGD.ClosePopup();
                        break;
                    }
                default:
                    {
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
                            sSql = "SELECT disTINCT RIGHT(CONVERT(VARCHAR(10),NGAY_AP_DUNG,103),7) AS THANG , CONVERT(VARCHAR(10),NGAY_AP_DUNG,103) NGAY FROM dbo.PHAN_BO_LUONG ORDER BY THANG DESC , NGAY DESC";
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
                            sSql = "SELECT disTINCT RIGHT(CONVERT(VARCHAR(10),THANG_AP_DUNG,103),7) AS THANG , CONVERT(VARCHAR(10),THANG_AP_DUNG,103) NGAY FROM dbo.PT_THUONG_TO_TRUONG ORDER BY THANG DESC , NGAY DESC";
                            dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                            Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay1, dtthang, false, true, true, true, true, this.Name);
                            grvNgay1.Columns["NGAY"].Visible = false;

                            try
                            {
                                cboThangADPTTT.Text = grvNgay1.GetFocusedRowCellValue("THANG").ToString();
                            }
                            catch
                            {
                                cboThangADPTTT.Text = DateTime.Now.ToString("MM/yyyy");
                            }
                            break;
                        }
                    case 2:
                        {
                            sSql = "SELECT disTINCT RIGHT(CONVERT(VARCHAR(10),NGAY_AP_DUNG,103),7) AS THANG , CONVERT(VARCHAR(10),NGAY_AP_DUNG,103) NGAY FROM dbo.PT_TRU_LUONG ORDER BY THANG DESC , NGAY DESC";
                            dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                            Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay1, dtthang, false, true, true, true, true, this.Name);
                            grvNgay1.Columns["THANG"].Visible = false;

                            try
                            {
                                cboNgayADTruLuong.Text = grvNgay1.GetFocusedRowCellValue("NGAY").ToString();
                            }
                            catch
                            {
                                cboNgayADTruLuong.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            }
                            break;
                        }

                    case 3:
                        {
                            sSql = "SELECT disTINCT RIGHT(CONVERT(VARCHAR(10),THANG_AP_DUNG,103),7) AS THANG , CONVERT(VARCHAR(10),THANG_AP_DUNG,103) NGAY FROM dbo.CACH_THUONG_GD_NM ORDER BY THANG DESC , NGAY DESC";
                            dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                            Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay1, dtthang, false, true, true, true, true, this.Name);
                            grvNgay1.Columns["NGAY"].Visible = false;

                            try
                            {
                                cboThangADTGD.Text = grvNgay1.GetFocusedRowCellValue("THANG").ToString();
                            }
                            catch
                            {
                                cboThangADTGD.Text = DateTime.Now.ToString("MM/yyyy");
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
        private void LoadDataPBL()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spNhapDLKhoiTaoTLNV", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "PBL";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                dt.Columns["ID_PBL"].ReadOnly = false;
                if (grdPBL.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdPBL, grvPBL, dt, true, true, false, true, true, "frmPhanBoLuong");
                    grvPBL.Columns["ID_PBL"].Visible = false;
                }
                else
                {
                    grdPBL.DataSource = dt;
                }


                dt = new DataTable();
                dt = ds.Tables[1].Copy();
                DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_CN", "MS_CN", "ID_CN", grvPBL, dt, "frmPhanBoLuong");
                cbo.EditValueChanged += cboID_CN_EditValueChanged;
                cbo.BeforePopup += cbo_BeforePopup;

                dt = new DataTable();
                dt = ds.Tables[2].Copy();
                DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo1 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo1, "ID_DV", "TEN_DV", "ID_DV", grvPBL, dt, "frmPhanBoLuong");
                //cbo.EditValueChanged += cboID_CVSX_EditValueChanged;

                LoadTextPBL();
            }
            catch { }
        }
        private void cboID_CN_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SearchLookUpEdit lookUp = sender as SearchLookUpEdit;
                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
                grvPBL.SetFocusedRowCellValue("ID_CN", Convert.ToInt64((dataRow.Row[0])));
                grvPBL.SetFocusedRowCellValue("HO_TEN", Convert.ToString(dataRow.Row[2]));
            }
            catch { }

        }
        private void cbo_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spNhapDLKhoiTaoTLNV", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "PBL";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[1].Copy();
                SearchLookUpEdit lookUp = sender as SearchLookUpEdit;
                lookUp.Properties.DataSource = dt;
                DataTable dtTmp = new DataTable();
                string sdkien = "( 1 = 1 )";
                try
                {
                    string sID = "";
                    DataTable dtTemp = new DataTable();
                    dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grvPBL).Copy();
                    for (int i = 0; i < dtTmp.Rows.Count; i++)
                    {
                        sID = sID + dtTmp.Rows[i]["ID_CN"].ToString() + ",";
                    }
                    if (dtTmp.Rows.Count != 0)
                    {
                        sID = sID.Substring(0, sID.Length - 1);
                        sdkien = "(ID_CN NOT IN (" + sID + "))";
                    }

                    dt.DefaultView.RowFilter = sdkien;
                }
                catch
                {
                    try
                    {
                        dtTmp.DefaultView.RowFilter = "";
                    }
                    catch { }
                }
            }
            catch { }
        }
        private void LoadDataPTThuongTT()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spNhapDLKhoiTaoTLNV", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "PT_THUONG_TT";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboThangADPTTT.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                adp.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0].Copy();
                if (grdPTTToTruong.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdPTTToTruong, grvPTTToTruong, dt, true, true, false, true, true, "frmPhanTramThuongTT");
                    grvPTTToTruong.Columns["ID_PT_THUONG_TT"].Visible = false;
                }
                else
                {
                    grdPTTToTruong.DataSource = dt;
                }


                dt = new DataTable();
                dt = ds.Tables[1].Copy();

                DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo1 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo1, "ID_DV", "TEN_DV", "ID_DV", grvPTTToTruong, dt, "frmPhanTramThuongTT");
                //cbo.EditValueChanged += cboID_CVSX_EditValueChanged;

                LoadTextPTTTT();
            }
            catch { }
        }
        private void LoadDataPTTruLuong()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spNhapDLKhoiTaoTLNV", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "PT_TRU_LUONG";
                cmd.Parameters.Add("@iLoai", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@Ngay", SqlDbType.DateTime).Value = Commons.Modules.ObjSystems.ConvertDateTime(cboNgayADTruLuong.Text);
                cmd.CommandType = CommandType.StoredProcedure;
                System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                adp.Fill(ds);
                DataTable dt = new DataTable();

                dt = ds.Tables[0].Copy();
                if (grdPTTruLuong.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdPTTruLuong, grvPTTruLuong, dt, true, true, false, true, true, "frmPhanTramTruLuong");
                    grvPTTruLuong.Columns["ID_PT_TRU_LUONG"].Visible = false;
                }
                else
                {
                    grdPTTruLuong.DataSource = dt;
                }
            }
            catch { }
        }
        private void LoadDataThuongGDNM()
        {
            try
            {
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spNhapDLKhoiTaoTLNV", conn);
                cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                cmd.Parameters.Add("@Tab", SqlDbType.NVarChar).Value = "THUONG_GDNM";
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
                    grvTGDNM.Columns["ID_THUONG_GDNM"].Visible = false;
                }
                else
                {
                    grdTGDNM.DataSource = dt;
                }
            }
            catch { }
        }

        private bool SaveData(string sTab, DevExpress.XtraGrid.GridControl grdData, DateTime dNgay)
        {
            try
            {
                string sBTKhoiTaoTLNL = "sBTKhoiTaoTLNL" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTKhoiTaoTLNL, (DataTable)grdData.DataSource, "");
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                DataSet ds = new DataSet();
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spNhapDLKhoiTaoTLNV", conn);
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
        private void DeleteData(DevExpress.XtraGrid.GridControl grdData, DevExpress.XtraGrid.Views.Grid.GridView grvData, string sTableName, string sIDName)
        {
            try
            {
                DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_XoaDong"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.No) return;
                string sBTDeleData = "sBTDeleData" + Commons.Modules.iIDUser;
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTDeleData, Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdData, grvData), "");
                string sSQL = "DELETE dbo." + sTableName + " FROM dbo." + sTableName + " T1 INNER JOIN " + sBTDeleData + " T2 ON T2." + sIDName + " = T1." + sIDName + "";
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSQL);
                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msg_XoaThanhCong"), Commons.Form_Alert.enmType.Success);
            }
            catch
            {
                Commons.Modules.ObjSystems.Alert(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgXoaKhongThanhCong"), Commons.Form_Alert.enmType.Error);
            }
        }
        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            switch (tabControl.SelectedTabPageIndex)
            {
                case 0:
                    {
                        LoadDataPBL();
                        break;
                    }
                case 1:
                    {
                        LoadDataPTThuongTT();
                        break;
                    }
                case 2:
                    {
                        LoadDataPTTruLuong();
                        break;
                    }
                case 3:
                    {
                        LoadDataThuongGDNM();
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
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;

            grvPBL.OptionsBehavior.Editable = !visible;
            grvPTTToTruong.OptionsBehavior.Editable = !visible;
            grvPTTruLuong.OptionsBehavior.Editable = !visible;
            grvTGDNM.OptionsBehavior.Editable = !visible;
        }

        private void LoadTextPBL()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)grdPBL.DataSource;
                lbl.Text = Commons.Modules.ObjLanguages.GetLanguage("frmPhanBoLuong", "lblPhanTramPhanBo") + " : " + Convert.ToDouble(dt.Compute("Sum(PHAN_TRAM)", "")) + "%";
            }
            catch
            {
                lbl.Text = Commons.Modules.ObjLanguages.GetLanguage("frmPhanBoLuong", "lblPhanTramPhanBo") + " : 0%";
            }
        }

        private void LoadTextPTTTT() // tab 2
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)grdPTTToTruong.DataSource;
                lbl1.Text = Commons.Modules.ObjLanguages.GetLanguage("frmPhanBoLuong", "lblPhanTramThuong") + " : " + Convert.ToDouble(dt.Compute("Sum(PHAN_TRAM)", "")) + "%";
            }
            catch
            {
                lbl1.Text = Commons.Modules.ObjLanguages.GetLanguage("frmPhanBoLuong", "lblPhanTramThuong") + " : 0%";
            }
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
                switch (tabControl.SelectedTabPageIndex)
                {
                    case 0:
                        {
                            sCotCN = grvPBL.FocusedColumn.FieldName;
                            data = grvPBL.GetFocusedRowCellValue(sCotCN);
                            dt1 = Commons.Modules.ObjSystems.GetDataTableMultiSelect(grdPBL, grvPBL);
                            dt = (DataTable)grdPBL.DataSource;
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
                Commons.Modules.sLoad = "0Load";
                switch (tabControl.SelectedTabPageIndex)
                {
                    case 0:
                        {
                            calThang.VistaCalendarViewStyle = VistaCalendarViewStyle.Default;
                            LoadThang(0);
                            LoadDataPBL();
                            break;
                        }
                    case 1:
                        {
                            calThang.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
                            LoadThang(1);
                            LoadDataPTThuongTT();
                            break;
                        }
                    case 2:
                        {
                            calThang.VistaCalendarViewStyle = VistaCalendarViewStyle.Default;
                            LoadThang(2);
                            LoadDataPTTruLuong();
                            break;
                        }
                    case 3:
                        {
                            calThang.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
                            LoadThang(3);
                            LoadDataThuongGDNM();
                            break;
                        }
                }
                Commons.Modules.sLoad = "";
            }
            catch { }
        }

        private void tabControl_SelectedPageChanging(object sender, DevExpress.XtraLayout.LayoutTabPageChangingEventArgs e)
        {
            if (!windowsUIButton.Buttons[0].Properties.Visible) e.Cancel = true;
        }

        private void grvPBL_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                grvPBL.SetFocusedRowCellValue("ID_PBL", 0);
            }
            catch { }
        }

        private void grvPTTToTruong_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                grvPTTToTruong.SetFocusedRowCellValue("ID_PT_THUONG_TT", 0);
            }
            catch { }
        }

        private void grvPTTruLuong_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                grvPTTruLuong.SetFocusedRowCellValue("ID_PT_TRU_LUONG", 0);
            }
            catch { }
        }

        private void grvTGDNM_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                grvTGDNM.SetFocusedRowCellValue("ID_THUONG_GDNM", 0);
            }
            catch { }
        }

        private void grvNgay1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                switch (tabControl.SelectedTabPageIndex)
                {
                    case 0:
                        {
                            cboThang.Text = grv.GetFocusedRowCellValue("NGAY").ToString();
                            cboThang.ClosePopup();
                            break;
                        }
                    case 1:
                        {
                            cboThangADPTTT.Text = grv.GetFocusedRowCellValue("THANG").ToString();
                            cboThangADPTTT.ClosePopup();
                            break;
                        }
                    case 2:
                        {
                            cboNgayADTruLuong.Text = grv.GetFocusedRowCellValue("NGAY").ToString();
                            cboNgayADTruLuong.ClosePopup();
                            break;
                        }
                }
            }
            catch { }
        }

        #region CellValueChanged
        private void grvPBL_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                if (e.Column.FieldName == "PHAN_TRAM")
                {
                    LoadTextPBL();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void grvPTTToTruong_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                if (e.Column.FieldName == "PHAN_TRAM")
                {
                    LoadTextPTTTT();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region ValidateRow
        private void grvPBL_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            GridView view = sender as GridView;

            DevExpress.XtraGrid.Columns.GridColumn ptPhanBo = view.Columns["PHAN_TRAM"];

            try
            {
                DataTable dt1 = new DataTable();
                dt1 = Commons.Modules.ObjSystems.ConvertDatatable(view);
                if (Convert.ToDouble(dt1.Compute("Sum(PHAN_TRAM)", "")) > 100)
                {
                    e.Valid = false;
                    view.SetColumnError(ptPhanBo, Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgPhanTramPhanBoKhongHopLe"));
                    return;
                }
            }
            catch
            {

            }
        }

        private void grvPBL_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvPBL_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
        private void grvPTTToTruong_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            grvPTTToTruong.ClearColumnErrors();

            GridView view = sender as GridView;
            DevExpress.XtraGrid.Columns.GridColumn PhanTram = view.Columns["PHAN_TRAM"];

            try
            {
                DataTable dt1 = new DataTable();
                dt1 = Commons.Modules.ObjSystems.ConvertDatatable(view);
                if (Convert.ToDouble(dt1.Compute("Sum(PHAN_TRAM)", "")) > 100)
                {
                    e.Valid = false;
                    view.SetColumnError(PhanTram, Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgPhanTramKhongHopLe"));
                    return;
                }

                if (dt1.AsEnumerable().Where(x => x.Field<Int64>("ID_DV").Equals(view.GetFocusedRowCellValue("ID_DV"))).CopyToDataTable().Rows.Count > 1)
                {
                    e.Valid = false;
                    e.ErrorText = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "msgTrungDLLuoi");
                    view.SetColumnError(view.Columns["ID_DV"], e.ErrorText);
                    return;
                }
            }
            catch
            {

            }
        }

        private void grvPTTToTruong_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvPTTToTruong_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        #endregion


    }
}