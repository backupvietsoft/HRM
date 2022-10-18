using Commons;
using DevExpress.Utils;
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
using System.Windows.Forms;
using Vs.Report;

namespace Vs.TimeAttendance
{
    public partial class ucVachTheLoi : DevExpress.XtraEditors.XtraUserControl
    {
        public static ucVachTheLoi _instance;
        public static ucVachTheLoi Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucVachTheLoi();
                return _instance;
            }
        }
        string sBT = "tabKeHoachDiCa" + Commons.Modules.ModuleName;
        public ucVachTheLoi()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, windowsUIButton);
            Commons.Modules.ObjSystems.ThayDoiNN(this, layoutControlGroup1);
        }

        private void ucVachTheLoi_Load(object sender, EventArgs e)
        {
            Commons.Modules.sLoad = "0Load";


            if (Commons.Modules.bolLinkCC)
            {
                datNgayChamCong.EditValue = Commons.Modules.dLinkCC;
            }
            else
            {
                datNgayChamCong.EditValue = DateTime.Now.Date;
            }





            //dinh dang ngay gio
            Commons.OSystems.SetDateEditFormat(datNgayDen);
            Commons.OSystems.SetDateEditFormat(datNgayVe);
            Commons.OSystems.SetDateEditFormat(datNgayChamCong);
            Commons.OSystems.SetTimeEditFormat(timDen);
            Commons.OSystems.SetTimeEditFormat(timVe);

            Commons.Modules.ObjSystems.LoadCboDonVi(cboDV);
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            Commons.Modules.ObjSystems.MLoadSearchLookUpEdit(cboID_XNG, Commons.Modules.ObjSystems.DataXacNhanGio(false), "ID_XNG", "TEN_XNG", "", true, true);
            cboID_XNG.EditValue = -99;
            LoadGrdCongNhan();
            Commons.Modules.sLoad = "";

            if (Modules.iPermission != 1)
            {
                windowsUIButton.Buttons[0].Properties.Visible = false;
                windowsUIButton.Buttons[1].Properties.Visible = false;
                windowsUIButton.Buttons[2].Properties.Visible = false;
                windowsUIButton.Buttons[6].Properties.Visible = false;
                windowsUIButton.Buttons[7].Properties.Visible = false;
            }
            else
            {
                if (Commons.Modules.bolLinkCC)
                {
                    Commons.Modules.ObjSystems.MLoadLookUpEdit(cboMSCN, Commons.Modules.ObjSystems.ConvertDatatable(grdCongNhan), "ID_CN", "MS_CN", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "MS_CN"));
                    enableButon(false);
                }
                else
                {
                    enableButon(true);
                }
            }
        }
        private void cboDV_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDV, cboXN);
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdCongNhan();
            Commons.Modules.sLoad = "";
        }
        private void cboXN_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDV, cboXN, cboTo);
            LoadGrdCongNhan();
            Commons.Modules.sLoad = "";
        }
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGrdCongNhan();
            Commons.Modules.sLoad = "";
        }
        private void windowsUIButton_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "sua":
                    {
                        if (grvCongNhan.RowCount == 0)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgBanChuaChonDuLieu);
                            return;
                        }
                        cboID_XNG.EditValue = -99;
                        Commons.Modules.ObjSystems.MLoadLookUpEdit(cboMSCN, Commons.Modules.ObjSystems.ConvertDatatable(grdCongNhan), "ID_CN", "MS_CN", Commons.Modules.ObjLanguages.GetLanguage(this.Name, "MS_CN"));
                        BingdingData();
                        enableButon(false);
                        break;
                    }
                case "xoa":
                    {
                        Int64 idcn = Convert.ToInt64(grvCongNhan.GetFocusedRowCellValue("ID_CN"));
                        if (grvCongNhan.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
                        if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
                        //xóa
                        try
                        {
                            string sSql = "DELETE dbo.DU_LIEU_QUET_THE WHERE ID_CN = " + idcn + " AND NGAY = '" +
                                Convert.ToDateTime(grvCongNhan.GetFocusedRowCellValue("NGAY_DEN")).ToString("yyyy/MM/dd") +
                                "' AND CONVERT(nvarchar(10),GIO_DEN,108) = '" +
                                Convert.ToDateTime(grvCongNhan.GetFocusedRowCellValue("GIO_DEN")).ToString("HH:mm:ss") + "'";
                            SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                            grvCongNhan.DeleteSelectedRows();
                        }
                        catch
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
                        }
                        break;
                    }
                case "luu":
                    {
                        try
                        {
                            Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, "tabTMPVachTheLoi" + Commons.Modules.UserName, Commons.Modules.ObjSystems.ConvertDatatable(grvCongNhan), "");
                            try { SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, "spSaveVachTheLoi", datNgayChamCong.DateTime.Date, "tabTMPVachTheLoi" + Commons.Modules.UserName); } catch (Exception EX) { }
                            Commons.Modules.ObjSystems.XoaTable("tabTMPVachTheLoi" + Commons.Modules.UserName);
                            enableButon(true);
                            LoadGrdCongNhan();
                        }
                        catch
                        {
                            Commons.Modules.ObjSystems.XoaTable("tabTMPVachTheLoi" + Commons.Modules.UserName);
                        }
                        break;
                    }
                case "khongluu":
                    {
                        enableButon(true);
                        LoadGrdCongNhan();
                        break;
                    }
                case "In":
                    {
                        frmViewReport frm = new frmViewReport();
                        DataTable dt;
                        System.Data.SqlClient.SqlConnection conn;
                        dt = new DataTable();
                        //string sTieuDe = "DANH SÁCH NHÂN VIÊN CHƯA ĐỦ DỮ LIỆU";
                        frm.rpt = new rptDSNVVachTheLoi(datNgayChamCong.DateTime, datNgayChamCong.DateTime);

                        try
                        {
                            conn = new System.Data.SqlClient.SqlConnection(Commons.IConnections.CNStr);
                            conn.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("rptDSNVVachTheLoi", conn);

                            cmd.Parameters.Add("@UName", SqlDbType.NVarChar, 50).Value = Commons.Modules.UserName;
                            cmd.Parameters.Add("@NNgu", SqlDbType.Int).Value = Commons.Modules.TypeLanguage;
                            //theo code cũ 
                            cmd.Parameters.Add("@Dvi", SqlDbType.Int).Value = cboMSCN.EditValue;
                            cmd.Parameters.Add("@XN", SqlDbType.Int).Value = datNgayDen.EditValue;
                            cmd.Parameters.Add("@TO", SqlDbType.Int).Value = txtCN.EditValue;
                            cmd.Parameters.Add("@NGAY", SqlDbType.DateTime).Value = Convert.ToDateTime(datNgayChamCong.EditValue).ToString("yyyy/MM/dd");
                            cmd.CommandType = CommandType.StoredProcedure;
                            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);

                            //DataSet ds = new DataSet();
                            dt = new DataTable();
                            adp.Fill(dt);

                            //dt = ds.Tables[0].Copy();
                            dt.TableName = "DA_TA";
                            frm.AddDataSource(dt);
                            frm.AddDataSource(Commons.Modules.ObjSystems.DataThongTinChung());
                        }
                        catch (Exception ex)
                        { }
                        frm.ShowDialog();

                        //======
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
            }
        }
        #region hàm xử lý dữ liệu
        private void LoadGrdCongNhan()
        {
            try
            {
                Commons.Modules.sLoad = "0Load";
                DataTable dt = new DataTable();
                dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetDuLieuQuetTheLoi", cboDV.EditValue, cboXN.EditValue, cboTo.EditValue, datNgayChamCong.DateTime, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                //dt.PrimaryKey = new DataColumn[] { dt.Columns["ID_CN"] };
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ReadOnly = false;
                }
                if (grdCongNhan.DataSource == null)
                {
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdCongNhan, grvCongNhan, dt, true, true, true, true, true, this.Name);
                    grvCongNhan.Columns["MS_CN"].OptionsColumn.AllowEdit = false;
                    grvCongNhan.Columns["HO_TEN"].OptionsColumn.AllowEdit = false;
                    grvCongNhan.Columns["NGAY_DEN"].OptionsColumn.AllowEdit = false;
                    grvCongNhan.Columns["NGAY_VE"].OptionsColumn.AllowEdit = false;
                    grvCongNhan.Columns["MS_THE_CC"].OptionsColumn.AllowEdit = false;
                    grvCongNhan.Columns["NGAY"].OptionsColumn.AllowEdit = false;

                    grvCongNhan.Columns["ID_CN"].Visible = false;
                    grvCongNhan.Columns["CHINH_SUA"].Visible = false;
                    grvCongNhan.Columns["GIO_DEN_LUU"].Visible = false;
                    grvCongNhan.Columns["GIO_VE_LUU"].Visible = false;
                }
                else
                {
                    grdCongNhan.DataSource = dt;
                }

                DataTable dID_NHOM = new DataTable();
                dID_NHOM.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetNhomCC", DateTime.Now, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.AddCombXtra("ID_NHOM", "TEN_NHOM", grvCongNhan, dID_NHOM, false, "ID_NHOM", "NHOM_CHAM_CONG");


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
                grvCongNhan.Columns["CA"].ColumnEdit = cboCa;
                cboCa.BeforePopup += cboCa_BeforePopup;
                cboCa.EditValueChanged += CboCa_EditValueChanged;

                RepositoryItemTimeEdit repositoryItemTimeEdit1 = new RepositoryItemTimeEdit();
                repositoryItemTimeEdit1.TimeEditStyle = TimeEditStyle.TouchUI;
                repositoryItemTimeEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
                repositoryItemTimeEdit1.Mask.EditMask = "HH:mm:ss";

                repositoryItemTimeEdit1.NullText = "00:00:00";
                repositoryItemTimeEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                repositoryItemTimeEdit1.DisplayFormat.FormatString = "HH:mm:ss";
                repositoryItemTimeEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                repositoryItemTimeEdit1.EditFormat.FormatString = "HH:mm:ss";
                repositoryItemTimeEdit1.Mask.UseMaskAsDisplayFormat = true;


                grvCongNhan.Columns["GIO_DEN"].ColumnEdit = repositoryItemTimeEdit1;
                grvCongNhan.Columns["GIO_VE"].ColumnEdit = repositoryItemTimeEdit1;

                DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit cbo = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                Commons.Modules.ObjSystems.AddCombSearchLookUpEdit(cbo, "ID_XNG", "TEN_XNG", "ID_XNG", grvCongNhan, Commons.Modules.ObjSystems.DataXacNhanGio(false), this.Name);
                Commons.Modules.sLoad = "";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
        }
        private void cboCa_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCaLV = new DataTable();
                dtCaLV.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCaLV", DateTime.Now, grvCongNhan.GetFocusedRowCellValue("ID_NHOM"), Commons.Modules.UserName, Commons.Modules.TypeLanguage));

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
        private void CboCa_EditValueChanged(object sender, EventArgs e)
        {
            try
            {


                LookUpEdit lookUp = sender as LookUpEdit;

                DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;

                grvCongNhan.SetFocusedRowCellValue("CA", (dataRow.Row[1]));
            }
            catch { }
        }
        private void cboID_XNG_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            DataRowView dataRow = lookUp.GetSelectedDataRow() as DataRowView;
            grvCongNhan.SetFocusedRowCellValue("ID_XNG", Convert.ToInt64((dataRow.Row[0])));
        }
        private void cboID_XNG_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                LookUpEdit lookUp = sender as LookUpEdit;
                lookUp.Properties.DataSource = Commons.Modules.ObjSystems.DataXacNhanGio(false);
            }
            catch { }
        }
        private bool Savedata()
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
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
            navigationFrame1.SelectedPageIndex = visible == true ? 0 : 1;

            grvCongNhan.OptionsBehavior.Editable = !visible;

        }
        private void BingdingData()
        {
            cboMSCN.EditValue = Convert.ToInt64(grvCongNhan.GetFocusedRowCellValue("ID_CN"));
            txtCN.EditValue = grvCongNhan.GetFocusedRowCellValue("HO_TEN");
            datNgayDen.DateTime = Convert.ToDateTime(grvCongNhan.GetFocusedRowCellValue("NGAY_DEN"));
            datNgayVe.DateTime = Convert.ToDateTime(grvCongNhan.GetFocusedRowCellValue("NGAY_VE"));
            timDen.EditValue = grvCongNhan.GetFocusedRowCellValue("GIO_DEN");
            timVe.EditValue = grvCongNhan.GetFocusedRowCellValue("GIO_VE");
        }

        #endregion

        private void grvCongNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (windowsUIButton.Buttons[5].Properties.Visible == true) return;
            BingdingData();
        }
        private void cboMSCN_EditValueChanged(object sender, EventArgs e)
        {
            //DataTable dt = Commons.Modules.ObjSystems.ConvertDatatable(grdCongNhan);
            //int index = dt.Rows.IndexOf(dt.Rows.Find(cboMSCN.EditValue));
            //grvCongNhan.FocusedRowHandle = index;
        }
        //cập nhật all
        private void btnGhiAll_Click(object sender, EventArgs e)
        {
            try
            {

                DateTime NgayDen = Convert.ToDateTime(datNgayDen.DateTime.Date.ToString().Substring(0, 10) + " " + timDen.Text);
                DateTime NgayVe = Convert.ToDateTime(datNgayVe.DateTime.Date.ToString().Substring(0, 10) + " " + timVe.Text);
                if (NgayVe <= NgayDen)
                {
                    Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgNgayKhongHopLe);
                    return;
                }
                for (int i = 0; i <= grvCongNhan.RowCount; i++)
                {
                    if (Convert.ToBoolean(grvCongNhan.GetRowCellValue(i, "CHINH_SUA")) == false)
                    {
                        grvCongNhan.SetRowCellValue(i, "NGAY_DEN", NgayDen.Date);
                        grvCongNhan.SetRowCellValue(i, "NGAY_VE", NgayVe.Date);
                        grvCongNhan.SetRowCellValue(i, "GIO_DEN", NgayDen.TimeOfDay.ToString());
                        grvCongNhan.SetRowCellValue(i, "GIO_VE", NgayVe.TimeOfDay.ToString());
                        grvCongNhan.SetRowCellValue(i, "GIO_DEN_LUU", NgayDen);
                        grvCongNhan.SetRowCellValue(i, "GIO_VE_LUU", NgayVe);
                        grvCongNhan.SetRowCellValue(i, "CHINH_SUA", true);
                        grvCongNhan.SetRowCellValue(i, "ID_XNG", cboID_XNG.EditValue);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void btnGhiMot_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime NgayDen = Convert.ToDateTime(datNgayDen.DateTime.Date.ToString().Substring(0, 10) + " " + timDen.Text);
                DateTime NgayVe = Convert.ToDateTime(datNgayVe.DateTime.Date.ToString().Substring(0, 10) + " " + timVe.Text);
                grvCongNhan.SetFocusedRowCellValue("NGAY_DEN", NgayDen.Date);
                grvCongNhan.SetFocusedRowCellValue("NGAY_VE", NgayVe.Date);
                grvCongNhan.SetFocusedRowCellValue("GIO_DEN", NgayDen.TimeOfDay.ToString());
                grvCongNhan.SetFocusedRowCellValue("GIO_VE", NgayVe.TimeOfDay.ToString());
                grvCongNhan.SetFocusedRowCellValue("GIO_DEN_LUU", NgayDen);
                grvCongNhan.SetFocusedRowCellValue("GIO_VE_LUU", NgayVe);
                grvCongNhan.SetFocusedRowCellValue("CHINH_SUA", true);
                grvCongNhan.SetFocusedRowCellValue("ID_XNG", cboID_XNG.EditValue);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
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

        private void grvCongNhan_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                DevExpress.XtraGrid.Columns.GridColumn ngayBD = view.Columns["GIO_DEN"];
                DevExpress.XtraGrid.Columns.GridColumn ngayKT = view.Columns["GIO_VE"];
                if (view.FocusedColumn == view.Columns["GIO_DEN"])
                {
                    DateTime? fromDate = Convert.ToDateTime("1900/01/01 " + Convert.ToDateTime(e.Value).TimeOfDay) as DateTime?;
                    DateTime? toDate = Convert.ToDateTime("1900/01/01 " + Convert.ToDateTime(view.GetRowCellValue(view.FocusedRowHandle, view.Columns["GIO_VE"])).TimeOfDay) as DateTime?;
                    if (fromDate > toDate)
                    {
                        e.Valid = false;
                        view.SetColumnError(ngayBD, "Giờ đến phải nhỏ hơn giờ về"); return;
                    }
                }
                if (view.FocusedColumn == view.Columns["GIO_VE"])
                {
                    DateTime? fromDate = Convert.ToDateTime("1900/01/01 " + Convert.ToDateTime(view.GetRowCellValue(view.FocusedRowHandle, view.Columns["GIO_DEN"])).TimeOfDay) as DateTime?;
                    DateTime? toDate = Convert.ToDateTime("1900/01/01 " + Convert.ToDateTime(e.Value).TimeOfDay) as DateTime?;
                    if (fromDate > toDate)
                    {
                        e.Valid = false;
                        view.SetColumnError(ngayKT, "Giờ về phải lớn hơn giờ đến"); return;
                    }
                }
                view.ClearColumnErrors();
            }
            catch (Exception ex) { }
        }

        private void grvCongNhan_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void grvCongNhan_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
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
        //Thong tin nhân sự
        public DXMenuItem MCreateMenuCapNhatAll(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            string sStr = Commons.Modules.ObjLanguages.GetLanguage(Commons.Modules.ModuleName, this.Name, "lblCapNhatAll", Commons.Modules.TypeLanguage);
            DXMenuItem menuThongTinNS = new DXMenuItem(sStr, new EventHandler(CapNhatAll));
            menuThongTinNS.Tag = new RowInfo(view, rowHandle);
            return menuThongTinNS;
        }
        public void CapNhatAll(object sender, EventArgs e)
        {
            try
            {
                string sCotCN = grvCongNhan.FocusedColumn.FieldName.ToString();
                try
                {
                    if (grvCongNhan.GetFocusedRowCellValue(grvCongNhan.FocusedColumn.FieldName).ToString() == "") return;
                    string sBTCongNhan = "sBTCongNhan" + Commons.Modules.iIDUser;
                    Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, sBTCongNhan, Commons.Modules.ObjSystems.ConvertDatatable(grvCongNhan), "");

                    DataTable dt = new DataTable();
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spUpdateChuotPhai_TiepNhan", sBTCongNhan, sCotCN, sCotCN.Substring(0, 4) == "NGAY" ? Convert.ToDateTime(grvCongNhan.GetFocusedRowCellValue(grvCongNhan.FocusedColumn.FieldName)).ToString("MM/dd/yyyy") : grvCongNhan.GetFocusedRowCellValue(grvCongNhan.FocusedColumn.FieldName)));
                    grdCongNhan.DataSource = dt;
                    Commons.Modules.ObjSystems.XoaTable(sCotCN);
                }
                catch (Exception ex)
                {
                    Commons.Modules.ObjSystems.XoaTable(sCotCN);
                }
            }
            catch (Exception ex) { }
        }
        private void grvDSUngVien_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    int irow = e.HitInfo.RowHandle;
                    e.Menu.Items.Clear();
                    if (windowsUIButton.Buttons[0].Properties.Visible) return;
                    if (grvCongNhan.FocusedColumn.FieldName.ToString() != "GIO_DEN" && grvCongNhan.FocusedColumn.FieldName.ToString() != "GIO_VE" && grvCongNhan.FocusedColumn.FieldName.ToString() != "ID_XNG") return;
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

        private void grdCongNhan_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Int64 idcn = Convert.ToInt64(grvCongNhan.GetFocusedRowCellValue("ID_CN"));
                if (grvCongNhan.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }
                if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
                //xóa
                try
                {
                    string sSql = "DELETE dbo.DU_LIEU_QUET_THE WHERE ID_CN = " + idcn + " AND NGAY = '" +
                        Convert.ToDateTime(grvCongNhan.GetFocusedRowCellValue("NGAY_DEN")).ToString("yyyy/MM/dd") +
                        "' AND CONVERT(nvarchar(10),GIO_DEN,108) = '" +
                        Convert.ToDateTime(grvCongNhan.GetFocusedRowCellValue("GIO_DEN")).ToString("HH:mm:ss") + "'";
                    SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                    grvCongNhan.DeleteSelectedRows();
                }
                catch
                {
                    Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
                }
            }
        }
    }
}
