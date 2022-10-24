using Commons;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Vs.Report;

namespace Vs.TimeAttendance
{
    public partial class ucViPhamNoiQuyLD : DevExpress.XtraEditors.XtraUserControl
    {
        private bool iAdd = false;
        private string ChuoiKT = "";
        public static ucViPhamNoiQuyLD _instance;
        public static ucViPhamNoiQuyLD Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucViPhamNoiQuyLD();
                return _instance;
            }
        }
        string sBT = "tabKeHoachDiCa" + Commons.Modules.ModuleName;
        public ucViPhamNoiQuyLD()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
        }
        private void ucViPhamNoiQuyLD_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboDV, Commons.Modules.ObjSystems.DataDonVi(false), "ID_DV", "TEN_DV", "TEN_DV");
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadThang();
            LoadGrdCongNhan();
            radTinHTrang_SelectedIndexChanged(null, null);
            LoadGrdVPNoiQuy();
            grvCongNhan_FocusedRowChanged(null, null);
            Commons.Modules.sLoad = "";
            if (Modules.iPermission != 1)
            {
                windowsUIButton.Buttons[0].Properties.Visible = false;
                windowsUIButton.Buttons[1].Properties.Visible = false;
                windowsUIButton.Buttons[2].Properties.Visible = false;
                windowsUIButton.Buttons[4].Properties.Visible = false;
                windowsUIButton.Buttons[7].Properties.Visible = false;
                windowsUIButton.Buttons[8].Properties.Visible = false;
            }
            else
            {
                enableButon(true);
            }
            grvViPhamNoiQuyLD.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdCongNhan();
            LoadGrdVPNoiQuy();
            grvCongNhan_FocusedRowChanged(null, null);
            Commons.Modules.sLoad = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdCongNhan();
            LoadGrdVPNoiQuy();
            grvCongNhan_FocusedRowChanged(null, null);
            Commons.Modules.sLoad = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdCongNhan();
            LoadGrdVPNoiQuy();
            grvCongNhan_FocusedRowChanged(null, null);
            Commons.Modules.sLoad = "";
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                WindowsUIButton btn = e.Button as WindowsUIButton;
                XtraUserControl ctl = new XtraUserControl();
                switch (btn.Tag.ToString())
                {

                    case "themsua":
                        {
                            iAdd = true;
                            LoadGrdCongNhan();
                            LoadGrdVPNoiQuy();
                            grvCongNhan_FocusedRowChanged(null, null);
                            Commons.Modules.ObjSystems.AddnewRow(grvViPhamNoiQuyLD, true);
                            enableButon(false);
                            break;
                        }
                    case "xoa":
                        {
                            DeleteVPNQ();
                            LoadGrdCongNhan();
                            LoadGrdVPNoiQuy();
                            grvCongNhan_FocusedRowChanged(null, null);
                            break;
                        }
                    case "In":
                        {
                            System.Data.SqlClient.SqlConnection conn;
                            DataTable dt = new DataTable();
                            frmViewReport frm = new frmViewReport();
                            frm.rpt = new rptBCViPhamNoiQuyLD(Convert.ToDateTime(cboThang.Text), "");

                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptViPhamNoiQuyLD", conn);
                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@THANG", SqlDbType.DateTime).Value = Convert.ToDateTime(cboThang.EditValue);
                            cmd.Parameters.Add("@DVi", SqlDbType.Int).Value = cboDV.EditValue;
                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = cboXN.EditValue;
                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = cboTo.EditValue;
                            cmd.CommandType = CommandType.StoredProcedure;

                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            dt = new DataTable();
                            dt = ds.Tables[0].Copy();
                            dt.TableName = "DATA";
                            frm.AddDataSource(dt);

                            frm.ShowDialog();

                            break;
                        }
                    case "luu":
                        {
                            grvViPhamNoiQuyLD.CloseEditor();
                            grvViPhamNoiQuyLD.UpdateCurrentRow();
                            Validate();
                            if (grvViPhamNoiQuyLD.HasColumnErrors) return;
                            DataTable dt = new DataTable();
                            dt = (DataTable)grdViPhamNoiQuyLD.DataSource;
                            if (!KiemTraLuoi(dt)) return;
                            if (Savedata() == false)
                            {
                                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                            }
                            iAdd = false;
                            LoadGrdCongNhan();
                            LoadGrdVPNoiQuy();
                            Commons.Modules.ObjSystems.DeleteAddRow(grvViPhamNoiQuyLD);
                            grvCongNhan_FocusedRowChanged(null, null);
                            LoadThang();
                            enableButon(true);

                            break;
                        }
                    case "khongluu":
                        {
                            iAdd = false;
                            LoadGrdCongNhan();
                            LoadGrdVPNoiQuy();
                            Commons.Modules.ObjSystems.DeleteAddRow(grvViPhamNoiQuyLD);
                            grvCongNhan_FocusedRowChanged(null, null);
                            enableButon(true);
                            break;
                        }
                    case "thoat":
                        {
                            Commons.Modules.ObjSystems.GotoHome(this);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

            }
            catch { }

        }

        #region hàm xử lý dữ liệu
        private void LoadGrdCongNhan()
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCongNhanVPNoiQuy", cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage, iAdd, Commons.Modules.ObjSystems.ConvertDateTime(cboThang.Text)));
                dt.Columns["CHON"].ReadOnly = false;
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdCongNhan, grvCongNhan, dt, false, true, true, true, true, this.Name);
                grvCongNhan.Columns["ID_CN"].Visible = false;
                grvCongNhan.Columns["TINH_TRANG"].Visible = false;
                grvCongNhan.Columns["CHON"].Visible = false;
                if (iAdd)
                {
                    dt.Columns["CDL"].ReadOnly = false;
                    grvCongNhan.OptionsSelection.MultiSelect = true;
                    grvCongNhan.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                    try
                    {
                        grvCongNhan.Columns["CDL"].Visible = false;
                        grvCongNhan.OptionsSelection.CheckBoxSelectorField = "CHON";
                        grvCongNhan.Columns["CHON"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    }
                    catch { }
                }
                else
                {
                    grvCongNhan.OptionsSelection.MultiSelect = false;
                    grvCongNhan.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                }


                Commons.Modules.sLoad = "";
            }
            catch (Exception ex)
            {
            }
        }
        private void LoadGrdVPNoiQuy()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListVI_PHAM_NQLD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, Convert.ToDateTime(cboThang.EditValue), cboDV.EditValue, cboXN.EditValue, cboTo.EditValue));

                dt.Columns["ID_NQLD"].ReadOnly = false;
                dt.Columns["NGAY"].ReadOnly = false;
                dt.Columns["ID_VPNQ"].ReadOnly = false;
                if (grdViPhamNoiQuyLD.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdViPhamNoiQuyLD, grvViPhamNoiQuyLD, dt, true, true, false, true, true, this.Name);
                    grvViPhamNoiQuyLD.Columns["ID_CN"].Visible = false;
                    grvViPhamNoiQuyLD.Columns["ID_VPNQ"].Visible = false;
                }
                else
                {
                    grdViPhamNoiQuyLD.DataSource = dt;
                }

                DataTable dt1 = new DataTable();
                dt1.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNQLD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));

                RepositoryItemLookUpEdit cboNQLD = new RepositoryItemLookUpEdit();
                cboNQLD.NullText = "";
                cboNQLD.ValueMember = "ID_NQLD";
                cboNQLD.DisplayMember = "NOI_DUNG";
                cboNQLD.DataSource = dt1;
                cboNQLD.Columns.Clear();

                cboNQLD.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NOI_DUNG"));
                cboNQLD.Columns["NOI_DUNG"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "NOI_DUNG");

                cboNQLD.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboNQLD.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                grvViPhamNoiQuyLD.Columns["ID_NQLD"].ColumnEdit = cboNQLD;

                cboNQLD.BeforePopup += cboNQLD_BeforePopup;
                cboNQLD.EditValueChanged += cboNQLD_EditValueChanged;
            }
            catch
            {

            }
        }
        private void cboNQLD_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetComboNQLD", Commons.Modules.UserName, Commons.Modules.TypeLanguage, 0));
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = dt;
                DataTable dtTmp = new DataTable();
                string sdkien = "( 1 = 1 )";
                try
                {
                    string sID = "";
                    DataTable dtTemp = new DataTable();
                    dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grvViPhamNoiQuyLD).Copy();
                    for (int i = 0; i < dtTmp.Rows.Count; i++)
                    {
                        sID = sID + dtTmp.Rows[i]["ID_NQLD"].ToString() + ",";
                    }
                    sID = sID.Substring(0, sID.Length - 1);
                    sdkien = "(ID_NQLD NOT IN (" + sID + "))";
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
        private void cboNQLD_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            try
            {
                grvViPhamNoiQuyLD.SetFocusedRowCellValue("ID_NQLD", dataRow[0]);
                grvViPhamNoiQuyLD.SetFocusedRowCellValue("ID_CN", grvCongNhan.GetFocusedRowCellValue("ID_CN"));
                grvCongNhan.SetFocusedRowCellValue("CDL", 1);
            }
            catch
            {

            }
        }

        private void CboMSCa_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                if (sender is LookUpEdit cbo)
                {
                    int IDNHOM = Convert.ToInt32(grvViPhamNoiQuyLD.GetFocusedRowCellValue("ID_NHOM"));
                    cbo.Properties.DataSource = null;
                    cbo.Properties.DataSource = Commons.Modules.ObjSystems.DataCa(IDNHOM);
                }
            }
            catch
            {
            }
        }

        private bool Savedata()
        {
            DataTable dkVPNoiQuyLD = new DataTable();
            string stbVPNoiQuyLD = "grvVPNoiQuyLD" + Commons.Modules.UserName;

            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbVPNoiQuyLD, (DataTable)grdViPhamNoiQuyLD.DataSource, "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "sPsaveVPNoiQuyLD", stbVPNoiQuyLD);
                Commons.Modules.ObjSystems.XoaTable(stbVPNoiQuyLD);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                Commons.Modules.ObjSystems.XoaTable(stbVPNoiQuyLD);
                return false;
            }
        }
        private void enableButon(bool visible)
        {
            windowsUIButton.Buttons[0].Properties.Visible = visible;
            windowsUIButton.Buttons[1].Properties.Visible = visible;
            windowsUIButton.Buttons[2].Properties.Visible = visible;
            windowsUIButton.Buttons[3].Properties.Visible = visible;
            windowsUIButton.Buttons[4].Properties.Visible = !visible;
            windowsUIButton.Buttons[5].Properties.Visible = !visible;
            windowsUIButton.Buttons[6].Properties.Visible = visible;

            cboDV.Properties.ReadOnly = !visible;
            cboXN.Properties.ReadOnly = !visible;
            cboTo.Properties.ReadOnly = !visible;
            cboThang.Properties.ReadOnly = !visible;

            grvViPhamNoiQuyLD.OptionsBehavior.Editable = !visible;
        }
        public void LoadThang()
        {
            try
            {
                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),NGAY,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),NGAY,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),NGAY,103),7) AS NGAY FROM dbo.VI_PHAM_NOI_QUY_LD ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                if (grdThang.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdThang, grvThang, dtthang, false, true, true, true, true, this.Name);
                    grvThang.Columns["M"].Visible = false;
                    grvThang.Columns["Y"].Visible = false;
                }
                else
                {
                    grdThang.DataSource = dtthang;
                }


                cboThang.Text = grvThang.GetFocusedRowCellValue("NGAY").ToString();
            }
            catch (Exception ex)
            {
                cboThang.Text = DateTime.Now.Month + "/" + DateTime.Now.Year;
            }
        }

        private void LoadNull()
        {
            try
            {
                if (cboThang.Text == "") cboThang.Text = DateTime.Now.ToShortDateString();
            }
            catch (Exception ex)
            {
                cboThang.Text = "";
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion

        private void radTinHTrang_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtTmp = new DataTable();
            string sdkien = "( 1 = 1 )";
            try
            {
                dtTmp = (DataTable)grdCongNhan.DataSource;
                if (radTinHTrang.SelectedIndex == 0) sdkien = "(TINH_TRANG = 1)";
                if (radTinHTrang.SelectedIndex == 1) sdkien = "(TINH_TRANG = 0)";
                dtTmp.DefaultView.RowFilter = sdkien;
            }
            catch
            {
                try
                {
                    dtTmp.DefaultView.RowFilter = "";
                }
                catch { }
            }
            grvCongNhan_FocusedRowChanged(null, null);
        }

        private void grvCongNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            DataTable dtTmp = new DataTable();
            String sIDCN;
            try
            {
                dtTmp = (DataTable)grdViPhamNoiQuyLD.DataSource;

                string sDK = "";
                sIDCN = "-1";
                try { sIDCN = grvCongNhan.GetFocusedRowCellValue("ID_CN").ToString(); } catch { }
                if (sIDCN != "-1")
                {
                    sDK = " ID_CN = '" + sIDCN + "' ";
                }
                else
                {
                    sDK = "1 = 0";
                }

                dtTmp.DefaultView.RowFilter = sDK;
            }
            catch { }
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

        private void grvThang_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboThang.Text = grvThang.GetFocusedRowCellValue("NGAY").ToString();
            }
            catch { LoadNull(); }
            cboThang.ClosePopup();
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

        private void cboThang_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadGrdCongNhan();
            LoadGrdVPNoiQuy();
            grvCongNhan_FocusedRowChanged(null, null);
            Commons.Modules.sLoad = "";
        }

        private void grvCongNhan_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(grvCongNhan.GetRowCellValue(e.RowHandle, grvCongNhan.Columns["CDL"])) == false)
                {
                    e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    e.HighPriority = true;
                }
                else
                {
                    e.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFCC");
                    e.HighPriority = true;
                }

            }
            catch
            {
            }
        }

        private void grdViPhamNoiQuyLD_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete && windowsUIButton.Buttons[0].Properties.Visible == false)
                {
                    grvViPhamNoiQuyLD.DeleteSelectedRows();
                    DataTable dt = new DataTable();
                    dt = Commons.Modules.ObjSystems.ConvertDatatable(grvViPhamNoiQuyLD);
                    dt = new DataTable();
                    dt = ((DataTable)grdViPhamNoiQuyLD.DataSource);
                    dt.AcceptChanges();
                    if (dt.Rows.Count == 0)
                    {
                        grvCongNhan.SetFocusedRowCellValue("CDL", 0);
                    }
                }

            }
            catch { }
        }
        public bool KiemKyTu(string strInput, string strChuoi)
        {

            if (strChuoi == "") strChuoi = ChuoiKT;

            for (int i = 0; i < strInput.Length; i++)
            {
                for (int j = 0; j < strChuoi.Length; j++)
                {
                    if (strInput[i] == strChuoi[j])
                    {
                        return true;
                    }
                }
            }
            if (strInput.Contains("//"))
            {
                return true;
            }
            return false;
        }

        public bool KiemDuLieu(GridView grvData, DataRow dr, string sCot, Boolean bKiemNull, int iDoDaiKiem, string sform)
        {
            string sDLKiem;
            try
            {
                sDLKiem = dr[sCot].ToString();
                if (bKiemNull)
                {
                    if (string.IsNullOrEmpty(sDLKiem))
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongDuocTrong"));
                        return false;
                    }
                    else
                    {
                        if (KiemKyTu(sDLKiem, ChuoiKT))  //KiemKyTu
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgCoChuaKyTuDB"));
                            return false;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(sDLKiem))
                    {
                        if (KiemKyTu(sDLKiem, ChuoiKT))  //KiemKyTu
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgCoChuaKyTuDB"));
                            dr["XOA"] = 1;
                            return false;
                        }
                    }
                }
                if (iDoDaiKiem != 0)
                {
                    if (sDLKiem.Length > iDoDaiKiem)
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgDoDaiKyTuVuocQua " + iDoDaiKiem));
                        return false;
                    }
                }
            }
            catch
            {
                dr.SetColumnError(sCot, "error");
                return false;
            }
            return true;
        }
        public bool KiemDuLieuNgay(GridView grvData, DataRow dr, string sCot, Boolean bKiemNull, string sform)
        {
            string sDLKiem;
            sDLKiem = dr[sCot].ToString();
            DateTime DLKiem;

            try
            {

                if (bKiemNull)
                {
                    if (string.IsNullOrEmpty(sDLKiem))
                    {
                        dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongduocTrong"));
                        return false;
                    }
                    else
                    {
                        //sDLKiem = DateTime.ParseExact(sDLKiem, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString();
                        if (!DateTime.TryParse(sDLKiem, out DLKiem))
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
                            return false;
                        }

                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(sDLKiem))
                    {
                        if (!DateTime.TryParse(sDLKiem, out DLKiem))
                        {
                            dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
                            return false;
                        }
                    }
                }
            }
            catch
            {
                dr.SetColumnError(sCot, Commons.Modules.ObjLanguages.GetLanguage(sform, "msgKhongPhaiNgay"));
                return false;
            }
            return true;
        }
        public bool KiemTrungDL(GridView grvData, DataTable dt, DataRow dr, string sCot, string sDLKiem, string tabName, string ColName, string sform)
        {
            string sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDL");
            try
            {
                if (dt.AsEnumerable().Where(x => x["ID_NQLD"].Equals(dr["ID_NQLD"]) && x["NGAY"].Equals(Convert.ToDateTime(dr["NGAY"])) && x["ID_CN"].Equals(dr["ID_CN"])).CopyToDataTable().Rows.Count > 1)
                {
                    sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDLLuoi");
                    dr.SetColumnError(sCot, sTenKTra);
                    return false;
                }
                else
                {
                    //if (Convert.ToInt32(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT COUNT(*) FROM dbo.[" + tabName + "] WHERE ID_CN = " + dr["ID_CN"] + " AND NGAY = '" + Convert.ToDateTime(cboNgay.Text).ToString("MM/dd/yyyy") + "' AND CA = N'"+sDLKiem.Substring(0, sDLKiem.IndexOf(';')) +"' AND GIO_BD = '"+ dr["GIO_BD"] + "'")) > 0)
                    //{
                    //    sTenKTra = Commons.Modules.ObjLanguages.GetLanguage(sform, "msgTrungDLCSDL");
                    //    dr.SetColumnError(sCot, sTenKTra);
                    //    return false;
                    //}
                }
                return true;
            }
            catch (Exception ex)
            {
                dr.SetColumnError(sCot, sTenKTra);
                return false;
            }
        }
        private bool KiemTraLuoi(DataTable dtSource)
        {
            int errorCount = 0;
            #region kiểm tra dữ liệu
            this.Cursor = Cursors.WaitCursor;
            foreach (DataRow dr in dtSource.Rows)
            {
                dr.ClearErrors();
                //Số hợp đồng lao động
                if (!KiemDuLieuNgay(grvViPhamNoiQuyLD, dr, "NGAY", true, this.Name))
                {
                    try
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = (DataTable)grdCongNhan.DataSource;
                        dt1.PrimaryKey = new DataColumn[] { dt1.Columns["ID_CN"] };
                        int index = dt1.Rows.IndexOf(dt1.Rows.Find(dr["ID_CN"]));
                        DataRow dr1 = dt1.Rows[index];
                        dr1.SetColumnError("MS_CN", "Error");
                    }
                    catch (Exception ex) { }
                    errorCount++;
                }
                string sID_NQLD = dr["ID_NQLD"].ToString();
                if (!KiemTrungDL(grvViPhamNoiQuyLD, dtSource, dr, "ID_NQLD", sID_NQLD, "VI_PHAM_NOI_QUY_LD", "ID_NQLD", this.Name))
                {
                    try
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = (DataTable)grdCongNhan.DataSource;
                        dt1.PrimaryKey = new DataColumn[] { dt1.Columns["ID_CN"] };
                        int index = dt1.Rows.IndexOf(dt1.Rows.Find(dr["ID_CN"]));
                        DataRow dr1 = dt1.Rows[index];
                        dr1.SetColumnError("MS_CN", "Error");
                    }
                    catch (Exception ex) { }

                    errorCount++;
                }
            }
            #endregion
            Commons.Modules.ObjSystems.HideWaitForm();
            if (errorCount != 0)
            {
                this.Cursor = Cursors.Default;
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuChuaHopLe"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                this.Cursor = Cursors.Default;
                return true;
            }
        }
        #region chuotphai
        class RowInfo
        {
            public RowInfo(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
            {
                this.RowHandle = rowHandle;
                this.View = view;
            }
            public DevExpress.XtraGrid.Views.Grid.GridView View;
            public int RowHandle;
        }
        public DXMenuItem MCreateMenuCapNhatAll(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblCapNhatViPham", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(CapNhatAll));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }
        public void CapNhatAll(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
                    string sBTNoiDung = "sBTNoiDungCapNhat" + Commons.Modules.iIDUser;
                    string sBTNoiDung_Cu = "sBTNoiDungCu" + Commons.Modules.iIDUser;
                    try
                    {
                        DataTable dt_capnhat = new DataTable();
                        dt_capnhat = ((DataTable)grdViPhamNoiQuyLD.DataSource).DefaultView.ToTable().Copy();
                        if (dt_capnhat.Rows.Count == 0) return;

                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, Commons.Modules.ObjSystems.ConvertDatatable(grdCongNhan), "");
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTNoiDung, dt_capnhat, "");
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTNoiDung_Cu, Commons.Modules.ObjSystems.ConvertDatatable(grdViPhamNoiQuyLD), "");
                        System.Data.SqlClient.SqlConnection conn;
                        conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                        conn.Open();
                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("spCapNhatVPNQ", conn);

                        cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                        cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                        cmd.Parameters.Add("@sBT", SqlDbType.NVarChar).Value = sBTCongNhan;
                        cmd.Parameters.Add("@sBT1", SqlDbType.NVarChar).Value = sBTNoiDung;
                        cmd.Parameters.Add("@sBT2", SqlDbType.NVarChar).Value = sBTNoiDung_Cu;
                        cmd.CommandType = CommandType.StoredProcedure;
                        System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adp.Fill(ds);
                        DataTable dt = new DataTable();
                        dt = ds.Tables[0].Copy();
                        grdViPhamNoiQuyLD.DataSource = dt;

                        dt = new DataTable();
                        dt = ds.Tables[1].Copy();
                        grdCongNhan.DataSource = dt;
                        grvCongNhan_FocusedRowChanged(null, null);
                        Commons.Modules.ObjSystems.XoaTable(sBTCongNhan);
                        Commons.Modules.ObjSystems.XoaTable(sBTNoiDung);
                    }
                    catch (Exception ex)
                    {
                        Commons.Modules.ObjSystems.XoaTable(sBTCongNhan);
                        Commons.Modules.ObjSystems.XoaTable(sBTNoiDung);
                    }
                }
                catch (Exception ex)
                {
                }
            }
            catch (Exception ex) { }
        }
        private void grvViPhamNoiQuyLD_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();
                    if (windowsUIButton.Buttons[0].Properties.Visible) return;
                    //if (grvViPhamNoiQuyLD.FocusedColumn.FieldName.ToString() == "MS_CN" || grvDSUngVien.FocusedColumn.FieldName.ToString() == "HO_TEN") return;
                    DevExpress.Utils.Menu.DXMenuItem itemCapNhatAll = MCreateMenuCapNhatAll(view, irow);
                    e.Menu.Items.Add(itemCapNhatAll);
                    //if (flag == false) return;
                }
            }
            catch
            {
            }
        }

        #endregion

        private void grvViPhamNoiQuyLD_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {
                grvViPhamNoiQuyLD.SetFocusedRowCellValue("ID_VPNQ", 0);
            }
            catch { }
        }

        private void grvViPhamNoiQuyLD_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvViPhamNoiQuyLD_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }
        private void DeleteVPNQ()
        {
            try
            {
                if (grvViPhamNoiQuyLD.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
                DialogResult res = XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgDeleteDangKyLamThem"), Commons.Modules.ObjLanguages.GetLanguage("frmChung", "msgfrmThongBao"), MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes)
                {
                    string sBT = "sBTDKLT" + Commons.Modules.iIDUser;
                    try
                    {
                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grdViPhamNoiQuyLD), "");
                        string sSql = "DELETE FROM dbo.VI_PHAM_NOI_QUY_LD FROM dbo.VI_PHAM_NOI_QUY_LD T1 INNER JOIN " + sBT + " T2 ON T1.ID_VPNQ = T2.ID_VPNQ";
                        SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                        Commons.Modules.ObjSystems.XoaTable(sBT);
                    }
                    catch
                    {
                        Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
                    }
                }
                else if (res == DialogResult.No)
                {
                    try
                    {

                        Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBT, Commons.Modules.ObjSystems.ConvertDatatable(grvViPhamNoiQuyLD), "");
                        string sSql = "DELETE FROM dbo.VI_PHAM_NOI_QUY_LD FROM dbo.VI_PHAM_NOI_QUY_LD T1 INNER JOIN " + sBT + " T2 ON T1.ID_VPNQ = T2.ID_VPNQ";
                        SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                        Commons.Modules.ObjSystems.XoaTable(sBT);
                    }
                    catch
                    {
                        Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
                    }
                }
                else
                {
                    return;
                }
            }
            catch
            {
                Commons.Modules.ObjSystems.XoaTable(sBT);
            }
        }
    }
}
