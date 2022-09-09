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
using Vs.Report;

namespace Vs.Payroll
{
    public partial class ucCachTinhLuong : DevExpress.XtraEditors.XtraUserControl
    {
        private static bool isAdd = false;
        public static ucCachTinhLuong _instance;
        public static ucCachTinhLuong Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucCachTinhLuong();
                return _instance;
            }
        }

        RepositoryItemTimeEdit repositoryItemTimeEdit1;
        DataTable CachTinhLuong = new DataTable();
        private bool thangtruoc = false;

        /// <summary>
        /// 
        /// </summary>
        public ucCachTinhLuong()
        {
            InitializeComponent();
            Commons.Modules.ObjSystems.ThayDoiNN(this, new List<LayoutControlGroup>() { Root }, btnALL);
            repositoryItemTimeEdit1 = new RepositoryItemTimeEdit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucCachTinhLuong_Load(object sender, EventArgs e)
        {
            try
            {
                Commons.Modules.sLoad = "0Load";

                repositoryItemTimeEdit1.TimeEditStyle = TimeEditStyle.TouchUI;
                repositoryItemTimeEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
                repositoryItemTimeEdit1.Mask.EditMask = "HH:mm";

                repositoryItemTimeEdit1.NullText = "00:00";
                repositoryItemTimeEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                repositoryItemTimeEdit1.DisplayFormat.FormatString = "HH:mm";
                repositoryItemTimeEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                repositoryItemTimeEdit1.EditFormat.FormatString = "HH:mm";

                EnableButon();
                LoadNgay();
                Commons.Modules.ObjSystems.LoadCboDonVi(cboDonVi);
                Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
                Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);

                LoadGridCongNhan();
                LoadGrdCachTinhLuong();

                DataTable dataCV = new DataTable();
                RepositoryItemLookUpEdit cboCV = new RepositoryItemLookUpEdit();

                cboCV.NullText = "";
                cboCV.ValueMember = "ID_CV";
                cboCV.DisplayMember = "TEN_CV";
                cboCV.DataSource = Commons.Modules.ObjSystems.DataChucVu(false,-1);
                cboCV.Columns.Clear();

                cboCV.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_CV"));
                cboCV.Columns["TEN_CV"].Caption = Commons.Modules.ObjLanguages.GetLanguage(this.Name, "TEN_CV");

                cboCV.AppearanceDropDownHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                cboCV.AppearanceDropDownHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                grvCachTinhLuong.Columns["ID_CV"].ColumnEdit = cboCV;

                cboCV.BeforePopup += cboCa_BeforePopup;
                cboCV.EditValueChanged += CboCa_EditValueChanged;

                Commons.Modules.sLoad = "";
            }
            catch { }
        }

        private void CboCa_EditValueChanged(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboCa_BeforePopup(object sender, EventArgs e)
        {
            try
            {
                Int64 id_cv = Convert.ToInt64(grvCachTinhLuong.GetFocusedRowCellValue("ID_CV"));
                if (sender is LookUpEdit cbo)
                {
                    try
                    {
                        DataTable DataCombo = (DataTable)cbo.Properties.DataSource;
                        DataTable DataLuoi = Commons.Modules.ObjSystems.ConvertDatatable(grdCachTinhLuong);
                        var DataNewCombo = DataCombo.AsEnumerable().Where(r => !DataLuoi.AsEnumerable()
                        .Any(r2 => r["ID_CV"].ToString().Trim() == r2["ID_CV"].ToString().Trim())).CopyToDataTable();
                        cbo.Properties.DataSource = null;
                        cbo.Properties.DataSource = DataNewCombo;
                    }
                    catch
                    {
                        cbo.Properties.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// load Grid
        /// </summary>
        private void LoadGrdCachTinhLuong()
        {
            decimal idCongNhan = -1;
            DataTable dt = new DataTable();

            try
            {
                if (grvCongNhan.FocusedRowHandle >= 0)
                {
                    decimal.TryParse(grvCongNhan.GetFocusedRowCellValue("ID_CN").ToString(), out idCongNhan);
                }
                DataTable dtLuoi = new DataTable();
                if (thangtruoc) {
                    
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListDLTT_CACH_TINH_LUONG_CN", idCongNhan, Convert.ToDateTime(cboNgay.EditValue).AddMonths(-1),
                                                                    Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                   
                } else
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCACH_TINH_LUONG_CN", idCongNhan, Convert.ToDateTime(cboNgay.EditValue),
                                                                    Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                }


                //dt.Columns["COM_CA"].ReadOnly = false;

                if (isAdd)
                    if (grdCachTinhLuong.DataSource == null) dtLuoi = dt; else dtLuoi = (DataTable)grdCachTinhLuong.DataSource;
                else
                    dtLuoi = dt;


                //, T2.ID_CTL,T1.ID_CN
                if (isAdd)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        try
                        {

                           String STR = "";
                            STR = " ID_CV = " + (String.IsNullOrEmpty(dr["ID_CV"].ToString()) ? "" : dr["ID_CV"].ToString()) +
                                " AND ID_CTL = " + (String.IsNullOrEmpty(dr["ID_CTL"].ToString()) ? "" : dr["ID_CTL"].ToString()) +
                                " AND ID_CN = " + (String.IsNullOrEmpty(dr["ID_CN"].ToString()) ? "" : dr["ID_CN"].ToString());

                            String sID_CV, sID_CTL, sID_CN;
                            string sDK = " 1 = 1 ";
                            sID_CV = ""; sID_CTL = ""; sID_CN = "";
                            try { sID_CV = dr["ID_CV"].ToString(); } catch { }
                            try { sID_CTL = dr["ID_CTL"].ToString(); } catch { }
                            try { sID_CN = dr["ID_CN"].ToString(); } catch { }

                            if (sID_CV != "") sDK = sDK + " AND ID_CV = '" + sID_CV + "' ";
                            if (sID_CTL != "") sDK = sDK + " AND ID_CTL = '" + sID_CTL + "' ";
                            if (sID_CN != "") sDK = sDK + " AND ID_CN = '" + sID_CN + "' ";
                            dtLuoi.DefaultView.RowFilter = sDK;
                        }
                        catch (Exception ex)
                        { }
                        if (dtLuoi.DefaultView.ToTable().Rows.Count == 0)
                        {

                            DataRow newRow = dtLuoi.NewRow();
                            newRow.SetField("ID_CN", dr["ID_CN"]);
                            newRow.SetField("ID_CV", dr["ID_CV"]);
                            newRow.SetField("ID_CTL", dr["ID_CTL"]);
                            newRow.SetField("THANG", dt.Rows[0]["THANG"]);
                            dtLuoi.Rows.Add(newRow);
                            dtLuoi.AcceptChanges();
                        };
                        
                    }
                    dtLuoi.DefaultView.RowFilter = "";
                }
                
                dtLuoi.Columns["ID_CV"].ReadOnly = false;
                dtLuoi.Columns["ID_CTL"].ReadOnly = false;
                dtLuoi.Columns["ID_CN"].ReadOnly = false;
                dtLuoi.Columns["THANG"].ReadOnly = false;
                if (!isAdd)
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdCachTinhLuong, grvCachTinhLuong, dtLuoi, false, false, false, true, true, this.Name);
                if (isAdd && thangtruoc)
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdCachTinhLuong, grvCachTinhLuong, dtLuoi, true, false, false, true, true, this.Name);

                if (isAdd)
                {
                    DataTable dtTmp = new DataTable();
                    dtTmp = (DataTable)grdCachTinhLuong.DataSource;
                    dtTmp.DefaultView.RowFilter = " ID_CN = " + idCongNhan;
                }
                
                DataTable dID_NHOM = new DataTable();
                dID_NHOM.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCTL",  Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                Commons.Modules.ObjSystems.AddCombXtra("ID_CTL", "TEN", grvCachTinhLuong , dID_NHOM, false, "ID_CTL", "CACH_TINH_LUONG");
                grvCachTinhLuong.Columns["THANG"].Visible = false;
                grvCachTinhLuong.Columns["ID_CN"].Visible = false;
                grvCachTinhLuong.Columns["ID_CTL"].Width = 250;
                grvCachTinhLuong.Columns["ID_CV"].Width = 250;
                //FormatGrvLamThem();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Get List CN
        /// </summary>
        private void LoadGridCongNhan()
        {
            DataTable dt = new DataTable();
            try
            {
                if (isAdd)
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetEditListCN_CACH_TINH_LUONG", Convert.ToDateTime(cboNgay.EditValue),
                                      cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdCongNhan, grvCongNhan, dt, false, false, false, true, true, this.Name);
                }
                else
                {
                    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCN_CACH_TINH_LUONG", Convert.ToDateTime(cboNgay.EditValue), 
                            cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                    Commons.Modules.ObjSystems.MLoadXtraGrid(grdCongNhan, grvCongNhan, dt, false, false, false, true, true, this.Name);
                    //do nothing;
                }
                //dt.Columns["CHON"].ReadOnly = false;

                FormatGridCongNhan();
                LoadGrdCachTinhLuong();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void FormatGridCongNhan()
        {
            grvCongNhan.Columns["ID_CN"].Visible = false;
            grvCongNhan.Columns["ID_CV"].Visible = false;
        }

        #region Combobox Changed
        /// <summary>
        /// cbo Don vi Change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboDonVi_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboXiNghiep(cboDonVi, cboXiNghiep);
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadGridCongNhan();
            LoadGrdCachTinhLuong();
            Commons.Modules.sLoad = "";
        }

        /// <summary>
        /// cbo Xi nghiep Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboXiNghiep_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            Commons.Modules.ObjSystems.LoadCboTo(cboDonVi, cboXiNghiep, cboTo);
            LoadGridCongNhan();
            LoadGrdCachTinhLuong();
            Commons.Modules.sLoad = "";
        }

        /// <summary>
        /// cbo To Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboTo_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGridCongNhan();
            LoadGrdCachTinhLuong();
            Commons.Modules.sLoad = "";
        }

        /// <summary>
        /// combo date change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboNgay_EditValueChanged(object sender, EventArgs e)
        {
            if (Commons.Modules.sLoad == "0Load") return;
            Commons.Modules.sLoad = "0Load";
            LoadGridCongNhan();
            LoadGrdCachTinhLuong();
            Commons.Modules.sLoad = "";
        }
        #endregion

        /// <summary>
        /// Format Grid
        /// </summary>
        //private void FormatGrvLamThem()
        //{
        //    //grvLamThem.Columns["ID_NHOM"].Visible = false;
        //    grvCachTinhLuong.Columns["GIO_BD"].ColumnEdit = this.repositoryItemTimeEdit1;
        //    grvCachTinhLuong.Columns["GIO_KT"].ColumnEdit = this.repositoryItemTimeEdit1;
        //    grvCachTinhLuong.Columns["GIO_BD"].OptionsColumn.ReadOnly = true;
        //    grvCachTinhLuong.Columns["PHUT_BD"].Visible = false;
        //    grvCachTinhLuong.Columns["PHUT_KT"].Visible = false;
        //}

        /// <summary>
        /// windows button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            XtraUserControl ctl = new XtraUserControl();
            switch (btn.Tag.ToString())
            {
                case "themsua":
                    {
                        isAdd = true;
                        EnableButon();
                        LoadGridCongNhan();

                        //decimal idCongNhan = -1;
                        //DataTable dt = new DataTable();

                        //    if (grvCongNhan.FocusedRowHandle >= 0)
                        //    {
                        //        decimal.TryParse(grvCongNhan.GetFocusedRowCellValue("ID_CN").ToString(), out idCongNhan);

                        //    }
                        //    dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetListCACH_TINH_LUONG_CN", idCongNhan, Convert.ToDateTime(cboNgay.EditValue),
                        //                                    Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                        //    //dt.Columns["COM_CA"].ReadOnly = false;
                        //    Commons.Modules.ObjSystems.MLoadXtraGrid(grdCachTinhLuong, grvCachTinhLuong, dt, false, false, false, false, true, this.Name);

                        //    DataTable dID_NHOM = new DataTable();
                        //    dID_NHOM.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "spGetCTL", Commons.Modules.UserName, Commons.Modules.TypeLanguage));
                        //    Commons.Modules.ObjSystems.AddCombXtra("ID_CTL", "TEN", grvCachTinhLuong, dID_NHOM, false);
                        //dt.Columns["ID_CV"].ReadOnly = false;
                        //grvCachTinhLuong.OptionsBehavior.Editable = true;
                        


                        Commons.Modules.ObjSystems.AddnewRow(grvCachTinhLuong, true);
                        break;
                    }
                case "xoa":
                    {
                        XoaDangKiGioLamThem();
                        LoadGridCongNhan();
                        LoadGrdCachTinhLuong();
                        break;
                    }
                case "ghi":
                    {

                        Validate();
                        if (grvCongNhan.HasColumnErrors) return;
                        if (Savedata() == false)
                        {
                            Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgDuLieuDangSuDung);
                        }
                        Commons.Modules.ObjSystems.DeleteAddRow(grvCachTinhLuong);
                        isAdd = false;
                        EnableButon();
                        LoadGridCongNhan();
                        thangtruoc = false;
                        LoadGrdCachTinhLuong();
                        break;
                    }
                case "khongghi":
                    {
                        Commons.Modules.ObjSystems.DeleteAddRow(grvCachTinhLuong);
                        isAdd = false;
                        EnableButon();
                        LoadGridCongNhan();
                        thangtruoc = false;
                        LoadGrdCachTinhLuong();
                        break;
                    }
                case "in":
                    {
                        InBaoCao();
                        break;
                    }
                case "thoat":
                    {
                        Commons.Modules.ObjSystems.GotoHome(this);
                        break;
                    }
                case "laydlthangtruoc":
                    {
                        grdCachTinhLuong.DataSource = null;
                        thangtruoc = true;
                        LoadGridCongNhan();
                       
                        break;
                    }
                case "xoatrangnhom":
                    {
                        Validate();
                        if (grvCongNhan.HasColumnErrors) return;
                        
                        break;
                    }
                case "chontatca":
                    {
                        
                        break;
                    }
                case "bochontatca":
                    {
                        
                        break;
                    }
            }
        }

        private void InBaoCao()
        {
            DataTable dt = new DataTable();
            dt.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, "rptBCDKTangCa", Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd"),
                                            cboDonVi.EditValue, cboXiNghiep.EditValue, cboTo.EditValue, Commons.Modules.UserName, Commons.Modules.TypeLanguage));

            frmViewReport frm = new frmViewReport();
            //Convert.ToInt64(grvCongNhan.GetFocusedRowCellValue("ID_CN"))
            string tieuDe = "DANH SÁCH NHÂN VIÊN ĐĂNG KÍ TĂNG CA";
            frm.rpt = new rptDKTangCa(Convert.ToDateTime(cboNgay.EditValue), tieuDe);
            if (dt == null || dt.Rows.Count == 0) return;
            dt.TableName = "DATA";
            frm.AddDataSource(dt);
            frm.ShowDialog();
        }
        
        private void CheckAllButton(bool val)
        {
            if (val)
            {
                grvCongNhan.BeginSelection();
                grvCongNhan.ClearSelection();
                grvCongNhan.SelectRange(grvCongNhan.FocusedRowHandle, grvCongNhan.FocusedRowHandle + 1);
                grvCongNhan.EndSelection();
            }
            else
            {

            }
        }

        #region Xu ly button

        /// <summary>
        /// btn cap nhat nhom
        /// </summary>
        /// <returns></returns>
        

        /// <summary>
        /// Xoa trang nhom 
        /// </summary>
        /// <returns></returns>
        

        /// <summary>
        /// Xoa dong
        /// </summary>
        private void XoaDangKiGioLamThem()
        {
            if (grvCachTinhLuong.RowCount == 0) { Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa); return; }

            if (Commons.Modules.ObjSystems.msgHoi(Commons.ThongBao.msgXoa) == DialogResult.No) return;
            //xóa
            try
            {
                string sSql = "DELETE dbo.CN_CACH_TINH_LUONG WHERE ID_CN = " + grvCachTinhLuong.GetFocusedRowCellValue("ID_CN") +
                                                        " AND ID_CV = "
                                                        + grvCachTinhLuong.GetFocusedRowCellValue("ID_CV") +
                                                        " AND THANG = '"
                                                        + Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd")+"'";

                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                grvCachTinhLuong.DeleteSelectedRows();
            }
            catch
            {
                Commons.Modules.ObjSystems.msgChung(Commons.ThongBao.msgKhongCoDuLieuXoa);
            }
            LoadGrdCachTinhLuong();
        }

        /// <summary>
        /// Save data
        /// </summary>
        /// <returns></returns>
        private bool Savedata()
        {
            string stbCTL = "grvCachTinhLuong" + Commons.Modules.UserName;
            string sSql = "";
            try
            {
                Commons.Modules.ObjSystems.MCreateTableToDatatable(Commons.IConnections.CNStr, stbCTL, (DataTable)grdCachTinhLuong.DataSource, "") ;

               
                sSql = " DELETE FROM CN_CACH_TINH_LUONG WHERE THANG = '" + Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd") +
                       "' AND ID_CN IN (SELECT DISTINCT ID_CN FROM " + stbCTL + ")" +
                       " INSERT INTO CN_CACH_TINH_LUONG (THANG, ID_CN, ID_CV, ID_CTL) " +
                       " SELECT '"+Convert.ToDateTime(cboNgay.EditValue).ToString("yyyyMMdd")+"' THANG, ID_CN, ID_CV, ID_CTL FROM " + stbCTL+" WHERE ISNULL(ID_CTL,0)>0" ;
                       //" WHERE CHON = 'True'";


                SqlHelper.ExecuteNonQuery(Commons.IConnections.CNStr, CommandType.Text, sSql);
                Commons.Modules.ObjSystems.XoaTable(stbCTL);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                Commons.Modules.ObjSystems.XoaTable(stbCTL);
                return false;
            }
        }

        #endregion Xu ly button

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visible"></param>
        private void EnableButon()
        {
            btnALL.Buttons[0].Properties.Visible = !isAdd;
            btnALL.Buttons[1].Properties.Visible = !isAdd;
            btnALL.Buttons[3].Properties.Visible = false;
            btnALL.Buttons[5].Properties.Visible = !isAdd;

            btnALL.Buttons[6].Properties.Visible = isAdd;
            btnALL.Buttons[7].Properties.Visible = isAdd;
            btnALL.Buttons[8].Properties.Visible = isAdd;
            btnALL.Buttons[2].Properties.Visible = isAdd;
            btnALL.Buttons[4].Properties.Visible = !isAdd;

            cboNgay.Enabled = !isAdd;
            cboDonVi.Enabled = !isAdd;
            cboXiNghiep.Enabled = !isAdd;
            cboTo.Enabled = !isAdd;
        }

        private void grvData_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
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

        #region Xu Ly Ngay
        /// <summary>
        /// Load Ngay
        /// </summary>
        private void LoadNgay()
        {
            try
            {

                //ItemForDateThang.Visibility = LayoutVisibility.Never;
                DataTable dtthang = new DataTable();
                string sSql = "SELECT disTINCT SUBSTRING(CONVERT(VARCHAR(10),THANG,103),4,2) as M, RIGHT(CONVERT(VARCHAR(10),THANG,103),4) AS Y ,RIGHT(CONVERT(VARCHAR(10),THANG,103),7) AS THANG FROM dbo.CN_CACH_TINH_LUONG ORDER BY Y DESC , M DESC";
                dtthang.Load(SqlHelper.ExecuteReader(Commons.IConnections.CNStr, CommandType.Text, sSql));
                Commons.Modules.ObjSystems.MLoadXtraGrid(grdNgay, grvNgay, dtthang, false, true, true, true, true, this.Name);
                grvNgay.Columns["M"].Visible = false;
                grvNgay.Columns["Y"].Visible = false;

                cboNgay.Text = grvNgay.GetFocusedRowCellValue("THANG").ToString();
            }
            catch (Exception ex)
            {
                cboNgay.Text=  DateTime.Now.Month + "/" + DateTime.Now.Year;
            }
        }

        /// <summary>
        /// load null cboNgay
        /// </summary>
        private void LoadNull()
        {
            try
            {
                if (cboNgay.Text == "") cboNgay.Text = DateTime.Now.ToShortDateString();
            }
            catch (Exception ex)
            {
                cboNgay.Text = "";
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        /// <summary>
        /// grid view combo ngay change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grvNgay_RowCellClick(object sender, RowCellClickEventArgs e)
        {
           
        }
        #endregion



        private void grvLamThem_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            decimal idCongNhan = -1;
            DataTable dt = new DataTable();

            try
            {
                if (grvCongNhan.FocusedRowHandle >= 0)
                {
                    decimal.TryParse(grvCongNhan.GetFocusedRowCellValue("ID_CN").ToString(), out idCongNhan);
                }
                grvCachTinhLuong.SetFocusedRowCellValue("ID_CN", idCongNhan);
                grvCachTinhLuong.SetFocusedRowCellValue("THANG", Convert.ToDateTime(cboNgay.EditValue));
            }
            catch { }
        }

        private void grvLamThem_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
        }

        

        private void grvCachTinhLuong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

           
        }

        private void grvNgay_RowCellClick_1(object sender, RowCellClickEventArgs e)
        {
            try
            {
                GridView grv = (GridView)sender;
                cboNgay.Text = grvNgay.GetFocusedRowCellValue("THANG").ToString();
            }
            catch { LoadNull(); }
            cboNgay.ClosePopup();
        }

        private void calThang_DateTimeCommit(object sender, EventArgs e)
        {
            try
            {
                //cboNgay.Text = cboNgay.DateTime.ToString("MM/yyyy");
                DataTable dtTmp = Commons.Modules.ObjSystems.ConvertDatatable(grdNgay);
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

        private void grvCongNhan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            try
            {
                LoadGrdCachTinhLuong();
            }
            catch
            {

            }
        }
    }
}