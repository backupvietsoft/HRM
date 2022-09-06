using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Vs.Report;

namespace Vs.Recruit
{
    public partial class ucPhongVan : DevExpress.XtraEditors.XtraUserControl
    {
        private Int64 iID_PV = 0;
        public ucPhongVan()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup> { Root }, btnALL);
        }
        #region even
        private void ucPhongVan_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";
            datTuNgay.DateTime = DateTime.Now.Date.AddDays(-DateTime.Now.Date.Day + 1);
            LoadCbo();
            LoadgrdPV(-1);
            BindingData(false);
            enableButon(true);
            Commons.Modules.sLoad = "";
            cboTTLoc_EditValueChanged(null, null);
            Commons.Modules.ObjSystems.SetPhanQuyen(btnALL);
            Commons.Modules.ObjSystems.DeleteAddRow(grvViTri);
            foreach (ToolStripMenuItem item in contextMenuStrip1.Items)
            {
                item.Text = Commons.Modules.ObjLanguages.GetLanguage(this.Name, item.Name);
            }
        }
        private void LoadgrdPV(Int64 iID)
        {
            try
            {
                DataTable dt = new DataTable();

                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListPV", datTuNgay.DateTime, datTuNgay.DateTime.AddMonths(1).AddDays(-1), Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_PV"] };
                if (grdPV.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdPV, grvPV, dt, false, false, false, true, true, this.Name);

                    grvPV.Columns["ID_PV"].Visible = false;
                    grvPV.Columns["PV_ON_OF_LINE"].Visible = false;
                    grvPV.Columns["ID_KHPV"].Visible = false;
                    grvPV.Columns["BUOC_PV"].Visible = false;
                    grvPV.Columns["NGUOI_PV_2"].Visible = false;
                    grvPV.Columns["NOI_DUNG_PV"].Visible = false;
                    grvPV.Columns["TG_BD"].Visible = false;
                    grvPV.Columns["TG_KT"].Visible = false;
                    grvPV.Columns["TINH_TRANG"].Visible = false;
                    grvPV.Columns["THONG_TIN"].Visible = false;
                    Commons.Modules.ObjSystems.AddCombXtra("ID_CN", "TEN_CN", "NGUOI_PV_1", grvPV, Commons.Modules.ObjSystems.DataCongNhan(false), true, "ID_CN", this.Name, true);
                }
                else
                {
                    grdPV.DataSource = dt;
                }
                if (iID != -1)
                {
                    int index = dt.Rows.IndexOf(dt.Rows.Find(iID));
                    grvPV.FocusedRowHandle = grvPV.GetRowHandle(index);
                }
            }
            catch
            {
            }
        }
        private void LoadgrdViTri()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_PV,A.ID_YCTD,A.ID_VTTD,SUM(B.SL_TUYEN) AS SL_TUYEN FROM PVUV_VTTD A INNER JOIN YCTD_VI_TRI_TUYEN B ON B.ID_VTTD = A.ID_VTTD AND B.ID_YCTD = A.ID_YCTD WHERE ID_PV  = " + iID_PV + " GROUP BY ID_PV,A.ID_YCTD,A.ID_VTTD"));
                dt.Columns["ID_YCTD"].ReadOnly = false;
                if (grdViTri.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdViTri, grvViTri, dt, false, false, false, true, true, this.Name);
                    grvViTri.Columns["ID_PV"].Visible = false;
                    //ID_VTTD,TEN_VTTD
                    Commons.Modules.ObjSystems.AddCombXtra("ID_YCTD", "MA_YCTD", grvViTri, Commons.Modules.ObjSystems.DataYeuCauTD(false, -1), true, "ID_YCTD", this.Name, true);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_VTTD", "TEN_VTTD", grvViTri, Commons.Modules.ObjSystems.DataViTri(-1, false), true, "ID_VTTD", this.Name, true);
                }
                else
                {
                    grdViTri.DataSource = dt;
                }
            }
            catch
            {
            }
        }

        private void LoadgrdUVPV(Boolean bBT)
        {
            try
            {
                DataTable dt = new DataTable();
                try
                {
                    //if (bBT == false)
                    //{
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT ID_PV,(SELECT TOP 1 HO + ' '+ TEN AS HO_TEN FROM dbo.UNG_VIEN WHERE ID_UV = A.ID_UV) AS HO_TEN,ID_YCTD,ID_VTTD,ID_UV,NOI_DUNG_PV,KET_QUA_PV,DAT  FROM dbo.UNG_VIEN_PHONG_VAN A WHERE ID_PV = " + iID_PV + " "));
                    //}
                    //else
                    //{
                    //    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, "SELECT * FROM dbo.sBTChonUV" + Commons.Modules.UserName));
                    //}
                }
                catch
                {
                }
                if (grdUVPV.DataSource == null)
                {
                    dt.Columns["ID_UV"].ReadOnly = true;
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdUVPV, grvUVPV, dt, false, false, true, true, true, this.Name);
                    Commons.Modules.ObjSystems.AddCombXtra("ID_UV", "MS_UV", grvUVPV, Commons.Modules.ObjSystems.DataUngVienTheoTT(false, 1), true, "ID_UV", this.Name, true);

                    grvUVPV.Columns["ID_PV"].Visible = false;
                    grvUVPV.Columns["ID_YCTD"].Visible = false;
                    grvUVPV.Columns["ID_VTTD"].Visible = false;

                    grvUVPV.Columns["ID_UV"].OptionsColumn.AllowEdit = false;
                }
                else
                {
                    grdUVPV.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnALL_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "them":
                    {
                        Commons.Modules.ObjSystems.AddnewRow(grvUVPV, false);
                        iID_PV = -1;
                        enableButon(false);
                        BindingData(true);
                        break;
                    }
                case "sua":
                    {
                        if (Convert.ToInt32(cboTinhTrang.EditValue) != 1)
                        {
                            XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuDaPhatSinhKhongSua"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (txtSO_PV.EditValue.ToString() == "") return;
                        Commons.Modules.ObjSystems.AddnewRow(grvUVPV, false);
                        enableButon(false);
                        break;
                    }
                case "xoa":
                    {
                       
                        XoaPhongVan();
                        break;

                    }
                case "In":
                    {
                        if (grvPV.RowCount == 0) return;
                        frmViewReport frm = new frmViewReport();
                        System.Data.SqlClient.SqlConnection conn;
                        DataTable dt = new DataTable();
                        frm.rpt = new rptPhieuPhongVanUngVien();
                        try
                        {
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptPhongVanTuyenDung", conn);
                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            cmd.Parameters.Add("@ID_PV", SqlDbType.BigInt).Value = Convert.ToInt64(grvPV.GetFocusedRowCellValue("ID_PV"));
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            adp.Fill(ds);
                            dt = new DataTable();
                            dt = ds.Tables[0].Copy();
                            dt.TableName = "DA_TA";
                            frm.AddDataSource(dt);

                            dt = ds.Tables[1].Copy();
                            dt.TableName = "NOI_DUNG";
                            frm.AddDataSource(dt);
                            frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                        }
                        catch
                        {
                        }


                        frm.ShowDialog();
                        break;
                    }
                case "refresh":
                    {
                        DataSet set = RefreshData();
                        grdViTri.DataSource = set.Tables[1];
                        grdUVPV.DataSource = set.Tables[2];
                        grvViTri_FocusedRowChanged(null, null);
                        break;
                    }

                case "luu":
                    {
                        if (!dxValidationProvider1.Validate()) return;
                        grvViTri.ValidateEditor();
                        if (grvViTri.HasColumnErrors) return;
                        if (!SaveData()) return;
                        LoadgrdPV(iID_PV);
                        cboTTLoc_EditValueChanged(null, null);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvUVPV);
                        enableButon(true);
                        break;
                    }
                case "khongluu":
                    {
                        Commons.Modules.ObjSystems.ClearValidationProvider(dxValidationProvider1);
                        Commons.Modules.ObjSystems.XoaTable("sBTChonUV" + Commons.Modules.iIDUser);
                        Commons.Modules.ObjSystems.XoaTable("sBTUV" + Commons.Modules.iIDUser);
                        BindingData(false);
                        enableButon(true);
                        Commons.Modules.ObjSystems.DeleteAddRow(grvUVPV);
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
        private bool SaveData()
        {
            try
            {
                int iKiem = Commons.Modules.ObjSystems.ConvertDatatable(grvUVPV).AsEnumerable().Count(x => string.IsNullOrEmpty(x["KET_QUA_PV"].ToString()));

                if(iKiem > 0)
                {
                    if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgBanChuaNhapDuKetQuaPV"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return false;
                }
                else
                {
                    cboTinhTrang.EditValue = 2;
                }    
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTVT" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grvViTri), "");
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "sBTUVPV" + Commons.Modules.iIDUser, Commons.Modules.ObjSystems.ConvertDatatable(grdUVPV), "");
                iID_PV = Convert.ToInt64(SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, "spSavePhongVan",
                    iID_PV,
                    txtSO_PV.EditValue,
                    datNgayPV.DateTime,
                    cboSoKeHoach.EditValue,
                    txtBuocPV.EditValue,
                    cboNguoiPV1.EditValue,
                    cboNguoiPV2.EditValue,
                    txtNDPV.EditValue,
                    timBacDau.Time.TimeOfDay,
                    timKetThuc.Time.TimeOfDay,
                    cboTinhTrang.EditValue,
                    txtThongTin.EditValue,
                    chkKieuPV.Checked,
                    "sBTVT" + Commons.Modules.iIDUser,
                    "sBTUVPV" + Commons.Modules.iIDUser));
                if (iID_PV != -1)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #region function 
        private void enableButon(bool visible)
        {
            btnALL.Buttons[0].Properties.Visible = visible;
            btnALL.Buttons[1].Properties.Visible = visible;
            btnALL.Buttons[2].Properties.Visible = visible;
            btnALL.Buttons[3].Properties.Visible = visible;
            btnALL.Buttons[4].Properties.Visible = visible;
            btnALL.Buttons[5].Properties.Visible = visible;
            btnALL.Buttons[6].Properties.Visible = !visible;
            btnALL.Buttons[7].Properties.Visible = !visible;
            btnALL.Buttons[8].Properties.Visible = !visible;
            btnALL.Buttons[9].Properties.Visible = visible;

            txtSO_PV.Properties.ReadOnly = visible;
            cboSoKeHoach.Properties.ReadOnly = visible;
            datNgayPV.Properties.ReadOnly = visible;
            cboNguoiPV1.Properties.ReadOnly = visible;
            cboNguoiPV2.Properties.ReadOnly = visible;
            timBacDau.Properties.ReadOnly = visible;
            timKetThuc.Properties.ReadOnly = visible;
            //chkKieuPV.Properties.ReadOnly = visible;
            txtThongTin.Properties.ReadOnly = visible;
            txtNDPV.Properties.ReadOnly = visible;
            cboTinhTrang.Properties.ReadOnly = visible;
            //txtBuocPV.Properties.ReadOnly = visible;

            cboTTLoc.Properties.ReadOnly = !visible;
            groDSPYC.Enabled = visible;
            datTuNgay.Properties.ReadOnly = !visible;
        }
        private void LoadCbo()
        {
            try
            {
                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboTTLoc, Commons.Modules.ObjSystems.DataTinhTrangPV(false), "ID_TT_KHPV", "TEN_TT_KHPV", "TEN_TT_KHPV");

                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNguoiPV1, Commons.Modules.ObjSystems.DataCongNhan(false), "ID_CN", "TEN_CN", "TEN_CN", true, true);
                Commons.Modules.ObjSystems.MLoadLookUpEdit(cboTinhTrang, Commons.Modules.ObjSystems.DataTinhTrangPV(false), "ID_TT_KHPV", "TEN_TT_KHPV", "TEN_TT_KHPV");
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboNguoiPV2, Commons.Modules.ObjSystems.DataCongNhan(false), "ID_CN", "TEN_CN", "TEN_CN", true, true);
                //ID_KHPV,SO_KHPV
                Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSoKeHoach, Commons.Modules.ObjSystems.DataKeHoachPV(false, -1), "ID_KHPV", "SO_KHPV", "SO_KHPV", true, true);
            }
            catch
            {
            }
        }
        private void BindingData(bool them)
        {
            Commons.Modules.sLoad = "0Load";
            if (them == true)
            {
                txtSO_PV.EditValue = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_PV(" + datNgayPV.DateTime.ToString("MM/dd/yyyy") + ")").ToString();
                cboSoKeHoach.EditValue = -1;
                datNgayPV.EditValue = DateTime.Now;
                cboTinhTrang.EditValue = 1;
                cboNguoiPV1.EditValue = -1;
                cboNguoiPV2.EditValue = -1;
                timBacDau.EditValue = "08:00:00";
                timKetThuc.EditValue = "10:00:00";
                txtThongTin.EditValue = "";
                txtNDPV.EditValue = "";
                iID_PV = -1;
            }
            else // Load data vao text
            {
                try
                {
                    iID_PV = Convert.ToInt64(grvPV.GetFocusedRowCellValue("ID_PV").ToString());
                    txtSO_PV.EditValue = grvPV.GetFocusedRowCellValue("MA_SO").ToString();
                    cboSoKeHoach.EditValue = Convert.ToInt64(grvPV.GetFocusedRowCellValue("ID_KHPV"));
                    chkKieuPV.EditValue = Convert.ToBoolean(grvPV.GetFocusedRowCellValue("PV_ON_OF_LINE"));
                    try
                    {
                        datNgayPV.EditValue = Convert.ToDateTime(grvPV.GetFocusedRowCellValue("NGAY_PV"));
                    }
                    catch
                    {
                        datNgayPV.EditValue = "";
                    }
                    cboTinhTrang.EditValue = Convert.ToInt32(grvPV.GetFocusedRowCellValue("TINH_TRANG"));
                    cboNguoiPV1.EditValue = Convert.ToInt64(grvPV.GetFocusedRowCellValue("NGUOI_PV_1"));
                    cboNguoiPV2.EditValue = Convert.ToInt64(grvPV.GetFocusedRowCellValue("NGUOI_PV_2"));
                    timBacDau.EditValue = grvPV.GetFocusedRowCellValue("TG_BD").ToString();
                    timKetThuc.EditValue = grvPV.GetFocusedRowCellValue("TG_KT").ToString();
                    txtThongTin.EditValue = grvPV.GetFocusedRowCellValue("THONG_TIN").ToString();
                    txtNDPV.EditValue = grvPV.GetFocusedRowCellValue("NOI_DUNG_PV").ToString();
                    txtBuocPV.EditValue = grvPV.GetFocusedRowCellValue("BUOC_PV").ToString();

                }
                catch
                {
                    txtSO_PV.EditValue = "";
                    cboSoKeHoach.EditValue = -1;
                    cboTinhTrang.EditValue = 1;
                    cboNguoiPV1.EditValue = -1;
                    cboNguoiPV2.EditValue = -1;
                    timBacDau.EditValue = "08:00:00";
                    timKetThuc.EditValue = "10:00:00";
                    txtThongTin.EditValue = "";
                    txtNDPV.EditValue = "";
                    iID_PV = -1;
                }
            }

            LoadgrdViTri();
            LoadgrdUVPV(false);
            Commons.Modules.sLoad = "";
            grvViTri_FocusedRowChanged(null, null);
        }
        #endregion
        private void datNgayLap_EditValueChanged(object sender, EventArgs e)
        {
            if (iID_PV == -1 && btnALL.Buttons[0].Properties.Visible == true)
            {
                txtSO_PV.EditValue = SqlHelper.ExecuteScalar(Commons.IConnections.CNStr, CommandType.Text, "SELECT dbo.AUTO_CREATE_SO_PV(" + datNgayPV.DateTime.ToString("MM/dd/yyyy") + ")").ToString();
            }
        }


        private void grvKHPV_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load" || btnALL.Buttons[0].Properties.Visible == false) return;
            BindingData(false);
        }



        private void XoaPhongVan()
        {
            if (Convert.ToInt32(cboTinhTrang.EditValue) != 1)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDuLieuDaPhatSinhKhongXoa"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDeleteKeHoachPhongVan"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //xóa
            try
            {
                //kiểm tra ID_KHPV có trong phỏng vấn không
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE FROM dbo.UNG_VIEN_PHONG_VAN WHERE ID_PV = " + iID_PV + "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DELETE FROM dbo.PVUV_VTTD WHERE ID_PV = " + iID_PV + "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "DBCC CHECKIDENT (PHONG_VAN,RESEED,0)DBCC CHECKIDENT (PHONG_VAN,RESEED) DELETE FROM dbo.PHONG_VAN WHERE ID_PV = " + iID_PV + "");
                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, " UPDATE A SET A.TINH_TRANG = 0 FROM dbo.KE_HOACH_PHONG_VAN A WHERE NOT EXISTS(SELECT * FROM dbo.PHONG_VAN B WHERE B.ID_KHPV = A.ID_KHPV) ");
                grvPV.DeleteSelectedRows();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", "msgDelDangSuDung") + "\n" + ex.Message.ToString(), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void grdPYC_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                XoaPhongVan();
            }
        }

        private void grdViTri_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (btnALL.Buttons[0].Properties.Visible == false && e.KeyData == Keys.Delete)
            {
                grvViTri.DeleteSelectedRows();
            }
        }

        private void grdThayThe_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (btnALL.Buttons[0].Properties.Visible == false && e.KeyData == Keys.Delete)
            {
                grvUVPV.DeleteSelectedRows();
            }
        }

        private void datTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            LoadgrdPV(iID_PV);
            cboTTLoc_EditValueChanged(null, null);
        }
        private void searchControl1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            grvKHPV_FocusedRowChanged(null, null);
        }

        private void grvViTri_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            try
            {
                Commons.Modules.ObjSystems.RowFilter(grdUVPV, grvUVPV.Columns["ID_YCTD"], grvUVPV.Columns["ID_VTTD"], grvViTri.GetFocusedRowCellValue("ID_YCTD").ToString(), grvViTri.GetFocusedRowCellValue("ID_VTTD").ToString());
            }
            catch
            {
                Commons.Modules.ObjSystems.RowFilter(grdUVPV, grvUVPV.Columns["ID_YCTD"], grvUVPV.Columns["ID_VTTD"], "-1", "-1");
            }
            //if (btnALL.Buttons[0].Properties.Visible == false)
            //{
            //    if (grvUVPV.RowCount > 0)
            //    {
            //        grvViTri.OptionsBehavior.Editable = false;
            //    }
            //    else
            //    {
            //        grvViTri.OptionsBehavior.Editable = true;
            //    }
            //}
        }

        private void cboSoKeHoach_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (btnALL.Buttons[0].Properties.Visible == false && iID_PV == -1 && Commons.Modules.sLoad == "" && cboSoKeHoach.EditValue.ToString() != "-1")
                {
                    DataSet set = RefreshData();
                    DataRow row = set.Tables[0].Rows[0];
                    cboNguoiPV1.EditValue = row["NGUOI_PV_1"];
                    cboNguoiPV2.EditValue = row["NGUOI_PV_2"];
                    chkKieuPV.Checked = Convert.ToBoolean(row["PV_ON_OF_LINE"]);
                    txtBuocPV.EditValue = row["BUOC_PV"];
                    grdViTri.DataSource = set.Tables[1];
                    grdUVPV.DataSource = set.Tables[2];
                    grvViTri_FocusedRowChanged(null, null);
                }
            }
            catch
            {
            }
        }

        private DataSet RefreshData()
        {
            try
            {
                DataSet set = new DataSet();
                set = SqlHelper.ExecuteDataset(Commons.IConnections.CNStr, "spGetRefeshPV", iID_PV, cboSoKeHoach.EditValue);
                return set;
            }
            catch
            {
                return null;
            }
        }
        private void cboTTLoc_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.ObjSystems.RowFilter(grdPV, grvPV.Columns["TINH_TRANG"], (cboTTLoc.EditValue).ToString());
                grvKHPV_FocusedRowChanged(null, null);
            }
            catch
            {
            }
        }

        private void cboSoKeHoach_QueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (((DataTable)grdViTri.DataSource).Rows.Count > 0)
            //{
            //    cboSoKeHoach.Properties.ReadOnly = true;
            //}
            //else
            //{
            //    cboSoKeHoach.Properties.ReadOnly = false;
            //load lại kế hoạch chỉ lấy kế hoạch
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboSoKeHoach, Commons.Modules.ObjSystems.DataKeHoachPV(false, 1), "ID_KHPV", "SO_KHPV", "SO_KHPV", true, true);
            //}
        }

        private void cboTinhTrang_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (iID_PV == -1) return;
            if (XtraMessageBox.Show(Commons.Modules.ObjLanguages.GetLanguage("frmMessage", Convert.ToInt32(cboTinhTrang.EditValue) == 1 ? "msgBanCoMuonKetThucPhieu" : "msgBanCoMuonChuyenDangThucHien"), Commons.Modules.ObjLanguages.GetLanguage("msgThongBao", "msg_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, "UPDATE dbo.PHONG_VAN SET TINH_TRANG =" + (Convert.ToInt32(cboTinhTrang.EditValue) == 1 ? "2" : "1") + " WHERE ID_PV = " + iID_PV + "");
            cboTinhTrang.EditValue = Convert.ToInt32(cboTinhTrang.EditValue) == 2 ? 1 : 2;
            //update trạng thái vào đây
            datTuNgay_EditValueChanged(null, null) ;

        }

        private void grvUVPV_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.HitInfo.InDataRow)
                {
                    contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                }
                else
                {
                    contextMenuStrip1.Hide();
                }
            }
            catch
            {
            }
        }

        private void mnuLinkUngVien_Click(object sender, EventArgs e)
        {
            Commons.Modules.iUngVien = Convert.ToInt64(grvUVPV.GetFocusedRowCellValue("ID_UV"));
            frmUngVien frm = new frmUngVien();
            frm.ShowDialog();
        }
    }

}
